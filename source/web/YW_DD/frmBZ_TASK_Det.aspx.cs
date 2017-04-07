using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Threading;
using PlatForm.DBUtility;
using PlatForm.Functions;
using PlatForm.CustomControlLib;
using System.Globalization;


// 赤几 班组任务单
public partial class YW_ZDH_frmBZ_TASK_Det : System.Web.UI.Page
{
    //websvrfunction webfun = new websvrfunction();
    string _sql;

    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sended"] != null)
        {
            if (Session["sended"].ToString() == "1" && ViewState["BackUrl"] != null)
                Response.Redirect(ViewState["BackUrl"].ToString());
        }

        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();
            lblFuncName.Text = GetLocalResourceObject("PageResource1.Title").ToString();
            btnDelFile.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "DeleteBeforeConfirm").ToString() + "');");

            if (Request["BackUrl"] != null)
                ViewState["BackUrl"] = Request["BackUrl"];
            else
                ViewState["BackUrl"] = null;

            object obj=null;

            if (Request["PackTypeNo"] != null && Request["CurLinkNo"] != null) //从工作流界面打开，肯定有PackTypeNo和CurLinkNo
            {
                ViewState["PackTypeNo"] = Request["PackTypeNo"];
                ViewState["CurLinkNo"] = Request["CurLinkNo"];
                if (Request["PackNo"] != null)
                {
                    ViewState["PackNo"] = Request["PackNo"];
                    _sql = "select f_recno from DMIS_SYS_DOC where f_packno=" + Request["PackNo"] + " and f_linkno=" + Request["CurLinkNo"];
                    ViewState["RecNo"] = DBOpt.dbHelper.ExecuteScalar(_sql);
                    if (ViewState["RecNo"] == null)
                    {
                        //JScript.Alert("业务表的记录号,请联系管理员！");
                        return;
                    }
                }
                else
                    ViewState["RecNo"] = -1;

                if (Request["CurWorkFlowNo"] != null) ViewState["CurWorkFlowNo"] = Request["CurWorkFlowNo"];
            }
            else    //从业务表查询界面打开，有PackTypeNo、TableName、RecNo
            {
                ViewState["PackTypeNo"] = Request["PackTypeNo"];
                ViewState["TableName"] = Request["TableName"];
                ViewState["RecNo"] = Request["RecNo"];
                _sql = "select F_PACKNO from DMIS_SYS_DOC where f_tablename='" + Request["TableName"].ToString() + "' and f_recno=" + ViewState["RecNo"].ToString();
                ViewState["PackNo"] = DBOpt.dbHelper.ExecuteScalar(_sql);
                if (ViewState["PackNo"] == null)
                {
                    //JScript.Alert("未找到业务号！");
                    return;
                }
                obj = DBOpt.dbHelper.ExecuteScalar("select f_no from DMIS_SYS_WORKFLOW where f_packno=" + ViewState["PackNo"].ToString() + " and f_status='在办'");
                if (obj != null)  //在办的业务
                    ViewState["CurWorkFlowNo"] = obj;
                else
                {
                    _sql = "select max(f_no) from DMIS_SYS_WORKFLOW where f_packno=" + ViewState["PackNo"].ToString() + " and f_status='完成'";   //归档的业务，最后的环节
                    ViewState["CurWorkFlowNo"] = DBOpt.dbHelper.ExecuteScalar(_sql);
                }
                ViewState["CurLinkNo"] = DBOpt.dbHelper.ExecuteScalar("select f_flowno from DMIS_SYS_WORKFLOW where f_no=" + ViewState["CurWorkFlowNo"].ToString());
            }

            //找当前环节对应的文档 
            _sql = "select a.f_no,a.f_name,a.f_tablename,a.f_reportfile from dmis_sys_doctype a,DMIS_SYS_WK_LINK_DOCTYPE b where a.f_no=b.F_DOCTYPENO and b.f_packtypeno=" +
                ViewState["PackTypeNo"].ToString() + " and b.F_LINKNO=" + ViewState["CurLinkNo"].ToString();
            DataTable temp = DBOpt.dbHelper.GetDataTable(_sql);
            if (temp == null || temp.Rows.Count < 1)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkNoDoc").ToString());  //"没有找到对应的文档！"
                return;
            }
            else
            {
                ViewState["DocTypeNo"] = temp.Rows[0][0];  //文档编号
                ViewState["DocTypeName"] = temp.Rows[0][1];  //文档名称
                ViewState["TableName"] = temp.Rows[0][2];  //文档对应的数据库表名称
                ViewState["REPORT_ID"] = temp.Rows[0][3];  //报表编号
            }

            int counts;
            btnSave.Enabled = false;
            btnSaveClose.Enabled = false;
            btnSend.Enabled = false;
            btnWithdraw.Enabled = false;
            btnAccept.Enabled = false;

            btnWithdraw.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "WkConfirmWithdraw").ToString() + "');");  //确定要退回到上一步?
            btnAccept.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "WkConfirmAccept").ToString() + "');");  //确定要接单?

            if (SetRight.IsAdminitrator(Session["MemberID"].ToString()))  //管理员开放修改业务的数据的权限，但不能发送、回退。不参与流转的处理
            {
                btnSave.Enabled =true;
                btnSaveClose.Enabled =true;
                WebWorkFlow.SetAllWebControlEnable(this.Page, Convert.ToInt16(ViewState["PackTypeNo"]), "T_BZ_TASK");
                WebWorkFlow.SetAllWebControlEnable(this.Page, Convert.ToInt16(ViewState["PackTypeNo"]), "T_BZ_TASK_CHEECK");
                WebWorkFlow.SetAllWebControlEnable(this.Page, Convert.ToInt16(ViewState["PackTypeNo"]), "T_BZ_TASK_DISPOSE");
            }
            else
            {
                if (Session["Oper"] != null)
                {
                    if (Convert.ToInt16(Session["Oper"]) > 0)  //有权限修改
                    {
                        WebWorkFlow.SetWebControlRight(this.Page, Session["RoleIDs"].ToString(), Convert.ToInt16(ViewState["PackTypeNo"]), Convert.ToInt16(ViewState["CurLinkNo"]), ViewState["TableName"].ToString());
                        btnSave.Enabled =true;
                        btnSaveClose.Enabled =true;
                        btnSend.Enabled =true;
                        btnWithdraw.Enabled =true;

                        //判断文件上传删除的权限，只要能修改委托书基本资料，则可以
                        if (hcbSTATION.Enabled)
                        {
                            btnAddFile.Enabled =true;
                            btnDelFile.Enabled =true;
                        }

                        //如果是最后一步，则弹出是否要归档的窗口
                        string sMainer = "";
                        if(ViewState["CurWorkFlowNo"]!=null) 
                            obj = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVER from DMIS_SYS_WORKFLOW where F_NO=" + ViewState["CurWorkFlowNo"]);
                        if (obj != null)
                            sMainer = obj.ToString();
                        else
                            btnSend.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "WkConfirmSubmit").ToString() + "');");  //确定要提交到下一步?(新建业务时。)

                        if (sMainer == Session["MemberName"].ToString())
                        {
                            int iCat = -1;
                            obj = DBOpt.dbHelper.ExecuteScalar("SELECT F_FLOWCAT FROM DMIS_SYS_FLOWLINK WHERE F_NO=" + ViewState["CurLinkNo"]);
                            if (obj != null) iCat = Convert.ToInt16(obj);

                            if (iCat == 2)
                                btnSend.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "WkConfirmFinish").ToString() + "');");//该项目已办理完成,是否要归档?
                            else
                                btnSend.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "WkConfirmSubmit").ToString() + "');");//确定要提交到下一步
                        }
                    }
                    else  //待办的状态下处理接单按钮
                    {
                        int flag = -1;  //判断是否接单
                        obj = DBOpt.dbHelper.ExecuteScalar("select f_working from DMIS_SYS_WORKFLOW where F_NO=" + ViewState["CurWorkFlowNo"]);
                        if (obj != null) flag = Convert.ToInt16(obj);
                        if (flag == 0)   //待办状态
                        {
                            string zbr = "";
                            obj = DBOpt.dbHelper.ExecuteScalar("select f_receiver from DMIS_SYS_WORKFLOW where F_NO=" + ViewState["CurWorkFlowNo"]);
                            if (obj != null) zbr = obj.ToString().Trim();
                            if (zbr == "" || zbr == Session["MemberName"].ToString()) btnAccept.Enabled =true;
                        }
                        else if (flag == 1)    //从办人员点击进入状态,只能提交
                        {
                            if (WebWorkFlow.IsCongBanRen(ViewState["PackNo"].ToString(), ViewState["CurWorkFlowNo"].ToString(), Session["MemberName"].ToString()))
                                btnSend.Enabled =true;
                        }
                        else
                        {
                            btnAccept.Enabled = false;
                        }
                    }
                }
            }

            FillDropDownList.FillHtmlCombxByTable(ref hcbSTATION, "DMIS_SYS_STATION", "NAME", "TID");
            initFile();　//显示上传的文件
            string tid;

            if (ViewState["PackNo"] != null)
            {
                //任务单内容，只有一条，故用PACK查找
                if (DBOpt.dbHelper.IsExist("T_BZ_TASK", "PACK_NO=" + ViewState["PackNo"].ToString()))
                    CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_BZ_TASK", "PACK_NO=" + ViewState["PackNo"].ToString());

                if (DBOpt.dbHelper.IsExist("T_BZ_TASK_CHEECK", "PACK_NO=" + ViewState["PackNo"].ToString()))
                    CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_BZ_TASK_CHEECK", "PACK_NO=" + ViewState["PackNo"].ToString());

                //任务处理情况，一个业务有多条记录，查找当前处理的步骤对应的文档是否在业务表是存在记录

                string tableName = DBOpt.dbHelper.ExecuteScalar("select f_tablename from DMIS_SYS_DOC where f_packno=" + ViewState["PackNo"].ToString() + " and f_linkno=" + ViewState["CurLinkNo"].ToString()).ToString();
                tid = DBOpt.dbHelper.ExecuteScalar("select f_recno from DMIS_SYS_DOC where f_packno=" + ViewState["PackNo"].ToString() + " and f_linkno=" + ViewState["CurLinkNo"].ToString()).ToString();
                obj = DBOpt.dbHelper.ExecuteScalar("select f_name from dmis_sys_flowlink where f_no=" + ViewState["CurLinkNo"].ToString());
                if (tableName == "T_BZ_TASK_DISPOSE")    //成员处理环节
                {
                    CustomControlFill.CustomControlFillByTableAndWhere(this.Page, tableName, "TID=" + tid);
                    tdCurDispoase.InnerText =  obj.ToString();
                    if (txtF_FLOWNAME.Text == "") txtF_FLOWNAME.Text = obj.ToString();

                }
                else  //不是处理环节
                {
                    tdCurDispoase.InnerText = "";
                }
                //填充已经完成的处理步骤数据
                initData();

                //2009-3-6 每个业务表保存业务号,故用一个控件来保存就可以了.
                if (txtPACK_NO.Text.Trim() == "") txtPACK_NO.Text = ViewState["PackNo"].ToString();
            }


            //确定当前步骤是否是第一步

            obj = DBOpt.dbHelper.ExecuteScalar("select f_flowcat from dmis_sys_flowlink where f_no=" + ViewState["CurLinkNo"].ToString());
            if (!obj.ToString().Equals("0"))  //不是第一步
            {
                //设置初始人名
                if (!SetRight.IsAdminitrator(Session["MemberID"].ToString())) //不是管理员才设置初始人员及日期,否则会把管理员的名称也赋值给相应的控件
                {
                    string[] name ={ "txtSTARTER", "txtCHECKER",  "txtFZR" };
                    TextBox txt;
                    for (int i = 0; i < name.Length; i++)
                    {
                        txt = (TextBox)Page.FindControl(name[i]);
                        if (txt == null) continue;
                        if (!txt.ReadOnly && txt.Text == "") txt.Text = Session["MemberName"].ToString();
                    }
                    //设置初始日期
                    string[] date ={ "wdlSTARTTIME", "wdlCHECK_DATE", "wdlDISPOSE_STARTTIME" };
                    WebDate wdl;
                    for (int i = 0; i < date.Length; i++)
                    {
                        wdl = (WebDate)Page.FindControl(date[i]);
                        if (wdl == null) continue;
                        if (wdl.Enabled && wdl.Text == "") wdl.setTime(DateTime.Now);
                    }
                }

                //设置从办人员的姓名，这样不用两次输入办理人员的姓名
                DataTable memberStatus = DBOpt.dbHelper.GetDataTable("select f_receiver from dmis_sys_memberstatus where f_packno=" + ViewState["PackNo"].ToString() + " and f_workflowno=" + ViewState["CurWorkFlowNo"].ToString());
                for (int i = 0; i < memberStatus.Rows.Count; i++)
                {
                    if (i == 0)
                        txtJDR1.Text = memberStatus.Rows[i][0].ToString();
                    else if (i == 1)
                        txtJDR2.Text = memberStatus.Rows[i][0].ToString();
                    else if (i == 2)
                        txtJDR3.Text = memberStatus.Rows[i][0].ToString();
                }
            }
            else   //开始步骤
            {
                tdCurDispoase.InnerText = "";

                if (ViewState["RecNo"].ToString() == "-1")  //新增任务书的情况
                {
                    _sql = "select count(*) from T_BZ_TASK where to_char(STARTTIME,'YYYYMM')='" + DateTime.Now.ToString("yyyyMM") + "'";
                    counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql)) + 1;
                    txtPH.Text = DateTime.Now.ToString("yyyyMM") + counts.ToString("000");
                    txtTID.Text = "";
                    txtSTARTER.Text = Session["MemberName"].ToString();
                    wdlSTARTTIME.setTime(DateTime.Now);
                }
                else   //第二次打开任务书，则还是第一步的步骤，则显示任务书的数据
                {
                    CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_BZ_TASK", "TID=" + ViewState["RecNo"].ToString());
                }
            }
        }
    }

    private void initData()
    {
        //_sql = "select * from T_ZDH_TASK_DISPOSE where PACK_NO=" + ViewState["PackNo"].ToString() + " and tid in (select f_recno from dmis_sys_doc where f_packno=" +
        //    ViewState["PackNo"].ToString() + " and f_tablename='T_ZDH_TASK_DISPOSE' and f_linkno in(select f_flowno from dmis_sys_workflow where f_packno=" + ViewState["PackNo"].ToString() 
        //    + " and (f_status='在办' or f_status='退回')))  order by STARTTIME";
        _sql = "select * from T_BZ_TASK_DISPOSE where PACK_NO=" + ViewState["PackNo"].ToString() + " order by STARTTIME";
        DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
        wgvCompleteTask.DataSource = dt;
        wgvCompleteTask.DataBind();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ViewState["REPORT_ID"] == null || ViewState["REPORT_ID"].ToString().Trim() == "")
        {
            Response.Write("<script language=javascript> alert('" + GetGlobalResourceObject("WebGlobalResource", "NoReportMessage").ToString() + "')</script");
            return;
        }
        string para, tt;

        para = txtPACK_NO.Text;
        tt = "window.open('../SYS_Common/frmCellReportDisplay.aspx?ReportID=" + ViewState["REPORT_ID"].ToString() + "&Values=" + para + "','报表打印')";

        Response.Write("<script language=javascript>");
        Response.Write(tt);
        Response.Write("</script>");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string  re = "";
        //系统管理员只修改所有文档对就的业务表的数据,但不产生新的记录
        if (SetRight.IsAdminitrator(Session["MemberID"].ToString()))
        {
            DataTable doc = DBOpt.dbHelper.GetDataTable("select distinct f_tablename,f_recno from dmis_sys_doc where f_packno=" + ViewState["PackNo"].ToString());
            for (int i = 0; i < doc.Rows.Count; i++)
            {
                if (doc.Rows[i][0].ToString() == "T_BZ_TASK_DISPOSE" && txtDISPOSE_TID.Text == "") continue;
                if (doc.Rows[i][0].ToString() == "T_BZ_TASK" && txtTID.Text == "") continue;
                if (doc.Rows[i][0].ToString() == "T_BZ_TASK_CHEECK" && txtCHECK_TID.Text == "") continue;

                re = ControlWebValidator.Validate(this.Page, doc.Rows[i][0].ToString());
                if (re != "")
                {
                    JScript.Alert(re);
                    tdMessage.InnerText = re;
                    return;
                }
                
                re = CustomControlSave.CustomControlSaveByTableNameReturnS(this.Page, doc.Rows[i][0].ToString());
                if (re != "")
                {
                    JScript.Alert(re);
                    tdMessage.InnerText = re;
                    return;
                }
                tdMessage.InnerText = "";
            }
            return;
        }

        bool isAdd = false;
        bool isStartLink = false;   //是否起始环节
        object obj;
        TextBox txtPrimaryField = null;
        Control con;
        //此业务对应的所有数据库表，一个环节只能保存一个数据库表
        DataTable tables = DBOpt.dbHelper.GetDataTable("select f_tablename from dmis_sys_doctype where f_doccat='业务表' and f_packtypeno=" + ViewState["PackTypeNo"].ToString());
        for (int i = 0; i < tables.Rows.Count; i++)
        {
            if (tables.Rows[i][0].ToString() == ViewState["TableName"].ToString())
            {
                con = Page.FindControl(DBOpt.dbHelper.ExecuteScalar("select a.custom_control_name from DMIS_SYS_COLUMNS a,DMIS_SYS_TABLES b where isprimary=1 and a.table_id=b.id and b.name='" + ViewState["TableName"].ToString() + "'").ToString());
                if (con == null)
                {
                    //JScript.Alert("对不起！无法找到表" + ViewState["TableName"].ToString() + "的主键对应的控件！");
                    //tdMessage.InnerText = "对不起！无法找到表" + ViewState["TableName"].ToString() + "的主键对应的控件！";
                    return;
                }
                txtPrimaryField = (TextBox)con;
                break;
            }
        }
        if (txtPrimaryField.Text == "") isAdd = true;

        //保存业务表的数据
        re = ControlWebValidator.Validate(this.Page, ViewState["TableName"].ToString());
        if (re != "")
        {
            JScript.Alert(re);
            tdMessage.InnerText = re;
            return;
        }
        tdMessage.InnerText = "";

        re = CustomControlSave.CustomControlSaveByTableNameReturnS(this.Page, ViewState["TableName"].ToString());
        if (re != "")
        {
            JScript.Alert(re);
            tdMessage.InnerText = re;
            return;
        }
        tdMessage.InnerText = "";

        _sql = "select f_flowcat from dmis_sys_flowlink where f_no=" + ViewState["CurLinkNo"].ToString();
        obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        if(obj!=null && obj.ToString()=="0") isStartLink = true;

        //新建业务数据并且是起始环节时，创建工作流数据
        if (isAdd && isStartLink)
        {
            uint packNo = 0;
            string station = "";
            string startTime, endTime;
            startTime = "";
            endTime = "";
            station = hcbSTATION.SelectedText;
            if (wdlSTARTTIME.getTime().ToString("yyyy-MM-dd") != "1900-01-01" && wdlENDTIME.getTime().ToString("yyyy-MM-dd") != "1900-01-01")
            {
                startTime = wdlSTARTTIME.getTime().ToString("dd-MM-yyyy HH:mm");
                endTime = wdlENDTIME.getTime().ToString("dd-MM-yyyy HH:mm");
            }

            if (WebWorkFlow.CreatePack(Convert.ToInt32(ViewState["PackTypeNo"]), txtCONTENT.Text, Session["MemberName"].ToString(), ref packNo, station,startTime,endTime) < 0)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkInsertInstanceFailMessage").ToString());  //创建业务数据失败！
                tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "WkInsertInstanceFailMessage").ToString();
                return;
            }
            tdMessage.InnerText = "";

            int iFirstFlowNo = -1;
            int iWorkFlowNo = -1;
            iFirstFlowNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_NO from DMIS_SYS_FLOWLINK where F_PACKTYPENO=" + ViewState["PackTypeNo"] + " and F_FLOWCAT=0"));

            bool bCreated = WebWorkFlow.CreateFlow(Convert.ToInt16(packNo), ref iWorkFlowNo, Session["MemberName"].ToString(), iFirstFlowNo, "", Session["MemberName"].ToString(), "", txtPrimaryField.Text);
            if (!bCreated)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkInsertTaskFailMessage").ToString());//创建工作流数据失败！
                tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "WkInsertTaskFailMessage").ToString();
                return;
            }
            tdMessage.InnerText = "";

            ViewState["PackNo"] = packNo;
            ViewState["CurWorkFlowNo"] = iWorkFlowNo;
            ViewState["CurLinkNo"] = iFirstFlowNo;
            ViewState["RecNo"] = txtPrimaryField.Text;

            //增加PACK_NO值
            //_sql = "update " + ViewState["TableName"].ToString() + " set PACK_NO=" + packNo + " where TID=" + txtPrimaryField.Text;
            //DBOpt.dbHelper.ExecuteSql(_sql);
        }

        //if (isAdd)   //增加总结验收时的情况。
        //{
            //2009-3-6所有步骤都要更新业务表中的PACK_NO,故保存时,都保存一下当前业务表中相应的业务号.
            _sql = "update " + ViewState["TableName"].ToString() + " set PACK_NO=" + ViewState["PackNo"].ToString() + " where TID=" + txtPrimaryField.Text;
            DBOpt.dbHelper.ExecuteSql(_sql);
        //}

        //只有用户保存了，确认它接收此任务了。
        //修改已经接收的状态，使此业务不能抽回
        if (ViewState["CurWorkFlowNo"] != null && Convert.ToInt16(ViewState["CurWorkFlowNo"]) > 0)
        {
            obj = DBOpt.dbHelper.ExecuteScalar("select f_working from dmis_sys_workflow where f_no=" + ViewState["CurWorkFlowNo"]);
            if (obj == null || Convert.ToInt16(obj) == 0)  //已经接收的，允许再接收
            {
                _sql = "update dmis_sys_workflow set f_working=1,f_receiver='" + Session["MemberName"].ToString() + "',f_receivedate='" +
                    DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "' where f_no=" + ViewState["CurWorkFlowNo"];
                DBOpt.dbHelper.ExecuteSql(_sql);
            }
        }
    }

    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        btnSave_Click(null, null);
        if (tdMessage.InnerText != "") return;

        if (ViewState["BackUrl"] != null)
            Response.Redirect(ViewState["BackUrl"].ToString());
        else
            Response.Write("<script language=javascript>window.close();</script>");
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["BackUrl"] != null)
            Response.Redirect(ViewState["BackUrl"].ToString());
        else
            Response.Write("<script language=javascript>window.close();</script>");
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        //提交前先保存
        btnSave_Click(null, null);
        if (tdMessage.InnerText != "") return;
        string returnString = "";
        object obj;

        //发送前判断汇集环节的所有分支是否都到达此节点
        obj = DBOpt.dbHelper.ExecuteScalar("select f_nodetype from dmis_sys_flowlink where f_no=" + ViewState["CurLinkNo"].ToString());
        if (obj != null && Convert.ToInt16(obj) == 2)
        {
            returnString = WebWorkFlow.InfluxTacheSendCondition(Convert.ToInt16(ViewState["PackTypeNo"]), Convert.ToInt16(ViewState["PackNo"]), Convert.ToInt16(ViewState["CurLinkNo"]), Convert.ToInt16(ViewState["CurWorkFlowNo"]));
            if (returnString.Length > 0)
            {
                JScript.Alert(returnString);
                tdMessage.InnerText = returnString;
                return;
            }
        }
        //发送前条件判断
        returnString = WebWorkFlow.TacheSendCondition(ViewState["PackTypeNo"].ToString(), ViewState["CurLinkNo"].ToString(), ViewState["PackNo"].ToString(), ViewState["CurWorkFlowNo"].ToString(), ViewState["TableName"].ToString(), ViewState["RecNo"].ToString());
        if (returnString.Length > 0)
        {
            JScript.Alert(returnString);
            tdMessage.InnerText = returnString;
            return;
        }

        string sMainer = "";
        obj = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVER from DMIS_SYS_WORKFLOW where F_NO=" + ViewState["CurWorkFlowNo"]);
        if (obj != null)
            sMainer = obj.ToString();

        if (sMainer == Session["MemberName"].ToString())
        {
            int iCat = -1;
            obj = DBOpt.dbHelper.ExecuteScalar("SELECT F_FLOWCAT FROM  DMIS_SYS_FLOWLINK WHERE F_NO=" + ViewState["CurLinkNo"]);
            if (obj != null) iCat = Convert.ToInt16(obj);

            if (iCat == 2)
            {
                int iFno = WebWorkFlow.EndFlow(Convert.ToInt16(ViewState["PackNo"]), Convert.ToInt16(ViewState["CurWorkFlowNo"]), Session["MemberName"].ToString());
                if (iFno == -1)
                {
                    JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkFinishFailMessage").ToString());//此业务归档失败，请联系统管理员！
                    return;
                }
                //2010-04-26，出现分支节点有退回的情况时，一个任务退回到分支节点，另一任务到了验收环节，
                //在任务没有结束时，也可以原先的程序也可以归档，故修改WebWorkFlow.EndFlow（）的代码，提示一下
                //还有任务没有完成，不允许归档
                else if (iFno == -2)
                {
                    JScript.Alert("-2");//此业务还有未完成的任务，请在流程图中查询未完成的任务！
                    return;
                }
                else
                {
                    //JScript.Alert("此业务归档成功！");
                    Response.Redirect(ViewState["BackUrl"].ToString());
                }
            }
            else
            {
                string paras;
                //发送岗位时，派工环节，弹出发送窗口
                if (WebWorkFlow.IsAssignTache(Convert.ToInt16(ViewState["PackTypeNo"]), Convert.ToInt16(ViewState["CurLinkNo"])))
                {
                    paras = "PackTypeNo=" + ViewState["PackTypeNo"] + "&CurLinkNo=" + ViewState["CurLinkNo"] + "&PackNo=" + ViewState["PackNo"] +
                        "&CurWorkFlowNo=" + ViewState["CurWorkFlowNo"] + "&TableName=" + ViewState["TableName"].ToString() + "&RecNo=" + ViewState["RecNo"].ToString();
                    Session["sended"] = 0;
                    JScript.OpenWindow("../SYS_WorkFlow/AssignTaskWindow.aspx?" + paras, "发送窗", "scrollbars=yes,width=700,height=500,top=20,left=100");
                    //Response.Write(webfun.OpenWin("../SYS_WorkFlow/AssignTaskWindow.aspx?" + paras, "发送窗", "scrollbars=yes,width=700,height=480,top=20,left=100"));
                }
                else  //非派工环节，直接发送即可
                {
                    int curTaskID = Convert.ToInt16(ViewState["CurWorkFlowNo"]);
                    if (WebWorkFlow.DirectCreateFlow(Convert.ToInt16(ViewState["PackNo"]), ref curTaskID, Session["MemberName"].ToString()))
                    {
                        //统计实际工作时间
                        //1、根据工作流的相关表DMIS_SYS_WORKFLOW DMIS_SYS_MEMBERSTATUS来统计实际工作时间
                        //WebWorkFlow.StatisicFactuslTimes(ViewState["PackTypeNo"].ToString(), ViewState["CurLinkNo"].ToString(),
                        //    ViewState["PackNo"].ToString(), ViewState["CurWorkFlowNo"].ToString(), ViewState["TableName"].ToString(), ViewState["RecNo"].ToString());

                        //2、根据业务表保存的人名来统计实际工作时间
                        string[] members = new string[4];
                        members[0] = txtFZR.Text;
                        members[1] = txtJDR1.Text;
                        members[2] = txtJDR2.Text;
                        members[3] = txtJDR3.Text;
                        if (ViewState["CurLinkNo"].ToString() == "49" || ViewState["CurLinkNo"].ToString() == "50")
                        {
                            WebWorkFlow.StatisicFactuslTimes(ViewState["PackTypeNo"].ToString(), ViewState["PackNo"].ToString(),
                                ViewState["CurLinkNo"].ToString(), ViewState["TableName"].ToString(), ViewState["RecNo"].ToString(), "否", members);

                        }
                        else if (ViewState["CurLinkNo"].ToString() == "47" || ViewState["CurLinkNo"].ToString() == "48")  //远动的才统计下站补助
                        {
                            WebWorkFlow.StatisicFactuslTimes(ViewState["PackTypeNo"].ToString(), ViewState["PackNo"].ToString(),
                                ViewState["CurLinkNo"].ToString(), ViewState["TableName"].ToString(), ViewState["RecNo"].ToString(), "是", members);
                        }

                        Response.Redirect(ViewState["BackUrl"].ToString());
                    }
                    else
                    {
                        JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkSubmitFailMessage").ToString());//"此任务发送失败，请联系统管理员！"
                        return;
                    }
                }
            }
        }
        else
        {
            //结束从办人员的办理状态
            if (WebWorkFlow.EndMemberStatus(Convert.ToInt16(ViewState["PackNo"]), Convert.ToInt16(ViewState["CurWorkFlowNo"]), Session["MemberName"].ToString()))
            {
                //JScript.Alert("提交成功！");
                Response.Redirect(ViewState["BackUrl"].ToString());
            }
            else
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkSubmitFailMessage").ToString());  //更新状态失败！
        }
    }

    //退回操作  
    protected void btnWithdraw_Click(object sender, EventArgs e)
    {
        //退回前先保存
        btnSave_Click(null, null);
        if (tdMessage.InnerText != "") return;

        _sql = "SELECT F_PREFLOWNO FROM DMIS_SYS_WORKFLOW WHERE F_PACKNO=" + ViewState["PackNo"]
        + " AND F_NO=" + ViewState["CurWorkFlowNo"] + " AND F_RECEIVER='" + Session["MemberName"] + "'";
        object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        if (obj == null)
        {
            //Response.Write("<script language=javascript>alert('只有主办人使用');</script>");
            return;
        }
        if (Convert.ToInt16(obj) < 0)
        {
            Response.Write("<script language=javascript>alert('" + GetGlobalResourceObject("WebGlobalResource", "WkFirstStepNoWithdraw").ToString() + "');</script>");  //流程的开始步骤不允许退回
            return;
        }

        //判断分支节点能否退回。
        if (!WebWorkFlow.IsCanWithdraw(Convert.ToInt16(ViewState["PackTypeNo"]), Convert.ToInt16(ViewState["PackNo"]), Convert.ToInt16(ViewState["CurLinkNo"]), Convert.ToInt16(ViewState["CurWorkFlowNo"])))
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkHasNextWorkingStepNoWithdraw").ToString());  //"还有下级任务在处理，不允许退回"
            return;
        }

        string paras;
        paras = "PackTypeNo=" + ViewState["PackTypeNo"] + "&CurLinkNo=" + ViewState["CurLinkNo"] +
            "&PackNo=" + ViewState["PackNo"] + "&CurWorkFlowNo=" + ViewState["CurWorkFlowNo"] + "&PreCurWorkFlowNo=" + obj.ToString();
        Response.Write("<script language=javascript>");
        Response.Write("window.open('../SYS_WorkFlow/InstanceWithdrawPopMessage.aspx?" + paras + "','退回'" +
            ",'height=200,width=440,top=100,left=100,scrollbars=no,resizable=yes');");
        Response.Write("</script>");
    }


    //在待办状态下接单
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        object obj = DBOpt.dbHelper.ExecuteScalar("select f_planday from dmis_sys_flowlink where f_no=" + ViewState["CurLinkNo"]);
        string finishedTime = "";
        if (obj != null) finishedTime = WebWorkFlow.GetLastTime(DateTime.Now, Convert.ToUInt32(obj));

        _sql = "update dmis_sys_workflow set f_working=1,f_receiver='" + Session["MemberName"].ToString() + "',f_receivedate='" + DateTime.Now.ToString("dd-MM-yyyy HH:mm")
            + "',F_LAST_FINISHED_TIME='" + finishedTime + "' where f_no=" + ViewState["CurWorkFlowNo"];
        if (DBOpt.dbHelper.ExecuteSql(_sql) < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkAcceptTaskFailMessage").ToString());  //"接单失败！"
            return;
        }
        Session["Oper"] = 1;
        Response.Redirect(Page.Request.RawUrl);
    }

    protected void lbnView_Click(object sender, EventArgs e)
    {
        if (sender == null) return;
        LinkButton lbn = (LinkButton)sender;
        int index = Convert.ToInt16(lbn.ID[lbn.ID.Length - 1].ToString()) - 1;  //取最后一个字符,数字只有一位
        MultiView1.ActiveViewIndex = index;
        string[] Cells = { "Cell1", "Cell2", "Cell3", "Cell4" };
        HtmlTableCell htc;
        for (int i = 0; i < Cells.Length; i++)
        {
            htc = (HtmlTableCell)Page.FindControl(Cells[i]);
            if (index == i)
                htc.Attributes["class"] = "SelectedTopBorder";
            else
                htc.Attributes["class"] = "TopBorder";

            if (i != 3)   //2009-3-10  莫玲要求点击其它标签页时，附件恢复。
            {
                initFile();
            }
        }
    }

    protected void btnAddFile_Click(object sender, EventArgs e)
    {
        if (ViewState["PackNo"] == null || Convert.ToInt16(ViewState["PackNo"]) < 0)
        {
            //JScript.Alert("请先保存任务再添加附件!");
            return;
        }
        Response.Write("<script language=javascript>window.open('../SYS_WorkFlow/UpLoad.aspx?PackNo=" + ViewState["PackNo"] + "','上传文件','height=358,width=600,scrollbars=yes,resizable=yes');</script>");
        //Response.Write(webfun.OpenWin("../SYS_WorkFlow/UpLoad.aspx?PackNo=" + ViewState["PackNo"], "上传文件", ""));
    }

    protected void btnDelFile_Click(object sender, EventArgs e)
    {
        if (dltFiles.Items.Count < 1) return;

        if (dltFiles.SelectedItem == null)   //没有数据时，不加上面一条语句，则出错。
        {
            //JScript.Alert("请先选择要删除的文件！");
            return;
        }
        //先删除文件
        string filename = DBOpt.dbHelper.ExecuteScalar("select F_FILENAME from DMIS_SYS_WK_FILE where F_NO=" + dltFiles.DataKeys[dltFiles.SelectedIndex].ToString()).ToString();
        string mapname = Page.MapPath("..\\upload\\");
        if (System.IO.File.Exists(mapname + filename)) System.IO.File.Delete(mapname + filename);
        //再删除数据库中相应的记录
        _sql = "delete from DMIS_SYS_WK_FILE where F_NO=" + dltFiles.DataKeys[dltFiles.SelectedIndex].ToString();
        DBOpt.dbHelper.ExecuteSql(_sql);
        initFile();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        initFile();
    }
    
    private void initFile()
    {
       int packNO;
       if (ViewState["PackNo"] != null && int.TryParse(ViewState["PackNo"].ToString(), out packNO))
       {
           DataTable dtFile;
           dtFile = DBOpt.dbHelper.GetDataTable("select * from dmis_sys_wk_file where F_PACKNO= " + ViewState["PackNo"]);
           dltFiles.DataSource = dtFile;
           dltFiles.DataBind();
           dltFiles.SelectedIndex = -1;
           childWin.Attributes["src"] = "";
       }
    }

    protected void dltFiles_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            dltFiles.SelectedIndex = e.Item.ItemIndex;
            HiddenField h = (HiddenField)dltFiles.Items[e.Item.ItemIndex].FindControl("hfdFileName");
            childWin.Attributes["src"] = @"..\upload\" + h.Value;
        }
    }

    public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
    {
        try
        {
            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(myFile, System.Text.Encoding.UTF8);
            try
            {
                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;
                long fileLength = myFile.Length;
                long startBytes = 0;

                int pack = 10240; //10K bytes
                int sleep = (int)Math.Floor((decimal)1000 * pack / _speed) + 1;
                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                }
                _Response.AddHeader("Connection", "Keep-Alive");
                _Response.ContentType = "application/octet-stream";
                _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int)Math.Floor((decimal)(fileLength - startBytes) / pack) + 1;

                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(pack));
                        Thread.Sleep(sleep);
                    }
                    else
                    {
                        i = maxCount;
                    }
                }
                _Response.Flush();
            }
            catch
            {
                return false;
            }
            finally
            {
                br.Close();
                myFile.Close();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

    protected void btnDown_Click(object sender, EventArgs e)
    {
        if (dltFiles == null) return;
        if (dltFiles.SelectedItem == null)
        {
            //JScript.Alert("请先选择要下载的附件!");
            return;
        }
        HiddenField hf = (HiddenField)dltFiles.SelectedItem.Controls[5];
        ResponseFile(Request, Response, hf.Value, Server.MapPath(@"..\upload\" + hf.Value), 10240);
    }

}

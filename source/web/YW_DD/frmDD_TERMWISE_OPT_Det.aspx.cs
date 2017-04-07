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
using PlatForm.DBUtility;
using PlatForm.Functions;
using PlatForm.CustomControlLib;
using System.Globalization;
using System.Threading;

public partial class YW_DD_frmDD_TERMWISE_OPT_Det : System.Web.UI.Page
{
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

            if (Request["BackUrl"] != null)
                ViewState["BackUrl"] = Server.UrlDecode(Request["BackUrl"]);
            else
                ViewState["BackUrl"] = null;

            object obj = null;

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
                if (ViewState["PackNo"] != null)
                {
                    obj = DBOpt.dbHelper.ExecuteScalar("select f_no from DMIS_SYS_WORKFLOW where f_packno=" + ViewState["PackNo"].ToString() + " and f_status='1'");
                    if (obj != null)  //在办的业务
                        ViewState["CurWorkFlowNo"] = obj;
                    else
                    {
                        _sql = "select max(f_no) from DMIS_SYS_WORKFLOW where f_packno=" + ViewState["PackNo"].ToString() + " and f_status='2'";   //归档的业务，最后的环节
                        ViewState["CurWorkFlowNo"] = DBOpt.dbHelper.ExecuteScalar(_sql);
                    }
                    ViewState["CurLinkNo"] = DBOpt.dbHelper.ExecuteScalar("select f_flowno from DMIS_SYS_WORKFLOW where f_no=" + ViewState["CurWorkFlowNo"].ToString());
                }
                else    //查看历史数据，不能操作任何
                {
                    btnSave.Enabled = false;
                    btnSaveClose.Enabled = false;
                    btnSend.Enabled = false;
                    btnWithdraw.Enabled = false;
                    btnAccept.Enabled = false;
                    CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_DD_TERMWISE_OPT_HEAD", "TID=" + ViewState["RecNo"].ToString());
                    return;
                }
            }

            //找当前环节对应的文档 
            _sql = "select a.f_no,a.f_name,a.f_tablename,a.f_reportfile from dmis_sys_doctype a,DMIS_SYS_WK_LINK_DOCTYPE b where a.f_no=b.F_DOCTYPENO and a.f_packtypedef=1 and b.f_packtypeno="
                      + ViewState["PackTypeNo"].ToString() + " and b.F_LINKNO=" + ViewState["CurLinkNo"].ToString();
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

            if (SetRight.IsAdminitrator(Session["MemberID"].ToString()))  //管理员开放本业务类型所涉及的所有业务表修改数据的权限，但不能发送、回退。不参与流转的处理
            {
                btnSave.Enabled = true;
                btnSaveClose.Enabled = true;
                _sql = "select f_tablename from dmis_sys_doctype where f_packtypeno=" + ViewState["PackTypeNo"].ToString();  //本业务所涉及到的所有业务表
                DataTable AllTables = DBOpt.dbHelper.GetDataTable(_sql);
                for (int i = 0; i < AllTables.Rows.Count; i++)
                    WebWorkFlow.SetAllWebControlEnable(this.Page, Convert.ToInt16(ViewState["PackTypeNo"]), AllTables.Rows[i][0].ToString());
            }
            else
            {
                if (Session["Oper"] != null)
                {
                    if (Convert.ToInt16(Session["Oper"]) > 0)  //有权限修改
                    {
                        WebWorkFlow.SetWebControlRight(this.Page, Session["RoleIDs"].ToString(), Convert.ToInt16(ViewState["PackTypeNo"]), Convert.ToInt16(ViewState["CurLinkNo"]), ViewState["TableName"].ToString());
                        btnSave.Enabled = true;
                        btnSaveClose.Enabled = true;
                        btnSend.Enabled = true;
                        btnWithdraw.Enabled = true;

                        //如果是最后一步，则弹出是否要归档的窗口
                        string sMainer = "";
                        if (ViewState["CurWorkFlowNo"] != null)
                            obj = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVER from DMIS_SYS_WORKFLOW where F_NO=" + ViewState["CurWorkFlowNo"]);
                        if (obj != null)
                            sMainer = obj.ToString();
                        else
                            btnSend.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "WkConfirmSubmit").ToString() + "');");  //确定要提交到下一步?(新建业务时。)

                        if (sMainer == Session["MemberName"].ToString())
                        {
                            int iCat = -1;
                            if (ViewState["CurLinkNo"] != null)
                            {
                                obj = DBOpt.dbHelper.ExecuteScalar("SELECT F_FLOWCAT FROM DMIS_SYS_FLOWLINK WHERE F_NO=" + ViewState["CurLinkNo"]);
                                if (obj != null) iCat = Convert.ToInt16(obj);
                            }

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
                            if (zbr == "" || zbr == Session["MemberName"].ToString()) btnAccept.Enabled = true;
                        }
                        else if (flag == 1)    //从办人员点击进入状态,只能提交
                        {
                            if (WebWorkFlow.IsCongBanRen(ViewState["PackNo"].ToString(), ViewState["CurWorkFlowNo"].ToString(), Session["MemberName"].ToString()))
                                btnSend.Enabled = true;
                        }
                        else
                        {
                            btnAccept.Enabled = false;
                        }
                    }
                }
            }

            //控制添加操作步骤的权限
            if (!txtTASK.ReadOnly)
            {
                btnAppendItem.Enabled = true;
                btnDeleteItem.Enabled = true;
                btnInsertItem.Enabled = true;
                btnTypicalOpt.Enabled = true;
                grvList.Columns[grvList.Columns.Count - 1].Visible = true;  //编辑隐藏
            }
            else
            {
                grvList.Columns[grvList.Columns.Count - 1].Visible = false;  //编辑隐藏
            }

            FillDropDownList.FillHtmlCombxByTable(ref hcbJHR, "dmis_view_depart_member_role", "MEMBER_NAME", "MEMBER_ID", "MEMBER_NAME", "ROLE_ID=5");//调度员
            FillDropDownList.FillHtmlCombxByTable(ref hcbSTATION, "DMIS_SYS_STATION", "NAME", "TID", "ORDER_ID");

            //状态

            ddlFLAG.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusNoCheck").ToString(), "0"));
            ddlFLAG.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusChecked").ToString(), "1"));
            ddlFLAG.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusComplete").ToString(), "2"));
            ddlFLAG.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusAbolish").ToString(), "3"));
            ddlFLAG.SelectedIndex = 0;

            if (Convert.ToInt32(ViewState["RecNo"]) > -1)
            {
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_DD_TERMWISE_OPT_HEAD", "TID=" + ViewState["RecNo"].ToString());
                initGridView();//取操作步骤
                if (!SetRight.IsAdminitrator(Session["MemberID"].ToString())) //不是管理员才设置初始人员及日期,否则会把管理员的名称也赋值给相应的控件
                {
                    //设置初始人名,主办人，从办人在后面设置
                    string[] name ={ "txtYPR" };
                    TextBox txt;
                    for (int i = 0; i < name.Length; i++)
                    {
                        txt = (TextBox)Page.FindControl(name[i]);
                        if (txt == null) continue;
                        if (!txt.ReadOnly && txt.Text == "") txt.Text = Session["MemberName"].ToString();
                    }
                    //设置初始日期
                    string[] date ={ "wdlDATEM", "wdlZXRQ" };
                    WebDate wdl;
                    for (int i = 0; i < date.Length; i++)
                    {
                        wdl = (WebDate)Page.FindControl(date[i]);
                        if (wdl == null) continue;
                        if (wdl.Enabled && wdl.Text == "") wdl.setTime(DateTime.Now);
                    }
                }
            }
            else   //新增时，确定操作票编号
            {
                _sql = "select count(*) from T_DD_TERMWISE_OPT_HEAD where to_char(DATEM,'YYYY')='" + DateTime.Now.ToString("yyyy") + "'";
                counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql)) + 1;
                txtPH.Text = DateTime.Now.ToString("yyyy") + counts.ToString("0000");
                txtYPR.Text = Session["MemberName"].ToString();
                wdlDATEM.setTime(DateTime.Now);
            }
        }
        else
        {
            //从典型票窗口生成的
            if (Session["RecNo"] != null)
            {
                ViewState["RecNo"] = Session["RecNo"];
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_DD_TERMWISE_OPT_HEAD", "TID=" + Session["RecNo"].ToString());
                initGridView();//取操作步骤
                Session["RecNo"] = null;
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ViewState["REPORT_ID"] == null || ViewState["REPORT_ID"].ToString().Trim() == "")
        {
            Response.Write("<script language=javascript> alert('" + GetGlobalResourceObject("WebGlobalResource", "NoReportMessage").ToString() + "')</script");
            return;
        }
        string para, tt;

        para = txtTID.Text;
        tt = "window.open('../SYS_Common/frmCellReportDisplay.aspx?ReportID=" + ViewState["REPORT_ID"].ToString() + "&Values=" + para + "','报表打印')";
        Response.Write("<script language=javascript>");
        Response.Write(tt);
        Response.Write("</script>");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string re;
        //系统管理员只修改所有文档对就的业务表的数据,但不产生新的记录。
        //2009-3-25  用户要求修改缺陷单的级别时，同时要修改业务流任务的描述。
        if (SetRight.IsAdminitrator(Session["MemberID"].ToString()))
        {
            DataTable doc = DBOpt.dbHelper.GetDataTable("select distinct f_tablename,f_recno from dmis_sys_doc where f_packno=" + ViewState["PackNo"].ToString());
            for (int i = 0; i < doc.Rows.Count; i++)
            {
                re = ControlWebValidator.Validate(this.Page, doc.Rows[i][0].ToString());
                if (re != "")
                {
                    JScript.Alert(re);
                    detail_info.InnerText = re;
                    return;
                }
                re = CustomControlSave.CustomControlSaveByTableNameReturnS(this.Page, doc.Rows[i][0].ToString());
                if (re != "")
                {
                    JScript.Alert(re);
                    detail_info.InnerText = re;
                    return;
                }
                else
                {
                    detail_info.InnerText = "";
                }
            }
            return;
        }

        //其它人员处理业务时保存
        bool isAdd = false;
        if (txtTID.Text == "") isAdd = true; //判断是否是新增的，保存之后，txtTID会有值，故在此判断

        re = ControlWebValidator.Validate(this.Page, ViewState["TableName"].ToString());
        if (re != "")
        {
            JScript.Alert(re);
            detail_info.InnerText = re;
            return;
        }
        re = CustomControlSave.CustomControlSaveByTableNameReturnS(this.Page, ViewState["TableName"].ToString());
        if (re != "")
        {
            JScript.Alert(re);
            detail_info.InnerText = re;
            return;
        }
        detail_info.InnerText = "";

        //第一次保存时，生成工作流数据
        if (txtPACK_NO.Text.Trim()=="")  //这才是判断标准
        {
            uint packNo = 0;
            string station = hcbSTATION.SelectedText;
            string planStarttime = wdlDATEM.getTime().ToString("dd-MM-yyyy HH:mm");

            double planHours = 0;
            string desc = "(" + txtPH.Text + ")" + txtTASK.Text.Trim();  //任务描述
            string planEndtime = wdlDATEM.getTime().AddHours(planHours).ToString("dd-MM-yyyy HH:mm");
    
            if (WebWorkFlow.CreatePack(Convert.ToInt32(ViewState["PackTypeNo"]), desc, Session["MemberName"].ToString(), ref packNo, station, planStarttime, planEndtime) < 0)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkInsertInstanceFailMessage").ToString());  //创建业务数据失败！
                detail_info.InnerText = GetGlobalResourceObject("WebGlobalResource", "WkInsertInstanceFailMessage").ToString();
                return;
            }

            int iFirstFlowNo = -1;
            int iWorkFlowNo = -1;
            iFirstFlowNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_NO from DMIS_SYS_FLOWLINK where F_PACKTYPENO=" + ViewState["PackTypeNo"] + " and F_FLOWCAT=0"));

            bool bCreated = WebWorkFlow.CreateFlow(Convert.ToInt16(packNo), ref iWorkFlowNo, Session["MemberName"].ToString(), iFirstFlowNo, "", Session["MemberName"].ToString(), "", txtTID.Text);   //第一次新建流程时，传接从办人的姓名列表
            if (!bCreated)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkInsertTaskFailMessage").ToString());//创建工作流数据失败！
                detail_info.InnerText = GetGlobalResourceObject("WebGlobalResource", "WkInsertTaskFailMessage").ToString();
                return;
            }
            ViewState["PackNo"] = packNo;
            ViewState["CurWorkFlowNo"] = iWorkFlowNo;
            ViewState["CurLinkNo"] = iFirstFlowNo;
            ViewState["RecNo"] = txtTID.Text;

            //增加PACK_NO值
            _sql = "update " + ViewState["TableName"].ToString() + " set PACK_NO=" + packNo + " where TID=" + txtTID.Text;
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0) 
                txtPACK_NO.Text = packNo.ToString();
        }

        //只有用户保存了，确认它接收此任务了。
        //修改已经接收的状态，使此业务不能抽回
        if (ViewState["CurWorkFlowNo"] != null && Convert.ToInt16(ViewState["CurWorkFlowNo"]) > 0)
        {
            object obj = DBOpt.dbHelper.ExecuteScalar("select f_working from dmis_sys_workflow where f_no=" + ViewState["CurWorkFlowNo"]);
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
        if (detail_info.InnerText.Length > 0) return;

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
        if (detail_info.InnerText != "") return;
        string returnString = "";


        //发送前条件判断
        returnString = WebWorkFlow.TacheSendCondition(ViewState["PackTypeNo"].ToString(), ViewState["CurLinkNo"].ToString(), ViewState["PackNo"].ToString(), ViewState["CurWorkFlowNo"].ToString(), ViewState["TableName"].ToString(), ViewState["RecNo"].ToString());
        if (returnString.Length > 0)
        {
            JScript.Alert(returnString);
            detail_info.InnerText = returnString;
            return;
        }

        string sMainer = "";
        object obj;
        obj = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVER from DMIS_SYS_WORKFLOW where F_NO=" + ViewState["CurWorkFlowNo"]);
        if (obj != null)
            sMainer = obj.ToString();

        if (sMainer == Session["MemberName"].ToString())  //主办人发送
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
                    JScript.OpenWindow("../SYS_WorkFlow/AssignTaskWindow.aspx?" + paras, "发送窗", "scrollbars=yes,width=670,height=500,top=20,left=100");
                    //Response.Write(webfun.OpenWin("../SYS_WorkFlow/AssignTaskWindow.aspx?" + paras, "发送窗", "scrollbars=yes,width=700,height=480,top=20,left=100"));
                }
                else  //非派工环节，直接发送即可
                {
                    int curTaskID = Convert.ToInt16(ViewState["CurWorkFlowNo"]);
                    if (WebWorkFlow.DirectCreateFlow(Convert.ToInt16(ViewState["PackNo"]), ref curTaskID, Session["MemberName"].ToString()))
                    {
                        //两种统计时间的方法，看用户用何种
                        //1、根据工作流的相关表DMIS_SYS_WORKFLOW DMIS_SYS_MEMBERSTATUS来统计实际工作时间
                        WebWorkFlow.StatisicFactuslTimes(ViewState["PackTypeNo"].ToString(), ViewState["CurLinkNo"].ToString(),
                            ViewState["PackNo"].ToString(), ViewState["CurWorkFlowNo"].ToString(), ViewState["TableName"].ToString(), ViewState["RecNo"].ToString());
                        //2、根据业务表保存的人名来统计实际工作时间
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
            //从办人员提交,只是结束其办理状态
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
        if (detail_info.InnerText != "") return;

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


    protected void btnAppendItem_Click(object sender, EventArgs e)
    {
        //if (ViewState["RecNo"] == null)  //先保存头
        //{
        //    string ret, sql;

        //    ret = ControlWebValidator.Validate(this.Page, Session["TableName"].ToString());
        //    if (ret.Length > 0)
        //    {
        //        info.InnerText = ret;
        //        return;
        //    }
        //    ret = CustomControlSave.CustomControlSaveByTableNameReturnS(this.Page, Session["TableName"].ToString(), out sql);
        //    if (ret.Length > 0)
        //    {
        //        info.InnerText = ret;
        //        return;
        //    }
        //    else
        //    {
        //        ViewState["RecNo"] = txtTID.Text;
        //        info.InnerText = "";
        //    }
        //}

        //先保存一下。
        btnSave_Click(null, null);

        uint maxTid = DBOpt.dbHelper.GetMaxNum("T_DD_TERMWISE_OPT_BODY", "TID");
        _sql = "insert into T_DD_TERMWISE_OPT_BODY(HEAD_TID,TID,XH) values(" + ViewState["RecNo"].ToString() + "," + maxTid.ToString() + "," + Convert.ToString(grvList.Rows.Count + 1) + ")";
        DBOpt.dbHelper.ExecuteSql(_sql);
        initGridView();
    }

    protected void btnInsertItem_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            //JScript.Alert("请先选择一操作步骤!");
            return;
        }
        int xh = grvList.SelectedIndex + 2;
        //先把插入点后面的序号都加1
        for (int i = grvList.SelectedIndex; i < grvList.Rows.Count; i++)
        {
            _sql = "update T_DD_TERMWISE_OPT_BODY set XH=" + xh + " where TID=" + grvList.DataKeys[i].Value;
            DBOpt.dbHelper.ExecuteSql(_sql);
            xh++;
        }
        uint maxTid = DBOpt.dbHelper.GetMaxNum("T_DD_TERMWISE_OPT_BODY", "TID");
        _sql = "insert into T_DD_TERMWISE_OPT_BODY(HEAD_TID,TID,XH) values(" + ViewState["RecNo"].ToString() + "," + maxTid.ToString() + "," + Convert.ToString(grvList.SelectedIndex + 1) + ")";
        DBOpt.dbHelper.ExecuteSql(_sql);
        initGridView();
    }

    protected void btnDeleteItem_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            //JScript.Alert("请先选择要删除的操作步骤!");
            return;
        }
        _sql = "delete from  T_DD_TERMWISE_OPT_BODY where TID=" + grvList.DataKeys[grvList.SelectedIndex].Value;
        DBOpt.dbHelper.ExecuteSql(_sql);
        BuildXH();
        initGridView();
    }

    private void BuildXH()
    {
        _sql = "select XH,TID from T_DD_TERMWISE_OPT_BODY where HEAD_TID=" + ViewState["RecNo"].ToString() + " order by XH";
        DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
        int xh;
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            if (int.TryParse(dt.Rows[i - 1][0].ToString(), out xh))
            {
                if (xh == i)
                    continue;
                else
                    _sql = "update T_DD_TERMWISE_OPT_BODY set XH=" + i.ToString() + " where TID=" + dt.Rows[i - 1][1].ToString();
            }
            else
            {
                _sql = "update T_DD_TERMWISE_OPT_BODY set XH=" + i.ToString() + " where TID=" + dt.Rows[i - 1][1].ToString();
            }
            DBOpt.dbHelper.ExecuteSql(_sql);
        }
        initGridView();
    }

    private void initGridView()
    {
        _sql = "select * from T_DD_TERMWISE_OPT_BODY where HEAD_TID=" + ViewState["RecNo"].ToString() + " order by XH";
        DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
        grvList.DataSource = dt;
        grvList.DataBind();
    }

    protected void grvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        grvList.EditIndex = -1;
        initGridView();
    }

    protected void grvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvList.EditIndex = e.NewEditIndex;
        grvList.EditRowStyle.BackColor = System.Drawing.Color.FromName("#F7CE90");
        initGridView();

        _sql = "select * from T_DD_TERMWISE_OPT_BODY where TID=" + grvList.DataKeys[e.NewEditIndex].Value;
        DataTable _dt = DBOpt.dbHelper.GetDataTable(_sql);

        HtmlComboBox hcbUNIT = (HtmlComboBox)grvList.Rows[e.NewEditIndex].FindControl("hcbUNIT");
        FillDropDownList.FillHtmlCombxByTable(ref hcbUNIT, "DMIS_SYS_STATION", "NAME", "TID", "ORDER_ID");
        HtmlComboBox hcbFLR = (HtmlComboBox)grvList.Rows[e.NewEditIndex].FindControl("hcbFLR");
        FillDropDownList.FillHtmlCombxByTable(ref hcbJHR, "dmis_view_depart_member_role", "MEMBER_NAME", "MEMBER_ID", "MEMBER_NAME", "ROLE_ID=5");//调度员
        HtmlComboBox hcbSLR = (HtmlComboBox)grvList.Rows[e.NewEditIndex].FindControl("hcbSLR");
        FillDropDownList.FillHtmlCombxByTable(ref hcbSLR, "dmis_view_depart_member_role", "MEMBER_NAME", "MEMBER_ID", "MEMBER_NAME", "ROLE_ID=5");//调度员

        if (_dt.Rows[0]["UNIT"] != Convert.DBNull)
            hcbUNIT.Text = _dt.Rows[0]["UNIT"].ToString();
        if (_dt.Rows[0]["FLR"] != Convert.DBNull)
            hcbFLR.Text = _dt.Rows[0]["FLR"].ToString();
        if (_dt.Rows[0]["SLR"] != Convert.DBNull)
            hcbSLR.Text = _dt.Rows[0]["SLR"].ToString();

        TextBox txtXH,txtCONTENT,txtFLSJ,txtSLSJ;
        txtXH = (TextBox)grvList.Rows[e.NewEditIndex].FindControl("txtXH");
        txtCONTENT = (TextBox)grvList.Rows[e.NewEditIndex].FindControl("txtCONTENT");
        txtFLSJ = (TextBox)grvList.Rows[e.NewEditIndex].FindControl("txtFLSJ");
        txtSLSJ = (TextBox)grvList.Rows[e.NewEditIndex].FindControl("txtSLSJ");

        if (_dt.Rows[0]["XH"] != Convert.DBNull)
            txtXH.Text = _dt.Rows[0]["XH"].ToString();
        if (_dt.Rows[0]["CONTENT"] != Convert.DBNull)
            txtCONTENT.Text = _dt.Rows[0]["CONTENT"].ToString();
        
        if (_dt.Rows[0]["FLSJ"] != Convert.DBNull)
            txtFLSJ.Text = _dt.Rows[0]["FLSJ"].ToString();
        
        if (_dt.Rows[0]["SLSJ"] != Convert.DBNull)
            txtSLSJ.Text = _dt.Rows[0]["SLSJ"].ToString();

        //执行的阶段，只能填写发令人、发令时间 、受令人、汇报时间
        if (txtTASK.Enabled)
        {
            txtXH.Enabled=true;
            hcbUNIT.Enabled = true;
            txtCONTENT.Enabled = true;
            txtFLSJ.Enabled = false;
            hcbSLR.Enabled = false;
            txtSLSJ.Enabled = false;
            hcbFLR.Enabled = false;
        }
        else
        {
            txtXH.Enabled = false;
            hcbUNIT.Enabled = false;
            txtCONTENT.Enabled = false;
            txtFLSJ.Enabled = true;
            hcbSLR.Enabled = true;
            txtSLSJ.Enabled = true;
            hcbFLR.Enabled = true;
        }

    }

    protected void grvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HtmlComboBox hcbUNIT = (HtmlComboBox)grvList.Rows[e.RowIndex].FindControl("hcbUNIT");
        HtmlComboBox hcbFLR = (HtmlComboBox)grvList.Rows[e.RowIndex].FindControl("hcbFLR");
        HtmlComboBox hcbSLR = (HtmlComboBox)grvList.Rows[e.RowIndex].FindControl("hcbSLR");
        TextBox txtFLSJ = (TextBox)grvList.Rows[e.RowIndex].FindControl("txtFLSJ");
        TextBox txtSLSJ = (TextBox)grvList.Rows[e.RowIndex].FindControl("txtSLSJ");
        TextBox txtXH = (TextBox)grvList.Rows[e.RowIndex].FindControl("txtXH");
        int temp;
        if (!int.TryParse(txtXH.Text, out temp))
        {
            detail_info.InnerText = GetGlobalResourceObject("WebGlobalResource", "NumericalValeError").ToString(); ;
            return;
        }
        TextBox txtCONTENT = (TextBox)grvList.Rows[e.RowIndex].FindControl("txtCONTENT");
        FieldPara[] fields = new FieldPara[7] { new FieldPara("UNIT",FieldType.String,hcbUNIT.SelectedText),
                                                new FieldPara("CONTENT",FieldType.String,txtCONTENT.Text),
                                                new FieldPara("XH",FieldType.Int,txtXH.Text),
                                                new FieldPara("SLR",FieldType.String,hcbSLR.SelectedText),
                                                new FieldPara("FLR",FieldType.String,hcbFLR.SelectedText),
                                                new FieldPara("FLSJ",FieldType.String,txtFLSJ.Text),
                                                new FieldPara("SLSJ",FieldType.String,txtSLSJ.Text)
                                                };
        WherePara[] wheres = new WherePara[1] { new WherePara("TID", FieldType.Int, grvList.DataKeys[e.RowIndex].Value.ToString(), "=", "and") };
        _sql = DBOpt.dbHelper.GetUpdateSql("T_DD_TERMWISE_OPT_BODY", fields, wheres);
        if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
        {
            grvList.EditIndex = -1;
            initGridView();
            detail_info.InnerText = GetGlobalResourceObject("WebGlobalResource", "SaveSuccessMessage").ToString();
        }
        else
        {
            detail_info.InnerText = GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage").ToString();
            return;
        }
    }

    protected void grvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0) return;
        grvList.SelectedRow.Cells[0].Text = "<img border=0 align=absmiddle src=../img/Selected.gif>";
    }

    protected void btnTypicalOpt_Click(object sender, EventArgs e)
    {
        //只有新增的还没有保存的票才可以选择
        if (txtTID.Text != "") return;
        JScript.OpenWindow("frmSelect_TYPICAL_OPT.aspx", "", "height=600,width=550,location=0,menubar=0");
    }
}

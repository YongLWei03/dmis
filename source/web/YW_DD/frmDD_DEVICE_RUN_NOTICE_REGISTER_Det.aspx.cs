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

//投运通知单
public partial class YW_DD_frmDD_DEVICE_RUN_NOTICE_REGISTER_Det : PageBaseDetail
{
    //websvrfunction webfun = new websvrfunction();
    string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sended"] != null)
        {
            if (Session["sended"].ToString() == "1" && ViewState["BackUrl"] != null)
                Response.Redirect(ViewState["BackUrl"].ToString());
        }

        if (!IsPostBack)
        {
            if (Request["BackUrl"] != null)
                ViewState["BackUrl"] = Request["BackUrl"];
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
                _sql = "select F_PACKNO from DMIS_SYS_DOC where f_TableName='" + Request["TableName"].ToString() + "' and f_recno=" + ViewState["RecNo"].ToString();
                ViewState["PackNo"] = DBOpt.dbHelper.ExecuteScalar(_sql);
                if (ViewState["PackNo"] == null)
                {
                    JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkNoFindPackNo").ToString());  //"没有找到对应的业务号！"
                    return;
                }
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
                            btnSend.Attributes.Add("onClick", "return confirm('" +GetGlobalResourceObject("WebGlobalResource", "WkConfirmSubmit").ToString()+"');");  //确定要提交到下一步?(新建业务时。)

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


            FillDropDownList.FillHtmlCombxByTable(ref hcbSTATION, "DMIS_SYS_STATION", "NAME", "TID", "ORDER_ID");
            FillDropDownList.FillHtmlCombxByTable(ref hcbBUILDER, "t_zh_wts_dw_para", "DW_NAME", "TID", "DW_ORDER");

            //FillDropDownList.FillHtmlCombxByTable(ref hcb投运单接收登记人, "DMIS_VIEW_DEPART_MEMBER_ROLE", "MEMBER_NAME", "MEMBER_ID", "MEMBER_NAME", "ROLE_ID=1 or ROLE_ID=2 or ROLE_ID=3");  //1:主网调度员;2：配网调度员；3为调度值长
            //FillDropDownList.FillHtmlCombxByTable(ref hcb投运调度员, "DMIS_VIEW_DEPART_MEMBER_ROLE", "MEMBER_NAME", "MEMBER_ID", "MEMBER_NAME", "ROLE_ID=1 or ROLE_ID=2 or ROLE_ID=3");
            //填充调度员,上面的语句会产生重复
            DataTable mem = DBOpt.dbHelper.GetDataTable("select distinct MEMBER_NAME from DMIS_VIEW_DEPART_MEMBER_ROLE where ROLE_ID=1 or ROLE_ID=2 or ROLE_ID=3");
            hcbACCEPTER.DataSource = mem;
            hcbACCEPTER.DataTextField = "MEMBER_NAME";
            hcbACCEPTER.DataBind();
            hcbDISPATCHER.DataSource = mem;
            hcbDISPATCHER.DataTextField = "MEMBER_NAME";
            hcbDISPATCHER.DataBind();

            if (Convert.ToInt32(ViewState["RecNo"]) > -1)
            {
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_DD_RUN_NOTICE", "TID=" + ViewState["RecNo"].ToString());
                //设置初始人名
                string[] name ={ "hcbACCEPTER", "hcbDISPATCHER" };
                HtmlComboBox hcb;
                for (int i = 0; i < name.Length; i++)
                {
                    hcb = (HtmlComboBox)Page.FindControl(name[i]);
                    if (hcb == null) continue;
                    if (hcb.Enabled && hcb.Text == "") hcb.Text = Session["MemberName"].ToString();
                }
                //设置初始日期
                string[] date ={ "wdlSTARTDATE", "wdlRUN_DATE" };
                WebDate wdl;
                for (int i = 0; i < date.Length; i++)
                {
                    wdl = (WebDate)Page.FindControl(date[i]);
                    if (wdl == null) continue;
                    if (wdl.Enabled && wdl.Text == "") wdl.setTime(DateTime.Now);
                }
            }
            else   //新增时，确定缺陷编号
            {
                _sql = "select max(PH) from T_DD_RUN_NOTICE";
                obj = DBOpt.dbHelper.ExecuteScalar(_sql);   //没有值时，obj.ToString()取值为System.Object

                if (obj == null || obj is DBNull)
                    counts = 1;
                else
                {
                    if (int.TryParse(obj.ToString(), out counts))
                        counts = counts + 1;
                    else
                        counts = 1;
                }
                txtPH.Text = counts.ToString();
                wdlSTARTDATE.setTime(DateTime.Now);
            }
        }
    }

    protected void bntPrint_Click(object sender, EventArgs e)
    {
        if (ViewState["REPORT_ID"] == null || ViewState["REPORT_ID"].ToString().Trim() == "")
        {
            Response.Write("<script language=javascript> alert('" + GetGlobalResourceObject("WebGlobalResource", "NoReportMessage").ToString() + "')</script");
            return;
        }
        string para, tt;

        para = wdlSTARTDATE.getTime().ToString("yyyyMM");
        tt = "window.open('../SYS_Common/frmCellReportDisplay.aspx?ReportID=" + ViewState["REPORT_ID"].ToString() + "&Values=" + para + "','报表打印')";

        Response.Write("<script language=javascript>");
        Response.Write(tt);
        Response.Write("</script>");
    }

    protected override void btnSave_Click(object sender, EventArgs e)
    {
        string re;
        //系统管理员只修改所有文档对就的业务表的数据,但不产生新的记录
        if (SetRight.IsAdminitrator(Session["MemberID"].ToString()))
        {
            DataTable doc = DBOpt.dbHelper.GetDataTable("select distinct f_tablename,f_recno from dmis_sys_doc where f_packno=" + ViewState["PackNo"].ToString());
            for (int i = 0; i < doc.Rows.Count; i++)
            {
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
        if (txtTID.Text == "") isAdd = true; //判断是否是新增的，保存之后，txtTID会有值，故在此判断

        re = ControlWebValidator.Validate(this.Page, ViewState["TableName"].ToString());
        if (re != "")
        {
            JScript.Alert(re);
            tdMessage.InnerText = re;
            return;
        }
        re = CustomControlSave.CustomControlSaveByTableNameReturnS(this.Page, ViewState["TableName"].ToString());
        if (re != "")
        {
            JScript.Alert(re);
            tdMessage.InnerText = re;
            return;
        }
        tdMessage.InnerText = "";

        //第一次保存时，生成工作流数据
        if (isAdd)
        {
            uint packNo = 0;
            string station = "";
            //if (hcb厂站.SelectedItem != null) station = hcb厂站.SelectedItem.Text;
            station = hcbSTATION.SelectedText;
            //
            string planStarttime = wdlSTARTDATE.getTime().ToString("dd-MM-yyyy HH:mm");
            string planEndtime = wdlENDDATE.getTime().ToString("dd-MM-yyyy HH:mm");

            if (WebWorkFlow.CreatePack(Convert.ToInt32(ViewState["PackTypeNo"]), "(" + txtPH.Text + ")" + txtDEVICE_NAME.Text, Session["MemberName"].ToString(), ref packNo, station, planStarttime, planEndtime) < 0)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkInsertInstanceFailMessage").ToString());  //创建业务数据失败！
                tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "WkInsertInstanceFailMessage").ToString();  
                return;
            }

            int iFirstFlowNo = -1;
            int iWorkFlowNo = -1;
            iFirstFlowNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_NO from DMIS_SYS_FLOWLINK where F_PACKTYPENO=" + ViewState["PackTypeNo"] + " and F_FLOWCAT=0"));

            bool bCreated = WebWorkFlow.CreateFlow(Convert.ToInt16(packNo), ref iWorkFlowNo, Session["MemberName"].ToString(), iFirstFlowNo, "", Session["MemberName"].ToString(), "", txtTID.Text);   //第一次新建流程时，传接从办人的姓名列表
            if (!bCreated)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkInsertTaskFailMessage").ToString());//创建工作流数据失败！
                tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "WkInsertTaskFailMessage").ToString();
                return;
            }
            ViewState["PackNo"] = packNo;
            ViewState["CurWorkFlowNo"] = iWorkFlowNo;
            ViewState["CurLinkNo"] = iFirstFlowNo;
            ViewState["RecNo"] = txtTID.Text;

            //增加PACK_NO值
            _sql = "update " + ViewState["TableName"].ToString() + " set PACK_NO=" + packNo + " where TID=" + txtTID.Text;
            DBOpt.dbHelper.ExecuteSql(_sql);
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
        btnSave_Click(null, null);
        if (tdMessage.InnerText != "") return;
        string returnString = "";

        //发送前条件判断
        returnString = WebWorkFlow.TacheSendCondition(ViewState["PackTypeNo"].ToString(), ViewState["CurLinkNo"].ToString(), ViewState["PackNo"].ToString(), ViewState["CurWorkFlowNo"].ToString(), ViewState["TableName"].ToString(), ViewState["RecNo"].ToString());
        if (returnString.Length > 0)
        {
            JScript.Alert(returnString);
            tdMessage.InnerText = returnString;
            return;
        }

        string sMainer = "";
        object obj;
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
                    JScript.OpenWindow("../SYS_WorkFlow/AssignTaskWindow.aspx?" + paras, "任务派工", "scrollbars=yes,width=700,height=500,top=20,left=100");
                    //Response.Write(webfun.OpenWin("../SYS_WorkFlow/AssignTaskWindow.aspx?" + paras, "发送窗", "scrollbars=yes,width=670,height=500,top=20,left=100"));
                }
                else  //非派工环节，直接发送即可
                {
                    int curTaskID = Convert.ToInt16(ViewState["CurWorkFlowNo"]);
                    if (WebWorkFlow.DirectCreateFlow(Convert.ToInt16(ViewState["PackNo"]), ref curTaskID, Session["MemberName"].ToString()))
                    {
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
            //Response.Write("<script language=javascript>alert('只有主办人使用');</script>");  //只有主办人使用
            return;
        }
        if (Convert.ToInt16(obj) < 0)
        {
            Response.Write("<script language=javascript>alert('"+GetGlobalResourceObject("WebGlobalResource", "WkFirstStepNoWithdraw").ToString()+"');</script>");  //流程的开始步骤不允许退回
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
}

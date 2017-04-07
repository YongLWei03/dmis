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

//赤几  停电检修计划
public partial class YW_YF_frmYF_POWERCUT_OVERHAUL_PLAN_Det : PageBaseWorkflowDetail
{
    protected virtual void Page_Load(object sender, EventArgs e)
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
                btnSave.Enabled = true;
                btnSaveClose.Enabled = true;
                WebWorkFlow.SetAllWebControlEnable(this.Page, Convert.ToInt16(ViewState["PackTypeNo"]), ViewState["TableName"].ToString());
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

                        //判断文件上传删除的权限，只要能修改委托书基本资料，则可以
                        if (hcbSTATION.Enabled)
                        {
                            btnAddFile.Enabled = true;
                            btnDelFile.Enabled = true;
                        }

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

            FillDropDownList.FillHtmlCombxByTable(ref hcbSTATION, "DMIS_SYS_STATION", "NAME", "TID");
            initFile();　//显示上传的文件
            string tid;

            if (ViewState["PackNo"] != null)
            {
                //任务单内容，只有一条，故用PACK查找
                if (DBOpt.dbHelper.IsExist(ViewState["TableName"].ToString(), "PACK_NO=" + ViewState["PackNo"].ToString()))
                    CustomControlFill.CustomControlFillByTableAndWhere(this.Page, ViewState["TableName"].ToString(), "PACK_NO=" + ViewState["PackNo"].ToString());

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
                    string[] name ={ "txtAPPLY_MAN", "txtLEADER", "txtAPPROVER","txtDIRECTOR","txtDISPATCHER" };
                    TextBox txt;
                    for (int i = 0; i < name.Length; i++)
                    {
                        txt = (TextBox)Page.FindControl(name[i]);
                        if (txt == null) continue;
                        if (!txt.ReadOnly && txt.Text == "") txt.Text = Session["MemberName"].ToString();
                    }
                    //设置初始日期
                    string[] date ={ "wdlAPPLY_STARTTIME", "wdlAPPLY_ENDTIME", "wdlAPPROVE_STARTTIME","wdlAPPROVE_ENDTIME","wdlDIRECTOR_TIME","wdlDISPATCH_FINISHEDTIME" };
                    WebDate wdl;
                    for (int i = 0; i < date.Length; i++)
                    {
                        wdl = (WebDate)Page.FindControl(date[i]);
                        if (wdl == null) continue;
                        if (wdl.Enabled && wdl.Text == "") wdl.setTime(DateTime.Now);
                    }
                }
            }
            else   //开始步骤
            {
                if (ViewState["RecNo"].ToString() == "-1")  //新增任务书的情况
                {
                    _sql = "select count(*) from " + ViewState["TableName"].ToString() + " where to_char(APPLY_STARTTIME,'YYYYMM')='" + DateTime.Now.ToString("yyyyMM") + "'";
                    counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql)) + 1;
                    txtNUM.Text = DateTime.Now.ToString("yyyyMM") + counts.ToString("000");
                    txtTID.Text = "";
                }
                else   //第二次，则还是第一步的步骤，则显示数据
                {
                    CustomControlFill.CustomControlFillByTableAndWhere(this.Page, ViewState["TableName"].ToString(), "TID=" + ViewState["RecNo"].ToString());
                }
            }
        }
    }
}

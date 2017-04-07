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

public partial class SYS_WorkFlow_ayf_InstanceWithdrawPopMessage : System.Web.UI.Page
{
    private string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["PackTypeNo"] != null)
                ViewState["PackTypeNo"] = Request["PackTypeNo"];
            if (Request["CurLinkNo"] != null)
                ViewState["CurLinkNo"] = Request["CurLinkNo"];
            if (Request["PackNo"] != null)
                ViewState["PackNo"] = Request["PackNo"];
            if (Request["CurWorkFlowNo"] != null)
                ViewState["CurWorkFlowNo"] = Request["CurWorkFlowNo"];

            if (Request["PreCurWorkFlowNo"] != null) ViewState["PreCurWorkFlowNo"] = Request["PreCurWorkFlowNo"];
            object obj;
            obj=DBOpt.dbHelper.ExecuteScalar("select f_desc from dmis_sys_pack where f_no="+ViewState["PackNo"]);
            if (obj != null)
                tdPackDesc.InnerText = obj.ToString();
            else
                tdMessage.InnerText = "无法等到任务描述！";
            DataTable temp = DBOpt.dbHelper.GetDataTable("select f_flowname,f_receiver from dmis_sys_workflow where f_no=" + ViewState["PreCurWorkFlowNo"]);
            if (temp != null && temp.Rows.Count > 0)
            {
                txtF_FLOWNAME.Text = temp.Rows[0][0].ToString();
                txtMEMBER_NAME.Text = temp.Rows[0][1].ToString();
            }
            else
            {
                tdMessage.InnerText = "无法找到要退回的上一步骤！";
                btnOK.Enabled = false;
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (txtREASON.Text == "")
        {
            tdMessage.InnerText = "请填写退回理由！";
            return;
        }
        if (WebWorkFlow.Withdraw(ViewState["PackNo"].ToString(), ViewState["CurWorkFlowNo"].ToString(), txtREASON.Text, Session["MemberName"].ToString()))
        {
            Session["sended"] = 1;
            JScript.CloseWin("refreshPage");
        }
        else
            tdMessage.InnerText = "退回不成功，请联系管理员！";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        JScript.CloseWindow();
    }
}

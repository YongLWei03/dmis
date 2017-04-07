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

public partial class SYS_Common_frmValidatePasswordByRole : System.Web.UI.Page
{
    private string _sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["RoleID"] == null || Request["RoleID"] == "")
            {
                tdMessage.InnerText = "岗位ID没有传递!";
                return;
            }
            if (Request["Object"] == null || Request["Object"] == "")
            {
                tdMessage.InnerText = "人员控件没有传递!";
                return;
            }
            ViewState["Object"] = Request["Object"];
            FillDropDownList.FillByTable(ref ddlMemberName, "DMIS_VIEW_DRPART_MEMBER_ROLE", "MEMBER_NAME", "MEMBER_ID", "MEMBER_NAME", "ROLE_ID in(" + Request["RoleID"]+")");
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (ddlMemberName.SelectedIndex < 0 || ddlMemberName.SelectedItem.Text.Trim()=="") return;
        _sql = "select count(*) from DMIS_SYS_MEMBER where ID=" + ddlMemberName.SelectedValue + " and PASSWORD='" + txtPassword.Text + "'";
        object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        if (obj == null || Convert.ToInt16(obj) == 0)
        {
            tdMessage.InnerText = "口令不对!";
            return;
        }
        else
        {
            string t1 = "window.opener.document.getElementById('" + ViewState["Object"].ToString() + "').value = '" + ddlMemberName.SelectedItem.Text + "';";
            Response.Write("<script language=javascript>");
            Response.Write(t1);
            Response.Write("window.close();");
            Response.Write("</script>");
        }
    }


}

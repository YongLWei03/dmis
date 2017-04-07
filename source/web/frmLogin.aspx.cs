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
using System.Data.Common;
using System.Data.OracleClient;
using System.Globalization;
using System.Threading;
using System.Xml;

public partial class frmlogin : System.Web.UI.Page
{

    protected override void InitializeCulture()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Page.Request.PhysicalApplicationPath+"Web.config");
        //XmlNodeList nodes = doc.SelectNodes("/configuration/system.web");
        XmlNodeList nodes = doc.GetElementsByTagName("globalization");
        if (nodes == null || nodes.Count < 1)
        {
            Session["UICulture"] = "en-us";
            Session["Culture"] = "en-us";
        }
        else
        {
            Session["UICulture"] = nodes[0].Attributes["uiCulture"].Value;
            Session["Culture"] = nodes[0].Attributes["culture"].Value;
        }
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (txtCode.Value.Trim() == "" || txtPwd.Value.Trim() == "")
        {
            lblMessage.Text = GetGlobalResourceObject("WebGlobalResource", "ItemNotNull").ToString();
            return;
        }
        string sql = "select ID,NAME,DEPART_ID,THEME,FLAG,PASSWORD from DMIS_SYS_MEMBER where CODE = '" + txtCode.Value.Trim() + "' and PASSWORD='" + txtPwd.Value + "' and flag=0";
        DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
        if (dt == null)
        {
            lblMessage.Text = GetGlobalResourceObject("WebGlobalResource", "DbFail").ToString();
            return;
        }

        if (dt.Rows.Count < 1)
        {
            lblMessage.Text = GetGlobalResourceObject("WebGlobalResource", "UserOrPwdWrong").ToString();
            WebLog.InsertLog("登录", "失败", "以用户名:" + txtCode.Value + "登录失败");
            return;
        }

        lblMessage.Text = "";
        Session["code"] = txtCode.Value.Trim();
        Session["name"] = dt.Rows[0]["NAME"].ToString();
        Session["MemberName"] = dt.Rows[0]["NAME"].ToString();
        Session["DepartID"] = dt.Rows[0]["DEPART_ID"];    //登录人员所属部门ID
        Session["MemberID"] = dt.Rows[0]["ID"];    //登录人员ID

        //2010-7-30  封掉外观主题
        //if (dt.Rows[0][3] == Convert.DBNull || dt.Rows[0][3].ToString().Trim() == "")  //主题
            Session["THEME"] = "default";
        //else
        //    Session["THEME"] = dt.Rows[0][3];

        //找登录人员的岗位
        System.Text.StringBuilder sbRole = new System.Text.StringBuilder();
        sql = "SELECT ROLE_ID FROM DMIS_SYS_MEMBER_ROLE WHERE MEMBER_ID=" + Session["MemberID"].ToString();
        DataTable dtRole = DBOpt.dbHelper.GetDataTable(sql);
        for (int i = 0; i < dtRole.Rows.Count; i++)
        {
            sbRole.Append(dtRole.Rows[i][0].ToString());
            sbRole.Append(",");
        }
        if (sbRole.Length > 0)
            Session["RoleIDs"] = sbRole.Remove(sbRole.Length - 1, 1);   //把最后一个逗号去掉
        else
            Session["RoleIDs"] = "";

        if (Session["RoleIDs"].ToString().Trim() == "")
        {
            //lblMessage.Text = "没有授权，不能登录！";
            WebLog.InsertLog("登录", "失败", "用户没有授权，不能登录！");
            return;
        }

        WebLog.InsertLog("登录", "成功", "登录成功");

        //Response.Write("<script> winobj=window.open('MainFrame.htm','','toolbar=no,menubar=no,titlebar=yes,directories=no,resizable=yes,status=yes');" +
        //                "winobj.moveTo(0,0);winobj.resizeTo(screen.width,screen.height);parent.window.opener=null;self.window.close();" +
        //                "</script>");
        Response.Redirect("MainFrame.htm");


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtCode.Value = "";
        txtPwd.Value = "";
    }

}

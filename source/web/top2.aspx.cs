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
using PlatForm.Functions;
using System.Globalization;
using System.Threading;

public partial class top2 : System.Web.UI.Page
{
    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblProductInfo.Text = GetGlobalResourceObject("WebGlobalResource", "ProductInfo").ToString();
            lblMan.Text = Session["MemberName"].ToString();
            DateTime dt = DateTime.Now;
            lblMan.Text = lblMan.Text +" "+ dt.ToString("dd-MM-yyyy") ;
            lbnRelogin.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "ReloginBeforeConfirm").ToString() + "');");  //确定要注销，重新登录?
            lbnExit.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "ExitBeforeConfirm").ToString() + "');");  //确定要退出?
        }
    }

    protected void lbnEnglish_Click(object sender, EventArgs e)
    {
        Session["UICulture"] = "es-ES";
        Session["Culture"] = "es-ES";
        Response.Write("<script>top.frames[0].location='top2.aspx';top.frames[1].location='left.aspx';top.frames[2].location.reload(true);</script>");
    }

    protected void lbnChina_Click(object sender, EventArgs e)
    {
        Session["UICulture"] = "zh-CN";
        Session["Culture"] = "zh-CN";
        //Response.Write("<script>top.frames[0].location='top2.aspx';top.frames[1].location='left.aspx';top.frames[2].location.reload(true);</script>");
        Response.Write("<script>top.frames[0].location='top2.aspx';top.frames[1].location='left.aspx';top.frames[2].location=top.frames[2].location.href;</script>");
    }

    protected void lbnRelogin_Click(object sender, EventArgs e)
    {
        WebLog.InsertLog("注销", "成功", "注销系统");
        Session["MemberID"] = null;
        Session["MemberName"] = null;
        Session["DepartID"] = null;
        Session["RoleIDs"] = null;
        //Response.Write("<script>parent.window.close();</script>");
        //Response.Write("<script>parent.window.open('frmlogin.aspx');</script>");
        Response.Write("<script>parent.window.location='frmlogin.aspx';</script>");
    }

    protected void lbnExit_Click(object sender, EventArgs e)
    {
        WebLog.InsertLog("退出", "成功", "退出系统");
        Session["MemberID"] = null;
        Session["MemberName"] = null;
        Session["DepartID"] = null;
        Session["RoleIDs"] = null;
        Response.Write("<script>parent.window.close();</script>");
    }
}

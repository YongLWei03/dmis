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

public partial class top : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            tdMember.InnerHtml = Session["MemberName"].ToString()+"　您好！";
            DateTime dt = DateTime.Now;
            string week;
            switch(dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week="星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week="星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week="星期三";
                    break;
                case DayOfWeek.Thursday:
                    week="星期四";
                    break;
                case DayOfWeek.Friday:
                    week="星期五";
                    break;
                case DayOfWeek.Saturday:
                    week="星期六";
                    break;
                case DayOfWeek.Sunday:
                    week="星期日";
                    break;
                default:
                    week = "";
                    break;
            }
            tdDate.InnerHtml = dt.Year + "年" + dt.Month + "月" + dt.Day + "日 "+ week;
        }
    }
    protected void imgZhuxiao_Click(object sender, ImageClickEventArgs e)
    {
        WebLog.InsertLog("注销","成功", "注销系统");
        Session["MemberID"] = null;
        Session["MemberName"] = null;
        Session["DepartID"] = null;
        Session["RoleIDs"] = null;
        Response.Write("<script>parent.window.close();</script>");
        Response.Write("<script>parent.window.open('frmlogin.aspx');</script>");
    }
    protected void imgExit_Click(object sender, ImageClickEventArgs e)
    {
        WebLog.InsertLog("退出","成功", "退出系统");
        Session["MemberID"] = null;
        Session["MemberName"] = null;
        Session["DepartID"] = null;
        Session["RoleIDs"] = null;
        Response.Write("<script>parent.window.close();</script>");
    }
}

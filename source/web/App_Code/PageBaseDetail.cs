using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.Globalization;
using System.Threading;

/// <summary>
/// 详细页面的祖先类，把保存、退出按钮的代码
/// </summary>
public class PageBaseDetail : System.Web.UI.Page
{
    protected HtmlTableCell tdPageMessage;   //页面导航中的显示页面页数、总数的信息栏（以后要删除）
    protected HtmlGenericControl info;       //显示信息区，主要显示保存时出错的信息
    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected virtual void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["MemberID"] == null) JScript.ReturnLogin();

        if (Session["Theme"] == null)
            Page.Theme = "default";
        else
            Page.Theme = Session["Theme"].ToString();

        if (!Page.IsPostBack)
        {
           // Session["Url"] =Request["URL"];
        }
    }


    protected virtual void btnSave_Click(object sender, EventArgs e)
    {
        string ret, sql;

        ret = ControlWebValidator.Validate(this.Page, Session["TableName"].ToString());
        if (ret.Length > 0)
        {
            info.InnerText = ret;
            return;
        }
        ret = CustomControlSave.CustomControlSaveByTableNameReturnS(this.Page, Session["TableName"].ToString(), out sql);
        if (ret.Length>0)
        {
            info.InnerText = ret;
            return;
        }
        else
        {
            info.InnerText = "";
            WebLog.InsertLog("", "成功", sql);
        }
        //JScript.CloseWin("refreshPage");

        Response.Redirect(Session["URL"].ToString());
    }


    protected virtual void btnReturn_Click(object sender, EventArgs e)
    {
       Response.Redirect(Session["URL"].ToString());
    }


    /// <summary>
    /// 页面出错，记录出错的日志
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void Page_Error(object sender, EventArgs e)
    {
        string errMsg;
        //得到系统上一个异常
        Exception currentError = Server.GetLastError();

        errMsg = "<link rel=\"stylesheet\" href=\"/default.css\">";
        errMsg += "<h1>" + (String)GetGlobalResourceObject("WebGlobalResource", "Error") + "</h1>" +
            (String)GetGlobalResourceObject("WebGlobalResource", "ErrorPosition") + Request.Url.ToString() + "<br/><hr/>" +
            (String)GetGlobalResourceObject("WebGlobalResource", "ErrorMessage") + " <font class=\"ErrorMessage\">" + currentError.Message.ToString() + "</font><hr/>" +
            "<b>Stack Trace:</b><br/>" +
            currentError.ToString();
        //如果发生致命应用程序错误
        //if (!(currentError is ApplicationException))
        //{
        //向Windows事件日志中写入错误日志
        // LogEvent(currentError.ToString(), EventLogEntryType.Error);
        //}
        //在页面中显示错误
        Response.Write(errMsg);
        //记录错误到日志中
        WebLog.InsertLog("错误", "", "网页名:" + Request.Url.ToString() + " ； 错误信息：" + currentError.Message.ToString() + "； 错误发生地点：" + currentError.StackTrace);
        //清除异常
        Server.ClearError();
    }

}

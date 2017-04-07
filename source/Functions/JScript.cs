using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace PlatForm.Functions
{
    public class JScript
    {
        /// <summary>
        /// 在客户端产生alert窗口,此函数要作废
        /// </summary>
        /// <param name="alertMsg"></param>
        public static void Alert(Page page,string alertMsg)
        {
            HttpContext.Current.Response.Write("<script language=javascript>alert('" + alertMsg + "');</script>");
        }


        /// <summary>
        /// 在客户端产生alert窗口
        /// </summary>
        /// <param name="alertMsg"></param>
        public static void Alert(string alertMsg)
        {
            string js = @"<script language=javascript>alert('" + alertMsg + "');</script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toURL">连接地址</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
        }


        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<Script language='JavaScript'>parent.opener=null;window.close();</Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 关闭本窗口，同时通过父窗口的某个隐藏控件更新父窗口的内容
        /// </summary>
        /// <param name="id1"></param>
        public static void CloseWin(string id1)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script language=javascript>\r\n");
            if (id1 != "")
            {
                sb.Append("var obj=window.opener.document.getElementById('" + id1 + "');\r\n");
                sb.Append("if(obj !=undefined){\r\n");
                sb.Append("if(obj.value=='1')\r\n");
                sb.Append("obj.value='0';\r\n");
                sb.Append("else\r\n");
                sb.Append("obj.value='1';\r\n}");
            }
            sb.Append("self.close();\r\n");
            sb.Append("</script>");
            HttpContext.Current.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 刷新父窗口,同时关闭本窗口
        /// </summary>
        public static void RefreshParent(string url)
        {
            string js = @"<Script language='JavaScript'>window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<Script language='JavaScript'> opener.location.reload();window.close();</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 刷新本窗口
        /// </summary>
        public static void RefreshSelf()
        {
            string js = @"<Script language='JavaScript'> window.location.reload();window.close();</Script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">头位置</param>
        /// <param name="left">左位置</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 转向Url制定的页面
        /// </summary>
        /// <param name="url">连接地址</param>
        public static void JavaScriptLocationHref(string url)
        {
            string js = @"<Script language='JavaScript'>window.location.replace('{0}');</Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
        }

        /// <summary>
        /// 打开模式窗口
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <param name="features"></param>
        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }


        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            string js = @"<script language=javascript>showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
        }

        /// <summary>
        /// 打开非模式窗口
        /// </summary>
        /// <param name="webFormUrl">网页名</param>
        /// <param name="features">特性</param>
        public static void OpenWindow(string webFormUrl,string windowName, string features)
        {
            string js = @"<script language=javascript>window.open('" + webFormUrl + "','" + windowName + "','" + features + "');</script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 打开窗口，如果已经打开则激活此窗口.,不起作用
        /// </summary>
        /// <param name="id1"></param>
        public static void OpenOrActiveWindow(string webFormUrl, string windowName, string features)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script language=javascript>\r\n");
            sb.Append("var win = null;\r\n");
            sb.Append("try{win.focus();win.close();}\r\n");
            sb.Append("catch(e){\r\n");
            sb.Append("win = window.open('" + webFormUrl + "','" + windowName + "','" + features+"',true);}\r\n");
            sb.Append("</script>");
            HttpContext.Current.Response.Write(sb.ToString());
        }



        /// <summary>
        /// 在客户端产生confirm窗口
        /// </summary>
        /// <param name="page"></param>
        /// <param name="confirmMsg"></param>
        public static void Confirm(Page page, string confirmMsg)
        {
            HttpContext.Current.Response.Write("<script language=javascript>confirm('" + confirmMsg + "');</script>");
        }

        /// <summary>
        /// 弹出选择人员窗口
        /// </summary>
        public static void OpenSelectMember(Page page,string members,string objectName)
        {
            string tt = "window.open('../frmSelectMember.aspx?members=" + members + "&object=" + objectName + "','frmSelectMember','width=700px,height=350px,left=100px,top=220px');";
            HttpContext.Current.Response.Write("<script language=javascript>");
            HttpContext.Current.Response.Write(tt);
            HttpContext.Current.Response.Write("</script>");
        }

        /// <summary>
        /// 弹出选择日期窗口
        /// </summary>
        /// <param name="page"></param>
        /// <param name="date"></param>
        /// <param name="objectName"></param>
        public static void OpenSelectDate(Page page, string date, string objectName)
        {
            string tt = "window.open('../frmSelectDate.aspx?date=" + date + "&object=" + objectName + "','frmSelectDate','width=300px,height=300px,left=100px,top=220px');";
            HttpContext.Current.Response.Write("<script language=javascript>");
            HttpContext.Current.Response.Write(tt);
            HttpContext.Current.Response.Write("</script>");
        }

        /// <summary>
        /// 弹出选择日期窗口
        /// </summary>
        /// <param name="page"></param>
        /// <param name="date"></param>
        /// <param name="objectName"></param>
        public static void OpenSelectDateTime(Page page, string date, string objectName)
        {
            string tt = "window.open('../frmSelectDateTime.aspx?date=" + date + "&object=" + objectName + "','frmSelectDateTime','width=350px,height=350px,left=100px,top=220px');";
            HttpContext.Current.Response.Write("<script language=javascript>");
            HttpContext.Current.Response.Write(tt);
            HttpContext.Current.Response.Write("</script>");
        }

        /// <summary>
        /// 弹出选择设备窗口
        /// </summary>
        public static void OpenSelectDevice(Page page, string device, string objectName)
        {
            string tt = "window.open('../frmSelectDevice.aspx?device=" + device + "&object=" + objectName + "','frmSelectDevice','width=250px,height=260px,left=100px,top=220px');";
            HttpContext.Current.Response.Write("<script language=javascript>");
            HttpContext.Current.Response.Write(tt);
            HttpContext.Current.Response.Write("</script>");
        }

        /// <summary>
        /// 弹出选择术语窗口
        /// </summary>
        public static void OpenSelectTerm(Page page, string term, string objectName)
        {
            string tt = "window.open('../frmSelectTerm.aspx?term=" + term + "&object=" + objectName + "','frmSelectTerm','width=700px,height=400px,left=100px,top=220px');";
            HttpContext.Current.Response.Write("<script language=javascript>");
            HttpContext.Current.Response.Write(tt);
            HttpContext.Current.Response.Write("</script>");
        }

        /// <summary>
        /// 身份失效时，返回登录界面
        /// </summary>
        public static void ReturnLogin(Page page)
        {
            HttpContext.Current.Response.Write("<script>alert('长时间不操作,身份失效,将重新登录!');</script>");
            HttpContext.Current.Response.Write("<script>parent.window.close();</script>");
            HttpContext.Current.Response.Write("<script>parent.window.open('../login.aspx','','width=1004,height=500,location=0,scrollbars=0,resize=0,menubar=0');</script>");
        }
        /// <summary>
        /// 身份失效时，返回登录界面
        /// </summary>
        public static void ReturnLogin()
        {
            HttpContext.Current.Response.Write("<script>alert('长时间不操作,身份失效,将重新登录!');</script>");
            HttpContext.Current.Response.Write("<script>parent.window.close();</script>");
            HttpContext.Current.Response.Write("<script>parent.window.open('../login.aspx','','width=1004,height=500,location=0,scrollbars=0,resize=0,menubar=0');</script>");
        }

  
    }
}

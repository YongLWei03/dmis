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
        /// �ڿͻ��˲���alert����,�˺���Ҫ����
        /// </summary>
        /// <param name="alertMsg"></param>
        public static void Alert(Page page,string alertMsg)
        {
            HttpContext.Current.Response.Write("<script language=javascript>alert('" + alertMsg + "');</script>");
        }


        /// <summary>
        /// �ڿͻ��˲���alert����
        /// </summary>
        /// <param name="alertMsg"></param>
        public static void Alert(string alertMsg)
        {
            string js = @"<script language=javascript>alert('" + alertMsg + "');</script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// ������Ϣ����ת���µ�URL
        /// </summary>
        /// <param name="message">��Ϣ����</param>
        /// <param name="toURL">���ӵ�ַ</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
        }


        /// <summary>
        /// �رյ�ǰ����
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<Script language='JavaScript'>parent.opener=null;window.close();</Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// �رձ����ڣ�ͬʱͨ�������ڵ�ĳ�����ؿؼ����¸����ڵ�����
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
        /// ˢ�¸�����,ͬʱ�رձ�����
        /// </summary>
        public static void RefreshParent(string url)
        {
            string js = @"<Script language='JavaScript'>window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// ˢ�´򿪴���
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<Script language='JavaScript'> opener.location.reload();window.close();</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// ˢ�±�����
        /// </summary>
        public static void RefreshSelf()
        {
            string js = @"<Script language='JavaScript'> window.location.reload();window.close();</Script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// ��ָ����С���´���
        /// </summary>
        /// <param name="url">��ַ</param>
        /// <param name="width">��</param>
        /// <param name="heigth">��</param>
        /// <param name="top">ͷλ��</param>
        /// <param name="left">��λ��</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// ת��Url�ƶ���ҳ��
        /// </summary>
        /// <param name="url">���ӵ�ַ</param>
        public static void JavaScriptLocationHref(string url)
        {
            string js = @"<Script language='JavaScript'>window.location.replace('{0}');</Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// ��ָ����Сλ�õ�ģʽ�Ի���
        /// </summary>
        /// <param name="webFormUrl">���ӵ�ַ</param>
        /// <param name="width">��</param>
        /// <param name="height">��</param>
        /// <param name="top">������λ��</param>
        /// <param name="left">������λ��</param>
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
        /// ��ģʽ����
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
        /// �򿪷�ģʽ����
        /// </summary>
        /// <param name="webFormUrl">��ҳ��</param>
        /// <param name="features">����</param>
        public static void OpenWindow(string webFormUrl,string windowName, string features)
        {
            string js = @"<script language=javascript>window.open('" + webFormUrl + "','" + windowName + "','" + features + "');</script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// �򿪴��ڣ�����Ѿ����򼤻�˴���.,��������
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
        /// �ڿͻ��˲���confirm����
        /// </summary>
        /// <param name="page"></param>
        /// <param name="confirmMsg"></param>
        public static void Confirm(Page page, string confirmMsg)
        {
            HttpContext.Current.Response.Write("<script language=javascript>confirm('" + confirmMsg + "');</script>");
        }

        /// <summary>
        /// ����ѡ����Ա����
        /// </summary>
        public static void OpenSelectMember(Page page,string members,string objectName)
        {
            string tt = "window.open('../frmSelectMember.aspx?members=" + members + "&object=" + objectName + "','frmSelectMember','width=700px,height=350px,left=100px,top=220px');";
            HttpContext.Current.Response.Write("<script language=javascript>");
            HttpContext.Current.Response.Write(tt);
            HttpContext.Current.Response.Write("</script>");
        }

        /// <summary>
        /// ����ѡ�����ڴ���
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
        /// ����ѡ�����ڴ���
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
        /// ����ѡ���豸����
        /// </summary>
        public static void OpenSelectDevice(Page page, string device, string objectName)
        {
            string tt = "window.open('../frmSelectDevice.aspx?device=" + device + "&object=" + objectName + "','frmSelectDevice','width=250px,height=260px,left=100px,top=220px');";
            HttpContext.Current.Response.Write("<script language=javascript>");
            HttpContext.Current.Response.Write(tt);
            HttpContext.Current.Response.Write("</script>");
        }

        /// <summary>
        /// ����ѡ�����ﴰ��
        /// </summary>
        public static void OpenSelectTerm(Page page, string term, string objectName)
        {
            string tt = "window.open('../frmSelectTerm.aspx?term=" + term + "&object=" + objectName + "','frmSelectTerm','width=700px,height=400px,left=100px,top=220px');";
            HttpContext.Current.Response.Write("<script language=javascript>");
            HttpContext.Current.Response.Write(tt);
            HttpContext.Current.Response.Write("</script>");
        }

        /// <summary>
        /// ���ʧЧʱ�����ص�¼����
        /// </summary>
        public static void ReturnLogin(Page page)
        {
            HttpContext.Current.Response.Write("<script>alert('��ʱ�䲻����,���ʧЧ,�����µ�¼!');</script>");
            HttpContext.Current.Response.Write("<script>parent.window.close();</script>");
            HttpContext.Current.Response.Write("<script>parent.window.open('../login.aspx','','width=1004,height=500,location=0,scrollbars=0,resize=0,menubar=0');</script>");
        }
        /// <summary>
        /// ���ʧЧʱ�����ص�¼����
        /// </summary>
        public static void ReturnLogin()
        {
            HttpContext.Current.Response.Write("<script>alert('��ʱ�䲻����,���ʧЧ,�����µ�¼!');</script>");
            HttpContext.Current.Response.Write("<script>parent.window.close();</script>");
            HttpContext.Current.Response.Write("<script>parent.window.open('../login.aspx','','width=1004,height=500,location=0,scrollbars=0,resize=0,menubar=0');</script>");
        }

  
    }
}

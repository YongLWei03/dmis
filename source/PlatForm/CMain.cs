using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections.Specialized;
using System.Windows.Forms;
using PlatForm.DBUtility;
using System.Net;
using System.Data.Common;

namespace PlatForm
{
    /// <summary>
    /// CMain 系统平台程序的入口点。
    /// </summary>
    public class CMain
    {
        //下面三个静态变量用在日志中
        public static string memberID="";  //此变量的赋值在frmLogin窗口中
        public static string memberName="";  //此变量的赋值在frmLogin窗口中
        public static string IP="";

        public CMain()
        {
            
        }

        [STAThread]
        static void Main()
        {
            IP = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();   //获取IP地址

            //设置语言
            string culture = System.Configuration.ConfigurationManager.AppSettings["Culture"];
            if (culture != "")
            {
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(culture);
                System.Threading.Thread.CurrentThread.CurrentCulture = ci;
                System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            }

            frmLogin login = new frmLogin();
            DialogResult dg = login.ShowDialog();
            if (dg == DialogResult.Cancel)
            {
                Application.Exit();
            }
            else
            {
                MainFrame main = new MainFrame();
                Application.Run(new MainFrame());
            }
        }
    }
}

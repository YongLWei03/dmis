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
    /// CMain ϵͳƽ̨�������ڵ㡣
    /// </summary>
    public class CMain
    {
        //����������̬����������־��
        public static string memberID="";  //�˱����ĸ�ֵ��frmLogin������
        public static string memberName="";  //�˱����ĸ�ֵ��frmLogin������
        public static string IP="";

        public CMain()
        {
            
        }

        [STAThread]
        static void Main()
        {
            IP = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();   //��ȡIP��ַ

            //��������
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

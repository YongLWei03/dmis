using System;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.Windows.Forms;
using PlatForm.DBUtility;
using System.Net;
using System.Data.Common;

namespace DataBackup
{
    /// <summary>
    /// CMain ϵͳƽ̨�������ڵ㡣
    /// </summary>
    public class CMain
    {
        public CMain()
        {
            
        }

        [STAThread]
        static void Main()
        {
                ExeDataBackup main = new ExeDataBackup();
                Application.Run(new ExeDataBackup());
        }
    }
}

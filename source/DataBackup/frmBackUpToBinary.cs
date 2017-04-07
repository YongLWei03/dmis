using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using System.IO;
using System.ServiceProcess;

namespace PlatForm
{
    public partial class frmBackUpToBinary : Form
    {
        public frmBackUpToBinary()
        {
            InitializeComponent();
        }

        private void frmBackUpToBinary_Load(object sender, EventArgs e)
        {
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            //Directory.SetCurrentDirectory(path);
            if (DBHelper.databaseType == "Oracle")
            {
                System.Diagnostics.Process.Start(@"Oracle-backup\backup.bat");
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                System.Diagnostics.Process.Start(@"SqlServer-backup\backup.bat");
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                //ServiceController sc = new ServiceController("SYBBCK_AOYUNFENG_BS");
                //if (sc.Status != ServiceControllerStatus.Running)
                //{
                //    lsbMsg.Items.Add("SYBBCK_AOYUNFENG_BS服务没有启动");
                //}
                System.Diagnostics.Process.Start(@"Sybase-backup\backup.bat");
            }

            

            //ServiceController[] scServices;
            //scServices = ServiceController.GetServices();
            //foreach (ServiceController scTemp in scServices)
            //{
            //    lsbMsg.Items.Add(scTemp.ServiceName);
            //}

        }

 

    }
}
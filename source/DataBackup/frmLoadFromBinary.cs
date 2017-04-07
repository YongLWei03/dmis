using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;

namespace DataBackup
{
    public partial class frmLoadFromBinary : Form
    {
        public frmLoadFromBinary()
        {
            InitializeComponent();
        }

        private void btnImp_Click(object sender, EventArgs e)
        {
            if (DBHelper.databaseType == "Oracle")
            {
                System.Diagnostics.Process.Start(@"Oracle-backup\backin.bat");
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                System.Diagnostics.Process.Start(@"SqlServer-backup\backin.bat");
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                //ServiceController sc = new ServiceController("SYBBCK_AOYUNFENG_BS");
                //if (sc.Status != ServiceControllerStatus.Running)
                //{
                //    lsbMsg.Items.Add("SYBBCK_AOYUNFENG_BS服务没有启动");
                //}
                System.Diagnostics.Process.Start(@"Sybase-backup\backin.bat");
            }
        }
    }
}
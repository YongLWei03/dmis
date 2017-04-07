using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PlatForm.DBUtility;
using System.Configuration;

namespace PlatForm
{
    public partial class frmLoadFromSQL : Form
    {
        public frmLoadFromSQL()
        {
            InitializeComponent();
        }

  
        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dlg.SelectedPath;
                ckbFiles.Items.Clear();
                DirectoryInfo di = new DirectoryInfo(txtPath.Text);
                FileInfo[] afi = di.GetFiles("*.txt", SearchOption.TopDirectoryOnly);
                foreach (FileInfo fi in afi)
                {
                    ckbFiles.Items.Add(fi.Name);
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbFiles.Items.Count; i++)
                ckbFiles.SetItemChecked(i, true);
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbFiles.Items.Count; i++)
                ckbFiles.SetItemChecked(i, false);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            //在所选择的目录下生成批处理文件,执行批处理文件,再删除它.(缺点:无法得知导入多少行.)
            if (txtPath.Text == "") return;
            if (ckbFiles.CheckedItems.Count < 1) return;

            string connString;
            string user, pwd, ds;
            int pos1,pos2;
            Directory.SetCurrentDirectory(txtPath.Text);
            StreamWriter bat = new StreamWriter("LoadFrom.bat");

            if (DBHelper.databaseType == "Oracle")
            {
                //根据连接串找数据源名  用户名  口令
                connString = ConfigurationManager.AppSettings["OraConnStringMain"];
                pos1 = connString.IndexOf("Data Source=");
                pos2 = connString.IndexOf(";", pos1);
                ds = connString.Substring(pos1+12, pos2 - pos1-12);
                pos1 = connString.IndexOf("User ID="); 
                pos2 = connString.IndexOf(";", pos1);
                user = connString.Substring(pos1 + 8, pos2 - pos1-8);
                pos1 = connString.IndexOf("Password=");
                pos2 = connString.IndexOf(";", pos1);
                pwd = connString.Substring(pos1 + 9, pos2 - pos1 - 9);

                using (StreamWriter sw = new StreamWriter("LoadFrom.txt"))
                {
                    for (int i = 0; i < ckbFiles.CheckedItems.Count; i++)
                       sw.WriteLine("@" + ckbFiles.CheckedItems[i].ToString());

                    sw.WriteLine("exit");
                }
                bat.WriteLine("sqlplus "+user+"/"+pwd+"@"+ds+ " @LoadFrom.txt");
            }
            else if (DBHelper.databaseType == "SqlServer")
            {   
                //根据连接串找数据源名  用户名  口令
                connString = ConfigurationManager.AppSettings["SqlConnStringMain"];
                pos1 = connString.IndexOf("Data Source=");
                pos2 = connString.IndexOf(";", pos1);
                ds = connString.Substring(pos1 + 12, pos2 - pos1 - 12);
                pos1 = connString.IndexOf("User ID=");
                pos2 = connString.IndexOf(";", pos1);
                user = connString.Substring(pos1 + 8, pos2 - pos1 - 8);
                pos1 = connString.IndexOf("Password=");
                pos2 = connString.IndexOf(";", pos1);
                pwd = connString.Substring(pos1 + 9, pos2 - pos1 - 9);

                for (int i = 0; i < ckbFiles.CheckedItems.Count; i++)
                    bat.WriteLine("osql -Uwebdmis -Pytdf0000 -Swebdmis <" + ckbFiles.CheckedItems[i].ToString());
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                //根据连接串找数据源名  用户名  口令
                connString = ConfigurationManager.AppSettings["SybConnStringMain"];
                pos1 = connString.IndexOf("Data Source=");
                pos2 = connString.IndexOf(";", pos1);
                ds = connString.Substring(pos1 + 12, pos2 - pos1 - 12);
                pos1 = connString.IndexOf("User ID=");
                pos2 = connString.IndexOf(";", pos1);
                user = connString.Substring(pos1 + 8, pos2 - pos1 - 8);
                pos1 = connString.IndexOf("Password=");
                pos2 = connString.IndexOf(";", pos1);
                pwd = connString.Substring(pos1 + 9, pos2 - pos1 - 9);

                for (int i = 0; i < ckbFiles.CheckedItems.Count; i++)
                    bat.WriteLine("isql -Uwebdmis -Pytdf0000 -Swebdmis <" + ckbFiles.CheckedItems[i].ToString());
            }
            else
            {
            }
            
            bat.Flush();
            bat.Close();

            //执行
            //System.Diagnostics.Process.Start("LoadFrom.bat");
        }

    }
}
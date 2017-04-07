using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using System.IO;

namespace DataBackup
{
    public partial class frmSetTime : Form
    {
        string _sql;
        public frmSetTime()
        {
            InitializeComponent();
        }

        private void frmSetTime_Load(object sender, EventArgs e)
        {
            if (DBHelper.databaseType == "Oracle")
            {
                _sql = "select username from all_users order by user_id";

            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                _sql = "select name from master.dbo.sysdatabases order by name";

            }
            else if (DBHelper.databaseType == "Sybase")
            {
                _sql = "select name from master.dbo.sysdatabases order by name";
            }
            else
            {
            }
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbbDataBase.Items.Add(dt.Rows[i][0].ToString());
            }
            InitInfo();
        }
        private void InitInfo()
        {
            if (File.Exists("ini.txt"))
            {
                StreamReader reader = new StreamReader("ini.txt", Encoding.Default);
                string strContent = reader.ReadLine();
                lsbInfo.Items.Clear();
                lsbInfo.Items.Add(strContent);
                strContent = strContent.Remove(0, 8);
                string[] str = strContent.Split(',');
                txtOut.Text = str[0];
                cbbOut.Text = str[1];
                strContent = reader.ReadLine();
                lsbInfo.Items.Add(strContent);
                strContent = strContent.Remove(0, 8);
                str = strContent.Split(',');
                if (str[0] != "��")
                {
                    txtIn.Text = str[0];
                    cbbIn.Text = str[1];
                }
                strContent = reader.ReadLine();
                lsbInfo.Items.Add(strContent);
                txtFile.Text = strContent.Remove(0, 7);
                strContent = reader.ReadLine();
                lsbInfo.Items.Add(strContent);
                cbbDataBase.Text = strContent.Remove(0, 5);
                strContent = reader.ReadLine();
                strContent = strContent.Remove(0, 7);
                str = strContent.Split(';');
                txtUsa.Text = str[0];
                txtP.Text = str[1].Remove(0, 3);
                while (!reader.EndOfStream)
                {
                    strContent = reader.ReadLine();
                    lsbTable.SelectedItem = strContent;
                    lsbInfo.Items.Add(strContent);
                }
                reader.Close();
                reader.Dispose();
            }
        }
        private void cbbDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsbTable.Items.Clear();
            label7.Text = cbbDataBase.SelectedItem.ToString();
            if (DBHelper.databaseType == "Oracle")
            {
                _sql = "select table_name from all_all_tables where owner='" + cbbDataBase.SelectedItem.ToString() + "' order by table_name";

            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                _sql = "select name from " + cbbDataBase.SelectedItem.ToString() + ".dbo.sysobjects where type in ('U')  order by name";
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                _sql = "select name from " + cbbDataBase.SelectedItem.ToString() + ".dbo.sysobjects where type in ('U') order by name ";
            }
            else
            {
            }

            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lsbTable.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            int iSunTable = lsbTable.Items.Count;
            for (int i = 0; i < iSunTable; i++)
            {
                lsbTable.SelectedIndex = i;
            }
        }

        private void btnSelectClean_Click(object sender, EventArgs e)
        {
            lsbTable.SelectedItems.Clear();
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "��ѡ��·��";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = dlg.SelectedPath;
            }
        }

        private void btnExe_Click(object sender, EventArgs e)
        {
            if (lsbTable.SelectedItems.Count == 0)
            {
                MessageBox.Show("��ѡ��Ҫ���ݵı�");
                return;
            }
            string strPath = txtFile.Text;
            if (strPath.Trim() == "")
            {
                MessageBox.Show("��ѡ��Ҫ�����ļ���·����");
                return;
            }
            else
            {
                if (Directory.Exists(strPath) == false)
                {
                    MessageBox.Show("�ļ��в����ڣ�");
                    return;
                }
            }
            if (txtOut.Text.Trim() == "")
            {
                MessageBox.Show("����дҪ���������ݿ����֣�");
                return;
            }
            else
            {
                if (cbbOut.Text.Trim() == "")
                {
                    MessageBox.Show("��ѡ�񵼳�����ʱ�����ַ�ʽ��");
                    return;
                }
            }
            if (txtIn.Text.Trim() != "")
            {
                if (cbbIn.Text.Trim() == "")
                {
                    MessageBox.Show("��ѡ��������ʱ�����ַ�ʽ��");
                    return;
                }
            }
            if (txtUsa.Text.Trim() == "")
            {

                MessageBox.Show("�������û�����");
                return;
            }
            if (File.Exists("ini-bak.txt"))
            {
                File.Delete("ini-bak.txt");
            }
            if (File.Exists("ini.txt"))
            {
                File.Move("ini.txt", "ini-bak.txt");
            }
            FileStream fs = new FileStream("ini.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine("Ҫ���ݵ����ݿ�:" + txtOut.Text.Trim() + "," + cbbOut.Text.Trim());
            if (txtIn.Text.Trim() != "")
                sw.WriteLine("Ҫ�ָ������ݿ�:" + txtIn.Text.Trim() + "," + cbbIn.Text.Trim());
            else
                sw.WriteLine("Ҫ�ָ������ݿ�:��," + cbbIn.Text.Trim());
            sw.WriteLine("�ļ����·��:" + txtFile.Text.Trim());
            sw.WriteLine("���ݿ���:" + cbbDataBase.Text.Trim());
            sw.WriteLine("���ݿ��û���:" + txtUsa.Text.Trim() + ";����:" + txtP.Text.Trim());
            for (int i = 0; i < lsbTable.SelectedItems.Count; i++)
            {
                sw.WriteLine(lsbTable.SelectedItems[i].ToString());
            }
            sw.Flush();
            fs.Close();
            StreamReader reader = new StreamReader("ini.txt", Encoding.Default);
            lsbInfo.Items.Clear();
            while (!reader.EndOfStream)
            {
                lsbInfo.Items.Add(reader.ReadLine());
            }
            reader.Close();
            reader.Dispose();
        }
    }
}
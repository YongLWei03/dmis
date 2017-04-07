using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PlatForm.DBUtility;
using System.Diagnostics;
using System.Collections;

namespace DataBackup
{
    public partial class frmDataIn : Form
    {
        public frmDataIn()
        {
            InitializeComponent();
        }

        private void frmDataIn_Load(object sender, EventArgs e)
        {

        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "请选择路径";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = dlg.SelectedPath;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            labText.Text = "";
            lsbTable.Items.Clear();
            if (txtFile.Text.Trim() == "")
            {
                labText.Text = "请选选择文件路径！";
                return;
            }
            string path = txtFile.Text;
            string[] fileNames = Directory.GetFiles(path);
            foreach (string file in fileNames)
            {
                lsbTable.Items.Add(file.Substring(path.Length + 1));
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

        private void btnExeIn_Click(object sender, EventArgs e)
        {
            labText.Text = "";
            if (txtFile.Text.Trim() == "")
            {
                labText.Text = "请选选择文件路径！";
                return;
            }
            if (MessageBox.Show("是否要导入数据?", "注意!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            //////////////////////////////
            lsbInfo.Items.Clear();
            txtSql.Text = "";
            dgvData.Rows.Clear();
            dgvData.Columns.Clear();
            lsbInfo.Visible = true;
            txtSql.Visible = false;
            dgvData.Visible = false;
            ///////////////////////////////
            btnExeIn.Enabled = false;
            for (int i = 0; i < lsbTable.SelectedItems.Count; i++)
            {
                string fileName = "", strINFO = "";
                switch (DBHelper.databaseType)
                {
                    case "Sybase":
                        fileName = "isql -Usa -P -Ssybase11 < "+txtFile.Text+ "\\" + lsbTable.SelectedItems[i].ToString();
                        strINFO = exeCmdDataIn(fileName);
                        lsbInfo.Items.Add(strINFO);
                        break;
                    case "Oracle":
                        fileName = "sqlplus df_dmis/df_dmis@dbs1 < " + txtFile.Text + "\\" + lsbTable.SelectedItems[i].ToString();
                        strINFO = exeCmdDataIn(fileName);
                        lsbInfo.Items.Add(strINFO);
                        break;
                    case "SqlServer":
                        fileName = "osql -Usa -P -Sdbs1 < " + txtFile.Text + "\\" + lsbTable.SelectedItems[i].ToString();
                        strINFO = exeCmdDataIn(fileName);
                        lsbInfo.Items.Add(strINFO);
                        break;
                }
            }
            btnExeIn.Enabled = true;
        }
        protected string exeCmdDataIn(string arguments)
        {
            labText.Text = "";
            //System.Diagnostics.Process.Start(fileName);
            Process p = new Process();
            p.StartInfo.FileName = "cmd";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(arguments);
            p.StandardInput.WriteLine("\r\nexit");
            string ss = "";
            for (int i = 0; i < 6; i++)
            {
                ss = p.StandardOutput.ReadLine();
                if (ss.Contains("成功"))
                {
                    break;
                }
            }
            p.Close();
            p.Dispose();
            return ss.Replace("删除","导入");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            labText.Text = "";
            //////////////////////////////
            lsbInfo.Items.Clear();
            txtSql.Text = "";
            dgvData.Rows.Clear();
            dgvData.Columns.Clear();
            lsbInfo.Visible = false;
            txtSql.Visible = true;
            dgvData.Visible = false;
            ///////////////////////////////
            string strContent = "";
            if (lsbTable.SelectedItems.Count <= 0)
            {
                labText.Text = "请先选择要显示的文件！";
                return;
            }
            if (!lsbTable.SelectedItems[0].ToString().Contains("txt"))
            {
                labText.Text = "只显示类型为txt的文件!";
                return;
            }
            try
            {
                StreamReader reader = new StreamReader(txtFile.Text + "\\" + lsbTable.SelectedItems[0].ToString(), Encoding.Default);
                while (!reader.EndOfStream)
                {
                    strContent = strContent + reader.ReadLine() + "\r\n";
                }
                txtSql.Text = strContent;
                reader.Close();
                reader.Dispose();
            }
            catch
            {
                labText.Text = "此文件不能显示！";
            }
        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            labText.Text = "";
            //////////////////////////////
            lsbInfo.Items.Clear();
            txtSql.Text = "";
            dgvData.Rows.Clear();
            dgvData.Columns.Clear();
            lsbInfo.Visible = false;
            txtSql.Visible = false;
            dgvData.Visible = true;
            ///////////////////////////////
            if (lsbTable.SelectedItems.Count < 0)
            {
                labText.Text = "请先选择要显示的文件！";
                return;
            }
            try
            {
                StreamReader reader = new StreamReader(txtFile.Text + "\\" + lsbTable.SelectedItems[0].ToString(), Encoding.Default);
                string tableName = reader.ReadLine();
                if (tableName.Contains("delete from "))//是备份数据文件
                {//组成列
                    tableName = tableName.Remove(0, 12);
                    string[] tmp = tableName.Split(' ');
                    tmp = tmp[0].Split('.');
                    DataTable dt = new DataTable();
                    switch (DBHelper.databaseType)
                    {
                        case "Oracle":
                            dt = DBOpt.dbHelper.GetDataTable("select column_name colName,data_type colType from ALL_TAB_COLUMNS where table_name='" + tmp[1].Trim(';') + "' and OWNER='" + tmp[0] + "'");
                            break;
                        case "SqlServer":
                            dt = DBOpt.dbHelper.GetDataTable("select b.name colName,b.xtype colType from " + tmp[0] + ".dbo.sysobjects a," + tmp[0] + ".dbo.syscolumns b where a.id=b.id and a.name='" + tmp[2] + "'");
                            break;
                        case "Sybase":
                            dt = DBOpt.dbHelper.GetDataTable("select b.name colName,b.type colType from " + tmp[0] + ".dbo.sysobjects a," + tmp[0] + ".dbo.syscolumns b where a.id=b.id and a.name='" + tmp[2] + "'");
                            break;
                    }
                    if (dt != null)
                    {
                        DataGridViewColumn[] cols = new DataGridViewColumn[dt.Rows.Count];
                        for(int i=0;i<dt.Rows.Count;i++)
                        {
                            cols[i] = new DataGridViewTextBoxColumn();
                            cols[i].Name = dt.Rows[i][0].ToString();
                        }
                        dgvData.Columns.Clear();
                        dgvData.Columns.AddRange(cols);
                    }
                    //添加数据
                    int row = 0;
                    while (!reader.EndOfStream)
                    {
                        string table = reader.ReadLine();
                        if (table.Contains("insert into "))
                        {
                            int i1 = table.IndexOf('(');
                            int i2 = table.IndexOf(')');
                            string strColum = table.Substring(i1 + 1, i2 - i1 - 1);
                            table = table.Remove(0, i2 + 1);
                            i1 = table.IndexOf("( ");
                            i2 = table.IndexOf(" )");
                            string strValue = "";
                            if (i2 == -1)//这有问题，如果中间有"\r\n"则为0，不知道怎么解决！
                            {
                                strValue = table.Substring(i1 + 1);
                            }
                            else
                            {
                                strValue = table.Substring(i1 + 1, i2 - i1 - 1);
                            }
                            if (strValue.Contains("TO_DATE"))//处理Oracle
                            {
                                strValue = strValue.Replace("TO_DATE(", " ").Replace(",'YYYY-MM-DD HH24:MI:SS')", " ");
                            }
                            string[] column = strColum.Split(',');
                            string[] value = strValue.Split(',');
                            dgvData.Rows.Add(1);
                            for (int i = 0; i < column.Length; i++)
                            {
                                dgvData.Rows[row].Cells[column[i].Trim().TrimStart(' ')].Value = value[i].Trim().TrimStart(' ').TrimStart('\'').TrimEnd('\'');
                            }
                            row = row + 1;
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                }
                else
                {
                    labText.Text = "此文件不是备份数据文件，不能显示数据！";
                    return;
                }
                reader.Close();
                reader.Dispose();
            }
            catch
            {
                labText.Text = "此文件不能显示！";
            }

        }



    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using System.IO;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace PlatForm
{
    public partial class frmBackUpToExcel : Form
    {
        private string _sql;
        Excel._Application app;
        Excel._Workbook wrk;
        Excel.Workbooks objBooks;
        Excel.Sheets objSheets;
        Excel._Worksheet objSheet;

        public frmBackUpToExcel()
        {
            InitializeComponent();
        }

        private void frmBackUpToExcel_Load(object sender, EventArgs e)
        {
            dtpStart.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy") + "-01-01");
            dtpEnd.Value = DateTime.Now;
            initDataBase();
        }

        private void initDataBase()
        {
            //DataTable dt;
            if (DBHelper.databaseType == "Oracle")
            {
                //_sql = "select username from all_users order by user_id";
                cbbDataBase.Items.Add("DF_DMIS");
                cbbDataBase.Items.Add("WEBDMIS");
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                //_sql = "select name from master.dbo.sysdatabases order by name";
                cbbDataBase.Items.Add("XOPENSODB");
                cbbDataBase.Items.Add("WEBDMIS");
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                //_sql = "select name from master.dbo.sysdatabases order by name";
                cbbDataBase.Items.Add("XOPENSODB");
                cbbDataBase.Items.Add("WEBDMIS");
            }
            else
            {
            }
        }

        private void cbbDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            trvTables.Nodes.Clear();

            if (DBHelper.databaseType == "Oracle")
            {
                _sql = "select table_name from all_tables where owner='" + cbbDataBase.SelectedItem.ToString() + "' order by table_name";
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
            }
            else if (DBHelper.databaseType == "Sybase")
            {
            }
            else
            {
            }

            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                trvTables.Nodes.Add(dt.Rows[i][0].ToString());
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in trvTables.Nodes)
                node.Checked = true;
        }

        private void btnSelectClean_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in trvTables.Nodes)
                node.Checked = false;
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                txtPath.Text = dlg.SelectedPath;
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;
            DateTime cur;
            int curMonth, curYear;
            Object obj;

            string path = txtPath.Text.Trim();
            
            if (start > end)
            {
                MessageBox.Show("起始日期不能大于终止日期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(path.Length<1)
            {
                MessageBox.Show("请先选择存放备份文件的目录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!Directory.Exists(path))
            {
                MessageBox.Show("目录:" + txtPath.Text.Trim() + " 不存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Directory.SetCurrentDirectory(path);

            try
            {
                app = new Excel.Application();
            }
            catch
            {
                MessageBox.Show("本机没有安装EXCEL,无法进行此操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            app.Visible = false;
 
            for (int i = 0; i < trvTables.Nodes.Count; i++)
            {
                if (!trvTables.Nodes[i].Checked) continue;

                //找对应的时间列
                _sql = "select QUERY_COL from DMIS_SYS_TABLES where OWNER='" + cbbDataBase.SelectedItem.ToString() + "' and NAME='" + trvTables.Nodes[i].Text + "'";
                obj = DBOpt.dbHelper.ExecuteScalar(_sql);
                if (obj == null || obj.ToString().Trim() == "")
                {
                    if (DBHelper.databaseType == "Oracle")
                        _sql = "select * from " + cbbDataBase.SelectedItem.ToString() + "." + trvTables.Nodes[i].Text;
                    else if (DBHelper.databaseType == "SqlServer" || DBHelper.databaseType == "Sybase")
                        _sql = "select * from " + cbbDataBase.SelectedItem.ToString() + ".dbo." + trvTables.Nodes[i].Text;
                    else
                        _sql = "";

                    GenExcel(_sql, trvTables.Nodes[i].Text, "");
                }
                else
                {
                    if (DBHelper.databaseType == "Oracle")
                    {
                        if (cbbTimeType.Text == "全部" || cbbTimeType.Text == "")
                        {
                            _sql = "select * from " + cbbDataBase.SelectedItem.ToString() + "." + trvTables.Nodes[i].Text + " where to_char(" + obj.ToString() + ",'YYYYMMDD')>='" + start.ToString("yyyyMMdd") + "' and to_char(" + obj.ToString() + ",'YYYYMMDD')<='" + end.ToString("yyyyMMdd") + "'";
                            GenExcel(_sql, trvTables.Nodes[i].Text, start.ToString("yyyyMMdd") + "-" + end.ToString("yyyyMMdd"));
                        }
                        else if (cbbTimeType.Text == "按日")
                        {
                            cur = start;
                            while (cur <= end)
                            {
                                _sql = "select * from " + cbbDataBase.SelectedItem.ToString() + "." + trvTables.Nodes[i].Text + " where to_char(" + obj.ToString() + ",'YYYYMMDD')='" + cur.ToString("yyyyMMdd") + "'";
                                GenExcel(_sql, trvTables.Nodes[i].Text, cur.ToString("yyyyMMdd"));
                                cur = cur.AddDays(1);
                            }
                        }
                        else if (cbbTimeType.Text == "按月")
                        {
                            cur = start;
                            curMonth = Convert.ToInt32(start.ToString("yyyyMM"));
                            while (curMonth <= Convert.ToInt32(end.ToString("yyyyMM")))
                            {
                                _sql = "select * from " + cbbDataBase.SelectedItem.ToString() + "." + trvTables.Nodes[i].Text + " where to_char(" + obj.ToString() + ",'YYYYMM')='" + cur.ToString("yyyyMM") + "'";
                                GenExcel(_sql, trvTables.Nodes[i].Text, cur.ToString("yyyyMM"));
                                cur = cur.AddMonths(1);
                                curMonth = Convert.ToInt32(cur.ToString("yyyyMM"));
                            }
                        }
                        else if (cbbTimeType.Text == "按年")
                        {
                            cur = start;
                            curYear = start.Year;
                            while (curYear <= end.Year)
                            {
                                _sql = "select * from " + cbbDataBase.SelectedItem.ToString() + "." + trvTables.Nodes[i].Text + " where to_char(" + obj.ToString() + ",'YYYY')='" + cur.Year + "'";
                                GenExcel(_sql, trvTables.Nodes[i].Text, cur.ToString("yyyy"));
                                cur = cur.AddYears(1);
                                curYear = cur.Year;
                            }
                        }
                    }
                    else if (DBHelper.databaseType == "SqlServer" || DBHelper.databaseType == "Sybase")
                    {
                        if (cbbTimeType.Text == "全部")
                        {
                            _sql = "select * from " + cbbDataBase.SelectedItem.ToString() + "." + trvTables.Nodes[i].Text + " where convert(char(8)," + obj.ToString() + ",112)>='" + start.ToString("yyyyMMdd") + "' and convert(char(8)," + obj.ToString() + ",112)<='" + end.ToString("yyyyMMdd") + "'";
                            GenExcel(_sql, trvTables.Nodes[i].Text, start.ToString("yyyyMMdd") + "-" + end.ToString("yyyyMMdd"));
                        }
                        else if (cbbTimeType.Text == "按日")
                        {
                            cur = start;
                            while (cur <= end)
                            {
                                _sql = "select * from " + cbbDataBase.SelectedItem.ToString() + "." + trvTables.Nodes[i].Text + " where convert(char(8)," + obj.ToString() + ",112)='" + cur.ToString("yyyyMMdd") + "'";
                                GenExcel(_sql, trvTables.Nodes[i].Text, cur.ToString("yyyyMMdd"));
                                cur = cur.AddDays(1);
                            }
                        }
                        else if (cbbTimeType.Text == "按月")
                        {
                            cur = start;
                            curMonth = Convert.ToInt16(start.ToString("yyyyMM"));
                            while (curMonth <= Convert.ToInt16(end.ToString("yyyyMM")))
                            {
                                _sql = "select * from " + cbbDataBase.SelectedItem.ToString() + "." + trvTables.Nodes[i].Text + " where convert(char(6)," + obj.ToString() + ",112)='" + cur.ToString("yyyyMM") + "'";
                                GenExcel(_sql, trvTables.Nodes[i].Text, cur.ToString("yyyyMM"));
                                cur = cur.AddMonths(1);
                                curMonth = Convert.ToInt16(cur.ToString("yyyyMM"));
                            }
                        }
                        else if (cbbTimeType.Text == "按年")
                        {
                            cur = start;
                            curYear = start.Year;
                            while (curYear <= end.Year)
                            {
                                _sql = "select * from " + cbbDataBase.SelectedItem.ToString() + "." + trvTables.Nodes[i].Text + " where convert(char(4)," + obj.ToString() + ",112)='" + cur.Year + "'";
                                GenExcel(_sql, trvTables.Nodes[i].Text, cur.ToString("yyyy"));
                                cur = cur.AddYears(1);
                                curYear = cur.Year;
                            }
                        }
                    }
                    else
                        _sql = "";
                }
            }
            //关闭EXCEL进程
            app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wrk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheets);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
            app = null;
            wrk = null;
            objBooks = null;
            objSheets = null;
            objSheet = null;
            GC.Collect();
        }

        private void btnSaveMessage_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfg = new SaveFileDialog();
            sfg.Filter = "Text files (*.txt)|*.txt";
            if (sfg.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfg.FileName);
                for (int i = 0; i < lsbMsg.Items.Count; i++) 
                    sw.WriteLine(lsbMsg.Items[i]);
                sw.Close();
            }
        }

        private void GenExcel(string sql,string tableName,string ymd)
        {
            DataTable val;
            val = DBOpt.dbHelper.GetDataTable(sql);
            if (val == null) return;
            string excelFileName;

            if (ymd != "")
                excelFileName = txtPath.Text + "\\" + tableName + "(" + ymd + ").xls";
            else
                excelFileName = txtPath.Text + "\\" + tableName + ".xls";

            //防止出现提示覆盖窗口,先删除已经存在的同名文件
            if (File.Exists(excelFileName)) File.Delete(excelFileName);

            object m_objOpt = System.Reflection.Missing.Value;
            objBooks = app.Workbooks;
            wrk = (Excel._Workbook)objBooks.Add(Missing.Value);
            objSheets = wrk.Worksheets;
            objSheet = (Excel._Worksheet)objSheets.get_Item(1); 
             

            //先写列名
            for (int i = 1; i <= val.Columns.Count; i++)
                objSheet.Cells[1, i] = val.Columns[i-1].ColumnName;

            //写数据
            for (int i = 0; i < val.Rows.Count; i++)
            {
                for (int j = 0; j < val.Columns.Count; j++)
                {
                    objSheet.Cells[i + 2, j + 1] = val.Rows[i][j]; 
                }
            }
            
            wrk.SaveAs(excelFileName, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, 
                Excel.XlSaveAsAccessMode.xlNoChange, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);

        }

        private void frmBackUpToExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭EXCEL
            if(app!=null) app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wrk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheets);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
            app = null;
            wrk = null;
            objBooks = null;
            objSheets = null;
            objSheet = null;
            GC.Collect();
        }


    }
}
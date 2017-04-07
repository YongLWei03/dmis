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
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;

namespace PlatForm
{
    public partial class frmLoadFromExcel : Form
    {
        private string _sql;

        public frmLoadFromExcel()
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
                FileInfo[] afi = di.GetFiles("*.xls", SearchOption.TopDirectoryOnly);
                foreach (FileInfo fi in afi)
                    ckbFiles.Items.Add(fi.Name);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "") return;
            if (ckbFiles.CheckedItems.Count < 1) return;

            //try
            //{
            //    app = new Excel.Application();
            //}
            //catch
            //{
            //    MessageBox.Show("����û�а�װEXCEL,�޷����д˲���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //app.Visible = false;

            //��EXCEL�ļ��е����ݶ���DataSet�С�

            string tableName;
            int pos;
            DataSet dsMyDataSet;
            DataTable stru;
            for (int i = 0; i < ckbFiles.CheckedItems.Count; i++)
            {
                pos = ckbFiles.CheckedItems[i].ToString().IndexOf('(');
                if (pos > 0)
                    tableName = ckbFiles.CheckedItems[i].ToString().Substring(0, pos);
                else
                {
                    pos = ckbFiles.CheckedItems[i].ToString().IndexOf('.');
                    tableName = ckbFiles.CheckedItems[i].ToString().Substring(0, pos);
                }
                //�õ��˴˱�Ľṹ
                _sql = "select * from " + tableName + " where 1=0";
                stru = DBOpt.dbHelper.GetDataTable(_sql);
                if (stru == null) //���ݿ��в����ڴ˱�
                {
                    lsbMsg.Items.Add("���ݿ��в����ڱ�:" + tableName );
                    return;  
                }

                try
                {
                    string strConString = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '" + txtPath.Text +"\\"+ ckbFiles.CheckedItems[i].ToString() + "';Extended Properties=\"Excel 8.0;IMEX=1\"";
                    OleDbConnection oleDbCon = new OleDbConnection(strConString);
                    OleDbDataAdapter oleDbAdapter = new OleDbDataAdapter("select * from [Sheet1$]", oleDbCon);
                    dsMyDataSet = new DataSet();
                    oleDbAdapter.Fill(dsMyDataSet);
                }
                catch (Exception ex)
                {
                    lsbMsg.Items.Add("��ȡ�ļ�" + ckbFiles.CheckedItems[i].ToString()+"ʧ��!");
                    continue;
                }
                if (dsMyDataSet.Tables[0].Rows.Count < 2)
                {
                    lsbMsg.Items.Add("�ļ�" + ckbFiles.CheckedItems[i].ToString() + "����������!");
                    continue;
                }

                OracleParameterCollection oraParas = new OracleParameterCollection();
                StringBuilder insertSql = new StringBuilder();
                StringBuilder insertCol = new StringBuilder();
                insertSql.Append("insert into " + tableName+"(");
                for (int j = 0; j < dsMyDataSet.Tables[0].Columns.Count; j++)
                {
                    for (int k = 0; k < stru.Columns.Count; k++)
                    {
                        if (dsMyDataSet.Tables[0].Columns[j].ColumnName == stru.Columns[k].ColumnName)
                        {
                            if (DBHelper.databaseType == "Oracle")
                            {
                                OracleParameter p = new OracleParameter();
                                p.ParameterName = stru.Columns[k].ColumnName;
                                insertSql.Append(stru.Columns[k].ColumnName + ",");
                                insertCol.Append(":" + stru.Columns[k].ColumnName + ",");
                                switch (stru.Columns[k].DataType.Name)
                                {
                                    case "String":
                                        p.OracleType = OracleType.VarChar;
                                        break;
                                    case "Char":
                                        p.OracleType = OracleType.Char;
                                        break;
                                    case "DateTime":
                                        p.OracleType = OracleType.DateTime;
                                        break;
                                    case "Int16":
                                        p.OracleType = OracleType.Int16;
                                        break;
                                    case "Int32":
                                        p.OracleType = OracleType.Int32;
                                        break;
                                    case "Decimal":
                                    case "Single":
                                        p.OracleType = OracleType.Float;
                                        break;
                                    case "Double":
                                        p.OracleType = OracleType.Double;
                                        break;
                                    case "UInt16":
                                        p.OracleType = OracleType.UInt16;
                                        break;
                                    case "UInt32":
                                        p.OracleType = OracleType.UInt32;
                                        break;
                                    case "Boolean":
                                    case "Byte":
                                    case "SByte":
                                    default:
                                        break;
                                }
                                oraParas.Add(p);
                            }
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
                    }
                }
                insertSql.Remove(insertSql.Length - 1, 1);
                insertSql.Append(") values(" + insertCol.Remove(insertCol.Length - 1, 1) + ")");

                //
                for (int k = 0; k < dsMyDataSet.Tables[0].Rows.Count; k++)
                {
                    for (int j = 0; j < dsMyDataSet.Tables[0].Columns.Count; j++)
                        ((OracleParameter)oraParas[j]).Value = dsMyDataSet.Tables[0].Rows[k][j];

                    if (DBHelper.databaseType == "Oracle")
                    {
                        DBOpt.dbHelper.ExecuteByParameter(insertSql.ToString(), oraParas);
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
                    
                }
            }
           
        }

        private void btnSaveMsg_Click(object sender, EventArgs e)
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

 


    }
}
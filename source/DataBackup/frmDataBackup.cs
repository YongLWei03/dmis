using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using System.IO;
using System.Data.Common;

namespace DataBackup
{
    public partial class frmDataBackup : Form
    {
        string _sql;

        public frmDataBackup()
        {
            InitializeComponent();
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
            {//�����ǿ��Ǳ�Ǽǵ������
                //object tableItem = DBOpt.dbHelper.ExecuteScalar("select DESCR from DMIS_SYS_TABLES where NAME='" + dt.Rows[i][0].ToString() + "'");
                //if (tableItem != null)
                //    lsbTable.Items.Add(tableItem.ToString());
                //else
                    lsbTable.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        private void frmDataBackup_Load(object sender, EventArgs e)
        {
            VisibleQueryCol(false);
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

        private void VisibleQueryCol(bool bl)
        {
            groupBox3.Visible = bl;
            rdbTime1.Visible = bl;
            rdbTime2.Visible = bl;
            label2.Visible = bl;
            label3.Visible = bl;
            cbbFYear.Visible = bl;
            cbbFMounth.Visible = bl;
            if (bl == false)
            {
                cbbEYear.Visible = bl;
                cbbEMounth.Visible = bl;
                label4.Visible = bl;
                label5.Visible = bl;
            }
            else
            {
                cbbFYear.SelectedIndex = DateTime.Now.Year - 2005;
                cbbFMounth.SelectedIndex = DateTime.Now.Month - 1;
                rdbTime1.Checked = bl;
            }
        }

        private void rdbQueryCol_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbQueryCol.Checked == true)
            {
                VisibleQueryCol(true);
            }
            else
            {
                VisibleQueryCol(false);
            }
        }

        private void rdbTime2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTime2.Checked == true)
            {
                cbbEYear.Visible = true;
                cbbEMounth.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                cbbEYear.SelectedIndex = DateTime.Now.Year - 2005;
                cbbEMounth.SelectedIndex = DateTime.Now.Month - 1;
            }
            else
            {
                cbbEYear.Visible = false;
                cbbEMounth.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
            }
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
        //1����ȫ����2������ѯ�У����û�в�ѯ�оͰ�ȫ��
        //����ѯ�У�1��ʱ�䣻2��ʱ���
        private void btnCreate_Click(object sender, EventArgs e)
        {
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
            if (lsbTable.SelectedItems.Count == 0)
            {
                MessageBox.Show("��ѡ��Ҫ���ݵı�");
                return;
            }
            lsbInfo.Items.Clear();
            btnCreate.Enabled = false;
            for (int i = 0; i < lsbTable.SelectedItems.Count; i++)
            {
                string strFileName = strPath + "/" + lsbTable.SelectedItems[i].ToString() + ".txt";
                if (File.Exists(strFileName))
                {
                    lsbInfo.Items.Add(lsbTable.SelectedItems[i].ToString() + "�Ѿ����ڲ��ܱ��ݸñ�;");
                }
                else
                {
                    string strTableName = "";
                    if(DBHelper.databaseType != "Oracle")
                        strTableName = label7.Text + ".dbo." + lsbTable.SelectedItems[i].ToString();
                    else
                        strTableName = label7.Text + "." + lsbTable.SelectedItems[i].ToString();
                    FileStream fs = new FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs,Encoding.Default);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    string[] result = new string[0];//���ɵĽ������
                    string strWhere = "";//��ѯ����
                    if (rdbQueryAll.Checked == true)//ȫ������
                    {
                        result = DataReaderToArray("select * from " + strTableName, strTableName);
                        if (DBHelper.databaseType != "Oracle")
                        {
                            sw.WriteLine("delete from " + strTableName);
                            sw.WriteLine("go");
                            sw.WriteLine("print '�ɹ�ɾ����" + strTableName + "�е�" + result.Length.ToString() + "����¼!'");
                            sw.WriteLine("go");
                        }
                        else
                        {
                            sw.WriteLine("delete from " + strTableName + ";");
                            sw.WriteLine("commit;");
                            sw.WriteLine("rem '�ɹ�ɾ����" + strTableName + "�е�" + result.Length.ToString() + "����¼!'");
                            sw.WriteLine("commit;");
                        }
                    }
                    else//����ѯ��
                    {
                        object strQueryCol = DBOpt.dbHelper.ExecuteScalar("select QUERY_COL from DMIS_SYS_TABLES where NAME='" + lsbTable.SelectedItems[i].ToString() + "'");
                        if (strQueryCol is System.DBNull || strQueryCol == null || strQueryCol.ToString().Trim() == "")//�ж϶������ѯ��û��û��ȫ�����ݣ�
                        {
                            result = DataReaderToArray("select * from " + strTableName, strTableName);
                            if (DBHelper.databaseType != "Oracle")
                            {
                                sw.WriteLine("delete from " + strTableName);
                                sw.WriteLine("go");
                                sw.WriteLine("print '�ɹ�ɾ����" + strTableName + "�е�" + result.Length.ToString() + "����¼!'");
                                sw.WriteLine("go");
                            }
                            else
                            {
                                sw.WriteLine("delete from " + strTableName + ";");
                                sw.WriteLine("commit;");
                                sw.WriteLine("rem '�ɹ�ɾ����" + strTableName + "�е�" + result.Length.ToString() + "����¼!'");
                                sw.WriteLine("commit;");
                            }
                        }
                        else
                        {
                            if (rdbTime1.Checked == true)//����
                            {
                                if (DBHelper.databaseType != "Oracle")
                                {
                                    strWhere = " where convert(char(6)," + strQueryCol.ToString() + ",112)='" + cbbFYear.SelectedItem + cbbFMounth.SelectedItem + "'";
                                    sw.WriteLine("delete from " + strTableName + strWhere);
                                    sw.WriteLine("go");
                                    result = DataReaderToArray("select * from " + strTableName + strWhere, strTableName);
                                    sw.WriteLine("print '�ɹ�ɾ����" + strTableName + "�е�" + result.Length.ToString() + "����¼!'");
                                    sw.WriteLine("go");
                                }
                                else
                                {
                                    strWhere = " where to_char(" + strQueryCol.ToString() + ",'yyyymm')=" + cbbFYear.SelectedItem + cbbFMounth.SelectedItem;
                                    sw.WriteLine("delete from " + strTableName + strWhere + ";");
                                    sw.WriteLine("commit;");
                                    result = DataReaderToArray("select * from " + strTableName + strWhere, strTableName);
                                    sw.WriteLine("rem '�ɹ�ɾ����" + strTableName + "�е�" + result.Length.ToString() + "����¼!'");
                                    sw.WriteLine("commit;");
                                }
                            }
                            else//��ʱ���(�����µ�����)
                            {
                                if (DBHelper.databaseType != "Oracle")
                                {
                                    strWhere = " where convert(char(6)," + strQueryCol.ToString() + ",112)>='" + cbbFYear.SelectedItem + cbbFMounth.SelectedItem + "' and convert(char(6)," + strQueryCol.ToString() + ",112)<='" + cbbEYear.SelectedItem + cbbEMounth.SelectedItem + "'";
                                    sw.WriteLine("delete from " + strTableName + strWhere);
                                    sw.WriteLine("go");
                                    result = DataReaderToArray("select * from " + strTableName + strWhere, strTableName);
                                    sw.WriteLine("print '�ɹ�ɾ����" + strTableName + "�е�" + result.Length.ToString() + "����¼!'");
                                    sw.WriteLine("go");
                                }
                                else
                                {
                                    strWhere = " where to_char(" + strQueryCol.ToString() + ",'yyyymm')>=" + cbbFYear.SelectedItem + cbbFMounth.SelectedItem + " and to_char(" + strQueryCol.ToString() + ",'yyyymm')<=" + cbbFYear.SelectedItem + cbbFMounth.SelectedItem;
                                    sw.WriteLine("delete from " + strTableName + strWhere + ";");
                                    sw.WriteLine("commit;");
                                    result = DataReaderToArray("select * from " + strTableName + strWhere, strTableName);
                                    sw.WriteLine("rem '�ɹ�ɾ����" + strTableName + "�е�" + result.Length.ToString() + "����¼!'");
                                    sw.WriteLine("commit;");
                                }
                            }
                        }
                    }
                    
                    for (int j = 0; j < result.Length; j++)
                    {
                        sw.WriteLine(result[j]);
                        if (j%10 == 9)
                        {
                            if (DBHelper.databaseType != "Oracle")
                                sw.WriteLine("go");
                            else
                                sw.WriteLine("commit;");
                        }

                    }
                    lsbInfo.Items.Add(lsbTable.SelectedItems[i].ToString() + "���ݳɹ���������"+result.Length.ToString()+"����¼;");
                    if (result.Length != 0)
                    {
                        if (DBHelper.databaseType != "Oracle")
                        {
                            sw.WriteLine("print '�ɹ������" + strTableName + "�е�" + result.Length.ToString() + "����¼!'");
                            sw.WriteLine("go");
                        }
                        else
                        {
                            sw.WriteLine("rem '�ɹ������" + strTableName + "�е�" + result.Length.ToString() + "����¼!'");
                            sw.WriteLine("commit;");
                        }
                    }
                    sw.Flush();
                    fs.Close();
                }
            }
            string fileName = strPath + "/insert.txt";
            FileStream fs1 = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1, Encoding.Default);
            sw1.BaseStream.Seek(0, SeekOrigin.End);
            for (int i = 0; i < lsbTable.SelectedItems.Count; i++)
            {
                switch (DBHelper.databaseType)
                {
                    case "Oracle":
                        sw1.WriteLine("sqlplus xopens/ytdf000@dbs1 < " + lsbTable.SelectedItems[i].ToString() + ".txt");
                        break;
                    case "Sybase":
                        sw1.WriteLine("isql -Usa -P -Ssybase11 < " + lsbTable.SelectedItems[i].ToString() + ".txt");
                        break;
                    case "SqlServer":
                        sw1.WriteLine("osql -Usa -P -Sdbs1 < " + lsbTable.SelectedItems[i].ToString() + ".txt");
                        break;
                }
            }
            sw1.Flush();
            fs1.Close();
            btnCreate.Enabled = true;
        }

        private string[] DataReaderToArray(string sql, string tableName)
        {
            //08.10.27 GLT�����޸�,����DataReader,����StringBuilder��ΪString.
            DataTable results = DBOpt.dbHelper.GetDataTable(sql);
            string[] arr = new string[results.Rows.Count];

            //�ٴ�������
            int ii = 0;
            for (int m = 0; m < results.Rows.Count; m++)
            {
                string vals = "";
                string cols = "";
                for (int n = 0; n < results.Columns.Count; n++)
                {
                    if (!(results.Rows[m][n] is System.DBNull))
                    {
                        cols += results.Columns[n].ColumnName + ",";
                        switch (results.Columns[n].DataType.FullName)
                        {
                            case "System.String":
                                //vals += "'" + results.Rows[m][n].ToString().Replace("'", "\"") + "',";
                                vals += "'" + results.Rows[m][n].ToString() + "',";
                                break;
                            case "System.DateTime":
                                if (DBHelper.databaseType == "Oracle")
                                    vals += "TO_DATE('" + Convert.ToDateTime(results.Rows[m][n]).ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS'),";
                                else
                                    vals += "'" + results.Rows[m][n].ToString() + "',";
                                break;
                            default:
                                vals += results.Rows[m][n].ToString() + ",";
                                break;
                        }

                    }
                }
                if (DBHelper.databaseType != "Oracle")
                    arr[ii] = "insert into " + tableName + " ( " + cols.TrimEnd(',') + " ) values ( " + vals.TrimEnd(',') + " )";
                else
                    arr[ii] = "insert into " + tableName + " ( " + cols.TrimEnd(',') + " ) values ( " + vals.TrimEnd(',') + " );";
                ii = ii + 1;
            }
            return arr;
        }

     }
}
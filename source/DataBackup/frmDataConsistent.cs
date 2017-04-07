using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using PlatForm.DBUtility;
using System.IO;

namespace DataBackup
{
    public partial class frmDataConsistent : Form
    {
        private string _sql;
        private OracleConnection oraConnMain;
        private OracleConnection oraConnSlave;
        private OracleCommand oraCmd;
        private OracleDataAdapter oraDa;
        private DataSet dsMainData;
        private DataSet dsSlaveData;
        private DataTable dtMainTable;
        private DataTable dtSlaveTable;

        public frmDataConsistent()
        {
            InitializeComponent();
            
        }

        private void frmDataConsistent_Load(object sender, EventArgs e)
        {
            lblMsg1.Text = "";
            if (ConfigurationManager.AppSettings["DoubleDatabase"] == "Yes")
            {
                if (DBHelper.databaseType == "Oracle")
                {
                    oraConnMain = new OracleConnection(ConfigurationManager.AppSettings["OraConnStringMain"]);
                    oraConnSlave = new OracleConnection(ConfigurationManager.AppSettings["OraConnStringSlave"]);
                    try
                    {
                        oraConnMain.Open();
                        oraConnSlave.Open();
                    }
                    catch
                    {
                        //MessageBox.Show("系统一个数据库服务器的还存处于故障状态，不允许使用此功能！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        btnQuery.Enabled = false;
                        return;
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
                dtpStart1.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy") + "-01-01");
                dtpEnd1.Value = DateTime.Now;
                initDataBase();

                dtpStart.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy")+"-01-01");
                dtpEnd.Value = DateTime.Now;
                initTables();
            }
            else
            {
                //MessageBox.Show("系统设置为单数据库服务器的模式，不允许使用此功能！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnQuery.Enabled = false;
                //btnStart2.Enabled = false;
                return;
            }
        }

        #region tabpage1

        private void initDataBase()
        {
            //DataTable dt;
            if (DBHelper.databaseType == "Oracle")
            {
                //_sql = "select username from all_users order by user_id";
                //dt = DBOpt.dbHelper.GetDataTable(_sql);
                ////只允许操作DF8360的数据库
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (!(dt.Rows[i][0].ToString() == "DF_DMIS" || dt.Rows[i][0].ToString() == "WEBDMIS")) continue;
                //    cbbDataBase.Items.Add(dt.Rows[i][0].ToString());
                //}
                cbbDataBase.Items.Add("DF_DMIS");
                cbbDataBase.Items.Add("WEBDMIS");
                cbbDataBase3.Items.Add("DF_DMIS");
                cbbDataBase3.Items.Add("WEBDMIS");
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                //_sql = "select name from master.dbo.sysdatabases order by name";
                cbbDataBase.Items.Add("XOPENSODB");
                cbbDataBase.Items.Add("WEBDMIS");
                cbbDataBase3.Items.Add("DF_DMIS");
                cbbDataBase3.Items.Add("WEBDMIS");
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                //_sql = "select name from master.dbo.sysdatabases order by name";
                cbbDataBase.Items.Add("XOPENSODB");
                cbbDataBase.Items.Add("WEBDMIS");
                cbbDataBase3.Items.Add("DF_DMIS");
                cbbDataBase3.Items.Add("WEBDMIS");
            }
            else
            {
            }
        }

        private void cbbDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbTable.Items.Clear();
            if (DBHelper.databaseType == "Oracle")
            {
                _sql = "select table_name from all_all_tables where owner='" + cbbDataBase.SelectedItem.ToString() + "' order by table_name";
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                _sql = "select name from " + cbbDataBase.SelectedItem.ToString() + ".dbo.sysobjects where type in ('U','V')  order by name";
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                _sql = "select name from " + cbbDataBase.SelectedItem.ToString() + ".dbo.sysobjects where type in ('U','V') order by name ";
            }
            else
            {
            }

            DataTable dtTable = new DataTable();
            oraDa = new OracleDataAdapter();
            oraCmd = new OracleCommand(_sql, oraConnMain);
            oraCmd.CommandType = CommandType.Text;
            oraDa.SelectCommand = oraCmd;
            oraDa.Fill(dtTable);
  
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbbTable.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            lblMsg1.Text = "";
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;
            if (start > end)
            {
                //MessageBox.Show("起始日期不能大于终止日期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                errorProvider1.SetError((Control)dtpStart, " ");
                return;
            }
            else
                errorProvider1.SetError((Control)dtpStart, "");

            if (cbbTable.Text.Trim()=="") return;

            this.Cursor = Cursors.WaitCursor;

            object obj;
            string wheres;
            _sql = "select query_col from dmis_sys_tables where owner='" + cbbDataBase.Text + "' and name='" + cbbTable.Text + "'";
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj == null || obj.ToString().Trim()=="")
                wheres = "1=1";
            else
                wheres = "to_char(" + obj.ToString() + ",'YYYYMMDD')>='" + start.ToString("yyyyMMdd") +"' and to_char(" + obj.ToString() + ",'YYYYMMDD')<='" + end.ToString("yyyyMMdd") + "'";

            if (DBHelper.databaseType == "Oracle")
            {
                obj=DBOpt.dbHelper.ExecuteScalar("select c.column_name from user_indexes i,user_ind_columns c where i.index_name=c.index_name and i.table_name ='" + cbbTable.Text + "' and i.uniqueness='UNIQUE'");
                if (obj == null)
                    _sql = "select * from " + cbbTable.Text + " where " + wheres;
                else
                    _sql = "select * from " + cbbTable.Text + " where " + wheres + " order by " + obj.ToString();
                

                dsMainData = new DataSet();
                oraDa = new OracleDataAdapter();
                oraCmd = new OracleCommand(_sql, oraConnMain);
                oraCmd.CommandType = CommandType.Text;
                oraDa.SelectCommand = oraCmd;
                oraDa.Fill(dsMainData);
                dgvMainServer1.AutoGenerateColumns = true;
                dgvMainServer1.DataSource = dsMainData.Tables[0];
                grpMain1.Text = "Main Database has " + dsMainData.Tables[0].Rows.Count + " records";
                dsSlaveData = new DataSet();
                oraCmd.Connection = oraConnSlave;
                oraCmd.CommandType = CommandType.Text;
                oraDa.SelectCommand = oraCmd;
                try
                {
                    oraDa.Fill(dsSlaveData);
                    dgvSlaveServer1.AutoGenerateColumns = true;
                    dgvSlaveServer1.DataSource = dsSlaveData.Tables[0];
                    grpSlave1.Text = "Slave Database has  " + dsSlaveData.Tables[0].Rows.Count + " records";
                }
                catch
                {
                    lblMsg1.Text = cbbTable.Text + " is not exist in Slave Database.";
                    return;
                }
                if (dsMainData.Tables[0].Columns.Count != dsSlaveData.Tables[0].Columns.Count)
                {
                    lblMsg1.Text = "Number of fields is not equal.";
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
            this.Cursor = Cursors.Arrow;
        }

        #endregion


        #region tabpage2  维护差异数据

        private void initTables()
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select id,name,descr from dmis_sys_tables order by type_id,order_id,descr";
            else
                _sql = "select id,name,OTHER_LANGUAGE_DESCR from dmis_sys_tables order by type_id,order_id,descr";

            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
                ckbTables.Items.Add(dt.Rows[i][2].ToString() + "(" + dt.Rows[i][0].ToString() + ")", false);
          }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbTables.Items.Count; i++)
                ckbTables.SetItemChecked(i, true);
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbTables.Items.Count; i++)
                ckbTables.SetItemChecked(i, false);
        }

        //重要的代码
        private void btnStart_Click(object sender, EventArgs e)
        {
            string tableID;
            int pos;  //最后一个'('的位置
            int counts;

            DateTime start, end;
            string tableName;
            DataTable tableParas;   //配置的表参数
            DataTable primaryKeys;  //主键列
            DataTable primaryData = new DataTable();         //主服务器中的数据
            DataTable slaveData = new DataTable();           //备服务器中的数据    思路:先把两个数据库中的数据都找出来,再根据主键互相比较,而不用每间次都从数据库中找
            StringBuilder insertSql = new StringBuilder();
            StringBuilder insertParas = new StringBuilder();

            lsbMsg.Items.Clear();

            start = dtpStart.Value;
            end = dtpEnd.Value;
            
            if (start > end)
            {
                //MessageBox.Show("起始日期不能大于终止日期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                errorProvider1.SetError((Control)dtpStart, " ");
                return;
            }
            else
                errorProvider1.SetError((Control)dtpStart, "");

            this.Cursor = Cursors.WaitCursor;

            oraCmd = new OracleCommand();
            oraDa = new OracleDataAdapter(); 

            for (int i = 0; i < ckbTables.CheckedItems.Count; i++)
            {
                //必须加如下语句，否则会出错
                primaryData.Clear();
                slaveData.Clear();
                primaryData.Constraints.Clear();
                slaveData.Constraints.Clear();
                primaryData.Columns.Clear();
                slaveData.Columns.Clear();

                counts = 0;
                pos = ckbTables.CheckedItems[i].ToString().LastIndexOf('(');
                tableID = ckbTables.CheckedItems[i].ToString().Substring(pos + 1, ckbTables.CheckedItems[i].ToString().Length - pos - 2);
                _sql = "select owner,name,query_col from dmis_sys_tables where id=" + tableID;
                tableParas = DBOpt.dbHelper.GetDataTable(_sql);
                if (tableParas == null || tableParas.Rows.Count < 1) continue;
                tableName = tableParas.Rows[0][0].ToString() + "." + tableParas.Rows[0][1].ToString();

                _sql = "select name from dmis_sys_columns where table_id=" + tableID + " and isprimary=1";
                primaryKeys = DBOpt.dbHelper.GetDataTable(_sql);
                if (primaryKeys == null || primaryKeys.Rows.Count < 1)
                {
                    lsbMsg.Items.Add(tableName + " has no primary key in paras.");
                    continue;  //没有主键不进行操作
                }

                if (tableParas.Rows[0][2] == Convert.DBNull || tableParas.Rows[0][2].ToString().Trim() == "")
                {
                    _sql = "select * from " + tableName;
                }
                else
                {
                    _sql = "select * from " + tableName + " where to_char(" + tableParas.Rows[0][2].ToString() + ",'YYYYMMDD')>='" + start.ToString("yyyyMMdd") +
                        "' and to_char(" + tableParas.Rows[0][2].ToString() + ",'YYYYMMDD')<='" + end.ToString("yyyyMMdd") + "'";
                }

                //取主数据库服务器中的数据
                oraCmd.Connection = oraConnMain;
                oraCmd.Parameters.Clear();
                oraCmd.CommandType = CommandType.Text;
                oraCmd.CommandText = _sql;
                oraDa.SelectCommand = oraCmd;
                oraDa.Fill(primaryData);

                //取备数据库服务器中的数据
                oraCmd.Connection = oraConnSlave;
                oraCmd.Parameters.Clear();
                oraCmd.CommandType = CommandType.Text;
                oraCmd.CommandText = _sql;
                oraDa.SelectCommand = oraCmd;
                oraDa.Fill(slaveData);

                //找带参数的sql语句
                insertSql.Remove(0, insertSql.Length);
                insertParas.Remove(0, insertParas.Length);
                insertSql.Append("insert into " + tableName + "(");
                for (int j = 0; j < primaryData.Columns.Count; j++)
                {
                    insertSql.Append(primaryData.Columns[j].ColumnName + ",");
                    insertParas.Append(":" + primaryData.Columns[j].ColumnName + ",");
                }
                insertSql.Remove(insertSql.Length - 1, 1);
                insertSql.Append(") values(");
                insertSql.Append(insertParas.Remove(insertParas.Length - 1, 1) + ")");

                //给两个DataTable设置相同的主键
                DataColumn[] keys = new DataColumn[primaryKeys.Rows.Count];
                DataColumn[] keys2 = new DataColumn[primaryKeys.Rows.Count];
                for (int j = 0; j < primaryKeys.Rows.Count; j++)
                {
                    for (int k = 0; k < primaryData.Columns.Count; k++)
                    {
                        if (primaryKeys.Rows[j][0].ToString().ToUpper() == primaryData.Columns[k].ColumnName.ToUpper())
                            keys[j] = primaryData.Columns[k];
                        if (primaryKeys.Rows[j][0].ToString().ToUpper() == slaveData.Columns[k].ColumnName.ToUpper())
                            keys2[j] = slaveData.Columns[k];
                    }
                }
                primaryData.PrimaryKey = keys;
                slaveData.PrimaryKey = keys2;

                //扫描主服务器中取出的数据,看在从备服务器取出的数据中是否存在.
                //把主服务器中的数据(但在备服务器中的数据)拷贝到备服务器中.
                object[] cols = new object[primaryKeys.Rows.Count];
                OracleParameter para;
                for (int j = 0; j < primaryData.Rows.Count; j++)
                {
                    for (int k = 0; k < primaryKeys.Rows.Count; k++)
                    {
                        cols[k] = primaryData.Rows[j][primaryKeys.Rows[k][0].ToString()];
                    }
                    if (slaveData.Rows.Contains(cols)) continue;
                    for (int m = 0; m < primaryData.Columns.Count; m++)
                    {
                        para = new OracleParameter();
                        para.ParameterName = primaryData.Columns[m].ColumnName;
                        para.Value = primaryData.Rows[j][m];
                        switch (primaryData.Columns[m].DataType.FullName)
                        {
                            case "System.Decimal":
                            case "System.Double":
                            case "System.Single":
                                para.OracleType = OracleType.Double;
                                break;
                            case "System.Varchar":
                                para.OracleType = OracleType.VarChar;
                                break;
                            case "System.DateTime":
                                para.OracleType = OracleType.DateTime;
                                break;
                            case "System.Iamge":
                                para.OracleType = OracleType.BFile;
                                break;
                            case "System.Int":
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                                para.OracleType = OracleType.Int32;
                                break;
                            case "System.Text":
                                para.OracleType = OracleType.LongVarChar;
                                break;
                            default:
                                para.OracleType = OracleType.VarChar;
                                break;

                        }
                        oraCmd.Parameters.Add(para);
                    }
                    oraCmd.Connection = oraConnSlave;
                    oraCmd.CommandType = CommandType.Text;
                    oraCmd.CommandText = insertSql.ToString();
                    if (oraCmd.ExecuteNonQuery() > 0)
                        counts++;
                    oraCmd.Parameters.Clear();
                }
                lsbMsg.Items.Add(tableName + " from main to slave success " + counts + " records!");//主数据库往备数据库成功导入
                //再把备服务器中的数据拷贝到主服务器中
                //重新取备服务器中的数据
                oraCmd.Connection = oraConnSlave;
                oraCmd.Parameters.Clear();
                oraCmd.CommandType = CommandType.Text;
                oraCmd.CommandText = _sql;
                oraDa.SelectCommand = oraCmd;
                oraDa.Fill(slaveData);
                counts = 0;
                for (int j = 0; j < slaveData.Rows.Count; j++)
                {
                    for (int k = 0; k < primaryKeys.Rows.Count; k++)
                    {
                        cols[k] = slaveData.Rows[j][primaryKeys.Rows[k][0].ToString()];
                    }
                    if (primaryData.Rows.Contains(cols)) continue;
                    for (int m = 0; m < slaveData.Columns.Count; m++)
                    {
                        para = new OracleParameter();
                        para.ParameterName = slaveData.Columns[m].ColumnName;
                        para.Value = slaveData.Rows[j][m];
                        switch (slaveData.Columns[m].DataType.FullName)
                        {
                            case "System.Decimal":
                            case "System.Double":
                            case "System.Single":
                                para.OracleType = OracleType.Double;
                                break;
                            case "System.Varchar":
                                para.OracleType = OracleType.VarChar;
                                break;
                            case "System.DateTime":
                                para.OracleType = OracleType.DateTime;
                                break;
                            case "System.Iamge":
                                para.OracleType = OracleType.BFile;
                                break;
                            case "System.Int":
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                                para.OracleType = OracleType.Int32;
                                break;
                            case "System.Text":
                                para.OracleType = OracleType.LongVarChar;
                                break;
                            default:
                                para.OracleType = OracleType.VarChar;
                                break;

                        }
                        oraCmd.Parameters.Add(para);
                    }
                    oraCmd.Connection = oraConnMain;
                    oraCmd.CommandType = CommandType.Text;
                    oraCmd.CommandText = insertSql.ToString();
                    if (oraCmd.ExecuteNonQuery() > 0)
                        counts++;
                    //oraCmd.ExecuteNonQuery();
                    oraCmd.Parameters.Clear();
                }

                lsbMsg.Items.Add(tableName + " from slave to main success " + counts + " records!");//备数据库往主数据库成功导入

            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnSaveMsg_Click(object sender, EventArgs e)
        {
            if (lsbMsg.Items.Count < 1) return;

            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Text files (*.txt)|*.txt";
            f.AddExtension = true;
            if (f.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(f.FileName);
                for (int i = 0; i < lsbMsg.Items.Count; i++)
                    sw.WriteLine(lsbMsg.Items[i]);
                sw.Close();
            }
        }

        #endregion


        #region tabpage3   查找差异表结构
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cbbDataBase3.Text == "")
            {
                //MessageBox.Show("请先选择某一数据库!");

                return;
            }
            this.Cursor = Cursors.WaitCursor;

            lsvDifferTables.Items.Clear();

            if (DBHelper.databaseType == "Oracle")
            {
                _sql = "select table_name from all_tables where owner='" + cbbDataBase3.SelectedItem.ToString() + "' order by table_name";
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                _sql = "select name from " + cbbDataBase3.SelectedItem.ToString() + ".dbo.sysobjects where type in ('U','V')  order by name";
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                _sql = "select name from " + cbbDataBase3.SelectedItem.ToString() + ".dbo.sysobjects where type in ('U','V') order by name ";
            }
            else
            {
            }

            dtMainTable = new DataTable();
            oraDa = new OracleDataAdapter();
            oraCmd = new OracleCommand(_sql, oraConnMain);
            oraCmd.CommandType = CommandType.Text;
            oraDa.SelectCommand = oraCmd;
            oraDa.Fill(dtMainTable);
            DataColumn[] keys1 = new DataColumn[1];
            keys1[0] = dtMainTable.Columns[0];
            dtMainTable.PrimaryKey = keys1;

            dtSlaveTable = new DataTable();
            oraCmd.Connection=oraConnSlave;
            oraDa.SelectCommand = oraCmd;
            oraDa.Fill(dtSlaveTable);
            DataColumn[] keys2 = new DataColumn[1];
            keys2[0] = dtSlaveTable.Columns[0];
            dtSlaveTable.PrimaryKey = keys2;

            DataTable mainTableStruct = new DataTable();
            DataTable slaveTableStruct = new DataTable();

            ListViewItem lvi;
            DataRow row;
            for (int i = 0; i < dtMainTable.Rows.Count; i++)
            {
                //1. 先查找备数据库服务器是否存在主服务器中相应的表
                row = dtSlaveTable.Rows.Find(dtMainTable.Rows[i][0]);
                if (row == null)
                {
                    lvi = new ListViewItem(dtMainTable.Rows[i][0].ToString());
                    lvi.ImageIndex = 2;
                    lvi.SubItems.Add("This table is not exist in slave database.");
                    lsvDifferTables.Items.Add(lvi);
                    continue;
                }
                else
                {
                    mainTableStruct.Rows.Clear();
                    slaveTableStruct.Rows.Clear();
                    _sql = "select COLUMN_NAME,DATA_TYPE,DATA_LENGTH from ALL_TAB_COLUMNS where OWNER='" + cbbDataBase3.Text + "' and TABLE_NAME='" + dtMainTable.Rows[i][0].ToString() + "' order by column_name";
                    oraCmd.Connection = oraConnMain;
                    oraCmd.CommandText = _sql;
                    oraDa.SelectCommand = oraCmd;
                    oraDa.Fill(mainTableStruct);

                    oraCmd.Connection = oraConnSlave;
                    oraDa.Fill(slaveTableStruct);
                    if (mainTableStruct.Rows.Count != slaveTableStruct.Rows.Count)  //2.列的数目是否相等
                    {
                        lvi = new ListViewItem(dtMainTable.Rows[i][0].ToString());
                        lvi.ImageIndex = 2;
                        lvi.SubItems.Add("Number of fields is not equal.");
                        lsvDifferTables.Items.Add(lvi);
                        continue;
                    }
                    else  //3.列名,列类型,列的长度是否相等
                    {
                        for (int j = 0; j < mainTableStruct.Rows.Count; j++)
                        {
                            if (mainTableStruct.Rows[j][0].ToString() != slaveTableStruct.Rows[j][0].ToString()) 
                            {
                                lvi = new ListViewItem(dtMainTable.Rows[i][0].ToString());
                                lvi.ImageIndex = 2;
                                lvi.SubItems.Add("Name of some column is not equal.");//某列名不相同
                                lsvDifferTables.Items.Add(lvi);
                                break;
                            }
                            if (mainTableStruct.Rows[j][1].ToString() != slaveTableStruct.Rows[j][1].ToString())
                            {
                                lvi = new ListViewItem(dtMainTable.Rows[i][0].ToString());
                                lvi.ImageIndex = 2;
                                lvi.SubItems.Add("Type of some column is different.");   //某列类型不相同
                                lsvDifferTables.Items.Add(lvi);
                                break;
                            }
                            if (mainTableStruct.Rows[j][2].ToString() != slaveTableStruct.Rows[j][2].ToString())
                            {
                                lvi = new ListViewItem(dtMainTable.Rows[i][0].ToString());
                                lvi.ImageIndex = 2;
                                lvi.SubItems.Add("Length of some column is different");   //某列长度不相同
                                lsvDifferTables.Items.Add(lvi);
                                break;
                            }
                        }
                    }
                }
            }

            //4. 再查找备数据库服务器中的表在主数据库服务器是否存在.
            for (int i = 0; i < dtSlaveTable.Rows.Count; i++)
            {
                row = dtMainTable.Rows.Find(dtSlaveTable.Rows[i][0]);
                if (row == null)
                {
                    lvi = new ListViewItem(dtSlaveTable.Rows[i][0].ToString());
                    lvi.ImageIndex = 2;
                    lvi.SubItems.Add("This table is not exist in main database.");//主数据库不存在此表
                    lsvDifferTables.Items.Add(lvi);
                }
            }
            //当没有不同之处时，也要提示一下
            if (lsvDifferTables.Items.Count < 1)
            {
                lvi = new ListViewItem("No difference");
                lvi.ImageIndex = 2;
                lvi.SubItems.Add("Main database and slave database is same structure");//主数据库不存在此表
                lsvDifferTables.Items.Add(lvi);
            }

            this.Cursor = Cursors.Arrow;
        }

 
        private void lsvDifferTables_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lsvDifferTables.SelectedItems.Count < 1) return;

            DataTable mainTableStruct = new DataTable();
            DataTable slaveTableStruct = new DataTable();
            string tableName = lsvDifferTables.SelectedItems[0].Text;
            lblTableName.Text = "Table name:" + tableName;
            _sql = "select COLUMN_NAME,DATA_TYPE,DATA_LENGTH from ALL_TAB_COLUMNS where OWNER='" + cbbDataBase3.Text + "' and TABLE_NAME='" + tableName + "' order by column_name";

            oraCmd.Connection = oraConnMain;
            oraCmd.CommandText = _sql;
            oraDa.SelectCommand = oraCmd;
            oraDa.Fill(mainTableStruct);
            dgvMainStruct.AutoGenerateColumns = true;
            dgvMainStruct.DataSource = mainTableStruct;

            oraCmd.Connection = oraConnSlave;
            oraDa.Fill(slaveTableStruct);
            dgvSlaveStruct.AutoGenerateColumns = true;
            dgvSlaveStruct.DataSource = slaveTableStruct;
            tabControl2.SelectedTab=tabPage5;
            
        }

        #endregion
 

        private void frmDataConsistent_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(oraConnMain!=null)
            {
                oraConnMain.Close();
                oraConnMain.Dispose();
            }
            if (oraConnSlave != null)
            {
                oraConnSlave.Close();
                oraConnSlave.Dispose();
            }
        }

        //控制只能选择一个
        private void ckbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            for(int i=0;i<ckbTables.Items.Count;i++)
            {
                if (ckbTables.Items[i] != ckbTables.SelectedItem)
                    ckbTables.SetItemChecked(i, false);
            }
        }






        //private void btnStart1_Click(object sender, EventArgs e)
        //{
        //    if (dsMainData.Tables[0].Rows.Count == dsSlaveData.Tables[0].Rows.Count)
        //    {
        //        //if (MessageBox.Show("是否要删除该记录?", "注意!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
        //        //lblMessage.Text = "两个数据库上的记录个数一样不需要维护!";
        //        return;
        //    }
        //    else
        //    {
        //        string strPK = (string)DBOpt.dbHelper.ExecuteScalar("select c.column_name from user_indexes i,user_ind_columns c where i.index_name=c.index_name and i.table_name ='" + cbbTable.Text + "' and i.uniqueness='UNIQUE'");
        //        string log = "",strDbName="";
        //        if (dsMainData.Tables[0].Rows.Count > dsSlaveData.Tables[0].Rows.Count)
        //        {
        //            strDbName = "备数据服务器 " + cbbTable.Text + " 中成功插入";
        //            for (int i = 0; i < dsMainData.Tables[0].Rows.Count; i++)
        //            {
        //                string pkWhere = strPK + "='" + dsMainData.Tables[0].Rows[i][strPK] + "'";
        //                if (!IsExistRecord(cbbTable.Text, pkWhere, oraConnSlave))
        //                {
        //                    string sql = DataGridViewToArray(ref dgvMainServer1, i, cbbTable.Text);
        //                    ExecuteSqlInsert(sql, oraConnSlave);
        //                    log += pkWhere + ",";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            strDbName = "主数据服务器 " + cbbTable.Text + " 中成功插入";
        //            for (int i = 0; i < dsSlaveData.Tables[0].Rows.Count; i++)
        //            {
        //                string pkWhere = strPK + "='" + dsSlaveData.Tables[0].Rows[i][strPK] + "'";
        //                if (!IsExistRecord(cbbTable.Text, pkWhere, oraConnMain))
        //                {
        //                    string sql = DataGridViewToArray(ref dgvSlaveServer1, i, cbbTable.Text);
        //                    ExecuteSqlInsert(sql, oraConnMain);
        //                    log += pkWhere + ",";
        //                }
        //            }
        //        }
        //        FileStream fs = new FileStream("insertLog" + DateTime.Today.Year.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
        //        StreamWriter sw = new StreamWriter(fs, Encoding.Default);
        //        sw.BaseStream.Seek(0, SeekOrigin.End);
        //        sw.WriteLine(DateTime.Today.ToLongDateString() + DateTime.Now.ToShortTimeString() + ":" + strDbName + log.Trim(','));
        //        sw.Flush();
        //        fs.Close();
        //    }
        //}

        //private void ExecuteSqlInsert(string sqlstr, OracleConnection dbConn)
        //{
        //    oraCmd = new OracleCommand(sqlstr, dbConn);
        //    oraCmd.Parameters.Clear();
        //    oraCmd.CommandType = CommandType.Text;
        //    oraCmd.CommandText = sqlstr;
        //    oraCmd.ExecuteNonQuery();
        //}

        //private bool IsExistRecord(string tableName, string wheres, OracleConnection dbConn)
        //{
        //    string _sql = "select count(*) from " + tableName + " where " + wheres;
        //    oraCmd = new OracleCommand(_sql, dbConn);
        //    oraCmd.CommandType = CommandType.Text;
        //    object obj = oraCmd.ExecuteScalar();
        //    if (obj == null) return false;
        //    if (Convert.ToInt16(obj) == 0)
        //        return false;
        //    else
        //        return true;
        //}

        //private string DataGridViewToArray(ref DataGridView dgv,int iRow,string tableName)
        //{
        //    string arr = "";
        //    string strValue = "";
        //    string strName = "";
        //    for (int i = 0; i < dgv.Columns.Count; i++)
        //    {
        //        object oTmp = dgv.Rows[iRow].Cells[i].Value;
        //        if (!(oTmp is System.DBNull))
        //        {
        //            strName = strName + dgv.Columns[i].Name + ", ";
        //            switch (dgv.Columns[i].ValueType.FullName)
        //            {
        //                case "System.String":
        //                    strValue += "'" + dgv.Rows[iRow].Cells[i].Value.ToString().Replace("'", "\"") + "',";
        //                    break;
        //                case "System.DateTime":
        //                    if (DBHelper.databaseType == "Oracle")
        //                        strValue += "TO_DATE('" + Convert.ToDateTime(dgv.Rows[iRow].Cells[i].Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS'),";
        //                    else
        //                        strValue += "'" + dgv.Rows[iRow].Cells[i].Value.ToString() + "',";
        //                    break;
        //                default:
        //                    strValue += dgv.Rows[iRow].Cells[i].Value.ToString() + ",";
        //                    break;
        //            }
        //        }
        //    }
        //    if (DBHelper.databaseType != "Oracle")
        //        arr = "insert into " + tableName + " ( " + strName.Trim().TrimEnd(',') + " ) values ( " + strValue.Trim().TrimEnd(',') + " )";
        //    else
        //        arr = "insert into " + tableName + " ( " + strName.Trim().TrimEnd(',') + " ) values ( " + strValue.Trim().TrimEnd(',') + " )";
        //    return arr;
        //}









    }
}
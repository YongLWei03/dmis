using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using System.Collections;

namespace DataBackup
{
    public partial class frmTableModify : Form
    {
        string _sql;
        public frmTableModify()
        {
            InitializeComponent();
        }

        private void frmTableModify_Load(object sender, EventArgs e)
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
            initData();
        }
        private void initData()
        {
            DataGridViewComboBoxColumn column2 = (DataGridViewComboBoxColumn)ddl2_tblColTable.Columns[/*"类型"*/1];
            column2.DataSource = DbDataType.DataTypeNames;
        }
        private void cbbDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsbTable.Items.Clear();
            //label7.Text = cbbDataBase.SelectedItem.ToString();
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

        private ArrayList _backupTableInfo;
        private void lsbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable colinfo = DBOpt.dbHelper.GetTableColumns(lsbTable.SelectedItem.ToString());
                if (colinfo != null)
                {
                    //
                    //ddl2_tblNameText.Text = (String)ddl2_tblList.SelectedItem;
                    ddl2_tblColTable.Rows.Clear();
                    if (_backupTableInfo == null)
                        _backupTableInfo = new ArrayList();
                    else
                        _backupTableInfo.Clear();
                    //
                    foreach (DataRow row in colinfo.Rows)
                    {
                        object[] dp = new object[6];
                        dp[0] = row.ItemArray[0].ToString();  //列名
                        dp[1] = DbDataType.getDbDataType(row.ItemArray[5].ToString());  //类型
                        dp[2] = int.Parse(row.ItemArray[2].ToString());  //长度
                        dp[3] = (row.ItemArray[7].ToString()).Equals("true");  //可为空
                        dp[4] = (row.ItemArray[8].ToString()).Equals("true");  //只读
                        dp[5] = (row.ItemArray[11].ToString()).Equals("true"); //主键
                        //
                        ddl2_tblColTable.Rows.Add(dp);
                        _backupTableInfo.Add(dp);
                    }
                }
            }
            catch (Exception ex)
            {
                //statusLab.Text = ex.Message;
            }
        }
        public class DbDataType
        {
            public const string INT = "int";
            public const string FLOAT = "float";
            public const string CHAR = "char";
            public const string VARCHAR = "varchar";
            public const string DATE = "date";

            public static readonly string[] DataTypeNames = new string[] { INT, FLOAT, CHAR, VARCHAR, DATE };

            public static string getDbDataType(String netDataType)
            {
                netDataType = netDataType.ToLower();
                if (netDataType.IndexOf(INT) >= 0)
                    return INT;
                else if (netDataType.IndexOf(FLOAT) >= 0)
                    return FLOAT;
                else if (netDataType.IndexOf(CHAR) >= 0)
                    return CHAR;
                else if (netDataType.IndexOf(VARCHAR) >= 0)
                    return VARCHAR;
                else if (netDataType.IndexOf(DATE) >= 0)
                    return DATE;
                else
                    return VARCHAR;
            }
        }

    }
}
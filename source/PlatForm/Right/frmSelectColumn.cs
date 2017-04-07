using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.Common;
using PlatForm.DBUtility;
using PlatForm.Functions;

namespace PlatForm
{
    public partial class frmSelectColumn : Form
    {
        string sql;
        public int tableID;
        public string db, tableName;

        public frmSelectColumn()
        {
            InitializeComponent();
        }

        private void frmSelectColumn_Load(object sender, EventArgs e)
        {
            db=txtTableName.Text.Substring(0,txtTableName.Text.IndexOf('.'));
            tableName=txtTableName.Text.Substring(txtTableName.Text.LastIndexOf('.')+1);
            if (DBHelper.databaseType == "Oracle")
            {
                sql = "select column_name from ALL_TAB_COLUMNS where table_name='" + tableName + "' and owner='" + db + "' and column_name not in( select name from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID + ")";
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                //已经添加的列不允许再添加
                sql = "select b.name from " + db + ".dbo.sysobjects a," + db + ".dbo.syscolumns b where a.id=b.id and a.name='" + tableName + "' and b.name not in( select name from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID+")";
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                sql = "select b.name from " + db + ".dbo.sysobjects a," + db + ".dbo.syscolumns b where a.id=b.id and a.name='" + tableName + "' and b.name not in( select NAME from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID + ")";
            }
            else
            {

            }
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            while (dr.Read())
            {
                cbbColumn.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbbColumn.SelectedItem == null)
            {
                MessageBox.Show(Main.Properties.Resources.SelectItem);//"请先选择某列！"
                return;
            }
            uint maxID = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_COLUMNS", "ID");
            int count = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString()));

            if (DBHelper.databaseType == "Oracle")
            {
                sql = "select data_type from ALL_TAB_COLUMNS where table_name='" + tableName + "' and owner='" + db + "' and column_name='" + cbbColumn.Text + "'";
                string dataType=DBOpt.dbHelper.ExecuteScalar(sql).ToString();
                switch (dataType)
                {
                    //字符串
                    case "CHAR":
                    case "VARCHAR2":
                    case "NVARCHAR2":
                    case "CLOB":
                    case "NCLOB":
                    case "NCHAR":
                    case "LONG":
                        sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,ORDER_ID,OTHER_LANGUAGE_DESCR) values(" 
                            + tableID + "," + maxID.ToString() + ",'" + cbbColumn.Text + "','" + cbbColumn.Text + "','String','txt" + cbbColumn.Text.ToUpper() + "','TextBox'," + Convert.ToString(count * 10) + ",'"+cbbColumn.Text+"')";
                        break;
                    //数值
                    case "NUMBER":
                    case "FLOAT":
                        sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,ORDER_ID,OTHER_LANGUAGE_DESCR) values("
                            + tableID + "," + maxID.ToString() + ",'" + cbbColumn.Text + "','" + cbbColumn.Text + "','Numeric','txt" + cbbColumn.Text.ToUpper() + "','TextBox'," + Convert.ToString(count * 10) + ",'" + cbbColumn.Text + "')";
                        break;
                    //日期
                    case "DATE":
                        sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,ORDER_ID,OTHER_LANGUAGE_DESCR) values("
                            + tableID + "," + maxID.ToString() + ",'" + cbbColumn.Text + "','" + cbbColumn.Text + "','Datetime','wdl" + cbbColumn.Text.ToUpper() + "','WebDateLib'," + Convert.ToString(count * 10) + ",'" + cbbColumn.Text + "')";
                        break;
                    default:
                        break;
                }
                DBOpt.dbHelper.ExecuteSql(sql);
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                sql = "select b.name,b.xtype from " + db + ".dbo.sysobjects a," + db + ".dbo.syscolumns b where a.id=b.id and b.name='" + cbbColumn.Text + "'";
                DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
                dr.Read();
                int type;
                type = Convert.ToInt32(dr[1]);
                switch (type)
                {
                    //字符串
                    case 35:
                    case 99:
                    case 167:
                    case 175:
                    case 231:
                    case 239:
                        sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,ORDER_ID) values(" + tableID + "," + maxID.ToString() + ",'" + dr[0].ToString() + "','" + dr[0].ToString() + "','String','txt" + dr[0].ToString().ToUpper() + "',"+Convert.ToString(count*10)+")";
                        break;
                    //数值
                    case 48:
                    case 52:
                    case 56:
                    case 59:
                    case 62:
                    case 106:
                    case 108:
                    case 172:
                        sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,ORDER_ID) values(" + tableID + "," + maxID.ToString() + ",'" + dr[0].ToString() + "','" + dr[0].ToString() + "','Numeric','txt" + dr[0].ToString().ToUpper() + "'," + Convert.ToString(count * 10) + ")";
                        break;
                    //日期
                    case 58:
                    case 61:
                        sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,ORDER_ID) values(" + tableID + "," + maxID.ToString() + ",'" + dr[0].ToString() + "','" + dr[0].ToString() + "','Datetime','txt" + dr[0].ToString().ToUpper() + "'," + Convert.ToString(count * 10) + ")";
                        break;
                    default:
                        break;
                }
                dr.Close();
                DBOpt.dbHelper.ExecuteSql(sql);
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                sql = "select b.name,b.type from " + db + ".dbo.sysobjects a," + db + ".dbo.syscolumns b where a.id=b.id and b.name='" + cbbColumn.Text + "'";
                DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
                dr.Read();
                int type;
                type = Convert.ToInt32(dr[1]);
                switch (type)
                {
                    //字符串
                    case 47:  //char mchar
                    case 39:  //varchar nchar 
                    case 35:
                        sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,ORDER_ID) values(" + tableID + "," + maxID.ToString() + ",'" + dr[0].ToString() + "','" + dr[0].ToString() + "','字符','txt" + dr[0].ToString().ToUpper() + "'," + Convert.ToString(count*10) + ")";
                        break;
                    //数值
                    case 50:  //bit
                    case 55:  //decimal
                    case 106:
                    case 62: //float
                    case 109: //floatn
                    case 56: //int
                    case 38:
                    case 60:
                    case 110:
                    case 63:
                    case 108:
                    case 59:
                    case 52:
                    case 122:
                    case 48:
                        sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,ORDER_ID) values(" + tableID + "," + maxID.ToString() + ",'" + dr[0].ToString() + "','" + dr[0].ToString() + "','数值','txt" + dr[0].ToString().ToUpper() + "'," + Convert.ToString(count*10) + ")";
                        break;
                    //日期
                    case 58:
                    case 61:
                    case 111:
                    case 37:
                        sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,ORDER_ID) values(" + tableID + "," + maxID.ToString() + ",'" + dr[0].ToString() + "','" + dr[0].ToString() + "','时间','txt" + dr[0].ToString().ToUpper() + "'," + Convert.ToString(count*10) + ")";
                        break;
                    default:
                        break;
                }
                dr.Close();
                DBOpt.dbHelper.ExecuteSql(sql);
            }
            else
            {
            }

            this.Close();
            this.Dispose();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
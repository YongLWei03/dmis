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
using System.Collections;

namespace DataBackup
{
    public partial class frmDataModify : Form
    {
        string _sql;
        public frmDataModify()
        {
            InitializeComponent();
        }

        private void frmDataModify_Load(object sender, EventArgs e)
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

        private void btnSelectCol_Click(object sender, EventArgs e)
        {
            setTableHead();
            dgvData.Rows.Clear();
        }
        private void setTableHead()
        {
            labIDName.Text = "";
            IList list = null;
            if (rdbAll.Checked)
                list = lsbColumn.Items;
            else
                list = lsbColumn.SelectedItems;
            if (list == null || list.Count == 0)
                return;
            String tblName = (String)lsbTable.SelectedItem;
            DataGridViewColumn[] cols = new DataGridViewColumn[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                cols[i] = new DataGridViewTextBoxColumn();
                cols[i].Name = (String)list[i];
                if (cols[i].Name == "TID" || cols[i].Name == "ID" || cols[i].Name == "F_NO")
                    labIDName.Text = cols[i].Name;
            }
            dgvData.Columns.Clear();
            dgvData.Columns.AddRange(cols);

        }

        private void lsbTable_SelectedIndexChanged(object sender, EventArgs e)
        {//处理lsbColumn添加Items
            ckbWhere.Checked = false;
            labWhere.Text = "";
           
            try
            {
                String tblName = (String)lsbTable.SelectedItem;
                DataTable dt=new DataTable();
                switch (DBHelper.databaseType)
                {
                    case "Oracle":
                        dt = DBOpt.dbHelper.GetDataTable("select column_name colName,data_type colType from ALL_TAB_COLUMNS where table_name='" + tblName + "' and OWNER='" + cbbDataBase.SelectedItem.ToString() + "'");
                        break;
                    case "SqlServer":
                        dt = DBOpt.dbHelper.GetDataTable("select b.name colName,b.xtype colType from " + cbbDataBase.SelectedItem.ToString() + ".dbo.sysobjects a," + cbbDataBase.SelectedItem.ToString() + ".dbo.syscolumns b where a.id=b.id and a.name='" + tblName + "'");
                        break;
                    case "Sybase":
                        //"select b.type from " + "webdmis.dbo.sysobjects a,webdmis.dbo.syscolumns b where a.id=b.id and a.name='" + Session["TableName"].ToString() + "' and b.name='" + ddlColmns.SelectedValue + "'";
                        dt = DBOpt.dbHelper.GetDataTable("select b.name colName,b.type colType from " + cbbDataBase.SelectedItem.ToString() + ".dbo.sysobjects a," + cbbDataBase.SelectedItem.ToString() + ".dbo.syscolumns b where a.id=b.id and a.name='" + tblName + "'");
                        break;
                }
                if (dt != null)
                {
                    lsbColumn.Items.Clear();
                    cbbColumn.Items.Clear();
                    cbbColumn.Text = ""; cbbAndOr.Text = ""; cbbGuanXi.Text = ""; txtValue.Text = "";
                    setVisible(false);
                    string colName = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        colName = getColumnType(row[1].ToString()) + "-" + row[0];
                        cbbColumn.Items.Add(colName);
                        lsbColumn.Items.Add(row[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                labText.Text = ex.Message;
            }
        }

        private void butQuery_Click(object sender, EventArgs e)
        {
            labWhere.Text = "";
            //处理查询条件
            string strWhere = "";
            if (ckbWhere.Checked == true)
            {
                strWhere = getTableWhere();
                if (strWhere == "0")
                {
                    labText.Text = "查询条件不对!";
                    return;
                }
            }
            //初始处理
            dgvData.Rows.Clear();
            if (dgvData.Columns == null || dgvData.Columns.Count == 0)
                return;
            //组织select的表名、列名、条件
            String tblname = (String)lsbTable.SelectedItem;
            if (DBHelper.databaseType == "Oracle")
                tblname = cbbDataBase.SelectedItem.ToString() + "." + tblname;
            else
                tblname = cbbDataBase.SelectedItem.ToString() + ".dbo." + tblname;
            String cols = "";
            for (int i = 0; i < dgvData.Columns.Count; i++)
            {
                if (i != 0)
                    cols += ",";
                cols += ((DataGridViewColumn)dgvData.Columns[i]).Name;
            }
            //读取数据，填写表格及状态条
            string sql = "select " + cols + " from " + tblname + (strWhere.Length > 0 ? " where " + strWhere : "");
            try
            {
                DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dgvData.Columns[i].ToolTipText = dt.Columns[i].DataType.ToString();
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvData.Rows.Add(dt.Rows[i]);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dgvData.Rows[i].Cells[j].Value = dt.Rows[i][j];
                    }
                }
                labText.Text = "执行[" + sql + "]成功，\n返回" + dt.Rows.Count + "条记录";
                labSelect.Text = sql;
            }
            catch (Exception ex)
            {
                labText.Text = "执行[" + sql + "]失败！\nerrormsg=" + ex.Message;
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedCells.Count == 0)
            {
                labText.Text = "请选择要修改的记录！";
                return;
            }
            if (labIDName.Text == "")
            {
                labText.Text = "你没有选择主键，不能修改！请先选择TID、ID或F_NO，如果没有TID、ID或F_NO此表不能修改！";
                return;
            }
            int selectRow = dgvData.SelectedCells[0].RowIndex;
            string strID = dgvData.Rows[selectRow].Cells[labIDName.Text].Value.ToString();
            String tblname = (String)lsbTable.SelectedItem;
            if (DBHelper.databaseType == "Oracle")
                tblname = cbbDataBase.SelectedItem.ToString() + "." + tblname;
            else
                tblname = cbbDataBase.SelectedItem.ToString() + ".dbo." + tblname;
            String cols = "", values = "", sql = "";
            if (!DBOpt.dbHelper.IsExist(tblname, labIDName.Text + "=" + strID))//不存在该记录则插入
            {
                for (int i = 0; i < dgvData.Columns.Count; i++)
                {
                    string colDataType = ((DataGridViewColumn)dgvData.Columns[i]).ToolTipText;
                    string colName = ((DataGridViewColumn)dgvData.Columns[i]).Name;
                    object colValue = dgvData.Rows[selectRow].Cells[colName].Value;
                    if (colDataType.ToLower().IndexOf("int") >= 0 || colDataType.ToLower().IndexOf("float") >= 0 || colDataType.ToLower().IndexOf("dec") >= 0)
                    {
                        if (colValue != null)
                        {
                            if (colValue.ToString() != "")
                            {
                                cols = cols + "," + colName;
                                values = values + "," + colValue;
                            }
                        }
                    }
                    else
                    {
                        if (colValue != null)
                        {
                            if (colValue.ToString() != "")
                            {
                                cols = cols + "," + colName;
                                values = values + ",'" + colValue + "'";
                            }
                        }
                    }
                    sql = "insert into " + tblname + "(" + cols.TrimStart(',') + ") values(" + values.TrimStart(',') + ")";
                }
            }
            else
            {
                for (int i = 0; i < dgvData.Columns.Count; i++)
                {
                    string colDataType = ((DataGridViewColumn)dgvData.Columns[i]).ToolTipText;
                    string colName = ((DataGridViewColumn)dgvData.Columns[i]).Name;
                    object colValue = dgvData.Rows[selectRow].Cells[colName].Value;
                    if (colDataType.ToLower().IndexOf("int") >= 0 || colDataType.ToLower().IndexOf("float") >= 0 || colDataType.ToLower().IndexOf("dec") >= 0)
                    {
                        if (colValue != null)
                        {
                            if (colValue.ToString() != "")
                                cols = cols + "," + colName + "=" + colValue;
                        }
                    }
                    else
                    {
                        if (colValue != null)
                        {
                            if (colValue.ToString() != "")
                                cols = cols + "," + colName + "='" + colValue + "'";
                        }
                    }
                }
                sql = "update " + tblname + " set " + cols.TrimStart(',') + " where " + labIDName.Text + "=" + strID;
            }
            if (DBOpt.dbHelper.ExecuteSql(sql) == -1)
                labText.Text = "执行[" + sql + "]失败!";
            else
                labText.Text = "执行[" + sql + "]成功!\n";
                
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedCells.Count == 0)
            {
                labText.Text = "请选择要删除的记录！";
                return;
            }
            if (labIDName.Text == "")
            {
                labText.Text = "你没有选择主键，不能删除！请先选择TID、ID或F_NO，如果没有TID、ID或F_NO此表不能删除！";
                return;
            }
            if (MessageBox.Show("是否要删除该记录?", "注意!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            int selectRow = dgvData.SelectedCells[0].RowIndex;
            string strID = dgvData.Rows[selectRow].Cells[labIDName.Text].Value.ToString();
            String tblname = (String)lsbTable.SelectedItem;
            if (DBHelper.databaseType == "Oracle")
                tblname = cbbDataBase.SelectedItem.ToString() + "." + tblname;
            else
                tblname = cbbDataBase.SelectedItem.ToString() + ".dbo." + tblname;
            string sql = "delete from " + tblname + " where " + labIDName.Text + "=" + strID;
            try
            {
                DBOpt.dbHelper.ExecuteSql(sql);
                labText.Text = "执行[" + sql + "]成功!\n";
            }
            catch (Exception ex)
            {
                labText.Text = "执行[" + sql + "]失败!\nerrormsg=" + ex.Message;
            }
            if (labSelect.Text != "")
            {
                dgvData.Rows.Clear();
                DataTable dt = DBOpt.dbHelper.GetDataTable(labSelect.Text);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dgvData.Columns[i].ToolTipText = dt.Columns[i].DataType.ToString();
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvData.Rows.Add(dt.Rows[i]);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dgvData.Rows[i].Cells[j].Value = dt.Rows[i][j];
                    }
                }
            }
        }


        /// <summary>
        /// 当列的类型是数值和时间时,填充关系符
        /// </summary>
        /// <param name="ddl"></param>
        private void initGuanXiByIntAndTime(ref ComboBox ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add("=");
            ddl.Items.Add("<");
            ddl.Items.Add(">");
            ddl.Items.Add(">=");
            ddl.Items.Add("<=");
            ddl.Items.Add("<>");
        }

        /// <summary>
        /// 当列的类型是字符时,填充关系符
        /// </summary>
        /// <param name="ddl"></param>
        private void initGuanXiByString(ref ComboBox ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add("=");
            ddl.Items.Add("like");
            ddl.Items.Add("<");
            ddl.Items.Add(">");
            ddl.Items.Add(">=");
            ddl.Items.Add("<=");
            ddl.Items.Add("<>");
        }

        private void setVisible( bool visible)
        {
            for (int i = 1; i < 4; i++)
            {
                cbbColumn1.Visible = visible;
                cbbColumn2.Visible = visible;
                cbbColumn3.Visible = visible;
                cbbAndOr1.Visible = visible;
                cbbAndOr2.Visible = visible;
                cbbAndOr3.Visible = visible;
                cbbGuanX1.Visible = visible;
                cbbGuanX2.Visible = visible;
                cbbGuanX3.Visible = visible;
                txtValue1.Visible = visible;
                txtValue2.Visible = visible;
                txtValue3.Visible = visible;
            }
        }
        private string getColumnType(string columnType)
        {
            string strType = "";
            if (DBHelper.databaseType == "Oracle")
            {
                if (columnType.IndexOf("HAR") > 0)  //字符列
                {
                    strType = "字符";
                }
                else if (columnType.IndexOf("UMB") > 0)  //数值列
                {
                    strType = "数值";
                }
                else if (columnType.IndexOf("DATE") > 0)  //日期时间列
                {
                    strType = "时间";
                }
            }
            else if (DBHelper.databaseType == "SqlServer")
            {

                int type = Convert.ToInt32(columnType);
                switch (type)
                {
                    //字符串
                    case 35:
                    case 99:
                    case 167:
                    case 175:
                    case 231:
                    case 239:
                        strType = "字符";
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
                        strType = "数值";
                        break;
                    //日期
                    case 58:
                    case 61:
                        strType = "时间";
                        break;
                    default:
                        break;
                }
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                int type = Convert.ToInt32(columnType);
                switch (type)
                {
                    //字符串
                    case 47:  //char mchar
                    case 39:  //varchar nchar 
                    case 35:
                        strType = "字符";
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
                        strType = "数值";
                        break;
                    //日期
                    case 58:
                    case 61:
                    case 111:
                    case 37:
                        strType = "时间";
                        break;
                    default:
                        break;
                }
            }
            return strType;
        }

        private void cbbColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbColumn.Text.Substring(0, 2) == "字符")
                initGuanXiByString(ref cbbGuanXi);
            else
                initGuanXiByIntAndTime(ref cbbGuanXi);
        }

        private void cbbColumn1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbColumn.Text.Substring(0, 2) == "字符")
                initGuanXiByString(ref cbbGuanX1);
            else
                initGuanXiByIntAndTime(ref cbbGuanX1);
        }

        private void cbbColumn2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbColumn.Text.Substring(0, 2) == "字符")
                initGuanXiByString(ref cbbGuanX2);
            else
                initGuanXiByIntAndTime(ref cbbGuanX2);
        }

        private void cbbColumn3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbColumn.Text.Substring(0, 2) == "字符")
                initGuanXiByString(ref cbbGuanX3);
            else
                initGuanXiByIntAndTime(ref cbbGuanX3);
        }

        private void cbbAndOr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbAndOr.Text.Trim() == "")
            {
                cbbAndOr1.Visible = false;
                cbbGuanX1.Visible = false;
                cbbColumn1.Visible = false;
                txtValue1.Visible = false;
                cbbColumn1.Items.Clear();
            }
            else
            {
                cbbAndOr1.Visible = true;
                cbbGuanX1.Visible = true;
                cbbColumn1.Visible = true;
                txtValue1.Visible = true;
                initCulumn(ref cbbColumn1);
                cbbColumn1.Text = ""; cbbAndOr1.Text = ""; cbbGuanX1.Text = ""; txtValue1.Text = "";
            }
        }

        private void cbbAndOr1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbAndOr1.Text.Trim() == "")
            {
                cbbAndOr2.Visible = false;
                cbbGuanX2.Visible = false;
                cbbColumn2.Visible = false;
                txtValue2.Visible = false;
                cbbColumn2.Items.Clear();
            }
            else
            {
                cbbAndOr2.Visible = true;
                cbbGuanX2.Visible = true;
                cbbColumn2.Visible = true;
                txtValue2.Visible = true;
                initCulumn(ref cbbColumn2);
                cbbColumn2.Text = ""; cbbAndOr2.Text = ""; cbbGuanX2.Text = ""; txtValue2.Text = "";
            }
        }

        private void cbbAndOr2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbAndOr2.Text.Trim() == "")
            {
                cbbAndOr3.Visible = false;
                cbbGuanX3.Visible = false;
                cbbColumn3.Visible = false;
                txtValue3.Visible = false;
                cbbColumn3.Items.Clear();
            }
            else
            {
                cbbAndOr3.Visible = true;
                cbbGuanX3.Visible = true;
                cbbColumn3.Visible = true;
                txtValue3.Visible = true;
                initCulumn(ref cbbColumn3);
                cbbColumn3.Text = ""; cbbAndOr3.Text = ""; cbbGuanX3.Text = ""; txtValue3.Text = "";
            }
        }
        private void initCulumn(ref ComboBox ddl)
        {
            String tblName = (String)lsbTable.SelectedItem;
            DataTable dt = new DataTable();
            switch (DBHelper.databaseType)
            {
                case "Oracle":
                    dt = DBOpt.dbHelper.GetDataTable("select column_name colName,data_type colType from ALL_TAB_COLUMNS where table_name='" + tblName + "' and OWNER='" + cbbDataBase.SelectedItem.ToString() + "'");
                    break;
                case "SqlServer":
                    dt = DBOpt.dbHelper.GetDataTable("select b.name colName,b.xtype colType from " + cbbDataBase.SelectedItem.ToString() + ".dbo.sysobjects a," + cbbDataBase.SelectedItem.ToString() + ".dbo.syscolumns b where a.id=b.id and a.name='" + tblName + "'");
                    break;
                case "Sybase":
                    //"select b.type from " + "webdmis.dbo.sysobjects a,webdmis.dbo.syscolumns b where a.id=b.id and a.name='" + Session["TableName"].ToString() + "' and b.name='" + ddlColmns.SelectedValue + "'";
                    dt = DBOpt.dbHelper.GetDataTable("select b.name colName,b.type colType from " + cbbDataBase.SelectedItem.ToString() + ".dbo.sysobjects a," + cbbDataBase.SelectedItem.ToString() + ".dbo.syscolumns b where a.id=b.id and a.name='" + tblName + "'");
                    break;
            }
            if (dt != null)
            {
                ddl.Items.Clear();
                ddl.Text = "";
                string colName = "";
                foreach (DataRow row in dt.Rows)
                {
                    colName = getColumnType(row[1].ToString()) + "-" + row[0];
                    ddl.Items.Add(colName);
                }
            }
        }
        private string getTableWhere()
        {
            string strWhere = "";
            if (txtValue.Text.Trim() == "")
            {
                labWhere.Text = "请先定义查找的值!";
                return "0";
            }
            else
            {
                if (cbbColumn.Text.Trim() == "" || cbbGuanXi.Text.Trim() == "")
                {
                    labWhere.Text = "列名、条件、关系都不能为空!";
                    return "0";
                }
                if (cbbColumn.Text.Substring(0, 2) == "数值")
                    strWhere = strWhere + cbbColumn.Text.Substring(3) + cbbGuanXi.Text + txtValue.Text + " " + cbbAndOr.Text + " ";
                else
                    strWhere = strWhere + cbbColumn.Text.Substring(3) + cbbGuanXi.Text + "'" + txtValue.Text + "' " + cbbAndOr.Text + " ";
            }
            if (txtValue1.Visible == true)
            {
                if (txtValue1.Text.Trim() == "")
                {
                    labWhere.Text = "请先定义查找的值!";
                    return "0";
                }
                else
                {
                    if (cbbColumn1.Text.Trim() == "" || cbbGuanX1.Text.Trim() == "")
                    {
                        labWhere.Text = "列名、条件、关系都不能为空!";
                        return "0";
                    }
                    if (cbbColumn1.Text.Substring(0, 2) == "数值")
                        strWhere = strWhere + cbbColumn1.Text.Substring(3) + cbbGuanX1.Text + txtValue1.Text + " " + cbbAndOr1.Text + " ";
                    else
                        strWhere = strWhere + cbbColumn1.Text.Substring(3) + cbbGuanX1.Text + "'" + txtValue1.Text + "' " + cbbAndOr1.Text + " ";
                }
            }
            if (txtValue2.Visible == true)
            {
                if (cbbAndOr.Text.Trim() == "")
                {
                    labWhere.Text = "请先定义查询条件的关系!";
                    return "0";
                }
                if (txtValue2.Text.Trim() == "")
                {
                    labWhere.Text = "请先定义查找的值!";
                    return "0";
                }
                else
                {
                    if (cbbColumn2.Text.Trim() == "" || cbbGuanX2.Text.Trim() == "")
                    {
                        labWhere.Text = "列名、条件、关系都不能为空!";
                        return "0";
                    }
                    if (cbbColumn2.Text.Substring(0, 2) == "数值")
                        strWhere = strWhere + cbbColumn2.Text.Substring(3) + cbbGuanX2.Text + txtValue2.Text + " " + cbbAndOr2.Text + " ";
                    else
                        strWhere = strWhere + cbbColumn2.Text.Substring(3) + cbbGuanX2.Text + "'" + txtValue2.Text + "' " + cbbAndOr2.Text + " ";
                }
            }
            if (txtValue3.Visible == true)
            {
                if (cbbAndOr.Text.Trim() == "" || cbbAndOr1.Text.Trim() == "")
                {
                    labWhere.Text = "请先定义查询条件的关系!";
                    return "0";
                }
                if (txtValue3.Text.Trim() == "")
                {
                    labWhere.Text = "请先定义查找的值!";
                    return "0";
                }
                else
                {
                    if (cbbColumn3.Text.Trim() == "" || cbbGuanX3.Text.Trim() == "")
                    {
                        labWhere.Text = "列名、条件、关系都不能为空!";
                        return "0";
                    }
                    if (cbbColumn3.Text.Substring(0, 2) == "数值")
                        strWhere = strWhere + cbbColumn3.Text.Substring(3) + cbbGuanX3.Text + txtValue3.Text + " " + cbbAndOr3.Text + " ";
                    else
                        strWhere = strWhere + cbbColumn3.Text.Substring(3) + cbbGuanX3.Text + "'" + txtValue3.Text + "' " + cbbAndOr3.Text + " ";
                }
            }
            labWhere.Text = strWhere;
            return strWhere;
        }

    }
}
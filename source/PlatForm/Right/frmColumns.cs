using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using System.Data.Common;
using PlatForm.Functions;
using System.IO;

using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Globalization;

namespace PlatForm
{
    public partial class frmColumns : Form
    {
        string _sql;
        string _tableId = "";
        string yes, no;   //存储不同语言的yes和no
        public frmColumns()
        {
            InitializeComponent();
        }

        private void frmColumns_Load(object sender, EventArgs e)
        {
            initTree();
            yes = Main.Properties.Resources.Yes;   //是
            no = Main.Properties.Resources.No;     //否

            cbbCONTROL_LIST_DISPLAY_ALIGN.Items.Add(new ComboxItem(Main.Properties.Resources.AlignLeft, "0"));//"靠左"
            cbbCONTROL_LIST_DISPLAY_ALIGN.Items.Add(new ComboxItem(Main.Properties.Resources.AlignCenter, "1"));//"居中"
            cbbCONTROL_LIST_DISPLAY_ALIGN.Items.Add(new ComboxItem(Main.Properties.Resources.AlignRight, "2"));//"靠右"
            cbbCONTROL_LIST_DISPLAY_ALIGN.Items.Add(new ComboxItem(Main.Properties.Resources.AlignJustified, "3"));//"左右对齐"
            cbbCONTROL_LIST_DISPLAY_ALIGN.SelectedIndex = 0;

        }

        private void initTree()
        {
            trvTables.Nodes.Clear();

            DataTable dt;
            DataTable dtChild;

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                dt = DBOpt.dbHelper.GetDataTable("select ID,DESCR from DMIS_SYS_TABLE_TYPE order by ORDER_ID");
            else
                dt = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_TABLE_TYPE order by ORDER_ID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode td = new TreeNode();
                td.Text = dt.Rows[i][1].ToString();
                td.Tag = dt.Rows[i][0];
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                    dtChild = DBOpt.dbHelper.GetDataTable("select ID,DESCR from DMIS_SYS_TABLES where TYPE_ID=" + dt.Rows[i][0].ToString() + " order by ORDER_ID");
                else
                    dtChild = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_TABLES where TYPE_ID=" + dt.Rows[i][0].ToString() + " order by ORDER_ID");

                for (int j = 0; j < dtChild.Rows.Count; j++)
                {
                    TreeNode tdChild = new TreeNode();
                    tdChild.Text = dtChild.Rows[j][1].ToString();
                    tdChild.Tag = dtChild.Rows[j][0];
                    tdChild.ToolTipText = dtChild.Rows[j][0].ToString();
                    tdChild.ImageIndex = 1;
                    tdChild.SelectedImageIndex = 2;
                    td.Nodes.Add(tdChild);
                }
                trvTables.Nodes.Add(td);
            }
        }

        private void trvTables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvTables.SelectedNode.Level != 1) return;
            txtTABLE_ID.Text = trvTables.SelectedNode.Tag.ToString();
            InitColumns();
        }

        private void lvColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            //基本信息
            //lvColumns.Items[0].
            if (lvColumns.SelectedItems.Count != 1) return;
            txtID.Text = lvColumns.SelectedItems[0].Text;
            txtNAME.Text = lvColumns.SelectedItems[0].SubItems[1].Text;
            txtDESCR.Text = lvColumns.SelectedItems[0].SubItems[2].Text;
            txtOTHER_LANGUAGE_DESCR.Text = lvColumns.SelectedItems[0].SubItems[3].Text;
            txtTYPE.Text = lvColumns.SelectedItems[0].SubItems[4].Text;

            if (lvColumns.SelectedItems[0].SubItems[5].Text == yes)
                ckbISPRIMARY.Checked = true;
            else
                ckbISPRIMARY.Checked = false;

            if (lvColumns.SelectedItems[0].SubItems[6].Text == yes)
                ckbISNULL.Checked = true;
            else
                ckbISNULL.Checked = false;

            txtCUSTOM_CONTROL_NAME.Text = lvColumns.SelectedItems[0].SubItems[7].Text;
            cbbCUSTOM_CONTROL_TYPE.Text = lvColumns.SelectedItems[0].SubItems[8].Text;
            cbbCUSTOM_CONTROL_SVAE_TYPE.Text = lvColumns.SelectedItems[0].SubItems[9].Text;

            txtLENGTH.Text = lvColumns.SelectedItems[0].SubItems[10].Text;
            if (lvColumns.SelectedItems[0].SubItems[11].Text == yes)
                ckbISDISPLAY.Checked = true;
            else
                ckbISDISPLAY.Checked = false;
            txtORDER_ID.Text = lvColumns.SelectedItems[0].SubItems[12].Text;

            //列表控件设置
            txtCONTROL_LIST_WIDTH.Text = lvColumns.SelectedItems[0].SubItems[13].Text;
            cbbCONTROL_LIST_DISPLAY_FORMAT.Text = lvColumns.SelectedItems[0].SubItems[14].Text;
            //简单的处理方式，Value和SelectedIndex取值一致。
            if (lvColumns.SelectedItems[0].SubItems[15].Text.Trim()!="")
                cbbCONTROL_LIST_DISPLAY_ALIGN.SelectedIndex = Convert.ToInt16(lvColumns.SelectedItems[0].SubItems[15].Text);
            
            //细节控件设置
            txtCONTROL_HEIGHT.Text = lvColumns.SelectedItems[0].SubItems[16].Text;
            txtFILL_EXPRESSION.Text = lvColumns.SelectedItems[0].SubItems[17].Text;
            cbbRELATING_COLUMN.Text = lvColumns.SelectedItems[0].SubItems[18].Text;
            cbbRELATING_COLUMN.Items.Clear();
            for (int i = 0; i < lvColumns.Items.Count; i++)
            {
                if (i == lvColumns.SelectedIndices[0]) continue; //本列不添加
                cbbRELATING_COLUMN.Items.Add(lvColumns.Items[i].SubItems[1].Text);
            }
            if (lvColumns.SelectedItems[0].SubItems[19].Text == yes)
                ckbCONTROL_DISPLAY_ONE_ROW.Checked = true;
            else
                ckbCONTROL_DISPLAY_ONE_ROW.Checked = false;
            txtRELATING_CONDITION.Text = lvColumns.SelectedItems[0].SubItems[20].Text;
        }

        private void InitColumns()
        {
            if (trvTables.SelectedNode == null) return;
            ListViewItem lv;
            _tableId = trvTables.SelectedNode.Tag.ToString();
            lvColumns.Items.Clear();
            //列的顺序要和左边的LISTVIEW控件的列的顺序一样，建表的脚本的列的顺序最后也一样，好查错。
            _sql = "select ID,NAME,DESCR,OTHER_LANGUAGE_DESCR,TYPE,ISPRIMARY,ISNULL,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE,LENGTH,ISDISPLAY,ORDER_ID," +
                   "CONTROL_LIST_WIDTH,CONTROL_LIST_DISPLAY_FORMAT,CONTROL_LIST_DISPLAY_ALIGN," +
                   "CONTROL_HEIGHT,FILL_EXPRESSION,RELATING_COLUMN,CONTROL_DISPLAY_ONE_ROW,RELATING_CONDITION  " +
                   " from DMIS_SYS_COLUMNS where TABLE_ID=" + trvTables.SelectedNode.Tag.ToString() + " order by ORDER_ID";

            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(dt.Rows[i][0].ToString());
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] is System.DBNull)
                        lv.SubItems.Add("");
                    else
                    {

                        if (dt.Columns[j].ColumnName == "ISPRIMARY" || dt.Columns[j].ColumnName == "ISNULL" || dt.Columns[j].ColumnName == "ISDISPLAY" || dt.Columns[j].ColumnName == "CONTROL_DISPLAY_ONE_ROW")
                        {
                            if (dt.Rows[i][j].ToString() == "1")
                                lv.SubItems.Add(yes);
                            else
                                lv.SubItems.Add(no);
                        }
                        else
                            lv.SubItems.Add(dt.Rows[i][j].ToString());
                    }
                }
                lvColumns.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lvColumns, Color.SkyBlue, Color.Lime);
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || _tableId == "" || txtDESCR.Text.Trim() == "" || txtNAME.Text.Trim() == "")
            {
                //MessageBox.Show("列描述不允许为空!");
                return;
            }
            int temp;
            string[] isInt = new string[4] { txtORDER_ID.Text, txtLENGTH.Text, txtCONTROL_LIST_WIDTH.Text, txtCONTROL_HEIGHT.Text };
            for (int i = 0; i < isInt.Length; i++)
            {
                if (isInt[i].Trim() == "") continue;
                if (!int.TryParse(isInt[i], out temp))
                {
                    //MessageBox.Show("必须输入整数!");
                    return;
                }
                else
                {
                    if (temp > 32767 || temp < 0)
                    {
                        //MessageBox.Show(this, "数值必须在0~32767之间！");
                        return;
                    }
                }
            }

            string align;
            if (cbbCONTROL_LIST_DISPLAY_ALIGN.SelectedItem == null)
                align = "";
            else
            {
                ComboxItem item = (ComboxItem)cbbCONTROL_LIST_DISPLAY_ALIGN.SelectedItem;
                align = item.Value;
            }

            FieldPara[] field = {new FieldPara("TABLE_ID",FieldType.Int,txtTABLE_ID.Text),
                                 new FieldPara("ID",FieldType.Int,txtID.Text),
								 new FieldPara("NAME",FieldType.String,txtNAME.Text),
								 new FieldPara("DESCR",FieldType.String,txtDESCR.Text),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtOTHER_LANGUAGE_DESCR.Text),
								 new FieldPara("TYPE",FieldType.String,txtTYPE.Text),
								 new FieldPara("ISNULL",FieldType.Int,ckbISNULL.Checked==true?"1":"0"),
                                 new FieldPara("ISPRIMARY",FieldType.Int,ckbISPRIMARY.Checked==true?"1":"0"),
                                 new FieldPara("LENGTH",FieldType.Int,txtLENGTH.Text),
                                 new FieldPara("ISDISPLAY",FieldType.Int,ckbISDISPLAY.Checked==true?"1":"0"),
                                 new FieldPara("CONTROL_LIST_WIDTH",FieldType.Int,txtCONTROL_LIST_WIDTH.Text),
                                 new FieldPara("CONTROL_LIST_DISPLAY_FORMAT",FieldType.String,cbbCONTROL_LIST_DISPLAY_FORMAT.Text),
                                 new FieldPara("CONTROL_LIST_DISPLAY_ALIGN",FieldType.String,align),                
                                 new FieldPara("CONTROL_HEIGHT",FieldType.Int,txtCONTROL_HEIGHT.Text),
                                 //new FieldPara("FILL_EXPRESSION",FieldType.String,txtFILL_EXPRESSION.Text.Replace('\'','^')),
                                 new FieldPara("RELATING_COLUMN",FieldType.String,cbbRELATING_COLUMN.Text),
                                 new FieldPara("ORDER_ID",FieldType.Int,txtORDER_ID.Text),
                                 new FieldPara("CUSTOM_CONTROL_NAME",FieldType.String,txtCUSTOM_CONTROL_NAME.Text),
                                 new FieldPara("CUSTOM_CONTROL_TYPE",FieldType.String,cbbCUSTOM_CONTROL_TYPE.Text),
                                 new FieldPara("CUSTOM_CONTROL_SVAE_TYPE",FieldType.String,cbbCUSTOM_CONTROL_SVAE_TYPE.Text),
                                 new FieldPara("CONTROL_DISPLAY_ONE_ROW",FieldType.Int,ckbCONTROL_DISPLAY_ONE_ROW.Checked==true?"1":"0"),
                                 new FieldPara("RELATING_CONDITION",FieldType.String,txtRELATING_CONDITION.Text)
                               };

            WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
            _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_COLUMNS", field, where);

            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
            {
                if (DBHelper.databaseType == "SqlServer")
                {
                }
                else if (DBHelper.databaseType == "Oracle")
                {
                    _sql = "update DMIS_SYS_COLUMNS set FILL_EXPRESSION=:FillExpression where ID=" + txtID.Text;
                    OracleParameter[] aPara = new OracleParameter[1];
                    OracleParameter pContent = new OracleParameter("FillExpression", OracleType.VarChar);
                    pContent.Value = txtFILL_EXPRESSION.Text;
                    aPara[0] = pContent;
                    DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                }
                else if (DBHelper.databaseType == "Sybase")
                {
                }
                else   //ODBC
                {
                }
            }
            InitColumns();
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (lvColumns.SelectedItems.Count < 1)
            {
                //MessageBox.Show("请先要选择删除的列!");
                return;
            }
            if (MessageBox.Show(Main.Properties.Resources.DeleteBeforeConfirm, Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            _sql = "delete from DMIS_SYS_COLUMNS where TABLE_ID=" + _tableId + " and ID=" + lvColumns.SelectedItems[0].Text;

            DBOpt.dbHelper.ExecuteSql(_sql);
            txtID.Text = "";
            InitColumns();
        }

        private void tsbAddAllColumns_Click(object sender, EventArgs e)
        {
            if (trvTables.SelectedNode.Level != 1)
            {
                //MessageBox.Show("添加所有列之前请先选择一个数据库表！");
                return;
            }
            if (lvColumns.Items.Count > 0)
            {
                //MessageBox.Show("数据库表已经有列，不允许使用此功能，只能单独添加一个列！");
                return;
            }

            uint maxID, orderID;
            string tableID;
            orderID = 0;
            tableID = trvTables.SelectedNode.Tag.ToString();
            maxID = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_COLUMNS", "ID");
            DataTable dt = DBOpt.dbHelper.GetDataTable("select NAME,OWNER from DMIS_SYS_TABLES where ID=" + tableID);

            if (DBHelper.databaseType == "Oracle")
            {
                string dataType;
                _sql = "select column_name,data_type from ALL_TAB_COLUMNS where table_name='" + dt.Rows[0][0].ToString() + "' and OWNER='" + dt.Rows[0][1].ToString() + "'";
                dt = DBOpt.dbHelper.GetDataTable(_sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataType = dt.Rows[i][1].ToString();
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
                            _sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,ORDER_ID,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CONTROL_DISPLAY_ONE_ROW,OTHER_LANGUAGE_DESCR) values("
                                + tableID + "," + maxID.ToString() + ",'" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][0].ToString() + "','String'," + orderID.ToString() + ",'txt" + dt.Rows[i][0].ToString().ToUpper() + "','TextBox',0,'" + dt.Rows[i][0].ToString() + "')";
                            break;
                        //数值
                        case "NUMBER":
                        case "FLOAT":
                            _sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,ORDER_ID,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CONTROL_DISPLAY_ONE_ROW,OTHER_LANGUAGE_DESCR) values("
                                + tableID + "," + maxID.ToString() + ",'" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][0].ToString() + "','Numeric'," + orderID.ToString() + ",'txt" + dt.Rows[i][0].ToString().ToUpper() + "','TextBox',0,'" + dt.Rows[i][0].ToString() + "')";
                            break;
                        //日期
                        case "DATE":
                            _sql = "insert into DMIS_SYS_COLUMNS(TABLE_ID,ID,NAME,DESCR,TYPE,ORDER_ID,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CONTROL_DISPLAY_ONE_ROW,OTHER_LANGUAGE_DESCR) values("
                                + tableID + "," + maxID.ToString() + ",'" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][0].ToString() + "','Datetime'," + orderID.ToString() + ",'wdl" + dt.Rows[i][0].ToString().ToUpper() + "','WebDateLib',0,'" + dt.Rows[i][0].ToString() + "')";
                            break;
                        default:
                            break;
                    }
                    DBOpt.dbHelper.ExecuteSql(_sql);
                    maxID = maxID + 1;
                    orderID = orderID + 10;
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

            InitColumns();
        }

        private void cbbCONTROL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbbCONTROL_TYPE.Text == "下拉列表")
            //{
            //    cbbFILL_STYLE.Enabled = true;
            //    txtFILL_EXPRESSION.Enabled = true;
            //    cbbRELATING_COLUMN.Enabled = true;
            //    txtRELATING_PROPERTY.Enabled = true;
            //}
            //else
            //{
            //    cbbFILL_STYLE.Text = "";
            //    cbbRELATING_COLUMN.Text = "";
            //    txtRELATING_PROPERTY.Text = "";
            //    txtFILL_EXPRESSION.Text = "";
            //    cbbFILL_STYLE.Enabled = false;
            //    txtFILL_EXPRESSION.Enabled = false;
            //    cbbRELATING_COLUMN.Enabled = false;
            //    txtRELATING_PROPERTY.Enabled = false;
            //}
        }

        private void cbbCUSTOM_CONTROL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCUSTOM_CONTROL_TYPE.Text.Trim() == "") return;

            switch (cbbCUSTOM_CONTROL_TYPE.Text.Trim())
            {
                case "TextBox":
                    cbbCUSTOM_CONTROL_SVAE_TYPE.Enabled = false;
                    cbbCUSTOM_CONTROL_SVAE_TYPE.SelectedIndex = -1;
                    if (txtCUSTOM_CONTROL_NAME.Text.Length > 3)
                    {
                        txtCUSTOM_CONTROL_NAME.Text = "txt" + txtCUSTOM_CONTROL_NAME.Text.Substring(3);
                    }
                    break;
                case "DropDownList":
                    cbbCUSTOM_CONTROL_SVAE_TYPE.Enabled = true;
                    cbbCUSTOM_CONTROL_SVAE_TYPE.SelectedIndex = 0;
                    if (txtCUSTOM_CONTROL_NAME.Text.Length > 3)
                    {
                        txtCUSTOM_CONTROL_NAME.Text = "ddl" + txtCUSTOM_CONTROL_NAME.Text.Substring(3);
                    }
                    break;
                case "CheckBox":
                    cbbCUSTOM_CONTROL_SVAE_TYPE.Enabled = false;
                    cbbCUSTOM_CONTROL_SVAE_TYPE.SelectedIndex = -1;
                    if (txtCUSTOM_CONTROL_NAME.Text.Length > 3)
                    {
                        txtCUSTOM_CONTROL_NAME.Text = "chk" + txtCUSTOM_CONTROL_NAME.Text.Substring(3);
                    }
                    break;
                case "CheckBoxList":
                    cbbCUSTOM_CONTROL_SVAE_TYPE.Enabled = false;
                    cbbCUSTOM_CONTROL_SVAE_TYPE.SelectedIndex = -1;
                    if (txtCUSTOM_CONTROL_NAME.Text.Length > 3)
                    {
                        txtCUSTOM_CONTROL_NAME.Text = "cbl" + txtCUSTOM_CONTROL_NAME.Text.Substring(3);
                    }
                    break;
                case "WebDateLib":
                    cbbCUSTOM_CONTROL_SVAE_TYPE.Enabled = false;
                    cbbCUSTOM_CONTROL_SVAE_TYPE.SelectedIndex = -1;
                    if (txtCUSTOM_CONTROL_NAME.Text.Length > 3)
                    {
                        txtCUSTOM_CONTROL_NAME.Text = "wdl" + txtCUSTOM_CONTROL_NAME.Text.Substring(3);
                    }
                    break;
                case "WebComboBox":
                    cbbCUSTOM_CONTROL_SVAE_TYPE.Enabled = false;
                    cbbCUSTOM_CONTROL_SVAE_TYPE.SelectedIndex = -1;
                    if (txtCUSTOM_CONTROL_NAME.Text.Length > 3)
                    {
                        txtCUSTOM_CONTROL_NAME.Text = "wcb" + txtCUSTOM_CONTROL_NAME.Text.Substring(3);
                    }
                    break;
                case "WebDropDown":
                    cbbCUSTOM_CONTROL_SVAE_TYPE.Enabled = false;
                    cbbCUSTOM_CONTROL_SVAE_TYPE.SelectedIndex = -1;
                    if (txtCUSTOM_CONTROL_NAME.Text.Length > 3)
                    {
                        txtCUSTOM_CONTROL_NAME.Text = "wdd" + txtCUSTOM_CONTROL_NAME.Text.Substring(3);
                    }
                    break;
                case "HtmlComboBox":
                    cbbCUSTOM_CONTROL_SVAE_TYPE.Enabled = false;
                    cbbCUSTOM_CONTROL_SVAE_TYPE.SelectedIndex = -1;
                    if (txtCUSTOM_CONTROL_NAME.Text.Length > 3)
                    {
                        txtCUSTOM_CONTROL_NAME.Text = "hcb" + txtCUSTOM_CONTROL_NAME.Text.Substring(3);
                    }
                    break;
                case "RadioButtonList":
                    cbbCUSTOM_CONTROL_SVAE_TYPE.Enabled = true;
                    cbbCUSTOM_CONTROL_SVAE_TYPE.SelectedIndex = 0;
                    if (txtCUSTOM_CONTROL_NAME.Text.Length > 3)
                    {
                        txtCUSTOM_CONTROL_NAME.Text = "rbl" + txtCUSTOM_CONTROL_NAME.Text.Substring(3);
                    }
                    break;
                case "HtmlInputText":
                    cbbCUSTOM_CONTROL_SVAE_TYPE.Enabled = false;
                    cbbCUSTOM_CONTROL_SVAE_TYPE.SelectedIndex = -1;
                    if (txtCUSTOM_CONTROL_NAME.Text.Length > 3)
                    {
                        txtCUSTOM_CONTROL_NAME.Text = "hit" + txtCUSTOM_CONTROL_NAME.Text.Substring(3);
                    }
                    break;
                default:
                    break;
            }
        }

        private void tlbAdd_Click(object sender, EventArgs e)
        {
            if (trvTables.SelectedNode.Level != 1)
            {
                //MessageBox.Show("添加数据库列之前请先选择一个数据库表！");
                return;
            }
            int id = Convert.ToInt16(trvTables.SelectedNode.Tag);
            frmSelectColumn column = new frmSelectColumn();
            column.tableID = id;
            DbDataReader dr = DBOpt.dbHelper.GetDataReader("select NAME,OWNER from DMIS_SYS_TABLES where ID=" + id.ToString());
            dr.Read();
            if (DBHelper.databaseType == "Oracle")
            {
                column.txtTableName.Text = dr[1].ToString() + "." + dr[0].ToString();
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                column.txtTableName.Text = dr[1].ToString() + ".dbo." + dr[0].ToString();
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                column.txtTableName.Text = dr[1].ToString() + ".dbo." + dr[0].ToString();
            }
            else
            {
            }
            dr.Close();
            if (column.ShowDialog() == DialogResult.OK)
            {
                InitColumns();
            }
        }

        private void Int_Validating(object sender, CancelEventArgs e)
        {
            int order;
            TextBox txt;
            if (sender is TextBox)
                txt = (TextBox)sender;
            else
                return;

            if (!(int.TryParse(txt.Text, out order) || txt.Text == ""))
            {
                errorProvider1.SetError((Control)sender, Main.Properties.Resources.NumericalValeError);  //不是数值类型;
                tlbSave.Enabled = false;
                e.Cancel = true;
            }
            else
            {
                tlbSave.Enabled = true;
                errorProvider1.SetError((Control)sender, "");
            }
        }

 
 

        private void frmColumns_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                tlbAdd_Click(null, null);
            else if (e.KeyCode == Keys.F3)
                tlbDelete_Click(null, null);
            else if (e.KeyCode == Keys.F4)
                tlbSave_Click(null, null);
        }

        //private void tlbRefresh_Click(object sender, EventArgs e)
        //{
        //    initTree();
        //    InitColumns();
        //}

        //刷新某节点下相应的表名。
        private void tsbTables_Click(object sender, EventArgs e)
        {
            if (trvTables.SelectedNode == null || trvTables.SelectedNode.Level != 0) return;
            trvTables.SelectedNode.Nodes.Clear();

            _sql = "select ID,DESCR from DMIS_SYS_TABLES where TYPE_ID=" + trvTables.SelectedNode.Tag.ToString()+ " order by ORDER_ID";
            DataTable dtChild;
            dtChild = DBOpt.dbHelper.GetDataTable(_sql);
            for (int j = 0; j < dtChild.Rows.Count; j++)
            {
                TreeNode tdChild = new TreeNode();
                tdChild.Text = dtChild.Rows[j][1].ToString();
                tdChild.Tag = dtChild.Rows[j][0];
                tdChild.ToolTipText = dtChild.Rows[j][0].ToString();
                tdChild.ImageIndex = 1;
                tdChild.SelectedImageIndex = 2;
                trvTables.SelectedNode.Nodes.Add(tdChild);
            }
        }

        private void ckbISPRIMARY_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbISPRIMARY.Checked) ckbISNULL.Checked = false;
        }


    }
}
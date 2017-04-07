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
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;

namespace PlatForm.DmisReport
{
    public partial class frmReport : Form
    {
        string _sql;

        public frmReport()
        {
            InitializeComponent();
        }


        private void frmReport_Load(object sender, EventArgs e)
        {
            initTree();
            initTableName();
        }

        private void initTree()
        {
            trvReport.Nodes.Clear();
            DataTable dt;
            DataTable dtChild;
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                dt = DBOpt.dbHelper.GetDataTable("select ID,NAME from DMIS_SYS_REPORT_TYPE order by ORDER_ID");
            else
                dt = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_REPORT_TYPE order by ORDER_ID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode td = new TreeNode();
                td.Text = dt.Rows[i][1].ToString();
                td.Tag = dt.Rows[i][0];
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                    dtChild = DBOpt.dbHelper.GetDataTable("select ID,NAME from DMIS_SYS_REPORT where TYPE_ID=" + dt.Rows[i][0].ToString() + " order by ORDER_ID");
                else
                    dtChild = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_REPORT where TYPE_ID=" + dt.Rows[i][0].ToString() + " order by ORDER_ID");

                for (int j = 0; j < dtChild.Rows.Count; j++)
                {
                    TreeNode tdChild = new TreeNode();
                    tdChild.Text = dtChild.Rows[j][1].ToString();
                    tdChild.Tag = dtChild.Rows[j][0];
                    tdChild.ImageIndex = 1;
                    tdChild.SelectedImageIndex = 2;
                    td.Nodes.Add(tdChild);
                }
                trvReport.Nodes.Add(td);
            }
            trvReport.SelectedNode = null;
        }

        private void trvReport_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvReport.SelectedNode.Level != 1) return;
            setAllControlNull();
            initReport();
            initParas();
            initListTables();
            initTreeTables();
        }

        /// <summary>
        /// 初始化ＴＡＢ"基本参数"
        /// </summary>
        private void initReport()
        {
            _sql = "select * from DMIS_SYS_REPORT where ID=" + trvReport.SelectedNode.Tag.ToString();
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            dr.Read();
            txtTYPE_ID.Text = dr["TYPE_ID"].ToString();
            txtID.Text = dr["ID"].ToString();
            txtNAME.Text = dr["NAME"].ToString();
            txtFILE_NAME.Text = dr["FILE_NAME"].ToString();
            cbbType.Text = dr["TYPE"] == Convert.DBNull ? "" : dr["TYPE"].ToString();
            txtORDER_ID.Text = dr["ORDER_ID"] == Convert.DBNull ? "" : dr["ORDER_ID"].ToString();
            txtOTHER_LANGUAGE_DESCR.Text = dr["OTHER_LANGUAGE_DESCR"] == Convert.DBNull ? "" : dr["OTHER_LANGUAGE_DESCR"].ToString();
            dr.Close();
        }

        /// <summary>
        /// 初始化ＴＡＢ"检索参数"
        /// </summary>
        private void initParas()
        {
            lsvParas.Items.Clear();
            ListViewItem lv;

            _sql = "select ID,DESCR,PARA_TYPE,DEPEND_ID,ORDER_ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_REPORT_PARA where REPORT_ID=" + trvReport.SelectedNode.Tag.ToString() + " order by ORDER_ID";
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
                        lv.SubItems.Add(dt.Rows[i][j].ToString());
                    }
                }
                lsvParas.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lsvParas, Color.SkyBlue, Color.Lime);
        }

        /// <summary>
        /// 初始化ＴＡＢ"数据库表参数"
        /// </summary>
        private void initListTables()
        {
            lsvTables.Items.Clear();
            ListViewItem lv;

            _sql = "select ID,TABLE_NAME,TABLE_TYPE,TABLE_ORDERS,TABLE_FILTER_WHERE,TABLE_PAGE_ROWS,ORDER_ID from DMIS_SYS_REPORT_TABLE where REPORT_ID=" + trvReport.SelectedNode.Tag.ToString() + " order by ORDER_ID";
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
                        lv.SubItems.Add(dt.Rows[i][j].ToString());
                    }
                }
                lsvTables.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lsvTables, Color.SkyBlue, Color.Lime);
        }

        /// <summary>
        /// 初始化数据库列参数中的表
        /// </summary>
        private void initTreeTables()
        {
            if (trvReport.SelectedNode ==null || trvReport.SelectedNode.Level != 1) return;

            trvTable.Nodes.Clear();
            _sql = "select ID,TABLE_NAME from DMIS_SYS_REPORT_TABLE where REPORT_ID=" + trvReport.SelectedNode.Tag.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode td = new TreeNode();
                td.Tag = dt.Rows[i][0];
                td.Text = dt.Rows[i][1].ToString();
                trvTable.Nodes.Add(td);
            }
        }
        /// <summary>
        /// 把所有控件值清空
        /// </summary>
        private void setAllControlNull()
        {
            //基本参数
            txtTYPE_ID.Text = "";
            txtID.Text = "";
            txtNAME.Text = "";
            txtFILE_NAME.Text = "";
            cbbType.Text = "";
            txtORDER_ID.Text = "";
            txtOTHER_LANGUAGE_DESCR.Text = "";

           //检索参数
            lsvParas.Items.Clear();
            txtParaID.Text = "";
            txtParaDESCR.Text = "";
            cbbPARA_TYPE.SelectedIndex=-1;
            cbbParaDEPEND_ID.SelectedIndex = -1;
            txtParaORDER_ID.Text = "";
            txtParaOTHER_LANGUAGE_DESCR.Text = "";

            //表参数
            lsvTables.Items.Clear();
            txtTableID.Text = "";
            cbbTABLE_NAME.SelectedIndex = -1;
            cbbTABLE_TYPE.SelectedIndex = -1;
            txtTABLE_PAGE_ROWS.Text = "";
            txtTableORDER_ID.Text = "";
            txtTABLE_ORDERS.Text = "";
            txtTABLE_FILTER_WHERE.Text = "";
            
            //列参数
            trvTable.Nodes.Clear();
            lsvColumns.Items.Clear();
            txtColumnID.Text = "";
            cbbColumnTABLE_NAME.SelectedIndex=-1;
            cbbColumnCOLUMN_NAME.SelectedIndex=-1;
            txtColumnCOLUMN_DESCR.Text = "";
            cbbDISPLAY_PATTERN.SelectedIndex = -1;
            txtColumnWORDS.Text = "";
            txtColumnORDER_ID.Text = "";
            txtR.Text = "";
            txtC.Text = "";
            txtP.Text = "";
            txtTABLE_ID.Text = "";
            txtColumnOTHER_LANGUAGE_DESCR.Text = "";
        }

        /// <summary>
        /// 把tab页"检索参数"中的所有控件值清空
        /// </summary>
        private void setParasControlNull()
        {
            //lsvParas.Items.Clear();
            txtParaID.Text = "";
            txtParaDESCR.Text = "";
            cbbPARA_TYPE.SelectedIndex = -1;
            cbbParaDEPEND_ID.SelectedIndex = -1;
            txtParaORDER_ID.Text = "";
            txtParaOTHER_LANGUAGE_DESCR.Text = "";
        }

        /// <summary>
        /// 把tab页"数据库表参数"中的所有控件值清空
        /// </summary>
        private void setTablesControlNull()
        {
            //lsvParas.Items.Clear();
            txtTableID.Text = "";
            cbbTABLE_NAME.SelectedIndex = -1;
            cbbTABLE_TYPE.SelectedIndex = -1;
            txtTABLE_PAGE_ROWS.Text = "";
            txtTableORDER_ID.Text = "";
            txtTABLE_ORDERS.Text = "";
            txtTABLE_FILTER_WHERE.Text = "";
        }

        /// <summary>
        /// 把tab页"数据库列参数"中的所有控件值清空
        /// </summary>
        private void setColumnControlNull()
        {
            //lsvColumns.Items.Clear();
            txtColumnID.Text = "";
            txtTABLE_ID.Text = "";
            cbbColumnTABLE_NAME.SelectedIndex = -1;
            cbbColumnCOLUMN_NAME.Text ="";
            txtColumnCOLUMN_DESCR.Text = "";
            //cbbDISPLAY_PATTERN.SelectedIndex = -1;  //为什么此行不起作用
            cbbDISPLAY_PATTERN.Text = "";
            txtColumnWORDS.Text = "";
            txtColumnORDER_ID.Text = "";
            txtR.Text = "";
            txtC.Text = "";
            txtP.Text = "";
            txtColumnOTHER_LANGUAGE_DESCR.Text = "";
        }


        private void lsvParas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvParas.SelectedItems.Count != 1) return;
            txtParaID.Text = lsvParas.SelectedItems[0].Text;
            txtParaDESCR.Text = lsvParas.SelectedItems[0].SubItems[1].Text;
            cbbPARA_TYPE.Text = lsvParas.SelectedItems[0].SubItems[2].Text;
            cbbParaDEPEND_ID.Text = lsvParas.SelectedItems[0].SubItems[3].Text;
            txtParaORDER_ID.Text = lsvParas.SelectedItems[0].SubItems[4].Text;
            txtParaOTHER_LANGUAGE_DESCR.Text = lsvParas.SelectedItems[0].SubItems[5].Text; 

            //设置依赖参数，从当前选中项的前面所有项填充
            cbbParaDEPEND_ID.Items.Clear();
            int pos=lsvParas.Items.IndexOf(lsvParas.SelectedItems[0]);
            for (int i = 0; i < pos; i++)
            {
                cbbParaDEPEND_ID.Items.Add(lsvParas.Items[i].Text);
            }
            cbbParaDEPEND_ID.Items.Add("");
        }


        private void lsvTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvTables.SelectedItems.Count != 1) return;
            cbbTABLE_NAME.Enabled = false;  //防止修改表名，出现DMIS_SYS_REPORT_CELL_COLUMN中的列和表不对应的情况
            txtTableID.Text=lsvTables.SelectedItems[0].Text;
            cbbTABLE_NAME.Text = lsvTables.SelectedItems[0].SubItems[1].Text;
            cbbTABLE_TYPE.SelectedIndex = cbbTABLE_TYPE.FindString(lsvTables.SelectedItems[0].SubItems[2].Text);
            txtTABLE_ORDERS.Text = lsvTables.SelectedItems[0].SubItems[3].Text;
            txtTABLE_FILTER_WHERE.Text = lsvTables.SelectedItems[0].SubItems[4].Text;
            txtTABLE_PAGE_ROWS.Text = lsvTables.SelectedItems[0].SubItems[5].Text;
            txtTableORDER_ID.Text = lsvTables.SelectedItems[0].SubItems[6].Text;
        }

        private void lsvColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvColumns.SelectedItems.Count != 1) return;
            txtColumnID.Text = lsvColumns.SelectedItems[0].Text;
            txtColumnCOLUMN_DESCR.Text = lsvColumns.SelectedItems[0].SubItems[2].Text;
            //cbbColumnCOLUMN_NAME.Text = lsvColumns.SelectedItems[0].SubItems[1].Text;  //放在下面是由于此控件要执行事件SelectedIndexChanged，而事件刚用到了上面的数据
            
            //if (cbbColumnCOLUMN_NAME.FindString(lsvColumns.SelectedItems[0].SubItems[1].Text) < 0)
                cbbColumnCOLUMN_NAME.Text = lsvColumns.SelectedItems[0].SubItems[1].Text;
            //else
            //    cbbColumnCOLUMN_NAME.SelectedIndex = cbbColumnCOLUMN_NAME.FindString(lsvColumns.SelectedItems[0].SubItems[1].Text);
            txtColumnOTHER_LANGUAGE_DESCR.Text = lsvColumns.SelectedItems[0].SubItems[3].Text;
            cbbDISPLAY_PATTERN.Text = lsvColumns.SelectedItems[0].SubItems[4].Text;
            txtColumnWORDS.Text = lsvColumns.SelectedItems[0].SubItems[5].Text;
            txtColumnORDER_ID.Text = lsvColumns.SelectedItems[0].SubItems[6].Text;
            cbbColumnTABLE_NAME.Text = lsvColumns.SelectedItems[0].SubItems[7].Text;
            txtR.Text=lsvColumns.SelectedItems[0].SubItems[8].Text;
            txtC.Text=lsvColumns.SelectedItems[0].SubItems[9].Text;
            txtP.Text = lsvColumns.SelectedItems[0].SubItems[10].Text;
            cbbCOLUMN_TYPE.Text = lsvColumns.SelectedItems[0].SubItems[11].Text;  //列类型
            txtTABLE_ID.Text = lsvColumns.SelectedItems[0].SubItems[12].Text;     //表ID,2007-10-16加上
        }


        private void tlbAdd_Click(object sender, EventArgs e)
        {
            if (tabReport.SelectedTab.Name == "tpgReport")//基本参数
            {
                if (trvReport.SelectedNode.Level != 0)
                {
                    //MessageBox.Show("请先选择一个报表分类！");
                    return;
                }
                setAllControlNull();
                txtTYPE_ID.Text = trvReport.SelectedNode.Tag.ToString();
                txtID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_REPORT", "ID").ToString();
            }
            else if (tabReport.SelectedTab.Name == "tpgParas")//检索参数
            {
                if (trvReport.SelectedNode.Level != 1)
                {
                    //MessageBox.Show("请先选择一个报表！");
                    return;
                }
                setParasControlNull();
                txtParaID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_REPORT_PARA", "ID").ToString();
            }
            else if (tabReport.SelectedTab.Name == "tpgTables")//数据库表参数
            {
                if (trvReport.SelectedNode.Level != 1)
                {
                    //MessageBox.Show("请先选择一个报表！");
                    return;
                }
                cbbTABLE_NAME.Enabled = true;
                setTablesControlNull();
                txtTableID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_REPORT_TABLE", "ID").ToString();
            }
            else
            {
                if (trvReport.SelectedNode.Level != 1)
                {
                    //MessageBox.Show("请先选择一个报表！");
                    return;
                }
                if (trvTable.SelectedNode==null)
                {
                    //MessageBox.Show("请先选择一个数据库表！");
                    return;
                }
                setColumnControlNull();
                cbbColumnTABLE_NAME.Text = trvTable.SelectedNode.Text;
                txtTABLE_ID.Text = trvTable.SelectedNode.Tag.ToString();
                txtColumnID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_REPORT_CELL_COLUMN", "ID").ToString();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (trvReport.SelectedNode.Level != 1)
            {
                //MessageBox.Show("删除前请先选择一个报表！");
                return;
            }

            if (tabReport.SelectedTab.Name == "tpgReport")//基本参数
            {
                if (MessageBox.Show(Reports.Properties.Resources.DeleteBeforeConfirm, Reports.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                _sql = "delete from DMIS_SYS_REPORT where ID=" + trvReport.SelectedNode.Tag.ToString();
                DBOpt.dbHelper.ExecuteSql(_sql);  //删除此报表的检索参数使用触发器
                trvReport.SelectedNode.Parent.Nodes.Remove(trvReport.SelectedNode);
                setAllControlNull();
            }
            else if (tabReport.SelectedTab.Name == "tpgParas")//检索参数
            {
                if (lsvParas.SelectedItems.Count != 1) return;

                if (MessageBox.Show(Reports.Properties.Resources.DeleteBeforeConfirm, Reports.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                _sql = "delete from DMIS_SYS_REPORT_PARA where ID=" + lsvParas.SelectedItems[0].Text;
                DBOpt.dbHelper.ExecuteSql(_sql);  //删除此报表的检索参数使用触发器
                lsvParas.Items.Remove(lsvParas.SelectedItems[0]);
                setParasControlNull();
            }
            else if (tabReport.SelectedTab.Name == "tpgTables")//数据库表参数
            {
                if (lsvTables.SelectedItems.Count != 1) return;

                if (MessageBox.Show(Reports.Properties.Resources.DeleteBeforeConfirm, Reports.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                _sql = "delete from DMIS_SYS_REPORT_TABLE where ID=" + lsvTables.SelectedItems[0].Text;
                if (DBOpt.dbHelper.ExecuteSql(_sql) > -1)
                {
                    lsvTables.Items.Remove(lsvTables.SelectedItems[0]);
                    setTablesControlNull();
                    initTreeTables();
                    lsvColumns.Items.Clear();
                    setColumnControlNull();
                 }
                else
                {
                    //MessageBox.Show("删除表时出错" + lsvTables.SelectedItems[0].SubItems[1].Text);
                    return;
                }
            }
            else
            {
                if (lsvColumns.SelectedItems.Count != 1) return;
                if (MessageBox.Show(Reports.Properties.Resources.DeleteBeforeConfirm, Reports.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                _sql = "delete from DMIS_SYS_REPORT_CELL_COLUMN where ID=" + lsvColumns.SelectedItems[0].Text;
                if (DBOpt.dbHelper.ExecuteSql(_sql) > -1)
                {
                    lsvColumns.Items.Remove(lsvColumns.SelectedItems[0]);
                    setColumnControlNull();
                }
                else
                {
                    //MessageBox.Show("删除列时出错"+lsvColumns.SelectedItems[0].SubItems[2].Text);
                    return;
                }
            }
        }

        /// <summary>
        /// 保存四个标签的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlbSave_Click(object sender, EventArgs e)
        {
            //string[] notNull = new string[9] {txtID.Text,txtNAME.Text,txtFILE_NAME.Text,txtParaDESCR.Text,cbbPARA_TYPE.Text,cbbTABLE_NAME.Text,
            //                                cbbColumnTABLE_NAME.Text,cbbColumnCOLUMN_NAME.Text,txtColumnCOLUMN_DESCR.Text};

           int temp;
           string[] isInt = new string[7] { txtORDER_ID.Text, cbbParaDEPEND_ID.Text,txtParaORDER_ID.Text, txtTABLE_PAGE_ROWS.Text, txtTableORDER_ID.Text, txtColumnWORDS.Text, txtColumnORDER_ID.Text };
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

            //先保存基本参数
            string[] notNullReport = new string[3] { txtID.Text, txtNAME.Text, txtFILE_NAME.Text };
            for (int i = 0; i < notNullReport.Length; i++)
            {
                if (notNullReport[i] == null || notNullReport[i].ToString().Trim() == "")
                {
                    MessageBox.Show(Reports.Properties.Resources.NoEmpty, Reports.Properties.Resources.Note);
                    return;
                }
            }

            string report_id;
            int counts;
            report_id = txtID.Text;
            counts=Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_REPORT where ID=" + report_id));
            FieldPara[] fieldReport = {new FieldPara("TYPE_ID",FieldType.Int,txtTYPE_ID.Text),
                                 new FieldPara("ID",FieldType.Int,txtID.Text),
								 new FieldPara("NAME",FieldType.String,txtNAME.Text),
								 new FieldPara("FILE_NAME",FieldType.String,txtFILE_NAME.Text),
								 new FieldPara("TYPE",FieldType.String,cbbType.Text),
								 new FieldPara("ORDER_ID",FieldType.Int,txtORDER_ID.Text),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtOTHER_LANGUAGE_DESCR.Text)
                               };

            if (counts == 0)  //插入
            {
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_REPORT", fieldReport);
                DBOpt.dbHelper.ExecuteSql(_sql);
                initTree();
            }
            else　　//修改
            {
                WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
                _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_REPORT", fieldReport, where);
                DBOpt.dbHelper.ExecuteSql(_sql);
                trvReport.SelectedNode.Text = txtNAME.Text;
            }
            
           

            //保存检索参数
            if (txtParaID.Text != "" && trvReport.SelectedNode!=null)
            {
                string[] notNullPara = new string[3] { txtParaID.Text, txtParaDESCR.Text, cbbPARA_TYPE.Text };
                for (int i = 0; i < notNullPara.Length; i++)
                {
                    if (notNullPara[i] == null || notNullPara[i].ToString().Trim() == "")
                    {
                        MessageBox.Show(Reports.Properties.Resources.NoEmpty, Reports.Properties.Resources.Note);
                        return;
                    }
                }
                //  先删除后插入,方便写代码
                _sql = "delete from DMIS_SYS_REPORT_PARA where ID=" + txtParaID.Text;
                DBOpt.dbHelper.ExecuteSql(_sql);
                FieldPara[] fieldParas = {new FieldPara("REPORT_ID",FieldType.Int,txtID.Text),
                     new FieldPara("ID",FieldType.Int,txtParaID.Text),
					 new FieldPara("DESCR",FieldType.String,txtParaDESCR.Text),
					 new FieldPara("PARA_TYPE",FieldType.String,cbbPARA_TYPE.Text),
                     new FieldPara("DEPEND_ID",FieldType.Int,cbbParaDEPEND_ID.Text),
					 new FieldPara("ORDER_ID",FieldType.Int,txtParaORDER_ID.Text),
                    new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtParaOTHER_LANGUAGE_DESCR.Text)
                   };

                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_REPORT_PARA", fieldParas);
                DBOpt.dbHelper.ExecuteSql(_sql);
                initParas();
            }


            //保存数据库表参数
            if (txtTableID.Text != "" && trvReport.SelectedNode != null)
            {
                string[] notNullTable = new string[2] { txtTableID.Text, cbbTABLE_NAME.Text };
                for (int i = 0; i < notNullTable.Length; i++)
                {
                    if (notNullTable[i] == null || notNullTable[i].ToString().Trim() == "")
                    {
                        MessageBox.Show(Reports.Properties.Resources.NoEmpty, Reports.Properties.Resources.Note);
                        return;
                    }
                }

                //不能先删除，因为和列有关系，只能先查找是否已经有此数据
                _sql = "select count(*) from DMIS_SYS_REPORT_TABLE where ID=" + txtTableID.Text;
                counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql));
                FieldPara[] fieldTables = {new FieldPara("REPORT_ID",FieldType.Int,txtID.Text),
                     new FieldPara("ID",FieldType.Int,txtTableID.Text),
					 new FieldPara("TABLE_NAME",FieldType.String,cbbTABLE_NAME.Text),
                     new FieldPara("TABLE_TYPE",FieldType.String,cbbTABLE_TYPE.Text),
					 new FieldPara("TABLE_PAGE_ROWS",FieldType.Int,txtTABLE_PAGE_ROWS.Text),
                     new FieldPara("ORDER_ID",FieldType.Int,txtTableORDER_ID.Text),
                     new FieldPara("TABLE_ORDERS",FieldType.String,txtTABLE_ORDERS.Text),
                     new FieldPara("TABLE_FILTER_WHERE",FieldType.String,txtTABLE_FILTER_WHERE.Text)
                   };
                if (counts == 0)  //插入
                {
                    _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_REPORT_TABLE", fieldTables);
                }
                else　　//修改
                {
                    WherePara[] where ={ new WherePara("ID", FieldType.Int, txtTableID.Text, "=", "and") };
                    _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_REPORT_TABLE", fieldTables, where);
                }

                if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)  //TABLE_FILTER_WHERE列可能保存to_char(datem,'yyyymm')这样的数据，故要用存储过程来更新
                {
                    if (DBHelper.databaseType == "SqlServer")
                    {
                        _sql = "update DMIS_SYS_REPORT_TABLE set TABLE_FILTER_WHERE=@TableFilterWhere where ID=" + txtTableID.Text;
                        System.Data.SqlClient.SqlParameter[] aPara = new System.Data.SqlClient.SqlParameter[1];
                        System.Data.SqlClient.SqlParameter pContent = new System.Data.SqlClient.SqlParameter("@TableFilterWhere", SqlDbType.VarChar);
                        pContent.Value = txtTABLE_FILTER_WHERE.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                    else if (DBHelper.databaseType == "Oracle")
                    {
                        _sql = "update DMIS_SYS_REPORT_TABLE set TABLE_FILTER_WHERE=:TableFilterWhere where ID=" + txtTableID.Text;
                        OracleParameter[] aPara = new OracleParameter[1];
                        OracleParameter pContent = new OracleParameter("TableFilterWhere", OracleType.VarChar);
                        pContent.Value = txtTABLE_FILTER_WHERE.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                    else if (DBHelper.databaseType == "Sybase")
                    {
                        _sql = "update DMIS_SYS_REPORT_TABLE set TABLE_FILTER_WHERE=? where ID=" + txtTableID.Text;
                        OleDbParameter[] aPara = new OleDbParameter[1];
                        OleDbParameter pContent = new OleDbParameter("@TableFilterWhere", OleDbType.VarChar);
                        pContent.Value = txtTABLE_FILTER_WHERE.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                    else   //ODBC
                    {
                        _sql = "update DMIS_SYS_REPORT_TABLE set TABLE_FILTER_WHERE=@TableFilterWhere where ID=" + txtTableID.Text;
                        OdbcParameter[] aPara = new OdbcParameter[1];
                        OdbcParameter pContent = new OdbcParameter("@TableFilterWhere", OdbcType.NVarChar);
                        pContent.Value = txtTABLE_FILTER_WHERE.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                }
                initListTables();
            }


            //保存数据库列参数
            if (txtColumnID.Text != "" && trvReport.SelectedNode != null)
            {
                string[] notNullColumn = new string[4] { txtColumnID.Text, cbbColumnTABLE_NAME.Text, cbbColumnCOLUMN_NAME.Text, txtColumnCOLUMN_DESCR.Text };
                for (int i = 0; i < notNullColumn.Length; i++)
                {
                    if (notNullColumn[i] == null || notNullColumn[i].ToString().Trim() == "")
                    {
                        MessageBox.Show(Reports.Properties.Resources.NoEmpty, Reports.Properties.Resources.Note);
                        return;
                    }
                }

                //先删除后插入,方便写代码
                _sql = "delete from DMIS_SYS_REPORT_CELL_COLUMN where ID=" + txtColumnID.Text;
                DBOpt.dbHelper.ExecuteSql(_sql);
                FieldPara[] fieldColumns = {new FieldPara("REPORT_ID",FieldType.Int,txtID.Text),
                     new FieldPara("ID",FieldType.Int,txtColumnID.Text),
                     new FieldPara("TABLE_ID",FieldType.Int,txtTABLE_ID.Text),
					 new FieldPara("TABLE_NAME",FieldType.String,cbbColumnTABLE_NAME.Text),
                     new FieldPara("COLUMN_NAME",FieldType.String,cbbColumnCOLUMN_NAME.Text),
                     new FieldPara("COLUMN_DESCR",FieldType.String,txtColumnCOLUMN_DESCR.Text),
					 new FieldPara("DISPLAY_PATTERN",FieldType.String,cbbDISPLAY_PATTERN.Text),
                     new FieldPara("WORDS",FieldType.Int,txtColumnWORDS.Text),
					 new FieldPara("ORDER_ID",FieldType.Int,txtColumnORDER_ID.Text),
                     new FieldPara("R",FieldType.Int,txtR.Text),
                     new FieldPara("C",FieldType.Int,txtC.Text),
                     new FieldPara("P",FieldType.Int,txtP.Text),
                     new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtColumnOTHER_LANGUAGE_DESCR.Text),
                     new FieldPara("COLUMN_TYPE",FieldType.String,cbbCOLUMN_TYPE.Text)
                   };

                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_REPORT_CELL_COLUMN", fieldColumns);
                
                //由于列名COLUMN_NAME可能包含单引号，如：right(Convert(char(8),故障时间,112),2)+'日'+convert(char(5),故障时间,108)
                //则必须使用参数的方式来更新此字段的内容。
                if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
                {
                    if (DBHelper.databaseType == "SqlServer")
                    {
                        _sql = "update DMIS_SYS_REPORT_CELL_COLUMN set COLUMN_NAME=@ColumnName where ID=" + txtColumnID.Text;
                        System.Data.SqlClient.SqlParameter[] aPara = new System.Data.SqlClient.SqlParameter[1];
                        System.Data.SqlClient.SqlParameter pContent = new System.Data.SqlClient.SqlParameter("@ColumnName", SqlDbType.VarChar);
                        pContent.Value = cbbColumnCOLUMN_NAME.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                    else if (DBHelper.databaseType == "Oracle")
                    {
                        _sql = "update DMIS_SYS_REPORT_CELL_COLUMN set COLUMN_NAME=:ColumnName where ID=" + txtColumnID.Text;
                        OracleParameter[] aPara = new OracleParameter[1];
                        OracleParameter pContent = new OracleParameter("ColumnName", OracleType.VarChar);
                        pContent.Value = cbbColumnCOLUMN_NAME.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                    else if (DBHelper.databaseType == "Sybase")
                    {
                        _sql = "update DMIS_SYS_REPORT_CELL_COLUMN set COLUMN_NAME=? where ID=" + txtColumnID.Text;
                        OleDbParameter[] aPara = new OleDbParameter[1];
                        OleDbParameter pContent = new OleDbParameter("@ColumnName", OleDbType.VarChar);
                        pContent.Value = cbbColumnCOLUMN_NAME.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                    else   //ODBC
                    {
                        _sql = "update DMIS_SYS_REPORT_CELL_COLUMN set COLUMN_NAME=@ColumnName where ID=" + txtColumnID.Text;
                        OdbcParameter[] aPara = new OdbcParameter[1];
                        OdbcParameter pContent = new OdbcParameter("@ColumnName", OdbcType.NVarChar);
                        pContent.Value = cbbColumnCOLUMN_NAME.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                }

                initColumns();
           }
        }

        private void picReportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.InitialDirectory = @"c:\df8360\source\web\report\";
            dlg.Filter = "Report File|*.cll;.cll";
            dlg.Title = "Report File";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtFILE_NAME.Text = dlg.FileName;
            }
        }

        /// <summary>
        /// 初始化界面上两个数据库表下拉控件中所用到的表名
        /// </summary>
        private void initTableName()
        {
            _sql = "select OWNER,NAME from DMIS_SYS_TABLES order by TYPE_ID asc,NAME asc";
            DbDataReader dr=DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                if(DBHelper.databaseType=="Oracle")
                {
                    cbbTABLE_NAME.Items.Add(dr[0].ToString() + "." + dr[1].ToString());
                    cbbColumnTABLE_NAME.Items.Add(dr[0].ToString()+"."+dr[1].ToString());
                }
                else if(DBHelper.databaseType=="SqlServer")
                {
                    cbbTABLE_NAME.Items.Add(dr[0].ToString() + ".dbo." + dr[1].ToString());
                    cbbColumnTABLE_NAME.Items.Add(dr[0].ToString()+".dbo."+dr[1].ToString());
                }
                else if (DBHelper.databaseType == "Sybase")
                {
                    cbbTABLE_NAME.Items.Add(dr[0].ToString() + ".dbo." + dr[1].ToString());
                    cbbColumnTABLE_NAME.Items.Add(dr[0].ToString() + ".dbo." + dr[1].ToString());
                }
                else
                    ;
            }
            dr.Close();

        }

        /// <summary>
        /// 此方法已经不使用了
        /// 列参数中的数据库表变化时，相应要把下拉列表列名也要变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbColumnTABLE_NAME_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbColumnTABLE_NAME.Text == "") return;
            string owner, tableName,tableID;

            owner = cbbColumnTABLE_NAME.Text.Substring(0, cbbColumnTABLE_NAME.Text.IndexOf('.') );
            tableName = cbbColumnTABLE_NAME.Text.Substring(cbbColumnTABLE_NAME.Text.LastIndexOf('.') + 1);
            tableID = DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where OWNER='" + owner + "' and NAME='" + tableName + "'" + " order by ORDER_ID").ToString();
            cbbColumnCOLUMN_NAME.Items.Clear();

            _sql = "select NAME from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID+" order by ORDER_ID";
            DbDataReader dr=DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                cbbColumnCOLUMN_NAME.Items.Add(dr[0]);
            }
            dr.Close();
        }


        private void trvTable_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvTable.SelectedNode == null) return;
            setColumnControlNull();
            initColumns();

            //从填充DMIS_SYS_COLUMNS中取出列，填充控件cbbColumnCOLUMN_NAME
            string owner, tableName, tableID;

            owner = trvTable.SelectedNode.Text.Substring(0, trvTable.SelectedNode.Text.IndexOf('.'));
            tableName = trvTable.SelectedNode.Text.Substring(trvTable.SelectedNode.Text.LastIndexOf('.') + 1);
            tableID = DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where OWNER='" + owner + "' and NAME='" + tableName + "'" + " order by ORDER_ID").ToString();
            cbbColumnCOLUMN_NAME.Items.Clear();

            _sql = "select NAME from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID + " order by ORDER_ID";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                cbbColumnCOLUMN_NAME.Items.Add(dr[0]);
            }
            dr.Close();

        }

        /// <summary>
        /// TAB页"数据库列参数"表选中后刷新此表所包含的列
        /// </summary>
        private void initColumns()
        {
            lsvColumns.Items.Clear();
            ListViewItem lv;

            _sql = "select ID,COLUMN_NAME,COLUMN_DESCR,OTHER_LANGUAGE_DESCR,DISPLAY_PATTERN,WORDS,ORDER_ID,TABLE_NAME,R,C,P,COLUMN_TYPE,TABLE_ID from DMIS_SYS_REPORT_CELL_COLUMN where REPORT_ID=" 
                + trvReport.SelectedNode.Tag.ToString() + " and TABLE_ID=" + trvTable.SelectedNode.Tag.ToString() + " order by ORDER_ID";
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
                        lv.SubItems.Add(dt.Rows[i][j].ToString());
                    }
                }
                lsvColumns.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lsvColumns, Color.SkyBlue, Color.Lime);
        }

        private void cbbDISPLAY_PATTERN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbDISPLAY_PATTERN.Text=="SingleLine")
                txtColumnWORDS.ReadOnly=true;
            else
                txtColumnWORDS.ReadOnly = false;
        }


        /// <summary>
        /// 自动写列描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbColumnCOLUMN_NAME_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (txtColumnCOLUMN_DESCR.Text != "") return;
            if (cbbColumnTABLE_NAME.Text == "") return;
            string tableName,owner,columnDescr;
            owner = cbbColumnTABLE_NAME.Text.Substring(0, cbbColumnTABLE_NAME.Text.IndexOf('.'));
            tableName = cbbColumnTABLE_NAME.Text.Substring(cbbColumnTABLE_NAME.Text.LastIndexOf('.')+1);
            _sql = "select a.DESCR from DMIS_SYS_COLUMNS a,DMIS_SYS_TABLES b where a.TABLE_ID=b.ID and b.NAME='" + tableName + "' and b.OWNER='" + owner + "' and a.NAME='" + cbbColumnCOLUMN_NAME.Text+"'";
            columnDescr = DBOpt.dbHelper.ExecuteScalar(_sql).ToString();
            txtColumnCOLUMN_DESCR.Text=columnDescr;
        }

        private void tabReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabReport.SelectedTab.Name == "tpgColumns")
            {
                initTreeTables();
            }
        }

        private void tlbAllColumns_Click(object sender, EventArgs e)
        {
            if (tabReport.SelectedTab.Name != "tpgColumns")
            {
                //MessageBox.Show("请先选择标签：数据库列参数");
                return;
            }
            if (trvTable.SelectedNode == null)
            {
                //MessageBox.Show("请先选择一个数据库表");
                return;
            }
            if (lsvColumns.Items.Count > 0)
            {
                //MessageBox.Show("已经存在列，不允许使用此功能，只能单独添加列!");
                return;
            }

            string tableName, owner;
            uint id;
            int counts = 1;
            id=DBOpt.dbHelper.GetMaxNum("DMIS_SYS_REPORT_CELL_COLUMN","ID");
            owner = trvTable.SelectedNode.Text.Substring(0, trvTable.SelectedNode.Text.IndexOf('.'));
            tableName = trvTable.SelectedNode.Text.Substring(trvTable.SelectedNode.Text.LastIndexOf('.')+1);
            _sql = "select a.NAME,a.DESCR,a.TYPE,a.OTHER_LANGUAGE_DESCR from DMIS_SYS_COLUMNS a,DMIS_SYS_TABLES b where a.TABLE_ID=b.ID and b.OWNER='" + owner + "' and b.NAME='" + tableName + "'";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _sql = "insert into DMIS_SYS_REPORT_CELL_COLUMN(REPORT_ID,ID,TABLE_ID,TABLE_NAME,COLUMN_NAME,COLUMN_DESCR,DISPLAY_PATTERN,ORDER_ID,COLUMN_TYPE,OTHER_LANGUAGE_DESCR) values (" +
                        trvReport.SelectedNode.Tag.ToString() + "," + id.ToString() + "," + trvTable.SelectedNode.Tag.ToString() + ",'" + trvTable.SelectedNode.Text + "','" + dt.Rows[i][0].ToString() + "','" +
                        dt.Rows[i][1].ToString() + "','SingleLine'," + counts.ToString() + ",'" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "')";
                DBOpt.dbHelper.ExecuteSql(_sql);
                id++;
                counts++;
            }
            initColumns();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            if (cbbTABLE_NAME.Text.Trim() == "")
            {
                //MessageBox.Show("请先选择一个数据库表","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            frmReportOrder order = new frmReportOrder();
            order.tableName = cbbTABLE_NAME.Text;
            order.returnString = txtTABLE_ORDERS.Text;
            if(order.ShowDialog()==DialogResult.OK)
                txtTABLE_ORDERS.Text = order.returnString;
            
        }

        private void btnFilters_Click(object sender, EventArgs e)
        {
            if (cbbTABLE_NAME.Text.Trim() == "")
            {
                //MessageBox.Show("请先选择一个数据库表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frmFilter filter = new frmFilter();
            filter.tableName = cbbTABLE_NAME.Text;
            filter.returnString = txtTABLE_FILTER_WHERE.Text;
            filter.reportID = txtID.Text;
            if (filter.ShowDialog() == DialogResult.OK)
                txtTABLE_FILTER_WHERE.Text = filter.returnString;
        }

        private void Int_Validating(object sender, CancelEventArgs e)
        {
            int order;
            TextBox txt;
            if (sender is TextBox)
            {
                txt = (TextBox)sender;
            }
            else
                return;

            if (!(int.TryParse(txt.Text, out order) || txt.Text == ""))
            {
                errorProvider1.SetError((Control)sender, Reports.Properties.Resources.NumericalValeError);
                tlbSave.Enabled = false;
                e.Cancel = true;
            }
            else
            {
                tlbSave.Enabled = true;
                errorProvider1.SetError((Control)sender, "");
            }
        }

        private void tlbFindColumnType_Click(object sender, EventArgs e)
        {
            if (tabReport.SelectedTab.Name != "tpgColumns")
            {
                //MessageBox.Show("请先选择标签：数据库列参数");
                return;
            }
            if (trvTable.SelectedNode == null)
            {
                //MessageBox.Show("请先选择一个数据库表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //要注意，列描述对就真正的列名，列名在此可以作转换。
            string tableName = trvTable.SelectedNode.Text.Substring(trvTable.SelectedNode.Text.LastIndexOf('.')+1);
            object obj;
            for (int i = 0; i < lsvColumns.Items.Count; i++)
            {
                _sql = "select a.TYPE from DMIS_SYS_COLUMNS a,DMIS_SYS_TABLES b where a.TABLE_ID=b.ID and a.NAME='" + lsvColumns.Items[i].SubItems[2].Text + "' and b.NAME='" + tableName+"'";
                obj = DBOpt.dbHelper.ExecuteScalar(_sql);
                if (obj == null) continue;
                _sql = "update DMIS_SYS_REPORT_CELL_COLUMN set COLUMN_TYPE='" + obj.ToString() + "' where ID=" + lsvColumns.Items[i].Text;
                DBOpt.dbHelper.ExecuteSql(_sql);
            }
            initColumns();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tabReport.SelectedTab.Name != "tpgColumns")
            {
                //MessageBox.Show("请先选择标签：数据库列参数");
                return;
            }
            if (trvTable.SelectedNode == null)
            {
                //MessageBox.Show("请先选择一个数据库表");
                return;
            }

            _sql = "select ID from DMIS_SYS_REPORT_CELL_COLUMN where TABLE_NAME='" + trvTable.SelectedNode.Text + "'";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _sql = "update DMIS_SYS_REPORT_CELL_COLUMN set TABLE_ID=" + trvTable.SelectedNode.Tag.ToString() + " where ID=" + dt.Rows[i][0].ToString();
                if (DBOpt.dbHelper.ExecuteSql(_sql) < 1)
                {
                    //MessageBox.Show("更新表ID失败");
                    return;
                }
            }
            

        }

        /// <summary>
        /// 对于多行列,双击此列则获取一行能显示多少个字符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtColumnWORDS_DoubleClick(object sender, EventArgs e)
        {
            //不是多行列不处理 
            if (cbbDISPLAY_PATTERN.Text != "MultiLine") return;    
            
            //找不到本机的报表文件,不处理．
            if (!System.IO.File.Exists(txtFILE_NAME.Text))
            {
                //MessageBox.Show("找不到报表文件：" + txtFILE_NAME.Text);
                return;
            }

            //没有设置此列的位置，不处理
            if (txtC.Text == "" || txtP.Text == "")
            {
                //MessageBox.Show("没有设置此列在报表文件中所处的页号或列号！");
                return;
            }
            int ret = m_cell.OpenFile(txtFILE_NAME.Text, "");
            if (ret != 1)
            {
                switch (ret)
                {
                    case -1:
                        MessageBox.Show("error number:-1");//"文件不存在！"
                        break;
                    case -2:
                        MessageBox.Show("error number:-2");//"文件操作错误！"
                        break;
                    case -3:
                        MessageBox.Show("error number:-3");//"文件格式错误！"
                        break;
                    case -4:
                        MessageBox.Show("error number:-4");//"密码错误！"
                        break;
                    case -5:
                        MessageBox.Show("error number:-5");//"不能打开高版本文件！"
                        break;
                    case -6:
                        MessageBox.Show("error number:-6");//"不能打开特定版本文件！"
                        break;
                    default:
                        break;
                }
                return;
            }
            int startCol = 0, startRow = 0, endCol = 0, endRow = 0;
            int cellWidth = 0;
            float fontSize;
            m_cell.SetCurSheet(Convert.ToInt16(txtP.Text));
            m_cell.GetMergeRange(Convert.ToInt16(txtC.Text), Convert.ToInt16(txtR.Text), ref startCol, ref startRow, ref endCol, ref endRow);
            for (int col = startCol; col <= endCol; col++)
            {
                cellWidth += m_cell.GetColWidth(0, col, Convert.ToInt16(txtP.Text));
            }
            
            fontSize = m_cell.GetCellFontSize(Convert.ToInt16(txtC.Text), Convert.ToInt16(txtR.Text), Convert.ToInt16(txtP.Text));
            if (fontSize == 0) fontSize = 9;
            //逻辑单位：1个单位表示1/10mm,而1英寸=72磅，1英寸=2.54cm    int(cell_len/lval*72.0/254*2) - 1;
            cellWidth = Convert.ToInt16((cellWidth / fontSize) * 0.56692913386) ; //英文字符数
            cellWidth = cellWidth / 2 + 1;   //中文字符数  ,加1完全是经验
            txtColumnWORDS.Text=cellWidth.ToString();
        }



        private void tmiAdd序号_Click(object sender, EventArgs e)
        {
            if (tabReport.SelectedTab.Name != "tpgColumns")
            {
                //MessageBox.Show("请先选择标签：数据库列参数");
                return;
            }
            if (trvTable.SelectedNode == null)
            {
                //MessageBox.Show("请先选择一个数据库表");
                return;
            }

            //查找此表是否已经存在序号列
            _sql = "select count(*) from DMIS_SYS_REPORT_CELL_COLUMN where REPORT_ID= " +trvReport.SelectedNode.Tag.ToString()+ " and TABLE_ID="+trvTable.SelectedNode.Tag.ToString()+" and COLUMN_NAME='serial_no'";
            object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (Convert.ToInt16(obj) > 0)
            {
                //MessageBox.Show("已经存在序号列，不允许再添加!");
                return;
            }
            else
            {
                uint max=DBOpt.dbHelper.GetMaxNum("DMIS_SYS_REPORT_CELL_COLUMN","ID");
                _sql = "insert into DMIS_SYS_REPORT_CELL_COLUMN(REPORT_ID,ID,TABLE_ID,TABLE_NAME,COLUMN_NAME,COLUMN_DESCR,OTHER_LANGUAGE_DESCR) values(" +
                     txtID.Text + "," + max.ToString() + "," + trvTable.SelectedNode.Tag.ToString() + ",'" + trvTable.SelectedNode.Text + "','serial_no','serial_no','Número')";
                DBOpt.dbHelper.ExecuteSql(_sql);
                initColumns();
            }
        }

        //更新西文列描述
        private void tmiUpdateColOtherDesc_Click(object sender, EventArgs e)
        {
            if (trvReport.SelectedNode.Level != 1) return;
            if (tabReport.SelectedTab.Name != "tpgColumns") return;
            if (trvTable.SelectedNode == null) return;

            string tableID;
            tableID = trvTable.SelectedNode.Tag.ToString();
            object colOtherDesc;
            for (int i = 0; i < lsvColumns.Items.Count; i++)
            {
                _sql = "select OTHER_LANGUAGE_DESCR from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID + " and NAME='" + lsvColumns.Items[i].SubItems[1] + "'";
                colOtherDesc = DBOpt.dbHelper.ExecuteScalar(_sql);
                if (colOtherDesc == null) continue;
                _sql = "update DMIS_SYS_REPORT_CELL_COLUMN set OTHER_LANGUAGE_DESCR='" + colOtherDesc.ToString() + "' where REPORT_ID=" + trvReport.SelectedNode.Tag.ToString() + 
                    " and TABLE_ID=" + tableID + " and COLUMN_NAME='" + lsvColumns.Items[i].SubItems[1] + "'";
                if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
                    lsvColumns.Items[i].SubItems[1].Text = colOtherDesc.ToString();
            }
        }



    }
}
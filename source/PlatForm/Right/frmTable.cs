using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using PlatForm.Functions;

namespace PlatForm
{
    public partial class frmTable : Form
    {
        string _sql;

        public frmTable()
        {
            InitializeComponent();
        }

        private void frmTable_Load(object sender, EventArgs e)
        {
            cbbDISPLAY_STYLE.Items.Add(new ComboxItem(Main.Properties.Resources.ColumnsTwo, "0"));//"二列"
            cbbDISPLAY_STYLE.Items.Add(new ComboxItem(Main.Properties.Resources.ColumnsOne, "1"));//"一列"
            cbbDISPLAY_STYLE.SelectedIndex = 0;

            tvTableType.Nodes.Clear();

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select ID,DESCR from DMIS_SYS_TABLE_TYPE order by ORDER_ID";
            else
                _sql = "select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_TABLE_TYPE order by ORDER_ID";

            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode td = new TreeNode();
                td.Text=dt.Rows[i][1].ToString();
                td.Tag = dt.Rows[i][0].ToString();
                tvTableType.Nodes.Add(td);
            }
            tvTableType.SelectedNode = tvTableType.Nodes[0];
        }

        private void InitTable(TreeNode e)
        {
            if (e == null) return;

            lsvTable.Items.Clear();
            ListViewItem lv;
            _sql = "select ID,OWNER,NAME,DESCR,PAGE_ROWS,ORDER_ID,DISPLAY_STYLE,OTHER_LANGUAGE_DESCR from DMIS_SYS_TABLES where TYPE_ID=" + e.Tag.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(dt.Rows[i][0].ToString());
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] is System.DBNull)
                        lv.SubItems.Add("");
                    else
                        lv.SubItems.Add(dt.Rows[i][j].ToString());
                }
                lsvTable.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lsvTable, Color.SkyBlue, Color.Lime);
        }

        private void tvTableType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            InitTable(tvTableType.SelectedNode);
        }

        private void tlbAdd_Click(object sender, EventArgs e)
        {
            if (tvTableType.SelectedNode == null)
            {
                MessageBox.Show(this, Main.Properties.Resources.SelectItem);//"添加数据库表之前，请先选择一个分类!"
                return;
            }
            frmSelectTable selectTable = new frmSelectTable(Convert.ToInt32(tvTableType.SelectedNode.Tag));
            selectTable.ShowDialog();
            InitTable(tvTableType.SelectedNode);
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (lsvTable.SelectedItems.Count < 1)
            {
                MessageBox.Show(this,Main.Properties.Resources.SelectDeleteItem );//"请先选择要删除的数据库表!"
                return;
            }
            else
            {
                if (DBOpt.dbHelper.IsExist("DMIS_SYS_COLUMNS", "TABLE_ID=" +lsvTable.SelectedItems[0].Text))
                {
                    //MessageBox.Show(this, "数据库表"+lsvTable.SelectedItems[0].SubItems[3].Text+"已经存在列，不允许删除！");
                    MessageBox.Show(this, Main.Properties.Resources.RelatdItemsNoDelete);
                    return;
                }
                if (MessageBox.Show(Main.Properties.Resources.DeleteBeforeConfirm, Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            }

            _sql = "delete from DMIS_SYS_TABLES where ID=" + lsvTable.SelectedItems[0].Text;
            DBOpt.dbHelper.ExecuteSql(_sql);
            InitTable(tvTableType.SelectedNode);
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtTYPE_ID.Text == "" || txtNAME.Text.Trim() == "" || txtTYPE_ID.Text.Trim() == "" || txtOWNER.Text.Trim() == "" || txtDESCR.Text.Trim() == "")
            {
                MessageBox.Show(this, Main.Properties.Resources.NoEmpty);//"某些项不允许为空！"
                return;
            }
            if (txtORDER_ID.Text.Trim() != "")
            {
                int order;
                if (!int.TryParse(txtORDER_ID.Text.Trim(), out order))
                {
                    MessageBox.Show(this, Main.Properties.Resources.NumericalValeError);//"排列序号不是整数类型！"
                    return;
                }
                else
                {
                    if (order > 32767 || order < 0)
                    {
                        //MessageBox.Show(this, "排列序号必须在0~32767之间！");
                        return;
                    }
                }
            }

            if (txtPAGE_ROWS.Text.Trim() != "")
            {
                int order;
                if (!int.TryParse(txtPAGE_ROWS.Text.Trim(), out order))
                {

                    //MessageBox.Show(this, "每页行数不是整数类型！");
                    MessageBox.Show(this, Main.Properties.Resources.NumericalValeError);
                    return;
                }
                else
                {
                    if (order > 32767 || order < 0)
                    {
                        //MessageBox.Show(this, "每页行数必须在0~32767之间！");
                        return;
                    }
                }
            }

            string style;
            if (cbbDISPLAY_STYLE.SelectedItem == null)
                style = "";
            else
            {
                ComboxItem item = (ComboxItem)cbbDISPLAY_STYLE.SelectedItem;
                style = item.Value;
            }

            FieldPara[] field = {new FieldPara("ID",FieldType.Int,txtID.Text),
								 new FieldPara("NAME",FieldType.String,txtNAME.Text),
								 new FieldPara("TYPE_ID",FieldType.Int,txtTYPE_ID.Text),
								 new FieldPara("OWNER",FieldType.String,txtOWNER.Text),
								 new FieldPara("DESCR",FieldType.String,txtDESCR.Text),
                                 new FieldPara("PAGE_ROWS",FieldType.Int,txtPAGE_ROWS.Text),
                                 new FieldPara("ORDER_ID",FieldType.Int,txtORDER_ID.Text),
                                 new FieldPara("DISPLAY_STYLE",FieldType.String,style),
                                 new FieldPara("QUERY_COL",FieldType.String,cbbQUERY_COL.Text),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtOTHER_LANGUAGE_DESCR.Text)
                               };
            if (DBOpt.dbHelper.IsExist("DMIS_SYS_TABLES", "ID=" + txtID.Text))
            {
                WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
                _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_TABLES", field, where);
            }
            else
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_TABLES", field);

            if (DBOpt.dbHelper.ExecuteSql(_sql) >= 0)
            {
                InitTable(tvTableType.SelectedNode);
            }
            else
            {
                //Log.InsertLog("", "失败", "维护数据表时,语句出错：" +  _sql);
                //MessageBox.Show(this, "更新失败，已经记录日志!");
                return;
            }
        }

        private void lvTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvTable.SelectedItems.Count != 1) return;

            _sql = "select * from DMIS_SYS_TABLES where ID=" + lsvTable.SelectedItems[0].Text;
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);

            //用时间类型的列填充cbbQUERY_COL  2008-8-11 
            _sql = "select NAME from DMIS_SYS_COLUMNS where TABLE_ID=" + lsvTable.SelectedItems[0].Text + " and TYPE='时间' order by ORDER_ID";
            DataTable DateCols=DBOpt.dbHelper.GetDataTable(_sql);
            DataRow drow = DateCols.NewRow();
            DateCols.Rows.InsertAt(drow, 0);
            cbbQUERY_COL.DisplayMember = "NAME";
            cbbQUERY_COL.ValueMember = "NAME";
            cbbQUERY_COL.DataSource = DateCols;

            txtTYPE_ID.Text = dt.Rows[0]["TYPE_ID"].ToString();
            txtID.Text = dt.Rows[0]["ID"].ToString();
            txtOWNER.Text = dt.Rows[0]["OWNER"].ToString();
            txtNAME.Text=dt.Rows[0]["NAME"].ToString();
            txtDESCR.Text = dt.Rows[0]["DESCR"].ToString();
            txtPAGE_ROWS.Text = dt.Rows[0]["PAGE_ROWS"] == Convert.DBNull ? "" : dt.Rows[0]["PAGE_ROWS"].ToString();
            txtORDER_ID.Text = dt.Rows[0]["ORDER_ID"] == Convert.DBNull ? "" : dt.Rows[0]["ORDER_ID"].ToString();
            cbbDISPLAY_STYLE.SelectedIndex = Convert.ToInt16(dt.Rows[0]["DISPLAY_STYLE"]);
            cbbQUERY_COL.Text = dt.Rows[0]["QUERY_COL"] == Convert.DBNull ? "" : dt.Rows[0]["QUERY_COL"].ToString();
            
            if (dt.Rows[0]["OTHER_LANGUAGE_DESCR"] != null)
                txtOTHER_LANGUAGE_DESCR.Text = dt.Rows[0]["OTHER_LANGUAGE_DESCR"].ToString();
            else
                txtOTHER_LANGUAGE_DESCR.Text = "";
        }


 
        private void txtORDER_ID_Validating(object sender, CancelEventArgs e)
        {
            int order;
            if (!(int.TryParse(txtORDER_ID.Text, out order) || txtORDER_ID.Text == ""))
            {
                errorProvider1.SetError((Control)sender, Main.Properties.Resources.NumericalValeError);
                tlbSave.Enabled = false;
                e.Cancel = true;
            }
            else
            {
                tlbSave.Enabled = true;
                errorProvider1.SetError((Control)sender, "");
            }
        }

        private void txtPAGE_ROWS_Validating(object sender, CancelEventArgs e)
        {
            int order;
            if (!(int.TryParse(txtPAGE_ROWS.Text, out order) || txtPAGE_ROWS.Text == ""))
            {
                errorProvider1.SetError((Control)sender, Main.Properties.Resources.NumericalValeError);
                tlbSave.Enabled = false;
                e.Cancel = true;
            }
            else
            {
                tlbSave.Enabled = true;
                errorProvider1.SetError((Control)sender, "");
            }
        }

        private void tlbRefresh_Click(object sender, EventArgs e)
        {
            InitTable(tvTableType.SelectedNode);
        }

        private void frmTable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                tlbAdd_Click(null, null);
            else if (e.KeyCode == Keys.F3)
                tlbDelete_Click(null, null);
            else if (e.KeyCode == Keys.F4)
                tlbSave_Click(null, null);
            else
                ;
        }

    }
}
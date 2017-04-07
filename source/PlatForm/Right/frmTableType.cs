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
    public partial class frmTableType : Form
    {
        private string _sql;

        public frmTableType()
        {
            InitializeComponent();
        }

        private void frmTableType_Load(object sender, EventArgs e)
        {
            InitTableType();
        }

        private void InitTableType()
        {
            lvTableType.Items.Clear();
            ListViewItem lv;
            DataTable dt = DBOpt.dbHelper.GetDataTable("select ID,DESCR,ORDER_ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_TABLE_TYPE order by ORDER_ID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(dt.Rows[i][0].ToString());

                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != null) lv.SubItems.Add(dt.Rows[i][j].ToString());
                }
                lvTableType.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lvTableType, Color.SkyBlue, Color.Lime);
        }

        private void tlbAdd_Click(object sender, EventArgs e)
        {
            txtID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_TABLE_TYPE", "ID").ToString();
            txtDESCR.Text = "";
            txtOTHER_LANGUAGE_DESCR.Text = "";
            int count = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_TABLE_TYPE"));
            txtORDER_ID.Text = Convert.ToString(count*10);
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (lvTableType.SelectedItems.Count < 1)
            {
                MessageBox.Show(this, Main.Properties.Resources.SelectDeleteItem);//"请先选择要删除的分类!"
                return;
            }
            else
            {
                if (DBOpt.dbHelper.IsExist("DMIS_SYS_TABLES", "TYPE_ID=" + lvTableType.SelectedItems[0].Text))
                {
                    MessageBox.Show(this, Main.Properties.Resources.RelatdItemsNoDelete);//"此表分类下已经存在数据库表，不允许删除!"
                    return;
                }
                if (MessageBox.Show(Main.Properties.Resources.DeleteBeforeConfirm, Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            }

            _sql = "delete from DMIS_SYS_TABLE_TYPE where ID=" + lvTableType.SelectedItems[0].Text;
            DBOpt.dbHelper.ExecuteSql(_sql);
            InitTableType();
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtDESCR.Text.Trim() == "")
            {
                //MessageBox.Show(this, "类型名称不允许为空！");
                return;
            }
            if (txtORDER_ID.Text.Trim() != "")
            {
                int order;
                if (!int.TryParse(txtORDER_ID.Text.Trim(), out order))
                {

                    //MessageBox.Show(this, "排列序号不是整数类型！");
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

            FieldPara[] field = {new FieldPara("ID",FieldType.Int,txtID.Text),
								 new FieldPara("ORDER_ID",FieldType.Int,txtORDER_ID.Text),
	                             new FieldPara("DESCR",FieldType.String,txtDESCR.Text),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtOTHER_LANGUAGE_DESCR.Text)
                               };
            if (DBOpt.dbHelper.IsExist("DMIS_SYS_TABLE_TYPE", "ID=" + txtID.Text))
            {
                WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
                _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_TABLE_TYPE", field, where);
            }
            else
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_TABLE_TYPE", field);

            DBOpt.dbHelper.ExecuteSql(_sql);
            InitTableType();
        }

        private void lvTableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvTableType.SelectedItems.Count != 1) return;

            txtID.Text = lvTableType.SelectedItems[0].Text;
            txtDESCR.Text = lvTableType.SelectedItems[0].SubItems[1].Text;
            txtORDER_ID.Text = lvTableType.SelectedItems[0].SubItems[2].Text;
            txtOTHER_LANGUAGE_DESCR.Text = lvTableType.SelectedItems[0].SubItems[3].Text; ;
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

        private void tlbRefresh_Click(object sender, EventArgs e)
        {
            InitTableType();
        }

        private void frmTableType_KeyUp(object sender, KeyEventArgs e)
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
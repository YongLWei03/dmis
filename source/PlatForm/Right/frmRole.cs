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
    public partial class frmRole : Form
    {

        private string _sql;

        public frmRole()
        {
            InitializeComponent();
        }

        private void frmRole_Load(object sender, EventArgs e)
        {
            initRole();
        }

        private void initRole()
        {
            lsvRole.Items.Clear();
            ListViewItem lv;
            DataTable dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,DESCR,OTHER_LANGUAGE_DESCR from DMIS_SYS_ROLE order by ID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(dt.Rows[i][0].ToString());

                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != null) lv.SubItems.Add(dt.Rows[i][j].ToString());
                }
                lsvRole.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lsvRole, Color.SkyBlue, Color.Lime);
        }

        private void tlbAdd_Click(object sender, EventArgs e)
        {
            txtID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_ROLE", "ID").ToString();
            txtNAME.Text = "";
            txtDESCR.Text = "";
            txtOTHER_LANGUAGE_DESCR.Text = "";
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                return;
            }

            FieldPara[] field = {new FieldPara("ID",FieldType.Int,txtID.Text),
								 new FieldPara("NAME",FieldType.String,txtNAME.Text),
	                             new FieldPara("DESCR",FieldType.String,txtDESCR.Text),
                new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtOTHER_LANGUAGE_DESCR.Text)
                               };

            if (DBOpt.dbHelper.IsExist("DMIS_SYS_ROLE", "ID=" + txtID.Text))
            {
                WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
                _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_ROLE", field, where);
            }
            else
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_ROLE", field);

            DBOpt.dbHelper.ExecuteSql(_sql);
            initRole();
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (lsvRole.SelectedItems.Count < 1)
            {
                MessageBox.Show(this, Main.Properties.Resources.SelectDeleteItem);//"请先选择要删除的岗位!"
                return;
            }
            else
            {
                if (lsvRole.SelectedItems[0].Text=="0")
                {
                    //MessageBox.Show(this, "不允许删除系统管理员的岗位!");
                    return;
                }

                if (DBOpt.dbHelper.IsExist("DMIS_SYS_MEMBER_ROLE", "ROLE_ID=" + lsvRole.SelectedItems[0].Text))
                {
                    MessageBox.Show(this,Main.Properties.Resources.RelatdItemsNoDelete );//"已经有人分配了此岗位，不允许要删除!"
                    return;
                }
                if (MessageBox.Show(Main.Properties.Resources.DeleteBeforeConfirm, Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            }

            _sql = "delete from DMIS_SYS_ROLE where ID=" + lsvRole.SelectedItems[0].Text;
            DBOpt.dbHelper.ExecuteSql(_sql);
            initRole();

            txtID.Text ="";
            txtNAME.Text = "";
            txtDESCR.Text = "";
            txtOTHER_LANGUAGE_DESCR.Text = "";
        }

        private void lsvRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvRole.SelectedItems.Count != 1) return;

            txtID.Text = lsvRole.SelectedItems[0].Text;
            txtNAME.Text = lsvRole.SelectedItems[0].SubItems[1].Text;
            txtDESCR.Text = lsvRole.SelectedItems[0].SubItems[2].Text;
            txtOTHER_LANGUAGE_DESCR.Text = lsvRole.SelectedItems[0].SubItems[3].Text;
            if (txtID.Text == "0")
                txtNAME.ReadOnly = true;
            else
                txtNAME.ReadOnly = false;
        }

        private void tlbRefresh_Click(object sender, EventArgs e)
        {
            initRole();
        }




    }
}
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
    public partial class frmDepartType : Form
    {
        string _sql;

        public frmDepartType()
        {
            InitializeComponent();
        }

        private void frmDepartType_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            lsvDepartType.Items.Clear();
            ListViewItem lv;
            DataTable dt = DBOpt.dbHelper.GetDataTable("select ID,NAME from DMIS_SYS_DEPART_TYPE order by ID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(dt.Rows[i][0].ToString());
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != null) lv.SubItems.Add(dt.Rows[i][j].ToString());
                }
                lsvDepartType.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lsvDepartType, Color.Gray, Color.Lime);
        }

         private void tlbAdd_Click(object sender, EventArgs e)
        {
            txtID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_DEPART_TYPE", "ID").ToString();
            txtNAME.Text = "";
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (lsvDepartType.SelectedItems.Count < 1)
            {
                //MessageBox.Show(this, "请先选择要删除的部门类型!");
                MessageBox.Show(this, Main.Properties.Resources.SelectDeleteItem);
                return;
            }
            else
            {
                if (MessageBox.Show(Main.Properties.Resources.DeleteBeforeConfirm + lsvDepartType.SelectedItems[0].SubItems[1].Text + "?", Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            }

            _sql = "delete from DMIS_SYS_DEPART_TYPE where ID=" + lsvDepartType.SelectedItems[0].Text;
            DBOpt.dbHelper.ExecuteSql(_sql);
            Init();
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtNAME.Text.Trim()=="")
            {
                return;
            }

            FieldPara[] field = {new FieldPara("ID",FieldType.Int,txtID.Text),
								 new FieldPara("NAME",FieldType.String,txtNAME.Text)
                               };

            if (DBOpt.dbHelper.IsExist("DMIS_SYS_DEPART_TYPE", "ID=" + txtID.Text))
            {
                WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
                _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_DEPART_TYPE", field, where);
            }
            else
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_DEPART_TYPE", field);

            DBOpt.dbHelper.ExecuteSql(_sql);
            Init();
        }

        private void lvDepartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDepartType.SelectedItems.Count != 1) return;

            txtID.Text = lsvDepartType.SelectedItems[0].Text;
            txtNAME.Text = lsvDepartType.SelectedItems[0].SubItems[1].Text;
        }

 
    }
}
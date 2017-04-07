using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using PlatForm.Functions;

namespace PlatForm.DmisReport
{
    public partial class frmReportType : Form
    {
        string _sql;

        public frmReportType()
        {
            InitializeComponent();
        }


        private void frmReportType_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            lsvReportType.Items.Clear();
            ListViewItem lv;
            DataTable dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,OTHER_LANGUAGE_DESCR,ORDER_ID from DMIS_SYS_REPORT_TYPE order by ORDER_ID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(dt.Rows[i][0].ToString());
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != null) lv.SubItems.Add(dt.Rows[i][j].ToString());
                }
                lsvReportType.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lsvReportType, Color.Gray, Color.Lime);
        }


        private void tlbAdd_Click(object sender, EventArgs e)
        {
            txtID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_REPORT_TYPE", "ID").ToString();
            txtNAME.Text = "";
            txtOTHER_LANGUAGE_DESCR.Text = "";
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (lsvReportType.SelectedItems.Count < 1)
            {
                //MessageBox.Show(this, "请先选择要删除的报表类型!");
                return;
            }
            else
            {
                if (DBOpt.dbHelper.IsExist("DMIS_SYS_REPORT", "TYPE_ID=" + lsvReportType.SelectedItems[0].Text))
                {
                    //MessageBox.Show(this, "此分类下还有报表，不允许删除!");
                    return;
                }
                if (MessageBox.Show(Reports.Properties.Resources.DeleteBeforeConfirm, Reports.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            }

            _sql = "delete from DMIS_SYS_REPORT_TYPE where ID=" + lsvReportType.SelectedItems[0].Text;
            DBOpt.dbHelper.ExecuteSql(_sql);
            Init();
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;
            if (txtORDER_ID.Text.Trim() != "")
            {
                byte order;
               
                if (!byte.TryParse(txtORDER_ID.Text.Trim(), out order))
                {

                    MessageBox.Show(this, Reports.Properties.Resources.NumericalValeError);
                    return;
                }
            }


            FieldPara[] field = {new FieldPara("ID",FieldType.Int,txtID.Text),
								 new FieldPara("NAME",FieldType.String,txtNAME.Text),
                                 new FieldPara("ORDER_ID",FieldType.Int,txtORDER_ID.Text),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtOTHER_LANGUAGE_DESCR.Text)
                               };

            if (DBOpt.dbHelper.IsExist("DMIS_SYS_REPORT_TYPE", "ID=" + txtID.Text))
            {
                WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
                _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_REPORT_TYPE", field, where);
            }
            else
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_REPORT_TYPE", field);

            DBOpt.dbHelper.ExecuteSql(_sql);
            Init();
        }

        private void lsvDepartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvReportType.SelectedItems.Count != 1) return;

            txtID.Text = lsvReportType.SelectedItems[0].Text;
            txtNAME.Text = lsvReportType.SelectedItems[0].SubItems[1].Text;
            txtOTHER_LANGUAGE_DESCR.Text = lsvReportType.SelectedItems[0].SubItems[2].Text;
            txtORDER_ID.Text = lsvReportType.SelectedItems[0].SubItems[3].Text;
        }

        private void txtORDER_ID_Validating(object sender, CancelEventArgs e)
        {
            int order;
            if (!(int.TryParse(txtORDER_ID.Text, out order) || txtORDER_ID.Text == ""))
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



    }
}
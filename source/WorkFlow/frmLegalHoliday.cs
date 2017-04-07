using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using PlatForm.DBUtility;

namespace PlatForm.WorkFlow
{
    public partial class frmLegalHoliday : Form
    {
        private string _sql;

        public frmLegalHoliday()
        {
            InitializeComponent();
        }

        private void frmLegalHoliday_Load(object sender, EventArgs e)
        {
            //填充年份
            int year = DateTime.Now.Year;
            for (int i = -10; i < 10; i++)
                cbbYear.Items.Add(year + i);
            cbbYear.Text = year.ToString();
            cbbYear_SelectedIndexChanged(null, null);
        }

        private void tlbAdd_Click(object sender, EventArgs e)
        {
            if (dtpHOLIDAY_DATE.Value == null)
            {
                //MessageBox.Show("请先选择要添加日期！");
                return;
            }
            object obj = DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_WK_LEGAL_HOLIDAY where to_char(HOLIDAY_DATE,'YYYYMMDD')='"+
                        dtpHOLIDAY_DATE.Value.ToString("yyyyMMdd")+"'");
            if (obj.ToString() == "1")
            {
                //MessageBox.Show(dtpHOLIDAY_DATE.Value.ToString("yyyy年MM月dd日")+"已经是节假日，不允许再添加！");
                return;
            }
            uint maxTid;
            maxTid = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WK_LEGAL_HOLIDAY", "TID");
            _sql = "insert into DMIS_SYS_WK_LEGAL_HOLIDAY(TID,HOLIDAY_DATE,NOTE) values(" +
                    maxTid + ",TO_DATE('" + dtpHOLIDAY_DATE.Value.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),'" + txtNOTE.Text + "')";
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0) cbbYear_SelectedIndexChanged(null, null);
        }

        private void tlbDel_Click(object sender, EventArgs e)
        {
            if (lsvRestDay.SelectedItems.Count < 1)
            {
                //MessageBox.Show("请先选择要删除的节假日！");
                return;
            }
            //if (MessageBox.Show("是否要删除节假日：" + lsvRestDay.SelectedItems[0].Text + "?", "注意!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            string tid = lsvRestDay.SelectedItems[0].SubItems[2].Text;
            _sql = "delete from DMIS_SYS_WK_LEGAL_HOLIDAY where TID="+tid;
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0) cbbYear_SelectedIndexChanged(null, null);
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtTID.Text == "") return;
            _sql = "update DMIS_SYS_WK_LEGAL_HOLIDAY set NOTE='" + txtNOTE.Text + "' where TID=" + txtTID.Text;
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0) cbbYear_SelectedIndexChanged(null, null);
        }

        private void cbbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = cbbYear.Text + "-1-1";
            DateTime d;
            if (!DateTime.TryParse(temp, out d))
            {
                //MessageBox.Show("年份无效!");
                return;
            }

            lsvRestDay.Items.Clear();
            ListViewItem lv;
            DataTable dt = DBOpt.dbHelper.GetDataTable("select HOLIDAY_DATE,NOTE,TID" +
                " from DMIS_SYS_WK_LEGAL_HOLIDAY where to_char(HOLIDAY_DATE,'YYYY')='" + cbbYear.Text + "' order by HOLIDAY_DATE");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(Convert.ToDateTime(dt.Rows[i][0]).ToString("yyyy-MM-dd"));
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (!(dt.Rows[i][j] is DBNull))
                        lv.SubItems.Add(dt.Rows[i][j].ToString());
                    else
                        lv.SubItems.Add("");

                }
                lsvRestDay.Items.Add(lv);
            }
        }

        private void lsvRestDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvRestDay.SelectedItems.Count != 1) return;
            dtpHOLIDAY_DATE.Value = Convert.ToDateTime(lsvRestDay.SelectedItems[0].Text);
            txtNOTE.Text = lsvRestDay.SelectedItems[0].SubItems[1].Text;
            txtTID.Text = lsvRestDay.SelectedItems[0].SubItems[2].Text;
        }




    }
}
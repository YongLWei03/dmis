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
    public partial class frmLogSearch : Form
    {
        private string sql;
        private DataTable _dt;

        public frmLogSearch()
        {
            InitializeComponent();
        }

        private void frmLogSearch_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = Convert.ToDateTime(DateTime.Now.Year.ToString()+"-"+ DateTime.Now.Month.ToString()+"-1");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            DateTime dtStart = dtpStartDate.Value;
            DateTime dtEnd = dtpEndDate.Value;
            if (dtStart > dtEnd)
            {
                MessageBox.Show("起止日期不能大于终止日期");
                return;
            }
            TimeSpan ts = dtEnd - dtStart;
            if (ts.Days > 31)
            {
                MessageBox.Show("时间范围不能超过31天！");
                return;
            }
            lsvLog.Items.Clear();
            ListViewItem lv;

            sql = "select OPT_TIME,MEMBER_NAME,MEMBER_ID,IP,LOG_TYPE,STATE,CONTENT from DMIS_SYS_LOG where convert(char(8),OPT_TIME,112)>='" +
                    dtStart.ToString("yyyyMMdd") + "' and convert(char(8),OPT_TIME,112)<='" + dtEnd.ToString("yyyyMMdd") + "' order by OPT_TIME desc";

            _dt = DBOpt.dbHelper.GetDataTable(sql);
            
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                lv = new ListViewItem(_dt.Rows[i][0].ToString());

                for (int j = 1; j < _dt.Columns.Count; j++)
                {
                    if (_dt.Rows[i][j] != null) lv.SubItems.Add(_dt.Rows[i][j].ToString());
                }
                lsvLog.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lsvLog, Color.SkyBlue, Color.Lime);
        }

       

    }
}
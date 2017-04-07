using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PlatForm.DBUtility;

namespace PlatForm
{
    public partial class frmSelectRelatingTableColumn : Form
    {
        string _sql;
        public string values="";

        public frmSelectRelatingTableColumn()
        {
            InitializeComponent();
        }

        public frmSelectRelatingTableColumn(string value)
        {
            InitializeComponent();
            values = value;
        }


        private void frmSelectRelatingTableColumn_Load(object sender, EventArgs e)
        {

            _sql = "select ID,NAME from DMIS_SYS_TABLES order by NAME";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            cbbTable.DisplayMember = "NAME";
            cbbTable.ValueMember = "ID";
            cbbTable.DataSource = dt;

            if (values.Length > 0)
            {
                string[] arr = values.Split('/');
                string tableID = DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + arr[0]+"'").ToString();
                cbbTable.SelectedIndex = cbbTable.FindStringExact(arr[0]);
                _sql = "select NAME from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID + " order by NAME";
                DataTable dt2 = DBOpt.dbHelper.GetDataTable(_sql);

                cbbDescColumn.Items.Clear();
                cbbValueColumn.Items.Clear();
                cbbQueryColumn.Items.Clear();
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    cbbDescColumn.Items.Add(dt2.Rows[i][0]);
                    cbbValueColumn.Items.Add(dt2.Rows[i][0]);
                    cbbQueryColumn.Items.Add(dt2.Rows[i][0]);
                }
                cbbDescColumn.SelectedIndex = cbbDescColumn.FindStringExact(arr[1]);
                cbbValueColumn.SelectedIndex = cbbDescColumn.FindStringExact(arr[2]);
                cbbQueryColumn.SelectedIndex = cbbDescColumn.FindStringExact(arr[3]);
            }
        }

        private void cbbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTable.SelectedIndex < 0) return;
            _sql = "select NAME from DMIS_SYS_COLUMNS where TABLE_ID=" + cbbTable.SelectedValue+ " order by NAME";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);

            cbbDescColumn.Items.Clear();
            cbbValueColumn.Items.Clear();
            cbbQueryColumn.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbbDescColumn.Items.Add(dt.Rows[i][0]);
                cbbValueColumn.Items.Add(dt.Rows[i][0]);
                cbbQueryColumn.Items.Add(dt.Rows[i][0]);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbbTable.SelectedItem == null)
            {
                MessageBox.Show("请先选择数据库表名！");
                return;
            }
            if (cbbDescColumn.SelectedItem == null)
            {
                MessageBox.Show("请先选择描述列！");
                return;
            }
            if (cbbValueColumn.SelectedItem == null)
            {
                MessageBox.Show("请先选择数据列！");
                return;
            }
            if (cbbQueryColumn.SelectedItem == null)
            {
                MessageBox.Show("请先选择检索列！");
                return;
            }
            values = cbbTable.Text + "/" + cbbDescColumn.Text + "/" + cbbValueColumn.Text + "/" + cbbQueryColumn.Text;
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


    
    }
}
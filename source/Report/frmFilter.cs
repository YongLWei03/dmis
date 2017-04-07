using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PlatForm.DBUtility;
using System.Data.Common;

namespace PlatForm.DmisReport
{
    public partial class frmFilter : Form
    {
        public string returnString;
        public string tableName;
        public string reportID;
        string _sql;

        public frmFilter()
        {
            InitializeComponent();
        }

        private void frmFilter_Load(object sender, EventArgs e)
        {
            if (returnString.Trim() != "")
            {
                string[] filters;
                string[] filter;
                filters = returnString.Split(';');

                for (int i = 0; i < filters.Length; i++)
                {
                    filter = filters[i].Split('@');
                    ListViewItem li = new ListViewItem();
                    li.Text = Convert.ToString(i + 1);
                    li.SubItems.Add(filter[0]);
                    li.SubItems.Add(filter[1]);
                    li.SubItems.Add(filter[2]);
                    li.SubItems.Add(filter[3]);
                    lsvFilter.Items.Add(li);
                }
            }
            initColumns();
            initValue();
        }

        private void initColumns()
        {
            string owner, table, tableID;

            owner = tableName.Substring(0, tableName.IndexOf('.'));
            table = tableName.Substring(tableName.LastIndexOf('.') + 1);
            tableID = DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where OWNER='" + owner + "' and NAME='" + table + "'" + " order by ORDER_ID").ToString();

            _sql = "select NAME from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID + " order by ORDER_ID";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
                cbbColumn.Items.Add(dr[0]);
            dr.Close();
        }

        private void initValue()
        {
            _sql = "select ID,DESCR from DMIS_SYS_REPORT_PARA where REPORT_ID=" + reportID+" order by ORDER_ID";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                cbbValue.Items.Add(":"+dr[0].ToString());
            }
            dr.Close();
            //如何查找所选中项
            //DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            //cbbValue.DataSource = dt;
            //cbbValue.DisplayMember = "DESCR";
            //cbbValue.ValueMember = "ID";
        }

        private void lsvFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvFilter.SelectedItems.Count < 1) return;

            cbbColumn.Text = lsvFilter.SelectedItems[0].SubItems[1].Text;
            cbbOP.Text = lsvFilter.SelectedItems[0].SubItems[2].Text;
            cbbValue.Text = lsvFilter.SelectedItems[0].SubItems[3].Text;
            cbbLogical.Text = lsvFilter.SelectedItems[0].SubItems[4].Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //"列名不能为空"  "运算符不能为空"  "值或参数不能为空"   "逻辑符不能为空"
            if (cbbColumn.Text=="" || cbbOP.Text == "" || cbbValue.Text == "" || cbbLogical.Text == "")
            {
                MessageBox.Show(Reports.Properties.Resources.NoEmpty, Reports.Properties.Resources.Note, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            int xh;
            if (lsvFilter.Items.Count == 0)
                xh = 1;
            else
            {
                xh = Convert.ToInt16(lsvFilter.Items[lsvFilter.Items.Count - 1].Text) + 1;
            }
            ListViewItem li = new ListViewItem();
            li.Text = xh.ToString();
            li.SubItems.Add(cbbColumn.Text);
            li.SubItems.Add(cbbOP.Text);
            li.SubItems.Add(cbbValue.Text);
            li.SubItems.Add(cbbLogical.Text);
            lsvFilter.Items.Add(li);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsvFilter.SelectedItems.Count != 1) return;

            lsvFilter.Items.Remove(lsvFilter.SelectedItems[0]);
            setXH();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < lsvFilter.Items.Count; i++)
            {
                if(i+1!=lsvFilter.Items.Count)
                    str.Append(lsvFilter.Items[i].SubItems[1].Text + "@" + lsvFilter.Items[i].SubItems[2].Text + "@" + lsvFilter.Items[i].SubItems[3].Text + "@" + lsvFilter.Items[i].SubItems[4].Text + ";");
                else
                    str.Append(lsvFilter.Items[i].SubItems[1].Text + "@" + lsvFilter.Items[i].SubItems[2].Text + "@" + lsvFilter.Items[i].SubItems[3].Text + "@" );
            }

            if (str.Length > 0)
                returnString = str.ToString();
            else
                returnString = "";
        }

        private void setXH()
        {
            for (int i = 0; i < lsvFilter.Items.Count; i++)
            {
                lsvFilter.Items[i].Text = (i + 1).ToString();
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PlatForm.DBUtility;
using System.Data.Common;

namespace PlatForm
{
    public partial class frmSetOrder : Form
    {
        public string returnString;
        public string tableID;
        string _sql;

        public frmSetOrder()
        {
            InitializeComponent();
        }

        private void frmSetOrder_Load(object sender, EventArgs e)
        {
            initColumns();
            cbbOrder.SelectedIndex = 0;
        }

        private void initColumns()
        {
            _sql = "select NAME from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID + " order by ORDER_ID";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                cbbColumn.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void lsvOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvOrder.SelectedItems.Count < 1) return;

            cbbColumn.Text = lsvOrder.SelectedItems[0].SubItems[1].Text;
            cbbOrder.Text = lsvOrder.SelectedItems[0].SubItems[2].Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbbColumn.Text == "" || cbbOrder.Text == "") return;
            for (int i = 0; i < lsvOrder.Items.Count; i++)
            {
                if (cbbColumn.Text == lsvOrder.Items[i].SubItems[1].Text)
                {
                    //MessageBox.Show("排序中已经存在此列，不允许再添加！", "提示");
                    return;
                }
            }

            int xh;
            if (lsvOrder.Items.Count == 0)
                xh = 1;
            else
            {
                xh = Convert.ToInt16(lsvOrder.Items[lsvOrder.Items.Count - 1].Text) + 1;
            }
            ListViewItem li = new ListViewItem();
            li.Text = xh.ToString();
            li.SubItems.Add(cbbColumn.Text);
            li.SubItems.Add(cbbOrder.Text);
            lsvOrder.Items.Add(li);
        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lsvOrder.SelectedItems.Count != 1) return;

            lsvOrder.Items.Remove(lsvOrder.SelectedItems[0]);
            setXH();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (lsvOrder.SelectedItems.Count != 1) return;
            if (lsvOrder.SelectedIndices[0] == 0) return;

            for (int i = 0; i < lsvOrder.Items.Count; i++)
            {
                if (cbbColumn.Text == lsvOrder.Items[i].SubItems[1].Text)
                {
                    //MessageBox.Show("排序中已经存在此列，不允许再添加！", "提示");
                    return;
                }
            }

            int index = lsvOrder.SelectedIndices[0];
            int xh;

            if (lsvOrder.Items.Count == 0)
                xh = 1;
            else
                xh = Convert.ToInt16(lsvOrder.Items[lsvOrder.Items.Count - 1].Text) + 1;

            ListViewItem li = new ListViewItem();
            li.Text = xh.ToString();
            li.SubItems.Add(cbbColumn.Text);
            li.SubItems.Add(cbbOrder.Text);
            lsvOrder.Items.Insert(index, li);
            setXH();
        }


        private void setXH()
        {
            for (int i = 0; i < lsvOrder.Items.Count; i++)
            {
                lsvOrder.Items[i].Text = (i + 1).ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < lsvOrder.Items.Count; i++)
            {
                str.Append(lsvOrder.Items[i].SubItems[1].Text + " " + lsvOrder.Items[i].SubItems[2].Text+",");
            }
            returnString = str.ToString().Substring(0, str.ToString().Length - 1);
        }

    }
}
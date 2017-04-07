using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using PlatForm.DBUtility;

namespace PlatForm
{
    public partial class frmSelectTableIDs : Form
    {
        string _sql;
        public string Ids;

        public frmSelectTableIDs(string ids)
        {
            Ids = ","+ids+",";    //为了判断方便，前后先加个,
            InitializeComponent();
        }

        private void frmSelectTableIDs_Load(object sender, EventArgs e)
        {
            tvTableType.Nodes.Clear();

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select ID,DESCR from DMIS_SYS_TABLE_TYPE order by ORDER_ID";
            else
                _sql = "select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_TABLE_TYPE order by ORDER_ID";

            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            if (dt==null || dt.Rows.Count < 1) return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode td = new TreeNode();
                td.Text = dt.Rows[i][1].ToString();
                td.Tag = dt.Rows[i][0].ToString();
                tvTableType.Nodes.Add(td);
            }
            tvTableType.SelectedNode = tvTableType.Nodes[0];

            //_sql = "select ID,NAME,DESCR,OWNER from DMIS_SYS_TABLES b where TYPE_ID=" + tvTableType.SelectedNode.Tag.ToString() + " order by ORDER_ID";
            //dt = DBOpt.dbHelper.GetDataTable(_sql);
            //ListViewItem lv;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    lv = new ListViewItem(dt.Rows[i][0].ToString());
            //    for (int j = 1; j < dt.Columns.Count; j++)
            //    {
            //        if (dt.Rows[i][j] is System.DBNull)
            //            lv.SubItems.Add("");
            //        else
            //            lv.SubItems.Add(dt.Rows[i][j].ToString());
            //    }
            //    if (Ids.IndexOf(","+lv.Text+",") >= 0) lv.Checked = true;
            //    lsvTables.Items.Add(lv);
            //}
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string temp="";

            for (int i = 0; i < lsvTables.Items.Count; i++)
            {
                if (lsvTables.Items[i].Checked)
                    temp = temp + lsvTables.Items[i].Text+",";
            }
            if (temp.Length > 0)
                Ids = temp.Substring(0, temp.Length - 1);
            else
                Ids = "";
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tvTableType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvTableType.SelectedNode == null) return;
            lsvTables.Items.Clear();

            _sql = "select ID,NAME,DESCR,OTHER_LANGUAGE_DESCR,OWNER from DMIS_SYS_TABLES b where TYPE_ID=" + tvTableType.SelectedNode.Tag.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            ListViewItem lv;
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
                if (Ids.IndexOf("," + lv.Text + ",") >= 0) lv.Checked = true;
                lsvTables.Items.Add(lv);
            }
        }

    }
}
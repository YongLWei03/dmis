using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using PlatForm.DBUtility;
using System.Globalization;


namespace PlatForm
{
    public partial class frmRolePurview : Form
    {
        DataTable _dt;
        string _sql;

        public frmRolePurview()
        {
            InitializeComponent();
        }

        private void frmRolePurview_Load(object sender, EventArgs e)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");
            else
                _dt = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");
            BuildTree(null);

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select ID,NAME from DMIS_SYS_ROLE order by ID";
            else
                _sql = "select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_ROLE order by ID";

            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                TreeNode td = new TreeNode();
                td.Text = dr[1].ToString();
                td.Tag = dr[0];
                trvRole.Nodes.Add(td);
            }
            dr.Close();
        }

        private void BuildTree(TreeNode tn)
        {
            int i;
            // 空节点时创建根节点，父ID为NULL的当作根节点
            if (tn == null)
            {
                trvTreeMenu.Nodes.Clear();
                for (i = 0; i < _dt.Rows.Count; i++)
                {
                    if (_dt.Rows[i]["PARENT_ID"].ToString() == "0")
                    {
                        TreeNode tmp = new TreeNode(_dt.Rows[i][1].ToString());
                        tmp.Tag = Int32.Parse(_dt.Rows[i][0].ToString());
                        trvTreeMenu.Nodes.Add(tmp);
                    }
                }
                // 循环递归创建树
                for (i = 0; i < trvTreeMenu.Nodes.Count; i++)
                {
                    BuildTree(trvTreeMenu.Nodes[i]);
                }
            }
            else // 节点非空为递归调用
            {
                for (i = 0; i < _dt.Rows.Count; i++)
                {
                    if (tn.Tag.ToString() == _dt.Rows[i]["PARENT_ID"].ToString())
                    {
                        TreeNode tmp = new TreeNode(_dt.Rows[i][1].ToString());
                        tmp.Tag = Int32.Parse(_dt.Rows[i][0].ToString());
                        tn.Nodes.Add(tmp);
                    }
                }
                for (i = 0; i < tn.Nodes.Count; i++)
                {
                    BuildTree(tn.Nodes[i]);
                }
            }
        }


        private void trvTreeMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            initListView();
            setListviewCheck();
        }

  /// <summary>
  /// 初始化权限列表
  /// </summary>
        private void initListView()
        {
            if (trvTreeMenu.SelectedNode == null) return;
            lsvPurview.Items.Clear();

            _sql = "select ID,DESCR,WEB_FILE,CONTROL_NAME,CONTROL_PROPERTY,CONTROL_VALUE,ORDER_ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_PURVIEW where MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " order by ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem lv = new ListViewItem(dt.Rows[i][0].ToString());
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] is System.DBNull)
                        lv.SubItems.Add("");
                    else
                        lv.SubItems.Add(dt.Rows[i][j].ToString());
                }
                lsvPurview.Items.Add(lv);
            }
        }

        /// <summary>
        /// 根据功能和角色设置权限Check
        /// </summary>
        private void setListviewCheck()
        { 
            if (trvTreeMenu.SelectedNode == null) return;
            if (trvRole.SelectedNode == null) return;
            if (lsvPurview.Items.Count < 1) return;

            int k = 0;
            _sql = "select PURVIEW_ID from DMIS_SYS_ROLE_PURVIEW where ROLE_ID=" + trvRole.SelectedNode.Tag.ToString() + " and MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " order by PURVIEW_ID";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                for (int i = k; i < lsvPurview.Items.Count; i++) 
                {
                    if (dr[0].ToString() == lsvPurview.Items[i].Text)
                    {
                        lsvPurview.Items[i].Checked = true;
                        k = i+1;
                        break;
                    }
                    else
                    {
                        lsvPurview.Items[i].Checked = false;
                    }
                }
            }
            dr.Close();
        }


        private void trvRole_AfterSelect(object sender, TreeViewEventArgs e)
        {
            initListView();
            setListviewCheck();
        }


        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (trvTreeMenu.SelectedNode == null) return;
            if (trvRole.SelectedNode == null) return;
            if (lsvPurview.Items.Count < 1) return;

            _sql = "delete from DMIS_SYS_ROLE_PURVIEW where ROLE_ID=" + trvRole.SelectedNode.Tag.ToString() + " and MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString();
            DBOpt.dbHelper.ExecuteSql(_sql);

            for (int i = 0; i < lsvPurview.Items.Count; i++)
            {
                if (lsvPurview.Items[i].Checked)
                {
                    _sql = "insert into DMIS_SYS_ROLE_PURVIEW(ROLE_ID,MODULE_ID,PURVIEW_ID) values(" + trvRole.SelectedNode.Tag.ToString() +
                            "," + trvTreeMenu.SelectedNode.Tag.ToString() + "," + lsvPurview.Items[i].Text + ")";
                    DBOpt.dbHelper.ExecuteSql(_sql);
                }
            }
        }

        private void tlbSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lsvPurview.Items.Count; i++)
            {
                lsvPurview.Items[i].Checked = true;
            }
        }

        private void tlbUnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lsvPurview.Items.Count; i++)
            {
                lsvPurview.Items[i].Checked = false;
            }
        }

        private void tlbRefresh_Click(object sender, EventArgs e)
        {
            initListView();
            setListviewCheck();
        }






    }


}
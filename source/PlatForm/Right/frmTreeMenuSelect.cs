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
    public partial class frmTreeMenuSelect : Form
    {
        DataTable _dt;
        string _sql;
        public string selectedMemuID;

        public frmTreeMenuSelect()
        {
            InitializeComponent();
        }

        private void frmTreeMenuSelect_Load(object sender, EventArgs e)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");
            else
                _dt = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");

            BuildTree(null);

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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (trvTreeMenu.SelectedNode == null) return;
            if (trvTreeMenu.SelectedNode.Tag.ToString() == selectedMemuID)
            {
                //MessageBox.Show("父节点不能是同一节点！");
                return;
            }
            _sql = "update DMIS_SYS_TREEMENU set PARENT_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " where ID=" + selectedMemuID;
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                this.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }





    }
}
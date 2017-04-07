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
    public partial class frmDepartSelect : Form
    {
        DataTable _dt;
        string _sql;
        public string selectedMemuID;

        public frmDepartSelect()
        {
            InitializeComponent();
        }

        private void frmDepartSelect_Load(object sender, EventArgs e)
        {
            _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,superior_id from DMIS_SYS_DEPART order by ORDER_ID");
            BuildTree(null);
        }

        private void BuildTree(TreeNode tn)
        {
            int i;
            // �սڵ�ʱ�������ڵ㣬��IDΪNULL�ĵ������ڵ�
            if (tn == null)
            {
                trvTreeMenu.Nodes.Clear();
                for (i = 0; i < _dt.Rows.Count; i++)
                {
                    if (_dt.Rows[i]["superior_id"].ToString() == "0")
                    {
                        TreeNode tmp = new TreeNode(_dt.Rows[i]["name"].ToString());
                        tmp.Tag = Int32.Parse(_dt.Rows[i]["id"].ToString());
                        trvTreeMenu.Nodes.Add(tmp);
                    }
                }
                // ѭ���ݹ鴴����
                for (i = 0; i < trvTreeMenu.Nodes.Count; i++)
                {
                    BuildTree(trvTreeMenu.Nodes[i]);
                }
            }
            else // �ڵ�ǿ�Ϊ�ݹ����
            {
                for (i = 0; i < _dt.Rows.Count; i++)
                {
                    if (tn.Tag.ToString() == _dt.Rows[i]["superior_id"].ToString())
                    {
                        TreeNode tmp = new TreeNode(_dt.Rows[i]["name"].ToString());
                        tmp.Tag = Int32.Parse(_dt.Rows[i]["id"].ToString());
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
                //MessageBox.Show("���ڵ㲻����ͬһ�ڵ㣡");
                return;
            }
            _sql = "update DMIS_SYS_MEMBER set DEPART_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " where ID=" + selectedMemuID;
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
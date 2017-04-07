using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PlatForm.DBUtility;
using PlatForm.Functions;

namespace PlatForm.WorkFlow
{
    public partial class frmSelectRole : Form
    {
        public int NodeID;
        public string roles;
        private string _sql;
        public frmSelectRole()
        {
            InitializeComponent();
        }
        private void frmSelectRole_Load(object sender, EventArgs e)
        {
            //显示此节点哪些岗位能操作
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select a.name from DMIS_SYS_ROLE a,DMIS_SYS_RIGHTS b where a.ID=b.F_ROLENO and b.f_catgory='流程角色' and b.f_foreignkey=" + NodeID;
            else
                _sql = "select a.OTHER_LANGUAGE_DESCR from DMIS_SYS_ROLE a,DMIS_SYS_RIGHTS b where a.ID=b.F_ROLENO and b.f_catgory='流程角色' and b.f_foreignkey=" + NodeID;
            
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
                clbRoles.Items.Add(dt.Rows[i][0].ToString(), false);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (clbRoles.CheckedItems.Count < 1)
            {
                //MessageBox.Show("至少要选择一个岗位！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            string temp, role_id;
            temp = "";
            for (int i = 0; i < clbRoles.CheckedItems.Count; i++)
            {
                _sql = "select ID from DMIS_SYS_ROLE where NAME='" + clbRoles.CheckedItems[i].ToString() + "'";
                role_id = DBOpt.dbHelper.ExecuteScalar(_sql).ToString();
                temp = temp+role_id + ",";
            }
            roles = temp.Substring(0, temp.Length - 1);
            this.DialogResult = DialogResult.OK;
        }



    }
}
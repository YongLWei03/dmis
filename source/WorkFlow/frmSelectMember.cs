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
    public partial class frmSelectMember : Form
    {
        public int NodeID;
        public string Names;
        private string _sql;

        public frmSelectMember()
        {
            InitializeComponent();
        }

        private void frmSelectMember_Load(object sender, EventArgs e)
        {
            //��ʾ�˽ڵ���Щ��Ա�ܲ���
            _sql = "select distinct b.NAME from DMIS_SYS_RIGHTS a,DMIS_SYS_MEMBER b,dmis_sys_member_role c where c.ROLE_ID=a.F_ROLENO and " +
                    " b.ID=c.MEMBER_ID and a.f_catgory='���̽�ɫ' and a.f_foreignkey=" + NodeID;
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
                clbMembers.Items.Add(dt.Rows[i][0].ToString(), false);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (clbMembers.CheckedItems.Count < 1)
            {
                //MessageBox.Show("����Ҫѡ��һ����Ա��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string temp, name;
            temp = "";
            for (int i = 0; i < clbMembers.CheckedItems.Count; i++)
            {
                _sql = "select code from DMIS_SYS_MEMBER where NAME='" + clbMembers.CheckedItems[i].ToString() + "'";
                name = DBOpt.dbHelper.ExecuteScalar(_sql).ToString();
                temp = temp + name + ",";
            }
            Names = temp.Substring(0, temp.Length - 1);
            this.DialogResult = DialogResult.OK;
        }
    }
}
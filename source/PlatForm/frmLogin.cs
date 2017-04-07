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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string _sql;
            int counts;
            if (txtCode.Text.Trim() == "")
            {
                //labMsg.Text = "���벻����Ϊ�գ�";
                return;
            }
            if (txtPwd.Text.Trim() == "")
            {
                //labMsg.Text = "�������Ϊ�գ�";
                return;
            }

            _sql = "select ID from DMIS_SYS_MEMBER where CODE='" + txtCode.Text.Trim() + "'";
            object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj == null)
            {
                //Log.InsertLog("��¼", "ʧ��", "δ���ҵ���Ա�ɣ�");
                //labMsg.Text = "δ���ҵ���Ա�ɣ�";
                return;
            }
            _sql = "select count(*) from DMIS_SYS_MEMBER_ROLE where ROLE_ID=0 and MEMBER_ID="+obj.ToString();
            counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql));
            if (counts == 0)
            {
                //labMsg.Text = "����ϵͳ����Ա���������¼��";
                //Log.InsertLog("��¼", "ʧ��", "�Դ��룺" + txtCode.Text.Trim() + "��¼ƽ̨ά������ʧ�ܣ�ԭ�򣺲���ϵͳ����Ա");
                return;
            }

            //�麣����Ŀ�����֤�����ϵĵ��ȹ���ϵͳ��һ��
            //string convertPwd="";
            //for (int i = 0; i < txtPwd.Text.Length; i++)
            //    convertPwd += (Convert.ToInt16(txtPwd.Text[i])).ToString("000");
            //_sql = "select count(*) from DMIS_SYS_MEMBER where CODE='" + txtCode.Text.Trim() + "' and password='" + convertPwd + "'";

            _sql = "select count(*) from DMIS_SYS_MEMBER where CODE='" + txtCode.Text.Trim() + "' and password='" + txtPwd.Text + "'";
            counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql));
            if (counts == 0)
            {
                labMsg.Visible = true;
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }
    }
}
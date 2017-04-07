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
                //labMsg.Text = "代码不允许为空！";
                return;
            }
            if (txtPwd.Text.Trim() == "")
            {
                //labMsg.Text = "口令不允许为空！";
                return;
            }

            _sql = "select ID from DMIS_SYS_MEMBER where CODE='" + txtCode.Text.Trim() + "'";
            object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj == null)
            {
                //Log.InsertLog("登录", "失败", "未能找到人员ＩＤ");
                //labMsg.Text = "未能找到人员ＩＤ";
                return;
            }
            _sql = "select count(*) from DMIS_SYS_MEMBER_ROLE where ROLE_ID=0 and MEMBER_ID="+obj.ToString();
            counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql));
            if (counts == 0)
            {
                //labMsg.Text = "不是系统管理员，不允许登录！";
                //Log.InsertLog("登录", "失败", "以代码：" + txtCode.Text.Trim() + "登录平台维护程序失败，原因：不是系统管理员");
                return;
            }

            //珠海特殊的口令验证，与老的调度管理系统相一致
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
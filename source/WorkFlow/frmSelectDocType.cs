using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using PlatForm.DBUtility;
using PlatForm.Functions;

namespace PlatForm.WorkFlow
{
    public partial class frmSelectDocType : Form
    {
        public int DocTypeID;
        public string DocTypeName;
        public int PackTypeID;
        private string _sql;
        public frmSelectDocType()
        {
            InitializeComponent();
        }

        private void frmSelectDocType_Load(object sender, EventArgs e)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select F_NO,F_NAME from dmis_sys_doctype where f_packtypeno=" + PackTypeID;
            else
                _sql = "select F_NO,OTHER_LANGUAGE_DESCR F_NAME from dmis_sys_doctype where f_packtypeno=" + PackTypeID;
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            cbbDocType.DataSource = dt;
            cbbDocType.DisplayMember = "F_NAME";
            cbbDocType.ValueMember = "F_NO";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbbDocType.SelectedItem==null)
            {
                //MessageBox.Show("要选择某一个文档！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DocTypeID = Convert.ToInt16(cbbDocType.SelectedValue);
            DocTypeName = cbbDocType.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
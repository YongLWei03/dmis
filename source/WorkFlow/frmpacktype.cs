using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.Functions;
using PlatForm.DBUtility;
using System.Data.Common;

namespace PlatForm.WorkFlow
{
    public partial class frmpacktype : Form
    {
 //       tabOperate pubfun = new tabOperate();
        public frmpacktype()
        {
            InitializeComponent();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "") {
                //MessageBox.Show("名称未填写，不能保存");
                tbName.Focus();
                return;
            }
            if (txtOTHER_LANGUAGE_DESCR.Text == "")
            {
                //MessageBox.Show("名称未填写，不能保存");
                txtOTHER_LANGUAGE_DESCR.Focus();
                return;
            }
            frmFlow.sPackName = tbName.Text;
            if (frmFlow.iPackNo > 0)
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.Append("UPDATE DMIS_SYS_PACKTYPE SET ");
                strBuild.Append(" F_NAME='" + ValueToField.StringToField(tbName.Text) + "',");
                strBuild.Append(" F_ISFLOW=" + Convert.ToInt16(cbflow.Checked) + ",");
                strBuild.Append(" F_ISCHECK=" + Convert.ToInt16(cbcheck.Checked) + ",");
                strBuild.Append(" F_ISARCHIEVE=" + Convert.ToInt16(cbarchive.Checked) + ",");
                strBuild.Append(" OTHER_LANGUAGE_DESCR='" + ValueToField.StringToField(txtOTHER_LANGUAGE_DESCR.Text) + "'");
                strBuild.Append(" WHERE F_NO=" + frmFlow.iPackNo);
                DBOpt.dbHelper.ExecuteSql(strBuild.ToString());
            }
            else
            {
               uint iMax = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PACKTYPE", "F_NO");

                StringBuilder strBuild = new StringBuilder();
                strBuild.Append("INSERT INTO DMIS_SYS_PACKTYPE(F_NO,F_NAME,F_ISFLOW,F_ISCHECK,F_ISARCHIEVE,OTHER_LANGUAGE_DESCR) VALUES(");
                strBuild.Append(iMax + ",");
                strBuild.Append("'"+ValueToField.StringToField(tbName.Text) + "',");   //ValueToField.StringToField函数已经去掉'
                strBuild.Append(Convert.ToInt16(cbflow.Checked) + ",");
                strBuild.Append(Convert.ToInt16(cbcheck.Checked) + ",");
                strBuild.Append(Convert.ToInt16(cbarchive.Checked)+",");
                strBuild.Append("'" + ValueToField.StringToField(txtOTHER_LANGUAGE_DESCR.Text) + "'");
                strBuild.Append( ")");
                DBOpt.dbHelper.ExecuteSql(strBuild.ToString());
                frmFlow.iPackNo = Convert.ToInt16(iMax);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void cbflow_CheckedChanged(object sender, EventArgs e)
        {
            if (cbflow.Checked == false)
            {
                cbcheck.Visible = false;
                cbarchive.Visible = false;
            }
            else 
            {
                cbcheck.Visible = true;
                cbarchive.Visible = true;          
            }
        }

        private void frmpacktype_Load(object sender, EventArgs e)
        {
            if (frmFlow.iPackNo >0)
            {
                DataTable dt1;
                dt1 = DBOpt.dbHelper.GetDataTable("SELECT * FROM DMIS_SYS_PACKTYPE WHERE F_NO=" + frmFlow.iPackNo);
                if (dt1.Rows.Count > 0)
                {
                    tbName.Text = FieldToValue.FieldToString(dt1.Rows[0]["F_NAME"]);
                    cbflow.Checked = FieldToValue.FieldToCheckBox(dt1.Rows[0]["F_ISFLOW"]);
                    cbcheck.Checked = FieldToValue.FieldToCheckBox(dt1.Rows[0]["F_ISCHECK"]);
                    cbarchive.Checked = FieldToValue.FieldToCheckBox(dt1.Rows[0]["F_ISARCHIEVE"]);
                    txtOTHER_LANGUAGE_DESCR.Text = FieldToValue.FieldToString(dt1.Rows[0]["OTHER_LANGUAGE_DESCR"]);
                }
            }
            if (cbflow.Checked == false) 
            {
                cbcheck.Visible = false;
                cbarchive.Visible = false;
            }
        }
    }
}
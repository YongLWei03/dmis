//**********************************************************************
//  业务文档维护  frmdoctype.cs
//**********************************************************************
//  维护各业务的文档，报表，数据表
//
//**********************************************************************
//   version      author      update-date     comment
//   v1.00                                     新规
//   v2.00        刘金平      2010/01/14       报表文本框改为下拉框，删除报表文件选择按钮
//**********************************************************************

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
using System.Globalization;

namespace PlatForm.WorkFlow
{
    public partial class frmdoctype : Form
    {
        
 //     tabOperate pubfun = new tabOperate();
        public frmdoctype()
        {
            InitializeComponent();
        }

        
        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "" )
            {
                //MessageBox.Show();
                txtName.Focus();
                return;
            }

            if(txtOTHER_LANGUAGE_DESCR.Text.Trim()=="")
            {
                //MessageBox.Show();
                txtOTHER_LANGUAGE_DESCR.Focus();
                return;
            }

            //DataTable dtTmp;
            //dtTmp = DBOpt.dbHelper.GetDataTable("select F_NO FROM DMIS_SYS_DOCTYPE WHERE F_PACKTYPEDEF=1 AND F_PACKTYPENO="
            //        + frmFlow.iPackNo);
            //if (dtTmp != null)
            //{
            //    if (dtTmp.Rows.Count > 0)
            //    {
            //        if ((FieldToValue.FieldToInt(dtTmp.Rows[0]["F_NO"]) != frmFlow.iDocNo))
            //        {
            //            if (cbAutoCreate.Checked)
            //            {
            //                MessageBox.Show("一个业务自动创建文档只能唯一");
            //                return;
            //            }
            //        }
            //    }
            //}
            frmFlow.sDocName = txtName.Text;
            if (frmFlow.iDocNo >0)
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.Append("UPDATE DMIS_SYS_DOCTYPE SET ");
                strBuild.Append(" F_NAME='" + ValueToField.StringToField(txtName.Text) + "',");
                strBuild.Append(" F_DOCCAT='" + ValueToField.StringToField(cbbType.Text) + "',");
                strBuild.Append(" F_FORMFILE='" + ValueToField.StringToField(txtForm.Text) + "',");

                //***** Modify Start v2.00 liujp 2010/01/14 报表文本框改为下拉框 *****
                strBuild.Append(" F_REPORTFILE='" + ValueToField.StringToField(cbbReport.SelectedValue) + "',");
                //***** Modify End *****

                strBuild.Append(" F_ICONFILE='" + ValueToField.StringToField(txtIcon.Text) + "',");
                strBuild.Append(" F_OPENICON='" + ValueToField.StringToField(txtOpenIcon.Text) + "',");
                strBuild.Append(" F_TABLENAME='" + ValueToField.StringToField(cbbTable.Text) + "',");
                strBuild.Append(" OTHER_LANGUAGE_DESCR='" + ValueToField.StringToField(txtOTHER_LANGUAGE_DESCR.Text) + "'");
                //strBuild.Append(" F_PACKTYPEDEF=" + Convert.ToInt16(cbAutoCreate.Checked));
                strBuild.Append(" WHERE F_NO=" + frmFlow.iDocNo);
                DBOpt.dbHelper.ExecuteSql(strBuild.ToString());
            }
            else {
                uint iMax=1;
                string sTmp = DBOpt.dbHelper.ExecuteScalar("SELECT MAX(F_NO) FROM DMIS_SYS_DOCTYPE").ToString();
                if (sTmp != "") iMax = Convert.ToUInt32(sTmp) + 1;

                StringBuilder strBuild = new StringBuilder();
                strBuild.Append("INSERT INTO DMIS_SYS_DOCTYPE(F_NO,F_PACKTYPENO,F_NAME,F_DOCCAT,");
                strBuild.Append("F_FORMFILE,F_REPORTFILE,F_ICONFILE,F_OPENICON,F_TABLENAME,OTHER_LANGUAGE_DESCR) VALUES(");
                strBuild.Append(iMax + ",");
                strBuild.Append(frmFlow.iPackNo + ",'");
                strBuild.Append( ValueToField.StringToField(txtName.Text) + "','");
                strBuild.Append( ValueToField.StringToField(cbbType.Text) + "','");
                strBuild.Append( ValueToField.StringToField(txtForm.Text) + "','");

                //***** Modify Start v2.00 liujp 2010/01/14 报表文本框改为下拉框 *****
                strBuild.Append(ValueToField.StringToField(cbbReport.SelectedValue) + "','");
                //***** Modify End *****

                strBuild.Append( ValueToField.StringToField(txtIcon.Text) + "','");
                strBuild.Append( ValueToField.StringToField(txtOpenIcon.Text) + "','");
                strBuild.Append(ValueToField.StringToField(cbbTable.Text) + "','");
                strBuild.Append(ValueToField.StringToField(txtOTHER_LANGUAGE_DESCR.Text) + "'");
                //strBuild.Append( Convert.ToInt16(cbAutoCreate.Checked));
                strBuild.Append(")");
                DBOpt.dbHelper.ExecuteSql(strBuild.ToString());
                frmFlow.iDocNo = Convert.ToInt16(iMax);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnForm_Click(object sender, EventArgs e)
        {
            open1.Filter = "web (*.aspx)|*.aspx";
            if (open1.ShowDialog() == DialogResult.OK)
                txtForm.Text = GetFileName(open1.FileName);
        }

        private void btnIcon_Click(object sender, EventArgs e)
        {
            open1.Filter = "image|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";
            if (open1.ShowDialog() == DialogResult.OK)
                txtIcon.Text = GetFileName(open1.FileName);
        }

        private void btnOpenIcon_Click(object sender, EventArgs e)
        {
            open1.Filter = "image|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";
            if (open1.ShowDialog() == DialogResult.OK)
                txtOpenIcon.Text = GetFileName(open1.FileName);
        }

        private void frmdoctype_Load(object sender, EventArgs e)
        {
            inTable();

            //***** Modify Start v2.00 liujp 2010/01/14 报表文本框改为下拉框 *****
            //取得报表信息SQL文
            StringBuilder sbReportSql = new StringBuilder();
            sbReportSql.Append("SELECT ID ID, ");

            //报表名称(中文、西班牙文)
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
            {
                sbReportSql.Append("       NAME NAME ");
            }
            else
            {
                sbReportSql.Append("       OTHER_LANGUAGE_DESCR NAME ");
            }
            sbReportSql.Append("  FROM DMIS_SYS_REPORT ");
            sbReportSql.Append(" ORDER BY TYPE_ID, ");
            sbReportSql.Append("          ORDER_ID ");

            //报表下拉框初始化
            DataTable dtReport = new DataTable();
            dtReport = DBOpt.dbHelper.GetDataTable(sbReportSql.ToString());
            cbbReport.DataSource = dtReport;
            cbbReport.DisplayMember = "NAME";
            cbbReport.ValueMember = "ID";
            cbbReport.SelectedIndex = -1;
            //***** Modify End *****

            if (frmFlow.iDocNo > 0) {

                DataTable dt1;
                dt1 = DBOpt.dbHelper.GetDataTable("SELECT * FROM DMIS_SYS_DOCTYPE WHERE F_NO="+frmFlow.iDocNo);
                if (dt1.Rows.Count > 0) { 
                    txtName.Text=FieldToValue.FieldToString(dt1.Rows[0]["F_NAME"]);
                    cbbType.Text = FieldToValue.FieldToString(dt1.Rows[0]["F_DOCCAT"]);
                    txtForm.Text = FieldToValue.FieldToString(dt1.Rows[0]["F_FORMFILE"]);
                    txtIcon.Text = FieldToValue.FieldToString(dt1.Rows[0]["F_ICONFILE"]);
                    txtOpenIcon.Text = FieldToValue.FieldToString(dt1.Rows[0]["F_OPENICON"]);
                    cbbTable.Text = FieldToValue.FieldToString(dt1.Rows[0]["F_TABLENAME"]);
                    txtOTHER_LANGUAGE_DESCR.Text = FieldToValue.FieldToString(dt1.Rows[0]["OTHER_LANGUAGE_DESCR"]);

                    //***** Modify Start v2.00 liujp 2010/01/14 报表文本框改为下拉框 *****
                    //报表下拉框默认初始值
                    if (!(dt1.Rows[0]["F_REPORTFILE"]!=Convert.DBNull || dt1.Rows[0]["F_REPORTFILE"].ToString().Equals("")))
                    {
                        int index;
                        if(int.TryParse(dt1.Rows[0]["F_REPORTFILE"].ToString(),out index))
                            cbbReport.SelectedValue = index;
                    }
                    //***** Modify End *****
                }

            }
            TypeChange();
        }


        private void inTable() {
            DataTable dt1=new DataTable();
            cbbTable.Items.Clear();
            dt1 = DBOpt.dbHelper.GetDataTable("select NAME from DMIS_SYS_TABLES order by ORDER_ID");
            if (dt1.Rows.Count > 0) {
                for(int i=0;i<dt1.Rows.Count;i++)
                    cbbTable.Items.Add(FieldToValue.FieldToString(dt1.Rows[i][0]));                
            } 
        }


        private string GetFileName(string sFullPathFile) { 
            sFullPathFile=sFullPathFile.Replace(@"/",@"\");
            if(sFullPathFile.IndexOf(@"\")>-1)
                return(sFullPathFile.Substring(sFullPathFile.LastIndexOf(@"\")+1));
            return (sFullPathFile);
        }


        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeChange();
        }


        private void TypeChange() {
            if (cbbType.Text == "File")
            {
                txtForm.Text = "upfile.aspx";
                cbbTable.Text = "DMIS_SYS_FILE";
                txtForm.Enabled = false;
                btnForm.Enabled = false;

                //***** Modify Start v2.00 liujp 2010/01/14 报表文本框改为下拉框 *****
                cbbReport.SelectedIndex = -1;
                cbbReport.Enabled = false;
                //***** Modify End *****

                cbbTable.Enabled = false;
            }
            else
            {
                txtForm.Enabled = true;
                btnForm.Enabled = true;

                //***** Modify Start v2.00 liujp 2010/01/14 报表文本框改为下拉框 *****
                cbbReport.Enabled = true;
                //***** Modify End *****

                cbbTable.Enabled = true;
            }
        
        }

 
    }
}
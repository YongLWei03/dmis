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
    public partial class frmMember : Form
    {
        private string _sql;
        private DataTable _dt;

        public frmMember()
        {
            InitializeComponent();
        }

        private void frmMember_Load(object sender, EventArgs e)
        {
            _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,SUPERIOR_ID from DMIS_SYS_DEPART order by ORDER_ID");
            BuildTree(null);

        }


        private void BuildTree(TreeNode tn)
        {
            int i;
            // 空节点时创建根节点，父ID为NULL的当作根节点
            if(tn == null)
            {
                trvDepart.Nodes.Clear();
                for(i = 0 ; i < _dt.Rows.Count ; i++)
                {
                    if (_dt.Rows[i]["SUPERIOR_ID"].ToString()=="0")
                    {
                        TreeNode tmp = new TreeNode(_dt.Rows[i]["name"].ToString());
                        tmp.Tag = Int32.Parse(_dt.Rows[i]["id"].ToString());
                        trvDepart.Nodes.Add(tmp);
                    }
                }
                // 循环递归创建树
                for (i = 0; i < trvDepart.Nodes.Count; i++)
                {
                    BuildTree(trvDepart.Nodes[i]);
                }
             }
            else // 节点非空为递归调用
            {
                for(i = 0 ; i < _dt.Rows.Count ; i ++)
                {
                    if (tn.Tag.ToString() == _dt.Rows[i]["SUPERIOR_ID"].ToString())
                    {
                        TreeNode tmp = new TreeNode(_dt.Rows[i]["name"].ToString());
                        tmp.Tag = Int32.Parse(_dt.Rows[i]["id"].ToString());
                        tn.Nodes.Add(tmp);
                    }
                }
                for(i = 0 ; i < tn.Nodes.Count ; i ++)
                {
                    BuildTree(tn.Nodes[i]);
                }
             }
        }

        private void trvDepart_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e == null) return;
            InitMemeber(e.Node.Tag.ToString());
        }

        private void InitMemeber(string departId)
        {
            lsMemeber.Items.Clear();
            ListViewItem lv;
            DataTable dt = DBOpt.dbHelper.GetDataTable("select ID,CODE,NAME,PASSWORD,FLAG,HOME_PHONE,OFFICE_PHONE,MOBILE,EMAIL,ADDRESS,THEME,ORDER_ID from DMIS_SYS_MEMBER where DEPART_ID=" + departId + " order by ORDER_ID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(dt.Rows[i][0].ToString());
                
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName != "PASSWORD")
                    {
                        if (dt.Rows[i][j] != null) lv.SubItems.Add(dt.Rows[i][j].ToString());
                    }
                    else
                        lv.SubItems.Add("***");
                }
                lsMemeber.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lsMemeber, Color.SkyBlue, Color.Lime);
        }

        private void tlbAdd_Click(object sender, EventArgs e)
        {
            if (trvDepart.SelectedNode == null)
            {
                MessageBox.Show(this, Main.Properties.Resources.SelectDepart);//"请先选择一个部门!"
                return;
            }

            txtDEPART_ID.Text = trvDepart.SelectedNode.Tag.ToString();
            txtID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_MEMBER", "ID").ToString();
            txtCODE.Text = "";
            txtNAME.Text = "";
            txtPASSWORD.Text = "";
            ckbFLAG.Checked = false;
            txtHOME_PHONE.Text = "";
            txtOFFICE_PHONE.Text = "";
            txtMOBILE.Text = "";
            txtEMAIL.Text = "";
            txtADDRESS.Text = "";
            cbSEX.Text = Main.Properties.Resources.Man;  //男
            txtTHEME.Text = "";

            int count = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_MEMBER where DEPART_ID=" + txtDEPART_ID.Text));
            txtORDER_ID.Text = Convert.ToString(count * 10);
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (lsMemeber.SelectedItems.Count < 1)
            {
                MessageBox.Show(this, Main.Properties.Resources.SelectDeleteItem);//"请先选择要删除的操作员!"
                return;
            }
            else
            {
                if (MessageBox.Show(Main.Properties.Resources.DeleteBeforeConfirm + lsMemeber.SelectedItems[0].SubItems[2].Text + "?", Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            }

            _sql = "delete from DMIS_SYS_MEMBER where ID=" + lsMemeber.SelectedItems[0].Text;
            DBOpt.dbHelper.ExecuteSql(_sql);
            InitMemeber(trvDepart.SelectedNode.Tag.ToString());

            txtDEPART_ID.Text ="";
            txtID.Text = "";
            txtCODE.Text = "";
            txtNAME.Text = "";
            txtPASSWORD.Text = "";
            ckbFLAG.Checked = false;
            txtHOME_PHONE.Text = "";
            txtOFFICE_PHONE.Text = "";
            txtMOBILE.Text = "";
            txtEMAIL.Text = "";
            txtADDRESS.Text = "";
            cbSEX.Text = Main.Properties.Resources.Man;
            txtTHEME.Text = "";
            txtORDER_ID.Text = "";
        }

        private void lsMemeber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsMemeber.SelectedItems.Count != 1) return;

            _sql = "select * from DMIS_SYS_MEMBER where ID=" + lsMemeber.SelectedItems[0].Text;
            _dt = DBOpt.dbHelper.GetDataTable(_sql);
            
            txtDEPART_ID.Text = trvDepart.SelectedNode.Tag.ToString();
            txtID.Text = _dt.Rows[0]["ID"].ToString();
            txtCODE.Text = _dt.Rows[0]["CODE"].ToString(); 
            txtNAME.Text = _dt.Rows[0]["NAME"].ToString();
            txtPASSWORD.Text = _dt.Rows[0]["PASSWORD"].ToString();   //口令不能显示明文
            ckbFLAG.Checked = Convert.ToInt16(_dt.Rows[0]["FLAG"]) == 0 ? false : true;
            txtHOME_PHONE.Text = _dt.Rows[0]["HOME_PHONE"]==Convert.DBNull?"":_dt.Rows[0]["HOME_PHONE"].ToString();
            txtOFFICE_PHONE.Text = _dt.Rows[0]["OFFICE_PHONE"] == Convert.DBNull ? "" : _dt.Rows[0]["OFFICE_PHONE"].ToString();
            txtMOBILE.Text = _dt.Rows[0]["MOBILE"] == Convert.DBNull ? "" : _dt.Rows[0]["MOBILE"].ToString();
            txtEMAIL.Text = _dt.Rows[0]["EMAIL"] == Convert.DBNull ? "" : _dt.Rows[0]["EMAIL"].ToString();
            txtADDRESS.Text = _dt.Rows[0]["ADDRESS"] == Convert.DBNull ? "" : _dt.Rows[0]["ADDRESS"].ToString();
            cbSEX.Text = _dt.Rows[0]["SEX"] == Convert.DBNull ? "" : _dt.Rows[0]["SEX"].ToString();
            txtTHEME.Text = _dt.Rows[0]["THEME"] == Convert.DBNull ? "" : _dt.Rows[0]["THEME"].ToString();
            txtORDER_ID.Text = _dt.Rows[0]["ORDER_ID"] == Convert.DBNull ? "" : _dt.Rows[0]["ORDER_ID"].ToString();           

        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtNAME.Text.Trim() == "" || txtDEPART_ID.Text.Trim() == "" || txtCODE.Text.Trim() == "" || txtPASSWORD.Text.Trim() == "")
            {
                MessageBox.Show(this, Main.Properties.Resources.NoEmpty);//"某些项不允许为空!"
                return;
            }
            if (txtORDER_ID.Text.Trim() != "")
            {
                int order;
                if (!int.TryParse(txtORDER_ID.Text.Trim(), out order))
                {

                    MessageBox.Show(this, Main.Properties.Resources.NumericalValeError);//"排列序号不是整数类型！"
                    return;
                }
            }

            FieldPara[] field = {new FieldPara("ID",FieldType.Int,txtID.Text),
								 new FieldPara("NAME",FieldType.String,txtNAME.Text),
								 new FieldPara("DEPART_ID",FieldType.Int,txtDEPART_ID.Text),
								 new FieldPara("CODE",FieldType.String,txtCODE.Text),
								 new FieldPara("PASSWORD",FieldType.String,txtPASSWORD.Text),
                                 new FieldPara("FLAG",FieldType.Int,ckbFLAG.Checked?"1":"0"),
                                 new FieldPara("HOME_PHONE",FieldType.String,txtHOME_PHONE.Text),
                                 new FieldPara("OFFICE_PHONE",FieldType.String,txtOFFICE_PHONE.Text),
                                 new FieldPara("MOBILE",FieldType.String,txtMOBILE.Text),
                                 new FieldPara("EMAIL",FieldType.String,txtEMAIL.Text),
                                 new FieldPara("ADDRESS",FieldType.String,txtADDRESS.Text),
                                 new FieldPara("SEX",FieldType.String,cbSEX.Text),
                                 new FieldPara("THEME",FieldType.String,txtTHEME.Text),
                                 new FieldPara("ORDER_ID",FieldType.Int,txtORDER_ID.Text)
                               };

            if (DBOpt.dbHelper.IsExist("DMIS_SYS_MEMBER", "ID=" + txtID.Text))
            {
                WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
                _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_MEMBER", field, where);
            }
            else
            {
                object obj = DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_MEMBER where CODE='" + txtCODE.Text.Trim() + "'");
                if (Convert.ToInt16(obj) > 0)
                {
                    MessageBox.Show(this, Main.Properties.Resources.DuplicateCode);//"已经存在相同的登录代码,请修改!"
                    return;
                }
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_MEMBER", field);
            }

            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
            {
                InitMemeber(trvDepart.SelectedNode.Tag.ToString());
                //如果是更新，则同时更新t_login中的口令.
                //string pwd="";
                //for (int i = 0; i < txtPASSWORD.Text.Length; i++)
                //{
                //    pwd += Convert.ToInt16(txtPASSWORD.Text[i]).ToString("000");
                //}
                //_sql = "update DMIS_SYS_MEMBER set PASSWORD='" + pwd + "' where ID=" + txtID.Text;
                //if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
                //{
                //    _sql = "update t_login set pass_wd='" + pwd + "' where code='" + txtCODE.Text + "'";
                //    if (DBOpt.dbHelper.ExecuteSql(_sql) < 0)
                //    {
                //        //MessageBox.Show(this, "更新t_login中的口令失败,请修改!");
                //        //return;
                //    }
                //}
            }
            
        }

        private void txtORDER_ID_Validating(object sender, CancelEventArgs e)
        {
            int order;
            if (!(int.TryParse(txtORDER_ID.Text, out order) || txtORDER_ID.Text == ""))
            {
                errorProvider1.SetError((Control)sender, Main.Properties.Resources.NumericalNote);
                tlbSave.Enabled = false;
                e.Cancel = true;
            }
            else
            {
                tlbSave.Enabled = true;
                errorProvider1.SetError((Control)sender, "");
            }
        }

        private void tlbRefresh_Click(object sender, EventArgs e)
        {
            if (trvDepart.SelectedNode == null) return;

            InitMemeber(trvDepart.SelectedNode.Tag.ToString());
        }

        private void frmMember_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                tlbAdd_Click(null, null);
            else if (e.KeyCode == Keys.F3)
                tlbDelete_Click(null, null);
            else if (e.KeyCode == Keys.F4)
                tlbSave_Click(null, null);
            else
                ;
        }

        private void tsbSelectDepart_Click(object sender, EventArgs e)
        {
            if (lsMemeber.SelectedItems.Count < 1) return;

            frmDepartSelect select = new frmDepartSelect();
            select.selectedMemuID = lsMemeber.SelectedItems[0].Text;
            if (select.ShowDialog() == DialogResult.OK)
            {
                tlbRefresh_Click(null, null);
            }
        }





    }
}
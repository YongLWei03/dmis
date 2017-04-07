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
    public partial class frmDepart : Form
    {
        private string sql;
        private DataTable _dt;
        
        public frmDepart()
        {
            InitializeComponent();
        }

        private void frmDepart_Load(object sender, EventArgs e)
        {
            InitComboBox();
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,SUPERIOR_ID from DMIS_SYS_DEPART order by ORDER_ID");
            else
                _dt = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR,SUPERIOR_ID from DMIS_SYS_DEPART order by ORDER_ID");
            BuildTree(null);
        }
        

        private void BuildTree(TreeNode tn)
        {
            int i;
            // 空节点时创建根节点，父ID为NULL的当作根节点
            if (tn == null)
            {
                trvDepart.Nodes.Clear();
                for (i = 0; i < _dt.Rows.Count; i++)
                {
                    if (_dt.Rows[i]["SUPERIOR_ID"].ToString() == "0")
                    {
                        TreeNode tmp = new TreeNode(_dt.Rows[i][1].ToString());
                        tmp.Tag = Int32.Parse(_dt.Rows[i][0].ToString());
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
                for (i = 0; i < _dt.Rows.Count; i++)
                {
                    if (tn.Tag.ToString() == _dt.Rows[i]["SUPERIOR_ID"].ToString())
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

        private void InitComboBox()
        {
            cbTYPE.Items.Clear();
            sql = "select ID,NAME from DMIS_SYS_DEPART_TYPE order by ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbTYPE.Items.Add(dt.Rows[i][1].ToString());
            }
            cbTYPE.Items.Add("");

            //
            cbSUPERIOR_ID.Items.Clear();
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                sql = "select ID,NAME from DMIS_SYS_DEPART order by ID";
            else
                sql = "select ID,OTHER_LANGUAGE_DESCR NAME from DMIS_SYS_DEPART order by ID";

            DataTable dt2 = DBOpt.dbHelper.GetDataTable(sql);
            DataRow row = dt2.NewRow();
            row[0] = "0";
            row[1] = "TOP";
            dt2.Rows.InsertAt(row, 0);
            cbSUPERIOR_ID.DataSource = dt2;
            cbSUPERIOR_ID.DisplayMember = "NAME";
            cbSUPERIOR_ID.ValueMember = "ID";
        }


        private void trvDepart_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string temp;
            sql = "select ID,NAME,TYPE,LEADER,ADDRESS,SUPERIOR_ID,POSTALCODE,TELEPHONE,ORDER_ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_DEPART where ID=" + e.Node.Tag.ToString();
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            txtID.Text = dt.Rows[0][0].ToString();
            txtNAME.Text = dt.Rows[0][1].ToString();
            if (dt.Rows[0][2] != null)
                cbTYPE.Text = dt.Rows[0][2].ToString();
            else
                cbTYPE.Text = "";

            if (dt.Rows[0][3] != null)
                txtLEADER.Text = dt.Rows[0][3].ToString();
            else
                txtLEADER.Text = "";

            if (dt.Rows[0][4] != null)
                txtADDRESS.Text = dt.Rows[0][4].ToString();
            else
                txtADDRESS.Text = "";
            if (dt.Rows[0][5].ToString() != "0")
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                    temp = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_DEPART where ID=" + dt.Rows[0][5].ToString()).ToString();
                else
                    temp = DBOpt.dbHelper.ExecuteScalar("select OTHER_LANGUAGE_DESCR from DMIS_SYS_DEPART where ID=" + dt.Rows[0][5].ToString()).ToString();
                
                cbSUPERIOR_ID.Text = temp;
                //cbSUPERIOR_ID.SelectedIndex=cbSUPERIOR_ID.Items.IndexOf(cbSUPERIOR_ID.FindString(temp));
            }
            else
                cbSUPERIOR_ID.SelectedIndex = 0;
            
            if (dt.Rows[0][6] != null)
                txtPOSTALCODE.Text = dt.Rows[0][6].ToString();
            else
                txtPOSTALCODE.Text = "";

            if (dt.Rows[0][7] != null)
                txtTELEPHONE.Text = dt.Rows[0][7].ToString();
            else
                txtTELEPHONE.Text = "";

            if (dt.Rows[0][8] != null)
                txtORDER_ID.Text = dt.Rows[0][8].ToString();
            else
                txtORDER_ID.Text = "";

            if (dt.Rows[0]["OTHER_LANGUAGE_DESCR"] != null)
                txtOTHER_LANGUAGE_DESCR.Text = dt.Rows[0]["OTHER_LANGUAGE_DESCR"].ToString();
            else
                txtOTHER_LANGUAGE_DESCR.Text = "";

            cbSUPERIOR_ID.Enabled = false;
        }

 

        private void tlbAdd_Click(object sender, EventArgs e)
        {
            txtID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_DEPART", "ID").ToString();
            cbTYPE.Text = "";
            txtNAME.Text = "";
            txtLEADER.Text = "";
            txtADDRESS.Text = "";
            txtPOSTALCODE.Text = "";
            txtTELEPHONE.Text = "";
            if (trvDepart.SelectedNode == null)
                cbSUPERIOR_ID.Text = "";
            else
                cbSUPERIOR_ID.SelectedIndex = cbSUPERIOR_ID.FindString(trvDepart.SelectedNode.Text);

            cbSUPERIOR_ID.Enabled = true;

            int count = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_DEPART"));
            txtORDER_ID.Text = Convert.ToString(count * 10);
            txtOTHER_LANGUAGE_DESCR.Text = "";
        }


        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (trvDepart.SelectedNode.Nodes.Count > 0)
            {
                MessageBox.Show(this, Main.Properties.Resources.RelatdItemsNoDelete);//"此部门下还有所属部门，不能删除!"
                return;
            }
            if (DBOpt.dbHelper.IsExist("DMIS_SYS_MEMBER", "DEPART_ID=" + txtID.Text))
            {
                MessageBox.Show(this, Main.Properties.Resources.RelatdItemsNoDelete);//"此部门下还有人员，不能删除!"
                return;
            }

            if (MessageBox.Show(Main.Properties.Resources.DeleteBeforeConfirm, Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            sql = "delete from DMIS_SYS_DEPART where ID=" + trvDepart.SelectedNode.Tag.ToString();
            DBOpt.dbHelper.ExecuteSql(sql);
            TreeNode td = trvDepart.SelectedNode.Parent;
            if (td == null)
                trvDepart.Nodes.Remove(trvDepart.SelectedNode);  //顶层单位没有下属单位的情况
            else
                td.Nodes.Remove(trvDepart.SelectedNode);　　　　//删除叶子这级的单位
        }


        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtNAME.Text.Trim() == "")
            {
                MessageBox.Show(this, Main.Properties.Resources.NoEmpty);
                return;
            }

            if (txtOTHER_LANGUAGE_DESCR.Text.Trim() == "")
            {
                MessageBox.Show(this, Main.Properties.Resources.NoEmpty);
                return;
            }

            if (txtORDER_ID.Text.Trim() != "")
            {
                int order;
                if (!int.TryParse(txtORDER_ID.Text.Trim(), out order))
                {

                    MessageBox.Show(this, Main.Properties.Resources.NumericalValeError);
                    return;
                }
            }

            FieldPara[] field = {new FieldPara("ID",FieldType.Int,txtID.Text),
								 new FieldPara("NAME",FieldType.String,txtNAME.Text),
								 new FieldPara("TYPE",FieldType.String,cbTYPE.Text),
								 new FieldPara("LEADER",FieldType.String,txtLEADER.Text),
								 new FieldPara("ADDRESS",FieldType.String,txtADDRESS.Text),
								 new FieldPara("SUPERIOR_ID",FieldType.Int,cbSUPERIOR_ID.SelectedValue.ToString()),
                                 new FieldPara("POSTALCODE",FieldType.String,txtPOSTALCODE.Text),
                                 new FieldPara("TELEPHONE",FieldType.String,txtTELEPHONE.Text),
                                 new FieldPara("ORDER_ID",FieldType.Int,txtORDER_ID.Text),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtOTHER_LANGUAGE_DESCR.Text)
                               };

            if (DBOpt.dbHelper.IsExist("DMIS_SYS_DEPART", "ID=" + txtID.Text))
            {
                WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
                sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_DEPART", field, where);
                if (DBOpt.dbHelper.ExecuteSql(sql) < 0)
                {
                    //Log.InsertLog("更新", "失败", sql);
                    return;
                }
                if(trvDepart.SelectedNode !=null) trvDepart.SelectedNode.Text = txtNAME.Text;
                cbSUPERIOR_ID.Enabled = false;
            }
            else
            {
                sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_DEPART", field);
                if (DBOpt.dbHelper.ExecuteSql(sql) < 0)
                {
                    return;
                }
                _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,SUPERIOR_ID from DMIS_SYS_DEPART order by ORDER_ID");
                BuildTree(null);
                cbSUPERIOR_ID.Enabled = false;
                //TreeNode td = new TreeNode();
                //td.Text = txtNAME.Text;
                //td.Tag = txtID.Text;
                
                //if (cbSUPERIOR_ID.Text == "顶级")
                //    trvDepart.Nodes.Add(td);
                //else
                //    trvDepart.SelectedNode.Nodes.Add(td);
            }
        }

        private void txtORDER_ID_Validating(object sender, CancelEventArgs e)
        {
            int order;
            if (!(int.TryParse(txtORDER_ID.Text, out order) || txtORDER_ID.Text == ""))
            {
                errorProvider1.SetError((Control)sender, Main.Properties.Resources.NumericalValeError);
                tlbSave.Enabled = false;
                e.Cancel = true;
            }
            else
            {
                tlbSave.Enabled = true;
                errorProvider1.SetError((Control)sender, "");
            }
        }

        private void frmDepart_KeyUp(object sender, KeyEventArgs e)
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





    }
}
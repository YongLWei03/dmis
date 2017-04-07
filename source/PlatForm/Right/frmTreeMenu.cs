using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;

namespace PlatForm
{
    public partial class frmTreeMenu : Form
    {
        DataTable _dt;
        string _sql;

        public frmTreeMenu()
        {
            InitializeComponent();
        }


        private void frmTreeMenu_Load(object sender, EventArgs e)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");
            else
                _dt = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");
            BuildTree(null);
            initReport();
        }


        private void initReport()
        {
            DataTable dt = DBOpt.dbHelper.GetDataTable("select ID,NAME from DMIS_SYS_REPORT order by TYPE_ID,ORDER_ID");
            dt.Rows.Add(dt.NewRow());
            cbbREPORT_ID.DataSource = dt;
            cbbREPORT_ID.DisplayMember = "NAME";
            cbbREPORT_ID.ValueMember = "ID";
            cbbREPORT_ID.SelectedIndex = -1;
        }


        private void BuildTree(TreeNode tn)
        {
            int i;
            // 空节点时创建根节点，父ID为NULL的当作根节点
            if (tn == null)
            {
                trvTreeMenu.Nodes.Clear();
                for (i = 0; i < _dt.Rows.Count; i++)
                {
                    if (_dt.Rows[i]["PARENT_ID"].ToString() == "0")
                    {
                        TreeNode tmp = new TreeNode(_dt.Rows[i][1].ToString());
                        tmp.Tag = Int32.Parse(_dt.Rows[i][0].ToString());
                        trvTreeMenu.Nodes.Add(tmp);
                    }
                }
                // 循环递归创建树
                for (i = 0; i < trvTreeMenu.Nodes.Count; i++)
                {
                    BuildTree(trvTreeMenu.Nodes[i]);
                }
            }
            else // 节点非空为递归调用
            {
                for (i = 0; i < _dt.Rows.Count; i++)
                {
                    if (tn.Tag.ToString() == _dt.Rows[i]["PARENT_ID"].ToString())
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

        private void tlbAdd_Click(object sender, EventArgs e)
        {
           txtID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_TREEMENU", "ID").ToString();
           if (trvTreeMenu.SelectedNode == null)
               txtPARENT_ID.Text = "0";    //不能为空
           else
               txtPARENT_ID.Text = trvTreeMenu.SelectedNode.Tag.ToString();

            txtNAME.Text = "";
            txtFILE_NAME.Text = "";

            cbbTARTGET.Text = "main";
            txtEXPAND_IMAGE.Text = "";
            cbbREPORT_ID.SelectedIndex = -1;
            txtTABLE_IDS.Text = "";
            txtORDERS.Text = "";
            txtOTHER_PARA.Text = "";
            txtOTHER_LANGUAGE_DESCR.Text = "";

            int count = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_TREEMENU where PARENT_ID=" + txtPARENT_ID.Text));
            txtORDER_ID.Text = Convert.ToString(count*10);
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (trvTreeMenu.SelectedNode == null)
            {
                //MessageBox.Show(this, "请先选择要删除的功能!");
                return;
            }
            if (trvTreeMenu.SelectedNode.Nodes.Count > 1)
            {
                //MessageBox.Show(this, "不允许删除有子功能的项!");
                return;
            }
            else
            {
                if (MessageBox.Show(Main.Properties.Resources.DeleteBeforeConfirm, Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            }
            _sql = "delete from DMIS_SYS_TREEMENU where ID=" + trvTreeMenu.SelectedNode.Tag.ToString();
            DBOpt.dbHelper.ExecuteSql(_sql);
            trvTreeMenu.Nodes.Remove(trvTreeMenu.SelectedNode);

            txtPARENT_ID.Text = "";
            txtID.Text = "";
            txtNAME.Text = "";
            txtFILE_NAME.Text = "";
            cbbTARTGET.Text = "";
            txtEXPAND_IMAGE.Text = "";
            cbbREPORT_ID.SelectedIndex = -1;
            txtOTHER_LANGUAGE_DESCR.Text = "";
            txtTABLE_IDS.Text = "";
            txtORDERS.Text = "";
            txtORDER_ID.Text = "";
            txtOTHER_PARA.Text = "";
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtPARENT_ID.Text.Trim() == "" || txtNAME.Text.Trim() == "")
            {
                MessageBox.Show(this, Main.Properties.Resources.NoEmpty, Main.Properties.Resources.Note,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            int temp;
            string[] isInt = new string[3] { txtPARENT_ID.Text, cbbREPORT_ID.SelectedValue == null ? "" : cbbREPORT_ID.SelectedValue.ToString(), txtORDER_ID.Text };
            for (int i = 0; i < isInt.Length; i++)
            {
                if (isInt[i] ==null || isInt[i].Trim() == "") continue;
                if (!int.TryParse(isInt[i], out temp))
                {
                    MessageBox.Show(this, Main.Properties.Resources.NumericalValeError, Main.Properties.Resources.Note, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            FieldPara[] field = {new FieldPara("ID",FieldType.Int,txtID.Text),
                                 new FieldPara("PARENT_ID",FieldType.Int,txtPARENT_ID.Text),
								 new FieldPara("NAME",FieldType.String,txtNAME.Text.Trim()),
								 new FieldPara("FILE_NAME",FieldType.String,txtFILE_NAME.Text.Trim()),
								 new FieldPara("TARTGET",FieldType.String,cbbTARTGET.Text.Trim()),
								 new FieldPara("EXPAND_IMAGE",FieldType.String,txtEXPAND_IMAGE.Text.Trim()),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtOTHER_LANGUAGE_DESCR.Text.Trim()),
                                 new FieldPara("REPORT_ID",FieldType.Int,cbbREPORT_ID.SelectedValue==null?"":cbbREPORT_ID.SelectedValue.ToString()),
                                 new FieldPara("TABLE_IDS",FieldType.String,txtTABLE_IDS.Text.Trim()),
                                 new FieldPara("OTHER_PARA",FieldType.String,txtOTHER_PARA.Text.Trim()),
                                 new FieldPara("ORDERS",FieldType.String,txtORDERS.Text.Trim()),
                                 new FieldPara("ORDER_ID",FieldType.Int,txtORDER_ID.Text.Trim())
                               };

            if (DBOpt.dbHelper.IsExist("DMIS_SYS_TREEMENU", "ID=" + txtID.Text))
            {
                WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
                _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_TREEMENU", field, where);
                if(trvTreeMenu.SelectedNode!=null) trvTreeMenu.SelectedNode.Text = txtNAME.Text;

                //由于列名OTHER_PARA可能包含单引号
                //则必须使用参数的方式来更新此字段的内容。
                if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
                {
                    if (DBHelper.databaseType == "SqlServer")
                    {
                        _sql = "update DMIS_SYS_TREEMENU set OTHER_PARA=@OtherPara where ID=" + txtID.Text;
                        SqlParameter[] aPara = new SqlParameter[1];
                        SqlParameter pContent = new SqlParameter("@OtherPara", SqlDbType.VarChar);
                        pContent.Value = txtOTHER_PARA.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                    else if (DBHelper.databaseType == "Oracle")
                    {
                        _sql = "update DMIS_SYS_TREEMENU set OTHER_PARA=:OtherPara where ID=" + txtID.Text;
                        OracleParameter[] aPara = new OracleParameter[1];
                        OracleParameter pContent = new OracleParameter("OtherPara", OracleType.VarChar);
                        pContent.Value = txtOTHER_PARA.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                    else if (DBHelper.databaseType == "Sybase")
                    {
                        _sql = "update DMIS_SYS_TREEMENU set COLUMN_NAME=@OtherPara where ID=" + txtID.Text;
                        OleDbParameter[] aPara = new OleDbParameter[1];
                        OleDbParameter pContent = new OleDbParameter("@OtherPara", OleDbType.VarChar);
                        pContent.Value = txtOTHER_PARA.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                    else   //ODBC
                    {
                        _sql = "update DMIS_SYS_TREEMENU set OTHER_PARA=@OtherPara where ID=" + txtID.Text;
                        OdbcParameter[] aPara = new OdbcParameter[1];
                        OdbcParameter pContent = new OdbcParameter("@OtherPara", OdbcType.VarChar);
                        pContent.Value = txtOTHER_PARA.Text;
                        aPara[0] = pContent;
                        DBOpt.dbHelper.ExecuteSqlByParas(_sql, aPara);
                    }
                }
            }
            else
            {
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_TREEMENU", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                TreeNode td = new TreeNode();
                td.Text = txtNAME.Text;
                td.Tag = txtID.Text;

                if (txtPARENT_ID.Text == "0")
                    trvTreeMenu.Nodes.Add(td);
                else
                    trvTreeMenu.SelectedNode.Nodes.Add(td);
            }
        }

        private void trvTreeMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _sql = "select * from DMIS_SYS_TREEMENU where ID=" + trvTreeMenu.SelectedNode.Tag.ToString();
            _dt = DBOpt.dbHelper.GetDataTable(_sql);

            txtID.Text = trvTreeMenu.SelectedNode.Tag.ToString(); ;
            txtPARENT_ID.Text = _dt.Rows[0]["PARENT_ID"].ToString();
            txtNAME.Text = trvTreeMenu.SelectedNode.Text;
            txtFILE_NAME.Text = _dt.Rows[0]["FILE_NAME"].ToString();

            cbbTARTGET.Text = _dt.Rows[0]["TARTGET"].ToString();
            txtEXPAND_IMAGE.Text = _dt.Rows[0]["EXPAND_IMAGE"] == Convert.DBNull ? "" : _dt.Rows[0]["EXPAND_IMAGE"].ToString();
            txtOTHER_LANGUAGE_DESCR.Text = _dt.Rows[0]["OTHER_LANGUAGE_DESCR"] == Convert.DBNull ? "" : _dt.Rows[0]["OTHER_LANGUAGE_DESCR"].ToString();
            if (_dt.Rows[0]["REPORT_ID"] == Convert.DBNull || _dt.Rows[0]["REPORT_ID"].ToString()=="")
                cbbREPORT_ID.SelectedIndex = -1;
            else
                cbbREPORT_ID.SelectedIndex = CMix.GetComboBoxIndexByValue(cbbREPORT_ID, "ID=" + _dt.Rows[0]["REPORT_ID"].ToString());

            txtTABLE_IDS.Text = _dt.Rows[0]["TABLE_IDS"] == Convert.DBNull ? "" : _dt.Rows[0]["TABLE_IDS"].ToString();
            txtORDER_ID.Text = _dt.Rows[0]["ORDER_ID"] == Convert.DBNull ? "" : _dt.Rows[0]["ORDER_ID"].ToString();
            txtORDERS.Text = _dt.Rows[0]["ORDERS"] == Convert.DBNull ? "" : _dt.Rows[0]["ORDERS"].ToString();
            txtOTHER_PARA.Text = _dt.Rows[0]["OTHER_PARA"] == Convert.DBNull ? "" : _dt.Rows[0]["OTHER_PARA"].ToString();
        }

        /// <summary>
        /// 选择链接文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.InitialDirectory=@"c:\df8360\source\web\";
            dlg.Filter = "asp.net (*.aspx)|*.aspx|(*.htm)|*.htm";
            dlg.Title = "File";
            string fileName;
            if (dlg.ShowDialog() == DialogResult.OK)
            {   
                fileName = dlg.FileName.Substring(dlg.InitialDirectory.Length);
                if (fileName.IndexOf('\\') < 0)
                    txtFILE_NAME.Text = fileName;
                else
                {
                    fileName = fileName.Replace('\\', '/');
                    txtFILE_NAME.Text = "./"+fileName;
                }
            }
        }

        private void btnEXPAND_IMAGE_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.InitialDirectory = @"c:\df8360\source\web\img\";
            dlg.Filter = "graph |*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";
            dlg.Title = "File";
            string fileName;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileName = dlg.FileName.Substring(dlg.InitialDirectory.Length);
                if (fileName.IndexOf('\\') < 0)
                    txtEXPAND_IMAGE.Text = "./img/"+fileName;
                else
                {
                    fileName = fileName.Replace('\\', '/');
                    txtEXPAND_IMAGE.Text = "./img/" + fileName;
                }
            }
        }

        private void btnTABLE_IDS_Click(object sender, EventArgs e)
        {
            frmSelectTableIDs dlg = new frmSelectTableIDs(txtTABLE_IDS.Text);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTABLE_IDS.Text = dlg.Ids;
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

        private void cbbREPORT_ID_Validating(object sender, CancelEventArgs e)
        {
            int n;
            if (!(int.TryParse(cbbREPORT_ID.SelectedValue.ToString(), out n) || cbbREPORT_ID.SelectedValue.ToString() == ""))
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

        private void txtPARENT_ID_Validating(object sender, CancelEventArgs e)
        {
            int order;
            if (!(int.TryParse(txtPARENT_ID.Text, out order)))
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

        private void btnTop_Click(object sender, EventArgs e)
        {
            //"是否要把此功能置于顶层？"
            if (MessageBox.Show(Main.Properties.Resources.CanTop, Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                txtPARENT_ID.Text = "0";
            }
        }

        private void btnORDERS_Click(object sender, EventArgs e)
        {
            if (txtTABLE_IDS.Text.Trim() == "")
            {
                //MessageBox.Show("请先设置数据库表编号！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            frmSetOrder order = new frmSetOrder();
            if (txtTABLE_IDS.Text.Trim().IndexOf(',') > 0)    //有多个表ＩＤ时取第一个表的ＩＤ
                order.tableID = txtTABLE_IDS.Text.Substring(0, txtTABLE_IDS.Text.IndexOf(','));
            else
                order.tableID = txtTABLE_IDS.Text.Trim();
            if (order.ShowDialog() == DialogResult.OK)
            {
                txtORDERS.Text = order.returnString;
            }
        }

        private void tlbRefresh_Click(object sender, EventArgs e)
        {
            _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");
            BuildTree(null);
        }

        private void tlbChangeParent_Click(object sender, EventArgs e)
        {
            if (trvTreeMenu.SelectedNode == null) return;
            frmTreeMenuSelect select = new frmTreeMenuSelect();
            select.selectedMemuID = trvTreeMenu.SelectedNode.Tag.ToString();
            if (select.ShowDialog() == DialogResult.OK)
            {
                _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");
                BuildTree(null);

                txtPARENT_ID.Text = "";
                txtID.Text = "";
                txtNAME.Text = "";
                txtFILE_NAME.Text = "";
                cbbTARTGET.Text = "";
                txtEXPAND_IMAGE.Text = "";
                txtOTHER_LANGUAGE_DESCR.Text = "";
                cbbREPORT_ID.SelectedIndex = -1;
                txtTABLE_IDS.Text = "";
                txtORDERS.Text = "";
                txtORDER_ID.Text = "";
                txtOTHER_PARA.Text = "";
            }

        }

        private void frmTreeMenu_KeyUp(object sender, KeyEventArgs e)
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
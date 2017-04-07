using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.Globalization;

namespace PlatForm
{
    public partial class frmPurview : Form
    {
        DataTable _dt;
        string _sql;

        public frmPurview()
        {
            InitializeComponent();
        }

        private void frmPurview_Load(object sender, EventArgs e)
        {

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");
            else
                _dt = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");
            BuildTree(null);
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



        private void trvTreeMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            initListView();
        }

        /// <summary>
        /// 设置权限列表控件
        /// </summary>
        private void initListView()
        {
            if (trvTreeMenu.SelectedNode == null) return;
            lsvPurview.Items.Clear();

            _sql = "select ID,DESCR,WEB_FILE,CONTROL_NAME,CONTROL_PROPERTY,CONTROL_VALUE,ORDER_ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_PURVIEW where MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem lv = new ListViewItem(dt.Rows[i][0].ToString());
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] is System.DBNull)
                        lv.SubItems.Add("");
                    else
                        lv.SubItems.Add(dt.Rows[i][j].ToString());
                }
                lsvPurview.Items.Add(lv);
            }
        }


        private void tlbAdd_Click(object sender, EventArgs e)
        {
            txtID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PURVIEW", "ID").ToString();
            if (trvTreeMenu.SelectedNode == null)
            {
                //MessageBox.Show("请先选择某一功能!");
                return;
            }
            else
            {
                txtMODULE_ID.Text=trvTreeMenu.SelectedNode.Tag.ToString();
                txtMODULE_NAME.Text = trvTreeMenu.SelectedNode.Text;
                txtID.Text = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PURVIEW", "ID").ToString();
                txtDESCR.Text="";
                txtWEB_FILE.Text="";
                txtCONTROL_NAME.Text="";
                cobCONTROL_PROPERTY.Text="";
                cobCONTROL_VALUE.Text="";
                txtOTHER_LANGUAGE_DESCR.Text = "";

                int count = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_PURVIEW where MODULE_ID=" + txtMODULE_ID.Text));
                txtORDER_ID.Text = Convert.ToString(count*10);
            }
        }


        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (lsvPurview.SelectedItems.Count < 1)
            {
                //MessageBox.Show("请先要选择删除的权限!");
                return;
            }
            if (MessageBox.Show(Main.Properties.Resources.DeleteBeforeConfirm, Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            _sql = "delete from DMIS_SYS_PURVIEW where MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and ID=" + lsvPurview.SelectedItems[0].Text;

            DBOpt.dbHelper.ExecuteSql(_sql);
            initListView();
        }


        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtMODULE_ID.Text.Trim() == "")
            {
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

            FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,txtMODULE_ID.Text),
                                 new FieldPara("ID",FieldType.Int,txtID.Text),
								 new FieldPara("DESCR",FieldType.String,txtDESCR.Text),
								 new FieldPara("WEB_FILE",FieldType.String,txtWEB_FILE.Text),
                                 new FieldPara("CONTROL_NAME",FieldType.String,txtCONTROL_NAME.Text),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,cobCONTROL_PROPERTY.Text),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,cobCONTROL_VALUE.Text),
                                 new FieldPara("ORDER_ID",FieldType.Int,txtORDER_ID.Text),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,txtOTHER_LANGUAGE_DESCR.Text)
                               };

            if (DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "ID=" + txtID.Text))
            {
                WherePara[] where ={ new WherePara("ID", FieldType.Int, txtID.Text, "=", "and") };
                _sql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_PURVIEW", field, where);
            }
            else
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);

            DBOpt.dbHelper.ExecuteSql(_sql);
            initListView();
        }


        private void lsvPurview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvPurview.SelectedItems.Count != 1) return;
            _sql = "select * from DMIS_SYS_PURVIEW where ID=" + lsvPurview.SelectedItems[0].Text;
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            dr.Read();
            txtMODULE_ID.Text = trvTreeMenu.SelectedNode.Tag.ToString();
            txtMODULE_NAME.Text = trvTreeMenu.SelectedNode.Text;
            txtID.Text = dr["ID"].ToString();
            txtDESCR.Text =dr["DESCR"] is System.DBNull?"": dr["DESCR"].ToString();
            txtWEB_FILE.Text = dr["WEB_FILE"] is System.DBNull?"": dr["WEB_FILE"].ToString();
            txtCONTROL_NAME.Text = dr["CONTROL_NAME"] is System.DBNull?"": dr["CONTROL_NAME"].ToString();
            cobCONTROL_PROPERTY.Text = dr["CONTROL_PROPERTY"] is System.DBNull?"": dr["CONTROL_PROPERTY"].ToString();
            cobCONTROL_VALUE.Text = dr["CONTROL_VALUE"] is System.DBNull?"": dr["CONTROL_VALUE"].ToString();
            txtORDER_ID.Text = dr["ORDER_ID"] is System.DBNull?"": dr["ORDER_ID"].ToString();
            txtOTHER_LANGUAGE_DESCR.Text = dr["OTHER_LANGUAGE_DESCR"] is System.DBNull ? "" : dr["OTHER_LANGUAGE_DESCR"].ToString();
            dr.Close();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            //dlg.InitialDirectory = Application.StartupPath;
            //dlg.InitialDirectory = @"c:\df8360\source\web\";
            dlg.Filter = "Asp.net File (*.aspx)|*.aspx|Htm File(*.htm)|*.htm|Html File(*.html)|*.html";
            dlg.Title = "File";
            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.FileName.LastIndexOf("\\") >= 0) 
                    txtWEB_FILE.Text=dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\")+1);
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


        private void tsbQuickAddRight_Click(object sender, EventArgs e)
        {
            if (trvTreeMenu.SelectedNode == null)
            {
                MessageBox.Show(Main.Properties.Resources.SelectItem);//"请先选择某一功能!"
                return;
            }
            uint maxID = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PURVIEW", "ID");

            //已经有了此控件,则不添加
            //网页名称要自己填写   2008-3-25，AYF在晋江已经修改了，不用自己写了
            string webFileList="", webFileDet="";
            object obj = DBOpt.dbHelper.ExecuteScalar("select FILE_NAME from DMIS_SYS_TREEMENU where ID=" + trvTreeMenu.SelectedNode.Tag.ToString());
            if (obj == null || obj.ToString() == "")
            {
                MessageBox.Show(Main.Properties.Resources.SetWebFile);//"请先设置此功能对应的网页名!"
                return;
            }

            webFileList = obj.ToString().Substring(obj.ToString().LastIndexOf('/') + 1);
            webFileDet = webFileList.Substring(0, webFileList.IndexOf('.')) + "_Det" + webFileList.Substring(webFileList.IndexOf('.'));
            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnAdd'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                    new FieldPara("ID",FieldType.Int,maxID.ToString()),
								    new FieldPara("DESCR",FieldType.String,"允许添加"),
                                    new FieldPara("WEB_FILE",FieldType.String,webFileList),
                                    new FieldPara("CONTROL_NAME",FieldType.String,"btnAdd"),
                                    new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                    new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                    new FieldPara("ORDER_ID",FieldType.Int,"1"),
                                    new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowAdd),//"允许添加"
                                    };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }
            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnDelete'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许删除"),//"允许删除"
                                 new FieldPara("WEB_FILE",FieldType.String,webFileList),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnDelete"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"2"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowDelete)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }
            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnSearch'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许查询"),//"允许查询"
                                 new FieldPara("WEB_FILE",FieldType.String,webFileList),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnSearch"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"3"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowSearch)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }
            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnPrint'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许打印"),//"允许打印"
                                 new FieldPara("WEB_FILE",FieldType.String,webFileList),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnPrint"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"4"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowPrint)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }
            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnSaveExcel'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许导出EXCEL"),//"允许导出EXCEL"
                                 new FieldPara("WEB_FILE",FieldType.String,webFileList),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnSaveExcel"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"5"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowExcel)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }
            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnSave'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许保存"),//"允许保存"
                                 new FieldPara("WEB_FILE",FieldType.String,webFileDet),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnSave"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"6"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowSave)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }
            
            initListView();
        }

        private void tlbRefresh_Click(object sender, EventArgs e)
        {
            initListView();
        }

        private void tsbQuickAddFileRight_Click(object sender, EventArgs e)
        {
            if (trvTreeMenu.SelectedNode == null)
            {
                MessageBox.Show(Main.Properties.Resources.SelectItem);//"请先选择某一功能!"
                return;
            }
            uint maxID = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PURVIEW", "ID");

            //已经有了此控件,则不添加
            //网页名称要自己填写
            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnAdd'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许添加"),//"允许添加"
                                 new FieldPara("WEB_FILE",FieldType.String,"frmFile.aspx"),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnAdd"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"1"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowAdd)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }

            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnDelete'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许删除"),//"允许删除"
                                 new FieldPara("WEB_FILE",FieldType.String,"frmFile.aspx"),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnDelete"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"2"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowDelete)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }

            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnSearch'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许查询"),//"允许查询"
                                 new FieldPara("WEB_FILE",FieldType.String,"frmFile.aspx"),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnSearch"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"3"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowSearch)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }

            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnModify'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许修改"),//"允许修改"
                                 new FieldPara("WEB_FILE",FieldType.String,"frmFile.aspx"),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnModify"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"4"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowModify)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }

            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnSaveCancel'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许保存并返回"),//"允许保存并返回"
                                 new FieldPara("WEB_FILE",FieldType.String,"frmFileNew.aspx"),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnSaveCancel"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"5"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowSaveReturn)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }

            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnSave'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许保存"),//
                                 new FieldPara("WEB_FILE",FieldType.String,"frmFileNew.aspx"),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnSave"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"6"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowSave)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }

            if (!DBOpt.dbHelper.IsExist("DMIS_SYS_PURVIEW", "MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString() + " and CONTROL_NAME='btnUpload'"))
            {
                FieldPara[] field = {new FieldPara("MODULE_ID",FieldType.Int,trvTreeMenu.SelectedNode.Tag.ToString()),
                                 new FieldPara("ID",FieldType.Int,maxID.ToString()),
								 new FieldPara("DESCR",FieldType.String,"允许上传"),
                                 new FieldPara("WEB_FILE",FieldType.String,"frmFileNew.aspx"),
                                 new FieldPara("CONTROL_NAME",FieldType.String,"btnUpload"),
                                 new FieldPara("CONTROL_PROPERTY",FieldType.String,"Enabled"),
                                 new FieldPara("CONTROL_VALUE",FieldType.String,"true"),
                                 new FieldPara("ORDER_ID",FieldType.Int,"7"),
                                 new FieldPara("OTHER_LANGUAGE_DESCR",FieldType.String,Main.Properties.Resources.AllowUpload)
                               };
                _sql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_PURVIEW", field);
                DBOpt.dbHelper.ExecuteSql(_sql);
                maxID++;
            }

            initListView();
        }

        private void frmPurview_KeyUp(object sender, KeyEventArgs e)
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
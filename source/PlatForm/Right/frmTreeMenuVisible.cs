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
    public partial class frmTreeMenuVisible : Form
    {
        DataTable _dt;
        string _sql;
        string _selectedRoles="";
        int flag = 0;

        public frmTreeMenuVisible()
        {
            InitializeComponent();
        }

        private void frmTreeMenuVisible_Load(object sender, EventArgs e)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");
            else
                _dt = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR,PARENT_ID from DMIS_SYS_TREEMENU order by ORDER_ID");

            BuildTree(null);
            InitCheckedList();
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


        private void InitCheckedList()
        {
            if (trvTreeMenu.SelectedNode == null) return;
            string role_ids="";

            _sql = "select ROLE_ID from DMIS_SYS_TREEMENU_ROLE_VISIBLE where MODULE_ID="+trvTreeMenu.SelectedNode.Tag.ToString();
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                role_ids = role_ids + dr[0].ToString() + ",";
            }
            dr.Close();
            role_ids = role_ids.TrimEnd(',');
            _selectedRoles = role_ids;
            role_ids = "," + role_ids + ",";
            chlRole.Items.Clear();
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select ID,NAME from DMIS_SYS_ROLE order by ID";
            else
                _sql = "select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_ROLE order by ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (role_ids.IndexOf(","+dt.Rows[i][0].ToString()+",") >= 0)
                    chlRole.Items.Add(dt.Rows[i][1].ToString(), true);
                else
                    chlRole.Items.Add(dt.Rows[i][1].ToString(), false);
            }
        }

        private void InitMember()
        {
            if (trvTreeMenu.SelectedNode == null) return;
            DataTable dt;
            ListViewItem lv;
            lsvMember.Items.Clear();
            //if (chlRole.CheckedItems.Count < 1) return;   //chlRole_ItemCheck事件的特点
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
            {
                if (_selectedRoles.Trim() != "")
                    _sql = "select distinct a.MEMBER_NAME,a.MEMBER_ID,a.DEPART_NAME from DMIS_VIEW_DEPART_MEMBER a,DMIS_SYS_MEMBER_ROLE b where a.MEMBER_ID=b.MEMBER_ID and b.ROLE_ID in(" + _selectedRoles + ")";
                else
                    _sql = "select distinct a.MEMBER_NAME,a.MEMBER_ID,a.DEPART_NAME from DMIS_VIEW_DEPART_MEMBER a,DMIS_SYS_MEMBER_ROLE b where 1=0";
            }
            else
            {
                if (_selectedRoles.Trim() != "")
                    _sql = "select distinct a.MEMBER_NAME,a.MEMBER_ID,a.OTHER_LANGUAGE_DESCR from DMIS_VIEW_DEPART_MEMBER a,DMIS_SYS_MEMBER_ROLE b where a.MEMBER_ID=b.MEMBER_ID and b.ROLE_ID in(" + _selectedRoles + ")";
                else
                    _sql = "select distinct a.MEMBER_NAME,a.MEMBER_ID,a.OTHER_LANGUAGE_DESCR from DMIS_VIEW_DEPART_MEMBER a,DMIS_SYS_MEMBER_ROLE b where 1=0";
            }
            dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                lv = new ListViewItem(dt.Rows[j][0].ToString());
                lv.SubItems.Add(dt.Rows[j][1].ToString());
                lv.SubItems.Add(dt.Rows[j][2].ToString());
                lsvMember.Items.Add(lv);
            }
        }


        private void trvTreeMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            flag = 0;
            InitCheckedList();
            InitMember();
            flag = 1;
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (trvTreeMenu.SelectedNode == null) return;
            string role_id;

            _sql = "delete from DMIS_SYS_TREEMENU_ROLE_VISIBLE where MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString();
            DBOpt.dbHelper.ExecuteSql(_sql);

            for (int i = 0; i < chlRole.CheckedItems.Count; i++)
            {
                _sql = "select ID from DMIS_SYS_ROLE where NAME='" + chlRole.CheckedItems[i].ToString() + "'";
                role_id = DBOpt.dbHelper.ExecuteScalar(_sql).ToString();
                _sql = "insert into DMIS_SYS_TREEMENU_ROLE_VISIBLE(MODULE_ID,ROLE_ID) values(" + trvTreeMenu.SelectedNode.Tag.ToString() + "," + role_id + ")";
                DBOpt.dbHelper.ExecuteSql(_sql);
            }
        }

        private void chlRole_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (flag != 0)
            {
                string role_id, temp;
                _sql = "select ID from DMIS_SYS_ROLE where NAME='" + chlRole.Items[e.Index].ToString() + "'";
                role_id = DBOpt.dbHelper.ExecuteScalar(_sql).ToString();
                if (e.NewValue == CheckState.Unchecked)    //取消
                {
                    temp = "," + _selectedRoles + ",";
                    temp = temp.Replace("," + role_id + ",", ",");
                    //if (_selectedRoles.IndexOf(role_id + ",") >= 0)  //处于中间
                    //   _selectedRoles= _selectedRoles.Remove(_selectedRoles.IndexOf(role_id + ","), 2);
                    //else
                    //   if (_selectedRoles.IndexOf(","+role_id)>=0) //处于最后一位，要把前面的,也去掉
                    //        _selectedRoles= _selectedRoles.Remove(_selectedRoles.IndexOf(","+role_id ), 2);
                    //   else    //列表只有此一个岗位代码
                    //        _selectedRoles = _selectedRoles.Remove(_selectedRoles.IndexOf(role_id), 1);
                    if (temp.Length < 3)
                        _selectedRoles = "";
                    else
                        _selectedRoles = temp.Substring(1, temp.Length - 2);  //去掉最两边的,
                }
                else
                    if (_selectedRoles == "")
                        _selectedRoles = role_id;
                    else
                        _selectedRoles = _selectedRoles + "," + role_id;

                InitMember();
            }
        }

        private void tlbRefresh_Click(object sender, EventArgs e)
        {
             flag = 0;
            InitCheckedList();
            InitMember();
            flag = 1;
        }

        private void tlbSelectAll_Click(object sender, EventArgs e)
        {
            for (int i=0; i < chlRole.Items.Count; i++)
                chlRole.SetItemChecked(i,true);
        }

        private void tlbUnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chlRole.Items.Count; i++)
                chlRole.SetItemChecked(i, false);

        }

        private void tlbChildVisible_Click(object sender, EventArgs e)
        {
            if (trvTreeMenu.SelectedNode == null)
            {
                //MessageBox.Show("请先选择某一功能！");
                return;
            }
            //if (MessageBox.Show("此操作将删除原先的可视性设置，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Cancel) return;

            DataTable parentVisible = DBOpt.dbHelper.GetDataTable("select ROLE_ID from DMIS_SYS_TREEMENU_ROLE_VISIBLE where MODULE_ID=" + trvTreeMenu.SelectedNode.Tag.ToString());
            if (parentVisible == null || parentVisible.Rows.Count == 0) return; //父菜单没有设置可视性则不处理
            DataTable childs = DBOpt.dbHelper.GetDataTable("select ID from DMIS_SYS_TREEMENU where PARENT_ID=" + trvTreeMenu.SelectedNode.Tag.ToString()+" order by ORDER_ID");
            for (int i = 0; i < childs.Rows.Count; i++)
            {
                _sql = "delete from DMIS_SYS_TREEMENU_ROLE_VISIBLE where MODULE_ID=" + childs.Rows[i][0].ToString();  //先删除原先的权限设置
                DBOpt.dbHelper.ExecuteSql(_sql);
                for (int j = 0; j < parentVisible.Rows.Count; j++)
                {
                    _sql = "insert into DMIS_SYS_TREEMENU_ROLE_VISIBLE(MODULE_ID,ROLE_ID) values(" + childs.Rows[i][0].ToString() + "," + parentVisible.Rows[j][0].ToString() + ")";
                    DBOpt.dbHelper.ExecuteSql(_sql);
                }
            }

        }






    }
}
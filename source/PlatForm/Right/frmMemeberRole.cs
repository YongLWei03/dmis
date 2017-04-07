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
    public partial class frmMemeberRole : Form
    {

        private string _sql;
        private DataTable _dt;

        public frmMemeberRole()
        {
            InitializeComponent();
        }

        private void frmMemeberRole_Load(object sender, EventArgs e)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _dt = DBOpt.dbHelper.GetDataTable("select ID,NAME,SUPERIOR_ID from DMIS_SYS_DEPART order by ORDER_ID");
            else
                _dt = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR NAME,SUPERIOR_ID from DMIS_SYS_DEPART order by ORDER_ID");
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
                        TreeNode tmp = new TreeNode(_dt.Rows[i]["NAME"].ToString());
                        tmp.Tag = Int32.Parse(_dt.Rows[i]["ID"].ToString());
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
                        TreeNode tmp = new TreeNode(_dt.Rows[i]["NAME"].ToString());
                        tmp.Tag = Int32.Parse(_dt.Rows[i]["ID"].ToString());
                        tn.Nodes.Add(tmp);
                    }
                }
                for (i = 0; i < tn.Nodes.Count; i++)
                {
                    BuildTree(tn.Nodes[i]);
                }
            }
        }

        private void trvDepart_AfterSelect(object sender, TreeViewEventArgs e)
        {
            initMemeber(e.Node.Tag.ToString());
            lsbHasRols.Items.Clear();
            lsbOtherRoles.Items.Clear();
        }

        private void initMemeber(string departId)
        {
            lsvMemeber.Items.Clear();
            ListViewItem lv;
            DataTable dt = DBOpt.dbHelper.GetDataTable("select ID,CODE,NAME,FLAG,ORDER_ID from DMIS_SYS_MEMBER where DEPART_ID=" + departId + " order by ORDER_ID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(dt.Rows[i][0].ToString());

                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != null) lv.SubItems.Add(dt.Rows[i][j].ToString());
                }
                lsvMemeber.Items.Add(lv);
            }
            CMix.SetListViewAlternatingBackColor(lsvMemeber, Color.SkyBlue, Color.Lime);
        }


        private void lsMemeber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvMemeber.SelectedItems.Count < 1) return;

            //
            //填充所选操作员拥有的岗位
            //
            lsbHasRols.Items.Clear();
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select a.ROLE_ID,b.NAME from DMIS_SYS_MEMBER_ROLE a,DMIS_SYS_ROLE b where a.ROLE_ID=b.ID and a.MEMBER_ID=" + lsvMemeber.SelectedItems[0].Text;
            else
                _sql = "select a.ROLE_ID,b.OTHER_LANGUAGE_DESCR from DMIS_SYS_MEMBER_ROLE a,DMIS_SYS_ROLE b where a.ROLE_ID=b.ID and a.MEMBER_ID=" + lsvMemeber.SelectedItems[0].Text;

            System.Data.Common.DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                lsbHasRols.Items.Add(dr[1].ToString()+"("+dr[0].ToString()+")");
            }
           dr.Close();

           //
           //填充所选操作员没有的岗位
           //

           lsbOtherRoles.Items.Clear();
           _sql ="select * from DMIS_SYS_ROLE where ID not in (select ROLE_ID from DMIS_SYS_MEMBER_ROLE where MEMBER_ID="+lsvMemeber.SelectedItems[0].Text+")";
           dr = DBOpt.dbHelper.GetDataReader(_sql);
           while (dr.Read())
           {
               lsbOtherRoles.Items.Add(dr["OTHER_LANGUAGE_DESCR"].ToString() + "(" + dr["ID"].ToString() + ")");
           }
           dr.Close();
        }

 
        private void lsbHasRols_DragDrop(object sender, DragEventArgs e)
        {
            //取出岗位编号,拖放数据的格式是：岗位名称(岗位编号)
            string data, roleID;
            data = (string)e.Data.GetData(typeof(string));
            roleID = data.Substring(data.IndexOf('(') + 1, data.IndexOf(')') - data.IndexOf('(')-1);
            _sql = "insert into DMIS_SYS_MEMBER_ROLE(MEMBER_ID,ROLE_ID) values(" + lsvMemeber.SelectedItems[0].Text + "," + roleID + ")";
            if (DBOpt.dbHelper.ExecuteSql(_sql) > -1)
            {
                //lsbHasRols.Items.Add(data);
                lsMemeber_SelectedIndexChanged(null, null);
            }
            //else
            //{
            //    Log.InsertLog("更新", "失败", _sql);
            //    MessageBox.Show(this, "授权失败，已经记录失败信息！");
            //}
        }

        private void lsbOtherRoles_MouseDown(object sender, MouseEventArgs e)
        {
            if (lsbOtherRoles.SelectedItems.Count != 1)
            {
                //MessageBox.Show(this, "请先选择要设置岗位的操作员！");
                return;
            }
            
            DoDragDrop(lsbOtherRoles.SelectedItem.ToString(), DragDropEffects.Copy);
        }

        private void lsbHasRols_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lsbHasRols_MouseDown(object sender, MouseEventArgs e)
        {
            if (lsbHasRols.SelectedItems.Count != 1)
            {
                //MessageBox.Show(this, "请先选择要设置岗位的操作员！");
                return;
            }
            DoDragDrop(lsbHasRols.SelectedItem.ToString(), DragDropEffects.Copy);
        }

        private void lsbOtherRoles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lsbOtherRoles_DragDrop(object sender, DragEventArgs e)
        {
            //取出岗位编号,拖放数据的格式是：岗位名称(岗位编号)
            string data, roleID;
            data = (string)e.Data.GetData(typeof(string));
            roleID = data.Substring(data.IndexOf('(') + 1, data.IndexOf(')') - data.IndexOf('(')-1);
            _sql = "delete from  DMIS_SYS_MEMBER_ROLE where MEMBER_ID=" + lsvMemeber.SelectedItems[0].Text + " and ROLE_ID=" + roleID ;
            if (DBOpt.dbHelper.ExecuteSql(_sql) > -1)
            {
                lsMemeber_SelectedIndexChanged(null, null);
            }
            //lsbHasRols.Items.Remove(lsbHasRols.SelectedItem);
        }
    }
}
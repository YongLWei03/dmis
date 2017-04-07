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
    public partial class frmRoleMemeber : Form
    {
        private string _sql;
        private DataTable _dt;

        public frmRoleMemeber()
        {
            InitializeComponent();
        }

        private void frmRoleMemeber_Load(object sender, EventArgs e)
        {

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select ID,NAME from DMIS_SYS_ROLE order by ID";
            else
                _sql = "select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_ROLE order by ID";

            _dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                TreeNode td = new TreeNode();
                td.Tag=_dt.Rows[i][0].ToString();
                td.Text = _dt.Rows[i][1].ToString();
                trvRole.Nodes.Add(td);
            }
        }

        private void trvRole_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvRole.SelectedNode == null) return;

            initHasMemeber(trvRole.SelectedNode.Tag.ToString());
            initOtherMemeber(trvRole.SelectedNode.Tag.ToString());
        }

        private void initHasMemeber(string roleID)
        {
            lsvHasMember.Items.Clear();
            ListViewItem lv;

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select MEMBER_ID,MEMBER_NAME,DEPART_NAME from DMIS_VIEW_DEPART_MEMBER_ROLE where ROLE_ID=" + roleID + " order by DEPART_ID";
            else
                _sql = "select MEMBER_ID,MEMBER_NAME,OTHER_LANGUAGE_DESCR from DMIS_VIEW_DEPART_MEMBER_ROLE where ROLE_ID=" + roleID + " order by DEPART_ID";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                lv = new ListViewItem(dr["MEMBER_ID"].ToString());
                lv.SubItems.Add(dr["MEMBER_NAME"].ToString());
                lv.SubItems.Add(dr[2].ToString());
                lsvHasMember.Items.Add(lv);
            }
            dr.Close();
        }

        private void initOtherMemeber(string roleID)
        {
            lsvOtherMember.Items.Clear();
            ListViewItem lv;

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select MEMBER_ID,MEMBER_NAME,DEPART_NAME from DMIS_VIEW_DEPART_MEMBER where MEMBER_ID not in " +
                           " ( select MEMBER_ID from DMIS_VIEW_DEPART_MEMBER_ROLE where ROLE_ID=" + roleID + ")" + " order by DEPART_ID";
            else
                _sql = "select MEMBER_ID,MEMBER_NAME,OTHER_LANGUAGE_DESCR from DMIS_VIEW_DEPART_MEMBER where MEMBER_ID not in " +
                           " ( select MEMBER_ID from DMIS_VIEW_DEPART_MEMBER_ROLE where ROLE_ID=" + roleID + ")" + " order by DEPART_ID";

            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            while (dr.Read())
            {
                lv = new ListViewItem(dr["MEMBER_ID"].ToString());
                lv.SubItems.Add(dr["MEMBER_NAME"].ToString());
                lv.SubItems.Add(dr[2].ToString());
                lsvOtherMember.Items.Add(lv);
            }
            dr.Close();
        }

        private void lsvOtherMember_MouseDown(object sender, MouseEventArgs e)
        {
            if (trvRole.SelectedNode == null)
            {
                MessageBox.Show(this,Main.Properties.Resources.SelectItem );//"请先选择要设置的岗位！"
                return;
            }
            if(lsvOtherMember.SelectedItems.Count==1)
                    DoDragDrop(lsvOtherMember.SelectedItems[0].Text, DragDropEffects.Copy);
        
            
        }

        private void lsvHasMember_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lsvHasMember_DragDrop(object sender, DragEventArgs e)
        {
            string memberID, roleID;
            memberID = (string)e.Data.GetData(typeof(string));
            roleID = trvRole.SelectedNode.Tag.ToString();
            _sql = "insert into DMIS_SYS_MEMBER_ROLE(MEMBER_ID,ROLE_ID) values(" + memberID + "," + roleID + ")";
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
            {
                initHasMemeber(roleID);
                lsvOtherMember.Items.Remove(lsvOtherMember.SelectedItems[0]);
            }

            
        }

        private void lsvHasMember_MouseDown(object sender, MouseEventArgs e)
        {
            if (trvRole.SelectedNode == null)
            {
                MessageBox.Show(this,Main.Properties.Resources.SelectItem );//"请先选择要设置的岗位！"
                return;
            }
            if (lsvHasMember.SelectedItems.Count == 1)
                DoDragDrop(lsvHasMember.SelectedItems[0].Text, DragDropEffects.Copy);
        }


        private void lsvOtherMember_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }


        private void lsvOtherMember_DragDrop(object sender, DragEventArgs e)
        {
            string memberID, roleID;
            memberID = (string)e.Data.GetData(typeof(string));
            roleID = trvRole.SelectedNode.Tag.ToString();
            _sql = "delete from DMIS_SYS_MEMBER_ROLE where MEMBER_ID=" + memberID + " and ROLE_ID= "+roleID;
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
            {
                initOtherMemeber(roleID);
                lsvHasMember.Items.Remove(lsvHasMember.SelectedItems[0]);
            }


        }
    }
}
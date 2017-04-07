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
    public partial class frmFlowRight : Form
    {

        ListViewItem li = new ListViewItem();
        string strSql;

        public frmFlowRight()
        {
            InitializeComponent();
        }

        private void lsvRight_Click(object sender, EventArgs e)
        {
            if (lsvRight.SelectedItems.Count < 1) return;
            li = lsvRight.SelectedItems[0];
            cbbRole.Text = li.Text;
            ParseRight(li.SubItems[1].Tag.ToString());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {       
            string sTmp1="",sTmp2="";
            GetRight(ref sTmp1,ref sTmp2);
            if(frmFlow.iDocNo>0){
                //文档权限

                ListViewItem lv=lsvRight.FindItemWithText(cbbRole.Text);
                if (lv != null) { 
                    lv.SubItems[1].Text = sTmp1; 
                    lv.SubItems[1].Tag = sTmp2;
                    strSql = "UPDATE DMIS_SYS_RIGHTS SET F_ACCESS='" + sTmp2 
                        + "' WHERE F_FOREIGNKEY=" + frmFlow.iDocNo 
                        + " AND F_CATGORY='文档' AND F_ROLENO=" + cbbRole.SelectedValue;
                    DBOpt.dbHelper.ExecuteSql(strSql);

                }
                else {
                    lv = new ListViewItem();
                    lv.Text = cbbRole.Text;
                    ListViewItem.ListViewSubItem lsv = new ListViewItem.ListViewSubItem();
                    lsv.Text = sTmp1;
                    lsv.Tag = sTmp2;
                    lv.SubItems.Add(lsv);
                    lsvRight.Items.Add(lv);
                    li = lv;
                    uint iMax = 1;
                    iMax = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_RIGHTS", "F_NO");
                    strSql = "INSERT INTO DMIS_SYS_RIGHTS VALUES(" + iMax +","
                        + frmFlow.iDocNo+",'文档',"+cbbRole.SelectedValue+",'"+sTmp2+"')";
                    DBOpt.dbHelper.ExecuteSql(strSql);
                }
            
            }
            else{
                //业务权限
                ListViewItem lv = lsvRight.FindItemWithText(cbbRole.Text);
                if (lv != null)
                {
                    lv.SubItems[1].Text = sTmp1;
                    lv.SubItems[1].Tag = sTmp2;
                    strSql = "UPDATE DMIS_SYS_RIGHTS SET F_ACCESS='" + sTmp2
                        + "' WHERE F_FOREIGNKEY=" + frmFlow.iPackNo
                        + " AND F_CATGORY='业务' AND F_ROLENO=" + cbbRole.SelectedValue;
                    DBOpt.dbHelper.ExecuteSql(strSql);

                }
                else
                {
                    lv = new ListViewItem();
                    lv.Text = cbbRole.Text;
                    ListViewItem.ListViewSubItem lsv = new ListViewItem.ListViewSubItem();
                    lsv.Text = sTmp1;
                    lsv.Tag = sTmp2;
                    lv.SubItems.Add(lsv);
                    lsvRight.Items.Add(lv);
                    li = lv;
                    uint iMax = 1;
                    iMax = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_RIGHTS", "F_NO");
                    strSql = "INSERT INTO DMIS_SYS_RIGHTS VALUES(" + iMax + ","
                        + frmFlow.iPackNo + ",'业务'," + cbbRole.SelectedValue + ",'" + sTmp2 + "')";
                    DBOpt.dbHelper.ExecuteSql(strSql);
                }
            }
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lsvRight.SelectedItems.Count < 1) return;
            ListViewItem lv = lsvRight.SelectedItems[0];
            if (frmFlow.iDocNo >= 0)
            {
                //文档权限
                    strSql = "DELETE FROM DMIS_SYS_RIGHTS WHERE F_FOREIGNKEY=" + frmFlow.iDocNo
                        + " AND F_CATGORY='文档' AND F_ROLENO=" + cbbRole.SelectedValue;
                    DBOpt.dbHelper.ExecuteSql(strSql);
            }
            else
            {
                //业务权限
                    strSql = "DELETE FROM DMIS_SYS_RIGHTS WHERE F_FOREIGNKEY=" + frmFlow.iPackNo
                        + " AND F_CATGORY='业务' AND F_ROLENO=" + cbbRole.SelectedValue;
                    DBOpt.dbHelper.ExecuteSql(strSql);
            }
            lsvRight.Items.Remove(lsvRight.SelectedItems[0]);
            li = new ListViewItem();

        }
        /// <summary>
        /// 保存前，得到勾选的权限内容
        /// </summary>
        /// <param name="sRightName"></param>
        /// <param name="sRightValue"></param>
        private void GetRight(ref string sRightName, ref string  sRightValue)
        {
            string sTmp = "0000000";
            string sTmp1 = "";
            if (chkRight1.Checked) { sTmp = "1000000"; sTmp1 =  "," +chkRight1.Text; }
            if (chkRight2.Checked) { sTmp = sTmp.Substring(0, 1) + "1" + "00000"; sTmp1 += "," + chkRight2.Text; }
            if (chkRight3.Checked) { sTmp = sTmp.Substring(0, 2) + "1" + "0000"; sTmp1 += "," + chkRight3.Text; }
            if (chkRight4.Checked) { sTmp = sTmp.Substring(0, 3) + "1" + "000"; sTmp1 += "," + chkRight4.Text; }
            if (chkRight5.Checked) { sTmp = sTmp.Substring(0, 4) + "1" + "00"; sTmp1 += "," + chkRight5.Text; }
            if (chkRight6.Checked) { sTmp = sTmp.Substring(0, 5) + "1" + "0"; sTmp1 += "," + chkRight6.Text; }
            if (chkRight7.Checked) { sTmp = sTmp.Substring(0, 6) + "1"; sTmp1 += "," + chkRight7.Text; }
            sRightValue=sTmp;
            if (sTmp1 != "")
                sTmp1 = sTmp1.Substring(1);
            sRightName = sTmp1;
        }
        /// <summary>
        /// 解析权限，重设检查框
        /// </summary>
        /// <param name="sValue"></param>
        private void ParseRight(string sValue) {
            if (sValue == "") sValue = "0000000";
            if (sValue.Substring(0, 1) == "1") chkRight1.Checked = true;
            else chkRight1.Checked = false;
            if (sValue.Substring(1, 1) == "1") chkRight2.Checked = true;
            else chkRight2.Checked = false;
            if (sValue.Substring(2, 1) == "1") chkRight3.Checked = true;
            else chkRight3.Checked = false;
            if (sValue.Substring(3, 1) == "1") chkRight4.Checked = true;
            else chkRight4.Checked = false;
            if (sValue.Substring(4, 1) == "1") chkRight5.Checked = true;
            else chkRight5.Checked = false;
            if (sValue.Substring(5, 1) == "1") chkRight6.Checked = true;
            else chkRight6.Checked = false;
            if (sValue.Substring(6, 1) == "1") chkRight7.Checked = true;
            else chkRight7.Checked = false;
        }

        /// <summary>
        /// 从数据库中取值，解析成汉字权限
        /// </summary>
        /// <param name="sValue"></param>
        private void DbGetRight(ref string sValue) {
            if (sValue == "" || sValue.Length<7) return;
            string sTmp = sValue;
            string sTmp1 = "";
            if ( sTmp.Substring(0, 1)=="1") { sTmp1 = "," + chkRight1.Text; }
            if (sTmp.Substring(1, 1) == "1") {sTmp1 += "," + chkRight2.Text; }
            if (sTmp.Substring(2, 1) == "1") {sTmp1 += "," + chkRight3.Text; }
            if (sTmp.Substring(3, 1) == "1") { sTmp1 += "," + chkRight4.Text; }
            if (sTmp.Substring(4, 1) == "1") {sTmp1 += "," + chkRight5.Text; }
            if (sTmp.Substring(5, 1) == "1") {sTmp1 += "," + chkRight6.Text; }
            if (sTmp.Substring(6, 1) == "1") {sTmp1 += "," + chkRight7.Text; }
            if (sTmp1 != "")
                sTmp1 = sTmp1.Substring(1);
            sValue = sTmp1;
        }


        /// <summary>
        /// 填充控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFlowRight_Load(object sender, EventArgs e)
        {
            cbbRole.Items.Clear();
            lsvRight.Items.Clear();
            strSql = "select ID,NAME from DMIS_SYS_ROLE order by ID";
            DataTable dt1, dt2;
            ListViewItem lv;
            dt1 = DBOpt.dbHelper.GetDataTable(strSql);
            cbbRole.DataSource = dt1;
            cbbRole.DisplayMember = "NAME";
            cbbRole.ValueMember = "ID";
            cbbRole.Refresh();
            if (frmFlow.iDocNo > 0) { 
                //提取权限文档
                strSql = "SELECT A.NAME,B.F_ACCESS FROM DMIS_SYS_ROLE A,DMIS_SYS_RIGHTS B"
                    + " WHERE A.ID=B.F_ROLENO AND B.F_CATGORY='文档' AND F_FOREIGNKEY="+ frmFlow.iDocNo;
                pan_pack.Visible = false;
            }
            else {
                //提取权限业务
                strSql = "SELECT A.NAME,B.F_ACCESS FROM DMIS_SYS_ROLE A,DMIS_SYS_RIGHTS B"
                    + " WHERE A.ID=B.F_ROLENO AND B.F_CATGORY='业务' AND F_FOREIGNKEY=" + frmFlow.iPackNo;            
            }
            dt2 = DBOpt.dbHelper.GetDataTable(strSql);
            if (dt2.Rows.Count > 0) 
                for(int j=0;j<dt2.Rows.Count;j++)
                {
                    lv = new ListViewItem();
                    lv.Text =FieldToValue.FieldToString(dt2.Rows[j][0]);
                    ListViewItem.ListViewSubItem lsv = new ListViewItem.ListViewSubItem();
                    string sTmp1 = FieldToValue.FieldToString(dt2.Rows[j][1]);
                    lsv.Tag = sTmp1;
                    DbGetRight(ref sTmp1);
                    lsv.Text = sTmp1;
                    lv.SubItems.Add(lsv);
                    lsvRight.Items.Add(lv);
                }
        }

   

    }
}
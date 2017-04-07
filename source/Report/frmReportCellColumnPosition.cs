using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using System.Data.Common;
using PlatForm.Functions;


namespace PlatForm.DmisReport
{
    public partial class frmReportCellColumnPosition : Form
    {
        string _sql;

        public frmReportCellColumnPosition()
        {
            InitializeComponent();
        }

        private void frmReportCellColumnPosition_Load(object sender, EventArgs e)
        {
            initTree();
        }

        private void initTree()
        {
            trvReport.Nodes.Clear();
            
            DataTable dt;
            DataTable dtChild;

            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                dt = DBOpt.dbHelper.GetDataTable("select ID,NAME from DMIS_SYS_REPORT_TYPE order by ORDER_ID");
            else
                dt = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_REPORT_TYPE order by ORDER_ID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode td = new TreeNode();
                td.Text = dt.Rows[i][1].ToString();
                td.Tag = dt.Rows[i][0];
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                    dtChild = DBOpt.dbHelper.GetDataTable("select ID,NAME from DMIS_SYS_REPORT where TYPE_ID=" + dt.Rows[i][0].ToString() + " order by ORDER_ID");
                else
                    dtChild = DBOpt.dbHelper.GetDataTable("select ID,OTHER_LANGUAGE_DESCR from DMIS_SYS_REPORT where TYPE_ID=" + dt.Rows[i][0].ToString() + " order by ORDER_ID");

                for (int j = 0; j < dtChild.Rows.Count; j++)
                {
                    TreeNode tdChild = new TreeNode();
                    tdChild.Text = dtChild.Rows[j][1].ToString();
                    tdChild.Tag = dtChild.Rows[j][0];
                    tdChild.ImageIndex = 2;
                    tdChild.SelectedImageIndex = 1;
                    td.Nodes.Add(tdChild);
                }
                trvReport.Nodes.Add(td);
            }
        }

        private void trvReport_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvReport.SelectedNode == null) return;
            if (trvReport.SelectedNode.Level != 1) return;

            trvTable.Nodes.Clear();
            _sql = "select ID,TABLE_NAME from DMIS_SYS_REPORT_TABLE where REPORT_ID=" + trvReport.SelectedNode.Tag.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode td = new TreeNode();
                td.Tag = dt.Rows[i][0];
                td.Text = dt.Rows[i][1].ToString();
                trvTable.Nodes.Add(td);
            }

            //ȡ����ģ���ļ���
            _sql = "select FILE_NAME from DMIS_SYS_REPORT where ID=" + trvReport.SelectedNode.Tag.ToString();
            object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            int ret;
            if (obj != null)
            {
                ret = m_cell.OpenFile(obj.ToString(), "");
                if (ret != 1)
                {
                    switch (ret)
                    {
                        case -1:
                            MessageBox.Show("error number:-1");//"�ļ������ڣ�"
                            break;
                        case -2:
                            MessageBox.Show("error number:-2");//"�ļ���������"
                            break;
                        case -3:
                            MessageBox.Show("error number:-3");//"�ļ���ʽ����"
                            break;
                        case -4:
                            MessageBox.Show("error number:-4");//"�������"
                            break;
                        case -5:
                            MessageBox.Show("error number:-5");//"���ܴ򿪸߰汾�ļ���"
                            break;
                        case -6:
                            MessageBox.Show("error number:-6");//"���ܴ��ض��汾�ļ���"
                            break;
                        default:
                            break;
                    }
                    m_cell.ResetContent();
                }
            }

            //���'��λ��'tabpageҳ�е���������
            lblTableName.Text = "";
            lsvColumn.Items.Clear();

        }

        private void trvTable_DoubleClick(object sender, EventArgs e)
        {
            if (trvTable.SelectedNode == null) return;

            initColumns();
            tabControl1.SelectedTab = tabPage2;
            lblTableName.Text = trvTable.SelectedNode.Text;
        }

        private void initColumns()
        {
            if (trvReport.SelectedNode == null) return;
            if (trvTable.SelectedNode == null) return;

            lsvColumn.Items.Clear();
            ListViewItem lv;
            _sql = "select ID,COLUMN_DESCR,R,C,P from DMIS_SYS_REPORT_CELL_COLUMN where REPORT_ID=" + trvReport.SelectedNode.Tag.ToString() + " and TABLE_ID=" + trvTable.SelectedNode.Tag.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(dt.Rows[i][0].ToString());
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != null) lv.SubItems.Add(dt.Rows[i][j].ToString());
                }
                lsvColumn.Items.Add(lv);
            }
        }

        private void m_cell_MouseDClick(object sender, AxCELL50Lib._DCell2000Events_MouseDClickEvent e)
        {
            if (lsvColumn.SelectedIndices.Count != 1) return;

            string id;
            int p = 0;
            p = m_cell.GetCurSheet();
            id = lsvColumn.SelectedItems[0].Text;
            _sql = "update DMIS_SYS_REPORT_CELL_COLUMN set R=" + e.row.ToString() + ",C=" + e.col.ToString() + ",P=" + p.ToString() + " where ID=" + id;
            DBOpt.dbHelper.ExecuteSql(_sql);
            initColumns();
        }

        private void cbbZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbZoom.SelectedIndex < 0) return;
            double temp = Convert.ToDouble(cbbZoom.Text.Substring(0, cbbZoom.Text.Length - 1));
            double zoom = temp / 100f;
            m_cell.SetScreenScale(0, zoom);
        }

        private void tlbDisplay_Click(object sender, EventArgs e)
        {
            if (trvReport.SelectedNode == null) return;
            if (trvReport.SelectedNode.Level != 1) return;
            _sql = "select ID,TABLE_NAME from DMIS_SYS_REPORT_TABLE where REPORT_ID=" + trvReport.SelectedNode.Tag.ToString() + " order by ORDER_ID";
            DataTable dtTable = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                _sql = "select ID,COLUMN_DESCR,R,C,P from DMIS_SYS_REPORT_CELL_COLUMN where REPORT_ID=" + trvReport.SelectedNode.Tag.ToString() + " and TABLE_ID=" + dtTable.Rows[i][0].ToString() + " order by ORDER_ID";
                DataTable dtColumn = DBOpt.dbHelper.GetDataTable(_sql);
                for (int j = 0; j < dtColumn.Rows.Count; j++)
                {
                    if (dtColumn.Rows[j][1] == Convert.DBNull) continue;
                    if (dtColumn.Rows[j][2] == Convert.DBNull || dtColumn.Rows[j][3] == Convert.DBNull || dtColumn.Rows[j][4] == Convert.DBNull) continue;  //�кš��кź�ҳ��û������ʱ�����������
                    m_cell.S(Convert.ToInt16(dtColumn.Rows[j][3]), Convert.ToInt16(dtColumn.Rows[j][2]), Convert.ToInt16(dtColumn.Rows[j][4]), dtColumn.Rows[j][1].ToString());
                    //������ɫʱ,���ò���,��ԭ��
                    //int redColor = System.Drawing.Color.Red.ToArgb();
                    //m_cell.SetCellTextColor(Convert.ToInt16(dtColumn.Rows[j][3]), Convert.ToInt16(dtColumn.Rows[j][2]), Convert.ToInt16(dtColumn.Rows[j][4]), redColor);  //����������ɫ
                    m_cell.SetCellFontStyle(Convert.ToInt16(dtColumn.Rows[j][3]), Convert.ToInt16(dtColumn.Rows[j][2]), Convert.ToInt16(dtColumn.Rows[j][4]), 6);  //����+б��
                }
            }
        }

        private void tlbHide_Click(object sender, EventArgs e)
        {
            if (trvReport.SelectedNode == null) return;
            if (trvReport.SelectedNode.Level != 1) return;
            _sql = "select ID,TABLE_NAME from DMIS_SYS_REPORT_TABLE where REPORT_ID=" + trvReport.SelectedNode.Tag.ToString() + " order by ORDER_ID";
            DataTable dtTable = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                _sql = "select ID,COLUMN_DESCR,R,C,P from DMIS_SYS_REPORT_CELL_COLUMN where REPORT_ID=" + trvReport.SelectedNode.Tag.ToString() + " and TABLE_ID=" + dtTable.Rows[i][0].ToString() + " order by ORDER_ID";
                DataTable dtColumn = DBOpt.dbHelper.GetDataTable(_sql);
                for (int j = 0; j < dtColumn.Rows.Count; j++)
                {
                    if (dtColumn.Rows[j][1] == Convert.DBNull) continue;
                    if (dtColumn.Rows[j][2] == Convert.DBNull || dtColumn.Rows[j][3] == Convert.DBNull || dtColumn.Rows[j][4] == Convert.DBNull) continue;  //�кš��кź�ҳ��û������ʱ�����������
                    m_cell.S(Convert.ToInt16(dtColumn.Rows[j][3]), Convert.ToInt16(dtColumn.Rows[j][2]), Convert.ToInt16(dtColumn.Rows[j][4]), "");
                }
            }
            m_cell.Invalidate();
        }

        private void lsvColumn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (m_cell.GetCurrentCol() < 0) return;
            if (m_cell.GetCurrentRow() < 0) return;
            string r, c, p;
            r = m_cell.GetCurrentRow().ToString();
            c = m_cell.GetCurrentCol().ToString();
            p = m_cell.GetCurSheet().ToString();

            string id;
            id = lsvColumn.SelectedItems[0].Text;
            //ȷ��Ҫ�޸ģ�
            if (MessageBox.Show(Reports.Properties.Resources.ModifyBeforeConfirm, Reports.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            _sql = "update DMIS_SYS_REPORT_CELL_COLUMN set R=" + r + ",C=" + c + ",P=" + p + " where ID=" + id;
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
            {
                lsvColumn.SelectedItems[0].SubItems[2].Text=r;
                lsvColumn.SelectedItems[0].SubItems[3].Text=c;
                lsvColumn.SelectedItems[0].SubItems[4].Text=p;
            }
            //initColumns();
        }








    }
}
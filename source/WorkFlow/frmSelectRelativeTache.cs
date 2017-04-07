using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using PlatForm.DBUtility;
using PlatForm.Functions;

namespace PlatForm.WorkFlow
{
    public partial class frmSelectRelativeTache : Form
    {
        public int NodeID;
        public int PackTypeID;
        private string _sql;
        private ArrayList nodes;
        public string node;
        public frmSelectRelativeTache()
        {
            InitializeComponent();
        }

        private void frmSelectRelativeTache_Load(object sender, EventArgs e)
        {
            //找到本节点之前的所有节点
            nodes = new ArrayList();
            FindPreNode(NodeID);
            foreach (object obj in nodes)
                cbbRelativeTache.Items.Add(obj.ToString());
        }

        private void FindPreNode(int nodeID)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                _sql = "select b.F_NAME,b.f_flowcat,a.f_startno from dmis_sys_flowline a,dmis_sys_flowlink b where a.f_startno=b.f_no and a.f_packtypeno=" + PackTypeID + " and a.f_endno=" + nodeID;
            else
                _sql = "select b.OTHER_LANGUAGE_DESCR,b.f_flowcat,a.f_startno from dmis_sys_flowline a,dmis_sys_flowlink b where a.f_startno=b.f_no and a.f_packtypeno=" + PackTypeID + " and a.f_endno=" + nodeID;
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            if (dt.Rows.Count == 1 && dt.Rows[0][1].ToString() == "0")
            {
                if (!nodes.Contains(dt.Rows[0][0])) nodes.Add(dt.Rows[0][0]);
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)   //先加节点
                {
                    if (nodes.Contains(dt.Rows[i][0])) continue;
                    nodes.Add(dt.Rows[i][0]);
                }
                
                for (int i = 0; i < dt.Rows.Count; i++)   //再扫描
                    FindPreNode(Convert.ToInt16(dt.Rows[i][2]));
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbbRelativeTache.SelectedItem==null)
            {
                //MessageBox.Show("要选择某一个节点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _sql = "select f_no from dmis_sys_flowlink where f_packtypeno=" + PackTypeID + " and f_name='" + cbbRelativeTache.Text + "'";
            node = DBOpt.dbHelper.ExecuteScalar(_sql).ToString();
            this.DialogResult = DialogResult.OK;
        }


    }
}
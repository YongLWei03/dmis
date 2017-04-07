using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MindFusion.FlowChartX;

using PlatForm.DBUtility;
using System.Data.Common;
using PlatForm.Functions;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;


namespace PlatForm.WorkFlow
{
    public partial class frmFlow : Form
    {
        public static int iPackNo, iDocNo;//所选择的业务编号、文档编号
        public static string sPackName, sDocName;//所选择的业务名、文档名
        private int iNodeId = -1;//环节节点编号
        private int iCurrOper = -1;
        private int iLineId = -1;  //连接编号
        private string strSql = "",sselTableName="", iselRoleNo="";
        Box fcNode;
        Arrow fcLine;
        bool bChangeType = false,bFlow=false;
        enum eFlowOper { selObj=0,startNode, midNode,endNode,descNode,mainLine,auxLine};
        string[] tsb = new string[6] { "tbStart", "tbNode", "tbEnd", "tbNote", "tbLine", "tbLine1" };


        public frmFlow()
        {
            
            InitializeComponent();
            //初始化变量
            this.tbRedo.Visible = false;
            this.tbUndo.Visible = false;
            fc_Flow.UndoManager.UndoEnabled = true;
            iPackNo = iDocNo =- 1;
            sPackName = sDocName = "";
        }

        private void frmFlow_Load(object sender, EventArgs e)
        {
            initForm();
            initPara();
            initNodeType(); ///2008-11 ayf　珠海
        }


        /// <summary>
        /// 设置按钮按下时的状态
        /// </summary>
        /// <param name="toolButtonName">按下的工具栏按钮名</param>
        private void setToolButtonStatus(string toolButtonName)
        {
            ToolStripButton tsb1;

            for (int i = 0; i < tsb.Length; i++)
            {
                tsb1 = (ToolStripButton)tbFlow.Items[tsb[i]];
                if (tsb[i] == toolButtonName)
                    tsb1.CheckState = CheckState.Checked;
                else
                    tsb1.CheckState = CheckState.Unchecked;
            }
        }


        /// <summary>
        /// 修改业务或文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void cm_pack_edit_Click(object sender, EventArgs e)
        {
            if (trvPack.SelectedNode == null) return;
            if (trvPack.SelectedNode.Parent != null) {
                  iDocNo = Convert.ToInt16(trvPack.SelectedNode.Tag.ToString());  //解决BUG_002
                  frmdoctype frm1 = new frmdoctype();
                  if (frm1.ShowDialog() == DialogResult.OK) 
                      trvPack.SelectedNode.Text = sDocName;
            }
            else {
                iPackNo = Convert.ToInt16(trvPack.SelectedNode.Tag.ToString());//解决BUG_002
                frmpacktype frm1 = new frmpacktype();
                if (frm1.ShowDialog() == DialogResult.OK)
                    trvPack.SelectedNode.Text = sPackName;            
            }
        }
        /// <summary>
        /// 删除业务或文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cm_pack_del_Click(object sender, EventArgs e)
        {
            if (trvPack.SelectedNode == null) return;
            if (trvPack.SelectedNode.Parent != null)
            {
                if (MessageBox.Show("是否要删除:" + trvPack.SelectedNode.Text,"提示",MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
                strSql = "SELECT F_TABLENAME FROM DMIS_SYS_DOCTYPE WHERE F_NO=" + iDocNo;
                string sTabName=DBOpt.dbHelper.ExecuteScalar(strSql).ToString();
                if (sTabName != "") {
                    strSql = "DELETE FROM DMIS_SYS_FLOWFIELDRIGHT WHERE F_PACKTYPENO=" + iPackNo + " AND F_TABLENAME='" + sTabName + "'";
                    DBOpt.dbHelper.ExecuteSql(strSql);
                }
                strSql = "DELETE FROM DMIS_SYS_DOCTYPE WHERE F_NO=" + iDocNo;
                DBOpt.dbHelper.ExecuteSql(strSql);
                trvPack.SelectedNode.Remove();
            }
            else
            {
                if (MessageBox.Show("是否要删除:" + trvPack.SelectedNode.Text, "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

                strSql = "DELETE FROM DMIS_SYS_RIGHTS WHERE F_FOREIGNKEY in(select F_NO from DMIS_SYS_DOCTYPE where F_PACKTYPENO=" + iPackNo
                    + ") AND F_CATGORY='文档'";
                DBOpt.dbHelper.ExecuteSql(strSql);
                strSql = "DELETE FROM DMIS_SYS_RIGHTS WHERE F_FOREIGNKEY in(select F_NO from DMIS_SYS_FLOWLINK where F_PACKTYPENO=" + iPackNo
                    + ") AND F_CATGORY='流程角色'";
                DBOpt.dbHelper.ExecuteSql(strSql);
                strSql = "DELETE FROM DMIS_SYS_RIGHTS WHERE F_FOREIGNKEY=" + iPackNo + " AND F_CATGORY='业务'";
                DBOpt.dbHelper.ExecuteSql(strSql);

                strSql = "DELETE FROM DMIS_SYS_DOCTYPE WHERE F_PACKTYPENO=" + iPackNo;
                DBOpt.dbHelper.ExecuteSql(strSql);
                strSql = "DELETE FROM DMIS_SYS_FLOWFIELDRIGHT WHERE F_PACKTYPENO=" + iPackNo;
                DBOpt.dbHelper.ExecuteSql(strSql);
                strSql = "DELETE FROM DMIS_SYS_FLOWLINK WHERE F_PACKTYPENO=" + iPackNo;
                DBOpt.dbHelper.ExecuteSql(strSql);
                strSql = "DELETE FROM DMIS_SYS_FLOWLINE WHERE F_PACKTYPENO=" + iPackNo;
                DBOpt.dbHelper.ExecuteSql(strSql);
                strSql = "DELETE FROM DMIS_SYS_PACKTYPE WHERE F_NO=" + iPackNo;
                DBOpt.dbHelper.ExecuteSql(strSql);
                trvPack.SelectedNode.Remove();
            }
        }

        /// <summary>
        /// 新增业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cm_new_pack_Click(object sender, EventArgs e)
        {
            frmpacktype frm1 = new frmpacktype();
            iPackNo = -1;
            if (frm1.ShowDialog() == DialogResult.OK)
            {
                    TreeNode tn1 = new TreeNode();
                    tn1.Text = sPackName;
                    tn1.Tag = iPackNo;
                    tn1.ImageIndex = 0;
                    tn1.SelectedImageIndex = 1;
                    trvPack.Nodes.Add(tn1);
                    trvPack.SelectedNode = tn1;
            }
        }

        /// <summary>
        /// 选择业务，新增文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cm_new_doc_Click(object sender, EventArgs e)
        {
            if (trvPack.SelectedNode == null) return;
            TreeNode tn11 = new TreeNode();
            if (trvPack.SelectedNode.Parent == null)
                tn11 = trvPack.SelectedNode;
            else 
                tn11 = trvPack.SelectedNode.Parent;

            iDocNo = -1;
            if (iPackNo == -1) tv_pack_AfterSelect(null, null);
            frmdoctype frm1 = new frmdoctype();
            if (frm1.ShowDialog() == DialogResult.OK)
            {
                TreeNode tn2 = new TreeNode();
                tn2.Text = sDocName;
                tn2.Tag = iDocNo;
                tn2.ImageIndex = 2;
                tn2.SelectedImageIndex = 3;
                tn11.Nodes.Add(tn2);
                trvPack.SelectedNode = tn2;
            }
        }


        /// <summary>
        /// 业务或文档的操作权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cm_pack_right_Click(object sender, EventArgs e)
        {
            if (trvPack.SelectedNode == null) return;
            frmFlowRight frm = new frmFlowRight();
            frm.ShowDialog();
        }

        private void tv_pack_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (trvPack.SelectedNode == null)
            {
                iPackNo = iDocNo = -1;
                sPackName = sDocName = "";
                return;
            }
            else if (trvPack.SelectedNode.Parent == null)
            {
                iCurrOper = -1;

                iPackNo = Convert.ToInt16(trvPack.SelectedNode.Tag.ToString());
                iDocNo = -1;
                sPackName = trvPack.SelectedNode.Text;
                sDocName = "";
                bChangeType = true;

                txtName.Text = "";
                tb_Cond.Text = "";
                txtDays.Text = "";
                lv_Field.Items.Clear();
                bFlow = FieldToValue.FieldToCheckBox(DBOpt.dbHelper.ExecuteScalar("select f_isflow from dmis_sys_packtype where f_no=" +iPackNo));
                if (bFlow)
                {
                    CreateFlow();
                    bChangeType = false;
                    this.panel3.Enabled = true;

                }
                else {
                    MessageBox.Show("非流程业务，不能操作流程设计");
                    this.panel3.Enabled = false;
                }
                return;
            }
            else {
                iPackNo = Convert.ToInt16(trvPack.SelectedNode.Parent.Tag.ToString());
                iDocNo = Convert.ToInt16(trvPack.SelectedNode.Tag.ToString());
                sPackName = trvPack.SelectedNode.Parent.Text;
                sDocName = trvPack.SelectedNode.Text;
                return;            
            }

        }

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fc_Flow_MouseDown(object sender, MouseEventArgs e)
        {
            float x,y;
            x=fc_Flow.ClientToDoc(new Point(e.X,e.Y)).X;
            y=fc_Flow.ClientToDoc(new Point(e.X,e.Y)).Y;
            object ob;
            uint iMax=0;
            if (iCurrOper > 0 && iCurrOper < 5) iMax = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_FLOWLINK", "F_NO");
            switch (iCurrOper) { 
                case 1:
                    strSql = "SELECT F_NO FROM DMIS_SYS_FLOWLINK WHERE F_PACKTYPENO=" + iPackNo + " AND F_FLOWCAT=0";
                    ob = DBOpt.dbHelper.ExecuteScalar(strSql);
                    if (ob != null)
                    {
                        //MessageBox.Show("对不起，起始环节只能存在一个！");
                        return;
                    }
                    
                    fcNode = fc_Flow.CreateBox(x,y, 10, 10);
                    fcNode.Text = WorkFlows.Properties.Resources.StartNode;  //"起始环节"
                    fcNode.Style = EBoxStyle.bsEllipse;
                    fcNode.FillColor = Color.Lime;// System.Drawing.ColorTranslator.FromWin32(192 + 255 * 256 + 192 * 65536);
                    fcNode.Tag = iMax;
                    fcNode.ToolTip = iMax.ToString();
                    CreateNode(fcNode, iMax);
                    iCurrOper = 0;
                    txtName.Text = fcNode.Text;
                    this.groupBox2.Enabled = true;
                    break;
                case 2:
                    fcNode = fc_Flow.CreateBox(x, y, 20, 10);
                    fcNode.Text = WorkFlows.Properties.Resources.MiddleNode;//"中间环节"
                    fcNode.Style = EBoxStyle.bsRect;
                    fcNode.FillColor =  System.Drawing.ColorTranslator.FromWin32(192 + 255 * 256 + 192 * 65536);
                    fcNode.Tag = iMax;
                    fcNode.ToolTip = iMax.ToString();
                    //iCurrOper = -1;可连续设置
                    txtName.Text = fcNode.Text;
                    CreateNode(fcNode, iMax);
                    this.groupBox2.Enabled = true;
                    break;
                case 3:
                    strSql = "SELECT F_NO FROM DMIS_SYS_FLOWLINK WHERE F_PACKTYPENO=" + iPackNo + " AND F_FLOWCAT=2";
                    ob = DBOpt.dbHelper.ExecuteScalar(strSql);
                    if (ob != null) 
                    {
                        //MessageBox.Show("对不起，结束环节只能存在一个！");
                        return;
                    }

                    fcNode = fc_Flow.CreateBox(x, y, 10, 10);
                    fcNode.Text = WorkFlows.Properties.Resources.EndNode;//"结束环节"
                    fcNode.Style = EBoxStyle.bsEllipse;
                    fcNode.FillColor = Color.DeepPink;
                    fcNode.Tag = iMax;
                    fcNode.ToolTip = iMax.ToString();
                    CreateNode(fcNode, iMax);
                    iCurrOper = 0;
                    txtName.Text = fcNode.Text;
                    this.groupBox2.Enabled = true;
                    break;
                case 4:
                    fcNode = fc_Flow.CreateBox(x, y, 20, 10);
                    fcNode.Text = WorkFlows.Properties.Resources.Remarks;//"备注"
                    fcNode.Style = EBoxStyle.bsRect;
                    fcNode.Transparent = true;
                    fcNode.FillColor = Color.Transparent;// System.Drawing.ColorTranslator.FromWin32(192 + 255 * 256 + 192 * 65536);
                    fcNode.Tag = iMax;
                    fcNode.ToolTip = iMax.ToString();
                    txtName.Text = fcNode.Text;
                    CreateNode(fcNode, iMax);
                    this.groupBox2.Enabled = false;
                    //iCurrOper = -1;可连续设置
                    break;
           }
        }

        //删除连线前确认一下
        private void fc_Flow_ArrowDeleting(object sender, ArrowConfirmArgs e)
        {
            //"确定要删除连线?"
            if (MessageBox.Show(WorkFlows.Properties.Resources.DeleteBeforeConfirm, WorkFlows.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.confirmed = false;
                return;
            }
        }

        //删除连线
        private void fc_Flow_ArrowDeleted(object sender, MindFusion.FlowChartX.ArrowEventArgs e)
        {
            if (bChangeType) return;   //当在左边树形控件的业务之间切换时，不能删除连线

            if ((e.arrow.Origin.Tag != null) && (e.arrow.Destination.Tag != null))
            {
                strSql = "DELETE FROM DMIS_SYS_FLOWLINE WHERE F_PACKTYPENO=" + iPackNo + " AND F_STARTNO=" + e.arrow.Origin.Tag + " AND F_ENDNO=" + e.arrow.Destination.Tag;
                if (DBOpt.dbHelper.ExecuteSql(strSql) < 0)
                {
                    //MessageBox.Show("删除连线出错", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
        }


        //删除节点前确认一下
        private void fc_Flow_BoxDeleting(object sender, BoxConfirmArgs e)
        {
            //"确定要删除节点
            if (MessageBox.Show(WorkFlows.Properties.Resources.DeleteBeforeConfirm, WorkFlows.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.confirmed = false;
                return;
            }
        }

        //删除节点
        private void fc_Flow_BoxDeleted(object sender, BoxEventArgs e)
        {
            if (bChangeType) return;   //当在左边树形控件的业务之间切换时，不能删除节点
            if (e.box.Tag.ToString() != "")
            {
                strSql = "DELETE FROM DMIS_SYS_FLOWLINK WHERE F_NO=" + e.box.Tag.ToString();
                if (DBOpt.dbHelper.ExecuteSql(strSql) < 0)
                {
                    //MessageBox.Show("删除节点出错","提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }
            }
        }


        private void fc_Flow_BoxSelecting(object sender, MindFusion.FlowChartX.BoxConfirmArgs e)
        {
            //判断是否是备注框，
            fcNode = e.box;
            if (fcNode.Transparent == true)   //程序设定备注框是透明的
            {
                groupBox2.Enabled = false;
                return;
            }

            groupBox1.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            groupBox5.Enabled = true;

            txtName.Text = e.box.Text;
            DataTable dt1;
            if (e.box.Tag != null) iNodeId = Int32.Parse(e.box.Tag.ToString());
            else iNodeId = -1;
            fcNode = e.box;
            dt1 = DBOpt.dbHelper.GetDataTable("SELECT F_FLOWCAT,F_PLANDAY,F_FROMCOND,IS_ASSIGN,F_INCEPT_HOURS,OTHER_LANGUAGE_DESCR FROM DMIS_SYS_FLOWLINK WHERE F_NO=" + iNodeId);
            if (dt1.Rows.Count > 0)
            {
                txtDays.Text = FieldToValue.FieldToTextBox(dt1.Rows[0]["F_PLANDAY"]);
                tb_Cond.Text = FieldToValue.FieldToTextBox(dt1.Rows[0]["F_FROMCOND"]);
                txtInceptHours.Text = FieldToValue.FieldToTextBox(dt1.Rows[0]["F_INCEPT_HOURS"]);
                int iicat = FieldToValue.FieldToInt(dt1.Rows[0]["F_FLOWCAT"]);
                if (dt1.Rows[0]["IS_ASSIGN"] == Convert.DBNull || dt1.Rows[0]["IS_ASSIGN"].ToString() == "否")
                    ckbIsAssign.Checked = false;
                else
                    ckbIsAssign.Checked = true;
                txtOTHER_LANGUAGE_DESCR.Text = FieldToValue.FieldToTextBox(dt1.Rows[0]["OTHER_LANGUAGE_DESCR"]);
            }
            inRole();
            if (tabControl1.SelectedIndex == 1)
            {
                inCbo();
            }
            groupBox2.Enabled = true;

            //ayf 2008-8-4  处理实体
            dt1 = DBOpt.dbHelper.GetDataTable("select * from dmis_sys_wk_deal_entity where template_id="+iPackNo+" and tache_id="+iNodeId);
            if (dt1 == null || dt1.Rows.Count < 1)
            {
                rbnNo.Checked = true;
                rbnMember.Checked = false;
                rbnRelativeTache.Checked = false;
                rbnRole.Checked = false;
                txtEntiID.Text = "";
            }
            else
            {
                if (dt1.Rows.Count == 1)
                {
                    if (dt1.Rows[0]["enti_type"].ToString() == "0")
                        rbnRole.Checked = true;
                    else if (dt1.Rows[0]["enti_type"].ToString() == "2")
                        rbnRelativeTache.Checked = true;
                    else if (dt1.Rows[0]["enti_type"].ToString() == "1")
                        rbnMember.Checked = true;

                    txtEntiID.Text = dt1.Rows[0]["enti_id"].ToString();
                }
            }


            //ayf 2008-10-14 工作流节点实际工作时间统计参数
            dt1 = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_WK_TIMES_STAT_PARA where F_PACKTYPENO=" + iPackNo + " and F_FLOWLINKNO=" + iNodeId);
            if (dt1 == null || dt1.Rows.Count < 1)
            {
                cbbSTARTTIME_COLUMN.Text = "";
                cbbENDTIME_COLUMN.Text = "";
            }
            else
            {
                cbbSTARTTIME_COLUMN.Text = dt1.Rows[0]["STARTTIME_COLUMN"].ToString();
                cbbENDTIME_COLUMN.Text = dt1.Rows[0]["ENDTIME_COLUMN"].ToString();
            }

            //ayf 2008-10-24 工作流变量和条件定义
            dgvVariable.Rows.Clear();
            dt1 = DBOpt.dbHelper.GetDataTable("select VAR_ID,VAR_CODE,VAR_NAME,VAR_TYPE,MAP_TYPE,MAP_STATEMENT,REMARK from DMIS_SYS_WK_VARIABLE where F_PACKTYPENO=" + iPackNo + " and LINK_OR_LINE=" + iNodeId+" and flag=0");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dgvVariable.Rows.Add(dt1.Rows[i]);
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    dgvVariable.Rows[i].Cells[j].Value = dt1.Rows[i][j];
                }
            }
            //dgvVariable.DataSource = dt1;

            dgvCondition.Rows.Clear();
            dt1 = DBOpt.dbHelper.GetDataTable("select COND_ID,COND_NAME,COND_EXPRESSION,ALERT_MESSAGE,ORDER_ID from DMIS_SYS_WK_CONDITION where F_PACKTYPENO=" + iPackNo + " and LINK_OR_LINE=" + iNodeId + " and COND_TYPE=0");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dgvCondition.Rows.Add(dt1.Rows[i]);
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    dgvCondition.Rows[i].Cells[j].Value = dt1.Rows[i][j];
                }
            }

            //ayf 处理环节类型
            strSql = "select F_NODETYPE from dmis_sys_flowlink where f_no=" + iNodeId;
            int NodeTypeID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(strSql));
            string NodeTypeText="";
            if (NodeTypeID == 0)
                NodeTypeText = WorkFlows.Properties.Resources.NodeTypeNormal;//"正常"
            else if (NodeTypeID == 1)
                NodeTypeText = WorkFlows.Properties.Resources.NodeTypeBranch;//"分支"
            else if (NodeTypeID == 2)
                NodeTypeText = WorkFlows.Properties.Resources.NodeTypeCollect;//"汇集"
            cbbNodeType.SelectedIndex = cbbNodeType.FindString(NodeTypeText);
        }

        private void fc_Flow_BoxTextEdited(object sender, MindFusion.FlowChartX.BoxTextArgs e)
        {
            if (iNodeId < 0) return;
            strSql = "UPDATE DMIS_SYS_FLOWLINK SET F_NAME='" + e.newText + "' WHERE F_NO=" + iNodeId;
            DBOpt.dbHelper.ExecuteSql(strSql);
            txtName.Text = e.newText;
        }


        //此事件好像没有执行，有什么用
        private void fc_Flow_BoxCreating(object sender, BoxConfirmArgs e)
        {
            if (iCurrOper < 1 || iCurrOper > 4)
            {
                e.confirmed = false;
                return;
            }
            else
            {
                object ob;

                if (iCurrOper == 1)  //起始环节
                {
                    strSql = "SELECT F_NO FROM DMIS_SYS_FLOWLINK WHERE F_PACKTYPENO=" + iPackNo + " AND F_FLOWCAT=0";
                    ob = DBOpt.dbHelper.ExecuteScalar(strSql);
                    if (ob != null)
                    {
                        //MessageBox.Show("对不起，起始环节只能存在一个！");\
                        e.confirmed = false;
                        return;
                    }
                }
                else if (iCurrOper == 3)
                {
                    strSql = "SELECT F_NO FROM DMIS_SYS_FLOWLINK WHERE F_PACKTYPENO=" + iPackNo + " AND F_FLOWCAT=2";
                    ob = DBOpt.dbHelper.ExecuteScalar(strSql);
                    if (ob != null)
                    {
                        //MessageBox.Show("对不起，结束环节只能存在一个！");
                        e.confirmed = false;
                        return;
                    }
                }
            }
        }

        private void fc_Flow_ArrowCreating(object sender, AttachConfirmArgs e)
        {
            if (!(iCurrOper == Convert.ToInt16(eFlowOper.mainLine) || iCurrOper ==  Convert.ToInt16(eFlowOper.auxLine)))
                e.confirmed = false;
        }
        

        private void fc_Flow_ArrowCreated(object sender, MindFusion.FlowChartX.ArrowEventArgs e)
        {
            //插入流线数据库DMIS_SYS_FLOWLINE
            fcLine = e.arrow;
  
            uint iMax = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_FLOWLINE","F_NO");

            strSql = "INSERT INTO DMIS_SYS_FLOWLINE VALUES("
                + iMax + "," + iPackNo + "," + e.arrow.Origin.Tag.ToString()
                + "," + e.arrow.Destination.Tag.ToString() + ",''," + Convert.ToString(iCurrOper - 5) + ")";
            DBOpt.dbHelper.ExecuteSql(strSql);
            if (iCurrOper == 6)
                fcLine.PenDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            else 
                fcLine.PenDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
         }

        private void fc_Flow_BoxModified(object sender, MindFusion.FlowChartX.BoxMouseArgs e)
        {
            if (iNodeId < 0) return;
            int x, y,iWid,iHei;
            x = fc_Flow.DocToClient(new PointF(e.box.BoundingRect.Left, e.box.BoundingRect.Top)).X;
            y = fc_Flow.DocToClient(new PointF(e.box.BoundingRect.Left, e.box.BoundingRect.Top)).Y;
            iWid = fc_Flow.DocToClient(new PointF(e.box.BoundingRect.Right, e.box.BoundingRect.Bottom)).X-x;
            iHei = fc_Flow.DocToClient(new PointF(e.box.BoundingRect.Right, e.box.BoundingRect.Bottom)).Y-y;
            strSql = "UPDATE DMIS_SYS_FLOWLINK SET F_LEFT=" + x 
                + ",F_TOP="+y
                + ",F_WIDTH=" + iWid
                + ",F_HEIGHT=" + iHei
                +" WHERE F_NO=" + iNodeId;
            DBOpt.dbHelper.ExecuteSql(strSql);
        }

        /// <summary>
        /// 更改节点名称，则更新节点显示，更改数据库内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Name_Leave(object sender, EventArgs e)
        {
            if (fcNode == null || iNodeId<0) return;
            fcNode.Text = txtName.Text;
            if (iNodeId < 0) return;
            strSql = "UPDATE DMIS_SYS_FLOWLINK SET F_NAME='" + txtName.Text + "' WHERE F_NO=" + iNodeId;
            DBOpt.dbHelper.ExecuteSql(strSql);
        }
        private void tb_Day_Leave(object sender, EventArgs e)
        {
            if (iNodeId < 0) return;
            strSql = "UPDATE DMIS_SYS_FLOWLINK SET F_PLANDAY=" + txtDays.Text + " WHERE F_NO=" + iNodeId;
            DBOpt.dbHelper.ExecuteSql(strSql);

        }
        /// <summary>
        /// 更改到达满足条件的数据库项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Cond_Leave(object sender, EventArgs e)
        {
            if (iNodeId < 0) return;
            strSql = "UPDATE DMIS_SYS_FLOWLINK SET F_FROMCOND='" + tb_Cond.Text + "' WHERE F_NO=" + iNodeId;
            DBOpt.dbHelper.ExecuteSql(strSql);
        }

        private void cb_Role_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Role.Text.Trim() == "") return;  //解决BUG_004


            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                iselRoleNo = DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_ROLE where NAME='" + cb_Role.Text + "'").ToString();
            else
                iselRoleNo = DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_ROLE where OTHER_LANGUAGE_DESCR='" + cb_Role.Text + "'").ToString();
            inFld();
            //cb_Doc.Items.Clear();
            //if (cb_Role.Text=="") return;
            ////iselRoleNo = DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_ROLE where NAME='" + cb_Role.Text + "'").ToString();
            //iselRoleNo = cb_Role.SelectedValue.ToString();
            //DataTable dtDoc;
            //strSql = "SELECT A.F_NAME FROM "
            //       + " DMIS_SYS_DOCTYPE A,DMIS_SYS_RIGHTS B,DMIS_SYS_PACKTYPE C "
            //       + " WHERE C.F_NO=A.F_PACKTYPENO AND B.F_FOREIGNKEY=A.F_NO AND C.F_NO=" + iPackNo
            //       + " AND B.F_CATGORY='文档' AND B.F_ROLENO=" + iselRoleNo;
            //dtDoc = DBOpt.dbHelper.GetDataTable(strSql);
            //if (dtDoc.Rows.Count > 0)
            //    for (int i = 0; i < dtDoc.Rows.Count; i++)
            //        cb_Doc.Items.Add(dtDoc.Rows[i]["F_NAME"].ToString());
            //if (cb_Doc.Items.Count > 0)  cb_Doc.SelectedIndex = 0;

        }
        private void tbSel_Click(object sender, EventArgs e)
        {
            iCurrOper = (int)eFlowOper.selObj;
            setToolButtonStatus("tbSel");
        }
        private void tbStart_Click(object sender, EventArgs e)
        {
            iCurrOper = (int)eFlowOper.startNode;
            setToolButtonStatus("tbStart");
        }
        private void tbNode_Click(object sender, EventArgs e)
        {
            iCurrOper = (int)eFlowOper.midNode;
            setToolButtonStatus("tbNode");
        }
        private void tbEnd_Click(object sender, EventArgs e)
        {
            iCurrOper = (int)eFlowOper.endNode;
            setToolButtonStatus("tbEnd");
        }
        private void tbLine_Click(object sender, EventArgs e)
        {
            iCurrOper = (int)eFlowOper.mainLine;
            setToolButtonStatus("tbLine");
        }
        private void tbNote_Click(object sender, EventArgs e)
        {
            iCurrOper = (int)eFlowOper.descNode;
            setToolButtonStatus("tbNote");
        }
        private void tbLine1_Click(object sender, EventArgs e)
        {
            iCurrOper = (int)eFlowOper.auxLine;
            setToolButtonStatus("tbLine1");
        }
        private void tbUndo_Click(object sender, EventArgs e)
        {
            fc_Flow.UndoManager.Undo();
        }
        private void tbRedo_Click(object sender, EventArgs e)
        {
            fc_Flow.UndoManager.Redo();
        }
        /// <summary>
        /// 删除所选的对象，同时删除数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDel_Click(object sender, EventArgs e)
        {
            if (fc_Flow.Selection == null) return;
            if (MessageBox.Show(WorkFlows.Properties.Resources.DeleteBeforeConfirm, WorkFlows.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            if(fc_Flow.Selection.Boxes.Count>0)
                while (fc_Flow.Selection.Boxes.Count>0)
                {
                    if (fc_Flow.Selection.Boxes[0].Tag != null)
                    {
                        strSql = "DELETE FROM DMIS_SYS_FLOWLINK WHERE F_NO=" + fc_Flow.Selection.Boxes[0].Tag;
                        DBOpt.dbHelper.ExecuteSql(strSql);
                    }
                    fc_Flow.DeleteObject(fc_Flow.Selection.Boxes[0]);
                }
            if (fc_Flow.Selection.Arrows.Count > 0)
                while (fc_Flow.Selection.Arrows.Count > 0)
                {
                    if (fc_Flow.Selection.Arrows[0].Tag != null)
                    {
                        strSql = "DELETE FROM DMIS_SYS_FLOWLINE WHERE F_NO=" + fc_Flow.Selection.Arrows[0].Tag;
                        DBOpt.dbHelper.ExecuteSql(strSql);
                    }
                    fc_Flow.DeleteObject(fc_Flow.Selection.Arrows[0]);

                }
            fc_Flow.Invalidate();
        }

        /// <summary>
        /// 改变角色或文档，则字段列表内容改变
        /// </summary>
        private void inFld() {
            lv_Field.Items.Clear();
            try {
                if (txtDocTypeID.Text == "") return;
                if (cb_Role.Text == "") return;

                sselTableName = DBOpt.dbHelper.ExecuteScalar("select F_TABLENAME from DMIS_SYS_DOCTYPE where F_NO=" + txtDocTypeID.Text ).ToString();
                DataTable dt1,dt2;

                dt1 = DBOpt.dbHelper.GetDataTable("SELECT * FROM " + sselTableName + " WHERE 0=1");

                strSql = "SELECT F_FIELDNAME FROM DMIS_SYS_FLOWFIELDRIGHT WHERE "
                    + "F_PACKTYPENO=" + iPackNo + " AND "
                    + "F_FLOWNO=" + iNodeId + " AND "
                    + "F_TABLENAME='" + sselTableName + "' AND "
                    + "F_ROLENO=" + iselRoleNo;//
                dt2 = DBOpt.dbHelper.GetDataTable(strSql);
                DataRow[] rws;
                ListViewItem lii = new ListViewItem();
                for (int i = 0; i < dt1.Columns.Count; i++) {
                    rws = dt2.Select("F_FIELDNAME='" + dt1.Columns[i].ColumnName+"'");
                    lii = new ListViewItem();
                    lii.Text = dt1.Columns[i].ColumnName;
                    if (rws.Length > 0) lii.Checked=true;
                    else lii.Checked = false; 
                     lv_Field.Items.Add(lii);
                }                   
            }
            catch (Exception ee) {
                Console.Write(ee.Message);
            }

        }
        /// <summary>
        /// 选择业务，根据权限填充环节属性的角色和文档
        /// </summary>
        private void inCbo()
        {
            if (iNodeId < 1) return;
            cb_Role.Items.Clear();
            //cb_Doc.Items.Clear();
            lv_Field.Items.Clear();

            strSql = "SELECT A.ID,A.NAME,A.OTHER_LANGUAGE_DESCR FROM DMIS_SYS_ROLE A,DMIS_SYS_RIGHTS B WHERE A.ID=B.F_ROLENO AND B.F_FOREIGNKEY=" + iNodeId
                + " AND B.F_CATGORY='流程角色'";
            DataTable dtRole;
            dtRole = DBOpt.dbHelper.GetDataTable(strSql);
            //cb_Role.DataSource = dtRole;
            //cb_Role.DisplayMember = "NAME";
            //cb_Role.ValueMember = "ID";
            if (dtRole.Rows.Count > 0)
                for (int i = 0; i < dtRole.Rows.Count; i++)
                {
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                        cb_Role.Items.Add(dtRole.Rows[i]["NAME"].ToString());
                    else
                        cb_Role.Items.Add(dtRole.Rows[i]["OTHER_LANGUAGE_DESCR"].ToString());
                }
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                strSql = "select a.F_NO,a.F_NAME from dmis_sys_doctype a, dmis_sys_wk_link_doctype b where a.f_no=b.f_doctypeno and b.f_packtypeno=" + iPackNo + " and f_linkno=" + iNodeId;
            else
                strSql = "select a.F_NO,a.OTHER_LANGUAGE_DESCR F_NAME from dmis_sys_doctype a, dmis_sys_wk_link_doctype b where a.f_no=b.f_doctypeno and b.f_packtypeno=" + iPackNo + " and f_linkno=" + iNodeId;
            DataTable doc = DBOpt.dbHelper.GetDataTable(strSql);
            if (doc.Rows.Count > 0)
            {
                txtDocTypeID.Text = doc.Rows[0][0].ToString();
                txtDocTypeName.Text = doc.Rows[0][1].ToString();
            }
            else
            {
                txtDocTypeID.Text = "";
                txtDocTypeName.Text = "";
            }
            if (cb_Role.Items.Count > 0) cb_Role.SelectedIndex = 0;
            cb_Role_SelectedIndexChanged(null, null);
          }
        /// <summary>
        /// 初始化树列表，加载业务和文档
        /// </summary>
        private void initForm()
        {
            DataTable dt, dt2;
            trvPack.Nodes.Clear();
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                strSql = "SELECT F_NO,F_NAME FROM DMIS_SYS_PACKTYPE ORDER BY F_NAME";
            else
                strSql = "SELECT F_NO,OTHER_LANGUAGE_DESCR F_NAME FROM DMIS_SYS_PACKTYPE ORDER BY F_NAME";
            dt = DBOpt.dbHelper.GetDataTable(strSql);
            TreeNode tn1, tn2;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tn1 = new TreeNode();
                    tn1.Text = FieldToValue.FieldToTextBox(dt.Rows[i][1]);
                    tn1.Tag = FieldToValue.FieldToTextBox(dt.Rows[i][0]);
                    tn1.ToolTipText = tn1.Tag.ToString();
                    tn1.ImageIndex = 0;
                    tn1.SelectedImageIndex = 1;
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                        strSql = "SELECT F_NO,F_NAME FROM DMIS_SYS_DOCTYPE WHERE F_PACKTYPENO=" + dt.Rows[i][0].ToString() + " ORDER BY F_NAME";
                    else
                        strSql = "SELECT F_NO,OTHER_LANGUAGE_DESCR F_NAME FROM DMIS_SYS_DOCTYPE WHERE F_PACKTYPENO=" + dt.Rows[i][0].ToString() + " ORDER BY F_NAME";
                    dt2 = DBOpt.dbHelper.GetDataTable(strSql);
                    if (dt2.Rows.Count > 0)
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            tn2 = new TreeNode();
                            tn2.Text = FieldToValue.FieldToTextBox(dt2.Rows[j][1]);
                            tn2.Tag = FieldToValue.FieldToTextBox(dt2.Rows[j][0]);
                            tn2.ToolTipText = tn2.Tag.ToString();
                            tn2.ImageIndex = 2;
                            tn2.SelectedImageIndex = 3;
                            tn1.Nodes.Add(tn2);
                        }
                    trvPack.Nodes.Add(tn1);
                }
            }
        }

        private void CreateFlow(){
            
            fc_Flow.ClearAll();
            LoadFromDbNode();
            LoadFromDBLine();
            fc_Flow.Invalidate(true);
        }

        /// <summary>
        /// 从数据库创建节点
        /// </summary>
        private void LoadFromDbNode()
        {
            DataTable dtFlow;
            dtFlow = DBOpt.dbHelper.GetDataTable("SELECT * FROM DMIS_SYS_FLOWLINK WHERE F_PACKTYPENO="+iPackNo);
            int iCat, iFno, x1, y1, iWid1, iHei1;
            float  x, y, iWid, iHei;
            string sCaption="";
            if (dtFlow.Rows.Count > 0)
            {
                for (int i = 0; i <= dtFlow.Rows.Count - 1; i++)
                {
                    x1 =FieldToValue.FieldToInt(dtFlow.Rows[i]["F_LEFT"]);
                    y1 = FieldToValue.FieldToInt(dtFlow.Rows[i]["F_TOP"]);
                    iWid1 = FieldToValue.FieldToInt(dtFlow.Rows[i]["F_WIDTH"]);
                    iHei1 = FieldToValue.FieldToInt(dtFlow.Rows[i]["F_HEIGHT"]);
                    x=fc_Flow.ClientToDoc(new Point(x1,y1)).X;
                    y = fc_Flow.ClientToDoc(new Point(x1, y1)).Y;
                    iWid = fc_Flow.ClientToDoc(new Point(x1 + iWid1, y1 + iHei1)).X-x;
                    iHei = fc_Flow.ClientToDoc(new Point(x1 + iWid1, y1 + iHei1)).Y-y;

                    iFno= FieldToValue.FieldToInt(dtFlow.Rows[i]["F_NO"]);
                    iCat = FieldToValue.FieldToInt(dtFlow.Rows[i]["F_FLOWCAT"]);
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                        sCaption= FieldToValue.FieldToString(dtFlow.Rows[i]["F_NAME"]);
                    else
                        sCaption = FieldToValue.FieldToString(dtFlow.Rows[i]["OTHER_LANGUAGE_DESCR"]);
                    iCat += 1;
                    if(iWid<0) iWid=10;
                    if(iHei<0) iHei=10;
                    if (iCat < 1 && iCat > 4) continue;
                    fcNode = fc_Flow.CreateBox(x,y, iWid, iHei);
                    fcNode.Text = sCaption;
                    fcNode.Tag = iFno;
                    fcNode.ToolTip = iFno.ToString();
                    switch (iCat) { 
                    case 1:               
                        fcNode.Style = EBoxStyle.bsEllipse;
                        fcNode.FillColor = Color.Lime;// System.Drawing.ColorTranslator.FromWin32(192 + 255 * 256 + 192 * 65536);
                        break;
                    case 2:
                        fcNode.Style = EBoxStyle.bsRect;
                        fcNode.FillColor =  System.Drawing.ColorTranslator.FromWin32(192 + 255 * 256 + 192 * 65536);
                        break;
                    case 3:
                        fcNode.Style = EBoxStyle.bsEllipse;
                        fcNode.FillColor = Color.DeepPink;
                        break;
                    case 4:
                        fcNode.Style = EBoxStyle.bsRect;
                        fcNode.Transparent = true;
                        fcNode.FillColor = Color.Transparent;// System.Drawing.ColorTranslator.FromWin32(192 + 255 * 256 + 192 * 65536);
                        break;
                    }
                    //fc_Flow.Add(fcNode);
                    
                }
            }
        }

        /// <summary>
        /// 从数据库创建连接流线
        /// </summary>
        private void LoadFromDBLine()
        {
            DataTable dtLine;
            dtLine = DBOpt.dbHelper.GetDataTable("SELECT * FROM DMIS_SYS_FLOWLINE WHERE F_PACKTYPENO=" + iPackNo);
            int iCat, iFno, iStartNo, iEndNo;
            if (dtLine.Rows.Count > 0)
            {
                for (int i = 0; i <= dtLine.Rows.Count - 1; i++)
                {
                    iStartNo = FieldToValue.FieldToInt(dtLine.Rows[i]["F_STARTNO"]);
                    iEndNo = FieldToValue.FieldToInt(dtLine.Rows[i]["F_ENDNO"]);
                    iCat = FieldToValue.FieldToInt(dtLine.Rows[i]["F_LINETYPE"]);
                    iFno = FieldToValue.FieldToInt(dtLine.Rows[i]["F_NO"]);
                    Box fbStart = fc_Flow.FindBox(iStartNo);
                    Box fbEnd = fc_Flow.FindBox(iEndNo);
                    if (fbStart != null && fbEnd != null)
                    {
                        fcLine = fc_Flow.CreateArrow(fbStart, fbEnd);
                        fcLine.Tag = iFno;
                        if (iCat == 1)
                            fcLine.PenDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        else
                            fcLine.PenDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    }

                }
            }
        }

        /// <summary>
        /// 创建节点时，更新数据库
        /// </summary>
        /// <param name="newbox"></param>
        /// <param name="iMax"></param>
        private void CreateNode(Box newbox,uint iMax) {
            //插入流线数据库DMIS_SYS_FLOWLINK
            int x, y, iWid, iHei;
            x = fc_Flow.DocToClient(new PointF(newbox.BoundingRect.Left, newbox.BoundingRect.Top)).X;
            y = fc_Flow.DocToClient(new PointF(newbox.BoundingRect.Left, newbox.BoundingRect.Top)).Y;
            iWid = fc_Flow.DocToClient(new PointF(newbox.BoundingRect.Right, newbox.BoundingRect.Bottom)).X - x;
            iHei = fc_Flow.DocToClient(new PointF(newbox.BoundingRect.Right, newbox.BoundingRect.Bottom)).Y - y;
            strSql = "INSERT INTO DMIS_SYS_FLOWLINK(f_no,f_packtypeno,f_name,f_flowcat,f_planday,f_fromcond,f_left,f_top,f_width,f_height,is_assign,OTHER_LANGUAGE_DESCR) VALUES("
                + iMax + "," + iPackNo + ",'" + newbox.Text + "',"
                + Convert.ToString(iCurrOper - 1) + ",0,''," + x + "," + y + "," + iWid + "," + iHei + ",'否','')";
            if (DBOpt.dbHelper.ExecuteSql(strSql) < 0)
            {
                //MessageBox.Show("插入节点失败！", "提示");
            }
        }

 
        private void inRole() {
            lv_Role.Items.Clear();
            DataTable dt1,dt2;
            strSql = "SELECT A.* FROM DMIS_SYS_ROLE A,DMIS_SYS_RIGHTS B WHERE A.ID=B.F_ROLENO AND B.F_FOREIGNKEY=" + iPackNo
                + " AND B.F_CATGORY='业务'";
            dt1 = DBOpt.dbHelper.GetDataTable(strSql);
            strSql = "select F_ROLENO from DMIS_SYS_RIGHTS where F_FOREIGNKEY=" + iNodeId 
                    + " AND F_CATGORY='流程角色'";
            dt2 = DBOpt.dbHelper.GetDataTable(strSql);
            ListViewItem lii = new ListViewItem();
            DataRow[] rws;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                rws = dt2.Select("F_ROLENO=" + dt1.Rows[i]["ID"].ToString());
                lii = new ListViewItem();
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                    lii.Text = dt1.Rows[i]["NAME"].ToString();
                else
                    lii.Text = dt1.Rows[i]["OTHER_LANGUAGE_DESCR"].ToString();
                lii.Tag = dt1.Rows[i]["ID"].ToString();
                if (rws.Length > 0) lii.Checked = true;
                else lii.Checked = false;
                lv_Role.Items.Add(lii);
             }       
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (trvPack.SelectedNode == null) return;
            if (fc_Flow.Selection.Boxes.Count < 0) return;
            if (tabControl1.SelectedIndex == 1) {
                inCbo();
            }
        }

        private void lv_Field_Leave(object sender, EventArgs e)
        {
            if (lv_Field.Items.Count < 1) return;
            strSql="delete from DMIS_SYS_FLOWFIELDRIGHT where F_FLOWNO="
                     + iNodeId + " and F_ROLENO=" + iselRoleNo
                     + " and F_TABLENAME='" + sselTableName + "'";
            DBOpt.dbHelper.ExecuteSql(strSql);

            uint iMax = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_FLOWFIELDRIGHT", "F_NO");

            for (int i = 0; i < lv_Field.CheckedItems.Count; i++) {
                strSql = "INSERT INTO DMIS_SYS_FLOWFIELDRIGHT(F_NO,F_PACKTYPENO,F_FLOWNO,F_ROLENO,F_TABLENAME,F_FIELDNAME,F_RIGHT) VALUES("
                    + iMax + "," + iPackNo + "," + iNodeId + "," + iselRoleNo + ",'" + sselTableName
                    + "','" + lv_Field.CheckedItems[i].Text + "','1100000')";
                DBOpt.dbHelper.ExecuteSql(strSql);
                iMax++;
            }
        }

        private void lv_Role_Leave(object sender, EventArgs e)
        {
            if (lv_Role.Items.Count < 1) return;
            strSql = "delete from DMIS_SYS_RIGHTS where F_FOREIGNKEY=" + iNodeId + " and F_CATGORY='流程角色'" ;
            DBOpt.dbHelper.ExecuteSql(strSql);

            uint iMax = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_RIGHTS","F_NO");

            for (int i = 0; i < lv_Role.CheckedItems.Count; i++)
            {
                strSql = "INSERT INTO DMIS_SYS_RIGHTS(F_NO,F_FOREIGNKEY,F_CATGORY,F_ROLENO,F_ACCESS) VALUES("
                    + iMax + "," + iNodeId + ",'流程角色'," + lv_Role.CheckedItems[i].Tag.ToString() + ",'1100000')";
                DBOpt.dbHelper.ExecuteSql(strSql);
                iMax++;
            }
        }


        private void fc_Flow_ActionUndone(object sender, UndoEventArgs e)
        {

            //if (e.Command.Title.ToLower() == "create" || e.Command.Title.ToLower() == "delete")
            //{
            //    if (iCurrOperPoint > -1)
            //    {
            //        try
            //        {
            //            strSql = history[iCurrOperPoint].ToString();
            //            if (strSql != "") DBOpt.dbHelper.ExecuteScalar(strSql);
            //            iCurrOperPoint--;
            //        }
            //        finally {
            //            e.Command.Execute(true);
            //        }
            //    }
            //}
        }

        private void fc_Flow_ActionRedone(object sender, UndoEventArgs e)
        {
            //if (e.Command.Title.ToLower() == "create" || e.Command.Title.ToLower() == "delete")
            //{
            //    if (iCurrOperPoint > -1)
            //    {
            //        try
            //        {
            //            strSql = history[iCurrOperPoint].ToString();
            //            if (strSql != "") DBOpt.dbHelper.ExecuteScalar(strSql);
            //            iCurrOperPoint++;
            //        }
            //        finally {
            //            e.Command.Execute(true);
            //        }
            //    }
            //}
        }

        private void txtDays_Validating(object sender, CancelEventArgs e)
        {
            float hours;
            if (!(float.TryParse(txtDays.Text, out hours) || txtDays.Text == ""))
            {
                errorProvider1.SetError((Control)sender, WorkFlows.Properties.Resources.NumericalValeError);
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError((Control)sender, "");
            }
        }

         private void ckbIsAssign_Leave(object sender, EventArgs e)
        {
            if (iNodeId < 0) return;
            if(ckbIsAssign.Checked)
                strSql = "UPDATE DMIS_SYS_FLOWLINK SET is_assign='是' WHERE F_NO=" + iNodeId;
            else
                strSql = "UPDATE DMIS_SYS_FLOWLINK SET is_assign='否' WHERE F_NO=" + iNodeId;

            DBOpt.dbHelper.ExecuteSql(strSql);
        }

        private void btnSelectEntity_Click(object sender, EventArgs e)
        {
            if (iNodeId < 0) return;
            if (rbnRole.Checked)
            {
                frmSelectRole role = new frmSelectRole();
                role.NodeID = iNodeId;
                if (role.ShowDialog() == DialogResult.OK)
                    txtEntiID.Text = role.roles;
            }
            else if (rbnMember.Checked)
            {
                frmSelectMember member = new frmSelectMember();
                member.NodeID=iNodeId;
                if (member.ShowDialog() == DialogResult.OK) txtEntiID.Text = member.Names;
            }
            else if (rbnRelativeTache.Checked)
            {
                strSql = "select f_flowcat from DMIS_SYS_FLOWLINK where f_no=" + iNodeId;
                object obj = DBOpt.dbHelper.ExecuteScalar(strSql);
                if (obj == null) return;
                if (obj.ToString() == "0")
                {
                    //MessageBox.Show("起始节点不允许设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmSelectRelativeTache tache = new frmSelectRelativeTache();
                tache.NodeID = iNodeId;
                tache.PackTypeID = iPackNo;
                if (tache.ShowDialog() == DialogResult.OK) txtEntiID.Text = tache.node;
            }
            else
            {
            }
        }

        //保存实体信息
        private void btnSaveEntity_Click(object sender, EventArgs e)
        {
            if (iNodeId < 0) return;
            if (rbnNo.Checked)   //取消实体
            {
                txtEntiID.Text = "";
                strSql = "delete from dmis_sys_wk_deal_entity where template_id=" + iPackNo + " and TACHE_ID=" + iNodeId;
                DBOpt.dbHelper.ExecuteSql(strSql);
            }
            else
            {
                if (txtEntiID.Text.Trim() == "")
                {
                    //MessageBox.Show("实体代码不能为空！","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }

                object obj = DBOpt.dbHelper.ExecuteScalar("select count(*) from dmis_sys_wk_deal_entity where template_id=" + iPackNo + " and tache_id=" + iNodeId);
                if (Convert.ToInt16(obj) < 1)
                {
                    strSql = "insert into dmis_sys_wk_deal_entity(template_id,version_no,TACHE_ID,ENTI_TYPE,ENTI_ID) values(" + iPackNo + ",'1.0.0'," + iNodeId;
                    if (rbnRole.Checked)
                        strSql = strSql + ",'0','" + txtEntiID.Text + "')";
                    else if (rbnMember.Checked)
                        strSql = strSql + ",'1','" + txtEntiID.Text + "')";
                    else if (rbnRelativeTache.Checked)
                        strSql = strSql + ",'2','" + txtEntiID.Text + "')";
                    else { }
                        
                }
                else
                {
                    if (rbnRole.Checked)
                        strSql = "update dmis_sys_wk_deal_entity set ENTI_TYPE='0',ENTI_ID='" + txtEntiID.Text + "' where template_id=" + iPackNo + " and TACHE_ID=" + iNodeId;
                    else if (rbnMember.Checked)
                        strSql = "update dmis_sys_wk_deal_entity set ENTI_TYPE='1',ENTI_ID='" + txtEntiID.Text + "' where template_id=" + iPackNo + " and TACHE_ID=" + iNodeId;
                    else
                        strSql = "update dmis_sys_wk_deal_entity set ENTI_TYPE='2',ENTI_ID='" + txtEntiID.Text + "' where template_id=" + iPackNo + " and TACHE_ID=" + iNodeId;
                }
                DBOpt.dbHelper.ExecuteSql(strSql);
            }
        }

 

        #region "工作流节点实际工作时间统计参数"
        private void btnSaveTimesStatPara_Click(object sender, EventArgs e)
        {
            if (iNodeId < 0) return;
            strSql = "delete from DMIS_SYS_WK_TIMES_STAT_PARA where F_PACKTYPENO=" + iPackNo + " and F_FLOWLINKNO=" + iNodeId;
            DBOpt.dbHelper.ExecuteSql(strSql);

            if (cbbSTARTTIME_COLUMN.Text == "" || cbbENDTIME_COLUMN.Text == "")
            {
                cbbSTARTTIME_COLUMN.Text = "";
                cbbENDTIME_COLUMN.Text = "";
            }
            else
            {            
                strSql = "insert into DMIS_SYS_WK_TIMES_STAT_PARA(F_PACKTYPENO,F_FLOWLINKNO,STARTTIME_COLUMN,ENDTIME_COLUMN) values(" +
                        iPackNo + "," + iNodeId + ",'" + cbbSTARTTIME_COLUMN.Text + "','" + cbbENDTIME_COLUMN.Text + "')";
                DBOpt.dbHelper.ExecuteSql(strSql);
 
            }
        }
        #endregion


        #region "变量设置"
        private void initPara()
        {
            //变量类型
            DataRow row = dsPara.Tables["VarTypePara"].NewRow();
            row["VarTypeCode"] = "string";
            row["VarTypeName"] = "字符串";
            dsPara.Tables["VarTypePara"].Rows.Add(row);

            row = dsPara.Tables["VarTypePara"].NewRow();
            row["VarTypeCode"] = "int";
            row["VarTypeName"] = "整数";
            dsPara.Tables["VarTypePara"].Rows.Add(row);

            row = dsPara.Tables["VarTypePara"].NewRow();
            row["VarTypeCode"] = "datetime";
            row["VarTypeName"] = "时间";
            dsPara.Tables["VarTypePara"].Rows.Add(row);

            row = dsPara.Tables["VarTypePara"].NewRow();
            row["VarTypeCode"] = "char";
            row["VarTypeName"] = "字符";
            dsPara.Tables["VarTypePara"].Rows.Add(row);

            row = dsPara.Tables["VarTypePara"].NewRow();
            row["VarTypeCode"] = "float";
            row["VarTypeName"] = "浮点数值";
            dsPara.Tables["VarTypePara"].Rows.Add(row);

            //映射类型
            row = dsPara.Tables["MapTypePara"].NewRow();
            row["MapTypeCode"] = 0;
            row["MapTypeName"] = "SQL语句";
            dsPara.Tables["MapTypePara"].Rows.Add(row);

            row = dsPara.Tables["MapTypePara"].NewRow();
            row["MapTypeCode"] = 1;
            row["MapTypeName"] = "Session变量";
            dsPara.Tables["MapTypePara"].Rows.Add(row);


        }

        private void btnVarModify_Click(object sender, EventArgs e)
        {
            if (dgvVariable.SelectedCells.Count == 0)
            {
                //MessageBox.Show("请选择要修改的变量！");
                return;
            }
            int rowID=dgvVariable.SelectedCells[0].RowIndex;
            if (dgvVariable.Rows[rowID].Cells["VAR_CODE"].Value == null || dgvVariable.Rows[rowID].Cells["VAR_CODE"].Value.ToString().Trim() == "")
            {
                //MessageBox.Show("变量代码不允许为空！");
                return;
            }
            string code = dgvVariable.Rows[rowID].Cells["VAR_CODE"].Value.ToString();

            if (dgvVariable.Rows[rowID].Cells["VAR_NAME"].Value == null || dgvVariable.Rows[rowID].Cells["VAR_NAME"].Value.ToString().Trim() == "")
            {
                //MessageBox.Show("变量名称不允许为空！");
                return;
            }
            string name = dgvVariable.Rows[rowID].Cells["VAR_NAME"].Value.ToString();

            if (dgvVariable.Rows[rowID].Cells["VAR_TYPE"].Value == null || dgvVariable.Rows[rowID].Cells["VAR_TYPE"].Value.ToString().Trim() == "")
            {
                //MessageBox.Show("变量类型不允许为空！");
                return;
            }
            string varType = dgvVariable.Rows[rowID].Cells["VAR_TYPE"].Value.ToString();

            if (dgvVariable.Rows[rowID].Cells["MAP_TYPE"].Value == null || dgvVariable.Rows[rowID].Cells["MAP_TYPE"].Value.ToString().Trim() == "")
            {
                //MessageBox.Show("映射类型不允许为空！");
                return;
            }
            string mapType = dgvVariable.Rows[rowID].Cells["MAP_TYPE"].Value.ToString();

            //if (dgvVariable.Rows[rowID].Cells["MAP_STATEMENT"].Value == null || dgvVariable.Rows[rowID].Cells["MAP_STATEMENT"].Value.ToString().Trim() == "")
            //{
            //    MessageBox.Show("映射语句不允许为空！");
            //    return;
            //}
            string statement ;
            if (dgvVariable.Rows[rowID].Cells["MAP_STATEMENT"].Value != null)
                statement = dgvVariable.Rows[rowID].Cells["MAP_STATEMENT"].Value.ToString();
            else
                statement = "";

            string remark;
            if (dgvVariable.Rows[rowID].Cells["REMARK"].Value != null)
                remark = dgvVariable.Rows[rowID].Cells["REMARK"].Value.ToString();
            else
                remark = "";

            uint max ;
            if (dgvVariable.Rows[rowID].Cells["VAR_ID"].Value == null)
                max = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WK_VARIABLE", "VAR_ID");
            else
                max = Convert.ToUInt16(dgvVariable.Rows[rowID].Cells["VAR_ID"].Value.ToString());

            FieldPara[] fieldTables = {new FieldPara("VAR_ID",FieldType.Int,max.ToString()),
                                         new FieldPara("F_PACKTYPENO",FieldType.Int,iPackNo.ToString()),
                                         new FieldPara("LINK_OR_LINE",FieldType.Int,groupBox1.Enabled?iNodeId.ToString():iLineId.ToString()),
					                     new FieldPara("VAR_CODE",FieldType.String,code),
                                         new FieldPara("VAR_NAME",FieldType.String,name),
					                     new FieldPara("VAR_TYPE",FieldType.String,varType),
                                         new FieldPara("MAP_TYPE",FieldType.Int,mapType),
                                         new FieldPara("FLAG",FieldType.Int,groupBox1.Enabled?"0":"1"),
                                         new FieldPara("REMARK",FieldType.String,remark)
                                       };
            if (dgvVariable.Rows[rowID].Cells["VAR_ID"].Value == null)  //插入
            {
                strSql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_WK_VARIABLE", fieldTables);
            }
            else　　//修改
            {
                WherePara[] where ={ new WherePara("VAR_ID", FieldType.Int, dgvVariable.Rows[rowID].Cells["VAR_ID"].Value.ToString(), "=", "and") };
                strSql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_WK_VARIABLE", fieldTables, where);
            }

            if (DBOpt.dbHelper.ExecuteSql(strSql) > 0)  //TABLE_FILTER_WHERE列可能保存to_char(datem,'yyyymm')这样的数据，故要用存储过程来更新
            {
                dgvVariable.Rows[rowID].Cells[0].Value = max;

                if (DBHelper.databaseType == "SqlServer")
                {

                }
                else if (DBHelper.databaseType == "Oracle")
                {
                    strSql = "update DMIS_SYS_WK_VARIABLE set MAP_STATEMENT=:MapStatement where VAR_ID=" + max.ToString();
                    OracleParameter[] aPara = new OracleParameter[1];
                    OracleParameter pContent = new OracleParameter("MapStatement", OracleType.VarChar);
                    pContent.Value = statement;
                    aPara[0] = pContent;
                    DBOpt.dbHelper.ExecuteSqlByParas(strSql, aPara);
                }
                else if (DBHelper.databaseType == "Sybase")
                {
                }
                else   //ODBC
                {
                }
            }
        }

        private void btnVarDelete_Click(object sender, EventArgs e)
        {
            if (dgvVariable.SelectedCells.Count == 0)
            {
                //MessageBox.Show("请选择要删除的变量！");
                return;
            }

            int rowID = dgvVariable.SelectedCells[0].RowIndex;
            if (dgvVariable.Rows[rowID].IsNewRow) return;
            string varID = dgvVariable.Rows[rowID].Cells["VAR_ID"].Value.ToString();
            //"是否要删除变量：
            if (MessageBox.Show(WorkFlows.Properties.Resources.DeleteBeforeConfirm, WorkFlows.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            strSql = "delete from DMIS_SYS_WK_VARIABLE where VAR_ID=" + varID;
            if (DBOpt.dbHelper.ExecuteSql(strSql) > 0)
                dgvVariable.Rows.Remove(dgvVariable.Rows[rowID]);
        }

        #endregion

        #region "条件设置"
        private void btnConModify_Click(object sender, EventArgs e)
        {
            if (dgvCondition.SelectedCells.Count == 0)
            {
                //MessageBox.Show("请选择要修改的条件！");
                return;
            }
            int rowID = dgvCondition.SelectedCells[0].RowIndex;

            if (dgvCondition.Rows[rowID].Cells["COND_NAME"].Value == null || dgvCondition.Rows[rowID].Cells["COND_NAME"].Value.ToString().Trim() == "")
            {
                MessageBox.Show(this, WorkFlows.Properties.Resources.NoEmpty, WorkFlows.Properties.Resources.Note, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //MessageBox.Show("条件名称不允许为空！");
                return;
            }
            string name = dgvCondition.Rows[rowID].Cells["COND_NAME"].Value.ToString();

            if (dgvCondition.Rows[rowID].Cells["COND_EXPRESSION"].Value == null || dgvCondition.Rows[rowID].Cells["COND_EXPRESSION"].Value.ToString().Trim() == "")
            {
                MessageBox.Show(this, WorkFlows.Properties.Resources.NoEmpty, WorkFlows.Properties.Resources.Note, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //MessageBox.Show("条件表达式不允许为空！");
                return;
            }
            string expression = dgvCondition.Rows[rowID].Cells["COND_EXPRESSION"].Value.ToString();

            if (dgvCondition.Rows[rowID].Cells["ORDER_ID"].Value == null || dgvCondition.Rows[rowID].Cells["ORDER_ID"].Value.ToString().Trim() == "")
            {
                MessageBox.Show(this, WorkFlows.Properties.Resources.NoEmpty, WorkFlows.Properties.Resources.Note, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //MessageBox.Show("序号不允许为空！");
                return;
            }
            string orderID = dgvCondition.Rows[rowID].Cells["ORDER_ID"].Value.ToString();

            if (dgvCondition.Rows[rowID].Cells["ALERT_MESSAGE"].Value == null || dgvCondition.Rows[rowID].Cells["ALERT_MESSAGE"].Value.ToString().Trim() == "")
            {
                MessageBox.Show(this, WorkFlows.Properties.Resources.NoEmpty, WorkFlows.Properties.Resources.Note, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //MessageBox.Show("告警语句不允许为空！");
                return;
            }
            string alert_message = dgvCondition.Rows[rowID].Cells["ALERT_MESSAGE"].Value.ToString();
            uint max;
            if (dgvCondition.Rows[rowID].Cells["COND_ID"].Value == null)
                max = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WK_CONDITION", "COND_ID");
            else
                max = Convert.ToUInt16(dgvCondition.Rows[rowID].Cells["COND_ID"].Value);

            FieldPara[] fieldTables = {new FieldPara("COND_ID",FieldType.Int,max.ToString()),
                      new FieldPara("F_PACKTYPENO",FieldType.Int,iPackNo.ToString()),
                      new FieldPara("LINK_OR_LINE",FieldType.Int,groupBox1.Enabled?iNodeId.ToString():iLineId.ToString()),
                      new FieldPara("COND_NAME",FieldType.String,name),
					  new FieldPara("ORDER_ID",FieldType.Int,orderID),
                      new FieldPara("ALERT_MESSAGE",FieldType.String,alert_message),
                      new FieldPara("COND_TYPE",FieldType.Int,groupBox1.Enabled?"0":"1")  //节点
                     };
            if (dgvCondition.Rows[rowID].Cells["COND_ID"].Value == null)  //插入
            {
                strSql = DBOpt.dbHelper.GetInserSql("DMIS_SYS_WK_CONDITION", fieldTables);
            }
            else　　//修改
            {
                WherePara[] where ={ new WherePara("COND_ID", FieldType.Int, dgvCondition.Rows[rowID].Cells["COND_ID"].Value.ToString(), "=", "and")};
                strSql = DBOpt.dbHelper.GetUpdateSql("DMIS_SYS_WK_CONDITION", fieldTables, where);
            }

            if (DBOpt.dbHelper.ExecuteSql(strSql) > 0)  //TABLE_FILTER_WHERE列可能保存to_char(datem,'yyyymm')这样的数据，故要用存储过程来更新
            {
                dgvCondition.Rows[rowID].Cells[0].Value = max;

                if (DBHelper.databaseType == "SqlServer")
                {

                }
                else if (DBHelper.databaseType == "Oracle")
                {
                    strSql = "update DMIS_SYS_WK_CONDITION set COND_EXPRESSION=:CondExpression where COND_ID=" + max.ToString() ;
                    OracleParameter[] aPara = new OracleParameter[1];
                    OracleParameter pContent = new OracleParameter("CondExpression", OracleType.VarChar);
                    pContent.Value = expression;
                    aPara[0] = pContent;
                    DBOpt.dbHelper.ExecuteSqlByParas(strSql, aPara);
                }
                else if (DBHelper.databaseType == "Sybase")
                {
                }
                else   //ODBC
                {
                }
            }
        }

        private void btnConDelete_Click(object sender, EventArgs e)
        {
            int rowID = dgvCondition.SelectedCells[0].RowIndex;
            if (dgvCondition.Rows[rowID].IsNewRow) return;

            if (dgvCondition.SelectedCells.Count == 0)
            {
                //MessageBox.Show("请选择要删除的条件！");
                return;
            }
            string name = dgvCondition.Rows[rowID].Cells["COND_NAME"].Value.ToString();
            
            //"是否要删除条件："
            if (MessageBox.Show(WorkFlows.Properties.Resources.DeleteBeforeConfirm, WorkFlows.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            strSql = "delete from DMIS_SYS_WK_CONDITION where COND_ID=" + dgvCondition.Rows[rowID].Cells["COND_ID"].Value.ToString();
            if (DBOpt.dbHelper.ExecuteSql(strSql) > 0)
                dgvCondition.Rows.Remove(dgvCondition.Rows[rowID]);
        }
        #endregion


        private void fc_Flow_ArrowSelecting(object sender, ArrowConfirmArgs e)
        {
            if (e.arrow == null) return;
            groupBox1.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            iLineId = Convert.ToInt16(e.arrow.Tag);

            dgvVariable.Rows.Clear();
            DataTable dt1 = DBOpt.dbHelper.GetDataTable("select VAR_ID,VAR_CODE,VAR_NAME,VAR_TYPE,MAP_TYPE,MAP_STATEMENT,REMARK from DMIS_SYS_WK_VARIABLE where F_PACKTYPENO=" + iPackNo + " and LINK_OR_LINE=" + iLineId + " and flag=1");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dgvVariable.Rows.Add(dt1.Rows[i]);
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    dgvVariable.Rows[i].Cells[j].Value = dt1.Rows[i][j];
                }
            }

            dgvCondition.Rows.Clear();
            dt1 = DBOpt.dbHelper.GetDataTable("select COND_ID,COND_NAME,COND_EXPRESSION,ALERT_MESSAGE,ORDER_ID from DMIS_SYS_WK_CONDITION where F_PACKTYPENO=" + iPackNo + " and LINK_OR_LINE=" + iLineId+" and COND_TYPE=1");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dgvCondition.Rows.Add(dt1.Rows[i]);
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    dgvCondition.Rows[i].Cells[j].Value = dt1.Rows[i][j];
                }
            }
        }

        private void btnSelectDoc_Click(object sender, EventArgs e)
        {
            if (iNodeId < 0) return;
            frmSelectDocType docType = new frmSelectDocType();
            docType.PackTypeID =iPackNo;
            if (docType.ShowDialog() == DialogResult.OK)
            {
                txtDocTypeID.Text = docType.DocTypeID.ToString();
                txtDocTypeName.Text = docType.DocTypeName;
                inFld();
            }
        }

        private void btnSaveLinkDoc_Click(object sender, EventArgs e)
        {
            if (txtDocTypeID.Text == "")
            {
                //MessageBox.Show("保存前请选择某一文档！");
                return;
            }
            //先删除相应的数据
            strSql = "delete from dmis_sys_wk_link_doctype where f_packtypeno="+iPackNo+" and f_linkno="+iNodeId;
            DBOpt.dbHelper.ExecuteSql(strSql);
            strSql = "delete from dmis_sys_flowfieldright where f_packtypeno=" + iPackNo + " and f_flowno=" + iNodeId + " and f_roleno=" + iselRoleNo;
            DBOpt.dbHelper.ExecuteSql(strSql);
            //再插入
            uint iMax = DBOpt.dbHelper.GetMaxNum("dmis_sys_wk_link_doctype", "tid");
            strSql = "insert into dmis_sys_wk_link_doctype(tid,f_packtypeno,f_linkno,f_doctypeno) values(" +
                +iMax + "," + iPackNo + "," + iNodeId + "," + txtDocTypeID.Text + ")";
            DBOpt.dbHelper.ExecuteSql(strSql);
            string tableName = DBOpt.dbHelper.ExecuteScalar("select f_tablename from dmis_sys_doctype where f_no=" + txtDocTypeID.Text).ToString();
            if (lv_Field.Items.Count < 1) return;
            iMax = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_FLOWFIELDRIGHT", "F_NO");
            for (int i = 0; i < lv_Field.CheckedItems.Count; i++)
            {
                strSql = "INSERT INTO DMIS_SYS_FLOWFIELDRIGHT(F_NO,F_PACKTYPENO,F_FLOWNO,F_ROLENO,F_TABLENAME,F_FIELDNAME,F_RIGHT) VALUES("
                    + iMax + "," + iPackNo + "," + iNodeId + "," + iselRoleNo + ",'" + tableName
                    + "','" + lv_Field.CheckedItems[i].Text + "','1100000')";
                DBOpt.dbHelper.ExecuteSql(strSql);
                iMax++;
            }
        }

        private void initNodeType()
        {
            ComboxItem item1 = new ComboxItem(WorkFlows.Properties.Resources.NodeTypeNormal, "0");//"正常"
            cbbNodeType.Items.Add(item1);
            ComboxItem item2 = new ComboxItem(WorkFlows.Properties.Resources.NodeTypeBranch, "1");//"分支"
            cbbNodeType.Items.Add(item2);
            ComboxItem item3 = new ComboxItem(WorkFlows.Properties.Resources.NodeTypeCollect, "2");//"汇集"
            cbbNodeType.Items.Add(item3);
        }

        private void cbbNodeType_Leave(object sender, EventArgs e)
        {
            if (iNodeId < 1) return;
            if (cbbNodeType.SelectedItem == null) return;
            ComboxItem item=(ComboxItem)cbbNodeType.SelectedItem;
            strSql = "update dmis_sys_flowlink set F_NODETYPE=" + item.Value + " where f_no=" + iNodeId;
            DBOpt.dbHelper.ExecuteSql(strSql);
        }

        private void txtInceptHours_Validating(object sender, CancelEventArgs e)
        {
            int hours;
            if (!(int.TryParse(txtInceptHours.Text, out hours) || txtInceptHours.Text == ""))
            {
                errorProvider1.SetError((Control)sender,WorkFlows.Properties.Resources.NumericalValeError);//"必须整数！"
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError((Control)sender, "");
            }
        }

        private void txtInceptHours_Leave(object sender, EventArgs e)
        {
            if (iNodeId < 0) return;
            strSql = "UPDATE DMIS_SYS_FLOWLINK SET F_INCEPT_HOURS=" + txtInceptHours.Text + " WHERE F_NO=" + iNodeId;
            DBOpt.dbHelper.ExecuteSql(strSql);
        }

        private void txtOTHER_LANGUAGE_DESCR_Leave(object sender, EventArgs e)
        {
            if (iNodeId < 0) return;
            strSql = "UPDATE DMIS_SYS_FLOWLINK SET OTHER_LANGUAGE_DESCR='" + txtOTHER_LANGUAGE_DESCR.Text + "' WHERE F_NO=" + iNodeId;
            DBOpt.dbHelper.ExecuteSql(strSql);
        }

        
       
    }
}
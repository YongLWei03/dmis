namespace PlatForm.WorkFlow
{
    partial class frmFlow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFlow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.trvPack = new System.Windows.Forms.TreeView();
            this.cm_Pack = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cm_pack_new = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_new_pack = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_new_doc = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_pack_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_pack_del = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cm_pack_right = new System.Windows.Forms.ToolStripMenuItem();
            this.img1 = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Cond = new System.Windows.Forms.TextBox();
            this.txtDays = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lv_Field = new System.Windows.Forms.ListView();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_Doc = new System.Windows.Forms.ComboBox();
            this.cb_Role = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.fc_Flow = new MindFusion.FlowChartX.FlowChart();
            this.tbFlow = new System.Windows.Forms.ToolStrip();
            this.tbSel = new System.Windows.Forms.ToolStripButton();
            this.tbSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbStart = new System.Windows.Forms.ToolStripButton();
            this.tbNode = new System.Windows.Forms.ToolStripButton();
            this.tbEnd = new System.Windows.Forms.ToolStripButton();
            this.tbNote = new System.Windows.Forms.ToolStripButton();
            this.tbSep = new System.Windows.Forms.ToolStripSeparator();
            this.tbLine = new System.Windows.Forms.ToolStripButton();
            this.tbLine1 = new System.Windows.Forms.ToolStripButton();
            this.tbSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbUndo = new System.Windows.Forms.ToolStripButton();
            this.tbRedo = new System.Windows.Forms.ToolStripButton();
            this.tbDel = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtInceptHours = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbbNodeType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ckbIsAssign = new System.Windows.Forms.CheckBox();
            this.lv_Role = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtDocTypeID = new System.Windows.Forms.TextBox();
            this.btnSelectDoc = new System.Windows.Forms.Button();
            this.txtDocTypeName = new System.Windows.Forms.TextBox();
            this.btnSaveLinkDoc = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbnNo = new System.Windows.Forms.RadioButton();
            this.rbnRelativeTache = new System.Windows.Forms.RadioButton();
            this.btnSaveEntity = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSelectEntity = new System.Windows.Forms.Button();
            this.rbnRole = new System.Windows.Forms.RadioButton();
            this.txtEntiID = new System.Windows.Forms.TextBox();
            this.rbnMember = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSaveTimesStatPara = new System.Windows.Forms.Button();
            this.cbbSTARTTIME_COLUMN = new System.Windows.Forms.ComboBox();
            this.cbbENDTIME_COLUMN = new System.Windows.Forms.ComboBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnVarDelete = new System.Windows.Forms.Button();
            this.btnVarModify = new System.Windows.Forms.Button();
            this.dgvVariable = new System.Windows.Forms.DataGridView();
            this.VAR_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VAR_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VAR_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VAR_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAP_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAP_STATEMENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btnConDelete = new System.Windows.Forms.Button();
            this.btnConModify = new System.Windows.Forms.Button();
            this.dgvCondition = new System.Windows.Forms.DataGridView();
            this.COND_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COND_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COND_EXPRESSION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALERT_MESSAGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dsPara = new System.Data.DataSet();
            this.VarTypePara = new System.Data.DataTable();
            this.VarTypeCode = new System.Data.DataColumn();
            this.VarTypeName = new System.Data.DataColumn();
            this.MapTypePara = new System.Data.DataTable();
            this.MapTypeCode = new System.Data.DataColumn();
            this.MapTypeName = new System.Data.DataColumn();
            this.panel1.SuspendLayout();
            this.cm_Pack.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tbFlow.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVariable)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPara)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VarTypePara)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapTypePara)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trvPack);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // trvPack
            // 
            this.trvPack.ContextMenuStrip = this.cm_Pack;
            resources.ApplyResources(this.trvPack, "trvPack");
            this.trvPack.ImageList = this.img1;
            this.trvPack.Name = "trvPack";
            this.trvPack.ShowNodeToolTips = true;
            this.trvPack.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_pack_AfterSelect);
            // 
            // cm_Pack
            // 
            this.cm_Pack.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cm_pack_new,
            this.cm_pack_edit,
            this.cm_pack_del,
            this.toolStripMenuItem1,
            this.cm_pack_right});
            this.cm_Pack.Name = "cm_Pack";
            resources.ApplyResources(this.cm_Pack, "cm_Pack");
            // 
            // cm_pack_new
            // 
            this.cm_pack_new.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cm_new_pack,
            this.cm_new_doc});
            this.cm_pack_new.Name = "cm_pack_new";
            resources.ApplyResources(this.cm_pack_new, "cm_pack_new");
            // 
            // cm_new_pack
            // 
            this.cm_new_pack.Name = "cm_new_pack";
            resources.ApplyResources(this.cm_new_pack, "cm_new_pack");
            this.cm_new_pack.Click += new System.EventHandler(this.cm_new_pack_Click);
            // 
            // cm_new_doc
            // 
            this.cm_new_doc.Name = "cm_new_doc";
            resources.ApplyResources(this.cm_new_doc, "cm_new_doc");
            this.cm_new_doc.Click += new System.EventHandler(this.cm_new_doc_Click);
            // 
            // cm_pack_edit
            // 
            this.cm_pack_edit.Name = "cm_pack_edit";
            resources.ApplyResources(this.cm_pack_edit, "cm_pack_edit");
            this.cm_pack_edit.Click += new System.EventHandler(this.cm_pack_edit_Click);
            // 
            // cm_pack_del
            // 
            this.cm_pack_del.Name = "cm_pack_del";
            resources.ApplyResources(this.cm_pack_del, "cm_pack_del");
            this.cm_pack_del.Click += new System.EventHandler(this.cm_pack_del_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // cm_pack_right
            // 
            this.cm_pack_right.Name = "cm_pack_right";
            resources.ApplyResources(this.cm_pack_right, "cm_pack_right");
            this.cm_pack_right.Click += new System.EventHandler(this.cm_pack_right_Click);
            // 
            // img1
            // 
            this.img1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img1.ImageStream")));
            this.img1.TransparentColor = System.Drawing.Color.Transparent;
            this.img1.Images.SetKeyName(0, "comp0.gif");
            this.img1.Images.SetKeyName(1, "comp1.gif");
            this.img1.Images.SetKeyName(2, "www6.gif");
            this.img1.Images.SetKeyName(3, "www4.gif");
            this.img1.Images.SetKeyName(4, "start.gif");
            this.img1.Images.SetKeyName(5, "node.gif");
            this.img1.Images.SetKeyName(6, "end.gif");
            this.img1.Images.SetKeyName(7, "line.gif");
            this.img1.Images.SetKeyName(8, "note.gif");
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tb_Cond
            // 
            resources.ApplyResources(this.tb_Cond, "tb_Cond");
            this.tb_Cond.Name = "tb_Cond";
            this.tb_Cond.Leave += new System.EventHandler(this.tb_Cond_Leave);
            // 
            // txtDays
            // 
            resources.ApplyResources(this.txtDays, "txtDays");
            this.txtDays.Name = "txtDays";
            this.txtDays.Leave += new System.EventHandler(this.tb_Day_Leave);
            this.txtDays.Validating += new System.ComponentModel.CancelEventHandler(this.txtDays_Validating);
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.Leave += new System.EventHandler(this.tb_Name_Leave);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lv_Field
            // 
            resources.ApplyResources(this.lv_Field, "lv_Field");
            this.lv_Field.CheckBoxes = true;
            this.lv_Field.Name = "lv_Field";
            this.lv_Field.UseCompatibleStateImageBehavior = false;
            this.lv_Field.View = System.Windows.Forms.View.List;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // cb_Doc
            // 
            this.cb_Doc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Doc.FormattingEnabled = true;
            resources.ApplyResources(this.cb_Doc, "cb_Doc");
            this.cb_Doc.Name = "cb_Doc";
            // 
            // cb_Role
            // 
            this.cb_Role.FormattingEnabled = true;
            resources.ApplyResources(this.cb_Role, "cb_Role");
            this.cb_Role.Name = "cb_Role";
            this.cb_Role.SelectedIndexChanged += new System.EventHandler(this.cb_Role_SelectedIndexChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.fc_Flow);
            this.panel4.Controls.Add(this.tbFlow);
            this.panel4.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // fc_Flow
            // 
            this.fc_Flow.AlignToGrid = false;
            this.fc_Flow.AllowInplaceEdit = true;
            resources.ApplyResources(this.fc_Flow, "fc_Flow");
            this.fc_Flow.ArrowBrush = new MindFusion.FlowChartX.SolidBrush("#FF000000");
            this.fc_Flow.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fc_Flow.ArrowHead = MindFusion.FlowChartX.EArrowHead.ahTriangle;
            this.fc_Flow.ArrowHeadSize = 3F;
            this.fc_Flow.ArrowPen = new MindFusion.FlowChartX.Pen("0;#FF000000;0;0;0;");
            this.fc_Flow.ArrowSegments = ((short)(2));
            this.fc_Flow.BackBrush = new MindFusion.FlowChartX.SolidBrush("#FFAAAAC8");
            this.fc_Flow.BoxBrush = new MindFusion.FlowChartX.SolidBrush("#FFDCDCFF");
            this.fc_Flow.BoxFrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fc_Flow.BoxPen = new MindFusion.FlowChartX.Pen("0;#FF000000;0;0;0;");
            this.fc_Flow.BoxText = "节点";
            this.fc_Flow.CurArrowCannotCreate = System.Windows.Forms.Cursors.No;
            this.fc_Flow.CurArrowEnd = System.Windows.Forms.Cursors.Hand;
            this.fc_Flow.CurArrowStart = System.Windows.Forms.Cursors.Hand;
            this.fc_Flow.CurCannotCreate = System.Windows.Forms.Cursors.No;
            this.fc_Flow.CurHorzResize = System.Windows.Forms.Cursors.SizeWE;
            this.fc_Flow.CurMainDgnlResize = System.Windows.Forms.Cursors.SizeNWSE;
            this.fc_Flow.CurModify = System.Windows.Forms.Cursors.SizeAll;
            this.fc_Flow.CurPointer = System.Windows.Forms.Cursors.Arrow;
            this.fc_Flow.CurSecDgnlResize = System.Windows.Forms.Cursors.SizeNESW;
            this.fc_Flow.CurVertResize = System.Windows.Forms.Cursors.SizeNS;
            this.fc_Flow.DefaultControlType = typeof(System.Windows.Forms.Button);
            this.fc_Flow.DocExtents = ((System.Drawing.RectangleF)(resources.GetObject("fc_Flow.DocExtents")));
            this.fc_Flow.InplaceEditFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fc_Flow.Name = "fc_Flow";
            this.fc_Flow.ShadowsStyle = MindFusion.FlowChartX.EShadowsStyle.shdNone;
            this.fc_Flow.TableBrush = new MindFusion.FlowChartX.SolidBrush("#FFB4A0A0");
            this.fc_Flow.TableCellBorders = MindFusion.FlowChartX.ECellBorders.cbSimple;
            this.fc_Flow.TableFrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fc_Flow.TablePen = new MindFusion.FlowChartX.Pen("0;#FF000000;0;0;0;");
            this.fc_Flow.TextFormat.Alignment = System.Drawing.StringAlignment.Center;
            this.fc_Flow.TextFormat.FormatFlags = System.Drawing.StringFormatFlags.NoFontFallback;
            this.fc_Flow.TextFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            this.fc_Flow.TextFormat.LineAlignment = System.Drawing.StringAlignment.Center;
            this.fc_Flow.TextFormat.Trimming = System.Drawing.StringTrimming.Character;
            this.fc_Flow.ArrowDeleted += new MindFusion.FlowChartX.ArrowEvent(this.fc_Flow_ArrowDeleted);
            this.fc_Flow.ArrowCreated += new MindFusion.FlowChartX.ArrowEvent(this.fc_Flow_ArrowCreated);
            this.fc_Flow.BoxTextEdited += new MindFusion.FlowChartX.BoxTextEditedEvent(this.fc_Flow_BoxTextEdited);
            this.fc_Flow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fc_Flow_MouseDown);
            this.fc_Flow.ActionUndone += new MindFusion.FlowChartX.UndoEvent(this.fc_Flow_ActionUndone);
            this.fc_Flow.BoxModified += new MindFusion.FlowChartX.BoxMouseEvent(this.fc_Flow_BoxModified);
            this.fc_Flow.BoxCreating += new MindFusion.FlowChartX.BoxConfirmation(this.fc_Flow_BoxCreating);
            this.fc_Flow.BoxDeleted += new MindFusion.FlowChartX.BoxEvent(this.fc_Flow_BoxDeleted);
            this.fc_Flow.ActionRedone += new MindFusion.FlowChartX.UndoEvent(this.fc_Flow_ActionRedone);
            this.fc_Flow.BoxSelecting += new MindFusion.FlowChartX.BoxConfirmation(this.fc_Flow_BoxSelecting);
            this.fc_Flow.ArrowSelecting += new MindFusion.FlowChartX.ArrowConfirmation(this.fc_Flow_ArrowSelecting);
            this.fc_Flow.ArrowDeleting += new MindFusion.FlowChartX.ArrowConfirmation(this.fc_Flow_ArrowDeleting);
            this.fc_Flow.ArrowCreating += new MindFusion.FlowChartX.AttachConfirmation(this.fc_Flow_ArrowCreating);
            this.fc_Flow.BoxDeleting += new MindFusion.FlowChartX.BoxConfirmation(this.fc_Flow_BoxDeleting);
            // 
            // tbFlow
            // 
            this.tbFlow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbSel,
            this.tbSep2,
            this.tbStart,
            this.tbNode,
            this.tbEnd,
            this.tbNote,
            this.tbSep,
            this.tbLine,
            this.tbLine1,
            this.tbSep3,
            this.tbUndo,
            this.tbRedo,
            this.tbDel});
            resources.ApplyResources(this.tbFlow, "tbFlow");
            this.tbFlow.Name = "tbFlow";
            // 
            // tbSel
            // 
            this.tbSel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbSel, "tbSel");
            this.tbSel.Name = "tbSel";
            this.tbSel.Click += new System.EventHandler(this.tbSel_Click);
            // 
            // tbSep2
            // 
            this.tbSep2.Name = "tbSep2";
            resources.ApplyResources(this.tbSep2, "tbSep2");
            // 
            // tbStart
            // 
            this.tbStart.BackColor = System.Drawing.SystemColors.Control;
            this.tbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbStart, "tbStart");
            this.tbStart.Name = "tbStart";
            this.tbStart.Click += new System.EventHandler(this.tbStart_Click);
            // 
            // tbNode
            // 
            this.tbNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbNode, "tbNode");
            this.tbNode.Name = "tbNode";
            this.tbNode.Click += new System.EventHandler(this.tbNode_Click);
            // 
            // tbEnd
            // 
            this.tbEnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbEnd, "tbEnd");
            this.tbEnd.Name = "tbEnd";
            this.tbEnd.Click += new System.EventHandler(this.tbEnd_Click);
            // 
            // tbNote
            // 
            this.tbNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbNote, "tbNote");
            this.tbNote.Name = "tbNote";
            this.tbNote.Click += new System.EventHandler(this.tbNote_Click);
            // 
            // tbSep
            // 
            this.tbSep.Name = "tbSep";
            resources.ApplyResources(this.tbSep, "tbSep");
            // 
            // tbLine
            // 
            this.tbLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbLine, "tbLine");
            this.tbLine.Name = "tbLine";
            this.tbLine.Click += new System.EventHandler(this.tbLine_Click);
            // 
            // tbLine1
            // 
            this.tbLine1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbLine1, "tbLine1");
            this.tbLine1.Name = "tbLine1";
            this.tbLine1.Click += new System.EventHandler(this.tbLine1_Click);
            // 
            // tbSep3
            // 
            this.tbSep3.Name = "tbSep3";
            resources.ApplyResources(this.tbSep3, "tbSep3");
            // 
            // tbUndo
            // 
            this.tbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbUndo, "tbUndo");
            this.tbUndo.Name = "tbUndo";
            this.tbUndo.Click += new System.EventHandler(this.tbUndo_Click);
            // 
            // tbRedo
            // 
            this.tbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbRedo, "tbRedo");
            this.tbRedo.Name = "tbRedo";
            this.tbRedo.Click += new System.EventHandler(this.tbRedo_Click);
            // 
            // tbDel
            // 
            this.tbDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbDel, "tbDel");
            this.tbDel.Name = "tbDel";
            this.tbDel.Click += new System.EventHandler(this.tbDel_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtOTHER_LANGUAGE_DESCR);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtInceptHours);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbbNodeType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ckbIsAssign);
            this.groupBox1.Controls.Add(this.tb_Cond);
            this.groupBox1.Controls.Add(this.lv_Role);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDays);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.txtOTHER_LANGUAGE_DESCR, "txtOTHER_LANGUAGE_DESCR");
            this.txtOTHER_LANGUAGE_DESCR.Name = "txtOTHER_LANGUAGE_DESCR";
            this.txtOTHER_LANGUAGE_DESCR.Leave += new System.EventHandler(this.txtOTHER_LANGUAGE_DESCR_Leave);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtInceptHours
            // 
            resources.ApplyResources(this.txtInceptHours, "txtInceptHours");
            this.txtInceptHours.Name = "txtInceptHours";
            this.txtInceptHours.Leave += new System.EventHandler(this.txtInceptHours_Leave);
            this.txtInceptHours.Validating += new System.ComponentModel.CancelEventHandler(this.txtInceptHours_Validating);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // cbbNodeType
            // 
            this.cbbNodeType.DisplayMember = "Display";
            this.cbbNodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNodeType.FormattingEnabled = true;
            resources.ApplyResources(this.cbbNodeType, "cbbNodeType");
            this.cbbNodeType.Name = "cbbNodeType";
            this.cbbNodeType.ValueMember = "Value";
            this.cbbNodeType.Leave += new System.EventHandler(this.cbbNodeType_Leave);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // ckbIsAssign
            // 
            resources.ApplyResources(this.ckbIsAssign, "ckbIsAssign");
            this.ckbIsAssign.Name = "ckbIsAssign";
            this.ckbIsAssign.UseVisualStyleBackColor = true;
            this.ckbIsAssign.Leave += new System.EventHandler(this.ckbIsAssign_Leave);
            // 
            // lv_Role
            // 
            resources.ApplyResources(this.lv_Role, "lv_Role");
            this.lv_Role.CheckBoxes = true;
            this.lv_Role.Name = "lv_Role";
            this.lv_Role.UseCompatibleStateImageBehavior = false;
            this.lv_Role.View = System.Windows.Forms.View.List;
            this.lv_Role.Leave += new System.EventHandler(this.lv_Role_Leave);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.txtDocTypeID);
            this.groupBox3.Controls.Add(this.btnSelectDoc);
            this.groupBox3.Controls.Add(this.txtDocTypeName);
            this.groupBox3.Controls.Add(this.btnSaveLinkDoc);
            this.groupBox3.Controls.Add(this.lv_Field);
            this.groupBox3.Controls.Add(this.cb_Doc);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.cb_Role);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // txtDocTypeID
            // 
            resources.ApplyResources(this.txtDocTypeID, "txtDocTypeID");
            this.txtDocTypeID.Name = "txtDocTypeID";
            this.txtDocTypeID.ReadOnly = true;
            // 
            // btnSelectDoc
            // 
            resources.ApplyResources(this.btnSelectDoc, "btnSelectDoc");
            this.btnSelectDoc.Name = "btnSelectDoc";
            this.btnSelectDoc.UseVisualStyleBackColor = true;
            this.btnSelectDoc.Click += new System.EventHandler(this.btnSelectDoc_Click);
            // 
            // txtDocTypeName
            // 
            resources.ApplyResources(this.txtDocTypeName, "txtDocTypeName");
            this.txtDocTypeName.Name = "txtDocTypeName";
            this.txtDocTypeName.ReadOnly = true;
            // 
            // btnSaveLinkDoc
            // 
            resources.ApplyResources(this.btnSaveLinkDoc, "btnSaveLinkDoc");
            this.btnSaveLinkDoc.Name = "btnSaveLinkDoc";
            this.btnSaveLinkDoc.UseVisualStyleBackColor = true;
            this.btnSaveLinkDoc.Click += new System.EventHandler(this.btnSaveLinkDoc_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox4);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.rbnNo);
            this.groupBox4.Controls.Add(this.rbnRelativeTache);
            this.groupBox4.Controls.Add(this.btnSaveEntity);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.btnSelectEntity);
            this.groupBox4.Controls.Add(this.rbnRole);
            this.groupBox4.Controls.Add(this.txtEntiID);
            this.groupBox4.Controls.Add(this.rbnMember);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // rbnNo
            // 
            resources.ApplyResources(this.rbnNo, "rbnNo");
            this.rbnNo.Name = "rbnNo";
            this.rbnNo.TabStop = true;
            this.rbnNo.UseVisualStyleBackColor = true;
            // 
            // rbnRelativeTache
            // 
            resources.ApplyResources(this.rbnRelativeTache, "rbnRelativeTache");
            this.rbnRelativeTache.Name = "rbnRelativeTache";
            this.rbnRelativeTache.TabStop = true;
            this.rbnRelativeTache.UseVisualStyleBackColor = true;
            // 
            // btnSaveEntity
            // 
            resources.ApplyResources(this.btnSaveEntity, "btnSaveEntity");
            this.btnSaveEntity.Name = "btnSaveEntity";
            this.btnSaveEntity.UseVisualStyleBackColor = true;
            this.btnSaveEntity.Click += new System.EventHandler(this.btnSaveEntity_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // btnSelectEntity
            // 
            resources.ApplyResources(this.btnSelectEntity, "btnSelectEntity");
            this.btnSelectEntity.Name = "btnSelectEntity";
            this.btnSelectEntity.UseVisualStyleBackColor = true;
            this.btnSelectEntity.Click += new System.EventHandler(this.btnSelectEntity_Click);
            // 
            // rbnRole
            // 
            resources.ApplyResources(this.rbnRole, "rbnRole");
            this.rbnRole.Name = "rbnRole";
            this.rbnRole.TabStop = true;
            this.rbnRole.UseVisualStyleBackColor = true;
            // 
            // txtEntiID
            // 
            resources.ApplyResources(this.txtEntiID, "txtEntiID");
            this.txtEntiID.Name = "txtEntiID";
            // 
            // rbnMember
            // 
            resources.ApplyResources(this.rbnMember, "rbnMember");
            this.rbnMember.Name = "rbnMember";
            this.rbnMember.TabStop = true;
            this.rbnMember.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox5);
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.btnSaveTimesStatPara);
            this.groupBox5.Controls.Add(this.cbbSTARTTIME_COLUMN);
            this.groupBox5.Controls.Add(this.cbbENDTIME_COLUMN);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // btnSaveTimesStatPara
            // 
            resources.ApplyResources(this.btnSaveTimesStatPara, "btnSaveTimesStatPara");
            this.btnSaveTimesStatPara.Name = "btnSaveTimesStatPara";
            this.btnSaveTimesStatPara.UseVisualStyleBackColor = true;
            this.btnSaveTimesStatPara.Click += new System.EventHandler(this.btnSaveTimesStatPara_Click);
            // 
            // cbbSTARTTIME_COLUMN
            // 
            this.cbbSTARTTIME_COLUMN.FormattingEnabled = true;
            resources.ApplyResources(this.cbbSTARTTIME_COLUMN, "cbbSTARTTIME_COLUMN");
            this.cbbSTARTTIME_COLUMN.Name = "cbbSTARTTIME_COLUMN";
            // 
            // cbbENDTIME_COLUMN
            // 
            this.cbbENDTIME_COLUMN.FormattingEnabled = true;
            resources.ApplyResources(this.cbbENDTIME_COLUMN, "cbbENDTIME_COLUMN");
            this.cbbENDTIME_COLUMN.Name = "cbbENDTIME_COLUMN";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnVarDelete);
            this.tabPage6.Controls.Add(this.btnVarModify);
            this.tabPage6.Controls.Add(this.dgvVariable);
            resources.ApplyResources(this.tabPage6, "tabPage6");
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btnVarDelete
            // 
            resources.ApplyResources(this.btnVarDelete, "btnVarDelete");
            this.btnVarDelete.Name = "btnVarDelete";
            this.btnVarDelete.UseVisualStyleBackColor = true;
            this.btnVarDelete.Click += new System.EventHandler(this.btnVarDelete_Click);
            // 
            // btnVarModify
            // 
            resources.ApplyResources(this.btnVarModify, "btnVarModify");
            this.btnVarModify.Name = "btnVarModify";
            this.btnVarModify.UseVisualStyleBackColor = true;
            this.btnVarModify.Click += new System.EventHandler(this.btnVarModify_Click);
            // 
            // dgvVariable
            // 
            resources.ApplyResources(this.dgvVariable, "dgvVariable");
            this.dgvVariable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVariable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VAR_ID,
            this.VAR_CODE,
            this.VAR_NAME,
            this.VAR_TYPE,
            this.MAP_TYPE,
            this.MAP_STATEMENT,
            this.REMARK});
            this.dgvVariable.MultiSelect = false;
            this.dgvVariable.Name = "dgvVariable";
            this.dgvVariable.RowTemplate.Height = 23;
            // 
            // VAR_ID
            // 
            resources.ApplyResources(this.VAR_ID, "VAR_ID");
            this.VAR_ID.Name = "VAR_ID";
            this.VAR_ID.ReadOnly = true;
            // 
            // VAR_CODE
            // 
            resources.ApplyResources(this.VAR_CODE, "VAR_CODE");
            this.VAR_CODE.Name = "VAR_CODE";
            // 
            // VAR_NAME
            // 
            resources.ApplyResources(this.VAR_NAME, "VAR_NAME");
            this.VAR_NAME.Name = "VAR_NAME";
            // 
            // VAR_TYPE
            // 
            resources.ApplyResources(this.VAR_TYPE, "VAR_TYPE");
            this.VAR_TYPE.Name = "VAR_TYPE";
            this.VAR_TYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MAP_TYPE
            // 
            resources.ApplyResources(this.MAP_TYPE, "MAP_TYPE");
            this.MAP_TYPE.Name = "MAP_TYPE";
            this.MAP_TYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MAP_STATEMENT
            // 
            resources.ApplyResources(this.MAP_STATEMENT, "MAP_STATEMENT");
            this.MAP_STATEMENT.Name = "MAP_STATEMENT";
            // 
            // REMARK
            // 
            resources.ApplyResources(this.REMARK, "REMARK");
            this.REMARK.Name = "REMARK";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btnConDelete);
            this.tabPage7.Controls.Add(this.btnConModify);
            this.tabPage7.Controls.Add(this.dgvCondition);
            resources.ApplyResources(this.tabPage7, "tabPage7");
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btnConDelete
            // 
            resources.ApplyResources(this.btnConDelete, "btnConDelete");
            this.btnConDelete.Name = "btnConDelete";
            this.btnConDelete.UseVisualStyleBackColor = true;
            this.btnConDelete.Click += new System.EventHandler(this.btnConDelete_Click);
            // 
            // btnConModify
            // 
            resources.ApplyResources(this.btnConModify, "btnConModify");
            this.btnConModify.Name = "btnConModify";
            this.btnConModify.UseVisualStyleBackColor = true;
            this.btnConModify.Click += new System.EventHandler(this.btnConModify_Click);
            // 
            // dgvCondition
            // 
            resources.ApplyResources(this.dgvCondition, "dgvCondition");
            this.dgvCondition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCondition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.COND_ID,
            this.COND_NAME,
            this.COND_EXPRESSION,
            this.ALERT_MESSAGE,
            this.ORDER_ID});
            this.dgvCondition.MultiSelect = false;
            this.dgvCondition.Name = "dgvCondition";
            this.dgvCondition.RowTemplate.Height = 23;
            // 
            // COND_ID
            // 
            resources.ApplyResources(this.COND_ID, "COND_ID");
            this.COND_ID.Name = "COND_ID";
            this.COND_ID.ReadOnly = true;
            // 
            // COND_NAME
            // 
            resources.ApplyResources(this.COND_NAME, "COND_NAME");
            this.COND_NAME.Name = "COND_NAME";
            // 
            // COND_EXPRESSION
            // 
            resources.ApplyResources(this.COND_EXPRESSION, "COND_EXPRESSION");
            this.COND_EXPRESSION.Name = "COND_EXPRESSION";
            // 
            // ALERT_MESSAGE
            // 
            resources.ApplyResources(this.ALERT_MESSAGE, "ALERT_MESSAGE");
            this.ALERT_MESSAGE.Name = "ALERT_MESSAGE";
            this.ALERT_MESSAGE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ORDER_ID
            // 
            resources.ApplyResources(this.ORDER_ID, "ORDER_ID");
            this.ORDER_ID.Name = "ORDER_ID";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // dsPara
            // 
            this.dsPara.DataSetName = "NewDataSet";
            this.dsPara.Tables.AddRange(new System.Data.DataTable[] {
            this.VarTypePara,
            this.MapTypePara});
            // 
            // VarTypePara
            // 
            this.VarTypePara.Columns.AddRange(new System.Data.DataColumn[] {
            this.VarTypeCode,
            this.VarTypeName});
            this.VarTypePara.TableName = "VarTypePara";
            // 
            // VarTypeCode
            // 
            this.VarTypeCode.ColumnName = "VarTypeCode";
            // 
            // VarTypeName
            // 
            this.VarTypeName.ColumnName = "VarTypeName";
            // 
            // MapTypePara
            // 
            this.MapTypePara.Columns.AddRange(new System.Data.DataColumn[] {
            this.MapTypeCode,
            this.MapTypeName});
            this.MapTypePara.TableName = "MapTypePara";
            // 
            // MapTypeCode
            // 
            this.MapTypeCode.ColumnName = "MapTypeCode";
            this.MapTypeCode.DataType = typeof(short);
            // 
            // MapTypeName
            // 
            this.MapTypeName.ColumnName = "MapTypeName";
            // 
            // frmFlow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "frmFlow";
            this.Load += new System.EventHandler(this.frmFlow_Load);
            this.panel1.ResumeLayout(false);
            this.cm_Pack.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tbFlow.ResumeLayout(false);
            this.tbFlow.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVariable)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPara)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VarTypePara)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapTypePara)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDays;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TreeView trvPack;
        private System.Windows.Forms.ContextMenuStrip cm_Pack;
        private System.Windows.Forms.ToolStripMenuItem cm_pack_new;
        private System.Windows.Forms.ToolStripMenuItem cm_pack_edit;
        private System.Windows.Forms.ToolStripMenuItem cm_pack_del;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cm_pack_right;
        private System.Windows.Forms.ToolStripMenuItem cm_new_pack;
        private System.Windows.Forms.ToolStripMenuItem cm_new_doc;
        private System.Windows.Forms.ImageList img1;
        private System.Windows.Forms.ListView lv_Field;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cb_Doc;
        private System.Windows.Forms.ComboBox cb_Role;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_Cond;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private MindFusion.FlowChartX.FlowChart fc_Flow;
        private System.Windows.Forms.ToolStrip tbFlow;
        private System.Windows.Forms.ToolStripButton tbStart;
        private System.Windows.Forms.ToolStripButton tbNode;
        private System.Windows.Forms.ToolStripButton tbEnd;
        private System.Windows.Forms.ToolStripButton tbLine;
        private System.Windows.Forms.ToolStripSeparator tbSep;
        private System.Windows.Forms.ToolStripButton tbNote;
        private System.Windows.Forms.ToolStripButton tbLine1;
        private System.Windows.Forms.ToolStripSeparator tbSep3;
        private System.Windows.Forms.ToolStripButton tbUndo;
        private System.Windows.Forms.ToolStripButton tbRedo;
        private System.Windows.Forms.ToolStripButton tbDel;
        private System.Windows.Forms.ToolStripButton tbSel;
        private System.Windows.Forms.ToolStripSeparator tbSep2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lv_Role;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbnRelativeTache;
        private System.Windows.Forms.RadioButton rbnMember;
        private System.Windows.Forms.RadioButton rbnRole;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtEntiID;
        private System.Windows.Forms.Button btnSelectEntity;
        private System.Windows.Forms.CheckBox ckbIsAssign;
        private System.Windows.Forms.Button btnSaveEntity;
        private System.Windows.Forms.RadioButton rbnNo;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ComboBox cbbSTARTTIME_COLUMN;
        private System.Windows.Forms.Button btnSaveTimesStatPara;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbbENDTIME_COLUMN;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.DataGridView dgvVariable;
        private System.Data.DataSet dsPara;
        private System.Data.DataTable VarTypePara;
        private System.Data.DataColumn VarTypeCode;
        private System.Data.DataColumn VarTypeName;
        private System.Data.DataTable MapTypePara;
        private System.Data.DataColumn MapTypeCode;
        private System.Data.DataColumn MapTypeName;
        private System.Windows.Forms.Button btnVarDelete;
        private System.Windows.Forms.Button btnVarModify;
        private System.Windows.Forms.Button btnConDelete;
        private System.Windows.Forms.Button btnConModify;
        private System.Windows.Forms.DataGridView dgvCondition;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtDocTypeName;
        private System.Windows.Forms.Button btnSaveLinkDoc;
        private System.Windows.Forms.Button btnSelectDoc;
        private System.Windows.Forms.TextBox txtDocTypeID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbNodeType;
        private System.Windows.Forms.TextBox txtInceptHours;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn VAR_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VAR_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VAR_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn VAR_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAP_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAP_STATEMENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARK;
        private System.Windows.Forms.DataGridViewTextBoxColumn COND_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn COND_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn COND_EXPRESSION;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALERT_MESSAGE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_ID;
        private System.Windows.Forms.TextBox txtOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.Label label14;
    }
}


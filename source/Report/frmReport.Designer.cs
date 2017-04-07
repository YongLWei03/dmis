namespace PlatForm.DmisReport
{
    partial class frmReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReport));
            this.trvReport = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbAdd = new System.Windows.Forms.ToolStripButton();
            this.tlbDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.tlbAllColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tmiAdd序号 = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiUpdateColOtherDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.tlbFindColumnType = new System.Windows.Forms.ToolStripButton();
            this.tabReport = new System.Windows.Forms.TabControl();
            this.tpgReport = new System.Windows.Forms.TabPage();
            this.txtOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.m_cell = new AxCELL50Lib.AxCell();
            this.cbbType = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.picReportFile = new System.Windows.Forms.PictureBox();
            this.txtORDER_ID = new System.Windows.Forms.TextBox();
            this.txtFILE_NAME = new System.Windows.Forms.TextBox();
            this.txtNAME = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtTYPE_ID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpgParas = new System.Windows.Forms.TabPage();
            this.lsvParas = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.DESCR = new System.Windows.Forms.ColumnHeader();
            this.PARA_TYPE = new System.Windows.Forms.ColumnHeader();
            this.DEPEND_ID = new System.Windows.Forms.ColumnHeader();
            this.ORDER_ID = new System.Windows.Forms.ColumnHeader();
            this.OTHER_LANGUAGE_DESCR = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtParaOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.cbbPARA_TYPE = new System.Windows.Forms.ComboBox();
            this.cbbParaDEPEND_ID = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtParaORDER_ID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtParaDESCR = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtParaID = new System.Windows.Forms.TextBox();
            this.tpgTables = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFilters = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.txtTABLE_FILTER_WHERE = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTableID = new System.Windows.Forms.TextBox();
            this.cbbTABLE_TYPE = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTableORDER_ID = new System.Windows.Forms.TextBox();
            this.cbbTABLE_NAME = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtTABLE_ORDERS = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtTABLE_PAGE_ROWS = new System.Windows.Forms.TextBox();
            this.lsvTables = new System.Windows.Forms.ListView();
            this.TALBE_ID = new System.Windows.Forms.ColumnHeader();
            this.TABLE_NAME = new System.Windows.Forms.ColumnHeader();
            this.TABLE_TYPE = new System.Windows.Forms.ColumnHeader();
            this.ORDERS = new System.Windows.Forms.ColumnHeader();
            this.FILTER_WHERE = new System.Windows.Forms.ColumnHeader();
            this.PAGE_ROWS = new System.Windows.Forms.ColumnHeader();
            this.TABLE_ORDER_ID = new System.Windows.Forms.ColumnHeader();
            this.tpgColumns = new System.Windows.Forms.TabPage();
            this.trvTable = new System.Windows.Forms.TreeView();
            this.lsvColumns = new System.Windows.Forms.ListView();
            this.COLUMN_ID = new System.Windows.Forms.ColumnHeader();
            this.COLUMN_NAME = new System.Windows.Forms.ColumnHeader();
            this.COLUMN_DESCR = new System.Windows.Forms.ColumnHeader();
            this.cOTHER_LANGUAGE_DESCR = new System.Windows.Forms.ColumnHeader();
            this.DISPLAY_PATTERN = new System.Windows.Forms.ColumnHeader();
            this.WORDS = new System.Windows.Forms.ColumnHeader();
            this.COLUMN_ORDER_ID = new System.Windows.Forms.ColumnHeader();
            this.COLUMN_TABLE_NAME = new System.Windows.Forms.ColumnHeader();
            this.R = new System.Windows.Forms.ColumnHeader();
            this.C = new System.Windows.Forms.ColumnHeader();
            this.P = new System.Windows.Forms.ColumnHeader();
            this.COLUMN_TYPE = new System.Windows.Forms.ColumnHeader();
            this.TABLE_ID = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTABLE_ID = new System.Windows.Forms.TextBox();
            this.cbbCOLUMN_TYPE = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtP = new System.Windows.Forms.TextBox();
            this.txtC = new System.Windows.Forms.TextBox();
            this.txtR = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtColumnID = new System.Windows.Forms.TextBox();
            this.cbbColumnCOLUMN_NAME = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtColumnORDER_ID = new System.Windows.Forms.TextBox();
            this.cbbDISPLAY_PATTERN = new System.Windows.Forms.ComboBox();
            this.cbbColumnTABLE_NAME = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtColumnCOLUMN_DESCR = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtColumnWORDS = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label32 = new System.Windows.Forms.Label();
            this.txtColumnOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.tsTool.SuspendLayout();
            this.tabReport.SuspendLayout();
            this.tpgReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cell)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReportFile)).BeginInit();
            this.tpgParas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpgTables.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tpgColumns.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // trvReport
            // 
            resources.ApplyResources(this.trvReport, "trvReport");
            this.trvReport.FullRowSelect = true;
            this.trvReport.ImageList = this.imageList1;
            this.trvReport.LineColor = System.Drawing.Color.Maroon;
            this.trvReport.Name = "trvReport";
            this.trvReport.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvReport_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "childNode.gif");
            this.imageList1.Images.SetKeyName(1, "show_menu.gif");
            this.imageList1.Images.SetKeyName(2, "selectChildNode.gif");
            this.imageList1.Images.SetKeyName(3, "Table.gif");
            this.imageList1.Images.SetKeyName(4, "Columns.gif");
            this.imageList1.Images.SetKeyName(5, "selectAll.gif");
            this.imageList1.Images.SetKeyName(6, "list_wdgz.gif");
            // 
            // tsTool
            // 
            this.tsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbAdd,
            this.tlbDelete,
            this.tlbSave,
            this.tlbAllColumns,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1,
            this.tlbFindColumnType});
            resources.ApplyResources(this.tsTool, "tsTool");
            this.tsTool.Name = "tsTool";
            // 
            // tlbAdd
            // 
            this.tlbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbAdd, "tlbAdd");
            this.tlbAdd.Name = "tlbAdd";
            this.tlbAdd.Tag = "新建人员";
            this.tlbAdd.Click += new System.EventHandler(this.tlbAdd_Click);
            // 
            // tlbDelete
            // 
            this.tlbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbDelete, "tlbDelete");
            this.tlbDelete.Name = "tlbDelete";
            this.tlbDelete.Click += new System.EventHandler(this.tlbDelete_Click);
            // 
            // tlbSave
            // 
            this.tlbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbSave, "tlbSave");
            this.tlbSave.Name = "tlbSave";
            this.tlbSave.Click += new System.EventHandler(this.tlbSave_Click);
            // 
            // tlbAllColumns
            // 
            this.tlbAllColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbAllColumns, "tlbAllColumns");
            this.tlbAllColumns.Name = "tlbAllColumns";
            this.tlbAllColumns.Click += new System.EventHandler(this.tlbAllColumns_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiAdd序号,
            this.tmiUpdateColOtherDesc});
            resources.ApplyResources(this.toolStripDropDownButton1, "toolStripDropDownButton1");
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            // 
            // tmiAdd序号
            // 
            this.tmiAdd序号.Name = "tmiAdd序号";
            resources.ApplyResources(this.tmiAdd序号, "tmiAdd序号");
            this.tmiAdd序号.Click += new System.EventHandler(this.tmiAdd序号_Click);
            // 
            // tmiUpdateColOtherDesc
            // 
            this.tmiUpdateColOtherDesc.Name = "tmiUpdateColOtherDesc";
            resources.ApplyResources(this.tmiUpdateColOtherDesc, "tmiUpdateColOtherDesc");
            this.tmiUpdateColOtherDesc.Click += new System.EventHandler(this.tmiUpdateColOtherDesc_Click);
            // 
            // tlbFindColumnType
            // 
            this.tlbFindColumnType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbFindColumnType, "tlbFindColumnType");
            this.tlbFindColumnType.Name = "tlbFindColumnType";
            this.tlbFindColumnType.Click += new System.EventHandler(this.tlbFindColumnType_Click);
            // 
            // tabReport
            // 
            resources.ApplyResources(this.tabReport, "tabReport");
            this.tabReport.Controls.Add(this.tpgReport);
            this.tabReport.Controls.Add(this.tpgParas);
            this.tabReport.Controls.Add(this.tpgTables);
            this.tabReport.Controls.Add(this.tpgColumns);
            this.tabReport.Name = "tabReport";
            this.tabReport.SelectedIndex = 0;
            this.tabReport.SelectedIndexChanged += new System.EventHandler(this.tabReport_SelectedIndexChanged);
            // 
            // tpgReport
            // 
            this.tpgReport.Controls.Add(this.txtOTHER_LANGUAGE_DESCR);
            this.tpgReport.Controls.Add(this.label30);
            this.tpgReport.Controls.Add(this.m_cell);
            this.tpgReport.Controls.Add(this.cbbType);
            this.tpgReport.Controls.Add(this.label25);
            this.tpgReport.Controls.Add(this.picReportFile);
            this.tpgReport.Controls.Add(this.txtORDER_ID);
            this.tpgReport.Controls.Add(this.txtFILE_NAME);
            this.tpgReport.Controls.Add(this.txtNAME);
            this.tpgReport.Controls.Add(this.txtID);
            this.tpgReport.Controls.Add(this.txtTYPE_ID);
            this.tpgReport.Controls.Add(this.label5);
            this.tpgReport.Controls.Add(this.label4);
            this.tpgReport.Controls.Add(this.label3);
            this.tpgReport.Controls.Add(this.label2);
            this.tpgReport.Controls.Add(this.label1);
            resources.ApplyResources(this.tpgReport, "tpgReport");
            this.tpgReport.Name = "tpgReport";
            this.tpgReport.UseVisualStyleBackColor = true;
            // 
            // txtOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.txtOTHER_LANGUAGE_DESCR, "txtOTHER_LANGUAGE_DESCR");
            this.txtOTHER_LANGUAGE_DESCR.Name = "txtOTHER_LANGUAGE_DESCR";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // m_cell
            // 
            resources.ApplyResources(this.m_cell, "m_cell");
            this.m_cell.Name = "m_cell";
            this.m_cell.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("m_cell.OcxState")));
            // 
            // cbbType
            // 
            this.cbbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbType.FormattingEnabled = true;
            this.cbbType.Items.AddRange(new object[] {
            resources.GetString("cbbType.Items"),
            resources.GetString("cbbType.Items1"),
            resources.GetString("cbbType.Items2")});
            resources.ApplyResources(this.cbbType, "cbbType");
            this.cbbType.Name = "cbbType";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // picReportFile
            // 
            resources.ApplyResources(this.picReportFile, "picReportFile");
            this.picReportFile.Name = "picReportFile";
            this.picReportFile.TabStop = false;
            this.picReportFile.Click += new System.EventHandler(this.picReportFile_Click);
            // 
            // txtORDER_ID
            // 
            resources.ApplyResources(this.txtORDER_ID, "txtORDER_ID");
            this.txtORDER_ID.Name = "txtORDER_ID";
            this.txtORDER_ID.Validating += new System.ComponentModel.CancelEventHandler(this.Int_Validating);
            // 
            // txtFILE_NAME
            // 
            resources.ApplyResources(this.txtFILE_NAME, "txtFILE_NAME");
            this.txtFILE_NAME.Name = "txtFILE_NAME";
            // 
            // txtNAME
            // 
            resources.ApplyResources(this.txtNAME, "txtNAME");
            this.txtNAME.Name = "txtNAME";
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            // 
            // txtTYPE_ID
            // 
            resources.ApplyResources(this.txtTYPE_ID, "txtTYPE_ID");
            this.txtTYPE_ID.Name = "txtTYPE_ID";
            this.txtTYPE_ID.ReadOnly = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tpgParas
            // 
            this.tpgParas.Controls.Add(this.lsvParas);
            this.tpgParas.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tpgParas, "tpgParas");
            this.tpgParas.Name = "tpgParas";
            this.tpgParas.UseVisualStyleBackColor = true;
            // 
            // lsvParas
            // 
            resources.ApplyResources(this.lsvParas, "lsvParas");
            this.lsvParas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.DESCR,
            this.PARA_TYPE,
            this.DEPEND_ID,
            this.ORDER_ID,
            this.OTHER_LANGUAGE_DESCR});
            this.lsvParas.FullRowSelect = true;
            this.lsvParas.Name = "lsvParas";
            this.lsvParas.UseCompatibleStateImageBehavior = false;
            this.lsvParas.View = System.Windows.Forms.View.Details;
            this.lsvParas.SelectedIndexChanged += new System.EventHandler(this.lsvParas_SelectedIndexChanged);
            // 
            // ID
            // 
            resources.ApplyResources(this.ID, "ID");
            // 
            // DESCR
            // 
            resources.ApplyResources(this.DESCR, "DESCR");
            // 
            // PARA_TYPE
            // 
            resources.ApplyResources(this.PARA_TYPE, "PARA_TYPE");
            // 
            // DEPEND_ID
            // 
            resources.ApplyResources(this.DEPEND_ID, "DEPEND_ID");
            // 
            // ORDER_ID
            // 
            resources.ApplyResources(this.ORDER_ID, "ORDER_ID");
            // 
            // OTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.OTHER_LANGUAGE_DESCR, "OTHER_LANGUAGE_DESCR");
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtParaOTHER_LANGUAGE_DESCR);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.cbbPARA_TYPE);
            this.groupBox1.Controls.Add(this.cbbParaDEPEND_ID);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtParaORDER_ID);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtParaDESCR);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtParaID);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtParaOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.txtParaOTHER_LANGUAGE_DESCR, "txtParaOTHER_LANGUAGE_DESCR");
            this.txtParaOTHER_LANGUAGE_DESCR.Name = "txtParaOTHER_LANGUAGE_DESCR";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // cbbPARA_TYPE
            // 
            this.cbbPARA_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPARA_TYPE.FormattingEnabled = true;
            this.cbbPARA_TYPE.Items.AddRange(new object[] {
            resources.GetString("cbbPARA_TYPE.Items"),
            resources.GetString("cbbPARA_TYPE.Items1"),
            resources.GetString("cbbPARA_TYPE.Items2"),
            resources.GetString("cbbPARA_TYPE.Items3"),
            resources.GetString("cbbPARA_TYPE.Items4")});
            resources.ApplyResources(this.cbbPARA_TYPE, "cbbPARA_TYPE");
            this.cbbPARA_TYPE.Name = "cbbPARA_TYPE";
            // 
            // cbbParaDEPEND_ID
            // 
            this.cbbParaDEPEND_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbParaDEPEND_ID.FormattingEnabled = true;
            resources.ApplyResources(this.cbbParaDEPEND_ID, "cbbParaDEPEND_ID");
            this.cbbParaDEPEND_ID.Name = "cbbParaDEPEND_ID";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // txtParaORDER_ID
            // 
            resources.ApplyResources(this.txtParaORDER_ID, "txtParaORDER_ID");
            this.txtParaORDER_ID.Name = "txtParaORDER_ID";
            this.txtParaORDER_ID.Validating += new System.ComponentModel.CancelEventHandler(this.Int_Validating);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtParaDESCR
            // 
            resources.ApplyResources(this.txtParaDESCR, "txtParaDESCR");
            this.txtParaDESCR.Name = "txtParaDESCR";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtParaID
            // 
            resources.ApplyResources(this.txtParaID, "txtParaID");
            this.txtParaID.Name = "txtParaID";
            this.txtParaID.ReadOnly = true;
            // 
            // tpgTables
            // 
            this.tpgTables.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tpgTables.Controls.Add(this.groupBox3);
            this.tpgTables.Controls.Add(this.lsvTables);
            resources.ApplyResources(this.tpgTables, "tpgTables");
            this.tpgTables.Name = "tpgTables";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.btnFilters);
            this.groupBox3.Controls.Add(this.btnOrders);
            this.groupBox3.Controls.Add(this.txtTABLE_FILTER_WHERE);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtTableID);
            this.groupBox3.Controls.Add(this.cbbTABLE_TYPE);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.txtTableORDER_ID);
            this.groupBox3.Controls.Add(this.cbbTABLE_NAME);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.txtTABLE_ORDERS);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.txtTABLE_PAGE_ROWS);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // btnFilters
            // 
            resources.ApplyResources(this.btnFilters, "btnFilters");
            this.btnFilters.ImageList = this.imageList1;
            this.btnFilters.Name = "btnFilters";
            this.btnFilters.UseVisualStyleBackColor = true;
            this.btnFilters.Click += new System.EventHandler(this.btnFilters_Click);
            // 
            // btnOrders
            // 
            resources.ApplyResources(this.btnOrders, "btnOrders");
            this.btnOrders.ImageList = this.imageList1;
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // txtTABLE_FILTER_WHERE
            // 
            resources.ApplyResources(this.txtTABLE_FILTER_WHERE, "txtTABLE_FILTER_WHERE");
            this.txtTABLE_FILTER_WHERE.Name = "txtTABLE_FILTER_WHERE";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtTableID
            // 
            resources.ApplyResources(this.txtTableID, "txtTableID");
            this.txtTableID.Name = "txtTableID";
            this.txtTableID.ReadOnly = true;
            // 
            // cbbTABLE_TYPE
            // 
            this.cbbTABLE_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTABLE_TYPE.FormattingEnabled = true;
            this.cbbTABLE_TYPE.Items.AddRange(new object[] {
            resources.GetString("cbbTABLE_TYPE.Items"),
            resources.GetString("cbbTABLE_TYPE.Items1")});
            resources.ApplyResources(this.cbbTABLE_TYPE, "cbbTABLE_TYPE");
            this.cbbTABLE_TYPE.Name = "cbbTABLE_TYPE";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // txtTableORDER_ID
            // 
            resources.ApplyResources(this.txtTableORDER_ID, "txtTableORDER_ID");
            this.txtTableORDER_ID.Name = "txtTableORDER_ID";
            this.txtTableORDER_ID.Validating += new System.ComponentModel.CancelEventHandler(this.Int_Validating);
            // 
            // cbbTABLE_NAME
            // 
            this.cbbTABLE_NAME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTABLE_NAME.FormattingEnabled = true;
            resources.ApplyResources(this.cbbTABLE_NAME, "cbbTABLE_NAME");
            this.cbbTABLE_NAME.Name = "cbbTABLE_NAME";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // txtTABLE_ORDERS
            // 
            resources.ApplyResources(this.txtTABLE_ORDERS, "txtTABLE_ORDERS");
            this.txtTABLE_ORDERS.Name = "txtTABLE_ORDERS";
            this.txtTABLE_ORDERS.ReadOnly = true;
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // txtTABLE_PAGE_ROWS
            // 
            resources.ApplyResources(this.txtTABLE_PAGE_ROWS, "txtTABLE_PAGE_ROWS");
            this.txtTABLE_PAGE_ROWS.Name = "txtTABLE_PAGE_ROWS";
            this.txtTABLE_PAGE_ROWS.Validating += new System.ComponentModel.CancelEventHandler(this.Int_Validating);
            // 
            // lsvTables
            // 
            resources.ApplyResources(this.lsvTables, "lsvTables");
            this.lsvTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TALBE_ID,
            this.TABLE_NAME,
            this.TABLE_TYPE,
            this.ORDERS,
            this.FILTER_WHERE,
            this.PAGE_ROWS,
            this.TABLE_ORDER_ID});
            this.lsvTables.FullRowSelect = true;
            this.lsvTables.MultiSelect = false;
            this.lsvTables.Name = "lsvTables";
            this.lsvTables.UseCompatibleStateImageBehavior = false;
            this.lsvTables.View = System.Windows.Forms.View.Details;
            this.lsvTables.SelectedIndexChanged += new System.EventHandler(this.lsvTables_SelectedIndexChanged);
            // 
            // TALBE_ID
            // 
            resources.ApplyResources(this.TALBE_ID, "TALBE_ID");
            // 
            // TABLE_NAME
            // 
            resources.ApplyResources(this.TABLE_NAME, "TABLE_NAME");
            // 
            // TABLE_TYPE
            // 
            resources.ApplyResources(this.TABLE_TYPE, "TABLE_TYPE");
            // 
            // ORDERS
            // 
            resources.ApplyResources(this.ORDERS, "ORDERS");
            // 
            // FILTER_WHERE
            // 
            resources.ApplyResources(this.FILTER_WHERE, "FILTER_WHERE");
            // 
            // PAGE_ROWS
            // 
            resources.ApplyResources(this.PAGE_ROWS, "PAGE_ROWS");
            // 
            // TABLE_ORDER_ID
            // 
            resources.ApplyResources(this.TABLE_ORDER_ID, "TABLE_ORDER_ID");
            // 
            // tpgColumns
            // 
            this.tpgColumns.Controls.Add(this.trvTable);
            this.tpgColumns.Controls.Add(this.lsvColumns);
            this.tpgColumns.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.tpgColumns, "tpgColumns");
            this.tpgColumns.Name = "tpgColumns";
            this.tpgColumns.UseVisualStyleBackColor = true;
            // 
            // trvTable
            // 
            resources.ApplyResources(this.trvTable, "trvTable");
            this.trvTable.ImageList = this.imageList1;
            this.trvTable.Name = "trvTable";
            this.trvTable.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTable_AfterSelect);
            // 
            // lsvColumns
            // 
            resources.ApplyResources(this.lsvColumns, "lsvColumns");
            this.lsvColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.COLUMN_ID,
            this.COLUMN_NAME,
            this.COLUMN_DESCR,
            this.cOTHER_LANGUAGE_DESCR,
            this.DISPLAY_PATTERN,
            this.WORDS,
            this.COLUMN_ORDER_ID,
            this.COLUMN_TABLE_NAME,
            this.R,
            this.C,
            this.P,
            this.COLUMN_TYPE,
            this.TABLE_ID});
            this.lsvColumns.FullRowSelect = true;
            this.lsvColumns.MultiSelect = false;
            this.lsvColumns.Name = "lsvColumns";
            this.lsvColumns.UseCompatibleStateImageBehavior = false;
            this.lsvColumns.View = System.Windows.Forms.View.Details;
            this.lsvColumns.SelectedIndexChanged += new System.EventHandler(this.lsvColumns_SelectedIndexChanged);
            // 
            // COLUMN_ID
            // 
            resources.ApplyResources(this.COLUMN_ID, "COLUMN_ID");
            // 
            // COLUMN_NAME
            // 
            resources.ApplyResources(this.COLUMN_NAME, "COLUMN_NAME");
            // 
            // COLUMN_DESCR
            // 
            resources.ApplyResources(this.COLUMN_DESCR, "COLUMN_DESCR");
            // 
            // cOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.cOTHER_LANGUAGE_DESCR, "cOTHER_LANGUAGE_DESCR");
            // 
            // DISPLAY_PATTERN
            // 
            resources.ApplyResources(this.DISPLAY_PATTERN, "DISPLAY_PATTERN");
            // 
            // WORDS
            // 
            resources.ApplyResources(this.WORDS, "WORDS");
            // 
            // COLUMN_ORDER_ID
            // 
            resources.ApplyResources(this.COLUMN_ORDER_ID, "COLUMN_ORDER_ID");
            // 
            // COLUMN_TABLE_NAME
            // 
            resources.ApplyResources(this.COLUMN_TABLE_NAME, "COLUMN_TABLE_NAME");
            // 
            // R
            // 
            resources.ApplyResources(this.R, "R");
            // 
            // C
            // 
            resources.ApplyResources(this.C, "C");
            // 
            // P
            // 
            resources.ApplyResources(this.P, "P");
            // 
            // COLUMN_TYPE
            // 
            resources.ApplyResources(this.COLUMN_TYPE, "COLUMN_TYPE");
            // 
            // TABLE_ID
            // 
            resources.ApplyResources(this.TABLE_ID, "TABLE_ID");
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.txtColumnOTHER_LANGUAGE_DESCR);
            this.groupBox2.Controls.Add(this.label32);
            this.groupBox2.Controls.Add(this.txtTABLE_ID);
            this.groupBox2.Controls.Add(this.cbbCOLUMN_TYPE);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.txtP);
            this.groupBox2.Controls.Add(this.txtC);
            this.groupBox2.Controls.Add(this.txtR);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.txtColumnID);
            this.groupBox2.Controls.Add(this.cbbColumnCOLUMN_NAME);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtColumnORDER_ID);
            this.groupBox2.Controls.Add(this.cbbDISPLAY_PATTERN);
            this.groupBox2.Controls.Add(this.cbbColumnTABLE_NAME);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtColumnCOLUMN_DESCR);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtColumnWORDS);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtTABLE_ID
            // 
            resources.ApplyResources(this.txtTABLE_ID, "txtTABLE_ID");
            this.txtTABLE_ID.Name = "txtTABLE_ID";
            // 
            // cbbCOLUMN_TYPE
            // 
            this.cbbCOLUMN_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCOLUMN_TYPE.FormattingEnabled = true;
            this.cbbCOLUMN_TYPE.Items.AddRange(new object[] {
            resources.GetString("cbbCOLUMN_TYPE.Items"),
            resources.GetString("cbbCOLUMN_TYPE.Items1"),
            resources.GetString("cbbCOLUMN_TYPE.Items2"),
            resources.GetString("cbbCOLUMN_TYPE.Items3"),
            resources.GetString("cbbCOLUMN_TYPE.Items4")});
            resources.ApplyResources(this.cbbCOLUMN_TYPE, "cbbCOLUMN_TYPE");
            this.cbbCOLUMN_TYPE.Name = "cbbCOLUMN_TYPE";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // txtP
            // 
            resources.ApplyResources(this.txtP, "txtP");
            this.txtP.Name = "txtP";
            // 
            // txtC
            // 
            resources.ApplyResources(this.txtC, "txtC");
            this.txtC.Name = "txtC";
            // 
            // txtR
            // 
            resources.ApplyResources(this.txtR, "txtR");
            this.txtR.Name = "txtR";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // txtColumnID
            // 
            resources.ApplyResources(this.txtColumnID, "txtColumnID");
            this.txtColumnID.Name = "txtColumnID";
            this.txtColumnID.ReadOnly = true;
            // 
            // cbbColumnCOLUMN_NAME
            // 
            this.cbbColumnCOLUMN_NAME.FormattingEnabled = true;
            resources.ApplyResources(this.cbbColumnCOLUMN_NAME, "cbbColumnCOLUMN_NAME");
            this.cbbColumnCOLUMN_NAME.Name = "cbbColumnCOLUMN_NAME";
            this.cbbColumnCOLUMN_NAME.SelectedIndexChanged += new System.EventHandler(this.cbbColumnCOLUMN_NAME_SelectedIndexChanged);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // txtColumnORDER_ID
            // 
            resources.ApplyResources(this.txtColumnORDER_ID, "txtColumnORDER_ID");
            this.txtColumnORDER_ID.Name = "txtColumnORDER_ID";
            this.txtColumnORDER_ID.Validating += new System.ComponentModel.CancelEventHandler(this.Int_Validating);
            // 
            // cbbDISPLAY_PATTERN
            // 
            this.cbbDISPLAY_PATTERN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDISPLAY_PATTERN.FormattingEnabled = true;
            this.cbbDISPLAY_PATTERN.Items.AddRange(new object[] {
            resources.GetString("cbbDISPLAY_PATTERN.Items"),
            resources.GetString("cbbDISPLAY_PATTERN.Items1"),
            resources.GetString("cbbDISPLAY_PATTERN.Items2"),
            resources.GetString("cbbDISPLAY_PATTERN.Items3"),
            resources.GetString("cbbDISPLAY_PATTERN.Items4")});
            resources.ApplyResources(this.cbbDISPLAY_PATTERN, "cbbDISPLAY_PATTERN");
            this.cbbDISPLAY_PATTERN.Name = "cbbDISPLAY_PATTERN";
            this.cbbDISPLAY_PATTERN.SelectedIndexChanged += new System.EventHandler(this.cbbDISPLAY_PATTERN_SelectedIndexChanged);
            // 
            // cbbColumnTABLE_NAME
            // 
            this.cbbColumnTABLE_NAME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbbColumnTABLE_NAME, "cbbColumnTABLE_NAME");
            this.cbbColumnTABLE_NAME.FormattingEnabled = true;
            this.cbbColumnTABLE_NAME.Name = "cbbColumnTABLE_NAME";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtColumnCOLUMN_DESCR
            // 
            resources.ApplyResources(this.txtColumnCOLUMN_DESCR, "txtColumnCOLUMN_DESCR");
            this.txtColumnCOLUMN_DESCR.Name = "txtColumnCOLUMN_DESCR";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // txtColumnWORDS
            // 
            resources.ApplyResources(this.txtColumnWORDS, "txtColumnWORDS");
            this.txtColumnWORDS.Name = "txtColumnWORDS";
            this.txtColumnWORDS.ReadOnly = true;
            this.txtColumnWORDS.DoubleClick += new System.EventHandler(this.txtColumnWORDS_DoubleClick);
            this.txtColumnWORDS.Validating += new System.ComponentModel.CancelEventHandler(this.Int_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // txtColumnOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.txtColumnOTHER_LANGUAGE_DESCR, "txtColumnOTHER_LANGUAGE_DESCR");
            this.txtColumnOTHER_LANGUAGE_DESCR.Name = "txtColumnOTHER_LANGUAGE_DESCR";
            // 
            // frmReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabReport);
            this.Controls.Add(this.tsTool);
            this.Controls.Add(this.trvReport);
            this.Name = "frmReport";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            this.tabReport.ResumeLayout(false);
            this.tpgReport.ResumeLayout(false);
            this.tpgReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cell)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReportFile)).EndInit();
            this.tpgParas.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpgTables.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tpgColumns.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvReport;
        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tlbAdd;
        private System.Windows.Forms.ToolStripButton tlbDelete;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabReport;
        private System.Windows.Forms.TabPage tpgReport;
        private System.Windows.Forms.TabPage tpgParas;
        private System.Windows.Forms.TabPage tpgColumns;
        private System.Windows.Forms.TextBox txtORDER_ID;
        private System.Windows.Forms.TextBox txtFILE_NAME;
        private System.Windows.Forms.TextBox txtNAME;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtTYPE_ID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picReportFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtParaDESCR;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtParaID;
        private System.Windows.Forms.ComboBox cbbParaDEPEND_ID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtParaORDER_ID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbbColumnTABLE_NAME;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtColumnCOLUMN_DESCR;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtColumnWORDS;
        private System.Windows.Forms.ListView lsvParas;
        private System.Windows.Forms.ListView lsvColumns;
        private System.Windows.Forms.ComboBox cbbDISPLAY_PATTERN;
        private System.Windows.Forms.ComboBox cbbPARA_TYPE;
        private System.Windows.Forms.ComboBox cbbColumnCOLUMN_NAME;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtColumnORDER_ID;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader DESCR;
        private System.Windows.Forms.ColumnHeader PARA_TYPE;
        private System.Windows.Forms.ColumnHeader DEPEND_ID;
        private System.Windows.Forms.ColumnHeader ORDER_ID;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtColumnID;
        private System.Windows.Forms.ColumnHeader COLUMN_ID;
        private System.Windows.Forms.ColumnHeader COLUMN_TABLE_NAME;
        private System.Windows.Forms.ColumnHeader COLUMN_NAME;
        private System.Windows.Forms.ColumnHeader COLUMN_DESCR;
        private System.Windows.Forms.ColumnHeader DISPLAY_PATTERN;
        private System.Windows.Forms.ColumnHeader WORDS;
        private System.Windows.Forms.ColumnHeader COLUMN_ORDER_ID;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tpgTables;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTableID;
        private System.Windows.Forms.ComboBox cbbTABLE_TYPE;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTableORDER_ID;
        private System.Windows.Forms.ComboBox cbbTABLE_NAME;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtTABLE_ORDERS;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtTABLE_PAGE_ROWS;
        private System.Windows.Forms.ListView lsvTables;
        private System.Windows.Forms.ColumnHeader TALBE_ID;
        private System.Windows.Forms.ColumnHeader TABLE_NAME;
        private System.Windows.Forms.ColumnHeader TABLE_TYPE;
        private System.Windows.Forms.ColumnHeader ORDERS;
        private System.Windows.Forms.ColumnHeader FILTER_WHERE;
        private System.Windows.Forms.ColumnHeader PAGE_ROWS;
        private System.Windows.Forms.ColumnHeader TABLE_ORDER_ID;
        private System.Windows.Forms.TreeView trvTable;
        private System.Windows.Forms.TextBox txtTABLE_FILTER_WHERE;
        private System.Windows.Forms.Button btnFilters;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.ToolStripButton tlbAllColumns;
        private System.Windows.Forms.ComboBox cbbType;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtP;
        private System.Windows.Forms.TextBox txtC;
        private System.Windows.Forms.TextBox txtR;
        private System.Windows.Forms.ColumnHeader R;
        private System.Windows.Forms.ColumnHeader C;
        private System.Windows.Forms.ColumnHeader P;
        private System.Windows.Forms.ComboBox cbbCOLUMN_TYPE;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ColumnHeader COLUMN_TYPE;
        private System.Windows.Forms.ToolStripButton tlbFindColumnType;
        private System.Windows.Forms.TextBox txtTABLE_ID;
        private System.Windows.Forms.ColumnHeader TABLE_ID;
        private AxCELL50Lib.AxCell m_cell;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem tmiAdd序号;
        private System.Windows.Forms.TextBox txtOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtParaOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ColumnHeader OTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.ColumnHeader cOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.ToolStripMenuItem tmiUpdateColOtherDesc;
        private System.Windows.Forms.TextBox txtColumnOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.Label label32;
    }
}
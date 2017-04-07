namespace PlatForm
{
    partial class frmTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTable));
            this.tvTableType = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbAdd = new System.Windows.Forms.ToolStripButton();
            this.tlbDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.lsvTable = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.OWNER = new System.Windows.Forms.ColumnHeader();
            this.NAME = new System.Windows.Forms.ColumnHeader();
            this.DESCR = new System.Windows.Forms.ColumnHeader();
            this.PAGE_ROWS = new System.Windows.Forms.ColumnHeader();
            this.ORDER_ID = new System.Windows.Forms.ColumnHeader();
            this.DISPLAY_STYLE = new System.Windows.Forms.ColumnHeader();
            this.OTHER_LANGUAGE_DESCR = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.cbbQUERY_COL = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbDISPLAY_STYLE = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtORDER_ID = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPAGE_ROWS = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDESCR = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNAME = new System.Windows.Forms.TextBox();
            this.txtOWNER = new System.Windows.Forms.TextBox();
            this.txtTYPE_ID = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tsTool.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tvTableType
            // 
            resources.ApplyResources(this.tvTableType, "tvTableType");
            this.tvTableType.FullRowSelect = true;
            this.tvTableType.ImageList = this.imageList1;
            this.tvTableType.LineColor = System.Drawing.Color.Maroon;
            this.tvTableType.Name = "tvTableType";
            this.tvTableType.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTableType_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "childNode.gif");
            this.imageList1.Images.SetKeyName(1, "show_menu.gif");
            this.imageList1.Images.SetKeyName(2, "selectChildNode.gif");
            this.imageList1.Images.SetKeyName(3, "img_pre.gif");
            this.imageList1.Images.SetKeyName(4, "img_first.gif");
            this.imageList1.Images.SetKeyName(5, "img_next.gif");
            this.imageList1.Images.SetKeyName(6, "img_end.gif");
            // 
            // tsTool
            // 
            this.tsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbAdd,
            this.tlbDelete,
            this.tlbSave,
            this.toolStripSeparator1,
            this.tlbRefresh});
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tlbRefresh
            // 
            this.tlbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbRefresh, "tlbRefresh");
            this.tlbRefresh.Name = "tlbRefresh";
            this.tlbRefresh.Click += new System.EventHandler(this.tlbRefresh_Click);
            // 
            // lsvTable
            // 
            resources.ApplyResources(this.lsvTable, "lsvTable");
            this.lsvTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.OWNER,
            this.NAME,
            this.DESCR,
            this.PAGE_ROWS,
            this.ORDER_ID,
            this.DISPLAY_STYLE,
            this.OTHER_LANGUAGE_DESCR});
            this.lsvTable.FullRowSelect = true;
            this.lsvTable.MultiSelect = false;
            this.lsvTable.Name = "lsvTable";
            this.lsvTable.UseCompatibleStateImageBehavior = false;
            this.lsvTable.View = System.Windows.Forms.View.Details;
            this.lsvTable.SelectedIndexChanged += new System.EventHandler(this.lvTable_SelectedIndexChanged);
            // 
            // ID
            // 
            resources.ApplyResources(this.ID, "ID");
            // 
            // OWNER
            // 
            resources.ApplyResources(this.OWNER, "OWNER");
            // 
            // NAME
            // 
            resources.ApplyResources(this.NAME, "NAME");
            // 
            // DESCR
            // 
            resources.ApplyResources(this.DESCR, "DESCR");
            // 
            // PAGE_ROWS
            // 
            resources.ApplyResources(this.PAGE_ROWS, "PAGE_ROWS");
            // 
            // ORDER_ID
            // 
            resources.ApplyResources(this.ORDER_ID, "ORDER_ID");
            // 
            // DISPLAY_STYLE
            // 
            resources.ApplyResources(this.DISPLAY_STYLE, "DISPLAY_STYLE");
            // 
            // OTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.OTHER_LANGUAGE_DESCR, "OTHER_LANGUAGE_DESCR");
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtOTHER_LANGUAGE_DESCR);
            this.groupBox1.Controls.Add(this.cbbQUERY_COL);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbbDISPLAY_STYLE);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtORDER_ID);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtPAGE_ROWS);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtDESCR);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtNAME);
            this.groupBox1.Controls.Add(this.txtOWNER);
            this.groupBox1.Controls.Add(this.txtTYPE_ID);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.txtOTHER_LANGUAGE_DESCR, "txtOTHER_LANGUAGE_DESCR");
            this.txtOTHER_LANGUAGE_DESCR.Name = "txtOTHER_LANGUAGE_DESCR";
            // 
            // cbbQUERY_COL
            // 
            this.cbbQUERY_COL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbQUERY_COL.FormattingEnabled = true;
            this.cbbQUERY_COL.Items.AddRange(new object[] {
            resources.GetString("cbbQUERY_COL.Items"),
            resources.GetString("cbbQUERY_COL.Items1"),
            resources.GetString("cbbQUERY_COL.Items2")});
            resources.ApplyResources(this.cbbQUERY_COL, "cbbQUERY_COL");
            this.cbbQUERY_COL.Name = "cbbQUERY_COL";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // cbbDISPLAY_STYLE
            // 
            this.cbbDISPLAY_STYLE.DisplayMember = "Display";
            this.cbbDISPLAY_STYLE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDISPLAY_STYLE.FormattingEnabled = true;
            resources.ApplyResources(this.cbbDISPLAY_STYLE, "cbbDISPLAY_STYLE");
            this.cbbDISPLAY_STYLE.Name = "cbbDISPLAY_STYLE";
            this.cbbDISPLAY_STYLE.ValueMember = "Value";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtORDER_ID
            // 
            resources.ApplyResources(this.txtORDER_ID, "txtORDER_ID");
            this.txtORDER_ID.Name = "txtORDER_ID";
            this.txtORDER_ID.Validating += new System.ComponentModel.CancelEventHandler(this.txtORDER_ID_Validating);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtPAGE_ROWS
            // 
            resources.ApplyResources(this.txtPAGE_ROWS, "txtPAGE_ROWS");
            this.txtPAGE_ROWS.Name = "txtPAGE_ROWS";
            this.txtPAGE_ROWS.Validating += new System.ComponentModel.CancelEventHandler(this.txtPAGE_ROWS_Validating);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtDESCR
            // 
            resources.ApplyResources(this.txtDESCR, "txtDESCR");
            this.txtDESCR.Name = "txtDESCR";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtNAME
            // 
            resources.ApplyResources(this.txtNAME, "txtNAME");
            this.txtNAME.Name = "txtNAME";
            this.txtNAME.ReadOnly = true;
            // 
            // txtOWNER
            // 
            resources.ApplyResources(this.txtOWNER, "txtOWNER");
            this.txtOWNER.Name = "txtOWNER";
            this.txtOWNER.ReadOnly = true;
            // 
            // txtTYPE_ID
            // 
            resources.ApplyResources(this.txtTYPE_ID, "txtTYPE_ID");
            this.txtTYPE_ID.Name = "txtTYPE_ID";
            this.txtTYPE_ID.ReadOnly = true;
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmTable
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lsvTable);
            this.Controls.Add(this.tsTool);
            this.Controls.Add(this.tvTableType);
            this.KeyPreview = true;
            this.Name = "frmTable";
            this.ShowInTaskbar = false;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmTable_KeyUp);
            this.Load += new System.EventHandler(this.frmTable_Load);
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvTableType;
        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tlbAdd;
        private System.Windows.Forms.ToolStripButton tlbDelete;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.ListView lsvTable;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtORDER_ID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPAGE_ROWS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDESCR;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNAME;
        private System.Windows.Forms.TextBox txtOWNER;
        private System.Windows.Forms.TextBox txtTYPE_ID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader OWNER;
        private System.Windows.Forms.ColumnHeader NAME;
        private System.Windows.Forms.ColumnHeader DESCR;
        private System.Windows.Forms.ColumnHeader PAGE_ROWS;
        private System.Windows.Forms.ColumnHeader ORDER_ID;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox cbbDISPLAY_STYLE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader DISPLAY_STYLE;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.ComboBox cbbQUERY_COL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.ColumnHeader OTHER_LANGUAGE_DESCR;
    }
}
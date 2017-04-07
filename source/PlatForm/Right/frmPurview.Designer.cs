namespace PlatForm
{
    partial class frmPurview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurview));
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbAdd = new System.Windows.Forms.ToolStripButton();
            this.tlbDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbQuickAddRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbQuickAddFileRight = new System.Windows.Forms.ToolStripButton();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.lsvPurview = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.DESCR = new System.Windows.Forms.ColumnHeader();
            this.WEB_FILE = new System.Windows.Forms.ColumnHeader();
            this.CONTROL_NAME = new System.Windows.Forms.ColumnHeader();
            this.CONTROL_PROPERTY = new System.Windows.Forms.ColumnHeader();
            this.CONTROL_VALUE = new System.Windows.Forms.ColumnHeader();
            this.ORDER_ID = new System.Windows.Forms.ColumnHeader();
            this.OTHER_LANGUAGE_DESCR = new System.Windows.Forms.ColumnHeader();
            this.cobCONTROL_PROPERTY = new System.Windows.Forms.ComboBox();
            this.txtWEB_FILE = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtMODULE_NAME = new System.Windows.Forms.TextBox();
            this.txtMODULE_ID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDESCR = new System.Windows.Forms.TextBox();
            this.cobCONTROL_VALUE = new System.Windows.Forms.ComboBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.trvTreeMenu = new System.Windows.Forms.TreeView();
            this.txtCONTROL_NAME = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtORDER_ID = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tsTool.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tsTool
            // 
            this.tsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbAdd,
            this.tlbDelete,
            this.tlbSave,
            this.tsbQuickAddRight,
            this.toolStripSeparator1,
            this.tsbQuickAddFileRight,
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
            // tsbQuickAddRight
            // 
            this.tsbQuickAddRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbQuickAddRight, "tsbQuickAddRight");
            this.tsbQuickAddRight.Name = "tsbQuickAddRight";
            this.tsbQuickAddRight.Click += new System.EventHandler(this.tsbQuickAddRight_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsbQuickAddFileRight
            // 
            this.tsbQuickAddFileRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbQuickAddFileRight, "tsbQuickAddFileRight");
            this.tsbQuickAddFileRight.Name = "tsbQuickAddFileRight";
            this.tsbQuickAddFileRight.Click += new System.EventHandler(this.tsbQuickAddFileRight_Click);
            // 
            // tlbRefresh
            // 
            this.tlbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbRefresh, "tlbRefresh");
            this.tlbRefresh.Name = "tlbRefresh";
            this.tlbRefresh.Click += new System.EventHandler(this.tlbRefresh_Click);
            // 
            // lsvPurview
            // 
            resources.ApplyResources(this.lsvPurview, "lsvPurview");
            this.lsvPurview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.DESCR,
            this.WEB_FILE,
            this.CONTROL_NAME,
            this.CONTROL_PROPERTY,
            this.CONTROL_VALUE,
            this.ORDER_ID,
            this.OTHER_LANGUAGE_DESCR});
            this.lsvPurview.FullRowSelect = true;
            this.lsvPurview.MultiSelect = false;
            this.lsvPurview.Name = "lsvPurview";
            this.lsvPurview.UseCompatibleStateImageBehavior = false;
            this.lsvPurview.View = System.Windows.Forms.View.Details;
            this.lsvPurview.SelectedIndexChanged += new System.EventHandler(this.lsvPurview_SelectedIndexChanged);
            // 
            // ID
            // 
            resources.ApplyResources(this.ID, "ID");
            // 
            // DESCR
            // 
            resources.ApplyResources(this.DESCR, "DESCR");
            // 
            // WEB_FILE
            // 
            resources.ApplyResources(this.WEB_FILE, "WEB_FILE");
            // 
            // CONTROL_NAME
            // 
            resources.ApplyResources(this.CONTROL_NAME, "CONTROL_NAME");
            // 
            // CONTROL_PROPERTY
            // 
            resources.ApplyResources(this.CONTROL_PROPERTY, "CONTROL_PROPERTY");
            // 
            // CONTROL_VALUE
            // 
            resources.ApplyResources(this.CONTROL_VALUE, "CONTROL_VALUE");
            // 
            // ORDER_ID
            // 
            resources.ApplyResources(this.ORDER_ID, "ORDER_ID");
            // 
            // OTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.OTHER_LANGUAGE_DESCR, "OTHER_LANGUAGE_DESCR");
            // 
            // cobCONTROL_PROPERTY
            // 
            resources.ApplyResources(this.cobCONTROL_PROPERTY, "cobCONTROL_PROPERTY");
            this.cobCONTROL_PROPERTY.FormattingEnabled = true;
            this.cobCONTROL_PROPERTY.Items.AddRange(new object[] {
            resources.GetString("cobCONTROL_PROPERTY.Items"),
            resources.GetString("cobCONTROL_PROPERTY.Items1"),
            resources.GetString("cobCONTROL_PROPERTY.Items2")});
            this.cobCONTROL_PROPERTY.Name = "cobCONTROL_PROPERTY";
            // 
            // txtWEB_FILE
            // 
            resources.ApplyResources(this.txtWEB_FILE, "txtWEB_FILE");
            this.txtWEB_FILE.Name = "txtWEB_FILE";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            // 
            // txtMODULE_NAME
            // 
            resources.ApplyResources(this.txtMODULE_NAME, "txtMODULE_NAME");
            this.txtMODULE_NAME.Name = "txtMODULE_NAME";
            this.txtMODULE_NAME.ReadOnly = true;
            // 
            // txtMODULE_ID
            // 
            resources.ApplyResources(this.txtMODULE_ID, "txtMODULE_ID");
            this.txtMODULE_ID.Name = "txtMODULE_ID";
            this.txtMODULE_ID.ReadOnly = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
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
            // txtDESCR
            // 
            resources.ApplyResources(this.txtDESCR, "txtDESCR");
            this.txtDESCR.Name = "txtDESCR";
            // 
            // cobCONTROL_VALUE
            // 
            resources.ApplyResources(this.cobCONTROL_VALUE, "cobCONTROL_VALUE");
            this.cobCONTROL_VALUE.FormattingEnabled = true;
            this.cobCONTROL_VALUE.Items.AddRange(new object[] {
            resources.GetString("cobCONTROL_VALUE.Items"),
            resources.GetString("cobCONTROL_VALUE.Items1")});
            this.cobCONTROL_VALUE.Name = "cobCONTROL_VALUE";
            // 
            // btnFile
            // 
            resources.ApplyResources(this.btnFile, "btnFile");
            this.btnFile.Name = "btnFile";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "parentNode.gif");
            this.imageList1.Images.SetKeyName(1, "selectChildNode.gif");
            // 
            // trvTreeMenu
            // 
            resources.ApplyResources(this.trvTreeMenu, "trvTreeMenu");
            this.trvTreeMenu.ImageList = this.imageList1;
            this.trvTreeMenu.LineColor = System.Drawing.Color.Maroon;
            this.trvTreeMenu.Name = "trvTreeMenu";
            this.trvTreeMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTreeMenu_AfterSelect);
            // 
            // txtCONTROL_NAME
            // 
            resources.ApplyResources(this.txtCONTROL_NAME, "txtCONTROL_NAME");
            this.txtCONTROL_NAME.Name = "txtCONTROL_NAME";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtOTHER_LANGUAGE_DESCR);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtORDER_ID);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.txtOTHER_LANGUAGE_DESCR, "txtOTHER_LANGUAGE_DESCR");
            this.txtOTHER_LANGUAGE_DESCR.Name = "txtOTHER_LANGUAGE_DESCR";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtORDER_ID
            // 
            resources.ApplyResources(this.txtORDER_ID, "txtORDER_ID");
            this.txtORDER_ID.Name = "txtORDER_ID";
            this.txtORDER_ID.Validating += new System.ComponentModel.CancelEventHandler(this.txtORDER_ID_Validating);
            // 
            // textBox3
            // 
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.Name = "textBox3";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmPurview
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtCONTROL_NAME);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.trvTreeMenu);
            this.Controls.Add(this.cobCONTROL_PROPERTY);
            this.Controls.Add(this.cobCONTROL_VALUE);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtMODULE_NAME);
            this.Controls.Add(this.txtWEB_FILE);
            this.Controls.Add(this.txtMODULE_ID);
            this.Controls.Add(this.txtDESCR);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsvPurview);
            this.Controls.Add(this.tsTool);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "frmPurview";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmPurview_KeyUp);
            this.Load += new System.EventHandler(this.frmPurview_Load);
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tlbAdd;
        private System.Windows.Forms.ToolStripButton tlbDelete;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView lsvPurview;
        private System.Windows.Forms.ComboBox cobCONTROL_PROPERTY;
        private System.Windows.Forms.TextBox txtWEB_FILE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtMODULE_NAME;
        private System.Windows.Forms.TextBox txtMODULE_ID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cobCONTROL_VALUE;
        private System.Windows.Forms.TextBox txtDESCR;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView trvTreeMenu;
        private System.Windows.Forms.TextBox txtCONTROL_NAME;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtORDER_ID;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader DESCR;
        private System.Windows.Forms.ColumnHeader WEB_FILE;
        private System.Windows.Forms.ColumnHeader CONTROL_NAME;
        private System.Windows.Forms.ColumnHeader CONTROL_PROPERTY;
        private System.Windows.Forms.ColumnHeader CONTROL_VALUE;
        private System.Windows.Forms.ColumnHeader ORDER_ID;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripButton tsbQuickAddRight;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.ToolStripButton tsbQuickAddFileRight;
        private System.Windows.Forms.TextBox txtOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColumnHeader OTHER_LANGUAGE_DESCR;
    }
}
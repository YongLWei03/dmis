namespace PlatForm
{
    partial class frmTreeMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTreeMenu));
            this.cbbTARTGET = new System.Windows.Forms.ComboBox();
            this.txtFILE_NAME = new System.Windows.Forms.TextBox();
            this.txtPARENT_ID = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trvTreeMenu = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbAdd = new System.Windows.Forms.ToolStripButton();
            this.tlbDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.tlbChangeParent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.txtTABLE_IDS = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtORDER_ID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNAME = new System.Windows.Forms.TextBox();
            this.txtEXPAND_IMAGE = new System.Windows.Forms.TextBox();
            this.txtOTHER_PARA = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTop = new System.Windows.Forms.Button();
            this.btnORDERS = new System.Windows.Forms.Button();
            this.txtORDERS = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnTABLE_IDS = new System.Windows.Forms.Button();
            this.btnEXPAND_IMAGE = new System.Windows.Forms.Button();
            this.cbbREPORT_ID = new System.Windows.Forms.ComboBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tsTool.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbbTARTGET
            // 
            resources.ApplyResources(this.cbbTARTGET, "cbbTARTGET");
            this.cbbTARTGET.FormattingEnabled = true;
            this.cbbTARTGET.Items.AddRange(new object[] {
            resources.GetString("cbbTARTGET.Items"),
            resources.GetString("cbbTARTGET.Items1")});
            this.cbbTARTGET.Name = "cbbTARTGET";
            // 
            // txtFILE_NAME
            // 
            resources.ApplyResources(this.txtFILE_NAME, "txtFILE_NAME");
            this.txtFILE_NAME.Name = "txtFILE_NAME";
            // 
            // txtPARENT_ID
            // 
            resources.ApplyResources(this.txtPARENT_ID, "txtPARENT_ID");
            this.txtPARENT_ID.Name = "txtPARENT_ID";
            this.txtPARENT_ID.ReadOnly = true;
            this.txtPARENT_ID.Validating += new System.ComponentModel.CancelEventHandler(this.txtPARENT_ID_Validating);
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
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
            // trvTreeMenu
            // 
            resources.ApplyResources(this.trvTreeMenu, "trvTreeMenu");
            this.trvTreeMenu.ImageList = this.imageList1;
            this.trvTreeMenu.LineColor = System.Drawing.Color.Maroon;
            this.trvTreeMenu.Name = "trvTreeMenu";
            this.trvTreeMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTreeMenu_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "parentNode.gif");
            this.imageList1.Images.SetKeyName(1, "selectChildNode.gif");
            // 
            // tsTool
            // 
            this.tsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbAdd,
            this.tlbDelete,
            this.tlbSave,
            this.tlbChangeParent,
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
            // tlbChangeParent
            // 
            this.tlbChangeParent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbChangeParent, "tlbChangeParent");
            this.tlbChangeParent.Name = "tlbChangeParent";
            this.tlbChangeParent.Click += new System.EventHandler(this.tlbChangeParent_Click);
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
            // txtTABLE_IDS
            // 
            resources.ApplyResources(this.txtTABLE_IDS, "txtTABLE_IDS");
            this.txtTABLE_IDS.Name = "txtTABLE_IDS";
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
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // txtNAME
            // 
            resources.ApplyResources(this.txtNAME, "txtNAME");
            this.txtNAME.Name = "txtNAME";
            // 
            // txtEXPAND_IMAGE
            // 
            resources.ApplyResources(this.txtEXPAND_IMAGE, "txtEXPAND_IMAGE");
            this.txtEXPAND_IMAGE.Name = "txtEXPAND_IMAGE";
            // 
            // txtOTHER_PARA
            // 
            resources.ApplyResources(this.txtOTHER_PARA, "txtOTHER_PARA");
            this.txtOTHER_PARA.Name = "txtOTHER_PARA";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtOTHER_LANGUAGE_DESCR);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnTop);
            this.groupBox1.Controls.Add(this.btnORDERS);
            this.groupBox1.Controls.Add(this.txtORDERS);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.btnTABLE_IDS);
            this.groupBox1.Controls.Add(this.btnEXPAND_IMAGE);
            this.groupBox1.Controls.Add(this.cbbREPORT_ID);
            this.groupBox1.Controls.Add(this.btnFile);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtORDER_ID);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.txtOTHER_LANGUAGE_DESCR, "txtOTHER_LANGUAGE_DESCR");
            this.txtOTHER_LANGUAGE_DESCR.Name = "txtOTHER_LANGUAGE_DESCR";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btnTop
            // 
            resources.ApplyResources(this.btnTop, "btnTop");
            this.btnTop.Image = global::Main.Properties.Resources.teban;
            this.btnTop.Name = "btnTop";
            this.btnTop.UseVisualStyleBackColor = true;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // btnORDERS
            // 
            resources.ApplyResources(this.btnORDERS, "btnORDERS");
            this.btnORDERS.Name = "btnORDERS";
            this.btnORDERS.UseVisualStyleBackColor = true;
            this.btnORDERS.Click += new System.EventHandler(this.btnORDERS_Click);
            // 
            // txtORDERS
            // 
            resources.ApplyResources(this.txtORDERS, "txtORDERS");
            this.txtORDERS.Name = "txtORDERS";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // btnTABLE_IDS
            // 
            resources.ApplyResources(this.btnTABLE_IDS, "btnTABLE_IDS");
            this.btnTABLE_IDS.Name = "btnTABLE_IDS";
            this.btnTABLE_IDS.UseVisualStyleBackColor = true;
            this.btnTABLE_IDS.Click += new System.EventHandler(this.btnTABLE_IDS_Click);
            // 
            // btnEXPAND_IMAGE
            // 
            resources.ApplyResources(this.btnEXPAND_IMAGE, "btnEXPAND_IMAGE");
            this.btnEXPAND_IMAGE.Name = "btnEXPAND_IMAGE";
            this.btnEXPAND_IMAGE.UseVisualStyleBackColor = true;
            this.btnEXPAND_IMAGE.Click += new System.EventHandler(this.btnEXPAND_IMAGE_Click);
            // 
            // cbbREPORT_ID
            // 
            resources.ApplyResources(this.cbbREPORT_ID, "cbbREPORT_ID");
            this.cbbREPORT_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbREPORT_ID.FormattingEnabled = true;
            this.cbbREPORT_ID.Name = "cbbREPORT_ID";
            // 
            // btnFile
            // 
            resources.ApplyResources(this.btnFile, "btnFile");
            this.btnFile.Name = "btnFile";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmTreeMenu
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtOTHER_PARA);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtEXPAND_IMAGE);
            this.Controls.Add(this.txtNAME);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tsTool);
            this.Controls.Add(this.cbbTARTGET);
            this.Controls.Add(this.txtTABLE_IDS);
            this.Controls.Add(this.txtFILE_NAME);
            this.Controls.Add(this.txtPARENT_ID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trvTreeMenu);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "frmTreeMenu";
            this.ShowInTaskbar = false;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmTreeMenu_KeyUp);
            this.Load += new System.EventHandler(this.frmTreeMenu_Load);
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbTARTGET;
        private System.Windows.Forms.TextBox txtFILE_NAME;
        private System.Windows.Forms.TextBox txtPARENT_ID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView trvTreeMenu;
        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tlbAdd;
        private System.Windows.Forms.ToolStripButton tlbDelete;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.TextBox txtTABLE_IDS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtORDER_ID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNAME;
        private System.Windows.Forms.TextBox txtEXPAND_IMAGE;
        private System.Windows.Forms.TextBox txtOTHER_PARA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.ComboBox cbbREPORT_ID;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnTABLE_IDS;
        private System.Windows.Forms.Button btnEXPAND_IMAGE;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtORDERS;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnORDERS;
        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.ToolStripButton tlbChangeParent;
        private System.Windows.Forms.TextBox txtOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.Label label6;
    }
}
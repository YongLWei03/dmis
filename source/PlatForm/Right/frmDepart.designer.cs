namespace PlatForm
{
    partial class frmDepart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDepart));
            this.trvDepart = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cbTYPE = new System.Windows.Forms.ComboBox();
            this.cbSUPERIOR_ID = new System.Windows.Forms.ComboBox();
            this.txtTELEPHONE = new System.Windows.Forms.TextBox();
            this.txtPOSTALCODE = new System.Windows.Forms.TextBox();
            this.txtLEADER = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtADDRESS = new System.Windows.Forms.TextBox();
            this.txtNAME = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtORDER_ID = new System.Windows.Forms.TextBox();
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbAdd = new System.Windows.Forms.ToolStripButton();
            this.tlbDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.tsTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // trvDepart
            // 
            resources.ApplyResources(this.trvDepart, "trvDepart");
            this.trvDepart.ImageList = this.imageList1;
            this.trvDepart.Name = "trvDepart";
            this.trvDepart.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDepart_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "parentNode.gif");
            this.imageList1.Images.SetKeyName(1, "selectChildNode.gif");
            // 
            // cbTYPE
            // 
            this.cbTYPE.FormattingEnabled = true;
            resources.ApplyResources(this.cbTYPE, "cbTYPE");
            this.cbTYPE.Name = "cbTYPE";
            // 
            // cbSUPERIOR_ID
            // 
            this.cbSUPERIOR_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbSUPERIOR_ID, "cbSUPERIOR_ID");
            this.cbSUPERIOR_ID.FormattingEnabled = true;
            this.cbSUPERIOR_ID.Name = "cbSUPERIOR_ID";
            this.cbSUPERIOR_ID.Tag = "";
            // 
            // txtTELEPHONE
            // 
            resources.ApplyResources(this.txtTELEPHONE, "txtTELEPHONE");
            this.txtTELEPHONE.Name = "txtTELEPHONE";
            // 
            // txtPOSTALCODE
            // 
            resources.ApplyResources(this.txtPOSTALCODE, "txtPOSTALCODE");
            this.txtPOSTALCODE.Name = "txtPOSTALCODE";
            // 
            // txtLEADER
            // 
            resources.ApplyResources(this.txtLEADER, "txtLEADER");
            this.txtLEADER.Name = "txtLEADER";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtADDRESS
            // 
            resources.ApplyResources(this.txtADDRESS, "txtADDRESS");
            this.txtADDRESS.Name = "txtADDRESS";
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
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtOTHER_LANGUAGE_DESCR);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtORDER_ID);
            this.groupBox1.Controls.Add(this.txtLEADER);
            this.groupBox1.Controls.Add(this.label1);
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
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txtORDER_ID
            // 
            resources.ApplyResources(this.txtORDER_ID, "txtORDER_ID");
            this.txtORDER_ID.Name = "txtORDER_ID";
            this.txtORDER_ID.Validating += new System.ComponentModel.CancelEventHandler(this.txtORDER_ID_Validating);
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
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmDepart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsTool);
            this.Controls.Add(this.cbTYPE);
            this.Controls.Add(this.cbSUPERIOR_ID);
            this.Controls.Add(this.txtTELEPHONE);
            this.Controls.Add(this.txtPOSTALCODE);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtADDRESS);
            this.Controls.Add(this.txtNAME);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trvDepart);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "frmDepart";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmDepart_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmDepart_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvDepart;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cbTYPE;
        private System.Windows.Forms.ComboBox cbSUPERIOR_ID;
        private System.Windows.Forms.TextBox txtTELEPHONE;
        private System.Windows.Forms.TextBox txtPOSTALCODE;
        private System.Windows.Forms.TextBox txtLEADER;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtADDRESS;
        private System.Windows.Forms.TextBox txtNAME;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtORDER_ID;
        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tlbAdd;
        private System.Windows.Forms.ToolStripButton tlbDelete;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.TextBox txtOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.Label label9;
    }
}
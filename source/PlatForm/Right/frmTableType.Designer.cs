namespace PlatForm
{
    partial class frmTableType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTableType));
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbAdd = new System.Windows.Forms.ToolStripButton();
            this.tlbDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.txtORDER_ID = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lvTableType = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.DESCR = new System.Windows.Forms.ColumnHeader();
            this.ORDER_ID = new System.Windows.Forms.ColumnHeader();
            this.OTHER_LANGUAGE_DESCR = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDESCR = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            // txtORDER_ID
            // 
            resources.ApplyResources(this.txtORDER_ID, "txtORDER_ID");
            this.txtORDER_ID.Name = "txtORDER_ID";
            this.txtORDER_ID.Validating += new System.ComponentModel.CancelEventHandler(this.txtORDER_ID_Validating);
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            // 
            // lvTableType
            // 
            resources.ApplyResources(this.lvTableType, "lvTableType");
            this.lvTableType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.DESCR,
            this.ORDER_ID,
            this.OTHER_LANGUAGE_DESCR});
            this.lvTableType.FullRowSelect = true;
            this.lvTableType.MultiSelect = false;
            this.lvTableType.Name = "lvTableType";
            this.lvTableType.UseCompatibleStateImageBehavior = false;
            this.lvTableType.View = System.Windows.Forms.View.Details;
            this.lvTableType.SelectedIndexChanged += new System.EventHandler(this.lvTableType_SelectedIndexChanged);
            // 
            // ID
            // 
            resources.ApplyResources(this.ID, "ID");
            // 
            // DESCR
            // 
            resources.ApplyResources(this.DESCR, "DESCR");
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
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.txtOTHER_LANGUAGE_DESCR);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtORDER_ID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDESCR);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.txtOTHER_LANGUAGE_DESCR, "txtOTHER_LANGUAGE_DESCR");
            this.txtOTHER_LANGUAGE_DESCR.Name = "txtOTHER_LANGUAGE_DESCR";
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
            // txtDESCR
            // 
            resources.ApplyResources(this.txtDESCR, "txtDESCR");
            this.txtDESCR.Name = "txtDESCR";
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
            // frmTableType
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvTableType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tsTool);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTableType";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmTableType_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmTableType_KeyUp);
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
        private System.Windows.Forms.TextBox txtORDER_ID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.ListView lvTableType;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader DESCR;
        private System.Windows.Forms.ColumnHeader ORDER_ID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDESCR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader OTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.TextBox txtOTHER_LANGUAGE_DESCR;
    }
}
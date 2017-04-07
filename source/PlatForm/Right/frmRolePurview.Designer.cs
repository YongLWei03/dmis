namespace PlatForm
{
    partial class frmRolePurview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRolePurview));
            this.trvTreeMenu = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lsvPurview = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.DESCR = new System.Windows.Forms.ColumnHeader();
            this.WEB_FILE = new System.Windows.Forms.ColumnHeader();
            this.CONTROL_NAME = new System.Windows.Forms.ColumnHeader();
            this.CONTROL_PROPERTY = new System.Windows.Forms.ColumnHeader();
            this.CONTROL_VALUE = new System.Windows.Forms.ColumnHeader();
            this.ORDER_ID = new System.Windows.Forms.ColumnHeader();
            this.trvRole = new System.Windows.Forms.TreeView();
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlbSelectAll = new System.Windows.Forms.ToolStripButton();
            this.tlbUnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.OTHER_LANGUAGE_DESCR = new System.Windows.Forms.ColumnHeader();
            this.tsTool.SuspendLayout();
            this.SuspendLayout();
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
            // lsvPurview
            // 
            resources.ApplyResources(this.lsvPurview, "lsvPurview");
            this.lsvPurview.CheckBoxes = true;
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
            // trvRole
            // 
            resources.ApplyResources(this.trvRole, "trvRole");
            this.trvRole.ImageList = this.imageList1;
            this.trvRole.Name = "trvRole";
            this.trvRole.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvRole_AfterSelect);
            // 
            // tsTool
            // 
            this.tsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbSave,
            this.toolStripSeparator1,
            this.tlbRefresh,
            this.tlbSelectAll,
            this.tlbUnSelectAll});
            resources.ApplyResources(this.tsTool, "tsTool");
            this.tsTool.Name = "tsTool";
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
            // tlbSelectAll
            // 
            this.tlbSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbSelectAll, "tlbSelectAll");
            this.tlbSelectAll.Name = "tlbSelectAll";
            this.tlbSelectAll.Click += new System.EventHandler(this.tlbSelectAll_Click);
            // 
            // tlbUnSelectAll
            // 
            this.tlbUnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbUnSelectAll, "tlbUnSelectAll");
            this.tlbUnSelectAll.Name = "tlbUnSelectAll";
            this.tlbUnSelectAll.Click += new System.EventHandler(this.tlbUnSelectAll_Click);
            // 
            // OTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.OTHER_LANGUAGE_DESCR, "OTHER_LANGUAGE_DESCR");
            // 
            // frmRolePurview
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsTool);
            this.Controls.Add(this.trvRole);
            this.Controls.Add(this.trvTreeMenu);
            this.Controls.Add(this.lsvPurview);
            this.Name = "frmRolePurview";
            this.Load += new System.EventHandler(this.frmRolePurview_Load);
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvTreeMenu;
        private System.Windows.Forms.ListView lsvPurview;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader DESCR;
        private System.Windows.Forms.ColumnHeader WEB_FILE;
        private System.Windows.Forms.ColumnHeader CONTROL_NAME;
        private System.Windows.Forms.ColumnHeader CONTROL_PROPERTY;
        private System.Windows.Forms.ColumnHeader CONTROL_VALUE;
        private System.Windows.Forms.ColumnHeader ORDER_ID;
        private System.Windows.Forms.TreeView trvRole;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlbSelectAll;
        private System.Windows.Forms.ToolStripButton tlbUnSelectAll;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.ColumnHeader OTHER_LANGUAGE_DESCR;
    }
}
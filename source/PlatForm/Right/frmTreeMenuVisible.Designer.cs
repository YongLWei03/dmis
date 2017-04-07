namespace PlatForm
{
    partial class frmTreeMenuVisible
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTreeMenuVisible));
            this.trvTreeMenu = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbSelectAll = new System.Windows.Forms.ToolStripButton();
            this.tlbUnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlbChildVisible = new System.Windows.Forms.ToolStripButton();
            this.chlRole = new System.Windows.Forms.CheckedListBox();
            this.lsvMember = new System.Windows.Forms.ListView();
            this.MEMBER_NAME = new System.Windows.Forms.ColumnHeader();
            this.MEMBER_ID = new System.Windows.Forms.ColumnHeader();
            this.DEPART_NAME = new System.Windows.Forms.ColumnHeader();
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
            // tsTool
            // 
            this.tsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbSelectAll,
            this.tlbUnSelectAll,
            this.tlbSave,
            this.toolStripSeparator1,
            this.tlbRefresh,
            this.tlbChildVisible});
            resources.ApplyResources(this.tsTool, "tsTool");
            this.tsTool.Name = "tsTool";
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
            // tlbChildVisible
            // 
            this.tlbChildVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbChildVisible, "tlbChildVisible");
            this.tlbChildVisible.Name = "tlbChildVisible";
            this.tlbChildVisible.Click += new System.EventHandler(this.tlbChildVisible_Click);
            // 
            // chlRole
            // 
            resources.ApplyResources(this.chlRole, "chlRole");
            this.chlRole.FormattingEnabled = true;
            this.chlRole.MultiColumn = true;
            this.chlRole.Name = "chlRole";
            this.chlRole.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chlRole_ItemCheck);
            // 
            // lsvMember
            // 
            resources.ApplyResources(this.lsvMember, "lsvMember");
            this.lsvMember.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MEMBER_NAME,
            this.MEMBER_ID,
            this.DEPART_NAME});
            this.lsvMember.Name = "lsvMember";
            this.lsvMember.UseCompatibleStateImageBehavior = false;
            this.lsvMember.View = System.Windows.Forms.View.Details;
            // 
            // MEMBER_NAME
            // 
            resources.ApplyResources(this.MEMBER_NAME, "MEMBER_NAME");
            // 
            // MEMBER_ID
            // 
            resources.ApplyResources(this.MEMBER_ID, "MEMBER_ID");
            // 
            // DEPART_NAME
            // 
            resources.ApplyResources(this.DEPART_NAME, "DEPART_NAME");
            // 
            // frmTreeMenuVisible
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvMember);
            this.Controls.Add(this.chlRole);
            this.Controls.Add(this.tsTool);
            this.Controls.Add(this.trvTreeMenu);
            this.Name = "frmTreeMenuVisible";
            this.Load += new System.EventHandler(this.frmTreeMenuVisible_Load);
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvTreeMenu;
        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckedListBox chlRole;
        private System.Windows.Forms.ListView lsvMember;
        private System.Windows.Forms.ColumnHeader MEMBER_NAME;
        private System.Windows.Forms.ColumnHeader MEMBER_ID;
        private System.Windows.Forms.ColumnHeader DEPART_NAME;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.ToolStripButton tlbSelectAll;
        private System.Windows.Forms.ToolStripButton tlbUnSelectAll;
        private System.Windows.Forms.ToolStripButton tlbChildVisible;
    }
}
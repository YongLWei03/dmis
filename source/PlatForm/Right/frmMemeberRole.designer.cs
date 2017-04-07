namespace PlatForm
{
    partial class frmMemeberRole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMemeberRole));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lsvMemeber = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.CODE = new System.Windows.Forms.ColumnHeader();
            this.NAME = new System.Windows.Forms.ColumnHeader();
            this.FLAG = new System.Windows.Forms.ColumnHeader();
            this.ORDER_ID = new System.Windows.Forms.ColumnHeader();
            this.trvDepart = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsbHasRols = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsbOtherRoles = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "parentNode.gif");
            this.imageList1.Images.SetKeyName(1, "selectChildNode.gif");
            // 
            // lsvMemeber
            // 
            resources.ApplyResources(this.lsvMemeber, "lsvMemeber");
            this.lsvMemeber.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.CODE,
            this.NAME,
            this.FLAG,
            this.ORDER_ID});
            this.lsvMemeber.FullRowSelect = true;
            this.lsvMemeber.GridLines = true;
            this.lsvMemeber.MultiSelect = false;
            this.lsvMemeber.Name = "lsvMemeber";
            this.lsvMemeber.UseCompatibleStateImageBehavior = false;
            this.lsvMemeber.View = System.Windows.Forms.View.Details;
            this.lsvMemeber.SelectedIndexChanged += new System.EventHandler(this.lsMemeber_SelectedIndexChanged);
            // 
            // ID
            // 
            resources.ApplyResources(this.ID, "ID");
            // 
            // CODE
            // 
            resources.ApplyResources(this.CODE, "CODE");
            // 
            // NAME
            // 
            resources.ApplyResources(this.NAME, "NAME");
            // 
            // FLAG
            // 
            resources.ApplyResources(this.FLAG, "FLAG");
            // 
            // ORDER_ID
            // 
            resources.ApplyResources(this.ORDER_ID, "ORDER_ID");
            // 
            // trvDepart
            // 
            resources.ApplyResources(this.trvDepart, "trvDepart");
            this.trvDepart.ImageList = this.imageList1;
            this.trvDepart.LineColor = System.Drawing.Color.Maroon;
            this.trvDepart.Name = "trvDepart";
            this.trvDepart.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDepart_AfterSelect);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.lsbHasRols);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lsbHasRols
            // 
            this.lsbHasRols.AllowDrop = true;
            this.lsbHasRols.FormattingEnabled = true;
            resources.ApplyResources(this.lsbHasRols, "lsbHasRols");
            this.lsbHasRols.Name = "lsbHasRols";
            this.lsbHasRols.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsbHasRols_DragEnter);
            this.lsbHasRols.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsbHasRols_DragDrop);
            this.lsbHasRols.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsbHasRols_MouseDown);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.lsbOtherRoles);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // lsbOtherRoles
            // 
            this.lsbOtherRoles.AllowDrop = true;
            resources.ApplyResources(this.lsbOtherRoles, "lsbOtherRoles");
            this.lsbOtherRoles.FormattingEnabled = true;
            this.lsbOtherRoles.Name = "lsbOtherRoles";
            this.lsbOtherRoles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsbOtherRoles_DragEnter);
            this.lsbOtherRoles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsbOtherRoles_DragDrop);
            this.lsbOtherRoles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsbOtherRoles_MouseDown);
            // 
            // frmMemeberRole
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.trvDepart);
            this.Controls.Add(this.lsvMemeber);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMemeberRole";
            this.Load += new System.EventHandler(this.frmMemeberRole_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView lsvMemeber;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader CODE;
        private System.Windows.Forms.ColumnHeader NAME;
        private System.Windows.Forms.ColumnHeader FLAG;
        private System.Windows.Forms.ColumnHeader ORDER_ID;
        private System.Windows.Forms.TreeView trvDepart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lsbOtherRoles;
        private System.Windows.Forms.ListBox lsbHasRols;
    }
}
namespace PlatForm
{
    partial class frmRoleMemeber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRoleMemeber));
            this.trvRole = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lsvHasMember = new System.Windows.Forms.ListView();
            this.MemberID = new System.Windows.Forms.ColumnHeader();
            this.MemberNAME = new System.Windows.Forms.ColumnHeader();
            this.Depart = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsvOtherMember = new System.Windows.Forms.ListView();
            this.OtherMemberID = new System.Windows.Forms.ColumnHeader();
            this.OtherMemberName = new System.Windows.Forms.ColumnHeader();
            this.OtherDepart = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvRole
            // 
            resources.ApplyResources(this.trvRole, "trvRole");
            this.trvRole.ImageList = this.imageList1;
            this.trvRole.Name = "trvRole";
            this.trvRole.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvRole_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "parentNode.gif");
            this.imageList1.Images.SetKeyName(1, "selectChildNode.gif");
            // 
            // lsvHasMember
            // 
            this.lsvHasMember.AllowDrop = true;
            resources.ApplyResources(this.lsvHasMember, "lsvHasMember");
            this.lsvHasMember.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MemberID,
            this.MemberNAME,
            this.Depart});
            this.lsvHasMember.FullRowSelect = true;
            this.lsvHasMember.MultiSelect = false;
            this.lsvHasMember.Name = "lsvHasMember";
            this.lsvHasMember.UseCompatibleStateImageBehavior = false;
            this.lsvHasMember.View = System.Windows.Forms.View.Details;
            this.lsvHasMember.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsvHasMember_DragEnter);
            this.lsvHasMember.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsvHasMember_DragDrop);
            this.lsvHasMember.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsvHasMember_MouseDown);
            // 
            // MemberID
            // 
            resources.ApplyResources(this.MemberID, "MemberID");
            // 
            // MemberNAME
            // 
            resources.ApplyResources(this.MemberNAME, "MemberNAME");
            // 
            // Depart
            // 
            resources.ApplyResources(this.Depart, "Depart");
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lsvOtherMember
            // 
            this.lsvOtherMember.AllowDrop = true;
            resources.ApplyResources(this.lsvOtherMember, "lsvOtherMember");
            this.lsvOtherMember.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.OtherMemberID,
            this.OtherMemberName,
            this.OtherDepart});
            this.lsvOtherMember.FullRowSelect = true;
            this.lsvOtherMember.MultiSelect = false;
            this.lsvOtherMember.Name = "lsvOtherMember";
            this.lsvOtherMember.UseCompatibleStateImageBehavior = false;
            this.lsvOtherMember.View = System.Windows.Forms.View.Details;
            this.lsvOtherMember.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsvOtherMember_DragEnter);
            this.lsvOtherMember.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsvOtherMember_DragDrop);
            this.lsvOtherMember.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsvOtherMember_MouseDown);
            // 
            // OtherMemberID
            // 
            resources.ApplyResources(this.OtherMemberID, "OtherMemberID");
            // 
            // OtherMemberName
            // 
            resources.ApplyResources(this.OtherMemberName, "OtherMemberName");
            // 
            // OtherDepart
            // 
            resources.ApplyResources(this.OtherDepart, "OtherDepart");
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.lsvOtherMember);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // frmRoleMemeber
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvHasMember);
            this.Controls.Add(this.trvRole);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmRoleMemeber";
            this.Load += new System.EventHandler(this.frmRoleMemeber_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvRole;
        private System.Windows.Forms.ListView lsvHasMember;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lsvOtherMember;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader MemberID;
        private System.Windows.Forms.ColumnHeader MemberNAME;
        private System.Windows.Forms.ColumnHeader Depart;
        private System.Windows.Forms.ColumnHeader OtherMemberID;
        private System.Windows.Forms.ColumnHeader OtherMemberName;
        private System.Windows.Forms.ColumnHeader OtherDepart;
    }
}
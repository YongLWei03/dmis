namespace PlatForm.WorkFlow
{
    partial class frmFlowRight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFlowRight));
            this.cbbRole = new System.Windows.Forms.ComboBox();
            this.chkRight3 = new System.Windows.Forms.CheckBox();
            this.chkRight2 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pan_pack = new System.Windows.Forms.Panel();
            this.chkRight6 = new System.Windows.Forms.CheckBox();
            this.chkRight7 = new System.Windows.Forms.CheckBox();
            this.chkRight1 = new System.Windows.Forms.CheckBox();
            this.chkRight5 = new System.Windows.Forms.CheckBox();
            this.chkRight4 = new System.Windows.Forms.CheckBox();
            this.lsvRight = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pan_pack.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbRole
            // 
            this.cbbRole.FormattingEnabled = true;
            resources.ApplyResources(this.cbbRole, "cbbRole");
            this.cbbRole.Name = "cbbRole";
            // 
            // chkRight3
            // 
            resources.ApplyResources(this.chkRight3, "chkRight3");
            this.chkRight3.Name = "chkRight3";
            this.chkRight3.UseVisualStyleBackColor = true;
            // 
            // chkRight2
            // 
            resources.ApplyResources(this.chkRight2, "chkRight2");
            this.chkRight2.Name = "chkRight2";
            this.chkRight2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnDel
            // 
            resources.ApplyResources(this.btnDel, "btnDel");
            this.btnDel.Name = "btnDel";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pan_pack);
            this.groupBox1.Controls.Add(this.cbbRole);
            this.groupBox1.Controls.Add(this.chkRight1);
            this.groupBox1.Controls.Add(this.chkRight5);
            this.groupBox1.Controls.Add(this.chkRight4);
            this.groupBox1.Controls.Add(this.chkRight2);
            this.groupBox1.Controls.Add(this.chkRight3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // pan_pack
            // 
            this.pan_pack.Controls.Add(this.chkRight6);
            this.pan_pack.Controls.Add(this.chkRight7);
            resources.ApplyResources(this.pan_pack, "pan_pack");
            this.pan_pack.Name = "pan_pack";
            // 
            // chkRight6
            // 
            resources.ApplyResources(this.chkRight6, "chkRight6");
            this.chkRight6.Name = "chkRight6";
            this.chkRight6.UseVisualStyleBackColor = true;
            // 
            // chkRight7
            // 
            resources.ApplyResources(this.chkRight7, "chkRight7");
            this.chkRight7.Name = "chkRight7";
            this.chkRight7.UseVisualStyleBackColor = true;
            // 
            // chkRight1
            // 
            resources.ApplyResources(this.chkRight1, "chkRight1");
            this.chkRight1.Name = "chkRight1";
            this.chkRight1.UseVisualStyleBackColor = true;
            // 
            // chkRight5
            // 
            resources.ApplyResources(this.chkRight5, "chkRight5");
            this.chkRight5.Name = "chkRight5";
            this.chkRight5.UseVisualStyleBackColor = true;
            // 
            // chkRight4
            // 
            resources.ApplyResources(this.chkRight4, "chkRight4");
            this.chkRight4.Name = "chkRight4";
            this.chkRight4.UseVisualStyleBackColor = true;
            // 
            // lsvRight
            // 
            this.lsvRight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lsvRight.FullRowSelect = true;
            this.lsvRight.GridLines = true;
            this.lsvRight.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("lsvRight.Items")))});
            resources.ApplyResources(this.lsvRight, "lsvRight");
            this.lsvRight.MultiSelect = false;
            this.lsvRight.Name = "lsvRight";
            this.lsvRight.UseCompatibleStateImageBehavior = false;
            this.lsvRight.View = System.Windows.Forms.View.Details;
            this.lsvRight.Click += new System.EventHandler(this.lsvRight_Click);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // frmFlowRight
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvRight);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFlowRight";
            this.Load += new System.EventHandler(this.frmFlowRight_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pan_pack.ResumeLayout(false);
            this.pan_pack.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbRole;
        private System.Windows.Forms.CheckBox chkRight3;
        private System.Windows.Forms.CheckBox chkRight2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRight6;
        private System.Windows.Forms.CheckBox chkRight7;
        private System.Windows.Forms.ListView lsvRight;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.CheckBox chkRight1;
        private System.Windows.Forms.CheckBox chkRight5;
        private System.Windows.Forms.CheckBox chkRight4;
        private System.Windows.Forms.Panel pan_pack;
    }
}
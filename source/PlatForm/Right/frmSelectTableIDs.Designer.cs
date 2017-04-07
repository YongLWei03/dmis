namespace PlatForm
{
    partial class frmSelectTableIDs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectTableIDs));
            this.lsvTables = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.NAME = new System.Windows.Forms.ColumnHeader();
            this.TABLE_DESCR = new System.Windows.Forms.ColumnHeader();
            this.OTHER_LANGUAGE_DESCR = new System.Windows.Forms.ColumnHeader();
            this.OWNER = new System.Windows.Forms.ColumnHeader();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tvTableType = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lsvTables
            // 
            this.lsvTables.CheckBoxes = true;
            this.lsvTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.NAME,
            this.TABLE_DESCR,
            this.OTHER_LANGUAGE_DESCR,
            this.OWNER});
            this.lsvTables.FullRowSelect = true;
            this.lsvTables.Location = new System.Drawing.Point(253, 9);
            this.lsvTables.MultiSelect = false;
            this.lsvTables.Name = "lsvTables";
            this.lsvTables.Size = new System.Drawing.Size(563, 388);
            this.lsvTables.TabIndex = 0;
            this.lsvTables.UseCompatibleStateImageBehavior = false;
            this.lsvTables.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "表ID";
            this.ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ID.Width = 50;
            // 
            // NAME
            // 
            this.NAME.Text = "表名";
            this.NAME.Width = 150;
            // 
            // TABLE_DESCR
            // 
            this.TABLE_DESCR.Text = "中文表名描述";
            this.TABLE_DESCR.Width = 150;
            // 
            // OTHER_LANGUAGE_DESCR
            // 
            this.OTHER_LANGUAGE_DESCR.Text = "西文表名描述";
            this.OTHER_LANGUAGE_DESCR.Width = 150;
            // 
            // OWNER
            // 
            this.OWNER.Text = "拥有者";
            this.OWNER.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OWNER.Width = 80;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(311, 410);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(440, 410);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tvTableType
            // 
            this.tvTableType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvTableType.FullRowSelect = true;
            this.tvTableType.ImageIndex = 0;
            this.tvTableType.ImageList = this.imageList1;
            this.tvTableType.LineColor = System.Drawing.Color.Maroon;
            this.tvTableType.Location = new System.Drawing.Point(9, 8);
            this.tvTableType.Name = "tvTableType";
            this.tvTableType.SelectedImageIndex = 2;
            this.tvTableType.Size = new System.Drawing.Size(235, 388);
            this.tvTableType.TabIndex = 3;
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
            // frmSelectTableIDs
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(827, 440);
            this.Controls.Add(this.tvTableType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lsvTables);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectTableIDs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择数据库表";
            this.Load += new System.EventHandler(this.frmSelectTableIDs_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvTables;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader OWNER;
        private System.Windows.Forms.ColumnHeader NAME;
        private System.Windows.Forms.ColumnHeader TABLE_DESCR;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TreeView tvTableType;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader OTHER_LANGUAGE_DESCR;
    }
}
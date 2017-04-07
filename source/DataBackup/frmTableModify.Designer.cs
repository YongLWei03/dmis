namespace DataBackup
{
    partial class frmTableModify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTableModify));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsbTable = new System.Windows.Forms.ListBox();
            this.cbbDataBase = new System.Windows.Forms.ComboBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.ddl2_tblColTable = new System.Windows.Forms.DataGridView();
            this.panel22 = new System.Windows.Forms.Panel();
            this.ddl2_tblColChk = new System.Windows.Forms.CheckBox();
            this.ddl2_tblNameChk = new System.Windows.Forms.CheckBox();
            this.ddl2_tblNameText = new System.Windows.Forms.TextBox();
            this.panel23 = new System.Windows.Forms.Panel();
            this.ddl2_doModifyBtn = new System.Windows.Forms.Button();
            this.ddl2_delBtn = new System.Windows.Forms.Button();
            this.ddl2_addBtn = new System.Windows.Forms.Button();
            this.ddl2_downBtn = new System.Windows.Forms.Button();
            this.ddl2_upBtn = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddl2_tblColTable)).BeginInit();
            this.panel22.SuspendLayout();
            this.panel23.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lsbTable);
            this.groupBox1.Controls.Add(this.cbbDataBase);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 524);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库表名";
            // 
            // lsbTable
            // 
            this.lsbTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbTable.FormattingEnabled = true;
            this.lsbTable.ItemHeight = 12;
            this.lsbTable.Location = new System.Drawing.Point(3, 47);
            this.lsbTable.Name = "lsbTable";
            this.lsbTable.Size = new System.Drawing.Size(207, 472);
            this.lsbTable.TabIndex = 1;
            this.lsbTable.SelectedIndexChanged += new System.EventHandler(this.lsbTable_SelectedIndexChanged);
            // 
            // cbbDataBase
            // 
            this.cbbDataBase.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbbDataBase.FormattingEnabled = true;
            this.cbbDataBase.Location = new System.Drawing.Point(3, 17);
            this.cbbDataBase.Name = "cbbDataBase";
            this.cbbDataBase.Size = new System.Drawing.Size(207, 20);
            this.cbbDataBase.TabIndex = 0;
            this.cbbDataBase.SelectedIndexChanged += new System.EventHandler(this.cbbDataBase_SelectedIndexChanged);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.ddl2_tblColTable);
            this.groupBox11.Controls.Add(this.panel22);
            this.groupBox11.Controls.Add(this.panel23);
            this.groupBox11.Location = new System.Drawing.Point(247, 12);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.groupBox11.Size = new System.Drawing.Size(657, 524);
            this.groupBox11.TabIndex = 70;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "所选表信息";
            // 
            // ddl2_tblColTable
            // 
            this.ddl2_tblColTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ddl2_tblColTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewComboBoxColumn3,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewCheckBoxColumn6,
            this.dataGridViewCheckBoxColumn7,
            this.dataGridViewCheckBoxColumn8});
            this.ddl2_tblColTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddl2_tblColTable.Location = new System.Drawing.Point(8, 84);
            this.ddl2_tblColTable.Name = "ddl2_tblColTable";
            this.ddl2_tblColTable.RowTemplate.Height = 23;
            this.ddl2_tblColTable.Size = new System.Drawing.Size(641, 399);
            this.ddl2_tblColTable.TabIndex = 64;
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.ddl2_tblColChk);
            this.panel22.Controls.Add(this.ddl2_tblNameChk);
            this.panel22.Controls.Add(this.ddl2_tblNameText);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel22.Location = new System.Drawing.Point(8, 17);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(641, 67);
            this.panel22.TabIndex = 63;
            // 
            // ddl2_tblColChk
            // 
            this.ddl2_tblColChk.AutoSize = true;
            this.ddl2_tblColChk.Location = new System.Drawing.Point(3, 45);
            this.ddl2_tblColChk.Name = "ddl2_tblColChk";
            this.ddl2_tblColChk.Size = new System.Drawing.Size(84, 16);
            this.ddl2_tblColChk.TabIndex = 49;
            this.ddl2_tblColChk.Text = "修改表字段";
            this.ddl2_tblColChk.UseVisualStyleBackColor = true;
            // 
            // ddl2_tblNameChk
            // 
            this.ddl2_tblNameChk.AutoSize = true;
            this.ddl2_tblNameChk.Location = new System.Drawing.Point(3, 14);
            this.ddl2_tblNameChk.Name = "ddl2_tblNameChk";
            this.ddl2_tblNameChk.Size = new System.Drawing.Size(72, 16);
            this.ddl2_tblNameChk.TabIndex = 48;
            this.ddl2_tblNameChk.Text = "修改表名";
            this.ddl2_tblNameChk.UseVisualStyleBackColor = true;
            // 
            // ddl2_tblNameText
            // 
            this.ddl2_tblNameText.Location = new System.Drawing.Point(81, 12);
            this.ddl2_tblNameText.Name = "ddl2_tblNameText";
            this.ddl2_tblNameText.Size = new System.Drawing.Size(261, 21);
            this.ddl2_tblNameText.TabIndex = 47;
            this.ddl2_tblNameText.Text = "new_table";
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.ddl2_doModifyBtn);
            this.panel23.Controls.Add(this.ddl2_delBtn);
            this.panel23.Controls.Add(this.ddl2_addBtn);
            this.panel23.Controls.Add(this.ddl2_downBtn);
            this.panel23.Controls.Add(this.ddl2_upBtn);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel23.Location = new System.Drawing.Point(8, 483);
            this.panel23.Name = "panel23";
            this.panel23.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panel23.Size = new System.Drawing.Size(641, 38);
            this.panel23.TabIndex = 62;
            // 
            // ddl2_doModifyBtn
            // 
            this.ddl2_doModifyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddl2_doModifyBtn.Location = new System.Drawing.Point(552, 7);
            this.ddl2_doModifyBtn.Name = "ddl2_doModifyBtn";
            this.ddl2_doModifyBtn.Size = new System.Drawing.Size(75, 23);
            this.ddl2_doModifyBtn.TabIndex = 56;
            this.ddl2_doModifyBtn.Text = "执行修改";
            this.ddl2_doModifyBtn.UseVisualStyleBackColor = true;
            // 
            // ddl2_delBtn
            // 
            this.ddl2_delBtn.Location = new System.Drawing.Point(271, 7);
            this.ddl2_delBtn.Name = "ddl2_delBtn";
            this.ddl2_delBtn.Size = new System.Drawing.Size(75, 23);
            this.ddl2_delBtn.TabIndex = 55;
            this.ddl2_delBtn.Text = "删除一行";
            this.ddl2_delBtn.UseVisualStyleBackColor = true;
            // 
            // ddl2_addBtn
            // 
            this.ddl2_addBtn.Location = new System.Drawing.Point(189, 7);
            this.ddl2_addBtn.Name = "ddl2_addBtn";
            this.ddl2_addBtn.Size = new System.Drawing.Size(75, 23);
            this.ddl2_addBtn.TabIndex = 54;
            this.ddl2_addBtn.Text = "增加一行";
            this.ddl2_addBtn.UseVisualStyleBackColor = true;
            // 
            // ddl2_downBtn
            // 
            this.ddl2_downBtn.Enabled = false;
            this.ddl2_downBtn.Image = ((System.Drawing.Image)(resources.GetObject("ddl2_downBtn.Image")));
            this.ddl2_downBtn.Location = new System.Drawing.Point(108, 7);
            this.ddl2_downBtn.Name = "ddl2_downBtn";
            this.ddl2_downBtn.Size = new System.Drawing.Size(75, 23);
            this.ddl2_downBtn.TabIndex = 53;
            this.ddl2_downBtn.UseVisualStyleBackColor = true;
            // 
            // ddl2_upBtn
            // 
            this.ddl2_upBtn.Enabled = false;
            this.ddl2_upBtn.Image = ((System.Drawing.Image)(resources.GetObject("ddl2_upBtn.Image")));
            this.ddl2_upBtn.Location = new System.Drawing.Point(27, 7);
            this.ddl2_upBtn.Name = "ddl2_upBtn";
            this.ddl2_upBtn.Size = new System.Drawing.Size(75, 23);
            this.ddl2_upBtn.TabIndex = 52;
            this.ddl2_upBtn.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "字段名";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewComboBoxColumn3
            // 
            this.dataGridViewComboBoxColumn3.HeaderText = "类型";
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            this.dataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "长度";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewCheckBoxColumn6
            // 
            this.dataGridViewCheckBoxColumn6.HeaderText = "主键/索引";
            this.dataGridViewCheckBoxColumn6.Name = "dataGridViewCheckBoxColumn6";
            this.dataGridViewCheckBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewCheckBoxColumn7
            // 
            this.dataGridViewCheckBoxColumn7.HeaderText = "空值";
            this.dataGridViewCheckBoxColumn7.Name = "dataGridViewCheckBoxColumn7";
            // 
            // dataGridViewCheckBoxColumn8
            // 
            this.dataGridViewCheckBoxColumn8.HeaderText = "唯一性";
            this.dataGridViewCheckBoxColumn8.Name = "dataGridViewCheckBoxColumn8";
            this.dataGridViewCheckBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // frmTableModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 593);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTableModify";
            this.Text = "修改表结构";
            this.Load += new System.EventHandler(this.frmTableModify_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ddl2_tblColTable)).EndInit();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lsbTable;
        private System.Windows.Forms.ComboBox cbbDataBase;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.DataGridView ddl2_tblColTable;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.CheckBox ddl2_tblColChk;
        private System.Windows.Forms.CheckBox ddl2_tblNameChk;
        private System.Windows.Forms.TextBox ddl2_tblNameText;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Button ddl2_doModifyBtn;
        private System.Windows.Forms.Button ddl2_delBtn;
        private System.Windows.Forms.Button ddl2_addBtn;
        private System.Windows.Forms.Button ddl2_downBtn;
        private System.Windows.Forms.Button ddl2_upBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn8;

    }
}
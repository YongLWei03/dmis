namespace DataBackup
{
    partial class frmDataModify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataModify));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsbTable = new System.Windows.Forms.ListBox();
            this.cbbDataBase = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labIDName = new System.Windows.Forms.Label();
            this.rdbPart = new System.Windows.Forms.RadioButton();
            this.rdbAll = new System.Windows.Forms.RadioButton();
            this.btnSelectCol = new System.Windows.Forms.Button();
            this.lsbColumn = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ckbWhere = new System.Windows.Forms.CheckBox();
            this.labWhere = new System.Windows.Forms.Label();
            this.cbbAndOr3 = new System.Windows.Forms.ComboBox();
            this.txtValue3 = new System.Windows.Forms.TextBox();
            this.cbbGuanX3 = new System.Windows.Forms.ComboBox();
            this.cbbColumn3 = new System.Windows.Forms.ComboBox();
            this.cbbAndOr2 = new System.Windows.Forms.ComboBox();
            this.txtValue2 = new System.Windows.Forms.TextBox();
            this.cbbGuanX2 = new System.Windows.Forms.ComboBox();
            this.cbbColumn2 = new System.Windows.Forms.ComboBox();
            this.cbbAndOr1 = new System.Windows.Forms.ComboBox();
            this.txtValue1 = new System.Windows.Forms.TextBox();
            this.cbbGuanX1 = new System.Windows.Forms.ComboBox();
            this.cbbColumn1 = new System.Windows.Forms.ComboBox();
            this.cbbAndOr = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cbbGuanXi = new System.Windows.Forms.ComboBox();
            this.cbbColumn = new System.Windows.Forms.ComboBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.labText = new System.Windows.Forms.Label();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.butQuery = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.labSelect = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
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
            this.groupBox1.TabIndex = 2;
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(177, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "label7";
            this.label7.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labIDName);
            this.groupBox2.Controls.Add(this.rdbPart);
            this.groupBox2.Controls.Add(this.rdbAll);
            this.groupBox2.Controls.Add(this.btnSelectCol);
            this.groupBox2.Controls.Add(this.lsbColumn);
            this.groupBox2.Location = new System.Drawing.Point(247, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 175);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表字段名";
            // 
            // labIDName
            // 
            this.labIDName.AutoSize = true;
            this.labIDName.Location = new System.Drawing.Point(180, 152);
            this.labIDName.Name = "labIDName";
            this.labIDName.Size = new System.Drawing.Size(0, 12);
            this.labIDName.TabIndex = 7;
            this.labIDName.Visible = false;
            // 
            // rdbPart
            // 
            this.rdbPart.AutoSize = true;
            this.rdbPart.Location = new System.Drawing.Point(109, 148);
            this.rdbPart.Name = "rdbPart";
            this.rdbPart.Size = new System.Drawing.Size(47, 16);
            this.rdbPart.TabIndex = 6;
            this.rdbPart.Text = "部分";
            this.rdbPart.UseVisualStyleBackColor = true;
            // 
            // rdbAll
            // 
            this.rdbAll.AutoSize = true;
            this.rdbAll.Checked = true;
            this.rdbAll.Location = new System.Drawing.Point(61, 148);
            this.rdbAll.Name = "rdbAll";
            this.rdbAll.Size = new System.Drawing.Size(47, 16);
            this.rdbAll.TabIndex = 5;
            this.rdbAll.TabStop = true;
            this.rdbAll.Text = "全部";
            this.rdbAll.UseVisualStyleBackColor = true;
            // 
            // btnSelectCol
            // 
            this.btnSelectCol.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectCol.Image")));
            this.btnSelectCol.Location = new System.Drawing.Point(6, 145);
            this.btnSelectCol.Name = "btnSelectCol";
            this.btnSelectCol.Size = new System.Drawing.Size(49, 23);
            this.btnSelectCol.TabIndex = 4;
            this.btnSelectCol.UseVisualStyleBackColor = true;
            this.btnSelectCol.Click += new System.EventHandler(this.btnSelectCol_Click);
            // 
            // lsbColumn
            // 
            this.lsbColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbColumn.FormattingEnabled = true;
            this.lsbColumn.ItemHeight = 12;
            this.lsbColumn.Location = new System.Drawing.Point(5, 17);
            this.lsbColumn.Margin = new System.Windows.Forms.Padding(2);
            this.lsbColumn.MultiColumn = true;
            this.lsbColumn.Name = "lsbColumn";
            this.lsbColumn.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lsbColumn.Size = new System.Drawing.Size(242, 124);
            this.lsbColumn.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ckbWhere);
            this.groupBox3.Controls.Add(this.labWhere);
            this.groupBox3.Controls.Add(this.cbbAndOr3);
            this.groupBox3.Controls.Add(this.txtValue3);
            this.groupBox3.Controls.Add(this.cbbGuanX3);
            this.groupBox3.Controls.Add(this.cbbColumn3);
            this.groupBox3.Controls.Add(this.cbbAndOr2);
            this.groupBox3.Controls.Add(this.txtValue2);
            this.groupBox3.Controls.Add(this.cbbGuanX2);
            this.groupBox3.Controls.Add(this.cbbColumn2);
            this.groupBox3.Controls.Add(this.cbbAndOr1);
            this.groupBox3.Controls.Add(this.txtValue1);
            this.groupBox3.Controls.Add(this.cbbGuanX1);
            this.groupBox3.Controls.Add(this.cbbColumn1);
            this.groupBox3.Controls.Add(this.cbbAndOr);
            this.groupBox3.Controls.Add(this.txtValue);
            this.groupBox3.Controls.Add(this.cbbGuanXi);
            this.groupBox3.Controls.Add(this.cbbColumn);
            this.groupBox3.Location = new System.Drawing.Point(505, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 175);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "检索条件";
            // 
            // ckbWhere
            // 
            this.ckbWhere.AutoSize = true;
            this.ckbWhere.Location = new System.Drawing.Point(6, -2);
            this.ckbWhere.Name = "ckbWhere";
            this.ckbWhere.Size = new System.Drawing.Size(72, 16);
            this.ckbWhere.TabIndex = 22;
            this.ckbWhere.Text = "检索条件";
            this.ckbWhere.UseVisualStyleBackColor = true;
            // 
            // labWhere
            // 
            this.labWhere.AutoSize = true;
            this.labWhere.ForeColor = System.Drawing.Color.Red;
            this.labWhere.Location = new System.Drawing.Point(7, 145);
            this.labWhere.Name = "labWhere";
            this.labWhere.Size = new System.Drawing.Size(0, 12);
            this.labWhere.TabIndex = 21;
            // 
            // cbbAndOr3
            // 
            this.cbbAndOr3.FormattingEnabled = true;
            this.cbbAndOr3.Location = new System.Drawing.Point(345, 110);
            this.cbbAndOr3.Name = "cbbAndOr3";
            this.cbbAndOr3.Size = new System.Drawing.Size(48, 20);
            this.cbbAndOr3.TabIndex = 20;
            this.cbbAndOr3.Visible = false;
            // 
            // txtValue3
            // 
            this.txtValue3.Location = new System.Drawing.Point(194, 109);
            this.txtValue3.Name = "txtValue3";
            this.txtValue3.Size = new System.Drawing.Size(145, 21);
            this.txtValue3.TabIndex = 19;
            this.txtValue3.Visible = false;
            // 
            // cbbGuanX3
            // 
            this.cbbGuanX3.FormattingEnabled = true;
            this.cbbGuanX3.Location = new System.Drawing.Point(136, 110);
            this.cbbGuanX3.Name = "cbbGuanX3";
            this.cbbGuanX3.Size = new System.Drawing.Size(52, 20);
            this.cbbGuanX3.TabIndex = 18;
            this.cbbGuanX3.Visible = false;
            // 
            // cbbColumn3
            // 
            this.cbbColumn3.FormattingEnabled = true;
            this.cbbColumn3.Location = new System.Drawing.Point(9, 110);
            this.cbbColumn3.Name = "cbbColumn3";
            this.cbbColumn3.Size = new System.Drawing.Size(121, 20);
            this.cbbColumn3.TabIndex = 17;
            this.cbbColumn3.Visible = false;
            this.cbbColumn3.SelectedIndexChanged += new System.EventHandler(this.cbbColumn3_SelectedIndexChanged);
            // 
            // cbbAndOr2
            // 
            this.cbbAndOr2.FormattingEnabled = true;
            this.cbbAndOr2.Items.AddRange(new object[] {
            "",
            "and",
            "or"});
            this.cbbAndOr2.Location = new System.Drawing.Point(345, 80);
            this.cbbAndOr2.Name = "cbbAndOr2";
            this.cbbAndOr2.Size = new System.Drawing.Size(48, 20);
            this.cbbAndOr2.TabIndex = 12;
            this.cbbAndOr2.Visible = false;
            this.cbbAndOr2.SelectedIndexChanged += new System.EventHandler(this.cbbAndOr2_SelectedIndexChanged);
            // 
            // txtValue2
            // 
            this.txtValue2.Location = new System.Drawing.Point(194, 79);
            this.txtValue2.Name = "txtValue2";
            this.txtValue2.Size = new System.Drawing.Size(145, 21);
            this.txtValue2.TabIndex = 11;
            this.txtValue2.Visible = false;
            // 
            // cbbGuanX2
            // 
            this.cbbGuanX2.FormattingEnabled = true;
            this.cbbGuanX2.Location = new System.Drawing.Point(136, 80);
            this.cbbGuanX2.Name = "cbbGuanX2";
            this.cbbGuanX2.Size = new System.Drawing.Size(52, 20);
            this.cbbGuanX2.TabIndex = 10;
            this.cbbGuanX2.Visible = false;
            // 
            // cbbColumn2
            // 
            this.cbbColumn2.FormattingEnabled = true;
            this.cbbColumn2.Location = new System.Drawing.Point(9, 80);
            this.cbbColumn2.Name = "cbbColumn2";
            this.cbbColumn2.Size = new System.Drawing.Size(121, 20);
            this.cbbColumn2.TabIndex = 9;
            this.cbbColumn2.Visible = false;
            this.cbbColumn2.SelectedIndexChanged += new System.EventHandler(this.cbbColumn2_SelectedIndexChanged);
            // 
            // cbbAndOr1
            // 
            this.cbbAndOr1.FormattingEnabled = true;
            this.cbbAndOr1.Items.AddRange(new object[] {
            "",
            "and",
            "or"});
            this.cbbAndOr1.Location = new System.Drawing.Point(345, 50);
            this.cbbAndOr1.Name = "cbbAndOr1";
            this.cbbAndOr1.Size = new System.Drawing.Size(48, 20);
            this.cbbAndOr1.TabIndex = 8;
            this.cbbAndOr1.Visible = false;
            this.cbbAndOr1.SelectedIndexChanged += new System.EventHandler(this.cbbAndOr1_SelectedIndexChanged);
            // 
            // txtValue1
            // 
            this.txtValue1.Location = new System.Drawing.Point(194, 49);
            this.txtValue1.Name = "txtValue1";
            this.txtValue1.Size = new System.Drawing.Size(145, 21);
            this.txtValue1.TabIndex = 7;
            this.txtValue1.Visible = false;
            // 
            // cbbGuanX1
            // 
            this.cbbGuanX1.FormattingEnabled = true;
            this.cbbGuanX1.Location = new System.Drawing.Point(136, 50);
            this.cbbGuanX1.Name = "cbbGuanX1";
            this.cbbGuanX1.Size = new System.Drawing.Size(52, 20);
            this.cbbGuanX1.TabIndex = 6;
            this.cbbGuanX1.Visible = false;
            // 
            // cbbColumn1
            // 
            this.cbbColumn1.FormattingEnabled = true;
            this.cbbColumn1.Location = new System.Drawing.Point(9, 50);
            this.cbbColumn1.Name = "cbbColumn1";
            this.cbbColumn1.Size = new System.Drawing.Size(121, 20);
            this.cbbColumn1.TabIndex = 5;
            this.cbbColumn1.Visible = false;
            this.cbbColumn1.SelectedIndexChanged += new System.EventHandler(this.cbbColumn1_SelectedIndexChanged);
            // 
            // cbbAndOr
            // 
            this.cbbAndOr.FormattingEnabled = true;
            this.cbbAndOr.Items.AddRange(new object[] {
            "",
            "and",
            "or"});
            this.cbbAndOr.Location = new System.Drawing.Point(345, 20);
            this.cbbAndOr.Name = "cbbAndOr";
            this.cbbAndOr.Size = new System.Drawing.Size(48, 20);
            this.cbbAndOr.TabIndex = 4;
            this.cbbAndOr.SelectedIndexChanged += new System.EventHandler(this.cbbAndOr_SelectedIndexChanged);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(194, 19);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(145, 21);
            this.txtValue.TabIndex = 2;
            // 
            // cbbGuanXi
            // 
            this.cbbGuanXi.FormattingEnabled = true;
            this.cbbGuanXi.Location = new System.Drawing.Point(136, 20);
            this.cbbGuanXi.Name = "cbbGuanXi";
            this.cbbGuanXi.Size = new System.Drawing.Size(52, 20);
            this.cbbGuanXi.TabIndex = 1;
            // 
            // cbbColumn
            // 
            this.cbbColumn.FormattingEnabled = true;
            this.cbbColumn.Location = new System.Drawing.Point(9, 20);
            this.cbbColumn.Name = "cbbColumn";
            this.cbbColumn.Size = new System.Drawing.Size(121, 20);
            this.cbbColumn.TabIndex = 0;
            this.cbbColumn.SelectedIndexChanged += new System.EventHandler(this.cbbColumn_SelectedIndexChanged);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(247, 193);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(657, 314);
            this.dgvData.TabIndex = 8;
            // 
            // labText
            // 
            this.labText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labText.AutoSize = true;
            this.labText.Location = new System.Drawing.Point(13, 539);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(0, 12);
            this.labText.TabIndex = 9;
            // 
            // btnClean
            // 
            this.btnClean.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClean.Location = new System.Drawing.Point(829, 513);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 12;
            this.btnClean.Text = "清空";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnModify
            // 
            this.btnModify.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnModify.Location = new System.Drawing.Point(439, 513);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 11;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // butQuery
            // 
            this.butQuery.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.butQuery.Location = new System.Drawing.Point(247, 513);
            this.butQuery.Name = "butQuery";
            this.butQuery.Size = new System.Drawing.Size(75, 23);
            this.butQuery.TabIndex = 10;
            this.butQuery.Text = "查询";
            this.butQuery.UseVisualStyleBackColor = true;
            this.butQuery.Click += new System.EventHandler(this.butQuery_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDelete.Location = new System.Drawing.Point(631, 513);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // labSelect
            // 
            this.labSelect.AutoSize = true;
            this.labSelect.Location = new System.Drawing.Point(250, 190);
            this.labSelect.Name = "labSelect";
            this.labSelect.Size = new System.Drawing.Size(0, 12);
            this.labSelect.TabIndex = 14;
            this.labSelect.Visible = false;
            // 
            // frmDataModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 593);
            this.Controls.Add(this.labSelect);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.butQuery);
            this.Controls.Add(this.labText);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDataModify";
            this.Text = "数据修改";
            this.Load += new System.EventHandler(this.frmDataModify_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lsbTable;
        private System.Windows.Forms.ComboBox cbbDataBase;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lsbColumn;
        private System.Windows.Forms.RadioButton rdbPart;
        private System.Windows.Forms.RadioButton rdbAll;
        private System.Windows.Forms.Button btnSelectCol;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label labText;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button butQuery;
        private System.Windows.Forms.Label labIDName;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label labSelect;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cbbGuanXi;
        private System.Windows.Forms.ComboBox cbbColumn;
        private System.Windows.Forms.ComboBox cbbAndOr;
        private System.Windows.Forms.ComboBox cbbAndOr3;
        private System.Windows.Forms.TextBox txtValue3;
        private System.Windows.Forms.ComboBox cbbGuanX3;
        private System.Windows.Forms.ComboBox cbbColumn3;
        private System.Windows.Forms.ComboBox cbbAndOr2;
        private System.Windows.Forms.TextBox txtValue2;
        private System.Windows.Forms.ComboBox cbbGuanX2;
        private System.Windows.Forms.ComboBox cbbColumn2;
        private System.Windows.Forms.ComboBox cbbAndOr1;
        private System.Windows.Forms.TextBox txtValue1;
        private System.Windows.Forms.ComboBox cbbGuanX1;
        private System.Windows.Forms.ComboBox cbbColumn1;
        private System.Windows.Forms.Label labWhere;
        private System.Windows.Forms.CheckBox ckbWhere;
    }
}
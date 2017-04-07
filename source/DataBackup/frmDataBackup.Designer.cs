namespace DataBackup
{
    partial class frmDataBackup
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectClean = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.lsbTable = new System.Windows.Forms.ListBox();
            this.cbbDataBase = new System.Windows.Forms.ComboBox();
            this.labText = new System.Windows.Forms.Label();
            this.rdbQueryAll = new System.Windows.Forms.RadioButton();
            this.rdbQueryCol = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbEYear = new System.Windows.Forms.ComboBox();
            this.rdbTime2 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbEMounth = new System.Windows.Forms.ComboBox();
            this.rdbTime1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbFMounth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbFYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lsbInfo = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnSelectClean);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.lsbTable);
            this.groupBox1.Controls.Add(this.cbbDataBase);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 538);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库表名";
            // 
            // btnSelectClean
            // 
            this.btnSelectClean.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSelectClean.Location = new System.Drawing.Point(122, 505);
            this.btnSelectClean.Name = "btnSelectClean";
            this.btnSelectClean.Size = new System.Drawing.Size(75, 23);
            this.btnSelectClean.TabIndex = 3;
            this.btnSelectClean.Text = "全   清";
            this.btnSelectClean.UseVisualStyleBackColor = true;
            this.btnSelectClean.Click += new System.EventHandler(this.btnSelectClean_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSelectAll.Location = new System.Drawing.Point(23, 505);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "全  选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // lsbTable
            // 
            this.lsbTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbTable.FormattingEnabled = true;
            this.lsbTable.ItemHeight = 12;
            this.lsbTable.Location = new System.Drawing.Point(7, 47);
            this.lsbTable.Name = "lsbTable";
            this.lsbTable.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lsbTable.Size = new System.Drawing.Size(207, 448);
            this.lsbTable.TabIndex = 1;
            // 
            // cbbDataBase
            // 
            this.cbbDataBase.FormattingEnabled = true;
            this.cbbDataBase.Location = new System.Drawing.Point(8, 17);
            this.cbbDataBase.Name = "cbbDataBase";
            this.cbbDataBase.Size = new System.Drawing.Size(205, 20);
            this.cbbDataBase.TabIndex = 0;
            this.cbbDataBase.SelectedIndexChanged += new System.EventHandler(this.cbbDataBase_SelectedIndexChanged);
            // 
            // labText
            // 
            this.labText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labText.AutoSize = true;
            this.labText.Location = new System.Drawing.Point(13, 535);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(0, 12);
            this.labText.TabIndex = 5;
            // 
            // rdbQueryAll
            // 
            this.rdbQueryAll.AutoSize = true;
            this.rdbQueryAll.Checked = true;
            this.rdbQueryAll.Location = new System.Drawing.Point(262, 32);
            this.rdbQueryAll.Name = "rdbQueryAll";
            this.rdbQueryAll.Size = new System.Drawing.Size(71, 16);
            this.rdbQueryAll.TabIndex = 6;
            this.rdbQueryAll.TabStop = true;
            this.rdbQueryAll.Text = "全部数据";
            this.rdbQueryAll.UseVisualStyleBackColor = true;
            // 
            // rdbQueryCol
            // 
            this.rdbQueryCol.AutoSize = true;
            this.rdbQueryCol.Location = new System.Drawing.Point(262, 58);
            this.rdbQueryCol.Name = "rdbQueryCol";
            this.rdbQueryCol.Size = new System.Drawing.Size(71, 16);
            this.rdbQueryCol.TabIndex = 7;
            this.rdbQueryCol.Text = "按查询列";
            this.rdbQueryCol.UseVisualStyleBackColor = true;
            this.rdbQueryCol.CheckedChanged += new System.EventHandler(this.rdbQueryCol_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(181, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "年";
            // 
            // cbbEYear
            // 
            this.cbbEYear.FormattingEnabled = true;
            this.cbbEYear.Items.AddRange(new object[] {
            "2005",
            "2006",
            "2007",
            "2008",
            "2009",
            "2010",
            "2011",
            "2012",
            "2013",
            "2014"});
            this.cbbEYear.Location = new System.Drawing.Point(102, 47);
            this.cbbEYear.Name = "cbbEYear";
            this.cbbEYear.Size = new System.Drawing.Size(73, 20);
            this.cbbEYear.TabIndex = 13;
            // 
            // rdbTime2
            // 
            this.rdbTime2.AutoSize = true;
            this.rdbTime2.Location = new System.Drawing.Point(26, 48);
            this.rdbTime2.Name = "rdbTime2";
            this.rdbTime2.Size = new System.Drawing.Size(59, 16);
            this.rdbTime2.TabIndex = 7;
            this.rdbTime2.Text = "时间段";
            this.rdbTime2.UseVisualStyleBackColor = true;
            this.rdbTime2.CheckedChanged += new System.EventHandler(this.rdbTime2_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cbbEMounth);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cbbEYear);
            this.groupBox3.Controls.Add(this.rdbTime2);
            this.groupBox3.Controls.Add(this.rdbTime1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cbbFMounth);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cbbFYear);
            this.groupBox3.Location = new System.Drawing.Point(344, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(391, 75);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(287, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 52);
            this.label6.TabIndex = 9;
            this.label6.Text = "如果按查询列备份数据，没有定义查询列时，将备份所有数据！";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "月";
            // 
            // cbbEMounth
            // 
            this.cbbEMounth.FormattingEnabled = true;
            this.cbbEMounth.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cbbEMounth.Location = new System.Drawing.Point(204, 47);
            this.cbbEMounth.Name = "cbbEMounth";
            this.cbbEMounth.Size = new System.Drawing.Size(54, 20);
            this.cbbEMounth.TabIndex = 15;
            // 
            // rdbTime1
            // 
            this.rdbTime1.AutoSize = true;
            this.rdbTime1.Checked = true;
            this.rdbTime1.Location = new System.Drawing.Point(26, 17);
            this.rdbTime1.Name = "rdbTime1";
            this.rdbTime1.Size = new System.Drawing.Size(47, 16);
            this.rdbTime1.TabIndex = 12;
            this.rdbTime1.TabStop = true;
            this.rdbTime1.Text = "时间";
            this.rdbTime1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "月";
            // 
            // cbbFMounth
            // 
            this.cbbFMounth.FormattingEnabled = true;
            this.cbbFMounth.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cbbFMounth.Location = new System.Drawing.Point(204, 17);
            this.cbbFMounth.Name = "cbbFMounth";
            this.cbbFMounth.Size = new System.Drawing.Size(54, 20);
            this.cbbFMounth.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "年";
            // 
            // cbbFYear
            // 
            this.cbbFYear.FormattingEnabled = true;
            this.cbbFYear.Items.AddRange(new object[] {
            "2005",
            "2006",
            "2007",
            "2008",
            "2009",
            "2010",
            "2011",
            "2012",
            "2013",
            "2014"});
            this.cbbFYear.Location = new System.Drawing.Point(102, 17);
            this.cbbFYear.Name = "cbbFYear";
            this.cbbFYear.Size = new System.Drawing.Size(73, 20);
            this.cbbFYear.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(240, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "保存位置：";
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(309, 100);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(316, 21);
            this.txtFile.TabIndex = 10;
            // 
            // btnPath
            // 
            this.btnPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPath.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPath.Location = new System.Drawing.Point(631, 99);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(27, 23);
            this.btnPath.TabIndex = 11;
            this.btnPath.Text = "..";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreate.Location = new System.Drawing.Point(665, 98);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(68, 23);
            this.btnCreate.TabIndex = 12;
            this.btnCreate.Text = "开始生成";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lsbInfo
            // 
            this.lsbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbInfo.FormattingEnabled = true;
            this.lsbInfo.ItemHeight = 12;
            this.lsbInfo.Location = new System.Drawing.Point(240, 130);
            this.lsbInfo.Name = "lsbInfo";
            this.lsbInfo.Size = new System.Drawing.Size(497, 412);
            this.lsbInfo.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(238, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "label7";
            this.label7.Visible = false;
            // 
            // frmDataBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 562);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lsbInfo);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.rdbQueryCol);
            this.Controls.Add(this.rdbQueryAll);
            this.Controls.Add(this.labText);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDataBackup";
            this.Text = "数据备份窗口";
            this.Load += new System.EventHandler(this.frmDataBackup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lsbTable;
        private System.Windows.Forms.ComboBox cbbDataBase;
        private System.Windows.Forms.Label labText;
        private System.Windows.Forms.Button btnSelectClean;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.RadioButton rdbQueryAll;
        private System.Windows.Forms.RadioButton rdbQueryCol;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbEYear;
        private System.Windows.Forms.RadioButton rdbTime2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbEMounth;
        private System.Windows.Forms.RadioButton rdbTime1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbFMounth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbFYear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ListBox lsbInfo;
        private System.Windows.Forms.Label label7;
    }
}
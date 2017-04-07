namespace DataBackup
{
    partial class frmDataIn
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
            this.btnPath = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnSelectClean = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.lsbTable = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExeIn = new System.Windows.Forms.Button();
            this.lsbInfo = new System.Windows.Forms.ListBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.txtSql = new System.Windows.Forms.TextBox();
            this.labText = new System.Windows.Forms.Label();
            this.btnShowData = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPath
            // 
            this.btnPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPath.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPath.Location = new System.Drawing.Point(79, 43);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(75, 23);
            this.btnPath.TabIndex = 14;
            this.btnPath.Text = "选择路径";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(79, 16);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(217, 21);
            this.txtFile.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "文件路径：";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(221, 43);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 15;
            this.btnRead.Text = "读取文件";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnSelectClean
            // 
            this.btnSelectClean.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSelectClean.Location = new System.Drawing.Point(221, 540);
            this.btnSelectClean.Name = "btnSelectClean";
            this.btnSelectClean.Size = new System.Drawing.Size(75, 23);
            this.btnSelectClean.TabIndex = 18;
            this.btnSelectClean.Text = "全   清";
            this.btnSelectClean.UseVisualStyleBackColor = true;
            this.btnSelectClean.Click += new System.EventHandler(this.btnSelectClean_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSelectAll.Location = new System.Drawing.Point(79, 540);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 17;
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
            this.lsbTable.Location = new System.Drawing.Point(11, 72);
            this.lsbTable.Name = "lsbTable";
            this.lsbTable.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lsbTable.Size = new System.Drawing.Size(285, 460);
            this.lsbTable.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lsbTable);
            this.groupBox1.Controls.Add(this.btnSelectClean);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.txtFile);
            this.groupBox1.Controls.Add(this.btnPath);
            this.groupBox1.Controls.Add(this.btnRead);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 569);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件名";
            // 
            // btnExeIn
            // 
            this.btnExeIn.Location = new System.Drawing.Point(534, 20);
            this.btnExeIn.Name = "btnExeIn";
            this.btnExeIn.Size = new System.Drawing.Size(75, 23);
            this.btnExeIn.TabIndex = 20;
            this.btnExeIn.Text = "导入数据";
            this.btnExeIn.UseVisualStyleBackColor = true;
            this.btnExeIn.Click += new System.EventHandler(this.btnExeIn_Click);
            // 
            // lsbInfo
            // 
            this.lsbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbInfo.FormattingEnabled = true;
            this.lsbInfo.HorizontalScrollbar = true;
            this.lsbInfo.ItemHeight = 12;
            this.lsbInfo.Location = new System.Drawing.Point(340, 61);
            this.lsbInfo.Name = "lsbInfo";
            this.lsbInfo.Size = new System.Drawing.Size(564, 520);
            this.lsbInfo.TabIndex = 21;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(340, 20);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 22;
            this.btnShow.Text = "显示内容";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // txtSql
            // 
            this.txtSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSql.Location = new System.Drawing.Point(340, 61);
            this.txtSql.Multiline = true;
            this.txtSql.Name = "txtSql";
            this.txtSql.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSql.Size = new System.Drawing.Size(564, 520);
            this.txtSql.TabIndex = 23;
            this.txtSql.Visible = false;
            this.txtSql.WordWrap = false;
            // 
            // labText
            // 
            this.labText.AutoSize = true;
            this.labText.Location = new System.Drawing.Point(640, 31);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(0, 12);
            this.labText.TabIndex = 24;
            // 
            // btnShowData
            // 
            this.btnShowData.Location = new System.Drawing.Point(437, 20);
            this.btnShowData.Name = "btnShowData";
            this.btnShowData.Size = new System.Drawing.Size(75, 23);
            this.btnShowData.TabIndex = 25;
            this.btnShowData.Text = "显示数据";
            this.btnShowData.UseVisualStyleBackColor = true;
            this.btnShowData.Click += new System.EventHandler(this.btnShowData_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(340, 61);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(564, 520);
            this.dgvData.TabIndex = 26;
            // 
            // frmDataIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 593);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnShowData);
            this.Controls.Add(this.labText);
            this.Controls.Add(this.txtSql);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.lsbInfo);
            this.Controls.Add(this.btnExeIn);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDataIn";
            this.Text = "数据导入";
            this.Load += new System.EventHandler(this.frmDataIn_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnSelectClean;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.ListBox lsbTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExeIn;
        private System.Windows.Forms.ListBox lsbInfo;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.TextBox txtSql;
        private System.Windows.Forms.Label labText;
        private System.Windows.Forms.Button btnShowData;
        private System.Windows.Forms.DataGridView dgvData;
    }
}
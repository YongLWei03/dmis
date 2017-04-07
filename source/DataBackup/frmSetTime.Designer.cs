namespace DataBackup
{
    partial class frmSetTime
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
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.txtIn = new System.Windows.Forms.TextBox();
            this.cbbOut = new System.Windows.Forms.ComboBox();
            this.cbbIn = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExe = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPath = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.lsbInfo = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUsa = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtP = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 569);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库表名";
            // 
            // btnSelectClean
            // 
            this.btnSelectClean.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSelectClean.Location = new System.Drawing.Point(135, 540);
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
            this.btnSelectAll.Location = new System.Drawing.Point(6, 540);
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
            this.lsbTable.Location = new System.Drawing.Point(3, 47);
            this.lsbTable.Name = "lsbTable";
            this.lsbTable.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lsbTable.Size = new System.Drawing.Size(207, 484);
            this.lsbTable.TabIndex = 1;
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
            this.label7.Location = new System.Drawing.Point(181, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "label7";
            this.label7.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "要备份的数据库：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "要恢复的数据库：";
            // 
            // txtOut
            // 
            this.txtOut.Location = new System.Drawing.Point(355, 9);
            this.txtOut.Name = "txtOut";
            this.txtOut.Size = new System.Drawing.Size(100, 21);
            this.txtOut.TabIndex = 8;
            // 
            // txtIn
            // 
            this.txtIn.Location = new System.Drawing.Point(355, 51);
            this.txtIn.Name = "txtIn";
            this.txtIn.Size = new System.Drawing.Size(100, 21);
            this.txtIn.TabIndex = 9;
            // 
            // cbbOut
            // 
            this.cbbOut.FormattingEnabled = true;
            this.cbbOut.Items.AddRange(new object[] {
            "日",
            "月"});
            this.cbbOut.Location = new System.Drawing.Point(461, 10);
            this.cbbOut.Name = "cbbOut";
            this.cbbOut.Size = new System.Drawing.Size(53, 20);
            this.cbbOut.TabIndex = 10;
            // 
            // cbbIn
            // 
            this.cbbIn.FormattingEnabled = true;
            this.cbbIn.Items.AddRange(new object[] {
            "月",
            "日"});
            this.cbbIn.Location = new System.Drawing.Point(461, 52);
            this.cbbIn.Name = "cbbIn";
            this.cbbIn.Size = new System.Drawing.Size(53, 20);
            this.cbbIn.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(526, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(179, 77);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "说明";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "月数据只在每月的一号执行！";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "月：则备份前一月的数据；";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "日：则备份前一日的数据；";
            // 
            // btnExe
            // 
            this.btnExe.Location = new System.Drawing.Point(630, 132);
            this.btnExe.Name = "btnExe";
            this.btnExe.Size = new System.Drawing.Size(75, 23);
            this.btnExe.TabIndex = 14;
            this.btnExe.Text = "生成配置文件";
            this.btnExe.UseVisualStyleBackColor = true;
            this.btnExe.Click += new System.EventHandler(this.btnExe_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "文件存放路径：";
            // 
            // btnPath
            // 
            this.btnPath.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPath.Location = new System.Drawing.Point(678, 92);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(27, 23);
            this.btnPath.TabIndex = 17;
            this.btnPath.Text = "..";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(355, 94);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(317, 21);
            this.txtFile.TabIndex = 16;
            // 
            // lsbInfo
            // 
            this.lsbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbInfo.FormattingEnabled = true;
            this.lsbInfo.ItemHeight = 12;
            this.lsbInfo.Location = new System.Drawing.Point(250, 166);
            this.lsbInfo.Name = "lsbInfo";
            this.lsbInfo.Size = new System.Drawing.Size(462, 412);
            this.lsbInfo.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(248, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "数据库用户名:";
            // 
            // txtUsa
            // 
            this.txtUsa.Location = new System.Drawing.Point(355, 134);
            this.txtUsa.Name = "txtUsa";
            this.txtUsa.Size = new System.Drawing.Size(60, 21);
            this.txtUsa.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(459, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "密码:";
            // 
            // txtP
            // 
            this.txtP.Location = new System.Drawing.Point(500, 134);
            this.txtP.Name = "txtP";
            this.txtP.Size = new System.Drawing.Size(60, 21);
            this.txtP.TabIndex = 22;
            // 
            // frmSetTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 593);
            this.Controls.Add(this.txtP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtUsa);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lsbInfo);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnExe);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbbIn);
            this.Controls.Add(this.cbbOut);
            this.Controls.Add(this.txtIn);
            this.Controls.Add(this.txtOut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSetTime";
            this.Text = "设置定时备份参数";
            this.Load += new System.EventHandler(this.frmSetTime_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectClean;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.ListBox lsbTable;
        private System.Windows.Forms.ComboBox cbbDataBase;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.TextBox txtIn;
        private System.Windows.Forms.ComboBox cbbOut;
        private System.Windows.Forms.ComboBox cbbIn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.ListBox lsbInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUsa;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtP;

    }
}
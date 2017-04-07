namespace PlatForm
{
    partial class frmLogSearch
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
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lsvLog = new System.Windows.Forms.ListView();
            this.OPT_TIME = new System.Windows.Forms.ColumnHeader();
            this.MEMBER_NAME = new System.Windows.Forms.ColumnHeader();
            this.MEMBER_ID = new System.Windows.Forms.ColumnHeader();
            this.IP = new System.Windows.Forms.ColumnHeader();
            this.LOG_TYPE = new System.Windows.Forms.ColumnHeader();
            this.STATE = new System.Windows.Forms.ColumnHeader();
            this.CONTENT = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(83, 19);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(116, 21);
            this.dtpStartDate.TabIndex = 0;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(288, 19);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(116, 21);
            this.dtpEndDate.TabIndex = 1;
            // 
            // lsvLog
            // 
            this.lsvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.OPT_TIME,
            this.MEMBER_NAME,
            this.MEMBER_ID,
            this.IP,
            this.LOG_TYPE,
            this.STATE,
            this.CONTENT});
            this.lsvLog.FullRowSelect = true;
            this.lsvLog.Location = new System.Drawing.Point(8, 62);
            this.lsvLog.Name = "lsvLog";
            this.lsvLog.Size = new System.Drawing.Size(699, 364);
            this.lsvLog.TabIndex = 2;
            this.lsvLog.UseCompatibleStateImageBehavior = false;
            this.lsvLog.View = System.Windows.Forms.View.Details;
            // 
            // OPT_TIME
            // 
            this.OPT_TIME.Text = "时间";
            this.OPT_TIME.Width = 100;
            // 
            // MEMBER_NAME
            // 
            this.MEMBER_NAME.Text = "操作人";
            this.MEMBER_NAME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MEMBER_ID
            // 
            this.MEMBER_ID.Text = "操作人ID";
            this.MEMBER_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IP
            // 
            this.IP.Text = "IP地址";
            this.IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP.Width = 100;
            // 
            // LOG_TYPE
            // 
            this.LOG_TYPE.Text = "类型";
            this.LOG_TYPE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // STATE
            // 
            this.STATE.Text = "状态";
            this.STATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CONTENT
            // 
            this.CONTENT.Text = "日志内容";
            this.CONTENT.Width = 300;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "开始日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "终止日期";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(699, 51);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(418, 18);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // frmLogSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 438);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lsvLog);
            this.Name = "frmLogSearch";
            this.Text = "日志查询";
            this.Load += new System.EventHandler(this.frmLogSearch_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ListView lsvLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ColumnHeader OPT_TIME;
        private System.Windows.Forms.ColumnHeader MEMBER_NAME;
        private System.Windows.Forms.ColumnHeader MEMBER_ID;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader LOG_TYPE;
        private System.Windows.Forms.ColumnHeader STATE;
        private System.Windows.Forms.ColumnHeader CONTENT;
    }
}
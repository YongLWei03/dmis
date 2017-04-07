namespace PlatForm
{
    partial class frmSelectRelatingTableColumn
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbbDescColumn = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbValueColumn = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbQueryColumn = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbTable = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(186, 195);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(51, 195);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbbDescColumn
            // 
            this.cbbDescColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDescColumn.FormattingEnabled = true;
            this.cbbDescColumn.Location = new System.Drawing.Point(83, 59);
            this.cbbDescColumn.MaxDropDownItems = 20;
            this.cbbDescColumn.Name = "cbbDescColumn";
            this.cbbDescColumn.Size = new System.Drawing.Size(197, 20);
            this.cbbDescColumn.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "描述列";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "数据库表名";
            // 
            // cbbValueColumn
            // 
            this.cbbValueColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbValueColumn.FormattingEnabled = true;
            this.cbbValueColumn.Location = new System.Drawing.Point(83, 101);
            this.cbbValueColumn.MaxDropDownItems = 20;
            this.cbbValueColumn.Name = "cbbValueColumn";
            this.cbbValueColumn.Size = new System.Drawing.Size(197, 20);
            this.cbbValueColumn.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "数据列";
            // 
            // cbbQueryColumn
            // 
            this.cbbQueryColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbQueryColumn.FormattingEnabled = true;
            this.cbbQueryColumn.Location = new System.Drawing.Point(83, 144);
            this.cbbQueryColumn.MaxDropDownItems = 20;
            this.cbbQueryColumn.Name = "cbbQueryColumn";
            this.cbbQueryColumn.Size = new System.Drawing.Size(197, 20);
            this.cbbQueryColumn.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "检索列";
            // 
            // cbbTable
            // 
            this.cbbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTable.FormattingEnabled = true;
            this.cbbTable.Location = new System.Drawing.Point(83, 21);
            this.cbbTable.MaxDropDownItems = 20;
            this.cbbTable.Name = "cbbTable";
            this.cbbTable.Size = new System.Drawing.Size(197, 20);
            this.cbbTable.TabIndex = 16;
            this.cbbTable.SelectedIndexChanged += new System.EventHandler(this.cbbTable_SelectedIndexChanged);
            // 
            // frmSelectRelatingTableColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 247);
            this.Controls.Add(this.cbbTable);
            this.Controls.Add(this.cbbQueryColumn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbbValueColumn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbbDescColumn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectRelatingTableColumn";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关联数据选择";
            this.Load += new System.EventHandler(this.frmSelectRelatingTableColumn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbbDescColumn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbValueColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbQueryColumn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbTable;
    }
}
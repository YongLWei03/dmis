namespace PlatForm.WorkFlow
{
    partial class frmdoctype
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmdoctype));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cbbType = new System.Windows.Forms.ComboBox();
            this.txtForm = new System.Windows.Forms.TextBox();
            this.btnForm = new System.Windows.Forms.Button();
            this.btnIcon = new System.Windows.Forms.Button();
            this.txtIcon = new System.Windows.Forms.TextBox();
            this.btnOpenIcon = new System.Windows.Forms.Button();
            this.txtOpenIcon = new System.Windows.Forms.TextBox();
            this.cbbTable = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.open1 = new System.Windows.Forms.OpenFileDialog();
            this.cbAutoCreate = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.cbbReport = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // cbbType
            // 
            this.cbbType.FormattingEnabled = true;
            this.cbbType.Items.AddRange(new object[] {
            resources.GetString("cbbType.Items"),
            resources.GetString("cbbType.Items1")});
            resources.ApplyResources(this.cbbType, "cbbType");
            this.cbbType.Name = "cbbType";
            this.cbbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // txtForm
            // 
            resources.ApplyResources(this.txtForm, "txtForm");
            this.txtForm.Name = "txtForm";
            // 
            // btnForm
            // 
            resources.ApplyResources(this.btnForm, "btnForm");
            this.btnForm.Name = "btnForm";
            this.btnForm.UseVisualStyleBackColor = true;
            this.btnForm.Click += new System.EventHandler(this.btnForm_Click);
            // 
            // btnIcon
            // 
            resources.ApplyResources(this.btnIcon, "btnIcon");
            this.btnIcon.Name = "btnIcon";
            this.btnIcon.UseVisualStyleBackColor = true;
            this.btnIcon.Click += new System.EventHandler(this.btnIcon_Click);
            // 
            // txtIcon
            // 
            resources.ApplyResources(this.txtIcon, "txtIcon");
            this.txtIcon.Name = "txtIcon";
            // 
            // btnOpenIcon
            // 
            resources.ApplyResources(this.btnOpenIcon, "btnOpenIcon");
            this.btnOpenIcon.Name = "btnOpenIcon";
            this.btnOpenIcon.UseVisualStyleBackColor = true;
            this.btnOpenIcon.Click += new System.EventHandler(this.btnOpenIcon_Click);
            // 
            // txtOpenIcon
            // 
            resources.ApplyResources(this.txtOpenIcon, "txtOpenIcon");
            this.txtOpenIcon.Name = "txtOpenIcon";
            // 
            // cbbTable
            // 
            this.cbbTable.FormattingEnabled = true;
            resources.ApplyResources(this.cbbTable, "cbbTable");
            this.cbbTable.Name = "cbbTable";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // cbAutoCreate
            // 
            resources.ApplyResources(this.cbAutoCreate, "cbAutoCreate");
            this.cbAutoCreate.BackColor = System.Drawing.SystemColors.Control;
            this.cbAutoCreate.ForeColor = System.Drawing.Color.Blue;
            this.cbAutoCreate.Name = "cbAutoCreate";
            this.cbAutoCreate.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.txtOTHER_LANGUAGE_DESCR, "txtOTHER_LANGUAGE_DESCR");
            this.txtOTHER_LANGUAGE_DESCR.Name = "txtOTHER_LANGUAGE_DESCR";
            // 
            // cbbReport
            // 
            this.cbbReport.FormattingEnabled = true;
            resources.ApplyResources(this.cbbReport, "cbbReport");
            this.cbbReport.Name = "cbbReport";
            // 
            // frmdoctype
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbbReport);
            this.Controls.Add(this.txtOTHER_LANGUAGE_DESCR);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbAutoCreate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbbTable);
            this.Controls.Add(this.btnOpenIcon);
            this.Controls.Add(this.txtOpenIcon);
            this.Controls.Add(this.btnIcon);
            this.Controls.Add(this.txtIcon);
            this.Controls.Add(this.btnForm);
            this.Controls.Add(this.cbbType);
            this.Controls.Add(this.txtForm);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmdoctype";
            this.Load += new System.EventHandler(this.frmdoctype_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cbbType;
        private System.Windows.Forms.TextBox txtForm;
        private System.Windows.Forms.Button btnForm;
        private System.Windows.Forms.Button btnIcon;
        private System.Windows.Forms.TextBox txtIcon;
        private System.Windows.Forms.Button btnOpenIcon;
        private System.Windows.Forms.TextBox txtOpenIcon;
        private System.Windows.Forms.ComboBox cbbTable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.OpenFileDialog open1;
        private System.Windows.Forms.CheckBox cbAutoCreate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.ComboBox cbbReport;
    }
}
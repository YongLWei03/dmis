namespace PlatForm.WorkFlow
{
    partial class frmpacktype
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmpacktype));
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.cbflow = new System.Windows.Forms.CheckBox();
            this.cbcheck = new System.Windows.Forms.CheckBox();
            this.cbarchive = new System.Windows.Forms.CheckBox();
            this.btSave = new System.Windows.Forms.Button();
            this.txtOTHER_LANGUAGE_DESCR = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tbName
            // 
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
            // 
            // cbflow
            // 
            resources.ApplyResources(this.cbflow, "cbflow");
            this.cbflow.Checked = true;
            this.cbflow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbflow.Name = "cbflow";
            this.cbflow.UseVisualStyleBackColor = true;
            this.cbflow.CheckedChanged += new System.EventHandler(this.cbflow_CheckedChanged);
            // 
            // cbcheck
            // 
            resources.ApplyResources(this.cbcheck, "cbcheck");
            this.cbcheck.Name = "cbcheck";
            this.cbcheck.UseVisualStyleBackColor = true;
            // 
            // cbarchive
            // 
            resources.ApplyResources(this.cbarchive, "cbarchive");
            this.cbarchive.Name = "cbarchive";
            this.cbarchive.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            resources.ApplyResources(this.btSave, "btSave");
            this.btSave.Name = "btSave";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // txtOTHER_LANGUAGE_DESCR
            // 
            resources.ApplyResources(this.txtOTHER_LANGUAGE_DESCR, "txtOTHER_LANGUAGE_DESCR");
            this.txtOTHER_LANGUAGE_DESCR.Name = "txtOTHER_LANGUAGE_DESCR";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // frmpacktype
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtOTHER_LANGUAGE_DESCR);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.cbarchive);
            this.Controls.Add(this.cbcheck);
            this.Controls.Add(this.cbflow);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmpacktype";
            this.Load += new System.EventHandler(this.frmpacktype_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.CheckBox cbflow;
        private System.Windows.Forms.CheckBox cbcheck;
        private System.Windows.Forms.CheckBox cbarchive;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox txtOTHER_LANGUAGE_DESCR;
        private System.Windows.Forms.Label label2;
    }
}
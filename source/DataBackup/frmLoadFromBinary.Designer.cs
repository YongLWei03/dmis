namespace DataBackup
{
    partial class frmLoadFromBinary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadFromBinary));
            this.btnImp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImp
            // 
            this.btnImp.AccessibleDescription = null;
            this.btnImp.AccessibleName = null;
            resources.ApplyResources(this.btnImp, "btnImp");
            this.btnImp.BackgroundImage = null;
            this.btnImp.Font = null;
            this.btnImp.Name = "btnImp";
            this.btnImp.UseVisualStyleBackColor = true;
            this.btnImp.Click += new System.EventHandler(this.btnImp_Click);
            // 
            // frmLoadFromBinary
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.btnImp);
            this.Font = null;
            this.Icon = null;
            this.Name = "frmLoadFromBinary";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImp;
    }
}
namespace PlatForm.DmisReport
{
    partial class frmFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFilter));
            this.lsvFilter = new System.Windows.Forms.ListView();
            this.xh = new System.Windows.Forms.ColumnHeader();
            this.column = new System.Windows.Forms.ColumnHeader();
            this.op = new System.Windows.Forms.ColumnHeader();
            this.value = new System.Windows.Forms.ColumnHeader();
            this.logical = new System.Windows.Forms.ColumnHeader();
            this.cbbColumn = new System.Windows.Forms.ComboBox();
            this.列名 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbOP = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbValue = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbLogical = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsvFilter
            // 
            this.lsvFilter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xh,
            this.column,
            this.op,
            this.value,
            this.logical});
            this.lsvFilter.FullRowSelect = true;
            resources.ApplyResources(this.lsvFilter, "lsvFilter");
            this.lsvFilter.Name = "lsvFilter";
            this.lsvFilter.UseCompatibleStateImageBehavior = false;
            this.lsvFilter.View = System.Windows.Forms.View.Details;
            this.lsvFilter.SelectedIndexChanged += new System.EventHandler(this.lsvFilter_SelectedIndexChanged);
            // 
            // xh
            // 
            resources.ApplyResources(this.xh, "xh");
            // 
            // column
            // 
            resources.ApplyResources(this.column, "column");
            // 
            // op
            // 
            resources.ApplyResources(this.op, "op");
            // 
            // value
            // 
            resources.ApplyResources(this.value, "value");
            // 
            // logical
            // 
            resources.ApplyResources(this.logical, "logical");
            // 
            // cbbColumn
            // 
            this.cbbColumn.FormattingEnabled = true;
            resources.ApplyResources(this.cbbColumn, "cbbColumn");
            this.cbbColumn.Name = "cbbColumn";
            // 
            // 列名
            // 
            resources.ApplyResources(this.列名, "列名");
            this.列名.Name = "列名";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbbOP
            // 
            this.cbbOP.FormattingEnabled = true;
            this.cbbOP.Items.AddRange(new object[] {
            resources.GetString("cbbOP.Items"),
            resources.GetString("cbbOP.Items1"),
            resources.GetString("cbbOP.Items2"),
            resources.GetString("cbbOP.Items3"),
            resources.GetString("cbbOP.Items4"),
            resources.GetString("cbbOP.Items5"),
            resources.GetString("cbbOP.Items6"),
            resources.GetString("cbbOP.Items7")});
            resources.ApplyResources(this.cbbOP, "cbbOP");
            this.cbbOP.Name = "cbbOP";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbbValue
            // 
            this.cbbValue.FormattingEnabled = true;
            resources.ApplyResources(this.cbbValue, "cbbValue");
            this.cbbValue.Name = "cbbValue";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cbbLogical
            // 
            this.cbbLogical.FormattingEnabled = true;
            this.cbbLogical.Items.AddRange(new object[] {
            resources.GetString("cbbLogical.Items"),
            resources.GetString("cbbLogical.Items1")});
            resources.ApplyResources(this.cbbLogical, "cbbLogical");
            this.cbbLogical.Name = "cbbLogical";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmFilter
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbLogical);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbOP);
            this.Controls.Add(this.列名);
            this.Controls.Add(this.cbbColumn);
            this.Controls.Add(this.lsvFilter);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFilter";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmFilter_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvFilter;
        private System.Windows.Forms.ComboBox cbbColumn;
        private System.Windows.Forms.Label 列名;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbOP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbLogical;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ColumnHeader xh;
        private System.Windows.Forms.ColumnHeader column;
        private System.Windows.Forms.ColumnHeader value;
        private System.Windows.Forms.ColumnHeader logical;
        private System.Windows.Forms.ColumnHeader op;
    }
}
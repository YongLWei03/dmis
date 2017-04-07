namespace PlatForm.WorkFlow
{
    partial class frmRestDateSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRestDateSet));
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTID = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.lsvRestDay = new System.Windows.Forms.ListView();
            this.RES_DATE = new System.Windows.Forms.ColumnHeader();
            this.RES_WEEKNAME = new System.Windows.Forms.ColumnHeader();
            this.IS_HOLIDAY = new System.Windows.Forms.ColumnHeader();
            this.NOTE = new System.Windows.Forms.ColumnHeader();
            this.TID = new System.Windows.Forms.ColumnHeader();
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbSearch = new System.Windows.Forms.ToolStripButton();
            this.tlbSetRestDate = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtRES_DATE = new System.Windows.Forms.TextBox();
            this.txtNOTE = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbbIS_HOLIDAY = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tsTool.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpStartDate
            // 
            resources.ApplyResources(this.dtpStartDate, "dtpStartDate");
            this.dtpStartDate.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dtpStartDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Value = new System.DateTime(2008, 7, 31, 0, 0, 0, 0);
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
            // dtpEndDate
            // 
            resources.ApplyResources(this.dtpEndDate, "dtpEndDate");
            this.dtpEndDate.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dtpEndDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Value = new System.DateTime(2008, 7, 31, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTID);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtTID
            // 
            resources.ApplyResources(this.txtTID, "txtTID");
            this.txtTID.Name = "txtTID";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dateTimePicker2);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker1.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Value = new System.DateTime(2008, 7, 31, 0, 0, 0, 0);
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
            // dateTimePicker2
            // 
            resources.ApplyResources(this.dateTimePicker2, "dateTimePicker2");
            this.dateTimePicker2.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker2.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Value = new System.DateTime(2008, 7, 31, 0, 0, 0, 0);
            // 
            // lsvRestDay
            // 
            resources.ApplyResources(this.lsvRestDay, "lsvRestDay");
            this.lsvRestDay.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RES_DATE,
            this.RES_WEEKNAME,
            this.IS_HOLIDAY,
            this.NOTE,
            this.TID});
            this.lsvRestDay.FullRowSelect = true;
            this.lsvRestDay.GridLines = true;
            this.lsvRestDay.MultiSelect = false;
            this.lsvRestDay.Name = "lsvRestDay";
            this.lsvRestDay.UseCompatibleStateImageBehavior = false;
            this.lsvRestDay.View = System.Windows.Forms.View.Details;
            this.lsvRestDay.SelectedIndexChanged += new System.EventHandler(this.lsvRestDay_SelectedIndexChanged);
            this.lsvRestDay.DoubleClick += new System.EventHandler(this.lsvRestDay_DoubleClick);
            // 
            // RES_DATE
            // 
            resources.ApplyResources(this.RES_DATE, "RES_DATE");
            // 
            // RES_WEEKNAME
            // 
            resources.ApplyResources(this.RES_WEEKNAME, "RES_WEEKNAME");
            // 
            // IS_HOLIDAY
            // 
            resources.ApplyResources(this.IS_HOLIDAY, "IS_HOLIDAY");
            // 
            // NOTE
            // 
            resources.ApplyResources(this.NOTE, "NOTE");
            // 
            // TID
            // 
            resources.ApplyResources(this.TID, "TID");
            // 
            // tsTool
            // 
            this.tsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbSearch,
            this.tlbSetRestDate,
            this.tlbSave,
            this.toolStripSeparator1});
            resources.ApplyResources(this.tsTool, "tsTool");
            this.tsTool.Name = "tsTool";
            // 
            // tlbSearch
            // 
            this.tlbSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbSearch, "tlbSearch");
            this.tlbSearch.Name = "tlbSearch";
            this.tlbSearch.Click += new System.EventHandler(this.tlbSearch_Click);
            // 
            // tlbSetRestDate
            // 
            this.tlbSetRestDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbSetRestDate, "tlbSetRestDate");
            this.tlbSetRestDate.Name = "tlbSetRestDate";
            this.tlbSetRestDate.Click += new System.EventHandler(this.tlbSetRestDate_Click);
            // 
            // tlbSave
            // 
            this.tlbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbSave, "tlbSave");
            this.tlbSave.Name = "tlbSave";
            this.tlbSave.Click += new System.EventHandler(this.tlbSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRES_DATE);
            this.groupBox3.Controls.Add(this.txtNOTE);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.cbbIS_HOLIDAY);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // txtRES_DATE
            // 
            resources.ApplyResources(this.txtRES_DATE, "txtRES_DATE");
            this.txtRES_DATE.Name = "txtRES_DATE";
            this.txtRES_DATE.ReadOnly = true;
            // 
            // txtNOTE
            // 
            resources.ApplyResources(this.txtNOTE, "txtNOTE");
            this.txtNOTE.Name = "txtNOTE";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // cbbIS_HOLIDAY
            // 
            this.cbbIS_HOLIDAY.BackColor = System.Drawing.SystemColors.ScrollBar;
            resources.ApplyResources(this.cbbIS_HOLIDAY, "cbbIS_HOLIDAY");
            this.cbbIS_HOLIDAY.FormattingEnabled = true;
            this.cbbIS_HOLIDAY.Items.AddRange(new object[] {
            resources.GetString("cbbIS_HOLIDAY.Items"),
            resources.GetString("cbbIS_HOLIDAY.Items1")});
            this.cbbIS_HOLIDAY.Name = "cbbIS_HOLIDAY";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dateTimePicker3);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.dateTimePicker4);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // dateTimePicker3
            // 
            resources.ApplyResources(this.dateTimePicker3, "dateTimePicker3");
            this.dateTimePicker3.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker3.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Value = new System.DateTime(2008, 7, 31, 0, 0, 0, 0);
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
            // dateTimePicker4
            // 
            resources.ApplyResources(this.dateTimePicker4, "dateTimePicker4");
            this.dateTimePicker4.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker4.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Value = new System.DateTime(2008, 7, 31, 0, 0, 0, 0);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // frmRestDateSet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsTool);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lsvRestDay);
            this.Controls.Add(this.groupBox3);
            this.Name = "frmRestDateSet";
            this.Load += new System.EventHandler(this.frmRestDateSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lsvRestDay;
        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tlbSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader RES_DATE;
        private System.Windows.Forms.ColumnHeader RES_WEEKNAME;
        private System.Windows.Forms.ColumnHeader IS_HOLIDAY;
        private System.Windows.Forms.ColumnHeader TID;
        private System.Windows.Forms.ToolStripButton tlbSetRestDate;
        private System.Windows.Forms.ColumnHeader NOTE;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTID;
        private System.Windows.Forms.TextBox txtNOTE;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbbIS_HOLIDAY;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.TextBox txtRES_DATE;
    }
}
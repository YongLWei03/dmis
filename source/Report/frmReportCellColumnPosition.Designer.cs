namespace PlatForm.DmisReport
{
    partial class frmReportCellColumnPosition
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportCellColumnPosition));
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbDisplay = new System.Windows.Forms.ToolStripButton();
            this.tlbHide = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.trvReport = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trvTable = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblTableName = new System.Windows.Forms.Label();
            this.lsvColumn = new System.Windows.Forms.ListView();
            this.TID = new System.Windows.Forms.ColumnHeader();
            this.ColumnName = new System.Windows.Forms.ColumnHeader();
            this.R = new System.Windows.Forms.ColumnHeader();
            this.C = new System.Windows.Forms.ColumnHeader();
            this.P = new System.Windows.Forms.ColumnHeader();
            this.m_cell = new AxCELL50Lib.AxCell();
            this.cbbZoom = new System.Windows.Forms.ComboBox();
            this.tsTool.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cell)).BeginInit();
            this.SuspendLayout();
            // 
            // tsTool
            // 
            this.tsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbDisplay,
            this.tlbHide,
            this.toolStripSeparator1});
            resources.ApplyResources(this.tsTool, "tsTool");
            this.tsTool.Name = "tsTool";
            // 
            // tlbDisplay
            // 
            this.tlbDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbDisplay, "tlbDisplay");
            this.tlbDisplay.Name = "tlbDisplay";
            this.tlbDisplay.Tag = "显示数据库列";
            this.tlbDisplay.Click += new System.EventHandler(this.tlbDisplay_Click);
            // 
            // tlbHide
            // 
            this.tlbHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbHide, "tlbHide");
            this.tlbHide.Name = "tlbHide";
            this.tlbHide.Tag = "隐藏数据库列";
            this.tlbHide.Click += new System.EventHandler(this.tlbHide_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // trvReport
            // 
            resources.ApplyResources(this.trvReport, "trvReport");
            this.trvReport.FullRowSelect = true;
            this.trvReport.ImageList = this.imageList1;
            this.trvReport.LineColor = System.Drawing.Color.Maroon;
            this.trvReport.Name = "trvReport";
            this.trvReport.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvReport_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "top_dic.gif");
            this.imageList1.Images.SetKeyName(1, "selectChildNode.gif");
            this.imageList1.Images.SetKeyName(2, "month.gif");
            this.imageList1.Images.SetKeyName(3, "Table.gif");
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.trvTable);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // trvTable
            // 
            resources.ApplyResources(this.trvTable, "trvTable");
            this.trvTable.FullRowSelect = true;
            this.trvTable.ImageList = this.imageList1;
            this.trvTable.LineColor = System.Drawing.Color.Maroon;
            this.trvTable.Name = "trvTable";
            this.trvTable.DoubleClick += new System.EventHandler(this.trvTable_DoubleClick);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.trvReport);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblTableName);
            this.tabPage2.Controls.Add(this.lsvColumn);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblTableName
            // 
            resources.ApplyResources(this.lblTableName, "lblTableName");
            this.lblTableName.ForeColor = System.Drawing.Color.Maroon;
            this.lblTableName.Name = "lblTableName";
            // 
            // lsvColumn
            // 
            resources.ApplyResources(this.lsvColumn, "lsvColumn");
            this.lsvColumn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TID,
            this.ColumnName,
            this.R,
            this.C,
            this.P});
            this.lsvColumn.FullRowSelect = true;
            this.lsvColumn.GridLines = true;
            this.lsvColumn.MultiSelect = false;
            this.lsvColumn.Name = "lsvColumn";
            this.lsvColumn.UseCompatibleStateImageBehavior = false;
            this.lsvColumn.View = System.Windows.Forms.View.Details;
            this.lsvColumn.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvColumn_MouseDoubleClick);
            // 
            // TID
            // 
            resources.ApplyResources(this.TID, "TID");
            // 
            // ColumnName
            // 
            resources.ApplyResources(this.ColumnName, "ColumnName");
            // 
            // R
            // 
            resources.ApplyResources(this.R, "R");
            // 
            // C
            // 
            resources.ApplyResources(this.C, "C");
            // 
            // P
            // 
            resources.ApplyResources(this.P, "P");
            // 
            // m_cell
            // 
            resources.ApplyResources(this.m_cell, "m_cell");
            this.m_cell.Name = "m_cell";
            this.m_cell.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("m_cell.OcxState")));
            this.m_cell.MouseDClick += new AxCELL50Lib._DCell2000Events_MouseDClickEventHandler(this.m_cell_MouseDClick);
            // 
            // cbbZoom
            // 
            this.cbbZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbbZoom, "cbbZoom");
            this.cbbZoom.FormattingEnabled = true;
            this.cbbZoom.Items.AddRange(new object[] {
            resources.GetString("cbbZoom.Items"),
            resources.GetString("cbbZoom.Items1"),
            resources.GetString("cbbZoom.Items2"),
            resources.GetString("cbbZoom.Items3"),
            resources.GetString("cbbZoom.Items4"),
            resources.GetString("cbbZoom.Items5"),
            resources.GetString("cbbZoom.Items6")});
            this.cbbZoom.Name = "cbbZoom";
            this.cbbZoom.SelectedIndexChanged += new System.EventHandler(this.cbbZoom_SelectedIndexChanged);
            // 
            // frmReportCellColumnPosition
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbbZoom);
            this.Controls.Add(this.m_cell);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tsTool);
            this.Name = "frmReportCellColumnPosition";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmReportCellColumnPosition_Load);
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_cell)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tlbDisplay;
        private System.Windows.Forms.ToolStripButton tlbHide;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TreeView trvReport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView trvTable;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lsvColumn;
        private System.Windows.Forms.ColumnHeader TID;
        private System.Windows.Forms.ColumnHeader ColumnName;
        private System.Windows.Forms.ColumnHeader R;
        private System.Windows.Forms.ColumnHeader C;
        private System.Windows.Forms.ColumnHeader P;
        private AxCELL50Lib.AxCell m_cell;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cbbZoom;
    }
}
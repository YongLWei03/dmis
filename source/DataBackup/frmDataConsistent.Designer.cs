namespace DataBackup
{
    partial class frmDataConsistent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataConsistent));
            this.dgvMainServer1 = new System.Windows.Forms.DataGridView();
            this.dgvSlaveServer1 = new System.Windows.Forms.DataGridView();
            this.grpMain1 = new System.Windows.Forms.GroupBox();
            this.grpSlave1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblMsg1 = new System.Windows.Forms.Label();
            this.dtpStart1 = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cbbDataBase = new System.Windows.Forms.ComboBox();
            this.cbbTable = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSaveMsg = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnCancelAll = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsbMsg = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbTables = new System.Windows.Forms.CheckedListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lsvDifferTables = new System.Windows.Forms.ListView();
            this.tableName = new System.Windows.Forms.ColumnHeader();
            this.whereDiffer = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.lblTableName = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvMainStruct = new System.Windows.Forms.DataGridView();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dgvSlaveStruct = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbbDataBase3 = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainServer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSlaveServer1)).BeginInit();
            this.grpMain1.SuspendLayout();
            this.grpSlave1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainStruct)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSlaveStruct)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMainServer1
            // 
            this.dgvMainServer1.AllowUserToAddRows = false;
            this.dgvMainServer1.AllowUserToDeleteRows = false;
            this.dgvMainServer1.AllowUserToOrderColumns = true;
            resources.ApplyResources(this.dgvMainServer1, "dgvMainServer1");
            this.dgvMainServer1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMainServer1.Name = "dgvMainServer1";
            this.dgvMainServer1.RowTemplate.Height = 23;
            // 
            // dgvSlaveServer1
            // 
            resources.ApplyResources(this.dgvSlaveServer1, "dgvSlaveServer1");
            this.dgvSlaveServer1.Name = "dgvSlaveServer1";
            this.dgvSlaveServer1.RowTemplate.Height = 23;
            // 
            // grpMain1
            // 
            resources.ApplyResources(this.grpMain1, "grpMain1");
            this.grpMain1.Controls.Add(this.dgvMainServer1);
            this.grpMain1.Name = "grpMain1";
            this.grpMain1.TabStop = false;
            // 
            // grpSlave1
            // 
            resources.ApplyResources(this.grpSlave1, "grpSlave1");
            this.grpSlave1.Controls.Add(this.dgvSlaveServer1);
            this.grpSlave1.Name = "grpSlave1";
            this.grpSlave1.TabStop = false;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.grpMain1);
            this.tabPage1.Controls.Add(this.grpSlave1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.lblMsg1);
            this.groupBox4.Controls.Add(this.dtpStart1);
            this.groupBox4.Controls.Add(this.dtpEnd1);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.btnQuery);
            this.groupBox4.Controls.Add(this.cbbDataBase);
            this.groupBox4.Controls.Add(this.cbbTable);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // lblMsg1
            // 
            resources.ApplyResources(this.lblMsg1, "lblMsg1");
            this.lblMsg1.Name = "lblMsg1";
            // 
            // dtpStart1
            // 
            resources.ApplyResources(this.dtpStart1, "dtpStart1");
            this.dtpStart1.Name = "dtpStart1";
            // 
            // dtpEnd1
            // 
            resources.ApplyResources(this.dtpEnd1, "dtpEnd1");
            this.dtpEnd1.Name = "dtpEnd1";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnQuery
            // 
            resources.ApplyResources(this.btnQuery, "btnQuery");
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cbbDataBase
            // 
            resources.ApplyResources(this.cbbDataBase, "cbbDataBase");
            this.cbbDataBase.Name = "cbbDataBase";
            this.cbbDataBase.SelectedIndexChanged += new System.EventHandler(this.cbbDataBase_SelectedIndexChanged);
            // 
            // cbbTable
            // 
            resources.ApplyResources(this.cbbTable, "cbbTable");
            this.cbbTable.Name = "cbbTable";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.btnSaveMsg);
            this.groupBox3.Controls.Add(this.btnSelectAll);
            this.groupBox3.Controls.Add(this.btnCancelAll);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Controls.Add(this.dtpStart);
            this.groupBox3.Controls.Add(this.dtpEnd);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // btnSaveMsg
            // 
            resources.ApplyResources(this.btnSaveMsg, "btnSaveMsg");
            this.btnSaveMsg.Name = "btnSaveMsg";
            this.btnSaveMsg.Click += new System.EventHandler(this.btnSaveMsg_Click);
            // 
            // btnSelectAll
            // 
            resources.ApplyResources(this.btnSelectAll, "btnSelectAll");
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnCancelAll
            // 
            resources.ApplyResources(this.btnCancelAll, "btnCancelAll");
            this.btnCancelAll.Name = "btnCancelAll";
            this.btnCancelAll.Click += new System.EventHandler(this.btnCancelAll_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dtpStart
            // 
            resources.ApplyResources(this.dtpStart, "dtpStart");
            this.dtpStart.Name = "dtpStart";
            // 
            // dtpEnd
            // 
            resources.ApplyResources(this.dtpEnd, "dtpEnd");
            this.dtpEnd.Name = "dtpEnd";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.lsbMsg);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // lsbMsg
            // 
            resources.ApplyResources(this.lsbMsg, "lsbMsg");
            this.lsbMsg.Name = "lsbMsg";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.ckbTables);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ckbTables
            // 
            resources.ApplyResources(this.ckbTables, "ckbTables");
            this.ckbTables.CheckOnClick = true;
            this.ckbTables.Name = "ckbTables";
            this.ckbTables.SelectedIndexChanged += new System.EventHandler(this.ckbTables_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tabControl2);
            this.tabPage3.Controls.Add(this.groupBox5);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            // 
            // tabControl2
            // 
            resources.ApplyResources(this.tabControl2, "tabControl2");
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lsvDifferTables);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            // 
            // lsvDifferTables
            // 
            resources.ApplyResources(this.lsvDifferTables, "lsvDifferTables");
            this.lsvDifferTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tableName,
            this.whereDiffer});
            this.lsvDifferTables.FullRowSelect = true;
            this.lsvDifferTables.GridLines = true;
            this.lsvDifferTables.MultiSelect = false;
            this.lsvDifferTables.Name = "lsvDifferTables";
            this.lsvDifferTables.SmallImageList = this.imageList1;
            this.lsvDifferTables.StateImageList = this.imageList1;
            this.lsvDifferTables.UseCompatibleStateImageBehavior = false;
            this.lsvDifferTables.View = System.Windows.Forms.View.Details;
            this.lsvDifferTables.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvDifferTables_MouseDoubleClick);
            // 
            // tableName
            // 
            resources.ApplyResources(this.tableName, "tableName");
            // 
            // whereDiffer
            // 
            resources.ApplyResources(this.whereDiffer, "whereDiffer");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "infofind.gif");
            this.imageList1.Images.SetKeyName(1, "manufacture.gif");
            this.imageList1.Images.SetKeyName(2, "Table.gif");
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lblTableName);
            this.tabPage5.Controls.Add(this.groupBox6);
            this.tabPage5.Controls.Add(this.groupBox7);
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            // 
            // lblTableName
            // 
            resources.ApplyResources(this.lblTableName, "lblTableName");
            this.lblTableName.Name = "lblTableName";
            // 
            // groupBox6
            // 
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Controls.Add(this.dgvMainStruct);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // dgvMainStruct
            // 
            resources.ApplyResources(this.dgvMainStruct, "dgvMainStruct");
            this.dgvMainStruct.Name = "dgvMainStruct";
            this.dgvMainStruct.RowTemplate.Height = 23;
            // 
            // groupBox7
            // 
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Controls.Add(this.dgvSlaveStruct);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // dgvSlaveStruct
            // 
            resources.ApplyResources(this.dgvSlaveStruct, "dgvSlaveStruct");
            this.dgvSlaveStruct.Name = "dgvSlaveStruct";
            this.dgvSlaveStruct.RowTemplate.Height = 23;
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.btnSearch);
            this.groupBox5.Controls.Add(this.cbbDataBase3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbbDataBase3
            // 
            resources.ApplyResources(this.cbbDataBase3, "cbbDataBase3");
            this.cbbDataBase3.Name = "cbbDataBase3";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmDataConsistent
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "frmDataConsistent";
            this.Load += new System.EventHandler(this.frmDataConsistent_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDataConsistent_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainServer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSlaveServer1)).EndInit();
            this.grpMain1.ResumeLayout(false);
            this.grpSlave1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainStruct)).EndInit();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSlaveStruct)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMainServer1;
        private System.Windows.Forms.DataGridView dgvSlaveServer1;
        private System.Windows.Forms.GroupBox grpMain1;
        private System.Windows.Forms.GroupBox grpSlave1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbDataBase;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckedListBox ckbTables;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Button btnCancelAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSaveMsg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lsbMsg;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpStart1;
        private System.Windows.Forms.DateTimePicker dtpEnd1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblMsg1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbDataBase3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListView lsvDifferTables;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DataGridView dgvSlaveStruct;
        private System.Windows.Forms.DataGridView dgvMainStruct;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.ColumnHeader tableName;
        private System.Windows.Forms.ColumnHeader whereDiffer;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
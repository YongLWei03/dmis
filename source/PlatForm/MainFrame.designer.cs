namespace PlatForm
{
    partial class MainFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbDepart = new System.Windows.Forms.ToolStripButton();
            this.tsbMember = new System.Windows.Forms.ToolStripButton();
            this.tsbRole = new System.Windows.Forms.ToolStripButton();
            this.tsbMemberRole = new System.Windows.Forms.ToolStripButton();
            this.tsbRoleMember = new System.Windows.Forms.ToolStripButton();
            this.tsbPurview = new System.Windows.Forms.ToolStripButton();
            this.tsbRolePurview = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTable = new System.Windows.Forms.ToolStripButton();
            this.tsbColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTreeMenu = new System.Windows.Forms.ToolStripButton();
            this.tsbTreeMenuVisible = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFlow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSwitchLanguage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mRight = new System.Windows.Forms.ToolStripMenuItem();
            this.mDepart = new System.Windows.Forms.ToolStripMenuItem();
            this.mMember = new System.Windows.Forms.ToolStripMenuItem();
            this.mRole = new System.Windows.Forms.ToolStripMenuItem();
            this.mMemberRole = new System.Windows.Forms.ToolStripMenuItem();
            this.mRoleMember = new System.Windows.Forms.ToolStripMenuItem();
            this.mDepartType = new System.Windows.Forms.ToolStripMenuItem();
            this.mRolePurview = new System.Windows.Forms.ToolStripMenuItem();
            this.mPurview = new System.Windows.Forms.ToolStripMenuItem();
            this.数据库参数维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mTableType = new System.Windows.Forms.ToolStripMenuItem();
            this.mTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.wEB菜单维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mTreeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mTreeMenuVisible = new System.Windows.Forms.ToolStripMenuItem();
            this.报表设计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mReportType = new System.Windows.Forms.ToolStripMenuItem();
            this.mReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mReportColumnPos = new System.Windows.Forms.ToolStripMenuItem();
            this.mWorkFlow = new System.Windows.Forms.ToolStripMenuItem();
            this.mFlow = new System.Windows.Forms.ToolStripMenuItem();
            this.mRestDate = new System.Windows.Forms.ToolStripMenuItem();
            this.mLegalHoliday = new System.Windows.Forms.ToolStripMenuItem();
            this.mDataModify = new System.Windows.Forms.ToolStripMenuItem();
            this.mDataBackUp = new System.Windows.Forms.ToolStripMenuItem();
            this.mBackUpToSQL = new System.Windows.Forms.ToolStripMenuItem();
            this.mBackUpToBinary = new System.Windows.Forms.ToolStripMenuItem();
            this.mDataIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mLoadFromSQL = new System.Windows.Forms.ToolStripMenuItem();
            this.mLoadFromBinary = new System.Windows.Forms.ToolStripMenuItem();
            this.mDataConsistent = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.tssMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssCompany = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssDatetime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssMember = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainToolStrip.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDepart,
            this.tsbMember,
            this.tsbRole,
            this.tsbMemberRole,
            this.tsbRoleMember,
            this.tsbPurview,
            this.tsbRolePurview,
            this.toolStripSeparator1,
            this.tsbTable,
            this.tsbColumns,
            this.toolStripSeparator2,
            this.tsbTreeMenu,
            this.tsbTreeMenuVisible,
            this.toolStripSeparator3,
            this.tsbFlow,
            this.toolStripSeparator5,
            this.tsbSwitchLanguage,
            this.toolStripSeparator4,
            this.tsbExit});
            resources.ApplyResources(this.MainToolStrip, "MainToolStrip");
            this.MainToolStrip.Name = "MainToolStrip";
            // 
            // tsbDepart
            // 
            this.tsbDepart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDepart.Image = global::Main.Properties.Resources.Depart;
            resources.ApplyResources(this.tsbDepart, "tsbDepart");
            this.tsbDepart.Name = "tsbDepart";
            this.tsbDepart.Click += new System.EventHandler(this.mDepart_Click);
            // 
            // tsbMember
            // 
            this.tsbMember.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMember.Image = global::Main.Properties.Resources.Member;
            resources.ApplyResources(this.tsbMember, "tsbMember");
            this.tsbMember.Name = "tsbMember";
            this.tsbMember.Click += new System.EventHandler(this.mMember_Click);
            // 
            // tsbRole
            // 
            this.tsbRole.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRole.Image = global::Main.Properties.Resources.Role;
            resources.ApplyResources(this.tsbRole, "tsbRole");
            this.tsbRole.Name = "tsbRole";
            this.tsbRole.Click += new System.EventHandler(this.mRole_Click);
            // 
            // tsbMemberRole
            // 
            this.tsbMemberRole.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMemberRole.Image = global::Main.Properties.Resources.MemberRole;
            resources.ApplyResources(this.tsbMemberRole, "tsbMemberRole");
            this.tsbMemberRole.Name = "tsbMemberRole";
            this.tsbMemberRole.Click += new System.EventHandler(this.mMemberRole_Click);
            // 
            // tsbRoleMember
            // 
            this.tsbRoleMember.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRoleMember.Image = global::Main.Properties.Resources.RoleMember;
            resources.ApplyResources(this.tsbRoleMember, "tsbRoleMember");
            this.tsbRoleMember.Name = "tsbRoleMember";
            this.tsbRoleMember.Click += new System.EventHandler(this.mRoleMember_Click);
            // 
            // tsbPurview
            // 
            this.tsbPurview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPurview.Image = global::Main.Properties.Resources.Purview;
            resources.ApplyResources(this.tsbPurview, "tsbPurview");
            this.tsbPurview.Name = "tsbPurview";
            this.tsbPurview.Click += new System.EventHandler(this.mPurview_Click);
            // 
            // tsbRolePurview
            // 
            this.tsbRolePurview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRolePurview.Image = global::Main.Properties.Resources.RolePurview;
            resources.ApplyResources(this.tsbRolePurview, "tsbRolePurview");
            this.tsbRolePurview.Name = "tsbRolePurview";
            this.tsbRolePurview.Click += new System.EventHandler(this.mRolePurview_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsbTable
            // 
            this.tsbTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTable.Image = global::Main.Properties.Resources.Table;
            resources.ApplyResources(this.tsbTable, "tsbTable");
            this.tsbTable.Name = "tsbTable";
            this.tsbTable.Click += new System.EventHandler(this.mTable_Click);
            // 
            // tsbColumns
            // 
            this.tsbColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbColumns.Image = global::Main.Properties.Resources.Columns;
            resources.ApplyResources(this.tsbColumns, "tsbColumns");
            this.tsbColumns.Name = "tsbColumns";
            this.tsbColumns.Click += new System.EventHandler(this.mColumns_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsbTreeMenu
            // 
            this.tsbTreeMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTreeMenu.Image = global::Main.Properties.Resources.TreeMenu;
            resources.ApplyResources(this.tsbTreeMenu, "tsbTreeMenu");
            this.tsbTreeMenu.Name = "tsbTreeMenu";
            this.tsbTreeMenu.Click += new System.EventHandler(this.mTreeMenu_Click);
            // 
            // tsbTreeMenuVisible
            // 
            this.tsbTreeMenuVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTreeMenuVisible.Image = global::Main.Properties.Resources.TreeMenuVisible;
            resources.ApplyResources(this.tsbTreeMenuVisible, "tsbTreeMenuVisible");
            this.tsbTreeMenuVisible.Name = "tsbTreeMenuVisible";
            this.tsbTreeMenuVisible.Click += new System.EventHandler(this.mTreeMenuVisible_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tsbFlow
            // 
            this.tsbFlow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFlow.Image = global::Main.Properties.Resources.Flow;
            resources.ApplyResources(this.tsbFlow, "tsbFlow");
            this.tsbFlow.Name = "tsbFlow";
            this.tsbFlow.Click += new System.EventHandler(this.mFlow_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // tsbSwitchLanguage
            // 
            this.tsbSwitchLanguage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbSwitchLanguage, "tsbSwitchLanguage");
            this.tsbSwitchLanguage.Name = "tsbSwitchLanguage";
            this.tsbSwitchLanguage.Click += new System.EventHandler(this.tsbSwitchLanguage_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // tsbExit
            // 
            this.tsbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExit.Image = global::Main.Properties.Resources.Exit;
            resources.ApplyResources(this.tsbExit, "tsbExit");
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mRight,
            this.数据库参数维护ToolStripMenuItem,
            this.wEB菜单维护ToolStripMenuItem,
            this.报表设计ToolStripMenuItem,
            this.mWorkFlow,
            this.mDataModify});
            resources.ApplyResources(this.MainMenu, "MainMenu");
            this.MainMenu.Name = "MainMenu";
            // 
            // mRight
            // 
            this.mRight.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mDepart,
            this.mMember,
            this.mRole,
            this.mMemberRole,
            this.mRoleMember,
            this.mDepartType,
            this.mRolePurview,
            this.mPurview});
            this.mRight.Name = "mRight";
            resources.ApplyResources(this.mRight, "mRight");
            // 
            // mDepart
            // 
            this.mDepart.Image = global::Main.Properties.Resources.Depart;
            this.mDepart.Name = "mDepart";
            resources.ApplyResources(this.mDepart, "mDepart");
            this.mDepart.Click += new System.EventHandler(this.mDepart_Click);
            // 
            // mMember
            // 
            this.mMember.Image = global::Main.Properties.Resources.StausMember;
            this.mMember.Name = "mMember";
            resources.ApplyResources(this.mMember, "mMember");
            this.mMember.Click += new System.EventHandler(this.mMember_Click);
            // 
            // mRole
            // 
            this.mRole.Image = global::Main.Properties.Resources.Role;
            this.mRole.Name = "mRole";
            resources.ApplyResources(this.mRole, "mRole");
            this.mRole.Click += new System.EventHandler(this.mRole_Click);
            // 
            // mMemberRole
            // 
            this.mMemberRole.Image = global::Main.Properties.Resources.MemberRole;
            this.mMemberRole.Name = "mMemberRole";
            resources.ApplyResources(this.mMemberRole, "mMemberRole");
            this.mMemberRole.Click += new System.EventHandler(this.mMemberRole_Click);
            // 
            // mRoleMember
            // 
            this.mRoleMember.Image = global::Main.Properties.Resources.RoleMember;
            this.mRoleMember.Name = "mRoleMember";
            resources.ApplyResources(this.mRoleMember, "mRoleMember");
            this.mRoleMember.Click += new System.EventHandler(this.mRoleMember_Click);
            // 
            // mDepartType
            // 
            this.mDepartType.Name = "mDepartType";
            resources.ApplyResources(this.mDepartType, "mDepartType");
            this.mDepartType.Click += new System.EventHandler(this.mDepartType_Click);
            // 
            // mRolePurview
            // 
            this.mRolePurview.Image = global::Main.Properties.Resources.RolePurview;
            this.mRolePurview.Name = "mRolePurview";
            resources.ApplyResources(this.mRolePurview, "mRolePurview");
            this.mRolePurview.Click += new System.EventHandler(this.mRolePurview_Click);
            // 
            // mPurview
            // 
            this.mPurview.Image = global::Main.Properties.Resources.Purview;
            this.mPurview.Name = "mPurview";
            resources.ApplyResources(this.mPurview, "mPurview");
            this.mPurview.Click += new System.EventHandler(this.mPurview_Click);
            // 
            // 数据库参数维护ToolStripMenuItem
            // 
            this.数据库参数维护ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTableType,
            this.mTable,
            this.mColumns});
            this.数据库参数维护ToolStripMenuItem.Name = "数据库参数维护ToolStripMenuItem";
            resources.ApplyResources(this.数据库参数维护ToolStripMenuItem, "数据库参数维护ToolStripMenuItem");
            // 
            // mTableType
            // 
            this.mTableType.Name = "mTableType";
            resources.ApplyResources(this.mTableType, "mTableType");
            this.mTableType.Click += new System.EventHandler(this.mTableType_Click);
            // 
            // mTable
            // 
            this.mTable.Image = global::Main.Properties.Resources.Table;
            this.mTable.Name = "mTable";
            resources.ApplyResources(this.mTable, "mTable");
            this.mTable.Click += new System.EventHandler(this.mTable_Click);
            // 
            // mColumns
            // 
            this.mColumns.Image = global::Main.Properties.Resources.Columns;
            this.mColumns.Name = "mColumns";
            resources.ApplyResources(this.mColumns, "mColumns");
            this.mColumns.Click += new System.EventHandler(this.mColumns_Click);
            // 
            // wEB菜单维护ToolStripMenuItem
            // 
            this.wEB菜单维护ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTreeMenu,
            this.mTreeMenuVisible});
            this.wEB菜单维护ToolStripMenuItem.Name = "wEB菜单维护ToolStripMenuItem";
            resources.ApplyResources(this.wEB菜单维护ToolStripMenuItem, "wEB菜单维护ToolStripMenuItem");
            // 
            // mTreeMenu
            // 
            this.mTreeMenu.Image = global::Main.Properties.Resources.TreeMenu;
            this.mTreeMenu.Name = "mTreeMenu";
            resources.ApplyResources(this.mTreeMenu, "mTreeMenu");
            this.mTreeMenu.Click += new System.EventHandler(this.mTreeMenu_Click);
            // 
            // mTreeMenuVisible
            // 
            this.mTreeMenuVisible.Image = global::Main.Properties.Resources.TreeMenuVisible;
            this.mTreeMenuVisible.Name = "mTreeMenuVisible";
            resources.ApplyResources(this.mTreeMenuVisible, "mTreeMenuVisible");
            this.mTreeMenuVisible.Click += new System.EventHandler(this.mTreeMenuVisible_Click);
            // 
            // 报表设计ToolStripMenuItem
            // 
            this.报表设计ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mReportType,
            this.mReport,
            this.mReportColumnPos});
            this.报表设计ToolStripMenuItem.Name = "报表设计ToolStripMenuItem";
            resources.ApplyResources(this.报表设计ToolStripMenuItem, "报表设计ToolStripMenuItem");
            // 
            // mReportType
            // 
            this.mReportType.Name = "mReportType";
            resources.ApplyResources(this.mReportType, "mReportType");
            this.mReportType.Click += new System.EventHandler(this.mReportType_Click);
            // 
            // mReport
            // 
            this.mReport.Name = "mReport";
            resources.ApplyResources(this.mReport, "mReport");
            this.mReport.Click += new System.EventHandler(this.mReport_Click);
            // 
            // mReportColumnPos
            // 
            this.mReportColumnPos.Name = "mReportColumnPos";
            resources.ApplyResources(this.mReportColumnPos, "mReportColumnPos");
            this.mReportColumnPos.Click += new System.EventHandler(this.mReportColumnPos_Click);
            // 
            // mWorkFlow
            // 
            this.mWorkFlow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFlow,
            this.mRestDate,
            this.mLegalHoliday});
            this.mWorkFlow.Name = "mWorkFlow";
            resources.ApplyResources(this.mWorkFlow, "mWorkFlow");
            // 
            // mFlow
            // 
            this.mFlow.Image = global::Main.Properties.Resources.Flow;
            this.mFlow.Name = "mFlow";
            resources.ApplyResources(this.mFlow, "mFlow");
            this.mFlow.Click += new System.EventHandler(this.mFlow_Click);
            // 
            // mRestDate
            // 
            this.mRestDate.Name = "mRestDate";
            resources.ApplyResources(this.mRestDate, "mRestDate");
            this.mRestDate.Click += new System.EventHandler(this.mRestDate_Click);
            // 
            // mLegalHoliday
            // 
            this.mLegalHoliday.Name = "mLegalHoliday";
            resources.ApplyResources(this.mLegalHoliday, "mLegalHoliday");
            this.mLegalHoliday.Click += new System.EventHandler(this.mLegalHoliday_Click);
            // 
            // mDataModify
            // 
            this.mDataModify.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mDataBackUp,
            this.mDataIn,
            this.mDataConsistent});
            this.mDataModify.Name = "mDataModify";
            resources.ApplyResources(this.mDataModify, "mDataModify");
            // 
            // mDataBackUp
            // 
            this.mDataBackUp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mBackUpToSQL,
            this.mBackUpToBinary});
            this.mDataBackUp.Name = "mDataBackUp";
            resources.ApplyResources(this.mDataBackUp, "mDataBackUp");
            // 
            // mBackUpToSQL
            // 
            this.mBackUpToSQL.Name = "mBackUpToSQL";
            resources.ApplyResources(this.mBackUpToSQL, "mBackUpToSQL");
            this.mBackUpToSQL.Click += new System.EventHandler(this.mBackUpToSQL_Click);
            // 
            // mBackUpToBinary
            // 
            this.mBackUpToBinary.Name = "mBackUpToBinary";
            resources.ApplyResources(this.mBackUpToBinary, "mBackUpToBinary");
            this.mBackUpToBinary.Click += new System.EventHandler(this.mBackUpToBinary_Click);
            // 
            // mDataIn
            // 
            this.mDataIn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mLoadFromSQL,
            this.mLoadFromBinary});
            this.mDataIn.Name = "mDataIn";
            resources.ApplyResources(this.mDataIn, "mDataIn");
            this.mDataIn.Click += new System.EventHandler(this.mDataIn_Click);
            // 
            // mLoadFromSQL
            // 
            this.mLoadFromSQL.Name = "mLoadFromSQL";
            resources.ApplyResources(this.mLoadFromSQL, "mLoadFromSQL");
            this.mLoadFromSQL.Click += new System.EventHandler(this.mLoadFromSQL_Click);
            // 
            // mLoadFromBinary
            // 
            this.mLoadFromBinary.Name = "mLoadFromBinary";
            resources.ApplyResources(this.mLoadFromBinary, "mLoadFromBinary");
            this.mLoadFromBinary.Click += new System.EventHandler(this.mLoadFromBinary_Click);
            // 
            // mDataConsistent
            // 
            this.mDataConsistent.Name = "mDataConsistent";
            resources.ApplyResources(this.mDataConsistent, "mDataConsistent");
            this.mDataConsistent.Click += new System.EventHandler(this.mDataConsistent_Click);
            // 
            // MainStatusStrip
            // 
            resources.ApplyResources(this.MainStatusStrip, "MainStatusStrip");
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMessage,
            this.tssCompany,
            this.tssDatetime,
            this.tssMember});
            this.MainStatusStrip.Name = "MainStatusStrip";
            // 
            // tssMessage
            // 
            resources.ApplyResources(this.tssMessage, "tssMessage");
            this.tssMessage.Name = "tssMessage";
            // 
            // tssCompany
            // 
            resources.ApplyResources(this.tssCompany, "tssCompany");
            this.tssCompany.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tssCompany.Name = "tssCompany";
            // 
            // tssDatetime
            // 
            resources.ApplyResources(this.tssDatetime, "tssDatetime");
            this.tssDatetime.Name = "tssDatetime";
            // 
            // tssMember
            // 
            resources.ApplyResources(this.tssMember, "tssMember");
            this.tssMember.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tssMember.Image = global::Main.Properties.Resources.StausMember;
            this.tssMember.Name = "tssMember";
            // 
            // MainFrame
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.MainToolStrip);
            this.Controls.Add(this.MainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainFrame";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton tsbDepart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbMember;
        private System.Windows.Forms.ToolStripButton tsbRole;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mRight;
        private System.Windows.Forms.ToolStripMenuItem 数据库参数维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wEB菜单维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报表设计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mWorkFlow;
        private System.Windows.Forms.ToolStripMenuItem mDepart;
        private System.Windows.Forms.ToolStripMenuItem mMember;
        private System.Windows.Forms.ToolStripMenuItem mRole;
        private System.Windows.Forms.ToolStripMenuItem mTable;
        private System.Windows.Forms.ToolStripMenuItem mColumns;
        private System.Windows.Forms.ToolStripMenuItem mTreeMenu;
        private System.Windows.Forms.ToolStripMenuItem mTreeMenuVisible;
        private System.Windows.Forms.ToolStripMenuItem mRolePurview;
        private System.Windows.Forms.ToolStripMenuItem mMemberRole;
        private System.Windows.Forms.ToolStripMenuItem mRoleMember;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tssCompany;
        private System.Windows.Forms.ToolStripStatusLabel tssMember;
        private System.Windows.Forms.ToolStripStatusLabel tssDatetime;
        private System.Windows.Forms.ToolStripMenuItem mDepartType;
        private System.Windows.Forms.ToolStripMenuItem mTableType;
        private System.Windows.Forms.ToolStripMenuItem mFlow;
        private System.Windows.Forms.ToolStripMenuItem mPurview;
        private System.Windows.Forms.ToolStripButton tsbMemberRole;
        private System.Windows.Forms.ToolStripButton tsbRoleMember;
        private System.Windows.Forms.ToolStripButton tsbPurview;
        private System.Windows.Forms.ToolStripButton tsbRolePurview;
        private System.Windows.Forms.ToolStripButton tsbTable;
        private System.Windows.Forms.ToolStripButton tsbColumns;
        private System.Windows.Forms.ToolStripButton tsbTreeMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbTreeMenuVisible;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbFlow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripMenuItem mReportType;
        private System.Windows.Forms.ToolStripMenuItem mReport;
        public System.Windows.Forms.ToolStripStatusLabel tssMessage;
        private System.Windows.Forms.ToolStripMenuItem mDataModify;
        private System.Windows.Forms.ToolStripMenuItem mDataBackUp;
        private System.Windows.Forms.ToolStripMenuItem mDataConsistent;
        private System.Windows.Forms.ToolStripMenuItem mReportColumnPos;
        private System.Windows.Forms.ToolStripMenuItem mRestDate;
        private System.Windows.Forms.ToolStripMenuItem mLegalHoliday;
        private System.Windows.Forms.ToolStripMenuItem mDataIn;
        private System.Windows.Forms.ToolStripMenuItem mBackUpToSQL;
        private System.Windows.Forms.ToolStripMenuItem mBackUpToBinary;
        private System.Windows.Forms.ToolStripMenuItem mLoadFromSQL;
        private System.Windows.Forms.ToolStripMenuItem mLoadFromBinary;
        private System.Windows.Forms.ToolStripButton tsbSwitchLanguage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

    }
}
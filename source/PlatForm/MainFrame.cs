using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PlatForm
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            //string culture = System.Configuration.ConfigurationManager.AppSettings["Culture"];
            //if (culture != "")
            //{
            //    System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(culture);
            //    System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            //    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            //}
            InitializeComponent();
        }


        private void MainFrame_Load(object sender, EventArgs e)
        {
            tssMember.Text = "  " +CMain.memberName;
        }
 
        private void mDepartType_Click(object sender, EventArgs e)
        {
           frmDepartType departType = new frmDepartType();
           departType.MdiParent = this;
           departType.Show();
        }

        private void mDepart_Click(object sender, EventArgs e)
        {
            frmDepart depart = new frmDepart();
            depart.MdiParent = this;
            depart.Show();
        }

        private void mMember_Click(object sender, EventArgs e)
        {
            frmMember memeber = new frmMember();
            memeber.MdiParent = this;
            memeber.Show();
        }

        private void mRole_Click(object sender, EventArgs e)
        {
            frmRole role = new frmRole();
            role.MdiParent = this;
            role.Show();
        }

        private void mMemberRole_Click(object sender, EventArgs e)
        {
            frmMemeberRole memRole = new frmMemeberRole();
            memRole.MdiParent = this;
            memRole.Show();
        }

        private void mTableType_Click(object sender, EventArgs e)
        {
            frmTableType tableType = new frmTableType();
            tableType.MdiParent = this;
            tableType.Show();
        }

        private void mTable_Click(object sender, EventArgs e)
        {
            frmTable table = new frmTable();
            table.MdiParent = this;
            table.Show();
        }

        private void mColumns_Click(object sender, EventArgs e)
        {
            frmColumns column = new frmColumns();
            column.MdiParent = this;
            column.Show();
        }

        private void mTreeMenu_Click(object sender, EventArgs e)
        {
            frmTreeMenu treeMenu = new frmTreeMenu();
            treeMenu.MdiParent = this;
            treeMenu.Show();
        }

        private void mRoleMember_Click(object sender, EventArgs e)
        {
            frmRoleMemeber roleMem = new frmRoleMemeber();
            roleMem.MdiParent = this;
            roleMem.Show();
        }

        private void mFlow_Click(object sender, EventArgs e)
        {
            WorkFlow.frmFlow flow = new WorkFlow.frmFlow();
            flow.MdiParent = this;
            flow.Show();
        }

        private void mPurview_Click(object sender, EventArgs e)
        {
            frmPurview purview = new frmPurview();
            purview.MdiParent = this;
            purview.Show();
        }

        private void mRolePurview_Click(object sender, EventArgs e)
        {
            frmRolePurview RolePurview = new frmRolePurview();
            RolePurview.MdiParent = this;
            RolePurview.Show();
        }

        private void mDayOff_Click(object sender, EventArgs e)
        {
            //WorkFlow.frmRestDay rest = new WorkFlow.frmRestDay();
            //rest.MdiParent = this;
            //rest.Show();            
            
        }

        private void mTreeMenuVisible_Click(object sender, EventArgs e)
        {
            frmTreeMenuVisible treeMenuVisible = new frmTreeMenuVisible();
            treeMenuVisible.MdiParent = this;
            treeMenuVisible.Show();   
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Main.Properties.Resources.ExitBeforeConfirm, Main.Properties.Resources.Note, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                Application.Exit();
        }

        private void mReportType_Click(object sender, EventArgs e)
        {
            DmisReport.frmReportType reportType = new DmisReport.frmReportType();
            reportType.MdiParent = this;
            reportType.Show();   
        }

        private void mReport_Click(object sender, EventArgs e)
        {
            DmisReport.frmReport report = new DmisReport.frmReport();
            report.MdiParent = this;
            report.Show();   
        }

 

        private void mLogSearch_Click(object sender, EventArgs e)
        {
            frmLogSearch logSearch = new frmLogSearch();
            logSearch.MdiParent = this;
            logSearch.Show();
        }

        private void mDataConsistent_Click(object sender, EventArgs e)
        {
            DataBackup.frmDataConsistent dataConsistent = new DataBackup.frmDataConsistent();
            dataConsistent.MdiParent = this;
            dataConsistent.Show();
        }

        private void mReportColumnPos_Click(object sender, EventArgs e)
        {
            DmisReport.frmReportCellColumnPosition reportPos = new DmisReport.frmReportCellColumnPosition();
            reportPos.MdiParent = this;
            reportPos.Show();   
        }

        private void mRestDate_Click(object sender, EventArgs e)
        {
            WorkFlow.frmRestDateSet restDate = new PlatForm.WorkFlow.frmRestDateSet();
            restDate.MdiParent = this;
            restDate.Show();   

        }

          private void mLegalHoliday_Click(object sender, EventArgs e)
        {
            WorkFlow.frmLegalHoliday legalHoliday = new PlatForm.WorkFlow.frmLegalHoliday();
            legalHoliday.MdiParent = this;
            legalHoliday.Show();   

        }

        private void mDataMod_Click(object sender, EventArgs e)
        {
            DataBackup.frmDataModify backUp = new DataBackup.frmDataModify();
            backUp.MdiParent = this;
            backUp.Show(); 
        }

        private void mDataIn_Click(object sender, EventArgs e)
        {
            //DataBackup.frmDataIn dataIn = new DataBackup.frmDataIn();
            //dataIn.MdiParent = this;
            //dataIn.Show();
        }

        private void mBackUpToSQL_Click(object sender, EventArgs e)
        {
            frmBackUpToSQL bpToSql = new frmBackUpToSQL();
            bpToSql.MdiParent = this;
            bpToSql.Show(); 
        }

        private void mBackUpToXML_Click(object sender, EventArgs e)
        {
            frmBackUpToXML bpToXml = new frmBackUpToXML();
            bpToXml.MdiParent = this;
            bpToXml.Show();
        }

        private void mBackUpToBinary_Click(object sender, EventArgs e)
        {
            frmBackUpToBinary bpToBinary = new frmBackUpToBinary();
            bpToBinary.MdiParent = this;
            bpToBinary.Show();

        }

        private void mLoadFromSQL_Click(object sender, EventArgs e)
        {
            frmLoadFromSQL lfSql = new frmLoadFromSQL();
            lfSql.MdiParent = this;
            lfSql.Show();
        }

        private void mLoadFromXML_Click(object sender, EventArgs e)
        {
            frmLoadFromXML lfXml = new frmLoadFromXML();
            lfXml.MdiParent = this;
            lfXml.Show();
        }

        private void mBackUpToExcel_Click(object sender, EventArgs e)
        {
            frmBackUpToExcel bpExcel = new frmBackUpToExcel();
            bpExcel.MdiParent = this;
            bpExcel.Show();
        }

        private void mLoadFromExcel_Click(object sender, EventArgs e)
        {
            frmLoadFromExcel lfExcel = new frmLoadFromExcel();
            lfExcel.MdiParent = this;
            lfExcel.Show();
        }

        private void tsbSwitchLanguage_Click(object sender, EventArgs e)
        {
            while (this.MdiChildren.Length > 0)   //关闭子窗口
            {
                this.MdiChildren[0].Close();
            }

            //关闭菜单、工具条、状态栏
            this.MainMenuStrip.Dispose();
            this.MainToolStrip.Dispose();
            this.MainStatusStrip.Dispose();

            //目前只能是中方和西班牙方互切换，其它语言时要还要修改。
            System.Globalization.CultureInfo ci;
            if(System.Threading.Thread.CurrentThread.CurrentCulture.Name=="es-ES")
                ci = new System.Globalization.CultureInfo("zh-CN");
            else
                ci = new System.Globalization.CultureInfo("es-ES");

            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            InitializeComponent();
        }

        private void mLoadFromBinary_Click(object sender, EventArgs e)
        {
            DataBackup.frmLoadFromBinary lfBry = new DataBackup.frmLoadFromBinary();
            lfBry.MdiParent = this;
            lfBry.Show();
        }


    }
}
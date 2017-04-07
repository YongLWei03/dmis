namespace PlatForm
{
    partial class frmMember
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMember));
            this.trvDepart = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lsMemeber = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.CODE = new System.Windows.Forms.ColumnHeader();
            this.NAME = new System.Windows.Forms.ColumnHeader();
            this.PASSWORD = new System.Windows.Forms.ColumnHeader();
            this.FLAG = new System.Windows.Forms.ColumnHeader();
            this.HOME_PHONE = new System.Windows.Forms.ColumnHeader();
            this.OFFICE_PHONE = new System.Windows.Forms.ColumnHeader();
            this.MOBILE = new System.Windows.Forms.ColumnHeader();
            this.EMAIL = new System.Windows.Forms.ColumnHeader();
            this.ADDRESS = new System.Windows.Forms.ColumnHeader();
            this.THEME = new System.Windows.Forms.ColumnHeader();
            this.ORDER_ID = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtORDER_ID = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtADDRESS = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ckbFLAG = new System.Windows.Forms.CheckBox();
            this.txtTHEME = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEMAIL = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMOBILE = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOFFICE_PHONE = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHOME_PHONE = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPASSWORD = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSEX = new System.Windows.Forms.ComboBox();
            this.txtNAME = new System.Windows.Forms.TextBox();
            this.txtCODE = new System.Windows.Forms.TextBox();
            this.txtDEPART_ID = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tlbAdd = new System.Windows.Forms.ToolStripButton();
            this.tlbDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectDepart = new System.Windows.Forms.ToolStripButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.tsTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // trvDepart
            // 
            resources.ApplyResources(this.trvDepart, "trvDepart");
            this.trvDepart.ImageList = this.imageList1;
            this.trvDepart.LineColor = System.Drawing.Color.Maroon;
            this.trvDepart.Name = "trvDepart";
            this.trvDepart.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDepart_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // lsMemeber
            // 
            resources.ApplyResources(this.lsMemeber, "lsMemeber");
            this.lsMemeber.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.CODE,
            this.NAME,
            this.PASSWORD,
            this.FLAG,
            this.HOME_PHONE,
            this.OFFICE_PHONE,
            this.MOBILE,
            this.EMAIL,
            this.ADDRESS,
            this.THEME,
            this.ORDER_ID});
            this.lsMemeber.FullRowSelect = true;
            this.lsMemeber.GridLines = true;
            this.lsMemeber.MultiSelect = false;
            this.lsMemeber.Name = "lsMemeber";
            this.lsMemeber.UseCompatibleStateImageBehavior = false;
            this.lsMemeber.View = System.Windows.Forms.View.Details;
            this.lsMemeber.SelectedIndexChanged += new System.EventHandler(this.lsMemeber_SelectedIndexChanged);
            // 
            // ID
            // 
            resources.ApplyResources(this.ID, "ID");
            // 
            // CODE
            // 
            resources.ApplyResources(this.CODE, "CODE");
            // 
            // NAME
            // 
            resources.ApplyResources(this.NAME, "NAME");
            // 
            // PASSWORD
            // 
            resources.ApplyResources(this.PASSWORD, "PASSWORD");
            // 
            // FLAG
            // 
            resources.ApplyResources(this.FLAG, "FLAG");
            // 
            // HOME_PHONE
            // 
            resources.ApplyResources(this.HOME_PHONE, "HOME_PHONE");
            // 
            // OFFICE_PHONE
            // 
            resources.ApplyResources(this.OFFICE_PHONE, "OFFICE_PHONE");
            // 
            // MOBILE
            // 
            resources.ApplyResources(this.MOBILE, "MOBILE");
            // 
            // EMAIL
            // 
            resources.ApplyResources(this.EMAIL, "EMAIL");
            // 
            // ADDRESS
            // 
            resources.ApplyResources(this.ADDRESS, "ADDRESS");
            // 
            // THEME
            // 
            resources.ApplyResources(this.THEME, "THEME");
            // 
            // ORDER_ID
            // 
            resources.ApplyResources(this.ORDER_ID, "ORDER_ID");
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtORDER_ID);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtADDRESS);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.ckbFLAG);
            this.groupBox1.Controls.Add(this.txtTHEME);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtEMAIL);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtMOBILE);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtOFFICE_PHONE);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtHOME_PHONE);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtPASSWORD);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbSEX);
            this.groupBox1.Controls.Add(this.txtNAME);
            this.groupBox1.Controls.Add(this.txtCODE);
            this.groupBox1.Controls.Add(this.txtDEPART_ID);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtORDER_ID
            // 
            resources.ApplyResources(this.txtORDER_ID, "txtORDER_ID");
            this.txtORDER_ID.Name = "txtORDER_ID";
            this.txtORDER_ID.Validating += new System.ComponentModel.CancelEventHandler(this.txtORDER_ID_Validating);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtADDRESS
            // 
            resources.ApplyResources(this.txtADDRESS, "txtADDRESS");
            this.txtADDRESS.Name = "txtADDRESS";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // ckbFLAG
            // 
            resources.ApplyResources(this.ckbFLAG, "ckbFLAG");
            this.ckbFLAG.Name = "ckbFLAG";
            this.ckbFLAG.UseVisualStyleBackColor = true;
            // 
            // txtTHEME
            // 
            resources.ApplyResources(this.txtTHEME, "txtTHEME");
            this.txtTHEME.Name = "txtTHEME";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // txtEMAIL
            // 
            resources.ApplyResources(this.txtEMAIL, "txtEMAIL");
            this.txtEMAIL.Name = "txtEMAIL";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // txtMOBILE
            // 
            resources.ApplyResources(this.txtMOBILE, "txtMOBILE");
            this.txtMOBILE.Name = "txtMOBILE";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txtOFFICE_PHONE
            // 
            resources.ApplyResources(this.txtOFFICE_PHONE, "txtOFFICE_PHONE");
            this.txtOFFICE_PHONE.Name = "txtOFFICE_PHONE";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtHOME_PHONE
            // 
            resources.ApplyResources(this.txtHOME_PHONE, "txtHOME_PHONE");
            this.txtHOME_PHONE.Name = "txtHOME_PHONE";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtPASSWORD
            // 
            resources.ApplyResources(this.txtPASSWORD, "txtPASSWORD");
            this.txtPASSWORD.Name = "txtPASSWORD";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cbSEX
            // 
            this.cbSEX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSEX.FormattingEnabled = true;
            this.cbSEX.Items.AddRange(new object[] {
            resources.GetString("cbSEX.Items"),
            resources.GetString("cbSEX.Items1")});
            resources.ApplyResources(this.cbSEX, "cbSEX");
            this.cbSEX.Name = "cbSEX";
            // 
            // txtNAME
            // 
            resources.ApplyResources(this.txtNAME, "txtNAME");
            this.txtNAME.Name = "txtNAME";
            // 
            // txtCODE
            // 
            resources.ApplyResources(this.txtCODE, "txtCODE");
            this.txtCODE.Name = "txtCODE";
            // 
            // txtDEPART_ID
            // 
            resources.ApplyResources(this.txtDEPART_ID, "txtDEPART_ID");
            this.txtDEPART_ID.Name = "txtDEPART_ID";
            this.txtDEPART_ID.ReadOnly = true;
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tsTool
            // 
            this.tsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbAdd,
            this.tlbDelete,
            this.tlbSave,
            this.toolStripSeparator1,
            this.tlbRefresh,
            this.tsbSelectDepart});
            resources.ApplyResources(this.tsTool, "tsTool");
            this.tsTool.Name = "tsTool";
            // 
            // tlbAdd
            // 
            this.tlbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbAdd, "tlbAdd");
            this.tlbAdd.Name = "tlbAdd";
            this.tlbAdd.Tag = "新建人员";
            this.tlbAdd.Click += new System.EventHandler(this.tlbAdd_Click);
            // 
            // tlbDelete
            // 
            this.tlbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbDelete, "tlbDelete");
            this.tlbDelete.Name = "tlbDelete";
            this.tlbDelete.Click += new System.EventHandler(this.tlbDelete_Click);
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
            // tlbRefresh
            // 
            this.tlbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tlbRefresh, "tlbRefresh");
            this.tlbRefresh.Name = "tlbRefresh";
            this.tlbRefresh.Click += new System.EventHandler(this.tlbRefresh_Click);
            // 
            // tsbSelectDepart
            // 
            this.tsbSelectDepart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbSelectDepart, "tsbSelectDepart");
            this.tsbSelectDepart.Name = "tsbSelectDepart";
            this.tsbSelectDepart.Click += new System.EventHandler(this.tsbSelectDepart_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmMember
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsTool);
            this.Controls.Add(this.lsMemeber);
            this.Controls.Add(this.trvDepart);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "frmMember";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmMember_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMember_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvDepart;
        private System.Windows.Forms.ListView lsMemeber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tlbAdd;
        private System.Windows.Forms.ToolStripButton tlbDelete;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader CODE;
        private System.Windows.Forms.ColumnHeader NAME;
        private System.Windows.Forms.ColumnHeader PASSWORD;
        private System.Windows.Forms.ColumnHeader FLAG;
        private System.Windows.Forms.ColumnHeader HOME_PHONE;
        private System.Windows.Forms.ColumnHeader OFFICE_PHONE;
        private System.Windows.Forms.ColumnHeader MOBILE;
        private System.Windows.Forms.ColumnHeader EMAIL;
        private System.Windows.Forms.ColumnHeader ADDRESS;
        private System.Windows.Forms.ColumnHeader THEME;
        private System.Windows.Forms.ColumnHeader ORDER_ID;
        private System.Windows.Forms.ComboBox cbSEX;
        private System.Windows.Forms.TextBox txtNAME;
        private System.Windows.Forms.TextBox txtCODE;
        private System.Windows.Forms.TextBox txtDEPART_ID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHOME_PHONE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPASSWORD;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTHEME;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtEMAIL;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMOBILE;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtOFFICE_PHONE;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ckbFLAG;
        private System.Windows.Forms.TextBox txtADDRESS;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtORDER_ID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.ToolStripButton tsbSelectDepart;
    }
}
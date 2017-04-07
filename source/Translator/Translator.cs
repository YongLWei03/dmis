using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Data.OracleClient;

namespace Translator
{
    public partial class Translator : Form
    {
        private string _sql;
        private string sourcePath;

        public Translator()
        {
            sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            InitializeComponent();
        }

        private void Translator_Load(object sender, EventArgs e)
        {
            //���Ʒ�����Ա�ܷ񿴼���ȡ�����ɰ�ť����ֹ�����
            if (ConfigurationManager.AppSettings["DisplayFlag"] == "1")
            {
                btnGet.Visible = true;
                btnGen.Visible = true;
                btnSingleGen.Visible = true;
            }
            else
            {
                btnGet.Visible = false;
                btnGen.Visible = false;
                btnSingleGen.Visible = false;
            }

            //��������б�
            //1��ȫ����Դ
            TreeNode node = new TreeNode("WEBȫ����Դ");
            node.Tag = "Global";
            trvType.Nodes.Add(node);

            //2��WEB����
            node = new TreeNode("WEB����");
            node.ToolTipText = "������Դ";
            node.Tag = "WEB";

            TreeNode child = new TreeNode("ͨ�ù���");
            child.Tag = "SYS_Common";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("�ĵ�����");
            child.Tag = "SYS_File";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("������");
            child.Tag = "SYS_WorkFlow";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("���ȹ���");
            child.Tag = "YW_DD";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("�ۺϹ���");
            child.Tag = "YW_GL";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("���վֵ�����");
            child.Tag = "YW_STATION";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("ͨ�Ź���");
            child.Tag = "YW_TX";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("�˷�����");
            child.Tag = "YW_YF";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("�Զ�������");
            child.Tag = "YW_ZDH";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("��������");
            child.Tag = "YW_BH";
            child.ImageIndex = 2;
            node.Nodes.Add(child);
            trvType.Nodes.Add(node);


            //3�����ݿ����
            node = new TreeNode("���ݿ����");
            node.Tag = "Database";

            child = new TreeNode("����");
            child.Tag = "DMIS_SYS_DEPART*ID*NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("��λ");
            child.Tag = "DMIS_SYS_ROLE*ID*NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            //child = new TreeNode("Ȩ��");  //�ö��ظ��ģ�Ҫ���⴦��
            //child.Tag = "DMIS_SYS_PURVIEW*ID*DESCR";
            //child.ImageIndex = 3;
            //node.Nodes.Add(child);

            child = new TreeNode("�����");
            child.Tag = "DMIS_SYS_TABLE_TYPE*ID*DESCR";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("���ݿ��");
            child.Tag = "DMIS_SYS_TABLES*ID*DESCR";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("���ݿ���");
            child.Tag = "DMIS_SYS_COLUMNS*ID*DESCR";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("�˵�");
            child.Tag = "DMIS_SYS_TREEMENU*ID*NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("��������");
            child.Tag = "DMIS_SYS_REPORT_TYPE*ID*NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("��������");
            child.Tag = "DMIS_SYS_REPORT*ID*NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("ҵ������");
            child.Tag = "DMIS_SYS_PACKTYPE*F_NO*F_NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("�ĵ�����");
            child.Tag = "DMIS_SYS_DOCTYPE*F_NO*F_NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("�ڵ�");
            child.Tag = "DMIS_SYS_FLOWLINK*F_NO*F_NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);
            trvType.Nodes.Add(node);


            //4�����ά������
            node = new TreeNode("ά��ƽ̨");
            node.Tag = "PlatForm";

            child = new TreeNode("ͨ����Դ");
            child.Tag = @"PlatForm\Properties\*Resources.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("��¼����");
            child.Tag = @"PlatForm\*frmLogin.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("������");
            child.Tag = @"PlatForm\*MainFrame.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("���Ű���");
            child.Tag = @"PlatForm\Right\*frmDepart.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("��Աά��");
            child.Tag = @"PlatForm\Right\*frmMember.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("��λά��");
            child.Tag = @"PlatForm\Right\*frmRole.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("����Ա�ĸ�λ");
            child.Tag = @"PlatForm\Right\*frmMemeberRole.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("��λ�Ĳ���Ա");
            child.Tag = @"PlatForm\Right\*frmRoleMemeber.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("���Ű�������");
            child.Tag = @"PlatForm\Right\*frmDepartType.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("��λ��Ȩ");
            child.Tag = @"PlatForm\Right\*frmRolePurview.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("Ȩ������");
            child.Tag = @"PlatForm\Right\*frmPurview.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("�����");
            child.Tag = @"PlatForm\Right\*frmTableType.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("��Ǽ�");
            child.Tag = @"PlatForm\Right\*frmTable.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("�еǼ�");
            child.Tag = @"PlatForm\Right\*frmColumns.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("�˵�����");
            child.Tag = @"PlatForm\Right\*frmTreeMenu.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("�ɼ�������");
            child.Tag = @"PlatForm\Right\*frmTreeMenuVisible.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("ѡ��˵�");
            child.Tag = @"PlatForm\Right\*frmTreeMenuSelect.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("��������");
            child.Tag = @"Report\*frmReportType.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("�������");
            child.Tag = @"Report\*frmReport.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("������λ��");
            child.Tag = @"Report\*frmReportCellColumnPosition.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("�����������");
            child.Tag = @"Report\*frmFilter.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("������������");
            child.Tag = @"Report\*frmReportOrder.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("������");
            child.Tag = @"WorkFlow\*frmFlow.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("��Ϣ��");
            child.Tag = @"WorkFlow\*frmRestDateSet.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("�����ڼ���");
            child.Tag = @"WorkFlow\*frmLegalHoliday.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("�ĵ�����");
            child.Tag = @"WorkFlow\*frmdoctype.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);
            
            child = new TreeNode("ҵ������");
            child.Tag = @"WorkFlow\*frmpacktype.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("ѡ����Ա");
            child.Tag = @"WorkFlow\*frmSelectMember.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("ѡ���λ");
            child.Tag = @"WorkFlow\*frmSelectRole.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("ѡ����ػ���");
            child.Tag = @"WorkFlow\*frmSelectRelativeTache.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("SQL��ʽ����");
            child.Tag = @"DataBackup\*frmBackUpToSQL.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("�����Ƹ�ʽ����");
            child.Tag = @"DataBackup\*frmBackUpToBinary.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("SQL��ʽ����");
            child.Tag = @"DataBackup\*frmLoadFromSQL.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("�����Ƹ�ʽ����");
            child.Tag = @"DataBackup\*frmLoadFromBinary.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("���ݿ�һ����ά��");
            child.Tag = @"DataBackup\*frmDataConsistent.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);
            trvType.Nodes.Add(node);
        }

        private void trvType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvType.SelectedNode == null) return;

            InitDataGridViewCols();
            int level = 0;
            level = trvType.SelectedNode.Level;

            if (level==0 && trvType.SelectedNode.Tag.ToString() == "Global")   //ȫ��
            {
                if(ckbAll.Checked)
                    _sql = "select Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID " +
                        " from t_main where Category='ȫ��'";
                else
                    _sql = "select Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID " +
                        " from t_main where Category='ȫ��' and ChineseDesc<>''";

                DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
                dgvMain.DataSource = dt;
            }
            else if (level==1)  
            {
                if(trvType.SelectedNode.Parent.Tag.ToString()=="WEB") //WEBҳ��
                {
                    if (ckbAll.Checked)
                        _sql = "select Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID " +
                            " from t_main where Category='WEB' and SubDirectory='" + trvType.SelectedNode.Tag.ToString() + "' order by WebPage";
                    else
                        _sql = "select Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID " +
                            " from t_main where Category='WEB' and SubDirectory='" + trvType.SelectedNode.Tag.ToString() + "' and ChineseDesc<>'' order by WebPage";
                    DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
                    dgvMain.DataSource = dt;

                }
                else if (trvType.SelectedNode.Parent.Tag.ToString() == "Database")  //ƽ̨����
                {
                    string tableName;
                    string[] paras = trvType.SelectedNode.Tag.ToString().Split('*');
                    tableName = paras[0];
                    _sql = "select ChineseDesc,SpanishDesc,EnglishDesc,TableName,RecTid,TID from T_DATABASE where TableName='" + tableName + "'";
                    DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
                    dgvDatabase.DataSource = dt;
                }
                else if (trvType.SelectedNode.Parent.Tag.ToString() == "PlatForm")
                {
                    _sql = "select Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,FileName,PathName,TID from T_PLATFORM where FUNC='" + trvType.SelectedNode.Text + "'";
                    DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
                    dgvPlatform.DataSource = dt;
                }
            }
            else 
            {

            }
        }


        /// <summary>
        /// ����Դ�ļ�����ӦҪ����Ķ�����ACCESS���ݿ��У�ֻ����������Դ�ļ��е�������
        /// Ӣ�ĺ��������ĵĲ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGet_Click(object sender, EventArgs e)
        {
            if (trvType.SelectedNode == null) return;

            string path = ConfigurationManager.AppSettings["SourcePath"];
            if (path.Trim() == "")
            {
                MessageBox.Show("û������Դ�����·��");
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            object obj;
            uint maxTid;
            string objectName;      //��Դ���Ķ�������
            string webPath;
            string fileName;
            XmlReader subXtr;
            XmlTextReader xmlChina;


            if (trvType.SelectedNode.Level==0 && trvType.SelectedNode.Tag.ToString() == "Global")   //ȫ��
            {
                if (!File.Exists(path + @"web\App_GlobalResources\WebGlobalResource.resx")) return;
                    
                xmlChina = new XmlTextReader(path + @"web\App_GlobalResources\WebGlobalResource.resx");
                while (xmlChina.Read())
                {
                    if (xmlChina.NodeType == XmlNodeType.Element && xmlChina.Name == "data")
                    {
                        if (xmlChina["name"].Trim() == "") continue;
                        _sql = "select count(*) from t_main where Category='ȫ��' and ObjectName='" + xmlChina["name"].Trim() + "'";
                        obj = DBOpt.dbHelper.ExecuteScalar(_sql);
                        if (obj == null || obj.ToString() == "0")  //���ݿ��в����ڴ���Դ����������ݿ��У����򲻼�
                        {
                            maxTid = DBOpt.dbHelper.GetMaxNum("t_main", "TID");
                            objectName = xmlChina["name"].Trim();
                            subXtr=xmlChina.ReadSubtree();//һ�������˽�<value></value>
                            while (subXtr.Read())
                            {
                                if (subXtr.Name == "value")
                                {
                                    _sql = "insert into t_main(Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID) values(" +
                                       "'ȫ����Դ','" + objectName + "','" + subXtr.ReadElementString() + "','','','','','ȫ��'," + maxTid.ToString() + ")";
                                    DBOpt.dbHelper.ExecuteSql(_sql);
                                    break;
                                }
                            }
                            subXtr.Close();
                        }
                    }
                }
                xmlChina.Close();
            }
            else //ѡ��ڶ���
            {
                if (trvType.SelectedNode.Level == 1)   //ѡ��ڶ��������
                {
                    //ÿ��ҳ�涼����ͬ�Ŀؼ������磺��ӡ�ɾ������ӡ�ȣ�Ϊ�˼��ٷ���Ĺ�����������Щ�ؼ��ĸ�ֵ����
                    //WEB�����App_CodeĿ¼�µ��ļ�SetPageControlLocalizationText.cs�У�����ȡʱ���������˲��ֵĴ�������
                    TreeNode parentNode = trvType.SelectedNode.Parent;
                    if (parentNode.Tag.ToString() == "WEB")
                    {
                        string ignoreObject = "btnQueryResource^btnAddResource^btnDeleteResource^btnModifyResource^btnSearchResource^btnSortResource^btnPrintResource^btnFirstResource^btnPreviousResource^btnNextResource^btnLastResource^btnTurnResource^lblTurnResource^btnSaveResource^btnReturnResource^";
                        webPath = path + "web\\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\";
                        if (Directory.Exists(webPath))  //WEB����
                        {
                            string[] fileNames = Directory.GetFiles(webPath);   //�ļ����а���·��
                            foreach (string temp in fileNames)
                            {
                                fileName = temp.Substring(temp.LastIndexOf("\\") + 1);  //ֻҪ�ļ���
                                if (fileName.IndexOf(".en.") > 0) continue;
                                if (fileName.IndexOf(".es.") > 0) continue;
                                string webPage = fileName.Substring(0, fileName.Length - 5);  //��ҳ����
                                xmlChina = new XmlTextReader(webPath + fileName);
                                while (xmlChina.Read())
                                {
                                    if (xmlChina.NodeType == XmlNodeType.Element && xmlChina.Name == "data")
                                    {
                                        if (xmlChina["name"].Trim() == "") continue;
                                        _sql = "select count(*) from t_main where Category='WEB' and WebPage='" + webPage + "' and ObjectName='" + xmlChina["name"].Trim() + "'";
                                        obj = DBOpt.dbHelper.ExecuteScalar(_sql);
                                        if (obj == null || obj.ToString() == "0")  //���ݿ��в����ڴ���Դ����������ݿ��У����򲻼�
                                        {
                                            maxTid = DBOpt.dbHelper.GetMaxNum("t_main", "TID");
                                            objectName = xmlChina["name"].Trim();

                                            if (ignoreObject.IndexOf(objectName) >= 0)   //ͨ�ö������յ���������
                                            {
                                                _sql = "insert into t_main(Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID) values(" +
                                                       "'������Դ','" + objectName + "','','','','" + webPage + "','" + trvType.SelectedNode.Tag.ToString() + "','WEB'," + maxTid.ToString() + ")";
                                                DBOpt.dbHelper.ExecuteSql(_sql);
                                            }
                                            else  //��ͨ�ö���
                                            {
                                                subXtr = xmlChina.ReadSubtree();//һ�������˽�<value></value>
                                                while (subXtr.Read())
                                                {
                                                    if (subXtr.Name == "value")
                                                    {
                                                        _sql = "insert into t_main(Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID) values(" +
                                                           "'������Դ','" + objectName + "','" + subXtr.ReadElementString() + "','','','" + webPage + "','" + trvType.SelectedNode.Tag.ToString() + "','WEB'," + maxTid.ToString() + ")";
                                                        DBOpt.dbHelper.ExecuteSql(_sql);
                                                        break;
                                                    }
                                                }
                                                subXtr.Close();
                                            }
                                        }
                                    }
                                }
                                xmlChina.Close();
                            }
                        }
                    }
                    else if (parentNode.Tag.ToString() == "Database")   //ȡƽ̨�����е����ݡ�
                    {
                        GetFromDatabase(trvType.SelectedNode.Tag.ToString());
                    }
                    else if (parentNode.Tag.ToString() == "PlatForm")  //ȡά�������resx��Դ�ļ�����
                    {
                        GetFromPlatForm(trvType.SelectedNode.Tag.ToString());
                    }

                }
            }
            this.Cursor = Cursors.Arrow;
            trvType_AfterSelect(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            string english, spanlish;
            string tid;
            if (dgvMain.Visible)
            {
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    tid = dgvMain.Rows[i].Cells[dgvMain.ColumnCount - 1].Value.ToString();
                    english = dgvMain.Rows[i].Cells["EnglishDesc"].Value.ToString();
                    spanlish = dgvMain.Rows[i].Cells["SpanishDesc"].Value.ToString();
                    _sql = "update t_main set EnglishDesc='" + english + "',SpanishDesc='" + spanlish + "' where TID=" + tid;
                    if (DBOpt.dbHelper.ExecuteSql(_sql) < 0)
                        MessageBox.Show("����Ϊ" + tid + "���ݸ���ʧ�ܣ�");
                }
            }
            else if(dgvDatabase.Visible)
            {
                for (int i = 0; i < dgvDatabase.Rows.Count; i++)
                {
                    tid = dgvDatabase.Rows[i].Cells[dgvDatabase.ColumnCount - 1].Value.ToString();
                    english = dgvDatabase.Rows[i].Cells[2].Value.ToString();
                    spanlish = dgvDatabase.Rows[i].Cells[1].Value.ToString();
                    _sql = "update T_DATABASE set EnglishDesc='" + english + "',SpanishDesc='" + spanlish + "' where TID=" + tid;
                    if (DBOpt.dbHelper.ExecuteSql(_sql) < 0)
                        MessageBox.Show("����Ϊ" + tid + "���ݸ���ʧ�ܣ�");
                }
            }

            else if (dgvPlatform.Visible)
            {
                for (int i = 0; i < dgvPlatform.Rows.Count; i++)
                {
                    
                    tid = dgvPlatform.Rows[i].Cells[dgvPlatform.ColumnCount - 1].Value.ToString();
                    english = dgvPlatform.Rows[i].Cells[4].Value.ToString();
                    spanlish = dgvPlatform.Rows[i].Cells[3].Value.ToString();
                    _sql = "update T_PLATFORM set EnglishDesc='" + english + "',SpanishDesc='" + spanlish + "' where TID=" + tid;
                    if (DBOpt.dbHelper.ExecuteSql(_sql) < 0)
                        MessageBox.Show("����Ϊ" + tid + "���ݸ���ʧ�ܣ�");
                }
            }

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// ������Դ�ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGen_Click(object sender, EventArgs e)
        {
            if (trvType.SelectedNode == null) return;

            string path = ConfigurationManager.AppSettings["SourcePath"];
            if (path.Trim() == "")
            {
                MessageBox.Show("û������Դ�����·��");
                return;
            }
            if (MessageBox.Show("�˲�������ɾ����Դ�ļ������ȱ���֮�ټ������Ƿ�ʼִ�����ɣ�","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.No) return;
            this.Cursor = Cursors.WaitCursor;
            DataTable pages;         //ҳ��
            DataTable objectNames;   //��ҳ������Ķ���

            if (trvType.SelectedNode.Tag.ToString() == "Global")   //ȫ��
            {
                //ɾ���ļ�
                if (File.Exists(path + @"web\App_GlobalResources\WebGlobalResource.en.resx"))
                    File.Delete(path + @"web\App_GlobalResources\WebGlobalResource.en.resx");
                if (File.Exists(path + @"web\App_GlobalResources\WebGlobalResource.es.resx"))
                    File.Delete(path + @"web\App_GlobalResources\WebGlobalResource.es.resx");

                //��ͬһ��Դģ���ȡ������XmlDataDocument���浽��ͬ���ļ���
                //XmlDataDocument enXmlDoc = new XmlDataDocument();
                XmlDataDocument esXmlDoc = new XmlDataDocument();
                //FileStream enStream = new FileStream(Application.StartupPath + @"\ResourceTemplateEn.xml", FileMode.Open);
                FileStream esStream = new FileStream(Application.StartupPath + @"\ResourceTemplateEs.xml", FileMode.Open);
                //enXmlDoc.Load(enStream);
                esXmlDoc.Load(esStream);
                XmlElement data,value;

                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    //data = enXmlDoc.CreateElement("data");
                    //data.SetAttribute("name",dgvMain.Rows[i].Cells["ObjectName"].Value.ToString());
                    //data.SetAttribute("xml:space", "preserve");
                    //value = enXmlDoc.CreateElement("value");
                    //value.InnerText = dgvMain.Rows[i].Cells["EnglishDesc"].Value.ToString();
                    //data.AppendChild(value);
                    //enXmlDoc.DocumentElement.AppendChild(data);

                    data = esXmlDoc.CreateElement("data");
                    data.SetAttribute("name", dgvMain.Rows[i].Cells["ObjectName"].Value.ToString());
                    data.SetAttribute("xml:space", "preserve");
                    value = esXmlDoc.CreateElement("value");
                    value.InnerText = dgvMain.Rows[i].Cells["SpanishDesc"].Value.ToString();
                    data.AppendChild(value);
                    esXmlDoc.DocumentElement.AppendChild(data);
                }

                //enXmlDoc.Save(path + @"web\App_GlobalResources\WebGlobalResource.en.resx");
                esXmlDoc.Save(path + @"web\App_GlobalResources\WebGlobalResource.es.resx");
                //enStream.Close();
                esStream.Close();
            }
            else if(trvType.SelectedNode.Level == 1)
            {
                if (trvType.SelectedNode.Parent.Tag.ToString() == "WEB")   //
                {
                    //���Ҵ�Ŀ¼�µ�App_LocalResources���ж��ٸ�������Դ�ļ�
                    _sql = "select distinct WebPage from t_main where SubDirectory='"+trvType.SelectedNode.Tag.ToString()+"'";
                    pages = DBOpt.dbHelper.GetDataTable(_sql);
                    XmlDataDocument enXmlDoc, esXmlDoc;
                    FileStream enStream, esStream;

                    for (int i = 0; i < pages.Rows.Count; i++)
                    {
                        //ɾ���ļ�
                        if (File.Exists(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".en.resx"))
                            File.Delete(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".en.resx");
                        if (File.Exists(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".es.resx"))
                            File.Delete(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".es.resx");

                        //��ͬһ��Դģ���ȡ������XmlDataDocument���浽��ͬ���ļ���
                        //enXmlDoc = new XmlDataDocument();
                        esXmlDoc = new XmlDataDocument();
                        //enStream = new FileStream(Application.StartupPath + @"\ResourceTemplateEn.xml", FileMode.Open);
                        esStream = new FileStream(Application.StartupPath + @"\ResourceTemplateEs.xml", FileMode.Open);
                        //enXmlDoc.Load(enStream);
                        esXmlDoc.Load(esStream);
                        XmlElement data, value;

                        _sql = "select ObjectName,EnglishDesc,SpanishDesc from t_main where SubDirectory='" + trvType.SelectedNode.Tag.ToString() + "' and WebPage='" + pages.Rows[i][0].ToString() + "' order by TID";
                        objectNames = DBOpt.dbHelper.GetDataTable(_sql);
                        for (int j = 0; j < objectNames.Rows.Count; j++)
                        {
                            //data = enXmlDoc.CreateElement("data");
                            //data.SetAttribute("name", objectNames.Rows[j]["ObjectName"].ToString());
                            //data.SetAttribute("xml:space", "preserve");
                            //value = enXmlDoc.CreateElement("value");
                            //value.InnerText = objectNames.Rows[j]["EnglishDesc"].ToString();
                            //data.AppendChild(value);
                            //enXmlDoc.DocumentElement.AppendChild(data);

                            data = esXmlDoc.CreateElement("data");
                            data.SetAttribute("name", objectNames.Rows[j]["ObjectName"].ToString());
                            data.SetAttribute("xml:space", "preserve");
                            value = esXmlDoc.CreateElement("value");
                            value.InnerText = objectNames.Rows[j]["SpanishDesc"].ToString();
                            data.AppendChild(value);
                            esXmlDoc.DocumentElement.AppendChild(data);
                        }

                        //enXmlDoc.Save(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".en.resx");
                        esXmlDoc.Save(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".es.resx");
                        //enStream.Close();
                        esStream.Close();
                    }
                }
                else if (trvType.SelectedNode.Parent.Tag.ToString() == "Database")  //�޸����ݿ������
                {
                    string tableName, primaryCol;
                    string[] paras = trvType.SelectedNode.Tag.ToString().Split('*');
                    tableName = paras[0];
                    primaryCol = paras[1];

                    using (OracleConnection oraCon = new OracleConnection())
                    {
                        oraCon.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["OraConnStringMain"];
                        oraCon.Open();
                        OracleCommand oraCmd = new OracleCommand();
                        oraCmd.Connection = oraCon;
                        for (int i = 0; i < dgvDatabase.Rows.Count; i++)
                        {
                            _sql = "update " + tableName + " set OTHER_LANGUAGE_DESCR='" + dgvDatabase.Rows[i].Cells[1].Value + "' where " + primaryCol + " = " + dgvDatabase.Rows[i].Cells[4].Value;
                            oraCmd.CommandText = _sql;
                            oraCmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (trvType.SelectedNode.Parent.Tag.ToString() == "PlatForm")  //�޸�ά��ƽ̨resx�ļ���Ӧ������
                {
                    GenerateToPlatform(trvType.SelectedNode.Tag.ToString());
                }

            }
            this.Cursor = Cursors.Arrow;
        }

        private void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            trvType_AfterSelect(null, null);
        }

        //�����ݿ�������л�ȡ
        private void GetFromDatabase(string p)
        {
            string tableName, primaryCol, descCol;
            object obj;
            string[] paras = p.Split('*');
            tableName = paras[0];
            primaryCol = paras[1];
            descCol = paras[2];
            uint maxTid;
            maxTid = DBOpt.dbHelper.GetMaxNum("T_DATABASE", "TID");

            //�����ݿ���ȡ��������Ӧ������  (DMIS_SYS_PURVIEW���⴦���ֹ�д�룩
            
            _sql = "select " + primaryCol + "," + descCol + " from " + tableName + " order by " + primaryCol;
            DataTable pf=new DataTable();  //Database��ȡ��������
            
            using (OracleConnection oraCon = new OracleConnection())
            {
                oraCon.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["OraConnStringMain"];
                oraCon.Open();
                OracleCommand oraCmd = new OracleCommand(_sql, oraCon);
                OracleDataAdapter oraAdp = new OracleDataAdapter(oraCmd);
                oraAdp.Fill(pf);
            }

            for (int i = 0; i < pf.Rows.Count; i++)
            {
                _sql = "select count(*) from T_DATABASE where TableName='" + tableName + "' AND RecTid=" + pf.Rows[i][0].ToString();
                obj = DBOpt.dbHelper.ExecuteScalar(_sql);
                if (obj == null || obj.ToString() == "0")  //ACCESS���в����ڣ������
                {
                    _sql = "insert into T_DATABASE(TID,TableName,RecTid,ChineseDesc) values(" +
                        maxTid + ",'" + tableName + "'," + pf.Rows[i][0].ToString() + ",'" + pf.Rows[i][1].ToString() + "')";
                    if (DBOpt.dbHelper.ExecuteSql(_sql) > 0) maxTid++;
                }
            }
        }

        //��ƽ̨resx�ļ��л�ȡ
        private void GetFromPlatForm(string p)
        {
            string path, resxFile;
            string[] paras = p.Split('*');
            path = paras[0];
            resxFile = paras[1];
            if (!File.Exists(sourcePath + path + resxFile))
            {
                MessageBox.Show(sourcePath + path + resxFile+"�ļ�������");
                return;
            }
            uint maxTid;
            string obj;
            string val="";
            FileStream fs;
            XmlTextReader xtr;
            XmlReader subXtr;
            fs = new FileStream(sourcePath + path + resxFile, FileMode.Open);
            xtr = new XmlTextReader(fs);
            maxTid=DBOpt.dbHelper.GetMaxNum("T_PLATFORM","TID");
            xtr.MoveToContent();
            while (xtr.Read()) 
            {
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "data")
                {
                    obj = xtr.GetAttribute("name");
                    if (obj.IndexOf(".Text") > 0 || obj.IndexOf(".ToolTipText") > 0)  //ֻȡ����������
                    {
                        subXtr = xtr.ReadSubtree();
                        while (subXtr.Read())
                        {
                            if (subXtr.NodeType == XmlNodeType.Element && subXtr.Name == "value")
                            {
                                val = subXtr.ReadElementString();  //ȡֵ 
                                _sql = "insert into T_PLATFORM(TID,Func,PathName,FileName,ObjectName,ChineseDesc) values (" + maxTid + ",'" + trvType.SelectedNode.Text + "','"
                                     + path + "','" + resxFile + "','" + obj + "','" + val + "')";
                                if (DBOpt.dbHelper.ExecuteSql(_sql) > 0) maxTid++;
                            }
                        }
                    }
                }
            }
        }

        //�ѷ���Ľ�����ɵ�resx�ļ���,ƽ̨����
        private void GenerateToPlatform(string p)
        {
            string path, resxFile;
            string resxEsFile, resxEnFile;   //
            string[] paras = p.Split('*');
            path = paras[0];
            resxFile = paras[1];
            if (!File.Exists(sourcePath + path + resxFile))
            {
                MessageBox.Show(sourcePath + path + resxFile + "�ļ�������");
                return;
            }
            //ɾ���������ĵ���Դ�ļ�
            resxEsFile = resxFile.Substring(0, resxFile.IndexOf('.')) + ".es.resx";
            if (File.Exists(sourcePath + path + resxEsFile))
            {
                File.Delete(sourcePath + path + resxEsFile);
            }
            //ɾ��Ӣ�ĵ���Դ�ļ�
            resxEnFile = resxFile.Substring(0, resxFile.IndexOf('.')) + ".en.resx";
            if (File.Exists(sourcePath + path + resxEnFile))
            {
                File.Delete(sourcePath + path + resxEnFile);
            }

            //�ѷ���Ľ��д����Ӧ����Դ�ļ���
            //XmlDataDocument enXmlDoc = new XmlDataDocument();
            //FileStream enFile = new FileStream(sourcePath + path + resxFile, FileMode.Open);
            //enXmlDoc.PreserveWhitespace = false;
            //enXmlDoc.Load(enFile);

            XmlNodeList nodes;
            //for (int i = 0; i < dgvPlatform.Rows.Count; i++)
            //{
            //    nodes = enXmlDoc.SelectNodes("//root/data[@name='" + dgvPlatform.Rows[i].Cells[1].Value.ToString() + "']");
            //    if (nodes != null && nodes.Count > 0)
            //    {
            //        foreach (XmlNode node in nodes)
            //        {
            //            if (node.NodeType == XmlNodeType.Element)
            //            {
            //                node.ChildNodes[1].InnerText = dgvPlatform.Rows[i].Cells[4].Value.ToString();
            //                break;
            //            }
            //        }
            //    }
            //}
            //enXmlDoc.Save(sourcePath + path + resxEnFile);
            //enFile.Close();

            XmlDataDocument esXmlDoc = new XmlDataDocument();
            FileStream esFile = new FileStream(sourcePath + path + resxFile, FileMode.Open);
            esXmlDoc.PreserveWhitespace = false;
            esXmlDoc.Load(esFile);
            for (int i = 0; i < dgvPlatform.Rows.Count; i++)
            {
                nodes = esXmlDoc.SelectNodes("//root/data[@name='" + dgvPlatform.Rows[i].Cells[1].Value.ToString() + "']");
                if (nodes != null && nodes.Count > 0)
                {
                    foreach (XmlNode node in nodes)
                    {
                        if (node.NodeType == XmlNodeType.Element)
                        {
                            if(node.ChildNodes.Count>1)
                                node.ChildNodes[1].InnerText = dgvPlatform.Rows[i].Cells[3].Value.ToString();
                            break;
                        }
                    }
                }
            }
            esXmlDoc.Save(sourcePath + path + resxEsFile);
            esFile.Close();
        }

        private void InitDataGridViewCols()
        {
            if ((trvType.SelectedNode.Level == 0 && trvType.SelectedNode.Tag.ToString() == "Global") ||
                (trvType.SelectedNode.Level == 1 && trvType.SelectedNode.Parent.Tag.ToString() == "WEB"))
            {
                dgvMain.Visible = true;
                dgvDatabase.Visible = false;
                dgvPlatform.Visible=false;
            }
            else if (trvType.SelectedNode.Level == 1 && trvType.SelectedNode.Parent.Tag.ToString() == "Database")
            {
                dgvMain.Visible = false;
                dgvDatabase.Visible = true;
                dgvPlatform.Visible = false;
            }
            else if (trvType.SelectedNode.Level == 1 && trvType.SelectedNode.Parent.Tag.ToString() == "PlatForm")
            {
                dgvMain.Visible = false;
                dgvDatabase.Visible = false;
                dgvPlatform.Visible = true;
            }
        }


        /// <summary>
        /// ��������WEB����ĳ��ҳ��������ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSingleGen_Click(object sender, EventArgs e)
        {
            if (trvType.SelectedNode == null) return;
            if (trvType.SelectedNode.Level != 1) return;
            if (trvType.SelectedNode.Parent.Tag.ToString() != "WEB") return;
            if (dgvMain.SelectedRows.Count == 0) return;   //û��ѡ���У��򲻴���

            string path = ConfigurationManager.AppSettings["SourcePath"];
            if (path.Trim() == "")
            {
                MessageBox.Show("û������Դ�����·��");
                return;
            }
            if (MessageBox.Show("�˲�������ɾ����Դ�ļ������ȱ���֮�ټ������Ƿ�ʼִ�����ɣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            this.Cursor = Cursors.WaitCursor;
            //��ҳ��
            string webpage = dgvMain.SelectedRows[0].Cells[5].Value.ToString();
            if (webpage.Trim() == "") return;   //û����ҳ�����򲻴���

            
            DataTable objectNames;   //��ҳ������Ķ���

            XmlDataDocument enXmlDoc, esXmlDoc;
            FileStream enStream, esStream;

             //ɾ���ļ�
            if (File.Exists(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + webpage + ".en.resx"))
                File.Delete(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + webpage + ".en.resx");
            if (File.Exists(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + webpage + ".es.resx"))
                File.Delete(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + webpage + ".es.resx");

            //��ͬһ��Դģ���ȡ������XmlDataDocument���浽��ͬ���ļ���
            //enXmlDoc = new XmlDataDocument();
            esXmlDoc = new XmlDataDocument();
            //enStream = new FileStream(Application.StartupPath + @"\ResourceTemplateEn.xml", FileMode.Open);
            esStream = new FileStream(Application.StartupPath + @"\ResourceTemplateEs.xml", FileMode.Open);
            //enXmlDoc.Load(enStream);
            esXmlDoc.Load(esStream);
            XmlElement data, value;

            _sql = "select ObjectName,EnglishDesc,SpanishDesc from t_main where SubDirectory='" + trvType.SelectedNode.Tag.ToString() + "' and WebPage='" + webpage + "' order by TID";
            objectNames = DBOpt.dbHelper.GetDataTable(_sql);
            for (int j = 0; j < objectNames.Rows.Count; j++)
            {
                //data = enXmlDoc.CreateElement("data");
                //data.SetAttribute("name", objectNames.Rows[j]["ObjectName"].ToString());
                //data.SetAttribute("xml:space", "preserve");
                //value = enXmlDoc.CreateElement("value");
                //value.InnerText = objectNames.Rows[j]["EnglishDesc"].ToString();
                //data.AppendChild(value);
                //enXmlDoc.DocumentElement.AppendChild(data);

                data = esXmlDoc.CreateElement("data");
                data.SetAttribute("name", objectNames.Rows[j]["ObjectName"].ToString());
                data.SetAttribute("xml:space", "preserve");
                value = esXmlDoc.CreateElement("value");
                value.InnerText = objectNames.Rows[j]["SpanishDesc"].ToString();
                data.AppendChild(value);
                esXmlDoc.DocumentElement.AppendChild(data);
            }

            //enXmlDoc.Save(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".en.resx");
            esXmlDoc.Save(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + webpage + ".es.resx");
            //enStream.Close();
            esStream.Close();
            this.Cursor = Cursors.Arrow;

            MessageBox.Show("���ɳɹ���");
        }
    }
}
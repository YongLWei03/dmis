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
            //控制翻译人员能否看见提取和生成按钮，防止误操作
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

            //添加树形列表
            //1、全局资源
            TreeNode node = new TreeNode("WEB全局资源");
            node.Tag = "Global";
            trvType.Nodes.Add(node);

            //2、WEB界面
            node = new TreeNode("WEB界面");
            node.ToolTipText = "本地资源";
            node.Tag = "WEB";

            TreeNode child = new TreeNode("通用功能");
            child.Tag = "SYS_Common";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("文档管理");
            child.Tag = "SYS_File";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("工作流");
            child.Tag = "SYS_WorkFlow";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("调度管理");
            child.Tag = "YW_DD";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("综合管理");
            child.Tag = "YW_GL";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("变电站值班管理");
            child.Tag = "YW_STATION";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("通信管理");
            child.Tag = "YW_TX";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("运方管理");
            child.Tag = "YW_YF";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("自动化管理");
            child.Tag = "YW_ZDH";
            child.ImageIndex = 2;
            node.Nodes.Add(child);

            child = new TreeNode("保护管理");
            child.Tag = "YW_BH";
            child.ImageIndex = 2;
            node.Nodes.Add(child);
            trvType.Nodes.Add(node);


            //3、数据库参数
            node = new TreeNode("数据库参数");
            node.Tag = "Database";

            child = new TreeNode("部门");
            child.Tag = "DMIS_SYS_DEPART*ID*NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("岗位");
            child.Tag = "DMIS_SYS_ROLE*ID*NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            //child = new TreeNode("权限");  //好多重复的，要特殊处理
            //child.Tag = "DMIS_SYS_PURVIEW*ID*DESCR";
            //child.ImageIndex = 3;
            //node.Nodes.Add(child);

            child = new TreeNode("表分类");
            child.Tag = "DMIS_SYS_TABLE_TYPE*ID*DESCR";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("数据库表");
            child.Tag = "DMIS_SYS_TABLES*ID*DESCR";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("数据库列");
            child.Tag = "DMIS_SYS_COLUMNS*ID*DESCR";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("菜单");
            child.Tag = "DMIS_SYS_TREEMENU*ID*NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("报表类型");
            child.Tag = "DMIS_SYS_REPORT_TYPE*ID*NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("报表名称");
            child.Tag = "DMIS_SYS_REPORT*ID*NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("业务类型");
            child.Tag = "DMIS_SYS_PACKTYPE*F_NO*F_NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("文档类型");
            child.Tag = "DMIS_SYS_DOCTYPE*F_NO*F_NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);

            child = new TreeNode("节点");
            child.Tag = "DMIS_SYS_FLOWLINK*F_NO*F_NAME";
            child.ImageIndex = 3;
            node.Nodes.Add(child);
            trvType.Nodes.Add(node);


            //4、添加维护程序
            node = new TreeNode("维护平台");
            node.Tag = "PlatForm";

            child = new TreeNode("通用资源");
            child.Tag = @"PlatForm\Properties\*Resources.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("登录窗口");
            child.Tag = @"PlatForm\*frmLogin.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("主窗口");
            child.Tag = @"PlatForm\*MainFrame.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("部门班组");
            child.Tag = @"PlatForm\Right\*frmDepart.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("人员维护");
            child.Tag = @"PlatForm\Right\*frmMember.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("岗位维护");
            child.Tag = @"PlatForm\Right\*frmRole.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("操作员的岗位");
            child.Tag = @"PlatForm\Right\*frmMemeberRole.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("岗位的操作员");
            child.Tag = @"PlatForm\Right\*frmRoleMemeber.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("部门班组类型");
            child.Tag = @"PlatForm\Right\*frmDepartType.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("岗位授权");
            child.Tag = @"PlatForm\Right\*frmRolePurview.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("权限设置");
            child.Tag = @"PlatForm\Right\*frmPurview.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("表分类");
            child.Tag = @"PlatForm\Right\*frmTableType.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("表登记");
            child.Tag = @"PlatForm\Right\*frmTable.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("列登记");
            child.Tag = @"PlatForm\Right\*frmColumns.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("菜单设置");
            child.Tag = @"PlatForm\Right\*frmTreeMenu.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("可见性设置");
            child.Tag = @"PlatForm\Right\*frmTreeMenuVisible.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("选择菜单");
            child.Tag = @"PlatForm\Right\*frmTreeMenuSelect.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("报表类型");
            child.Tag = @"Report\*frmReportType.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("报表参数");
            child.Tag = @"Report\*frmReport.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("报表列位置");
            child.Tag = @"Report\*frmReportCellColumnPosition.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("报表过滤条件");
            child.Tag = @"Report\*frmFilter.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("报表排序条件");
            child.Tag = @"Report\*frmReportOrder.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("工作流");
            child.Tag = @"WorkFlow\*frmFlow.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("休息日");
            child.Tag = @"WorkFlow\*frmRestDateSet.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("法定节假日");
            child.Tag = @"WorkFlow\*frmLegalHoliday.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("文档类型");
            child.Tag = @"WorkFlow\*frmdoctype.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);
            
            child = new TreeNode("业务类型");
            child.Tag = @"WorkFlow\*frmpacktype.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("选择人员");
            child.Tag = @"WorkFlow\*frmSelectMember.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("选择岗位");
            child.Tag = @"WorkFlow\*frmSelectRole.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("选择相关环节");
            child.Tag = @"WorkFlow\*frmSelectRelativeTache.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("SQL格式备份");
            child.Tag = @"DataBackup\*frmBackUpToSQL.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("二进制格式备份");
            child.Tag = @"DataBackup\*frmBackUpToBinary.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("SQL格式导入");
            child.Tag = @"DataBackup\*frmLoadFromSQL.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("二进制格式导入");
            child.Tag = @"DataBackup\*frmLoadFromBinary.resx";
            child.ImageIndex = 4;
            node.Nodes.Add(child);

            child = new TreeNode("数据库一致性维护");
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

            if (level==0 && trvType.SelectedNode.Tag.ToString() == "Global")   //全局
            {
                if(ckbAll.Checked)
                    _sql = "select Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID " +
                        " from t_main where Category='全局'";
                else
                    _sql = "select Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID " +
                        " from t_main where Category='全局' and ChineseDesc<>''";

                DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
                dgvMain.DataSource = dt;
            }
            else if (level==1)  
            {
                if(trvType.SelectedNode.Parent.Tag.ToString()=="WEB") //WEB页面
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
                else if (trvType.SelectedNode.Parent.Tag.ToString() == "Database")  //平台参数
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
        /// 从资源文件把相应要翻译的对象导入ACCESS数据库中，只导入中文资源文件中的描述。
        /// 英文和西班牙文的不导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGet_Click(object sender, EventArgs e)
        {
            if (trvType.SelectedNode == null) return;

            string path = ConfigurationManager.AppSettings["SourcePath"];
            if (path.Trim() == "")
            {
                MessageBox.Show("没有设置源程序的路径");
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            object obj;
            uint maxTid;
            string objectName;      //资源中文对象描述
            string webPath;
            string fileName;
            XmlReader subXtr;
            XmlTextReader xmlChina;


            if (trvType.SelectedNode.Level==0 && trvType.SelectedNode.Tag.ToString() == "Global")   //全局
            {
                if (!File.Exists(path + @"web\App_GlobalResources\WebGlobalResource.resx")) return;
                    
                xmlChina = new XmlTextReader(path + @"web\App_GlobalResources\WebGlobalResource.resx");
                while (xmlChina.Read())
                {
                    if (xmlChina.NodeType == XmlNodeType.Element && xmlChina.Name == "data")
                    {
                        if (xmlChina["name"].Trim() == "") continue;
                        _sql = "select count(*) from t_main where Category='全局' and ObjectName='" + xmlChina["name"].Trim() + "'";
                        obj = DBOpt.dbHelper.ExecuteScalar(_sql);
                        if (obj == null || obj.ToString() == "0")  //数据库中不存在此资源，则加入数据库中，否则不加
                        {
                            maxTid = DBOpt.dbHelper.GetMaxNum("t_main", "TID");
                            objectName = xmlChina["name"].Trim();
                            subXtr=xmlChina.ReadSubtree();//一定读到了节<value></value>
                            while (subXtr.Read())
                            {
                                if (subXtr.Name == "value")
                                {
                                    _sql = "insert into t_main(Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID) values(" +
                                       "'全局资源','" + objectName + "','" + subXtr.ReadElementString() + "','','','','','全局'," + maxTid.ToString() + ")";
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
            else //选择第二层
            {
                if (trvType.SelectedNode.Level == 1)   //选择第二层的数据
                {
                    //每个页面都有相同的控件，比如：添加、删除、打印等，为了减少翻译的工作量，把这些控件的赋值放在
                    //WEB处理的App_Code目录下的文件SetPageControlLocalizationText.cs中，故提取时，不用作此部分的处理工作。
                    TreeNode parentNode = trvType.SelectedNode.Parent;
                    if (parentNode.Tag.ToString() == "WEB")
                    {
                        string ignoreObject = "btnQueryResource^btnAddResource^btnDeleteResource^btnModifyResource^btnSearchResource^btnSortResource^btnPrintResource^btnFirstResource^btnPreviousResource^btnNextResource^btnLastResource^btnTurnResource^lblTurnResource^btnSaveResource^btnReturnResource^";
                        webPath = path + "web\\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\";
                        if (Directory.Exists(webPath))  //WEB界面
                        {
                            string[] fileNames = Directory.GetFiles(webPath);   //文件名中包含路径
                            foreach (string temp in fileNames)
                            {
                                fileName = temp.Substring(temp.LastIndexOf("\\") + 1);  //只要文件名
                                if (fileName.IndexOf(".en.") > 0) continue;
                                if (fileName.IndexOf(".es.") > 0) continue;
                                string webPage = fileName.Substring(0, fileName.Length - 5);  //网页名称
                                xmlChina = new XmlTextReader(webPath + fileName);
                                while (xmlChina.Read())
                                {
                                    if (xmlChina.NodeType == XmlNodeType.Element && xmlChina.Name == "data")
                                    {
                                        if (xmlChina["name"].Trim() == "") continue;
                                        _sql = "select count(*) from t_main where Category='WEB' and WebPage='" + webPage + "' and ObjectName='" + xmlChina["name"].Trim() + "'";
                                        obj = DBOpt.dbHelper.ExecuteScalar(_sql);
                                        if (obj == null || obj.ToString() == "0")  //数据库中不存在此资源，则加入数据库中，否则不加
                                        {
                                            maxTid = DBOpt.dbHelper.GetMaxNum("t_main", "TID");
                                            objectName = xmlChina["name"].Trim();

                                            if (ignoreObject.IndexOf(objectName) >= 0)   //通用对象插入空的中文描述
                                            {
                                                _sql = "insert into t_main(Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID) values(" +
                                                       "'本地资源','" + objectName + "','','','','" + webPage + "','" + trvType.SelectedNode.Tag.ToString() + "','WEB'," + maxTid.ToString() + ")";
                                                DBOpt.dbHelper.ExecuteSql(_sql);
                                            }
                                            else  //非通用对象
                                            {
                                                subXtr = xmlChina.ReadSubtree();//一定读到了节<value></value>
                                                while (subXtr.Read())
                                                {
                                                    if (subXtr.Name == "value")
                                                    {
                                                        _sql = "insert into t_main(Func,ObjectName,ChineseDesc,SpanishDesc,EnglishDesc,WebPage,SubDirectory,Category,TID) values(" +
                                                           "'本地资源','" + objectName + "','" + subXtr.ReadElementString() + "','','','" + webPage + "','" + trvType.SelectedNode.Tag.ToString() + "','WEB'," + maxTid.ToString() + ")";
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
                    else if (parentNode.Tag.ToString() == "Database")   //取平台参数中的数据。
                    {
                        GetFromDatabase(trvType.SelectedNode.Tag.ToString());
                    }
                    else if (parentNode.Tag.ToString() == "PlatForm")  //取维护程序的resx资源文件数据
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
                        MessageBox.Show("主键为" + tid + "数据更新失败！");
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
                        MessageBox.Show("主键为" + tid + "数据更新失败！");
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
                        MessageBox.Show("主键为" + tid + "数据更新失败！");
                }
            }

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// 生成资源文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGen_Click(object sender, EventArgs e)
        {
            if (trvType.SelectedNode == null) return;

            string path = ConfigurationManager.AppSettings["SourcePath"];
            if (path.Trim() == "")
            {
                MessageBox.Show("没有设置源程序的路径");
                return;
            }
            if (MessageBox.Show("此操作将先删除资源文件，请先保存之再继续，是否开始执行生成？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.No) return;
            this.Cursor = Cursors.WaitCursor;
            DataTable pages;         //页面
            DataTable objectNames;   //此页面包含的对象

            if (trvType.SelectedNode.Tag.ToString() == "Global")   //全局
            {
                //删除文件
                if (File.Exists(path + @"web\App_GlobalResources\WebGlobalResource.en.resx"))
                    File.Delete(path + @"web\App_GlobalResources\WebGlobalResource.en.resx");
                if (File.Exists(path + @"web\App_GlobalResources\WebGlobalResource.es.resx"))
                    File.Delete(path + @"web\App_GlobalResources\WebGlobalResource.es.resx");

                //从同一资源模板读取，各个XmlDataDocument保存到不同的文件中
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
                    //查找此目录下的App_LocalResources下有多少个本地资源文件
                    _sql = "select distinct WebPage from t_main where SubDirectory='"+trvType.SelectedNode.Tag.ToString()+"'";
                    pages = DBOpt.dbHelper.GetDataTable(_sql);
                    XmlDataDocument enXmlDoc, esXmlDoc;
                    FileStream enStream, esStream;

                    for (int i = 0; i < pages.Rows.Count; i++)
                    {
                        //删除文件
                        if (File.Exists(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".en.resx"))
                            File.Delete(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".en.resx");
                        if (File.Exists(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".es.resx"))
                            File.Delete(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + pages.Rows[i]["WebPage"] + ".es.resx");

                        //从同一资源模板读取，各个XmlDataDocument保存到不同的文件中
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
                else if (trvType.SelectedNode.Parent.Tag.ToString() == "Database")  //修改数据库的内容
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
                else if (trvType.SelectedNode.Parent.Tag.ToString() == "PlatForm")  //修改维护平台resx文件相应的内容
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

        //从数据库参数表中获取
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

            //从数据库中取出所有相应的数据  (DMIS_SYS_PURVIEW特殊处理，手工写入）
            
            _sql = "select " + primaryCol + "," + descCol + " from " + tableName + " order by " + primaryCol;
            DataTable pf=new DataTable();  //Database中取出的数据
            
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
                if (obj == null || obj.ToString() == "0")  //ACCESS库中不存在，则插入
                {
                    _sql = "insert into T_DATABASE(TID,TableName,RecTid,ChineseDesc) values(" +
                        maxTid + ",'" + tableName + "'," + pf.Rows[i][0].ToString() + ",'" + pf.Rows[i][1].ToString() + "')";
                    if (DBOpt.dbHelper.ExecuteSql(_sql) > 0) maxTid++;
                }
            }
        }

        //从平台resx文件中获取
        private void GetFromPlatForm(string p)
        {
            string path, resxFile;
            string[] paras = p.Split('*');
            path = paras[0];
            resxFile = paras[1];
            if (!File.Exists(sourcePath + path + resxFile))
            {
                MessageBox.Show(sourcePath + path + resxFile+"文件不存在");
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
                    if (obj.IndexOf(".Text") > 0 || obj.IndexOf(".ToolTipText") > 0)  //只取这两样翻译
                    {
                        subXtr = xtr.ReadSubtree();
                        while (subXtr.Read())
                        {
                            if (subXtr.NodeType == XmlNodeType.Element && subXtr.Name == "value")
                            {
                                val = subXtr.ReadElementString();  //取值 
                                _sql = "insert into T_PLATFORM(TID,Func,PathName,FileName,ObjectName,ChineseDesc) values (" + maxTid + ",'" + trvType.SelectedNode.Text + "','"
                                     + path + "','" + resxFile + "','" + obj + "','" + val + "')";
                                if (DBOpt.dbHelper.ExecuteSql(_sql) > 0) maxTid++;
                            }
                        }
                    }
                }
            }
        }

        //把翻译的结果生成到resx文件中,平台部分
        private void GenerateToPlatform(string p)
        {
            string path, resxFile;
            string resxEsFile, resxEnFile;   //
            string[] paras = p.Split('*');
            path = paras[0];
            resxFile = paras[1];
            if (!File.Exists(sourcePath + path + resxFile))
            {
                MessageBox.Show(sourcePath + path + resxFile + "文件不存在");
                return;
            }
            //删除西班牙文的资源文件
            resxEsFile = resxFile.Substring(0, resxFile.IndexOf('.')) + ".es.resx";
            if (File.Exists(sourcePath + path + resxEsFile))
            {
                File.Delete(sourcePath + path + resxEsFile);
            }
            //删除英文的资源文件
            resxEnFile = resxFile.Substring(0, resxFile.IndexOf('.')) + ".en.resx";
            if (File.Exists(sourcePath + path + resxEnFile))
            {
                File.Delete(sourcePath + path + resxEnFile);
            }

            //把翻译的结果写到相应的资源文件中
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
        /// 单独生成WEB部分某个页面的资料文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSingleGen_Click(object sender, EventArgs e)
        {
            if (trvType.SelectedNode == null) return;
            if (trvType.SelectedNode.Level != 1) return;
            if (trvType.SelectedNode.Parent.Tag.ToString() != "WEB") return;
            if (dgvMain.SelectedRows.Count == 0) return;   //没有选择行，则不处理

            string path = ConfigurationManager.AppSettings["SourcePath"];
            if (path.Trim() == "")
            {
                MessageBox.Show("没有设置源程序的路径");
                return;
            }
            if (MessageBox.Show("此操作将先删除资源文件，请先保存之再继续，是否开始执行生成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            this.Cursor = Cursors.WaitCursor;
            //网页名
            string webpage = dgvMain.SelectedRows[0].Cells[5].Value.ToString();
            if (webpage.Trim() == "") return;   //没有网页名，则不处理

            
            DataTable objectNames;   //此页面包含的对象

            XmlDataDocument enXmlDoc, esXmlDoc;
            FileStream enStream, esStream;

             //删除文件
            if (File.Exists(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + webpage + ".en.resx"))
                File.Delete(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + webpage + ".en.resx");
            if (File.Exists(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + webpage + ".es.resx"))
                File.Delete(path + @"web\" + trvType.SelectedNode.Tag.ToString() + "\\App_LocalResources\\" + webpage + ".es.resx");

            //从同一资源模板读取，各个XmlDataDocument保存到不同的文件中
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

            MessageBox.Show("生成成功！");
        }
    }
}
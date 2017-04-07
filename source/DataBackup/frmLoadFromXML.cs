using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using PlatForm.DBUtility;
using System.Configuration;
using System.Xml;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;

namespace PlatForm
{
    public partial class frmLoadFromXML : Form
    {
        public frmLoadFromXML()
        {
            InitializeComponent();
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dlg.SelectedPath;
                ckbFiles.Items.Clear();
                DirectoryInfo di = new DirectoryInfo(txtPath.Text);
                FileInfo[] afi = di.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
                foreach (FileInfo fi in afi)
                    ckbFiles.Items.Add(fi.Name);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbFiles.Items.Count; i++)
                ckbFiles.SetItemChecked(i, true);
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbFiles.Items.Count; i++)
                ckbFiles.SetItemChecked(i, false);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "") return;
            if (ckbFiles.CheckedItems.Count < 1) return;

            FileStream fs;
            XmlTextReader xtr;
            XmlReader subXtr;
            OracleParameterCollection oraParas=new OracleParameterCollection();
            StringBuilder insertSql = new StringBuilder();
            StringBuilder insertCols = new StringBuilder();
            string tableName="";

            for (int i = 0; i < ckbFiles.CheckedItems.Count; i++)
            {
                fs = new FileStream(txtPath.Text+"\\"+ckbFiles.CheckedItems[i].ToString(), FileMode.Open);
                xtr = new XmlTextReader(fs);
                if (!xtr.ReadToFollowing("Rows")) continue;
                tableName = xtr.GetAttribute("TableName");
                if (tableName == null || tableName == "") continue;

                while (xtr.Read())
                {
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Row")
                    {
                        insertSql.Remove(0, insertSql.Length);
                        insertCols.Remove(0, insertCols.Length);

                        insertSql.Append("insert into " + tableName + "(");
                        subXtr = xtr.ReadSubtree();
                        oraParas.Clear();

                        while (subXtr.Read())
                        {
                            if (subXtr.NodeType == XmlNodeType.Element && subXtr.Name!="Row")
                            {
                                insertSql.Append(subXtr.Name + ",");
                                insertCols.Append(":" + subXtr.Name + ",");

                                if (DBHelper.databaseType == "Oracle")
                                {
                                    OracleParameter p = new OracleParameter();
                                    p.ParameterName = subXtr.Name;
                                    switch (subXtr.GetAttribute("ColType"))
                                    {
                                        case "String":
                                            p.OracleType = OracleType.VarChar;
                                            p.Value = subXtr.ReadElementString();
                                            break;
                                        case "Char":
                                            p.OracleType = OracleType.Char;
                                            p.Value = Convert.ToChar(subXtr.ReadElementString());
                                            break;
                                        case "DateTime":
                                            p.OracleType = OracleType.DateTime;
                                            p.Value =Convert.ToDateTime(subXtr.ReadElementString());
                                            break;
                                        case "Int16":
                                            p.OracleType = OracleType.Int16;
                                            p.Value = Convert.ToInt16(subXtr.ReadElementString());
                                            break;
                                        case "Int32":
                                            p.OracleType = OracleType.Int32;
                                            p.Value = Convert.ToInt32(subXtr.ReadElementString());
                                            break;
                                        case "Decimal":
                                        case "Single":
                                            p.OracleType = OracleType.Float;
                                            p.Value = Convert.ToSingle(subXtr.ReadElementString());
                                            break;
                                        case "Double":
                                            p.OracleType = OracleType.Double;
                                            p.Value = Convert.ToDouble(subXtr.ReadElementString());
                                            break;
                                        case "UInt16":
                                            p.OracleType = OracleType.UInt16;
                                            p.Value = Convert.ToUInt16(subXtr.ReadElementString());
                                            break;
                                        case "UInt32":
                                            p.OracleType = OracleType.UInt32;
                                            p.Value = Convert.ToUInt32(subXtr.ReadElementString());
                                            break;
                                        case "Boolean":
                                        case "Byte":
                                        case "SByte":
                                        default:
                                            break;
                                    }
                                    
                                    oraParas.Add(p);
                                }
                                else if (DBHelper.databaseType == "SqlServer")
                                {
                                }
                                else if (DBHelper.databaseType == "Sybase")
                                {
                                }
                                else
                                {
                                }
                            }
                        }
                        subXtr.Close();
                        insertSql.Remove(insertSql.Length - 1, 1);
                        insertSql.Append(") values(" + insertCols.Remove(insertCols.Length - 1, 1) + ")");

                        if (DBHelper.databaseType == "Oracle")
                        {
                            DBOpt.dbHelper.ExecuteByParameter(insertSql.ToString(), oraParas);
                        }
                        else if (DBHelper.databaseType == "SqlServer")
                        {
                        }
                        else if (DBHelper.databaseType == "Sybase")
                        {
                        }
                        else
                        {
                        }
                        
                        
                    }
                }
                xtr.Close();
                fs.Close();
                
             }
        }

        //private void test(object sender, EventArgs e)
        //{
        //    string sql;
        //    sql = "insert into aaaa(a,b,c) values(:a,:b,:c)";
        //    OracleParameterCollection oraParas = new OracleParameterCollection();
        //    OracleParameter p = new OracleParameter();
        //    p.ParameterName = "a";
        //    p.OracleType = OracleType.VarChar;
        //    p.Value = "tes'adf";
        //    oraParas.Add(p);

            
        //    OracleParameter p1 =  new OracleParameter();
        //    p1.ParameterName = "b";
        //    p1.OracleType = OracleType.DateTime;
        //    p1.Value = DateTime.Now;
        //    oraParas.Add(p1);

        //    OracleParameter p2 = new OracleParameter();
        //    p2.ParameterName = "c";
        //    p2.OracleType = OracleType.Number;
        //    p2.Value = 12;
        //    oraParas.Add(p2);

        //    DBOpt.dbHelper.ExecuteByParameter(sql, oraParas);

        //}


    }
}
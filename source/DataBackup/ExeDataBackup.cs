using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PlatForm.DBUtility;
using System.Data.Common;
using System.Diagnostics;

namespace DataBackup
{
    public partial class ExeDataBackup : Form
    {
        public ExeDataBackup()
        {
            InitializeComponent();
        }

        private void ExeDataBackup_Load(object sender, EventArgs e)
        {
            if (!File.Exists("ini.txt"))
            {
                FileStream fs = new FileStream("log" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(DateTime.Now.ToString()+" : û�����ö�ʱ���ݲ���!");
                sw.Flush();
                fs.Close();
                this.Close();
                return;
            }
            //���ݱ��ݲ��֣�
            string sOutData = "", sInData = "", sOutQuery = "", sInQuery = "", strU = "", strP = "";
            string[] tmp = new string[2];
            int iFlag = 0;//�ж��Ƿ���Ҫ��������!
            StreamReader reader = new StreamReader("ini.txt", Encoding.Default);
            sOutData = reader.ReadLine();
            sOutData = sOutData.Remove(0, 8);
            tmp = sOutData.Split(',');
            sOutData = tmp[0];
            sOutQuery = tmp[1];
            if (sOutQuery == "��")
            {
                if (DateTime.Today.Day != 1)
                {
                    FileStream fs = new FileStream("log" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(DateTime.Now.ToString() + " : ��������ʱ�ǰ���ִ�У����첻��1�ţ���ִ��!");
                    sw.Flush();
                    fs.Close();
                    reader.Close();
                    reader.Dispose();
                    this.Close();
                    return;
                }
            }
            sInData = reader.ReadLine();
            sInData = sInData.Remove(0, 8);
            tmp = sInData.Split(',');
            sInData = tmp[0];
            sInQuery = tmp[1];
            if (sInData.Trim() != "��")
                iFlag = 1;
            string filePath = reader.ReadLine();
            filePath = filePath.Remove(0, 7);
            if (Directory.Exists(filePath) == false)
            {
                FileStream fs = new FileStream("log" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(DateTime.Now.ToString() + " : û�����ñ��ݲ�����·��!");
                sw.Flush();
                fs.Close();
                reader.Close();
                reader.Dispose();
                this.Close();
                return;
            }
            string fileName = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString("00");
            Directory.CreateDirectory(filePath + "\\" + fileName);
            string dataName = reader.ReadLine();
            dataName = dataName.Remove(0, 5);
            strU = reader.ReadLine();
            strU = strU.Remove(0, 7);
            tmp = strU.Split(';');
            strU = tmp[0];
            strP = tmp[1].Remove(0, 3);
            int i = 0;//��¼�ɹ��������ٱ�
            while (!reader.EndOfStream)
            {
                string tableName = reader.ReadLine();
                string strFileName = filePath + "/" + fileName + "/" + tableName + ".txt";
                if (File.Exists(strFileName))
                {
                    FileStream fs = new FileStream("log" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(DateTime.Now.ToString() + " : �ļ������Ѵ��ڱ�" + tableName + "���ܱ���!");
                    sw.Flush();
                    fs.Close();
                }
                else
                {
                    FileStream fs = new FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    string[] result = new string[0];//���ɵĽ������
                    string strWhere = "";//��ѯ����

                    object strQueryCol = DBOpt.dbHelper.ExecuteScalar("select QUERY_COL from DMIS_SYS_TABLES where NAME='" + tableName + "'");
                    if (strQueryCol is System.DBNull || strQueryCol == null || strQueryCol.ToString().Trim() == "")//�ж϶������ѯ��û��û��ȫ�����ݣ�
                    {
                        result = DataReaderToArray("select * from " + dataName + ".dbo." + tableName, dataName + ".dbo." + tableName);
                        if (DBHelper.databaseType != "Oracle")
                        {
                            sw.WriteLine("delete from " + dataName + ".dbo." + tableName);
                            sw.WriteLine("go");
                            sw.WriteLine("print '�ɹ�ɾ����" + dataName + ".dbo." + tableName + "�е�" + result.Length.ToString() + "����¼!'");
                            sw.WriteLine("go");
                        }
                        else
                        {
                            sw.WriteLine("delete from " + dataName + ".dbo." + tableName + ";");
                            sw.WriteLine("commit;");
                            sw.WriteLine("print '�ɹ�ɾ����" + dataName + ".dbo." + tableName + "�е�" + result.Length.ToString() + "����¼!'");
                            sw.WriteLine("commit;");
                        }
                    }
                    else
                    {
                        if (sOutQuery == "��")
                        {
                            if (DBHelper.databaseType != "Oracle")
                            {
                                strWhere = " where convert(char(6)," + strQueryCol.ToString() + ",112)='" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString("00") + "'";
                                sw.WriteLine("delete from " + dataName + ".dbo." + tableName + strWhere);
                                sw.WriteLine("go");
                                result = DataReaderToArray("select * from " + tableName + strWhere, tableName);
                                sw.WriteLine("print '�ɹ�ɾ����" + dataName + ".dbo." + tableName + "�е�" + result.Length.ToString() + "����¼!'");
                                sw.WriteLine("go");
                            }
                            else
                            {
                                strWhere = " where to_char(" + strQueryCol.ToString() + ",'yyyymm')=" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString("00");
                                sw.WriteLine("delete from " + dataName + ".dbo." + tableName + strWhere + ";");
                                sw.WriteLine("commit;");
                                result = DataReaderToArray("select * from " + tableName + strWhere, tableName);
                                sw.WriteLine("print '�ɹ�ɾ����" + dataName + ".dbo." + tableName + "�е�" + result.Length.ToString() + "����¼!'");
                                sw.WriteLine("commit;");
                            }
                        }
                        else
                        {
                            if (DBHelper.databaseType != "Oracle")
                            {
                                strWhere = " where convert(char(8)," + strQueryCol.ToString() + ",112)='" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00") + "'";
                                sw.WriteLine("delete from " + dataName + ".dbo." + tableName + strWhere);
                                sw.WriteLine("go");
                                result = DataReaderToArray("select * from " + dataName + ".dbo." + tableName + strWhere, dataName + ".dbo." + tableName);
                                sw.WriteLine("print '�ɹ�ɾ����" + dataName + ".dbo." + tableName + "�е�" + result.Length.ToString() + "����¼!'");
                                sw.WriteLine("go");
                            }
                            else
                            {
                                strWhere = " where to_char(" + strQueryCol.ToString() + ",'yyyymmdd')=" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00");
                                sw.WriteLine("delete from " + dataName + ".dbo." + tableName + strWhere + ";");
                                sw.WriteLine("commit;");
                                result = DataReaderToArray("select * from " + dataName + ".dbo." + tableName + strWhere, dataName + ".dbo." + tableName);
                                sw.WriteLine("print '�ɹ�ɾ����" + dataName + ".dbo." + tableName + "�е�" + result.Length.ToString() + "����¼!'");
                                sw.WriteLine("commit;");
                            }
                        }
                    }
                    for (int j = 0; j < result.Length; j++)
                    {
                        sw.WriteLine(result[j]);
                        if (j % 10 == 9)
                        {
                            if (DBHelper.databaseType != "Oracle")
                                sw.WriteLine("go");
                            else
                                sw.WriteLine("commit;");
                        }

                    }
                    if (result.Length != 0)
                    {
                        sw.WriteLine("print '�ɹ������" + dataName + ".dbo." + tableName + "�е�" + result.Length.ToString() + "����¼!'");
                        if (DBHelper.databaseType != "Oracle")
                            sw.WriteLine("go");
                        else
                            sw.WriteLine("commit;");
                    }
                    sw.Flush();
                    fs.Close();
                    i = i + 1;
                }
            }
            reader.Close();
            reader.Dispose();
            FileStream fs1 = new FileStream("log" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1, Encoding.Default);
            sw1.BaseStream.Seek(0, SeekOrigin.End);
            sw1.WriteLine(DateTime.Now.ToString() + " : �����ݿ�" + sOutData + "�гɹ����ݳ�" + i.ToString() + "����!");
            sw1.Flush();
            fs1.Close();
            if (iFlag == 1)
            {
                if (sInQuery == "��")
                {
                    if (DateTime.Today.Day != 1)
                    {
                        FileStream fs = new FileStream("log" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                        sw.BaseStream.Seek(0, SeekOrigin.End);
                        sw.WriteLine(DateTime.Now.ToString() + " : ��������ʱ�ǰ���ִ�У����첻��1�ţ���ִ��!");
                        sw.Flush();
                        fs.Close();
                        reader.Close();
                        reader.Dispose();
                        this.Close();
                        return;
                    }
                }
                    i = 0;
                    StreamReader readerIn = new StreamReader("ini.txt", Encoding.Default);
                    readerIn.ReadLine(); readerIn.ReadLine(); readerIn.ReadLine(); readerIn.ReadLine();
                    while (!readerIn.EndOfStream)
                    {
                        string arguments = "";
                        string tableName = readerIn.ReadLine();
                        switch (DBHelper.databaseType)
                        {
                            case "Sybase":
                                arguments = "isql -U" + strU + " -P" + strP + " -S" + sInData + " < " + filePath + "\\" + fileName + "\\" + tableName + ".txt";
                                exeCmdDataIn(arguments);
                                break;
                            case "Oracle":
                                arguments = "sqlplus " + strU + "/" + strP + "@" + sInData + " < " + filePath + "\\" + fileName + "\\" + tableName + ".txt";
                                exeCmdDataIn(arguments);
                                break;
                            case "SqlServer":
                                arguments = "osql -U" + strU + " -P" + strP + " -S" + sInData + " < " + filePath + "\\" + fileName + "\\" + tableName + ".txt";
                                exeCmdDataIn(arguments);
                                break;
                        }
                        i = i + 1;
                    }
                    readerIn.Close();
                    readerIn.Dispose();
                    FileStream fs2 = new FileStream("log" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw2 = new StreamWriter(fs2, Encoding.Default);
                    sw2.BaseStream.Seek(0, SeekOrigin.End);
                    sw2.WriteLine(DateTime.Now.ToString() + " : �����ݿ�" + sInData + "�гɹ�����" + i.ToString() + "����!");
                    sw2.Flush();
                    fs2.Close();
                
            }
            this.Close();
        }
        protected string exeCmdDataIn(string arguments)
        {
            //System.Diagnostics.Process.Start(fileName);
            Process p = new Process();
            p.StartInfo.FileName = "cmd";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(arguments);
            p.StandardInput.WriteLine("\r\nexit");
            string ss = "";
            for (int i = 0; i < 6; i++)
            {
                ss = p.StandardOutput.ReadLine();
                if (ss.Contains("�ɹ�"))
                {
                    break;
                }
            }
            p.Close();
            p.Dispose();
            return ss.Replace("ɾ��", "����");
        }

        private string[] DataReaderToArray(string sql, string tableName)
        {
            //08.10.27 GLT�����޸�,����DataReader,����StringBuilder��ΪString.
            DataTable results = DBOpt.dbHelper.GetDataTable(sql);
            string[] arr = new string[results.Rows.Count];

            //�ٴ�������
            int ii = 0;
            for (int m = 0; m < results.Rows.Count; m++)
            {
                string vals = "";
                string cols = "";
                for (int n = 0; n < results.Columns.Count; n++)
                {
                    if (!(results.Rows[m][n] is System.DBNull))
                    {
                        cols += results.Columns[n].ColumnName + ",";
                        switch (results.Columns[n].DataType.FullName)
                        {
                            case "System.String":
                                vals += "'" + results.Rows[m][n].ToString().Replace("'", "\"") + "',";
                                break;
                            case "System.DateTime":
                                if (DBHelper.databaseType == "Oracle")
                                    vals += "TO_DATE('" + Convert.ToDateTime(results.Rows[m][n]).ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS'),";
                                else
                                    vals += "'" + results.Rows[m][n].ToString() + "',";
                                break;
                            default:
                                vals += results.Rows[m][n].ToString() + ",";
                                break;
                        }

                    }
                }
                if (DBHelper.databaseType != "Oracle")
                    arr[ii] = "insert into " + tableName + " ( " + cols.TrimEnd(',') + " ) values ( " + vals.TrimEnd(',') + " )";
                else
                    arr[ii] = "insert into " + tableName + " ( " + cols.TrimEnd(',') + " ) values ( " + vals.TrimEnd(',') + " );";
                ii = ii + 1;
            }
            return arr;
        }

    }
}
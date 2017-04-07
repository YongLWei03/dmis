using System;
using System.Collections.Generic;
using System.Text;
using PlatForm.DBUtility;
using System.Data;
using System.Collections;
using System.Data.Common;
using System.Globalization;

namespace PlatForm.DmisReport
{
    /// <summary>
    /// CELL������,���𱨱���ʾ���ݵ�����,�����ض���ʽ��String��
    /// </summary>
    [Serializable]
    public class CellReport
    {
        private int _id;                //����id
        private string _name;           //��������
        private string _type;           //�������ͣ�Single��MultiFixed��MultiFlexible���Ϸ���������Ʊ
        private string _cellFileName;   //�����Ӧ��CELL�ļ���
        public string[,] Tables;        //�������漰�����ݿ��
        public ArrayList TableParas;    //�����
        public string[,] Paras;         //����:�ڵ��õ���ҳ��ֵ
        public string[,] Columns;       //��
        public string[,] MultiColumns;  //������Ŷ��к͸��е���
        private DataSet ReportData;     //��Ŵ����ݿ��л�ȡ������

        public string PagesOrRows;         //����MultiFixed�ı���,���ҳ��������Ϣ;����MultiFlexible,��Ӧ���ӻ���ٵ�����.

        private string _sql;

        //2010-1-25 Ϊ�˹��ʻ���Ҫ�����Ӳ���language�����ڴ�ű��ػ���ʱ�Ĺ��ʻ����롣
        public CellReport(int id, string language)
        {
            _id = id;
            if (language == "zh-CN")
                _sql = "select NAME,FILE_NAME,TYPE from DMIS_SYS_REPORT where ID=" + id;
            else
                _sql = "select OTHER_LANGUAGE_DESCR NAME,FILE_NAME,TYPE from DMIS_SYS_REPORT where ID=" + id;

            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            dr.Read();
            ID = id;

            _name = dr[0].ToString();
            //ֻ���ļ���,����·��
            if (dr[1] != Convert.DBNull)
                _cellFileName = dr[1].ToString().Substring(dr[1].ToString().LastIndexOf('\\') + 1);
            else
                _cellFileName = "";
            _type = dr[2] != Convert.DBNull ? dr[2].ToString() : "";

            dr.Close();
            if (language == "zh-CN")
                _sql = "select ID,DESCR,PARA_TYPE,DEPEND_ID from DMIS_SYS_REPORT_PARA where REPORT_ID=" + id + " order by ORDER_ID";
            else
                _sql = "select ID,OTHER_LANGUAGE_DESCR DESCR,PARA_TYPE,DEPEND_ID from DMIS_SYS_REPORT_PARA where REPORT_ID=" + id + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            Paras = new string[dt.Rows.Count, 5];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Paras[i, 0] = dt.Rows[i][0].ToString();  //����id
                Paras[i, 1] = dt.Rows[i][1].ToString();  //��������
                Paras[i, 2] = dt.Rows[i][2].ToString();  //��������
                Paras[i, 3] = dt.Rows[i][3].ToString();  //��������id
                Paras[i, 4] = "";                        //����ȡֵ   ,��ֵ�ڱ�����ʾʱ��ֵ
            }

            TableParas = new ArrayList();
            //�Ȱ�Multi�ı����ǰ��,��ɨ��Multi�ı�,�Ծ���Ҫ���Ӷ���ҳ
            _sql = "select ID,TABLE_NAME,TABLE_TYPE,TABLE_ORDERS,TABLE_FILTER_WHERE,TABLE_PAGE_ROWS from DMIS_SYS_REPORT_TABLE where REPORT_ID=" + id + " order by TABLE_TYPE asc,ORDER_ID";
            DataTable dtTable = DBOpt.dbHelper.GetDataTable(_sql);
            Tables = new string[dtTable.Rows.Count, 8];
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                Tables[i, 0] = dtTable.Rows[i][0].ToString();   //id
                Tables[i, 1] = dtTable.Rows[i][1].ToString().Trim();   //����
                Tables[i, 2] = dtTable.Rows[i][2].ToString().Trim();   //������
                Tables[i, 3] = dtTable.Rows[i][3].ToString().Trim();   //��������
                Tables[i, 4] = dtTable.Rows[i][4].ToString().Trim();   //��������
                Tables[i, 5] = dtTable.Rows[i][5].ToString();   //Multi��ʱÿҳ��ʾ������
                Tables[i, 6] = dtTable.Rows[i][1].ToString().Substring(dtTable.Rows[i][1].ToString().LastIndexOf('.')+1)+i.ToString();   //ֻҪ����,����ǰ׺,��webdmis.dbo.   WEBDMIS.
                Tables[i, 7] = "";                              //�˱����ڵ�ҳ��,���ڻ�ҳʱ���� 

                //�Ҳ����ͱ���֮��Ķ�Ӧ��ϵ
                string temp = Tables[i, 4];
                string[] paraArray = temp.Split(';');
                string[] singlePara;
                for (int j = 0; j < paraArray.Length; j++)
                {
                    singlePara = paraArray[j].Split('@');
                    TablePara tp = new TablePara();
                    tp.TableName = Tables[i, 1];  //����    ��:  dmis_dd_dyqtqjl
                    tp.ColumnName=singlePara[0]; //����     ��:  TID
                    tp.ParaCode = singlePara[2]; //�������� ��:  :1
                    TableParas.Add(tp);
                }
            }


            _sql = "select ID,TABLE_NAME,COLUMN_NAME,COLUMN_DESCR,DISPLAY_PATTERN,WORDS,P,R,C,COLUMN_TYPE,TABLE_ID from DMIS_SYS_REPORT_CELL_COLUMN where REPORT_ID=" + id + " order by ORDER_ID";
            DataTable dtColumn = DBOpt.dbHelper.GetDataTable(_sql);
            Columns = new string[dtColumn.Rows.Count, 11];
            for (int i = 0; i < dtColumn.Rows.Count; i++)
            {
                Columns[i, 0] = dtColumn.Rows[i][0].ToString();   //��id
                Columns[i, 1] = dtColumn.Rows[i][1].ToString().Trim();   //����������
                Columns[i, 2] = dtColumn.Rows[i][2].ToString().Trim();   //����
                Columns[i, 3] = dtColumn.Rows[i][3].ToString().Trim();   //������,Ҳ�����б���
                Columns[i, 4] = dtColumn.Rows[i][4].ToString();   //��ʾ��ʽ
                Columns[i, 5] = dtColumn.Rows[i][5].ToString();   //������ʾ��String��
                Columns[i, 6] = dtColumn.Rows[i][6].ToString();   //ҳ��
                Columns[i, 7] = dtColumn.Rows[i][7].ToString();   //�к�
                Columns[i, 8] = dtColumn.Rows[i][8].ToString();   //�к�
                Columns[i, 9] = dtColumn.Rows[i][9].ToString();   //������
                Columns[i, 10] = dtColumn.Rows[i][10].ToString();   //��ID

                //����ʼҳ��
                for (int j = 0; j <= Tables.GetUpperBound(0); j++)   
                {
                    if (Columns[i, 10] == Tables[j, 0] && Tables[j, 7] != "") break; //�Ѿ���ֵ�Ĳ�����
                    if (Columns[i, 10] == Tables[j, 0])
                    {
                        Tables[j, 7] = Columns[i, 6];   //�˱��Ӧ��ҳ��
                        break;
                    }
                }
            }

            int k = 0;
            MultiColumns = new string[dtColumn.Rows.Count, 10];    //������û�������ͣ��϶���String��
            for (int i = 0; i < dtColumn.Rows.Count; i++)
            {
                if (Columns[i, 4] == "MultiLine" || Columns[i, 4] == "CompartLine")
                {
                    MultiColumns[k, 0] = dtColumn.Rows[i][0].ToString();
                    MultiColumns[k, 1] = dtColumn.Rows[i][1].ToString();
                    MultiColumns[k, 2] = dtColumn.Rows[i][2].ToString();
                    MultiColumns[k, 3] = dtColumn.Rows[i][3].ToString();
                    MultiColumns[k, 4] = dtColumn.Rows[i][4].ToString();
                    MultiColumns[k, 5] = dtColumn.Rows[i][5].ToString();
                    MultiColumns[k, 6] = dtColumn.Rows[i][6].ToString();
                    MultiColumns[k, 7] = dtColumn.Rows[i][7].ToString();
                    MultiColumns[k, 8] = dtColumn.Rows[i][8].ToString();
                    MultiColumns[k, 9] = dtColumn.Rows[i][10].ToString();   //��ID
                    k++;
                }
            }
        }

        public int ID
        {
            get {return _id;}
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string CellFileName
        {
            get { return _cellFileName; }
            set { _cellFileName = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }

        }

        /// <summary>
        /// �����ݿ��л�ȡ������ص�����
        /// </summary>
        /// <returns></returns>
        private DataSet GetDataSet()
        {
            //����sql�������
            string[] sqls = new string[Tables.GetUpperBound(0) + 1];
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbColumns = new StringBuilder();
            for (int j = 0; j <= Tables.GetUpperBound(0); j++)
            {
                if (Tables[j, 1] == "DMIS_SYS_REPORT_CELL_OTH_COLS") continue;    //���SCADA���������������ı�,���ܹ���SQL���,ֻ��һ��һ�еĴ���
                
                sbColumns.Remove(0, sbColumns.Length);
                sbSql.Remove(0, sbSql.Length);

                for (int m = 0; m < Columns.GetUpperBound(0) + 1; m++)  //������
                {
                    if (Columns[m, 2] == "serial_no") continue; //����в��ӵ�sql�����,����������ʱ������
                    if (Tables[j, 0] == Columns[m, 10])   //Ҫ�ñ�ID�Ƚ�,�����ñ���
                        sbColumns.Append(@Columns[m, 2] + " " + Columns[m, 3] + ",");   //���� �б���
                }
                sbSql.Append("select ");
                sbSql.Append(sbColumns.Remove(sbColumns.Length - 1, 1));
                sbSql.Append(" from ");
                sbSql.Append(Tables[j, 1] + " " + Tables[j, 6] + " ");   //�п��ܴ���ͬ���ı���,�ʱ����ټ�1λ���������Ϊ��ı���,���б����в��ܰ���.
                //����
                string wheres;
                if (Tables[j, 4] != "")
                {
                    wheres = Tables[j, 4];  //��ʽ����:convert(char(8),DATEM,112)@=@:2@And
                    for (int k = 0; k <= Paras.GetUpperBound(0); k++)
                    {
                        if (wheres.IndexOf(Paras[k, 0]) > 0)
                        {
                            if (Paras[k, 2] == "String")  //��������
                            {
                                wheres = wheres.Replace(":" + Paras[k, 0], "'" + Paras[k, 4] + "'");   //��:2����ֵ
                            }
                            else if (Paras[k, 2] == "Numeric")
                            {
                                wheres = wheres.Replace(":" + Paras[k, 0], Paras[k, 4]);
                            }
                            else if (Paras[k, 2] == "Date")
                            {
                                wheres = wheres.Replace(":" + Paras[k, 0], "'" + Paras[k, 4] + "'");
                            }
                            else
                            {
                                wheres = wheres.Replace(":" + Paras[k, 0], "'" + Paras[k, 4] + "'");
                            }
                        }
                    }
                    wheres = wheres.Replace('@', ' ');   //��@���ɿո�
                    wheres = wheres.Replace(';', ' ');   //��;���ɿո�
                    sbSql.Append(" where ");
                    sbSql.Append(wheres);
                }

                //��������
                if (Tables[j, 3].Trim() != "")
                {
                    sbSql.Append(" order by ");
                    sbSql.Append(Tables[j, 3]);
                }
                sqls[j] = sbSql.ToString();
            }

            //�����ݿ���ȡֵ
            DataSet ds = new DataSet();
            for (int j = 0; j <= Tables.GetUpperBound(0); j++)
            {
                DataTable dt = DBOpt.dbHelper.GetDataTable(sqls[j]);
                dt.TableName = Tables[j, 6];   //�ñ�ID��Ϊ Dataset�еı���,�������������ı���,��Ϊ��ͬһ����������ͬ�����ݿ��
                ds.Tables.Add(dt);
            }
            return ds;
        }

        /// <summary>
        /// ��ʾ��������
        /// </summary>
        /// <returns></returns>
        public string Display()
        {
            string ret="";

            //��ȡ����
            ReportData = GetDataSet();
            PagesOrRows = "";
            switch (_type)
            {
                case "Single":
                    ret = DisplaySingle();
                    break;
                case "MultiFixed":
                    ret = DisplayMultiFix();
                    break;
                case "MultiFlexible":
                    break;
                default :
                    break;
            }
            return ret;
        }

        /// <summary>
        /// ��ʾSingle����
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string DisplaySingle()
        {
            StringBuilder sbValues = new StringBuilder();
            string columnType="String";
            string value;
            for (int i = 0; i <= Tables.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= Columns.GetUpperBound(0); j++)
                {
                    if (Columns[j, 10] != Tables[i, 0]) continue;  //��ID�Ƚ�,�����ñ���
                    if (ReportData.Tables[Tables[i, 6]].Rows.Count > 0)    //�����ݲ�����,ֻ���ɵ�һ����¼������
                    {
                        //�������û�������кš��кš�ҳ�������ɿͻ��˵Ĵ���
                        int r1, c1, p1;
                        if (Columns[j, 7] == null || Columns[j, 8] == null || Columns[j, 6] == null) continue;
                        if (!int.TryParse(Columns[j, 7], out r1) || !int.TryParse(Columns[j, 8], out c1) || !int.TryParse(Columns[j, 6], out p1)) continue;
                        //��ʽ:  �к�^�к�^ҳ��^����^����^��ʾ��ʽ���к�^�к�^ҳ��^����^����^��ʾ��ʽ���к�^�к�^ҳ��^����^����^��ʾ��ʽ.................
                        if (ReportData.Tables[Tables[i, 6]].Rows[0][Columns[j, 3]] != Convert.DBNull)
                        {
                            if (Columns[j, 9] == "Time")
                            {
                                columnType = "Time";
                                value = Convert.ToDateTime(ReportData.Tables[Tables[i, 6]].Rows[0][Columns[j, 3]]).ToShortTimeString();
                            }
                            else if (Columns[j, 9] == "Numeric")
                            {
                                columnType = "Numeric";
                                value = ReportData.Tables[Tables[i, 6]].Rows[0][Columns[j, 3]].ToString();
                            }
                            else if (Columns[j, 9] == "Date")
                            {
                                columnType = "Date";
                                //value = Convert.ToDateTime(ReportData.Tables[Tables[i, 6]].Rows[0][Columns[j, 3]]).ToShortDateString();
                                columnType = "DateTime";
                                CultureInfo ci = new CultureInfo("es-ES");
                                try
                                {
                                    DateTime dt = DateTime.Parse(ReportData.Tables[Tables[i, 6]].Rows[0][Columns[j, 3]].ToString(), ci);
                                    value = dt.ToString("dd-MM-yyyy");
                                }
                                catch
                                {
                                    value = "";
                                }
                            }
                            else if (Columns[j, 9] == "DateTime")
                            {
                                columnType = "DateTime";
                                //value = Convert.ToDateTime(ReportData.Tables[Tables[i, 6]].Rows[0][Columns[j, 3]]).ToString("dd-MM-yy HH:mm");
                                columnType = "DateTime";
                                CultureInfo ci = new CultureInfo("es-ES");
                                try
                                {
                                    DateTime dt = DateTime.Parse(ReportData.Tables[Tables[i, 6]].Rows[0][Columns[j, 3]].ToString(), ci);
                                    value = dt.ToString("dd-MM-yyyy HH:mm");
                                }
                                catch
                                {
                                    value = "";
                                }
                            }
                            else
                            {
                                columnType = "String";
                                value = ReportData.Tables[Tables[i, 6]].Rows[0][Columns[j, 3]].ToString();
                            }
                        }
                        else
                        {
                            value = "";
                        }
                        sbValues.Append(Columns[j, 7] + "^" + Columns[j, 8] + "^" + Columns[j, 6] + "^" + value + "^" + columnType + "^" + Columns[j, 4] + "��");
                    }
                }
            }

            //�����Ƿ���SCADA������
            int no = IsExitTable_Other_Columns();
            if (no >= 0)
            {
                //��ȡSCADA�����ݻ����Ժ���
                //GetOtherColumnsData(no);
            }

            return sbValues.ToString();
        }

        /// <summary>
        /// ��ʾMultiFixed�ı���
        /// </summary>
        /// <returns></returns>
        private string DisplayMultiFix()
        {
            #region 1����ɨ��Multi�ı�,��������кͻ����С�
            bool isSerial_no;
            for (int i = 0; i <= Tables.GetUpperBound(0); i++)
            {
                if (Tables[i, 2] != "Multi") break;   //����Tables�Ѿ�����,��ѭ����Single�ı����ٴ�����

                //������������,���ȼӴ���
                isSerial_no = false;
                for (int j = 0; j <= Columns.GetUpperBound(0); j++)
                {
                    if (Columns[j, 1] == Tables[i, 1] && Columns[j, 2] == "serial_no")
                    {
                        isSerial_no = true;
                        break;
                    }
                }
                if (isSerial_no)  //���������,�����Ӵ���
                {
                    DataColumn colSerial_no = new DataColumn("serial_no");
                    colSerial_no.DataType = System.Type.GetType("System.Int16");
                    ReportData.Tables[Tables[i, 6]].Columns.Add(colSerial_no);
                    for (int s = 0; s < ReportData.Tables[Tables[i, 6]].Rows.Count; s++)  //����Ÿ�ֵ
                    {
                        ReportData.Tables[Tables[i, 6]].Rows[s]["serial_no"] = s + 1;
                    }
                }

                //�Ҵ˱��Ӧ�Ļ�����
                int words = 0;         //������ÿ����ʾ��String��,��ȫ��״̬�µ�String����
                string content;
                string temp;
                byte[] bytes;
                for (int k = 0; k <= MultiColumns.GetUpperBound(0); k++)
                {
                    if (MultiColumns[k, 1] == "" || MultiColumns[k, 1] == null) break;  //û�л����оͲ���ѭ����,Ч�ʸ�Щ
                    if (MultiColumns[k, 9] != Tables[i, 0]) continue;   //�����в��Ǵ˱�ID��ʱ,���ô���

                    if (!int.TryParse(MultiColumns[k, 5], out words)) break;   //������û����������ʱ,������
                    //words��ȫ��״̬�µ�String������
                    words = 2 * words;
                    for (int j = 0; j < ReportData.Tables[Tables[i, 6]].Rows.Count; j++)
                    {
                        if (ReportData.Tables[Tables[i, 6]].Rows[j][MultiColumns[k, 3]] == Convert.DBNull)
                        {
                            content = "";
                        }
                        else
                        {
                            content = ReportData.Tables[Tables[i, 6]].Rows[j][MultiColumns[k, 3]].ToString();
                            content = content.Replace("\r\n", "");
                        }
                        bytes = Encoding.Default.GetBytes(content);

                        if (bytes.Length > words)
                        {
                            temp = Encoding.Default.GetString(bytes, 0, words);
                            if (temp[temp.Length - 1] == '?')  //���ֱ��г�һ��
                            {
                                ReportData.Tables[Tables[i, 6]].Rows[j][MultiColumns[k, 3]] = Encoding.Default.GetString(bytes, 0, words - 1);
                                temp = Encoding.Default.GetString(bytes, words-1, bytes.Length-words+1);
                                bytes = Encoding.Default.GetBytes(temp);
                            }
                            else
                            {
                                ReportData.Tables[Tables[i, 6]].Rows[j][MultiColumns[k, 3]] = temp;
                                temp = Encoding.Default.GetString(bytes, words, bytes.Length - words );
                                bytes = Encoding.Default.GetBytes(temp);
                            }
                            while (bytes.Length > 0)
                            {
                                if (ReportData.Tables[Tables[i, 6]].Rows.Count == j + 1)  //�������һ��,������������
                                {
                                    DataRow drow = ReportData.Tables[Tables[i, 6]].NewRow();
                                    if (bytes.Length > words)
                                    {
                                        temp = Encoding.Default.GetString(bytes, 0, words);
                                        if (temp[temp.Length - 1]== '?')
                                        {
                                            drow[MultiColumns[k, 3]] = Encoding.Default.GetString(bytes, 0, words - 1);
                                            //temp = Encoding.Default.GetString(bytes, words, bytes.Length - words + 1);  //���л����
                                            temp = Encoding.Default.GetString(bytes, words-1, bytes.Length - words+1);
                                            bytes = Encoding.Default.GetBytes(temp);
                                        }
                                        else
                                        {
                                            drow[MultiColumns[k, 3]] = temp;
                                            temp = Encoding.Default.GetString(bytes, words, bytes.Length - words ); 
                                            bytes = Encoding.Default.GetBytes(temp);
                                        }
                                    }
                                    else
                                    {
                                        drow[MultiColumns[k, 3]] = Encoding.Default.GetString(bytes);
                                        temp = "";
                                        bytes = Encoding.Default.GetBytes(temp);
                                    }
                                    j++;
                                    ReportData.Tables[Tables[i, 6]].Rows.InsertAt(drow, j);
                                }
                                else
                                {
                                    //���һ���������������У���ڶ��л���ʱ����һ��ǡ���ǵ�һ�������в��������У���
                                    //��Ҫ�����������ˡ�
                                    if (ReportData.Tables[Tables[i, 6]].Rows[j + 1].RowState == DataRowState.Added)
                                    {
                                        if (bytes.Length > words)
                                        {
                                            temp = Encoding.Default.GetString(bytes, 0, words);
                                            if (temp[temp.Length - 1]== '?')
                                            {
                                                ReportData.Tables[Tables[i, 6]].Rows[j + 1][MultiColumns[k, 3]] = Encoding.Default.GetString(bytes, 0, words - 1);
                                                temp = Encoding.Default.GetString(bytes, words-1, bytes.Length - words);
                                                bytes = Encoding.Default.GetBytes(temp);

                                            }
                                            else
                                            {
                                                ReportData.Tables[Tables[i, 6]].Rows[j + 1][MultiColumns[k, 3]] = temp;
                                                temp = Encoding.Default.GetString(bytes, words, bytes.Length - words );
                                                bytes = Encoding.Default.GetBytes(temp);

                                            }
                                        }
                                        else
                                        {
                                            ReportData.Tables[Tables[i, 6]].Rows[j + 1][MultiColumns[k, 3]] = Encoding.Default.GetString(bytes);
                                            temp = "";
                                            bytes = Encoding.Default.GetBytes(temp);
                                        }
                                        j++;
                                    }
                                    else   //�������еķ�ʽ
                                    {
                                        DataRow drow = ReportData.Tables[Tables[i, 6]].NewRow();
                                        if (bytes.Length > words)
                                        {
                                            temp = Encoding.Default.GetString(bytes, 0, words);
                                            if (temp[temp.Length - 1]== '?')
                                            {
                                                drow[MultiColumns[k, 3]] = Encoding.Default.GetString(bytes, 0, words - 1);
                                                //temp = Encoding.Default.GetString(bytes, words, bytes.Length - words + 1);  //���л����
                                                temp = Encoding.Default.GetString(bytes, words-1, bytes.Length - words+1 );
                                                bytes = Encoding.Default.GetBytes(temp);

                                            }
                                            else
                                            {
                                                drow[MultiColumns[k, 3]] = temp;
                                                temp = Encoding.Default.GetString(bytes, words, bytes.Length - words);
                                                bytes = Encoding.Default.GetBytes(temp);

                                            }
                                        }
                                        else
                                        {
                                            drow[MultiColumns[k, 3]] = Encoding.Default.GetString(bytes);
                                            temp = "";
                                            bytes = Encoding.Default.GetBytes(temp);
                                        }
                                        j++;
                                        ReportData.Tables[Tables[i, 6]].Rows.InsertAt(drow, j);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion


            #region 2�����ɶ��¼��ı�Ŀͻ��˴���
            //���ڶ��¼����ܴ�������ҳ���������������ҳ��Single�������ҲҪ��ʾ�����ȴ�����¼�������
            StringBuilder sbValues = new StringBuilder();
            Int16 curPageNo = 0;        //���¼��ǰ��ҳ��
            int addPages = 0;      //һ�����ӵ�ҳ����,Ҫ�����ͻ��˵����ؿؼ�,�Ա��ڿͻ��˾����Ƿ�Ҫ���Ӷ���ҳ
            string columnType="String";
            string value;
            Hashtable tableIdPages = new Hashtable();

            for (int i = 0; i <= Tables.GetUpperBound(0); i++)
            {
                if (Tables[i, 2] != "Multi") break;   //����Tables�Ѿ�����,��ѭ����Single�ı����ٴ�����
                if (!Int16.TryParse(Tables[i, 7], out curPageNo)) curPageNo = 0;
                
                //׷��ҳ�ķ�ʽ
                int startRow = 0;      //���¼��Ŀ�ʼ�к�
                int curRow = 0;        //���¼��ĵ�ǰ�к�

                int MultiTabelPerPageRows ;   //���¼��ÿҳ��ʾ������
                if(!int.TryParse(Tables[i, 5],out MultiTabelPerPageRows)) break;  //���¼��û������ÿ��ҳ��ʾ������,����ʾ
                for (int p = 0; p <= Columns.GetUpperBound(0); p++)          //�Ҷ��¼��ʼ�к�
                {
                    if (Columns[p, 10] == Tables[i, 0])
                    {
                        startRow = Convert.ToInt16(Columns[p, 7]);
                        curRow = startRow;
                        break;
                    }
                }

                for (int k = 0; k < ReportData.Tables[Tables[i, 6]].Rows.Count; k++)
                {
                    if ((curRow - startRow) >= MultiTabelPerPageRows)
                    {
                        curRow = startRow;
                        curPageNo++;
                        addPages++;
                    }
                    for (int h = 0; h <= Columns.GetUpperBound(0); h++)
                    {
                        if (Columns[h, 10] != Tables[i, 0]) continue;
                        //���ڶ��¼��,�������û�������к������ɿͻ��˵Ĵ���
                        int c1;
                        if (Columns[h, 8] == null) continue;
                        if (!int.TryParse(Columns[h, 8], out c1)) continue;

                        if (ReportData.Tables[Tables[i, 6]].Rows[k][Columns[h, 3]] != Convert.DBNull)
                        {
                            if (Columns[h, 9] == "Time")
                            {
                                columnType = "Time";
                                value = Convert.ToDateTime(ReportData.Tables[Tables[i, 6]].Rows[k][Columns[h, 3]]).ToShortTimeString();
                            }
                            else if (Columns[h, 9] == "Numeric")
                            {
                                columnType = "Numeric";
                                value = ReportData.Tables[Tables[i, 6]].Rows[k][Columns[h, 3]].ToString();
                            }
                            else if (Columns[h, 9] == "Date")
                            {
                                columnType = "Date";
                                //value = Convert.ToDateTime(ReportData.Tables[Tables[i, 6]].Rows[k][Columns[h, 3]]).ToShortDateString();
                                CultureInfo ci = new CultureInfo("es-ES");
                                try
                                {
                                    DateTime dt = DateTime.Parse(ReportData.Tables[Tables[i, 6]].Rows[k][Columns[h, 3]].ToString(), ci);
                                    value = dt.ToString("dd-MM-yyyy");
                                }
                                catch
                                {
                                    value = "";
                                }
                            }
                            else if (Columns[h, 9] == "DateTime")
                            {
                                columnType = "DateTime";
                                CultureInfo ci = new CultureInfo("es-ES");
                                try
                                {
                                    DateTime dt = DateTime.Parse(ReportData.Tables[Tables[i, 6]].Rows[k][Columns[h, 3]].ToString(), ci);
                                    value = dt.ToString("dd-MM-yyyy HH:mm");
                                }
                                catch
                                {
                                    value = "";
                                }
                                //value = Convert.ToDateTime(ReportData.Tables[Tables[i, 6]].Rows[k][Columns[h, 3]]).ToString("dd-MM-yy HH:mm");
                            }
                            else
                            {
                                columnType = "String";
                                value = ReportData.Tables[Tables[i, 6]].Rows[k][Columns[h, 3]].ToString();
                            }
                        }
                        else
                        {
                            if (Columns[h, 9] == "Time")
                                columnType = "Time";
                            else if (Columns[h, 9] == "Numeric")
                                columnType = "Numeric";
                            else if (Columns[h, 9] == "Date")
                                columnType = "Date";
                            else if (Columns[h, 9] == "DateTime")
                                columnType = "DateTime";
                            else
                                columnType = "String";

                            value = "";
                        }
                        sbValues.Append(curRow + "^" + Columns[h, 8] + "^" + curPageNo + "^" + value + "^" + columnType + "^" + Columns[h, 4] + "��");
                    }
                    curRow++;
                }

                tableIdPages.Add(Tables[i, 7], addPages);   //�˶�Ӧ���¼��ĳ�ʼҳ�ż�����Ӧ���ӵ�ҳ��
            }
            #endregion


            #region 3������Single�ı�Ŀͻ��˴���
            //���Ҵ˵���¼������¼�������˶���ҳ;
            //��ÿҳ���ɴ���;
            //�������ڵ�ʵ��˼��,ͬһSingle����ֻ�ܷ���ͬһҳ����.
            for (int i = 0; i <= Tables.GetUpperBound(0); i++)
            {
                if (Tables[i, 2] == "Multi") continue;   //����Tables�Ѿ�����,��ѭ����Single�ı��ٴ���

                //��ѯ���¼���ڵ�ҳ��,�˶�Ӧ��ҳ�������˶���ҳ
                Int16 pages = 0;
                Int16 curPage = 0;
                foreach (DictionaryEntry e in tableIdPages)
                {
                    if (e.Key.ToString() == Tables[i, 7])   //����¼�����ڵ�ҳ�źʹ˶��¼�����ڵ�ҳ����ͬ
                    {
                        Int16.TryParse(e.Value.ToString(),out pages);
                        break;
                    }
                }
                if (!Int16.TryParse(Tables[i, 7], out curPage))
                    curPage = 0;
                
                for (int j = curPage; j <= curPage+pages; j++)   //��ɨ��ҳ
                {
                    for (int p = 0; p <= Columns.GetUpperBound(0); p++)   //��ɨ����
                    {
                        if (Columns[p, 10] != Tables[i, 0]) continue;
                        if (ReportData.Tables[Tables[i, 6]].Rows.Count > 0)  //�����ݲ�����,ֻ���ɵ�һ����¼������
                        {
                            //�������û�������кš��кš�ҳ�������ɿͻ��˵Ĵ���
                            int r1, c1, p1;
                            if (Columns[p, 7] == null || Columns[p, 8] == null || Columns[p, 6] == null) continue;
                            if (!int.TryParse(Columns[p, 7], out r1) || !int.TryParse(Columns[p, 8], out c1) || !int.TryParse(Columns[p, 6], out p1)) continue;
                            if (ReportData.Tables[Tables[i, 6]].Rows[0][Columns[p, 3]] != Convert.DBNull)
                            {
                                if (Columns[p, 9] == "Time")
                                {
                                    columnType = "Time";
                                    value = Convert.ToDateTime(ReportData.Tables[Tables[i, 6]].Rows[0][Columns[p, 3]]).ToShortTimeString();
                                }
                                else if (Columns[p, 9] == "Numeric")
                                {
                                    columnType = "Numeric";
                                    value = ReportData.Tables[Tables[i, 6]].Rows[0][Columns[p, 3]].ToString();
                                }
                                else if (Columns[p, 9] == "Date")
                                {
                                    columnType = "Date";
                                    value = Convert.ToDateTime(ReportData.Tables[Tables[i, 6]].Rows[0][Columns[p, 3]]).ToShortDateString();
                                }
                                else if (Columns[p, 9] == "DateTime")
                                {
                                    columnType = "DateTime";
                                    value = Convert.ToDateTime(ReportData.Tables[Tables[i, 6]].Rows[0][Columns[p, 3]]).ToString("dd-MM-yy HH:mm");
                                }
                                else
                                {
                                    columnType = "String";
                                    value = ReportData.Tables[Tables[i, 6]].Rows[0][Columns[p, 3]].ToString();
                                }
                            }
                            else
                            {
                                if (Columns[p, 9] == "Time")
                                    columnType = "Time";
                                else if (Columns[p, 9] == "Numeric")
                                    columnType = "Numeric";
                                else if (Columns[p, 9] == "Date")
                                    columnType = "Date";
                                else if (Columns[p, 9] == "DateTime")
                                    columnType = "DateTime";
                                else
                                    columnType = "String";

                                value = "";
                            }

                            //��ʽ:  �к�^�к�^ҳ��^����^����^��ʾ��ʽ���к�^�к�^ҳ��^����^����^��ʾ��ʽ���к�^�к�^ҳ��^����^����^��ʾ��ʽ.................
                            sbValues.Append(Columns[p, 7] + "^" + Columns[p, 8] + "^" + j + "^" + value + "^" + columnType + "^" + Columns[p, 4] + "��");
                        }
                    }
                }
            }

            //���ɿͻ�����ص�ҳ��������Ϣ.
            foreach (DictionaryEntry e in tableIdPages)
            {
                PagesOrRows += e.Key.ToString() + "^" + e.Value.ToString() + "��";
            }

            return sbValues.ToString();
            #endregion
 
        }

        /// <summary>
        /// ��ʾMultiFlexible�ı���
        /// </summary>
        /// <returns></returns>
        private string DisplayMultiFlex()
        {
            return "";
        }

        /// <summary>
        /// �жϱ�����ص����ݿ�����Ƿ����DMIS_SYS_REPORT_CELL_OTH_COLS,���򷵻�����Tables�е����,���򷵻�-1
        /// </summary>
        /// <returns>Tables�е����</returns>
        private int IsExitTable_Other_Columns()
        {
            for (int i = 0; i <= Tables.GetUpperBound(0); i++)
            {
                if (Tables[i, 1] == "DMIS_SYS_REPORT_CELL_OTH_COLS") return i;
            }
            return -1;
        }

        /// <summary>
        /// ��ʾDMIS_SYS_REPORT_CELL_OTH_COLS�е�����
        /// </summary>
        /// <param name="no">DMIS_SYS_REPORT_CELL_OTH_COLS�ڱ���������ݿ���е����</param>
        /// <returns></returns>
        private string GetOtherColumnsData(int no)
        {
            return "";
        }


    }


    /// <summary>
    /// ������ṹ,��Ϊ��CellReport�ĳ�Ա����
    /// </summary>
    [Serializable]
    public struct TablePara
    {
        public string TableName; //���� 
        public string ColumnName;//����
        public string ParaCode;//�������� :1
    }
}

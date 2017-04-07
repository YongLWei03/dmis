using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;
using PlatForm.DBUtility;

namespace PlatForm.DmisReport
{
    /// <summary>
    /// ˮ��������,����Ҫ����,��CrystalReport�����
    /// </summary>
    [Serializable]
    public class DmisReport
    {
        private string _sql;

        public string ID;             //����ɣ�
        public string name;           //��������
        public string fileName;        //�����ˮ�������ļ�����ȫ����·��
        public string type;           //��������
        public string[] paraValues;   //������ֵ������ʾʱ��ֵ
        public string[,] paras;       //����
        public string[,] tables;      //��
        public string[,] tableParas;  //��ͱ������֮��Ĺ�ϵ
        public string[,] columns;     //��
        public string[,] multi_columns;//������Ŷ��к͸��е���
        public string[] tableSql;      //����ģӣѣ����

        public DmisReport()
        {

        }

        public DmisReport(string id)
        {
            _sql = "select NAME,FILE_NAME,TYPE from DMIS_SYS_REPORT where ID=" + id;
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
            dr.Read();
            ID = id;
            
            name = dr[0].ToString();
            fileName = dr[1]!=Convert.DBNull? dr[1].ToString():"";
            type = dr[2]!=Convert.DBNull?dr[2].ToString():"";
            dr.Close();

            _sql = "select ID,DESCR,PARA_TYPE,DEPEND_ID from DMIS_SYS_REPORT_PARA where REPORT_ID=" + id + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            paras=new string[dt.Rows.Count,4];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                paras[i, 0] = dt.Rows[i][0].ToString();
                paras[i, 1] = dt.Rows[i][1].ToString();
                paras[i, 2] = dt.Rows[i][2].ToString();
                paras[i, 3] = dt.Rows[i][3].ToString();
            }

            _sql = "select ID,TABLE_NAME,TABLE_TYPE,TABLE_ORDERS,TABLE_FILTER_WHERE,TABLE_PAGE_ROWS from DMIS_SYS_REPORT_TABLE where REPORT_ID=" + id + " order by ORDER_ID";
            DataTable dtTable = DBOpt.dbHelper.GetDataTable(_sql);
            tables = new string[dtTable.Rows.Count, 6];
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                tables[i, 0] = dtTable.Rows[i][0].ToString();
                tables[i, 1] = dtTable.Rows[i][1].ToString();
                tables[i, 2] = dtTable.Rows[i][2].ToString();
                tables[i, 3] = dtTable.Rows[i][3].ToString();
                tables[i, 4] = dtTable.Rows[i][4].ToString();
                tables[i, 5] = dtTable.Rows[i][5].ToString();

                //�Ҳ����ͱ���֮��Ķ�Ӧ��ϵ
                string temp = tables[i, 4];
                string [] paraArray=temp.Split(';');
                string[] singlePara;
                tableParas = new string[paraArray.Length, 3];
                for (int j = 0; j < paraArray.Length; j++)
                {
                    singlePara=paraArray[j].Split('@');
                    tableParas[j, 0] = tables[i, 1];  //����     dmis_dd_dyqtqjl
                    tableParas[j, 1] = singlePara[0]; //����     tid
                    tableParas[j, 2] = singlePara[2]; //�������� :1
                }
            }


            _sql = "select ID,TABLE_NAME,COLUMN_NAME,COLUMN_DESCR,DISPLAY_PATTERN,WORDS from DMIS_SYS_REPORT_COLUMN where REPORT_ID=" + id + " order by ORDER_ID";
            DataTable dtColumn = DBOpt.dbHelper.GetDataTable(_sql);
            columns = new string[dtColumn.Rows.Count, 6];
            for (int i = 0; i < dtColumn.Rows.Count; i++)
            {
                columns[i, 0] = dtColumn.Rows[i][0].ToString();
                columns[i, 1] = dtColumn.Rows[i][1].ToString();
                columns[i, 2] = dtColumn.Rows[i][2].ToString();
                columns[i, 3] = dtColumn.Rows[i][3].ToString();
                columns[i, 4] = dtColumn.Rows[i][4].ToString();
                columns[i, 5] = dtColumn.Rows[i][5].ToString();
            }

            int k = 0;
            multi_columns = new string[dtColumn.Rows.Count, 6];
            for (int i = 0; i < dtColumn.Rows.Count; i++)
            {
                if (columns[i, 4] == "����" || columns[i, 4] == "����")
                {
                    multi_columns[k, 0] = dtColumn.Rows[i][0].ToString();
                    multi_columns[k, 1] = dtColumn.Rows[i][1].ToString();
                    multi_columns[k, 2] = dtColumn.Rows[i][2].ToString();
                    multi_columns[k, 3] = dtColumn.Rows[i][3].ToString();
                    multi_columns[k, 4] = dtColumn.Rows[i][4].ToString();
                    multi_columns[k, 5] = dtColumn.Rows[i][5].ToString();
                    k++;
                }
            }

            //����SQL�������
            string cols;
           // StringBuilder cols=new StringBuilder();
            tableSql = new string[tables.GetUpperBound(0)+1];
            for (int i = 0; i <= tables.GetUpperBound(0); i++)
            {
                cols = "";
                for (int j = 0; j <= columns.GetUpperBound(0); j++)
                {
                    if (columns[j, 1] == tables[i, 1])
                        cols=cols+ columns[j, 2] + ",";
                }
                if (cols.ToString() == "")
                    cols = "*";
                else
                    cols = cols.Substring(0, cols.Length - 1);

                tableSql[i] = "select " + cols + " from " + tables[i, 1];
                if (tables[i, 4] != "")  //where���
                {
                    string temp=tables[i, 4];
                    temp = temp.Replace('@',' ');
                    temp = temp.Replace(';',' ');
                    temp = temp.Substring(0, temp.Length - 3);  //ȥ�����һ��And��Or
                    tableSql[i] = tableSql[i] + " where " + temp;
                }
                if (tables[i, 3] != "") //order���
                {
                    tableSql[i] = tableSql[i] + " order by " + tables[i, 3];
                }
            }
        }


        /// <summary>
        /// ���ݱ������͵õ����ݼ�
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet(params string[] paraAndValues)
        {
            paraValues = paraAndValues;
            if (type == "����¼")
            {
                return GetSingle();
                //return ManufactureDataSet();
            }
            else if (type == "���¼�̶���ʽ")
            {
                return GetFix();
            }
            else if (type == "���¼������ʽ")
            {
               return GetFlex();
            }
            else if (type == "����Ʊ")
            {
                return new DataSet();
            }
            else
            {
                return new DataSet();
            }

        }
        /// <summary>
        /// ����¼
        /// </summary>
        /// <returns></returns>
        public DataSet GetSingle()
        {
            string[] sql = new string[tableSql.Length];
            string[] singleParaValue;   //һ���������Ӧֵ
            string temp;
            for (int j = 0; j < tableSql.Length; j++)
            {
                temp = tableSql[j];
                for (int i = 0; i < paraValues.Length; i++)
                {
                    singleParaValue = paraValues[i].Split('@');
                    temp = temp.Replace(":" + singleParaValue[0], "'" + singleParaValue[1] + "'");
                }
                sql[j] = temp;
            }
            DataSet ds = DBOpt.dbHelper.GetDataSet(sql);
            return ds;
        }


        /// <summary>
        /// ���¼�̶���ʽ������һҳ��ʾ����ʱ��������
        /// </summary>
        /// <param name="paraAndValues"></param>
        /// <returns></returns>
        public DataSet GetFix()
        {
            string[] sql = new string[tableSql.Length];
            string[] singleParaValue;   //һ���������Ӧֵ
            string temp;
            for (int j = 0; j < tableSql.Length; j++)
            {
                temp = tableSql[j];
                for (int i = 0; i < paraValues.Length; i++)
                {
                    singleParaValue = paraValues[i].Split('@');
                    temp = temp.Replace(":" + singleParaValue[0], "'" + singleParaValue[1] + "'");
                }
                sql[j] = temp;
            }
            DataSet ds = DBOpt.dbHelper.GetDataSet(sql);

            //���д���
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (!IsMultiTable(ds.Tables[i].TableName)) continue;

                for (int j = 0; j <= multi_columns.GetUpperBound(0); j++)
                {
                    if (multi_columns[j, 1] != ds.Tables[i].TableName) continue;  //���Ǵ˱�Ļ����У��򲻴���
                    if (multi_columns[j, 0] == "") break;                       //���������β�����˳���ѭ��
                    if (multi_columns[j, 5] == "") continue;                    //û�����ö��ٸ��������򲻴���

                    int currentRow = 0;
                    int words = Convert.ToInt16(multi_columns[j, 5]);
                    for (int k = 0; k < ds.Tables[i].Rows.Count; k++)
                    {
                        k = currentRow;

                        temp = ds.Tables[i].Rows[k][multi_columns[j, 2]].ToString();
                        if (temp.Length > words)
                        {
                            ds.Tables[i].Rows[k][multi_columns[j, 2]] = temp.Substring(0, words);
                            temp = temp.Substring(words);
                            while (temp.Length > 0)
                            {
                                DataRow drow = ds.Tables[i].NewRow();
                                if (temp.Length > words)
                                {
                                    drow[multi_columns[j, 2]] = temp.Substring(0, words);
                                    temp = temp.Substring(words);
                                }
                                else
                                {
                                    drow[multi_columns[j, 2]] = temp;
                                    temp = "";
                                }
                                currentRow = currentRow + 1;
                                ds.Tables[i].Rows.InsertAt(drow, currentRow);
                            }
                        }
                        else
                        {
                            currentRow = currentRow + 1;
                        }
                    }
                }
            }


            //һҳ������ʾ����ʱ���������
            int pageRows = 0;
            int mod;
            for (int m = 0; m <= tables.GetUpperBound(0); m++)
            {
                if (IsMultiTable(tables[m, 1]))
                {
                    pageRows = Convert.ToInt16(tables[m, 5]);
                    mod = ds.Tables[tables[m, 1]].Rows.Count % pageRows;
                    for (int n = 0; n < (pageRows - mod); n++)
                    {
                        DataRow drow = ds.Tables[tables[m, 1]].NewRow();
                        ds.Tables[tables[m, 1]].Rows.Add(drow);
                    }
                }
            }
            return ds;
        }


        /// <summary>
        /// ���¼������ʽ������һҳ��ʾ����ʱ����������
        /// </summary>
        /// <returns></returns>
        public DataSet GetFlex()
        {
            string[] sql = new string[tableSql.Length];
            string[] singleParaValue;   //һ���������Ӧֵ
            string temp;
            for (int j = 0; j < tableSql.Length; j++)
            {
                temp = tableSql[j];
                for (int i = 0; i < paraValues.Length; i++)
                {
                    singleParaValue = paraValues[i].Split('@');
                    temp = temp.Replace(":" + singleParaValue[0], "'" + singleParaValue[1] + "'");
                }
                sql[j] = temp;
            }
            DataSet ds = DBOpt.dbHelper.GetDataSet(sql);

            //���д���
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (!IsMultiTable(ds.Tables[i].TableName)) continue;
                
                for (int j = 0; j <= multi_columns.GetUpperBound(0); j++)
                {
                    if (multi_columns[j, 1]!=ds.Tables[i].TableName) continue;  //���Ǵ˱�Ļ����У��򲻴���
                    if (multi_columns[j, 0] == "") break;                       //���������β�����˳���ѭ��
                    if (multi_columns[j, 5] == "") continue;                    //û�����ö��ٸ��������򲻴���
                    
                    int currentRow = 0;
                    int words = Convert.ToInt16(multi_columns[j, 5]);
                    for (int k = 0; k < ds.Tables[i].Rows.Count; k++)
                    {
                        k = currentRow;

                        temp = ds.Tables[i].Rows[k][multi_columns[j, 2]].ToString();
                        if (temp.Length > words)
                        {
                            ds.Tables[i].Rows[k][multi_columns[j, 2]] = temp.Substring(0, words);
                            temp = temp.Substring(words);
                            while (temp.Length > 0)
                            {
                                DataRow drow = ds.Tables[i].NewRow();
                                if (temp.Length > words)
                                {
                                    drow[multi_columns[j, 2]] = temp.Substring(0, words);
                                    temp = temp.Substring(words);
                                }
                                else
                                {
                                    drow[multi_columns[j, 2]] = temp;
                                    temp = "";
                                }
                                currentRow = currentRow + 1;
                                ds.Tables[i].Rows.InsertAt(drow, currentRow);
                            }
                        }
                        else
                        {
                            currentRow = currentRow + 1;
                        }
                    }
                }
            }
            return ds;
        }


        public DataTable GetDataTable(params string[] paraAndValues)
        {
            string[] sql = new string[tableSql.Length];
            string[] singleParaValue;
            string temp;
            for (int j = 0; j < tableSql.Length; j++)
            {
                temp = tableSql[j];
                for (int i = 0; i < paraAndValues.Length; i++)
                {
                    singleParaValue = paraAndValues[i].Split('@');
                    temp = temp.Replace(":" + singleParaValue[0], "'" + singleParaValue[1] + "'");
                }
                sql[j] = temp;
            }

            DataTable dt = DBOpt.dbHelper.GetDataTable(sql[0]);
            int startPos, endPos;
            startPos = sql[0].IndexOf(" from ") + 6;    //sql����е�fromһ��ҪСд
            endPos = sql[0].IndexOf(" ", startPos);
            dt.TableName = sql[0].Substring(startPos, endPos - startPos);


            //���д���
            for (int i = 0; i <= multi_columns.GetUpperBound(0); i++)
            {
                if (multi_columns[i, 0] == "") break;   //�������ݵ�β�����˳�ѭ��

                //if (multi_columns[i, 1] == dt.TableName)
                if (multi_columns[i, 1] == dt.TableName)
                {
                    if (multi_columns[i, 5] == "") continue;   //û�����ö��ٸ��������򲻴���
                    int currentRow = 0;
                    int words = Convert.ToInt16(multi_columns[i, 5]);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        j = currentRow;

                        temp = dt.Rows[j][multi_columns[i, 2]].ToString();
                        if (temp.Length > words)
                        {
                            dt.Rows[j][multi_columns[i, 2]] = temp.Substring(0, words);
                            temp = temp.Substring(words);
                            while (temp.Length > 0)
                            {
                                DataRow drow = dt.NewRow();
                                if (temp.Length > words)
                                {
                                    drow[multi_columns[i, 2]] = temp.Substring(0, words);
                                    temp = temp.Substring(words);
                                }
                                else
                                {
                                    drow[multi_columns[i, 2]] = temp;
                                    temp = "";
                                }
                                currentRow = currentRow + 1;
                                dt.Rows.InsertAt(drow, currentRow);
                            }
                        }
                        else
                        {
                            currentRow = currentRow + 1;
                        }
                    }
                }
            }


            //һҳ������ʾ����ʱ���������
            int pageRows = 0;
            for (int i = 0; i <= tables.GetUpperBound(0); i++)
            {
                if (sql[0].IndexOf(tables[i, 1]) > 0)
                {
                    if (tables[i, 2].Trim() == "���¼")   //why���˸��ո�
                    {
                        pageRows = Convert.ToInt16(tables[i, 5]);
                        break;
                    }
                }
            }
            if (pageRows > 0)
            {
                int mod = dt.Rows.Count % pageRows;
                for (int i = 0; i < (pageRows - mod); i++)
                {
                    DataRow drow = dt.NewRow();
                    dt.Rows.Add(drow);
                }
            }

            return dt;
        }


        //�жϱ���Ƕ��¼��
        public bool IsMultiTable(string tableName)
        {
            for (int i = 0; i <= tables.GetUpperBound(0); i++)
            {
                if (tables[i, 1] == tableName)
                {
                    if (tables[i, 2] == "����¼")
                        return false;
                    else
                        return true;
                }
            }
            return false;
        }

        //������
        public DataSet ManufactureDataSet(params string[] paraAndValues)
        {
            DataSet ds = new DataSet();
            for (int i = 0; i <= tables.GetUpperBound(0); i++)
            {
                DataTable dt = new DataTable(tables[i,1]);
                for (int j = 0; j <= columns.GetUpperBound(0); j++)
                {
                    if (columns[j, 1] == tables[i, 1])
                    {
                        dt.Columns.Add(new DataColumn(columns[j, 2],typeof(string)));
                    }
                }
                dt.TableName = tables[i, 1];
                ds.Tables.Add(dt);
            }

            string[] singleParaValue;
            string temp;
            string[] sql = new string[tableSql.Length];

            for (int j = 0; j < tableSql.Length; j++)
            {
                temp = tableSql[j];
                for (int i = 0; i < paraAndValues.Length; i++)
                {
                    singleParaValue = paraAndValues[i].Split('@');
                    temp = temp.Replace(":" + singleParaValue[0], "'" + singleParaValue[1] + "'");
                }
                sql[j] = temp;
            }

            DBOpt.dbHelper.GetDataSet(ref ds, sql);
            return ds;

        }


        public string[] GetTables(params string[] paraAndValues)
        {
            string[] sql = new string[tableSql.Length];
            string[] singleParaValue;
            string temp;
            for (int j = 0; j < tableSql.Length; j++)
            {
                temp = tableSql[j];
                for (int i = 0; i < paraAndValues.Length; i++)
                {
                    singleParaValue = paraAndValues[i].Split('@');
                    temp = temp.Replace(":" + singleParaValue[0], "'" + singleParaValue[1] + "'");
                }
                sql[j] = temp;
            }
            return sql;
        }


    }
}

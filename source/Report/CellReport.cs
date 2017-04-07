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
    /// CELL报表类,负责报表显示数据的生成,返回特定格式的String串
    /// </summary>
    [Serializable]
    public class CellReport
    {
        private int _id;                //报表id
        private string _name;           //报表名称
        private string _type;           //报表类型：Single、MultiFixed、MultiFlexible、南方电网操作票
        private string _cellFileName;   //报表对应的CELL文件名
        public string[,] Tables;        //报表所涉及的数据库表
        public ArrayList TableParas;    //表参数
        public string[,] Paras;         //参数:在调用的网页赋值
        public string[,] Columns;       //列
        public string[,] MultiColumns;  //单独存放多行和隔行的列
        private DataSet ReportData;     //存放从数据库中获取的数据

        public string PagesOrRows;         //对于MultiFixed的报表,存放页面增加信息;对于MultiFlexible,对应增加或减少的行数.

        private string _sql;

        //2010-1-25 为了国际化的要求，增加参数language，用于存放本地化是时的国际化代码。
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
            //只找文件名,不加路径
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
                Paras[i, 0] = dt.Rows[i][0].ToString();  //参数id
                Paras[i, 1] = dt.Rows[i][1].ToString();  //参数描述
                Paras[i, 2] = dt.Rows[i][2].ToString();  //参数类型
                Paras[i, 3] = dt.Rows[i][3].ToString();  //依赖参数id
                Paras[i, 4] = "";                        //参数取值   ,此值在报表显示时赋值
            }

            TableParas = new ArrayList();
            //先把Multi的表放在前面,先扫描Multi的表,以决定要增加多少页
            _sql = "select ID,TABLE_NAME,TABLE_TYPE,TABLE_ORDERS,TABLE_FILTER_WHERE,TABLE_PAGE_ROWS from DMIS_SYS_REPORT_TABLE where REPORT_ID=" + id + " order by TABLE_TYPE asc,ORDER_ID";
            DataTable dtTable = DBOpt.dbHelper.GetDataTable(_sql);
            Tables = new string[dtTable.Rows.Count, 8];
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                Tables[i, 0] = dtTable.Rows[i][0].ToString();   //id
                Tables[i, 1] = dtTable.Rows[i][1].ToString().Trim();   //表名
                Tables[i, 2] = dtTable.Rows[i][2].ToString().Trim();   //表类型
                Tables[i, 3] = dtTable.Rows[i][3].ToString().Trim();   //排序条件
                Tables[i, 4] = dtTable.Rows[i][4].ToString().Trim();   //检索参数
                Tables[i, 5] = dtTable.Rows[i][5].ToString();   //Multi表时每页显示的行数
                Tables[i, 6] = dtTable.Rows[i][1].ToString().Substring(dtTable.Rows[i][1].ToString().LastIndexOf('.')+1)+i.ToString();   //只要表名,不用前缀,如webdmis.dbo.   WEBDMIS.
                Tables[i, 7] = "";                              //此表所在的页号,用于换页时处理 

                //找参数和表、列之间的对应关系
                string temp = Tables[i, 4];
                string[] paraArray = temp.Split(';');
                string[] singlePara;
                for (int j = 0; j < paraArray.Length; j++)
                {
                    singlePara = paraArray[j].Split('@');
                    TablePara tp = new TablePara();
                    tp.TableName = Tables[i, 1];  //表名    如:  dmis_dd_dyqtqjl
                    tp.ColumnName=singlePara[0]; //列名     如:  TID
                    tp.ParaCode = singlePara[2]; //参数代码 如:  :1
                    TableParas.Add(tp);
                }
            }


            _sql = "select ID,TABLE_NAME,COLUMN_NAME,COLUMN_DESCR,DISPLAY_PATTERN,WORDS,P,R,C,COLUMN_TYPE,TABLE_ID from DMIS_SYS_REPORT_CELL_COLUMN where REPORT_ID=" + id + " order by ORDER_ID";
            DataTable dtColumn = DBOpt.dbHelper.GetDataTable(_sql);
            Columns = new string[dtColumn.Rows.Count, 11];
            for (int i = 0; i < dtColumn.Rows.Count; i++)
            {
                Columns[i, 0] = dtColumn.Rows[i][0].ToString();   //列id
                Columns[i, 1] = dtColumn.Rows[i][1].ToString().Trim();   //列所属表名
                Columns[i, 2] = dtColumn.Rows[i][2].ToString().Trim();   //列名
                Columns[i, 3] = dtColumn.Rows[i][3].ToString().Trim();   //列描述,也就是列别名
                Columns[i, 4] = dtColumn.Rows[i][4].ToString();   //显示方式
                Columns[i, 5] = dtColumn.Rows[i][5].ToString();   //第行显示的String数
                Columns[i, 6] = dtColumn.Rows[i][6].ToString();   //页号
                Columns[i, 7] = dtColumn.Rows[i][7].ToString();   //行号
                Columns[i, 8] = dtColumn.Rows[i][8].ToString();   //列号
                Columns[i, 9] = dtColumn.Rows[i][9].ToString();   //列类型
                Columns[i, 10] = dtColumn.Rows[i][10].ToString();   //表ID

                //赋初始页号
                for (int j = 0; j <= Tables.GetUpperBound(0); j++)   
                {
                    if (Columns[i, 10] == Tables[j, 0] && Tables[j, 7] != "") break; //已经赋值的不处理
                    if (Columns[i, 10] == Tables[j, 0])
                    {
                        Tables[j, 7] = Columns[i, 6];   //此表对应的页号
                        break;
                    }
                }
            }

            int k = 0;
            MultiColumns = new string[dtColumn.Rows.Count, 10];    //换行列没有列类型，肯定是String串
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
                    MultiColumns[k, 9] = dtColumn.Rows[i][10].ToString();   //表ID
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
        /// 从数据库中获取报表相关的数据
        /// </summary>
        /// <returns></returns>
        private DataSet GetDataSet()
        {
            //构造sql语句数组
            string[] sqls = new string[Tables.GetUpperBound(0) + 1];
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbColumns = new StringBuilder();
            for (int j = 0; j <= Tables.GetUpperBound(0); j++)
            {
                if (Tables[j, 1] == "DMIS_SYS_REPORT_CELL_OTH_COLS") continue;    //存放SCADA参数和其它参数的表,不能构造SQL语句,只能一列一列的处理
                
                sbColumns.Remove(0, sbColumns.Length);
                sbSql.Remove(0, sbSql.Length);

                for (int m = 0; m < Columns.GetUpperBound(0) + 1; m++)  //构造列
                {
                    if (Columns[m, 2] == "serial_no") continue; //序号列不加到sql语句中,在生成数据时生成列
                    if (Tables[j, 0] == Columns[m, 10])   //要用表ID比较,不能用表名
                        sbColumns.Append(@Columns[m, 2] + " " + Columns[m, 3] + ",");   //列名 列别名
                }
                sbSql.Append("select ");
                sbSql.Append(sbColumns.Remove(sbColumns.Length - 1, 1));
                sbSql.Append(" from ");
                sbSql.Append(Tables[j, 1] + " " + Tables[j, 6] + " ");   //有可能存在同样的表名,故表名再加1位的序号来作为表的别名,但列别名中不能包含.
                //条件
                string wheres;
                if (Tables[j, 4] != "")
                {
                    wheres = Tables[j, 4];  //格式如下:convert(char(8),DATEM,112)@=@:2@And
                    for (int k = 0; k <= Paras.GetUpperBound(0); k++)
                    {
                        if (wheres.IndexOf(Paras[k, 0]) > 0)
                        {
                            if (Paras[k, 2] == "String")  //参数类型
                            {
                                wheres = wheres.Replace(":" + Paras[k, 0], "'" + Paras[k, 4] + "'");   //把:2换成值
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
                    wheres = wheres.Replace('@', ' ');   //把@换成空格
                    wheres = wheres.Replace(';', ' ');   //把;换成空格
                    sbSql.Append(" where ");
                    sbSql.Append(wheres);
                }

                //排序条件
                if (Tables[j, 3].Trim() != "")
                {
                    sbSql.Append(" order by ");
                    sbSql.Append(Tables[j, 3]);
                }
                sqls[j] = sbSql.ToString();
            }

            //从数据库中取值
            DataSet ds = new DataSet();
            for (int j = 0; j <= Tables.GetUpperBound(0); j++)
            {
                DataTable dt = DBOpt.dbHelper.GetDataTable(sqls[j]);
                dt.TableName = Tables[j, 6];   //用表ID作为 Dataset中的表名,而不能用真正的表名,因为在同一报表中有相同的数据库表
                ds.Tables.Add(dt);
            }
            return ds;
        }

        /// <summary>
        /// 显示报表数据
        /// </summary>
        /// <returns></returns>
        public string Display()
        {
            string ret="";

            //先取数据
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
        /// 显示Single报表
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
                    if (Columns[j, 10] != Tables[i, 0]) continue;  //表ID比较,不能用表名
                    if (ReportData.Tables[Tables[i, 6]].Rows.Count > 0)    //有数据才生成,只生成第一条记录的数据
                    {
                        //如果参数没有设置行号、列号、页号则不生成客户端的代码
                        int r1, c1, p1;
                        if (Columns[j, 7] == null || Columns[j, 8] == null || Columns[j, 6] == null) continue;
                        if (!int.TryParse(Columns[j, 7], out r1) || !int.TryParse(Columns[j, 8], out c1) || !int.TryParse(Columns[j, 6], out p1)) continue;
                        //格式:  行号^列号^页号^数据^类型^显示方式◆行号^列号^页号^数据^类型^显示方式◆行号^列号^页号^数据^类型^显示方式.................
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
                        sbValues.Append(Columns[j, 7] + "^" + Columns[j, 8] + "^" + Columns[j, 6] + "^" + value + "^" + columnType + "^" + Columns[j, 4] + "◆");
                    }
                }
            }

            //查找是否有SCADA等数据
            int no = IsExitTable_Other_Columns();
            if (no >= 0)
            {
                //读取SCADA的数据还有以后处理
                //GetOtherColumnsData(no);
            }

            return sbValues.ToString();
        }

        /// <summary>
        /// 显示MultiFixed的报表
        /// </summary>
        /// <returns></returns>
        private string DisplayMultiFix()
        {
            #region 1、先扫描Multi的表,处理序号列和换行列。
            bool isSerial_no;
            for (int i = 0; i <= Tables.GetUpperBound(0); i++)
            {
                if (Tables[i, 2] != "Multi") break;   //由于Tables已经排序,故循环到Single的表则不再处理了

                //如果存在序号列,则先加此列
                isSerial_no = false;
                for (int j = 0; j <= Columns.GetUpperBound(0); j++)
                {
                    if (Columns[j, 1] == Tables[i, 1] && Columns[j, 2] == "serial_no")
                    {
                        isSerial_no = true;
                        break;
                    }
                }
                if (isSerial_no)  //存在序号列,则增加此列
                {
                    DataColumn colSerial_no = new DataColumn("serial_no");
                    colSerial_no.DataType = System.Type.GetType("System.Int16");
                    ReportData.Tables[Tables[i, 6]].Columns.Add(colSerial_no);
                    for (int s = 0; s < ReportData.Tables[Tables[i, 6]].Rows.Count; s++)  //对序号赋值
                    {
                        ReportData.Tables[Tables[i, 6]].Rows[s]["serial_no"] = s + 1;
                    }
                }

                //找此表对应的换行列
                int words = 0;         //换行列每行显示的String数,对全角状态下的String个数
                string content;
                string temp;
                byte[] bytes;
                for (int k = 0; k <= MultiColumns.GetUpperBound(0); k++)
                {
                    if (MultiColumns[k, 1] == "" || MultiColumns[k, 1] == null) break;  //没有换行列就不再循环了,效率高些
                    if (MultiColumns[k, 9] != Tables[i, 0]) continue;   //换行列不是此表ID的时,不用处理

                    if (!int.TryParse(MultiColumns[k, 5], out words)) break;   //换行列没有设置字数时,不处理
                    //words是全角状态下的String个数。
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
                            if (temp[temp.Length - 1] == '?')  //汉字被切除一半
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
                                if (ReportData.Tables[Tables[i, 6]].Rows.Count == j + 1)  //到了最后一行,必须增加新行
                                {
                                    DataRow drow = ReportData.Tables[Tables[i, 6]].NewRow();
                                    if (bytes.Length > words)
                                    {
                                        temp = Encoding.Default.GetString(bytes, 0, words);
                                        if (temp[temp.Length - 1]== '?')
                                        {
                                            drow[MultiColumns[k, 3]] = Encoding.Default.GetString(bytes, 0, words - 1);
                                            //temp = Encoding.Default.GetString(bytes, words, bytes.Length - words + 1);  //此行会出错
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
                                    //如果一个表有两个换行列，则第二行换行时，下一行恰好是第一处换行列产生的新行，则
                                    //不要再增加新行了。
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
                                    else   //增加新行的方式
                                    {
                                        DataRow drow = ReportData.Tables[Tables[i, 6]].NewRow();
                                        if (bytes.Length > words)
                                        {
                                            temp = Encoding.Default.GetString(bytes, 0, words);
                                            if (temp[temp.Length - 1]== '?')
                                            {
                                                drow[MultiColumns[k, 3]] = Encoding.Default.GetString(bytes, 0, words - 1);
                                                //temp = Encoding.Default.GetString(bytes, words, bytes.Length - words + 1);  //此行会出错
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


            #region 2、生成多记录表的表的客户端代码
            //由于多记录表可能存在增加页的情况，在新增的页中Single表的数据也要显示，故先处理多记录表的数据
            StringBuilder sbValues = new StringBuilder();
            Int16 curPageNo = 0;        //多记录表当前的页号
            int addPages = 0;      //一共增加的页码数,要传给客户端的隐藏控件,以便在客户端决定是否要增加多少页
            string columnType="String";
            string value;
            Hashtable tableIdPages = new Hashtable();

            for (int i = 0; i <= Tables.GetUpperBound(0); i++)
            {
                if (Tables[i, 2] != "Multi") break;   //由于Tables已经排序,故循环到Single的表则不再处理了
                if (!Int16.TryParse(Tables[i, 7], out curPageNo)) curPageNo = 0;
                
                //追加页的方式
                int startRow = 0;      //多记录表的开始行号
                int curRow = 0;        //多记录表的当前行号

                int MultiTabelPerPageRows ;   //多记录表每页显示的行数
                if(!int.TryParse(Tables[i, 5],out MultiTabelPerPageRows)) break;  //多记录表没有设置每间页显示的行数,不显示
                for (int p = 0; p <= Columns.GetUpperBound(0); p++)          //找多记录表开始行号
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
                        //对于多记录表,如果参数没有设置列号则不生成客户端的代码
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
                        sbValues.Append(curRow + "^" + Columns[h, 8] + "^" + curPageNo + "^" + value + "^" + columnType + "^" + Columns[h, 4] + "◆");
                    }
                    curRow++;
                }

                tableIdPages.Add(Tables[i, 7], addPages);   //此对应多记录表的初始页号及它对应增加的页数
            }
            #endregion


            #region 3、生成Single的表的客户端代码
            //先找此单记录表随多记录表增加了多少页;
            //再每页生成代码;
            //按照现在的实现思想,同一Single的列只能放在同一页面上.
            for (int i = 0; i <= Tables.GetUpperBound(0); i++)
            {
                if (Tables[i, 2] == "Multi") continue;   //由于Tables已经排序,故循环到Single的表再处理

                //查询表记录所在的页号,此对应的页号增加了多少页
                Int16 pages = 0;
                Int16 curPage = 0;
                foreach (DictionaryEntry e in tableIdPages)
                {
                    if (e.Key.ToString() == Tables[i, 7])   //单记录表所在的页号和此多记录表所在的页号相同
                    {
                        Int16.TryParse(e.Value.ToString(),out pages);
                        break;
                    }
                }
                if (!Int16.TryParse(Tables[i, 7], out curPage))
                    curPage = 0;
                
                for (int j = curPage; j <= curPage+pages; j++)   //先扫描页
                {
                    for (int p = 0; p <= Columns.GetUpperBound(0); p++)   //再扫描列
                    {
                        if (Columns[p, 10] != Tables[i, 0]) continue;
                        if (ReportData.Tables[Tables[i, 6]].Rows.Count > 0)  //有数据才生成,只生成第一条记录的数据
                        {
                            //如果参数没有设置行号、列号、页号则不生成客户端的代码
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

                            //格式:  行号^列号^页号^数据^类型^显示方式◆行号^列号^页号^数据^类型^显示方式◆行号^列号^页号^数据^类型^显示方式.................
                            sbValues.Append(Columns[p, 7] + "^" + Columns[p, 8] + "^" + j + "^" + value + "^" + columnType + "^" + Columns[p, 4] + "◆");
                        }
                    }
                }
            }

            //生成客户端相关的页面增加信息.
            foreach (DictionaryEntry e in tableIdPages)
            {
                PagesOrRows += e.Key.ToString() + "^" + e.Value.ToString() + "◆";
            }

            return sbValues.ToString();
            #endregion
 
        }

        /// <summary>
        /// 显示MultiFlexible的报表
        /// </summary>
        /// <returns></returns>
        private string DisplayMultiFlex()
        {
            return "";
        }

        /// <summary>
        /// 判断报表相关的数据库表中是否存在DMIS_SYS_REPORT_CELL_OTH_COLS,是则返回数组Tables中的序号,否则返回-1
        /// </summary>
        /// <returns>Tables中的序号</returns>
        private int IsExitTable_Other_Columns()
        {
            for (int i = 0; i <= Tables.GetUpperBound(0); i++)
            {
                if (Tables[i, 1] == "DMIS_SYS_REPORT_CELL_OTH_COLS") return i;
            }
            return -1;
        }

        /// <summary>
        /// 显示DMIS_SYS_REPORT_CELL_OTH_COLS中的数据
        /// </summary>
        /// <param name="no">DMIS_SYS_REPORT_CELL_OTH_COLS在报表相关数据库表中的序号</param>
        /// <returns></returns>
        private string GetOtherColumnsData(int no)
        {
            return "";
        }


    }


    /// <summary>
    /// 表参数结构,作为类CellReport的成员类型
    /// </summary>
    [Serializable]
    public struct TablePara
    {
        public string TableName; //表名 
        public string ColumnName;//列名
        public string ParaCode;//参数代码 :1
    }
}

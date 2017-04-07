using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;

namespace PlatForm.DBUtility
{

    /// <summary>
    /// OleDb数据库处理对象，负责与除Oracle,Microsoft SQL Server之外的数据库交互。
    /// 主要是Sybase、DB2、MySQL数据库
    /// 敖云峰   2007-8-4
    /// </summary>
    
    public class OleDbHelper : DBHelper
    {
        public static OleDbConnection mainConn = new OleDbConnection();
        public static OleDbConnection slaveConn = new OleDbConnection();
        static OleDbCommand cmd = new OleDbCommand();

        /// <summary> 
        /// 静态构造函数，设置上述几个变量 
        /// </summary> 
        static OleDbHelper()
        {
            mainConn.ConnectionString=mainConnectString;
            if (isDoubleDatabase) slaveConn.ConnectionString = slaveConnectString;
        }


        /// <summary> 
        /// 打开数据库连接 
        /// </summary> 
        private static void openConnection()
        {
            if (mainConn.State == ConnectionState.Closed)
            {
                mainConn.ConnectionString = mainConnectString;
                cmd.Connection = mainConn;
                mainConn.Open();
                if (isDoubleDatabase)
                {
                    if (slaveConn.State == ConnectionState.Closed)
                    {
                        slaveConn.ConnectionString = slaveConnectString;
                        slaveConn.Open();
                    }
                }
            }
        }


        /// <summary> 
        /// 关闭当前数据库连接 
        /// </summary> 
        private static void closeConnection()
        {
            if (mainConn.State == ConnectionState.Open)
                mainConn.Close();
            if (isDoubleDatabase)
            {
                if (slaveConn.State == ConnectionState.Open)
                    slaveConn.Close();
            }
        }

        /// <summary> 
        /// 执行Sql查询语句 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        public override int ExecuteSql(string sqlstr)
        {
            int ret;
            openConnection();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstr;
            ret = cmd.ExecuteNonQuery();
            closeConnection();
            return ret;
        }

        public override int ExecuteByParameter(string sqlstr, DbParameterCollection dbPara)
        {
            return -1;
        }

        /// <summary> 
        /// 执行Sql查询语句并返回第一行的第一条记录,返回值为object 使用时需要拆箱操作 -> Unbox 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>object 返回值 </returns> 
        public override object ExecuteScalar(string sqlstr)
        {
            object obj = new object();

            openConnection();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstr;
            obj = cmd.ExecuteScalar();
            closeConnection();
            return obj;
        }

        /// <summary> 
        /// 执行Sql查询语句,同时进行事务处理 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        public override void ExecuteSqlWithTransaction(string sqlstr)
        {
            OleDbTransaction trans;
            trans = mainConn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {
                openConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlstr;
                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                closeConnection();
            }
        }


        /// <summary> 
        /// 返回指定Sql语句的OleDbDataReader，请注意，在使用后请关闭本对象，同时将自动调用closeConnection()来关闭数据库连接 
        /// 方法关闭数据库连接 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>OleDbDataReader对象</returns> 
        public override DbDataReader GetDataReader(string sqlstr)
        {
            DbDataReader dr;
            OleDbDataReader drOle = null;
            try
            {
                openConnection();
                cmd.CommandText = sqlstr;
                cmd.CommandType = CommandType.Text;
                drOle = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    drOle.Close();
                    closeConnection();
                }
                catch
                {
                }
            }
            dr = drOle;
            return dr;
        }


        /// <summary> 
        /// 返回指定Sql语句的OleDbDataReader，请注意，在使用后请关闭本对象，同时将自动调用closeConnection()来关闭数据库连接 
        /// 方法关闭数据库连接 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <param name="dr">传入的ref DataReader 对象</param> 
        public override void GetDataReader(string sqlstr, ref DbDataReader dr)
        {
            OleDbDataReader drOle = null;
            try
            {
                openConnection();
                cmd.CommandText = sqlstr;
                cmd.CommandType = CommandType.Text;
                drOle = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dr = drOle;
            }
            catch
            {
                try
                {
                    if (drOle != null && !drOle.IsClosed)
                        drOle.Close();
                }
                catch
                {
                }
                finally
                {
                    closeConnection();
                }
            }
        }


        /// <summary> 
        /// 返回指定Sql语句的DataSet 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>DataSet</returns> 
        public override DataSet GetDataSet(string sqlstr)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            openConnection();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstr;
            da.SelectCommand = cmd;
            da.Fill(ds);
            closeConnection();
            return ds;
        }


        /// <summary>
        /// 根据ＳＱＬ语句数组来返回DataSet,以表名作为其中每个DataTable的名称
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public override DataSet GetDataSet(params string[] sqlstr)
        {
            int startPos, endPos;
            string tableName;
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();

            openConnection();
            cmd.CommandType = CommandType.Text;
            for (int i = 0; i < sqlstr.Length; i++)
            {
                startPos = sqlstr[i].IndexOf(" from ") + 6;
                endPos = sqlstr[i].IndexOf(" ", startPos);
                tableName = sqlstr[i].Substring(startPos, endPos - startPos);
                cmd.CommandText = sqlstr[i];
                da.SelectCommand = cmd;
                da.Fill(ds, tableName);
            }
            closeConnection();
            return ds;
        }

        /// <summary> 
        /// 返回指定Sql语句的DataSet 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <param name="ds">传入的引用DataSet对象</param> 
        public override void GetDataSet(string sqlstr, ref DataSet ds)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();

            openConnection();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstr;
            da.SelectCommand = cmd;
            da.Fill(ds);
            closeConnection();
        }

        /// <summary> 
        /// 返回指定Sql语句的DataTable 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>DataTable</returns> 
        public override DataTable GetDataTable(string sqlstr)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataTable datatable = new DataTable();
            try
            {
                openConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlstr;
                da.SelectCommand = cmd;
                da.Fill(datatable);
            }
            catch
            {
                datatable = null;
            }
            finally
            {
                closeConnection();
            }
            return datatable;
        }

        /// <summary> 
        /// 执行指定Sql语句,同时给传入DataTable进行赋值 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <param name="dt">ref DataTable dt </param> 
        public override void GetDataTable(string sqlstr, ref DataTable dt)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            openConnection();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstr;
            da.SelectCommand = cmd;
            da.Fill(dt);
            closeConnection();
        }

        /// <summary> 
        /// 执行带参数存储过程并返回数据集合 
        /// </summary> 
        /// <param name="procName">存储过程名称</param> 
        /// <param name="parameters">SqlParameterCollection 输入参数</param> 
        /// <returns></returns> 
        public override DataTable GetDataTable(string procName, DbParameterCollection parameters)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataTable datatable = new DataTable();

            openConnection();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            foreach (OleDbParameter para in parameters)
            {
                OleDbParameter p = (OleDbParameter)para;
                cmd.Parameters.Add(p);
            }
            da.SelectCommand = cmd;
            da.Fill(datatable);

            closeConnection();

            return datatable;
        }

        /// <summary> 
        /// 执行指定Sql语句,返回DataView 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        public override DataView GetDataView(string sqlstr)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataView dv = new DataView();
            DataSet ds = new DataSet();
            openConnection();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstr;
            da.SelectCommand = cmd;
            da.Fill(ds);
            dv = ds.Tables[0].DefaultView;
            closeConnection();
            return dv;
        }

        /// <summary> 
        /// 返回指定表的某列最大值
        /// </summary> 
        /// <param name="tableName">数据库表名</param> 
        /// <param name="fieldName">数据库列名</param> 
        public override uint GetMaxNum(string tableName, string fieldName)
        {
            string sql;
            uint value;
            sql = "select max(" + fieldName + ") from " + tableName;
            object re = ExecuteScalar(sql);
            if (re is System.DBNull)
                value = 1;
            else
                value = Convert.ToUInt32(re) + 1;
            return value;
        }

        /// <summary> 
        /// 执行存储过程 
        /// </summary> 
        /// <param name="procName">存储过程名</param> 
        /// <param name="coll">OleDbParameter 集合</param> 
        public override void ExecutePorcedure(string procName, DbParameter[] coll)
        {
            openConnection();
            for (int i = 0; i < coll.Length; i++)
            {
                cmd.Parameters.Add(coll[i]);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            closeConnection();
        }


        /// <summary> 
        /// 执行存储过程并返回数据集 
        /// </summary> 
        /// <param name="procName">存储过程名称</param> 
        /// <param name="coll">OleDbParameter集合</param> 
        /// <param name="ds">DataSet </param> 
        public override void ExecutePorcedure(string procName, DbParameter[] coll, ref DataSet ds)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            openConnection();
            for (int i = 0; i < coll.Length; i++)
            {
                cmd.Parameters.Add(coll[i]);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;

            da.SelectCommand = cmd;
            da.Fill(ds);

            cmd.Parameters.Clear();
            closeConnection();
        }

        /// <summary>
        /// 利用GetSchema返回所需的所有表、视图
        /// </summary>
        /// <param name="srestrictionValues">table/view/....</param>
        /// <returns>表的第四个为TABLE_NAME</returns>
        public override DataTable GetDataTableS(string srestrictionValues)
        {
            DataTable datatable = new DataTable();
            openConnection();
            if (srestrictionValues != "")
            {
                string[] sArr;
                sArr = new string[4];
                sArr[3] = srestrictionValues;
                datatable = mainConn.GetSchema("TABLES", sArr);
            }
            else
                datatable = mainConn.GetSchema("TABLES");
            closeConnection();
            return datatable;
        }
        /// <summary>
        /// 利用GetSchema返回所需表的所有字段
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns>表的第四个为COLUMN_NAME</returns>
        public override DataTable GetColumnS(string TableName)
        {
            DataTable datatable = new DataTable();
            datatable.Columns.Add(new DataColumn("COLUMN_NAME"));
            datatable.AcceptChanges();
            if (TableName == "") return (datatable);
            openConnection();
            if (TableName != "")
            {
                DataTable dt1;
                DataRow rw;
                dt1 = GetDataTable("select * from " + TableName + " where 0=1");
                if (dt1.Columns.Count > 0)
                {
                    for (int i = 0; i < dt1.Columns.Count; i++)
                    {
                        rw = datatable.NewRow();
                        rw[0] = dt1.Columns[i].ColumnName;
                        datatable.Rows.Add(rw);
                    }
                    datatable.AcceptChanges();
                }
            }
            closeConnection();
            return datatable;
        }
 



        /// <summary>
        /// 根据窗口中的输入控件得到INSERT　SQL语句，然后交给r处理
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">控件、值类型、值</param>
        /// <returns>得到的sql语句</returns>
        public override string GetInserSql(string tableName, params FieldPara[] fields)
        {
            StringBuilder values = new StringBuilder();
            StringBuilder field = new StringBuilder();

            string sql;
            int i;

            for (i = 0; i <= fields.GetUpperBound(0); i++)
            {
                if (fields[i].fieldName == null || fields[i].fieldName == "") continue;   //没有列员不处理

                if (!(fields[i].fieldValue == null || fields[i].fieldValue.Trim() == ""))   //值为空或者为“”的不考虑
                {
                    field.Append(fields[i].fieldName + ",");

                    if (fields[i].fieldType == FieldType.String)
                    {
                        values.Append("'" + fields[i].fieldValue.Replace('\'','‘') + "',");    //要把内容中包含'换成全角状态的‘
                    }
                    else if (fields[i].fieldType == FieldType.Int)
                    {
                        values.Append(fields[i].fieldValue + ",");
                    }
                    else
                    {
                        values.Append("'" + fields[i].fieldValue + "',");  //Sybase是以日期时间yyyy-mm-dd hh:mm:ss表示，MySql和DB2则要再考虑
                    }
                }
            }
            field.Remove(field.Length - 1, 1);
            values.Remove(values.Length - 1, 1);

            sql = "insert into " + tableName + "(" + field.ToString() + ") values(" + values.ToString() + ")";
            return sql;
        }


        /// <summary>
        /// 根据窗口中的输入控件得到UPDATE　SQL语句，然后交给OleDbHelper处理
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">控件、值类型、值</param>
        /// <param name="wheres">条件</param>
        /// <returns>得到的sql语句</returns>
        public override string GetUpdateSql(string tableName, FieldPara[] fields, params WherePara[] wheres)
        {
            StringBuilder where = new StringBuilder();
            StringBuilder para = new StringBuilder();
            string sql;
            int i;

            for (i = 0; i <= fields.GetUpperBound(0); i++)
            {
                if (fields[i].fieldName == null || fields[i].fieldName == "") continue;   //没有列员不处理

                if (fields[i].fieldValue != null)
                {
                    if (fields[i].fieldType == FieldType.String)
                    {
                        para.Append(fields[i].fieldName + "='" + fields[i].fieldValue.Replace('\'', '‘') + "',");
                    }
                    else if (fields[i].fieldType == FieldType.Int)
                    {
                        if (fields[i].fieldValue != "")
                            para.Append(fields[i].fieldName + "=" + fields[i].fieldValue + ",");
                        else
                            para.Append(fields[i].fieldName + "=NULL,");   //如果为空，则置为空，否则数值型的数据在界面上不能删除
                    }
                    else
                    {
                        if (fields[i].fieldValue != "")
                            para.Append(fields[i].fieldName + "='" + fields[i].fieldValue + "',");  //日期时间以yyyy-mm-dd hh:mm:ss表示
                        else
                            para.Append(fields[i].fieldName + "=NULL,");
                    }
                }
                else
                {
                    para.Append(fields[i].fieldName + "=NULL,");
                }
            }
            para.Remove(para.Length - 1, 1);

            for (i = 0; i <= wheres.GetUpperBound(0); i++)
            {
                if (wheres[i].fieldType == FieldType.String)
                {
                    where.Append(wheres[i].fieldName + wheres[i].optType + "'" + wheres[i].fieldValue + "' " + wheres[i].relationType + " ");
                }
                else if (wheres[i].fieldType == FieldType.Int)
                {
                    where.Append(wheres[i].fieldName + wheres[i].optType + wheres[i].fieldValue + wheres[i].relationType + " ");
                }
                else
                {
                    where.Append(wheres[i].fieldName + wheres[i].optType + " '" + wheres[i].fieldValue + "'" + wheres[i].relationType + " ");
                }
            }
            where.Remove(where.Length - 4, 4);

            sql = "update " + tableName + " set " + para.ToString() + " where " + where.ToString();
            return sql;
        }

        /// <summary>
        /// 得到where语句，主要用于自定义查询
        /// </summary>
        /// <param name="wheres"></param>
        /// <returns></returns>
        public override string GetQueryWheres(params WherePara[] wheres)
        {
            StringBuilder wh = new StringBuilder();

            for (int i = 0; i <= wheres.GetUpperBound(0); i++)
            {
                if (wheres[i].fieldName == "" || wheres[i].fieldName == null) continue;
                if (wheres[i].fieldType == FieldType.String)
                {
                    wh.Append(wheres[i].fieldName + wheres[i].optType + "'" + wheres[i].fieldValue + "' " + wheres[i].relationType + " ");
                }
                else if (wheres[i].fieldType == FieldType.Int)
                {
                    wh.Append(wheres[i].fieldName + wheres[i].optType + wheres[i].fieldValue + wheres[i].relationType + " ");
                }
                else
                {
                    wh.Append(wheres[i].fieldName + wheres[i].optType + " '" + wheres[i].fieldValue + "'" + wheres[i].relationType + " ");
                }
            }
            if (wh.Length > 0)
            {
                wh.Remove(wh.Length - 4, 4);
                return wh.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="wheres">条件</param>
        /// <returns>true存在，false不存在</returns>
        public override bool IsExist(string tableName, string wheres)
        {
            string sql;
            object obj;
            sql = " select count(*) from " + tableName + " where " + wheres;

            obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null) return false;
            if (Convert.ToInt16(obj) == 0)
                return false;
            else
                return true;
        }

        public override DataTable GetDataTableByParams(string sql, params DbParameter[] parameters)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataTable datatable = new DataTable();

            openConnection();
            cmd.Parameters.Clear();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql.Replace('@', '?');
                for (int i = 0; i < parameters.Length; i++)
                {
                    OleDbParameter p = (OleDbParameter)parameters[i];
                    cmd.Parameters.Add(p);
                }
                da.SelectCommand = cmd;
                da.Fill(datatable);
                cmd.Parameters.Clear();
                closeConnection();
                return datatable;
            }
            catch 
            {
                cmd.Parameters.Clear();
                closeConnection();
                return datatable;
            }
        }


        public override int ExecuteSqlByParas(string sqlstr, params DbParameter[] dbPara)
        {
            int ret;
            openConnection();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlstr;
                for (int i = 0; i < dbPara.Length; i++)
                {
                    cmd.Parameters.Add(dbPara[i]);
                }
                ret = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return ret;
            }
            catch
            {
                cmd.Parameters.Clear();
                return -1;
            }
        }

    }


}

using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;

namespace PlatForm.DBUtility
{
    public class OracleHelper : DBHelper
    {
        static OracleHelper() { }

        public override int ExecuteSql(string sqlstr)
        {
            int ret1, ret2;
            ret1 = 0;
            ret2 = 0;
            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd=new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                ret1 = mainCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                LogError("主服务器", "ExecuteSql()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }


            if (isDoubleDatabase)
            {
                OracleConnection slaveConn = new OracleConnection(slaveConnectString);
                OracleCommand slaveCmd = new OracleCommand();
                PrepareCommand(slaveCmd, slaveConn, CommandType.Text, sqlstr);
                try
                {
                    ret2 = slaveCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    LogError("备服务器", "ExecuteSql()<" + sqlstr + ">", e.Message);
                }
                finally
                {
                    if (slaveConn.State == ConnectionState.Open) slaveConn.Close();
                    slaveConn.Dispose();
                    slaveCmd.Dispose();
                }
            }
            return ret1 + ret2;
        }

        public override int ExecuteSqlByParas(string sqlstr, params DbParameter[] dbPara)
        {
            int ret1, ret2;
            ret1 = 0;
            ret2 = 0;

            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                for (int i = 0; i < dbPara.Length; i++)
                {
                    mainCmd.Parameters.Add(dbPara[i].ParameterName, dbPara[i].DbType);
                    mainCmd.Parameters[dbPara[i].ParameterName].Value = dbPara[i].Value;
                }
                ret1 = mainCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                LogError("主服务器", "ExecuteSqlByParas()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                mainCmd.Parameters.Clear();
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }

            if (isDoubleDatabase)
            {
                OracleConnection slaveConn = new OracleConnection(slaveConnectString);
                OracleCommand slaveCmd = new OracleCommand();
                PrepareCommand(slaveCmd, slaveConn, CommandType.Text, sqlstr);
                try
                {
                    for (int i = 0; i < dbPara.Length; i++)
                    {
                        slaveCmd.Parameters.Add(dbPara[i].ParameterName, dbPara[i].DbType);
                        slaveCmd.Parameters[dbPara[i].ParameterName].Value = dbPara[i].Value;
                    }
                    ret2 = slaveCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    LogError("备服务器", "ExecuteSqlByParas()<" + sqlstr + ">", e.Message);
                }
                finally
                {
                    slaveCmd.Parameters.Clear();
                    if (slaveConn.State == ConnectionState.Open) slaveConn.Close();
                    slaveConn.Dispose();
                    slaveCmd.Dispose();
                }
            }
            return ret1 + ret2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="dbPara"></param>
        /// <returns></returns>
        public override int ExecuteByParameter(string sqlstr, DbParameterCollection dbPara)
        {
            int ret1, ret2; ;
            ret1 = 0;
            ret2 = 0;

            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                for (int i = 0; i < dbPara.Count; i++)
                {
                    mainCmd.Parameters.Add(dbPara[i].ParameterName, dbPara[i].DbType);
                    mainCmd.Parameters[dbPara[i].ParameterName].Value = dbPara[i].Value;
                }
                ret1 = mainCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                LogError("主服务器", "ExecuteSqlByParas()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                mainCmd.Parameters.Clear();
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }

            if (isDoubleDatabase)
            {
                OracleConnection slaveConn = new OracleConnection(slaveConnectString);
                OracleCommand slaveCmd = new OracleCommand();
                PrepareCommand(slaveCmd, slaveConn, CommandType.Text, sqlstr);
                try
                {
                    for (int i = 0; i < dbPara.Count; i++)
                    {
                        slaveCmd.Parameters.Add(dbPara[i].ParameterName, dbPara[i].DbType);
                        slaveCmd.Parameters[dbPara[i].ParameterName].Value = dbPara[i].Value;
                    }
                    ret2 = slaveCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    LogError("备服务器", "ExecuteSqlByParas()<" + sqlstr + ">", e.Message);
                }
                finally
                {
                    slaveCmd.Parameters.Clear();
                    if (slaveConn.State == ConnectionState.Open) slaveConn.Close();
                    slaveConn.Dispose();
                    slaveCmd.Dispose();
                }
            }
            return ret1 + ret2;
        }

        /// <summary> 
        /// 执行Sql查询语句并返回第一行的第一条记录,返回值为object 使用时需要拆箱操作 -> Unbox 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>object 返回值 </returns> 
        public override object ExecuteScalar(string sqlstr)
        {
            object obj = new object();

            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                obj = mainCmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                LogError("主服务器", "ExecuteSqlByParas()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }
            return obj;
        }

        /// <summary> 
        /// 执行Sql查询语句,同时进行事务处理，对于单个SQL语句，没有什么用处 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        public override void ExecuteSqlWithTransaction(string sqlstr)
        {
            OracleTransaction trans;
            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            trans = mainConn.BeginTransaction();
            try
            {
                mainCmd.Transaction = trans;
                mainCmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                LogError("主服务器", "ExecuteSqlWithTransaction()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }

            if (isDoubleDatabase)
            {
                OracleConnection slaveConn = new OracleConnection(slaveConnectString);
                OracleCommand slaveCmd = new OracleCommand();
                PrepareCommand(slaveCmd, slaveConn, CommandType.Text, sqlstr);
                trans = slaveConn.BeginTransaction();
                try
                {
                    slaveCmd.Transaction = trans;
                    slaveCmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    LogError("备服务器", "ExecuteSqlWithTransaction()<" + sqlstr + ">", e.Message);
                }
                finally
                {
                    if (slaveConn.State == ConnectionState.Open) slaveConn.Close();
                    slaveConn.Dispose();
                    slaveCmd.Dispose();
                }
            }
        }

        /// <summary> 
        /// 执行Sql查询语句,同时进行事务处理 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        public override int ExecuteSqlWithTransaction(params string[] sqlstr)
        {
            int ret = 1;
            OracleTransaction trans;
            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, "");
            trans = mainConn.BeginTransaction();
            try
            {
                mainCmd.Transaction = trans;
                for (int j = 0; j < sqlstr.Length; j++)
                {
                    if (sqlstr[j].Trim() == "") continue;
                    mainCmd.CommandText = sqlstr[j];
                    mainCmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                LogError("主服务器", "ExecuteSqlWithTransaction()<" + sqlstr + ">", e.Message);
                ret = -1;
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }
            if (ret < 0) return ret;

            if (isDoubleDatabase)
            {
                OracleConnection slaveConn = new OracleConnection(slaveConnectString);
                OracleCommand slaveCmd = new OracleCommand();
                PrepareCommand(slaveCmd, slaveConn, CommandType.Text, "");
                trans = slaveConn.BeginTransaction();
                try
                {
                    slaveCmd.Transaction = trans;
                    for (int j = 0; j < sqlstr.Length; j++)
                    {
                        if (sqlstr[j].Trim() == "") continue;
                        slaveCmd.CommandText = sqlstr[j];
                        slaveCmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    LogError("备服务器", "ExecuteSqlWithTransaction()<" + sqlstr + ">", e.Message);
                    ret = -1;
                }
                finally
                {
                    if (slaveConn.State == ConnectionState.Open) slaveConn.Close();
                    slaveConn.Dispose();
                    slaveCmd.Dispose();
                }
            }
            return ret;
        }

        /// <summary> 
        /// 返回指定Sql语句的OracleDataReader，请注意，在使用后请关闭本对象，同时将自动调用closeConnection()来关闭数据库连接 
        /// 方法关闭数据库连接 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>OracleDataReader对象</returns> 
        public override DbDataReader GetDataReader(string sqlstr)
        {
            DbDataReader dr;
            OracleDataReader drOra = null;

            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                drOra = mainCmd.ExecuteReader();
            }
            catch (Exception e)
            {
               LogError("主服务器", "GetDataReader()<" + sqlstr + ">", e.Message);
               drOra.Close();
               if (mainConn.State == ConnectionState.Open) mainConn.Close();
               mainConn.Dispose();
               mainCmd.Dispose();
           }
            dr = drOra;
            return dr;
        }

        /// <summary> 
        /// 返回指定Sql语句的OracleDataReader，请注意，在使用后请关闭本对象，同时将自动调用closeConnection()来关闭数据库连接 
        /// 方法关闭数据库连接 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <param name="dr">传入的ref DataReader 对象</param> 
        public override void GetDataReader(string sqlstr, ref DbDataReader dr)
        {
            OracleDataReader drOra = null;
            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                drOra = mainCmd.ExecuteReader();
                dr = drOra;
            }
            catch (Exception e)
            {
                LogError("主服务器", "GetDataReader()<" + sqlstr + ">", e.Message);
                drOra.Close();
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
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
            OracleDataAdapter da = new OracleDataAdapter();

            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                da.SelectCommand = mainCmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                LogError("主服务器", "GetDataSet()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }
            da.Dispose();
            return ds;
        }


        /// <summary> 
        /// 返回指定Sql语句的DataSet 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <param name="ds">传入的引用DataSet对象</param> 
        public override void GetDataSet(string sqlstr, ref DataSet ds)
        {
            OracleDataAdapter da = new OracleDataAdapter();
            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                da.SelectCommand = mainCmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                LogError("主服务器", "GetDataSet()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }
            da.Dispose();
         }

        /// <summary> 
        /// 返回指定Sql语句的DataTable 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>DataTable</returns> 
        public override DataTable GetDataTable(string sqlstr)
        {
            OracleDataAdapter da = new OracleDataAdapter();
            DataTable datatable = new DataTable();

            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                da.SelectCommand = mainCmd;
                da.Fill(datatable);
            }
            catch (Exception e)
            {
                datatable = null;
                LogError("主服务器", "GetDataSet()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }
            da.Dispose();
            return datatable;
        }

        /// <summary> 
        /// 执行指定Sql语句,同时给传入DataTable进行赋值 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <param name="dt">ref DataTable dt </param> 
        public override void GetDataTable(string sqlstr, ref DataTable dt)
        {
            OracleDataAdapter da = new OracleDataAdapter();
            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                da.SelectCommand = mainCmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
                LogError("主服务器", "GetDataTable()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }
            da.Dispose();
        }


        /// <summary> 
        /// 执行带参数存储过程并返回数据集合 
        /// 还没有用上,还要修改2009-3-4
        /// </summary> 
        /// <param name="procName">存储过程名称</param> 
        /// <param name="parameters">SqlParameterCollection 输入参数</param> 
        /// <returns></returns> 
        public override DataTable GetDataTable(string procName, DbParameterCollection parameters)
        {
            OracleDataAdapter da = new OracleDataAdapter();
            DataTable datatable = new DataTable();
            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.StoredProcedure, procName);
            try
            {
                foreach (OracleParameter para in parameters)
                {
                    OracleParameter p = (OracleParameter)para;
                    mainCmd.Parameters.Add(p);
                }
                da.SelectCommand = mainCmd;
                da.Fill(datatable);
            }
            catch (Exception e)
            {
                datatable = null;
                LogError("主服务器", "GetDataTable()<" + procName + ">", e.Message);
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }
            da.Dispose();
            return datatable;
        }


        /// <summary> 
        /// 返回指定表的某列最大值
        /// </summary> 
        /// <param name="tableName">数据库表名</param> 
        /// <param name="fieldName">数据库列名</param> 
        public override uint GetMaxNum(string tableName, string fieldName)
        {
            string sqlstr;
            uint value1, value2;
            object re;
            value1 = 0;
            value2 = 0;
            sqlstr = "select max(" + fieldName + ") from " + tableName;

            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                re = mainCmd.ExecuteScalar();
                if (re is System.DBNull)
                    value1 = 1;
                else
                    value1 = Convert.ToUInt32(re) + 1;
            }
            catch (Exception e)
            {
                LogError("主服务器", "GetMaxNum()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }

            if (isDoubleDatabase)
            {
                OracleConnection slaveConn = new OracleConnection(slaveConnectString);
                OracleCommand slaveCmd = new OracleCommand();
                PrepareCommand(slaveCmd, slaveConn, CommandType.Text, sqlstr);
                try
                {
                    re = slaveCmd.ExecuteScalar();
                    if (re is System.DBNull)
                        value2 = 1;
                    else
                        value2 = Convert.ToUInt32(re) + 1;
                }
                catch (Exception e)
                {
                    LogError("备服务器", "GetMaxNum()<" + sqlstr + ">", e.Message);
                }
                finally
                {
                    if (slaveConn.State == ConnectionState.Open) slaveConn.Close();
                    slaveConn.Dispose();
                    slaveCmd.Dispose();
                }
            }
            return Math.Max(value1, value2);
        }

        /// <summary>
        /// 根据条件，返回指定表已经存在记录某列的最大值
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">数据库列名</param>
        /// <param name="wheres">条件</param>
        /// <returns>返回unit类型的值</returns>
        public override uint GetMaxNum(string tableName, string fieldName, string wheres)
        {
            string sqlstr;
            uint value1, value2;
            object re;
            value1 = 0;
            value2 = 0;
            sqlstr = "select max(" + fieldName + ") from " + tableName + " where " + wheres;

            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.Text, sqlstr);
            try
            {
                re = mainCmd.ExecuteScalar();
                if (re is System.DBNull)
                    value1 = 1;
                else
                    value1 = Convert.ToUInt32(re) + 1;
            }
            catch (Exception e)
            {
                LogError("主服务器", "GetMaxNum()<" + sqlstr + ">", e.Message);
            }
            finally
            {
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }

            if (isDoubleDatabase)
            {
                OracleConnection slaveConn = new OracleConnection(slaveConnectString);
                OracleCommand slaveCmd = new OracleCommand();
                PrepareCommand(slaveCmd, slaveConn, CommandType.Text, sqlstr);
                try
                {
                    re = slaveCmd.ExecuteScalar();
                    if (re is System.DBNull)
                        value2 = 1;
                    else
                        value2 = Convert.ToUInt32(re) + 1;
                }
                catch (Exception e)
                {
                    LogError("备服务器", "GetMaxNum()<" + sqlstr + ">", e.Message);
                }
                finally
                {
                    if (slaveConn.State == ConnectionState.Open) slaveConn.Close();
                    slaveConn.Dispose();
                    slaveCmd.Dispose();
                }
            }
            return Math.Max(value1, value2);
        }



        /// <summary> 
        /// 执行存储过程 
        /// </summary> 
        /// <param name="procName">存储过程名</param> 
        /// <param name="coll">OracleParameter 集合</param> 
        public override void ExecutePorcedure(string procName, DbParameter[] coll)
        {
            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.StoredProcedure, procName);
            try
            {
                for (int i = 0; i < coll.Length; i++)
                {
                    mainCmd.Parameters.Add(coll[i]);
                }
                mainCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                LogError("主服务器", "ExecutePorcedure()<" + procName + ">", e.Message);
            }
            finally
            {
                mainCmd.Parameters.Clear();
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }


            if (isDoubleDatabase)
            {
                OracleConnection slaveConn = new OracleConnection(slaveConnectString);
                OracleCommand slaveCmd = new OracleCommand();
                PrepareCommand(slaveCmd, slaveConn, CommandType.StoredProcedure, procName);
                try
                {
                    for (int i = 0; i < coll.Length; i++)
                    {
                        slaveCmd.Parameters.Add(coll[i]);
                    }
                    slaveCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    LogError("备服务器", "ExecutePorcedure()<" + procName + ">", e.Message);
                }
                finally
                {
                    slaveCmd.Parameters.Clear();
                    if (slaveConn.State == ConnectionState.Open) slaveConn.Close();
                    slaveConn.Dispose();
                    slaveCmd.Dispose();
                }
            }
        }

        /// <summary> 
        /// 执行存储过程并返回数据集 
        /// 2009-3-4  还未用上,还待修改
        /// </summary> 
        /// <param name="procName">存储过程名称</param> 
        /// <param name="coll">OracleParameter集合</param> 
        /// <param name="ds">DataSet </param> 
        public override void ExecutePorcedure(string procName, DbParameter[] coll, ref DataSet ds)
        {
            OracleDataAdapter da = new OracleDataAdapter();
            OracleConnection mainConn = new OracleConnection(mainConnectString);
            OracleCommand mainCmd = new OracleCommand();
            PrepareCommand(mainCmd, mainConn, CommandType.StoredProcedure, procName);
            try
            {
                for (int i = 0; i < coll.Length; i++)
                {
                    mainCmd.Parameters.Add(coll[i]);
                }
                da.SelectCommand = mainCmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                LogError("主服务器", "ExecutePorcedure()<" + procName + ">", e.Message);
            }
            finally
            {
                mainCmd.Parameters.Clear();
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }
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
            string temp;

            for (i = 0; i <= fields.GetUpperBound(0); i++)
            {
                if (fields[i].fieldName == null || fields[i].fieldName == "") continue;   //没有列员不处理

                if (!(fields[i].fieldValue == null || fields[i].fieldValue.Trim() == "" || fields[i].fieldValue.Trim() == "''"))   //值为空或者为“”的不考虑
                {
                    field.Append(fields[i].fieldName + ",");

                    if (fields[i].fieldType == FieldType.String)
                    {
                        temp = fields[i].fieldValue.Replace('\'', '‘');
                        temp = temp.Replace('"', '“');
                        values.Append("'" + temp + "',");    //要把内容中包含'换成全角状态的‘
                    }
                    else if (fields[i].fieldType == FieldType.Int)
                    {
                        values.Append(fields[i].fieldValue + ",");
                    }
                    else
                    {
                        values.Append("to_date('" + fields[i].fieldValue + "','dd-mm-yyyy hh24:mi:ss'),");  //Sybase是以日期时间yyyy-mm-dd hh:mm:ss表示，MySql和DB2则要再考虑
                    }
                }
            }
            field.Remove(field.Length - 1, 1);
            values.Remove(values.Length - 1, 1);

            sql = "insert into " + tableName + "(" + field.ToString() + ") values(" + values.ToString() + ")";
            return sql;
        }

        /// <summary>
        /// 根据窗口中的输入控件得到UPDATE SQL语句，然后交给OracleHelper处理
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
            string temp;

            for (i = 0; i <= fields.GetUpperBound(0); i++)
            {
                if (fields[i].fieldName == null || fields[i].fieldName == "") continue;   //没有列员不处理

                if (fields[i].fieldValue != null)
                {
                    if (fields[i].fieldType == FieldType.String)
                    {
                        if (fields[i].fieldValue != "''")
                        {
                            temp = fields[i].fieldValue.Replace('\'', '‘');
                            temp = temp.Replace('"', '“');
                            para.Append(fields[i].fieldName + "='" + temp + "',");
                        }
                        else
                            para.Append(fields[i].fieldName + "=NULL,");
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
                            para.Append(fields[i].fieldName + "=to_date('" + fields[i].fieldValue + "','dd-mm-yyyy hh24:mi:ss'),");  //日期时间以yyyy-mm-dd hh:mm:ss表示
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
            sql = "select count(*) from " + tableName + " where " + wheres;

            obj = ExecuteScalar(sql);
            if (obj == null) return false;
            if (Convert.ToInt16(obj) == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 把DataTable中的数据插入到数据库中相应的表中，DataTable的名称就是表名
        /// </summary>
        /// <param name="values">存放数据的DataTable</param>
        /// <param name="FailCounts">失败的条数</param>
        /// <returns>成功的条件</returns>
        public override int InsertByDataTable(DataTable values, ref int FailCounts)
        {
            if (values == null || values.Rows.Count < 1) return -1;
            if (values.TableName == "") return -1;
            StringBuilder cols = new StringBuilder();
            StringBuilder vals = new StringBuilder();
            int successCount = 0;
            string sql;

            for (int i = 0; i < values.Rows.Count; i++)
            {
                for (int j = 0; j < values.Columns.Count; j++)
                {
                    if (values.Rows[i][j] == Convert.DBNull) continue;
                    cols.Append(values.Columns[j].ColumnName + ",");
                    if (values.Columns[j].DataType == System.Type.GetType("System.String"))
                    {
                        vals.Append("'" + values.Rows[i][j].ToString().Replace("'", "‘") + "',");
                    }
                    else if (values.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                    {
                        vals.Append("TO_DATE('" + Convert.ToDateTime(values.Rows[i][j]).ToString("yyyy-MM-dd HH:mm") + "','YYYY-MM-DD HH24:MI'),");
                    }
                    else
                    {
                        vals.Append(values.Rows[i][j].ToString() + ",");
                    }

                    //switch (values.Columns[j].DataType)   //为什么此语句有错误，提示要是整数
                    //{
                    //    case System.Type.GetType("System.String"):   //字符串
                    //        vals.Append("'" + values.Rows[i][j].ToString().Replace("'", "‘") + "',");
                    //        cols.Append(values.Columns[j].ColumnName);
                    //        break;
                    //    case System.Type.GetType("System.DateTime"):  //时间
                    //        vals.Append("TO_DATE('" + Convert.ToDateTime(values.Rows[i][j]).ToString("yyyy-MM-DD HH:mm") + "','YYYY-MM-DD HH24:MI'),");
                    //        cols.Append(values.Columns[j].ColumnName);
                    //        break;
                    //    case System.Type.GetType("System.Decimal"):   //数值
                    //    case System.Type.GetType("System.Double"):
                    //    case System.Type.GetType("System.Int16"):
                    //    case System.Type.GetType("System.Int32"):
                    //    case System.Type.GetType("System.Int64"):
                    //    case System.Type.GetType("System.Single"):
                    //    case System.Type.GetType("System.UInt16"):
                    //    case System.Type.GetType("System.UInt32"):
                    //    case System.Type.GetType("System.UInt64"):
                    //        vals.Append(values.Rows[i][j].ToString()+ "',");
                    //        cols.Append(values.Columns[j].ColumnName);
                    //        break;
                    //    default:   //其它类型的不考虑
                    //        break;
                    //}
                }
                sql = "insert into " + values.TableName + "(" + cols.Remove(cols.Length - 1, 1).ToString() + ") values(" + vals.Remove(vals.Length - 1, 1).ToString() + ")";
                if (ExecuteSql(sql) > 0)
                    successCount++;
                else
                    FailCounts++;
                cols.Remove(0, cols.Length);
                vals.Remove(0, vals.Length);
            }
            return successCount;
        }

        /// <summary>
        /// 2009-1-4,珠海用户在点击所有缺陷查询时,系统反映慢,导致其它用户无法使用DF8360
        /// 问题比较大,特增加此分页函数的提高效率.
        /// 当然还可以采用存储过程来查询分页数据.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override DataTable GetPagedDataTable(int pageIndex, int pageSize, ref int pageCounts, ref int totalRows, string sql)
        {
            int startRow;       //起始行号
            int endRow;         //终止行号
            string sqlCounts;   //计算总记录数的SQL语句
            string s;
            DataTable result = new DataTable();

            object obj;
            int indexFrom;
            indexFrom = sql.ToLower().IndexOf(" from ");
            if (indexFrom < 0) return result;

            sqlCounts = "select count(*) " + sql.Substring(indexFrom);
            obj = ExecuteScalar(sqlCounts);
            if (obj == null || Convert.ToInt16(obj) == 0) return result;
            totalRows = Convert.ToInt16(obj);
            pageCounts = totalRows / pageSize + 1;
            endRow = pageIndex * pageSize + pageSize;
            startRow = endRow - pageSize + 1;
            s = sql.Substring(0, indexFrom) + " from(select rownum RowIndex, A.* from (" + sql + ") A )  where RowIndex between "
                + startRow + " and " + endRow;
            result = GetDataTable(s);
            return result;
        }

        /// <summary>
        /// 记录错误信息.2010-02-04 
        /// </summary>
        public static void LogError(string serverName, string wheres, string err)
        {
            if (HttpContext.Current != null && File.Exists(HttpContext.Current.Server.MapPath("~") + "//DbLog.txt"))
            {
                //StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath("~") + "//DbLog.txt");
                //sw.WriteLine(DateTime.Now + serverName + HttpContext.Current.Request.FilePath + "[" + wheres + "]" + "{" + err + "}");
                //sw.Close();
            }
        }

        /// <summary>
        /// 操作数据库前的准备工作
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn,  CommandType cmdType, string cmdText )
        {
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;
            }
            catch (Exception e)
            {
                return;
            }
        }

    }//类
}//命名空间

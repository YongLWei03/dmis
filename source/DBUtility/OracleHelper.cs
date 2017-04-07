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
                LogError("��������", "ExecuteSql()<" + sqlstr + ">", e.Message);
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
                    LogError("��������", "ExecuteSql()<" + sqlstr + ">", e.Message);
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
                LogError("��������", "ExecuteSqlByParas()<" + sqlstr + ">", e.Message);
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
                    LogError("��������", "ExecuteSqlByParas()<" + sqlstr + ">", e.Message);
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
                LogError("��������", "ExecuteSqlByParas()<" + sqlstr + ">", e.Message);
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
                    LogError("��������", "ExecuteSqlByParas()<" + sqlstr + ">", e.Message);
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
        /// ִ��Sql��ѯ��䲢���ص�һ�еĵ�һ����¼,����ֵΪobject ʹ��ʱ��Ҫ������� -> Unbox 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>object ����ֵ </returns> 
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
                LogError("��������", "ExecuteSqlByParas()<" + sqlstr + ">", e.Message);
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
        /// ִ��Sql��ѯ���,ͬʱ�������������ڵ���SQL��䣬û��ʲô�ô� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
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
                LogError("��������", "ExecuteSqlWithTransaction()<" + sqlstr + ">", e.Message);
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
                    LogError("��������", "ExecuteSqlWithTransaction()<" + sqlstr + ">", e.Message);
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
        /// ִ��Sql��ѯ���,ͬʱ���������� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
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
                LogError("��������", "ExecuteSqlWithTransaction()<" + sqlstr + ">", e.Message);
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
                    LogError("��������", "ExecuteSqlWithTransaction()<" + sqlstr + ">", e.Message);
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
        /// ����ָ��Sql����OracleDataReader����ע�⣬��ʹ�ú���رձ�����ͬʱ���Զ�����closeConnection()���ر����ݿ����� 
        /// �����ر����ݿ����� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>OracleDataReader����</returns> 
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
               LogError("��������", "GetDataReader()<" + sqlstr + ">", e.Message);
               drOra.Close();
               if (mainConn.State == ConnectionState.Open) mainConn.Close();
               mainConn.Dispose();
               mainCmd.Dispose();
           }
            dr = drOra;
            return dr;
        }

        /// <summary> 
        /// ����ָ��Sql����OracleDataReader����ע�⣬��ʹ�ú���رձ�����ͬʱ���Զ�����closeConnection()���ر����ݿ����� 
        /// �����ر����ݿ����� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <param name="dr">�����ref DataReader ����</param> 
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
                LogError("��������", "GetDataReader()<" + sqlstr + ">", e.Message);
                drOra.Close();
                if (mainConn.State == ConnectionState.Open) mainConn.Close();
                mainConn.Dispose();
                mainCmd.Dispose();
            }
        }

        /// <summary> 
        /// ����ָ��Sql����DataSet 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
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
                LogError("��������", "GetDataSet()<" + sqlstr + ">", e.Message);
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
        /// ����ָ��Sql����DataSet 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <param name="ds">���������DataSet����</param> 
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
                LogError("��������", "GetDataSet()<" + sqlstr + ">", e.Message);
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
        /// ����ָ��Sql����DataTable 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
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
                LogError("��������", "GetDataSet()<" + sqlstr + ">", e.Message);
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
        /// ִ��ָ��Sql���,ͬʱ������DataTable���и�ֵ 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
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
                LogError("��������", "GetDataTable()<" + sqlstr + ">", e.Message);
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
        /// ִ�д������洢���̲��������ݼ��� 
        /// ��û������,��Ҫ�޸�2009-3-4
        /// </summary> 
        /// <param name="procName">�洢��������</param> 
        /// <param name="parameters">SqlParameterCollection �������</param> 
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
                LogError("��������", "GetDataTable()<" + procName + ">", e.Message);
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
        /// ����ָ�����ĳ�����ֵ
        /// </summary> 
        /// <param name="tableName">���ݿ����</param> 
        /// <param name="fieldName">���ݿ�����</param> 
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
                LogError("��������", "GetMaxNum()<" + sqlstr + ">", e.Message);
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
                    LogError("��������", "GetMaxNum()<" + sqlstr + ">", e.Message);
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
        /// ��������������ָ�����Ѿ����ڼ�¼ĳ�е����ֵ
        /// </summary>
        /// <param name="tableName">���ݿ����</param>
        /// <param name="fieldName">���ݿ�����</param>
        /// <param name="wheres">����</param>
        /// <returns>����unit���͵�ֵ</returns>
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
                LogError("��������", "GetMaxNum()<" + sqlstr + ">", e.Message);
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
                    LogError("��������", "GetMaxNum()<" + sqlstr + ">", e.Message);
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
        /// ִ�д洢���� 
        /// </summary> 
        /// <param name="procName">�洢������</param> 
        /// <param name="coll">OracleParameter ����</param> 
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
                LogError("��������", "ExecutePorcedure()<" + procName + ">", e.Message);
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
                    LogError("��������", "ExecutePorcedure()<" + procName + ">", e.Message);
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
        /// ִ�д洢���̲��������ݼ� 
        /// 2009-3-4  ��δ����,�����޸�
        /// </summary> 
        /// <param name="procName">�洢��������</param> 
        /// <param name="coll">OracleParameter����</param> 
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
                LogError("��������", "ExecutePorcedure()<" + procName + ">", e.Message);
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
        /// ���ݴ����е�����ؼ��õ�INSERT��SQL��䣬Ȼ�󽻸�r����
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="fields">�ؼ���ֵ���͡�ֵ</param>
        /// <returns>�õ���sql���</returns>
        public override string GetInserSql(string tableName, params FieldPara[] fields)
        {
            StringBuilder values = new StringBuilder();
            StringBuilder field = new StringBuilder();

            string sql;
            int i;
            string temp;

            for (i = 0; i <= fields.GetUpperBound(0); i++)
            {
                if (fields[i].fieldName == null || fields[i].fieldName == "") continue;   //û����Ա������

                if (!(fields[i].fieldValue == null || fields[i].fieldValue.Trim() == "" || fields[i].fieldValue.Trim() == "''"))   //ֵΪ�ջ���Ϊ�����Ĳ�����
                {
                    field.Append(fields[i].fieldName + ",");

                    if (fields[i].fieldType == FieldType.String)
                    {
                        temp = fields[i].fieldValue.Replace('\'', '��');
                        temp = temp.Replace('"', '��');
                        values.Append("'" + temp + "',");    //Ҫ�������а���'����ȫ��״̬�ġ�
                    }
                    else if (fields[i].fieldType == FieldType.Int)
                    {
                        values.Append(fields[i].fieldValue + ",");
                    }
                    else
                    {
                        values.Append("to_date('" + fields[i].fieldValue + "','dd-mm-yyyy hh24:mi:ss'),");  //Sybase��������ʱ��yyyy-mm-dd hh:mm:ss��ʾ��MySql��DB2��Ҫ�ٿ���
                    }
                }
            }
            field.Remove(field.Length - 1, 1);
            values.Remove(values.Length - 1, 1);

            sql = "insert into " + tableName + "(" + field.ToString() + ") values(" + values.ToString() + ")";
            return sql;
        }

        /// <summary>
        /// ���ݴ����е�����ؼ��õ�UPDATE SQL��䣬Ȼ�󽻸�OracleHelper����
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="fields">�ؼ���ֵ���͡�ֵ</param>
        /// <param name="wheres">����</param>
        /// <returns>�õ���sql���</returns>
        public override string GetUpdateSql(string tableName, FieldPara[] fields, params WherePara[] wheres)
        {
            StringBuilder where = new StringBuilder();
            StringBuilder para = new StringBuilder();
            string sql;
            int i;
            string temp;

            for (i = 0; i <= fields.GetUpperBound(0); i++)
            {
                if (fields[i].fieldName == null || fields[i].fieldName == "") continue;   //û����Ա������

                if (fields[i].fieldValue != null)
                {
                    if (fields[i].fieldType == FieldType.String)
                    {
                        if (fields[i].fieldValue != "''")
                        {
                            temp = fields[i].fieldValue.Replace('\'', '��');
                            temp = temp.Replace('"', '��');
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
                            para.Append(fields[i].fieldName + "=NULL,");   //���Ϊ�գ�����Ϊ�գ�������ֵ�͵������ڽ����ϲ���ɾ��
                    }
                    else
                    {
                        if (fields[i].fieldValue != "")
                            para.Append(fields[i].fieldName + "=to_date('" + fields[i].fieldValue + "','dd-mm-yyyy hh24:mi:ss'),");  //����ʱ����yyyy-mm-dd hh:mm:ss��ʾ
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
        /// �õ�where��䣬��Ҫ�����Զ����ѯ
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
        /// �жϼ�¼�Ƿ����
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="wheres">����</param>
        /// <returns>true���ڣ�false������</returns>
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
        /// ��DataTable�е����ݲ��뵽���ݿ�����Ӧ�ı��У�DataTable�����ƾ��Ǳ���
        /// </summary>
        /// <param name="values">������ݵ�DataTable</param>
        /// <param name="FailCounts">ʧ�ܵ�����</param>
        /// <returns>�ɹ�������</returns>
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
                        vals.Append("'" + values.Rows[i][j].ToString().Replace("'", "��") + "',");
                    }
                    else if (values.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                    {
                        vals.Append("TO_DATE('" + Convert.ToDateTime(values.Rows[i][j]).ToString("yyyy-MM-dd HH:mm") + "','YYYY-MM-DD HH24:MI'),");
                    }
                    else
                    {
                        vals.Append(values.Rows[i][j].ToString() + ",");
                    }

                    //switch (values.Columns[j].DataType)   //Ϊʲô������д�����ʾҪ������
                    //{
                    //    case System.Type.GetType("System.String"):   //�ַ���
                    //        vals.Append("'" + values.Rows[i][j].ToString().Replace("'", "��") + "',");
                    //        cols.Append(values.Columns[j].ColumnName);
                    //        break;
                    //    case System.Type.GetType("System.DateTime"):  //ʱ��
                    //        vals.Append("TO_DATE('" + Convert.ToDateTime(values.Rows[i][j]).ToString("yyyy-MM-DD HH:mm") + "','YYYY-MM-DD HH24:MI'),");
                    //        cols.Append(values.Columns[j].ColumnName);
                    //        break;
                    //    case System.Type.GetType("System.Decimal"):   //��ֵ
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
                    //    default:   //�������͵Ĳ�����
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
        /// 2009-1-4,�麣�û��ڵ������ȱ�ݲ�ѯʱ,ϵͳ��ӳ��,���������û��޷�ʹ��DF8360
        /// ����Ƚϴ�,�����Ӵ˷�ҳ���������Ч��.
        /// ��Ȼ�����Բ��ô洢��������ѯ��ҳ����.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override DataTable GetPagedDataTable(int pageIndex, int pageSize, ref int pageCounts, ref int totalRows, string sql)
        {
            int startRow;       //��ʼ�к�
            int endRow;         //��ֹ�к�
            string sqlCounts;   //�����ܼ�¼����SQL���
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
        /// ��¼������Ϣ.2010-02-04 
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
        /// �������ݿ�ǰ��׼������
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

    }//��
}//�����ռ�

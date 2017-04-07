using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace PlatForm.DBUtility
{
    public class SQLHelper:DBHelper
    {
        public static SqlConnection mainConn = new SqlConnection();
        public static SqlConnection slaveConn = new SqlConnection();
        static SqlCommand cmd = new SqlCommand();

        /// <summary> 
        /// ��̬���캯�������������������� 
        /// </summary> 
        static SQLHelper()
        {
            mainConn.ConnectionString=mainConnectString;
            if (isDoubleDatabase) slaveConn.ConnectionString = slaveConnectString;
        }

        /// <summary> 
        /// �����ݿ����� 
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
        /// �رյ�ǰ���ݿ����� 
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
        /// ִ��Sql��ѯ��� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        public override int ExecuteSql(string sqlstr)
        {
            int ret;
            openConnection();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlstr;
                ret = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();  //����Ҫ����
                closeConnection();
                return ret;
            }
            catch
            {
                cmd.Parameters.Clear();  //����Ҫ����
                return -1;
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
                    cmd.Parameters.Add(dbPara[i]);
                ret = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return ret;
            }
            catch 
            {
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="dbPara"></param>
        /// <returns></returns>
        public override int ExecuteByParameter(string sqlstr, DbParameterCollection dbPara)
        {
            int ret;
            openConnection();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlstr;
                cmd.Parameters.Clear();
                for(int i=0;i<dbPara.Count;i++)
                    cmd.Parameters.Add(dbPara[i]);
                ret = cmd.ExecuteNonQuery();
                return ret;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary> 
        /// ִ��Sql��ѯ��䲢���ص�һ�еĵ�һ����¼,����ֵΪobject ʹ��ʱ��Ҫ������� -> Unbox 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>object ����ֵ </returns> 
        public override object ExecuteScalar(string sqlstr)
        {
            object obj = new object();
            openConnection();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlstr;
                obj = cmd.ExecuteScalar();
                closeConnection();
                return obj;
            }
            catch
            {
                return null;
            }
           
        }


        /// <summary> 
        /// ִ��Sql��ѯ���,ͬʱ���������� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        public override void ExecuteSqlWithTransaction(string sqlstr)
        {
            SqlTransaction trans;
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
        /// ����ָ��Sql����OracleDataReader����ע�⣬��ʹ�ú���رձ�����ͬʱ���Զ�����closeConnection()���ر����ݿ����� 
        /// �����ر����ݿ����� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>OracleDataReader����</returns> 
        public override DbDataReader GetDataReader(string sqlstr)
        {
            DbDataReader dr;
            SqlDataReader drSql = null;
            try
            {
                openConnection();
                cmd.CommandText = sqlstr;
                cmd.CommandType = CommandType.Text;
                drSql = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    drSql.Close();
                    closeConnection();
                }
                catch
                {
                }
            }
            dr = drSql;
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
            SqlDataReader drSql = null;
            try
            {
                openConnection();
                cmd.Parameters.Clear();
                cmd.CommandText = sqlstr;
                cmd.CommandType = CommandType.Text;
                drSql = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dr = drSql;
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.Message);
                try
                {
                    if (drSql != null && !drSql.IsClosed)
                        drSql.Close();
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
        /// ����ָ��Sql����DataSet 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>DataSet</returns> 
        public override DataSet GetDataSet(string sqlstr)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            openConnection();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstr;
            da.SelectCommand = cmd;
            da.Fill(ds);
            closeConnection();
            return ds;
        }


        /// <summary> 
        /// ����ָ��Sql����DataSet 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <param name="ds">���������DataSet����</param> 
        public override void GetDataSet(string sqlstr, ref DataSet ds)
        {
            SqlDataAdapter da = new SqlDataAdapter();

            openConnection();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstr;
            da.SelectCommand = cmd;
            da.Fill(ds);
            closeConnection();
        }


        /// <summary>
        /// ���ݣӣѣ��������������DataSet,�Ա�����Ϊ����ÿ��DataTable������
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public override DataSet GetDataSet(params string[] sqlstr)
        {
            int startPos, endPos;
            string tableName;
            SqlDataAdapter da = new SqlDataAdapter();
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
                da.Fill(ds,tableName);
            }
            closeConnection();
            return ds;
        }


        /// <summary>
        /// ���ݣӣѣ��������������DataSet,�Ա�����Ϊ����ÿ��DataTable������
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public override void GetDataSet(ref DataSet ds, params string[] sqlstr)
        {
            int startPos, endPos;
            string tableName;
            SqlDataAdapter da = new SqlDataAdapter();

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
        }


        /// <summary> 
        /// ����ָ��Sql����DataTable 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>DataTable</returns> 
        public override DataTable GetDataTable(string sqlstr)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable datatable = new DataTable();
            
            openConnection();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstr;
            da.SelectCommand = cmd;
            da.Fill(datatable);
            closeConnection();
            return datatable;
        }


        /// <summary> 
        /// ִ��ָ��Sql���,ͬʱ������DataTable���и�ֵ 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <param name="dt">ref DataTable dt </param> 
        public override void GetDataTable(string sqlstr, ref DataTable dt)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            openConnection();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlstr;
            da.SelectCommand = cmd;
            da.Fill(dt);
            closeConnection();
        }


        /// <summary> 
        /// ִ�д������洢���̲��������ݼ��� 
        /// </summary> 
        /// <param name="procName">�洢��������</param> 
        /// <param name="parameters">SqlParameterCollection �������</param> 
        /// <returns></returns> 
        public override DataTable GetDataTable(string procName, DbParameterCollection parameters)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable datatable = new DataTable();

            openConnection();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            foreach (SqlParameter para in parameters)
            {
                SqlParameter p = (SqlParameter)para;
                cmd.Parameters.Add(p);
            }
            da.SelectCommand = cmd;
            da.Fill(datatable);
            cmd.Parameters.Clear();  //���ж�Ҫ����
            closeConnection();

            return datatable;
        }

        public override DataTable GetDataTableByParams(string sql, params DbParameter[] parameters)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable datatable = new DataTable();

            openConnection();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            for(int i=0;i<parameters.Length;i++)
            {
                SqlParameter p = (SqlParameter)parameters[i];
                cmd.Parameters.Add(p);
            }
            da.SelectCommand = cmd;
            da.Fill(datatable);
            cmd.Parameters.Clear();

            closeConnection();

            return datatable;
        }



        /// <summary> 
        /// ִ��ָ��Sql���,����DataView 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        public override DataView GetDataView(string sqlstr)
        {
            SqlDataAdapter da = new SqlDataAdapter();
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
        /// ����ָ�����ĳ�����ֵ,�Ѿ���1����Ҫ���ڸ��������TID��ֵ
        /// </summary> 
        /// <param name="tableName">���ݿ����</param> 
        /// <param name="fieldName">���ݿ�����</param> 
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
        /// ��������������ָ�����Ѿ����ڼ�¼ĳ�е����ֵ
        /// </summary>
        /// <param name="tableName">���ݿ����</param>
        /// <param name="fieldName">���ݿ�����</param>
        /// <param name="wheres">����</param>
        /// <returns>����unit���͵�ֵ</returns>
        public override uint GetMaxNum(string tableName, string fieldName, string wheres)
        {
            string sql;
            uint value;
            sql = "select max(" + fieldName + ") from " + tableName +" where "+ wheres;
            object re = ExecuteScalar(sql);
            if (re is System.DBNull)
                value = 0;
            else
                value = Convert.ToUInt32(re);
            return value;
        }


        /// <summary> 
        /// ִ�д洢���� 
        /// </summary> 
        /// <param name="procName">�洢������</param> 
        /// <param name="coll">SqlParameter ����</param> 
        public override void ExecutePorcedure(string procName, DbParameter[] coll)
        {
            openConnection();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
          
            for (int i = 0; i < coll.Length; i++)
            {
                cmd.Parameters.Add(coll[i]);
            }

            cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            closeConnection();
        }


        /// <summary> 
        /// ִ�д洢���̲��������ݼ� 
        /// </summary> 
        /// <param name="procName">�洢��������</param> 
        /// <param name="coll">SqlParameter����</param> 
        /// <param name="ds">DataSet </param> 
        public override void ExecutePorcedure(string procName, DbParameter[] coll, ref DataSet ds)
        {
            SqlDataAdapter da = new SqlDataAdapter();
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

            for (i = 0; i <= fields.GetUpperBound(0); i++)
            {
                if (fields[i].fieldName == null || fields[i].fieldName == "") continue;   //û����Ա������

                if (!(fields[i].fieldValue==null || fields[i].fieldValue.Trim()==""))   //ֵΪ�ջ���Ϊ�����Ĳ�����
                {
                    field.Append(fields[i].fieldName + ",");
            
                    if (fields[i].fieldType == FieldType.String)
                    {
                        values.Append("'" + fields[i].fieldValue.Replace('\'', '��') + "',");
                    }
                    else if (fields[i].fieldType == FieldType.Int)
                    {
                        values.Append(fields[i].fieldValue + ",");
                    }
                    else
                    {
                        values.Append("'" + fields[i].fieldValue + "',");  //����ʱ����yyyy-mm-dd hh:mm:ss��ʾ
                    }
                }
             }
             field.Remove(field.Length - 1, 1);
             values.Remove(values.Length - 1, 1);

            sql = "insert into " + tableName + "(" + field.ToString() + ") values(" + values.ToString() + ")";
            return sql;
        }


        /// <summary>
        /// ���ݴ����е�����ؼ��õ�UPDATE��SQL��䣬Ȼ�󽻸�OracleHelper����
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

            for (i = 0; i <= fields.GetUpperBound(0); i++)
            {
                if (fields[i].fieldName == null || fields[i].fieldName == "") continue;   //û����Ա������

                if (fields[i].fieldValue != null)
                {
                    if (fields[i].fieldType == FieldType.String)
                    {
                        para.Append( fields[i].fieldName + "='" + fields[i].fieldValue + "',");
                    }
                    else if (fields[i].fieldType == FieldType.Int)
                    {
                        if (fields[i].fieldValue!="")
                            para.Append( fields[i].fieldName + "=" + fields[i].fieldValue + ",");
                        else
                            para.Append(fields[i].fieldName + "=NULL,");   //���Ϊ�գ�����Ϊ�գ�������ֵ�͵������ڽ����ϲ���ɾ��
                    }
                    else
                    {
                        if (fields[i].fieldValue != "")
                            para.Append( fields[i].fieldName + "='" + fields[i].fieldValue + "',");  //����ʱ����yyyy-mm-dd hh:mm:ss��ʾ
                    }
                }
                else
                {
                    para.Append( fields[i].fieldName + "=NULL,");
                }
            }
            para.Remove(para.Length - 1, 1);

            for (i = 0; i <= wheres.GetUpperBound(0); i++)
            {
                if (wheres[i].fieldType == FieldType.String)
                {
                    where.Append( wheres[i].fieldName + wheres[i].optType + "'" + wheres[i].fieldValue + "' " + wheres[i].relationType + " ");
                }
                else if (fields[i].fieldType == FieldType.Int)
                {
                    where.Append( wheres[i].fieldName + wheres[i].optType + wheres[i].fieldValue + wheres[i].relationType + " ");
                }
                else
                {
                    where.Append( wheres[i].fieldName + wheres[i].optType + " '" + wheres[i].fieldValue + "'" + wheres[i].relationType + " ");
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
            StringBuilder wh=new StringBuilder();

            for (int i = 0; i <= wheres.GetUpperBound(0); i++)
            {
                if (wheres[i].fieldName == "" || wheres[i].fieldName==null) continue;
                if (wheres[i].fieldType == FieldType.String)
                {
                    wh.Append( wheres[i].fieldName + wheres[i].optType + "'" + wheres[i].fieldValue + "' " + wheres[i].relationType + " ");
                }
                else if (wheres[i].fieldType == FieldType.Int)
                {
                    wh.Append( wheres[i].fieldName + wheres[i].optType + wheres[i].fieldValue + wheres[i].relationType + " ");
                }
                else
                {
                    wh.Append( wheres[i].fieldName + wheres[i].optType + " '" + wheres[i].fieldValue + "'" + wheres[i].relationType + " ");
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
            sql = " select count(*) from " + tableName + " where " + wheres;
           
            obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null) return false;
            if (Convert.ToInt16(obj) == 0)
                return false;
            else
                return true;
        }


    
    }
}

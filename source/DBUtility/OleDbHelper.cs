using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;

namespace PlatForm.DBUtility
{

    /// <summary>
    /// OleDb���ݿ⴦����󣬸������Oracle,Microsoft SQL Server֮������ݿ⽻����
    /// ��Ҫ��Sybase��DB2��MySQL���ݿ�
    /// ���Ʒ�   2007-8-4
    /// </summary>
    
    public class OleDbHelper : DBHelper
    {
        public static OleDbConnection mainConn = new OleDbConnection();
        public static OleDbConnection slaveConn = new OleDbConnection();
        static OleDbCommand cmd = new OleDbCommand();

        /// <summary> 
        /// ��̬���캯�������������������� 
        /// </summary> 
        static OleDbHelper()
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
        /// ִ��Sql��ѯ��䲢���ص�һ�еĵ�һ����¼,����ֵΪobject ʹ��ʱ��Ҫ������� -> Unbox 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>object ����ֵ </returns> 
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
        /// ִ��Sql��ѯ���,ͬʱ���������� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
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
        /// ����ָ��Sql����OleDbDataReader����ע�⣬��ʹ�ú���رձ�����ͬʱ���Զ�����closeConnection()���ر����ݿ����� 
        /// �����ر����ݿ����� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>OleDbDataReader����</returns> 
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
        /// ����ָ��Sql����OleDbDataReader����ע�⣬��ʹ�ú���رձ�����ͬʱ���Զ�����closeConnection()���ر����ݿ����� 
        /// �����ر����ݿ����� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <param name="dr">�����ref DataReader ����</param> 
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
        /// ����ָ��Sql����DataSet 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
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
        /// ���ݣӣѣ��������������DataSet,�Ա�����Ϊ����ÿ��DataTable������
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
        /// ����ָ��Sql����DataSet 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <param name="ds">���������DataSet����</param> 
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
        /// ����ָ��Sql����DataTable 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
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
        /// ִ��ָ��Sql���,ͬʱ������DataTable���и�ֵ 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
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
        /// ִ�д������洢���̲��������ݼ��� 
        /// </summary> 
        /// <param name="procName">�洢��������</param> 
        /// <param name="parameters">SqlParameterCollection �������</param> 
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
        /// ִ��ָ��Sql���,����DataView 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
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
        /// ����ָ�����ĳ�����ֵ
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
        /// ִ�д洢���� 
        /// </summary> 
        /// <param name="procName">�洢������</param> 
        /// <param name="coll">OleDbParameter ����</param> 
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
        /// ִ�д洢���̲��������ݼ� 
        /// </summary> 
        /// <param name="procName">�洢��������</param> 
        /// <param name="coll">OleDbParameter����</param> 
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
        /// ����GetSchema������������б���ͼ
        /// </summary>
        /// <param name="srestrictionValues">table/view/....</param>
        /// <returns>��ĵ��ĸ�ΪTABLE_NAME</returns>
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
        /// ����GetSchema���������������ֶ�
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns>��ĵ��ĸ�ΪCOLUMN_NAME</returns>
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

                if (!(fields[i].fieldValue == null || fields[i].fieldValue.Trim() == ""))   //ֵΪ�ջ���Ϊ�����Ĳ�����
                {
                    field.Append(fields[i].fieldName + ",");

                    if (fields[i].fieldType == FieldType.String)
                    {
                        values.Append("'" + fields[i].fieldValue.Replace('\'','��') + "',");    //Ҫ�������а���'����ȫ��״̬�ġ�
                    }
                    else if (fields[i].fieldType == FieldType.Int)
                    {
                        values.Append(fields[i].fieldValue + ",");
                    }
                    else
                    {
                        values.Append("'" + fields[i].fieldValue + "',");  //Sybase��������ʱ��yyyy-mm-dd hh:mm:ss��ʾ��MySql��DB2��Ҫ�ٿ���
                    }
                }
            }
            field.Remove(field.Length - 1, 1);
            values.Remove(values.Length - 1, 1);

            sql = "insert into " + tableName + "(" + field.ToString() + ") values(" + values.ToString() + ")";
            return sql;
        }


        /// <summary>
        /// ���ݴ����е�����ؼ��õ�UPDATE��SQL��䣬Ȼ�󽻸�OleDbHelper����
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
                        para.Append(fields[i].fieldName + "='" + fields[i].fieldValue.Replace('\'', '��') + "',");
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
                            para.Append(fields[i].fieldName + "='" + fields[i].fieldValue + "',");  //����ʱ����yyyy-mm-dd hh:mm:ss��ʾ
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

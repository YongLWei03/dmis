using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;



namespace PlatForm.DBUtility
{
    public class DBHelper
    {
        public static readonly bool isDoubleDatabase;         //�Ƿ�˫������
        public static readonly string databaseType;           //���ݿ����� 
        public static readonly string mainConnectString;      //�����ݿ���������Ӵ�
        public static readonly string slaveConnectString;     //�����ݿ���������Ӵ�
        

        static DBHelper()
        {
            string connectName, slaveConnectName;

            databaseType = ConfigurationManager.AppSettings["DatabaseType"];
            string doubleDatabase = ConfigurationManager.AppSettings["DoubleDatabase"];

            if (databaseType == "Oracle")
            {
                connectName="OraConnStringMain";
                slaveConnectName = "OraConnStringSlave";
            }
            else if (databaseType == "SqlServer")
            {
                connectName = "SqlConnStringMain";
                slaveConnectName = "SqlConnStringSlave";
            }
            else if (databaseType == "Sybase")
            {
                connectName = "SybConnStringMain";
                slaveConnectName = "SybConnStringSlave";
            }
            else if (databaseType == "Access")
            {
                connectName = "AccessConnStringMain";
                slaveConnectName = "AccessConnStringSlave";
            }
            else
            {
                connectName = "OdbcConnStringMain";
                slaveConnectName = "OdbcConnStringSlave";
            }
            mainConnectString = ConfigurationManager.AppSettings[connectName];
            if (doubleDatabase == "Yes")
            {
                isDoubleDatabase = true;
                slaveConnectString = ConfigurationManager.AppSettings[slaveConnectName];
            }
            else
            {
                isDoubleDatabase = false;
                slaveConnectString = null;
            }

        }

        public virtual void OpenConnection()
        {
        }

        public virtual void CloseConnection()
        {
        }

        public virtual int ExecuteSql(string sqlstr)
        {
            return -1;
        }


        public virtual int ExecuteSqlByParas(string sqlstr,params DbParameter[] dbPara)
        {
            return -1;
        }

        /// <summary>
        /// ͨ��������ִ��
        /// </summary>
        /// <param name="sqlstr">sql���</param>
        /// <param name="dbPara"></param>
        /// <returns></returns>
        public virtual int ExecuteByParameter(string sqlstr, DbParameterCollection dbPara)
        {
            return -1;
        }


        /// <summary> 
        /// ִ��Sql��ѯ��䲢���ص�һ�еĵ�һ����¼,����ֵΪobject ʹ��ʱ��Ҫ������� -> Unbox 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>object ����ֵ </returns> 
        public virtual object ExecuteScalar(string sqlstr)
        {
           return new object();
        }

        /// <summary> 
        /// ִ��Sql��ѯ���,ͬʱ���������� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        public virtual void ExecuteSqlWithTransaction(string sqlstr)
        {
        }

        /// <summary>
        /// ִ�ж��Sql�������,���������� 
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public virtual int ExecuteSqlWithTransaction(params string[] sqlstr)
        {
            return -1;
        }


        /// <summary> 
        /// ����ָ��Sql����DbDataReader 
        /// �����ر����ݿ����� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>DBDataReader����</returns> 
        public virtual DbDataReader GetDataReader(string sqlstr)
        {
            //�����ܷ��س����࣬���÷����ݷ��ʷֹ�������DataReader����

            if (databaseType == "Oracle")
            {
                System.Data.OracleClient.OracleCommand cmd = new System.Data.OracleClient.OracleCommand();
                System.Data.OracleClient.OracleDataReader oraDr = cmd.ExecuteReader();
                return oraDr;
            }
            else if (databaseType == "SqlServer")
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                System.Data.SqlClient.SqlDataReader sqlDr = cmd.ExecuteReader();
                return sqlDr;
            }
            else if (databaseType == "Sybase")
            {
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
                System.Data.OleDb.OleDbDataReader oleDr = cmd.ExecuteReader();
                return oleDr;
            }
            else
            {
                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand();
                System.Data.Odbc.OdbcDataReader odbcDr = cmd.ExecuteReader();
                return odbcDr;
            }
        }


        /// <summary> 
        /// ����ָ��Sql����DbDataReader����ע�⣬��ʹ�ú���رձ�����ͬʱ���Զ�����closeConnection()���ر����ݿ����� 
        /// �����ر����ݿ����� 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <param name="dr">�����ref DataReader ����</param> 
        public virtual void GetDataReader(string sqlstr, ref DbDataReader dr)
        {
        }


        /// <summary> 
        /// ����ָ��Sql����DataSet 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>DataSet</returns> 
        public virtual DataSet GetDataSet(string sqlstr)
        {
            return new DataSet();
        }

        /// <summary>
        /// ���ݣӣѣ��������������DataSet,�Ա�����Ϊ����ÿ��DataTable������
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public virtual DataSet GetDataSet(params string[] sqlstr)
        {
            return new DataSet();
        }


        /// <summary>
        /// ���ݣӣѣ��������������DataSet,�Ա�����Ϊ����ÿ��DataTable������
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public virtual void  GetDataSet(ref DataSet ds, params string[] sqlstr)
        {
            
        }


        /// <summary> 
        /// ����ָ��Sql����DataSet 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <param name="ds">���������DataSet����</param> 
        public virtual void GetDataSet(string sqlstr, ref DataSet ds)
        {
        }

        
         
        /// <summary> 
        /// ����ָ��Sql����DataTable 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <returns>DataTable</returns> 
        public virtual DataTable GetDataTable(string sqlstr)
        {
            return new DataTable();
        }

        /// <summary> 
        /// ִ��ָ��Sql���,ͬʱ������DataTable���и�ֵ 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        /// <param name="dt">ref DataTable dt </param> 
        public virtual void GetDataTable(string sqlstr, ref DataTable dt)
        {
        }

        /// <summary>
        /// ͨ��sql�������ݿ��������ȡDataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual DataTable GetDataTableByParams(string sql, params DbParameter[] parameters)
        {
            return new DataTable();
        }
         /// <summary> 
        /// ִ�д������洢���̲��������ݼ��� 
        /// </summary> 
        /// <param name="procName">�洢��������</param> 
        /// <param name="parameters">DbParameterCollection �������</param> 
        /// <returns></returns> 
        public virtual DataTable GetDataTable(string procName, DbParameterCollection parameters)
        {
            return new DataTable();
        }

        /// <summary> 
        /// ִ��ָ��Sql���,����DataView 
        /// </summary> 
        /// <param name="sqlstr">�����Sql���</param> 
        public virtual DataView GetDataView(string sqlstr)
        {
            return new DataView();
        }

         /// <summary> 
         /// ִ�д洢���� 
         /// </summary> 
         /// <param name="procName">�洢������</param> 
        /// <param name="coll">DbParameter ����</param> 
        public virtual void ExecutePorcedure(string procName, DbParameter[] coll)
        {
        }


         /// <summary> 
         /// ִ�д洢���̲��������ݼ� 
         /// </summary> 
         /// <param name="procName">�洢��������</param> 
        /// <param name="coll">DbParameter����</param> 
         /// <param name="ds">DataSet </param> 
        public virtual void ExecutePorcedure(string procName, DbParameter[] coll, ref DataSet ds)
        {
        }
        

        /// <summary> 
        /// ����ָ�����ĳ�����ֵ
        /// </summary> 
        /// <param name="tableName">���ݿ����</param> 
        /// <param name="fieldName">���ݿ�����</param> 
        public virtual uint GetMaxNum(string tableName, string fieldName)
        {
            return 0;
        }

        /// <summary>
        /// ��������������ָ��������ֵ
        /// </summary>
        /// <param name="tableName">���ݿ����</param>
        /// <param name="fieldName">���ݿ�����</param>
        /// <param name="wheres">����</param>
        /// <returns>����unit���͵�ֵ</returns>
        public virtual uint GetMaxNum(string tableName, string fieldName, string wheres)
        {
            return 0;
        }

        /// <summary>
        /// ����GetSchema������������б���ͼ
        /// </summary>
        /// <param name="srestrictionValues">table/view/....</param>
        /// <returns>��ĵ��ĸ�ΪTABLE_NAME</returns>
        public virtual DataTable GetDataTableS(string srestrictionValues) {
            return new DataTable();
        }
         /// <summary>
        /// ����GetSchema���������������ֶ�
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns>��ĵ��ĸ�ΪCOLUMN_NAME</returns>
        public virtual DataTable GetColumnS(string TableName) {
            return new DataTable();
        }
                /// <summary>
        /// ͨ����ѯ��䷵�ض�Ӧ�Ĳ�ѯֵ
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns>string</returns>
        public virtual void GetValue(string strSql, ref string sReturt)
        { 
            
        }
        /// <summary>
        /// ��ȡ�������͵�ֵ
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="iResult"></param>
        public virtual void GetValue(string strSql, ref int iResult) { 
        
        }
        /// <summary>
                /// ��ȡ��ѯ����ļ�
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="sSplit"></param>
        /// <param name="sReturn"></param>
        public virtual void GetValueS(string strSql, string sSplit,ref string sReturn)
        {

        }


        /// <summary>
        /// ���ݴ����е�����ؼ��õ�INSERT��SQL��䣬Ȼ�󽻸�r����
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="fields">�ؼ���ֵ���͡�ֵ</param>
        /// <returns>�õ���sql���</returns>
        public virtual string GetInserSql(string tableName, params FieldPara[] fields)
        {
            return "";
        }


        /// <summary>
        /// ���ݴ����е�����ؼ��õ�UPDATE��SQL��䣬Ȼ�󽻸�OracleHelper����
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="fields">�ؼ���ֵ���͡�ֵ</param>
        /// <param name="wheres">����</param>
        /// <returns>�õ���sql���</returns>
        public virtual string GetUpdateSql(string tableName, FieldPara[] fields, params WherePara[] wheres)
        {
            return "";
        }


        /// <summary>
        /// ���Զ����ѯʱ�������ѯ����
        /// </summary>
        /// <param name="wheres"></param>
        /// <returns></returns>
        public virtual string GetQueryWheres(params WherePara[] wheres)
        {
            return "";
        }

        /// <summary>
        /// �жϼ�¼�Ƿ����
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="wheres">����</param>
        /// <returns>true���ڣ�false������</returns>
        public virtual bool IsExist(string tableName, string wheres)
        {
            return false;
        }

        /// <summary>
        /// ˫������ģʽ�£���������ʧЧʱ����insert,delete,update�����sql��䱣�浽��DMIS_SYS_MISSING_SQL��
        /// </summary>
        /// <param name="sql">Ҫ�����SQL���</param>
        /// <returns></returns>
        public virtual int InsertMissingSql(string sql)
        {
            return 0;
        }

        /// <summary>
        /// ��DataTable�е����ݲ��뵽���ݿ�����Ӧ�ı��У�DataTable�����ƾ��Ǳ���
        /// </summary>
        /// <param name="values">������ݵ�DataTable</param>
        /// <param name="FailCounts">ʧ�ܵ�����</param>
        /// <returns>�ɹ�������</returns>
        public virtual int InsertByDataTable(DataTable values,ref int FailCounts)
        {
            return 0;
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
        public virtual DataTable GetPagedDataTable(int pageIndex, int pageSize,ref int pageCounts,ref int totalRows, string sql)
        {
            return new DataTable();
        }

    }
}

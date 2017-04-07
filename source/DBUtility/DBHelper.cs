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
        public static readonly bool isDoubleDatabase;         //是否双服务器
        public static readonly string databaseType;           //数据库类型 
        public static readonly string mainConnectString;      //主数据库服务器连接串
        public static readonly string slaveConnectString;     //备数据库服务器连接串
        

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
        /// 通过参数来执行
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        /// <param name="dbPara"></param>
        /// <returns></returns>
        public virtual int ExecuteByParameter(string sqlstr, DbParameterCollection dbPara)
        {
            return -1;
        }


        /// <summary> 
        /// 执行Sql查询语句并返回第一行的第一条记录,返回值为object 使用时需要拆箱操作 -> Unbox 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>object 返回值 </returns> 
        public virtual object ExecuteScalar(string sqlstr)
        {
           return new object();
        }

        /// <summary> 
        /// 执行Sql查询语句,同时进行事务处理 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        public virtual void ExecuteSqlWithTransaction(string sqlstr)
        {
        }

        /// <summary>
        /// 执行多个Sql更新语句,进行事务处理 
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public virtual int ExecuteSqlWithTransaction(params string[] sqlstr)
        {
            return -1;
        }


        /// <summary> 
        /// 返回指定Sql语句的DbDataReader 
        /// 方法关闭数据库连接 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>DBDataReader对象</returns> 
        public virtual DbDataReader GetDataReader(string sqlstr)
        {
            //程序不能返回抽象类，故用分数据访问分工来生成DataReader对象

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
        /// 返回指定Sql语句的DbDataReader，请注意，在使用后请关闭本对象，同时将自动调用closeConnection()来关闭数据库连接 
        /// 方法关闭数据库连接 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <param name="dr">传入的ref DataReader 对象</param> 
        public virtual void GetDataReader(string sqlstr, ref DbDataReader dr)
        {
        }


        /// <summary> 
        /// 返回指定Sql语句的DataSet 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>DataSet</returns> 
        public virtual DataSet GetDataSet(string sqlstr)
        {
            return new DataSet();
        }

        /// <summary>
        /// 根据ＳＱＬ语句数组来返回DataSet,以表名作为其中每个DataTable的名称
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public virtual DataSet GetDataSet(params string[] sqlstr)
        {
            return new DataSet();
        }


        /// <summary>
        /// 根据ＳＱＬ语句数组来返回DataSet,以表名作为其中每个DataTable的名称
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public virtual void  GetDataSet(ref DataSet ds, params string[] sqlstr)
        {
            
        }


        /// <summary> 
        /// 返回指定Sql语句的DataSet 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <param name="ds">传入的引用DataSet对象</param> 
        public virtual void GetDataSet(string sqlstr, ref DataSet ds)
        {
        }

        
         
        /// <summary> 
        /// 返回指定Sql语句的DataTable 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <returns>DataTable</returns> 
        public virtual DataTable GetDataTable(string sqlstr)
        {
            return new DataTable();
        }

        /// <summary> 
        /// 执行指定Sql语句,同时给传入DataTable进行赋值 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        /// <param name="dt">ref DataTable dt </param> 
        public virtual void GetDataTable(string sqlstr, ref DataTable dt)
        {
        }

        /// <summary>
        /// 通过sql语句和数据库参数来获取DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual DataTable GetDataTableByParams(string sql, params DbParameter[] parameters)
        {
            return new DataTable();
        }
         /// <summary> 
        /// 执行带参数存储过程并返回数据集合 
        /// </summary> 
        /// <param name="procName">存储过程名称</param> 
        /// <param name="parameters">DbParameterCollection 输入参数</param> 
        /// <returns></returns> 
        public virtual DataTable GetDataTable(string procName, DbParameterCollection parameters)
        {
            return new DataTable();
        }

        /// <summary> 
        /// 执行指定Sql语句,返回DataView 
        /// </summary> 
        /// <param name="sqlstr">传入的Sql语句</param> 
        public virtual DataView GetDataView(string sqlstr)
        {
            return new DataView();
        }

         /// <summary> 
         /// 执行存储过程 
         /// </summary> 
         /// <param name="procName">存储过程名</param> 
        /// <param name="coll">DbParameter 集合</param> 
        public virtual void ExecutePorcedure(string procName, DbParameter[] coll)
        {
        }


         /// <summary> 
         /// 执行存储过程并返回数据集 
         /// </summary> 
         /// <param name="procName">存储过程名称</param> 
        /// <param name="coll">DbParameter集合</param> 
         /// <param name="ds">DataSet </param> 
        public virtual void ExecutePorcedure(string procName, DbParameter[] coll, ref DataSet ds)
        {
        }
        

        /// <summary> 
        /// 返回指定表的某列最大值
        /// </summary> 
        /// <param name="tableName">数据库表名</param> 
        /// <param name="fieldName">数据库列名</param> 
        public virtual uint GetMaxNum(string tableName, string fieldName)
        {
            return 0;
        }

        /// <summary>
        /// 根据条件，返回指定表的最大值
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">数据库列名</param>
        /// <param name="wheres">条件</param>
        /// <returns>返回unit类型的值</returns>
        public virtual uint GetMaxNum(string tableName, string fieldName, string wheres)
        {
            return 0;
        }

        /// <summary>
        /// 利用GetSchema返回所需的所有表、视图
        /// </summary>
        /// <param name="srestrictionValues">table/view/....</param>
        /// <returns>表的第四个为TABLE_NAME</returns>
        public virtual DataTable GetDataTableS(string srestrictionValues) {
            return new DataTable();
        }
         /// <summary>
        /// 利用GetSchema返回所需表的所有字段
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns>表的第四个为COLUMN_NAME</returns>
        public virtual DataTable GetColumnS(string TableName) {
            return new DataTable();
        }
                /// <summary>
        /// 通过查询语句返回对应的查询值
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns>string</returns>
        public virtual void GetValue(string strSql, ref string sReturt)
        { 
            
        }
        /// <summary>
        /// 获取整数类型的值
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="iResult"></param>
        public virtual void GetValue(string strSql, ref int iResult) { 
        
        }
        /// <summary>
                /// 获取查询结果的集
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="sSplit"></param>
        /// <param name="sReturn"></param>
        public virtual void GetValueS(string strSql, string sSplit,ref string sReturn)
        {

        }


        /// <summary>
        /// 根据窗口中的输入控件得到INSERT　SQL语句，然后交给r处理
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">控件、值类型、值</param>
        /// <returns>得到的sql语句</returns>
        public virtual string GetInserSql(string tableName, params FieldPara[] fields)
        {
            return "";
        }


        /// <summary>
        /// 根据窗口中的输入控件得到UPDATE　SQL语句，然后交给OracleHelper处理
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">控件、值类型、值</param>
        /// <param name="wheres">条件</param>
        /// <returns>得到的sql语句</returns>
        public virtual string GetUpdateSql(string tableName, FieldPara[] fields, params WherePara[] wheres)
        {
            return "";
        }


        /// <summary>
        /// 在自定义查询时，构造查询条件
        /// </summary>
        /// <param name="wheres"></param>
        /// <returns></returns>
        public virtual string GetQueryWheres(params WherePara[] wheres)
        {
            return "";
        }

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="wheres">条件</param>
        /// <returns>true存在，false不存在</returns>
        public virtual bool IsExist(string tableName, string wheres)
        {
            return false;
        }

        /// <summary>
        /// 双服务器模式下，备服务器失效时，把insert,delete,update三类的sql语句保存到表DMIS_SYS_MISSING_SQL中
        /// </summary>
        /// <param name="sql">要保存的SQL语句</param>
        /// <returns></returns>
        public virtual int InsertMissingSql(string sql)
        {
            return 0;
        }

        /// <summary>
        /// 把DataTable中的数据插入到数据库中相应的表中，DataTable的名称就是表名
        /// </summary>
        /// <param name="values">存放数据的DataTable</param>
        /// <param name="FailCounts">失败的条数</param>
        /// <returns>成功的条件</returns>
        public virtual int InsertByDataTable(DataTable values,ref int FailCounts)
        {
            return 0;
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
        public virtual DataTable GetPagedDataTable(int pageIndex, int pageSize,ref int pageCounts,ref int totalRows, string sql)
        {
            return new DataTable();
        }

    }
}

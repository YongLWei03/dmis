using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PlatForm.DBUtility;

using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;

/// <summary>
/// 记录ＷＥＢ操作时的日志
/// </summary>
public class WebLog
{
    public static int InsertLog(string optType,string state,string content)
    {
        //if (HttpContext.Current.Session["MemberID"] == null || HttpContext.Current.Session["MemberName"] == null) return -1;  //身份失效不记录
        //如果在记录登录失败时,就不能记录，故把上一条取消
        if (content == null || content.Trim() == "") return -1;   //没有内容不记录
        uint maxTID;
        string sql,type;
        maxTID = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_LOG", "TID");

        if (optType == "")   //操作类型
        {
            type = content.Trim().Substring(0, 6);
            if (type.ToLower() == "insert")
                optType = "添加";
            else if(type.ToLower()=="delete")
                optType = "删除";
            else if(type.ToLower()=="update")
                optType = "修改";
            else
                optType = "未知("+type.ToLower()+")";
        }
        if (state == "") state = "成功";
        if (DBHelper.databaseType == "Oracle")
        {
            if (HttpContext.Current.Session["MemberID"] == null)  //记录登录失败时的日志
            {
                sql = "insert into DMIS_SYS_LOG(TID,OPT_TIME,IP,LOG_TYPE,STATE,CONTENT) values(" +
                        maxTID.ToString() + ",TO_DATE('" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "','DD-MM-YYYY HH24:MI:SS'),'" +
                        HttpContext.Current.Request.UserHostAddress + "','" + optType + "','" + state + "',:Con)";
            }
            else
            {
                sql = "insert into DMIS_SYS_LOG(TID,OPT_TIME,MEMBER_ID,MEMBER_NAME,IP,LOG_TYPE,STATE,CONTENT) values(" +
                        maxTID.ToString() + ",TO_DATE('" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "','DD-MM-YYYY HH24:MI:SS'),'" + HttpContext.Current.Session["MemberID"].ToString() +
                        "','" + HttpContext.Current.Session["MemberName"].ToString() +
                        "','" + HttpContext.Current.Request.UserHostAddress + "','" + optType + "','" + state + "',:Con)";
            }

            OracleParameter[] aPara = new OracleParameter[1];
            OracleParameter pContent = new OracleParameter("Con", OracleType.VarChar, 4000);
            pContent.Value = content;
            aPara[0] = pContent;
            return DBOpt.dbHelper.ExecuteSqlByParas(sql, aPara);
        }
        else if (DBHelper.databaseType == "SqlServer")
        {
            if (HttpContext.Current.Session["MemberID"] == null)  //记录登录失败时的日志
            {
                sql = "insert into DMIS_SYS_LOG(TID,OPT_TIME,IP,LOG_TYPE,STATE,CONTENT) values(" +
                        maxTID.ToString() + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                        "','" + HttpContext.Current.Request.UserHostAddress + "','" + optType + "','" + state + "',@Content)";
            }
            else
            {
                sql = "insert into DMIS_SYS_LOG(TID,OPT_TIME,MEMBER_ID,MEMBER_NAME,IP,LOG_TYPE,STATE,CONTENT) values(" +
                        maxTID.ToString() + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + HttpContext.Current.Session["MemberID"].ToString() +
                        "','" + HttpContext.Current.Session["MemberName"].ToString() +
                        "','" + HttpContext.Current.Request.UserHostAddress + "','" + optType + "','" + state + "',@Content)";
            }
            SqlParameter[] aPara = new SqlParameter[1];
            SqlParameter pContent = new SqlParameter("@Content", SqlDbType.Text,content.Length);
            pContent.Value = content;
            aPara[0] = pContent;
            return DBOpt.dbHelper.ExecuteSqlByParas(sql, aPara);
        }
        else if (DBHelper.databaseType == "Sybase")
        {
            if (HttpContext.Current.Session["MemberID"] == null)  //记录登录失败时的日志
            {
                sql = "insert into DMIS_SYS_LOG(TID,OPT_TIME,IP,LOG_TYPE,STATE,CONTENT) values(" +
                        maxTID.ToString() + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + 
                        "','" + HttpContext.Current.Request.UserHostAddress + "','" + optType + "','" + state + "',?)";
            }
            else
            {
                sql = "insert into DMIS_SYS_LOG(TID,OPT_TIME,MEMBER_ID,MEMBER_NAME,IP,LOG_TYPE,STATE,CONTENT) values(" +
                        maxTID.ToString() + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + HttpContext.Current.Session["MemberID"].ToString() +
                        ",'" + HttpContext.Current.Session["MemberName"].ToString() +
                        "','" + HttpContext.Current.Request.UserHostAddress + "','" + optType + "','" + state + "',?)";
            }
            OleDbParameter[] aPara = new OleDbParameter[1];
            OleDbParameter pContent = new OleDbParameter("@Content", OleDbType.LongVarChar, content.Length);
            pContent.Value = content;
            aPara[0] = pContent;
            return DBOpt.dbHelper.ExecuteSqlByParas(sql, aPara);
        }
        else if (DBHelper.databaseType == "ODBC")
        {
            return 1;
        }
        else
        {
            return 1;
        }
    }

}

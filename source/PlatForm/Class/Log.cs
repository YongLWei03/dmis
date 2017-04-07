using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PlatForm.DBUtility;

using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;

namespace PlatForm
{
    /// <summary>
    /// ��־��
    /// </summary>
    public class Log
    {
        public static int InsertLog(string optType, string state, string content)
        {
            if (content == null || content.Trim() == "") return -1;   //û�����ݲ���¼
            uint maxTID;
            string sql, type;
            maxTID = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_LOG", "TID");

            if (optType == "")   //��������
            {
                type = content.Trim().Substring(0, 6);
                if (type.ToLower() == "insert")
                    optType = "���";
                else if (type.ToLower() == "delete")
                    optType = "ɾ��";
                else if (type.ToLower() == "update")
                    optType = "�޸�";
                else
                    optType = "δ֪(" + type.ToLower() + ")";
            }
            if (state == "") state = "�ɹ�";

            sql = "insert into DMIS_SYS_LOG(TID,OPT_TIME,MEMBER_ID,MEMBER_NAME,IP,LOG_TYPE,STATE,CONTENT) values(" +
                    maxTID.ToString() + ",'" + DateTime.Now.ToString() + "','" + CMain.memberID +
                    "','" + CMain.memberName +
                    "','" + CMain.IP + "','" + optType + "','" + state + "',@Content)";

            SqlParameter[] aPara = new SqlParameter[1];
            SqlParameter pContent = new SqlParameter("@Content", SqlDbType.Text);
            pContent.Value = content;
            aPara[0] = pContent;
            return DBOpt.dbHelper.ExecuteSqlByParas(sql, aPara);

        }
    }
}

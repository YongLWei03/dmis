using System;
using System.Collections.Generic;
using System.Text;

using PlatForm.DBUtility;
using System.Data;

namespace PlatForm.Functions
{
    /// <summary>
    /// 珠海用户  ,从老系统中判断登录用户是否有某岗位
    /// 此类已经作废2008－9　敖云峰
    /// </summary>
    public class CheckRole
    {
        /// <summary>
        /// 根据单个岗位判断
        /// </summary>
        /// <param name="memberCode"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public static bool IsRole(string memberCode,string roleCode)
        {
            string sql = " select ROLE_CODE from T_PERSON_ROLE where PERSON_CODE='" + memberCode + "'";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (roleCode.Trim() == dt.Rows[i][0].ToString().Trim()) return true;
            }
            return false;
        }

        public static bool IsRole(string memberCode, params string [] roleCodes)
        {
            string sql = " select ROLE_CODE from T_PERSON_ROLE where PERSON_CODE='" + memberCode + "'";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for(int j=0;j<roleCodes.Length;j++)
                    if (roleCodes[j].Trim() == dt.Rows[i][0].ToString().Trim()) return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否是管理员
        /// </summary>
        /// <param name="memberCode"></param>
        /// <returns></returns>
        public static bool IsAdmin(string  memberCode)
        {
            string sql = " select count(*) from T_PERSON_ROLE where PERSON_CODE='" + memberCode + "' and ROLE_CODE='000'";
            object obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null)
            {
                return false;
            }
            else
            {
                if (Convert.ToInt16(obj) > 0)
                    return true;
                else
                    return false;
            }
         }

        /// <summary>
        /// 判断用户是否在此用户组中
        /// </summary>
        /// <param name="memeberCode">用户代码</param>
        /// <param name="GroupName">组代码</param>
        /// <returns></returns>
        public static bool IsGroup(string memeberCode, string GroupName)
        {
            string sql = "select count(*) from T_LOGIN where CODE='" + memeberCode + "' and GROUP_NAME='" + GroupName + "'";
            object obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null)
            {
                return false;
            }
            else
            {
                if (Convert.ToInt16(obj) > 0)
                    return true;
                else
                    return false;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PlatForm.DBUtility;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;

namespace PlatForm.Functions
{
    public class SetRight
    {
        /// <summary>
        /// 根据页面上控件的权限
        /// </summary>
        /// <param name="page">要设置的网页名</param>
        /// <param name="id">模块功能ＩＤ</param>
        /// <param name="roleIDs">角色ＩＤ列表</param>
        public static void SetPageRight(Page page, string id, string roleIDs)
        {
            WebControl wn;
            string fileName, sql;

            fileName = page.Request.FilePath;
            fileName = fileName.Substring(fileName.LastIndexOf('/') + 1);

            sql = "select CONTROL_NAME,CONTROL_PROPERTY,CONTROL_VALUE from DMIS_VIEW_PURVIEW_ROLE where MODULE_ID=" + id + " and WEB_FILE='" + fileName + "' and ROLE_ID in(" + roleIDs + ")";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wn = (WebControl)page.FindControl(dt.Rows[i][0].ToString());
                if (wn == null) continue;
                if (wn is TextBox)
                {
                    if (dt.Rows[i][1].ToString() == "ReadOnly")
                    {
                        TextBox txt = (TextBox)wn;
                        if (dt.Rows[i][2].ToString() == "true")
                            txt.ReadOnly = true;
                        else
                            txt.ReadOnly = false;
                    }
                    else if(dt.Rows[i][1].ToString() == "Enabled")
                    {
                        if (dt.Rows[i][2].ToString() == "true")
                            wn.Enabled = true;
                        else
                            wn.Enabled = false;
                    }
                    else if (dt.Rows[i][1].ToString() == "Visible")
                    {
                        if (dt.Rows[i][2].ToString() == "true")
                            wn.Visible = true;
                        else
                            wn.Visible = false;
                    }
                    else
                    {
                    }
                }
                else
                {
                    if (dt.Rows[i][1].ToString() == "Enabled")
                    {

                        if (dt.Rows[i][2].ToString() == "true")
                            wn.Enabled = true;
                        else
                            wn.Enabled = false;
                    }
                    else
                    {
                        if (dt.Rows[i][2].ToString() == "true")
                            wn.Visible = true;
                        else
                            wn.Visible = false;
                    }
                }
            }
        }


        /// <summary>
        /// 判断是否是系统管理员，系统管理员的角色ＩＤ是0
        /// </summary>
        /// <param name="MemberID">人员ＩＤ</param>
        /// <returns>为true 则是</returns>
        public static bool IsAdminitrator(string MemberID)
        {
            object obj;
            obj = DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_MEMBER_ROLE where MEMBER_ID=" + MemberID + " and ROLE_ID=0");
            if (obj == null)
                return false;
            else
            {
                if (Convert.ToInt16(obj) > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 根据某一功能的某个控件（经常是“添加”）控钮，来判断此登录对此功能是否有权限增、删、改记录
        /// </summary>
        /// <param name="funcID">功能ID</param>
        /// <param name="webFile">网页名称</param>
        /// <param name="controlName">控件名称</param>
        /// <param name="MemberID">人员ID</param>
        /// <returns></returns>
        public static bool IsModify(string funcID,string webFile,string controlName, string MemberID)
        {
            string sql = "select b.ROLE_ID from DMIS_SYS_PURVIEW a,DMIS_SYS_ROLE_PURVIEW b where a.MODULE_ID=" + funcID +
                " and a.WEB_FILE='" + webFile + "' and a.CONTROL_NAME='" + controlName + "' and a.CONTROL_PROPERTY='Enabled' and a.CONTROL_VALUE='true' and a.ID=b.PURVIEW_ID";

            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            if (dt == null) return false;

            string roles = ","+HttpContext.Current.Session["RoleIDs"].ToString() + ",";   //登录人员岗位列表
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (roles.IndexOf(","+dt.Rows[i][0].ToString() + ",") > -1)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 判断某人员是否有某岗位
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static bool IsRole(string memberID, string roleID)
        {
            string sql = " select count(*) from dmis_sys_member_role where member_id=" + memberID + " and role_id=" + roleID;
            object obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null) return false;
            if (Convert.ToInt16(obj) > 0)
                return true;
            else
                return false;
        }
       
        /// <summary>
        /// 判断某人员是否至少有多个岗位中的一个
        /// </summary>
        /// <param name="memberCode"></param>
        /// <param name="roleCodes"></param>
        /// <returns></returns>
        public static bool IsRole(string memberID, params string[] roleIDs)
        {
            string sql;
            object obj;
            for (int i = 0; i < roleIDs.Length; i++)
            {
                sql = " select count(*) from dmis_sys_member_role where member_id=" + memberID + " and role_id=" + roleIDs[i];
                obj = DBOpt.dbHelper.ExecuteScalar(sql);
                if (obj == null) continue;
                if (Convert.ToInt16(obj) > 0)
                    return true;
                else
                    continue;
            }
            return false;
        }

        /// <summary>
        /// 判断某人是否处于某组中
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="departID"></param>
        /// <returns></returns>
        public static bool IsDepart(string memberID, string departID)
        {
            string sql = " select count(*) from dmis_sys_member where id=" + memberID + " and depart_id=" + departID;
            object obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null) return false;
            if (Convert.ToInt16(obj) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断某人员有某角色且在某部门区域中
        /// 赤几有好多变电站，同时可以使用本系统，故要这样判断。
        /// 2009-7-8 ayf
        /// </summary>
        /// <param name="memberID">人员ID</param>
        /// <param name="departID">部门ID</param>
        /// <param name="roleID">岗位ID</param>
        /// <returns></returns>
        public static bool IsDepartRole(string memberID, string departID, string roleID)
        {
            //先判断在某部门
            string sql = " select count(*) from dmis_sys_member where id=" + memberID + " and depart_id=" + departID;
            object obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null) return false;
            if (Convert.ToInt16(obj) < 1) return false;

            //再判断有某岗位
            sql = " select count(*) from dmis_sys_member_role where member_id=" + memberID + " and role_id=" + roleID;
            obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null) return false;
            if (Convert.ToInt16(obj) > 0)
                return true;
            else
                return false;
        }
    }
}

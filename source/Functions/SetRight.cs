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
        /// ����ҳ���Ͽؼ���Ȩ��
        /// </summary>
        /// <param name="page">Ҫ���õ���ҳ��</param>
        /// <param name="id">ģ�鹦�ܣɣ�</param>
        /// <param name="roleIDs">��ɫ�ɣ��б�</param>
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
        /// �ж��Ƿ���ϵͳ����Ա��ϵͳ����Ա�Ľ�ɫ�ɣ���0
        /// </summary>
        /// <param name="MemberID">��Ա�ɣ�</param>
        /// <returns>Ϊtrue ����</returns>
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
        /// ����ĳһ���ܵ�ĳ���ؼ��������ǡ���ӡ�����ť�����жϴ˵�¼�Դ˹����Ƿ���Ȩ������ɾ���ļ�¼
        /// </summary>
        /// <param name="funcID">����ID</param>
        /// <param name="webFile">��ҳ����</param>
        /// <param name="controlName">�ؼ�����</param>
        /// <param name="MemberID">��ԱID</param>
        /// <returns></returns>
        public static bool IsModify(string funcID,string webFile,string controlName, string MemberID)
        {
            string sql = "select b.ROLE_ID from DMIS_SYS_PURVIEW a,DMIS_SYS_ROLE_PURVIEW b where a.MODULE_ID=" + funcID +
                " and a.WEB_FILE='" + webFile + "' and a.CONTROL_NAME='" + controlName + "' and a.CONTROL_PROPERTY='Enabled' and a.CONTROL_VALUE='true' and a.ID=b.PURVIEW_ID";

            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            if (dt == null) return false;

            string roles = ","+HttpContext.Current.Session["RoleIDs"].ToString() + ",";   //��¼��Ա��λ�б�
           
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
        /// �ж�ĳ��Ա�Ƿ���ĳ��λ
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
        /// �ж�ĳ��Ա�Ƿ������ж����λ�е�һ��
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
        /// �ж�ĳ���Ƿ���ĳ����
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
        /// �ж�ĳ��Ա��ĳ��ɫ����ĳ����������
        /// �༸�кö���վ��ͬʱ����ʹ�ñ�ϵͳ����Ҫ�����жϡ�
        /// 2009-7-8 ayf
        /// </summary>
        /// <param name="memberID">��ԱID</param>
        /// <param name="departID">����ID</param>
        /// <param name="roleID">��λID</param>
        /// <returns></returns>
        public static bool IsDepartRole(string memberID, string departID, string roleID)
        {
            //���ж���ĳ����
            string sql = " select count(*) from dmis_sys_member where id=" + memberID + " and depart_id=" + departID;
            object obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null) return false;
            if (Convert.ToInt16(obj) < 1) return false;

            //���ж���ĳ��λ
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

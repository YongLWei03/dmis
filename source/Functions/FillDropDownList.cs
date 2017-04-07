using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI.WebControls;
using PlatForm.DBUtility;
using PlatForm.CustomControlLib;
namespace PlatForm.Functions
{

    /// <summary>
    /// ������WEBҳ���DropDownList�ؼ����ֵ
    /// </summary>
    /// 
    public class FillDropDownList
    {
        /// <summary>
        /// ��䳧վ
        /// </summary>
        /// <param name="ddl"></param>
        public static void FillStation(ref DropDownList ddl)
        {
            string sql;
            sql = "select TID,���� from ��վ������ order by ���";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            ddl.Items.Clear();
            ddl.DataTextField = "����";
            ddl.DataValueField = "TID";
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, "");
        }

        /// <summary>
        /// ���ݳ�վ��������豸
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="stationCode"></param>
        public static void FillDeviceByStationName(ref DropDownList ddl, string stationName)
        {
            ddl.Items.Clear();
        }


        /// <summary>
        /// ���ݸ�λ�ɣ������Ա�б�
        /// </summary>
        /// <param name="ddl">�����б�ؼ�</param>
        /// <param name="RoleID">��λ�ɣ�</param>
        public static void FillMemberByRoleID(ref DropDownList ddl, string RoleID)
        {
            string sql;
            sql = "select MEMBER_NAME from DMIS_VIEW_DRPART_MEMBER_ROLE where ROLE_ID=" + RoleID + " order by DEPART_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            ddl.Items.Clear();
            ddl.DataTextField = "MEMBER_NAME";
            ddl.DataValueField = "MEMBER_NAME";
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, "");
        }


        /// <summary>
        /// ���ݲ��ţɣ������Ա�б�
        /// </summary>
        /// <param name="ddl">Ҫ���Ŀؼ�</param>
        /// <param name="DepartID">���ţɣ�</param>
        public static void FillMemberByDepartID(ref DropDownList ddl, string DepartID)
        {
            string sql;
            sql = "select MEMBER_NAME from DMIS_VIEW_DRPART_MEMBER_ROLE where DEPART_ID=" + DepartID;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            ddl.Items.Clear();
            ddl.DataTextField = "MEMBER_NAME";
            ddl.DataValueField = "MEMBER_NAME";
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, "");
        }
 
        /// <summary>
        /// ��ĳ�������������������ؼ�,������ValueColumn����
        /// </summary>
        /// <param name="ddl">Ҫ���Ŀؼ�</param>
        /// <param name="TableName">����</param>
        /// <param name="TextColumn">��ʾ��</param>
        /// <param name="ValueColumn">������</param>
        public static void FillByTable(ref DropDownList ddl, string TableName,string TextColumn,string ValueColumn)
        {
            string sql;
            sql = "select " + ValueColumn + "," + TextColumn + " from " + TableName + " order by " + ValueColumn;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            ddl.Items.Clear();
            ddl.DataTextField = TextColumn;
            ddl.DataValueField = ValueColumn;
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, "");
        }
        /// <summary>
        /// ��ĳ�������������������ؼ�
        /// </summary>
        /// <param name="ddl">Ҫ���Ŀؼ�</param>
        /// <param name="TableName">����</param>
        /// <param name="TextColumn">��ʾ��</param>
        /// <param name="ValueColumn">������</param>
        /// <param name="Orders">������������ʽ������ ����ʽ  ��:DATEM desc</param>
        public static void FillByTable(ref DropDownList ddl, string TableName, string TextColumn, string ValueColumn,string Orders)
        {
            string sql;
            sql = "select " + ValueColumn + "," + TextColumn +  " from " + TableName + " order by " + Orders;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            ddl.Items.Clear();
            ddl.DataTextField = TextColumn;
            ddl.DataValueField = ValueColumn;
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, "");
        }
        /// <summary>
        /// ��ĳ�������������������ؼ�
        /// </summary>
        /// <param name="ddl">Ҫ���Ŀؼ�</param>
        /// <param name="TableName">����</param>
        /// <param name="TextColumn">��ʾ��</param>
        /// <param name="ValueColumn">������</param>
        /// <param name="Orders">������������ʽ������ ����ʽ  ��:DATEM desc</param>
        /// <param name="Wheres">��ʽ��"����1 = ֵ1 and ����2 = ֵ2 ..."</param>
        public static void FillByTable(ref DropDownList ddl, string TableName, string TextColumn, string ValueColumn, string Orders,string Wheres)
        {
            string sql;
            sql = "select " + ValueColumn + "," + TextColumn +  " from " + TableName + " where " + Wheres + " order by " + Orders;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            ddl.Items.Clear();
            ddl.DataTextField = TextColumn;
            ddl.DataValueField = ValueColumn;
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, "");
        }

        /// <summary>
        /// ��ĳ�������������������ؼ�,oracle�в�����TextColumn��ValueColumn��ͬһ�У�������֮
        /// </summary>
        /// <param name="ddl">Ҫ���Ŀؼ�</param>
        /// <param name="TableName">����</param>
        /// <param name="TextColumn">��ʾ��</param>
        public static void FillByTable(ref DropDownList ddl, string TableName, string TextColumn)
        {
            string sql;
            sql = "select " + TextColumn + " from " + TableName + " order by " + TextColumn;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            ddl.Items.Clear();
            ddl.DataTextField = TextColumn;
            ddl.DataValueField = TextColumn;
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, "");
        }

        public static void FillHtmlCombxByTable(ref HtmlComboBox hcb, string TableName, string TextColumn, string ValueColumn)
        {
            string sql;
            sql = "select " + ValueColumn + "," + TextColumn +  " from " + TableName + " order by " + ValueColumn;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            hcb.Items.Clear();
            hcb.DataTextField = TextColumn;
            hcb.DataValueField = ValueColumn;
            hcb.DataSource = dt;
            hcb.DataBind();
        }

        public static void FillHtmlCombxByTable(ref HtmlComboBox hcb, string TableName, string TextColumn, string ValueColumn,string Orders)
        {
            string sql;
            sql = "select " + ValueColumn + "," + TextColumn + " from " + TableName + " order by " + Orders;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            hcb.Items.Clear();
            hcb.DataTextField = TextColumn;
            hcb.DataValueField = ValueColumn;
            hcb.DataSource = dt;
            hcb.DataBind();
        }

        public static void FillHtmlCombxByTable(ref HtmlComboBox hcb, string TableName, string TextColumn, string ValueColumn, string Orders, string Wheres)
        {
            string sql;
            sql = "select " + ValueColumn + "," + TextColumn + " from " + TableName + " where " + Wheres + " order by " + Orders;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            hcb.Items.Clear();
            hcb.DataTextField = TextColumn;
            hcb.DataValueField = ValueColumn;
            hcb.DataSource = dt;
            hcb.DataBind();
        }

        //����GLT���

        /// <summary>
        /// ͨ��sql���ddl������
        /// </summary>
        /// <param name="hcb">�ؼ���</param>
        /// <param name="sql">sql���</param>
        /// <returns>������ʾ</returns>
        public static string FillDDLBySql(ref DropDownList ddl, string sql)
        {
            string str = "";
            try
            {
                DataTable dt = DBOpt.dbHelper.GetDataTable(sql.Replace('^', '\''));
                ddl.Items.Clear();
                ddl.DataTextField = dt.Columns[0].ColumnName;
                ddl.DataValueField = dt.Columns[1].ColumnName;
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, "");
            }
            catch
            {
                str = "Sql�ű������⣬���޸ģ�";
            }
            return str;
        }

        /// <summary>
        /// ͨ��sql���hcb������
        /// </summary>
        /// <param name="hcb">�ؼ���</param>
        /// <param name="sql">sql���</param>
        /// <returns>������ʾ</returns>
        public static string FillHCBBySql(ref HtmlComboBox hcb, string sql)
        {
            string str = "";
            try
            {
                DataTable dt = DBOpt.dbHelper.GetDataTable(sql.Replace('^', '\''));
                hcb.Items.Clear();
                hcb.DataTextField = dt.Columns[0].ColumnName;
                hcb.DataValueField = dt.Columns[1].ColumnName;
                hcb.DataSource = dt;
                hcb.DataBind();
                hcb.Items.Insert(0, "");
            }
            catch
            {
                str = "Sql�ű������⣬���޸ģ�";
            }
            return str;
        }

 

    }



}

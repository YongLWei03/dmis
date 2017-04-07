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
    /// 负责向WEB页面的DropDownList控件填充值
    /// </summary>
    /// 
    public class FillDropDownList
    {
        /// <summary>
        /// 填充厂站
        /// </summary>
        /// <param name="ddl"></param>
        public static void FillStation(ref DropDownList ddl)
        {
            string sql;
            sql = "select TID,名称 from 厂站参数表 order by 序号";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            ddl.Items.Clear();
            ddl.DataTextField = "名称";
            ddl.DataValueField = "TID";
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, "");
        }

        /// <summary>
        /// 根据厂站代码填充设备
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="stationCode"></param>
        public static void FillDeviceByStationName(ref DropDownList ddl, string stationName)
        {
            ddl.Items.Clear();
        }


        /// <summary>
        /// 根据岗位ＩＤ填充人员列表
        /// </summary>
        /// <param name="ddl">下拉列表控件</param>
        /// <param name="RoleID">岗位ＩＤ</param>
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
        /// 根据部门ＩＤ填充人员列表
        /// </summary>
        /// <param name="ddl">要填充的控件</param>
        /// <param name="DepartID">部门ＩＤ</param>
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
        /// 把某表的所有数据填充下拉控件,排序按照ValueColumn列排
        /// </summary>
        /// <param name="ddl">要填充的控件</param>
        /// <param name="TableName">表名</param>
        /// <param name="TextColumn">显示列</param>
        /// <param name="ValueColumn">数据列</param>
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
        /// 把某表的所有数据填充下拉控件
        /// </summary>
        /// <param name="ddl">要填充的控件</param>
        /// <param name="TableName">表名</param>
        /// <param name="TextColumn">显示列</param>
        /// <param name="ValueColumn">数据列</param>
        /// <param name="Orders">排序条件，格式：列名 排序方式  如:DATEM desc</param>
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
        /// 把某表的所有数据填充下拉控件
        /// </summary>
        /// <param name="ddl">要填充的控件</param>
        /// <param name="TableName">表名</param>
        /// <param name="TextColumn">显示列</param>
        /// <param name="ValueColumn">数据列</param>
        /// <param name="Orders">排序条件，格式：列名 排序方式  如:DATEM desc</param>
        /// <param name="Wheres">格式如"条件1 = 值1 and 条件2 = 值2 ..."</param>
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
        /// 把某表的所有数据填充下拉控件,oracle中不允许TextColumn和ValueColumn是同一列，故重载之
        /// </summary>
        /// <param name="ddl">要填充的控件</param>
        /// <param name="TableName">表名</param>
        /// <param name="TextColumn">显示列</param>
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

        //以下GLT添加

        /// <summary>
        /// 通过sql添加ddl的数据
        /// </summary>
        /// <param name="hcb">控件名</param>
        /// <param name="sql">sql语句</param>
        /// <returns>出错提示</returns>
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
                str = "Sql脚本有问题，请修改！";
            }
            return str;
        }

        /// <summary>
        /// 通过sql添加hcb的数据
        /// </summary>
        /// <param name="hcb">控件名</param>
        /// <param name="sql">sql语句</param>
        /// <returns>出错提示</returns>
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
                str = "Sql脚本有问题，请修改！";
            }
            return str;
        }

 

    }



}

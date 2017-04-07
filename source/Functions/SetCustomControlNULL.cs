using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Web.UI.HtmlControls;
using PlatForm.DBUtility;
using PlatForm.CustomControlLib;

namespace PlatForm.Functions
{
    /// <summary>
    /// 添加数据之前，把控件的值置为空
    /// </summary>
    public class SetCustomControlNULL
    {
        /// <summary>
        /// 根据表名，把此页面的控件置为空
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="TableName">表名</param>
        public static void SetWebCustomControlNULL(Page page, string TableName)
        {
            string sql;
            int tableID;
            TextBox txt;
            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            HtmlComboBox hcb;
            WebDate wdl;

            tableID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + TableName + "'"));
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] is System.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() == "") continue;   //平台中有设置此列对应的控件名，不处理
                if (dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString() == "TextBox")
                {
                    txt = (TextBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                    if (txt == null) continue;   //配置的控件名与实际不一致
                    txt.Text = "";
                }
                else if (dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString() == "DropDownList")
                {
                    ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                    if (ddl == null) continue;
                    ddl.SelectedIndex = -1;
                }
                else if (dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString() == "CheckBox")
                {
                    ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                    if (ckb == null) continue;
                    ckb.Checked = false;
                }
                else if (dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString() == "RadioListButtion")
                {
                    rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                    if (rbl == null) continue;
                    rbl.SelectedIndex = -1;
                }
                else if (dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString() == "HtmlComboBox")
                {
                    hcb = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                    if (hcb == null) continue;
                    hcb.Text="";
                }
                else if (dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString() == "WebDateLib")
                {
                    wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                    if (wdl == null) continue;
                    wdl.setNull();
                }
                else
                {

                }
            }
        }


        /// <summary>
        /// 根据表ID，把此页面的控件置为空
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="TableID">表ID</param>
        public static void SetWebCustomControlNULL(Page page, int TableID)
        {
        }

  
    }
}

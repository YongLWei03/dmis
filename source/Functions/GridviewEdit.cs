using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using PlatForm.DBUtility;
using PlatForm.CustomControlLib;
using System.Web.UI.HtmlControls;

namespace PlatForm.Functions
{
    /// <summary>
    /// 此类用于GridView控件在编辑时记取数据和保存数据的操作
    /// </summary>
    public class GridViewEdit
    {
         /// <summary>
        /// 在GridView控件的RowEditing事件中调用，用于读取数据绑定到可编辑的控件上
         /// </summary>
         /// <param name="TableID">没有用表名，可能此GridView控件还有细节页面，表名在保存此细节页面中已经用到，为了区别故用表ID</param>
         /// <param name="EditIndex">所编辑的行ID</param>
         /// <param name="TIDValue">主键TID的值，表一定要有TID的主键列</param>
        public static void GridViewEditing(ref GridView grv,int TableID,int EditIndex,int TIDValue)
        {
            string sql;
            string tableName;
            TextBox txt;
            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //用于签名列,输入相应人的口令才能把人名放到控件中
            object obj;　　　//自定制控件

            tableName = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + TableID.ToString()).ToString();
            
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);

            sql = "select * from " + tableName + " where TID=" + grv.DataKeys[EditIndex].Value;
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] is System.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() == "") continue;   //平台中有设置此列对应的控件名，不处理

                    //这种方式的后果是：如果某个控件在平台设置错误，与实际没对应，则此列不会得到修改
                    //如果一个表有多个GridView修改，则可以减少平台中同样一个表的个数。
                    obj = grv.Rows[EditIndex].FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                    if (obj == null) continue;

                    switch (dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString())
                    {
                        case "TextBox":
                            txt = (TextBox)obj;
                            if (txt != null)   //配置的控件名与实际不一致
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    txt.Text = "";
                                else
                                    txt.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "WebDateLib":
                            wdl = (WebDate)obj;
                            if (wdl != null)  //配置的控件名与实际不一致
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    wdl.setNull();
                                else
                                    wdl.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "WebComboBox":
                            wbx = (WebComboBox)obj;
                            if (wbx != null)   //配置的控件名与实际不一致
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    wbx.Text = "";
                                else
                                    wbx.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "HtmlComboBox":
                            hbx = (HtmlComboBox)obj;
                            if (hbx != null)   //配置的控件名与实际不一致
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    hbx.SelectedText = "";
                                else
                                    hbx.SelectedText = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "WebDropDown":
                            wdd = (WebDropDownList)obj;
                            if (wdd != null)   //配置的控件名与实际不一致
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    wdd.Text = "";
                                else
                                    wdd.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "DropDownList":
                            ddl = (DropDownList)obj;
                            if (ddl != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    ddl.SelectedIndex = -1;
                                else
                                {
                                    if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Value")
                                        ddl.SelectedIndex = FieldToValue.FieldToDropDownListByValue(dr[dt.Rows[i]["NAME"].ToString()], ddl);
                                    else
                                        ddl.SelectedIndex = FieldToValue.FieldToDropDownListByText(dr[dt.Rows[i]["NAME"].ToString()], ddl);
                                }
                            }
                            break;
                        case "CheckBox":
                            ckb = (CheckBox)obj;
                            if (ckb != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    ckb.Checked = false;
                                else
                                    ckb.Checked = FieldToValue.FieldToCheckBox(dr[dt.Rows[i]["NAME"].ToString()]);
                            }
                            break;
                        case "RadioListButtion":
                            rbl = (RadioButtonList)obj;
                            if (rbl != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    rbl.SelectedIndex = -1;
                                else
                                {
                                    if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Value")
                                        rbl.SelectedIndex = FieldToValue.FieldToRadioListButtonByValue(dr[dt.Rows[i]["NAME"].ToString()], rbl);
                                    else
                                        rbl.SelectedIndex = FieldToValue.FieldToRadioListButtonByText(dr[dt.Rows[i]["NAME"].ToString()], rbl);
                                }
                            }
                            break;
                        case "HtmlInputText":
                            hit = (HtmlInputText)obj;
                            if (hit != null)   //配置的控件名与实际不一致
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    hit.Value="";
                                else
                                    hit.Value = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            dr.Close();
        }

        /// <summary>
        /// 在GridView控件的RowUpdating事件中调用，用于获取更新的SQL语句
        /// </summary>
        /// <param name="grv"></param>
        /// <param name="TableID"></param>
        /// <param name="EditIndex"></param>
        /// <param name="TIDValue"></param>
        /// <returns></returns>
        public static string GetGridViewRowUpdating(ref GridView grv, int TableID, int EditIndex, int TIDValue)
        {
            string tableName, sql;
            string contolType;
            tableName = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + TableID.ToString()).ToString();

            //共有多少列数据要保存,CUSTOM_CONTROL_NAME列一定是配置了控件名的，如果不要保存的列则定要删除控件名
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " and CUSTOM_CONTROL_NAME is not NULL order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];

            TextBox txt;
            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //没有定义控件名，则不处理保存代码
                if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] == Convert.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString().Trim() == "") continue;
              
                //这种方式的后果是：如果某个控件在平台设置错误，与实际没对应，则此列不会得到修改
                //如果一个表有多个GridView修改，则可以减少平台中同样一个表的个数。
                object obj;
                obj = grv.Rows[EditIndex].FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                if (obj == null) continue;


                FieldPara fp = new FieldPara();
                fp.fieldName = dt.Rows[i]["NAME"].ToString();
                if (dt.Rows[i]["TYPE"].ToString() == "String")
                    fp.fieldType = FieldType.String;
                else if (dt.Rows[i]["TYPE"].ToString() == "Datetime")
                    fp.fieldType = FieldType.Datetime;
                else
                    fp.fieldType = FieldType.Int;
                contolType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                switch (contolType)
                {
                    case "TextBox":
                        txt = (TextBox)obj;
                        if (txt == null) return i.ToString();
                        fp.fieldValue = ValueToField.StringToField(txt.Text);
                        break;
                    case "DropDownList":
                        ddl = (DropDownList)obj;
                        if (ddl == null) return i.ToString();
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.DropDownListToFieldByText(ddl);
                        else
                            fp.fieldValue = ValueToField.DropDownListToFieldByValue(ddl);
                        break;
                    case "CheckBox":
                        ckb = (CheckBox)obj;
                        if (ckb == null) return i.ToString();
                        fp.fieldValue = ValueToField.CheckBoxToField(ckb.Checked);
                        break;
                    case "RadioButtonList":
                        rbl = (RadioButtonList)obj;
                        if (rbl == null) return i.ToString();
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByText(rbl);
                        else
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByValue(rbl);
                        break;
                    case "WebDateLib":
                        wdl = (WebDate)obj;
                        if (wdl == null) return i.ToString();
                        fp.fieldValue = ValueToField.StringToField(wdl.Text);
                        break;
                    case "WebComboBox":
                        wbx = (WebComboBox)obj;
                        if (wbx == null) return i.ToString();
                        fp.fieldValue = ValueToField.StringToField(wbx.Text);
                        break;
                    case "HtmlComboBox":
                        hbx = (HtmlComboBox)obj;
                        if (hbx == null) return i.ToString();
                        fp.fieldValue = ValueToField.StringToField(hbx.SelectedText);
                        break;
                    case "WebDropDown":
                        wdd = (WebDropDownList)obj;
                        if (wdd == null) return i.ToString();
                        fp.fieldValue = ValueToField.StringToField(wdd.Text);
                        break;
                    case "HtmlInputText":
                        hit = (HtmlInputText)obj;
                        if (hit == null) return i.ToString();
                        fp.fieldValue = ValueToField.StringToField(hit.Value);
                        break;
                    default:
                        break;
                }
                fields[i] = fp;
            }

            //主键必须是TID
            WherePara[] wheres = new WherePara[1] { new WherePara("TID", FieldType.Int, grv.DataKeys[EditIndex].Value.ToString(), "=", "and") };
            sql = DBOpt.dbHelper.GetUpdateSql(tableName, fields, wheres);
            return sql;
        }

    }
}

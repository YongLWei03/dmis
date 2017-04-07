using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using PlatForm.DBUtility;

namespace PlatForm.Functions
{
    public class ControlWindowValidator
    {
        /// <summary>
        /// 传递的是表ＩＤ的情况
        /// </summary>
        /// <param name="form"></param>
        /// <param name="tableID"></param>
        /// <returns></returns>
        //public static string Validate(Form form, int tableID)
        //{
        //    string sql = "select NAME,DESCR,TYPE,ISNULL,ISPRIMARY from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
        //    DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
        //    return GetMessage(form, ref dt);
        //}
        ///// <summary>
        ///// 传递的是表名的情况
        ///// </summary>
        ///// <param name="form"></param>
        ///// <param name="tableName"></param>
        ///// <returns></returns>
        //public static string Validate(Form form, string tableName)
        //{
        //    string sql = "select a.NAME,a.DESCR,a.TYPE,a.ISNULL,a.ISPRIMARY from DMIS_SYS_COLUMNS a,DMIS_SYS_TABLES b where a.TABLE_ID=b.ID and b.NAME='" + tableName + "'  order by a.ORDER_ID";
        //    DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
        //    return GetMessage(form, ref dt);
        //}

        //private static string GetMessage(Form form, ref DataTable dt)
        //{
        //    StringBuilder message = new StringBuilder();
        //    System.Windows.Forms.Control control;


        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt16(dt.Rows[i]["ISPRIMARY"]) == 1) continue;   //主键不用处理
        //        //if (dt.Rows[i]["NAME"].ToString().ToUpper() == "TID" || dt.Rows[i]["NAME"].ToString().ToUpper() == "F_NO") continue;   //主键不处理
        //        //form.Controls.Find("txt" + dt.Rows[i]["NAME"].ToString(),true).
        //        control = form.Controls[form.Controls.IndexOfKey("txt" + dt.Rows[i]["NAME"].ToString())];
        //        if (dt.Rows[i]["TYPE"].ToString() == "字符")
        //        {
        //            if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 1)   //允许为空，不处理
        //            {
        //                continue;
        //            }
        //            else
        //            {

        //                if (form.Controls.Find("txt" + dt.Rows[i]["NAME"].ToString(),true).Length=1)  //TextBox控件
        //                {
                            
        //                    control = form.FindControl("txt" + dt.Rows[i]["NAME"].ToString());
        //                    TextBox txt = (TextBox)control;
        //                    if (txt.Text.Trim() == "")
        //                    {
        //                        message.Append(dt.Rows[i]["DESCR"].ToString() + "：不允许为空；");
        //                    }
        //                }
        //                else if (form.FindControl("cbb" + dt.Rows[i]["NAME"].ToString()) != null) //ComboBox控件
        //                {
        //                    control = form.FindControl("cbb" + dt.Rows[i]["NAME"].ToString());
        //                    ComboBox cbb = (ComboBox)control;
        //                    if (cbb.SelectedIndex < 0)
        //                    {
        //                        message.Append(dt.Rows[i]["DESCR"].ToString() + "：不允许为空；");
        //                    }
        //                }
        //                else
        //                {
        //                }
        //            }
        //        }
        //        else if (dt.Rows[i]["TYPE"].ToString() == "时间")
        //        {
        //            if (form.FindControl("txt" + dt.Rows[i]["NAME"].ToString()) != null)  //TextBox控件
        //            {
        //                control = form.FindControl("txt" + dt.Rows[i]["NAME"].ToString());
        //                TextBox txt = (TextBox)control;

        //                if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 1)
        //                {
        //                    if (txt.Text.Trim() == "")
        //                        continue;
        //                    else
        //                    {
        //                        DateTime dtTemp;
        //                        if (!DateTime.TryParse(txt.Text.Trim(), out dtTemp))
        //                            message.Append(dt.Rows[i]["DESCR"].ToString() + "：时间格式不对；");
        //                    }
        //                }
        //                else
        //                {
        //                    if (txt.Text.Trim() == "")
        //                        message.Append(dt.Rows[i]["DESCR"].ToString() + "：不允许为空；");
        //                    else
        //                    {
        //                        DateTime dtTemp;
        //                        if (!DateTime.TryParse(txt.Text.Trim(), out dtTemp))
        //                            message.Append(dt.Rows[i]["DESCR"].ToString() + "：时间格式不对；");
        //                    }
        //                }

        //            }
        //            else if (form.FindControl("cbb" + dt.Rows[i]["NAME"].ToString()) != null) //ComboBox控件   程序中要注意只保存为Text，而不是Value
        //            {
        //                control = form.FindControl("cbb" + dt.Rows[i]["NAME"].ToString());
        //                ComboBox cbb = (ComboBox)control;
        //                if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 1)
        //                {
        //                    if (cbb.SelectedIndex < 0)
        //                        continue;
        //                    else
        //                    {
        //                        DateTime dtTemp;
        //                        if (!DateTime.TryParse(ddl.SelectedItem.Text, out dtTemp))
        //                            message.Append(dt.Rows[i]["DESCR"].ToString() + "：时间格式不对；");
        //                    }
        //                }
        //                else
        //                {
        //                    if (cbb.SelectedIndex < 0)
        //                        message.Append(dt.Rows[i]["DESCR"].ToString() + "：不允许为空；");
        //                    else
        //                    {
        //                        DateTime dtTemp;
        //                        if (!DateTime.TryParse(cbb.SelectedItem.Text, out dtTemp))
        //                            message.Append(dt.Rows[i]["DESCR"].ToString() + "：时间格式不对；");
        //                    }
        //                }
        //            }
        //            else  //日期其它控件类型不处理
        //            {
        //            }
        //        }
        //        else   //数值类型
        //        {
        //            if (form.FindControl("txt" + dt.Rows[i]["NAME"].ToString()) != null)  //TextBox控件
        //            {
        //                control = form.FindControl("txt" + dt.Rows[i]["NAME"].ToString());
        //                TextBox txt = (TextBox)control;
        //                if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 1)
        //                {
        //                    if (txt.Text.Trim() == "")
        //                        continue;
        //                    else
        //                    {
        //                        decimal dc;
        //                        if (!Decimal.TryParse(txt.Text.Trim(), out dc))
        //                            message.Append(dt.Rows[i]["DESCR"].ToString() + "：不是数值类型；");
        //                    }
        //                }
        //                else
        //                {
        //                    if (txt.Text.Trim() == "")
        //                        message.Append(dt.Rows[i]["DESCR"].ToString() + "：不允许为空；");
        //                    else
        //                    {
        //                        decimal dc;
        //                        if (!Decimal.TryParse(txt.Text.Trim(), out dc))
        //                            message.Append(dt.Rows[i]["DESCR"].ToString() + "：不是数值类型；");
        //                    }
        //                }
        //            }
        //            else if (form.FindControl("cbb" + dt.Rows[i]["NAME"].ToString()) != null) //DropDownList控件   程序中要注意只保存为Value，而不是Text
        //            {
        //                control = form.FindControl("cbb" + dt.Rows[i]["NAME"].ToString());
        //                ComboBox cbb = (ComboBox)control;
        //                if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 1)
        //                {
        //                    if (cbb.SelectedIndex < 0)
        //                        continue;
        //                    else
        //                    {
        //                        decimal dc;
        //                        if (!Decimal.TryParse(cbb.SelectedItem.Value, out dc))
        //                            message.Append(dt.Rows[i]["DESCR"].ToString() + "：不是数值类型；");
        //                    }
        //                }
        //                else
        //                {
        //                    if (cbb.SelectedIndex < 0)
        //                        message.Append(dt.Rows[i]["DESCR"].ToString() + "：不允许为空；");
        //                    else
        //                    {
        //                        decimal dc;
        //                        if (!Decimal.TryParse(cbb.SelectedItem.Value, out dc))
        //                            message.Append(dt.Rows[i]["DESCR"].ToString() + "：不是数值类型；");
        //                    }
        //                }
        //            }
        //            else if (form.FindControl("chk" + dt.Rows[i]["NAME"].ToString()) != null) //CheckBox,不用处理
        //            {

        //            }
        //            else    //其它控件
        //            {
        //            }
        //        }
        //    }

        //    if (message.Length == 0)
        //        return "";
        //    else
        //        return message.ToString();
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Data;
using PlatForm.DBUtility;
using System.Web.UI.WebControls;
using System.Globalization;


/// <summary>
/// ������֮ǰ���жϸ����ؼ���ֵ�Ƿ���Ч���Ƿ�Ϊ��
/// �֣ףţ´���ͣףɣΣģϣף�ϣңʹ���
/// </summary>

namespace PlatForm.Functions
{
    /// <summary>
    /// Web���򱣴�֮ǰ����֤
    /// </summary>
    public class ControlWebValidator
    {
        /// <summary>
        /// ���ݵ��Ǳ�ɣĵ����
        /// </summary>
        /// <param name="page"></param>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public static string Validate(Page page, int tableID)
        {
            string sql = "select NAME,DESCR,TYPE,ISNULL,ISPRIMARY,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            return GetMessage(page, ref dt);
        }
        /// <summary>
        /// ���ݵ��Ǳ��������
        /// </summary>
        /// <param name="page"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string Validate(Page page, string tableName)
        {
            string sql = "select a.NAME,a.DESCR,a.TYPE,a.ISNULL,a.ISPRIMARY,a.CUSTOM_CONTROL_NAME,a.CUSTOM_CONTROL_TYPE from DMIS_SYS_COLUMNS a,DMIS_SYS_TABLES b where a.TABLE_ID=b.ID and b.NAME='" + tableName + "'  order by a.ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            return GetMessage(page, ref dt);
        }

        private static string GetMessage(Page page, ref DataTable dt)
        {
            StringBuilder message = new StringBuilder();
            System.Web.UI.Control control;
            string columnType, controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ISPRIMARY"].ToString().ToUpper() == "1") continue;   //����������
                columnType = dt.Rows[i]["TYPE"].ToString();
                switch (columnType)
                {
                    case "String":
                        if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 1) continue;  //����Ϊ�գ�������
                        controlType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                        switch (controlType)
                        {
                            case "TextBox":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();   //������
                                System.Web.UI.WebControls.TextBox txt = (System.Web.UI.WebControls.TextBox)control;
                                if (txt.Text.Trim() == "")
                                    message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());   //������Ϊ��
                                break;
                            case "DropDownList":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                                System.Web.UI.WebControls.DropDownList ddl = (System.Web.UI.WebControls.DropDownList)control;
                                if (ddl.SelectedIndex < 0)
                                    message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                                break;
                            case "RadioButtonList":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                                System.Web.UI.WebControls.RadioButtonList rbl = (System.Web.UI.WebControls.RadioButtonList)control;
                                if (rbl.SelectedIndex < 0)
                                    message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                                break;
                            case "HtmlComboBox":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                                PlatForm.CustomControlLib.HtmlComboBox hcb = (PlatForm.CustomControlLib.HtmlComboBox)control;
                                if (hcb.Text.Trim() == "")
                                    message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                                break;
                            //case "ValidatePassword":
                            //    control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            //    if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                            //    PlatForm.CustomControlLib.ValidatePasswordByRole vpr = (PlatForm.CustomControlLib.ValidatePasswordByRole)control;
                            //    if (vpr.Text.Trim() == "")
                            //        message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                            //    break;
                            case "HtmlInputText":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                                System.Web.UI.HtmlControls.HtmlInputText hControl = (System.Web.UI.HtmlControls.HtmlInputText)control;
                                if (hControl.Value.Trim() == "")
                                    message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Datetime":
                        controlType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                        switch (controlType)
                        {
                            case "TextBox":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                                System.Web.UI.WebControls.TextBox txt = (System.Web.UI.WebControls.TextBox)control;

                                if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 1)
                                {
                                    if (txt.Text.Trim() == "") continue;
                                    DateTime dtTemp;
                                    if (!DateTime.TryParse(txt.Text.Trim(), out dtTemp))
                                        message.Append(dt.Rows[i]["DESCR"].ToString() +HttpContext.GetGlobalResourceObject("WebGlobalResource", "TimeValueError").ToString());   //ʱ���ʽ���ԣ�
                                }
                                else
                                {
                                    if (txt.Text.Trim() == "")
                                        message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                                    else
                                    {
                                        DateTime dtTemp;
                                        if (!DateTime.TryParse(txt.Text.Trim(), out dtTemp))
                                            message.Append(dt.Rows[i]["DESCR"].ToString() +HttpContext.GetGlobalResourceObject("WebGlobalResource", "TimeValueError").ToString());
                                    }
                                }
                                break;
                            case "DropDownList":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                                System.Web.UI.WebControls.DropDownList ddl = (System.Web.UI.WebControls.DropDownList)control;
                                if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 1)
                                {
                                    if (ddl.SelectedIndex < 0) continue;
                                    DateTime dtTemp;
                                    if (!DateTime.TryParse(ddl.SelectedItem.Text, out dtTemp))
                                        message.Append(dt.Rows[i]["DESCR"].ToString() +HttpContext.GetGlobalResourceObject("WebGlobalResource", "TimeValueError").ToString());

                                }
                                else
                                {
                                    if (ddl.SelectedIndex < 0)
                                        message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                                    else
                                    {
                                        DateTime dtTemp;
                                        if (!DateTime.TryParse(ddl.SelectedItem.Text, out dtTemp))
                                            message.Append(dt.Rows[i]["DESCR"].ToString() +HttpContext.GetGlobalResourceObject("WebGlobalResource", "TimeValueError").ToString());
                                    }
                                }
                                break;
                            case "WebDateLib":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                                PlatForm.CustomControlLib.WebDate wbl = (PlatForm.CustomControlLib.WebDate)control;
                                if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 0)  //ʱ���ʽ�϶���
                                {
                                    if (wbl.Text == "") message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                                }
                                //2008��12��30�� 45ʱ24��
                                DateTime temp = wbl.getTime();
                                if (temp.ToString("yyyyMMdd") == "00010101")                               
                                    message.Append(dt.Rows[i]["DESCR"].ToString() +HttpContext.GetGlobalResourceObject("WebGlobalResource", "TimeValueError").ToString());
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Numeric":
                        controlType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                        switch (controlType)
                        {
                            case "TextBox":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                                System.Web.UI.WebControls.TextBox txt = (System.Web.UI.WebControls.TextBox)control;
                                if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 1)
                                {
                                    if (txt.Text.Trim() == "") continue;

                                    decimal dc;
                                    if (!Decimal.TryParse(txt.Text.Trim(), out dc))
                                        message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NumericalValeError").ToString());   //������ֵ����
                                }
                                else
                                {
                                    if (txt.Text.Trim() == "")
                                        message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                                    else
                                    {
                                        decimal dc;
                                        if (!Decimal.TryParse(txt.Text.Trim(), out dc))
                                            message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NumericalValeError").ToString());
                                    }
                                }
                                break;
                            case "DropDownList":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                                System.Web.UI.WebControls.DropDownList ddl = (System.Web.UI.WebControls.DropDownList)control;
                                if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) == 1)
                                {
                                    if (ddl.SelectedIndex < 0) continue;
                                    decimal dc;
                                    if (!Decimal.TryParse(ddl.SelectedItem.Value, out dc))
                                        message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NumericalValeError").ToString());
                                }
                                else
                                {
                                    if (ddl.SelectedIndex < 0)
                                        message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                                    else
                                    {
                                        decimal dc;
                                        if (!Decimal.TryParse(ddl.SelectedItem.Value, out dc))
                                            message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NumericalValeError").ToString());
                                    }
                                }
                                break;
                            case "CheckBox":
                                break;
                            case "RadioButtonList":
                                control = page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                                if (control == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                                System.Web.UI.WebControls.RadioButtonList rbl = (System.Web.UI.WebControls.RadioButtonList)control;
                                if (Convert.ToInt16(dt.Rows[i]["ISNULL"]) != 1)
                                {
                                    if (rbl.SelectedIndex < 0)
                                        message.Append(dt.Rows[i]["DESCR"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "NoEmpty").ToString());
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                } //switch
            } //for
            return message.ToString();
        }




    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;

namespace PlatForm.Functions
{
    public class ValueToField
    {
        /// <summary>
        /// 写入数据库时，若为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string StringToField(Object obj,string defaultValue)
        {
            if (obj == null) return defaultValue;
            return obj.ToString();
        }

        /// <summary>
        /// 写入数据库时，若为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string StringToField(Object obj)
        {
            if (obj == null) return "NULL";
            return obj.ToString();
        }


        /// <summary>
        /// 把时间型字符串格式的字符转换为Oracle格式所要求的格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DateTimeToFieldByOracle(string value)
        {
            if (value == null || value.Trim() == "") return "NULL";
            DateTime dt;
            if (!DateTime.TryParse(value, out dt)) 
                return "NULL";
            else
                return "TO_DATE('"+dt.ToString("dd-MM-yyyy HH:mm:ss")+"','DD-MM-YYYY HH24:MI:SS')";
        }

        /// <summary>
        /// 把字符串中的半角的单引号和双引号转换成全角的单引号和双引号
        /// 主要是因为珠海现场在使用OracleParamter来更新数据库时没有调试成功。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string StringToFieldByConvert(Object obj)
        {
            if (obj == null) return "NULL";
            string temp = obj.ToString();
            temp = temp.Replace("'", "‘");
            temp = temp.Replace('"', '”');
            return "'"+temp+"'";
        }

        /// <summary>
        /// 把CheckBox的值转换为数据库要存放的值,为什么非是1和0
        /// 
        /// </summary>
        /// <param name="bValue"></param>
        /// <returns></returns>
        public static string CheckBoxToField(bool bValue)
        {
            if (bValue)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public static string CheckBoxListToField(System.Web.UI.WebControls.CheckBoxList cbl)
        {
            if (cbl.Items.Count == 0) return "";
            string strText ="";
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                if (cbl.Items[i].Selected == true)
                    strText = strText + cbl.Items[i].Text + ",";
            }
            return strText.Trim(',');
        }

        /// <summary>
        /// 把控件DropDownList的Text转换为数据库要存放的值
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="TextOrValue"></param>
        /// <returns></returns>
        public static string DropDownListToFieldByText(System.Web.UI.WebControls.DropDownList ddl)
        {
            if (ddl.SelectedIndex < 0) return "";
            return ddl.SelectedItem.Text;
        }

        /// <summary>
        /// 把控件DropDownList的Vaule转换为数据库要存放的值
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="TextOrValue"></param>
        /// <returns></returns>
        public static string DropDownListToFieldByValue(System.Web.UI.WebControls.DropDownList ddl)
        {
            if (ddl.SelectedIndex < 0) return "";
            return ddl.SelectedValue ;
        }

        public static string ComboxToFieldByText( System.Windows.Forms.ComboBox cbb)
        {
            if (cbb.SelectedIndex < 0) return "";
            return cbb.SelectedText;
        }

        /// <summary>
        /// 把控件RadioButtonList的Text转换为数据库要存放的值
        /// </summary>
        /// <param name="rbl"></param>
        /// <returns></returns>
        public static string RadioButtonListToFieldByText(RadioButtonList rbl)
        {
            if (rbl.SelectedIndex < 0) return "";
            return rbl.SelectedItem.Text;
        }

        /// <summary>
        /// 把控件RadioButtonList的Value转换为数据库要存放的值
        /// </summary>
        /// <param name="rbl"></param>
        /// <returns></returns>
        public static string RadioButtonListToFieldByValue(RadioButtonList rbl)
        {
            if (rbl.SelectedIndex < 0) return "";
             return rbl.SelectedValue;
        }


    }
}

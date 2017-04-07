using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Globalization;

namespace PlatForm.Functions
{
    public class FieldToValue
    {
        /// <summary>
        /// 把数据库读出来的值转换为TextkBox的值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FieldToTextBox(Object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return ("");
            return (Convert.ToString(obj).Trim());
        }

        
        /// <summary>
        /// 把数据库读出来的值转换为CheckBox的值,此值只能为1(true)或0(false)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool FieldToCheckBox(Object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return false;
            if (Convert.ToInt16(obj) == 1)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 通过属性Value查找WEB控件DropDownList的INDEX
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int FieldToDropDownListByValue(Object obj, DropDownList ddl)
        {
            if (obj == null || Convert.IsDBNull(obj)) return (-1);
            return ddl.Items.IndexOf(ddl.Items.FindByValue(obj.ToString()));
        }

        /// <summary>
        /// 通过属性Text查找WEB控件DropDownList的INDEX
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int FieldToDropDownListByText(Object obj, DropDownList ddl)
        {
            if (obj == null || Convert.IsDBNull(obj)) return (-1);
            return ddl.Items.IndexOf(ddl.Items.FindByText(obj.ToString()));
        }


        /// <summary>
        /// 通过属性Value查找WEB控件RadioButtonList的INDEX
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// 
        public static int FieldToRadioListButtonByValue(Object obj, RadioButtonList rbl)
        {
            if (obj == null || Convert.IsDBNull(obj)) return (-1);
            return rbl.Items.IndexOf(rbl.Items.FindByValue(obj.ToString()));
        }

        /// <summary>
        /// 通过Text属性查找WEB控件RadioButtonList的INDEX
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// 
        public static int FieldToRadioListButtonByText(Object obj, RadioButtonList rbl)
        {
            if (obj == null || Convert.IsDBNull(obj)) return (-1);
            return rbl.Items.IndexOf(rbl.Items.FindByText(obj.ToString()));
        }




        /// <summary>
        /// 读出来的字段数据转换成字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FieldToString(Object obj, string defaultValue)
        {
            if (obj == null || Convert.IsDBNull(obj)) return (defaultValue);
            return (Convert.ToString(obj).Trim());
        }

        public static string FieldToString(Object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return ("");
            return (Convert.ToString(obj).Trim());
        }

        /// <summary>
        /// 转换为数值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>转换不成功，返回-1，这有点不合适，还要找其它方法</returns>
        public static int FieldToInt(object obj)
        {
            if (obj == null) return -1;

            int ret;
            if (int.TryParse(obj.ToString(), out ret))
                return ret;
            else
                return -1;
        }

        /// <summary>
        /// 转换为日期
        /// 2010-3-8 加上本地化的判断
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime FieldToDateTime(object obj)
        {
            DateTime dResult;
            if (HttpContext.Current.Session["UICulture"] != null)
            {
                CultureInfo ci = new CultureInfo(HttpContext.Current.Session["UICulture"].ToString());
                try
                {
                    dResult = DateTime.Parse(obj.ToString(), ci);
                    return dResult;
                }
                catch
                {
                    return Convert.ToDateTime("1900-01-01");
                }
            }
            else
            {
                if (DateTime.TryParse(obj.ToString(), out dResult))
                    return dResult;
                else
                    return dResult;
            }
        }
        



        //以下是唐文的代码，不符合代码规则，故取消

        /// <summary>
        /// 过滤空对象，返回整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>int</returns>
        public static int iifInt(Object obj,int DefaultValue)
        {
            int iResult = DefaultValue;
            try { iResult = Convert.ToInt32(obj); }
            catch (Exception ee) { Console.Write(ee.Message); }
            return (iResult);
        }


        /// <summary>
        /// 过滤空对象，返回字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string iifStr(Object obj,string defaultValue)
        {
            string sResult = defaultValue;
            try { sResult = Convert.ToString(obj).Trim(); }
            catch (Exception ee) { Console.Write(ee.Message); }
            return (sResult);
        }
        

        /// <summary>
        /// 过滤空对象，返回日期
        /// </summary>
        /// <param name="obj1"></param>
        /// <returns>DateTime</returns>
        public static DateTime iifDate(Object obj1,DateTime defaultValue)
        {
            DateTime dResult = defaultValue;
            try { dResult = Convert.ToDateTime(obj1); }
            catch (Exception ee) { Console.Write(ee.Message); }
            return (dResult);
        }




    }
}

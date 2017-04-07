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
        /// �����ݿ��������ֵת��ΪTextkBox��ֵ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FieldToTextBox(Object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return ("");
            return (Convert.ToString(obj).Trim());
        }

        
        /// <summary>
        /// �����ݿ��������ֵת��ΪCheckBox��ֵ,��ֵֻ��Ϊ1(true)��0(false)
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
        /// ͨ������Value����WEB�ؼ�DropDownList��INDEX
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int FieldToDropDownListByValue(Object obj, DropDownList ddl)
        {
            if (obj == null || Convert.IsDBNull(obj)) return (-1);
            return ddl.Items.IndexOf(ddl.Items.FindByValue(obj.ToString()));
        }

        /// <summary>
        /// ͨ������Text����WEB�ؼ�DropDownList��INDEX
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int FieldToDropDownListByText(Object obj, DropDownList ddl)
        {
            if (obj == null || Convert.IsDBNull(obj)) return (-1);
            return ddl.Items.IndexOf(ddl.Items.FindByText(obj.ToString()));
        }


        /// <summary>
        /// ͨ������Value����WEB�ؼ�RadioButtonList��INDEX
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
        /// ͨ��Text���Բ���WEB�ؼ�RadioButtonList��INDEX
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
        /// ���������ֶ�����ת�����ַ���
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
        /// ת��Ϊ��ֵ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>ת�����ɹ�������-1�����е㲻���ʣ���Ҫ����������</returns>
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
        /// ת��Ϊ����
        /// 2010-3-8 ���ϱ��ػ����ж�
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
        



        //���������ĵĴ��룬�����ϴ�����򣬹�ȡ��

        /// <summary>
        /// ���˿ն��󣬷�������
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
        /// ���˿ն��󣬷����ַ���
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
        /// ���˿ն��󣬷�������
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

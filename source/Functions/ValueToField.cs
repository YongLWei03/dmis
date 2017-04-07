using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;

namespace PlatForm.Functions
{
    public class ValueToField
    {
        /// <summary>
        /// д�����ݿ�ʱ����Ϊ��
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string StringToField(Object obj,string defaultValue)
        {
            if (obj == null) return defaultValue;
            return obj.ToString();
        }

        /// <summary>
        /// д�����ݿ�ʱ����Ϊ��
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string StringToField(Object obj)
        {
            if (obj == null) return "NULL";
            return obj.ToString();
        }


        /// <summary>
        /// ��ʱ�����ַ�����ʽ���ַ�ת��ΪOracle��ʽ��Ҫ��ĸ�ʽ
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
        /// ���ַ����еİ�ǵĵ����ź�˫����ת����ȫ�ǵĵ����ź�˫����
        /// ��Ҫ����Ϊ�麣�ֳ���ʹ��OracleParamter���������ݿ�ʱû�е��Գɹ���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string StringToFieldByConvert(Object obj)
        {
            if (obj == null) return "NULL";
            string temp = obj.ToString();
            temp = temp.Replace("'", "��");
            temp = temp.Replace('"', '��');
            return "'"+temp+"'";
        }

        /// <summary>
        /// ��CheckBox��ֵת��Ϊ���ݿ�Ҫ��ŵ�ֵ,Ϊʲô����1��0
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
        /// �ѿؼ�DropDownList��Textת��Ϊ���ݿ�Ҫ��ŵ�ֵ
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
        /// �ѿؼ�DropDownList��Vauleת��Ϊ���ݿ�Ҫ��ŵ�ֵ
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
        /// �ѿؼ�RadioButtonList��Textת��Ϊ���ݿ�Ҫ��ŵ�ֵ
        /// </summary>
        /// <param name="rbl"></param>
        /// <returns></returns>
        public static string RadioButtonListToFieldByText(RadioButtonList rbl)
        {
            if (rbl.SelectedIndex < 0) return "";
            return rbl.SelectedItem.Text;
        }

        /// <summary>
        /// �ѿؼ�RadioButtonList��Valueת��Ϊ���ݿ�Ҫ��ŵ�ֵ
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

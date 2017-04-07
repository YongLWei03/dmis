using System;
using System.Collections.Generic;
using System.Text;
using PlatForm.DBUtility;

namespace PlatForm.Functions
{
    /// <summary>
    /// �й�����ʱ�������������
    /// </summary>
    public class CDateTime
    {
        /// <summary>
        /// ��ȡ���ĵ����ڼ�
        /// </summary>
        /// <param name="dt">����ʱ�����ͱ���</param>
        /// <returns></returns>
        public static string GetWeekName(DateTime dt)
        {
            if (dt == null) return "";
            string weekName="";
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    weekName = "������";
                    break;
                case DayOfWeek.Monday:
                    weekName = "����һ";
                    break;
                case DayOfWeek.Tuesday:
                    weekName = "���ڶ�";
                    break;
                case DayOfWeek.Wednesday:
                    weekName = "������";
                    break;
                case DayOfWeek.Thursday:
                    weekName = "������";
                    break;
                case DayOfWeek.Friday:
                    weekName = "������";
                    break;
                default:
                    weekName = "������";
                    break;
            }
            return weekName;
        }
        /// <summary>
        /// ��ȡ���ĵ����ڼ�
        /// </summary>
        /// <param name="sdt">�ַ����ͱ���,��ʽҪ��: yyyy-mm-dd</param>
        /// <returns></returns>
        public static string GetWeekName(string sdt)
        {
            if (sdt == null ) return "";
            DateTime dt;
            if(!DateTime.TryParse(sdt,out dt))  return "";
            string weekName = "";
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    weekName = "������";
                    break;
                case DayOfWeek.Monday:
                    weekName = "����һ";
                    break;
                case DayOfWeek.Tuesday:
                    weekName = "���ڶ�";
                    break;
                case DayOfWeek.Wednesday:
                    weekName = "������";
                    break;
                case DayOfWeek.Thursday:
                    weekName = "������";
                    break;
                case DayOfWeek.Friday:
                    weekName = "������";
                    break;
                default:
                    weekName = "������";
                    break;
            }
            return weekName;
        }
        /// <summary>
        /// ��ȡ�����ж�����
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int GetMonthDays(int year, int month)
        {
            int days;
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    days = 31;
                    break;
                case 2:
                    if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))/* ���꣨leap year�� */
                        days = 29;
                    else
                        days = 28; //ƽ��
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    days = 30;
                    break;
                default:
                    days = 0;
                    break;
            }
            return days;
        }

        /// <summary>
        /// �ж��Ƿ��Ƿ����ڼ���
        /// </summary>
        /// <param name="dt">ĳһ����</param>
        /// <returns></returns>
        public static bool IsHoliday(DateTime dt)
        {
            object obj = DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_WK_LEGAL_HOLIDAY where to_char(HOLIDAY_DATE,'YYYYMMDD')='" + dt.ToString("yyyyMMdd") + "'");
            
            if (Convert.ToInt16(obj) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// �ж��Ƿ���Ϣ��
        /// </summary>
        /// <param name="dt">ĳһ����</param>
        /// <returns></returns>
        public static bool IsWeekend(DateTime dt)
        {
            object obj = DBOpt.dbHelper.ExecuteScalar("select is_holiday from dmis_sys_wk_restday where to_char(res_date,'YYYYMMDD')='" + dt.ToString("yyyyMMdd") + "'");
            if (obj == null)
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    return true;
                else
                    return false;
            }
            else
            {
                if (obj.ToString() == "0")
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsWorkDay(DateTime dt)
        {
            object obj = DBOpt.dbHelper.ExecuteScalar("select is_holiday from dmis_sys_wk_restday where to_char(res_date,'YYYYMMDD')='" + dt.ToString("yyyyMMdd") + "'");
            if (obj == null)   //�������е���Ϣ��û���ҵ����õ���Ϣ������ĩ��������Ϣ�գ������ǹ�����
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    return false;
                else
                    return true;
            }
            else  //���������Ѿ��ҵ�������������.
            {
                if (obj.ToString() == "1")
                    return false;
                else
                    return true;
            }
        }

    }



}

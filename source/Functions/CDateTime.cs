using System;
using System.Collections.Generic;
using System.Text;
using PlatForm.DBUtility;

namespace PlatForm.Functions
{
    /// <summary>
    /// 有关日期时间的其它操作类
    /// </summary>
    public class CDateTime
    {
        /// <summary>
        /// 获取中文的星期几
        /// </summary>
        /// <param name="dt">日期时间类型变量</param>
        /// <returns></returns>
        public static string GetWeekName(DateTime dt)
        {
            if (dt == null) return "";
            string weekName="";
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    weekName = "星期日";
                    break;
                case DayOfWeek.Monday:
                    weekName = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    weekName = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    weekName = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    weekName = "星期四";
                    break;
                case DayOfWeek.Friday:
                    weekName = "星期五";
                    break;
                default:
                    weekName = "星期六";
                    break;
            }
            return weekName;
        }
        /// <summary>
        /// 获取中文的星期几
        /// </summary>
        /// <param name="sdt">字符串型变量,格式要求: yyyy-mm-dd</param>
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
                    weekName = "星期日";
                    break;
                case DayOfWeek.Monday:
                    weekName = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    weekName = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    weekName = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    weekName = "星期四";
                    break;
                case DayOfWeek.Friday:
                    weekName = "星期五";
                    break;
                default:
                    weekName = "星期六";
                    break;
            }
            return weekName;
        }
        /// <summary>
        /// 获取此月有多少天
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
                    if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))/* 闰年（leap year） */
                        days = 29;
                    else
                        days = 28; //平月
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
        /// 判断是否是法定节假日
        /// </summary>
        /// <param name="dt">某一日期</param>
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
        /// 判断是否休息日
        /// </summary>
        /// <param name="dt">某一日期</param>
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
        /// 是否工作日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsWorkDay(DateTime dt)
        {
            object obj = DBOpt.dbHelper.ExecuteScalar("select is_holiday from dmis_sys_wk_restday where to_char(res_date,'YYYYMMDD')='" + dt.ToString("yyyyMMdd") + "'");
            if (obj == null)   //工作流中的休息日没有找到配置的信息。则周末二天是休息日，其它是工作日
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    return false;
                else
                    return true;
            }
            else  //工作流中已经找到此日期配置了.
            {
                if (obj.ToString() == "1")
                    return false;
                else
                    return true;
            }
        }

    }



}

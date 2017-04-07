using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using PlatForm.DBUtility;

namespace PlatForm.Functions
{
    /// <summary>
    /// Excel文件操作类
    /// </summary>
    public class ExcelOpt
    {
        /// <summary>
        /// 生成EXCEL文件
        /// </summary>
        /// <param name="sql">要检索数据的SQL语句</param>
        /// <param name="tableID">在平台中DMIS_SYS_TABLES中的ID号</param>
        /// <param name="path">生成文件的全名，包括路径</param>
        /// <param name="fileName">生成文件名</param>
        /// <returns></returns>
        public static int GenerateExcel(string sql,string tableID,out string path,out string fileName)
        {
            path = "";
            fileName = "";
            object columnDesc;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            if (dt == null) return -1;  //sql语句有误，返回

            fileName = "Output" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";
            path = HttpContext.Current.Server.MapPath(fileName);
            //存在同名的文件则先删除
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, new System.Text.UnicodeEncoding());

            //先写中文列标题
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                columnDesc = DBOpt.dbHelper.ExecuteScalar("select DESCR from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID+" and NAME='"+dt.Columns[j].ColumnName.ToUpper()+"'");
                if (columnDesc != null)
                {
                    sw.Write(columnDesc.ToString());
                    sw.Write("\t");
                }
                else
                {
                    sw.Write(dt.Columns[j].ColumnName);
                    sw.Write("\t");
                }
            }
            sw.WriteLine("");
            //再写数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sw.Write(dt.Rows[i][j].ToString());
                    sw.Write("\t");
                }
                sw.WriteLine("");
            }
            sw.Flush();
            sw.Close();
            return 1;
       }


       //GLT修改 ： 本来是通过Sql生产DataTable，现在直接传入了DataTable！
        public static int GenerateExcel(DataTable excelDt, string tableID, out string path, out string fileName)
        {
            path = "";
            fileName = "";
            object columnDesc;
            //DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            DataTable dt = excelDt;
            if (dt == null) return -1;  //sql语句有误，返回

            fileName = "Output" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";
            path = HttpContext.Current.Server.MapPath(fileName);
            //存在同名的文件则先删除
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, new System.Text.UnicodeEncoding());

            //先写中文列标题
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                columnDesc = DBOpt.dbHelper.ExecuteScalar("select DESCR from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID + " and NAME='" + dt.Columns[j].ColumnName.ToUpper() + "'");
                if (columnDesc != null)
                {
                    sw.Write(columnDesc.ToString());
                    sw.Write("\t");
                }
                else
                {
                    sw.Write(dt.Columns[j].ColumnName);
                    sw.Write("\t");
                }
            }
            sw.WriteLine("");
            //再写数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sw.Write(dt.Rows[i][j].ToString());
                    sw.Write("\t");
                }
                sw.WriteLine("");
            }
            sw.Flush();
            sw.Close();
            return 1;
        }

        /// <summary>
        /// 把EXCEL文件中的内容生成DataTable
        /// </summary>
        /// <param name="fileName">服务器的全路径的文件名</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fileName)
        {
            //把EXCEL文件中的数据读到DataSet中。
            string strConString = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '" + fileName + "';Extended Properties=\"Excel 8.0;IMEX=1\"";
            OleDbConnection oleDbCon = new OleDbConnection(strConString);
            OleDbDataAdapter oleDbAdapter = new OleDbDataAdapter("select * from [Sheet1$]", oleDbCon);
            DataSet dsMyDataSet = new DataSet();
            oleDbAdapter.Fill(dsMyDataSet);
            return dsMyDataSet.Tables[0];
        }

    }
}

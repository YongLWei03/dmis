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
    /// Excel�ļ�������
    /// </summary>
    public class ExcelOpt
    {
        /// <summary>
        /// ����EXCEL�ļ�
        /// </summary>
        /// <param name="sql">Ҫ�������ݵ�SQL���</param>
        /// <param name="tableID">��ƽ̨��DMIS_SYS_TABLES�е�ID��</param>
        /// <param name="path">�����ļ���ȫ��������·��</param>
        /// <param name="fileName">�����ļ���</param>
        /// <returns></returns>
        public static int GenerateExcel(string sql,string tableID,out string path,out string fileName)
        {
            path = "";
            fileName = "";
            object columnDesc;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            if (dt == null) return -1;  //sql������󣬷���

            fileName = "Output" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";
            path = HttpContext.Current.Server.MapPath(fileName);
            //����ͬ�����ļ�����ɾ��
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, new System.Text.UnicodeEncoding());

            //��д�����б���
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
            //��д����
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


       //GLT�޸� �� ������ͨ��Sql����DataTable������ֱ�Ӵ�����DataTable��
        public static int GenerateExcel(DataTable excelDt, string tableID, out string path, out string fileName)
        {
            path = "";
            fileName = "";
            object columnDesc;
            //DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            DataTable dt = excelDt;
            if (dt == null) return -1;  //sql������󣬷���

            fileName = "Output" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";
            path = HttpContext.Current.Server.MapPath(fileName);
            //����ͬ�����ļ�����ɾ��
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, new System.Text.UnicodeEncoding());

            //��д�����б���
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
            //��д����
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
        /// ��EXCEL�ļ��е���������DataTable
        /// </summary>
        /// <param name="fileName">��������ȫ·�����ļ���</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fileName)
        {
            //��EXCEL�ļ��е����ݶ���DataSet�С�
            string strConString = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '" + fileName + "';Extended Properties=\"Excel 8.0;IMEX=1\"";
            OleDbConnection oleDbCon = new OleDbConnection(strConString);
            OleDbDataAdapter oleDbAdapter = new OleDbDataAdapter("select * from [Sheet1$]", oleDbCon);
            DataSet dsMyDataSet = new DataSet();
            oleDbAdapter.Fill(dsMyDataSet);
            return dsMyDataSet.Tables[0];
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace PlatForm.Functions
{
    /// <summary>
    /// ������EXCEL�ļ��󣬴��ฺ����ļ����ص��ͻ��˵ļ������
    /// </summary>
    public class DownLoadFile
    {
        /// <summary>
        /// ���ļ����ص��ͻ��˵ļ������
        /// </summary>
        /// <param name="filePath">�ļ�·���������ļ���</param>
        /// <param name="fileName">�ļ���</param>
        public static void DownFile(string filePath, string fileName)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("Big5");
            HttpContext.Current.Response.WriteFile(fileInfo.FullName);
            HttpContext.Current.Response.Flush();
            fileInfo.Delete();    //ɾ���������˵��ļ���
            HttpContext.Current.Response.End();
        }
    }
}

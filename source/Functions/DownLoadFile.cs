using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace PlatForm.Functions
{
    /// <summary>
    /// 在生成EXCEL文件后，此类负责把文件下载到客户端的计算机中
    /// </summary>
    public class DownLoadFile
    {
        /// <summary>
        /// 把文件下载到客户端的计算机中
        /// </summary>
        /// <param name="filePath">文件路径，包括文件名</param>
        /// <param name="fileName">文件名</param>
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
            fileInfo.Delete();    //删除服务器端的文件。
            HttpContext.Current.Response.End();
        }
    }
}

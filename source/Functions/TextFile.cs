using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;


namespace PlatForm.Functions
{

    /// <summary>
    /// 负责处理文本文件类
    /// </summary>
    public class TextFile
    {
        /// <summary>
        /// 通过StreamReader类来访问文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>返回结果集的Hashtable</returns>
        public static Hashtable ReadByStreamReader(string fileName)
        {
            if (!File.Exists(fileName)) return null;
            Hashtable ht = new Hashtable();
            using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("GB18030")))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.IndexOf('@') < 0) continue;   //文本中间如果没有字符'@'隔开，则不读
                    ht.Add(line.Substring(0,line.IndexOf('@')),line.Substring(line.IndexOf('@')+1));
                }
            }
            return ht;
        }


    }
}

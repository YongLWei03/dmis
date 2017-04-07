using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;


namespace PlatForm.Functions
{

    /// <summary>
    /// �������ı��ļ���
    /// </summary>
    public class TextFile
    {
        /// <summary>
        /// ͨ��StreamReader���������ļ�
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <returns>���ؽ������Hashtable</returns>
        public static Hashtable ReadByStreamReader(string fileName)
        {
            if (!File.Exists(fileName)) return null;
            Hashtable ht = new Hashtable();
            using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("GB18030")))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.IndexOf('@') < 0) continue;   //�ı��м����û���ַ�'@'�������򲻶�
                    ht.Add(line.Substring(0,line.IndexOf('@')),line.Substring(line.IndexOf('@')+1));
                }
            }
            return ht;
        }


    }
}

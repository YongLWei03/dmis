/// <summary>
/// ��̬ȫ�ֺ�����
/// </summary>
namespace PlatForm.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class static_DF
    {
        public static_DF() { }
        /// <summary>
        /// �ų��ն���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string FldToStr(Object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return ("");
            return (Convert.ToString(obj).Trim());
        }
        /// <summary>
        /// ���ʹ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defvalue"></param>
        /// <returns>T</returns>
        public static T FldToValue<T>(object obj,T defvalue){
            T result;
            if (obj == null|| Convert.IsDBNull(obj)) { result = defvalue; }
            else {
                try { result = (T)obj;}
                catch (Exception ee)//InvalidCastException
                {
                    Console.WriteLine(ee.Message);
                    result = defvalue;
                }
            }
            return (result);
        }

        /// <summary>
        /// д�����ݿ�ʱ����Ϊ�գ��򷵻�NULL,���򣬷��ؼӵ����ŵ�ֵ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string StrToFld(string obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return ("NULL");
            if (obj.ToString() == "") return ("NULL");
            return ("'" + obj.ToString() + "'");
        }
        /// <summary>
        /// ���˿ն��󣬷�������
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>int</returns>
        public static int iifInt(Object obj)
        {
            int iResult = -1;
            try { iResult = Convert.ToInt32(obj); }
            catch (Exception ee) { Console.Write(ee.Message); }
            return (iResult);
        }
        /// <summary>
        /// ���˿ն��󣬷����ַ���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string iifStr(Object obj)
        {
            string sResult = "";
            try { sResult = Convert.ToString(obj).Trim(); }
            catch (Exception ee) { Console.Write(ee.Message); }
            return (sResult);
        }
        /// <summary>
        /// ���˿ն��󣬷�������
        /// </summary>
        /// <param name="obj1"></param>
        /// <returns>DateTime</returns>
        public static DateTime iifDate(Object obj1)
        {
            DateTime dResult = DateTime.Now;
            try { dResult = Convert.ToDateTime(obj1); }
            catch (Exception ee) {Console.Write(ee.Message); }
            return (dResult);
        }
        /// <summary>
        /// �ַ�������鷭ת������Ĳ����ַ�
        /// </summary>
        /// <param name="sSvr"></param>
        /// <param name="pDst"></param>
        /// <param name="iBit"></param>
        /// <param name="cFill"></param>
        /// <param name="bFill"></param>
        /// <returns></returns>
        public static int Reverse(char[] sSvr, ref char[] pDst, int iBit, char cFill)
        {
            int iCount = sSvr.GetLength(0);
            if (iCount % iBit != 0) iCount = (iCount / iBit + 1) * iBit;
            char[] pTmp;
            pTmp = new char[iCount];
            pDst = new char[iCount];
            chrCpy(sSvr, ref pTmp, iCount, cFill, true);
            pDst = new char[sSvr.GetLength(0)];
            for (int i = 0; i < iCount; i += iBit)
                for (int j = 1; j <= iBit; j++)
                    pDst[i + iBit - j] = pTmp[i + j - 1];
            return (sSvr.GetLength(0));
        }
        /// <summary>
        /// �ַ����ƣ������������������ָ�����ַ�
        /// </summary>
        /// <param name="pSvr"></param>
        /// <param name="pDst"></param>
        /// <param name="iCount"></param>
        /// <param name="cFill"></param>
        /// <param name="bFill"></param>
        /// <returns></returns>
        public static int chrCpy(char[] pSvr, ref char[] pDst, int iCount, char cFill, bool bFill)
        {
            int iiCount = iCount;
            if (pSvr.GetLength(0) < iiCount) iiCount = pSvr.GetLength(0);
            pDst = new char[iCount];
            for (int i = 0; i < iiCount; i++)
                pDst[i] = pSvr[i];
            if (iiCount < iCount && bFill)
                for (int i = iiCount; i < iCount; i++)
                    pDst[i] = cFill;
            return (iCount);
        }

    }
    /// <summary>
    /// ��ջ����
    /// </summary>
��    public class Stack
��    {
����    Entry top;
����    public void Push(object data){
������    top=new Entry(top,data);
����    }

����    public object Pop(){
������    if (top==null) throw new InvalidOperationException();
��������    object result=top.data;
��������    top=top.next;
��������    return result;
����    }
         /// <summary>
         /// ��ջ��Ľڵ���
         /// </summary>
����    class Entry
����    {
������    public Entry next;
������    public object data;

������    public Entry(Entry next,object data){
��������    this.next=next;
��������    this.data=data;
������    }
����    }
��    }

}

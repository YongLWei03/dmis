using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using PlatForm.DBUtility;
using System.Globalization;

namespace PlatForm.Functions
{
    /// <summary>
    /// ��������WEB������
    /// </summary>
    public class WebWorkFlow
    {
        /// <summary>
        /// ͨ��ҵ��������֣�����ҵ�񣬷���ҵ��ı��
        /// </summary>
        /// <param name="iPackType">ҵ�����ͱ��</param>
        /// <param name="iPackNo">����ҵ����</param>
        public static int CreatePack(int iPackType, string sPackDesc, string sUsr, ref uint iPackNo)
        {
            iPackNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PACK", "F_NO");
            string sName = "";
            sName = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_PACKTYPE where F_NO=" + iPackType).ToString();
            sPackDesc = sPackDesc.Replace('\'', '��');
            sPackDesc = sPackDesc.Replace('"', '��');
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into DMIS_SYS_PACK(F_NO,F_PACKTYPENO,F_PACKNAME,");
            sql.Append("F_CREATEMAN,F_CREATEDATE,F_ARCHIVEDATE,F_STATUS,F_DESC) VALUES(");
            sql.Append(iPackNo + "," + iPackType + ",'" + sName + "','" + sUsr + "','" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "',");
            sql.Append("null,'1','" + sPackDesc + "')");
            return (DBOpt.dbHelper.ExecuteSql(sql.ToString()));
        }

        /// <summary>
        /// 2008��9��1�����Ʒ�
        /// �麣�û�����Ҫ��Ҫ���ճ�վ����ѯĳ��ʱ��֮���ж���ҵ�����
        /// ��F_MSG�ֶ������泧վ��������д��ѯ��䡣
        /// 
        /// </summary>
        /// <param name="iPackType"></param>
        /// <param name="sPackDesc"></param>
        /// <param name="sUsr"></param>
        /// <param name="iPackNo"></param>
        /// <param name="station">��վ���������ӵ�</param>
        /// <returns></returns>
        public static int CreatePack(int iPackType, string sPackDesc, string sUsr, ref uint iPackNo,string station,string planStartime,string planEndtime)
        {
            iPackNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PACK", "F_NO");
            string sName = "";
            sName = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_PACKTYPE where F_NO=" + iPackType).ToString();
            StringBuilder sql = new StringBuilder();
            sPackDesc = sPackDesc.Replace('\'', '��');
            sPackDesc = sPackDesc.Replace('"', '��');
            sql.Append("insert into DMIS_SYS_PACK(F_NO,F_PACKTYPENO,F_PACKNAME,");
            sql.Append("F_CREATEMAN,F_CREATEDATE,F_ARCHIVEDATE,F_STATUS,F_DESC,F_MSG,PLAN_STARTTIME,PLAN_ENDTIME) VALUES(");
            sql.Append(iPackNo + "," + iPackType + ",'" + sName + "','" + sUsr + "','" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "',");
            sql.Append("null,'1','" + sPackDesc + "','" + station + "','" + planStartime + "','" + planEndtime + "')");
            return (DBOpt.dbHelper.ExecuteSql(sql.ToString()));
        }

        /// <summary>
        /// �����ĵ�
        /// </summary>
        /// <param name="iRecNo">��¼���</param>
        /// <param name="iPackNo">ҵ����</param>
        /// <param name="iDocTypeNo">�ĵ����ͱ��</param>
        /// <param name="sUsr">�����û�</param>
        /// <param name="iDocNo">�����ĵ����</param>
        public static int CreateDoc(int iRecNo, int iPackNo, int iDocTypeNo, string sUsr, ref uint iDocNo)
        {
            iDocNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_DOC", "F_NO");
            string sName, sDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            DataTable dtTmp;
            dtTmp = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_DOCTYPE where F_NO=" + iDocTypeNo);
            sName = FieldToValue.FieldToString(dtTmp.Rows[0]["F_NAME"].ToString());
            string sTableName = FieldToValue.FieldToString(dtTmp.Rows[0]["F_TABLENAME"].ToString());

            //�����ĵ���¼
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("insert into DMIS_SYS_DOC(F_NO,F_PACKNO,F_DOCTYPENO,F_DOCNAME,F_CREATEMAN,F_CREATEDATE,F_TABLENAME,F_RECNO) VALUES(");
            sql.Append(iDocNo + "," + iPackNo + "," + iDocTypeNo + ",'" + sName + "','" + sUsr + "','" + sDate + "','" + sTableName + "'," + iRecNo + ")");
            return (DBOpt.dbHelper.ExecuteSql(sql.ToString()));
        }


        /// <summary>
        /// ��������,����������Ϣ��Ҫ������Ϣ�����������Ϣ��
        /// ���ڴ���ҵ��ʱ��������ʱʹ��
        /// </summary>
        /// <param name="iCurrWorkFlowNo">��ǰ�������̱�ţ�������ʼ������Ϊ-1</param>
        /// <param name="sUsr">��ǰ�û�</param>
        /// <param name="selFlowNo">Ҫ���������̵����������еı�ţ���ʼ���̿ɵ���GetNextFlow(-1,iPackType)�õ�</param>
        /// <param name="sReceiver">��һ���̵Ľ����ߣ������","�ָ�</param>
        /// <param name="sMainer">��Ҫ�����ߣ�����������ת��</param>
        /// <param name="sDesc">���̸������</param>
        /// <param name="RecNo">���ͻ���ҵ����¼��ţ������������ҵ�����ͬ������һ����Ҳ�Ǵ�TID��������</param>
        /// <returns>������سɹ�����ɵ��ö���Ϣ</returns>
        public static bool CreateFlow(int iPackNo,ref int iCurrWorkFlowNo, string sUsr, int selFlowNo, string sReceiver, string sMainer, string sDesc,string RecNo)
        {
            string sql;
            string sNow = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            int iPackType = -1;
            iPackType = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_PACKTYPENO from DMIS_SYS_PACK where F_NO=" + iPackNo));
            uint ConsumeMinutes = 0;  //ʵ�ʻ���ʱ��
            uint PlanMinutes = 0;     //�ƻ�����ʱ��PlanHours��λ������
            uint InceptMinutes = 0;     //�ӵ�ʱ��InceptHours��λ������
            object objRecDate;

            int curLinkNo;
            int curDocTypeNo, nextDocTypeNo;
            string curTableName, nextTableName;
            string curTID, nextTID = "";
            string nextDoctypeName;
            curTID = RecNo;

            if (iCurrWorkFlowNo > -1)  //������ʼ����
            {
                string sMainer0 = "";
                sMainer0 = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVER from DMIS_SYS_WORKFLOW where F_NO=" + iCurrWorkFlowNo).ToString();
                objRecDate = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVEDATE from DMIS_SYS_MEMBERSTATUS where F_WORKFLOWNO=" + iCurrWorkFlowNo);
                if (objRecDate == null)  //ȡ�������ֵ�ֵ��̫�鷳����Ҫ�úÿ������ʵ�� ��
                {
                    objRecDate = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVEDATE from DMIS_SYS_WORKFLOW where F_NO=" + iCurrWorkFlowNo);
                }

                //2010-6-21 �����޲��Գ��ֵ�����:GetConsumeHours()
                DateTime start;
                CultureInfo ci = new CultureInfo("es-ES");
                try
                {
                    start = DateTime.Parse(objRecDate.ToString(), ci);
                    ConsumeMinutes = GetConsumeHours(start, DateTime.Now);
                }
                catch { }

                sql = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='2',F_FINISHDATE='" + sNow + "',F_WORKDAY=" + ConsumeMinutes + " where F_WORKFLOWNO=" + iCurrWorkFlowNo;
                DBOpt.dbHelper.ExecuteSql(sql);
                //if (DBOpt.dbHelper.ExecuteSql(sql) < 0)
                //{
                //    JScript.Alert("update DMIS_SYS_MEMBERSTATUS error!");
                //    return false;
                //}

                if (sMainer0 != sUsr) return (true);//���������߲��ܲ������̣�ֻ�ܸı�״̬����ֹ����
                //�������ݿ�
                sql = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='2',F_WORKDAY=" + ConsumeMinutes + ",f_finishdate='" + sNow + "' where F_NO=" + iCurrWorkFlowNo;
                if (DBOpt.dbHelper.ExecuteSql(sql) < 0)
                {
                    JScript.Alert("update DMIS_SYS_WORKFLOW error!");
                    return false;
                }

                //�����ڶ�Ӧ�ı�����TID
                curLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_FLOWNO from DMIS_SYS_WORKFLOW where F_NO=" + iCurrWorkFlowNo));
                curTableName = DBOpt.dbHelper.ExecuteScalar("select a.F_TABLENAME from DMIS_SYS_DOCTYPE a,DMIS_SYS_WK_LINK_DOCTYPE b where a.F_NO=b.F_DOCTYPENO and b.F_PACKTYPENO=" +
                    iPackType + " and F_LINKNO=" + curLinkNo).ToString();
                curTID = DBOpt.dbHelper.ExecuteScalar("select F_RECNO from DMIS_SYS_DOC where F_PACKNO=" + iPackNo + " and F_LINKNO=" + curLinkNo).ToString();

                //��һ���ڶ�Ӧ�ı���
                nextDocTypeNo=Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_DOCTYPENO from DMIS_SYS_WK_LINK_DOCTYPE where F_PACKTYPENO="+iPackType+" and F_LINKNO="+selFlowNo));
                nextTableName = DBOpt.dbHelper.ExecuteScalar("select a.F_TABLENAME from DMIS_SYS_DOCTYPE a,DMIS_SYS_WK_LINK_DOCTYPE b where a.F_NO=b.F_DOCTYPENO and b.F_PACKTYPENO=" +
                   iPackType + " and F_LINKNO=" + selFlowNo).ToString();
                nextDoctypeName=DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_DOCTYPE where F_NO="+nextDocTypeNo).ToString();

            }
            else  //����ʼ���ڶ�Ӧ�ı���
            {
                curTableName = DBOpt.dbHelper.ExecuteScalar("select a.F_TABLENAME from DMIS_SYS_DOCTYPE a,DMIS_SYS_WK_LINK_DOCTYPE b where a.F_NO=b.F_DOCTYPENO and b.F_PACKTYPENO=" +
                    iPackType + " and F_LINKNO=" + selFlowNo).ToString();
                nextTableName=curTableName;
                nextDocTypeNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_DOCTYPENO from DMIS_SYS_WK_LINK_DOCTYPE where F_PACKTYPENO=" + iPackType + " and F_LINKNO=" + selFlowNo));
                nextDoctypeName = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_DOCTYPE where F_NO=" + nextDocTypeNo).ToString();
            }

            uint iMaxNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WORKFLOW", "F_NO");
            PlanMinutes = Convert.ToUInt32(DBOpt.dbHelper.ExecuteScalar("select F_PLANDAY from DMIS_SYS_FLOWLINK where F_NO=" + selFlowNo));
            InceptMinutes = Convert.ToUInt32(DBOpt.dbHelper.ExecuteScalar("select F_INCEPT_HOURS from DMIS_SYS_FLOWLINK where F_NO=" + selFlowNo));

            //���ӵ�ʱ��,��ʼ��������Ϊ��,��������Ӧ���ڴ�����
            string LastInecptTime = GetLastTime(DateTime.Now, InceptMinutes);
            //������ʱ��,��ʼ�����ڴ�����,��������Ӧ���ڽӵ���ʱ����,����Ϊ��
            string LastFinishedTime = GetLastTime(DateTime.Now, PlanMinutes);

            
            //�ڵ�����  ayf
            string sName = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_FLOWLINK where F_NO=" + selFlowNo).ToString();
            int flowCat = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_flowcat from dmis_sys_flowlink where f_no="+selFlowNo));
            sql = "insert into DMIS_SYS_WORKFLOW(F_NO,F_PACKNO,F_FLOWNO,"
                        + "F_FLOWNAME,F_SENDER,F_SENDDATE,F_RECEIVER,F_RECEIVEDATE,F_PLANDAY,F_INCEPT_HOURS,F_WORKDAY,"
                        + "F_MSG,F_STATUS,F_WORKING,F_PREFLOWNO,F_LAST_INCEPT_TIME,F_LAST_FINISHED_TIME)  VALUES("
                        + iMaxNo + "," + iPackNo + "," + selFlowNo + ","
                        + "'" + sName + "','" + sUsr + "','" + sNow + "','" + sMainer + "',";
            if (flowCat == 0)   //��ʼ�ڵ�ʱ��Ҫ��F_WORKING��ֵ����Ϊ1�������ڳ��ʱ������ֵ�һ��
                sql += "'" + sNow + "'," + PlanMinutes + "," + InceptMinutes + ",0,'" + sDesc + "','1',1," + iCurrWorkFlowNo + ",'','" + LastFinishedTime + "')";
            else
                sql += "''," + PlanMinutes + "," + InceptMinutes + ",0,'" + sDesc + "','1',0," + iCurrWorkFlowNo + ",'" + LastInecptTime + "','')";

            if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
            {
                JScript.Alert( "insert DMIS_SYS_WORKFLOW error!");
                return false;
            }
            //�����½���������Ӧ��DMIS_SYS_DOC�еļ�¼
            int counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_DOC where F_PACKNO=" + iPackNo + " and F_LINKNO=" + selFlowNo));
            if (counts == 0)  //���������½����ڶ�Ӧ�Ĺ����ĵ�����,���½�֮
            {
                if (curTableName == nextTableName)   //��һ���ں���һ������ͬ������ʼ���ڵ����
                {
                    Int16 tid;
                    if (Int16.TryParse(curTID, out tid))   //curTID����ֵ�����
                        nextTID = curTID;
                    else
                    {
                        nextTID = DBOpt.dbHelper.GetMaxNum(nextTableName, "TID").ToString();  //������ֵ�������
                        sql = "insert into " + nextTableName + "(TID,PACK_NO) values(" + nextTID + "," + iPackNo + ")";
                        if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                        {
                            JScript.Alert("��ҵ���" + nextTableName + " error!");
                            return false;
                        }
                    }
                }
                else  //��һ��������һ���ڲ���ͬ������һ����ҵ���Ҫ�½�ҵ�����ݡ�
                {
                    nextTID = DBOpt.dbHelper.GetMaxNum(nextTableName, "TID").ToString();
                    sql = "insert into " + nextTableName + "(TID,PACK_NO) values(" + nextTID + "," + iPackNo + ")";
                    if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                    {
                        JScript.Alert("insert " + nextTableName + " error!");
                        return false;
                    }
                }
                uint iMaxDocNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_DOC", "F_NO");
                sql="insert into DMIS_SYS_DOC(F_NO,F_PACKNO,F_DOCTYPENO,F_DOCNAME,F_CREATEMAN,F_CREATEDATE,F_TABLENAME,F_RECNO,f_linkno) VALUES("+
                    iMaxDocNo + "," + iPackNo + "," + nextDocTypeNo + ",'" + nextDoctypeName + "','" + sUsr + "','" + sNow + "','" + nextTableName + "'," + nextTID + "," + selFlowNo + ")";
                if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                {
                    JScript.Alert("insert DMIS_SYS_DOC error!");
                    return false;
                }
            }

            //����Ӱ���Ա�Ĺ�����������
            string[] sArrRec = sReceiver.Split(",".ToCharArray());
            for (int i = 0; i <= sArrRec.Length - 1; i++)
            {
                if (sArrRec[i].Trim() == "") continue;   //F_RECEIVER�в������ֵ

                sql = "insert into DMIS_SYS_MEMBERSTATUS(F_PACKNO,F_WORKFLOWNO,F_SENDER,F_SENDDATE,"
                    + "F_RECEIVER,F_RECEIVEDATE,F_STATUS,F_PLANDAY,F_WORKDAY) VALUES(";
                sql += iPackNo + "," + iMaxNo + ",'" + sUsr + "','" + sNow + "','";
                sql += sArrRec[i] + "','" + sNow + "','1'," + PlanMinutes + ",0)";
                if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                {
                    JScript.Alert( "insert DMIS_SYS_MEMBERSTATUS error!");
                    return false;
                }
                // WriteSms(sUsr, sArrRec[i], sPackName, "������Ϣ", sDesc);
            }
            iCurrWorkFlowNo = (Int32)iMaxNo;
            return (true);
            //���Ŵ���
        }


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="iCurrWorkFlowNo">��Ҫ���յ����̱��</param>
        /// <param name="sUsr">��ǰ�û�</param>
        /// <returns>���سɹ������ˢ��ҳ��</returns>
        public static int ReceivFlow(int iCurrWorkFlowNo, string sUsr)
        {
            //��Ҫ�����������
            string sql;
            sql = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='1',F_RECEIVEDATE='" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "' where F_RECEIVER='" + sUsr + "' AND F_NO=" + iCurrWorkFlowNo;
            if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
            {
                JScript.Alert( "update DMIS_SYS_WORKFLOW error!");
                return -1;
            }

            sql = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='1',F_RECEIVEDATE='" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "' where F_RECEIVER='" + sUsr + "' AND F_WORKFLOWNO=" + iCurrWorkFlowNo;
            if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
            {
                JScript.Alert( "update DMIS_SYS_MEMBERSTATUS error!");
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// �������̣�ҵ��鵵
        /// </summary>
        /// <param name="iPackNo"></param>
        public static int EndFlow(int iPackNo, int iWorkFlowNo, string sOper)
        {
            //��Ҫ�����������
            string sql;
            float hours = 0;
            object obj;
            int curLinkNo;
            DateTime receiveDate;
            CultureInfo ci = new CultureInfo("es-ES");

            //2010-04-26���ڰ��������У����ַ�֧�ڵ����˻ص����ʱ��һ�������˻ص���֧�ڵ㣬��һ���������ջ��ڣ�
            //������û�н���ʱ��Ҳ����ԭ�ȵĳ���Ҳ���Թ鵵�����޸�WebWorkFlow.EndFlow�����Ĵ��룬��ʾһ��
            //��������û����ɣ�������鵵
            //2010-04-30
            //�����ջ����ж�����񣬹��ж�ʱ���ҵĹ�����:�������ջ��ڵ������ڵ㻹��δ��ɵ������򷵻�
            sql = "select F_FLOWNO from dmis_sys_workflow where f_packno=" + iPackNo + " and f_no=" + iWorkFlowNo;
            obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null) return -1;

            sql = "select count(*) from dmis_sys_workflow where f_packno=" + iPackNo + " and F_FLOWNO<>" + obj.ToString() + " and f_status='�ڰ�'";
            obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj != null)
            {
                if (Convert.ToInt16(obj) > 0)
                    return -2;   //��Դ˷���ֵ���ھ���Ӧ������������ʾ����ҵ����δ��ɵ�������������ͼ�в�ѯδ��ɵ�����
            }

            //�ѴӰ��ߵ�״̬�������
            DataTable memberStatus = DBOpt.dbHelper.GetDataTable("select F_RECEIVER,f_receivedate from DMIS_SYS_MEMBERSTATUS where F_PACKNO=" + iPackNo + " and F_WORKFLOWNO=" + iWorkFlowNo);
            for (int i = 0; i < memberStatus.Rows.Count; i++)  //�Ӱ���Ա
            {
                //2010-6-21 �����޲��Գ��ֵ�����:�ַ���ת����������
                try
                {
                    receiveDate = DateTime.Parse(memberStatus.Rows[i][1].ToString(), ci);
                    hours = GetConsumeHours(receiveDate, DateTime.Now);
                }
                catch
                {
                }

 
                sql = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='2',F_FINISHDATE='" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "',F_WORKDAY=" + hours + " where F_PACKNO=" + iPackNo + " and F_WORKFLOWNO=" + iWorkFlowNo + " and F_RECEIVER='" + memberStatus.Rows[i][0].ToString() + "'";
                if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                {
                    JScript.Alert( "update DMIS_SYS_MEMBERSTATUS error!");
                    return -1;
                }
                hours = 0;
            }

            //����󻷽ڵ����������ҳ���
            obj = DBOpt.dbHelper.ExecuteScalar("select f_flowno from dmis_sys_workflow where f_packno=" + iPackNo + " and f_no=" + iWorkFlowNo);
            if (obj == null) return -1;   //û���ҵ���ǰ�ڵ���
            curLinkNo = Convert.ToInt16(obj);
            DataTable tasks = DBOpt.dbHelper.GetDataTable("select * from dmis_sys_workflow where f_packno=" + iPackNo + " and f_flowno=" + curLinkNo);
            //�����߽᰸
            if (tasks.Rows.Count > 0)
            {
                for (int i = 0; i < tasks.Rows.Count; i++)
                {

                    //2010-6-21 �����޲��Գ��ֵ�����:�ַ���ת����������
                    try
                    {
                        receiveDate = DateTime.Parse(tasks.Rows[i]["F_RECEIVEDATE"].ToString(), ci);
                        hours = GetConsumeHours(receiveDate, DateTime.Now);
                    }
                    catch
                    {
                    }
                    sql = "update DMIS_SYS_WORKFLOW set F_WORKDAY=" + hours + ",F_STATUS='2',F_FINISHDATE='" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "' where F_NO=" + tasks.Rows[i]["F_NO"].ToString();
                    if (DBOpt.dbHelper.ExecuteSql(sql) < 0)
                    {
                        JScript.Alert("update DMIS_SYS_WORKFLOW error!");
                        return -1;
                    }
                    hours = 0;
                }
                sql = "UPDATE DMIS_SYS_PACK SET F_STATUS='2',F_ARCHIVEDATE='" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "' where F_NO=" + iPackNo;
                if (DBOpt.dbHelper.ExecuteSql(sql) < 0)
                {
                    JScript.Alert("update DMIS_SYS_PACK error!");
                    return -1;
                }
            }


            //����������ϵ��
            sql = "update DMIS_SYS_WK_PACK_RELY set RELY_PACKNO_STATUS='2' where RELY_PACKNO=" + iPackNo;
            DBOpt.dbHelper.ExecuteSql(sql);
            return 1;
        }



        /// <summary>
        /// ��ȡ����ҵ��״̬��ҵ�������б�
        /// </summary>
        /// <param name="sUsr">�����û�</param>
        /// <param name="sFlowState">ҵ������״̬���ڰ졢���͡�����</param>
        /// <returns></returns>
        public DataTable GetStatPack(string sUsr, string sFlowState)
        {
            DataTable dt;
            string sql = "select * from DMIS_VIEW_FLOWSTATE where F_RECEIVER='" + sUsr + "' AND F_STATUS='" + sFlowState + "'";
            dt = DBOpt.dbHelper.GetDataTable(sql);
            return (dt);
        }



        /// <summary>
        /// ��ȡ���̵ĸ������
        /// </summary>
        /// <param name="iCurrFlowNo"></param>
        /// <returns></returns>
        public static string GetFlowDesc(int iCurrFlowNo)
        {
            object obj = DBOpt.dbHelper.ExecuteScalar("select F_MSG from DMIS_SYS_WORKFLOW where F_NO=" + iCurrFlowNo);
            if (obj == null)
                return "";
            else
                return (obj.ToString());
        }


        /// <summary>
        /// ��ȡʣ������
        /// </summary>
        /// <param name="iCurrFlowNo"></param>
        /// <param name="sUsr"></param>
        /// <returns></returns>
        public static int FlowRestDay(int iCurrFlowNo, string sUsr, ref int iWorkDay)
        {
            DataTable dt1;
            DateTime iStart, iEnd;
            iStart = iEnd = DateTime.Now;
            string sRest = "";
            dt1 = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_MEMBERSTATUS where F_WORKFLOWNO=" + iCurrFlowNo + " AND F_RECEIVER='" + sUsr + "'");
            if (dt1.Rows.Count > 0)
            {
                iStart = FieldToValue.FieldToDateTime(dt1.Rows[0]["F_SENDDATE"]);
                iEnd = FieldToValue.FieldToDateTime(dt1.Rows[0]["F_FINISHDATE"]);

                //iPack��iPackType����һ��ֵ��?
                int iPack = FieldToValue.FieldToInt(dt1.Rows[0]["F_PACKNO"]);
                int iPackType = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_PACKTYPENO from DMIS_SYS_PACK where F_NO=" + iPack));

                sRest = GetRestDays(iPackType);   //���ӹ��˻�ȡ�����ݼ����ڣ�������
                int iPlan = FieldToValue.FieldToInt(dt1.Rows[0]["F_PLANDAY"]);
                int iwork = FieldToValue.FieldToInt(dt1.Rows[0]["F_WORKDAY"]);
                if (iStart.Equals(iEnd)) return 0;
                if (iwork < 0)
                {
                    DateTime dt2 = DateTime.Now;
                    iwork = 0;
                    do
                    {
                        if (sRest.IndexOf(iStart.ToString("yyyy-MM-dd")) < 0)
                            iwork++;
                        iStart = iStart.AddDays(1);
                    } while (iStart.ToString("yyyy-MM-dd").CompareTo(iEnd.ToString("yyyy-MM-dd")) < 0);
                }

                iWorkDay = iwork;
                return (iPlan - iwork);
            }
            return (0);
        }

        /// <summary>
        /// ����ҵ�����ͣ���ȡ��Ϣ�գ�������������
        /// Ŀǰ���㷨�ǰ����е���Ϣ�ն�ȡ�����ˣ������ǳ�������
        /// </summary>
        /// <param name="iPackType"></param>
        /// <returns></returns>
        public static string GetRestDays(int iPackType)
        {
            string sResult = "";
            DataTable dt;
            DateTime dt1;
            dt = DBOpt.dbHelper.GetDataTable("select RESTDAY from DMIS_SYS_RESTDAY where F_PACKTYPENO=" + iPackType);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt1 = Convert.ToDateTime(dt.Rows[i][0]);
                    sResult += dt1.ToString("yyyy-MM-dd") + ",";
                }
                sResult = sResult.Substring(0, sResult.Length - 1);
            }
            return (sResult);
        }


        /// <summary>
        /// ���ݵ�ǰ���ںţ��ó���һ���ڵ�����
        /// ���iCurrFlowNo=-1����Ϊ��һ������
        /// </summary>
        /// <param name="CurrFlowNo"></param>
        /// <returns></returns>
        public static DataTable GetNextFlow(int iCurrFlowNo, int iPackType)
        {
            string sql;
            DataTable dtFlow;
            if (iCurrFlowNo > -1)
                sql = "select * from DMIS_SYS_FLOWLINK where "
                   + "F_NO IN(select F_ENDNO from DMIS_SYS_FLOWLINE where F_STARTNO=" + iCurrFlowNo + ")";
            else
                sql = "select * from DMIS_SYS_FLOWLINK where F_FLOWCAT=0 AND F_PACKTYPENO=" + iPackType;
            dtFlow = DBOpt.dbHelper.GetDataTable(sql);
            return (dtFlow);
        }


        /// <summary>
        /// ��ȡ���������̽ڵ�Ŀɲ����û�
        /// ����ID,NAME
        /// </summary>
        /// <param name="iFlowNo"></param>
        /// <returns></returns>
        public static DataTable GetFlowRightUser(int iFlowNo)
        {
            DataTable dtFlow;
            dtFlow = DBOpt.dbHelper.GetDataTable("select DISTINCT A.ID,A.NAME "
                + " from DMIS_SYS_MEMBER A,DMIS_SYS_MEMBER_ROLE B,DMIS_SYS_FLOWFIELDRIGHT C "
                + " where A.ID=B.MEMBER_ID AND B.ROLE_ID=C.F_ROLENO AND C.F_FLOWNO=" + iFlowNo);
            return (dtFlow);
        }


        /// <summary>
        /// �õ�ҵ������нڵ�
        /// </summary>
        /// <param name="iPackType"></param>
        /// <returns></returns>
        public static DataTable GetFlows(int iPackType)
        {
            DataTable dtFlow;
            dtFlow = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_FLOWLINK where F_PACKTYPENO=" + iPackType + " order by f_no");
            return (dtFlow);
        }


        /// <summary>
        ///  ���ص�ǰ�ڵ��ǰһ�ڵ�
        /// </summary>
        /// <param name="iCurrFlowNo"></param>
        /// <param name="sSender"></param>
        /// <returns></returns>
        public static DataTable GetPrevWorkFlow(int iCurrFlowNo, string sSender)
        {
            DataTable dtFlow;
            dtFlow = DBOpt.dbHelper.GetDataTable("select * from DMIS_VIEW_FLOWSTATE where ��һ���� in(select F_PREFLOWNO from DMIS_SYS_WORKFLOW where f_no="
                + iCurrFlowNo + " and F_SENDER='" + sSender + "')");
            return (dtFlow);
        }


        /// <summary>
        /// /// ���ص�ǰҵ��������ĵ�
        /// </summary>
        /// <param name="iPackNo"></param>
        /// <returns></returns>
        public static DataTable GetWorkDocs(int iPackNo)
        {
            DataTable dtDoc;
            dtDoc = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_DOC where F_PACKNO=" + iPackNo);
            return (dtDoc);
        }


        /// <summary>
        /// ���ݵ�¼�û�ӵ�е�Ȩ�ޣ����ؿɲ������ĵ�����
        /// </summary>
        /// <param name="iPackTypeNo"></param>
        /// <param name="iRoleNo"></param>
        /// <returns></returns>
        public static DataTable GetDocs(int iPackTypeNo, string sRoleNos)
        {
            DataTable dtDoc;
            string sql = "select * from DMIS_SYS_DOCTYPE where F_PACKTYPENO=" + iPackTypeNo +
                         " and F_NO in(select F_FOREIGNKEY from DMIS_SYS_RIGHTS where F_ROLENO IN(" + sRoleNos + ") and F_CATGORY='�ĵ�')";

            dtDoc = DBOpt.dbHelper.GetDataTable(sql.ToString());
            return (dtDoc);
        }


        /// <summary>
        /// ��ȡ��ɫ�ڵ�ǰ�����Ƿ���д����ֶε���Ч��
        /// </summary>
        /// <param name="iRoleNo">��ɫ���</param>
        /// <param name="iFlowNo">�������õĻ��ڱ��</param>
        /// <param name="sTabName">ҵ�������</param>
        /// <param name="sFldname">�ֶ�����</param>
        /// <returns>true/false</returns>
        public static bool FieldRight(string iRoleNo, int iFlowNo, string sTabName, string sFldname)
        {
            //if (("," + sRoleNoS + ",").IndexOf(",0,") > -1) return (true);
            StringBuilder sql = new StringBuilder();

            sql.Append("select F_RIGHT from DMIS_SYS_FLOWFIELDRIGHT where F_ROLENO IN(" + iRoleNo + ")");
            sql.Append(" AND F_FLOWNO=" + iFlowNo);
            sql.Append(" AND F_TABLENAME='" + sTabName + "'");
            sql.Append(" AND F_FIELDNAME='" + sFldname + "'");

            object ret;
            ret = DBOpt.dbHelper.ExecuteScalar(sql.ToString());
            if (ret != null)
                return (true);
            else
                return (false);
        }

        /// <summary>
        /// ��ҵ��������ҳ��ʱ������ؼ���Ȩ��
        /// </summary>
        /// <param name="page">ҳ��</param>
        /// <param name="iRoleNo">��ɫ</param>
        /// <param name="iPackTypeNo">ҵ���</param>
        /// <param name="iFlowNo">���̺�</param>
        /// <param name="sTabName">����</param>
        public static void SetWebControlRight(Page page, string iRoleNo, int iPackTypeNo, int iFlowNo, string sTabName)
        {
            string sql;
            int tableID;
            object obj;
            WebControl wc;

            obj = DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + sTabName + "'");
            if (obj == null) return;
            tableID = Convert.ToInt16(obj);

            sql = "select CUSTOM_CONTROL_NAME from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);

            sql = "select A.F_RIGHT,B.CUSTOM_CONTROL_NAME from DMIS_SYS_FLOWFIELDRIGHT A,DMIS_SYS_COLUMNS B where A.F_PACKTYPENO=" + iPackTypeNo + " and A.F_FLOWNO=" + iFlowNo +
                  " and A.F_TABLENAME='" + sTabName + "' and A.F_FIELDNAME=B.NAME and B.TABLE_ID=" + tableID + " and  A.F_ROLENO in(" + iRoleNo + ")";

            DataTable dt1 = DBOpt.dbHelper.GetDataTable(sql);
            DataRow[] rws;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wc = (WebControl)page.FindControl(dt.Rows[i][0].ToString());
                if (wc == null) continue;
                if (("," + iRoleNo + ",").IndexOf(",0,") > -1)��//����Ա,����ƽ̨���ø���������ʲôȨ��
                {
                    if (wc is TextBox)
                        ((TextBox)wc).ReadOnly = false;
                    else
                        wc.Enabled = true;

                    wc.BackColor = System.Drawing.Color.Transparent;
                }
                else
                {
                    rws = dt1.Select("CUSTOM_CONTROL_NAME='" + dt.Rows[i][0].ToString() + "'");
                    if (rws.Length > 0)
                    {
                        if (wc is TextBox)
                            ((TextBox)wc).ReadOnly = false;
                        else
                            wc.Enabled = true;

                        wc.BackColor = System.Drawing.Color.Transparent;
                    }
                }
            }
        }

        /// <summary>
        /// ����ϵͳ����Ա��ҵ����������пɱ༭�ؼ���Ȩ�ޡ�
        /// </summary>
        /// <param name="page"></param>
        /// <param name="iPackTypeNo"></param>
        /// <param name="sTabName"></param>
        public static void SetAllWebControlEnable(Page page, int iPackTypeNo, string sTabName)
        {
            string sql;
            int tableID;
            object obj;
            WebControl wc;

            obj = DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + sTabName + "'");
            if (obj == null) return;
            tableID = Convert.ToInt16(obj);

            sql = "select CUSTOM_CONTROL_NAME from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID;
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wc = (WebControl)page.FindControl(dt.Rows[i][0].ToString());
                if (wc == null) continue;
                if (wc is TextBox)
                    ((TextBox)wc).ReadOnly = false;
                else
                    wc.Enabled = true;
                wc.BackColor = System.Drawing.Color.Transparent;
             }
        }

        /// <summary>
        /// ��ý�ɫ��ҵ�����͵�Ȩ������
        /// </summary>
        /// <param name="iPackTypeNo">ҵ�����ͱ��</param>
        /// <param name="iRoleNo">��ɫ���</param>
        /// <returns>Ȩ�����ݣ���/д/����/ɾ��....</returns>
        public static string sPackTypeRight(int iPackTypeNo, string sRoleNoS)
        {
            if (("," + sRoleNoS + ",").IndexOf(",0,") > -1) return ("1111111");  //����Աʲô��������

            object ret;
            ret = DBOpt.dbHelper.ExecuteScalar("select F_ACCESS from DMIS_SYS_RIGHTS where F_FOREIGNKEY="
                + iPackTypeNo + " AND F_ROLENO IN(" + sRoleNoS + ") AND F_CATGORY='ҵ��'");

            if (ret == null)
                return "";
            else
                return ret.ToString();
        }


        /// <summary>
        /// ��ý�ɫ���ĵ����͵�Ȩ������
        /// </summary>
        /// <param name="iDocTypeNo">�ĵ����ͱ��</param>
        /// <param name="iRoleNo">��ɫ���</param>
        /// <returns>Ȩ�����ݣ���/д/����/ɾ��....��ʽ:1111100</returns>
        public static string sDocTypeRight(int iDocTypeNo, string sRoleNoS)
        {
            if (("," + sRoleNoS + ",").IndexOf(",0,") > -1) return ("1111111");�� //����Աʲô��������

            object ret;
            ret = DBOpt.dbHelper.ExecuteScalar("select F_ACCESS from DMIS_SYS_RIGHTS where F_FOREIGNKEY="
                + iDocTypeNo + " AND F_ROLENO IN(" + sRoleNoS + ") AND F_CATGORY='�ĵ�'");

            if (ret == null)
                return "";
            else
                return ret.ToString();
        }

        /// <summary>
        /// ��ý�ɫ���ĵ����͵�Ȩ������,��¼��Ա�����ж����ɫ����һ���ĵ��ж��ַ���Ȩ�ޣ��ʷ��ص���һ��DataTable
        /// </summary>
        /// <param name="iDocTypeNo">�ĵ����ͱ��</param>
        /// <param name="iRoleNo">��ɫ���</param>
        /// <returns>Ȩ�����ݣ���/д/����/ɾ��....��ʽ:1111100</returns>
        public static DataTable DocTypeRights(int iDocTypeNo, string sRoleNoS)
        {
            DataTable ret;
            ret = DBOpt.dbHelper.GetDataTable("select F_ACCESS from DMIS_SYS_RIGHTS where F_FOREIGNKEY="
                + iDocTypeNo + " AND F_ROLENO IN(" + sRoleNoS + ") AND F_CATGORY='�ĵ�'");

            if (("," + sRoleNoS + ",").IndexOf(",0,") > -1)//����Աʲô��������
            {
                DataRow row = ret.NewRow();
                row[0] = "1111111";
                ret.Rows.InsertAt(row, 0);
            }
            return ret;
        }

        public static void WriteSms(string sSend, string sRec, string sTitle, string sType, string sCont)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            uint iMax = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_SMS", "F_NO");
            sql.Append("INSERT INTO DMIS_SYS_SMS(F_NO, F_TITLE,F_CONT, F_TYPE, F_SENDER, F_SENDDATE, F_RECEIVER, F_FLAG) VALUES(");
            sql.Append(iMax + ",'" + sTitle + "','" + sCont + "','" + sType + "','" + sSend + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + sRec + "',0)");
            DBOpt.dbHelper.ExecuteSql(sql.ToString());
        }
        //Դ��һ��һ��,����ͬһ��ҵ�����
        //2007-03-25 ����
        public static string DataInherit(string TableName, string sField, int iPackNo)
        {
            string sReturn = "";
            DataTable datDt;
            string strSTable,  sSql;
            object obj;
            try
            {
                sSql = "SELECT * FROM DMIS_SYS_DATAINHERIT WHERE F_TTNAME='" + TableName + "' AND F_TFNAME='" + sField+"'";
                datDt = DBOpt.dbHelper.GetDataTable(sSql);
                if (datDt != null)
                {
                    if (datDt.Rows.Count > 0)
                    {
                        strSTable = FieldToValue.FieldToString(datDt.Rows[0]["F_STNAME"]);
                        sSql = "select " + datDt.Rows[0]["F_SFNAME"].ToString() + " from " + strSTable;
                        if (strSTable.ToLower() == "dmis_sys_pack")//��ϵͳ����ȡֵ                            
                            sSql += " where f_no=" + iPackNo;
                        else if (strSTable == TableName)//ͬһ������¼�¼
                            sSql += " where tid in(select max(tid) from " + TableName + " order by tid)";
                        else
                        {//ͬһ����Ĳ�ͬ��
                            sSql += " where tid in(select f_recno from dmis_sys_doc where f_tablename='"
                                + strSTable + "' and f_packno=" + iPackNo + ")";
                        }

                            obj = DBOpt.dbHelper.ExecuteScalar(sSql);
                             if (obj != null)
                                 sReturn = obj.ToString();
                        }
                    }
            }
            catch (Exception ex)
            {

            }
            return (sReturn);

        }

        /// <summary>
        /// �ύǰ�жϱ�����ֶ��Ƿ���д���ݣ����ϣ�����ͨ������������ʵ�֣�
        /// </summary>
        /// <param name="iPackTypeNo">ҵ�����ͱ��</param>
        /// <param name="iPackNo">����ҵ������</param>
        /// <param name="iFlowNo">�ڵ����ͱ��</param>
        /// <returns>����ǿ����ݣ�������ύ��������</returns>
        public static string NeedField(int iPackTypeNo,int iPackNo,int iFlowTypeNo)
        {
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbReturn = new StringBuilder();
            object obj;
            string sql;
            DataRow[] rws;
            sql = "select f_tablename,f_recno from dmis_sys_doc where f_packno =" + iPackNo;  //�ҶԾ͵ı�������¼��
            DataTable dtDoc = DBOpt.dbHelper.GetDataTable(sql);
            sql = "select f_tablename,f_fieldname from DMIS_SYS_NEEDFIELD where f_flowno=" + iFlowTypeNo;  //���ұ�ҵ�����Ҫ��д���ֶ�
            DataTable dtNeedField = DBOpt.dbHelper.GetDataTable(sql);
            for (int i = 0; i < dtDoc.Rows.Count; i++)
            {
                rws = dtNeedField.Select("F_TABLENAME='" + dtDoc.Rows[i][0].ToString() + "'");
                if (rws.Length < 1) continue;
                for (int j = 0; j < rws.Length; j++)
                    sbColumns.Append(rws[j][1].ToString() + ",");
                sql = "select " + sbColumns.Remove(sbColumns.Length - 1, 1).ToString() + " from " + dtDoc.Rows[i][0].ToString() + " where f_no=" + dtDoc.Rows[i][1].ToString();
                DataTable dtTemp = DBOpt.dbHelper.GetDataTable(sql);
                for (int k = 0; k < dtTemp.Columns.Count; k++)
                {
                    if (dtTemp.Rows[0][k] == Convert.DBNull || dtTemp.Rows[0][k].ToString() == "")  //����Ϊ�գ�����������
                    {
                        obj = DBOpt.dbHelper.ExecuteScalar("select a.descr from dmis_sys_columns a,dmis_sys_tables b where a.table_id=b.id and b.name='" + dtDoc.Rows[i][0].ToString() + "' and a.name='" + dtTemp.Columns[k].ColumnName + "'");
                        if (obj == null) continue;
                        sbReturn.Append(obj.ToString() + "No permite una vac��a.;");
                    }
                }
            }
            if (sbReturn.Length > 0)
                return sbReturn.ToString();
            else
                return "";
        }

        /// <summary>
        /// ���̷���ʱ�����ұ������б�����д���ֶΣ����ϣ�����ͨ������������ʵ�֣�
        /// </summary>
        /// <param name="page"></param>
        /// <param name="iPackTypeNo"></param>
        /// <param name="iPackNo"></param>
        /// <param name="iFlowTypeNo"></param>
        /// <returns></returns>
        public static string NeedFieldByControl(Page page, int iPackTypeNo, int iPackNo, int iFlowTypeNo)
        {
            StringBuilder sbReturn = new StringBuilder();
            System.Web.UI.Control control;
            object obj;
            string sql;
            DataRow[] rws;
            sql = "select f_tablename,f_recno from dmis_sys_doc where f_packno =" + iPackNo;  //�Ҷ�Ӧ�ı�������¼��
            DataTable dtDoc = DBOpt.dbHelper.GetDataTable(sql);
            sql = "select f_tablename,f_fieldname from DMIS_SYS_NEEDFIELD where f_flowno=" + iFlowTypeNo;  //���ұ�ҵ�����Ҫ��д���ֶ�
            DataTable dtNeedField = DBOpt.dbHelper.GetDataTable(sql);
            for (int i = 0; i < dtDoc.Rows.Count; i++)
            {
                rws = dtNeedField.Select("F_TABLENAME='" + dtDoc.Rows[i][0].ToString() + "'");
                if (rws.Length < 1) continue;
                for (int j = 0; j < rws.Length; j++)
                {
                    obj = DBOpt.dbHelper.ExecuteScalar("select a.custom_control_name from dmis_sys_columns a,dmis_sys_tables b where a.table_id=b.id and b.name='" + dtDoc.Rows[i][0].ToString() + "' and a.name='" + rws[j][1].ToString() + "'");
                    if (obj == null) continue;
                    control = page.FindControl(obj.ToString());
                    if (control == null) continue;
                    
                    if (control is TextBox)
                    {

                        if (((TextBox)control).Text == "")
                        {
                            obj = DBOpt.dbHelper.ExecuteScalar("select a.descr from dmis_sys_columns a,dmis_sys_tables b where a.table_id=b.id and b.name='" + dtDoc.Rows[i][0].ToString() + "' and a.name='" + rws[j][1].ToString() + "'");
                            if (obj != null) sbReturn.Append(obj.ToString() + "No permite una vac��a.;");
                        }
                    }
                    else if (control is DropDownList)
                    {
                        if (((DropDownList)control).Text == "")
                        {
                            obj = DBOpt.dbHelper.ExecuteScalar("select a.descr from dmis_sys_columns a,dmis_sys_tables b where a.table_id=b.id and b.name='" + dtDoc.Rows[i][0].ToString() + "' and a.name='" + rws[j][1].ToString() + "'");
                            if (obj != null) sbReturn.Append(obj.ToString() + "No permite una vac��a.;");
                        }
                    }
                    else
                        continue;
                }
            }
            if (sbReturn.Length > 0)
                return sbReturn.ToString();
            else
                return "";
        }

        /// <summary>
        /// �༸����
        /// �麣�û�Ҫ��ί����᰸ʱ�������صİ೤�ɵ�û�н��������ί���鲻�ܽ᰸��
        /// </summary>
        /// <param name="wtsPH">ί������</param>
        /// <returns></returns>
        public static bool IsPdFinished(string wtsPH)
        {
            DataTable dt;
            dt = DBOpt.dbHelper.GetDataTable("select F_NO,PD_PH from T_ZH_PD where WTS_PH='" + wtsPH + "'");
            if (dt == null || dt.Rows.Count == 0) return true;   //��ί����û�з����ɵ������Թ鵵

            //�೤�ɵ��ڱ�dmis_sys_packtype�еı��Ϊ3,�ɵ������ڱ�dmis_sys_doctype�еı��Ϊ4
            string sql;
            sql = "select a.f_status from dmis_sys_pack a,dmis_sys_doc b where a.f_no=b.f_packno and a.f_packtypeno=3 and b.f_doctypeno=4 and b.f_tablename='T_ZH_PD' and b.f_recno=" + dt.Rows[0][0].ToString();
            Object obj=DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj != null)
                if (obj.ToString() == "2")
                    return true;
                else
                    return false;
            else
                return true;
        }

        /// <summary>
        /// �жϵ�ǰ�����Ƿ����ɹ����ڣ����򵯳����ʹ��ڣ�����ֱ�ӷ��͡�
        /// </summary>
        /// <param name="iPackNo"></param>
        /// <param name="iCurrWorkFlowNo"></param>
        /// <returns></returns>
        public static bool IsAssignTache(int iPackNo, int CurLinkNo)
        {
            int counts;
            object temp;
            //2008-5��Ϊ��һ���ڵ��ж��������Ϊ���ɹ�����,���ǲ��Ե�,�����������������,ֻ��һ���������·���.
            //�����ǰ����һ���ڵ��ж��������Ϊ���ɹ����ڡ�
            //counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from dmis_sys_flowline where f_startno=" + CurLinkNo));
            //if (counts != 1) return true;

            temp = DBOpt.dbHelper.ExecuteScalar("select IS_ASSIGN from dmis_sys_flowlink where f_no=" + CurLinkNo);  //��ǰ�ڵ��Ƿ����ɹ�����
            if (temp == null) return false;
            if (temp.ToString() == "��")
                return true;
            else
                return false;
        }

        /// <summary>
        /// ֱ�ӷ�������ʱ������������������
        /// ��������,����������Ϣ��Ҫ������Ϣ�����������Ϣ��
        /// ���ڴ���ҵ��ʱ��������ʱʹ��
        /// </summary>
        /// <param name="iCurrWorkFlowNo">��ǰ�������̱�ţ�������ʼ������Ϊ-1</param>
        /// <param name="sUsr">��ǰ�û�</param>
        /// <param name="sDesc">���̸������</param>
        /// <returns>������سɹ�����ɵ��ö���Ϣ</returns>
        public static bool DirectCreateFlow(int iPackNo, ref int iCurrWorkFlowNo, string sUsr)
        {
            string sql;
            string sNow = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            int iPackType = -1;
            iPackType = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_PACKTYPENO from DMIS_SYS_PACK where F_NO=" + iPackNo));
            uint ConsumeMinutes = 0;  //ʵ�ʻ���ʱ��
            uint PlanMinutes = 0;     //�ƻ�����ʱ��
            uint InceptMinutes = 0;     //�ӵ�ʱ��

            //��ǰ���ڵ���Ϣ
            int curFlowNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_flowno from dmis_sys_workflow where F_NO=" + iCurrWorkFlowNo));
            int curDocTypeNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_DOCTYPENO from DMIS_SYS_WK_LINK_DOCTYPE where F_PACKTYPENO=" + iPackType + " and F_LINKNO=" + curFlowNo));
            string curTableName = DBOpt.dbHelper.ExecuteScalar("select F_TABLENAME from DMIS_SYS_DOC where F_PACKNO=" + iPackNo + " and F_LINKNO=" + curFlowNo).ToString();
            string curTID = DBOpt.dbHelper.ExecuteScalar("select F_RECNO from DMIS_SYS_DOC where F_PACKNO=" + iPackNo + " and F_LINKNO=" + curFlowNo).ToString();

            if (iCurrWorkFlowNo > -1)
            {
                string srecDate = "";
                srecDate = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVEDATE from DMIS_SYS_WORKFLOW where F_NO=" + iCurrWorkFlowNo).ToString();
                string sMainer0 = "";
                sMainer0 = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVER from DMIS_SYS_WORKFLOW where F_NO=" + iCurrWorkFlowNo).ToString();

                //2010-6-21 �����޲��Գ��ֵ�����:GetConsumeHours()����Ҫ�漰���ػ�������ת������
                DateTime start;
                CultureInfo ci = new CultureInfo("es-ES");
                try
                {
                    start = DateTime.Parse(srecDate, ci);
                    ConsumeMinutes = GetConsumeHours(start, DateTime.Now);
                }
                catch { }
                
                PlanMinutes = Convert.ToUInt32(DBOpt.dbHelper.ExecuteScalar("select F_PLANDAY from DMIS_SYS_WORKFLOW where F_NO=" + iCurrWorkFlowNo));
                
                //2009-3-12 �û�Ҫ��ͬʱ���´Ӱ���Ա��״̬��
                sql = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='2',F_FINISHDATE='" + sNow + "',F_WORKDAY=" + ConsumeMinutes
                         + "  where F_WORKFLOWNO=" + iCurrWorkFlowNo;
                DBOpt.dbHelper.ExecuteSql(sql);

                if (sMainer0 != sUsr) return (true);//���������߲��ܲ������̣�ֻ�ܸı�״̬����ֹ����
                //�������ݿ⣬����ǻ㼯���ڣ����������������������
                //sql = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='2',F_WORKDAY=" + ConsumeMinutes + ",f_finishdate='" + sNow + "' where F_NO=" + iCurrWorkFlowNo;
                //��������������£��ѱ�ʵ���ͱ����ڶ�Ӧ���������񶼽���
                sql = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='2',F_WORKDAY=" + ConsumeMinutes + ",f_finishdate='" + sNow + "' where f_packno=" + iPackNo + " and f_flowno=" + curFlowNo;
                if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                {
                    JScript.Alert("update DMIS_SYS_WORKFLOW error!");
                    return false;
                }
            }

            //�ұ������µ���������,����������ߵ�����,����
            string tableTID = DBOpt.dbHelper.ExecuteScalar("select f_recno from dmis_sys_doc where f_packno=" + iPackNo).ToString();  //�ұ����ڶ�Ӧ��ҵ���TID
            DataTable nextNode = DBOpt.dbHelper.GetDataTable("select * from dmis_sys_flowline where f_packtypeno=" + iPackType + " and f_startno=" + curFlowNo);
            int nextFlowNo;
            string entIType, enTiID, receiver;
            uint iMaxNo;
            string sName;
            int flowCat;
            int nextDocTypeNo;     //��һ���ڶ�Ӧ���ĵ�����ID
            string nextTableName;  //��һ���ڶ�Ӧ��ҵ���
            string nextTID;        //��һ���ڶԵ�ҵ�������TID��ֵ
            uint nextDocNo=0;         //��һ���ڶ�Ӧ���ĵ���

            for (int i = 0; i < nextNode.Rows.Count; i++)
            {
                if (!LinkGetCondition(iPackType.ToString(), nextNode.Rows[i]["F_NO"].ToString(), iPackNo.ToString(), iCurrWorkFlowNo.ToString(), "", tableTID.ToString())) continue;  //����������,������.
                nextFlowNo = Convert.ToInt16(nextNode.Rows[i]["f_endno"]);
                nextDocTypeNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_DOCTYPENO from DMIS_SYS_WK_LINK_DOCTYPE where F_PACKTYPENO=" + iPackType + " and F_LINKNO=" + nextFlowNo));
                receiver = "";
                entIType=DBOpt.dbHelper.ExecuteScalar("select ENTI_TYPE from DMIS_SYS_WK_DEAL_ENTITY where TEMPLATE_ID=" + iPackType + " and TACHE_ID=" + nextFlowNo).ToString();
                enTiID = DBOpt.dbHelper.ExecuteScalar("select ENTI_ID from DMIS_SYS_WK_DEAL_ENTITY where TEMPLATE_ID=" + iPackType + " and TACHE_ID=" + nextFlowNo).ToString();
                if (entIType == "0")  //��λ
                {
                    receiver = "";
                }
                else if (entIType == "2")//��ػ���
                    receiver = DBOpt.dbHelper.ExecuteScalar("select f_receiver from DMIS_SYS_WORKFLOW where f_packno=" + iPackNo + " and f_flowno=" + enTiID).ToString();
                else if (entIType == "1")//��Ա
                    receiver = enTiID;

                iMaxNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WORKFLOW", "F_NO");
                PlanMinutes = Convert.ToUInt32(DBOpt.dbHelper.ExecuteScalar("select F_PLANDAY from DMIS_SYS_FLOWLINK where F_NO=" + nextFlowNo));
                InceptMinutes = Convert.ToUInt32(DBOpt.dbHelper.ExecuteScalar("select F_INCEPT_HOURS from DMIS_SYS_FLOWLINK where F_NO=" + nextFlowNo));
                
                //���ӵ�ʱ��,��ʼ��������Ϊ��,��������Ӧ���ڴ�����
                string LastInecptTime = GetLastTime(DateTime.Now, InceptMinutes);
                //������ʱ��,ֱ�ӷ��Ϳ϶�������ʼ����,��F_LAST_FINISHED_TIME�е�ֵ���ڴ�����,�ڴ�������������

                sName = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_FLOWLINK where F_NO=" + nextFlowNo).ToString();
                flowCat = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_flowcat from dmis_sys_flowlink where f_no=" + nextFlowNo));

                sql = "insert into DMIS_SYS_WORKFLOW(F_NO,F_PACKNO,F_FLOWNO,"
                        + "F_FLOWNAME,F_SENDER,F_SENDDATE,ENTI_TYPE,ENTI_ID,F_RECEIVER,F_PLANDAY,F_INCEPT_HOURS,F_WORKDAY,"
                        + "F_STATUS,F_WORKING,F_PREFLOWNO,F_LAST_INCEPT_TIME)  VALUES("
                        + iMaxNo + "," + iPackNo + "," + nextFlowNo + ","
                        + "'" + sName + "','" + sUsr + "','" + sNow + "','" + entIType + "','" + enTiID + "','" + receiver + "',";
                if (flowCat == 0)   //��ʼ�ڵ�ʱ��Ҫ��F_WORKING��ֵ����Ϊ1�������ڳ��ʱ������ֵ�һ��
                    sql += PlanMinutes + "," + InceptMinutes + ",0,'1',1," + iCurrWorkFlowNo + ",'" + LastInecptTime + "')";
                else
                    sql += PlanMinutes + "," + InceptMinutes + ",0,'1',0," + iCurrWorkFlowNo + ",'" + LastInecptTime + "')";

                if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                {
                    JScript.Alert("insert DMIS_SYS_WORKFLOW error!");
                    return false;
                }
                else  //����˻��ڵ�DMIS_SYS_DOC�еļ�¼
                {
                    //2010-4-21 �����dmis_sys_doctype�д��ڴ�ʵ�����һ��ںŵļ�¼�����ٲ���
                    sql = "f_packno=" + iPackNo + " and f_linkno=" + nextFlowNo;
                    if (DBOpt.dbHelper.IsExist("DMIS_SYS_DOC", sql))
                        return true;

                    nextTableName = DBOpt.dbHelper.ExecuteScalar("select F_TABLENAME from dmis_sys_doctype where F_NO=" + nextDocTypeNo).ToString();
                    if (curTableName == nextTableName)  //��һ��������һ�������õ��ĵ�,��
                        nextTID=curTID;
                    else
                        nextTID = DBOpt.dbHelper.GetMaxNum(nextTableName, "TID").ToString();
                    if (CreateDoc(nextTID, iPackNo.ToString(), sUsr, ref nextDocNo, nextFlowNo.ToString()) < 0)
                    {
                        JScript.Alert("insert DMIS_SYS_DOC error!");
                        return false;
                    }
                    else   //2009-1-14,����dmis_sys_doc(f_packno��f_recno)����Ӧҵ�����Ӧ��(pack_no��TID)����Ӧ�����
                    {      //���ڴ��Ȳ���ҵ��������,ҵ�����Ҫע��(ֻ����TID��PACK_NO�в�����Ϊ��,������ҪΪ��)
                        if (!DBOpt.dbHelper.IsExist(nextTableName, "TID=" + nextTID))
                        {
                            sql = "insert into " + nextTableName + "(TID,PACK_NO) values(" + nextTID + "," + iPackNo + ")";
                            DBOpt.dbHelper.ExecuteSql(sql);
                        }
                    }
                }
           }
           // iCurrWorkFlowNo = (Int32)iMaxNo;
            return (true);
            //���Ŵ���
        }

        /// <summary>
        /// �������������˶���ʱ��
        /// </summary>
        /// <param name="recevierTime">�յ�ʱ��</param>
        /// <param name="finishedTime">���ʱ��</param>
        /// <returns>ע�⣬���ص��Ƿ�����</returns>
        public static uint GetConsumeHours(DateTime receiveTime, DateTime finishedTime)
        {
            //Web.config�������õ��ϰ�ʱ���
            string ConfigureAmStart, ConfigureAmEnd, ConfigurePmStart, ConfigurePmEnd;
            DateTime AmStart, AmEnd, PmStart, PmEnd, temp;
            DataRow row;
            int index;
            string sql;
            ConfigureAmStart = System.Configuration.ConfigurationManager.AppSettings["AM_START"];
            ConfigureAmEnd = System.Configuration.ConfigurationManager.AppSettings["AM_END"];
            ConfigurePmStart = System.Configuration.ConfigurationManager.AppSettings["PM_START"];
            ConfigurePmEnd = System.Configuration.ConfigurationManager.AppSettings["PM_END"];
            
            //��Ϣ�ղ������������õ���Ϣ�ղ���
            sql = "select * from DMIS_SYS_WK_RESTDAY where to_char(RES_DATE,'YYYYMMDD')>='" + receiveTime.ToString("yyyyMMdd") + "' and to_char(RES_DATE,'YYYYMMDD')<='" + finishedTime.ToString("yyyyMMdd") + "' order by RES_DATE";
            DataTable restDays = DBOpt.dbHelper.GetDataTable(sql);
            DataColumn[] keys = new DataColumn[1];
            keys[0] = restDays.Columns["RES_DATE"];
            restDays.PrimaryKey = keys;

            //ȷ����ʱ��ʼ����
            temp = receiveTime;
            while (restDays.Rows.Contains(Convert.ToDateTime(temp.ToString("yyyy-MM-dd"))))
            {
                row = restDays.Rows.Find(Convert.ToDateTime(temp.ToString("yyyy-MM-dd")));
                index = restDays.Rows.IndexOf(row);
                if (restDays.Rows[index]["IS_HOLIDAY"].ToString() == "1")//��
                {
                    //2009-1-23 ԭ�ȵ��﷨û��"temp="��һ����,�������temp�Ǽ���,��������ѭ��,����ҳ��û����Ӧ������.
                    temp = Convert.ToDateTime(temp.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);
                }
                else
                {
                    break;
                }

                //2009-1-23 ��������һ��Ҳûȥ��ѭ��,��ǿ�Ƴ�
                if (temp.ToString("yyyy-MM-dd") == Convert.ToDateTime(restDays.Rows[restDays.Rows.Count - 1]["RES_DATE"]).ToString("yyyy-MM-dd"))
                    break;
            }

            //ȷ����ʱ��ʼʱ��
            if (temp < Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigureAmStart)) //�������ϰ�ʱ��֮ǰ����ʱ�������ϰ�ʱ�俪ʼ
                temp = Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigureAmStart);
            else if (temp >= Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigureAmEnd) && temp <=Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigurePmStart) ) //�������ϰ�ʱ��������ϰ�ʱ��֮��ķ�Χ�ڣ���ʱ�������ϰ�ʱ�俪ʼ
                temp = Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigurePmStart);
            else if (temp > Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigurePmEnd))  //�������°�ʱ��֮���ڵڶ���������ϰ�ʱ��֮ǰ����ʱ�ӵڶ�����ϰ�ʱ�俪ʼ
                temp = Convert.ToDateTime(temp.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);
            else      //����ʱ�䷶Χ
                temp = temp;
            
            //��ʼ��ʱ
            uint totalMinutes=0;
            TimeSpan ts;
            while (temp < finishedTime)
            {
                AmStart=Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigureAmStart);
                AmEnd=Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigureAmEnd);
                PmStart=Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigurePmStart);
                PmEnd = Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigurePmEnd);
                if (temp <= AmEnd)
                {
                    if(finishedTime<=AmEnd)   // �������ϰ�ʱ��֮�����
                        ts=finishedTime-temp;
                    else
                        ts = AmEnd - temp;

                    temp = PmStart;
                    totalMinutes = totalMinutes + Convert.ToUInt32(ts.TotalMinutes);
                }
                else if (temp <= PmEnd)  //�������ϰ�ʱ��֮�����
                {
                    if (finishedTime <= PmEnd)
                    {
                        ts = finishedTime - temp;
                        temp = PmEnd;
                    }
                    else
                    {
                        ts = PmEnd - temp;
                        temp = temp.AddDays(1);
                        //�жϵڶ����Ƿ�����Ϣ�գ�ֱ���ҵ�������Ϣ�ռ���
                        while (restDays.Rows.Contains(Convert.ToDateTime(temp.ToString("yyyy-MM-dd"))))
                        {
                            row = restDays.Rows.Find(Convert.ToDateTime(temp.ToString("yyyy-MM-dd")));
                            index = restDays.Rows.IndexOf(row);
                            if (restDays.Rows[index]["IS_HOLIDAY"].ToString() == "1")//��
                                temp = temp.AddDays(1);
                            else
                                break;
                        }
                        temp = Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigureAmStart);
                    }
                    totalMinutes = totalMinutes + Convert.ToUInt32(ts.TotalMinutes);
                }
            }
            return totalMinutes;
        }


        /// <summary>
        /// �麣�û�Ҫ��
        /// �����û���,�жϴ��û���ĳ�������Ƿ���������,�����,������޸����ݲ��ύ����,����ֻ�ܿ�����.
        /// </summary>
        /// <param name="InstanceID"></param>
        /// <param name="WorkflowID"></param>
        /// <param name="MemberName"></param>
        /// <returns></returns>
        public static bool IsZhuBanRen(string PackID,string WorkflowID,string MemberName)
        {
            Object obj = DBOpt.dbHelper.ExecuteScalar("select f_receiver from dmis_sys_workflow where f_packno=" + PackID + " and f_no=" + WorkflowID);
            if (obj == null) return false;
            if (obj.ToString() == MemberName)
                return true;
            else
                return false;
        }

        /// <summary>
        /// �Ƿ�Ӱ���Ա
        /// </summary>
        /// <param name="PackID"></param>
        /// <param name="WorkflowID"></param>
        /// <param name="MemberName"></param>
        /// <returns></returns>
        public static bool IsCongBanRen(string PackID, string WorkflowID, string MemberName)
        {
            DataTable dt = DBOpt.dbHelper.GetDataTable("select f_receiver from dmis_sys_memberstatus where f_packno=" + PackID + " and f_workflowno=" + WorkflowID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == MemberName) return true;
            }
            return false;
        }

        /// <summary>
        /// ͳ��ҵ���������ʵ�ʻ��ѵ�ʱ�䣬ʱ��ȡҵ����ж���Ŀ�ʼʱ�����ֹʱ����
        /// </summary>
        /// <param name="PackTypeNo"></param>
        /// <param name="LinkNo"></param>
        /// <param name="PackNo"></param>
        /// <param name="WorkFlowNo"></param>
        public static void StatisicFactuslTimes(string PackTypeNo, string LinkNo,string PackNo, string WorkFlowNo,string TableName,string TableTID)
        {
            DataTable dt = DBOpt.dbHelper.GetDataTable("select STARTTIME_COLUMN,ENDTIME_COLUMN from DMIS_SYS_WK_TIMES_STAT_PARA where F_PACKTYPENO=" + PackTypeNo + " and F_FLOWLINKNO=" + LinkNo);
            if (dt == null || dt.Rows.Count < 1) return;
            if (dt.Rows[0][0] == Convert.DBNull || dt.Rows[0][1] == Convert.DBNull) return;
            string PACKTYPENAME = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_PACKTYPE where F_NO=" + PackTypeNo).ToString();
            string FLOWNAME = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_FLOWLINK where F_NO=" + LinkNo).ToString();
            string PACK_DESC = DBOpt.dbHelper.ExecuteScalar("select F_DESC from DMIS_SYS_PACK where F_NO=" + PackNo).ToString();
            string station = DBOpt.dbHelper.ExecuteScalar("select f_msg from DMIS_SYS_PACK where F_NO=" + PackNo).ToString();
            string sql;
            sql = "delete from DMIS_SYS_WK_TASK_WORKINGTIMES where F_PACKNO=" + PackNo + " and F_WORKFLOWNO=" + WorkFlowNo;
            DBOpt.dbHelper.ExecuteSql(sql);
            
            sql = "select " + dt.Rows[0][0] + "," + dt.Rows[0][1] + " from " + TableName + " where TID=" + TableTID;
            DataTable dtValues = DBOpt.dbHelper.GetDataTable(sql);
            if (dtValues == null || dtValues.Rows.Count < 1) return;
            
            DateTime dtStart, dtEnd;
            if (dtValues.Rows[0][0] == Convert.DBNull || dtValues.Rows[0][1] == Convert.DBNull) return;
            dtStart = Convert.ToDateTime(dtValues.Rows[0][0]);
            dtEnd = Convert.ToDateTime(dtValues.Rows[0][1]);
            if (dtStart > dtEnd) return;
            TimeSpan ts = dtEnd - dtStart;
            //�ұ��������е���Ա
            //����������
            sql = "select f_receiver from dmis_sys_workflow where f_packno=" + PackNo + " and f_no=" + WorkFlowNo;
            DataTable dtMember = DBOpt.dbHelper.GetDataTable(sql);
            //���ҴӰ���Ա
            sql = "select f_receiver from dmis_sys_memberstatus where f_packno=" + PackNo + " and f_workflowno=" + WorkFlowNo;
            DataTable dtTemp = DBOpt.dbHelper.GetDataTable(sql);
            //�ϲ����������в�����Ա
            if (dtTemp != null && dtTemp.Rows.Count > 0)  dtMember.Merge(dtTemp);

            //����ͳ������
            uint max = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WK_TASK_WORKINGTIMES", "TID");
            for (int i = 0; i < dtMember.Rows.Count; i++)
            {
                sql = "insert into DMIS_SYS_WK_TASK_WORKINGTIMES(TID,F_PACKTYPENO,F_FLOWNO,F_PACKNO,F_WORKFLOWNO,F_MEMEBER_NAME,STARTTIME,ENDTIME,HOURS,F_PACKTYPENAME,F_FLOWNAME,F_PACK_DESC,STATION) values(" +
                        max + "," + PackTypeNo + "," + LinkNo + "," + PackNo + "," + WorkFlowNo + ",'" + dtMember.Rows[i][0] + "',TO_DATE('" + dtStart.ToString("yyyy-MM-dd HH:mm")+"','YYYY-MM-DD HH24:MI'),"+
                        "TO_DATE('" + dtEnd.ToString("yyyy-MM-dd HH:mm") + "','YYYY-MM-DD HH24:MI')" + "," + ts.TotalHours.ToString() + ",'" + PACKTYPENAME + "','" + FLOWNAME + "','" + PACK_DESC + "','"+station+"')";
                if (DBOpt.dbHelper.ExecuteSql(sql) > 0) max++;
            }
        }

        /// <summary>
        /// �����˻أ�ֻ���Ǳ���֧���������еķ�֧�������˻ء�
        /// </summary>
        /// <param name="WorkFlowNo">����ID</param>
        /// <returns></returns>
        public static bool Withdraw(string PackNo,string WorkFlowNo,string Reason,string MemberName)
        {
            string today = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            uint iMaxNo;
            string sRecer = "";
            object obj;
            string[] _sqls = new string[2];
            string sql;
            //��ǰһ�ڵ�������
            string preWorkflowNo = DBOpt.dbHelper.ExecuteScalar("select f_preflowno from DMIS_SYS_WORKFLOW where f_no=" + WorkFlowNo).ToString();
            //ǰһ�ڵ�Ĺ���������
            DataTable preWorkflow = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_WORKFLOW where f_no=" + preWorkflowNo);
            //��ǰһ�ڵ��µ����з�֧
            //DataTable dtTmp11 = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_WORKFLOW where f_preflowno=" + preWorkflowNo);
            iMaxNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WORKFLOW", "F_NO");
            sRecer = preWorkflow.Rows[0]["f_receiver"].ToString();

            //ֻ��Ҫ�˻صķ�֧��״̬���ı��ڰ�,�����Ӱ���Ա��״̬
            _sqls[0] = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='3',F_RECEIVEDATE='" + today + "',F_FINISHDATE='" + today + "' WHERE F_WORKFLOWNO=" + WorkFlowNo;
            _sqls[1] = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='3',F_FINISHDATE='" + today + "',F_WORKING=1,F_WORKDAY=0 WHERE F_NO=" + WorkFlowNo;
            if (DBOpt.dbHelper.ExecuteSqlWithTransaction(_sqls) < 0) return false;
            //�ڲ������̴����У��п����ж����֧ͬʱ�˻���һ������Ҫ�ж�һ����һ���Ĺ����������Ƿ���ڣ������ܲ���
            sql = "select count(*) from DMIS_SYS_WORKFLOW where F_PACKNO=" + PackNo + " and F_FLOWNO=" + preWorkflow.Rows[0]["F_FLOWNO"].ToString() + " and F_STATUS='1'";
            obj = DBOpt.dbHelper.ExecuteScalar(sql);
            //���빤���������ݲ�д�˻����
            if (Convert.ToInt16(obj) == 0)
            {
                sql = "INSERT INTO DMIS_SYS_WORKFLOW(F_NO,F_PACKNO,F_FLOWNO,F_FLOWNAME,F_SENDER,F_SENDDATE,F_RECEIVER,F_RECEIVEDATE,F_PLANDAY,"
                        + "F_STATUS,F_PREFLOWNO,F_WORKING,F_MSG)  VALUES(" + iMaxNo + "," + PackNo + "," + preWorkflow.Rows[0]["F_FLOWNO"].ToString()
                        + ",'" + preWorkflow.Rows[0]["F_FLOWNAME"].ToString() + "','" + MemberName + "','"
                        + today + "','" + sRecer + "','" + today + "'," + preWorkflow.Rows[0]["F_PLANDAY"].ToString() + ",'1'," + preWorkflow.Rows[0]["f_preflowno"].ToString() + ",0,'" + Reason + "')";
                if (DBOpt.dbHelper.ExecuteSql(sql) < 0) return false;
            }
            return true;

            ////�����з�֧��״̬���ı��ڰ�,�����Ӱ���Ա��״̬
            //for (int i = 0; i < dtTmp11.Rows.Count; i++)
            //{
            //    _sqls[0] = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='3',F_RECEIVEDATE='" + today + "',F_FINISHDATE='" + today + "' WHERE F_WORKFLOWNO=" + dtTmp11.Rows[i]["f_no"];
            //    _sqls[1] = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='3',F_WORKING=1,F_WORKDAY=0 WHERE F_NO=" + dtTmp11.Rows[i]["f_no"];
            //    if (DBOpt.dbHelper.ExecuteSqlWithTransaction(_sqls) < 0) return false;
            //    //ֻ�������˻صķ�֧�Ų��빤���������ݲ�д�˻����,�����Ϳ���֪����˭�˻ص�.
            //    if (dtTmp11.Rows[i]["f_no"].ToString() == WorkFlowNo)
            //    {
            //        sql = "INSERT INTO DMIS_SYS_WORKFLOW(F_NO,F_PACKNO,F_FLOWNO,F_FLOWNAME,F_SENDER,F_SENDDATE,F_RECEIVER,F_RECEIVEDATE,F_PLANDAY,"
            //                + "F_STATUS,F_PREFLOWNO,F_WORKING,F_MSG)  VALUES(" + iMaxNo + "," + PackNo + "," + preWorkflow.Rows[0]["F_FLOWNO"].ToString()
            //                + ",'" + preWorkflow.Rows[0]["F_FLOWNAME"].ToString() + "','" + MemberName + "','"
            //                + today + "','" + sRecer + "','" + today + "'," + preWorkflow.Rows[0]["F_PLANDAY"].ToString() + ",'1'," + preWorkflow.Rows[0]["f_preflowno"].ToString() + ",0,'" + Reason + "')";
            //        if (DBOpt.dbHelper.ExecuteSql(sql) < 0) return false;
            //    }
            //}
            //return true;
        }

        /// <summary>
        /// �������,ֻҪ���е���һ����֧��û����,�Ϳ����ջ�
        /// </summary>
        /// <param name="PackNo">ҵ����</param>
        /// <param name="WorkFlowNo">���˸շ��͵������,״̬��"���"</param>
        /// <returns></returns>
        public static bool Retake(string WorkFlowNo,string MemberName)
        {
            string[] _sqls = new string[2];
            //�ұ��ڵ������͵�������һ�ڵ�
            DataTable workFlows = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_WORKFLOW where f_preflowno=" + WorkFlowNo);
            for (int i = 0; i < workFlows.Rows.Count; i++)
            {
                _sqls[0] = "DELETE FROM DMIS_SYS_MEMBERSTATUS WHERE F_WORKFLOWNO=" + workFlows.Rows[i]["F_NO"].ToString();
                _sqls[1] = "DELETE FROM DMIS_SYS_WORKFLOW WHERE F_NO=" + workFlows.Rows[i]["F_NO"].ToString();
                if (DBOpt.dbHelper.ExecuteSqlWithTransaction(_sqls) < 0) return false;
            }

            _sqls[0] = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='1' WHERE F_NO=" + WorkFlowNo;
            _sqls[1] = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='1' WHERE F_WORKFLOWNO=" + WorkFlowNo + " AND F_RECEIVER='" + MemberName + "'";
            if (DBOpt.dbHelper.ExecuteSqlWithTransaction(_sqls) > 0) 
                return true;
            else
                return false;
        }


        /// <summary>
        /// �ڵ㷢��ǰ���������,���������,��ʾ�û�
        /// </summary>
        /// <param name="PackTypeNo"></param>
        /// <param name="LinkNo"></param>
        /// <param name="PackNo"></param>
        /// <param name="WorkFlowNo"></param>
        /// <param name="TableName"></param>
        /// <param name="TableTID"></param>
        /// <returns></returns>
        public static string TacheSendCondition(string PackTypeNo, string LinkNo, string PackNo, string WorkFlowNo, string TableName, string TableTID)
        {
            string _sql;
            //��������
            _sql = "select * from DMIS_SYS_WK_CONDITION where COND_TYPE=0 and F_PACKTYPENO=" + PackTypeNo + " and LINK_OR_LINE=" + LinkNo + " order by ORDER_ID";
            DataTable TacheCond = DBOpt.dbHelper.GetDataTable(_sql); 
            if (TacheCond == null || TacheCond.Rows.Count < 1) return "";

            //���ڱ���
            _sql = "select * from DMIS_SYS_WK_VARIABLE where F_PACKTYPENO=" + PackTypeNo+ " and LINK_OR_LINE=" + LinkNo+" and FLAG=0" ;
            DataTable TacheVariable = DBOpt.dbHelper.GetDataTable(_sql);
            if (TacheVariable == null || TacheVariable.Rows.Count < 1) return "";

            //��价�ڱ�����ȡֵ.
            DataColumn dcValue = new DataColumn("VAR_VALUE");   //�����ӱ������ֵ����
            dcValue.DataType = System.Type.GetType("System.String");
            TacheVariable.Columns.Add(dcValue);
            object obj;
            for (int i = 0; i < TacheVariable.Rows.Count; i++)
            {
                if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 0)   //SQL���
                {
                    if(TacheVariable.Rows[i]["MAP_STATEMENT"].ToString().IndexOf(" where ")>0)
                        obj = DBOpt.dbHelper.ExecuteScalar(TacheVariable.Rows[i]["MAP_STATEMENT"].ToString() + " and TID=" + TableTID);
                    else
                        obj = DBOpt.dbHelper.ExecuteScalar(TacheVariable.Rows[i]["MAP_STATEMENT"].ToString() + " where TID=" + TableTID);

                    if (obj != null) TacheVariable.Rows[i]["VAR_VALUE"] = obj.ToString().Replace("\r\n", "");
                    
                }
                else if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 1)   //ȫ�ֱ���,������Session��.
                {
                    if (HttpContext.Current.Session[TacheVariable.Rows[i]["MAP_STATEMENT"].ToString()] != null)
                        TacheVariable.Rows[i]["VAR_VALUE"] = HttpContext.Current.Session[TacheVariable.Rows[i]["MAP_STATEMENT"].ToString()];
                }
                else if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 2)   //�洢����
                {
                    //�Ժ���ʵ��
                }
            }

            //�ѻ��ڱ�����ȡֵ�ŵ�����������,���жϱ��ʽ���߼�ֵ
            string expression;
            for (int i = 0; i < TacheCond.Rows.Count; i++)
            {
                expression = TacheCond.Rows[i]["COND_EXPRESSION"].ToString();
                for (int j = 0; j < TacheVariable.Rows.Count; j++)
                {
                    if (expression.IndexOf(TacheVariable.Rows[j]["VAR_CODE"].ToString()) < 0) continue;
                    switch (TacheVariable.Rows[j]["VAR_TYPE"].ToString())
                    {
                        case "string":
                            expression = expression.Replace(TacheVariable.Rows[j]["VAR_CODE"].ToString(), "\""+TacheVariable.Rows[j]["VAR_VALUE"]+"\"");
                            break;
                        case "char":
                            expression = expression.Replace(TacheVariable.Rows[j]["VAR_CODE"].ToString(), "'" + TacheVariable.Rows[j]["VAR_VALUE"] + "'");
                            break;
                        case "int":
                            expression = expression.Replace(TacheVariable.Rows[j]["VAR_CODE"].ToString(), TacheVariable.Rows[j]["VAR_VALUE"].ToString());
                            break;
                        case "datetime":
                            string temp = "Convert.ToDateTime(\"" + Convert.ToDateTime(TacheVariable.Rows[j]["VAR_VALUE"]).ToString("dd-MM-yyyy HH:mm") + "\")";
                            expression = expression.Replace(TacheVariable.Rows[j]["VAR_CODE"].ToString(), temp);
                            break;
                        case "double":
                            expression = expression.Replace(TacheVariable.Rows[j]["VAR_CODE"].ToString(), TacheVariable.Rows[j]["VAR_VALUE"].ToString());
                            break;
                        default:
                            break;
                    }
                }
                if (!Evaluator.EvaluateToBool(expression)) return TacheCond.Rows[i]["ALERT_MESSAGE"].ToString();   //ֻҪ��һ����������������ʾ��
            }
            return "";
        }

        /// <summary>
        /// ���ߵ�������
        /// </summary>
        /// <param name="PackTypeNo"></param>
        /// <param name="LineNo"></param>
        /// <param name="PackNo"></param>
        /// <param name="WorkFlowNo"></param>
        /// <param name="TableName"></param>
        /// <param name="TableTID"></param>
        /// <returns></returns>
        public static bool LinkGetCondition(string PackTypeNo, string LineNo, string PackNo, string WorkFlowNo, string TableName, string TableTID)
        {
            string _sql;
            //��������
            _sql = "select * from DMIS_SYS_WK_CONDITION where COND_TYPE=1 and F_PACKTYPENO=" + PackTypeNo + " and LINK_OR_LINE=" + LineNo + " order by ORDER_ID";
            DataTable LinkCond = DBOpt.dbHelper.GetDataTable(_sql);
            if (LinkCond == null || LinkCond.Rows.Count < 1) return true;  //������

            //ҵ�����
            _sql = "select * from DMIS_SYS_WK_VARIABLE where F_PACKTYPENO=" + PackTypeNo + " and LINK_OR_LINE=" + LineNo+" and FLAG=1";
            DataTable TacheVariable = DBOpt.dbHelper.GetDataTable(_sql);
            if (TacheVariable == null || TacheVariable.Rows.Count < 1) return true;   //�ޱ���

            //������߱�����ȡֵ.
            DataColumn dcValue = new DataColumn("VAR_VALUE");   //�����ӱ������ֵ����
            dcValue.DataType = System.Type.GetType("System.String");
            TacheVariable.Columns.Add(dcValue);
            object obj;
            for (int i = 0; i < TacheVariable.Rows.Count; i++)
            {
                if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 0)   //SQL���
                {
                    if (TacheVariable.Rows[i]["MAP_STATEMENT"].ToString().IndexOf(" where ") > 0)
                        obj = DBOpt.dbHelper.ExecuteScalar(TacheVariable.Rows[i]["MAP_STATEMENT"].ToString() + " and TID=" + TableTID);
                    else
                        obj = DBOpt.dbHelper.ExecuteScalar(TacheVariable.Rows[i]["MAP_STATEMENT"].ToString() + " where TID=" + TableTID);

                    if (obj != null)
                    {
                        TacheVariable.Rows[i]["VAR_VALUE"] = obj.ToString().Replace("\r\n","");   //�����а����س�ʱ��������������ȥ��
                    }
                }
                else if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 1)   //ȫ�ֱ���,������Session��,ȡ��������,������ӳ�����.
                {
                    if (HttpContext.Current.Session[TacheVariable.Rows[i]["VAR_CODE"].ToString()] != null)
                        TacheVariable.Rows[i]["VAR_VALUE"] = HttpContext.Current.Session[TacheVariable.Rows[i]["VAR_CODE"].ToString()];
                }
                else if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 2)   //�洢����
                {
                    //�Ժ���ʵ��
                }
            }

            //�ѻ��ڱ�����ȡֵ�ŵ�����������,���жϱ��ʽ���߼�ֵ
            string expression;
            for (int i = 0; i < LinkCond.Rows.Count; i++)
            {
                expression = LinkCond.Rows[i]["COND_EXPRESSION"].ToString();
                for (int j = 0; j < TacheVariable.Rows.Count; j++)
                {
                    if (expression.IndexOf(TacheVariable.Rows[j]["VAR_CODE"].ToString()) < 0) continue;
                    switch (TacheVariable.Rows[j]["VAR_TYPE"].ToString())
                    {
                        case "string":
                            expression = expression.Replace(TacheVariable.Rows[j]["VAR_CODE"].ToString(), "\"" + TacheVariable.Rows[j]["VAR_VALUE"] + "\"");
                            break;
                        case "char":
                            expression = expression.Replace(TacheVariable.Rows[j]["VAR_CODE"].ToString(), "'" + TacheVariable.Rows[j]["VAR_VALUE"] + "'");
                            break;
                        case "int":
                            expression = expression.Replace(TacheVariable.Rows[j]["VAR_CODE"].ToString(), TacheVariable.Rows[j]["VAR_VALUE"].ToString());
                            break;
                        case "datetime":
                            string temp = "Convert.ToDateTime(\"" + Convert.ToDateTime(TacheVariable.Rows[j]["VAR_VALUE"]).ToString("dd-MM-yyyy HH:mm") + "\")";
                            expression = expression.Replace(TacheVariable.Rows[j]["VAR_CODE"].ToString(), temp);
                            break;
                        case "double":
                            expression = expression.Replace(TacheVariable.Rows[j]["VAR_CODE"].ToString(), TacheVariable.Rows[j]["VAR_VALUE"].ToString());
                            break;
                        default:
                            break;
                    }
                }
                if (!Evaluator.EvaluateToBool(expression)) return false;
            }
            return true;
        }


        /// <summary>
        /// �����Ӱ���Ա��״̬
        /// </summary>
        /// <param name="iPackNo">ҵ���</param>
        /// <param name="iWorkFlowNo">��ǰ��������</param>
        /// <param name="sOper">�Ӱ���Ա����</param>
        /// <returns></returns>
        public static bool EndMemberStatus(int iPackNo, int iWorkFlowNo, string sOper)
        {
            string sql;
            uint ConsumeMinutes = 0;  //ʵ�ʻ���ʱ��
            float playHours = 0;  //�ƻ�����ʱ��
            object objRecDate = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVEDATE from DMIS_SYS_MEMBERSTATUS where F_PACKNO=" + iPackNo + " and F_WORKFLOWNO=" + iWorkFlowNo + " and F_RECEIVER='" + sOper + "'");
            if (objRecDate == null)  //ȡ�������ֵ�ֵ��̫�鷳����Ҫ�úÿ������ʵ�� ��
            {
                objRecDate = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVEDATE from DMIS_SYS_WORKFLOW where F_PACKNO=" + iPackNo + " and F_NO=" + iWorkFlowNo);
            }

            DateTime start;
            CultureInfo ci = new CultureInfo("es-ES");
            try
            {
                start = DateTime.Parse(objRecDate.ToString(), ci);
                ConsumeMinutes = GetConsumeHours(start, DateTime.Now);
            }
            catch { }
            
            objRecDate=DBOpt.dbHelper.ExecuteScalar("select f_planday from DMIS_SYS_WORKFLOW where F_PACKNO=" + iPackNo + " and F_NO=" + iWorkFlowNo);
            if (objRecDate != null) playHours = Convert.ToSingle(objRecDate);

            sql = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='2',F_FINISHDATE='" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") +
                "',F_WORKDAY=" + ConsumeMinutes + ",F_PLANDAY=" + playHours + "  WHERE F_WORKFLOWNO="
                + iWorkFlowNo + " AND F_RECEIVER='" + sOper + "'";
            if (DBOpt.dbHelper.ExecuteSql(sql) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// �ж��l��Ȩ�޲���ҵ���еĸ���
        /// ���и�ȱ�ݣ�ֻ����һ��ҵ����ֻ��һ�����ļ����͵��ĵ������������Ա�����̵����в��趼�������ɾ������
        /// </summary>
        /// <param name="iPackTypeNo"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool IsCanUploadFile(int iPackTypeNo,string roles)
        {
            //һ��ĳҵ��ֻ��һ�����ļ����͵��ĵ�����ֻ���Ǵ�����
            object obj;
            obj = DBOpt.dbHelper.ExecuteScalar("select F_NO from DMIS_SYS_DOCTYPE where f_doccat='���ļ�' and f_packtypeno=" + iPackTypeNo);
            if (obj == null) return false;
            string DocTypeNo,right;
            DocTypeNo = obj.ToString();
            DataTable dt = DBOpt.dbHelper.GetDataTable("select * from dmis_sys_rights where f_catgory='�ĵ�' and f_foreignkey=" + DocTypeNo+" and f_roleno in("+roles+")");
            if (dt == null || dt.Rows.Count < 1) return false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                right = dt.Rows[i]["f_access"].ToString();
                if (right[1] == '1') return true;  //�ڶ�λ�Ǵ�����Ȩ��
            }
            return false;
        }

        /// <summary>
        /// �����ĵ�,�µ����˼����ÿһ��������DMIS_SYS_DOC�ж�Ҫ������ؼ�¼
        /// </summary>
        /// <param name="iRecNo">��¼���</param>
        /// <param name="iPackNo">ҵ����</param>
        /// <param name="iDocTypeNo">�ĵ����ͱ��</param>
        /// <param name="sUsr">�����û�</param>
        /// <param name="iDocNo">�����ĵ����</param>
        /// <param name="LinkNo">��ǰ���ں�</param>
        public static int CreateDoc(string RecNo, string PackNo, string sUsr, ref uint iDocNo,string LinkNo)
        {
            iDocNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_DOC", "F_NO");
            string sName, sDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            string PackNoType = DBOpt.dbHelper.ExecuteScalar("select f_packtypeno from dmis_sys_pack where f_no=" + PackNo).ToString();
            DataTable dtTmp;
            dtTmp = DBOpt.dbHelper.GetDataTable("select a.* from DMIS_SYS_DOCTYPE a,DMIS_SYS_WK_LINK_DOCTYPE b where a.F_NO=b.F_DOCTYPENO and b.F_PACKTYPENO=" + PackNoType + " and b.F_LINKNO=" + LinkNo);
            sName = FieldToValue.FieldToString(dtTmp.Rows[0]["F_NAME"].ToString());
            string sTableName = FieldToValue.FieldToString(dtTmp.Rows[0]["F_TABLENAME"].ToString());
            string DocTypeNo = dtTmp.Rows[0]["F_NO"].ToString();
            //�����ĵ���¼
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("insert into DMIS_SYS_DOC(F_NO,F_PACKNO,F_DOCTYPENO,F_DOCNAME,F_CREATEMAN,F_CREATEDATE,F_TABLENAME,F_RECNO,f_linkno) VALUES(");
            sql.Append(iDocNo + "," + PackNo + "," + DocTypeNo + ",'" + sName + "','" + sUsr + "','" + sDate + "','" + sTableName + "'," + RecNo + "," + LinkNo + ")");
            return (DBOpt.dbHelper.ExecuteSql(sql.ToString()));
        }

        //�㼯���ڷ���ʱ������������Ӧ�Ĳ��л��ڵ������ɵķ�֧�Ƿ񶼵��˴˻㼯���ڣ������ܷ���
        //�������еķ�֧���ӵ��˲ſ������·��͡�
        public static string InfluxTacheSendCondition(int PackTypeNo,int PackNo,int CurLinkNo,int CurWorkFlowNo)
        {
            string sql;
            bool isFind = false;
            int step = 0;  //���㼯���ڵ�ǰ�沽����ܻ��л㼯����
            int preLinkNo,preWorkFlow;
            DataTable dt;
            int nodeType;  //0��һ��ڵ㡢1����֧�ڵ㡢2���㼯�ڵ�
            preLinkNo = CurLinkNo;
            do
            {
                sql = "select f_startno from dmis_sys_flowline where f_packtypeno=" + PackTypeNo + " and f_endno=" + preLinkNo;
                preLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(sql));
                sql = "select f_flowcat,f_nodetype from dmis_sys_flowlink where f_no=" + preLinkNo;
                dt = DBOpt.dbHelper.GetDataTable(sql);
                nodeType = Convert.ToInt16(dt.Rows[0][1]);
                if(nodeType==1)  //��������֧�ڵ�
                {
                    if (step == 0)
                    {
                        isFind = true;
                        break;
                    }
                    else
                        step--;
                }
                else if (nodeType == 2) //�������㼯�ڵ�
                {
                    step++;
                }
            } while (dt.Rows[0][0].ToString()!="0");  //������ʼ�ڵ�

            if (!isFind) return "error";//"û���ҵ���Ӧ�Ļ㼯���ڣ���������������������ϵ����Ա��";
            //��Ӧ�ķ�֧�ڵ�����Ĺ�������,ע��Ҫ
            preWorkFlow = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_workflow where f_packno=" + PackNo + " and f_flowno=" + preLinkNo + " and f_status='2'"));
            dt = DBOpt.dbHelper.GetDataTable("select distinct f_flowno from dmis_sys_workflow where f_preflowno=" + preWorkFlow + " and (f_status='2' or f_status='1')");
            int effluxCouts = dt.Rows.Count;
            //��Ӧ�Ļ㼯�Ѿ�����Ĺ�������
            int influxCouts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from dmis_sys_workflow where f_packno=" + PackNo + " and f_flowno="+CurLinkNo+" and f_status='1'"));
            if (effluxCouts > influxCouts)
                return "error";//"ǰ�滹������û����ɣ������ύ��";
            else
            {
                //���з�֧�Ѿ������㼯�ڵ㣬�����еĽڵ�û�нӵ��������ܷ���
                int acceptInfluxCouts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from dmis_sys_workflow where f_packno=" + PackNo + " and f_flowno=" + CurLinkNo + " and f_status='1' and f_working=1"));
                if (influxCouts > acceptInfluxCouts)
                    return "error";//"��������û�нӵ��������ύ��";
                else
                    return "";
            }
                
            
        }

        /// <summary>
        /// ͨ��ҵ�������д����Ա��ͳ�ƹ���ʱ��
        /// ��������ͨ����������Ҳ����ͳ��ʱ��
        /// </summary>
        /// <param name="PackTypeNo"></param>
        /// <param name="PackNo"></param>
        /// <param name="CurLinkNo"></param>
        /// <param name="TableName"></param>
        /// <param name="TableTID"></param>
        /// <param name="IsToStation">�Ƿ���վ��ȡֵ:�ǡ���</param>
        /// <param name="Members">��Ա�����б�</param>
        public static void StatisicFactuslTimes(string PackTypeNo, string PackNo,string CurLinkNo, string TableName, string TableTID,string IsToStation,params string[] Members)
        {
            DataTable dt = DBOpt.dbHelper.GetDataTable("select STARTTIME_COLUMN,ENDTIME_COLUMN from DMIS_SYS_WK_TIMES_STAT_PARA where F_PACKTYPENO=" + PackTypeNo + " and F_FLOWLINKNO=" + CurLinkNo);
            if (dt == null || dt.Rows.Count < 1) return;
            if (dt.Rows[0][0] == Convert.DBNull || dt.Rows[0][1] == Convert.DBNull) return;
            string PACKTYPENAME = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_PACKTYPE where F_NO=" + PackTypeNo).ToString();
            string PACK_DESC = DBOpt.dbHelper.ExecuteScalar("select F_DESC from DMIS_SYS_PACK where F_NO=" + PackNo).ToString();
            string station = DBOpt.dbHelper.ExecuteScalar("select f_msg from DMIS_SYS_PACK where F_NO=" + PackNo).ToString();
            string sql;
            sql = "delete from DMIS_SYS_WK_TASK_WORKINGTIMES where F_PACKNO=" + PackNo + " and F_FLOWNO=" + CurLinkNo;
            DBOpt.dbHelper.ExecuteSql(sql);

            sql = "select " + dt.Rows[0][0] + "," + dt.Rows[0][1] + " from " + TableName + " where TID=" + TableTID;
            DataTable dtValues = DBOpt.dbHelper.GetDataTable(sql);
            if (dtValues == null || dtValues.Rows.Count < 1) return;

            DateTime dtStart, dtEnd;
            if (dtValues.Rows[0][0] == Convert.DBNull || dtValues.Rows[0][1] == Convert.DBNull) return;
            dtStart = Convert.ToDateTime(dtValues.Rows[0][0]);
            dtEnd = Convert.ToDateTime(dtValues.Rows[0][1]);
            if (dtStart > dtEnd) return;
            TimeSpan ts = dtEnd - dtStart;

            //����ͳ������
            uint max = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WK_TASK_WORKINGTIMES", "TID");
            for (int i = 0; i < Members.Length; i++)
            {
                if (Members[i]==null || Members[i].Trim() == "") continue;
                sql = "insert into DMIS_SYS_WK_TASK_WORKINGTIMES(TID,F_PACKTYPENO,F_PACKNO,F_FLOWNO,F_MEMEBER_NAME,STARTTIME,ENDTIME,HOURS,F_PACKTYPENAME,F_PACK_DESC,STATION,IS_TO_STATION) values(" +
                        max + "," + PackTypeNo + "," + PackNo + "," + CurLinkNo + ",'" + Members[i] + "',TO_DATE('" + dtStart.ToString("dd-MM-yyyy HH:mm") + "','YYYY-MM-DD HH24:MI')," +
                        "TO_DATE('" + dtEnd.ToString("dd-MM-yyyy HH:mm") + "','YYYY-MM-DD HH24:MI')" + "," + ts.TotalHours.ToString() + ",'" + PACKTYPENAME + "','"  + PACK_DESC + "','" + station + "','"+IsToStation+"')";
                if (DBOpt.dbHelper.ExecuteSql(sql) > 0) max++;
            }
        }

        /// <summary>
        /// �˻�֮ǰ���ж��Ƿ����˻ء�
        /// ��������Ƿ�֧�ڣ������һ���¼��ڵ��˻أ��������¼��ڵ�������ڰ죬�������˻ء�
        /// ֻ�е����з�֧���˻أ���˷�֧�ڵ�ſ����˻�
        /// </summary>
        /// <param name="PackTypeNo"></param>
        /// <param name="PackNo"></param>
        /// <param name="CurLinkNo"></param>
        /// <param name="CurWorkFlowNo"></param>
        /// <returns></returns>
        public static bool IsCanWithdraw(int PackTypeNo, int PackNo, int CurLinkNo, int CurWorkFlowNo)
        {
            int nodeType;
            nodeType = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_nodetype from dmis_sys_flowlink where f_no=" + CurLinkNo));
            //ֻ���Ƿ�֧�ڵ�
            if (nodeType != 1) return true;   //0:һ��ڵ�  1:��֧�ڵ�  2:�㼯�ڵ�

            //�жϴ˷�֧�ڵ�������������ڰ������Ƿ���ڡ�
            string sql;
            int counts;
            sql = "select count(*) from dmis_sys_workflow where f_packno=" + PackNo + " and (f_status='1' or f_status='3') and " +
                " f_flowno in( select f_endno from dmis_sys_flowline where f_startno=" + CurLinkNo + ")";
            counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(sql));
            if (counts == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// �༸û��������ܣ�û�з�����Ӧ����
        /// ���ҵ�ǰҵ����������ҵ���Ƿ���ɣ��������ύ�� 
        /// </summary>
        /// <param name="PackNo"></param>
        /// <param name="CurLinkNo"></param>
        /// <param name="ret">���ص�δ��ɵ�����ҵ�����ͺ���������</param>
        /// <returns></returns>
        public static bool IsRelyPack(int PackNo, int CurLinkNo,out string ret)
        {
            string sql;
            object obj;
            DataTable dt;
            Int16 subPackno=0;
            ret = "";
            //sql = "select RELY_PACKNO from DMIS_SYS_WK_PACK_RELY where PACKNO=" + PackNo + " and LINKNO=" + CurLinkNo + " and RELY_PACKNO_STATUS='1'";
            sql = "select SUBPACKNO from DMIS_SYS_PACK where f_no=" + PackNo;
            obj=DBOpt.dbHelper.ExecuteScalar(sql);
            if (!(obj == null || Int16.TryParse(obj.ToString(), out subPackno)))
            {
                return false;
            }
            else
            {
                sql = "select f_packname,f_desc,f_status from DMIS_SYS_PACK where f_no=" + subPackno;
                dt = DBOpt.dbHelper.GetDataTable(sql);
                if(dt==null || dt.Rows.Count<1)
                    return false;
                else
                {
                    if (dt.Rows[0]["f_status"].ToString() == "2" || dt.Rows[0]["f_status"].ToString() == "4")
                        return false;
                    else
                    {
                        ret = dt.Rows[0]["f_packname"].ToString() + "(" + dt.Rows[0]["f_desc"].ToString() + ");δ���,�������ύ!";
                        return true;
                    }
                }
            }
        }

        /// <summary>
        /// �༸����
        /// ����ĳһ�������ݣ������µ����̣����ݶ�һ��
        /// �麣�û��豸ȱ�ݽ��������õ�����ԭ�Ƚ��������̽᰸��������һ����Ҫ��ȱ�����̡�
        /// ����NewRecNoֻ�ܴ���һ��ֵ����ֻ�ܴ���ҵ��ֻ����һ��ҵ������͵��ĵ������豸ȱ�ݴ���ģ��
        /// </summary>
        /// <param name="OldPackNo">ԭ�ȵ�ҵ����</param>
        /// <param name="NewRecNo">�µ�ҵ�������</param>
        /// <param name="NewPackNo">�����ɵ�ҵ����</param>
        /// <returns></returns>
        public static bool CopyPack(int OldPackNo, int NewRecNo, ref int NewPackNo)
        {
            uint maxPackNo;
            int failCounts=0;
            DataTable dt;
            maxPackNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PACK", "F_NO");
            //����DMIS_SYS_PACK�е����ݡ�
            dt = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_PACK where F_NO=" + OldPackNo);
            if (dt.Rows.Count < 0) return false;
            dt.Rows[0]["F_NO"] = maxPackNo;
            dt.TableName="DMIS_SYS_PACK";
            if (DBOpt.dbHelper.InsertByDataTable(dt, ref failCounts) < 0) return false;

            //����DMIS_SYS_WORKFLOW��dmis_sys_memberstatus�е�����
            uint maxFlowNo;
            int oldFlowNo;
            maxFlowNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WORKFLOW", "F_NO");
            DataTable workflow = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_WORKFLOW where f_packno=" + OldPackNo + " order by f_no");
            workflow.TableName = "DMIS_SYS_WORKFLOW";
            DataTable memberStatus = DBOpt.dbHelper.GetDataTable("select * from dmis_sys_memberstatus where f_packno=" + OldPackNo + " order by f_workflowno");
            memberStatus.TableName = "dmis_sys_memberstatus";
            for (int i=0; i < workflow.Rows.Count; i++)
            {
                oldFlowNo = Convert.ToInt16(workflow.Rows[i]["F_NO"]);
                workflow.Rows[i]["F_NO"] = maxFlowNo;
                workflow.Rows[i]["F_PACKNO"] = maxPackNo;
                for (int k = 0; k < workflow.Rows.Count; k++)
                {
                    if(Convert.ToInt16(workflow.Rows[k]["F_PREFLOWNO"])==oldFlowNo)
                        workflow.Rows[k]["F_PREFLOWNO"]=maxFlowNo;
                }
                for (int j = 0; j < memberStatus.Rows.Count; j++)
                {
                    if (Convert.ToInt16(memberStatus.Rows[j]["F_WORKFLOWNO"]) == oldFlowNo)
                    {
                        memberStatus.Rows[j]["F_WORKFLOWNO"] = maxFlowNo;
                        memberStatus.Rows[j]["F_PACKNO"] = maxPackNo;
                    }
                }
                maxFlowNo++;
            }

            if (DBOpt.dbHelper.InsertByDataTable(workflow, ref failCounts) < 0) return false;
            DBOpt.dbHelper.InsertByDataTable(memberStatus, ref failCounts);

            //�½�DMIS_SYS_DOC�е�����
            //���л���ֻ��Ӧһ���ĵ���ֻ���ø���f_tablename
            uint maxDocNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_DOC", "F_NO");
            DataTable doc = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_DOC where f_packno=" + OldPackNo + " order by f_no");
            doc.TableName = "DMIS_SYS_DOC";
            for (int i=0; i < doc.Rows.Count; i++)
            {
                doc.Rows[i]["F_NO"] = maxDocNo;
                doc.Rows[i]["F_PACKNO"] = maxPackNo;
                doc.Rows[i]["F_RECNO"] = NewRecNo;
                maxDocNo++;
            }
            if (DBOpt.dbHelper.InsertByDataTable(doc, ref failCounts) < 0) return false;
            NewPackNo = Convert.ToInt16(maxPackNo);
            return true;
        }

        //ɾ��ĳҵ��
        public static void DeletePack(int PackNo,string Member)
        {
            string [] sqls=new string[4];
            string sql;
            DataTable pack = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_PACK where f_no=" + PackNo);
            DataTable doc = DBOpt.dbHelper.GetDataTable("select f_tablename,f_recno from dmis_sys_doc where f_packno=" + PackNo);

            //ɾ����������
            sql = "delete from DMIS_SYS_PACK where f_no=" + PackNo;
            sqls[0] = sql;
            sql = "delete from DMIS_SYS_WORKFLOW where f_packno=" + PackNo;
            sqls[1] = sql;
            sql = "delete from dmis_sys_memberstatus where f_packno=" + PackNo;
            sqls[2] = sql;
            sql = "delete from dmis_sys_doc where f_packno=" + PackNo;
            sqls[3] = sql;
            DBOpt.dbHelper.ExecuteSqlWithTransaction(sqls);

            //ɾ��ҵ�������
            if(doc.Rows.Count>0)
            {
                string[] delYW = new string[doc.Rows.Count];
                for (int i = 0; i < doc.Rows.Count; i++)
                    delYW[i] = "delete from " + doc.Rows[i][0].ToString() + " where TID=" + doc.Rows[i][1].ToString();
                DBOpt.dbHelper.ExecuteSqlWithTransaction(delYW);
            }

            //��¼ɾ����־
            if (pack.Rows.Count > 0)
            {
                uint maxLogTid = DBOpt.dbHelper.GetMaxNum("dmis_sys_wk_opt_history", "tid");
                sql = "insert into dmis_sys_wk_opt_history(tid,packno,opt_type,datem,member_name,reason,F_PACKTYPENO,F_PACKTYPENAME) values(" +
                    maxLogTid + "," + PackNo + ",'ɾ��',TO_DATE('" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "','YYYY-MM-DD HH24:MI'),'" +
                    Member + "','ɾ��ҵ�����ͣ�" + pack.Rows[0]["f_packname"].ToString() + "������������" + pack.Rows[0]["f_desc"].ToString() + "�������ˣ�"
                    + pack.Rows[0]["f_createman"].ToString() + "������ʱ�䣺" + pack.Rows[0]["f_createdate"].ToString() + "'," + pack.Rows[0]["f_packtypeno"].ToString() + ",'" + pack.Rows[0]["f_packname"].ToString() + "')";
                DBOpt.dbHelper.ExecuteSql(sql);
            }

        }


        /// <summary>
        /// �ӵ������ڴ����и�ʱ�����ƣ�Ϊ�˴��뷽�㣬�ѽӵ��ͽڵ������������ʱ�������Ȼ�󱣴浽DMIS_SYS_WORKFLOW�С�
        /// DMIS_SYS_MEMBERSTATUS�л�û�д���
        /// </summary>
        /// <param name="start">����ʱ�䣨��ӵ��������ʱ�䣩���ӵ�ʱ�䣨������������ʱ�䣩</param>
        /// <param name="minutes">�����������������õİ����������</param>
        /// <returns>dd-MM-yyyy HH:mm��ʽ���ı�</returns>
        public static string GetLastTime(DateTime start, uint minutes)
        {
            if (start == null || minutes <= 0) return "";

            //Web.config�������õ��ϰ�ʱ���
            string ConfigureAmStart, ConfigureAmEnd, ConfigurePmStart, ConfigurePmEnd;
            DateTime AmStart, AmEnd, PmStart, PmEnd;
            string isHoliDay;
            Object obj;
            ConfigureAmStart = System.Configuration.ConfigurationManager.AppSettings["AM_START"];
            ConfigureAmEnd = System.Configuration.ConfigurationManager.AppSettings["AM_END"];
            ConfigurePmStart = System.Configuration.ConfigurationManager.AppSettings["PM_START"];
            ConfigurePmEnd = System.Configuration.ConfigurationManager.AppSettings["PM_END"];
            AmStart = Convert.ToDateTime(start.ToString("yyyy-MM-dd ") + ConfigureAmStart);
            AmEnd = Convert.ToDateTime(start.ToString("yyyy-MM-dd ") + ConfigureAmEnd);
            PmStart = Convert.ToDateTime(start.ToString("yyyy-MM-dd ") + ConfigurePmStart);
            PmEnd = Convert.ToDateTime(start.ToString("yyyy-MM-dd ") + ConfigurePmEnd);
            uint amMinutes, pmMinutes;  //����������ϰ�Сʱ��
            TimeSpan ts = AmEnd - AmStart;
            amMinutes = Convert.ToUInt32(ts.TotalMinutes);
            ts = PmEnd - PmStart;
            pmMinutes = Convert.ToUInt32(ts.TotalMinutes);

            while (minutes > 0)
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select is_holiday from dmis_sys_wk_restday where to_char(res_date,'YYYYMMDD')='" + start.ToString("yyyyMMdd") + "'");
                if (obj == null)   //�������еĽڼ���û���ҵ����õ���Ϣ������ĩ��������Ϣ�գ������ǹ�����
                {
                    if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
                        isHoliDay = "1";
                    else
                        isHoliDay = "0";
                }
                else
                    isHoliDay = obj.ToString();

                if (isHoliDay == "1")
                {
                    start = Convert.ToDateTime(start.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);  //�ڶ���������ϰ�ʱ�俪ʼ��;
                    continue;
                }

                AmStart = Convert.ToDateTime(start.ToString("yyyy-MM-dd ") + ConfigureAmStart);
                AmEnd = Convert.ToDateTime(start.ToString("yyyy-MM-dd ") + ConfigureAmEnd);
                PmStart = Convert.ToDateTime(start.ToString("yyyy-MM-dd ") + ConfigurePmStart);
                PmEnd = Convert.ToDateTime(start.ToString("yyyy-MM-dd ") + ConfigurePmEnd);

                if (start <= AmEnd)
                {
                    if(start<=AmStart) start = AmStart;
                    ts = AmEnd - start;

                    if (Convert.ToInt32(ts.TotalMinutes) >= minutes)  //�������
                    {
                        start = start.AddMinutes(minutes);
                        minutes = 0;
                    }
                    else
                    {
                        minutes = minutes - Convert.ToUInt32(ts.TotalMinutes);
                        if (minutes > pmMinutes)
                        {
                            minutes = minutes - pmMinutes;
                            start = Convert.ToDateTime(start.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);  //�ڶ���������ϰ�ʱ�俪ʼ��
                            continue;
                        }
                        else   //�������
                        {
                            start = Convert.ToDateTime(start.ToString("yyyy-MM-dd ") + ConfigurePmStart);
                            start = start.AddMinutes(minutes);
                            minutes = 0;
                        }
                    }
                }
                else if (start > AmEnd && start <= PmEnd)
                {
                    if (start <= PmStart) start = PmStart;
                    ts = PmEnd - start;
                    if (Convert.ToInt32(ts.TotalMinutes) >= minutes)
                    {
                        start = start.AddMinutes(minutes);
                        minutes = 0;
                    }
                    else
                    {
                        minutes = minutes - Convert.ToUInt32(ts.TotalMinutes);
                        start = Convert.ToDateTime(start.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);  //�ڶ���������ϰ�ʱ�俪ʼ��;
                    }
                }
                else  //�°�֮��
                {
                    start = Convert.ToDateTime(start.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);  //�ڶ���������ϰ�ʱ�俪ʼ��;
                }
            }
            return start.ToString("dd-MM-yyyy HH:mm");
        }

    }  //��
}��//�����ռ�

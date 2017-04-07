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
    /// 工作流的WEB操作类
    /// </summary>
    public class WebWorkFlow
    {
        /// <summary>
        /// 通过业务类的名字，创建业务，返回业务的编号
        /// </summary>
        /// <param name="iPackType">业务类型编号</param>
        /// <param name="iPackNo">返回业务编号</param>
        public static int CreatePack(int iPackType, string sPackDesc, string sUsr, ref uint iPackNo)
        {
            iPackNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PACK", "F_NO");
            string sName = "";
            sName = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_PACKTYPE where F_NO=" + iPackType).ToString();
            sPackDesc = sPackDesc.Replace('\'', '‘');
            sPackDesc = sPackDesc.Replace('"', '“');
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into DMIS_SYS_PACK(F_NO,F_PACKTYPENO,F_PACKNAME,");
            sql.Append("F_CREATEMAN,F_CREATEDATE,F_ARCHIVEDATE,F_STATUS,F_DESC) VALUES(");
            sql.Append(iPackNo + "," + iPackType + ",'" + sName + "','" + sUsr + "','" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "',");
            sql.Append("null,'1','" + sPackDesc + "')");
            return (DBOpt.dbHelper.ExecuteSql(sql.ToString()));
        }

        /// <summary>
        /// 2008－9－1　敖云峰
        /// 珠海用户特殊要求，要求按照厂站来查询某段时间之内有多少业务办理。
        /// 故F_MSG字段来保存厂站名，方便写查询语句。
        /// 
        /// </summary>
        /// <param name="iPackType"></param>
        /// <param name="sPackDesc"></param>
        /// <param name="sUsr"></param>
        /// <param name="iPackNo"></param>
        /// <param name="station">厂站名，新增加的</param>
        /// <returns></returns>
        public static int CreatePack(int iPackType, string sPackDesc, string sUsr, ref uint iPackNo,string station,string planStartime,string planEndtime)
        {
            iPackNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PACK", "F_NO");
            string sName = "";
            sName = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_PACKTYPE where F_NO=" + iPackType).ToString();
            StringBuilder sql = new StringBuilder();
            sPackDesc = sPackDesc.Replace('\'', '‘');
            sPackDesc = sPackDesc.Replace('"', '“');
            sql.Append("insert into DMIS_SYS_PACK(F_NO,F_PACKTYPENO,F_PACKNAME,");
            sql.Append("F_CREATEMAN,F_CREATEDATE,F_ARCHIVEDATE,F_STATUS,F_DESC,F_MSG,PLAN_STARTTIME,PLAN_ENDTIME) VALUES(");
            sql.Append(iPackNo + "," + iPackType + ",'" + sName + "','" + sUsr + "','" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "',");
            sql.Append("null,'1','" + sPackDesc + "','" + station + "','" + planStartime + "','" + planEndtime + "')");
            return (DBOpt.dbHelper.ExecuteSql(sql.ToString()));
        }

        /// <summary>
        /// 创建文档
        /// </summary>
        /// <param name="iRecNo">记录编号</param>
        /// <param name="iPackNo">业务编号</param>
        /// <param name="iDocTypeNo">文档类型编号</param>
        /// <param name="sUsr">创建用户</param>
        /// <param name="iDocNo">返回文档编号</param>
        public static int CreateDoc(int iRecNo, int iPackNo, int iDocTypeNo, string sUsr, ref uint iDocNo)
        {
            iDocNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_DOC", "F_NO");
            string sName, sDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            DataTable dtTmp;
            dtTmp = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_DOCTYPE where F_NO=" + iDocTypeNo);
            sName = FieldToValue.FieldToString(dtTmp.Rows[0]["F_NAME"].ToString());
            string sTableName = FieldToValue.FieldToString(dtTmp.Rows[0]["F_TABLENAME"].ToString());

            //创建文档记录
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("insert into DMIS_SYS_DOC(F_NO,F_PACKNO,F_DOCTYPENO,F_DOCNAME,F_CREATEMAN,F_CREATEDATE,F_TABLENAME,F_RECNO) VALUES(");
            sql.Append(iDocNo + "," + iPackNo + "," + iDocTypeNo + ",'" + sName + "','" + sUsr + "','" + sDate + "','" + sTableName + "'," + iRecNo + ")");
            return (DBOpt.dbHelper.ExecuteSql(sql.ToString()));
        }


        /// <summary>
        /// 创建流程,但不发送消息，要发送消息，则另调用消息类
        /// 用于创建业务时或发送流程时使用
        /// </summary>
        /// <param name="iCurrWorkFlowNo">当前工作流程编号，创建初始流程则为-1</param>
        /// <param name="sUsr">当前用户</param>
        /// <param name="selFlowNo">要创建的流程的流程配置中的编号，初始流程可调用GetNextFlow(-1,iPackType)得到</param>
        /// <param name="sReceiver">下一流程的接收者，多个用","分隔</param>
        /// <param name="sMainer">主要接收者，用于流程流转用</param>
        /// <param name="sDesc">流程附加意见</param>
        /// <param name="RecNo">发送环节业务表记录编号，如果两个环节业务表相同，则下一环节也是此TID，否则不是</param>
        /// <returns>如果返回成功，则可调用短消息</returns>
        public static bool CreateFlow(int iPackNo,ref int iCurrWorkFlowNo, string sUsr, int selFlowNo, string sReceiver, string sMainer, string sDesc,string RecNo)
        {
            string sql;
            string sNow = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            int iPackType = -1;
            iPackType = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_PACKTYPENO from DMIS_SYS_PACK where F_NO=" + iPackNo));
            uint ConsumeMinutes = 0;  //实际花费时间
            uint PlanMinutes = 0;     //计划花费时间PlanHours单位：分钟
            uint InceptMinutes = 0;     //接单时限InceptHours单位：分钟
            object objRecDate;

            int curLinkNo;
            int curDocTypeNo, nextDocTypeNo;
            string curTableName, nextTableName;
            string curTID, nextTID = "";
            string nextDoctypeName;
            curTID = RecNo;

            if (iCurrWorkFlowNo > -1)  //不是起始环节
            {
                string sMainer0 = "";
                sMainer0 = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVER from DMIS_SYS_WORKFLOW where F_NO=" + iCurrWorkFlowNo).ToString();
                objRecDate = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVEDATE from DMIS_SYS_MEMBERSTATUS where F_WORKFLOWNO=" + iCurrWorkFlowNo);
                if (objRecDate == null)  //取两个部分的值，太麻烦，还要好好考虑如何实现 。
                {
                    objRecDate = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVEDATE from DMIS_SYS_WORKFLOW where F_NO=" + iCurrWorkFlowNo);
                }

                //2010-6-21 曹艳艳测试出现的问题:GetConsumeHours()
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

                if (sMainer0 != sUsr) return (true);//非主操作者不能操作流程，只能改变状态，终止发送
                //更新数据库
                sql = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='2',F_WORKDAY=" + ConsumeMinutes + ",f_finishdate='" + sNow + "' where F_NO=" + iCurrWorkFlowNo;
                if (DBOpt.dbHelper.ExecuteSql(sql) < 0)
                {
                    JScript.Alert("update DMIS_SYS_WORKFLOW error!");
                    return false;
                }

                //本环节对应的表名及TID
                curLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_FLOWNO from DMIS_SYS_WORKFLOW where F_NO=" + iCurrWorkFlowNo));
                curTableName = DBOpt.dbHelper.ExecuteScalar("select a.F_TABLENAME from DMIS_SYS_DOCTYPE a,DMIS_SYS_WK_LINK_DOCTYPE b where a.F_NO=b.F_DOCTYPENO and b.F_PACKTYPENO=" +
                    iPackType + " and F_LINKNO=" + curLinkNo).ToString();
                curTID = DBOpt.dbHelper.ExecuteScalar("select F_RECNO from DMIS_SYS_DOC where F_PACKNO=" + iPackNo + " and F_LINKNO=" + curLinkNo).ToString();

                //下一环节对应的表名
                nextDocTypeNo=Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_DOCTYPENO from DMIS_SYS_WK_LINK_DOCTYPE where F_PACKTYPENO="+iPackType+" and F_LINKNO="+selFlowNo));
                nextTableName = DBOpt.dbHelper.ExecuteScalar("select a.F_TABLENAME from DMIS_SYS_DOCTYPE a,DMIS_SYS_WK_LINK_DOCTYPE b where a.F_NO=b.F_DOCTYPENO and b.F_PACKTYPENO=" +
                   iPackType + " and F_LINKNO=" + selFlowNo).ToString();
                nextDoctypeName=DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_DOCTYPE where F_NO="+nextDocTypeNo).ToString();

            }
            else  //找起始环节对应的表名
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

            //最后接单时间,起始环节设置为空,其它环节应该在此设置
            string LastInecptTime = GetLastTime(DateTime.Now, InceptMinutes);
            //最后完成时间,起始环节在此设置,其它环节应该在接单的时候处理,设置为空
            string LastFinishedTime = GetLastTime(DateTime.Now, PlanMinutes);

            
            //节点类型  ayf
            string sName = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_FLOWLINK where F_NO=" + selFlowNo).ToString();
            int flowCat = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_flowcat from dmis_sys_flowlink where f_no="+selFlowNo));
            sql = "insert into DMIS_SYS_WORKFLOW(F_NO,F_PACKNO,F_FLOWNO,"
                        + "F_FLOWNAME,F_SENDER,F_SENDDATE,F_RECEIVER,F_RECEIVEDATE,F_PLANDAY,F_INCEPT_HOURS,F_WORKDAY,"
                        + "F_MSG,F_STATUS,F_WORKING,F_PREFLOWNO,F_LAST_INCEPT_TIME,F_LAST_FINISHED_TIME)  VALUES("
                        + iMaxNo + "," + iPackNo + "," + selFlowNo + ","
                        + "'" + sName + "','" + sUsr + "','" + sNow + "','" + sMainer + "',";
            if (flowCat == 0)   //起始节点时，要把F_WORKING的值设置为1，否则在抽回时，会出现第一步
                sql += "'" + sNow + "'," + PlanMinutes + "," + InceptMinutes + ",0,'" + sDesc + "','1',1," + iCurrWorkFlowNo + ",'','" + LastFinishedTime + "')";
            else
                sql += "''," + PlanMinutes + "," + InceptMinutes + ",0,'" + sDesc + "','1',0," + iCurrWorkFlowNo + ",'" + LastInecptTime + "','')";

            if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
            {
                JScript.Alert( "insert DMIS_SYS_WORKFLOW error!");
                return false;
            }
            //插入新建工作流对应的DMIS_SYS_DOC中的记录
            int counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_DOC where F_PACKNO=" + iPackNo + " and F_LINKNO=" + selFlowNo));
            if (counts == 0)  //还不存在新建环节对应的工作文档数据,则新建之
            {
                if (curTableName == nextTableName)   //上一环节和下一环节相同或是起始环节的情况
                {
                    Int16 tid;
                    if (Int16.TryParse(curTID, out tid))   //curTID存在值的情况
                        nextTID = curTID;
                    else
                    {
                        nextTID = DBOpt.dbHelper.GetMaxNum(nextTableName, "TID").ToString();  //不存在值的情况，
                        sql = "insert into " + nextTableName + "(TID,PACK_NO) values(" + nextTID + "," + iPackNo + ")";
                        if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                        {
                            JScript.Alert("向业务表" + nextTableName + " error!");
                            return false;
                        }
                    }
                }
                else  //上一环节与下一环节不相同，则下一环节业务表要新建业务数据。
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

            //插入从办人员的工作流程数据
            string[] sArrRec = sReceiver.Split(",".ToCharArray());
            for (int i = 0; i <= sArrRec.Length - 1; i++)
            {
                if (sArrRec[i].Trim() == "") continue;   //F_RECEIVER列不允许空值

                sql = "insert into DMIS_SYS_MEMBERSTATUS(F_PACKNO,F_WORKFLOWNO,F_SENDER,F_SENDDATE,"
                    + "F_RECEIVER,F_RECEIVEDATE,F_STATUS,F_PLANDAY,F_WORKDAY) VALUES(";
                sql += iPackNo + "," + iMaxNo + ",'" + sUsr + "','" + sNow + "','";
                sql += sArrRec[i] + "','" + sNow + "','1'," + PlanMinutes + ",0)";
                if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                {
                    JScript.Alert( "insert DMIS_SYS_MEMBERSTATUS error!");
                    return false;
                }
                // WriteSms(sUsr, sArrRec[i], sPackName, "桌面消息", sDesc);
            }
            iCurrWorkFlowNo = (Int32)iMaxNo;
            return (true);
            //短信处理
        }


        /// <summary>
        /// 接收流程
        /// </summary>
        /// <param name="iCurrWorkFlowNo">需要接收的流程编号</param>
        /// <param name="sUsr">当前用户</param>
        /// <returns>返回成功，则可刷新页面</returns>
        public static int ReceivFlow(int iCurrWorkFlowNo, string sUsr)
        {
            //主要针对主流程者
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
        /// 结束流程，业务归档
        /// </summary>
        /// <param name="iPackNo"></param>
        public static int EndFlow(int iPackNo, int iWorkFlowNo, string sOper)
        {
            //主要针对主流程者
            string sql;
            float hours = 0;
            object obj;
            int curLinkNo;
            DateTime receiveDate;
            CultureInfo ci = new CultureInfo("es-ES");

            //2010-04-26，在班组任务单中，出现分支节点有退回的情况时，一个任务退回到分支节点，另一任务到了验收环节，
            //在任务没有结束时，也可以原先的程序也可以归档，故修改WebWorkFlow.EndFlow（）的代码，提示一下
            //还有任务没有完成，不允许归档
            //2010-04-30
            //在验收环节有多个任务，故判断时，找的规则是:不在验收环节的其它节点还有未完成的任务，则返回
            sql = "select F_FLOWNO from dmis_sys_workflow where f_packno=" + iPackNo + " and f_no=" + iWorkFlowNo;
            obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj == null) return -1;

            sql = "select count(*) from dmis_sys_workflow where f_packno=" + iPackNo + " and F_FLOWNO<>" + obj.ToString() + " and f_status='在办'";
            obj = DBOpt.dbHelper.ExecuteScalar(sql);
            if (obj != null)
            {
                if (Convert.ToInt16(obj) > 0)
                    return -2;   //针对此返回值，在具体应用有作如下提示：此业务还有未完成的任务，请在流程图中查询未完成的任务！
            }

            //把从办者的状态设置完成
            DataTable memberStatus = DBOpt.dbHelper.GetDataTable("select F_RECEIVER,f_receivedate from DMIS_SYS_MEMBERSTATUS where F_PACKNO=" + iPackNo + " and F_WORKFLOWNO=" + iWorkFlowNo);
            for (int i = 0; i < memberStatus.Rows.Count; i++)  //从办人员
            {
                //2010-6-21 曹艳艳测试出现的问题:字符串转换成日期型
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

            //把最后环节的所有任务都找出来
            obj = DBOpt.dbHelper.ExecuteScalar("select f_flowno from dmis_sys_workflow where f_packno=" + iPackNo + " and f_no=" + iWorkFlowNo);
            if (obj == null) return -1;   //没有找到当前节点编号
            curLinkNo = Convert.ToInt16(obj);
            DataTable tasks = DBOpt.dbHelper.GetDataTable("select * from dmis_sys_workflow where f_packno=" + iPackNo + " and f_flowno=" + curLinkNo);
            //主办者结案
            if (tasks.Rows.Count > 0)
            {
                for (int i = 0; i < tasks.Rows.Count; i++)
                {

                    //2010-6-21 曹艳艳测试出现的问题:字符串转换成日期型
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


            //更新依赖关系表
            sql = "update DMIS_SYS_WK_PACK_RELY set RELY_PACKNO_STATUS='2' where RELY_PACKNO=" + iPackNo;
            DBOpt.dbHelper.ExecuteSql(sql);
            return 1;
        }



        /// <summary>
        /// 获取所需业务状态的业务流程列表
        /// </summary>
        /// <param name="sUsr">操作用户</param>
        /// <param name="sFlowState">业务流程状态：在办、发送、待办</param>
        /// <returns></returns>
        public DataTable GetStatPack(string sUsr, string sFlowState)
        {
            DataTable dt;
            string sql = "select * from DMIS_VIEW_FLOWSTATE where F_RECEIVER='" + sUsr + "' AND F_STATUS='" + sFlowState + "'";
            dt = DBOpt.dbHelper.GetDataTable(sql);
            return (dt);
        }



        /// <summary>
        /// 获取流程的附加意见
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
        /// 获取剩余天数
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

                //iPack和iPackType不是一个值吗?
                int iPack = FieldToValue.FieldToInt(dt1.Rows[0]["F_PACKNO"]);
                int iPackType = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_PACKTYPENO from DMIS_SYS_PACK where F_NO=" + iPack));

                sRest = GetRestDays(iPackType);   //不加过滤获取所有休假日期，不合适
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
        /// 根据业务类型，获取休息日，不计入流程中
        /// 目前的算法是把所有的休息日都取出来了，这样非常不合理。
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
        /// 根据当前环节号，得出下一环节的内容
        /// 如果iCurrFlowNo=-1，则为第一个环节
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
        /// 获取配置中流程节点的可操作用户
        /// 返回ID,NAME
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
        /// 得到业务的所有节点
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
        ///  返回当前节点的前一节点
        /// </summary>
        /// <param name="iCurrFlowNo"></param>
        /// <param name="sSender"></param>
        /// <returns></returns>
        public static DataTable GetPrevWorkFlow(int iCurrFlowNo, string sSender)
        {
            DataTable dtFlow;
            dtFlow = DBOpt.dbHelper.GetDataTable("select * from DMIS_VIEW_FLOWSTATE where 上一环节 in(select F_PREFLOWNO from DMIS_SYS_WORKFLOW where f_no="
                + iCurrFlowNo + " and F_SENDER='" + sSender + "')");
            return (dtFlow);
        }


        /// <summary>
        /// /// 返回当前业务的所有文档
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
        /// 根据登录用户拥有的权限，返回可操作的文档类型
        /// </summary>
        /// <param name="iPackTypeNo"></param>
        /// <param name="iRoleNo"></param>
        /// <returns></returns>
        public static DataTable GetDocs(int iPackTypeNo, string sRoleNos)
        {
            DataTable dtDoc;
            string sql = "select * from DMIS_SYS_DOCTYPE where F_PACKTYPENO=" + iPackTypeNo +
                         " and F_NO in(select F_FOREIGNKEY from DMIS_SYS_RIGHTS where F_ROLENO IN(" + sRoleNos + ") and F_CATGORY='文档')";

            dtDoc = DBOpt.dbHelper.GetDataTable(sql.ToString());
            return (dtDoc);
        }


        /// <summary>
        /// 获取角色在当前环节是否具有处理字段的有效性
        /// </summary>
        /// <param name="iRoleNo">角色编号</param>
        /// <param name="iFlowNo">流程配置的环节编号</param>
        /// <param name="sTabName">业务表名称</param>
        /// <param name="sFldname">字段名称</param>
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
        /// 打开业务处理数据页面时，处理控件的权限
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="iRoleNo">角色</param>
        /// <param name="iPackTypeNo">业务号</param>
        /// <param name="iFlowNo">流程号</param>
        /// <param name="sTabName">表名</param>
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
                if (("," + iRoleNo + ",").IndexOf(",0,") > -1)　//管理员,故在平台不用给管理设置什么权限
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
        /// 对于系统管理员打开业务表，开放所有可编辑控件的权限。
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
        /// 获得角色的业务类型的权限内容
        /// </summary>
        /// <param name="iPackTypeNo">业务类型编号</param>
        /// <param name="iRoleNo">角色编号</param>
        /// <returns>权限内容：读/写/创建/删除....</returns>
        public static string sPackTypeRight(int iPackTypeNo, string sRoleNoS)
        {
            if (("," + sRoleNoS + ",").IndexOf(",0,") > -1) return ("1111111");  //管理员什么都可以做

            object ret;
            ret = DBOpt.dbHelper.ExecuteScalar("select F_ACCESS from DMIS_SYS_RIGHTS where F_FOREIGNKEY="
                + iPackTypeNo + " AND F_ROLENO IN(" + sRoleNoS + ") AND F_CATGORY='业务'");

            if (ret == null)
                return "";
            else
                return ret.ToString();
        }


        /// <summary>
        /// 获得角色的文档类型的权限内容
        /// </summary>
        /// <param name="iDocTypeNo">文档类型编号</param>
        /// <param name="iRoleNo">角色编号</param>
        /// <returns>权限内容：读/写/创建/删除....格式:1111100</returns>
        public static string sDocTypeRight(int iDocTypeNo, string sRoleNoS)
        {
            if (("," + sRoleNoS + ",").IndexOf(",0,") > -1) return ("1111111");　 //管理员什么都可以做

            object ret;
            ret = DBOpt.dbHelper.ExecuteScalar("select F_ACCESS from DMIS_SYS_RIGHTS where F_FOREIGNKEY="
                + iDocTypeNo + " AND F_ROLENO IN(" + sRoleNoS + ") AND F_CATGORY='文档'");

            if (ret == null)
                return "";
            else
                return ret.ToString();
        }

        /// <summary>
        /// 获得角色的文档类型的权限内容,登录人员可以有多个角色，对一个文档有多种访问权限，故返回的是一个DataTable
        /// </summary>
        /// <param name="iDocTypeNo">文档类型编号</param>
        /// <param name="iRoleNo">角色编号</param>
        /// <returns>权限内容：读/写/创建/删除....格式:1111100</returns>
        public static DataTable DocTypeRights(int iDocTypeNo, string sRoleNoS)
        {
            DataTable ret;
            ret = DBOpt.dbHelper.GetDataTable("select F_ACCESS from DMIS_SYS_RIGHTS where F_FOREIGNKEY="
                + iDocTypeNo + " AND F_ROLENO IN(" + sRoleNoS + ") AND F_CATGORY='文档'");

            if (("," + sRoleNoS + ",").IndexOf(",0,") > -1)//管理员什么都可以做
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
        //源表不一定一个,但在同一个业务包中
        //2007-03-25 唐文
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
                        if (strSTable.ToLower() == "dmis_sys_pack")//从系统表中取值                            
                            sSql += " where f_no=" + iPackNo;
                        else if (strSTable == TableName)//同一表的上下记录
                            sSql += " where tid in(select max(tid) from " + TableName + " order by tid)";
                        else
                        {//同一任务的不同表
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
        /// 提交前判断必填的字段是否填写内容（作废，现在通过发送条件来实现）
        /// </summary>
        /// <param name="iPackTypeNo">业务类型编号</param>
        /// <param name="iPackNo">具体业务主键</param>
        /// <param name="iFlowNo">节点类型编号</param>
        /// <returns>如果是空内容，则可以提交，否则不行</returns>
        public static string NeedField(int iPackTypeNo,int iPackNo,int iFlowTypeNo)
        {
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbReturn = new StringBuilder();
            object obj;
            string sql;
            DataRow[] rws;
            sql = "select f_tablename,f_recno from dmis_sys_doc where f_packno =" + iPackNo;  //找对就的表名及记录号
            DataTable dtDoc = DBOpt.dbHelper.GetDataTable(sql);
            sql = "select f_tablename,f_fieldname from DMIS_SYS_NEEDFIELD where f_flowno=" + iFlowTypeNo;  //查找本业务必须要填写的字段
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
                    if (dtTemp.Rows[0][k] == Convert.DBNull || dtTemp.Rows[0][k].ToString() == "")  //内容为空，则找列描述
                    {
                        obj = DBOpt.dbHelper.ExecuteScalar("select a.descr from dmis_sys_columns a,dmis_sys_tables b where a.table_id=b.id and b.name='" + dtDoc.Rows[i][0].ToString() + "' and a.name='" + dtTemp.Columns[k].ColumnName + "'");
                        if (obj == null) continue;
                        sbReturn.Append(obj.ToString() + "No permite una vacía.;");
                    }
                }
            }
            if (sbReturn.Length > 0)
                return sbReturn.ToString();
            else
                return "";
        }

        /// <summary>
        /// 流程发送时，查找本步骤中必须填写的字段（作废，现在通过发送条件来实现）
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
            sql = "select f_tablename,f_recno from dmis_sys_doc where f_packno =" + iPackNo;  //找对应的表名及记录号
            DataTable dtDoc = DBOpt.dbHelper.GetDataTable(sql);
            sql = "select f_tablename,f_fieldname from DMIS_SYS_NEEDFIELD where f_flowno=" + iFlowTypeNo;  //查找本业务必须要填写的字段
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
                            if (obj != null) sbReturn.Append(obj.ToString() + "No permite una vacía.;");
                        }
                    }
                    else if (control is DropDownList)
                    {
                        if (((DropDownList)control).Text == "")
                        {
                            obj = DBOpt.dbHelper.ExecuteScalar("select a.descr from dmis_sys_columns a,dmis_sys_tables b where a.table_id=b.id and b.name='" + dtDoc.Rows[i][0].ToString() + "' and a.name='" + rws[j][1].ToString() + "'");
                            if (obj != null) sbReturn.Append(obj.ToString() + "No permite una vacía.;");
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
        /// 赤几作废
        /// 珠海用户要求委托书结案时，如果相关的班长派单没有结束，则此委托书不能结案。
        /// </summary>
        /// <param name="wtsPH">委托书编号</param>
        /// <returns></returns>
        public static bool IsPdFinished(string wtsPH)
        {
            DataTable dt;
            dt = DBOpt.dbHelper.GetDataTable("select F_NO,PD_PH from T_ZH_PD where WTS_PH='" + wtsPH + "'");
            if (dt == null || dt.Rows.Count == 0) return true;   //此委托书没有发生派单，可以归档

            //班长派单在表dmis_sys_packtype中的编号为3,派单内容在表dmis_sys_doctype中的编号为4
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
        /// 判断当前环节是否是派工环节，是则弹出发送窗口，否则直接发送。
        /// </summary>
        /// <param name="iPackNo"></param>
        /// <param name="iCurrWorkFlowNo"></param>
        /// <returns></returns>
        public static bool IsAssignTache(int iPackNo, int CurLinkNo)
        {
            int counts;
            object temp;
            //2008-5认为下一个节点有多个，则认为是派工环节,这是不对的,多个条件是有条件的,只有一个或多个往下发送.
            //如果当前的下一个节点有多个，则认为是派工环节。
            //counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from dmis_sys_flowline where f_startno=" + CurLinkNo));
            //if (counts != 1) return true;

            temp = DBOpt.dbHelper.ExecuteScalar("select IS_ASSIGN from dmis_sys_flowlink where f_no=" + CurLinkNo);  //当前节点是否是派工环节
            if (temp == null) return false;
            if (temp.ToString() == "是")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 直接发送流程时，创建工作流程数据
        /// 创建流程,但不发送消息，要发送消息，则另调用消息类
        /// 用于创建业务时或发送流程时使用
        /// </summary>
        /// <param name="iCurrWorkFlowNo">当前工作流程编号，创建初始流程则为-1</param>
        /// <param name="sUsr">当前用户</param>
        /// <param name="sDesc">流程附加意见</param>
        /// <returns>如果返回成功，则可调用短消息</returns>
        public static bool DirectCreateFlow(int iPackNo, ref int iCurrWorkFlowNo, string sUsr)
        {
            string sql;
            string sNow = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            int iPackType = -1;
            iPackType = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_PACKTYPENO from DMIS_SYS_PACK where F_NO=" + iPackNo));
            uint ConsumeMinutes = 0;  //实际花费时间
            uint PlanMinutes = 0;     //计划花费时间
            uint InceptMinutes = 0;     //接单时限

            //当前环节的信息
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

                //2010-6-21 曹艳艳测试出现的问题:GetConsumeHours()，主要涉及本地化的日期转换问题
                DateTime start;
                CultureInfo ci = new CultureInfo("es-ES");
                try
                {
                    start = DateTime.Parse(srecDate, ci);
                    ConsumeMinutes = GetConsumeHours(start, DateTime.Now);
                }
                catch { }
                
                PlanMinutes = Convert.ToUInt32(DBOpt.dbHelper.ExecuteScalar("select F_PLANDAY from DMIS_SYS_WORKFLOW where F_NO=" + iCurrWorkFlowNo));
                
                //2009-3-12 用户要求同时更新从办人员的状态。
                sql = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='2',F_FINISHDATE='" + sNow + "',F_WORKDAY=" + ConsumeMinutes
                         + "  where F_WORKFLOWNO=" + iCurrWorkFlowNo;
                DBOpt.dbHelper.ExecuteSql(sql);

                if (sMainer0 != sUsr) return (true);//非主操作者不能操作流程，只能改变状态，终止发送
                //更新数据库，如果是汇集环节，则不能用如下语句这样更新
                //sql = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='2',F_WORKDAY=" + ConsumeMinutes + ",f_finishdate='" + sNow + "' where F_NO=" + iCurrWorkFlowNo;
                //须用下面的语句更新，把本实例和本环节对应的所有任务都结束
                sql = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='2',F_WORKDAY=" + ConsumeMinutes + ",f_finishdate='" + sNow + "' where f_packno=" + iPackNo + " and f_flowno=" + curFlowNo;
                if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                {
                    JScript.Alert("update DMIS_SYS_WORKFLOW error!");
                    return false;
                }
            }

            //找本环节下的所有连线,如果满足连线的条件,则发送
            string tableTID = DBOpt.dbHelper.ExecuteScalar("select f_recno from dmis_sys_doc where f_packno=" + iPackNo).ToString();  //找本环节对应的业务表TID
            DataTable nextNode = DBOpt.dbHelper.GetDataTable("select * from dmis_sys_flowline where f_packtypeno=" + iPackType + " and f_startno=" + curFlowNo);
            int nextFlowNo;
            string entIType, enTiID, receiver;
            uint iMaxNo;
            string sName;
            int flowCat;
            int nextDocTypeNo;     //下一环节对应的文档类型ID
            string nextTableName;  //下一环节对应的业务表
            string nextTID;        //下一环节对的业务表主键TID的值
            uint nextDocNo=0;         //下一环节对应的文档号

            for (int i = 0; i < nextNode.Rows.Count; i++)
            {
                if (!LinkGetCondition(iPackType.ToString(), nextNode.Rows[i]["F_NO"].ToString(), iPackNo.ToString(), iCurrWorkFlowNo.ToString(), "", tableTID.ToString())) continue;  //不满足条件,不发送.
                nextFlowNo = Convert.ToInt16(nextNode.Rows[i]["f_endno"]);
                nextDocTypeNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select F_DOCTYPENO from DMIS_SYS_WK_LINK_DOCTYPE where F_PACKTYPENO=" + iPackType + " and F_LINKNO=" + nextFlowNo));
                receiver = "";
                entIType=DBOpt.dbHelper.ExecuteScalar("select ENTI_TYPE from DMIS_SYS_WK_DEAL_ENTITY where TEMPLATE_ID=" + iPackType + " and TACHE_ID=" + nextFlowNo).ToString();
                enTiID = DBOpt.dbHelper.ExecuteScalar("select ENTI_ID from DMIS_SYS_WK_DEAL_ENTITY where TEMPLATE_ID=" + iPackType + " and TACHE_ID=" + nextFlowNo).ToString();
                if (entIType == "0")  //岗位
                {
                    receiver = "";
                }
                else if (entIType == "2")//相关环节
                    receiver = DBOpt.dbHelper.ExecuteScalar("select f_receiver from DMIS_SYS_WORKFLOW where f_packno=" + iPackNo + " and f_flowno=" + enTiID).ToString();
                else if (entIType == "1")//人员
                    receiver = enTiID;

                iMaxNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WORKFLOW", "F_NO");
                PlanMinutes = Convert.ToUInt32(DBOpt.dbHelper.ExecuteScalar("select F_PLANDAY from DMIS_SYS_FLOWLINK where F_NO=" + nextFlowNo));
                InceptMinutes = Convert.ToUInt32(DBOpt.dbHelper.ExecuteScalar("select F_INCEPT_HOURS from DMIS_SYS_FLOWLINK where F_NO=" + nextFlowNo));
                
                //最后接单时间,起始环节设置为空,其它环节应该在此设置
                string LastInecptTime = GetLastTime(DateTime.Now, InceptMinutes);
                //最后完成时间,直接发送肯定不是起始环节,故F_LAST_FINISHED_TIME列的值不在此设置,在待办任务中设置

                sName = DBOpt.dbHelper.ExecuteScalar("select F_NAME from DMIS_SYS_FLOWLINK where F_NO=" + nextFlowNo).ToString();
                flowCat = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_flowcat from dmis_sys_flowlink where f_no=" + nextFlowNo));

                sql = "insert into DMIS_SYS_WORKFLOW(F_NO,F_PACKNO,F_FLOWNO,"
                        + "F_FLOWNAME,F_SENDER,F_SENDDATE,ENTI_TYPE,ENTI_ID,F_RECEIVER,F_PLANDAY,F_INCEPT_HOURS,F_WORKDAY,"
                        + "F_STATUS,F_WORKING,F_PREFLOWNO,F_LAST_INCEPT_TIME)  VALUES("
                        + iMaxNo + "," + iPackNo + "," + nextFlowNo + ","
                        + "'" + sName + "','" + sUsr + "','" + sNow + "','" + entIType + "','" + enTiID + "','" + receiver + "',";
                if (flowCat == 0)   //起始节点时，要把F_WORKING的值设置为1，否则在抽回时，会出现第一步
                    sql += PlanMinutes + "," + InceptMinutes + ",0,'1',1," + iCurrWorkFlowNo + ",'" + LastInecptTime + "')";
                else
                    sql += PlanMinutes + "," + InceptMinutes + ",0,'1',0," + iCurrWorkFlowNo + ",'" + LastInecptTime + "')";

                if (DBOpt.dbHelper.ExecuteSql(sql) < 1)
                {
                    JScript.Alert("insert DMIS_SYS_WORKFLOW error!");
                    return false;
                }
                else  //插入此环节的DMIS_SYS_DOC中的记录
                {
                    //2010-4-21 如果在dmis_sys_doctype中存在此实例号且环节号的记录，则不再插入
                    sql = "f_packno=" + iPackNo + " and f_linkno=" + nextFlowNo;
                    if (DBOpt.dbHelper.IsExist("DMIS_SYS_DOC", sql))
                        return true;

                    nextTableName = DBOpt.dbHelper.ExecuteScalar("select F_TABLENAME from dmis_sys_doctype where F_NO=" + nextDocTypeNo).ToString();
                    if (curTableName == nextTableName)  //上一环节与下一环节引用的文档,表
                        nextTID=curTID;
                    else
                        nextTID = DBOpt.dbHelper.GetMaxNum(nextTableName, "TID").ToString();
                    if (CreateDoc(nextTID, iPackNo.ToString(), sUsr, ref nextDocNo, nextFlowNo.ToString()) < 0)
                    {
                        JScript.Alert("insert DMIS_SYS_DOC error!");
                        return false;
                    }
                    else   //2009-1-14,出现dmis_sys_doc(f_packno和f_recno)与相应业务表相应列(pack_no和TID)不对应的情况
                    {      //故在此先插入业务表的数据,业务表建表要注意(只允许TID和PACK_NO列不允许为空,其它都要为空)
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
            //短信处理
        }

        /// <summary>
        /// 计算任务处理花费了多少时间
        /// </summary>
        /// <param name="recevierTime">收到时间</param>
        /// <param name="finishedTime">完成时间</param>
        /// <returns>注意，返回的是分钟数</returns>
        public static uint GetConsumeHours(DateTime receiveTime, DateTime finishedTime)
        {
            //Web.config中所配置的上班时间段
            string ConfigureAmStart, ConfigureAmEnd, ConfigurePmStart, ConfigurePmEnd;
            DateTime AmStart, AmEnd, PmStart, PmEnd, temp;
            DataRow row;
            int index;
            string sql;
            ConfigureAmStart = System.Configuration.ConfigurationManager.AppSettings["AM_START"];
            ConfigureAmEnd = System.Configuration.ConfigurationManager.AppSettings["AM_END"];
            ConfigurePmStart = System.Configuration.ConfigurationManager.AppSettings["PM_START"];
            ConfigurePmEnd = System.Configuration.ConfigurationManager.AppSettings["PM_END"];
            
            //休息日参数表中所设置的休息日参数
            sql = "select * from DMIS_SYS_WK_RESTDAY where to_char(RES_DATE,'YYYYMMDD')>='" + receiveTime.ToString("yyyyMMdd") + "' and to_char(RES_DATE,'YYYYMMDD')<='" + finishedTime.ToString("yyyyMMdd") + "' order by RES_DATE";
            DataTable restDays = DBOpt.dbHelper.GetDataTable(sql);
            DataColumn[] keys = new DataColumn[1];
            keys[0] = restDays.Columns["RES_DATE"];
            restDays.PrimaryKey = keys;

            //确定计时开始日期
            temp = receiveTime;
            while (restDays.Rows.Contains(Convert.ToDateTime(temp.ToString("yyyy-MM-dd"))))
            {
                row = restDays.Rows.Find(Convert.ToDateTime(temp.ToString("yyyy-MM-dd")));
                index = restDays.Rows.IndexOf(row);
                if (restDays.Rows[index]["IS_HOLIDAY"].ToString() == "1")//是
                {
                    //2009-1-23 原先的语法没有"temp="这一部分,导致如果temp是假期,则会出现死循环,发生页面没有响应的现象.
                    temp = Convert.ToDateTime(temp.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);
                }
                else
                {
                    break;
                }

                //2009-1-23 如果到最后一天也没去此循环,则强制出
                if (temp.ToString("yyyy-MM-dd") == Convert.ToDateTime(restDays.Rows[restDays.Rows.Count - 1]["RES_DATE"]).ToString("yyyy-MM-dd"))
                    break;
            }

            //确定计时开始时间
            if (temp < Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigureAmStart)) //在上午上班时间之前，计时从上午上班时间开始
                temp = Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigureAmStart);
            else if (temp >= Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigureAmEnd) && temp <=Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigurePmStart) ) //在上午上班时间和下午上班时间之间的范围内，计时从下午上班时间开始
                temp = Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigurePmStart);
            else if (temp > Convert.ToDateTime(temp.ToString("yyyy-MM-dd ") + ConfigurePmEnd))  //在下午下班时间之后，在第二天的上午上班时间之前，计时从第二天的上班时间开始
                temp = Convert.ToDateTime(temp.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);
            else      //正常时间范围
                temp = temp;
            
            //开始计时
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
                    if(finishedTime<=AmEnd)   // 在上午上班时间之内完成
                        ts=finishedTime-temp;
                    else
                        ts = AmEnd - temp;

                    temp = PmStart;
                    totalMinutes = totalMinutes + Convert.ToUInt32(ts.TotalMinutes);
                }
                else if (temp <= PmEnd)  //在下午上班时间之内完成
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
                        //判断第二天是否是休息日，直到找到不是休息日即可
                        while (restDays.Rows.Contains(Convert.ToDateTime(temp.ToString("yyyy-MM-dd"))))
                        {
                            row = restDays.Rows.Find(Convert.ToDateTime(temp.ToString("yyyy-MM-dd")));
                            index = restDays.Rows.IndexOf(row);
                            if (restDays.Rows[index]["IS_HOLIDAY"].ToString() == "1")//是
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
        /// 珠海用户要求
        /// 根据用户名,判断此用户在某任务中是否是主办者,如果是,则可以修改数据并提交任务,否则只能看内容.
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
        /// 是否从办人员
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
        /// 统计业务输过程中实际花费的时间，时间取业务表中定义的开始时间和终止时间列
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
            //找本环节所有的人员
            //先找主办者
            sql = "select f_receiver from dmis_sys_workflow where f_packno=" + PackNo + " and f_no=" + WorkFlowNo;
            DataTable dtMember = DBOpt.dbHelper.GetDataTable(sql);
            //再找从办人员
            sql = "select f_receiver from dmis_sys_memberstatus where f_packno=" + PackNo + " and f_workflowno=" + WorkFlowNo;
            DataTable dtTemp = DBOpt.dbHelper.GetDataTable(sql);
            //合并本环节所有参与人员
            if (dtTemp != null && dtTemp.Rows.Count > 0)  dtMember.Merge(dtTemp);

            //插入统计数据
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
        /// 任务退回，只考虑本分支，其它并行的分支不考虑退回。
        /// </summary>
        /// <param name="WorkFlowNo">任务ID</param>
        /// <returns></returns>
        public static bool Withdraw(string PackNo,string WorkFlowNo,string Reason,string MemberName)
        {
            string today = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            uint iMaxNo;
            string sRecer = "";
            object obj;
            string[] _sqls = new string[2];
            string sql;
            //找前一节点的任务号
            string preWorkflowNo = DBOpt.dbHelper.ExecuteScalar("select f_preflowno from DMIS_SYS_WORKFLOW where f_no=" + WorkFlowNo).ToString();
            //前一节点的工作流数据
            DataTable preWorkflow = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_WORKFLOW where f_no=" + preWorkflowNo);
            //找前一节点下的所有分支
            //DataTable dtTmp11 = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_WORKFLOW where f_preflowno=" + preWorkflowNo);
            iMaxNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WORKFLOW", "F_NO");
            sRecer = preWorkflow.Rows[0]["f_receiver"].ToString();

            //只把要退回的分支的状态都改变在办,包括从办人员的状态
            _sqls[0] = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='3',F_RECEIVEDATE='" + today + "',F_FINISHDATE='" + today + "' WHERE F_WORKFLOWNO=" + WorkFlowNo;
            _sqls[1] = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='3',F_FINISHDATE='" + today + "',F_WORKING=1,F_WORKDAY=0 WHERE F_NO=" + WorkFlowNo;
            if (DBOpt.dbHelper.ExecuteSqlWithTransaction(_sqls) < 0) return false;
            //在并行流程处理中，有可能有多个分支同时退回上一步，故要判断一下上一步的工作流数据是否存在，否则不能插入
            sql = "select count(*) from DMIS_SYS_WORKFLOW where F_PACKNO=" + PackNo + " and F_FLOWNO=" + preWorkflow.Rows[0]["F_FLOWNO"].ToString() + " and F_STATUS='1'";
            obj = DBOpt.dbHelper.ExecuteScalar(sql);
            //插入工作流程数据并写退回意见
            if (Convert.ToInt16(obj) == 0)
            {
                sql = "INSERT INTO DMIS_SYS_WORKFLOW(F_NO,F_PACKNO,F_FLOWNO,F_FLOWNAME,F_SENDER,F_SENDDATE,F_RECEIVER,F_RECEIVEDATE,F_PLANDAY,"
                        + "F_STATUS,F_PREFLOWNO,F_WORKING,F_MSG)  VALUES(" + iMaxNo + "," + PackNo + "," + preWorkflow.Rows[0]["F_FLOWNO"].ToString()
                        + ",'" + preWorkflow.Rows[0]["F_FLOWNAME"].ToString() + "','" + MemberName + "','"
                        + today + "','" + sRecer + "','" + today + "'," + preWorkflow.Rows[0]["F_PLANDAY"].ToString() + ",'1'," + preWorkflow.Rows[0]["f_preflowno"].ToString() + ",0,'" + Reason + "')";
                if (DBOpt.dbHelper.ExecuteSql(sql) < 0) return false;
            }
            return true;

            ////把所有分支的状态都改变在办,包括从办人员的状态
            //for (int i = 0; i < dtTmp11.Rows.Count; i++)
            //{
            //    _sqls[0] = "UPDATE DMIS_SYS_MEMBERSTATUS SET F_STATUS='3',F_RECEIVEDATE='" + today + "',F_FINISHDATE='" + today + "' WHERE F_WORKFLOWNO=" + dtTmp11.Rows[i]["f_no"];
            //    _sqls[1] = "UPDATE DMIS_SYS_WORKFLOW SET F_STATUS='3',F_WORKING=1,F_WORKDAY=0 WHERE F_NO=" + dtTmp11.Rows[i]["f_no"];
            //    if (DBOpt.dbHelper.ExecuteSqlWithTransaction(_sqls) < 0) return false;
            //    //只有属于退回的分支才插入工作流程数据并写退回意见,这样就可以知道是谁退回的.
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
        /// 抽回任务,只要所有的下一步分支都没接收,就可以收回
        /// </summary>
        /// <param name="PackNo">业务编号</param>
        /// <param name="WorkFlowNo">本人刚发送的任务号,状态是"完成"</param>
        /// <returns></returns>
        public static bool Retake(string WorkFlowNo,string MemberName)
        {
            string[] _sqls = new string[2];
            //找本节点所发送的所有下一节点
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
        /// 节点发送前的条件检查,如果不满足,提示用户
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
            //环节条件
            _sql = "select * from DMIS_SYS_WK_CONDITION where COND_TYPE=0 and F_PACKTYPENO=" + PackTypeNo + " and LINK_OR_LINE=" + LinkNo + " order by ORDER_ID";
            DataTable TacheCond = DBOpt.dbHelper.GetDataTable(_sql); 
            if (TacheCond == null || TacheCond.Rows.Count < 1) return "";

            //环节变量
            _sql = "select * from DMIS_SYS_WK_VARIABLE where F_PACKTYPENO=" + PackTypeNo+ " and LINK_OR_LINE=" + LinkNo+" and FLAG=0" ;
            DataTable TacheVariable = DBOpt.dbHelper.GetDataTable(_sql);
            if (TacheVariable == null || TacheVariable.Rows.Count < 1) return "";

            //填充环节变量的取值.
            DataColumn dcValue = new DataColumn("VAR_VALUE");   //先增加保存变量值的列
            dcValue.DataType = System.Type.GetType("System.String");
            TacheVariable.Columns.Add(dcValue);
            object obj;
            for (int i = 0; i < TacheVariable.Rows.Count; i++)
            {
                if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 0)   //SQL语句
                {
                    if(TacheVariable.Rows[i]["MAP_STATEMENT"].ToString().IndexOf(" where ")>0)
                        obj = DBOpt.dbHelper.ExecuteScalar(TacheVariable.Rows[i]["MAP_STATEMENT"].ToString() + " and TID=" + TableTID);
                    else
                        obj = DBOpt.dbHelper.ExecuteScalar(TacheVariable.Rows[i]["MAP_STATEMENT"].ToString() + " where TID=" + TableTID);

                    if (obj != null) TacheVariable.Rows[i]["VAR_VALUE"] = obj.ToString().Replace("\r\n", "");
                    
                }
                else if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 1)   //全局变量,保存在Session中.
                {
                    if (HttpContext.Current.Session[TacheVariable.Rows[i]["MAP_STATEMENT"].ToString()] != null)
                        TacheVariable.Rows[i]["VAR_VALUE"] = HttpContext.Current.Session[TacheVariable.Rows[i]["MAP_STATEMENT"].ToString()];
                }
                else if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 2)   //存储过程
                {
                    //以后再实现
                }
            }

            //把环节变量的取值放到环节条件中,并判断表达式的逻辑值
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
                if (!Evaluator.EvaluateToBool(expression)) return TacheCond.Rows[i]["ALERT_MESSAGE"].ToString();   //只要有一个条件不满足则提示！
            }
            return "";
        }

        /// <summary>
        /// 连线到达条件
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
            //连线条件
            _sql = "select * from DMIS_SYS_WK_CONDITION where COND_TYPE=1 and F_PACKTYPENO=" + PackTypeNo + " and LINK_OR_LINE=" + LineNo + " order by ORDER_ID";
            DataTable LinkCond = DBOpt.dbHelper.GetDataTable(_sql);
            if (LinkCond == null || LinkCond.Rows.Count < 1) return true;  //无条件

            //业务变量
            _sql = "select * from DMIS_SYS_WK_VARIABLE where F_PACKTYPENO=" + PackTypeNo + " and LINK_OR_LINE=" + LineNo+" and FLAG=1";
            DataTable TacheVariable = DBOpt.dbHelper.GetDataTable(_sql);
            if (TacheVariable == null || TacheVariable.Rows.Count < 1) return true;   //无变量

            //填充连线变量的取值.
            DataColumn dcValue = new DataColumn("VAR_VALUE");   //先增加保存变量值的列
            dcValue.DataType = System.Type.GetType("System.String");
            TacheVariable.Columns.Add(dcValue);
            object obj;
            for (int i = 0; i < TacheVariable.Rows.Count; i++)
            {
                if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 0)   //SQL语句
                {
                    if (TacheVariable.Rows[i]["MAP_STATEMENT"].ToString().IndexOf(" where ") > 0)
                        obj = DBOpt.dbHelper.ExecuteScalar(TacheVariable.Rows[i]["MAP_STATEMENT"].ToString() + " and TID=" + TableTID);
                    else
                        obj = DBOpt.dbHelper.ExecuteScalar(TacheVariable.Rows[i]["MAP_STATEMENT"].ToString() + " where TID=" + TableTID);

                    if (obj != null)
                    {
                        TacheVariable.Rows[i]["VAR_VALUE"] = obj.ToString().Replace("\r\n","");   //内容中包含回车时，则会出错，所以先去掉
                    }
                }
                else if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 1)   //全局变量,保存在Session中,取变量代码,而不是映射语句.
                {
                    if (HttpContext.Current.Session[TacheVariable.Rows[i]["VAR_CODE"].ToString()] != null)
                        TacheVariable.Rows[i]["VAR_VALUE"] = HttpContext.Current.Session[TacheVariable.Rows[i]["VAR_CODE"].ToString()];
                }
                else if (Convert.ToInt16(TacheVariable.Rows[i]["MAP_TYPE"]) == 2)   //存储过程
                {
                    //以后再实现
                }
            }

            //把环节变量的取值放到环节条件中,并判断表达式的逻辑值
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
        /// 结束从办人员的状态
        /// </summary>
        /// <param name="iPackNo">业务号</param>
        /// <param name="iWorkFlowNo">当前工作流号</param>
        /// <param name="sOper">从办人员姓名</param>
        /// <returns></returns>
        public static bool EndMemberStatus(int iPackNo, int iWorkFlowNo, string sOper)
        {
            string sql;
            uint ConsumeMinutes = 0;  //实际花费时间
            float playHours = 0;  //计划花费时间
            object objRecDate = DBOpt.dbHelper.ExecuteScalar("select F_RECEIVEDATE from DMIS_SYS_MEMBERSTATUS where F_PACKNO=" + iPackNo + " and F_WORKFLOWNO=" + iWorkFlowNo + " and F_RECEIVER='" + sOper + "'");
            if (objRecDate == null)  //取两个部分的值，太麻烦，还要好好考虑如何实现 。
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
        /// 判断誰有权限操作业务中的附件
        /// 还有个缺陷，只考虑一个业务中只有一个单文件类型的文档，而且相关人员在流程的所有步骤都可以添加删除附件
        /// </summary>
        /// <param name="iPackTypeNo"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool IsCanUploadFile(int iPackTypeNo,string roles)
        {
            //一般某业务只有一个单文件类型的文档，故只考虑此类型
            object obj;
            obj = DBOpt.dbHelper.ExecuteScalar("select F_NO from DMIS_SYS_DOCTYPE where f_doccat='单文件' and f_packtypeno=" + iPackTypeNo);
            if (obj == null) return false;
            string DocTypeNo,right;
            DocTypeNo = obj.ToString();
            DataTable dt = DBOpt.dbHelper.GetDataTable("select * from dmis_sys_rights where f_catgory='文档' and f_foreignkey=" + DocTypeNo+" and f_roleno in("+roles+")");
            if (dt == null || dt.Rows.Count < 1) return false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                right = dt.Rows[i]["f_access"].ToString();
                if (right[1] == '1') return true;  //第二位是创建的权限
            }
            return false;
        }

        /// <summary>
        /// 创建文档,新的设计思想是每一个环节在DMIS_SYS_DOC中都要插入相关记录
        /// </summary>
        /// <param name="iRecNo">记录编号</param>
        /// <param name="iPackNo">业务编号</param>
        /// <param name="iDocTypeNo">文档类型编号</param>
        /// <param name="sUsr">创建用户</param>
        /// <param name="iDocNo">返回文档编号</param>
        /// <param name="LinkNo">当前环节号</param>
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
            //创建文档记录
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("insert into DMIS_SYS_DOC(F_NO,F_PACKNO,F_DOCTYPENO,F_DOCNAME,F_CREATEMAN,F_CREATEDATE,F_TABLENAME,F_RECNO,f_linkno) VALUES(");
            sql.Append(iDocNo + "," + PackNo + "," + DocTypeNo + ",'" + sName + "','" + sUsr + "','" + sDate + "','" + sTableName + "'," + RecNo + "," + LinkNo + ")");
            return (DBOpt.dbHelper.ExecuteSql(sql.ToString()));
        }

        //汇集环节发送时，查找与它对应的并行环节的所生成的分支是否都到了此汇集环节，否则不能发送
        //而且所有的分支都接单了才可能往下发送。
        public static string InfluxTacheSendCondition(int PackTypeNo,int PackNo,int CurLinkNo,int CurWorkFlowNo)
        {
            string sql;
            bool isFind = false;
            int step = 0;  //本汇集环节的前面步骤可能还有汇集环节
            int preLinkNo,preWorkFlow;
            DataTable dt;
            int nodeType;  //0：一般节点、1：分支节点、2：汇集节点
            preLinkNo = CurLinkNo;
            do
            {
                sql = "select f_startno from dmis_sys_flowline where f_packtypeno=" + PackTypeNo + " and f_endno=" + preLinkNo;
                preLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(sql));
                sql = "select f_flowcat,f_nodetype from dmis_sys_flowlink where f_no=" + preLinkNo;
                dt = DBOpt.dbHelper.GetDataTable(sql);
                nodeType = Convert.ToInt16(dt.Rows[0][1]);
                if(nodeType==1)  //又碰到分支节点
                {
                    if (step == 0)
                    {
                        isFind = true;
                        break;
                    }
                    else
                        step--;
                }
                else if (nodeType == 2) //又碰到汇集节点
                {
                    step++;
                }
            } while (dt.Rows[0][0].ToString()!="0");  //到了起始节点

            if (!isFind) return "error";//"没有找到相应的汇集环节，工作流程配置有误，请联系管理员！";
            //对应的分支节点产生的工作流数,注意要
            preWorkFlow = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_workflow where f_packno=" + PackNo + " and f_flowno=" + preLinkNo + " and f_status='2'"));
            dt = DBOpt.dbHelper.GetDataTable("select distinct f_flowno from dmis_sys_workflow where f_preflowno=" + preWorkFlow + " and (f_status='2' or f_status='1')");
            int effluxCouts = dt.Rows.Count;
            //对应的汇集已经到达的工作流数
            int influxCouts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from dmis_sys_workflow where f_packno=" + PackNo + " and f_flowno="+CurLinkNo+" and f_status='1'"));
            if (effluxCouts > influxCouts)
                return "error";//"前面还有任务没有完成，不能提交！";
            else
            {
                //所有分支已经到过汇集节点，但还有的节点没有接单，还不能发送
                int acceptInfluxCouts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from dmis_sys_workflow where f_packno=" + PackNo + " and f_flowno=" + CurLinkNo + " and f_status='1' and f_working=1"));
                if (influxCouts > acceptInfluxCouts)
                    return "error";//"还有任务没有接单，不能提交！";
                else
                    return "";
            }
                
            
        }

        /// <summary>
        /// 通过业务表所填写的人员来统计工作时间
        /// 这样不用通过分派任务也可以统计时间
        /// </summary>
        /// <param name="PackTypeNo"></param>
        /// <param name="PackNo"></param>
        /// <param name="CurLinkNo"></param>
        /// <param name="TableName"></param>
        /// <param name="TableTID"></param>
        /// <param name="IsToStation">是否下站，取值:是、否</param>
        /// <param name="Members">人员数组列表</param>
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

            //插入统计数据
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
        /// 退回之前，判断是否能退回。
        /// 规则：如果是分支节，如果有一个下级节点退回，但还有下级节点的任务在办，则不允许退回。
        /// 只有当所有分支都退回，则此分支节点才可以退回
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
            //只考虑分支节点
            if (nodeType != 1) return true;   //0:一般节点  1:分支节点  2:汇集节点

            //判断此分支节点的所有生产的在办任务是否存在。
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
        /// 赤几没有这个功能，没有翻译相应内容
        /// 查找当前业务所依赖的业务是否完成，否则不能提交。 
        /// </summary>
        /// <param name="PackNo"></param>
        /// <param name="CurLinkNo"></param>
        /// <param name="ret">返回的未完成的依赖业务类型和任务名称</param>
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
                        ret = dt.Rows[0]["f_packname"].ToString() + "(" + dt.Rows[0]["f_desc"].ToString() + ");未完成,不允许提交!";
                        return true;
                    }
                }
            }
        }

        /// <summary>
        /// 赤几作废
        /// 根据某一流程数据，生成新的流程，数据都一样
        /// 珠海用户设备缺陷降级处理用到，把原先紧急的流程结案，再生成一个重要的缺陷流程。
        /// 由于NewRecNo只能传递一个值，故只能处理业务只包含一个业务表类型的文档，像设备缺陷处理模块
        /// </summary>
        /// <param name="OldPackNo">原先的业务编号</param>
        /// <param name="NewRecNo">新的业务表主键</param>
        /// <param name="NewPackNo">新生成的业务编号</param>
        /// <returns></returns>
        public static bool CopyPack(int OldPackNo, int NewRecNo, ref int NewPackNo)
        {
            uint maxPackNo;
            int failCounts=0;
            DataTable dt;
            maxPackNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_PACK", "F_NO");
            //新增DMIS_SYS_PACK中的数据。
            dt = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_PACK where F_NO=" + OldPackNo);
            if (dt.Rows.Count < 0) return false;
            dt.Rows[0]["F_NO"] = maxPackNo;
            dt.TableName="DMIS_SYS_PACK";
            if (DBOpt.dbHelper.InsertByDataTable(dt, ref failCounts) < 0) return false;

            //新增DMIS_SYS_WORKFLOW和dmis_sys_memberstatus中的数据
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

            //新建DMIS_SYS_DOC中的数据
            //所有环节只对应一个文档，只不用更新f_tablename
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

        //删除某业务
        public static void DeletePack(int PackNo,string Member)
        {
            string [] sqls=new string[4];
            string sql;
            DataTable pack = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_PACK where f_no=" + PackNo);
            DataTable doc = DBOpt.dbHelper.GetDataTable("select f_tablename,f_recno from dmis_sys_doc where f_packno=" + PackNo);

            //删除流程数据
            sql = "delete from DMIS_SYS_PACK where f_no=" + PackNo;
            sqls[0] = sql;
            sql = "delete from DMIS_SYS_WORKFLOW where f_packno=" + PackNo;
            sqls[1] = sql;
            sql = "delete from dmis_sys_memberstatus where f_packno=" + PackNo;
            sqls[2] = sql;
            sql = "delete from dmis_sys_doc where f_packno=" + PackNo;
            sqls[3] = sql;
            DBOpt.dbHelper.ExecuteSqlWithTransaction(sqls);

            //删除业务表数据
            if(doc.Rows.Count>0)
            {
                string[] delYW = new string[doc.Rows.Count];
                for (int i = 0; i < doc.Rows.Count; i++)
                    delYW[i] = "delete from " + doc.Rows[i][0].ToString() + " where TID=" + doc.Rows[i][1].ToString();
                DBOpt.dbHelper.ExecuteSqlWithTransaction(delYW);
            }

            //记录删除日志
            if (pack.Rows.Count > 0)
            {
                uint maxLogTid = DBOpt.dbHelper.GetMaxNum("dmis_sys_wk_opt_history", "tid");
                sql = "insert into dmis_sys_wk_opt_history(tid,packno,opt_type,datem,member_name,reason,F_PACKTYPENO,F_PACKTYPENAME) values(" +
                    maxLogTid + "," + PackNo + ",'删除',TO_DATE('" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "','YYYY-MM-DD HH24:MI'),'" +
                    Member + "','删除业务类型：" + pack.Rows[0]["f_packname"].ToString() + "；任务描述：" + pack.Rows[0]["f_desc"].ToString() + "；创建人："
                    + pack.Rows[0]["f_createman"].ToString() + "；创建时间：" + pack.Rows[0]["f_createdate"].ToString() + "'," + pack.Rows[0]["f_packtypeno"].ToString() + ",'" + pack.Rows[0]["f_packname"].ToString() + "')";
                DBOpt.dbHelper.ExecuteSql(sql);
            }

        }


        /// <summary>
        /// 接单、环节处理都有个时间限制，为了代码方便，把接单和节点办理的最后期限时间求出，然后保存到DMIS_SYS_WORKFLOW中。
        /// DMIS_SYS_MEMBERSTATUS中还没有处理。
        /// </summary>
        /// <param name="start">发送时间（求接单最后期限时间）、接单时间（求办理最后期限时间）</param>
        /// <param name="minutes">工作流配置中所设置的办理分钟数。</param>
        /// <returns>dd-MM-yyyy HH:mm格式的文本</returns>
        public static string GetLastTime(DateTime start, uint minutes)
        {
            if (start == null || minutes <= 0) return "";

            //Web.config中所配置的上班时间段
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
            uint amMinutes, pmMinutes;  //上午和下午上班小时数
            TimeSpan ts = AmEnd - AmStart;
            amMinutes = Convert.ToUInt32(ts.TotalMinutes);
            ts = PmEnd - PmStart;
            pmMinutes = Convert.ToUInt32(ts.TotalMinutes);

            while (minutes > 0)
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select is_holiday from dmis_sys_wk_restday where to_char(res_date,'YYYYMMDD')='" + start.ToString("yyyyMMdd") + "'");
                if (obj == null)   //工作流中的节假日没有找到配置的信息。则周末二天是休息日，其它是工作日
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
                    start = Convert.ToDateTime(start.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);  //第二天从早上上班时间开始算;
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

                    if (Convert.ToInt32(ts.TotalMinutes) >= minutes)  //上午结束
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
                            start = Convert.ToDateTime(start.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);  //第二天从早上上班时间开始算
                            continue;
                        }
                        else   //下午结束
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
                        start = Convert.ToDateTime(start.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);  //第二天从早上上班时间开始算;
                    }
                }
                else  //下班之后
                {
                    start = Convert.ToDateTime(start.AddDays(1).ToString("yyyy-MM-dd ") + ConfigureAmStart);  //第二天从早上上班时间开始算;
                }
            }
            return start.ToString("dd-MM-yyyy HH:mm");
        }

    }  //类
}　//命名空间

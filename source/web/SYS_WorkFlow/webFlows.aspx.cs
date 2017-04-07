using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PlatForm.DBUtility;
using PlatForm.Functions;

public partial class webFlows : System.Web.UI.Page
{
    string strSql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
		if (Request["PackTypeNo"] == null) return; 
		if (Request["PackNo"] == null) return;

        strSql = "SELECT * FROM DMIS_SYS_FLOWLINK WHERE F_PACKTYPENO=" + Request["PackTypeNo"] + " ORDER BY F_FLOWCAT";
        DataTable dtFlow = DBOpt.dbHelper.GetDataTable(strSql);
        strSql = "SELECT F_STARTNO,F_ENDNO FROM DMIS_SYS_FLOWLINE WHERE F_PACKTYPENO=" + Request["PackTypeNo"]
            + " ORDER BY F_STARTNO,F_ENDNO";
        DataTable dtLine = DBOpt.dbHelper.GetDataTable(strSql);
        DataTable dtWork,dtZhuBan;
        DataRow[] rws;
        int iLeft = 0, iTop = 0;
        string sTmp = "", recNos, sStatus, sPreNode;
        int iMinx, iMiny, iMaxx, iMaxy;
        float iScale;
        float iScale1;
        iMinx = FieldToValue.FieldToInt(DBOpt.dbHelper.ExecuteScalar("select min(F_LEFT) from DMIS_SYS_FLOWLINK where F_PACKTYPENO=" + Request["PackTypeNo"]));
        iMiny = FieldToValue.FieldToInt(DBOpt.dbHelper.ExecuteScalar("select min(F_TOP) from DMIS_SYS_FLOWLINK where F_PACKTYPENO=" + Request["PackTypeNo"]));
        iMaxx = FieldToValue.FieldToInt(DBOpt.dbHelper.ExecuteScalar("select max(F_LEFT) from DMIS_SYS_FLOWLINK where F_PACKTYPENO=" + Request["PackTypeNo"]));
        iMaxy = FieldToValue.FieldToInt(DBOpt.dbHelper.ExecuteScalar("select max(F_TOP) from DMIS_SYS_FLOWLINK where F_PACKTYPENO=" + Request["PackTypeNo"]));
        iScale = 1;
        iScale1 = 1;
        if (((iMaxx - iMinx) > 700))
        {
            iScale = 700 / (iMaxx - iMinx);
        }
        if (((iMaxy - iMiny) > 600))
        {
            iScale1 = 600 / (iMaxx - iMinx);
        }
        for (int i = 0; i <= dtFlow.Rows.Count - 1; i++)
        {
            iLeft = FieldToValue.FieldToInt(dtFlow.Rows[i]["F_LEFT"]);//Convert.ToInt32(Convert.ToInt32(FieldToValue.FieldToInt(dtFlow.Rows[i]["F_LEFT"]) - iMinx) * iScale);
            iTop = FieldToValue.FieldToInt(dtFlow.Rows[i]["F_TOP"]);// Convert.ToInt32(Convert.ToInt32(FieldToValue.FieldToInt(dtFlow.Rows[i]["F_TOP"]) - iMiny) * iScale1);
            recNos = "";
            sStatus = FieldToValue.FieldToString(DBOpt.dbHelper.ExecuteScalar("SELECT DISTINCT F_STATUS FROM DMIS_SYS_WORKFLOW WHERE F_FLOWNO=" + dtFlow.Rows[i]["F_NO"] + " AND F_PACKNO=" + Request["PackNo"]));
            strSql = "SELECT F_RECEIVER FROM DMIS_SYS_WORKFLOW  " + " WHERE F_FLOWNO=" + dtFlow.Rows[i]["F_NO"] + " AND F_PACKNO=" + Request["PackNo"];
            dtZhuBan = DBOpt.dbHelper.GetDataTable(strSql);
            if (dtZhuBan.Rows.Count > 0) recNos = dtZhuBan.Rows[0][0].ToString() + ",";
            strSql = "SELECT A.F_RECEIVER FROM DMIS_SYS_MEMBERSTATUS A,DMIS_SYS_WORKFLOW B " + " WHERE A.F_WORKFLOWNO=B.F_NO AND B.F_FLOWNO=" + dtFlow.Rows[i]["F_NO"] + " AND B.F_PACKNO=" + Request["PackNo"];
            dtWork = DBOpt.dbHelper.GetDataTable(strSql);
            if (dtWork.Rows.Count > 0)
            {
                for (int j = 0; j <= dtWork.Rows.Count - 1; j++)
                {
                    recNos += FieldToValue.FieldToString(dtWork.Rows[j][0]) + ",";
                }
            }
            if ((recNos.Length > 0))
            {
                recNos = recNos.Substring(0, recNos.Length - 1);
            }
            rws = dtLine.Select("F_ENDNO=" + dtFlow.Rows[i]["F_NO"]);
            sPreNode = "";
            if ((rws.Length > 0))
            {
                for (int j = 0; j <= rws.Length - 1; j++)
                {
                    sPreNode += FieldToValue.FieldToString(rws[j]["F_STARTNO"]) + "_";
                }
                sPreNode = sPreNode.Substring(0, sPreNode.Length - 1);
            }
            else
            {
                sPreNode = "-1";
            }
            if(Session["Culture"]==null || Session["Culture"].ToString()=="zh-CN")
                sTmp += dtFlow.Rows[i]["F_NO"].ToString() + ":" + FieldToValue.FieldToString(dtFlow.Rows[i]["F_NAME"]) + ":" + iLeft + ":" + iTop + ":" + recNos + ":" + sStatus + ":" + sPreNode + "|";
            else
                sTmp += dtFlow.Rows[i]["F_NO"].ToString() + ":" + FieldToValue.FieldToString(dtFlow.Rows[i]["OTHER_LANGUAGE_DESCR"]) + ":" + iLeft + ":" + iTop + ":" + recNos + ":" + sStatus + ":" + sPreNode + "|";

        }
        if ((sTmp.Length > 0))
        {
            if ((sTmp.Substring(sTmp.Length - 1, 1) == "|"))
            {
                sTmp = sTmp.Substring(0, sTmp.Length - 1);
                NodeData.Value = sTmp;
            }
        }
    }
}

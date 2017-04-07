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
using System.Globalization;

public partial class SYS_WorkFlow_InstanceSuspend : PageBaseList
{
    string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();

            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-") + "01"));
            wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
            FillDropDownList.FillByTable(ref ddlSTATION, "DMIS_SYS_STATION", "NAME");

            ddlPackStatus.Items.Clear();
            ddlPackStatus.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusWorking").ToString(), "1"));
            ddlPackStatus.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusSuspend").ToString(), "3"));
            ddlPackStatus.SelectedIndex = 0;

            ViewState["BaseSql"] = "SELECT distinct A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,"
                     + "B.F_FLOWNAME,B.F_SENDER,B.F_SENDDATE,B.F_RECEIVER,B.f_planday,B.f_receivedate,B.F_FLOWNO,A.F_PACKTYPENO,B.F_NO as F_CurWorkFlowNo,A.F_MSG "
                     + " FROM DMIS_SYS_PACK A,DMIS_SYS_WORKFLOW B ";

            System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();
            BaseCond.Append(" WHERE A.F_NO=B.F_PACKNO AND B.F_STATUS='1' and A.F_STATUS='" + ddlPackStatus.SelectedItem.Value + "'");
            BaseCond.Append(" order by B.F_SENDDATE desc");
            ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();
            GridViewBind();
            ViewState["refreshPage"] = 0;
        }
        else
        {
            if (ViewState["refreshPage"].ToString() != refreshPage.Value)  //要求页面刷新
            {
                btnSearch_Click(null, null);
                ViewState["refreshPage"] = refreshPage.Value;
            }
        }
    }


    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row;
        if (!int.TryParse(e.CommandArgument.ToString(), out row)) return;

        if (e.CommandName == "FlowTable")  //流程
        {
            Response.Redirect("FlowTable.aspx?InstanceID=" + grvList.DataKeys[row].Value.ToString() + "&BackUrl=" + Page.Request.RawUrl);
        }
        else if (e.CommandName == "Deal")   //详细
        {
            object obj;
            int RecNo;             //记录编号
            int PackTypeNo;        //业务类型编号
            int PackNo;            //业务编号
            int CurLinkNo;         //当前环节号
            int CurWorkFlowNo;     //工作流编号 dmis_sys_workflow表中的f_no值
            PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[2]);
            CurLinkNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            CurWorkFlowNo = Convert.ToInt16(grvList.DataKeys[row].Values[3]);
            PackNo = Convert.ToInt16(grvList.DataKeys[row].Value);

            _sql = "select f_recno from DMIS_SYS_DOC where F_PACKNO=" + PackNo + " and F_LINKNO=" + CurLinkNo;
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj == null)
                RecNo = -1;
            else
                RecNo = Convert.ToInt16(obj);

            DataTable docType = DBOpt.dbHelper.GetDataTable("select a.f_no,a.f_formfile,a.f_tablename,a.f_target from dmis_sys_doctype a,DMIS_SYS_WK_LINK_DOCTYPE b where a.f_no=b.F_DOCTYPENO and b.f_packtypeno="
                + PackTypeNo + " and b.F_LINKNO=" + CurLinkNo);
            if (docType == null || docType.Rows.Count < 1)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkNoDoc").ToString());
                return;
            }
            Session["Oper"] = 0;
            Session["sended"] = "0";
            Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                    "&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo + "&PackNo=" + PackNo + "&CurWorkFlowNo=" + CurWorkFlowNo);

        }
        else if (e.CommandName == "Suspend")
        {
            Response.Write("<script language=javascript>");

            if(ddlPackStatus.SelectedItem.Value=="1")
                Response.Write("window.open('InstanceSuspendPopMessage.aspx?InstanceID=" + grvList.DataKeys[row].Value.ToString() + "&OptType=挂起" + "','挂起'" +
                     ",'height=200,width=440,top=100,left=100,scrollbars=no,resizable=yes');");
            else
                Response.Write("window.open('InstanceSuspendPopMessage.aspx?InstanceID=" + grvList.DataKeys[row].Value.ToString() + "&OptType=恢复" + "','恢复'" +
                     ",'height=200,width=440,top=100,left=100,scrollbars=no,resizable=yes');");

            Response.Write("</script>");
        }
    }

    protected override void btnSearch_Click(object sender, EventArgs e)
    {
        if (wdlStart.getTime() > wdlEnd.getTime())
        {
            //JScript.Alert("起始日期不能晚于终止日期！");
            return;
        }

        System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();
        BaseCond.Append(" WHERE A.F_NO=B.F_PACKNO AND B.F_STATUS='1' and A.F_STATUS='" + ddlPackStatus.SelectedItem.Value + "'");

        
        //日期b.F_SENDDATE
        BaseCond.Append(" and TO_DATE(b.F_SENDDATE,'DD-MM-YYYY HH24:MI')>=TO_DATE('" + wdlStart.getTime().ToString("dd-MM-yyyy") + " 00:00','DD-MM-YYYY HH24:MI') and TO_DATE(b.F_SENDDATE,'DD-MM-YYYY HH24:MI')<=TO_DATE('" + wdlEnd.getTime().ToString("dd-MM-yyyy") + " 23:59','DD-MM-YYYY HH24:MI')");

        //厂站
        if (ddlSTATION.SelectedItem != null && ddlSTATION.SelectedItem.Text != "")
            BaseCond.Append(" and a.f_msg='" + ddlSTATION.SelectedItem.Text + "'");

        //模糊查询某个工作任务
        if (txtTaskDesc.Text.Trim() != "")
            BaseCond.Append(" and a.f_desc like '%" + txtTaskDesc.Text + "%'");

        
        //加排序条件
        BaseCond.Append(" order by B.F_SENDDATE desc");
        ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();
        GridViewBind();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btn = (LinkButton)e.Row.Cells[2].Controls[0];
            if (ddlPackStatus.SelectedItem.Value == "1")
            {
                btn.Text = GetGlobalResourceObject("WebGlobalResource", "WkStatusSuspend").ToString();  //挂起
                btn.Attributes.Add("onclick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "WkConfirmSuspend").ToString() + "');");
            }
            else
            {
                btn.Text = GetGlobalResourceObject("WebGlobalResource", "WkRestore").ToString();   //恢复
                btn.Attributes.Add("onclick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "WkConfirmRestore").ToString() + "');");
            }

            if (e.Row.Cells[12].Text == "" || e.Row.Cells[12].Text == "&nbsp;")
                e.Row.Cells[12].Text = "";
            else
            {
                DateTime start;
                CultureInfo ci = new CultureInfo("es-ES");
                try
                {
                    start = DateTime.Parse(e.Row.Cells[12].Text, ci);
                    e.Row.Cells[12].Text = WebWorkFlow.GetConsumeHours(start, DateTime.Now).ToString("f2");
                }
                catch { }

                //e.Row.Cells[12].Text = WebWorkFlow.GetConsumeHours(Convert.ToDateTime(e.Row.Cells[12].Text), DateTime.Now).ToString("f2");
            }
        }
    }

}

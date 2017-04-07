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

public partial class SYS_WorkFlow_InstanceRetake : PageBaseList
{
    private string _sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();

            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.Year.ToString() + "-01-01"));
            wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
            FillDropDownList.FillByTable(ref ddlSTATION, "DMIS_SYS_STATION", "NAME");

            //只有主办者才能抽回
            ViewState["BaseSql"] = "SELECT A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,"
                     + "B.F_FLOWNAME,B.f_receiver,B.F_SENDER,B.F_SENDDATE,B.F_FLOWNO,A.F_PACKTYPENO,B.F_NO as F_CurWorkFlowNo,A.F_MSG,B.f_preflowno "
                     + " FROM DMIS_SYS_PACK A,DMIS_SYS_WORKFLOW B ";
            System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();
            BaseCond.Append(" where A.F_NO=B.F_PACKNO and A.F_STATUS='1' and b.f_working=0 AND b.F_STATUS='1' AND b.f_sender='" + Session["MemberName"] + "'");
            BaseCond.Append(" order by B.f_receivedate desc");
            ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();
            GridViewBind();
        }
    }

 
    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row;
        if (!int.TryParse(e.CommandArgument.ToString(), out row)) return;

        object obj;
        int PackNo;            //业务编号
        int RecNo;             //记录编号
        int DocTypeNo;         //文档类型号
        int PackTypeNo;        //业务类型编号
        int CurLinkNo;         //当前环节号
        string sRight = "";    //文档权限编码
        int CurWorkFlowNo;     //工作流编号 dmis_sys_workflow表中的f_no值
        PackNo = Convert.ToInt16(grvList.DataKeys[row].Values[0]);
        PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[2]);
        CurLinkNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
        CurWorkFlowNo = Convert.ToInt16(grvList.DataKeys[row].Values[3]);

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
        DocTypeNo = Convert.ToInt16(docType.Rows[0][0]);

        if (e.CommandName == "FlowTable")  //流程
        {
            Session["Oper"] = 0;
            Session["sended"] = "0";
            Response.Redirect("FlowTable.aspx?InstanceID=" + PackNo + @"&BackUrl=" + Page.Request.RawUrl);
        }
        else if (e.CommandName == "Deal")   //详细
        {
            Session["Oper"] = 0;
            Session["sended"] = "0";
            Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                    "&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo + "&PackNo=" + PackNo + "&CurWorkFlowNo=" + CurWorkFlowNo);
        }
        else if (e.CommandName == "Retake")   //抽回
        {
            //找回自己刚完成任务时的工作流编号 dmis_sys_workflow表中的f_no值
            obj = DBOpt.dbHelper.ExecuteScalar("SELECT F_PREFLOWNO FROM DMIS_SYS_WORKFLOW WHERE F_PACKNO="
                + PackNo + " AND F_STATUS='1' AND F_WORKING=0 AND F_SENDER='" + Session["MemberName"] + "'");
            if (obj == null)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkNoFindWorkFlow").ToString());  //无法找到工作流编号！
                return;
            }
            string preWorkFlowNo=Convert.ToString(obj);
            if (WebWorkFlow.Retake(preWorkFlowNo, Session["MemberName"].ToString()))
                GridViewBind();
            else
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkFailToWithdrawTaskMessage").ToString());   //抽回任务失败,请联系管理员！
                return;
            }
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
        BaseCond.Append(" where A.F_NO=B.F_PACKNO and A.F_STATUS='1' and b.f_working=0 AND b.F_STATUS='1' AND b.f_sender='" + Session["MemberName"] + "'");

        //日期
        BaseCond.Append(" and TO_DATE(b.F_SENDDATE,'DD-MM-YYYY HH24:MI')>=TO_DATE('" + wdlStart.getTime().ToString("dd-MM-yyyy") + " 00:00','DD-MM-YYYY HH24:MI') and TO_DATE(b.F_SENDDATE,'DD-MM-YYYY HH24:MI')<=TO_DATE('" + wdlEnd.getTime().ToString("dd-MM-yyyy") + " 23:59','DD-MM-YYYY HH24:MI')");

        //厂站
        if (ddlSTATION.SelectedItem != null && ddlSTATION.SelectedItem.Text != "")
            BaseCond.Append(" and a.f_msg='" + ddlSTATION.SelectedItem.Text + "'");

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
            btn.Attributes.Add("onclick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "WkConfirmRetake").ToString() + "');");//确定要抽回此任务？
            btn.Enabled = true;
        }
    }

    /// <summary>
    /// GridView控件的数据绑定过程
    /// </summary>
    protected override void GridViewBind()
    {
        DataTable flow = DBOpt.dbHelper.GetDataTable(ViewState["sql"].ToString());
        if (flow == null)
        {
            tdPageMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "GridViewSqlErrorMessage").ToString();
            return;
        }
        DataColumn[] keys = new DataColumn[2];   //一个人在一个业务中进行了两项工作，有两个完成项，故要有两列作主键
        keys[0] = flow.Columns["F_NO"];
        keys[1] = flow.Columns["F_CurWorkFlowNo"];
        flow.PrimaryKey = keys;

        //某一节点有多个分支,如果某一个分支接收了,则不允许抽回,但上述的SQL语句已经包含了此任务,故要删除
        //如果在上述SQL语句中做到此点,就好多了,不用要此作处理.
        int AllCounts, NoWorkingCounts;
        for (int i = 0; i < flow.Rows.Count; i++)
        {
            AllCounts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from dmis_sys_workflow where f_packno=" + flow.Rows[i]["F_NO"] + " and f_preflowno=" + flow.Rows[i]["f_preflowno"]));
            NoWorkingCounts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from dmis_sys_workflow where f_packno=" + flow.Rows[i]["F_NO"] + " and f_preflowno=" + flow.Rows[i]["f_preflowno"] + " and f_working=0"));
            if (AllCounts == NoWorkingCounts)  //所有都没有接收,则可以抽回
                continue;
            else
                flow.Rows[i].Delete();
        }

        DataView view = flow.DefaultView;
        view.RowStateFilter = DataViewRowState.Unchanged;
        view.Sort = "F_SENDDATE DESC";
        grvRef.DataSource = view;
        grvRef.DataBind();
        grvRef.SelectedIndex = -1;
        if (tdPageMessage != null)
        {
            if (flow.Rows.Count == 0)
                tdPageMessage.InnerText = "";
            else
                tdPageMessage.InnerText = (String)GetGlobalResourceObject("WebGlobalResource", "PageNumber") + (grvRef.PageIndex + 1).ToString() + "/" + grvRef.PageCount.ToString() + " " + (String)GetGlobalResourceObject("WebGlobalResource", "Records") + flow.Rows.Count.ToString();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected override void btnSaveExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=result.xls");
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grvList.AllowPaging = false;
        grvList.AllowSorting = false;
        GridViewBind();
        grvList.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());
        Response.End();

        grvList.AllowPaging = true;
        grvList.AllowSorting = true;
        GridViewBind();
    }

}

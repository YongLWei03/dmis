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

public partial class SYS_WorkFlow_frmPackWorkingTask : PageBaseList
{
    private string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            if (Request["packTypeID"] != null && Request["packTypeID"] != "") ViewState["packTypeID"] = Request["packTypeID"];
            if (Request["linkID"] != null && Request["linkID"] != "") ViewState["linkID"] = Request["linkID"];


            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-") + "01"));
            wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
            FillDropDownList.FillByTable(ref ddlSTATION, "DMIS_SYS_STATION", "NAME");
            FillDropDownList.FillHtmlCombxByTable(ref hcbMember, "dmis_sys_member", "name", "code", "depart_id,order_id");

            ViewState["BaseSql"] = "SELECT distinct A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,A.PLAN_STARTTIME,A.PLAN_ENDTIME,A.SUBPACKNO,"
                     + "B.F_FLOWNAME,B.f_receiver,B.f_receivedate,B.F_SENDER,B.F_SENDDATE,B.F_FLOWNO,A.F_PACKTYPENO,B.F_NO as F_CurWorkFlowNo,A.F_MSG,B.F_LAST_FINISHED_TIME "
                     + " FROM DMIS_SYS_PACK A,DMIS_SYS_WORKFLOW B ";

            System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();

            BaseCond.Append(" WHERE A.F_NO=B.F_PACKNO and A.F_STATUS='1' AND B.F_STATUS='1' AND B.F_WORKING=1");
            
            if (ViewState["packTypeID"] != null)
                BaseCond.Append(" and A.f_packtypeno = " + ViewState["packTypeID"].ToString());

            if (ViewState["linkID"] != null)
                BaseCond.Append(" and b.f_flowno = " + ViewState["linkID"].ToString());

            BaseCond.Append(" order by B.f_receivedate desc");

            ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();

            GridViewBind();
        }
    }

     protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row;
        if (!int.TryParse(e.CommandArgument.ToString(), out row)) return;

        if (e.CommandName == "FlowTable")  //流程
        {
            Response.Redirect("FlowTable.aspx?InstanceID=" + grvList.DataKeys[row].Value.ToString() + @"&BackUrl=" + Page.Request.RawUrl);
        }
        else if (e.CommandName == "Deal")   //办理
        {
            object obj;
            int RecNo;             //记录编号
            int DocTypeNo;         //文档类型号
            int PackTypeNo;        //业务类型编号
            int CurLinkNo;         //当前环节号
            int PackNo;            //当前业务号
            int CurWorkFlowNo;     //工作流编号 dmis_sys_workflow表中的f_no值

            PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[2]);
            PackNo = Convert.ToInt16(grvList.DataKeys[row].Value);
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
                JScript.Alert("无法找到相应的文档！");
                return;
            }
            DocTypeNo = Convert.ToInt16(docType.Rows[0][0]);
            Session["Oper"] = 0;
            Session["sended"] = "0";
            Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo + @"&BackUrl=" + Server.UrlEncode(Page.Request.RawUrl) +
                "&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo + "&PackNo=" + PackNo + "&CurWorkFlowNo=" + CurWorkFlowNo);
        }
    }

    protected override void btnSearch_Click(object sender, EventArgs e)
    {
        if (wdlStart.getTime() > wdlEnd.getTime())
        {
            JScript.Alert("起始日期不能晚于终止日期！");
            return;
        }

        System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();

        //
        BaseCond.Append(" WHERE A.F_NO=B.F_PACKNO and A.F_STATUS='1' AND B.F_STATUS='1' AND B.F_WORKING=1");

        //
        if (hcbMember.SelectedText.Trim() != "")
        {
            BaseCond.Append(" and b.f_receiver='" + hcbMember.SelectedText.Trim() + "'");
        }

        //日期
        BaseCond.Append(" and substr(b.f_receivedate,1,10)>='" + wdlStart.getTime().ToString("yyyy-MM-dd") + "' and substr(b.f_receivedate,1,10)<='" + wdlEnd.getTime().ToString("yyyy-MM-dd") + "' ");

        //厂站
        if (ddlSTATION.SelectedItem != null && ddlSTATION.SelectedItem.Text != "")
        {
            BaseCond.Append(" and a.f_msg='" + ddlSTATION.SelectedItem.Text + "'");
        }
        //选定的某个业务查询
        if (ViewState["packTypeID"] != null)
            BaseCond.Append(" and A.f_packtypeno = " + ViewState["packTypeID"].ToString());

        if (ViewState["linkID"] != null)
            BaseCond.Append(" and b.f_flowno = " + ViewState["linkID"].ToString());

        BaseCond.Append(" order by B.F_SENDDATE desc");

        ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();  //主办者
        GridViewBind();
    }

    /// <summary>
    /// GridView控件的数据绑定过程
    /// </summary>
    protected override void GridViewBind()
    {
        DataTable flow = DBOpt.dbHelper.GetDataTable(ViewState["sql"].ToString());
        if (flow == null)
        {
            tdPageMessage.InnerText = "GridView控件的SQL语句有误！";
            return;
        }
        DataColumn[] keys = new DataColumn[2];   //一个人在一个业务中进行了两项工作，有两个完成项，故要有两列作主键
        keys[0] = flow.Columns["F_NO"];
        keys[1] = flow.Columns["F_CurWorkFlowNo"];
        flow.PrimaryKey = keys;

        DataView view = flow.DefaultView;
        view.Sort = "PLAN_STARTTIME,F_SENDDATE";
        grvRef.DataSource = view;
        grvRef.DataBind();
        grvRef.SelectedIndex = -1;
        if (tdPageMessage != null)
        {
            if (flow.Rows.Count == 0)
                tdPageMessage.InnerText = "";
            else
                tdPageMessage.InnerText = "第" + (grvRef.PageIndex + 1).ToString() + "页/共" + grvRef.PageCount.ToString() + "页 记录共 " + flow.Rows.Count.ToString() + " 条";
        }
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TimeSpan ts;
            DateTime LastFinishedTime;
            if (!(e.Row.Cells[11].Text == "" || e.Row.Cells[11].Text == "&nbsp;"))
            {
                if (!DateTime.TryParse(e.Row.Cells[11].Text, out LastFinishedTime)) return;
                ts = LastFinishedTime - DateTime.Now;
                e.Row.Cells[12].Text = ts.TotalHours.ToString();//("f2")
                if (DateTime.Now > LastFinishedTime)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }

            //查找是否有子流程
            object obj;
            obj = DBOpt.dbHelper.ExecuteScalar("select SUBPACKNO from DMIS_SYS_PACK where F_NO=" + e.Row.Cells[e.Row.Cells.Count - 1].Text);
            if (obj != null && obj.ToString() != "")
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select F_STATUS from DMIS_SYS_PACK where F_NO=" + obj.ToString());
                if (obj != null && obj.ToString() == "正常")
                {
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Blue;
                    e.Row.ToolTip = "还有相应的子流程没有结束,还不允许提交到下一步!";
                }
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected override void btnSaveExcel_Click(object sender, EventArgs e)
    {
        if (grvList.Rows.Count < 1) return;

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

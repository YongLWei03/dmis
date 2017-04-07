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

public partial class SYS_WorkFlow_WorkingTask : PageBaseList
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

            Session["sended"] = null;   //防止第二次添加时,不弹出派工任务窗口(AssignTaskWindow.aspx)

            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.Year.ToString() + "-01-01"));
            wdlEnd.setTime(DateTime.Now);
            //wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")));
            FillDropDownList.FillByTable(ref ddlSTATION, "DMIS_SYS_STATION", "NAME");

            //主办人员
            ViewState["BaseSql"] = "SELECT distinct A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,A.PLAN_STARTTIME,A.PLAN_ENDTIME,A.SUBPACKNO,A.F_STATUS as InstanceStatus,"
                     + "B.F_FLOWNAME,B.f_receiver,B.f_receivedate,B.F_SENDER,B.F_SENDDATE,B.F_FLOWNO,A.F_PACKTYPENO,B.F_NO as F_CurWorkFlowNo,A.F_MSG,B.F_LAST_FINISHED_TIME "
                     + " FROM DMIS_SYS_PACK A,DMIS_SYS_WORKFLOW B ";

            //从办人员
            ViewState["OtherBaseSql"] = "SELECT distinct A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,A.PLAN_STARTTIME,A.PLAN_ENDTIME,A.SUBPACKNO,A.F_STATUS as InstanceStatus,"
                    + "B.F_FLOWNAME,B.f_receiver,B.f_receivedate,B.F_SENDER,B.F_SENDDATE,B.F_FLOWNO,A.F_PACKTYPENO,C.f_workflowno as F_CurWorkFlowNo,A.F_MSG,B.F_LAST_FINISHED_TIME "
                    + " FROM DMIS_SYS_PACK A,DMIS_SYS_WORKFLOW B,DMIS_SYS_MEMBERSTATUS C ";


            System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();
            System.Text.StringBuilder OtherCond = new System.Text.StringBuilder();

            BaseCond.Append(" WHERE A.F_NO=B.F_PACKNO and (A.F_STATUS='1' or A.F_STATUS='暂缓') and B.F_RECEIVER='" + Session["MemberName"] + "' AND B.F_STATUS='1' AND B.F_WORKING=1");
            OtherCond.Append(" WHERE A.F_NO=B.F_PACKNO AND B.F_NO=C.F_WORKFLOWNO and A.F_NO=C.F_PACKNO and (A.F_STATUS='1' or A.F_STATUS='暂缓') "
                    + " AND C.F_RECEIVER='" + Session["MemberName"] + "' AND C.F_STATUS='1'");  //AND B.F_WORKING=1

            BaseCond.Append(" order by B.f_receivedate desc");
            OtherCond.Append(" order by B.f_receivedate desc");

            ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();
            ViewState["OtherSql"] = ViewState["OtherBaseSql"] + OtherCond.ToString();   //从办者

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
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "GridViewSqlErrorMessage").ToString());
                return;
            }
            DocTypeNo = Convert.ToInt16(docType.Rows[0][0]);
            DataTable rights = WebWorkFlow.DocTypeRights(DocTypeNo, Session["RoleIDs"].ToString()); ;
            if (rights != null && rights.Rows.Count>0)
            {
                if(grvList.Rows[row].Cells[6].Text==Session["MemberName"].ToString())  //是主办人，可以修改
                    Session["Oper"] = 1;
                else
                    Session["Oper"] = 0;     //从办人员，不能修改。

                Session["sended"] = "0";
                Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                    "&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo + "&PackNo=" + PackNo + "&CurWorkFlowNo=" + CurWorkFlowNo);
            }
            else
            {
                Response.Write("<script language=javascript>alert('" + GetGlobalResourceObject("WebGlobalResource", "WkNoRightToRead").ToString() + "')</script>");  //对不起！你无权操作本文档
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
        System.Text.StringBuilder OtherCond = new System.Text.StringBuilder();

        //主办人员DMIS_SYS_WORKFLOW  从办人员DMIS_SYS_MEMBERSTATUS,在办任务不用考虑处理实体的情况,只考虑登录本人
        BaseCond.Append(" WHERE A.F_NO=B.F_PACKNO");
        OtherCond.Append(" WHERE A.F_NO=B.F_PACKNO AND B.F_NO=C.F_WORKFLOWNO and A.F_NO=C.F_PACKNO");

        BaseCond.Append(" and B.F_RECEIVER='" + Session["MemberName"] + "' AND B.F_STATUS='1' AND B.F_WORKING=1");
        OtherCond.Append(" AND C.F_RECEIVER='" + Session["MemberName"] + "' AND C.F_STATUS='1' AND B.F_WORKING=1");

        //日期
        BaseCond.Append(" and TO_DATE(b.f_receivedate,'DD-MM-YYYY HH24:MI')>=TO_DATE('" + wdlStart.getTime().ToString("dd-MM-yyyy") + " 00:00','DD-MM-YYYY HH24:MI') and TO_DATE(b.f_receivedate,'DD-MM-YYYY HH24:MI')<=TO_DATE('" + wdlEnd.getTime().ToString("dd-MM-yyyy") + " 23:59','DD-MM-YYYY HH24:MI')");
        OtherCond.Append(" and TO_DATE(c.f_receivedate,'DD-MM-YYYY HH24:MI')>=TO_DATE('" + wdlStart.getTime().ToString("dd-MM-yyyy") + " 00:00','DD-MM-YYYY HH24:MI') and TO_DATE(c.f_receivedate,'DD-MM-YYYY HH24:MI')<=TO_DATE('" + wdlEnd.getTime().ToString("dd-MM-yyyy") + " 23:59','DD-MM-YYYY HH24:MI')");

        //厂站
        if (ddlSTATION.SelectedItem != null && ddlSTATION.SelectedItem.Text != "")
        {
            BaseCond.Append(" and a.f_msg='" + ddlSTATION.SelectedItem.Text + "'");
            OtherCond.Append(" and a.f_msg='" + ddlSTATION.SelectedItem.Text + "'");
        }

        BaseCond.Append(" order by B.F_SENDDATE desc");
        OtherCond.Append(" order by B.F_SENDDATE desc");

        ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();  //主办者
        ViewState["OtherSql"] = ViewState["OtherBaseSql"] + OtherCond.ToString();   //从办者
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
            tdPageMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "GridViewSqlErrorMessage").ToString();
            return;
        }
        DataColumn[] keys = new DataColumn[2];   //一个人在一个业务中进行了两项工作，有两个完成项，故要有两列作主键
        keys[0] = flow.Columns["F_NO"];
        keys[1] = flow.Columns["F_CurWorkFlowNo"];
        flow.PrimaryKey = keys;

        //2009-3-12  用户要求不要显示从办人员
        //DataTable flowOther = DBOpt.dbHelper.GetDataTable(ViewState["OtherSql"].ToString());
        //object[] cols = new object[2];
        //for (int i = 0; i < flowOther.Rows.Count; i++)
        //{
        //    cols[0] = flowOther.Rows[i]["F_NO"];
        //    cols[1] = flowOther.Rows[i]["F_CurWorkFlowNo"];
        //    if (flow.Rows.Contains(cols)) continue;
        //    DataRow drow = flow.NewRow();
        //    for (int j = 0; j < flowOther.Columns.Count; j++)
        //    {
        //        drow[j] = flowOther.Rows[i][j];
        //    }
        //    flow.Rows.Add(drow);
        //}

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
                tdPageMessage.InnerText = (String)GetGlobalResourceObject("WebGlobalResource", "PageNumber")+ (grvRef.PageIndex + 1).ToString() + "/" + grvRef.PageCount.ToString() + " " +(String)GetGlobalResourceObject("WebGlobalResource", "Records") + flow.Rows.Count.ToString();
        }
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[e.Row.Cells.Count - 2].Text = GetGlobalResourceObject("WebGlobalResource", "WkStatusWorking").ToString(); 
            TimeSpan ts;
            DateTime LastFinishedTime;
            if (!(e.Row.Cells[11].Text == "" || e.Row.Cells[11].Text == "&nbsp;"))
            {
                if (!DateTime.TryParse(e.Row.Cells[11].Text, out LastFinishedTime)) return;
                ts = LastFinishedTime - DateTime.Now;
                e.Row.Cells[12].Text = ts.TotalHours.ToString("f2");
                if (DateTime.Now > LastFinishedTime)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
            
            //查找是否有子流程
            object obj;
            obj=DBOpt.dbHelper.ExecuteScalar("select SUBPACKNO from DMIS_SYS_PACK where F_NO="+e.Row.Cells[e.Row.Cells.Count-1].Text); 
            if(obj!=null && obj.ToString()!="")
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select F_STATUS from DMIS_SYS_PACK where F_NO=" + obj.ToString());
                if (obj != null && obj.ToString() == "1")
                {
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Blue;
                    //e.Row.ToolTip = "还有相应的子流程没有结束,还不允许提交到下一步!";   //赤几的项目目前没有子流程的功能。
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

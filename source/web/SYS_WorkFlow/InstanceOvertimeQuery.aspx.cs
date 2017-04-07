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

public partial class SYS_WorkFlow_InstanceOvertimeQuery : PageBaseList
{
    private string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.Year.ToString() + "-01-01"));
            wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
            initPackType();

            ViewState["BaseSql"] = "select * from DMIS_SYS_PACK A";
            ViewState["sql"] = ViewState["BaseSql"].ToString() + " WHERE A.F_STATUS='结案' and A.f_packtypeno in(" + ViewState["packTypeIds"].ToString() + ") order by A.f_archivedate desc";
            GridViewBind();
        }
    }

    private void initPackType()
    {
        //没有加权限,都可以浏览,通过菜单的可见性来设置
        _sql = "select f_no,f_name from DMIS_SYS_PACKTYPE";
        DataTable packType = DBOpt.dbHelper.GetDataTable(_sql);
        System.Text.StringBuilder packTypeId = new System.Text.StringBuilder();
        for (int i = 0; i < packType.Rows.Count; i++)
        {
            packTypeId.Append(packType.Rows[i][0].ToString() + ",");
        }
        ViewState["packTypeIds"] = packTypeId.Remove(packTypeId.Length - 1, 1).ToString();  //只能处理有权限的业务

        CheckBox ckb;
        dlsPackType.DataSource = packType;
        dlsPackType.DataBind();
        foreach (DataListItem item in dlsPackType.Items)
        {
            ckb = (CheckBox)item.Controls[1];
            ckb.Checked = true;
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
            int PackTypeNo;        //业务类型编号
            int CurLinkNo;         //当前环节号
            int PackNo;            //当前业务号
            int CurWorkFlowNo;     //工作流编号 dmis_sys_workflow表中的f_no值

            PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            PackNo = Convert.ToInt16(grvList.DataKeys[row].Value);
            //当前节点是最后一个节点
            CurLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packtypeno=" + PackTypeNo + " and f_flowcat=2"));
            //最后一个环节的工作流号
            CurWorkFlowNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packno=" + PackNo + " and f_flowno=" + CurLinkNo));
            //最后一个环节对应的业务表中的记录号
            _sql = "select f_recno from DMIS_SYS_DOC where F_PACKNO=" + PackNo + " and f_linkno=" + CurLinkNo;
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj == null)
            {
                JScript.Alert("无法找到业务表的记录编号！");
                return;
            }
            RecNo = Convert.ToInt16(obj);

            DataTable docType = DBOpt.dbHelper.GetDataTable("select a.f_no,a.f_formfile,a.f_tablename,a.f_target from dmis_sys_doctype a,DMIS_SYS_WK_LINK_DOCTYPE b where a.f_no=b.F_DOCTYPENO and b.f_packtypeno="
                    + PackTypeNo + " and b.F_LINKNO=" + CurLinkNo);
            if (docType == null || docType.Rows.Count < 1)
            {
                JScript.Alert("无法找到相应的文档！");
                return;
            }
            Session["sended"] = "0";
            Session["Oper"] = "0"; //不允许修改数据
            Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                "&PackTypeNo=" + PackTypeNo + "&PackNo=" + PackNo + "&CurLinkNo=" + CurLinkNo + "&CurWorkFlowNo=" + CurWorkFlowNo);
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (wdlStart.getTime() > wdlEnd.getTime())
        {
            JScript.Alert("起始日期不能晚于终止日期！");
            return;
        }

        //选定的某个业务查询
        System.Text.StringBuilder packType = new System.Text.StringBuilder();
        CheckBox ckb;
        foreach (DataListItem item in dlsPackType.Items)
        {
            ckb = (CheckBox)item.Controls[1];
            if (ckb.Checked) packType.Append(ckb.ToolTip + ",");
        }
        if (packType.Length < 1)
        {
            JScript.Alert("查询时至少要选择一个业务类别！");
            return;
        }
        ViewState["packTypeIds"] = packType.Remove(packType.Length - 1, 1);

        System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();

        BaseCond.Append(" WHERE A.F_STATUS='结案' ");

        //日期范围
        BaseCond.Append(" and substr(a.f_archivedate,1,10)>='" + wdlStart.getTime().ToString("yyyy-MM-dd") + "' and substr(a.f_archivedate,1,10)<='" + wdlEnd.getTime().ToString("yyyy-MM-dd") + "' ");

        //选定的某个业务查询
        BaseCond.Append(" and A.f_packtypeno in(" + ViewState["packTypeIds"].ToString() + ")");

        //加排序条件
        BaseCond.Append(" order by a.f_archivedate desc");

        ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();
        GridViewBind();
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TimeSpan ts;
            double sj = 0, jh = 0;        //实际所费时间和计划所费时间
            DateTime start, end;          //创建时间和归档时间
            DateTime jh_start, jh_end;   //计划开始时间和计划结束时间
            if (!(e.Row.Cells[6].Text == "" || e.Row.Cells[6].Text == "&nbsp;") && !(e.Row.Cells[7].Text == "" || e.Row.Cells[7].Text == "&nbsp;"))
            {
                DateTime.TryParse(e.Row.Cells[6].Text, out start);
                DateTime.TryParse(e.Row.Cells[7].Text, out end);
                if (start != null && end != null)
                {
                    ts = end - start;
                    sj = ts.TotalHours;
                    e.Row.Cells[8].Text = sj.ToString("f2");
                }
            }
            if (!(e.Row.Cells[9].Text == "" || e.Row.Cells[9].Text == "&nbsp;") && !(e.Row.Cells[10].Text == "" || e.Row.Cells[10].Text == "&nbsp;"))
            {
                DateTime.TryParse(e.Row.Cells[9].Text, out jh_start);
                DateTime.TryParse(e.Row.Cells[10].Text, out jh_end);
                if (jh_start != null && jh_end != null)
                {
                    ts = jh_end - jh_start;
                    jh = ts.TotalHours;
                    e.Row.Cells[11].Text = jh.ToString("f2");
                }
            }
            if (jh < sj && jh != 0 && sj != 0)   //超时
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected override void GridViewBind()
    {
        DataTable flow = DBOpt.dbHelper.GetDataTable(ViewState["sql"].ToString());
        if (flow == null)
        {
            tdPageMessage.InnerText = "GridView控件的SQL语句有误！";
            return;
        }

        TimeSpan ts;
        double sj = 0, jh = 0;        //实际所费时间和计划所费时间
        DateTime start, end;          //创建时间和归档时间
        DateTime jh_start, jh_end;   //计划开始时间和计划结束时间

        for (int i = 0; i < flow.Rows.Count; i++)
        {
            if (flow.Rows[i]["f_createdate"] == Convert.DBNull || flow.Rows[i]["f_archivedate"] == Convert.DBNull
                || flow.Rows[i]["plan_starttime"] == Convert.DBNull || flow.Rows[i]["plan_endtime"] == Convert.DBNull)
            {
                flow.Rows.Remove(flow.Rows[i]);
                i--;
                continue;
            }
            if (!DateTime.TryParse(flow.Rows[i]["f_createdate"].ToString(), out start) ||
                !DateTime.TryParse(flow.Rows[i]["f_archivedate"].ToString(), out end) ||
                !DateTime.TryParse(flow.Rows[i]["plan_starttime"].ToString(), out jh_start) ||
                !DateTime.TryParse(flow.Rows[i]["plan_endtime"].ToString(), out jh_end))
            {
                flow.Rows.Remove(flow.Rows[i]);
                i--;
                continue;
            }

            ts = end - start;
            sj = ts.TotalHours;
            ts = jh_end - jh_start;
            jh = ts.TotalHours;
            if (sj < jh)
            {
                flow.Rows.Remove(flow.Rows[i]);
                i--;
            }
        }

        DataView view = flow.DefaultView;
        view.Sort = "f_archivedate";
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

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

//归档业务查询
public partial class SYS_WorkFlow_FinishedTask : PageBaseList
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

            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy") + "-01-01"));
            wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
            FillDropDownList.FillByTable(ref ddlSTATION, "DMIS_SYS_STATION", "NAME");

            ViewState["BaseSql"] = "select * from DMIS_SYS_PACK A";
            ViewState["sql"] = ViewState["BaseSql"].ToString() + " WHERE A.F_STATUS='2' order by A.f_archivedate desc";
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
            int PackTypeNo;        //业务类型编号
            int CurLinkNo;         //当前环节号
            int PackNo;            //当前业务号
            int CurWorkFlowNo;     //工作流编号 dmis_sys_workflow表中的f_no值

            PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            PackNo = Convert.ToInt16(grvList.DataKeys[row].Value);
            //当前节点是最后一个节点
            //找最后一个环节不对,是因为设备缺陷有降级处理功能,没到最后一步就归档了,故下述语句不对
            //CurLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packtypeno=" + PackTypeNo + " and f_flowcat=2"));
            //最后一个环节的工作流号,也就是最大的环节号
            //CurWorkFlowNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select max(f_no) from dmis_sys_flowlink where f_packno=" + PackNo + " and f_flowno=" + CurLinkNo));
            CurWorkFlowNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select max(f_no) from dmis_sys_workflow where f_packno=" + PackNo ));
            CurLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_flowno from dmis_sys_workflow where f_no=" + CurWorkFlowNo));
            //最后一个环节对应的业务表中的记录号
            _sql = "select f_recno from DMIS_SYS_DOC where F_PACKNO=" + PackNo + " and f_linkno=" + CurLinkNo;
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj == null)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkNoDoc").ToString());
                return;
            }
            RecNo = Convert.ToInt16(obj);
            
            DataTable docType = DBOpt.dbHelper.GetDataTable("select a.f_no,a.f_formfile,a.f_tablename,a.f_target from dmis_sys_doctype a,DMIS_SYS_WK_LINK_DOCTYPE b where a.f_no=b.F_DOCTYPENO and b.f_packtypeno="
                    + PackTypeNo + " and b.F_LINKNO=" + CurLinkNo);
            if (docType == null || docType.Rows.Count < 1)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkNoDoc").ToString());
                return;
            }
            Session["sended"] = "0"; 
            Session["Oper"] = "0"; //不允许修改数据
            Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                "&PackTypeNo=" + PackTypeNo + "&PackNo=" + PackNo + "&CurLinkNo=" + CurLinkNo + "&CurWorkFlowNo=" + CurWorkFlowNo);
        }
    }

    protected override void btnSearch_Click(object sender, EventArgs e)
    {
        if (wdlStart.getTime() > wdlEnd.getTime())
            return;

        System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();
        System.Text.StringBuilder members = new System.Text.StringBuilder();

        BaseCond.Append(" WHERE A.F_STATUS='2' ");

        //日期范围
        BaseCond.Append(" and TO_DATE(a.f_archivedate,'DD-MM-YYYY HH24:MI')>=TO_DATE('" + wdlStart.getTime().ToString("dd-MM-yyyy") + " 00:00','DD-MM-YYYY HH24:MI') and TO_DATE(a.f_archivedate,'DD-MM-YYYY HH24:MI')<=TO_DATE('" + wdlEnd.getTime().ToString("dd-MM-yyyy") + " 23:59','DD-MM-YYYY HH24:MI') ");

        //厂站
        if (ddlSTATION.SelectedItem != null && ddlSTATION.SelectedItem.Text != "")
            BaseCond.Append(" and a.f_msg='" + ddlSTATION.SelectedItem.Text + "'");

        //模糊查询某个工作任务
        if (txtTaskDesc.Text.Trim() != "")
            BaseCond.Append(" and a.f_desc like '%" + txtTaskDesc.Text + "%'");

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
            double sj=0, jh=0;               //实际所费时间和计划所费时间
            DateTime start,end;          //创建时间和归档时间
            DateTime jh_start, jh_end;   //计划开始时间和计划结束时间
            if (!(e.Row.Cells[6].Text == "" || e.Row.Cells[6].Text == "&nbsp;") && !(e.Row.Cells[7].Text == "" || e.Row.Cells[7].Text == "&nbsp;"))
            {
                DateTime.TryParse(e.Row.Cells[6].Text, out start);
                DateTime.TryParse(e.Row.Cells[7].Text, out end);
                if (start != null && end != null)
                {
                    ts = end - start;
                    sj = ts.TotalHours;
                    try
                    {
                        e.Row.Cells[8].Text = sj.ToString("f2");
                    }
                    catch
                    { 
                        e.Row.Cells[8].Text = "..."; 
                    }
                }
            }
            if (!(e.Row.Cells[9].Text == "" || e.Row.Cells[9].Text == "&nbsp;") && !(e.Row.Cells[10].Text == "" || e.Row.Cells[10].Text == "&nbsp;"))
            {
                DateTime.TryParse(e.Row.Cells[9].Text , out jh_start);
                DateTime.TryParse(e.Row.Cells[10].Text, out jh_end);
                if (jh_start != null && jh_end != null)
                {
                    ts = jh_end - jh_start;
                    jh = ts.TotalHours;
                    try
                    {
                        e.Row.Cells[11].Text = jh.ToString("f2");
                    }
                    catch
                    {
                        e.Row.Cells[11].Text = "....";
                    }
                }
            }
            if (jh < sj && jh!=0 && sj!=0)   //超时
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
            }
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

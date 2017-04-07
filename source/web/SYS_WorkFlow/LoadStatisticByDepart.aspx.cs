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

public partial class SYS_WorkFlow_LoadStatisticByDepart : PageBaseList
{
    private string _sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            _sql = "select id,name from dmis_sys_depart order by order_id";
            DataTable depart = DBOpt.dbHelper.GetDataTable(_sql);
            ddlDepart.DataTextField = "name";
            ddlDepart.DataValueField = "id";
            ddlDepart.DataSource = depart;
            ddlDepart.DataBind();
            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.Year.ToString() + "-01-01"));
            wdlEnd.setTime(DateTime.Now);
        }
    }

     //统计
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DateTime startDate, endDate;
        startDate = wdlStart.getTime();
        endDate = wdlEnd.getTime();
        if (startDate > endDate)
        {
            JScript.Alert("开始日期不能大于结束日期！");
            return;
        }
        if (ddlDepart.SelectedItem == null)
        {
            JScript.Alert("请先选择某一部门！");
            return;
        }

        _sql = "select code,name from dmis_sys_member where depart_id="+ddlDepart.SelectedValue+" order by ORDER_ID";
        DataTable mem = DBOpt.dbHelper.GetDataTable(_sql);
        DataTable tasks;
        DataRow drow;
        DataTable result=null;

        DataColumn dcDepart = new DataColumn("Depart");
        dcDepart.DataType = System.Type.GetType("System.String");
        DataColumn dcMember = new DataColumn("Member");
        dcMember.DataType = System.Type.GetType("System.String");
        
        for (int i = 0; i < mem.Rows.Count; i++)
        {
            _sql = "select a.F_NAME as F_PACKNAME,sum(c.F_WORKDAY) as totalHours,count(*) as COUNTS " +
            " from DMIS_SYS_PACKTYPE a,DMIS_SYS_PACK b,DMIS_SYS_WORKFLOW c where " +
             " a.f_no=b.F_PACKTYPENO and b.f_no=c.F_PACKNO and c.F_RECEIVER='" + mem.Rows[i][1].ToString() + "' group by a.F_NAME";
            tasks = DBOpt.dbHelper.GetDataTable(_sql);
            if (result == null )
            {
                result = tasks.Clone();
                result.Columns.Add(dcDepart);
                result.Columns.Add(dcMember);
            }

            for (int j = 0; j < tasks.Rows.Count; j++)
            {
                drow = result.NewRow();
                drow["Depart"] = ddlDepart.SelectedItem.Text;
                drow["Member"] = mem.Rows[i][1];
                drow["F_PACKNAME"] = tasks.Rows[j][0];
                drow["totalHours"] = tasks.Rows[j][1];
                drow["COUNTS"] = tasks.Rows[j][2];
                result.Rows.Add(drow);
            }
        }
        ViewState["dt"] = result;
        GridViewBind();
    }

    protected override void GridViewBind()
    {
        if (ViewState["dt"] == null) return;

        grvList.DataSource = (DataTable)ViewState["dt"];
        grvList.DataBind();
        grvList.SelectedIndex = -1;
        if (tdPageMessage != null)
        {
            if (((DataTable)ViewState["dt"]).Rows.Count == 0)
                tdPageMessage.InnerText = "";
            else
                tdPageMessage.InnerText = "第" + (grvList.PageIndex + 1).ToString() + "页/共" + grvList.PageCount.ToString() + "页 记录共 " + ((DataTable)ViewState["dt"]).Rows.Count.ToString() + " 条";
        }
    }

    protected override void btnPrint_Click(object sender, EventArgs e)
    {
        if (Session["ReportId"] == null || Session["ReportId"].ToString().Trim() == "")
        {
            Response.Write("<script language=javascript> alert('此模块没有设置报表打印功能！')</script");
            return;
        }
        // JScript.OpenWindow("../frmCellReportDisplay.aspx?ReportID=" + Session["ReportId"].ToString() + "&Values=" + uMonth.Month, "报表打印", "resizable=yes,scrollbars=no,status=yes,width=600px,height=500px,left=100px,top=10px");
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

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
           // e.Row.Cells[1].Text = "共处理任务：" + rows.ToString() + " 项，花费时间：" + totalHours.ToString("f2") + " 小时。";
        }
    }
}

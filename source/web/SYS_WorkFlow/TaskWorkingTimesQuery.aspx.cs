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

public partial class SYS_WorkFlow_TaskWorkingTimesQuery : PageBaseList
{
    private string _sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            _sql = "select code,name from dmis_sys_member order by depart_id,order_id";
            DataTable mem = DBOpt.dbHelper.GetDataTable(_sql);
            DataRow row = mem.NewRow();
            row[0] = "全部";
            row[1] = "全部";
            mem.Rows.InsertAt(row, 0);
            ddlMember.DataSource = mem;
            ddlMember.DataTextField = "name";
            ddlMember.DataValueField = "code";
            ddlMember.DataBind();

            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.Year.ToString() + "-01-01"));
            wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));

            ViewState["BaseSql"] = "select * from DMIS_SYS_WK_TASK_WORKINGTIMES";
            ViewState["BaseQuery"] = " to_char(STARTTIME,'YYYYMMDD')>='" + wdlStart.getTime().ToString("yyyyMMdd") + "' and to_char(STARTTIME,'YYYYMMDD')<='" + wdlEnd.getTime().ToString("yyyyMMdd") + "'";
            if (Session["Orders"] == null)   //平台中没有设置排序条件
                ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"];
            else
                ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["Orders"];
            GridViewBind();
            Session["CustomOrder"] = null;
        }
        else
        {
            //自定义排序页面关闭后,刷新GridView
            if (Session["CustomOrder"] != null && ViewState["sql"].ToString().IndexOf(Session["CustomOrder"].ToString()) < 0)
            {
                if (ViewState["BaseQuery"] != null)  //页面自带查询条件
                    ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["CustomOrder"];
                else   //无查询条件
                    ViewState["sql"] = ViewState["BaseSql"] + " order by " + Session["CustomOrder"];
                GridViewBind();
                return;
            }
        }
    }
    protected override void btnSearch_Click(object sender, EventArgs e)
    {
        if (wdlStart.getTime() > wdlEnd.getTime())
        {
            JScript.Alert("起始日期不能晚于终止日期！");
            return;
        }
        System.Text.StringBuilder conditions = new System.Text.StringBuilder();
        conditions.Append(" to_char(STARTTIME,'YYYYMMDD')>='" + wdlStart.getTime().ToString("yyyyMMdd") +
            "' and to_char(STARTTIME,'YYYYMMDD')<='" + wdlEnd.getTime().ToString("yyyyMMdd") + "' ");
        if (ddlMember.Text != "全部")
            conditions.Append(" and F_MEMEBER_NAME='" + ddlMember.Text + "'");

        if (Session["Orders"] != null)   //平台中没有设置排序条件
            ViewState["sql"] = ViewState["BaseSql"] + " where " + conditions.ToString();
        else
            ViewState["sql"] = ViewState["BaseSql"] + " where " + conditions.ToString() + " order by " + Session["Orders"];

        GridViewBind();
    }


    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row;
        if (!int.TryParse(e.CommandArgument.ToString(), out row)) return;

        if (e.CommandName == "Deal")   //办理
        {
            String TableName;
            int RecNo;             //记录编号
            int PackTypeNo;        //业务类型编号
            string url;

            //从业务表查询界面打开，有PackTypeNo、TableName、RecNo
            PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            _sql = "select f_tablename,f_recno,f_doctypeno from DMIS_SYS_DOC where F_PACKNO=" + grvList.DataKeys[row].Values[2].ToString() + " and f_linkno=" + grvList.DataKeys[row].Values[3].ToString();
            DataTable doc = DBOpt.dbHelper.GetDataTable(_sql);

            if (doc == null || doc.Rows.Count < 1)
            {
                JScript.Alert("无法找到相应的文档！");
                return;
            }
            TableName = doc.Rows[0][0].ToString();
            RecNo = Convert.ToInt16(doc.Rows[0][1]);
            url = DBOpt.dbHelper.ExecuteScalar("select f_formfile from dmis_sys_doctype where f_no=" + doc.Rows[0][2].ToString()).ToString();
            Session["Oper"] = 0;
            Session["sended"] = 0;
            Response.Redirect(url + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                "&PackTypeNo=" + PackTypeNo + "&TableName=" + TableName);
        }
        else if(e.CommandName == "IsToStation")
        {

        }
    }

    protected override void btnSort_Click(object sender, EventArgs e)
    {
        if (Session["MainTableId"] == null || Session["MainTableId"].ToString().Trim() == "")
        {
            JScript.Alert(Page, "此模块没有设置数据库表参数，无法排序！");
            return;
        }
        JScript.OpenWindow("../SYS_Common/frmSetSort.aspx?MainTableId=" + Session["MainTableId"].ToString(), "排序定义窗口", "resizable=yes,scrollbars=no,status=yes,width=600px,height=500px,left=100px,top=10px");
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        float hours;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (float.TryParse(e.Row.Cells[e.Row.Cells.Count - 2].Text, out hours))
            {
                e.Row.Cells[e.Row.Cells.Count - 2].Text = hours.ToString("f2");
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

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

public partial class SYS_WorkFlow_OptLogSearch : PageBaseList
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

            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-")+"01"));
            wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") ));

            ViewState["BaseSql"] = "select * from DMIS_SYS_WK_OPT_HISTORY";
            ViewState["BaseQuery"] = "1=1";
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
            return;

        System.Text.StringBuilder conditions = new System.Text.StringBuilder();
        conditions.Append(" WHERE ");

        conditions.Append(" to_char(DATEM,'YYYYMMDD')>='" + wdlStart.getTime().ToString("yyyyMMdd") +
            "' and to_char(DATEM,'YYYYMMDD')<='" + wdlEnd.getTime().ToString("yyyyMMdd")+"' ");
        if (ddlOptType.Text != "全部")
            conditions.Append(" and OPT_TYPE='" + ddlOptType.Text + "'");

        if (Session["Orders"] == null)   //平台中没有设置排序条件
            ViewState["sql"] = ViewState["BaseSql"]  + conditions.ToString();
        else
            ViewState["sql"] = ViewState["BaseSql"]  + conditions.ToString() + " order by " + Session["Orders"];

        GridViewBind();
    }
 
    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row;
        if (!int.TryParse(e.CommandArgument.ToString(), out row)) return;

        if (grvList.Rows[row].Cells[2].Text == "删除")
        {
            //JScript.Alert("已经删除的业务不允许查看数据或流程！");
            return;
        }
        if (e.CommandName == "FlowTable")  //流程
        {
            Response.Redirect("FlowTable.aspx?InstanceID=" + grvList.DataKeys[row].Values[1].ToString() + @"&BackUrl=" + Page.Request.RawUrl);
        }
        else if (e.CommandName == "Deal")   //办理
        {
            object obj;
            int RecNo;             //记录编号
            int PackTypeNo;        //业务类型编号
            int PackNo;
            int maxWorkFlowNo;
            int LinkNo;
            string TableName;
            string url;

            PackNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            _sql = "select f_packtypeno from dmis_sys_pack where f_no=" + PackNo;
            PackTypeNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql));
            //找当前业务的最大工作流编号
            _sql = "select max(f_no) from dmis_sys_workflow where f_packno=" + PackNo;
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj is System.DBNull)
            {
                //JScript.Alert("无法找到最大的工作流号！");
                return;
            }
            maxWorkFlowNo = Convert.ToInt16(obj);

            _sql = "select f_flowno from dmis_sys_workflow where f_no=" + maxWorkFlowNo;
            LinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql));

            _sql = "select f_tablename,f_recno,f_doctypeno from DMIS_SYS_DOC where F_PACKNO=" + PackNo + " and f_linkno=" + LinkNo;
            DataTable doc = DBOpt.dbHelper.GetDataTable(_sql);
            if (doc == null || doc.Rows.Count < 1)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkNoDoc").ToString());
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

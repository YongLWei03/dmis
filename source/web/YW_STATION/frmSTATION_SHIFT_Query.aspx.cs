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

/*赤几  变电站值班记录查询*/

public partial class YW_STATION_frmSTATION_SHIFT_Query : PageBaseList
{
    private string _sql;
    private DataTable _dt;
    private object obj;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();
            lblFuncName.Text = Session["FuncName"].ToString();
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM") + "-01"));
            wdlEnd.setTime(DateTime.Now);
            initStation();
            ViewState["BaseSql"] = "select * from " + Session["TableName"] + "";
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
            }
        }
    }

    private void initStation()
    {
        _sql = "select ID,NAME from DMIS_SYS_DEPART where SUPERIOR_ID=5 order by ORDER_ID";   //取平台维护中变电站的所有列表。
        _dt = DBOpt.dbHelper.GetDataTable(_sql);
        DataRow r = _dt.NewRow();
        r[0] = 0;
        r[1] = GetGlobalResourceObject("WebGlobalResource", "All");
        _dt.Rows.InsertAt(r, 0);
        ddlStation.DataValueField = "ID";
        ddlStation.DataTextField = "NAME";
        ddlStation.DataSource = _dt;
        ddlStation.DataBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DateTime start, end;
        start = wdlStart.getTime();
        end = wdlEnd.getTime();
        if (start > end) return;
        System.Text.StringBuilder query = new System.Text.StringBuilder();

        if (ddlStation.SelectedItem != null && ddlStation.SelectedValue != "0")
            query.Append("STATION='" + ddlStation.SelectedItem.Text + "'");
        else
            query.Append("1=1");

        query.Append(" and to_char(DATEM,'YYYYMMDD')>='" + start.ToString("yyyyMMdd") + "' and to_char(DATEM,'YYYYMMDD')<='" + end.ToString("yyyyMMdd") + "'");
        ViewState["BaseQuery"] = query.ToString();

        if (Session["Orders"] == null)
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"];
        else
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["Orders"];

        GridViewBind();
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (sender == null) return;
        if (e.Row.RowType == DataControlRowType.DataRow)  //显示厂站描述
        {
            if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
                _sql = "select NAME from DMIS_SYS_DEPART where ID=" + e.Row.Cells[0].Text;
            else
                _sql = "select OTHER_LANGUAGE_DESCR from DMIS_SYS_DEPART where ID=" + e.Row.Cells[0].Text;
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj != null) e.Row.Cells[0].Text = obj.ToString();
        }
    }
}

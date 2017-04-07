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
using System.Threading;
public partial class SYS_Common_frmBrowseByMonth : PageBaseList
{
    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;
        if (!Page.IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();
            lblFuncName.Text = Session["FuncName"].ToString();
            btnDelete.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "DeleteBeforeConfirm").ToString() + "');");
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
            if (Session["DateQueryCol"] == null)
            {
                JScript.Alert("Date Query！");  //要翻译
                return;
            }

            //获取每页显示的行数
            object obj = DBOpt.dbHelper.ExecuteScalar("select PAGE_ROWS from DMIS_SYS_TABLES where ID=" + Session["MainTableId"].ToString());
            if (obj == null)
                grvList.PageSize = 20;
            else
                grvList.PageSize = Convert.ToInt16(obj);

            ViewState["BaseSql"] = "select * from " + Session["TableName"] + "";
            uwcMonth.Month = DateTime.Now.ToString("dd-MM-yyyy");
            btnQuery_Click(null, null);
            Session["CustomOrder"] = null;
        }
        else
        {
            //自定义排序页面关闭后,刷新GridView
            if (Session["CustomOrder"] != null && ViewState["sql"].ToString().IndexOf(Session["CustomOrder"].ToString()) < 0)
            {
                LoadHeader();
                if (ViewState["BaseQuery"] != null)  //页面自带查询条件
                    ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["CustomOrder"];
                else   //无查询条件
                    ViewState["sql"] = ViewState["BaseSql"] + " order by " + Session["CustomOrder"];
                GridViewBind();
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (Session["DateQueryCol"] != null)
        {
            LoadHeader();
            ViewState["BaseQuery"] = "to_char(" + Session["DateQueryCol"].ToString() + ",'MMYYYY')='" + uwcMonth.Month + "'";
            if (Session["Orders"] == null) 
                ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"];
            else
                ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["Orders"];

            GridViewBind();
        }
    }

    protected void LoadHeader()
    {
        grvList.Columns.Clear();
        ButtonField b = new ButtonField();
        b.Text = " " + "<img border=0 align=absmiddle src=../img/unselected.gif>" + " ";
        b.HeaderText = GetGlobalResourceObject("WebGlobalResource", "Select").ToString();  //选择
        b.CommandName = "Select";
        b.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        b.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        b.ItemStyle.Width = new Unit("30px");
        grvList.Columns.Add(b);

        string _sql = "select NAME,DESCR,OTHER_LANGUAGE_DESCR,CONTROL_LIST_WIDTH,CONTROL_LIST_DISPLAY_FORMAT,CONTROL_LIST_DISPLAY_ALIGN,TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and ISDISPLAY=1 order by ORDER_ID";
        DataTable _dt = DBOpt.dbHelper.GetDataTable(_sql);
        for (int i = 0; i < _dt.Rows.Count; i++)
        {
            BoundField bf = new BoundField();
            if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
                bf.HeaderText = _dt.Rows[i]["DESCR"] == Convert.DBNull ? "" : _dt.Rows[i]["DESCR"].ToString();
            else
                bf.HeaderText = _dt.Rows[i]["OTHER_LANGUAGE_DESCR"] == Convert.DBNull ? "" : _dt.Rows[i]["OTHER_LANGUAGE_DESCR"].ToString();
            bf.DataField = _dt.Rows[i]["NAME"].ToString();
            if (_dt.Rows[i]["CONTROL_LIST_WIDTH"] != Convert.DBNull) 
                bf.ItemStyle.Width = new Unit(_dt.Rows[i]["CONTROL_LIST_WIDTH"].ToString()+"px");
            if (_dt.Rows[i]["CONTROL_LIST_DISPLAY_FORMAT"] != Convert.DBNull && _dt.Rows[i]["CONTROL_LIST_DISPLAY_FORMAT"].ToString().Trim().Length > 0)
            {
                bf.DataFormatString =  _dt.Rows[i]["CONTROL_LIST_DISPLAY_FORMAT"].ToString();
                if (_dt.Rows[i]["TYPE"].ToString() == "Datetime") bf.HtmlEncode = false;
            }
            if (_dt.Rows[i]["CONTROL_LIST_DISPLAY_ALIGN"] != Convert.DBNull)
            {
                if (_dt.Rows[i]["CONTROL_LIST_DISPLAY_ALIGN"].ToString() == "1")
                    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                else if (_dt.Rows[i]["CONTROL_LIST_DISPLAY_ALIGN"].ToString() == "0")
                    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                else if (_dt.Rows[i]["CONTROL_LIST_DISPLAY_ALIGN"].ToString() == "2")
                    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                else if (_dt.Rows[i]["CONTROL_LIST_DISPLAY_ALIGN"].ToString() == "3")
                    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Justify;
            }
            grvList.Columns.Add(bf);
        }
    }

    protected override void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmBrowseDetail.aspx?TID=&URL=" + Session["Url"].ToString() + "&FuncId=" + Session["FuncId"].ToString());
    }

    protected override void btnModify_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "ModifyMessage").ToString()); //"请先选择要修改的记录！"
            return;
        }
        Response.Redirect("frmBrowseDetail.aspx?TID=" + grvList.SelectedDataKey[0].ToString() + "&URL=" + Session["Url"].ToString() + "&FuncId=" + Session["FuncId"].ToString());
    }
}

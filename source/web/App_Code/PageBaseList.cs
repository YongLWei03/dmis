using System;
using System.Data;
using System.Configuration;
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

/// <summary>
/// PageBase 的摘要说明
/// </summary>
public class PageBaseList:System.Web.UI.Page
{
    protected GridView grvRef;   //在后代的Page_Load事件代码中要把实际的GridView控件赋给此变量。
    protected TextBox txtPageNumber; //页面导航中的手工输入页码的控件
    protected HtmlTableCell tdPageMessage;   //页面导航中的显示页面页数、总数的信息栏

    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }


    protected virtual void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["MemberID"] == null)
        {
            JScript.ReturnLogin();
        }

        if (!Page.IsPostBack)
        {
            //2008-5-20 满足页面不在功能菜单中注册的情况
            if (Request["ID"] == null || Request["ID"].Trim() == "")
            {
                Session["FuncId"] = null;
                return;
            }

            Session["FuncId"] = Request["ID"];
            string _sql = "select REPORT_ID,TABLE_IDS,OTHER_PARA,ORDERS,NAME,OTHER_LANGUAGE_DESCR from DMIS_SYS_TREEMENU where ID=" + Request["ID"];
            DataTable _dt = DBOpt.dbHelper.GetDataTable(_sql);

            //功能名称,用于在自定义查询显示时显示功能名
            if (Session["Culture"] != null && Session["Culture"].ToString() == "zh-CN")
                Session["FuncName"] = _dt.Rows[0]["NAME"];
            else
                Session["FuncName"] = _dt.Rows[0]["OTHER_LANGUAGE_DESCR"];

            //报表ID
            if (_dt.Rows[0]["REPORT_ID"] == Convert.DBNull || _dt.Rows[0]["REPORT_ID"].ToString().Trim() == "")
                Session["ReportId"] = null;
            else
                Session["ReportId"] = _dt.Rows[0]["REPORT_ID"];


            //数据库IDs
            if (_dt.Rows[0]["TABLE_IDS"] != Convert.DBNull && _dt.Rows[0]["TABLE_IDS"].ToString().Trim()!="")
            {
                Session["TableIds"] = _dt.Rows[0]["TABLE_IDS"];
                // Session["MainTableId"]: 主表ID，用于自定义排序和自定义查询中
                if (_dt.Rows[0]["TABLE_IDS"].ToString().IndexOf(',') > 0)
                    Session["MainTableId"] = _dt.Rows[0]["TABLE_IDS"].ToString().Substring(0, _dt.Rows[0]["TABLE_IDS"].ToString().IndexOf(','));
                else
                    Session["MainTableId"] = _dt.Rows[0]["TABLE_IDS"].ToString();
               
                Session["TableName"] = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + Session["MainTableId"].ToString());  //在细节页面时使用
                //2009-6-22 增加日期查询列，用于通用浏览界面中
                Session["DateQueryCol"] = DBOpt.dbHelper.ExecuteScalar("select QUERY_COL from DMIS_SYS_TABLES where ID=" + Session["MainTableId"].ToString());  //
                //2010-9-16 增加细节页面自定列显示风格,一列、二列
                Session["ColumnStyle"] = DBOpt.dbHelper.ExecuteScalar("select DISPLAY_STYLE from DMIS_SYS_TABLES where ID=" + Session["MainTableId"].ToString());  //

            }
            else
            {
                Session["MainTableId"] = null;
                Session["TableIds"] = null;
                Session["TableName"] = null;
                Session["DateQueryCol"] = null;
                Session["ColumnStyle"] = null;
            }

            //平台中的其它参数，例如文档管理中的文件存放路径
            if (_dt.Rows[0]["OTHER_PARA"] == Convert.DBNull || _dt.Rows[0]["OTHER_PARA"].ToString().Trim() == "")
                Session["OtherPara"] = null;
            else
                Session["OtherPara"] = _dt.Rows[0]["OTHER_PARA"];

            //平台中程序中设置的排序条件
            if (_dt.Rows[0]["ORDERS"] == Convert.DBNull || _dt.Rows[0]["ORDERS"].ToString().Trim() == "")
                Session["Orders"] = null;
            else
                Session["Orders"] = _dt.Rows[0]["ORDERS"];  

            Session["URL"] =  Page.Request.Url.ToString();
        }

        if (Session["Theme"] == null)
            Page.Theme = "default";
        else
            Page.Theme = Session["Theme"].ToString();
    }

    /// <summary>
    /// 页面导航按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void NavigateToPage(object sender, EventArgs e)
    {
        string pageInfo = ((Button)sender).CommandName.ToLower();
        switch (pageInfo)
        {
            case "first":
                grvRef.PageIndex = 0;
                break;
            case "prev":
                if (grvRef.PageIndex > 0)
                {
                    grvRef.PageIndex -= 1;
                }
                break;
            case "next":
                if (grvRef.PageIndex < (grvRef.PageCount - 1))
                {
                    grvRef.PageIndex += 1;
                }
                break;
            case "last":
                grvRef.PageIndex = (grvRef.PageCount - 1) < 0 ? 0 : (grvRef.PageCount - 1);
                break;
            case "go":
                if (txtPageNumber.Text == "") { return; }
                int num;
                if (!int.TryParse(txtPageNumber.Text, out num))
                {
                    txtPageNumber.Text = "";
                    return;
                }
                if (num > grvRef.PageCount)
                {
                    txtPageNumber.Text = grvRef.PageCount.ToString();
                    grvRef.PageIndex = grvRef.PageCount - 1;
                }
                else
                {
                    grvRef.PageIndex = num - 1;
                }
                break;
            default:
                grvRef.PageIndex = 0;
                break;
        }
        GridViewBind();
    }

    /// <summary>
    /// GridView控件的数据绑定过程
    /// </summary>
    protected virtual void GridViewBind()
    {
        DataTable dt = DBOpt.dbHelper.GetDataTable(ViewState["sql"].ToString());
        if(dt==null)
        {
            if(tdPageMessage!=null) 
                tdPageMessage.InnerText = (String)GetGlobalResourceObject("WebGlobalResource", "GridViewSqlErrorMessage");
            return;
        }
        grvRef.DataSource = dt;
        grvRef.DataBind();
        grvRef.SelectedIndex = -1;
        if (tdPageMessage != null)
        {
            if (dt.Rows.Count == 0)
                tdPageMessage.InnerText = "";
            else
                tdPageMessage.InnerText = (String)GetGlobalResourceObject("WebGlobalResource", "PageNumber") + (grvRef.PageIndex + 1).ToString() + "/" + grvRef.PageCount.ToString() + " " + (String)GetGlobalResourceObject("WebGlobalResource", "Records") + dt.Rows.Count.ToString();
        }
    }

    /// <summary>
    /// 页面出错，记录出错的日志
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void Page_Error(object sender, EventArgs e)
    {
        string errMsg;
        //得到系统上一个异常
        Exception currentError = Server.GetLastError();

        errMsg = "<link rel=\"stylesheet\" href=\"/default.css\">";
        errMsg += "<h1>" + (String)GetGlobalResourceObject("WebGlobalResource", "Error") + "</h1>" +
            (String)GetGlobalResourceObject("WebGlobalResource", "ErrorPosition") + Request.Url.ToString() + "<br/><hr/>" +
            (String)GetGlobalResourceObject("WebGlobalResource", "ErrorMessage") + " <font class=\"ErrorMessage\">" + currentError.Message.ToString() + "</font><hr/>" +
            "<b>Stack Trace:</b><br/>" +
            currentError.ToString();
        //如果发生致命应用程序错误
        //if (!(currentError is ApplicationException))
        //{
            //向Windows事件日志中写入错误日志
           // LogEvent(currentError.ToString(), EventLogEntryType.Error);
        //}
        //在页面中显示错误
        Response.Write(errMsg);
        //记录错误到日志中
        WebLog.InsertLog("错误", "", "网页名:" + Request.Url.ToString() + " ； 错误信息：" + currentError.Message.ToString() + "； 错误发生地点：" + currentError.StackTrace);
        //清除异常
        Server.ClearError();
    }

    /// <summary>
    /// 添加按钮的触发事件，细节页面的命令须是“(详细页面)_Det.aspx”的格式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 1].Substring(0, Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 1].IndexOf(".aspx")) + "_Det.aspx?TID=&URL=" + Session["Url"].ToString());
    }

    protected virtual void btnModify_Click(object sender, EventArgs e)
    {
        if (grvRef.SelectedIndex < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "ModifyMessage").ToString()); //"请先选择要修改的记录！"
            return;
        }
        Response.Redirect(Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 1].Substring(0, Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 1].IndexOf(".aspx")) + "_Det.aspx?TID=" + grvRef.SelectedDataKey[0].ToString() + "&URL=" + Session["Url"].ToString());
    }

    /// <summary>
    /// 自定义查询界面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void btnSearch_Click(object sender, EventArgs e)
    {
        if (Session["MainTableId"] == null || Session["MainTableId"].ToString().Trim() == "")
        {
            JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "NoTableID"));
            return;
        }
        //自定义查询采用弹出窗口，在弹出窗口选择完条件后，再打开一个查询结果的新窗口，而不是返回本窗口。
        JScript.OpenWindow("../SYS_Common/frmSearchBuild.aspx", "", "resizable=yes,scrollbars=yes,width=700px,height=500px,left=100px,top=10px");//(String)GetGlobalResourceObject("WebGlobalResource", "SearchBuildWindow")
    }

    /// <summary>
    /// 报表打印界面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void btnPrint_Click(object sender, EventArgs e)
    {
        if (Session["ReportId"] == null || Session["ReportId"].ToString().Trim() == "")
        {
            Response.Write("<script language=javascript> alert('" + GetGlobalResourceObject("WebGlobalResource", "NoReportMessage").ToString() + "')</script");
            return;
        }
        //要想直接传递参数打印，则使用注释中的语法，paras的格式如下:条件值1^条件值2^条件值3^......
        //JScript.OpenWindow("../SYS_Common/frmCellReportDisplay.aspx?ReportID=" + Session["ReportId"].ToString()+"&Values="+paras, "报表打印", "toolbar=no,menubar=no,titlebar=yes,directories=no,resizable=yes,status=yes");
        JScript.OpenWindow("../SYS_Common/frmCellReportDisplay.aspx?ReportID=" + Session["ReportId"].ToString(), "", "toolbar=no,menubar=no,titlebar=yes,directories=no,resizable=yes,status=yes");
        //(String)GetGlobalResourceObject("WebGlobalResource", "ReportPrint")
    }


    protected virtual void btnSort_Click(object sender, EventArgs e)
    {
        if (Session["MainTableId"] == null || Session["MainTableId"].ToString().Trim() == "")
        {
            JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "NoTableID"));
            return;
        }
        JScript.OpenWindow("../SYS_Common/frmSetSort.aspx?MainTableId=" + Session["MainTableId"].ToString(), "", "resizable=yes,scrollbars=no,status=yes,width=600px,height=500px,left=100px,top=10px");//GetGlobalResourceObject("WebGlobalResource", "SortBuildWindow").ToString()
    }

    protected virtual void btnDelete_Click(object sender, EventArgs e)
    {
        if (grvRef.SelectedIndex < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "DeleteMessage").ToString());//"请先选择要删除的记录！"
            return;
        }

        string sql = "delete from " + Session["TableName"] + " where TID=" + grvRef.SelectedDataKey.Value;
        DBOpt.dbHelper.ExecuteSql(sql);
        GridViewBind();
    }

    protected virtual void grvRef_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender == null) return;
        GridView g=(GridView)sender;

        if (g.SelectedIndex < 0) return;
        g.SelectedRow.Cells[0].Text = "<img border=0 align=absmiddle src=../img/Selected.gif>";
    }

    #region 点击GridView列标题时的排序
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected virtual void grvList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, "ASC");
            }
        }

        protected virtual void SortGridView(string sortExpression, string direction)
        {
            DataTable dt = DBOpt.dbHelper.GetDataTable(ViewState["sql"].ToString());
            DataView dv = new DataView(dt);

            dv.Sort = sortExpression + " " + direction;

            grvRef.DataSource = dv;
            grvRef.DataBind();
        }

    #endregion

    /// <summary>
    /// 保存EXCEL按钮事件代码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void btnSaveExcel_Click(object sender, EventArgs e)
    {
        //下述代码在导出EXCEL时，如果某列的内容包含回车键，则导出的数据不对，乱了。
        //if (ViewState["sql"] == null)
        //{
        //    JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "NoSqlMessage"));
        //    return;
        //}
        //if (Session["MainTableId"] == null)
        //{
        //    JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "NoTableID"));
        //    return;
        //}
        //string path, fileName;
        //if (ExcelOpt.GenerateExcel(ViewState["sql"].ToString(), Session["MainTableId"].ToString(), out path, out fileName) == -1)
        //{
        //    JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage"));
        //    return;
        //}
        //DownLoadFile.DownFile(path, fileName);


        if (grvRef.Rows.Count < 1)
        {
            //JScript.Alert("没有数据,无法导出EXCEL文件!");
            return;
        }

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=result.xls");
        Response.Charset = "UTF-8";
        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grvRef.AllowPaging = false;
        grvRef.AllowSorting = false;
        DataTable dt = DBOpt.dbHelper.GetDataTable(ViewState["sql"].ToString());
        grvRef.DataSource = dt;
        grvRef.DataBind();

        grvRef.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());
        Response.End();

        grvRef.AllowPaging = true;
        grvRef.AllowSorting = true;
        GridViewBind();
    }

    //此方法是为了导出EXCEL方便
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    #region 在GridView控件中直接编辑时所触发的事件
    protected virtual void grvRef_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        grvRef.EditIndex = -1;
        GridViewBind();
    }

    protected virtual void grvRef_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvRef.EditIndex = e.NewEditIndex;
        grvRef.EditRowStyle.BackColor = System.Drawing.Color.FromName("#F7CE90");
        GridViewBind();

        int tableID,tid;
        tableID=Convert.ToInt16(Session["MainTableId"]);
        tid=Convert.ToInt16(grvRef.DataKeys[e.NewEditIndex].Value);
        GridViewEdit.GridViewEditing(ref grvRef, tableID, e.NewEditIndex, tid);
    }

    protected virtual void grvRef_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sql;
        int tableID, tid;
        tableID = Convert.ToInt16(Session["MainTableId"]);
        tid = Convert.ToInt16(grvRef.DataKeys[e.RowIndex].Value);
        sql = GridViewEdit.GetGridViewRowUpdating(ref grvRef, tableID, e.RowIndex, tid);
        if (DBOpt.dbHelper.ExecuteSql(sql) > 0)
        {
            grvRef.EditIndex = -1;
            GridViewBind();
        }
        else
        {
            JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage"));
            return;
        }
    }

    #endregion

}

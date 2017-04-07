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
using System.Data.Common;
using System.Text;
using System.Globalization;
using System.Threading;



public partial class yw_zhuhai_frmSearchResult : System.Web.UI.Page
{
    string sql;

    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MemberID"] == null) JScript.ReturnLogin();

        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();
            tdTitle.InnerText = Session["FuncName"].ToString().Trim();
            tdCondition.InnerText = Session["SearchWheresDesc"].ToString();
            GenerateSql();
            GridViewBind();
        }
    }

    private void GenerateSql()
    {
        if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
            sql = "select NAME,DESCR,ISPRIMARY,TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and ISDISPLAY=1 order by ISDISPLAY";
        else
            sql = "select NAME,OTHER_LANGUAGE_DESCR DESCR,ISPRIMARY,TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and ISDISPLAY=1 order by ISDISPLAY";
        
        DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
        sql = "";

        Hashtable primaryKey = new Hashtable(dt.Rows.Count);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sql = sql + dt.Rows[i][0].ToString() + ",";
            if (!(dt.Rows[i][2] == Convert.DBNull) && Convert.ToInt16(dt.Rows[i][2]) == 1)
            {
                primaryKey.Add(i, dt.Rows[i][0].ToString());
            }
        }

        if (sql.Length > 0)
            sql = sql.Substring(0, sql.Length - 1);
        else
            sql = "*";
        ViewState["columns"] = sql;
        if (Session["SearchWheres"] == null)
            Session["SearchSql"] = "select " + sql + " from " + Session["TableName"].ToString() + " a " + (Session["Orders"] != null ? " order by " + Session["Orders"].ToString() : "");
        else
            Session["SearchSql"] = "select " + sql + " from " + Session["TableName"].ToString() + " a  where " + Session["SearchWheres"].ToString() + (Session["Orders"] != null ? " order by " + Session["Orders"].ToString() : "");
        ViewState["dt"] = dt;
        ViewState["primaryKey"] = primaryKey;
        dt.Dispose();
    }


    private void LoadColumnHead()
    {
        grvList.Columns.Clear();
        System.Text.StringBuilder _cols = new System.Text.StringBuilder();
        DataTable dt = (DataTable)ViewState["dt"];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            _cols.Append(dt.Rows[i][0].ToString() + ",");
            BoundField bf = new BoundField();
            bf.HeaderText = dt.Rows[i][1].ToString();
            bf.DataField = dt.Rows[i][0].ToString();
            if (dt.Rows[i][3].ToString() == "Datetime")
            {
                bf.DataFormatString = "{0:dd-MM-yyyy HH:mm}";
                bf.HtmlEncode = false;
            }    
            grvList.Columns.Add(bf);
        }


        ButtonField b = new ButtonField();
        b.Text = " " + "<img border=0 align=absmiddle src=../img/icon_search2.gif>" + " ";
        b.HeaderText = GetGlobalResourceObject("WebGlobalResource", "ComSearchResult001").ToString();//要翻译
        b.CommandName = "Select";
        b.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        b.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        b.ItemStyle.Width = new Unit("30px");
        grvList.Columns.Add(b);
    }

    protected void NavigateToPage(object sender, EventArgs e)
    {
        string pageInfo = ((Button)sender).CommandName.ToLower();
        switch (pageInfo)
        {
            case "first":
                grvList.PageIndex = 0;
                break;
            case "prev":
                if (grvList.PageIndex > 0)
                {
                    grvList.PageIndex -= 1;
                }
                break;
            case "next":
                if (grvList.PageIndex < (grvList.PageCount - 1))
                {
                    grvList.PageIndex += 1;
                }
                break;
            case "last":
                grvList.PageIndex = (grvList.PageCount - 1) < 0 ? 0 : (grvList.PageCount - 1);
                break;
            case "go":
                if (txtPage.Text == "") { return; }
                int pages;
                if (!int.TryParse(txtPage.Text, out pages))
                {
                    txtPage.Text = "";
                    return;
                }
                if (pages > grvList.PageCount)
                {
                    txtPage.Text = grvList.PageCount.ToString();
                    grvList.PageIndex = grvList.PageCount - 1;
                }
                else
                {
                    grvList.PageIndex = pages - 1;
                }
                break;
            default:
                grvList.PageIndex = 0;
                break;
        }

        GridViewBind();
    }

    private void GridViewBind()
    {
        LoadColumnHead();

        DataTable dt = DBOpt.dbHelper.GetDataTable(Session["SearchSql"].ToString());
        grvList.DataSource = dt;
        grvList.DataBind();
        grvList.SelectedIndex = -1;
        if (dt.Rows.Count == 0)
        {
            tdMessage.InnerText = "";
        }
        else
        {
            tdMessage.InnerText =  (grvList.PageIndex + 1).ToString() + "/" + grvList.PageCount.ToString() + " "+ dt.Rows.Count.ToString() ;
        }
    }

    protected void grvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0) return;
        Hashtable primaryKey = (Hashtable)ViewState["primaryKey"];
        int id=0;
        object obj;
        string colType;
        string primaryValue = "";

        foreach (DictionaryEntry de in primaryKey)
        {
            id = Convert.ToInt16(de.Key);
            if (DBHelper.databaseType == "SqlServer")
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and NAME='" + de.Value.ToString() + "'");
                colType = obj.ToString();
                if (colType == "Numeric")
                {
                    primaryValue = primaryValue + de.Value + "=" + grvList.SelectedRow.Cells[id].Text + " and ";
                }
                else if (colType == "String")
                {
                    primaryValue = primaryValue + de.Value + "='" + grvList.SelectedRow.Cells[id].Text + "' and ";
                }
                else if (colType == "Datetime")
                {
                    primaryValue = primaryValue + de.Value + "='" + grvList.SelectedRow.Cells[id].Text + "' and ";
                }
                else
                {
                }
            }
            else if (DBHelper.databaseType == "Oracle")
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and NAME='" + de.Value.ToString() + "'");
                colType = obj.ToString();
                if (colType == "Numeric")
                {
                    primaryValue = primaryValue + de.Value + "=" + grvList.SelectedRow.Cells[id].Text + " and ";
                }
                else if (colType == "String")
                {
                    primaryValue = primaryValue + de.Value + "='" + grvList.SelectedRow.Cells[id].Text + "' and ";
                }
                else if (colType == "Datetime")
                {
                    primaryValue = primaryValue + "to_char(" + de.Value + ",'DD-MM-YYYY HH24:MI')='" + grvList.SelectedRow.Cells[id].Text + "' and ";
                }
                else
                {
                }
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and NAME='" + de.Value.ToString() + "'");
                colType = obj.ToString();
                if (colType == "Numeric")
                {
                    primaryValue = primaryValue + de.Value + "=" + grvList.SelectedRow.Cells[id].Text + " and ";
                }
                else if (colType == "String")
                {
                    primaryValue = primaryValue + de.Value + "='" + grvList.SelectedRow.Cells[id].Text + "' and ";
                }
                else if (colType == "Datetime")
                {
                    primaryValue = primaryValue + de.Value + "='" + grvList.SelectedRow.Cells[id].Text + "' and ";
                }
                else
                {
                }
                
            }
            else
            {
            }
        }
        primaryValue = primaryValue.Substring(0, primaryValue.Length - 4);
        Session["SearchDetailWhere"] = primaryValue;
        Response.Write("<scr" + "ipt language=javascript>window.open('frmSearchDetail.aspx',null,'target=_blank,status=yes,toolbar=yes,menubar=yes,location=yes,resizable=yes,scrollbars=yes');</scr" + "ipt>");
     
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected void imbOutExcel_Click(object sender, ImageClickEventArgs e)
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

    public void SaveToExcel(DataTable objTable)
    {
        int CountR = objTable.Rows.Count;//行数
        int CountC = objTable.Columns.Count;//列数
        Response.Clear();
        Response.Buffer = true;

        //设置Http的头信息,编码格式
        Response.AppendHeader("Content-Disposition", "attachment;filename=result.xls");
        Response.ContentType = "application/ms-excel";


        //设置编码
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        //写表头
        for (int i = 0; i < CountC; i++)
        {
            Response.Write(objTable.Columns[i].ColumnName + "\t");
        }
        Response.Write("\n");
        //写表内容
        for (int RowNo = 0; RowNo <= CountR - 1; RowNo++)
        {
            string RowContent = "";
            for (int CloumnNo = 0; CloumnNo <= CountC - 1; CloumnNo++)
            {
                RowContent += Convert.ToString(objTable.Rows[RowNo][CloumnNo]) + "\t";
            }
            RowContent += "\n";
            Response.Write(RowContent);
        }
        Response.End();
    } 


}

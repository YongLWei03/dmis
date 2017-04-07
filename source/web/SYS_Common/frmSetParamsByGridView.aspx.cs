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

public partial class SYS_Common_frmSetParamsByGridView : PageBaseList
{
    private string _sql;
    private string _columns;
    private DataTable _dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvTable;
        if (!IsPostBack)
        {
            //找主键列，目前规定只能有一列且是整数类型。
            object obj = DBOpt.dbHelper.ExecuteScalar("select NAME FROM DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and isprimary=1");
            if (obj == null)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "NoPrimaryColumns").ToString());
                return;
            }
            ViewState["PK_ColName"] = obj;


            lblTitle.Text = Session["FuncName"].ToString();
            System.Text.StringBuilder _cols = new System.Text.StringBuilder("");
            if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
                _sql = "select NAME,DESCR from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and ISDISPLAY=1 order by ORDER_ID";
            else
                _sql = "select NAME,OTHER_LANGUAGE_DESCR DESCR from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and ISDISPLAY=1 order by ORDER_ID";
            _dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                _cols.Append(_dt.Rows[i][0].ToString() + ",");
            }
            _columns = _cols.ToString().Substring(0, _cols.Length - 1);
            if(Session["Orders"]!=null)
                ViewState["sql"] = "select " + _columns + " from " + Session["TableName"] + " order by " + Session["Orders"].ToString();
            else
                ViewState["sql"] = "select " + _columns + " from " + Session["TableName"];
            GridViewBind();
        }
    }


    protected void LoadHeader()
    {
        while (grvTable.Columns.Count > 1)
        {
            grvTable.Columns.Remove(grvTable.Columns[grvTable.Columns.Count - 1]);
        }

        if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
            _sql = "select NAME,DESCR from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and ISDISPLAY=1 order by ORDER_ID";
        else
            _sql = "select NAME,OTHER_LANGUAGE_DESCR DESCR from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and ISDISPLAY=1 order by ORDER_ID";
        _dt = DBOpt.dbHelper.GetDataTable(_sql);
        for (int i = 0; i < _dt.Rows.Count; i++)
        {
            TemplateField tf = new TemplateField();
            GridviewEditItemTemplate ei = new GridviewEditItemTemplate(Session["MainTableId"].ToString(), _dt.Rows[i][0].ToString());
            tf.EditItemTemplate = ei;
            GridviewItemTemplate gt = new GridviewItemTemplate(_dt.Rows[i][0].ToString());
            tf.ItemTemplate = gt;
            tf.HeaderText = _dt.Rows[i][1].ToString();
            grvTable.Columns.Add(tf);
        }
    }

    protected override void GridViewBind()
    {
        LoadHeader();
        _dt = DBOpt.dbHelper.GetDataTable(ViewState["sql"].ToString());
        if (_dt == null)
        {
            tdPageMessage.InnerText = (String)GetGlobalResourceObject("WebGlobalResource", "GridViewSqlErrorMessage");
            return;
        }
        grvTable.DataSource = _dt;
        grvTable.DataBind();
        string[] keys ={ ViewState["PK_ColName"].ToString() };
        grvTable.DataKeyNames = keys;

        if (tdPageMessage != null)
        {
            if (_dt.Rows.Count == 0)
                tdPageMessage.InnerText = "";
            else
                tdPageMessage.InnerText = (String)GetGlobalResourceObject("WebGlobalResource", "Records") + _dt.Rows.Count.ToString();
        }
    }


    protected void grvTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex < 0) return;
        _sql = "delete from " + Session["TableName"] + " where " + ViewState["PK_ColName"].ToString() + "=" + grvTable.DataKeys[e.RowIndex].Value;
        if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
            GridViewBind();
    }

    protected void grvTable_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "New")  
        {
            uint maxTid = DBOpt.dbHelper.GetMaxNum(Session["TableName"].ToString(),ViewState["PK_ColName"].ToString());
            _sql = "insert into " + Session["TableName"].ToString() + "(" + ViewState["PK_ColName"].ToString() + ") values(" + maxTid + ")";
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
                GridViewBind();
        }
        //else if (e.CommandName == "Delete")
        //{
        //    _sql = "delete from " + Session["TableName"] + " where " + ViewState["PK_ColName"].ToString() + "=" + grvTable.DataKeys[Convert.ToInt16(e.CommandArgument)].Value;
        //    if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
        //        GridViewBind();
        //}
    }

    protected override void grvRef_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        LoadHeader();

        //_sql = "select NAME,custom_control_name from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and ISDISPLAY=1 order by ORDER_ID";
        //_dt = DBOpt.dbHelper.GetDataTable(_sql);
        //for (int i = 0; i < _dt.Rows.Count; i++)
        //{
        //    grvRef.Rows[e.RowIndex].Cells[i + 1].Controls[0].ID = _dt.Rows[i][1].ToString();
        //}


        string sql;
        int tableID, tid;
        tableID = Convert.ToInt16(Session["MainTableId"]);
        tid = Convert.ToInt16(grvRef.DataKeys[e.RowIndex].Value);
        GridViewRow row = grvRef.Rows[e.RowIndex];
        sql = GridViewEdit.GetGridViewRowUpdating(ref grvRef, tableID, e.RowIndex, tid);
        sql = "update t_dd_running_log_type set name='测试' where tid=8";
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

    //加上以下事件，删除时不会执行相应的删除代码？晕
    //protected void grvTable_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        ImageButton ib = (ImageButton)e.Row.Cells[0].Controls[2];  //删除
    //        if(ib.CommandName=="Delete")
    //            ib.Attributes.Add("onclick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "DeleteBeforeConfirm").ToString() + "')");
    //    }
    //}

}

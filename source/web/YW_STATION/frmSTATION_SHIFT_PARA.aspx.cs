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

using System.Text;
using PlatForm.DBUtility;
using PlatForm.Functions;
using PlatForm.CustomControlLib;

public partial class YW_STATION_frmSTATION_SHIFT_PARA : PageBaseList
{
    string _sql;
    DataTable _dt;
    object obj;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;

        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();
            lblFuncName.Text = Session["FuncName"].ToString();
            btnDelete.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "DeleteBeforeConfirm").ToString() + "');");

            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
            initStation();
            btnQuery_Click(null, null);
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
        if (ddlStation.SelectedValue == "0")
            ViewState["sql"] = "select * from T_STATION_SHIFT_PARA  order by STATION_ID,SHIFT_STARTTIME";
        else
            ViewState["sql"] = "select * from T_STATION_SHIFT_PARA where STATION_ID=" + ddlStation.SelectedValue + " order by SHIFT_STARTTIME";
        GridViewBind();
    }

    protected void grv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (sender == null) return;

        GridView grv = (GridView)sender;
        grv.EditIndex = e.NewEditIndex;
        grv.EditRowStyle.BackColor = System.Drawing.Color.FromName("#F7CE90");
        GridViewBind();

        int tid = Convert.ToInt16(grv.DataKeys[e.NewEditIndex].Value);
        GridViewEdit.GridViewEditing(ref grv, Convert.ToInt16(Session["MainTableId"]), e.NewEditIndex, tid);
    }

    protected void grv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (sender == null) return;

        GridView grv = (GridView)sender;
        int tid, tableID;

        tid = Convert.ToInt16(grv.DataKeys[e.RowIndex].Value);
        tableID = Convert.ToInt16(Session["MainTableId"]);

        _sql = GridViewEdit.GetGridViewRowUpdating(ref grv, tableID, e.RowIndex, tid);

        if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
        {
            grv.EditIndex = -1;
            GridViewBind();
        }
        else
        {
            //JScript.Alert("数据保存失败!");
            return;
        }
    }

    protected override void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlStation.SelectedItem == null || ddlStation.SelectedValue == "0") return;

        uint max = DBOpt.dbHelper.GetMaxNum("T_STATION_SHIFT_PARA", "TID");
        _sql = "insert into T_STATION_SHIFT_PARA(TID,STATION_ID) values(" +  max.ToString() + "," + ddlStation.SelectedValue + ")";
        if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
        {
            btnQuery_Click(null, null);

            int rowID;
            for (rowID = 0; rowID < grvList.Rows.Count; rowID++)
            {
                if (grvList.DataKeys[rowID].Value.ToString() == max.ToString()) break;
            }
            GridViewEditEventArgs arg = new GridViewEditEventArgs(rowID);
            grv_RowEditing(grvList, arg);
        }
        else
        {
            //WebLog.InsertLog("", "失败", _sql);
            return;
        }
    }

    //删除最后一条记录
    protected override void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlStation.SelectedItem == null || ddlStation.SelectedValue == "0") return;
        if (grvList.Rows.Count < 1) return;

        _sql = "delete from T_STATION_SHIFT_PARA where TID=" + grvList.DataKeys[grvList.Rows.Count-1].Value.ToString();
        DBOpt.dbHelper.ExecuteSql(_sql);
        GridViewBind();
    }

    protected void grv_RowDataBound(object sender, GridViewRowEventArgs e)
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

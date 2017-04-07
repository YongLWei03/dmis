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

/*!--赤几 自动化机房设备巡视记录--*/

public partial class YW_ZDH_frmZDH_COMPU_ROOM_PATROL_LIST_Det : PageBaseDetail
{
    private string _sql;
    private object obj;
    private DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetDetailPageControlLocalizationText();
            lblFuncName.Text = Session["FuncName"].ToString();
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());

            if (Request["date"] != null)
                wdlDate.Text = Request["date"];
            else
                wdlDate.setTime(DateTime.Now);

            btnSave_Click(null, null);
        }
    }

    protected override void btnSave_Click(object sender, EventArgs e)
    {
        if (wdlDate.Text == "") return;
        ViewState["Date"] = wdlDate.getTime().ToString("yyyyMMdd");
        _sql = "select count(*) from T_ZDH_COMPU_ROOM_PATROL_LIST where to_char(DATEM,'YYYYMMDD')='" + ViewState["Date"].ToString() + "'";
        obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        if (obj.ToString() == "0")  //没有此日的数据，则添加。
        {
            uint max = DBOpt.dbHelper.GetMaxNum("T_ZDH_COMPU_ROOM_PATROL_LIST", "TID");
            dt = DBOpt.dbHelper.GetDataTable("select DEVICE_NAME from T_ZDH_COMPU_PATROL_DEVICE_PARA order by ORDER_ID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0] == Convert.DBNull || dt.Rows[i][0].ToString().Trim() == "") continue;
                _sql = "insert into T_ZDH_COMPU_ROOM_PATROL_LIST(TID,DATEM,INSPECTOR,DEVICE_NAME) values(" + max + ",TO_DATE('" + ViewState["Date"].ToString() + "','YYYYMMDD'),'" +
                    Session["MemberName"].ToString() + "','" + dt.Rows[i][0].ToString() + "')";
                DBOpt.dbHelper.ExecuteSql(_sql);
                max++;
            }
        }
        grvList_DataBind();
    }

    private void grvList_DataBind()
    {
        _sql = "select * from T_ZDH_COMPU_ROOM_PATROL_LIST where to_char(DATEM,'YYYYMMDD')='" + ViewState["Date"].ToString() + "'";
        dt = DBOpt.dbHelper.GetDataTable(_sql);
        grvList.DataSource = dt;
        grvList.DataBind();
    }

    #region 在GridView控件中直接编辑时所触发的事件
    protected virtual void grvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        grvList.EditIndex = -1;
        grvList_DataBind();
    }

    protected virtual void grvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvList.EditIndex = e.NewEditIndex;
        grvList.EditRowStyle.BackColor = System.Drawing.Color.FromName("#F7CE90");
        grvList_DataBind();

        int tableID,tid;
        tableID=Convert.ToInt16(Session["MainTableId"]);
        tid=Convert.ToInt16(grvList.DataKeys[e.NewEditIndex].Value);
        GridViewEdit.GridViewEditing(ref grvList, tableID, e.NewEditIndex, tid);
    }

    protected virtual void grvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sql;
        int tableID, tid;
        tableID = Convert.ToInt16(Session["MainTableId"]);
        tid = Convert.ToInt16(grvList.DataKeys[e.RowIndex].Value);
        sql = GridViewEdit.GetGridViewRowUpdating(ref grvList, tableID, e.RowIndex, tid);
        if (DBOpt.dbHelper.ExecuteSql(sql) > 0)
        {
            grvList.EditIndex = -1;
            grvList_DataBind();
        }
        else
        {
            JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage"));
            return;
        }
    }

    #endregion
}

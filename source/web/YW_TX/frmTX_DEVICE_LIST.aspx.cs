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

/*赤几   通信设备台账*/

public partial class YW_TX_frmTX_DEVICE_LIST : PageBaseList
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
            lblFuncName.Text = Session["FuncName"].ToString();
            btnDelete.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "DeleteBeforeConfirm").ToString() + "');");
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());

            initStation();
            ViewState["BaseSql"] = "select * from " + Session["TableName"] + "";
            btnQuery_Click(null, null);
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
        _sql = "select TID,STATION from T_TX_RUNNING_LOG_STATION_PARA order by ORDER_ID";
        DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
        DataRow r = dt.NewRow();
        r[0] = 0;
        r[1] = GetGlobalResourceObject("WebGlobalResource", "All");  //全部
        dt.Rows.InsertAt(r, 0);
        ddlStation.DataTextField = "STATION";
        ddlStation.DataValueField = "TID";
        ddlStation.DataSource = dt;
        ddlStation.DataBind();
        ddlStation.SelectedIndex = 0;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (ddlStation.SelectedItem == null || ddlStation.SelectedValue == "0")
            ViewState["BaseQuery"] = "1=1";
        else
            ViewState["BaseQuery"] = "STATION='" + ddlStation.SelectedItem.Text + "'";

        if (Session["Orders"] == null)
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"];
        else
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["Orders"];

        GridViewBind();
    }

    //利用通用的细节页面，减少工作量。
    protected override void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SYS_Common/frmBrowseDetail.aspx?TID=&URL=" + Session["Url"].ToString() + "&FuncId=" + Session["FuncId"].ToString());
    }

    protected override void btnModify_Click(object sender, EventArgs e)
    {
        if (grvRef.SelectedIndex < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "ModifyMessage").ToString()); //"请先选择要修改的记录！"
            return;
        }
        Response.Redirect("../SYS_Common/frmBrowseDetail.aspx?TID=" + grvRef.SelectedDataKey[0].ToString() + "&URL=" + Session["Url"].ToString() + "&FuncId=" + Session["FuncId"].ToString());
    }

}

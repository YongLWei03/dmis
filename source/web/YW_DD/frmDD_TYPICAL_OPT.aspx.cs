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
// 赤几   典型票 
public partial class YW_DD_frmDD_TYPICAL_OPT : PageBaseList
{
    string _sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;
        if (!Page.IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();
            lblFuncName.Text = GetLocalResourceObject("PageResource1.Title").ToString();
            btnDelete.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "DeleteBeforeConfirm").ToString() + "');");
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());

            FillDropDownList.FillByTable(ref ddlStation, "DMIS_SYS_STATION", "NAME", "TID", "ORDER_ID");

            ViewState["BaseSql"] = "select * from " + Session["TableName"] + "";
            //模块的查询条件，一般是按年、月、日查询；此变量在“检索”按钮中修改，在此初始化。
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
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (ddlStation.SelectedItem != null)
            ViewState["BaseQuery"] = "STATION='" + ddlStation.SelectedItem.Text + "'";
        else
            ViewState["BaseQuery"] = "1=1";

        if (Session["Orders"] == null)   //平台中没有设置排序条件
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"];
        else
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["Orders"];

        GridViewBind();
    }

    protected override void btnDelete_Click(object sender, EventArgs e)
    {
        if (grvRef.SelectedIndex < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "DeleteMessage").ToString());//"请先选择要删除的记录！"
            return;
        }
        //先删除操作步骤，成功之后再删除头部分。
        string sql = "delete from T_DD_TYPICAL_OPT_BODY where HEAD_TID=" + grvRef.SelectedDataKey.Value;
        if (DBOpt.dbHelper.ExecuteSql(sql) >= 0)   //如果只有大于0，则没有步骤的情况下，无法删除头部分记录。
        {
            sql = "delete from " + Session["TableName"] + " where TID=" + grvRef.SelectedDataKey.Value;
            DBOpt.dbHelper.ExecuteSql(sql);
            GridViewBind();
        }
    }
}

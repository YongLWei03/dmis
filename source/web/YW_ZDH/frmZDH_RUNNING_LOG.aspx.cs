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
/*赤几   自动化系统运行日志*/

public partial class YW_ZDH_frmZDH_RUNNING_LOG : PageBaseList
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

            ViewState["BaseSql"] = "select distinct DATEM,OPERATOR,WEATHER from " + Session["TableName"] + "";
            ViewState["BaseQuery"] = "to_char(DATEM,'YYYYMM')='" + DateTime.Now.ToString("yyyyMM") + "'";
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

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ViewState["BaseQuery"] = "to_char(DATEM,'YYYYMM')='" + uMonth.Month + "'";

        if (Session["Orders"] == null)
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"];
        else
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["Orders"];

        GridViewBind();
    }

    protected override void btnDelete_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "DeleteMessage").ToString());//"请先选择要删除的记录！"
            return;
        }
        _sql = "delete from T_ZDH_RUNNING_LOG where to_char(DATEM,'YYYYMMDD')='" + Convert.ToDateTime(grvList.SelectedDataKey.Value).ToString("yyyyMMdd") + "'";
        DBOpt.dbHelper.ExecuteSql(_sql);
        GridViewBind();
    }

    protected override void btnModify_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "ModifyMessage").ToString()); //"请先选择要修改的记录！"
            return;
        }
        Response.Redirect("frmZDH_RUNNING_LOG_Det.aspx?date=" + grvList.SelectedDataKey[0].ToString() + "&URL=" + Session["URL"].ToString());
    }
}

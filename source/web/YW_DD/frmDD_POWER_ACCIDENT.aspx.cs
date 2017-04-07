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

//赤几 电网事故记录
public partial class YW_DD_frmDD_POWER_ACCIDENT : PageBaseList
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

            ViewState["BaseSql"] = "select * from "+Session["TableName"]+"";
            //模块的查询条件，一般是按年、月、日查询；此变量在“检索”按钮中修改，在此初始化。
            ViewState["BaseQuery"] = "to_char(STARTTIME,'YYYYMM')='" + DateTime.Now.ToString("yyyyMM") + "'";
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
            //新增时，关闭新增窗口，控制刷新此页面
            //if (Session["NeedRefesh"] != null && Session["NeedRefesh"].ToString() == "是")
            //{
            //    GridViewBind();
            //    Session["NeedRefesh"] = "否";
            //    return;
            //}
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ViewState["BaseQuery"] = "to_char(STARTTIME,'YYYYMM')='" + uMonth.Month + "'";
        if (Session["Orders"] == null)   //平台中没有设置排序条件
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"];
        else
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["Orders"];

        GridViewBind();
    }

    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("frmDD_POWER_ACCIDENT_Det.aspx?TID=&URL=" + Session["Url"].ToString());
    //}

    //protected void btnModify_Click(object sender, EventArgs e)
    //{
    //    if (grvList.SelectedIndex < 0)
    //    {
    //        JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "ModifyMessage").ToString()); //"请先选择要修改的记录！"
    //        return;
    //    }
    //    Response.Redirect("frmDD_POWER_ACCIDENT_Det.aspx?TID=" + grvList.SelectedDataKey[0].ToString() + "&URL=" + Session["Url"].ToString());
    //}
}

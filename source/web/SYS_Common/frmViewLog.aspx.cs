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

public partial class yw_ztgl_frmViewLog :PageBaseList
{
     protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!Page.IsPostBack)
        {
            ViewState["CurPageIndex"] = 0;
            ViewState["PageCount"] = 0;
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());

            ViewState["BaseSql"] = "select * from DMIS_SYS_LOG";
            //模块的查询条件，一般是按年、月、日查询；此变量在“检索”按钮中修改，在此初始化。
            ViewState["BaseQuery"] = "to_char(OPT_TIME,'YYYYMM')='" + DateTime.Now.ToString("yyyyMM") + "'";
            if (Session["Orders"] == null)   //平台中没有设置排序条件
                ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by OPT_TIME desc";
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
                if (Session["BaseQuery"] != null)  //页面自带查询条件
                    ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["CustomOrder"];
                else   //无查询条件
                    ViewState["sql"] = ViewState["BaseSql"] + " order by " + Session["CustomOrder"];
                GridViewBind();
                return;
            }
            //新增时，关闭新增窗口，控制刷新此页面
            if (Session["NeedRefesh"] != null && Session["NeedRefesh"].ToString() == "是")
            {
                GridViewBind();
                Session["NeedRefesh"] = "否";
                return;
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ViewState["sql"] = "select * from DMIS_SYS_LOG where to_char(OPT_TIME,'YYYYMM')='" + uwcMonth.Month + "' order by OPT_TIME desc";
        GridViewBind();
    }

    /// <summary>
    /// GridView控件的数据绑定过程
    /// </summary>
    protected override void GridViewBind()
    {
        //DataTable dt = DBOpt.dbHelper.GetDataTable(ViewState["sql"].ToString());
        int rows, pageCount;
        rows = 0;
        pageCount = 0;
        DataTable dt = DBOpt.dbHelper.GetPagedDataTable(Convert.ToInt16(ViewState["CurPageIndex"]), grvList.PageSize, ref pageCount, ref rows, ViewState["sql"].ToString());
        ViewState["PageCount"] = pageCount;
        if (dt == null)
        {
            tdPageMessage.InnerText = "GridView控件的SQL语句有误！";
            return;
        }
        grvList.DataSource = dt;
        grvList.DataBind();
        grvList.SelectedIndex = -1;
        if (tdPageMessage != null)
        {
            if (dt.Rows.Count == 0)
                tdPageMessage.InnerText = "";
            else
                tdPageMessage.InnerText =  (Convert.ToInt16(ViewState["CurPageIndex"]) + 1).ToString() + "/" + pageCount.ToString() + " " + rows.ToString() + " ";
        }
    }

    protected override void NavigateToPage(object sender, EventArgs e)
    {
        string pageInfo = ((Button)sender).CommandName.ToLower();
        switch (pageInfo)
        {
            case "first":
                ViewState["CurPageIndex"] = 0;
                break;
            case "prev":
                if (Convert.ToInt16(ViewState["CurPageIndex"]) > 0)
                    ViewState["CurPageIndex"] = Convert.ToInt16(ViewState["CurPageIndex"]) - 1;
                break;
            case "next":
                if (Convert.ToInt16(ViewState["CurPageIndex"]) < (Convert.ToInt16(ViewState["PageCount"]) - 1))
                    ViewState["CurPageIndex"] = Convert.ToInt16(ViewState["CurPageIndex"]) + 1;
                break;
            case "last":
                ViewState["CurPageIndex"] = (Convert.ToInt16(ViewState["PageCount"]) - 1) < 0 ? 0 : (Convert.ToInt16(ViewState["PageCount"]) - 1);
                break;
            case "go":
                if (txtPageNumber.Text == "") { return; }
                int num;
                if (!int.TryParse(txtPageNumber.Text, out num))
                {
                    txtPageNumber.Text = "";
                    return;
                }
                if (num > Convert.ToInt16(ViewState["PageCount"]))
                {
                    txtPageNumber.Text = Convert.ToInt16(ViewState["PageCount"]).ToString();
                    ViewState["CurPageIndex"] = Convert.ToInt16(ViewState["PageCount"]) - 1;
                }
                else
                {
                    ViewState["CurPageIndex"] = num - 1;
                }
                break;
            default:
                ViewState["CurPageIndex"] = 0;
                break;
        }
        GridViewBind();
    }

    protected override void SortGridView(string sortExpression, string direction)
    {
        int rows, pageCount;
        rows = 0;
        pageCount = 0;
        DataTable dt = DBOpt.dbHelper.GetPagedDataTable(Convert.ToInt16(ViewState["CurPageIndex"]), grvList.PageSize, ref pageCount, ref rows, ViewState["sql"].ToString());
        DataView dv = new DataView(dt);
        dv.Sort = sortExpression + " " + direction;
        grvList.DataSource = dv;
        grvList.DataBind();
    }

}

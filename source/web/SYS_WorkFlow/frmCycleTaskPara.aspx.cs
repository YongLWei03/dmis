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

public partial class SYS_WorkFlow_frmCycleTaskPara : PageBaseList
{
    private string _sql;
    private object obj;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!Page.IsPostBack)
        {
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());


            //开始查询
            ViewState["BaseSql"] = "select * from DMIS_SYS_WK_CYCLE_TASK_PARA";
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
                return;
            }
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmCycleTaskPara_Det.aspx?TID=&URL=" + Session["URL"].ToString());
    }
    

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            JScript.Alert(this.Page, "请先选择要删除的记录！");
            return;
        }
        
        //再删除文档基本信息
        _sql = "delete from DMIS_SYS_WK_CYCLE_TASK_PARA where TID=" + grvList.SelectedDataKey.Value;
        DBOpt.dbHelper.ExecuteSql(_sql);
        GridViewBind();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            JScript.Alert(this.Page, "请先选择要修改的记录！");
            return;
        }
        Response.Redirect("frmCycleTaskPara_Det.aspx?TID=" + grvList.SelectedDataKey[0].ToString() + "&URL=" + Session["URL"].ToString());
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text != "")
            {
                _sql = "select F_NAME from DMIS_SYS_PACKTYPE where F_NO=" + e.Row.Cells[2].Text;
                obj = DBOpt.dbHelper.ExecuteScalar(_sql);
                if (obj != null) e.Row.Cells[2].Text = obj.ToString();
            }
        }
    }

}

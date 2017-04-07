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

public partial class YW_DD_frmDD_ShiftList :PageBaseList
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
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();
            lblFuncName.Text = Session["FuncName"].ToString();


            ViewState["BaseSql"] = "select * from T_DD_SHIFT";
            ViewState["BaseQuery"] = "to_char(DATEM,'YYYYMM')='" + DateTime.Now.ToString("yyyyMM") + "'";
            if (Session["Orders"] == null)   //平台中没有设置排序条件
                ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"];
            else
                ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["Orders"];
            GridViewBind();
            Session["CustomOrder"] = null;

            //只有管理员可以修改状态
            if(!SetRight.IsAdminitrator(Session["MemberID"].ToString()))
            {
                grvList.Columns[9].Visible = false;
            }
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
        ViewState["BaseQuery"] = "to_char(DATEM,'YYYYMM')='" +uwcMonth.Month + "'";
        if (Session["Orders"] == null)   //平台中没有设置排序条件
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"];
        else
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["Orders"];

        GridViewBind();
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[8].Text == "0")
            {
                e.Row.Cells[8].Text = "已值";
            }
            else if (e.Row.Cells[8].Text == "1")
            {
                e.Row.Cells[8].Text = "当值";
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Row.Cells[8].Text = "未值";
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Blue;
            }

            _sql = "select SHIFT_NAME from T_DD_SHIFT_PARA where TID=" + e.Row.Cells[2].Text;
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj != null)
                e.Row.Cells[2].Text = obj.ToString();
            else
                e.Row.Cells[2].Text = "";


            if (!SetRight.IsAdminitrator(Session["MemberID"].ToString()))
            {
                grvList.Columns[9].Visible = false;
            }
            else
            {
                LinkButton modButton = (LinkButton)e.Row.Cells[9].Controls[0];
                modButton.Attributes.Add("onclick", "return confirm('确认要修改状态吗?');");
            }
        }
    }
    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ModifyFlag")   //"已值  ->  当值  ->  未值"   这样的循环修改状态
        {
            int index = Convert.ToInt16(e.CommandArgument);
            if (grvList.Rows[index].Cells[8].Text == "已值")   //已值改为当值
                _sql = "update T_DD_SHIFT set flag=1 where TID=" + grvList.DataKeys[index][0].ToString();
            else if (grvList.Rows[index].Cells[8].Text == "当值")  //当值改为未值
                _sql = "update T_DD_SHIFT set flag=0 where TID=" + grvList.DataKeys[index][0].ToString();
            else    //未值改为已值
                _sql = "update T_DD_SHIFT set flag=2 where TID=" + grvList.DataKeys[index][0].ToString();

            if (DBOpt.dbHelper.ExecuteSql(_sql) < 1)
                JScript.Alert("ERROR!");
            else
                GridViewBind();
        }
    }
}

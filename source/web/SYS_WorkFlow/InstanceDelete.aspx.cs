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

public partial class SYS_WorkFlow_InstanceDelete : PageBaseList
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

            ViewState["BaseSql"] = "SELECT F_NO,F_PACKNAME,F_DESC,F_CREATEMAN,F_PACKTYPENO,F_MSG,f_createdate FROM DMIS_SYS_PACK";

            initPackType();
            btnSearch_Click(null, null);
        }
    }

    private void initPackType()
    {
        //还没有利用特办的权限
        _sql = "select distinct a.f_no,a.f_name from DMIS_SYS_PACKTYPE a,DMIS_SYS_RIGHTS b where a.F_NO=b.f_foreignkey and b.f_catgory='业务' and f_roleno in(" + Session["RoleIDs"].ToString() + ") ";
        DataTable packType = DBOpt.dbHelper.GetDataTable(_sql);
        ddlPackType.DataTextField = "f_name";
        ddlPackType.DataValueField = "f_no";
        ddlPackType.DataSource = packType;
        ddlPackType.DataBind();
        if (packType.Rows.Count > 0) ddlPackType.SelectedIndex = 0;
    }


    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row;
        if (!int.TryParse(e.CommandArgument.ToString(), out row)) return;

        int PackNo;            //业务编号
        PackNo = Convert.ToInt16(grvList.DataKeys[row].Values[0]);
        if (e.CommandName == "FlowTable")  //流程
        {
            Session["Oper"] = 0;
            Session["sended"] = "0";
            Response.Redirect("FlowTable.aspx?InstanceID=" + PackNo + @"&BackUrl=" + Page.Request.RawUrl);
        }
        else if (e.CommandName == "Del")   //删除
        {
            WebWorkFlow.DeletePack(PackNo, Session["MemberName"].ToString());
            GridViewBind();
        }
    }


    protected override void btnSearch_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();

        BaseCond.Append(" where ");
        //选定的某个业务查询
        if (ddlPackType.SelectedItem != null && ddlPackType.SelectedItem.Value != "0")
            BaseCond.Append(" f_packtypeno=" + ddlPackType.SelectedValue);

        //模糊查询某个工作任务,暂时取消
        //if (txtTaskDesc.Text.Trim() != "")   
        //{
        //    BaseCond.Append(" and a.f_desc like '%" + txtTaskDesc.Text + "%'");
        //}

        //加排序条件
        BaseCond.Append(" order by f_createdate desc");

        ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();
        GridViewBind();
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btn = (LinkButton)e.Row.Cells[1].Controls[0];
            btn.Attributes.Add("onclick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "WkConfirmDelete").ToString() + "');");
            btn.Enabled = true;
        }
    }

    /// <summary>
    /// GridView控件的数据绑定过程
    /// </summary>
    protected override void GridViewBind()
    {
        DataTable flow = DBOpt.dbHelper.GetDataTable(ViewState["sql"].ToString());
        if (flow == null)
        {
            tdPageMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "GridViewSqlErrorMessage").ToString();
            return;
        }
        grvRef.DataSource = flow;
        grvRef.DataBind();
        grvRef.SelectedIndex = -1;
        if (tdPageMessage != null)
        {
            if (flow.Rows.Count == 0)
                tdPageMessage.InnerText = "";
            else
                tdPageMessage.InnerText = tdPageMessage.InnerText = (String)GetGlobalResourceObject("WebGlobalResource", "PageNumber") + (grvRef.PageIndex + 1).ToString() + "/" + grvRef.PageCount.ToString() + " " + (String)GetGlobalResourceObject("WebGlobalResource", "Records") + flow.Rows.Count.ToString();
        }
    }
}

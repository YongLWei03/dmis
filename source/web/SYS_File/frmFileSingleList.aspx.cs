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
using System.IO;
using System.Globalization;

public partial class SYS_File_frmFileSingleList : PageBaseList
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
            lblFuncName.Text = Session["FuncName"].ToString();
            btnDelete.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "DeleteBeforeConfirm").ToString() + "');");
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());

            //文档路径
            if (Session["OtherPara"] != null)
            {
                if (Session["OtherPara"].ToString().IndexOf("path") > -1)
                {
                    int startPos = Session["OtherPara"].ToString().IndexOf("path=");
                    int endPos = Session["OtherPara"].ToString().IndexOf(';');
                    if (endPos > 0)
                        Session["FilePath"] = Session["OtherPara"].ToString().Substring(startPos + 5, endPos - startPos - 5);
                    else
                        Session["FilePath"] = Session["OtherPara"].ToString().Substring(startPos + 5);
                }
                else
                {
                    JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "FileNoPath").ToString());//"此功能没有设置文件存放路径，请联系管理员设置！"
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    btnModify.Enabled = false;
                    return;
                }
            }
            else
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "FileNoPath").ToString());//"此功能没有设置文件存放路径，请联系管理员设置！"
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnModify.Enabled = false;
                return;
            }

            ViewState["BaseSql"] = "select * from T_FILE_SINGLE";
            ViewState["BaseQuery"] = "MODULE_ID=" + Session["FuncId"].ToString();

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

    protected override void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmFileSingleNew.aspx?URL=" + Session["URL"].ToString());
    }

    protected override  void btnDelete_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "DeleteMessage").ToString());//"请先选择要删除的记录！"
            return;
        }

        //先删除文件
        string fileName = grvList.Rows[grvList.SelectedIndex].Cells[5].Text;
        string mapname = Page.MapPath(Session["FilePath"].ToString());
        if (File.Exists(mapname + fileName))
            File.Delete(mapname + fileName);


        //再删除文档基本信息
        _sql = "delete from T_FILE_SINGLE where TID=" + grvList.SelectedDataKey.Value;
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
        Response.Redirect("frmFileSingleNew.aspx?TID=" + grvList.SelectedDataKey[0].ToString() + "&URL=" + Session["URL"].ToString());
    }

    //由于文件管理的特殊性,对加了前提条件
    protected override void btnSearch_Click(object sender, EventArgs e)
    {
        if (Session["MainTableId"] == null || Session["MainTableId"].ToString().Trim() == "")
        {
            JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "NoTableID"));
            return;
        }
        JScript.OpenWindow("../SYS_Common/frmSearchBuild.aspx?Precondition=MODULE_ID=" + Session["FuncID"].ToString(),
            (String)GetGlobalResourceObject("WebGlobalResource", "SearchBuildWindow"), 
            "resizable=1,scrollbars=1,width=700px,height=500px,left=100px,top=10px");
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlf = (HyperLink)e.Row.Cells[6].Controls[1];
            if (e.Row.Cells[5].Text.Trim() != "" && e.Row.Cells[5].Text.Trim() != "&nbsp;")
                hlf.NavigateUrl = Session["FilePath"].ToString() + HttpUtility.UrlEncode(e.Row.Cells[5].Text);
            else
                hlf.Visible = false;
        }
    }


}

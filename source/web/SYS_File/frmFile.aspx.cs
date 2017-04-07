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


public partial class frmFile :  PageBaseList
{
    string _sql;
    DataTable _dt;

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

            //开始查询
            ViewState["BaseSql"] = "select * from T_FILE_INFO";
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


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ////为了防止在上传页面保存时出现错误，在此先添加上传文件基本信息　
        //uint maxID;
        //maxID = DBOpt.dbHelper.GetMaxNum("T_FILE_INFO", "TID");
        //_sql = "insert into T_FILE_INFO(TID,MODULE_ID,DESCR,NEW_DATE,MEMBER) values(" + maxID + "," + Session["FuncId"].ToString() + ",' ','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" + Session["MemberName"].ToString() + "')";
        //if (DBOpt.dbHelper.ExecuteSql(_sql) == -1)
        //{
        //    JScript.Alert(Page, "插入数据失败，请联系管理员！");
        //    return;
        //}
        Response.Redirect("frmFileNew.aspx?URL=" + Session["URL"].ToString());
    }
    

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "DeleteMessage").ToString());//"请先选择要删除的记录！"
            return;
        }
        //先删除附件
        _sql = "select * from T_FILE_ACCESSORIES where FILE_ID=" + grvList.SelectedDataKey.Value;
        _dt = DBOpt.dbHelper.GetDataTable(_sql);
        for (int i = 0; i < _dt.Rows.Count; i++)
        {
            string path, fileName, mapname;
            path = _dt.Rows[i]["FILE_PATH"].ToString();
            fileName = _dt.Rows[i]["FILE_NAME"].ToString();
            _sql = "delete from T_FILE_ACCESSORIES where TID=" + _dt.Rows[i]["TID"].ToString(); 
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
            {
                mapname = Page.MapPath(path);
                if (File.Exists(mapname + fileName))
                {
                    File.Delete(mapname + fileName);
                }
            }
        }
        //再删除文档基本信息
        _sql = "delete from T_FILE_INFO where TID=" + grvList.SelectedDataKey.Value;
        DBOpt.dbHelper.ExecuteSql(_sql);
        GridViewBind();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "ModifyMessage").ToString()); //"请先选择要修改的记录！"
            return;
        }
        Response.Redirect("frmFileNew.aspx?TID=" + grvList.SelectedDataKey[0].ToString() + "&URL=" + Session["URL"].ToString());
    }

    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            grvList.SelectedIndex = Convert.ToInt16(e.CommandArgument);
            Session["FileTid"] = grvList.SelectedDataKey.Value;
            object obj=DBOpt.dbHelper.ExecuteScalar("select count(*) from T_FILE_ACCESSORIES where FILE_ID="+Session["FileTid"].ToString());
            if (obj==null || Convert.ToInt16(obj)==0)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "FileNoAccessories").ToString()); //"没有上传的附件！"
                return;
            }
            else
            {
                string tt = "window.open('FileViewMainFrame.htm','FileViewMainFrame','scrollbars=1,resizable=1');";
                Response.Write("<script language=javascript>");
                Response.Write(tt);
                Response.Write("</script>");
            }
        }
    }

    //由于文件管理的特殊性,加了前提条件。
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
}

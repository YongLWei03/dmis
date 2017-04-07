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
using System.Threading;

public partial class SYS_WorkFlow_NewTask : System.Web.UI.Page
{
    string _sql;

    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dt;
            dt = Convert.ToDateTime("01-12-2010 12:22");


            //_sql = "select distinct a.f_no,a.f_name from dmis_sys_packtype a,dmis_sys_rights b where " +
            //    "a.f_no=b.f_foreignkey and b.f_catgory='业务' and b.f_roleno in(" + Session["RoleIDs"].ToString() + ") and substr(f_access,3,1)='1'";
            if (Session["UICulture"] != null && Session["UICulture"].ToString() == "es-ES")
                _sql = "select f_no,OTHER_LANGUAGE_DESCR f_name from dmis_sys_packtype order by f_no";
            else
                _sql = "select f_no,f_name from dmis_sys_packtype order by f_no";
            DataTable packType = DBOpt.dbHelper.GetDataTable(_sql);
            grvList.DataSource = packType;
            grvList.DataBind();

            Session["sended"] = null;   //防止第二次添加时,不弹出派工任务窗口(AssignTaskWindow.aspx)

            //默认选中第一项
            ImageButton img;
            for (int i = 0; i < grvList.Rows.Count; i++)
            {
                img = (ImageButton)grvList.Rows[i].Cells[0].Controls[0];
                if (img.Visible)
                {
                    grvList.SelectedIndex = i;
                    grvList_SelectedIndexChanged(null, null);
                    break;
                }
            }
        }
    }


    protected void grvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0) return;
        ImageButton img;
        for (int i = 0; i < grvList.Rows.Count; i++)
        {
            img = (ImageButton)grvList.Rows[i].Cells[0].Controls[0];
            if (grvList.SelectedIndex == i)
                img.ImageUrl = "~/img/selected.gif";
            else
                img.ImageUrl = "~/img/unselected.gif";
        }
        //grvList.SelectedRow.Cells[0].Text = "<img border=0 align=absmiddle src=../img/Selected.gif>";
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0) return;

        int PackTypeNo = Convert.ToInt16(grvList.SelectedDataKey[0]);
        //查找起始节点编号
        int CurLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packtypeno=" + PackTypeNo + " and f_flowcat=0"));
        
        //找开始环节对应的文档
        DataTable docType = DBOpt.dbHelper.GetDataTable("select a.f_no,a.f_formfile,a.f_tablename,a.f_target from dmis_sys_doctype a,DMIS_SYS_WK_LINK_DOCTYPE b where a.f_no=b.F_DOCTYPENO and a.f_packtypedef=1 and a.f_packtypeno="
            + PackTypeNo + " and b.F_LINKNO=" + CurLinkNo);
        if (docType == null || docType.Rows.Count < 1)
        {
            JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkNoDoc").ToString());
            return;
        }
        Session["Oper"] = 1;
        Response.Redirect(docType.Rows[0][1].ToString() + "?" + @"BackUrl=" + Page.Request.RawUrl+
            "&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo);
        //System.Text.StringBuilder tt = new System.Text.StringBuilder();
        //tt.Append("<script language=javascript>\r\n");
        //tt.Append("window.location='" + docType.Rows[0][1].ToString() + "?" + @"BackUrl=" + Page.Request.RawUrl);
        //tt.Append("&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo + "'\r\n");
        //tt.Append("</script>");
        //Response.Write(tt.ToString());
    }
    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int counts = 0;
        ImageButton img;
        object obj;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            img = (ImageButton)e.Row.Cells[0].Controls[0];
            if (Session["UICulture"] != null && Session["UICulture"].ToString() == "es-ES")
                obj = DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_packtype where OTHER_LANGUAGE_DESCR='" + e.Row.Cells[1].Text + "'");
            else
                obj = DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_packtype where f_name='" + e.Row.Cells[1].Text + "'");
            
            if (obj == null)
            {
                img.Visible = false;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                _sql = "select count(*) from dmis_sys_rights where f_foreignkey=" + obj.ToString() + " and f_catgory='业务' and f_roleno in(" + Session["RoleIDs"].ToString() + ") and substr(f_access,3,1)='1'";
                counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql));
                if (counts == 0)
                {
                    img.Visible = false;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Gray;
                }
            }
        }
    }
}

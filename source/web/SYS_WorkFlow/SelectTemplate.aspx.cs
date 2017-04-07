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

public partial class SYS_WorkFlow_SelectTemplate : System.Web.UI.Page
{
    string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _sql = "select distinct a.f_no,a.f_name from dmis_sys_packtype a,dmis_sys_rights b where "+
                "a.f_no=b.f_foreignkey and b.f_catgory='业务' and b.f_roleno in(" + Session["RoleIDs"].ToString() + ") and substr(f_access,3,1)='1'";
            DataTable packType = DBOpt.dbHelper.GetDataTable(_sql);
            grvList.DataSource = packType;
            grvList.DataBind();
        }
    }


    protected void grvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0) return;
        grvList.SelectedRow.Cells[0].Text = "<img border=0 align=absmiddle src=../../img/Selected.gif>";
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0) return;

        int PackTypeNo = Convert.ToInt16(grvList.SelectedDataKey[0]);
        //查找起始节点编号
        int CurLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packtypeno=" + PackTypeNo + " and f_flowcat=0"));
        //找对应的文档，目前的设计思想只能对应一个文档
        DataTable docType = DBOpt.dbHelper.GetDataTable("select f_no,f_formfile,f_tablename,f_target from dmis_sys_doctype where f_packtypedef=1 and f_packtypeno=" + PackTypeNo);
        if (docType == null || docType.Rows.Count < 1)
        {
            JScript.Alert("无法找到相应的文档！");
            return;
        }
       // int DocTypeNo = Convert.ToInt16(docType.Rows[0][0]);
        System.Text.StringBuilder tt = new System.Text.StringBuilder();
        tt.Append("<script language=javascript>\r\n");
        tt.Append("window.opener.location='" + docType.Rows[0][1].ToString() + "?" + @"BackUrl=../SYS_WorkFLow/CurrentTask.aspx");
        tt.Append("&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo+"'\r\n");
        tt.Append("self.close();\r\n");
        tt.Append("</script>");
        Session["Oper"] = 1;
        Response.Write(tt.ToString());
    }

}

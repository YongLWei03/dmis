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
//赤几  安全活动记录
public partial class YW_GL_frmGL_SAFE_ACTIVITY : PageBaseList
{
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
            hcbYear.Text = DateTime.Now.Year.ToString();
            ViewState["BaseSql"] = "select * from " + Session["TableName"];
            //模块的查询条件，一般是按年、月、日查询；此变量在“检索”按钮中修改，在此初始化。
            btnQuery_Click(null, null);
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
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!SetRight.IsAdminitrator(Session["MemberID"].ToString()))
            ViewState["BaseQuery"] = "to_char(DATEM,'YYYY')='" + hcbYear.Text + "' and DEPART_ID=" + Session["DepartID"].ToString();
        else
            ViewState["BaseQuery"] = "to_char(DATEM,'YYYY')='" + hcbYear.Text + "'";

        if (Session["Orders"] == null)   //平台中没有设置排序条件
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"];
        else
            ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["Orders"];

        GridViewBind();
    }

    //根据部门ID条件过滤。
    protected override void btnSearch_Click(object sender, EventArgs e)
    {
        if (Session["MainTableId"] == null || Session["MainTableId"].ToString().Trim() == "")
        {
            JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "NoTableID"));
            return;
        }
        if (!SetRight.IsAdminitrator(Session["MemberID"].ToString()))
        {
            JScript.OpenWindow("../SYS_Common/frmSearchBuild.aspx?Precondition=DEPART_ID=" + Session["DepartID"].ToString(),
                (String)GetGlobalResourceObject("WebGlobalResource", "SearchBuildWindow"),
                "resizable=1,scrollbars=1,width=700px,height=500px,left=100px,top=10px");
        }
        else
        {
            JScript.OpenWindow("../SYS_Common/frmSearchBuild.aspx",
                    (String)GetGlobalResourceObject("WebGlobalResource", "SearchBuildWindow"),
                    "resizable=1,scrollbars=1,width=700px,height=500px,left=100px,top=10px");
        }
    }
}

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
//事故预想记录
public partial class YW_GL_frmGL_ACCIDENT_FORECAST_Det : PageBaseDetail
{
    protected void Page_Load(object sender, EventArgs e)
    {
        info = detail_info;

        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetDetailPageControlLocalizationText();
            object res = GetLocalResourceObject("PageResource1.Title");
            if (res != null) lblFuncName.Text = res.ToString();

            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());

            if (Request["TID"] != "")
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, Session["TableName"].ToString(), "TID=" + Request["TID"]);
            else
            {
                wdlDATEM.setTime(DateTime.Now);
                txtDEPART_ID.Text = Session["DepartID"].ToString();
            }
        }
    }
}

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
//赤几 电网事故记录
public partial class YW_DD_frmDD_POWER_ACCIDENT_Det : PageBaseDetail
{
    protected void Page_Load(object sender, EventArgs e)
    {
        info = detail_info;
        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetDetailPageControlLocalizationText();
            lblFuncName.Text = GetLocalResourceObject("PageResource1.Title").ToString();

            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
            FillDropDownList.FillHtmlCombxByTable(ref hcbWEATHER, "DMIS_SYS_WEATHER", "NAME", "TID", "ORDER_ID");
            FillDropDownList.FillHtmlCombxByTable(ref hcbSTATION, "DMIS_SYS_STATION", "NAME", "TID", "ORDER_ID");

            if (Request["TID"] != "")
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, Session["TableName"].ToString(), "TID=" + Request["TID"]);
            else
            {
                txtDISPATCHER.Text = Session["MemberName"].ToString();
                wdlSTARTTIME.setTime(DateTime.Now);
                wdlENDTIME.setTime(DateTime.Now);
            }
        }
    }
}

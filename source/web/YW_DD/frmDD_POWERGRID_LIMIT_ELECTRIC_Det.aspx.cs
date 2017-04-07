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
//赤几 电网限电记录
public partial class YW_DD_frmDD_POWERGRID_LIMIT_ELECTRIC_Det : PageBaseDetail
{
    private string _sql;

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

            FillDropDownList.FillHtmlCombxByTable(ref hcbSTATION, "DMIS_SYS_STATION", "NAME", "TID", "ORDER_ID");
            if (Request["TID"] != "")
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, Session["TableName"].ToString(), "TID=" + Request["TID"]);
            else
            {
                txtSTARTER.Text = Session["MemberName"].ToString();
                wdlSTARTTIME.setTime(DateTime.Now);
                wdlENDTIME.setTime(DateTime.Now);
                txtRESTORE_STARTER.Text = Session["MemberName"].ToString();
            }
        }
    }

    protected override void btnSave_Click(object sender, EventArgs e)
    {
        DateTime start, end;
        float loads;
        TimeSpan ts;

        start = wdlSTARTTIME.getTime();
        end = wdlENDTIME.getTime();
        ts = end - start;
        if (float.TryParse(txtLOADS.Text, out loads) && ts.TotalHours>0)
        {
            txtSTOP_HOURS.Text = ts.TotalHours.ToString();
            txtLOSSES.Text = (loads * ts.TotalHours).ToString();
        }
        base.btnSave_Click(sender, e);
    }
}

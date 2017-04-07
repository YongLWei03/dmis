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
using System.Globalization;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.Threading;
public partial class frmModiyfPassword : System.Web.UI.Page
{

    string _sql;
    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["MemberID"] == null) JScript.ReturnLogin(this.Page);

        if (Session["THEME"] == null)
            Page.Theme = "default";
        else
            Page.Theme = Session["THEME"].ToString();

    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (txtOldPassword.Text == "")
        {
            tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "ComModiyfPassword001").ToString();  //要翻译
            return;
        }

        if (txtNewPassword.Text == "")
        {
            tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "ComModiyfPassword002").ToString();  //要翻译
            return;
        }

        if (txtAgain.Text == "")
        {
            tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "ComModiyfPassword003").ToString(); //要翻译
            return;
        }

        if(txtNewPassword.Text!=txtAgain.Text)
        {
            tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "ComModiyfPassword004").ToString(); //要翻译
            return;
        }

        _sql = "select PASSWORD from DMIS_SYS_MEMBER where ID=" + Session["MemberID"].ToString();
        Object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        if (obj == null)
        {
            return;
        }
        if (obj.ToString() != txtOldPassword.Text)
        {
            tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "ComModiyfPassword005").ToString(); //要翻译
            return;
        }

        _sql = "update DMIS_SYS_MEMBER set PASSWORD='" + txtNewPassword.Text + "' where ID=" + Session["MemberID"].ToString();
        if (DBOpt.dbHelper.ExecuteSql(_sql) > -1)
        {
            tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "ComModiyfPassword006").ToString(); //要翻译
        }
        else
        {
            tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "ComModiyfPassword007").ToString(); //要翻译
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtOldPassword.Text = "";
        txtNewPassword.Text = "";
        txtAgain.Text = "";
    }
}

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
using PlatForm.Functions;


public partial class uSelectMonth : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["uDatem"] == null || Request["uDatem"].Trim() == "")
                txtMonth.Text = DateTime.Now.ToString("MM-yyyy");
            else
            {
                DateTime dt = Convert.ToDateTime(Request["uDatem"]);
                txtMonth.Text = dt.ToString("MM-yyyy");
            }
        }
    }


    protected void imbPerMonth_Click(object sender, ImageClickEventArgs e)
    {
        if (txtMonth.Text == "") return;
        DateTime dt;

        if (!DateTime.TryParse(txtMonth.Text,out dt))
        {
            //JScript.Alert("月份格式不对！");
            return;
        }
        txtMonth.Text = dt.AddMonths(-1).ToString("MM-yyyy");
    }


    protected void imbCurMonth_Click(object sender, ImageClickEventArgs e)
    {
        txtMonth.Text = DateTime.Now.ToString("MM-yyyy");
    }
    protected void imbNextMonth_Click(object sender, ImageClickEventArgs e)
    {
        if (txtMonth.Text == "") return;
        DateTime dt;

        if (!DateTime.TryParse(txtMonth.Text, out dt))
        {
            //JScript.Alert(this.Page, "月份格式不对！");
            return;
        }
        txtMonth.Text = dt.AddMonths(1).ToString("MM-yyyy");
    }

    /// <summary>
    /// 返回格式yyyy-MM
    /// </summary>
    public string Month
    {
        get
        {
            DateTime dt;
            if (DateTime.TryParse(txtMonth.Text, out dt))
            {
                return dt.ToString("yyyyMM");
            }
            else
            {
                return "";
            }
        }
        set
        {
            DateTime dt;
            if (DateTime.TryParse(value, out dt))
            {
                txtMonth.Text = dt.ToString("MM-yyyy");
            }
            else
            {
                txtMonth.Text = "";
            }
        }
    }


 }

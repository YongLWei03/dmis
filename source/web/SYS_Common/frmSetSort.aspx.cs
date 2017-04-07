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

using System.Text;
using System.Drawing;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.Globalization;
using System.Threading;

public partial class frmSetSort : System.Web.UI.Page
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


        if (!Page.IsPostBack)
        {
            ///Session["SortBackURL"]=Request["URL"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable dtSelect = new DataTable();
            System.Type typeString = System.Type.GetType("System.String");
            DataColumn dcOrder = new DataColumn("ORDER", typeString);
            dtSelect.Columns.Add(dcOrder);
            DataColumn dcName = new DataColumn("NAME", typeString);
            dtSelect.Columns.Add(dcName);
            DataColumn dcDESCR = new DataColumn("DESCR", typeString);
            dtSelect.Columns.Add(dcDESCR);
            DataColumn dcOrderType = new DataColumn("ORDER_TYPE", typeString);
            dtSelect.Columns.Add(dcOrderType);
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dcOrder;
            dtSelect.PrimaryKey = keys;
            ViewState["dtSelect"] = dtSelect;

            BuildGridView();
        }
    }


    void BuildGridView()
    {
        if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
            _sql = "select NAME,DESCR from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " order by ORDER_ID";
        else
            _sql = "select NAME,OTHER_LANGUAGE_DESCR DESCR from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " order by ORDER_ID";
        DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
        grvColumns.DataSource = dt;
        grvColumns.DataBind();
    }

    protected void grvColumns_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grvColumns.SelectedIndex < 0) return;
        grvColumns.SelectedRow.Cells[0].Text = "<img border=0 align=absmiddle src=../img/Selected.gif>";
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        StringBuilder sorts = new StringBuilder();
        for (int i = 0; i < ((DataTable)ViewState["dtSelect"]).Rows.Count; i++)
        {
            if (((DataTable)ViewState["dtSelect"]).Rows[i]["ORDER_TYPE"].ToString() == GetGlobalResourceObject("WebGlobalResource", "ComSetSort001").ToString())//要翻译
            {
                sorts.Append(((DataTable)ViewState["dtSelect"]).Rows[i]["NAME"].ToString() + " ASC,");
            }
            else
            {
                sorts.Append(((DataTable)ViewState["dtSelect"]).Rows[i]["NAME"].ToString() + " DESC,");
            }
        }
        if (sorts.Length < 1)
        {
            JScript.Alert(this.Page, GetGlobalResourceObject("WebGlobalResource", "ComSetSort003").ToString());//要翻译
            return;
        }

        else
        {
            sorts.Remove(sorts.Length - 1, 1);  //除去最后一个,
            //refreshPage
            Session["CustomOrder"] = sorts.ToString();
            JScript.CloseWin("refreshPage"); 
            //JScript.RefreshParent(Session["Url"].ToString());
            //sorts.Remove(sorts.Length - 1, 1);  //除去最后一个,
            //Session["SortBackURL"] = Session["SortBackURL"].ToString() + "&SortOrders=" + sorts.ToString();   //利用此传递参数在返回页中确定是从左边的树形菜单找开的页面，还是从排序返回的页面。
            //Response.Redirect(Session["SortBackURL"].ToString());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        JScript.CloseWindow();
        //Session["CustomOrder"] = null;
       // Response.Redirect(Session["SortBackURL"].ToString());
    }

    protected void grvSelectedColumns_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grvSelectedColumns.SelectedIndex < 0) return;
        grvSelectedColumns.SelectedRow.Cells[0].Text = "<img border=0 align=absmiddle src=../img/Selected.gif>";
    }

    protected void grvSelectedColumns_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Order")
        {
            string name = grvSelectedColumns.Rows[Convert.ToInt16(e.CommandArgument)].Cells[1].Text;
            DataRow dr = ((DataTable)ViewState["dtSelect"]).Rows.Find(name);
            if (dr["ORDER_TYPE"].ToString() == GetGlobalResourceObject("WebGlobalResource", "ComSetSort001").ToString())//要翻译
                dr["ORDER_TYPE"] = GetGlobalResourceObject("WebGlobalResource", "ComSetSort002").ToString();//要翻译
            else
                dr["ORDER_TYPE"] = GetGlobalResourceObject("WebGlobalResource", "ComSetSort001").ToString();//要翻译

            DataView dv = ((DataTable)ViewState["dtSelect"]).DefaultView;
            dv.Sort = "ORDER ASC";
            grvSelectedColumns.DataSource = dv;
            grvSelectedColumns.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (grvColumns.SelectedIndex < 0) return;

        //不允许加相同列
        bool isExist = false;
        string name = grvColumns.SelectedValue.ToString();
        for (int i = 0; i < ((DataTable)ViewState["dtSelect"]).Rows.Count; i++)
        {
            if (name == ((DataTable)ViewState["dtSelect"]).Rows[i]["NAME"].ToString())
            {
                isExist = true;
                break;
            }
        }
        if (isExist) return;

        DataRow dr = ((DataTable)ViewState["dtSelect"]).NewRow();
        string[] values = new string[4] { ((DataTable)ViewState["dtSelect"]).Rows.Count.ToString("00"), grvColumns.SelectedValue.ToString(), grvColumns.SelectedRow.Cells[1].Text, GetGlobalResourceObject("WebGlobalResource", "ComSetSort001").ToString() };
        dr.ItemArray = values;
        ((DataTable)ViewState["dtSelect"]).Rows.Add(dr);
        DataView dv = ((DataTable)ViewState["dtSelect"]).DefaultView;
        dv.Sort = "ORDER ASC";
        grvSelectedColumns.DataSource = dv;
        grvSelectedColumns.DataBind();
    }


    protected void btnDel_Click(object sender, EventArgs e)
    {
        if (grvSelectedColumns.SelectedIndex < 0) return;

        string name = grvSelectedColumns.SelectedValue.ToString();
        DataRow dr = ((DataTable)ViewState["dtSelect"]).Rows.Find(name);
        ((DataTable)ViewState["dtSelect"]).Rows.Remove(dr);

        for (int i = 0; i < ((DataTable)ViewState["dtSelect"]).Rows.Count; i++)
        {
            ((DataTable)ViewState["dtSelect"]).Rows[i]["ORDER"] = i.ToString("00");
        }
        DataView dv = ((DataTable)ViewState["dtSelect"]).DefaultView;
        dv.Sort = "ORDER ASC";
        grvSelectedColumns.DataSource = dv;
        grvSelectedColumns.DataBind();
        grvSelectedColumns.SelectedIndex = -1;
    }


    protected void btnUp_Click(object sender, EventArgs e)
    {
        if (grvSelectedColumns.SelectedIndex <= 0) return;   //没有选中或第一条不处理

        int preIndex = grvSelectedColumns.SelectedIndex - 1;
        string preName = grvSelectedColumns.Rows[preIndex].Cells[1].Text;
        DataRow drPre = ((DataTable)ViewState["dtSelect"]).Rows.Find(preName);
        string name = grvSelectedColumns.SelectedValue.ToString();
        DataRow dr = ((DataTable)ViewState["dtSelect"]).Rows.Find(name);
        dr["ORDER"] = "temp";
        drPre["ORDER"] = name;
        dr["ORDER"] = preName;
        DataView dv = ((DataTable)ViewState["dtSelect"]).DefaultView;
        dv.Sort = "ORDER ASC";
        grvSelectedColumns.DataSource = dv;
        grvSelectedColumns.DataBind();
        grvSelectedColumns.SelectedIndex = -1;
    }

    protected void btnDown_Click(object sender, EventArgs e)
    {
        if (grvSelectedColumns.SelectedIndex < 0) return;   //没有选中不处理
        if (grvSelectedColumns.SelectedIndex == grvSelectedColumns.Rows.Count - 1) return; //最后一条不处理

        int NextIndex = grvSelectedColumns.SelectedIndex + 1;
        string NextName = grvSelectedColumns.Rows[NextIndex].Cells[1].Text;
        DataRow drPre = ((DataTable)ViewState["dtSelect"]).Rows.Find(NextName);
        string name = grvSelectedColumns.SelectedValue.ToString();
        DataRow dr = ((DataTable)ViewState["dtSelect"]).Rows.Find(name);
        dr["ORDER"] = "temp";
        drPre["ORDER"] = name;
        dr["ORDER"] = NextName;
        DataView dv = ((DataTable)ViewState["dtSelect"]).DefaultView;
        dv.Sort = "ORDER ASC";
        grvSelectedColumns.DataSource = dv;
        grvSelectedColumns.DataBind();
        grvSelectedColumns.SelectedIndex = -1;
    }
}

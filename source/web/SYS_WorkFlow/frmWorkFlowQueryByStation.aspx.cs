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

public partial class YW_ZDH_frmWorkFlowQueryByStation : PageBaseList
{
    private string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!Page.IsPostBack)
        {
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
            iniTree();
            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.Year.ToString() + "-01-01"));
            wdlEnd.setTime(DateTime.Now);
        }
    }

    private void iniTree()
    {
        trvStation.Nodes.Clear();
        DataTable dtStation = DBOpt.dbHelper.GetDataTable("select NAME from T_STATION_TYPE order by NO");
        for (int i = 0; i < dtStation.Rows.Count; i++)
        {
            TreeNode tdStation = new TreeNode();
            tdStation.Text = dtStation.Rows[i][0].ToString();
            tdStation.ImageUrl = "~/img/n_b61.gif";
            trvStation.Nodes.Add(tdStation);
        }
    }

    protected void trvStation_SelectedNodeChanged(object sender, EventArgs e)
    {
        btnSearch_Click(null, null);
    }
    protected override void btnSearch_Click(object sender, EventArgs e)
    {
        if (trvStation.SelectedNode == null)
        {
            JScript.Alert("请先选择某一厂站！");
            return;
        }
        if (wdlStart.getTime() > wdlEnd.getTime())
        {
            JScript.Alert("开始日期不能大于结束日期！");
            return;
        }
        string startDate, endDate;
        startDate = wdlStart.getTime().ToString("yyyy-MM-dd")+" 00:00";
        endDate = wdlEnd.getTime().ToString("yyyy-MM-dd")+" 23:59";

        ViewState["sql"]="select * from DMIS_SYS_PACK where f_createdate>='" + startDate + "' and f_createdate<='" + endDate +
            "' and f_msg='" + trvStation.SelectedNode.Text + "' order by F_PACKNAME,f_createdate";
        GridViewBind();
    }

    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row;
        if (!int.TryParse(e.CommandArgument.ToString(), out row)) return;

        if (e.CommandName == "Query")
        {
            int RecNo;             //记录编号
            int maxWorkFlowNo;
            int LinkNo;
            string TableName;
            string url;

            int PackNo = Convert.ToInt16(grvList.DataKeys[row].Values[0]);
            int PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            //找当前业务的最大工作流编号
            _sql = "select max(f_no) from dmis_sys_workflow where f_packno=" + PackNo;
            maxWorkFlowNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql));

            _sql = "select f_flowno from dmis_sys_workflow where f_no=" + maxWorkFlowNo;
            LinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql));

            _sql = "select f_tablename,f_recno,f_doctypeno from DMIS_SYS_DOC where F_PACKNO=" + PackNo + " and f_linkno=" + LinkNo;
            DataTable doc = DBOpt.dbHelper.GetDataTable(_sql);
            if (doc == null || doc.Rows.Count < 1)
            {
                JScript.Alert("无法找到相应的文档！");
                return;
            }
            TableName = doc.Rows[0][0].ToString();
            RecNo = Convert.ToInt16(doc.Rows[0][1]);
            url = DBOpt.dbHelper.ExecuteScalar("select f_formfile from dmis_sys_doctype where f_no=" + doc.Rows[0][2].ToString()).ToString();

            Session["Oper"] = 0;
            Session["sended"] = 0;
            Response.Redirect(url + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                "&PackTypeNo=" + PackTypeNo + "&TableName=" + TableName);

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected override void btnSaveExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=result.xls");
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grvList.AllowPaging = false;
        grvList.AllowSorting = false;
        GridViewBind();
        grvList.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());
        Response.End();

        grvList.AllowPaging = true;
        grvList.AllowSorting = true;
        GridViewBind();
    }

}

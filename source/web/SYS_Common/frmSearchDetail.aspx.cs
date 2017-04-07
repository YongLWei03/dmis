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
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Threading;
public partial class frmSearchDetail : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            if (Session["MemberID"] == null)
            {
                Response.Write("<scr" + "ipt language=javascript>alert('" + GetGlobalResourceObject("WebGlobalResource", "ComSearchDetail001").ToString() + "')</scr" + "ipt>");//要翻译，可能已经有了
                return;
            }
            if (Request["TID"] != null)
            {
                Session["SearchDetailWhere"] = "TID=" + Request["TID"];
            }

            lbName.Text = Session["FuncName"].ToString();
            string [] count=Session["TableIds"].ToString().Split(',');
            LoadTable();

            if (count.Length == 2)
            {
                //LoadDetail1();
            }
            else if (count.Length == 3)
            {
                //LoadDetail1();
                //LoadDetail2();
            }
            else
            {
            }
        }		
    }


    private void LoadTable()  //装主表的数据
    {
        if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
            _sql = "select NAME,DESCR,ISPRIMARY from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " order by ORDER_ID";
        else
            _sql = "select NAME,OTHER_LANGUAGE_DESCR DESCR,ISPRIMARY from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " order by ORDER_ID";
        DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);

        System.Text.StringBuilder columnName = new System.Text.StringBuilder();
        string[] columnDesc = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            columnName.Append(dt.Rows[i][0].ToString());
            columnName.Append(",");
            columnDesc[i] = dt.Rows[i][1].ToString();
        }
        columnName.Remove(columnName.Length - 1, 1);
        _sql = "select " + columnName.ToString() + " from " + Session["TableName"].ToString() + " where " + Session["SearchDetailWhere"];
        DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
        try
        {
            dr.Read();
            for (int j = 0; j < dr.FieldCount; j++)
            {
                TableCell colDesc = new TableCell();
                colDesc.Text = columnDesc[j];
                colDesc.BackColor = Color.LightGray;
                colDesc.Wrap = false;
                colDesc.HorizontalAlign = HorizontalAlign.Center;
                colDesc.Width = new Unit("30%");
                TableCell colValue = new TableCell();
                if (dr[j] == Convert.DBNull)
                    colValue.Text = "/";
                else
                    colValue.Text = dr[j].ToString();
                colValue.HorizontalAlign = HorizontalAlign.Left;

                TableRow row = new TableRow();
                row.Cells.Add(colDesc);
                row.Cells.Add(colValue);
                tbMain.Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            dr.Close();
            dr.Dispose();
        }
        dr.Close();
    }







}

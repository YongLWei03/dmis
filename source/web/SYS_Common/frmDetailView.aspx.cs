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
using System.Globalization;
using System.Threading;
public partial class frmDetailView : System.Web.UI.Page
{
    string _sql;
    DataTable _dtColumnHead,_dtColumnValue;

    
    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if(Session["MemberID"]==null) JScript.ReturnLogin(Page);

        if (!Page.IsPostBack)
        {
            
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _sql = "select NAME from DMIS_SYS_TREEMENU where ID=" + Session["ID"].ToString();
            tdTitle.InnerText = DBOpt.dbHelper.ExecuteScalar(_sql).ToString();

            string[] tableID;
            string tableIDs = Session["TABLE_IDS"].ToString().Trim();
            tableID = tableIDs.Split(',');

            for (int i = 0; i < tableID.Length; i++)
            {
                if (i == 0)
                    BuildMainTable(tableID[0]);
                else
                    BuildSecondTable(tableID[i]);
            }

        }
    }

    private void BuildMainTable(string id)
    {
        _sql = "select NAME from DMIS_SYS_TABLES where ID=" + id;
        string tableName = DBOpt.dbHelper.ExecuteScalar(_sql).ToString();

        _sql = "select NAME,DESCR from DMIS_SYS_COLUMNS where TABLE_ID=" + id + " order by ORDER_ID";
        _dtColumnHead = DBOpt.dbHelper.GetDataTable(_sql);

        string columnName = "";
        string[] columnDesc = new string[_dtColumnHead.Rows.Count];

        for (int i = 0; i < _dtColumnHead.Rows.Count; i++)
        {
            columnName = columnName + _dtColumnHead.Rows[i][0] + ",";
            columnDesc[i] = _dtColumnHead.Rows[i][1].ToString();
        }
        columnName = columnName.Substring(0, columnName.Length - 1);

        _sql = "select " + columnName + " from " + tableName + "  where TID=" + Request["TID"];
       
        DbDataReader dr = DBOpt.dbHelper.GetDataReader(_sql);
        if (dr.Read())
        {
            for (int j = 0; j < dr.FieldCount; j++)
            {
                TableCell colDesc = new TableCell();
                colDesc.Text = columnDesc[j];
                colDesc.CssClass = "detailtd";

                TableCell colValue = new TableCell();
                if (dr[j] == Convert.DBNull)
                    colValue.Text = "/";
                else
                    colValue.Text = dr[j].ToString();
                colValue.CssClass = "controltd";

                TableRow row = new TableRow();
                row.Cells.Add(colDesc);
                row.Cells.Add(colValue);
                tabMainTable.Rows.Add(row);
            }
        }
        dr.Close();
    }

    private void BuildSecondTable(string id)
    {
        
    }


}

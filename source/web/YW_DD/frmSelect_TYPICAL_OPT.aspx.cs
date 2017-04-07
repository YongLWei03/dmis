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

public partial class YW_DD_frmSelect_TYPICAL_OPT : PageBaseList
{
    private string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!Page.IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();

            FillDropDownList.FillByTable(ref ddlStation, "DMIS_SYS_STATION", "NAME", "TID", "ORDER_ID");
            ddlStation.SelectedIndex = 1;
            ddlStation_SelectedIndexChanged(null, null);
        }
    }

    protected void ddlStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStation.SelectedIndex < 0) return;
        _sql = "select * from T_DD_TYPICAL_OPT_HEAD where STATION='" + ddlStation.SelectedItem.Text + "'";
        ViewState["sql"] = _sql;
        GridViewBind();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            //JScript.Alert("请先选择某一典型票!");
            return;
        }
        int no = 1;
        uint maxTid, maxBodyTid;
        _sql = "select count(*) from T_DD_TERMWISE_OPT_HEAD where to_char(DATEM,'YYYY')='" + DateTime.Now.ToString("yyyy") + "'";
        int counts = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(_sql)) + 1;
        string ph = DateTime.Now.ToString("yyyy") + counts.ToString("0000");

        maxTid = DBOpt.dbHelper.GetMaxNum("T_DD_TERMWISE_OPT_HEAD", "TID");
        maxBodyTid = DBOpt.dbHelper.GetMaxNum("T_DD_TERMWISE_OPT_BODY", "TID");
        _sql = "insert into T_DD_TERMWISE_OPT_HEAD(TID,TASK,DATEM,YPR,PH,STATION) values(" + maxTid.ToString() + ",'" + grvList.SelectedRow.Cells[2].Text + "'," +
              "TO_DATE('" + DateTime.Now.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),'" + Session["MemberName"].ToString() + "','" + ph + "','" + ddlStation.SelectedItem.Text + "')";

        if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
        {
            DataTable dt = DBOpt.dbHelper.GetDataTable("select * from T_DD_TYPICAL_OPT_BODY where HEAD_TID=" + grvList.SelectedDataKey[0].ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _sql = "insert into T_DD_TERMWISE_OPT_BODY(HEAD_TID,TID,UNIT,XH,CONTENT) values(" + maxTid.ToString() + "," + maxBodyTid.ToString() + ",'"  +
                     dt.Rows[i]["UNIT"].ToString() + "'," + no.ToString() + ",'" + dt.Rows[i]["CONTENT"].ToString() + "')";
                if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
                {
                    maxBodyTid++;
                    no++;
                }
            }
        }

        ////从界面上无法知道是否从典型票中，只能从数据库中来判断
        ////如果选择了典型票，则T_DD_TERMWISE_OPT_HEAD有一条新记录，但PACK_NO没有值。
        ////需要取值此票，马上把相应的PACK_NO赋上，再显示此票
        //string tid, pack_no;
        //_sql = "select max(TID) from T_DD_TERMWISE_OPT_HEAD";
        //object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        //if (obj == null) return;
        //tid = obj.ToString();
        //_sql = "select PACK_NO from T_DD_TERMWISE_OPT_HEAD where TID=" + tid;
        //obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        //if (obj != null) return; //有业务编号，则不处理。

        //显示票内容
        Session["RecNo"] = maxTid;
        //CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_DD_TERMWISE_OPT_HEAD", "TID=" + ViewState["RecNo"].ToString());
        //initGridView();//取操作步骤
        JScript.CloseWin("refreshPage");
    }
}

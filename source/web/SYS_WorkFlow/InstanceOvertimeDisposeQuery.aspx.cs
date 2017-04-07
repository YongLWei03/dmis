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

public partial class SYS_WorkFlow_InstanceOvertimeDisposeQuery : PageBaseList
{
    private string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.Year.ToString() + "-01-01"));
            wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
            initPackType();

            ViewState["BaseSql"] = "select A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,"
                         + "B.F_FLOWNAME,B.f_receiver,B.f_finishdate,B.F_SENDER,B.F_SENDDATE,B.F_FLOWNO,A.F_PACKTYPENO,B.F_NO as F_CurWorkFlowNo,A.F_MSG,B.F_PLANDAY,b.f_workday,B.f_last_finished_time,B.F_STATUS "
                         + " from DMIS_SYS_PACK A,DMIS_SYS_WORKFLOW B ";

            System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();
            BaseCond.Append(" where B.F_PACKNO=A.F_NO");
            BaseCond.Append(" and ((B.f_finishdate>B.f_last_finished_time) or ((B.f_finishdate is null or B.f_finishdate='') and B.f_last_finished_time<to_char(sysdate,'YYYY-MM-DD HH24:MI')))");

            //业务
            BaseCond.Append(" and A.f_packtypeno in(" + ViewState["packTypeIds"].ToString() + ")");

            //加排序条件
            BaseCond.Append(" order by B.f_last_finished_time desc");
            ViewState["sql"] = ViewState["BaseSql"] + BaseCond.ToString();

            GridViewBind();
        }
    }

    private void initPackType()
    {
        //没有加权限,都可以浏览,通过菜单的可见性来设置
        _sql = "select f_no,f_name from DMIS_SYS_PACKTYPE";
        DataTable packType = DBOpt.dbHelper.GetDataTable(_sql);
        System.Text.StringBuilder packTypeId = new System.Text.StringBuilder();
        for (int i = 0; i < packType.Rows.Count; i++)
        {
            packTypeId.Append(packType.Rows[i][0].ToString() + ",");
        }
        ViewState["packTypeIds"] = packTypeId.Remove(packTypeId.Length - 1, 1).ToString();  //只能处理有权限的业务

        CheckBox ckb;
        dlsPackType.DataSource = packType;
        dlsPackType.DataBind();
        foreach (DataListItem item in dlsPackType.Items)
        {
            ckb = (CheckBox)item.Controls[1];
            ckb.Checked = true;
        }
    }

    protected override void btnSearch_Click(object sender, EventArgs e)
    {
        if (wdlStart.getTime() > wdlEnd.getTime())
        {
            JScript.Alert("起始日期不能晚于终止日期！");
            return;
        }

        //选定的某个业务查询
        System.Text.StringBuilder packType = new System.Text.StringBuilder();
        CheckBox ckb;
        foreach (DataListItem item in dlsPackType.Items)
        {
            ckb = (CheckBox)item.Controls[1];
            if (ckb.Checked) packType.Append(ckb.ToolTip + ",");
        }
        if (packType.Length < 1)
        {
            JScript.Alert("查询时至少要选择一个业务类别！");
            return;
        }
        ViewState["packTypeIds"] = packType.Remove(packType.Length - 1, 1);

        System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();
        BaseCond.Append(" where B.F_PACKNO=A.F_NO");
        BaseCond.Append(" and ((B.f_finishdate>B.f_last_finished_time) or ((B.f_finishdate is null or B.f_finishdate='') and B.f_last_finished_time<to_char(sysdate,'YYYY-MM-DD HH24:MI')))");

        //日期
        BaseCond.Append(" and substr(b.f_last_finished_time,1,10)>='" + wdlStart.getTime().ToString("yyyy-MM-dd") + "' and substr(b.f_last_finished_time,1,10)<='" + wdlEnd.getTime().ToString("yyyy-MM-dd") + "' ");

        //业务
        BaseCond.Append(" and A.f_packtypeno in(" + ViewState["packTypeIds"].ToString() + ")");

        if (ddlStatus.Text != "全部")
            BaseCond.Append(" and B.F_STATUS='" + ddlStatus.Text + "'");

        //加排序条件
        BaseCond.Append(" order by B.f_last_finished_time desc");

        ViewState["sql"] = ViewState["BaseSql"] + BaseCond.ToString();

        GridViewBind();
    }

    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row;
        if (!int.TryParse(e.CommandArgument.ToString(), out row)) return;

        if (e.CommandName == "FlowTable")  //流程
        {
            Response.Redirect("FlowTable.aspx?InstanceID=" + grvList.DataKeys[row].Value.ToString() + @"&BackUrl=" + Page.Request.RawUrl);
        }
        else if (e.CommandName == "Deal")   //办理
        {
            object obj;
            int RecNo;             //记录编号
            int DocTypeNo;         //文档类型号
            int PackTypeNo;        //业务类型编号
            int PackNo;
            int CurLinkNo;         //当前环节号
            int CurWorkFlowNo;     //工作流编号 dmis_sys_workflow表中的f_no值
            PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[2]);
            PackNo = Convert.ToInt16(grvList.DataKeys[row].Value);
            CurLinkNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            CurWorkFlowNo = Convert.ToInt16(grvList.DataKeys[row].Values[3]);

            _sql = "select f_recno from DMIS_SYS_DOC where F_PACKNO=" + PackNo + " and F_LINKNO=" + CurLinkNo;
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj == null)
                RecNo = -1;
            else
                RecNo = Convert.ToInt16(obj);

            DataTable docType = DBOpt.dbHelper.GetDataTable("select a.f_no,a.f_formfile,a.f_tablename,a.f_target from dmis_sys_doctype a,DMIS_SYS_WK_LINK_DOCTYPE b where a.f_no=b.F_DOCTYPENO and b.f_packtypeno="
                + PackTypeNo + " and b.F_LINKNO=" + CurLinkNo);
            if (docType == null || docType.Rows.Count < 1)
            {
                JScript.Alert("无法找到相应的文档！");
                return;
            }
            DocTypeNo = Convert.ToInt16(docType.Rows[0][0]);
            Session["sended"] = "0";
            Session["Oper"] = "0"; //不允许修改数据
            Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                "&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo + "&PackNo=" + PackNo + "&CurWorkFlowNo=" + CurWorkFlowNo);
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

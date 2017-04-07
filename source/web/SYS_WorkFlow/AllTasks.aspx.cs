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

public partial class SYS_WorkFlow_AllTasks : PageBaseList
{
    private string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();
            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.Year.ToString() + "-01-01"));
            wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));

            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusAll").ToString(), "0"));  //全部
            ddlStatus.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusWorking").ToString(), "1"));  //在办
            ddlStatus.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusFinish").ToString(), "2"));  //结案
            ddlStatus.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusSuspend").ToString(), "3"));  //挂起
            ddlStatus.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "WkStatusAbolish").ToString(), "4"));  //作废
            ddlStatus.SelectedIndex = 0;

            FillDropDownList.FillByTable(ref ddlSTATION, "DMIS_SYS_STATION", "NAME");

            ViewState["BaseSql"] = "SELECT * from DMIS_SYS_PACK a ";
            //显示所有的数据
            ViewState["sql"] = ViewState["BaseSql"].ToString() + " order by a.f_createdate desc ";  //主办者
            GridViewBind();

            //btnSearch_Click(null, null);
        }
    }

    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row;
        if (!int.TryParse(e.CommandArgument.ToString(), out row)) return;

        if (e.CommandName == "FlowTable")  //流程
        {
            Response.Redirect("FlowTable.aspx?InstanceID=" + grvList.DataKeys[row].Value.ToString() + @"&BackUrl=" + Page.Request.RawUrl);
        }
        else if (e.CommandName == "Deal")   //查看
        {
            object obj=null;
            int RecNo;             //记录编号
            int DocTypeNo;         //文档类型号
            int PackTypeNo;        //业务类型编号
            int CurLinkNo;         //当前环节号
            int PackNo;            //当前业务号
            int CurWorkFlowNo;     //工作流编号 dmis_sys_workflow表中的f_no值

            PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            PackNo = Convert.ToInt16(grvList.DataKeys[row].Value);
            if (grvList.Rows[row].Cells[6].Text == GetGlobalResourceObject("WebGlobalResource", "WkStatusFinish").ToString())  //结案
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packtypeno=" + PackTypeNo + " and f_flowcat=2"); //结案的找结束环节
            }
            else if (grvList.Rows[row].Cells[6].Text == GetGlobalResourceObject("WebGlobalResource", "WkStatusWorking").ToString())  //正常
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select f_flowno from dmis_sys_workflow where f_packno=" + PackNo + " and f_status='1'");
            }
            else if (grvList.Rows[row].Cells[6].Text == GetGlobalResourceObject("WebGlobalResource", "WkStatusAbolish").ToString()) //作废
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packtypeno=" + PackTypeNo + " and f_flowcat=0");  //作废的找起始环节
            }
            else if (grvList.Rows[row].Cells[6].Text == GetGlobalResourceObject("WebGlobalResource", "WkStatusSuspend").ToString()) //挂起
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packtypeno=" + PackTypeNo + " and f_status='1'");  //作废的找起始环节
            } 
            if (obj == null)
            {
                //JScript.Alert("无法找到业务表的环节号！");
                return;
            }
            CurLinkNo = Convert.ToInt16(obj);
            //最后一个环节的工作流号
            CurWorkFlowNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_workflow where f_packno=" + PackNo + " and f_flowno=" + CurLinkNo));
            //最后一个环节对应的业务表中的记录号
            _sql = "select f_recno from DMIS_SYS_DOC where F_PACKNO=" + PackNo + " and F_LINKNO=" + CurLinkNo;
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj == null)
            {
                //JScript.Alert("无法找到业务表的记录编号！");
                return;
            }
            RecNo = Convert.ToInt16(obj);

            DataTable docType = DBOpt.dbHelper.GetDataTable("select a.f_no,a.f_formfile,a.f_tablename,a.f_target from dmis_sys_doctype a,DMIS_SYS_WK_LINK_DOCTYPE b where a.f_no=b.F_DOCTYPENO and b.f_packtypeno="
                    + PackTypeNo + " and b.F_LINKNO=" + CurLinkNo);
            if (docType == null || docType.Rows.Count < 1)
            {
                JScript.Alert(GetGlobalResourceObject("WebGlobalResource", "WkNoDoc").ToString());
                return;
            }
            Session["sended"] = "0";
            Session["Oper"] = "0"; //不允许修改数据
            Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                "&PackTypeNo=" + PackTypeNo + "&PackNo=" + PackNo + "&CurLinkNo=" + CurLinkNo + "&CurWorkFlowNo=" + CurWorkFlowNo);
        }
    }

    protected override void btnSearch_Click(object sender, EventArgs e)
    {
        if (wdlStart.getTime() > wdlEnd.getTime()) return;

        System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();
        System.Text.StringBuilder members = new System.Text.StringBuilder();
        DataTable names;

        BaseCond.Append(" WHERE ");

        //日期范围
        BaseCond.Append(" TO_DATE(a.f_createdate,'DD-MM-YYYY HH24:MI')>=TO_DATE('" + wdlStart.getTime().ToString("dd-MM-yyyy") + " 00:00','DD-MM-YYYY HH24:MI') and TO_DATE(a.f_createdate,'DD-MM-YYYY HH24:MI')<=TO_DATE('" + wdlEnd.getTime().ToString("dd-MM-yyyy") + " 23:59','DD-MM-YYYY HH24:MI') ");

        //状态
        if (ddlStatus.SelectedIndex != 0)
            BaseCond.Append(" and  A.F_STATUS='" + ddlStatus.SelectedItem.Value+"'");
            
        //厂站
        if (ddlSTATION.SelectedItem != null && ddlSTATION.SelectedItem.Text != "")
            BaseCond.Append(" and a.f_msg='" + ddlSTATION.SelectedItem.Text + "'");

        //模糊查询某个工作任务
        if (txtTaskDesc.Text.Trim() != "")
            BaseCond.Append(" and a.f_desc like '%" + txtTaskDesc.Text + "%'");

        //加排序条件
        BaseCond.Append(" order by a.f_createdate desc");

        ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();  //主办者
        GridViewBind();
    }

 
    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int packNo;
            DataTable workingFlow;
            string flowName, receiver;
            flowName = "";
            receiver = "";

            if (e.Row.Cells[6].Text == "" || e.Row.Cells[6].Text == "&nbsp;" || e.Row.Cells[6].Text == "2")  //结案状态
            {
                e.Row.Cells[6].Text = GetGlobalResourceObject("WebGlobalResource", "WkStatusFinish").ToString();
                e.Row.Cells[7].Text = "";
                e.Row.Cells[8].Text = "";
            }
            else if (e.Row.Cells[6].Text == "1")   //在办
            {
                e.Row.Cells[6].Text = GetGlobalResourceObject("WebGlobalResource", "WkStatusWorking").ToString();
                if (int.TryParse(e.Row.Cells[e.Row.Cells.Count - 1].Text, out packNo))
                {
                    workingFlow = DBOpt.dbHelper.GetDataTable("select f_flowname,f_receiver from dmis_sys_workflow where f_packno=" + packNo + " and f_status='1' order by f_receivedate");
                    for (int i = 0; i < workingFlow.Rows.Count; i++)
                    {
                        flowName = flowName + workingFlow.Rows[i][0].ToString() + ";";
                        if (workingFlow.Rows[i][1] != Convert.DBNull)
                            receiver = receiver + workingFlow.Rows[i][1].ToString() + ";";
                        else
                            receiver = receiver + " ;";

                    }
                    if (flowName.Length > 0)
                        e.Row.Cells[7].Text = flowName.Substring(0, flowName.Length - 1);
                    if (receiver.Length > 0)
                        e.Row.Cells[8].Text = receiver.Substring(0, receiver.Length - 1);
                }
            }
            else if (e.Row.Cells[6].Text == "3")  //挂起
            {
                e.Row.Cells[6].Text = GetGlobalResourceObject("WebGlobalResource", "WkStatusSuspend").ToString();
                e.Row.Cells[7].Text = "";
                e.Row.Cells[8].Text = "";
            }
            else if (e.Row.Cells[6].Text == "4")   //作废
            {
                e.Row.Cells[6].Text = GetGlobalResourceObject("WebGlobalResource", "WkStatusAbolish").ToString();
                e.Row.Cells[7].Text = "";
                e.Row.Cells[8].Text = "";
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected override void btnSaveExcel_Click(object sender, EventArgs e)
    {
        if (grvList.Rows.Count < 1) return;

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

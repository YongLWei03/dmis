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

public partial class SYS_WorkFlow_InstanceWorkingTimesQuery : PageBaseList
{
    private string _sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            ViewState["BaseSql"] = "SELECT distinct A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,A.PLAN_STARTTIME,A.PLAN_ENDTIME,"
                     + "A.F_PACKTYPENO,A.f_archivedate,A.f_createdate "
                     + " FROM DMIS_SYS_PACK A ";
            initPackType();
            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM") + "-01"));
            wdlEnd.setTime(DateTime.Now);
            btnOK_Click(null, null);
        }
    }

    private void initPackType()
    {
        //还没有利用特办的权限
        _sql = "select distinct a.f_no,a.f_name from DMIS_SYS_PACKTYPE a,DMIS_SYS_RIGHTS b where a.F_NO=b.f_foreignkey and b.f_catgory='业务' and f_roleno in(" + Session["RoleIDs"].ToString() + ") ";
        DataTable packType = DBOpt.dbHelper.GetDataTable(_sql);
        DataRow row = packType.NewRow();
        row[0] = 0;
        row[1] = "全部";
        packType.Rows.InsertAt(row, 0);
        ddlPackType.DataTextField = "f_name";
        ddlPackType.DataValueField = "f_no";
        ddlPackType.DataSource = packType;
        ddlPackType.DataBind();
        System.Text.StringBuilder packTypeId = new System.Text.StringBuilder();
        for (int i = 0; i < packType.Rows.Count; i++)
        {
            packTypeId.Append(packType.Rows[i][0].ToString() + ",");
        }
        ViewState["packTypeIds"] = packTypeId.Remove(packTypeId.Length - 1, 1).ToString();  //只能处理有权限的业务
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
            int PackTypeNo;        //业务类型编号
            int CurLinkNo;         //当前环节号
            int PackNo;            //当前业务号
            int CurWorkFlowNo;     //工作流编号 dmis_sys_workflow表中的f_no值

            PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            PackNo = Convert.ToInt16(grvList.DataKeys[row].Value);
            //当前节点是最后一个节点
            CurLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packtypeno=" + PackTypeNo + " and f_flowcat=2"));
            //最后一个环节的工作流号
            CurWorkFlowNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packno=" + PackNo + " and f_flowno=" + CurLinkNo));
            //最后一个环节对应的业务表中的记录号
            _sql = "select f_recno from DMIS_SYS_DOC where F_PACKNO=" + PackNo + " and f_linkno=" + CurLinkNo;
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj == null)
            {
                JScript.Alert("无法找到业务表的记录编号！");
                return;
            }
            RecNo = Convert.ToInt16(obj);

            DataTable docType = DBOpt.dbHelper.GetDataTable("select a.f_no,a.f_formfile,a.f_tablename,a.f_target from dmis_sys_doctype a,DMIS_SYS_WK_LINK_DOCTYPE b where a.f_no=b.F_DOCTYPENO and b.f_packtypeno="
                    + PackTypeNo + " and b.F_LINKNO=" + CurLinkNo);
            if (docType == null || docType.Rows.Count < 1)
            {
                JScript.Alert("无法找到相应的文档！");
                return;
            }
            Session["sended"] = "0";
            Session["Oper"] = "0"; //不允许修改数据
            Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                "&PackTypeNo=" + PackTypeNo + "&PackNo=" + PackNo + "&CurLinkNo=" + CurLinkNo + "&CurWorkFlowNo=" + CurWorkFlowNo);
        }
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        DateTime dt1, dt2;
        dt1 = wdlStart.getTime();
        dt2 = wdlEnd.getTime();
        if (dt1 > dt2)
        {
            JScript.Alert("开始日期不能大于结束日期");
            return;
        }
        System.Text.StringBuilder conditions = new System.Text.StringBuilder();
        conditions.Append(" WHERE A.F_STATUS='结案' ");
        conditions.Append(" and substr(A.f_createdate,1,10)>='" + dt1.ToString("yyyy-MM-dd") + "' and substr(A.f_createdate,1,10)<='" + dt2.ToString("yyyy-MM-dd") + "'");
        if (ddlPackType.SelectedItem == null || ddlPackType.SelectedIndex == 0)
            conditions.Append(" and A.f_packtypeno in(" + ViewState["packTypeIds"].ToString() + ")");
        else
            conditions.Append(" and a.f_packtypeno=" + ddlPackType.SelectedValue);
        conditions.Append(" order by A.f_archivedate desc");

        ViewState["sql"] = ViewState["BaseSql"].ToString() + conditions.ToString();
        GridViewBind();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TimeSpan ts;
            double sj = 0, jh = 0;               //实际所费时间和计划所费时间
            DateTime start, end;          //创建时间和归档时间
            DateTime jh_start, jh_end;   //计划开始时间和计划结束时间
            if (!(e.Row.Cells[5].Text == "" || e.Row.Cells[5].Text == "&nbsp;") && !(e.Row.Cells[6].Text == "" || e.Row.Cells[6].Text == "&nbsp;"))
            {
                DateTime.TryParse(e.Row.Cells[5].Text, out start);
                DateTime.TryParse(e.Row.Cells[6].Text, out end);
                if (start != null && end != null)
                {
                    ts = end - start;
                    sj = ts.TotalHours;
                    e.Row.Cells[7].Text = sj.ToString("f2");
                }
            }
            if (!(e.Row.Cells[8].Text == "" || e.Row.Cells[8].Text == "&nbsp;") && !(e.Row.Cells[9].Text == "" || e.Row.Cells[9].Text == "&nbsp;"))
            {
                DateTime.TryParse(e.Row.Cells[8].Text, out jh_start);
                DateTime.TryParse(e.Row.Cells[9].Text, out jh_end);
                if (jh_start != null && jh_end != null)
                {
                    ts = jh_end - jh_start;
                    jh = ts.TotalHours;
                    e.Row.Cells[10].Text = jh.ToString("f2");
                }
            }
            if (jh < sj && jh != 0 && sj != 0)   //超时
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}

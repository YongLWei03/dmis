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


public partial class SYS_WorkFlow_InstanceDefer : PageBaseList
{
    string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            wdlStart.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-") + "01"));
            wdlEnd.setTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
            FillDropDownList.FillByTable(ref ddlSTATION, "DMIS_SYS_STATION", "NAME");
            initPackType();

            ViewState["BaseSql"] = "SELECT distinct A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,"
                     + "B.F_FLOWNAME,B.F_SENDER,B.F_SENDDATE,B.F_RECEIVER,B.f_planday,B.f_receivedate,B.F_FLOWNO,A.F_PACKTYPENO,B.F_NO as F_CurWorkFlowNo,A.F_MSG "
                     + " FROM DMIS_SYS_PACK A,DMIS_SYS_WORKFLOW B ";

            System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();
            BaseCond.Append(" WHERE A.F_NO=B.F_PACKNO AND B.F_STATUS='1' and A.F_STATUS='" + ddlPackStatus.Text + "'");
            BaseCond.Append(" and A.f_packtypeno in(" + ViewState["packTypeIds"].ToString() + ")");
            BaseCond.Append(" order by B.F_SENDDATE desc");
            ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();
            GridViewBind();
            ViewState["refreshPage"] = 0;
        }
        else
        {
            if (ViewState["refreshPage"].ToString() != refreshPage.Value)  //要求页面刷新
            {
                btnSearch_Click(null, null);
                ViewState["refreshPage"] = refreshPage.Value;
            }
        }
    }


    private void initPackType()
    {
        //利用特办的权限来判断是否有权限操作。
        _sql = "select distinct a.f_no,a.f_name from DMIS_SYS_PACKTYPE a,DMIS_SYS_RIGHTS b where a.F_NO=b.f_foreignkey and b.f_catgory='业务' and substr(b.f_access,7,1)='1' and f_roleno in(" + Session["RoleIDs"].ToString() + ") ";
        DataTable packType = DBOpt.dbHelper.GetDataTable(_sql);
        System.Text.StringBuilder packTypeId = new System.Text.StringBuilder();
        for (int i = 0; i < packType.Rows.Count; i++)
        {
            packTypeId.Append(packType.Rows[i][0].ToString() + ",");
        }
        if (packTypeId.Length > 0)
            ViewState["packTypeIds"] = packTypeId.Remove(packTypeId.Length - 1, 1).ToString();  //只能处理有权限的业务
        else
            ViewState["packTypeIds"] = DBOpt.dbHelper.GetMaxNum("dmis_sys_packtype", "f_no");  //没有角色可以处理此业务，值不能取空，所以则取最大的业务分类号，不可能取到值，

        CheckBox ckb;
        dlsPackType.DataSource = packType;
        dlsPackType.DataBind();
        foreach (DataListItem item in dlsPackType.Items)
        {
            ckb = (CheckBox)item.Controls[1];
            ckb.Checked = true;
        }
    }

    protected void grvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row;
        if (!int.TryParse(e.CommandArgument.ToString(), out row)) return;

        if (e.CommandName == "FlowTable")  //流程
        {
            Response.Redirect("FlowTable.aspx?InstanceID=" + grvList.DataKeys[row].Value.ToString() + "&BackUrl=" + Page.Request.RawUrl);
        }
        else if (e.CommandName == "Deal")   //详细
        {
            object obj;
            int RecNo;             //记录编号
            int PackTypeNo;        //业务类型编号
            int PackNo;            //业务编号
            int CurLinkNo;         //当前环节号
            int CurWorkFlowNo;     //工作流编号 dmis_sys_workflow表中的f_no值
            PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[2]);
            CurLinkNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            CurWorkFlowNo = Convert.ToInt16(grvList.DataKeys[row].Values[3]);
            PackNo = Convert.ToInt16(grvList.DataKeys[row].Value);

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
            Session["Oper"] = 0;
            Session["sended"] = "0";
            Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo + @"&BackUrl=" + Page.Request.RawUrl +
                    "&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo + "&PackNo=" + PackNo + "&CurWorkFlowNo=" + CurWorkFlowNo);

        }
        else if (e.CommandName == "Suspend")
        {
            Response.Write("<script language=javascript>");

            if (ddlPackStatus.Text == "正常")
                Response.Write("window.open('InstanceSuspendPopMessage.aspx?InstanceID=" + grvList.DataKeys[row].Value.ToString() + "&OptType=暂缓" + "','暂缓'" +
                     ",'height=200,width=440,top=100,left=100,scrollbars=no,resizable=yes');");
            else
                Response.Write("window.open('InstanceSuspendPopMessage.aspx?InstanceID=" + grvList.DataKeys[row].Value.ToString() + "&OptType=恢复" + "','恢复'" +
                     ",'height=200,width=440,top=100,left=100,scrollbars=no,resizable=yes');");

            Response.Write("</script>");
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
        BaseCond.Append(" WHERE A.F_NO=B.F_PACKNO AND B.F_STATUS='1' and A.F_STATUS='" + ddlPackStatus.Text + "'");

        //日期
        BaseCond.Append(" and substr(b.F_SENDDATE,1,10)>='" + wdlStart.getTime().ToString("yyyy-MM-dd") + "' and substr(b.F_SENDDATE,1,10)<='" + wdlEnd.getTime().ToString("yyyy-MM-dd") + "' ");

        //厂站
        if (ddlSTATION.SelectedItem != null && ddlSTATION.SelectedItem.Text != "")
            BaseCond.Append(" and a.f_msg='" + ddlSTATION.SelectedItem.Text + "'");

        //选定的某个业务查询
        BaseCond.Append(" and A.f_packtypeno in(" + ViewState["packTypeIds"].ToString() + ")");

        //模糊查询某个工作任务
        if (txtTaskDesc.Text.Trim() != "")
            BaseCond.Append(" and a.f_desc like '%" + txtTaskDesc.Text + "%'");

        //加排序条件
        BaseCond.Append(" order by B.F_SENDDATE desc");
        ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();
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
            LinkButton btn = (LinkButton)e.Row.Cells[2].Controls[0];
            if (ddlPackStatus.Text == "正常")
            {
                btn.Text = "暂缓";
                btn.Attributes.Add("onclick", "return confirm('确定要把此业务暂缓?');");
            }
            else
            {
                btn.Text = "恢复";
                btn.Attributes.Add("onclick", "return confirm('确定要把此业务恢复?');");
            }

            if (e.Row.Cells[12].Text == "" || e.Row.Cells[12].Text == "&nbsp;")
                e.Row.Cells[12].Text = "";
            else
                e.Row.Cells[12].Text = WebWorkFlow.GetConsumeHours(Convert.ToDateTime(e.Row.Cells[12].Text), DateTime.Now).ToString("f2");
        }
    }

}

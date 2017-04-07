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
using PlatForm.DBUtility;
using System.Globalization;

public partial class SYS_WorkFlow_AssignTaskWindow : PageBaseDetail
{
    private string _sql;
    private object obj;
    websvrfunction webfun = new websvrfunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Request["PackNo"] != null)
                ViewState["PackNo"] = Request["PackNo"];
            else
                Response.Write("<script language=javascript>returnvalue='';self.close();</script>");

            if (Request["PackTypeNo"] != null)
                ViewState["PackTypeNo"] = Request["PackTypeNo"];
            if (Request["CurLinkNo"] != null)
                ViewState["CurLinkNo"] = Request["CurLinkNo"];
            if (Request["CurWorkFlowNo"] != null)
                ViewState["CurWorkFlowNo"] = Request["CurWorkFlowNo"];
            if (Request["TableName"] != null)
                ViewState["TableName"] = Request["TableName"];
            if (Request["RecNo"] != null)
                ViewState["RecNo"] = Request["RecNo"];

            initTache();
            BuildTable();
        }
    }

    private void initTache()
    {
        DataTable dt;

        //任务名称
        obj = DBOpt.dbHelper.ExecuteScalar("select f_desc from DMIS_SYS_PACK where F_NO=" + ViewState["PackNo"].ToString());
        if (obj != null) tdPackName.InnerText = obj.ToString();
        
        //当前环节
        obj = DBOpt.dbHelper.ExecuteScalar("select f_flowname from dmis_sys_workflow where f_packno=" + ViewState["PackNo"].ToString() + " and f_flowno=" + ViewState["CurLinkNo"].ToString());
        if (obj != null) tdCurrentTacheName.InnerText = obj.ToString();

        //列出本环节下面的所有步骤，
        //如果是退回的情况下，有的环节发送了，故只显示还没有发送（即退回分支的环节）
        _sql = "select F_NO,F_NAME from DMIS_SYS_FLOWLINK where F_PACKTYPENO=" + ViewState["PackTypeNo"] + " and F_NO IN ("                              
            + "select F_ENDNO from DMIS_SYS_FLOWLINE where F_PACKTYPENO=" + ViewState["PackTypeNo"] + " and  F_STARTNO=" + ViewState["CurLinkNo"] + ")";
        dt = DBOpt.dbHelper.GetDataTable(_sql);
        DataColumn[] key1 = new DataColumn[1];
        key1[0] = dt.Columns["F_NO"];
        dt.PrimaryKey = key1;

        //显示已经发送的环节.
        _sql = "select f_flowno,f_flowname from dmis_sys_workflow where f_packno=" + ViewState["PackNo"].ToString() + " and (f_status='1' or f_status='2') "
            + " and f_flowno in (select F_ENDNO from DMIS_SYS_FLOWLINE where F_PACKTYPENO=" + ViewState["PackTypeNo"] + " and  F_STARTNO=" + ViewState["CurLinkNo"] + ")";
        DataTable haveSendTache = DBOpt.dbHelper.GetDataTable(_sql);


        //去掉已经发送的分支
        DataRow row;
        for (int i = 0; i < haveSendTache.Rows.Count; i++)
        {
            row = dt.Rows.Find(haveSendTache.Rows[i][0]);
            if (row != null)
                dt.Rows.Remove(row);
        }

        //找出满足条件的环节
        string LinkNo;
        DataTable target = dt.Clone();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            LinkNo = DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowline where  F_STARTNO=" + ViewState["CurLinkNo"] + " and F_ENDNO=" + dt.Rows[i]["F_NO"].ToString()).ToString();
            if (WebWorkFlow.LinkGetCondition(ViewState["PackTypeNo"].ToString(), LinkNo, ViewState["PackNo"].ToString(),
                ViewState["CurWorkFlowNo"].ToString(), ViewState["TableName"].ToString(), ViewState["RecNo"].ToString()))
            {
                DataRow row2 = target.NewRow();
                row2[0] = dt.Rows[i]["F_NO"];
                row2[1] = dt.Rows[i]["F_NAME"];
                target.Rows.Add(row2);
            }
        }

        //再绑定
        if (target.Rows.Count > 0)
        {
            rblTache.DataTextField = "F_NAME";
            rblTache.DataValueField = "F_NO";
            rblTache.DataSource = target;
            rblTache.DataBind();
            rblTache.SelectedIndex = 0;
            rblTache_SelectedIndexChanged(null, null);
        }
    }

    //构造存放发送环节及人员人员列表的DataTable
    private void BuildTable()
    {
        DataTable result = new DataTable();
        DataColumn[] keys = new DataColumn[1];

        DataColumn dcFlowID = new DataColumn("FlowID");
        dcFlowID.DataType = System.Type.GetType("System.Int16");
        result.Columns.Add(dcFlowID);
        keys[0] = dcFlowID;  //主键列

        DataColumn dcFlowName = new DataColumn("FlowName");
        dcFlowName.DataType = System.Type.GetType("System.String");
        result.Columns.Add(dcFlowName);

        DataColumn dcFZR = new DataColumn("FZR");
        dcFZR.DataType = System.Type.GetType("System.String");
        result.Columns.Add(dcFZR);

        DataColumn dcCJRY1 = new DataColumn("CJRY");
        dcCJRY1.DataType = System.Type.GetType("System.String");
        result.Columns.Add(dcCJRY1);

        result.PrimaryKey = keys;
        ViewState["result"] = result;
    }


    private void GridViewDataBound()
    {
        grvSelectTache.DataSource = (DataTable)ViewState["result"];
        grvSelectTache.DataBind();
    }

    protected void rblTache_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTache.SelectedItem == null) return;

        _sql = "select distinct a.CODE,a.NAME from dmis_sys_member a,dmis_sys_member_role b,dmis_sys_role c where a.id=b.member_id and b.role_id=c.id ";
        _sql += " and c.id in(select F_ROLENO from DMIS_SYS_RIGHTS  where F_FOREIGNKEY=" + rblTache.SelectedValue + " AND F_CATGORY='流程角色')";
        DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);

        ddlFzr.DataTextField = "NAME";
        ddlFzr.DataValueField = "CODE";
        ddlFzr.DataSource = dt;
        ddlFzr.DataBind();

        chlWorkingPeople.DataTextField = "NAME";
        chlWorkingPeople.DataValueField = "CODE";
        chlWorkingPeople.DataSource = dt;
        chlWorkingPeople.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlFzr.SelectedItem == null)
        {
            tdErrorMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "WkSelectWorkleader").ToString();//请选择工作负责人!
            return;
        }
        DataRow row = ((DataTable)ViewState["result"]).Rows.Find(rblTache.SelectedValue);
        if (row != null)  //已经存在此环节的数据,先删除它
            ((DataTable)ViewState["result"]).Rows.Remove(row);

        System.Text.StringBuilder cjr = new System.Text.StringBuilder();   //从办人员

        //再创建此行的数据
        DataRow newRow = ((DataTable)ViewState["result"]).NewRow();
        newRow["FlowID"] = rblTache.SelectedItem.Value;
        newRow["FlowName"] = rblTache.SelectedItem.Text;
        newRow["FZR"] = ddlFzr.SelectedItem.Text;
        foreach (ListItem li in chlWorkingPeople.Items)
        {
            if (!li.Selected) continue;
            if (li.Value == ddlFzr.SelectedValue) continue;  //又选择了负责人,则不处理.
            cjr.Append(li.Text + ",");
        }
        if (cjr.Length > 0)
            newRow["CJRY"] = cjr.Remove(cjr.Length - 1, 1).ToString();
        else
            newRow["CJRY"] = "";

        ((DataTable)ViewState["result"]).Rows.Add(newRow);
        GridViewDataBound();
    }

    protected void grvSelectTache_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            DataRow row = ((DataTable)ViewState["result"]).Rows[Convert.ToInt16(e.CommandArgument)];
            ((DataTable)ViewState["result"]).Rows.Remove(row);
            GridViewDataBound();
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (((DataTable)ViewState["result"]).Rows.Count < 1)
        {
            tdErrorMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "WkMustSelectOneTache").ToString();//至少要选择一个发送的环节!
            return;
        }

        string sReceive = "";
        bool bCreated = false;
        int iWorkFlowNo = Convert.ToInt32(ViewState["CurWorkFlowNo"]);

        DataTable result = (DataTable)ViewState["result"];
        for (int i = 0; i < result.Rows.Count; i++)
        {
            if (result.Rows[i]["CJRY"] != null && result.Rows[i]["CJRY"].ToString().Trim()!="") sReceive = result.Rows[i]["CJRY"].ToString();
            
            iWorkFlowNo = Convert.ToInt32(ViewState["CurWorkFlowNo"]);  
            bCreated = WebWorkFlow.CreateFlow(Convert.ToInt32(ViewState["PackNo"]), ref iWorkFlowNo,
                        Session["MemberName"].ToString(), Convert.ToInt32(result.Rows[i]["FlowID"]), sReceive, result.Rows[i]["FZR"].ToString(), txtMessage.Text, ViewState["RecNo"].ToString());
            if (!bCreated)
            {
                tdErrorMessage.InnerText = "发送到环节：" + result.Rows[i]["FlowName"].ToString()+" 出错！";
                return;
            }
            sReceive = "";
            //统计实际工作时间
            WebWorkFlow.StatisicFactuslTimes(ViewState["PackTypeNo"].ToString(), ViewState["CurLinkNo"].ToString(),
                ViewState["PackNo"].ToString(), ViewState["CurWorkFlowNo"].ToString(), ViewState["TableName"].ToString(), ViewState["RecNo"].ToString());
        }

        Session["sended"] = 1;
        ViewState["CurWorkFlowNo"] = iWorkFlowNo;
        Response.Write(webfun.CloseWin("refreshPage"));
    }




}

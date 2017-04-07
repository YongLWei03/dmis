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

public partial class SYS_WorkFlow_CurrentTask : PageBaseList
{
    private string _sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;
        txtPageNumber = txtPage;

        if (!IsPostBack)
        {
            //2009-3-12包含从办人员的脚本
            //ViewState["BaseSql"] = "SELECT distinct A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,"
            //         + "B.F_FLOWNAME,B.f_receiver,B.f_receivedate,B.F_SENDER,B.F_SENDDATE,B.F_FLOWNO,A.F_PACKTYPENO,B.F_NO as F_CurWorkFlowNo,A.F_MSG "
            //         + " FROM DMIS_SYS_PACK A,DMIS_SYS_WORKFLOW B,DMIS_SYS_MEMBERSTATUS C ";

            ViewState["BaseSql"] = "SELECT distinct A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,"
                     + "B.F_FLOWNAME,B.f_receiver,B.f_receivedate,B.F_SENDER,B.F_SENDDATE,B.F_FLOWNO,A.F_PACKTYPENO,B.F_NO as F_CurWorkFlowNo,A.F_MSG "
                     + " FROM DMIS_SYS_PACK A,DMIS_SYS_WORKFLOW B ";
           

            //专业取处理实体是岗位时的任务列表
            ViewState["EntiBaseSql"] = "SELECT distinct A.F_NO,A.F_PACKNAME,A.F_DESC,A.F_CREATEMAN,"
                     + "B.F_FLOWNAME,B.f_receiver,B.f_receivedate,B.F_SENDER,B.F_SENDDATE,B.F_FLOWNO,A.F_PACKTYPENO,B.F_NO as F_CurWorkFlowNo,A.F_MSG "
                     + " FROM DMIS_SYS_PACK A,DMIS_SYS_WORKFLOW B ";
            
            initPackType();
            btnSearch_Click(null, null);
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
            Response.Redirect("FlowTable.aspx?InstanceID="+grvList.DataKeys[row].Value.ToString()+@"&BackUrl=" + Page.Request.RawUrl);
        }
        else if (e.CommandName == "Deal")   //办理
        {
            object obj;
            int RecNo;             //记录编号
            int DocTypeNo;         //文档类型号
            int PackTypeNo;        //业务类型编号
            int CurLinkNo;         //当前环节号
            string sRight = "";    //文档权限编码
            int CurWorkFlowNo;     //工作流编号 dmis_sys_workflow表中的f_no值
            PackTypeNo = Convert.ToInt16(grvList.DataKeys[row].Values[2]);
            CurLinkNo = Convert.ToInt16(grvList.DataKeys[row].Values[1]);
            CurWorkFlowNo = Convert.ToInt16(grvList.DataKeys[row].Values[3]);

            _sql = "select f_recno,F_DOCTYPENO from DMIS_SYS_DOC where F_PACKNO=" + grvList.DataKeys[row].Value.ToString() + " and F_LINKNO=" + CurLinkNo;
            DataTable doc = DBOpt.dbHelper.GetDataTable(_sql);
            if (doc == null || doc.Rows.Count < 1)
            {
                JScript.Alert("无法找到业务表的记录编号！");
                return;
            }
            RecNo = Convert.ToInt16(doc.Rows[0][0]);
            DataTable docType = DBOpt.dbHelper.GetDataTable("select f_no,f_formfile,f_tablename,f_target from dmis_sys_doctype where f_no=" + doc.Rows[0][1].ToString());
            if (docType == null || docType.Rows.Count<1)
            {
                JScript.Alert("无法找到相应的文档！");
                return;
            }
            DocTypeNo = Convert.ToInt16(docType.Rows[0][0]);
            sRight = WebWorkFlow.sDocTypeRight(DocTypeNo, Session["RoleIDs"].ToString());
            if (sRight != "")
            {
                Session["Oper"] = 0;
                if (sRight[2] == '1' || sRight[3] == '1') 
                    Session["Oper"] = 1;
                Session["sended"] = "0";
                Response.Redirect(docType.Rows[0][1].ToString() + "?RecNo=" + RecNo+ @"&BackUrl=" +Page.Request.RawUrl+
                    "&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo + "&PackNo=" + grvList.DataKeys[row].Value.ToString() + "&CurWorkFlowNo=" + CurWorkFlowNo);
            }
            else
            {
                Response.Write("<script language=javascript>alert('对不起！你无权操作本文档')</script>");
            }
        }
    }

    protected override void btnSearch_Click(object sender, EventArgs e)
    {

        //针对处理实体类型是岗位时，先构造条件
        System.Text.StringBuilder entiCond = new System.Text.StringBuilder();
        string[] roles = Session["RoleIDs"].ToString().Split(',');
        string temp;
        for (int i = 0; i < roles.Length; i++)
        {
            temp = "," + roles[i] + ",";
            entiCond.Append("INSTR(',' || b.enti_id || ',','" + temp + "')>0 or ");
        }

        System.Text.StringBuilder BaseCond = new System.Text.StringBuilder();
        System.Text.StringBuilder EntiCond = new System.Text.StringBuilder();
        //2009-3-12包含从办人员的脚本
        //BaseCond.Append(" WHERE A.F_NO=B.F_PACKNO AND B.F_NO=C.F_WORKFLOWNO AND A.F_NO=C.F_PACKNO and A.F_STATUS<>'结案'"
        //                + " AND ((C.F_RECEIVER='" + Session["MemberName"] + "' AND C.F_STATUS='1') or (B.F_RECEIVER='" + Session["MemberName"] + "' AND B.F_STATUS='1'))");
        BaseCond.Append(" WHERE (A.F_NO=B.F_PACKNO AND and A.F_STATUS<>'结案') "
                + " AND (B.F_RECEIVER='" + Session["MemberName"] + "' AND B.F_STATUS='1')");

        EntiCond.Append(" where A.F_NO=B.F_PACKNO and A.F_STATUS<>'结案' and B.F_STATUS='1' and "
                         + "((b.enti_type='岗位' and (b.f_receiver='' or b.f_receiver is null) and (" + entiCond.Remove(entiCond.Length - 3, 3).ToString() + ")) or "  //处理实现为岗位时，没有接收的情况
                         + "(b.enti_type='岗位' and b.f_receiver='" + Session["MemberName"].ToString() + "') or "                                                      //处理实现为岗位时，接收的情况,已经有人名了
                         + "((b.enti_type='相关环节' or b.enti_type='人员') and b.f_receiver='" + Session["MemberName"].ToString() + "'))");
        
        if (ddlPackType.SelectedItem == null || ddlPackType.SelectedIndex == 0)
        {
            BaseCond.Append(" and A.f_packtypeno in(" + ViewState["packTypeIds"].ToString() + ")");
            EntiCond.Append(" and A.f_packtypeno in(" + ViewState["packTypeIds"].ToString() + ")");
        }
        else
        {
            BaseCond.Append(" and a.f_packtypeno=" + ddlPackType.SelectedValue);
            EntiCond.Append(" and a.f_packtypeno=" + ddlPackType.SelectedValue);
        }
        if (txtTaskDesc.Text.Trim() != "")
        {
            BaseCond.Append(" and a.f_desc like '%" + txtTaskDesc.Text + "%'");
            EntiCond.Append(" and a.f_desc like '%" + txtTaskDesc.Text + "%'");
        }
        BaseCond.Append(" order by B.F_SENDDATE desc");
        EntiCond.Append(" order by B.F_SENDDATE desc");

        ViewState["sql"] = ViewState["BaseSql"].ToString() + BaseCond.ToString();
        ViewState["EntiSql"] = ViewState["EntiBaseSql"] + EntiCond.ToString();
        GridViewBind();

    }

    /// <summary>
    /// GridView控件的数据绑定过程
    /// </summary>
    protected override void GridViewBind()
    {
        DataTable flow = DBOpt.dbHelper.GetDataTable(ViewState["sql"].ToString());
        if (flow == null)
        {
            tdPageMessage.InnerText = "GridView控件的SQL语句有误！";
            return;
        }
        DataColumn[] keys = new DataColumn[2];   //一个人在一个业务中进行了两项工作，有两个完成项，故要有两列作主键
        keys[0] = flow.Columns["F_NO"];
        keys[1] = flow.Columns["F_CurWorkFlowNo"];
        flow.PrimaryKey = keys;

        DataTable flowRole = DBOpt.dbHelper.GetDataTable(ViewState["EntiSql"].ToString());

        object[] cols = new object[2];
        for (int i = 0; i < flowRole.Rows.Count; i++)
        {
            cols[0] = flowRole.Rows[i]["F_NO"];
            cols[1] = flowRole.Rows[i]["F_CurWorkFlowNo"];
            if (flow.Rows.Contains(cols)) continue;
            DataRow drow = flow.NewRow();
            for (int j = 0; j < flowRole.Columns.Count; j++)
            {
                drow[j] = flowRole.Rows[i][j];
            }
            flow.Rows.Add(drow);
        }

        DataView view = flow.DefaultView;
        view.Sort = "F_SENDDATE DESC";
        grvRef.DataSource = view;
        grvRef.DataBind();
        grvRef.SelectedIndex = -1;
        if (tdPageMessage != null)
        {
            if (flow.Rows.Count == 0)
                tdPageMessage.InnerText = "";
            else
                tdPageMessage.InnerText = "第" + (grvRef.PageIndex + 1).ToString() + "页/共" + grvRef.PageCount.ToString() + "页 记录共 " + flow.Rows.Count.ToString() + " 条";
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlPackType.SelectedItem == null || ddlPackType.SelectedIndex == 0)
        {
            JScript.Alert("请先选择要新建的业务类别");
            return;
        }
        int PackTypeNo = Convert.ToInt16(ddlPackType.SelectedValue);
        //查找起始节点编号
        int CurLinkNo = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select f_no from dmis_sys_flowlink where f_packtypeno=" + PackTypeNo + " and f_flowcat=0"));
        //找对应的文档，目前的设计思想只能对应一个文档
        DataTable docType = DBOpt.dbHelper.GetDataTable("select f_no,f_formfile,f_tablename,f_target from dmis_sys_doctype where f_packtypedef=1 and f_packtypeno=" + PackTypeNo);
        if (docType == null || docType.Rows.Count < 1)
        {
            JScript.Alert("无法找到相应的文档！");
            return;
        }
        Session["Oper"] = 1;
        Response.Redirect(docType.Rows[0][1].ToString() + "?" + @"BackUrl="+Page.Request.RawUrl+"&PackTypeNo=" + PackTypeNo + "&CurLinkNo=" + CurLinkNo);
    }

    //计算所耗小时数
    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[10].Text == "" || e.Row.Cells[10].Text == "&nbsp;")
                e.Row.Cells[10].Text = "";
            else
                e.Row.Cells[10].Text = WebWorkFlow.GetConsumeHours(Convert.ToDateTime(e.Row.Cells[10].Text), DateTime.Now).ToString("f2");
        }
    }

}

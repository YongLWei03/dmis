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

public partial class SYS_WorkFlow_FlowTable : PageBaseList
{
    string _sql;
    DataTable members;
    string temp;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["BackUrl"] != null)
                ViewState["BackUrl"] = Request["BackUrl"];

            _sql = "select f_packname,f_desc,f_packtypeno from dmis_sys_pack where f_no=" + Request["InstanceID"];
            DataTable temp = DBOpt.dbHelper.GetDataTable(_sql);
            if (temp.Rows.Count > 0)
            {
                tdPackTypeName.InnerText=temp.Rows[0][0].ToString();
                tdPackDesc.InnerText = temp.Rows[0][1].ToString();
                ViewState["PackTypeNo"] = temp.Rows[0][2];
            }
            ViewState["PackNo"]=Request["InstanceID"];

            //先从DMIS_SYS_WORKFLOW中插入数据
            _sql = "SELECT A.F_NO,A.F_PACKNO,A.F_FLOWNAME,A.F_STATUS,A.F_SENDER,A.F_SENDDATE,"
                    + "A.F_RECEIVER,A.F_RECEIVEDATE,A.F_FINISHDATE,A.F_MSG,a.f_planday,a.f_workday"
                    + " FROM DMIS_SYS_WORKFLOW A WHERE A.F_PACKNO=" + Request["InstanceID"] + " ORDER BY A.F_NO";
            DataTable wk = new DataTable();
            wk = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < wk.Rows.Count; i++)
            {
                if (wk.Rows[i]["F_RECEIVER"] is System.DBNull)   //由于F_RECEIVER是主键列，故在要先赋值。
                    wk.Rows[i]["F_RECEIVER"] = "";
            }
            DataColumn[] keys = new DataColumn[3];
            keys[0] = wk.Columns["F_PACKNO"];
            keys[1] = wk.Columns["F_NO"];
            keys[2] = wk.Columns["F_RECEIVER"];
            wk.PrimaryKey = keys;

            //用户要求显示子流程的办理过程
            _sql = "select SUBPACKNO from  DMIS_SYS_PACK " + " where f_no=" + ViewState["PackNo"].ToString();
            object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (!(obj == null || obj.ToString() == ""))
            {
                _sql = "SELECT A.F_NO,A.F_PACKNO,A.F_FLOWNAME,A.F_STATUS,A.F_SENDER,A.F_SENDDATE,"
                    + "A.F_RECEIVER,A.F_RECEIVEDATE,A.F_FINISHDATE,A.F_MSG,a.f_planday,a.f_workday"
                    + " FROM DMIS_SYS_WORKFLOW A WHERE A.F_PACKNO=" + obj.ToString() + " ORDER BY A.F_NO";
               DataTable subWk = new DataTable();
               subWk = DBOpt.dbHelper.GetDataTable(_sql);
               wk.Merge(subWk);
            }



            //然后从DMIS_SYS_MEMBERSTATUS中取数据，如果在wk中没有，则插入
            //_sql = "SELECT B.F_NO,B.F_PACKNO,B.F_FLOWNAME,A.F_STATUS,B.F_SENDER,B.F_SENDDATE,"
            //        + "A.F_RECEIVER,A.F_RECEIVEDATE,A.F_FINISHDATE,B.F_DESC,A.f_planday,A.f_workday"
            //        + " FROM DMIS_SYS_MEMBERSTATUS A, DMIS_SYS_WORKFLOW B"
            //        + " WHERE B.F_PACKNO=" + Request["InstanceID"] + " AND B.F_NO=A.F_WORKFLOWNO";

            //DataTable status = DBOpt.dbHelper.GetDataTable(_sql);
            //object[] cols = new object[3];
            //for (int i = 0; i < status.Rows.Count; i++)
            //{
            //    cols[0] = status.Rows[i]["F_PACKNO"];
            //    cols[1] = status.Rows[i]["F_NO"];
            //    cols[2] = status.Rows[i]["F_RECEIVER"];
            //    if (wk.Rows.Contains(cols)) continue;
            //    DataRow drow = wk.NewRow();
            //    for (int j = 0; j < status.Columns.Count; j++)
            //    {
            //        drow[j] = status.Rows[i][j];
            //    }
            //    wk.Rows.Add(drow);
            //}

            DataView view = wk.DefaultView;
            view.Sort = "F_PACKNO ASC,F_SENDDATE ASC";

            grvList.DataSource = view;
            grvList.DataBind();

            //原先的代码，老是有重复记录，出错。
            //_sql = "SELECT A.F_NO,A.F_PACKNO,A.F_FLOWNAME,B.F_STATUS,A.F_SENDER,A.F_SENDDATE,"
            //        + "B.F_RECEIVER,B.F_RECEIVEDATE,B.F_FINISHDATE,A.F_DESC,b.f_planday,b.f_workday"
            //        + " FROM DMIS_SYS_WORKFLOW A,DMIS_SYS_MEMBERSTATUS B "
            //        + " WHERE A.F_PACKNO=" + Request["InstanceID"] + " AND A.F_NO=B.F_WORKFLOWNO ORDER BY B.F_WORKFLOWNO";

            //DataTable flow = new DataTable();
            //flow = DBOpt.dbHelper.GetDataTable(_sql);

            //DataColumn[] keys = new DataColumn[3];
            //keys[0] = flow.Columns["F_PACKNO"];
            //keys[1] = flow.Columns["F_NO"];
            //keys[2] = flow.Columns["F_RECEIVER"];
            //flow.PrimaryKey = keys;


            ////增加发送到岗位的功能，在DMIS_SYS_MEMBERSTATUS表中无记录，故还要加入此部分的记录
            //_sql = "SELECT A.F_NO,A.F_PACKNO,A.F_FLOWNAME,A.F_STATUS,A.F_SENDER,A.F_SENDDATE,"
            //        + "A.F_RECEIVER,A.F_RECEIVEDATE,A.F_FINISHDATE,A.F_DESC,a.f_planday,a.f_workday"
            //        + " FROM DMIS_SYS_WORKFLOW A WHERE A.F_PACKNO=" + Request["InstanceID"] + " ORDER BY A.F_NO";
            //DataTable flowRole = new DataTable();
            //flowRole = DBOpt.dbHelper.GetDataTable(_sql);

            //object[] cols = new object[3];
            //for (int i = 0; i < flowRole.Rows.Count; i++)
            //{
            //    cols[0] = flowRole.Rows[i]["F_PACKNO"];
            //    cols[1] = flowRole.Rows[i]["F_NO"];
            //    if (flowRole.Rows[i]["F_RECEIVER"] is System.DBNull)   //由于F_RECEIVER是主键列，故在要先赋值。
            //    {
            //        cols[2] = "未指定";
            //        flowRole.Rows[i]["F_RECEIVER"] = "未指定";
            //    }
            //    else
            //        cols[2] = flowRole.Rows[i]["F_RECEIVER"];
            //    if (flow.Rows.Contains(cols)) continue;
            //    DataRow drow = flow.NewRow();
            //    for (int j = 0; j < flowRole.Columns.Count; j++)
            //    {
            //        drow[j] = flowRole.Rows[i][j];
            //    }
            //    flow.Rows.Add(drow);
            //}
            //DataView view = flow.DefaultView;
            //view.Sort = "F_NO ASC";

            //grvList.DataSource = view;
            //grvList.DataBind();
        }
    }

    protected void grvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //增加序号
            e.Row.Cells[0].Text = Convert.ToString(e.Row.DataItemIndex + 1);
            //增加从办人员列表
            members = DBOpt.dbHelper.GetDataTable("select f_receiver from dmis_sys_memberstatus where f_packno=" +ViewState["PackNo"].ToString()+ " and f_workflowno="+e.Row.Cells[e.Row.Cells.Count-1].Text);
            if (members != null && members.Rows.Count > 0)
            {
                temp = "(";
                for (int i = 0; i < members.Rows.Count; i++)
                {
                    temp += members.Rows[i][0].ToString() + " ";
                }
                e.Row.Cells[5].Text = e.Row.Cells[5].Text+temp.Trim()+")";
            }
        }
    }
 
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["BackUrl"].ToString());
    }
 
    protected void btnFlowChart_Click(object sender, EventArgs e)
    {
        string paras;
        paras = "PackTypeNo=" + ViewState["PackTypeNo"]  +"&PackNo=" + ViewState["PackNo"];

        Response.Write("<script language=javascript>");
        Response.Write("window.open('../SYS_WorkFlow/webFlows.aspx?" + paras + "','退回'" +
            ",'height=600,width=800,top=100,left=100,scrollbars=yes,resizable=yes');");
        Response.Write("</script>");
    }
}

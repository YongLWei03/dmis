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

public partial class SYS_WorkFlow_SelectReassignMember : System.Web.UI.Page
{
    private string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["InstanceID"] != null)
                ViewState["InstanceID"] = Request["InstanceID"];
            if (Request["CurWorkFlowNo"] != null)
                ViewState["CurWorkFlowNo"] = Request["CurWorkFlowNo"];
            if (Request["StdTacheDesc"] != null)
                txtF_FLOWNAME.Text = Request["StdTacheDesc"];
            if (Request["Member"] != null)
              txtMEMBER_NAME.Text = Request["Member"];
        
            object obj;
            obj = DBOpt.dbHelper.ExecuteScalar("select f_desc from dmis_sys_pack where f_no=" + ViewState["InstanceID"]);
            if (obj != null)
                tdPackDesc.InnerText = obj.ToString();
            else
                tdMessage.InnerText = "";   //无法等到任务描述！

            string departId=Convert.ToString(DBOpt.dbHelper.ExecuteScalar("select depart_id from dmis_view_depart_member where member_name='"+Request["Member"]+"'"));
            initDepart();
            ddlDepart.SelectedIndex = ddlDepart.Items.IndexOf(ddlDepart.Items.FindByValue(departId));
            ddlDepart_SelectedIndexChanged(null, null);
        }
    }

    private void initDepart()
    {
        _sql = "select id,name from dmis_sys_depart order by order_id";
        DataTable depart = DBOpt.dbHelper.GetDataTable(_sql);
        ddlDepart.DataTextField = "name";
        ddlDepart.DataValueField = "id";
        ddlDepart.DataSource = depart;
        ddlDepart.DataBind();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (rblMember.SelectedItem == null)
        {
            tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "ItemNotNull").ToString(); // "请选择要改派的人员！";
            return;
        }
        string packTypeNo, packTypeName;
        packTypeNo = DBOpt.dbHelper.ExecuteScalar("select f_packtypeno from dmis_sys_pack where f_no=" + ViewState["InstanceID"]).ToString();
        packTypeName = DBOpt.dbHelper.ExecuteScalar("select f_packname from dmis_sys_pack where f_no=" + ViewState["InstanceID"]).ToString();

        string[] sqls = new string[2];
        sqls[0] = "update dmis_sys_workflow set f_receiver ='" + rblMember.SelectedItem.Text + "',f_receivedate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where f_no=" + ViewState["CurWorkFlowNo"].ToString();
        sqls[1] = "update dmis_sys_memberstatus set f_receiver='" + rblMember.SelectedItem.Text + "',f_receivedate='" + DateTime.Now.ToString("yyyy-MM-dd")+"' where f_packno="
                  + ViewState["InstanceID"].ToString() + " and f_workflowno=" + ViewState["CurWorkFlowNo"] + " and f_receiver='" + txtMEMBER_NAME.Text+"'";

        if (DBOpt.dbHelper.ExecuteSqlWithTransaction(sqls) > 0)
        {
            uint maxTid = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WK_OPT_HISTORY", "tid");
            string reason = "将任务：" + tdPackDesc.InnerText + "  由 " + txtMEMBER_NAME.Text + " 改派给 " + rblMember.SelectedItem.Text;
            _sql = "insert into DMIS_SYS_WK_OPT_HISTORY(tid,packno,opt_type,datem,member_name,reason,F_PACKTYPENO,F_PACKTYPENAME) values("
                    + maxTid + "," + ViewState["InstanceID"].ToString() + ",'改派',TO_DATE('" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "','DD-MM-YYYY HH24:MI:SS'),'"
                    + Session["MemberName"].ToString() + "','" + reason + "'," + packTypeNo + ",'" + packTypeName + "')";
            DBOpt.dbHelper.ExecuteSql(_sql);

            //如果改派的主办人也是从办者之中，则删除从办
            _sql = "delete from dmis_sys_memberstatus where f_packno="
                  + ViewState["InstanceID"].ToString() + " and f_workflowno=" + ViewState["CurWorkFlowNo"] + " and f_receiver='" + rblMember.SelectedItem.Text + "'";
            DBOpt.dbHelper.ExecuteSql(_sql);

            JScript.CloseWin("refreshPage");
        }
        else
        {
            tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage").ToString();//"更改状态失败！请联系管理员！";
        }
    }

    protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepart.SelectedItem == null) return;
        _sql = "select member_name,member_id from dmis_view_depart_member where depart_id=" + ddlDepart.SelectedValue;
        DataTable member = DBOpt.dbHelper.GetDataTable(_sql);

        //把改派人的姓名去掉，不可能把任务改派给原先的办理者
        DataRow drow=null;
        for (int i = 0; i < member.Rows.Count; i++)
        {
            if (member.Rows[i][0].ToString() == txtMEMBER_NAME.Text)
            {
                drow = member.Rows[i];
                break;
            }
        }
        if (drow != null) member.Rows.Remove(drow);

        rblMember.DataTextField = "member_name";
        rblMember.DataValueField = "member_id";
        rblMember.DataSource = member;
        rblMember.DataBind();
    }
}

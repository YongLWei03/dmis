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


public partial class SYS_WorkFlow_frmPackTypes : System.Web.UI.Page
{
    string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _sql = "select distinct a.f_no,a.f_name from DMIS_SYS_PACKTYPE a,DMIS_SYS_RIGHTS b where a.F_NO=b.f_foreignkey and b.f_catgory='业务' and f_roleno in(" + Session["RoleIDs"].ToString() + ") ";
            DataTable packType = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < packType.Rows.Count; i++)
            {
                TreeNode firstLevel = new TreeNode();
                firstLevel.Text = packType.Rows[i][1].ToString();
                firstLevel.Value = packType.Rows[i][0].ToString();
                firstLevel.NavigateUrl = "../SYS_WorkFlow/frmPackWorkingTask.aspx?packTypeID=" + firstLevel.Value;
                firstLevel.ImageUrl = "../img/top_dic.gif";
                firstLevel.Target = "PackWorkingTask";
                initNodes(firstLevel.Value, firstLevel);
                trvPackTypes.Nodes.Add(firstLevel);
            }
        }
    }

    private void initNodes(string packTypeID,TreeNode node)
    {
        _sql = "select f_no,f_name from dmis_sys_flowlink where f_packtypeno=" + packTypeID+ " order by f_no";
        DataTable links = DBOpt.dbHelper.GetDataTable(_sql);
        for (int i = 0; i < links.Rows.Count; i++)
        {
            TreeNode secondLevel = new TreeNode();
            secondLevel.Text = links.Rows[i][1].ToString();
            secondLevel.Value = links.Rows[i][0].ToString();
            secondLevel.NavigateUrl = "../SYS_WorkFlow/frmPackWorkingTask.aspx?packTypeID=" + packTypeID + "&linkID=" + secondLevel.Value;
            secondLevel.ImageUrl = "../img/parentNode.gif";
            secondLevel.Target = "PackWorkingTask";
            node.ChildNodes.Add(secondLevel);
        }
    }




}

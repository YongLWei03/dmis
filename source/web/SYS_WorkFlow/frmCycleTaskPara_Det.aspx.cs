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

public partial class SYS_WorkFlow_frmCycleTaskPara_Det : PageBaseDetail
{
    private string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        tdPageMessage = tdMessage;

        if (!IsPostBack)
        {
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
            FillDropDownList.FillByTable(ref ddl对应业务ID, "DMIS_SYS_PACKTYPE", "F_NAME", "F_NO");
            if (Request["TID"] != "")
            {
                FillDropDownList.FillByTable(ref ddl任务启动人, "DMIS_SYS_MEMBER", "NAME", "ID");
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "DMIS_SYS_WK_CYCLE_TASK_PARA", "TID=" + Request["TID"]);
            }
            else
            {
                ddl对应业务ID_SelectedIndexChanged(null, null);
            }
            ddl周期类型_SelectedIndexChanged(null, null);
        }
    }

    protected override void btnSave_Click(object sender, EventArgs e)
    {
        //先保存数据;
        string ret, sql;
        if (ddl周期类型.SelectedItem.Text == "按年")
        {
            txt月份数字.Text = ddl1.SelectedItem.Value;
            txt日期数字.Text = ddl2.SelectedItem.Value;
            txt发生时候.Text = ddl1.SelectedItem.Value + "月" + ddl2.SelectedItem.Value + "日";
        }
        else if (ddl周期类型.SelectedItem.Text == "按季")
        {
            txt月份数字.Text = ddl1.SelectedItem.Value;
            txt日期数字.Text = ddl2.SelectedItem.Value;
            txt发生时候.Text = "第"+ddl1.SelectedItem.Value + "个月份" + ddl2.SelectedItem.Value + "日";
        }
        else if (ddl周期类型.SelectedItem.Text == "按月")
        {
            txt月份数字.Text = "";
            txt日期数字.Text = ddl2.SelectedItem.Value;
            txt发生时候.Text = ddl2.SelectedItem.Value + "日";
        }
        else if (ddl周期类型.SelectedItem.Text == "按周")
        {
            txt月份数字.Text = "";
            txt日期数字.Text = ddl2.SelectedItem.Value;
            txt发生时候.Text = ddl2.SelectedItem.Text;
        }


        ret = ControlWebValidator.Validate(this.Page, Session["TableName"].ToString());
        if (ret.Length > 0)
        {
            tdPageMessage.InnerText = ret;
            return;
        }
        ret = CustomControlSave.CustomControlSaveByTableNameReturnS(this.Page, Session["TableName"].ToString(), out sql);
        if (ret.Length > 0)
        {
            tdPageMessage.InnerText = ret;
            //WebLog.InsertLog("", "失败", sql);
            return;
        }
        else
        {
            tdPageMessage.InnerText = "";
            //WebLog.InsertLog("", "成功", sql);
        }

    }



    protected void ddl对应业务ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select member_name from dmis_view_depart_member_role where role_id in(select f_roleno from dmis_sys_rights where f_foreignkey=" + ddl对应业务ID.SelectedItem.Value + " and f_catgory='业务' and substr(f_access,3,1)='1')";
        DataTable mem = DBOpt.dbHelper.GetDataTable(_sql);
        ddl任务启动人.DataTextField = "MEMBER_NAME";
        ddl任务启动人.DataValueField = "MEMBER_NAME";
        ddl任务启动人.DataSource = mem;
        ddl任务启动人.DataBind();
    }

    protected void ddl周期类型_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl周期类型.SelectedIndex < 0) return;
        if (ddl周期类型.SelectedItem.Text == "按年")
        {
            lbl1.Visible = true;
            ddl1.Visible = true;
            lbl1.Text = "月份";
            initMonth();
            lbl2.Visible = true;
            lbl2.Text = "日期";
            initDay(ref ddl2);
        }
        else if (ddl周期类型.SelectedItem.Text == "按季")
        {
            lbl1.Visible = true;
            lbl1.Text = "第几个月份";
            ddl1.Visible = true;
            initSeason();
            lbl2.Visible = true;
            lbl2.Text = "日期";
            initDay(ref ddl2);
        }
        else if (ddl周期类型.SelectedItem.Text == "按月")
        {
            lbl1.Visible = false;
            ddl1.Visible = false;
            ddl1.Items.Clear();
            lbl2.Visible = true;
            lbl2.Text = "日期";
            initDay(ref ddl2);
        }
        else if (ddl周期类型.SelectedItem.Text == "按周")
        {
            lbl1.Visible = false;
            ddl1.Visible = false;
            initWeekName();
            lbl2.Visible = true;
            lbl2.Text = "星期";
        }
    }

    private void initMonth()
    {
        ddl1.Items.Clear();
        for (int i = 1; i <= 12; i++)
        {
            ddl1.Items.Add(new ListItem(i.ToString(),i.ToString()));
        }
    }

    private void initWeekName()
    {
        ddl2.Items.Clear();
        ddl2.Items.Add(new ListItem("星期日","0"));
        ddl2.Items.Add(new ListItem("星期一","1"));
        ddl2.Items.Add(new ListItem("星期二","2"));
        ddl2.Items.Add(new ListItem("星期三","3"));
        ddl2.Items.Add(new ListItem("星期四","4"));
        ddl2.Items.Add(new ListItem("星期五","5"));
        ddl2.Items.Add(new ListItem("星期六","6"));
    }

    private void initSeason()
    {
        ddl1.Items.Clear();
        ddl1.Items.Add(new ListItem("1","1"));
        ddl1.Items.Add(new ListItem("2","2"));
        ddl1.Items.Add(new ListItem("3","3"));
    }

    private void initDay(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        for (int i = 1; i <= 31; i++)
        {
            ddl.Items.Add(new ListItem(i.ToString(),i.ToString()));
        }
    }

}

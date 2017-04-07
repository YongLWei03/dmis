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
using System.Text;
using PlatForm.DBUtility;
using PlatForm.Functions;
using PlatForm.CustomControlLib;
using System.Globalization;

/*赤几  变电站值班日志*/


public partial class YW_STATION_frmSTATION_RUNNING_LOG : PageBaseList
{
    private string _sql;
    private DataTable _dt;
    private object obj;
    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;

        if (!IsPostBack)
        {
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();
            lblFuncName.Text = Session["FuncName"].ToString();
            btnDelete.Attributes.Add("onclick", " return confirm('" + GetGlobalResourceObject("WebGlobalResource", "DeleteBeforeConfirm").ToString() + "');");

            //为了突出哪个变电站，功能名要加变电站名称
            if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
                _sql = "select NAME from DMIS_SYS_DEPART where ID=" + Session["DepartID"].ToString();
            else
                _sql = "select OTHER_LANGUAGE_DESCR from DMIS_SYS_DEPART where ID=" + Session["DepartID"].ToString();
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj != null)
                lblFuncName.Text += "[" + obj.ToString() + "]";

            initShift();
            ViewState["BaseSql"] = "select * from T_STATION_RUNNING_LOG";
            Session["CustomOrder"] = null;
            //打最新的当值记录
            _sql = "select DATEM,SHIFT from T_STATION_SHIFT where FLAG=1 and STATION_ID=" + Session["DepartID"].ToString() + "order by DATEM desc,SHIFT desc";
            _dt = DBOpt.dbHelper.GetDataTable(_sql);
            if (_dt.Rows.Count > 0)
            {
                ViewState["CurrentShiftDatem"] = _dt.Rows[0][0];
                ViewState["CurrentShift"] = _dt.Rows[0][1];
                wdlDate.setTime(Convert.ToDateTime(_dt.Rows[0][0]));
                ddlShift.SelectedIndex = ddlShift.Items.IndexOf(ddlShift.Items.FindByValue(_dt.Rows[0][1].ToString()));
                btnQuery_Click(null, null);
            }
            else
            {
                ViewState["CurrentShiftDatem"] = null;
                ViewState["CurrentShift"] = null;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                //JScript.Alert("还不存在当前自动化值班记录，请联系管理员！");
                return;
            }
        }
        else
        {
            //自定义排序页面关闭后,刷新GridView
            if (Session["CustomOrder"] != null && ViewState["sql"].ToString().IndexOf(Session["CustomOrder"].ToString()) < 0)
            {
                if (ViewState["BaseQuery"] != null)  //页面自带查询条件
                    ViewState["sql"] = ViewState["BaseSql"] + " where " + ViewState["BaseQuery"] + " order by " + Session["CustomOrder"];
                else   //无查询条件
                    ViewState["sql"] = ViewState["BaseSql"] + " order by " + Session["CustomOrder"];
                GridViewBind();
            }
        }
    }

    private void initShift()
    {
        _sql = "select TID,SHIFT_NAME,to_char(SHIFT_STARTTIME,'HH:MI') ss from T_STATION_SHIFT_PARA where STATION_ID=" + Session["DepartID"].ToString() + " order by ss";
        _dt = DBOpt.dbHelper.GetDataTable(_sql);
        ddlShift.DataTextField = "SHIFT_NAME";
        ddlShift.DataValueField = "TID";
        ddlShift.DataSource = _dt;
        ddlShift.DataBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (ddlShift.SelectedItem == null) return;
        string shiftDate = wdlDate.getTime().ToString("yyyyMMdd");
        string shift = ddlShift.SelectedValue;

        ViewState["BaseQuery"] = "SHIFT_DATE='" + shiftDate + "' and SHIFT=" + shift + " and STATION_ID=" + Session["DepartID"].ToString();
        ViewState["sql"] = "select * from T_STATION_RUNNING_LOG where SHIFT_DATE='" + shiftDate + "' and SHIFT=" + shift
                    + " and STATION_ID=" + Session["DepartID"].ToString() + " order by DATEM";

        //找此班次的值班状态
        string man1, man2, man3, man4;
        string status;
        _sql = "select CURRENT_SHIFT_MAN1,CURRENT_SHIFT_MAN2,CURRENT_SHIFT_MAN3,CURRENT_SHIFT_MAN4,FLAG from T_STATION_SHIFT where STATION_ID="
            + Session["DepartID"].ToString() + " and to_char(DATEM,'YYYYMMDD')='" + shiftDate + "' and shift=" + shift;
        _dt = DBOpt.dbHelper.GetDataTable(_sql);
        if (_dt.Rows.Count > 0)
        {
            man1 = _dt.Rows[0][0] == Convert.DBNull ? "" : _dt.Rows[0][0].ToString();
            man2 = _dt.Rows[0][1] == Convert.DBNull ? "" : _dt.Rows[0][1].ToString();
            man3 = _dt.Rows[0][2] == Convert.DBNull ? "" : _dt.Rows[0][2].ToString();
            man4 = _dt.Rows[0][3] == Convert.DBNull ? "" : _dt.Rows[0][3].ToString();
            status = _dt.Rows[0][4] == Convert.DBNull ? "" : _dt.Rows[0][4].ToString();

            if (SetRight.IsAdminitrator(Session["MemberID"].ToString()))
            {
                //btnAdd.Enabled = true;  //管理员不能添加，否则无法判断是哪个变电站，但可以修改
                btnDelete.Enabled = true;
                grvList.Columns[6].Visible = true;
            }
            else
            {
                //只允许本班次的人修改当值记录
                if ((man1 == Session["MemberName"].ToString() || man2 == Session["MemberName"].ToString() ||
                        man3 == Session["MemberName"].ToString() || man4 == Session["MemberName"].ToString()) && status == "1")
                {
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                    grvList.Columns[6].Visible = true;
                }
                else
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    grvList.Columns[6].Visible = false;
                }
            }
        }
        else //没有当前班次
        {
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
        }
        if (grvList.EditIndex > -1)
        {
            GridViewCancelEditEventArgs args = new GridViewCancelEditEventArgs(grvList.EditIndex);
            grvRef_RowCancelingEdit(grvList, args);
        }
        GridViewBind();
    }

    //要填充DropDownList的值，无法使用祖先的代码
    protected override void grvRef_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (sender == null) return;

        GridView grv = (GridView)sender;
        grv.EditIndex = e.NewEditIndex;
        grv.EditRowStyle.BackColor = System.Drawing.Color.FromName("#F7CE90");
        GridViewBind();

        int tableID = Convert.ToInt16(Session["MainTableId"]);
        DropDownList ddlCATEGORY = (DropDownList)grv.Rows[e.NewEditIndex].FindControl("ddlCATEGORY");
        FillDropDownList.FillByTable(ref ddlCATEGORY, "T_STATION_RUNNING_LOG_TYPE", "NAME", "TID", "ORDER_ID");

        int tid = Convert.ToInt16(grv.DataKeys[e.NewEditIndex].Value);
        GridViewEdit.GridViewEditing(ref grv, tableID, e.NewEditIndex, tid);
    }

 
    //protected override void btnPrint_Click(object sender, EventArgs e)
    //{
    //    if (Session["ReportId"] == null || Session["ReportId"].ToString().Trim() == "")
    //    {
    //        Response.Write("<script language=javascript> alert('" + GetGlobalResourceObject("WebGlobalResource", "NoReportMessage").ToString() + "')</script");
    //        return;
    //    }
    //    if (wdlDate.Text == "") return;
    //    if (ddlShift.SelectedItem == null) return;

    //    string shiftDate = wdlDate.getTime().ToString("yyyyMMdd");
    //    string shift = ddlShift.SelectedValue;
    //    JScript.OpenWindow("../SYS_Common/frmCellReportDisplay.aspx?ReportID=" + Session["ReportId"].ToString() + "&Values=" + shiftDate+"^"+shift, "报表打印", "toolbar=no,menubar=no,titlebar=yes,directories=no,resizable=yes,status=yes");
    //}

    protected override void btnAdd_Click(object sender, EventArgs e)
    {
        uint max = DBOpt.dbHelper.GetMaxNum("T_STATION_RUNNING_LOG", "TID");
        _sql = "insert into T_STATION_RUNNING_LOG(TID,SHIFT_DATE,SHIFT,DATEM,DISPATCHER,STATION_ID) values(" +
            max + ",'" + wdlDate.getTime().ToString("yyyyMMdd") + "'," +ddlShift.SelectedValue+
            ",TO_DATE('" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "','DD-MM-YYYY HH24:MI:SS'),'" 
            + Session["MemberName"].ToString() +"',"+Session["DepartID"].ToString() +")";
        if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
        {
            btnQuery_Click(null, null);
            int rowID;
            for (rowID = 0; rowID < grvList.Rows.Count; rowID++)
            {
                if (grvList.DataKeys[rowID].Value.ToString() == max.ToString()) break;
            }
            GridViewEditEventArgs arg = new GridViewEditEventArgs(rowID);
            grvRef_RowEditing(grvList, arg);
        }
        else
        {
            //JScript.Alert("添加自动化值班记事失败，请联系管理员！");
            //WebLog.InsertLog("", "失败", _sql);
            return;
        }
    }

    protected void btnCurrentShift_Click(object sender, EventArgs e)
    {
        if (ViewState["CurrentShiftDatem"] != null && ViewState["CurrentShift"] != null)
        {
            wdlDate.setTime(Convert.ToDateTime(ViewState["CurrentShiftDatem"]));
            ddlShift.SelectedIndex = ddlShift.Items.IndexOf(ddlShift.Items.FindByValue(ViewState["CurrentShift"].ToString()));
            btnQuery_Click(null, null);
        }
        else
        {
            //JScript.Alert("当前班次不存在！");
        }
    }
 
}

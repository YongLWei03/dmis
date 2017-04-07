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

//<!--赤几 自动化值班记事-->
public partial class YW_ZDH_frmZDH_SHIFT_LOG_LIST : PageBaseList
{
    string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        grvRef = grvList;
        tdPageMessage = tdMessage;

        if (!IsPostBack)
        {
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
            //if (!btnAdd.Enabled)  //没有权限的，只能浏览
            //{
            //    return;
            //}

            _sql = "select TID,日期,系统值班人 from T_ZDH_自动化值班记录 where 状态='当值' order by 日期 desc";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            if (dt.Rows.Count > 0)
            {
                ViewState["系统值班人"] = dt.Rows[0][2];
                ViewState["当值日期"] = dt.Rows[0][1];
                wdlDate.setTime(Convert.ToDateTime(dt.Rows[0][1]));
                btnQuery_Click(null, null);
            }
            else
            {
                ViewState["系统值班人"] = "";
                ViewState["当值日期"] = "1900-01-01";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                JScript.Alert("还不存在当前自动化值班记录，请联系管理员！");
                return;
            }


        }
        else
        {
            //双击时用
            SetGridViewEditRow();
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string shiftDate = wdlDate.getTime().ToString("yyyyMMdd");
        //找此日的值班状态
        _sql = "select 状态 from T_ZDH_自动化值班记录 where to_char(日期,'YYYYMMDD')='" + shiftDate + "'";
        object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        string status = "";
        if (obj != null) status = obj.ToString();

        if (SetRight.IsAdminitrator(Session["MemberID"].ToString()))
        {
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            grvList.Columns[8].Visible = true;
        }
        else
        {
            //只允许本人修改当值记录
            if (Session["MemberName"].ToString() == ViewState["系统值班人"].ToString() && status == "当值")
            {
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                grvList.Columns[8].Visible = true;
            }
            else
            {
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                grvList.Columns[8].Visible = false;
            }
        }

        if (grvList.EditIndex > -1)
        {
            GridViewCancelEditEventArgs args = new GridViewCancelEditEventArgs(grvList.EditIndex);
            grvRef_RowCancelingEdit(grvList, args);
        }

        ViewState["sql"] = "select * from T_ZDH_自动化值班记事 where to_char(值班日期,'YYYYMMDD')='" + shiftDate + "' order by 发生时间";
        GridViewBind();
    }

    protected void grv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (sender == null) return;

        GridView grv = (GridView)sender;
        grv.EditIndex = e.NewEditIndex;
        grv.EditRowStyle.BackColor = System.Drawing.Color.FromName("#F7CE90");
        GridViewBind();

        int tableID = Convert.ToInt16(Session["MainTableId"]);
        HtmlComboBox hcb厂站 = (HtmlComboBox)grv.Rows[e.NewEditIndex].FindControl("hcb厂站");
        //HtmlComboBox hcb联系人 = (HtmlComboBox)grv.Rows[e.NewEditIndex].FindControl("hcb联系人");
        DropDownList ddl类别 = (DropDownList)grv.Rows[e.NewEditIndex].FindControl("ddl类别");
        DropDownList ddl记录人 = (DropDownList)grv.Rows[e.NewEditIndex].FindControl("ddl记录人");
        //FillDropDownList.FillHtmlCombxByTable(ref hcb事件来源, "DMIS_SYS_DEPART", "NAME", "ID", "order_id", "TYPE='自动化记事'");
        FillDropDownList.FillHtmlCombxByTable(ref hcb厂站, "T_STATION_TYPE", "NAME", "TID", "NO");
        FillDropDownList.FillByTable(ref ddl类别, "T_ZDH_记事_类别参数", "名称", "TID", "序号");
        FillDropDownList.FillByTable(ref ddl记录人, "DMIS_VIEW_DEPART_MEMBER_ROLE", "MEMBER_NAME", "MEMBER_ID", "MEMBER_NAME", "ROLE_ID=4");

        int tid = Convert.ToInt16(grv.DataKeys[e.NewEditIndex].Value);
        GridViewEdit.GridViewEditing(ref grv, tableID, e.NewEditIndex, tid);
    }

    protected void grv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (sender == null) return;

        GridView grv = (GridView)sender;
        int tid, tableID;

        string shiftDate = wdlDate.getTime().ToString("yyyyMMdd");
        tid = Convert.ToInt16(grv.DataKeys[e.RowIndex].Value);
        tableID = Convert.ToInt16(Session["MainTableId"]);

        WebDate wdl发生时间, wdl结束时间;
        wdl发生时间 = (WebDate)grv.Rows[e.RowIndex].FindControl("wdl发生时间");
        wdl结束时间 = (WebDate)grv.Rows[e.RowIndex].FindControl("wdl结束时间");
        TimeSpan ts = DateTime.Now - DateTime.Now;   //不让两个相同时间相减，则会出现未赋局部变量的错误。
        string updateSql;

        if (!(wdl结束时间.getTime() == null || wdl结束时间.getTime().ToString("yyyy") == "1900"))
        {
            DateTime dt1, dt2;
            dt1 = wdl发生时间.getTime();
            dt2 = wdl结束时间.getTime();
            if (dt1 > dt2)
            {
                JScript.Alert("发生时间不能大于结束时间！");
                return;
            }
            ts = dt2 - dt1;
        }
        else
        {
            updateSql = "update T_ZDH_自动化值班记事 set 小时数=NULL where TID=" + tid;   //取消小时数
            DBOpt.dbHelper.ExecuteSql(updateSql);
        }


        updateSql = GridViewEdit.GetGridViewRowUpdating(ref grv, tableID, e.RowIndex, tid);

        if (DBOpt.dbHelper.ExecuteSql(updateSql) > 0)
        {
            grv.EditIndex = -1;
            if (ts.TotalHours > 0)   //如果有时间再更新时间
            {
                updateSql = "update T_ZDH_自动化值班记事 set 小时数=" + ts.TotalHours.ToString() + " where TID=" + tid;
                DBOpt.dbHelper.ExecuteSql(updateSql);
            }
            GridViewBind();
        }
        else
        {
            JScript.Alert("数据保存失败!");
            return;
        }
    }


    protected override void btnPrint_Click(object sender, EventArgs e)
    {
        if (Session["ReportId"] == null || Session["ReportId"].ToString().Trim() == "")
        {
            JScript.Alert("此模块没有设置报表打印功能！");
            return;
        }
        if (wdlDate.Text == "")
        {
            JScript.Alert("请先选择日期和班次！");
            return;
        }
        string shiftDate = wdlDate.getTime().ToString("yyyyMMdd");
        JScript.OpenWindow("../SYS_Common/frmCellReportDisplay.aspx?ReportID=" + Session["ReportId"].ToString() + "&Values=" + shiftDate, "报表打印", "toolbar=no,menubar=no,titlebar=yes,directories=no,resizable=yes,status=yes");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        uint max = DBOpt.dbHelper.GetMaxNum("T_ZDH_自动化值班记事", "TID");
        _sql = "insert into T_ZDH_自动化值班记事(TID,值班日期,发生时间,记录人) values(" +
            max.ToString() + ",TO_DATE('" + wdlDate.getTime().ToString("dd-MM-yyyy") + "','DD-MM-YYYY')," +
            "TO_DATE('" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "','DD-MM-YYYY HH24:MI:SS'),'" + ViewState["系统值班人"].ToString() + "')";
        if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
        {
            btnQuery_Click(null, null);

            int rowID;
            for (rowID = 0; rowID < grvList.Rows.Count; rowID++)
            {
                if (grvList.DataKeys[rowID].Value.ToString() == max.ToString()) break;
            }
            GridViewEditEventArgs arg = new GridViewEditEventArgs(rowID);
            grv_RowEditing(grvList, arg);
        }
        else
        {
            JScript.Alert("添加自动化值班记事失败，请联系管理员！");
            WebLog.InsertLog("", "失败", _sql);
            return;
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            JScript.Alert(this.Page, "请先选择要删除的记录！");
            return;
        }

        _sql = "delete from T_ZDH_自动化值班记事 where TID=" + grvList.SelectedDataKey.Value;
        DBOpt.dbHelper.ExecuteSql(_sql);
        GridViewBind();
    }


    protected void btnCurrentShift_Click(object sender, EventArgs e)
    {
        if (ViewState["当值日期"] != null)
        {
            wdlDate.setTime(Convert.ToDateTime(ViewState["当值日期"]));
            btnQuery_Click(null, null);
        }
        else
        {
            JScript.Alert("当前班次不存在！");
        }
    }

    //GridView双击时有用
    private void SetGridViewEditRow()
    {
        //if (hihEditRowIndex.Value != "")
        //{
        //    if (grvList.EditIndex == Convert.ToInt32(hihEditRowIndex.Value)) return;  //此行已经是编辑状态，则不用再进入。

        //    GridViewEditEventArgs arg = new GridViewEditEventArgs(Convert.ToInt32(hihEditRowIndex.Value));
        //    grv_RowEditing(grvList, arg);
        //    hihEditRowIndex.Value = ""; //双击完之后，进行可编辑状态，就把把清空。
        //}
    }

    protected void grv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (sender == null) return;
        GridView grv = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (btnDelete.Enabled) //有权限的，绑定双击编辑
                e.Row.Attributes.Add("ondblclick", "OnClickedRow('" + e.Row.RowIndex + "');");
            else
                e.Row.Attributes.Remove("ondblclick");
        }
    }
}

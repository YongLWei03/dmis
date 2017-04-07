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
using System.Data.Common;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.Globalization;

//赤几 调度值班记录

public partial class YW_DD_frmDD_Shift :PageBaseList
{
    string _sql;
    DataTable _dt;
    object obj;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetListPageControlLocalizationText();
            lblFuncName.Text = Session["FuncName"].ToString();
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
            btnJIAOBAN.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "Common004").ToString() + "');");
            btnDelete.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "DeleteBeforeConfirm").ToString() + "');");

            FillDropDownList.FillByTable(ref ddlWEATHER, "DMIS_SYS_WEATHER", "NAME", "TID");
            FillDDY();
            FillShift();
            if (ddlQueryShift.Items.Count < 1)
            {
                //JScript.Alert("还没有设置调度值班班次信息，请联系系统管理员！");
                return;
            }

            DateTime st;
            //没有权限修改交接班记录的人只能查看数据，不能执行下面查找或生成当前班次的记录。
            if (!SetRight.IsModify(Session["FuncID"].ToString(), "frmDD_Shift.aspx", "btnSave", Session["MemberID"].ToString())) return;

            if (SetRight.IsAdminitrator(Session["MemberID"].ToString()))
            {
                btnAdd.Visible = true;
                btnDelete.Visible = true;
            }
            int days = 1; //没有设置值班天数，则为1
            if (ddlQueryShift.Items.Count == 1)   //值班参数表只有一条记录时，则要判断值班天数，多条记录则为1
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select SHIFT_DAYS from T_DD_SHIFT_PARA");
                if (obj != null)  //得到值班天数
                    days = Convert.ToInt16(obj);
            }
            else
                days = 1;


            uint tid = 1;

            /***************************************
             * 判断当前班次的规则如下：
             * 1、首先找flag为1（正在当班）的记录，没有找到则以当前日期和第一个班次新建当前班次
             * 2、如果有flag为1（正在当班）的记录，则再判断此当班日期是否在今天以前的days内之内；如果不是，则把此表内条件flag=1（正值）的记录的列flag的值设置为0(已值),再以当前日期和第一个班次新建当前班次；
             * 3、flag为1（正在当班）的记录的当班日期在今天以前的days天之内，则当前班次就是此班次。
             * 
             **************************************/
            _sql = "select TID,DATEM,SHIFT from T_DD_SHIFT where FLAG=1 order by DATEM desc";
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            if (dt.Rows.Count > 0)  //找到了当班的记录
            {
                st = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))).AddDays(-days);   //值班天数之前的00:00，用于下列比较
                if ((DateTime)dt.Rows[0]["DATEM"] < st)  //当前班次在值班天数以前，则新建班次
                {
                    _sql = "update T_DD_SHIFT set flag=0 where flag=1 ";   //先把当班记录状态设置为已交班
                    DBOpt.dbHelper.ExecuteSql(_sql);
                    //查找当日的第一个班次的记录在数据库中是否存在
                    obj = DBOpt.dbHelper.ExecuteScalar("select count(*) from T_DD_SHIFT where to_char(DATEM,'YYYYMMDD')='" + DateTime.Now.ToString("yyyyMMdd") + "' and SHIFT=" + ddlQueryShift.Items[0].Value );
                    if (obj == null || Convert.ToInt16(obj) == 0)   //数据库中没有当日的第一个班次的记录
                    {
                        tid = DBOpt.dbHelper.GetMaxNum("T_DD_SHIFT", "TID");
                        obj = DBOpt.dbHelper.ExecuteScalar("select SHIFT_STARTTIME,to_char(SHIFT_STARTTIME,'HH24:MI') ss from T_DD_SHIFT_PARA order by ss");
                        st = (DateTime)obj;
                        _sql = "insert into T_DD_SHIFT(TID,DATEM,SHIFT,FLAG) values(" + tid + ",TO_DATE('"
                            + DateTime.Now.ToString("yyyy-MM-dd") + st.ToString(" HH:mm") + "','YYYY-MM-DD HH24:MI')," + ddlQueryShift.Items[0].Value + "," + ddlQueryShift.Items[0].Value+ ")";
                        if (DBOpt.dbHelper.ExecuteSql(_sql) < 0)
                        {
                            JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "Common001"));
                            MultiView1.Visible = false;
                            return;
                        }
                    }
                    else　//数据库中有当日的第一个班次的记录，则把状态设置为当班状态（为1）
                    {
                        _sql = "update T_DD_SHIFT set flag=1 where to_char(DATEM,'YYYYMMDD')='" + DateTime.Now.ToString("yyyyMMdd") + "' and SHIFT=" + ddlQueryShift.Items[0].Value ;
                        if (DBOpt.dbHelper.ExecuteSql(_sql) != 1)
                        {
                            JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "Common002"));
                            MultiView1.Visible = false;
                            return;
                        }
                        tid = Convert.ToUInt16(DBOpt.dbHelper.ExecuteScalar("select TID from T_DD_SHIFT where to_char(DATEM,'YYYYMMDD')='" + DateTime.Now.ToString("yyyyMMdd") + "' and SHIFT=" + ddlQueryShift.Items[0].Value ));
                    }
                    Session["CurrentShiftDate"] = DateTime.Now.ToString("yyyyMMdd");
                    Session["CurrentShift"] = 1;
                    ViewState["CurrentShiftDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else  //当前班次是昨天以后，则以此作为当前班次
                {
                    Session["CurrentShiftDate"] = ((DateTime)dt.Rows[0][1]).ToString("yyyyMMdd");
                    Session["CurrentShift"] = dt.Rows[0][2];
                    tid = Convert.ToUInt16(dt.Rows[0][0]);
                    ViewState["CurrentShiftDate"] = ((DateTime)dt.Rows[0][1]).ToString("yyyy-MM-dd");
                }
            }
            else   //没有当前班次，则以当日的第一个班次新建当前班次
            {
                obj = DBOpt.dbHelper.ExecuteScalar("select count(*) from T_DD_SHIFT where to_char(DATEM,'YYYYMMDD')='" + DateTime.Now.ToString("yyyyMMdd") + "' and SHIFT=" + ddlQueryShift.Items[0].Value );
                if (obj == null || Convert.ToInt16(obj) == 0)   //数据库中没有当日的第一个班次的记录
                {
                    tid = DBOpt.dbHelper.GetMaxNum("T_DD_SHIFT", "TID");

                    obj = DBOpt.dbHelper.ExecuteScalar("select SHIFT_STARTTIME,to_char(SHIFT_STARTTIME,'HH24:MI') ss from T_DD_SHIFT_PARA order by ss");
                    st = (DateTime)obj;
                    _sql = "insert into T_DD_SHIFT(TID,DATEM,SHIFT,FLAG) values(" + tid + ",TO_DATE('" + DateTime.Now.ToString("yyyy-MM-dd") + st.ToString(" HH:mm") + "','YYYY-MM-DD HH24:MI')"
                        + "," + ddlQueryShift.Items[0].Value + ",1)";
                    if (DBOpt.dbHelper.ExecuteSql(_sql) < 0)
                    {
                        JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "Common001"));
                        MultiView1.Visible = false;
                        return;
                    }
                }
                else //数据库中有当日的第一个班次的记录,把其状态改为当值（为1）
                {
                    _sql = "update T_DD_SHIFT set flag=1 where to_char(DATEM,'YYYYMMDD')='" + DateTime.Now.ToString("yyyyMMdd") + "' and SHIFT=" + ddlQueryShift.Items[0].Value ;
                    if (DBOpt.dbHelper.ExecuteSql(_sql) != 1)
                    {
                        JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "Common002"));
                        MultiView1.Visible = false;
                        return;
                    }
                    tid = Convert.ToUInt16(DBOpt.dbHelper.ExecuteScalar("select TID from T_DD_SHIFT where to_char(DATEM,'YYYYMMDD')='" + DateTime.Now.ToString("yyyyMMdd") + "' and SHIFT=" + ddlQueryShift.Items[0].Value));
                }
                Session["CurrentShiftDate"] = DateTime.Now.ToString("yyyyMMdd");
                Session["CurrentShift"] = ddlQueryShift.Items[0].Value;
                ViewState["CurrentShiftDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            }
            CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_DD_SHIFT", "TID=" + tid);
            wdlQueryDate.setTime(Convert.ToDateTime(ViewState["CurrentShiftDate"]));
            ddlQueryShift.SelectedIndex = ddlQueryShift.Items.IndexOf(ddlQueryShift.Items.FindByValue(Session["CurrentShift"].ToString()));
            btnQuery_Click(null, null);
        }
    }

    /// <summary>
    /// 填充本调度值班人员下拉列表
    /// </summary>
    private void FillDDY()
    {
        //注意：调度值班人员的岗位ID是5
        _sql = "select MEMBER_NAME from DMIS_VIEW_DEPART_MEMBER_ROLE where ROLE_ID=5";
        _dt = DBOpt.dbHelper.GetDataTable(_sql);

        ddlCURRENT_SHIFT_MAN1.DataTextField = "MEMBER_NAME";
        ddlCURRENT_SHIFT_MAN1.DataValueField = "MEMBER_NAME";
        ddlCURRENT_SHIFT_MAN1.DataSource = _dt;
        ddlCURRENT_SHIFT_MAN1.DataBind();
        ddlCURRENT_SHIFT_MAN1.Items.Insert(0, "");

        ddlCURRENT_SHIFT_MAN2.DataTextField = "MEMBER_NAME";
        ddlCURRENT_SHIFT_MAN2.DataValueField = "MEMBER_NAME";
        ddlCURRENT_SHIFT_MAN2.DataSource = _dt;
        ddlCURRENT_SHIFT_MAN2.DataBind();
        ddlCURRENT_SHIFT_MAN2.Items.Insert(0, "");

        ddlCURRENT_SHIFT_MAN3.DataTextField = "MEMBER_NAME";
        ddlCURRENT_SHIFT_MAN3.DataValueField = "MEMBER_NAME";
        ddlCURRENT_SHIFT_MAN3.DataSource = _dt;
        ddlCURRENT_SHIFT_MAN3.DataBind();
        ddlCURRENT_SHIFT_MAN3.Items.Insert(0, "");

        ddlCURRENT_SHIFT_MAN4.DataTextField = "MEMBER_NAME";
        ddlCURRENT_SHIFT_MAN4.DataValueField = "MEMBER_NAME";
        ddlCURRENT_SHIFT_MAN4.DataSource = _dt;
        ddlCURRENT_SHIFT_MAN4.DataBind();
        ddlCURRENT_SHIFT_MAN4.Items.Insert(0, "");

        ddlNEXT_SHIFT_MAN1.DataTextField = "MEMBER_NAME";

        ddlNEXT_SHIFT_MAN1.DataValueField = "MEMBER_NAME";
        ddlNEXT_SHIFT_MAN1.DataSource = _dt;
        ddlNEXT_SHIFT_MAN1.DataBind();
        ddlNEXT_SHIFT_MAN1.Items.Insert(0, "");

        ddlNEXT_SHIFT_MAN2.DataTextField = "MEMBER_NAME";
        ddlNEXT_SHIFT_MAN2.DataValueField = "MEMBER_NAME";
        ddlNEXT_SHIFT_MAN2.DataSource = _dt;
        ddlNEXT_SHIFT_MAN2.DataBind();
        ddlNEXT_SHIFT_MAN2.Items.Insert(0, "");

        ddlNEXT_SHIFT_MAN3.DataTextField = "MEMBER_NAME";
        ddlNEXT_SHIFT_MAN3.DataValueField = "MEMBER_NAME";
        ddlNEXT_SHIFT_MAN3.DataSource = _dt;
        ddlNEXT_SHIFT_MAN3.DataBind();
        ddlNEXT_SHIFT_MAN3.Items.Insert(0, "");

        ddlNEXT_SHIFT_MAN4.DataTextField = "MEMBER_NAME";
        ddlNEXT_SHIFT_MAN4.DataValueField = "MEMBER_NAME";
        ddlNEXT_SHIFT_MAN4.DataSource = _dt;
        ddlNEXT_SHIFT_MAN4.DataBind();
        ddlNEXT_SHIFT_MAN4.Items.Insert(0, "");
    }

    /// <summary>
    /// 填充班次
    /// </summary>
    private void FillShift()
    {
        _sql = "select TID,SHIFT_NAME,to_char(SHIFT_STARTTIME,'HH24:MI') ss from T_DD_SHIFT_PARA order by ss";
        _dt = DBOpt.dbHelper.GetDataTable(_sql);

        ddlQueryShift.DataTextField = "SHIFT_NAME";
        ddlQueryShift.DataValueField = "TID";
        ddlQueryShift.DataSource = _dt;
        ddlQueryShift.DataBind();

        ddlSHIFT.DataTextField = "SHIFT_NAME";
        ddlSHIFT.DataValueField = "TID";
        ddlSHIFT.DataSource = _dt;
        ddlSHIFT.DataBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DateTime shiftDate = wdlQueryDate.getTime();
        _sql = "select TID from T_DD_SHIFT where to_char(DATEM,'YYYYMMDD')='" + shiftDate.ToString("yyyyMMdd") + "' and SHIFT=" + ddlQueryShift.SelectedValue;
        object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        if (obj != null)   //此班次存在
        {
            lblMessage.Text = "";
            MultiView1.Visible = true;
            CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_DD_SHIFT", "TID=" + obj.ToString());
        }
        else
        {
            wdlDATEM.setNull();
            ddlSHIFT.SelectedIndex = -1;
            btnSave.Enabled = false;
            MultiView1.Visible = false;
            lblMessage.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Common003");
        }

        /******************权限控制**********/
        //历史班次的数据只允许管理员修改,本班次的记录只能本班次的人修改
        string man1, man2, man3, man4;
        man1 = ddlCURRENT_SHIFT_MAN1.Text;
        man2 = ddlCURRENT_SHIFT_MAN2.Text;
        man3 = ddlCURRENT_SHIFT_MAN3.Text;
        man4 = ddlCURRENT_SHIFT_MAN4.Text;
        if (SetRight.IsAdminitrator(Session["MemberID"].ToString()))
        {
            btnSave.Enabled = true;
        }
        else
        {
            //不是当前班
             if (Session["CurrentShiftDate"] == null || Session["CurrentShift"] == null || shiftDate.ToString("yyyyMMdd") != Session["CurrentShiftDate"].ToString() || Session["CurrentShift"].ToString() != ddlQueryShift.SelectedValue)
             {
                 btnSave.Enabled = false;
             }
             else 
             {
                 //只允许本班次的人修改当值记录,或者当班人员全部为空时也可以修改（主要是为了第一次新建班次时，没有人员时所用）
                 if ((man1 == Session["MemberName"].ToString() || man2 == Session["MemberName"].ToString() || man3 == Session["MemberName"].ToString() || man4 == Session["MemberName"].ToString())
                     || (man1.Trim() == "" && man2.Trim() == "" && man3.Trim() == "" && man4.Trim() == ""))
                     btnSave.Enabled = true;
                  else
                        btnSave.Enabled = false;

             }
        }
        //隐藏交接班按钮
        if (txtFLAG.Text == "1" && btnSave.Enabled && !SetRight.IsAdminitrator(Session["MemberID"].ToString()))
            btnJIAOBAN.Visible = true;
        else
            btnJIAOBAN.Visible = false;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string re, sql;
        re = ControlWebValidator.Validate(this.Page, "T_DD_SHIFT");
        if (re != "")
        {
            lblMessage.Text = re;
            return;
        }
        if (CustomControlSave.CustomControlSaveByTableName(this.Page, "T_DD_SHIFT", out sql) < 0)
        {
            lblMessage.Text = (String)GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage");
            return;
        }
        lblMessage.Text = (String)GetGlobalResourceObject("WebGlobalResource", "SaveSuccessMessage");
    }

    protected void lbnView1_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        Cell1.Attributes["class"] = "SelectedTopBorder";
        Cell2.Attributes["class"] = "TopBorder";
        Cell3.Attributes["class"] = "TopBorder";
        Cell4.Attributes["class"] = "TopBorder";
    }
    protected void lbnView2_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        Cell1.Attributes["class"] = "TopBorder";
        Cell2.Attributes["class"] = "SelectedTopBorder";
        Cell3.Attributes["class"] = "TopBorder";
        Cell4.Attributes["class"] = "TopBorder";
    }
    protected void lbnView3_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;
        Cell1.Attributes["class"] = "TopBorder";
        Cell2.Attributes["class"] = "TopBorder";
        Cell3.Attributes["class"] = "SelectedTopBorder";
        Cell4.Attributes["class"] = "TopBorder";
    }
    protected void lbnView4_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 3;
        Cell1.Attributes["class"] = "TopBorder";
        Cell2.Attributes["class"] = "TopBorder";
        Cell3.Attributes["class"] = "TopBorder";
        Cell4.Attributes["class"] = "SelectedTopBorder";
    }

    protected override void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlQueryShift.SelectedIndex < 0) return;
        _sql = "select count(*) from T_DD_SHIFT where to_char(DATEM,'YYYYMMDD')='" + wdlQueryDate.getTime().ToString("yyyyMMdd")
            + "' and SHIFT=" + ddlQueryShift.SelectedValue ;

        int counts;
        obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        if (obj == null)
            counts = 0;
        else
            counts = Convert.ToUInt16(obj);
        if (counts > 0)
        {
            //JScript.Alert(this.Page, "已经存在此班次的数据，不允许添加！");
            return;
        }
        MultiView1.Visible = true;
        MultiView1.ActiveViewIndex = 0;
        Cell1.Attributes["class"] = "SelectedTopBorder";
        Cell2.Attributes["class"] = "TopBorder";
        Cell3.Attributes["class"] = "TopBorder";
        Cell4.Attributes["class"] = "TopBorder";

        SetCustomControlNULL.SetWebCustomControlNULL(this.Page, "T_DD_SHIFT");
        wdlDATEM.setTime(wdlQueryDate.getTime());
        ddlSHIFT.SelectedIndex = ddlSHIFT.Items.IndexOf(ddlSHIFT.Items.FindByValue(ddlQueryShift.SelectedValue));
        txtFLAG.Text = "0";   //只加已值的班次
    }

    protected override void btnDelete_Click(object sender, EventArgs e)
    {
        if (txtTID.Text != "")
        {
            _sql = "delete from T_DD_SHIFT where TID=" + txtTID.Text;
            if (DBOpt.dbHelper.ExecuteSql(_sql) < 1)
            {
                JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "DeleteFailMessage"));
                return;
            }
            else
            {
                btnSave.Enabled = false;
                MultiView1.Visible = false;
                lblMessage.Text = (String)GetGlobalResourceObject("WebGlobalResource", "DeleteSuccessMessage");
            }
        }
    }

    protected void btnJIAOBAN_Click(object sender, EventArgs e)
    {
        _dt = DBOpt.dbHelper.GetDataTable("select TID,SHIFT_STARTTIME,SHIFT_ENDTIME,SHIFT_DAYS,to_char(SHIFT_STARTTIME,'HH24:MI') ss from T_DD_SHIFT_PARA order by ss");

        DateTime dtJiaoBan, dtNextDatem;
        DateTime dtStartTime, dtEndTime;
        int rowIndex;
        int days = 1;
        if (_dt.Rows.Count == 1 && _dt.Rows[0]["SHIFT_DAYS"] != null)   //只有一条记录时才取天数,否则都为1
            days = Convert.ToInt32(_dt.Rows[_dt.Rows.Count - 1][3]);

        DateTime dtTemp;
        int nextShift, curShift;

        if (ddlSHIFT.SelectedValue == _dt.Rows[_dt.Rows.Count - 1]["TID"].ToString()) 　　//最后一个班次
        {
            nextShift = Convert.ToInt16(ddlSHIFT.Items[0].Value);
            dtStartTime = Convert.ToDateTime(_dt.Rows[_dt.Rows.Count - 1]["SHIFT_STARTTIME"]);
            dtEndTime = Convert.ToDateTime(_dt.Rows[_dt.Rows.Count - 1]["SHIFT_ENDTIME"]);

            dtTemp = wdlDATEM.getTime().AddDays(days);
            dtJiaoBan = Convert.ToDateTime(dtTemp.ToString("yyyy-MM-dd") + " " + dtEndTime.ToString("HH:mm"));
            if (dtJiaoBan > DateTime.Now)
            {
                JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "DDShift001") + dtJiaoBan.ToString("dd-MM-yyyy HH:mm") + (String)GetGlobalResourceObject("WebGlobalResource", "DDShift002"));
                return;
            }
            wdlJOIN_TIME.setTime(dtJiaoBan);
            txtFLAG.Text = "0";  //已值
            if (CustomControlSave.CustomControlSaveByTableName(this.Page, "T_DD_SHIFT") < 0)
            {
                lblMessage.Text = (String)GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage");
                return;
            }
            //判断下一班次在数据库中是否已经存在
            object obj = DBOpt.dbHelper.ExecuteScalar("select TID from T_DD_SHIFT where to_char(DATEM,'YYYYMMDD')='" + dtJiaoBan.ToString("yyyyMMdd") + "' and SHIFT=" + nextShift);
            if (obj == null)
            {
                //通过CustomControlSave.CustomControlSaveByTableName的方式来保存,而不是通过SQL语句来保存
                txtTID.Text = "";  //保存是新增记录
                txtFLAG.Text = "1";
                wdlDATEM.setTime(dtJiaoBan);
                ddlSHIFT.SelectedIndex = 0;
                ddlCURRENT_SHIFT_MAN1.SelectedIndex = ddlNEXT_SHIFT_MAN1.Items.IndexOf(ddlNEXT_SHIFT_MAN1.Items.FindByText(ddlNEXT_SHIFT_MAN1.Text));
                ddlCURRENT_SHIFT_MAN2.SelectedIndex = ddlNEXT_SHIFT_MAN2.Items.IndexOf(ddlNEXT_SHIFT_MAN2.Items.FindByText(ddlNEXT_SHIFT_MAN2.Text));
                ddlCURRENT_SHIFT_MAN3.SelectedIndex = ddlNEXT_SHIFT_MAN2.Items.IndexOf(ddlNEXT_SHIFT_MAN3.Items.FindByText(ddlNEXT_SHIFT_MAN3.Text));
                ddlCURRENT_SHIFT_MAN4.SelectedIndex = ddlNEXT_SHIFT_MAN4.Items.IndexOf(ddlNEXT_SHIFT_MAN4.Items.FindByText(ddlNEXT_SHIFT_MAN4.Text));
                ddlNEXT_SHIFT_MAN1.SelectedIndex = -1;
                ddlNEXT_SHIFT_MAN2.SelectedIndex = -1;
                ddlNEXT_SHIFT_MAN3.SelectedIndex = -1;
                ddlNEXT_SHIFT_MAN4.SelectedIndex = -1;
                wdlJOIN_TIME.setNull();
                if (CustomControlSave.CustomControlSaveByTableName(this.Page, "T_DD_SHIFT") < 0)
                {
                    //lblMessage.Text = "生成新班次时失败，请联系管理员！";
                    return;
                }
            }
            else
            {
                _sql = "update T_DD_SHIFT set flag=1 where TID=" + obj.ToString();
                if (DBOpt.dbHelper.ExecuteSql(_sql) < 0)
                {
                    //lblMessage.Text = "更新新班次的状态时出错，请联系管理员！";
                    return;
                }
            }
            wdlQueryDate.setTime(dtJiaoBan);
            ddlQueryShift.SelectedIndex = 0;
            Session["CurrentShiftDate"] = dtJiaoBan.ToString("yyyyMMdd");
            Session["CurrentShift"] = ddlQueryShift.SelectedValue;
            btnQuery_Click(null, null);
            ViewState["CurrentShiftDate"] = wdlQueryDate.Text;
        }
        else     //不是最后一个班次
        {
            curShift = Convert.ToInt16(ddlSHIFT.SelectedValue);
            nextShift = Convert.ToInt16(ddlSHIFT.Items[ddlSHIFT.SelectedIndex + 1].Value);
            dtNextDatem = Convert.ToDateTime("1900-01-01 00:00");  //先赋值
            foreach (DataRow row in _dt.Rows)
            {
                if (row["TID"].ToString() == curShift.ToString())
                {
                    rowIndex = _dt.Rows.IndexOf(row);  //当前班次在参数表中的行号。
                    dtStartTime = Convert.ToDateTime(row["SHIFT_STARTTIME"]);
                    dtNextDatem = Convert.ToDateTime(row["SHIFT_ENDTIME"]);
                    break;
                }
            }

            dtTemp = wdlDATEM.getTime();
            dtJiaoBan = Convert.ToDateTime(dtTemp.ToString("yyyy-MM-dd") + " " + dtNextDatem.ToString("HH:mm"));
            if (dtJiaoBan > DateTime.Now)
            {
                JScript.Alert((String)GetGlobalResourceObject("WebGlobalResource", "DDShift001") + dtJiaoBan.ToString("dd-MM-yyyy HH:mm") + (String)GetGlobalResourceObject("WebGlobalResource", "DDShift002"));
                return;
            }
            wdlJOIN_TIME.setTime(dtJiaoBan);
            txtFLAG.Text = "0";
            if (CustomControlSave.CustomControlSaveByTableName(this.Page, "T_DD_SHIFT") < 0)
            {
                lblMessage.Text = (String)GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage");
                return;
            }
            //判断下一班次在数据库中是否已经存在
            obj = DBOpt.dbHelper.ExecuteScalar("select TID from T_DD_SHIFT where to_char(DATEM,'YYYYMMDD')='" + dtJiaoBan.ToString("yyyyMMdd") + "' and SHIFT=" + nextShift);
            if (obj == null)
            {
                txtTID.Text = "";  //保存是新增记录
                txtFLAG.Text = "1";
                wdlDATEM.setTime(dtJiaoBan);
                ddlSHIFT.SelectedIndex = ddlSHIFT.Items.IndexOf(ddlSHIFT.Items.FindByValue(nextShift.ToString()));
                ddlCURRENT_SHIFT_MAN1.SelectedIndex = ddlNEXT_SHIFT_MAN1.Items.IndexOf(ddlNEXT_SHIFT_MAN1.Items.FindByText(ddlNEXT_SHIFT_MAN1.Text));
                ddlCURRENT_SHIFT_MAN2.SelectedIndex = ddlNEXT_SHIFT_MAN2.Items.IndexOf(ddlNEXT_SHIFT_MAN2.Items.FindByText(ddlNEXT_SHIFT_MAN2.Text));
                ddlCURRENT_SHIFT_MAN3.SelectedIndex = ddlNEXT_SHIFT_MAN2.Items.IndexOf(ddlNEXT_SHIFT_MAN3.Items.FindByText(ddlNEXT_SHIFT_MAN3.Text));
                ddlCURRENT_SHIFT_MAN4.SelectedIndex = ddlNEXT_SHIFT_MAN4.Items.IndexOf(ddlNEXT_SHIFT_MAN4.Items.FindByText(ddlNEXT_SHIFT_MAN4.Text));
                ddlNEXT_SHIFT_MAN1.SelectedIndex = -1;
                ddlNEXT_SHIFT_MAN2.SelectedIndex = -1;
                ddlNEXT_SHIFT_MAN3.SelectedIndex = -1;
                ddlNEXT_SHIFT_MAN4.SelectedIndex = -1;
                wdlJOIN_TIME.setNull();
                if (CustomControlSave.CustomControlSaveByTableName(this.Page, "T_DD_SHIFT") < 0)
                {
                    lblMessage.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Common001");
                    return;
                }
            }
            else
            {
                _sql = "update T_DD_SHIFT set FLAG=1 where TID=" + obj.ToString();
                if (DBOpt.dbHelper.ExecuteSql(_sql) < 0)
                {
                    return;
                }
            }

            wdlQueryDate.Text = dtJiaoBan.ToString("yyyy-MM-dd");
            ddlQueryShift.SelectedIndex = ddlQueryShift.Items.IndexOf(ddlQueryShift.Items.FindByValue(nextShift.ToString()));
            Session["CurrentShiftDate"] = dtJiaoBan.ToString("yyyyMMdd");
            Session["CurrentShift"] = nextShift.ToString();
            ViewState["CurrentShiftDate"] = wdlQueryDate.Text;
            btnQuery_Click(null, null);
        }

    }

    protected void btnCurrentShift_Click(object sender, EventArgs e)
    {
        _sql = "select TID from T_DD_SHIFT where to_char(DATEM,'YYYYMMDD')='" + Session["CurrentShiftDate"].ToString()
            + "' and SHIFT=" + Session["CurrentShift"];
        object obj = DBOpt.dbHelper.ExecuteScalar(_sql);
        if (obj == null)   //此班次不存在
        {
            btnSave.Enabled = false;
            MultiView1.Visible = false;
            lblMessage.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Common003");
        }
        else
        {
            wdlQueryDate.Text = ViewState["CurrentShiftDate"].ToString();
            ddlQueryShift.SelectedIndex = ddlQueryShift.Items.IndexOf(ddlQueryShift.Items.FindByValue(Session["CurrentShift"].ToString()));
            MultiView1.Visible = true;
            CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_DD_SHIFT", "TID=" + obj.ToString());

            DateTime shiftDate = wdlQueryDate.getTime();
            /******************权限控制**********/
            //历史班次的数据只允许管理员修改,本班次的记录只能本班次的人修改
            string man1, man2, man3, man4;
            man1 = ddlCURRENT_SHIFT_MAN1.Text;
            man2 = ddlCURRENT_SHIFT_MAN2.Text;
            man3 = ddlCURRENT_SHIFT_MAN3.Text;
            man4 = ddlCURRENT_SHIFT_MAN4.Text;
            if (SetRight.IsAdminitrator(Session["MemberID"].ToString()))
            {
                btnSave.Enabled = true;
            }
            else
            {
                //不是当前班
                if (Session["CurrentShiftDate"] == null || Session["CurrentShift"] == null || shiftDate.ToString("yyyyMMdd") != Session["CurrentShiftDate"].ToString() || Session["CurrentShift"].ToString() != ddlQueryShift.SelectedValue)
                {
                    btnSave.Enabled = false;
                }
                else
                {
                    //只允许本班次的人修改当值记录
                    if ((man1 == Session["MemberName"].ToString() || man2 == Session["MemberName"].ToString() ||
                              man3 == Session["MemberName"].ToString() || man4 == Session["MemberName"].ToString()))
                        btnSave.Enabled = true;
                    else
                        btnSave.Enabled = false;

                }
            }

            //隐藏交接班按钮
            if (txtFLAG.Text == "1" && btnSave.Enabled && !SetRight.IsAdminitrator(Session["MemberID"].ToString()))
                btnJIAOBAN.Visible = true;
            else
                btnJIAOBAN.Visible = false;

        }
    }

    protected override void btnPrint_Click(object sender, EventArgs e)
    {
        if (Session["ReportId"] == null || Session["ReportId"].ToString().Trim() == "")
        {
            Response.Write("<script language=javascript> alert('" + (String)GetGlobalResourceObject("WebGlobalResource", "NoReportMessage") + "')</script");
            return;
        }
        if (ddlQueryShift.SelectedItem == null) return;
        string values = wdlQueryDate.getTime().ToString("dd-MM-yy") + "^" + ddlQueryShift.SelectedValue ;
        JScript.OpenWindow("../SYS_Common/frmCellReportDisplay.aspx?ReportID=" + Session["ReportId"].ToString() + "&Values=" + values, (String)GetGlobalResourceObject("WebGlobalResource", "ReportPrint"), "resizable=1,scrollbars=1");
    }

}

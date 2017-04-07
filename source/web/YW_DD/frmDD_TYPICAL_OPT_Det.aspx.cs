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

using PlatForm.CustomControlLib;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.Globalization;
//赤几 典型票

public partial class YW_DD_frmDD_TYPICAL_OPT_Det : PageBaseDetail
{
    private string _sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        info = detail_info;
        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetDetailPageControlLocalizationText();
            lblFuncName.Text = GetLocalResourceObject("PageResource1.Title").ToString();

            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());
            btnDeleteItem.Attributes.Add("onClick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "DeleteBeforeConfirm").ToString() + "');");

            if (!btnAppendItem.Enabled)  //没有权限修改，则把步骤的编辑列隐藏
                grvList.Columns[grvList.Columns.Count - 1].Visible = false;

            FillDropDownList.FillByTable(ref ddlSTATION, "DMIS_SYS_STATION", "NAME", "TID", "ORDER_ID");

            if (Request["TID"] != "")
            {
                ViewState["DxpTID"] = Request["TID"];
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, Session["TableName"].ToString(), "TID=" + Request["TID"]);
                initGridView();
            }
            else
            {
                ViewState["DxpTID"] = null;
            }
        }
    }

    protected override void btnSave_Click(object sender, EventArgs e)
    {
        string ret, sql;

        ret = ControlWebValidator.Validate(this.Page, Session["TableName"].ToString());
        if (ret.Length > 0)
        {
            info.InnerText = ret;
            return;
        }
        ret = CustomControlSave.CustomControlSaveByTableNameReturnS(this.Page, Session["TableName"].ToString(), out sql);
        if (ret.Length > 0)
        {
            info.InnerText = ret;
            return;
        }
        else
        {
            ViewState["DxpTID"] = txtTID.Text;
            info.InnerText = "";
        }
        Response.Redirect(Session["URL"].ToString());
    }

    protected void btnAppendItem_Click(object sender, EventArgs e)
    {
        if (ViewState["DxpTID"] == null)  //先保存头
        {
            string ret, sql;

            ret = ControlWebValidator.Validate(this.Page, Session["TableName"].ToString());
            if (ret.Length > 0)
            {
                info.InnerText = ret;
                return;
            }
            ret = CustomControlSave.CustomControlSaveByTableNameReturnS(this.Page, Session["TableName"].ToString(), out sql);
            if (ret.Length > 0)
            {
                info.InnerText = ret;
                return;
            }
            else
            {
                ViewState["DxpTID"] = txtTID.Text;
                info.InnerText = "";
            }
        }
        uint maxTid = DBOpt.dbHelper.GetMaxNum("T_DD_TYPICAL_OPT_BODY", "TID");
        _sql = "insert into T_DD_TYPICAL_OPT_BODY(HEAD_TID,TID,XH) values(" + ViewState["DxpTID"].ToString() + "," + maxTid.ToString() + "," + Convert.ToString(grvList.Rows.Count + 1) + ")";
        DBOpt.dbHelper.ExecuteSql(_sql);
        initGridView();
    }

    protected void btnInsertItem_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            //JScript.Alert("请先选择一操作步骤!");
            return;
        }
        int xh = grvList.SelectedIndex + 2;
        //先把插入点后面的序号都加1
        for (int i = grvList.SelectedIndex; i < grvList.Rows.Count; i++)
        {
            _sql = "update T_DD_TYPICAL_OPT_BODY set XH=" + xh + " where TID=" + grvList.DataKeys[i].Value;
            DBOpt.dbHelper.ExecuteSql(_sql);
            xh++;
        }
        uint maxTid = DBOpt.dbHelper.GetMaxNum("T_DD_TYPICAL_OPT_BODY", "TID");
        _sql = "insert into T_DD_TYPICAL_OPT_BODY(HEAD_TID,TID,XH) values(" + ViewState["DxpTID"].ToString() + "," + maxTid.ToString() + "," + Convert.ToString(grvList.SelectedIndex + 1) + ")";
        DBOpt.dbHelper.ExecuteSql(_sql);
        initGridView();
    }

    protected void btnDeleteItem_Click(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0)
        {
            //JScript.Alert("请先选择要删除的操作步骤!");
            return;
        }
        _sql = "delete from  T_DD_TYPICAL_OPT_BODY where TID=" + grvList.DataKeys[grvList.SelectedIndex].Value;
        DBOpt.dbHelper.ExecuteSql(_sql);
        BuildXH();
        initGridView();
    }

    private void BuildXH()
    {
        _sql = "select XH,TID from T_DD_TYPICAL_OPT_BODY where HEAD_TID=" + ViewState["DxpTID"].ToString() + " order by XH";
        DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
        int xh;
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            if (int.TryParse(dt.Rows[i - 1][0].ToString(), out xh))
            {
                if (xh == i)
                    continue;
                else
                    _sql = "update T_DD_TYPICAL_OPT_BODY set XH=" + i.ToString() + " where TID=" + dt.Rows[i - 1][1].ToString();
            }
            else
            {
                _sql = "update T_DD_TYPICAL_OPT_BODY set XH=" + i.ToString() + " where TID=" + dt.Rows[i - 1][1].ToString();
            }
            DBOpt.dbHelper.ExecuteSql(_sql);
        }
        initGridView();
    }

    private void initGridView()
    {
        _sql = "select * from T_DD_TYPICAL_OPT_BODY where HEAD_TID=" + ViewState["DxpTID"].ToString() + " order by XH";
        DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
        grvList.DataSource = dt;
        grvList.DataBind();
    }

    protected void grvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        grvList.EditIndex = -1;
        initGridView();
    }
    protected void grvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvList.EditIndex = e.NewEditIndex;
        grvList.EditRowStyle.BackColor = System.Drawing.Color.FromName("#F7CE90");
        initGridView();

        _sql = "select * from T_DD_TYPICAL_OPT_BODY where TID=" + grvList.DataKeys[e.NewEditIndex].Value;
        DataTable _dt = DBOpt.dbHelper.GetDataTable(_sql);

        HtmlComboBox hcb = (HtmlComboBox)grvList.Rows[e.NewEditIndex].FindControl("hcbUNIT");
        FillDropDownList.FillHtmlCombxByTable(ref hcb, "DMIS_SYS_STATION", "NAME", "TID", "ORDER_ID");

        if (_dt.Rows[0]["UNIT"] != Convert.DBNull)
            hcb.Text = _dt.Rows[0]["UNIT"].ToString();
        if (grvList.Rows[e.NewEditIndex].FindControl("txtXH") != null)
        {
            TextBox txt = (TextBox)grvList.Rows[e.NewEditIndex].FindControl("txtXH");
            if (_dt.Rows[0]["XH"] != Convert.DBNull)
                txt.Text = _dt.Rows[0]["XH"].ToString();
        }
        if (grvList.Rows[e.NewEditIndex].FindControl("txtCONTENT") != null)
        {
            TextBox txt = (TextBox)grvList.Rows[e.NewEditIndex].FindControl("txtCONTENT");
            if (_dt.Rows[0]["CONTENT"] != Convert.DBNull)
                txt.Text = _dt.Rows[0]["CONTENT"].ToString();
        }
    }

    protected void grvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HtmlComboBox hcbUNIT = (HtmlComboBox)grvList.Rows[e.RowIndex].FindControl("hcbUNIT");
        TextBox txtXH = (TextBox)grvList.Rows[e.RowIndex].FindControl("txtXH");
        int temp;
        if (!int.TryParse(txtXH.Text, out temp))
        {
            detail_info.InnerText = GetGlobalResourceObject("WebGlobalResource", "NumericalValeError").ToString(); ;
            return;
        }
        TextBox txtCONTENT = (TextBox)grvList.Rows[e.RowIndex].FindControl("txtCONTENT");
        FieldPara[] fields = new FieldPara[3] { new FieldPara("UNIT",FieldType.String,hcbUNIT.SelectedText),
                                                new FieldPara("CONTENT",FieldType.String,txtCONTENT.Text),
                                                new FieldPara("XH",FieldType.Int,txtXH.Text)
                                                };
        WherePara[] wheres = new WherePara[1] { new WherePara("TID", FieldType.Int, grvList.DataKeys[e.RowIndex].Value.ToString(), "=", "and") };
        _sql = DBOpt.dbHelper.GetUpdateSql("T_DD_TYPICAL_OPT_BODY", fields, wheres);
        if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
        {
            grvList.EditIndex = -1;
            initGridView();
            detail_info.InnerText = GetGlobalResourceObject("WebGlobalResource", "SaveSuccessMessage").ToString();
        }
        else
        {
            detail_info.InnerText = GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage").ToString();
            return;
        }
    }
    protected void grvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grvList.SelectedIndex < 0) return;
        grvList.SelectedRow.Cells[0].Text = "<img border=0 align=absmiddle src=../img/Selected.gif>";
    }
}

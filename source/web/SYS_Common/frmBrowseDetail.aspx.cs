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
using System.Globalization;
using PlatForm.CustomControlLib;
using System.Threading;
public partial class SYS_Common_frmBrowseDetail : PageBaseDetail
{
    private string _sql;
    private object obj;
    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ColumnStyle"] != null)
        {
            if (Session["ColumnStyle"].ToString() == "0")  //二列
                generateControlByTwoColumn();
            else  //一列
                generateControlByOneColumn();
        }

        if (!IsPostBack)
        {
            if (Request["FuncId"] == null) return;
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetDetailPageControlLocalizationText();
            _sql = "select * from DMIS_SYS_TREEMENU where ID=" + Request["FuncId"];
            DataTable treeMenu = DBOpt.dbHelper.GetDataTable(_sql);
            if (treeMenu.Rows.Count < 1) return;
            if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
                lblFuncName.Text = treeMenu.Rows[0]["NAME"] == Convert.DBNull ? "" : treeMenu.Rows[0]["NAME"].ToString();
            else
                lblFuncName.Text = treeMenu.Rows[0]["OTHER_LANGUAGE_DESCR"] == Convert.DBNull ? "" : treeMenu.Rows[0]["OTHER_LANGUAGE_DESCR"].ToString();

            //_sql = "select DISPLAY_STYLE from DMIS_SYS_TABLES where ID=" + Session["MainTableId"].ToString();
            //obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            //if (Session["ColumnStyle"] == null || Session["ColumnStyle"].ToString() == "0")  //二列
            //{
            //    Session["ColumnStyle"] = 0;
            //    generateControlByTwoColumn();
            //}
            //else  //一列
            //{
            //    Session["ColumnStyle"] = 1;
            //    generateControlByOneColumn();
            //}

            SetRight.SetPageRight(this.Page, Request["FuncId"].ToString(), Session["RoleIDs"].ToString());

            if (Request["TID"] != "")
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, Session["TableName"].ToString(), "TID=" + Request["TID"]);
        }
    }

    protected override void btnSave_Click(object sender, EventArgs e)
    {
        string ret, sql;

        ret = ControlWebValidator.Validate(this.Page, Convert.ToInt16(Session["MainTableId"].ToString()));
        if (ret.Length > 0)
        {
            detail_info.InnerText = ret;
            return;
        }
        ret = CustomControlSave.CustomControlSaveByTableIDReturnS(this.Page, Convert.ToInt16(Session["MainTableId"].ToString()), out sql);
        if (ret.Length > 0)
        {
            detail_info.InnerText = ret;
            //tdPageMessage.InnerText = ret;
            return;
        }
        else
        {
            detail_info.InnerText = "";
            //WebLog.InsertLog("", "成功", sql);
        }
        //JScript.CloseWin("refreshPage");

        Response.Redirect(Session["URL"].ToString());
    }

    //一列的情况下
    private void generateControlByOneColumn()
    {
        Table t = new Table();
        TableRow tr;
        TableCell tcDesc,tcControl;
        int controlHeight;
        
        t.CssClass = "slim_table";
        DataTable cols = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableID"].ToString() + " order by ORDER_ID");
        
        //页面上已经有了txtTID控件，先把它删除它
        DataColumn[] keys = new DataColumn[1];
        keys[0] = cols.Columns["NAME"];
        cols.PrimaryKey = keys;
        DataRow row= cols.Rows.Find("TID");
        if (row != null) cols.Rows.Remove(row);


        for (int i = 0; i < cols.Rows.Count; i++)
        {
            //if (cols.Rows[i]["NAME"].ToString() == "TID") continue; //主键不用生成，已经在界面上加入 
            //temp = cols.Rows[i]["NAME"].ToString();

            tr = new TableRow();
            //描述列
            tcDesc = new TableCell();
            if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
                tcDesc.Text = cols.Rows[i]["DESCR"].ToString();
            else
                tcDesc.Text = cols.Rows[i]["OTHER_LANGUAGE_DESCR"].ToString();
            tcDesc.CssClass = "slim_table_td_desc_one";
            tr.Cells.Add(tcDesc);

            //控件列
            tcControl = new TableCell();
            switch (cols.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString())
            {
                case "TextBox":
                    TextBox txt = new TextBox();
                    txt.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    //如果设置了控件高度，则认为是多行方式。
                    if (cols.Rows[i]["CUSTOM_CONTROL_TYPE"] != Convert.DBNull && int.TryParse(cols.Rows[i]["CONTROL_HEIGHT"].ToString(), out controlHeight))
                    {
                        txt.TextMode = TextBoxMode.MultiLine;
                        txt.Height = new Unit(controlHeight.ToString()+"px");
                        txt.Width = new Unit("95%");
                    }
                    tcControl.Controls.Add(txt);
                    break;
                case "DropDownList":
                    DropDownList ddl = new DropDownList();
                    ddl.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    //查找此列是否有被关联，即此列的下拉列表的值要依靠其它列的取值来填充，如果没有，则填充，否则
                    if (!isRelated(ddl.ID))
                    {
                        if (cols.Rows[i]["FILL_EXPRESSION"] != Convert.DBNull) //充值表达式不为空，则填充
                            FillDropDownList(ref ddl, cols.Rows[i]["FILL_EXPRESSION"].ToString());
                        //找此列是否有关联列，有则加触发事件
                        if (cols.Rows[i]["RELATING_COLUMN"] != Convert.DBNull && cols.Rows[i]["RELATING_COLUMN"].ToString().Length > 0)
                        {
                            ddl.AutoPostBack = true;
                            ddl.ToolTip = cols.Rows[i]["RELATING_COLUMN"].ToString();  //把所关联的列放在此，方便处理
                            ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                        }
                    }
                    tcControl.Controls.Add(ddl);
                    break;
                case "WebDateLib":
                    WebDate wd = new WebDate();
                    wd.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    tcControl.Controls.Add(wd);
                    break;
                case "HtmlComboBox":
                    HtmlComboBox hcb = new HtmlComboBox();
                    hcb.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    if (!isRelated(hcb.ID))
                    {
                        if (cols.Rows[i]["FILL_EXPRESSION"] != Convert.DBNull) //充值表达式不为空，则填充
                            FillHtmlComboBox(ref hcb, cols.Rows[i]["FILL_EXPRESSION"].ToString());
                        //找此列是否有关联列，有则加触发事件
                        if (cols.Rows[i]["RELATING_COLUMN"] != Convert.DBNull && cols.Rows[i]["RELATING_COLUMN"].ToString().Length > 0)
                        {
                            hcb.AutoPostBack = true;
                            hcb.ToolTip = cols.Rows[i]["RELATING_COLUMN"].ToString(); //把所关联的列放在此，方便处理
                            hcb.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                        }
                    }
                    tcControl.Controls.Add(hcb);
                    break;
                case "CheckBox":
                    CheckBox cb = new CheckBox();
                    cb.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    tcControl.Controls.Add(cb);
                    break;
                case "RadioButtonList":
                    RadioButtonList rbl = new RadioButtonList();
                    rbl.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    tcControl.Controls.Add(rbl);
                    break;
                case "CheckBoxList":
                    CheckBoxList cbl = new CheckBoxList();
                    cbl.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    tcControl.Controls.Add(cbl);
                    break;
                default:
                    break;
            }
            tcControl.CssClass = "slim_table_td_control_one";
            tr.Cells.Add(tcControl);
            t.Rows.Add(tr);
        }
        detail_data.Controls.Add(t);
    }

    //二列的情况下
    private void generateControlByTwoColumn()
    {
        Table t = new Table();
        TableRow tr=new TableRow();
        TableCell tcDesc, tcControl;
        int controlHeight;
        int newRowFlag = 0;

        t.CssClass = "slim_table";
        DataTable cols = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableID"].ToString() + " order by ORDER_ID");
        
        //页面上已经有了txtTID控件，先把它删除它
        DataColumn[] keys = new DataColumn[1];
        keys[0] = cols.Columns["NAME"];
        cols.PrimaryKey = keys;
        DataRow row = cols.Rows.Find("TID");
        if (row != null) cols.Rows.Remove(row);

        
        for (int i = 0; i < cols.Rows.Count; i++)
        {
            //if (cols.Rows[i]["NAME"].ToString() == "TID")
            //{
            //    if(i<cols.Rows.Count-1)
            //        continue; //主键不用生成，已经在界面上加入 
            //    else
            //    {
            //        if(newRowFlag % 2!=0) 
            //        {
            //            t.Rows.Add(tr);
            //            break;
            //        }
            //    }
            //}
            if (newRowFlag % 2==0)
                tr = new TableRow();

            

            //描述列
            tcDesc = new TableCell();
            if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
                tcDesc.Text = cols.Rows[i]["DESCR"].ToString();
            else
                tcDesc.Text = cols.Rows[i]["OTHER_LANGUAGE_DESCR"].ToString();
            tcDesc.CssClass = "slim_table_td_desc";
            tr.Cells.Add(tcDesc);

            //控件列
            tcControl = new TableCell();
            tcControl.CssClass = "slim_table_td_control";
            switch (cols.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString())
            {
                case "TextBox":
                    TextBox txt = new TextBox();
                    txt.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    //如果设置了控件高度，则认为是多行方式。
                    if (cols.Rows[i]["CUSTOM_CONTROL_TYPE"] != Convert.DBNull && int.TryParse(cols.Rows[i]["CONTROL_HEIGHT"].ToString(), out controlHeight))
                    {
                        txt.TextMode = TextBoxMode.MultiLine;
                        txt.Height = new Unit(controlHeight.ToString() + "px");
                        txt.Width = new Unit("95%");
                    }
                    tcControl.Controls.Add(txt);
                    break;
                case "DropDownList":
                    DropDownList ddl = new DropDownList();
                    ddl.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    //查找此列是否有被关联，即此列的下拉列表的值要依靠其它列的取值来填充，如果没有，则填充，否则
                    if (!isRelated(ddl.ID))
                    {
                        if (cols.Rows[i]["FILL_EXPRESSION"] != Convert.DBNull) //充值表达式不为空，则填充
                            FillDropDownList(ref ddl, cols.Rows[i]["FILL_EXPRESSION"].ToString());
                        //找此列是否有关联列，有则加触发事件
                        if (cols.Rows[i]["RELATING_COLUMN"] != Convert.DBNull && cols.Rows[i]["RELATING_COLUMN"].ToString().Length > 0)
                        {
                            ddl.AutoPostBack = true;
                            ddl.ToolTip = cols.Rows[i]["RELATING_COLUMN"].ToString();  //把所关联的列放在此，方便处理
                            ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                        }
                    }
                    tcControl.Controls.Add(ddl);
                    break;
                case "WebDateLib":
                    WebDate wd = new WebDate();
                    wd.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    wd.DateStyle = WebDate.DateEnum.DateFormat3;  //统一格式为yyyy-MM-dd HH:mm
                    tcControl.Controls.Add(wd);
                    break;
                case "HtmlComboBox":
                    HtmlComboBox hcb = new HtmlComboBox();
                    hcb.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    if (!isRelated(hcb.ID))
                    {
                        if (cols.Rows[i]["FILL_EXPRESSION"] != Convert.DBNull) //充值表达式不为空，则填充
                            FillHtmlComboBox(ref hcb, cols.Rows[i]["FILL_EXPRESSION"].ToString());
                        //找此列是否有关联列，有则加触发事件
                        if (cols.Rows[i]["RELATING_COLUMN"] != Convert.DBNull && cols.Rows[i]["RELATING_COLUMN"].ToString().Length > 0)
                        {
                            hcb.AutoPostBack = true;
                            hcb.ToolTip = cols.Rows[i]["RELATING_COLUMN"].ToString(); //把所关联的列放在此，方便处理
                            hcb.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                        }
                    }
                    tcControl.Controls.Add(hcb);
                    break;
                case "CheckBox":
                    CheckBox cb = new CheckBox();
                    cb.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    tcControl.Controls.Add(cb);
                    break;
                case "RadioButtonList":
                    RadioButtonList rbl = new RadioButtonList();
                    rbl.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    tcControl.Controls.Add(rbl);
                    break;
                case "CheckBoxList":
                    CheckBoxList cbl = new CheckBoxList();
                    cbl.ID = cols.Rows[i]["CUSTOM_CONTROL_NAME"].ToString();
                    tcControl.Controls.Add(cbl);
                    break;
                default:
                    break;
            }
            //如果本行是一行显示的情况，则newRowFlag还是为0
            if (cols.Rows[i]["CONTROL_DISPLAY_ONE_ROW"] != Convert.DBNull && cols.Rows[i]["CONTROL_DISPLAY_ONE_ROW"].ToString() == "1")
            {
                tcControl.ColumnSpan = 3;
                newRowFlag = 0;
            }
            else if (i < cols.Rows.Count-1 && newRowFlag % 2 == 0 && cols.Rows[i + 1]["CONTROL_DISPLAY_ONE_ROW"] != Convert.DBNull && cols.Rows[i + 1]["CONTROL_DISPLAY_ONE_ROW"].ToString() == "1")   //第二列的控件是一行显示
            {
                tcControl.ColumnSpan = 3;
                newRowFlag = 0;
            }
            else
                newRowFlag++;

            tr.Cells.Add(tcControl);

            if (newRowFlag % 2 == 0 || i == cols.Rows.Count - 1)
            {
                t.Rows.Add(tr);   //如果下一列要新增加行，则结束加入此行到表格中，否则要增加第二列
                //newRowFlag ++;
            }
        }
        detail_data.Controls.Add(t);
    }

    //填充DropDownList控件
    private void FillDropDownList(ref DropDownList ddl, string sql)
    {
        DataTable val = DBOpt.dbHelper.GetDataTable(sql);
        if (val == null) return;
        DataRow row = val.NewRow();
        val.Rows.InsertAt(row, 0);
        //第一列是数据列
        ddl.DataValueField = val.Columns[0].ColumnName;
        //如果有第二列，则是显示列，没有则第一列也是显示列。
        if(val.Columns.Count>1)
            ddl.DataTextField = val.Columns[1].ColumnName;
        else
            ddl.DataTextField = val.Columns[0].ColumnName;

        ddl.DataSource = val;
        ddl.DataBind();
    }

    //填充HtmlComboBox控件
    private void FillHtmlComboBox(ref HtmlComboBox hcb, string sql)
    {
        DataTable val = DBOpt.dbHelper.GetDataTable(sql);
        if (val == null) return;
        DataRow row = val.NewRow();
        val.Rows.InsertAt(row, 0);
        //第一列是数据列
        hcb.DataValueField = val.Columns[0].ColumnName;
        //如果有第二列，则是显示列，没有则第一列也是显示列。
        if (val.Columns.Count > 1)
            hcb.DataTextField = val.Columns[1].ColumnName;
        else
            hcb.DataTextField = val.Columns[0].ColumnName;

        hcb.DataSource = val;
        hcb.DataBind();
    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl;
        HtmlComboBox hcb;
        string selectedValue;    //关联控件所选择的值
        string relatedCols;      //被关联列s
        string relatedCons;      //被关联条件s
        DataTable temp;          //被关联列的相应信息
        Control wc;
        string ControlID;        //控件ID

        if (sender is DropDownList)
        {
            ddl = (DropDownList)sender;
            if (ddl.SelectedItem == null) return;
            selectedValue = ddl.SelectedItem.Value;
            ControlID = ddl.ID;
        }
        else if (sender is HtmlComboBox)
        {
            hcb = (HtmlComboBox)sender;
            if (hcb.SelectedItem == null) return;
            selectedValue = hcb.SelectedItem.Value;
            ControlID = hcb.ID;
        }
        else
            return;


        //查找相关联列
        DataTable dt = DBOpt.dbHelper.GetDataTable("select * from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableID"].ToString() + " and CUSTOM_CONTROL_NAME='" + ControlID + "'");
        if (dt.Rows.Count != 1) return;
        if (dt.Rows[0]["RELATING_COLUMN"] == null || dt.Rows[0]["RELATING_COLUMN"].ToString().Trim().Length == 0) return; //没有关联列
        if (dt.Rows[0]["RELATING_CONDITION"] == null || dt.Rows[0]["RELATING_CONDITION"].ToString().Trim().Length == 0) return; //没有关联列
        relatedCols = dt.Rows[0]["RELATING_COLUMN"].ToString();
        relatedCons = dt.Rows[0]["RELATING_CONDITION"].ToString();
        string[] cols = relatedCols.Split(',');
        string[] cons = relatedCons.Split(',');
        for (int i = 0; i < cols.GetLength(0); i++)
        {
            //找被关联列的充值表达式
            _sql = "select CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,FILL_EXPRESSION from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableID"].ToString() + " and NAME='" + cols[i] + "'";
            temp = DBOpt.dbHelper.GetDataTable(_sql);
            if (temp == null || temp.Rows.Count < 1 ) continue;
            if (temp.Rows[0]["FILL_EXPRESSION"] == Convert.DBNull) continue;
            _sql = temp.Rows[0]["FILL_EXPRESSION"].ToString() + " where " + cons[i] + "'" + selectedValue + "'";   //条件的格式是: 列名1操作符1,列名2操作符2,列名3操作符3.......  Col1=,Col2>,.....

            wc = Page.FindControl(temp.Rows[0]["CUSTOM_CONTROL_NAME"].ToString());
            if (wc is DropDownList)
            {
                ddl = (DropDownList)wc;
                FillDropDownList(ref ddl, _sql);
            }
            else if (wc is HtmlComboBox)
            {
                hcb = (HtmlComboBox)wc;
                FillHtmlComboBox(ref hcb, _sql);
            }
        }
    }

    //找到此列（HtmlComboBox，DropDownList是否被关联），如果是，则先不填充值
    private bool isRelated(string colName)
    {
        
        if (DBHelper.databaseType == "Oracle")
        {
            _sql = "select count(*) from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableID"].ToString() +" and instr(','|| RELATING_COLUMN || ',',',' || '" + colName + "' || ',')>0";
            obj = DBOpt.dbHelper.ExecuteScalar(_sql);
            if (obj == null || Convert.ToInt16(obj) < 1)
                return false;
            else
                return true;
        }
        else if (DBHelper.databaseType == "SqlServer")
        {
        }
        else if (DBHelper.databaseType == "Sybase")
        {
        }
        else
        {
            
        }
        return false;
    }

}

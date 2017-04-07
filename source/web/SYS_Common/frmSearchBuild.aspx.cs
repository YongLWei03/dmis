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
using PlatForm.CustomControlLib;
using System.Text;
using System.Data.Common;
using System.Globalization;
using System.Threading;


public partial class frmSearchBuild : System.Web.UI.Page
{
    private string sql;

    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MemberID"] == null)  JScript.ReturnLogin();

        if (!IsPostBack)
        {
            if (Session["FuncName"] != null) tdTitle.InnerText = Session["FuncName"].ToString();
            Session["TableName"] = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + Session["MainTableId"].ToString());
            //加前提条件。
            if(Request["Precondition"]!=null && Request["Precondition"]!="")
            {
                ViewState["Precondition"] = Request["Precondition"];
            }

            createNewRow();
            StringBuilder sbValues = new StringBuilder();
            DropDownList ddlColumns, ddlGuanXi, ddlRoJi;
            HtmlComboBox cbbValue;

            //列名
            ddlColumns = (DropDownList)tblQuery.Rows[1].Cells[0].Controls[0];
            sbValues.Append(ddlColumns.SelectedValue + "▲");
            //关系符
            ddlGuanXi = (DropDownList)tblQuery.Rows[1].Cells[1].Controls[0];
            sbValues.Append(ddlGuanXi.SelectedValue + "▲");
            //值
            cbbValue = (HtmlComboBox)tblQuery.Rows[1].Cells[2].Controls[0];
            sbValues.Append(cbbValue.Text + "▲");
            //逻辑符
            ddlRoJi = (DropDownList)tblQuery.Rows[1].Cells[3].Controls[0];
            sbValues.Append(ddlRoJi.SelectedValue + "▲◆");

            ViewState["values"] = sbValues.Remove(sbValues.Length - 1, 1).ToString();
        }
        else
        {
            if (ViewState["values"] != null)  buildTable();
        }
    }

    private void createNewRow()
    {
        TableRow tr = new TableRow();
        tr.HorizontalAlign = HorizontalAlign.Center;
        tr.Height = new Unit("25px");
        //加列
        TableCell tc1 = new TableCell();
        tc1.Width = new Unit("150px");
        DropDownList ddlColumn = new DropDownList();
        ddlColumn.ID = "column" + (tblQuery.Rows.Count + 1).ToString();
        ddlColumn.Width = new Unit("145px");
        initColumns(ref ddlColumn);
        ddlColumn.AutoPostBack = true;
        ddlColumn.SelectedIndexChanged += new EventHandler(ddlColumns_SelectedIndexChanged);
        tc1.Controls.Add(ddlColumn);
        tr.Cells.Add(tc1);

        //加关系符
        TableCell tc2 = new TableCell();
        tc2.Width = new Unit("75px");
        DropDownList ddlGuanXi = new DropDownList();
        ddlGuanXi.Width = new Unit("70px");
        tc2.Controls.Add(ddlGuanXi);
        tr.Cells.Add(tc2);

        //加值
        TableCell tc3 = new TableCell();
        tc3.Width = new Unit("180px");
        HtmlComboBox cbbValue = new HtmlComboBox();
        cbbValue.ID = "value" + (tblQuery.Rows.Count + 1).ToString();
        cbbValue.Width = new Unit("175px");
        tc3.Controls.Add(cbbValue);
        tr.Cells.Add(tc3);

        //加逻辑符
        TableCell tc4 = new TableCell();
        tc4.Width = new Unit("65px");
        DropDownList ddlRoJi = new DropDownList();
        ddlRoJi.ID = "rojin" + (tblQuery.Rows.Count + 1).ToString();
        initRoJi(ref ddlRoJi);
        ddlRoJi.SelectedIndex = -1;
        ddlRoJi.Width = new Unit("60px");
        ddlRoJi.AutoPostBack = true;
        ddlRoJi.SelectedIndexChanged += new EventHandler(ddlRoJi_SelectedIndexChanged);
        tc4.Controls.Add(ddlRoJi);
        tr.Cells.Add(tc4);

        //加删除按钮
        TableCell tc5 = new TableCell();
        tc5.Width = new Unit("50px");
        ImageButton imb = new ImageButton();
        imb.ID = "del" + (tblQuery.Rows.Count + 1).ToString();
        imb.Attributes.Add("onclick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild001").ToString() + ");");  ////要翻译
        imb.ImageUrl = "../img/delete.gif";
        imb.Click += new ImageClickEventHandler(ImageButton_Click);
        tc5.Controls.Add(imb);
        tr.Cells.Add(tc5);

        tblQuery.Rows.Add(tr);

        ddlColumns_SelectedIndexChanged(ddlColumn, null);

    }

    protected void ddlColumns_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlColmns = (DropDownList)sender;
        if (ddlColmns.ID == null) return;
        int row = Convert.ToInt16(ddlColmns.ID.Substring(6));
        HtmlComboBox cbbValue = (HtmlComboBox)tblQuery.Rows[row - 1].Cells[2].Controls[0];
        cbbValue.Items.Clear();
        cbbValue.Text = "";

        //关系操作符
        DropDownList ddlGuanxi=(DropDownList)tblQuery.Rows[row - 1].Cells[1].Controls[0];  
        string colType ;
        object obj = DBOpt.dbHelper.ExecuteScalar("select TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString()+" and NAME='" + ddlColmns.SelectedValue+"'");
        if(obj==null)
            colType="String";
        else
            colType=obj.ToString();

        int type;

        //列的类型不同,填充的关系操作符也不相同
        if (colType == "String")
        {
            if (DBHelper.databaseType == "Sybase")
            {
                sql = "select b.type from " + "webdmis.dbo.sysobjects a,webdmis.dbo.syscolumns b where a.id=b.id and a.name='" + Session["TableName"].ToString() + "' and b.name='" + ddlColmns.SelectedValue + "'";
                type = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(sql));
                if (type == 35)
                {
                    initGuanXiByText(ref ddlGuanxi);
                    sql = "select " + ddlColmns.SelectedValue + " from  " + Session["TableName"].ToString();
                }
                else
                {
                    initGuanXiByString(ref ddlGuanxi);
                    sql = "select distinct " + ddlColmns.SelectedValue + " from  " + Session["TableName"].ToString() + " order by " + ddlColmns.SelectedValue + " desc ";
                }
            }
            else if (DBHelper.databaseType == "Oracle")
            {
                initGuanXiByString(ref ddlGuanxi);
                sql = "select distinct " + ddlColmns.SelectedValue + " from  " + Session["TableName"].ToString() + " order by " + ddlColmns.SelectedValue + " desc ";
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                sql = "select b.type from " + "webdmis.dbo.sysobjects a,webdmis.dbo.syscolumns b where a.id=b.id and a.name='" + Session["TableName"].ToString() + "' and b.name='" + ddlColmns.SelectedValue + "'";
                type = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar(sql));
                if (type == 35)
                {
                    initGuanXiByText(ref ddlGuanxi);
                    sql = "select " + ddlColmns.SelectedValue + " from  " + Session["TableName"].ToString();
                }
                else
                {
                    initGuanXiByString(ref ddlGuanxi);
                    sql = "select distinct " + ddlColmns.SelectedValue + " from  " + Session["TableName"].ToString() + " order by " + ddlColmns.SelectedValue + " desc ";
                }
            }
            else
            {
            }
        }
        else if (colType == "Datetime")
        {
            initGuanXiByIntAndTime(ref ddlGuanxi);
            if (DBHelper.databaseType == "Sybase")
                sql = "select distinct convert(char(11)," + ddlColmns.SelectedValue + ",111) + convert(char(5)," + ddlColmns.SelectedValue + ",108)  from  " + Session["TableName"].ToString() + " a order by " + ddlColmns.SelectedValue + " desc ";
            else if (DBHelper.databaseType == "Oracle")
                //sql = "select distinct to_char("+ ddlColmns.SelectedValue + ",'dd-MM-yyyy hh24:mi') from  " + Session["TableName"].ToString() + " a order by " + ddlColmns.SelectedValue + " desc ";
                sql = "select to_char(a." + ddlColmns.SelectedValue + ",'dd-MM-yyyy hh24:mi') from  " + Session["TableName"].ToString() + " a order by " + ddlColmns.SelectedValue + " desc ";
            else if (DBHelper.databaseType == "SqlServer")
                sql = "select distinct convert(char(16)," + ddlColmns.SelectedValue + ",121) " + ddlColmns.SelectedValue + " from  " + Session["TableName"].ToString() + " a order by " + ddlColmns.SelectedValue + " desc ";
            else
                sql = "";
        }
        else if (colType == "Numeric")
        {
            initGuanXiByIntAndTime(ref ddlGuanxi);
            sql = "select distinct " + ddlColmns.SelectedValue + " from  " + Session["TableName"].ToString() + " order by " + ddlColmns.SelectedValue + " desc ";
        }
        else
        {
        }


        DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
        int i = 0;
        while (dr.Read() && i < 100)   //最多加100个
        {
            cbbValue.Items.Add(dr[0].ToString());
            i++;
        }
        dr.Close();
    }

    protected void ddlRoJi_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        int rows = Convert.ToInt16(ddl.ID.Substring(5));

        if (rows != tblQuery.Rows.Count) return;   //不是最后一条，不增加新行
        createNewRow();
        GenerateViewState();
    }

    private void initColumns(ref DropDownList ddl)
    {
        if (Session["Culture"] == null || Session["Culture"].ToString() == "zh-CN")
            sql = "select a.NAME NAME,a.DESCR DESCR from DMIS_SYS_COLUMNS a,DMIS_SYS_TABLES b where a.TABLE_ID=b.ID and a.TABLE_ID=" + Session["MainTableId"] + " order by a.ORDER_ID";
        else
            sql = "select a.NAME NAME,a.OTHER_LANGUAGE_DESCR DESCR from DMIS_SYS_COLUMNS a,DMIS_SYS_TABLES b where a.TABLE_ID=b.ID and a.TABLE_ID=" + Session["MainTableId"] + " order by a.ORDER_ID";
        
        DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
        ddl.Items.Clear();
        ddl.DataSource = dt;
        ddl.DataTextField = "DESCR";
        ddl.DataValueField = "NAME";
        ddl.DataBind();
    }

    private void buildTable()
    {
        createTableHeader();

        string[] aRow = ViewState["values"].ToString().Split('◆');

        for (int i = 0; i < aRow.Length; i++)
        {
            string[] aValues = aRow[i].Split('▲');

            TableRow tr = new TableRow();
            tr.HorizontalAlign = HorizontalAlign.Center;
            tr.Height = new Unit("25px");

            //加列
            TableCell tc1 = new TableCell();
            tc1.Width = new Unit("150px");
            DropDownList ddlColumn = new DropDownList();
            ddlColumn.ID = "column" + (tblQuery.Rows.Count + 1).ToString();
            ddlColumn.Width = new Unit("145px");
            initColumns(ref ddlColumn);
            ddlColumn.AutoPostBack = true;
            ddlColumn.SelectedIndexChanged += new EventHandler(ddlColumns_SelectedIndexChanged);
            ddlColumn.SelectedIndex = ddlColumn.Items.IndexOf(ddlColumn.Items.FindByValue(aValues[0]));
            tc1.Controls.Add(ddlColumn);
            tr.Cells.Add(tc1);

            //加关系符
            TableCell tc2 = new TableCell();
            tc2.Width = new Unit("75px");
            DropDownList ddlGuanXi = new DropDownList();
            ddlGuanXi.Width = new Unit("70px");
            string colType;
            object obj = DBOpt.dbHelper.ExecuteScalar("select TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and NAME='" + ddlColumn.SelectedValue + "'");
            if (obj == null)
                colType = "String";
            else
                colType = obj.ToString();
            if (colType == "String")
                initGuanXiByString(ref ddlGuanXi);
            else
                initGuanXiByIntAndTime(ref ddlGuanXi);
            ddlGuanXi.SelectedIndex = ddlGuanXi.Items.IndexOf(ddlGuanXi.Items.FindByValue(aValues[1]));
            tc2.Controls.Add(ddlGuanXi);
            tr.Cells.Add(tc2);

            //加值
            TableCell tc3 = new TableCell();
            tc3.Width = new Unit("180px");
            HtmlComboBox cbbValue = new HtmlComboBox();
            cbbValue.ID = "value" + (tblQuery.Rows.Count + 1).ToString();
            cbbValue.Height = ddlGuanXi.Height;
            cbbValue.Width = new Unit("175px");
            cbbValue.Text = aValues[2];
            tc3.Controls.Add(cbbValue);
            tr.Cells.Add(tc3);

            //加逻辑符
            TableCell tc4 = new TableCell();
            tc4.Width = new Unit("65px");
            DropDownList ddlRoJi = new DropDownList();
            ddlRoJi.ID = "rojin" + (tblQuery.Rows.Count + 1).ToString();
            initRoJi(ref ddlRoJi);
            ddlRoJi.SelectedIndex = ddlRoJi.Items.IndexOf(ddlRoJi.Items.FindByValue(aValues[3]));
            ddlRoJi.Width = new Unit("60px");
            ddlRoJi.AutoPostBack = true;
            ddlRoJi.SelectedIndexChanged += new EventHandler(ddlRoJi_SelectedIndexChanged);
            tc4.Controls.Add(ddlRoJi);
            tr.Cells.Add(tc4);

            //加删除按钮
            TableCell tc5 = new TableCell();
            tc5.Width = new Unit("50px");
            ImageButton imb = new ImageButton();
            imb.ID = "del" + (tblQuery.Rows.Count + 1).ToString();
            imb.Attributes.Add("onclick", "return confirm('" + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild001").ToString() + "');");   //要翻译
            imb.ImageUrl = "~/img/delete.gif";
            imb.ToolTip = GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild002").ToString(); //要翻译
            imb.Click += new ImageClickEventHandler(ImageButton_Click);
            tc5.Controls.Add(imb);
            tr.Cells.Add(tc5);

            tblQuery.Rows.Add(tr);
        }

    }

    /// <summary>
    /// 当列的类型是数值和时间时,填充关系符
    /// </summary>
    /// <param name="ddl"></param>
    private void initGuanXiByIntAndTime(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild003").ToString(), "="));//要翻译
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild004").ToString(), "<"));//要翻译
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild005").ToString(), ">"));//要翻译
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild006").ToString(), ">="));//要翻译
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild007").ToString(), "<="));//要翻译
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild008").ToString(), "<>"));//要翻译
    }

    /// <summary>
    /// 当列的类型是字符时,填充关系符
    /// </summary>
    /// <param name="ddl"></param>
    private void initGuanXiByString(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild009").ToString(), "like"));//要翻译
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild003").ToString(), "="));//要翻译
        //字符串在执行如下注释的操作时，会出现无法预知的错误，在比较西班牙文格式的字符串时，如13-09-2010就比25-06-2010小，实际正好相反。
        //ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild004").ToString(), "<"));//要翻译
        //ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild005").ToString(), ">"));//要翻译
        //ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild006").ToString(), ">="));//要翻译
        //ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild007").ToString(), "<="));//要翻译
        //ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild008").ToString(), "<>"));//要翻译
    }

    /// <summary>
    /// 当列的类型是Text时,填充关系符
    /// </summary>
    /// <param name="ddl"></param>
    private void initGuanXiByText(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild009").ToString(), "like"));//要翻译
    }

    private void initRoJi(ref DropDownList ddl)
    {
        ddl.Items.Add(new ListItem("", ""));
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild010").ToString(), "and"));//要翻译
        ddl.Items.Add(new ListItem(GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild011").ToString(), "or"));//要翻译
    }

    //删除事件
    protected void ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        GenerateViewState();
        ImageButton btn = (ImageButton)sender;
        int row = Convert.ToInt16(btn.ID.Substring(3));  //哪行删除
        StringBuilder sb = new StringBuilder();
        string[] aRow = ViewState["values"].ToString().Split('◆');

        if (aRow.Length == 1) return;   //最后一行不删除

        for (int i = 0; i < aRow.Length; i++)
        {
            if (i + 2 == row) continue;  //删除的行不加
            sb.Append(aRow[i] + "◆");
        }
        ViewState["values"] = sb.Remove(sb.Length - 1, 1).ToString();
        buildTable();
    }

    //生成ViewState
    private void GenerateViewState()
    {
        StringBuilder sbValues = new StringBuilder();
        DropDownList ddlColumns, ddlGuanXi, ddlRoJi;
        HtmlComboBox cbbValue;
        for (int i = 1; i < tblQuery.Rows.Count; i++)
        {
            //列名
            ddlColumns = (DropDownList)tblQuery.Rows[i].Cells[0].Controls[0];
            sbValues.Append(ddlColumns.SelectedValue + "▲");
            //关系符
            ddlGuanXi = (DropDownList)tblQuery.Rows[i].Cells[1].Controls[0];
            sbValues.Append(ddlGuanXi.SelectedValue + "▲");
            //值
            cbbValue = (HtmlComboBox)tblQuery.Rows[i].Cells[2].Controls[0];
            sbValues.Append(cbbValue.Text + "▲");
            //逻辑符
            ddlRoJi = (DropDownList)tblQuery.Rows[i].Cells[3].Controls[0];
            sbValues.Append(ddlRoJi.SelectedValue + "▲◆");
        }
        ViewState["values"] = sbValues.Remove(sbValues.Length - 1, 1).ToString();
    }

    //创建表头
    private void createTableHeader()
    {
        tblQuery.Rows.Clear();
        TableRow tr = new TableRow();
        tr.HorizontalAlign = HorizontalAlign.Center;
        //加列
        TableCell tc1 = new TableCell();
        tc1.Width = new Unit("150px");
        tc1.Text = GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild012").ToString();//要翻译
        tr.Cells.Add(tc1);

        //加关系符
        TableCell tc2 = new TableCell();
        tc2.Width = new Unit("75px");
        tc2.Text = GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild013").ToString();//要翻译
        tr.Cells.Add(tc2);

        //加值
        TableCell tc3 = new TableCell();
        tc3.Width = new Unit("180px");
        tc3.Text = GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild014").ToString();//要翻译
        tr.Cells.Add(tc3);

        //加逻辑符
        TableCell tc4 = new TableCell();
        tc4.Width = new Unit("65px");
        tc4.Text = GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild015").ToString();//要翻译
        tr.Cells.Add(tc4);

        //加删除按钮
        TableCell tc5 = new TableCell();
        tc5.Width = new Unit("50px");
        tc5.Text = GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild016").ToString();//要翻译
        tr.Cells.Add(tc5);

        tblQuery.Rows.Add(tr);
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        StringBuilder sbQuickWheres = new StringBuilder();
        StringBuilder sbQuickWheresDesc = new StringBuilder();  //自定义查询条件的中文描述，用于显示给用户看

        GenerateViewState();
        buildTable();

        StringBuilder sbMessage = new StringBuilder();
        DropDownList ddlColumn, ddlGuanXi, ddlRoJi;
        HtmlComboBox cbbValue;
        string colType;
        object obj;
        DateTime dt;
        double d;

        //判断取的值是否有效,主要是针对时间类型的数据
        for (int j = 1; j < tblQuery.Rows.Count; j++)
        {
            ddlColumn = (DropDownList)tblQuery.Rows[j].Cells[0].Controls[0];
            cbbValue = (HtmlComboBox)tblQuery.Rows[j].Cells[2].Controls[0];
            obj = DBOpt.dbHelper.ExecuteScalar("select TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + Session["MainTableId"].ToString() + " and NAME='" + ddlColumn.SelectedValue + "'");
            if (obj == null)
                colType = "String";
            else
                colType = obj.ToString();

            if (colType == "Datetime")
            {
                try
                {
                    dt = DateTime.Parse(cbbValue.Text, new CultureInfo("es-ES"));
                    if (cbbValue.Text.Trim().Length < 10) sbMessage.Append(ddlColumn.SelectedItem.Text + ":" + cbbValue.Text + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild017").ToString() + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild018").ToString());//要翻译
                }
                catch
                {
                    sbMessage.Append(ddlColumn.SelectedItem.Text + ":" + cbbValue.Text + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild017").ToString() + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild018").ToString());//要翻译
                }

                //if (!DateTime.TryParse(cbbValue.Text, out dt))
                //{
                //    sbMessage.Append(ddlColumn.SelectedItem.Text + ":" + cbbValue.Text + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild017").ToString() + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild018").ToString());//要翻译
                //}
                //else
                //{
                //    if (cbbValue.Text.Trim().Length < 10) sbMessage.Append(ddlColumn.SelectedItem.Text + ":" + cbbValue.Text + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild017").ToString() + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild018").ToString());//要翻译
                //}
            }
            else if (colType == "Numeric")
            {
                if (!Double.TryParse(cbbValue.Text, out d))
                {
                    sbMessage.Append(ddlColumn.SelectedItem.Text + ":" + cbbValue.Text + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild017").ToString() + GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild019").ToString());//要翻译
                }
            }
            else
            {
            }
        }

        if (sbMessage.Length > 0)
        {
            tdMessage.InnerText = sbMessage.ToString();
            //JScript.Alert(sbMessage.ToString());
            buildTable();
            return;
        }

        string firstValue = "";
        if (tblQuery.Rows.Count == 2)  //只有一行，则值没有数据的情况时，不用考虑自定义的查询
        {
            HtmlComboBox cbbFirstValue;
            cbbFirstValue = (HtmlComboBox)tblQuery.Rows[1].Cells[2].Controls[0];
            firstValue = cbbFirstValue.Text;
        }
        StringBuilder sbWheres = new StringBuilder();
        StringBuilder sbWheresDesc = new StringBuilder();  //自定义查询条件的中文描述，用于显示给用户看

        if (!(tblQuery.Rows.Count == 2 && firstValue == ""))
        {
            for (int i = 1; i < tblQuery.Rows.Count; i++)
            {
                ddlColumn = (DropDownList)tblQuery.Rows[i].Cells[0].Controls[0];
                ddlGuanXi = (DropDownList)tblQuery.Rows[i].Cells[1].Controls[0];
                cbbValue = (HtmlComboBox)tblQuery.Rows[i].Cells[2].Controls[0];
                ddlRoJi = (DropDownList)tblQuery.Rows[i].Cells[3].Controls[0];

                obj = DBOpt.dbHelper.ExecuteScalar("select TYPE from DMIS_SYS_COLUMNS a,DMIS_SYS_TABLES b where a.TABLE_ID=b.ID and b.ID="+Session["MainTableId"].ToString()+" and  a.NAME='" + ddlColumn.SelectedValue+"'");
                if (obj == null)
                    colType = "String";
                else
                    colType = obj.ToString();


                if (i != tblQuery.Rows.Count - 1)    //处理最后一个逻辑符时的操作
                {
                    if (ddlGuanXi.SelectedValue != "like")
                    {

                        if (colType == "Datetime")
                        {
                            if (DBHelper.databaseType == "Oracle")
                            {
                                sbWheres.Append( ddlColumn.SelectedValue);
                                sbWheres.Append(ddlGuanXi.SelectedValue);
                                sbWheres.Append("to_date('" + cbbValue.Text + "','dd-MM-yyyy hh24:mi')");
                                sbWheres.Append(ddlRoJi.Text != "" ? ddlRoJi.SelectedValue : "and");
                                sbWheres.Append(" ");
                            }
                            else
                            {
                                sbWheres.Append(ddlColumn.SelectedValue);
                                sbWheres.Append(ddlGuanXi.SelectedValue);
                                sbWheres.Append(" '");
                                sbWheres.Append(cbbValue.Text);
                                sbWheres.Append("' ");
                                sbWheres.Append(ddlRoJi.Text != "" ? ddlRoJi.SelectedValue : "and");
                                sbWheres.Append(" ");
                            }
                        }
                        else
                        {
                            sbWheres.Append(ddlColumn.SelectedValue);
                            sbWheres.Append(ddlGuanXi.SelectedValue);
                            if (colType != "Numeric") sbWheres.Append(" '");
                            sbWheres.Append(cbbValue.Text);
                            if (colType != "Numeric") sbWheres.Append("' ");
                            sbWheres.Append(ddlRoJi.Text != "" ? ddlRoJi.SelectedValue : "and"); //没有选择逻辑符则为and
                            sbWheres.Append(" ");
                        }
                    }
                    else
                    {
                        sbWheres.Append(ddlColumn.SelectedValue);
                        sbWheres.Append("  ");
                        sbWheres.Append(ddlGuanXi.SelectedValue);
                        sbWheres.Append("  '%");
                        sbWheres.Append(cbbValue.Text);
                        sbWheres.Append("%' ");
                        sbWheres.Append(ddlRoJi.Text != "" ? ddlRoJi.SelectedValue : "and"); //没有选择逻辑符则为and
                        sbWheres.Append(" ");
                    }
                    sbWheresDesc.Append("[" + ddlColumn.SelectedItem.Text + "] " + ddlGuanXi.SelectedItem.Text + " " + cbbValue.Text + " " + ddlRoJi.SelectedItem.Text + " ");
                }
                else
                {   //最后一项不加逻辑符
                    if (ddlGuanXi.SelectedValue != "like")
                    {
                        if (colType == "Datetime")
                        {
                            if (DBHelper.databaseType == "Oracle")
                            {
                                sbWheres.Append(ddlColumn.SelectedValue);
                                sbWheres.Append(ddlGuanXi.SelectedValue);
                                sbWheres.Append("to_date('" + cbbValue.Text + "','dd-MM-yyyy hh24:mi')");
                            }
                            else
                            {
                                sbWheres.Append(ddlColumn.SelectedValue);
                                sbWheres.Append(ddlGuanXi.SelectedValue);
                                sbWheres.Append(" '");
                                sbWheres.Append(cbbValue.Text);
                                sbWheres.Append("' ");
                            }
                        }
                        else
                        {
                            sbWheres.Append(ddlColumn.SelectedValue);
                            sbWheres.Append(ddlGuanXi.SelectedValue);
                            if (colType != "Numeric") sbWheres.Append(" '");
                            sbWheres.Append(cbbValue.Text);
                            if (colType != "Numeric") sbWheres.Append("' ");
                        }
                    }
                    else
                    {
                        sbWheres.Append(ddlColumn.SelectedValue);
                        sbWheres.Append("  ");
                        sbWheres.Append(ddlGuanXi.SelectedValue);
                        sbWheres.Append("  '%");
                        sbWheres.Append(cbbValue.Text);
                        sbWheres.Append("%' ");
                    }
                    sbWheresDesc.Append("[" + ddlColumn.SelectedItem.Text + "] " + ddlGuanXi.SelectedItem.Text + " " + cbbValue.Text + " ");
                }
            }
        }


        if (sbWheres.Length > 0)
        {
            if (ViewState["Precondition"] != null)   //加前提条件
                Session["SearchWheres"] = ViewState["Precondition"].ToString()+" and ("+sbWheres.ToString()+")";
            else
                Session["SearchWheres"] = sbWheres.ToString();
            Session["SearchWheresDesc"] = sbWheresDesc.ToString();
        }
        else
        {
            Session["SearchWheres"] = "";
            Session["SearchWheresDesc"] = "";
        }

        
        if (Session["SearchWheres"].ToString().Trim().Length > 0)
        {
            Response.Redirect("frmSearchResult.aspx");
        }
        else
        {
            tdMessage.InnerText = GetGlobalResourceObject("WebGlobalResource", "ComSearchBuild020").ToString();//要翻译
            return;
        }
    }

 


}

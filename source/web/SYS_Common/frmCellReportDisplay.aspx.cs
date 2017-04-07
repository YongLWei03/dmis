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
using PlatForm.DmisReport;
using System.Drawing;
using System.Data.Common;
using PlatForm.CustomControlLib;
using System.Text;
using System.Threading;
using System.Globalization;

public partial class frmCellReportDisplay : System.Web.UI.Page
{
    string _sql;
    Hashtable htDDL;

    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["UICulture"].ToString());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
        base.InitializeCulture();
    }

    protected void Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfHostID.Value = Request.UserHostAddress;   //用户要求在办公网和SCADA网内都要打印，则先判断用户在哪个网上发送请求

            if (Request["ReportID"] != null && Request["ReportID"].Trim() != "")
            {
                CellReport report = new CellReport(Convert.ToInt16(Request["ReportID"]), Session["UICulture"].ToString());
                tdReportName.InnerText = report.Name;
                hdnCellFileName.Value = report.CellFileName;
                ViewState["report"] = report;
                hdnReportPath.Value = System.Configuration.ConfigurationManager.AppSettings["ReportPath"];
            }
            else
            {
                JScript.Alert("");//没有报表参数,无法打印,将关闭此窗口!
                JScript.CloseWindow();
            }
        }
        if (ViewState["report"] != null)
            buildQeruyParaControl();
        else
        {
            JScript.Alert("");//报表参数失效,窗口将关闭!
            JScript.CloseWindow();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //放值,在业务中往此页面传值时,值的顺序和格式要和平台报表参数中设置的一样,否则不会正确显示数据

            if (Request["Values"] != null && Request["Values"] != "")
            {
                CellReport report = (CellReport)ViewState["report"];
                HtmlComboBox ddl;
                string[] values = Request["Values"].Split('^');
                for (int i = 0; i <= report.Paras.GetUpperBound(0); i++)
                {
                    ddl = (HtmlComboBox)Page.FindControl(report.Paras[i, 0]);
                    ddl.Text = values[i];
                    report.Paras[i, 4] = values[i];
                }
                btnDisplay_Click(null, null);  //把数据显示出来
            }
        }
    }


    /// <summary>
    /// 生成检索控件
    /// </summary>
    private void buildQeruyParaControl()
    {
        CellReport report = (CellReport)ViewState["report"];
        htDDL = new Hashtable();
        int counts = 0;

        System.Web.UI.WebControls.Table t = new System.Web.UI.WebControls.Table();
        t.Width = new Unit("90%");
        t.BorderWidth = new Unit("0px");
        t.CellPadding = 0;
        t.CellSpacing = 1;
        t.BackColor = System.Drawing.Color.DarkGray;


        for (int i = 0; i <= report.Paras.GetUpperBound(0); i++)
        {
            TableRow tr = new TableRow();
            TableCell tcDescr = new TableCell();
            tcDescr.BorderWidth = new Unit("1px");
            tcDescr.BackColor = Color.DarkGray;
            tcDescr.HorizontalAlign = HorizontalAlign.Center;
            tcDescr.VerticalAlign = VerticalAlign.Middle;
            tcDescr.Text = report.Paras[i, 1];
            tcDescr.ToolTip = report.Paras[i, 0];
            tr.Cells.Add(tcDescr);

            TableCell tcControl = new TableCell();
            tcControl.BorderWidth = new Unit("1px");
            tcControl.BackColor = Color.White;

            HtmlComboBox ddl = new HtmlComboBox();
            
            ddl.ID = report.Paras[i, 0];
            ddl.ToolTip = report.Paras[i, 0];
            ddl.Width = new Unit("200px");
            
            for (int j = 0; j < report.TableParas.Count; j++)
            {
                TablePara tp = (TablePara)report.TableParas[j];
                if (":" + report.Paras[i, 0] == tp.ParaCode)
                {
                    _sql = "select distinct " + tp.ColumnName + " from " + tp.TableName + " order by " + tp.ColumnName + " desc";
                    DbDataReader drValue = DBOpt.dbHelper.GetDataReader(_sql);
                    while (drValue.Read())
                    {
                        if (counts > 99) break;  //只放100个值
                        if(ddl.Items.FindByText(drValue[0].ToString().Trim())==null) //没有找到则加
                            ddl.Items.Add(drValue[0].ToString().Trim());
                    }
                    drValue.Close();
                }
            }

            //依赖列
            for (int k = i + 1; k <= report.Paras.GetUpperBound(0); k++)
            {
                if (report.Paras[k, 3] == report.Paras[i, 0])   //从i参数后面开始找其它参数是否关联它了
                {
                    ddl.AutoPostBack = true;
                    ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                    break;
                }
            }

            tcControl.Controls.Add(ddl);
            tr.Cells.Add(tcControl);
            htDDL.Add(ddl.ToolTip, ddl);
            t.Rows.Add(tr);
        }
        tdControl.Controls.Add(t);
    }

    private void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        int pos = 0; //关联参数名称在tableParas中的位置
        string sql;
        int counts = 0;
        Control cn;
        HtmlComboBox ddlNext;  //被关联的参数
        HtmlComboBox ddl = (HtmlComboBox)sender;  //正在选择时的参数
        int id = Convert.ToInt16(ddl.ID);
        CellReport report = (CellReport)ViewState["report"];


        for (int i = 0; i <= report.Paras.GetUpperBound(0); i++)
        {
            if (report.Paras[i, 3] == id.ToString())   //此此参数被后面的关联
            {
                cn = Page.FindControl(report.Paras[i, 0]);   //被关联的控件
                if (cn == null) continue;
                ddlNext = (HtmlComboBox)cn;
                ddlNext.Items.Clear();

                for (int j = 0; j < report.TableParas.Count; j++)   //扫描tableParas数组，看看所被关联参数如何从数据库中取值
                {
                    TablePara tp = (TablePara)report.TableParas[j];
                    TablePara tp2=new TablePara();
                    if (tp.ParaCode == (":" + i.ToString()))
                    {
                        pos = j;
                        tp2 = (TablePara)report.TableParas[pos];
                    }
                    if (":" + report.Paras[i, 0] == tp.ParaCode)
                    {
                        ddlNext.Items.Clear();
                        ddlNext.Text = "";
                        sql = "select distinct " + tp2.ColumnName + " from " + tp2.TableName + " where " + tp2.ColumnName + "='" + ddl.SelectedValue + "'";
                        DbDataReader drValue = DBOpt.dbHelper.GetDataReader(sql);
                        while (drValue.Read())
                        {
                            if (counts > 99) break;  //只放100个值
                            ddlNext.Items.Add(drValue[0].ToString().Trim());
                        }
                        drValue.Close();
                    }
                }
            }
        }
    }

    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        CellReport report = (CellReport)ViewState["report"];
        HtmlComboBox ddl;  //被关联的参数

        //取值
        for (int i = 0; i <= report.Paras.GetUpperBound(0); i++)
        {
            ddl = (HtmlComboBox)Page.FindControl(report.Paras[i, 0]);
            if (ddl.Text == "")
            {
                //JScript.Alert("参数:" + report.Paras[i, 1] + "还没有选择数据!");
                JScript.Alert( report.Paras[i, 1] );
                return;
            }
            else
            {
                report.Paras[i, 4] = ddl.Text;
            }
        }

        hdnValue.Value = report.Display();   //值
        hdnPageNo.Value = report.PagesOrRows;    //页数,如果是0,则在客户端不用增加页码
    }






}

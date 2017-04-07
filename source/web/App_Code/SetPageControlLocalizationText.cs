using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.Globalization;
using System.Threading;

/// <summary>
/// 设置页面通用控件,比如添加、删除、修改等本地化文本，以减少相关翻译的工作量
/// 在设计翻译工具时，这些通用控件就不要导入了
/// 分列表和细节页面的设置函数。
/// 只能取全局资源，还不能取本地资源（如功能名称），故在相应的面代码中还要写本地资源相关的代码。
/// </summary>
public class PageControlLocalizationText:TemplateControl
{
    private Page curPage;
    private Button bt;
    private Label lb;

    public PageControlLocalizationText(Page p)
    {
        curPage = p;
    }
    /// <summary>
    /// 设置列表界面语言
    /// </summary>
    public void SetListPageControlLocalizationText()
    {
        //检索
        if (curPage.FindControl("btnQuery") != null)
        {
            bt = (Button)curPage.FindControl("btnQuery");
            bt.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Retrieve");
        }
        //添加
        if (curPage.FindControl("btnAdd") != null)
        {
            bt = (Button)curPage.FindControl("btnAdd");
            bt.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Add");
        }
        //删除
        if (curPage.FindControl("btnDelete") != null)
        {
            bt = (Button)curPage.FindControl("btnDelete");
            bt.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Delete");
        }
        //修改
        if (curPage.FindControl("btnModify") != null)
        {
            bt = (Button)curPage.FindControl("btnModify");
            bt.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Modify");
        }
        //查询
        if (curPage.FindControl("btnSearch") != null)
        {
            bt = (Button)curPage.FindControl("btnSearch");
            bt.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Search");
        }
        //排序
        if (curPage.FindControl("btnSort") != null)
        {
            bt = (Button)curPage.FindControl("btnSort");
            bt.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Sort");
        }
        //打印
        if (curPage.FindControl("btnPrint") != null)
        {
            bt = (Button)curPage.FindControl("btnPrint");
            bt.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Print");
        }
        //首页
        if (curPage.FindControl("btnFirst") != null)
        {
            bt = (Button)curPage.FindControl("btnFirst");
            bt.Text = "|<";
            bt.ToolTip = (String)GetGlobalResourceObject("WebGlobalResource", "PageFirst");
            bt.Width = new Unit("50px");
        }
        //上一页
        if (curPage.FindControl("btnPrevious") != null)
        {
            bt = (Button)curPage.FindControl("btnPrevious");
            bt.Text = "<<";
            bt.ToolTip = (String)GetGlobalResourceObject("WebGlobalResource", "PagePrevious");
            bt.Width = new Unit("50px");
        }
        //下一页
        if (curPage.FindControl("btnNext") != null)
        {
            bt = (Button)curPage.FindControl("btnNext");
            bt.Text = ">>";
            bt.ToolTip = (String)GetGlobalResourceObject("WebGlobalResource", "PageNext");
            bt.Width = new Unit("50px");
        }
        //末页
        if (curPage.FindControl("btnLast") != null)
        {
            bt = (Button)curPage.FindControl("btnLast");
            bt.Text = ">|";
            bt.ToolTip = (String)GetGlobalResourceObject("WebGlobalResource", "PageLast");
            bt.Width = new Unit("50px");
        }
        //页确定
        if (curPage.FindControl("btnTurn") != null)
        {
            bt = (Button)curPage.FindControl("btnTurn");
            bt.Text = (String)GetGlobalResourceObject("WebGlobalResource", "PageTurnOk");
        }
        //转向
        if (curPage.FindControl("lblTurn") != null)
        {
            lb = (Label)curPage.FindControl("lblTurn");
            lb.Text = (String)GetGlobalResourceObject("WebGlobalResource", "PageTurn");
        }
    }

    /// <summary>
    /// 设置细节页面控件语言
    /// </summary>
    public void SetDetailPageControlLocalizationText()
    {
        //保存
        if (curPage.FindControl("btnSave") != null)
        {
            bt = (Button)curPage.FindControl("btnSave");
            bt.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Save");
        }
        //返回
        if (curPage.FindControl("btnReturn") != null)
        {
            bt = (Button)curPage.FindControl("btnReturn");
            bt.Text = (String)GetGlobalResourceObject("WebGlobalResource", "Return");
        }
    }

}

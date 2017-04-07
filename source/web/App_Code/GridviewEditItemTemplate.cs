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
using System.Globalization;

/// <summary>
/// 2009-4-15 敖云峰
/// 用于动态生成Gridview控件中的TemplateField列
/// 主要用于参数设置界面SYS_Common/frmSetParamsByGridView.aspx
/// </summary>
/// 
public class GridviewEditItemTemplate : ITemplate
{
    private string tableID;
    private string colname;

    public GridviewEditItemTemplate(string tableID,string colname)
    {
        this.tableID = tableID;
        this.colname = colname; 
    } 

    public void InstantiateIn(Control container)    
    {
        //从参数表中找相应配置的控件名
        object obj;
        obj = DBOpt.dbHelper.ExecuteScalar("select custom_control_name from DMIS_SYS_COLUMNS where table_id="+tableID+" and name='"+this.colname+"'");
        if (obj != null)
        {
            TextBox t = new TextBox();
            t.ID = obj.ToString();
            t.DataBinding += new EventHandler(this.OnDataBinding);
            container.Controls.Add(t);
        }
    } 
   
    public void OnDataBinding(object sender, EventArgs e)    
    {
         TextBox t = (TextBox)sender;
         GridViewRow container = (GridViewRow)t.NamingContainer;
         t.Text = ((DataRowView)container.DataItem)[colname].ToString();
    }
}

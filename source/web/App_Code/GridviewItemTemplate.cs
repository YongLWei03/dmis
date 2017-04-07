using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 2009-4-15 敖云峰
/// 用于动态生成Gridview控件中的TemplateField列
/// 主要用于参数设置界面SYS_Common/frmSetParamsByGridView.aspx
/// </summary>
public class GridviewItemTemplate : ITemplate
{
     private string colname;

     public GridviewItemTemplate(string colname)
     {
         this.colname = colname; 
     } 

     public void InstantiateIn(Control container)    
     {
         Label l = new Label();
         l.DataBinding += new EventHandler(this.OnDataBinding);
         container.Controls.Add(l); 
     } 
   
     public void OnDataBinding(object sender, EventArgs e)    
     {
         Label l = (Label)sender;        
         GridViewRow container = (GridViewRow)l.NamingContainer;        
         l.Text = ((DataRowView)container.DataItem)[colname].ToString();
     }

}

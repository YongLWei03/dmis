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
using System.Text;

public partial class left : System.Web.UI.Page
{
    DataTable _dt,_dtAll;
    string _sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //标准程序的SQL语句,只找所能看见的功能
            if (Session["Culture"] != null && Session["Culture"].ToString() == "zh-CN")
                _sql = "select distinct a.ID ID,a.NAME NAME,a.FILE_NAME FILE_NAME,a.TARTGET TARTGET,a.EXPAND_IMAGE EXPAND_IMAGE,a.PARENT_ID PARENT_ID,a.ORDER_ID from DMIS_SYS_TREEMENU a,DMIS_SYS_TREEMENU_ROLE_VISIBLE b where a.ID=b.MODULE_ID and b.ROLE_ID in( " + Session["RoleIDs"].ToString() + ") order by a.ORDER_ID";
            else
                _sql = "select distinct a.ID ID,a.OTHER_LANGUAGE_DESCR NAME,a.FILE_NAME FILE_NAME,a.TARTGET TARTGET,a.EXPAND_IMAGE EXPAND_IMAGE,a.PARENT_ID PARENT_ID,a.ORDER_ID from DMIS_SYS_TREEMENU a,DMIS_SYS_TREEMENU_ROLE_VISIBLE b where a.ID=b.MODULE_ID and b.ROLE_ID in( " + Session["RoleIDs"].ToString() + ") order by a.ORDER_ID";
            
            _dt = DBOpt.dbHelper.GetDataTable(_sql);

            //标准程序构造树形列表,把下行代码注释取消,同时把其后面的代码注释掉即可
            BuildTree(null);

            //河北邢台用户要求的SQL语句,列出所有的功能
            //动态增加一列,来标记是否可见
            //_sql = "select distinct ID,NAME,FILE_NAME,TARTGET,EXPAND_IMAGE,PARENT_ID,ORDER_ID from DMIS_SYS_TREEMENU order by PARENT_ID,ORDER_ID";
            //_dtAll = DBOpt.dbHelper.GetDataTable(_sql);
            //DataColumn IsVisible=new DataColumn("IsVisible");
            //IsVisible.DataType = System.Type.GetType("System.Int16");
            //_dtAll.Columns.Add(IsVisible);

            //DataRow[] foundRows;
            //for (int i = 0; i < _dtAll.Rows.Count; i++)
            //{
            //    foundRows = _dt.Select("ID=" + _dtAll.Rows[i]["ID"].ToString());
            //    if (foundRows == null || foundRows.Length < 1)   //不可见
            //        _dtAll.Rows[i]["IsVisible"] = 0;
            //    else
            //        _dtAll.Rows[i]["IsVisible"] = 1;
            //}
            ////河北邢台 构造树形列表
            //BuildAllTree(null);
        }
    }


    /// <summary>
    /// 标准程序,只显示能见的项
    /// 赤峰项目采用
    /// </summary>
    /// <param name="tn"></param>
    private void BuildTree(TreeNode tn)
    {
        int i;
        // 空节点时创建根节点，父ID为NULL的当作根节点
        if (tn == null)
        {
            trvTreeMenu.Nodes.Clear();
            for (i = 0; i < _dt.Rows.Count; i++)
            {
                if (_dt.Rows[i]["PARENT_ID"].ToString() == "0")
                {
                    TreeNode tmp = new TreeNode();
                    tmp.Text = _dt.Rows[i]["NAME"].ToString();
                    tmp.Value = _dt.Rows[i]["ID"].ToString();
                    if (_dt.Rows[i]["FILE_NAME"] == Convert.DBNull || _dt.Rows[i]["FILE_NAME"].ToString().Trim() == "")
                    {
                        tmp.NavigateUrl = "#";
                        tmp.Target = null;
                    }
                    else
                    {
                        tmp.NavigateUrl = _dt.Rows[i]["FILE_NAME"].ToString() + "?ID=" + tmp.Value;
                        tmp.Target = _dt.Rows[i]["TARTGET"] != Convert.DBNull ? _dt.Rows[i]["TARTGET"].ToString() : "main";
                    }
                    tmp.ImageUrl = _dt.Rows[i]["EXPAND_IMAGE"] != Convert.DBNull ? _dt.Rows[i]["EXPAND_IMAGE"].ToString() : "img/parentNode.gif";
                    trvTreeMenu.Nodes.Add(tmp);
                }
            }
            // 循环递归创建树
            for (i = 0; i < trvTreeMenu.Nodes.Count; i++)
            {
                BuildTree(trvTreeMenu.Nodes[i]);
            }
        }
        else // 节点非空为递归调用
        {
            for (i = 0; i < _dt.Rows.Count; i++)
            {
                if (tn.Value == _dt.Rows[i]["PARENT_ID"].ToString())
                {
                    TreeNode tmp = new TreeNode(_dt.Rows[i]["name"].ToString());
                    tmp.Text = _dt.Rows[i]["NAME"].ToString();
                    tmp.Value = _dt.Rows[i]["ID"].ToString();
                    if (_dt.Rows[i]["FILE_NAME"] == Convert.DBNull || _dt.Rows[i]["FILE_NAME"].ToString().Trim() == "")
                    {
                        tmp.NavigateUrl = "#";
                        tmp.Target = null;
                    }
                    else
                    {
                        tmp.NavigateUrl = _dt.Rows[i]["FILE_NAME"].ToString() + "?ID=" + tmp.Value;
                        tmp.Target = _dt.Rows[i]["TARTGET"] != Convert.DBNull ? _dt.Rows[i]["TARTGET"].ToString() : "main";
                    }
                    tmp.ImageUrl = _dt.Rows[i]["EXPAND_IMAGE"] != Convert.DBNull ? _dt.Rows[i]["EXPAND_IMAGE"].ToString() : "img/parentNode.gif";
                    tn.ChildNodes.Add(tmp);
                }
            }
            for (i = 0; i < tn.ChildNodes.Count; i++)
            {
                BuildTree(tn.ChildNodes[i]);
            }
        }
    }


    /// <summary>
    /// 2007-10-15  河北邢台用户要求显示所有的功能,如果没有权限操作的功能,则用灰色显示此项,点击时没有反应
    /// 赤几项目没有采用
    /// </summary>
    /// <param name="tn"></param>
    private void BuildAllTree(TreeNode tn)
    {
        int i;
        // 空节点时创建根节点，父ID为NULL的当作根节点
        if (tn == null)
        {
            trvTreeMenu.Nodes.Clear();
            for (i = 0; i < _dtAll.Rows.Count; i++)
            {
                if (_dtAll.Rows[i]["PARENT_ID"].ToString() == "0")
                {
                    TreeNode tmp = new TreeNode();
                    tmp.Text = _dtAll.Rows[i]["NAME"].ToString();
                    tmp.Value = _dtAll.Rows[i]["ID"].ToString();
                    if (_dtAll.Rows[i]["IsVisible"].ToString() == "1")
                    {
                        if (_dtAll.Rows[i]["FILE_NAME"] == Convert.DBNull || _dtAll.Rows[i]["FILE_NAME"].ToString().Trim() == "")
                        {
                            tmp.NavigateUrl = "#";
                            tmp.Target = null;
                        }
                        else
                        {
                            tmp.NavigateUrl = _dtAll.Rows[i]["FILE_NAME"].ToString() + "?ID=" + tmp.Value;
                            tmp.Target = _dtAll.Rows[i]["TARTGET"] != Convert.DBNull ? _dtAll.Rows[i]["TARTGET"].ToString() : "main";
                        }
                        tmp.ImageUrl = _dtAll.Rows[i]["EXPAND_IMAGE"] != Convert.DBNull ? _dtAll.Rows[i]["EXPAND_IMAGE"].ToString() : "img/parentNode.gif";
                    }
                    else
                    {
                        tmp.NavigateUrl = "#";
                        tmp.Target = null;
                        tmp.ToolTip = "对不起,您无权访问此功能!";
                        tmp.ImageUrl = "img/ico_key.gif";
                    }
                    trvTreeMenu.Nodes.Add(tmp);
                }
            }
            // 循环递归创建树
            for (i = 0; i < trvTreeMenu.Nodes.Count; i++)
            {
                BuildAllTree(trvTreeMenu.Nodes[i]);
            }
        }
        else // 节点非空为递归调用
        {
            for (i = 0; i < _dtAll.Rows.Count; i++)
            {
                if (tn.Value == _dtAll.Rows[i]["PARENT_ID"].ToString())
                {
                    TreeNode tmp = new TreeNode();//_dtAll.Rows[i]["NAME"].ToString()
                    tmp.Text = _dtAll.Rows[i]["NAME"].ToString();
                    tmp.Value = _dtAll.Rows[i]["ID"].ToString();
                    if (_dtAll.Rows[i]["IsVisible"].ToString() == "1")
                    {

                        if (_dtAll.Rows[i]["FILE_NAME"] == Convert.DBNull || _dtAll.Rows[i]["FILE_NAME"].ToString().Trim() == "")
                        {
                            tmp.NavigateUrl = "#";
                            tmp.Target = null;
                        }
                        else
                        {
                            tmp.NavigateUrl = _dtAll.Rows[i]["FILE_NAME"].ToString() + "?ID=" + tmp.Value;
                            tmp.Target = _dtAll.Rows[i]["TARTGET"] != Convert.DBNull ? _dtAll.Rows[i]["TARTGET"].ToString() : "main";
                        }
                        tmp.ImageUrl = _dtAll.Rows[i]["EXPAND_IMAGE"] != Convert.DBNull ? _dtAll.Rows[i]["EXPAND_IMAGE"].ToString() : "img/parentNode.gif";
                    }
                    else
                    {
                        tmp.NavigateUrl = "#";
                        tmp.Target = null;
                        tmp.ToolTip = "对不起,您无权访问此功能!";
                        tmp.ImageUrl = "img/ico_key.gif";
                    }
                    tn.ChildNodes.Add(tmp);
                }
            }

            for (i = 0; i < tn.ChildNodes.Count; i++)
            {
                BuildAllTree(tn.ChildNodes[i]);
            }
        }
    }

}

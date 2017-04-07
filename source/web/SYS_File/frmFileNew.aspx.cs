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

using System.Data.Common;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.IO;
using System.Globalization;

public partial class frmFileNew : PageBaseDetail
{
    private string _sql;
    private DataTable _dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        info = detail_info;
        if (!IsPostBack)
        {
            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetDetailPageControlLocalizationText();
            lblFuncName.Text = Session["FuncName"].ToString();
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());

            if (Request["TID"] == null || Request["TID"] == "")
            {   
                txtMODULE_ID.Text = Session["FuncId"].ToString();
                wdlNEW_DATE.setTime(DateTime.Now);
                txtMEMBER.Text = Session["MemberName"].ToString();
            }
            else
            {
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_FILE_INFO", "TID=" + Request["TID"]);
                _sql = "select * from T_FILE_ACCESSORIES where FILE_ID=" + Request["TID"];
                _dt = DBOpt.dbHelper.GetDataTable(_sql);
                grvUpFileList.DataSource = _dt;
                grvUpFileList.DataBind();
            }
        }
    }

    protected void btnSaveCancel_Click(object sender, EventArgs e)
    {
        btnSave_Click(null, null);
        if (detail_info.InnerText.Length > 0) return;
        Response.Redirect(Session["URL"].ToString());
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
            info.InnerText = "";
            WebLog.InsertLog("", "成功", sql);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (Session["FilePath"] == null)
        {
            detail_info.InnerText=GetGlobalResourceObject("WebGlobalResource", "FileNoPath").ToString();  // "没有设置上传文件的存入路径，请联系管理员！"
            return;
        }
        if (fulFile.FileName == "")
        {
            detail_info.InnerText=GetGlobalResourceObject("WebGlobalResource", "FileNoAccessories").ToString();   //没有上传的附件！
            return;
        }
        //if (fulFile.PostedFile.ContentLength > 4096000)
        //{
        //    JScript.Alert(this.Page, "上传文件大小不允许超过4M！");
        //    return;
        //}

        uint maxTID;
        int type;
        string fileName, fileSuffix;
        object obj;
        maxTID = DBOpt.dbHelper.GetMaxNum("T_FILE_ACCESSORIES", "TID");
        fileName=fulFile.FileName.Substring(fulFile.FileName.LastIndexOf(@"\") + 1);
        fileSuffix = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();   //统一为小写
        _sql="select TID from T_FILE_TYPE where SUFFIX like '%"+fileSuffix+"%'";
        obj=DBOpt.dbHelper.ExecuteScalar(_sql);
        if(obj==null)
            type=1;   //其它文件
        else
            type=Convert.ToInt16(obj);

        if (txtDESCR.Text.Trim() == "") txtDESCR.Text = fileName;

        //上传之前先保存,否则有孤立的文件存在
        btnSave_Click(null, null);
        if (detail_info.InnerText != "") return;

        //上传文件
        string mapname;
        mapname = Page.MapPath(Session["FilePath"].ToString());

        if (File.Exists(mapname + fileName))
        {
            detail_info.InnerText=GetGlobalResourceObject("WebGlobalResource", "FileIterativeMessage").ToString();   //服务器上存在同名的文件，请更改文件名
            return;
        }
        try
        {
            fulFile.SaveAs(mapname + fileName);
        }
        catch (Exception ex)
        {
            WebLog.InsertLog("上传文件", "失败","失败原因："+ex.Message);
            detail_info.InnerText=GetGlobalResourceObject("WebGlobalResource", "FileUploadFailMessage").ToString();  //上传文件失败，请联系管理员
            return;
        }

        //保存上传文件参数
        if(DBHelper.databaseType=="Oracle")
            _sql = "insert into T_FILE_ACCESSORIES(TID,FILE_ID,FILE_NAME,FILE_PATH,TYPE_ID,FILE_SUFFIX,UP_DATE) values(" +
                maxTID + "," + txtTID.Text + ",'" + fileName + "','" + Session["FilePath"].ToString() + "'," + type + ",'" + fileSuffix.ToLower() + "',TO_DATE('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','YYYY-MM-DD HH24:MI'))";
        else
            _sql = "insert into T_FILE_ACCESSORIES(TID,FILE_ID,FILE_NAME,FILE_PATH,TYPE_ID,FILE_SUFFIX,UP_DATE) values(" +
                maxTID + "," + txtTID.Text + ",'" + fileName + "','" + Session["FilePath"].ToString() + "'," + type + ",'" + fileSuffix.ToLower() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')";

        if (DBOpt.dbHelper.ExecuteSql(_sql) == -1)
        {
            WebLog.InsertLog("", "失败", _sql);
            detail_info.InnerText=GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage").ToString();//数据保存失败，请联系管理员！
            return;
        }

        _sql = "select * from T_FILE_ACCESSORIES where FILE_ID=" + txtTID.Text;
        _dt = DBOpt.dbHelper.GetDataTable(_sql);
        grvUpFileList.DataSource = _dt;
        grvUpFileList.DataBind();
    }


    protected void grvUpFileList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string path, fileName, mapname;
            path = grvUpFileList.Rows[Convert.ToInt16(e.CommandArgument)].Cells[2].Text;
            fileName = grvUpFileList.Rows[Convert.ToInt16(e.CommandArgument)].Cells[3].Text;
            _sql = "delete from T_FILE_ACCESSORIES where TID=" + grvUpFileList.Rows[Convert.ToInt16(e.CommandArgument)].Cells[0].Text;
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
            {
                mapname = Page.MapPath(path);
                if (File.Exists(mapname + fileName))
                {
                    File.Delete(mapname + fileName);
                }
                _sql = "select * from T_FILE_ACCESSORIES where FILE_ID=" + txtTID.Text;
                _dt = DBOpt.dbHelper.GetDataTable(_sql);
                grvUpFileList.DataSource = _dt;
                grvUpFileList.DataBind();
            }
            else
            {
                WebLog.InsertLog("", "失败", _sql);
                detail_info.InnerText=GetGlobalResourceObject("WebGlobalResource", "DeleteFailMessage").ToString();  //数据删除失败
                return;
            }
        }
    }

    protected void grvUpFileList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton delButton = (LinkButton)e.Row.Cells[5].Controls[0];
            delButton.Attributes.Add("onclick", "return confirm('"+GetGlobalResourceObject("WebGlobalResource", "DeleteMessage").ToString()+"');");
        }
    }
}

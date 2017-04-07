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

public partial class SYS_File_frmFileSingleNew : PageBaseDetail
{
    private string _sql;
    private DataTable _dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        info = detail_info;

        if (!IsPostBack)
        {
            Session["URL"] = Request["URL"];

            PageControlLocalizationText pl = new PageControlLocalizationText(this);
            pl.SetDetailPageControlLocalizationText();
            lblFuncName.Text = Session["FuncName"].ToString(); 
            SetRight.SetPageRight(this.Page, Session["FuncId"].ToString(), Session["RoleIDs"].ToString());

            if (Request["TID"] == null || Request["TID"] == "")
            {
                txtMODULE_ID.Text = Session["FuncId"].ToString();
                wdlDATEM.setTime(DateTime.Now);
                txtSCR.Text = Session["MemberName"].ToString();
                txtFILE_PATH.Text = Session["FilePath"].ToString();
            }
            else
            {
                CustomControlFill.CustomControlFillByTableAndWhere(this.Page, "T_FILE_SINGLE", "TID=" + Request["TID"]);
                if (txtFILE_NAME.Text.Trim() != "") btnUpload.Enabled = false;  //已经有文件的不允许再上传
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
            info.InnerText = "";
            WebLog.InsertLog("", "成功", sql);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //上传之前先保存,否则有孤立的文件存在
        btnSave_Click(null, null);
        if (info.InnerText != "") return;

        if (Session["FilePath"] == null)
        {
            info.InnerText = GetGlobalResourceObject("WebGlobalResource", "FileNoPath").ToString();   // "没有设置上传文件的存入路径，请联系管理员！";
            return;
        }
        if (fulFile.FileName == "")
        {
            info.InnerText = GetGlobalResourceObject("WebGlobalResource", "FileNoAccessories").ToString();  // "请先选择要上传的文件！";
            return;
        }

        //上传文件
        string mapname = Page.MapPath(Session["FilePath"].ToString());
        string fileName = fulFile.FileName.Substring(fulFile.FileName.LastIndexOf(@"\") + 1);
        if (File.Exists(mapname + fileName))
        {
            info.InnerText = GetGlobalResourceObject("WebGlobalResource", "FileIterativeMessage").ToString();// "服务器上存在同名的文件，请更改文件名！";
            return;
        }
        try
        {
            fulFile.SaveAs(mapname + fileName);
            txtFILE_NAME.Text = fulFile.FileName.Substring(fulFile.FileName.LastIndexOf(@"\") + 1);
            if (txtDESCR.Text.Trim() == "")
                txtDESCR.Text = txtFILE_NAME.Text;
            txtFILE_SUFFIX.Text = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();   //统一为小写
            btnSave_Click(null, null);
        }
        catch (Exception ex)
        {
            WebLog.InsertLog("上传文件", "失败", "失败原因：" + ex.Message);
            info.InnerText = GetGlobalResourceObject("WebGlobalResource", "FileUploadFailMessage").ToString();  //上传文件失败，请联系管理员
            return;
        }
    }



}

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
using System.IO;

public partial class UpLoad : System.Web.UI.Page
{
    string sql;
    websvrfunction webfun = new websvrfunction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["PackNo"] == null)
            Response.Write("<script language=javascript>returnvalue='';self.close();</script>");
        else
            ViewState["PackNo"] = Request["PackNo"];
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
		if (MyFileInput.PostedFile != null) 
        {
            string fileName, fileSuffix, path;
            fileName = MyFileInput.FileName.Substring(MyFileInput.FileName.LastIndexOf(@"\") + 1);
            fileSuffix = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();   //统一为小写

            //判断上传的文件在服务器是否存在
            path = Server.MapPath("..\\upload\\");
            if (File.Exists(path + fileName))
            {
                JScript.Alert(this.Page, "服务器存在同名的文件，请修改文件名再上传！");
                return;
            }

			uint iMaxNo;
			uint iFileNo;
			iMaxNo = 0;

            iFileNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WK_FILE", "F_NO");
            try
            {
                MyFileInput.PostedFile.SaveAs(path + fileName);
            }
            catch (Exception ex)
            {
                WebLog.InsertLog("上传文件", "失败", "失败原因：" + ex.Message);
                JScript.Alert( "上传文件时失败，已保存失败信息，请联系管理员");
                return;
            }
            
            if (txtTitle.Text.Trim() == "") txtTitle.Text = fileName.Substring(0, fileName.IndexOf('.'));
            if (txtDesc.Text.Trim() == "") txtDesc.Text = fileName.Substring(0, fileName.IndexOf('.'));

            sql = "INSERT INTO DMIS_SYS_WK_FILE(F_NO,F_CAPTION,F_FILETYPE,F_FILENAME,F_DESC,F_PACKNO) VALUES("
                + iFileNo + ",'" + txtTitle.Text + "'" + ",'" + fileSuffix + "','" + fileName + "','" + txtDesc.Text + "'," + ViewState["PackNo"] + ")";
			DBOpt.dbHelper.ExecuteSql(sql);
			


            //感觉没有必要插数据到DMIS_SYS_DOC中。
            //iMaxNo = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_DOC", "F_NO");
            //sql = "INSERT INTO DMIS_SYS_DOC(F_NO,F_PACKNO,F_DOCTYPENO,F_DOCNAME,"
            //    + "F_CREATEMAN,F_CREATEDATE,F_TABLENAME,F_RECNO) " 
            //    + " VALUES(" + iMaxNo + "," + Session["PackNo"] + "," + Session["DocTypeNo"]
            //    + ",'" + txtTitle.Text + "','" + Session["MemberName"] + "','" + DateTime.Now.ToString("yyyy-MM-dd") 
            //    + "','DMIS_SYS_WK_FILE'," + iFileNo + ")";

			//DBOpt.dbHelper.ExecuteSql(sql);
		}
        else
        {
            JScript.Alert("请先选择要上传的文件！");
            return;
        }
        Response.Write(webfun.CloseWin("refreshPage"));
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Write(webfun.CloseWin(""));
    }


    protected void MyFileInput_DataBinding(object sender, EventArgs e)
    {
        if (MyFileInput.FileName != "")
        {
            string sTmp= MyFileInput.FileName;
            txtTitle.Text = sTmp.Substring(0,sTmp.LastIndexOf(".")-1);
            txtDesc.Text = "";
        }
    }
}

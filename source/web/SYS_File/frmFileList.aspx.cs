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
using System.IO;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.Threading;

public partial class frmFileList : System.Web.UI.Page
{

    string _sql;
    DataTable _dt;
    protected void Page_PreInit(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            _sql = "select a.TID,a.FILE_NAME, b.ICO from T_FILE_ACCESSORIES a,T_FILE_TYPE b where a.TYPE_ID=b.TID and a.FILE_ID=" + Session["FileTid"].ToString();
            _dt = DBOpt.dbHelper.GetDataTable(_sql);
            dlsFileList.DataSource = _dt;
            dlsFileList.DataBind();
        }
    }
  
    protected void dlsFileList_ItemCommand(object source, DataListCommandEventArgs e)
    {
        dlsFileList.SelectedIndex = e.Item.ItemIndex;
        string tid,path,fileName;
        tid = dlsFileList.DataKeys[e.Item.ItemIndex].ToString();
        if (dlsFileList.SelectedItem.Controls[5] is HyperLink)
        {
            HyperLink hl = (HyperLink)dlsFileList.SelectedItem.Controls[5];
            hl.ForeColor = System.Drawing.Color.Red;
        }

        ViewState["TID"] = tid;
        _sql="select FILE_NAME,FILE_PATH from T_FILE_ACCESSORIES where TID="+tid;
        _dt=DBOpt.dbHelper.GetDataTable(_sql);
        if (_dt.Rows.Count == 1)
        {
            path = _dt.Rows[0][1].ToString();
            fileName = _dt.Rows[0][0].ToString();
                
            string tt = "parent.frames[1].location.href ='" + path + fileName + "';";
            Response.Write("<script language=javascript>");
            Response.Write(tt);
            Response.Write("</script>");

            //if (e.CommandName == "DownLoad")
            //{
            //    ResponseFile(Request, Response, fileName, Server.MapPath(path + fileName), 10240);
            //}
        }

    }
    

    //public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
    //{
    //    try
    //    {
    //        FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    //        BinaryReader br = new BinaryReader(myFile,System.Text.Encoding.UTF8);
    //        try
    //        {
    //            _Response.AddHeader("Accept-Ranges", "bytes");
    //            _Response.Buffer = false;
    //            long fileLength = myFile.Length;
    //            long startBytes = 0;

    //            int pack = 10240; //10K bytes
    //            int sleep = (int)Math.Floor((decimal)1000 * pack / _speed) + 1;
    //            if (_Request.Headers["Range"] != null)
    //            {
    //                _Response.StatusCode = 206;
    //                string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
    //                startBytes = Convert.ToInt64(range[1]);
    //            }
    //            _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
    //            if (startBytes != 0)
    //            {
    //                _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
    //            }
    //            _Response.AddHeader("Connection", "Keep-Alive");
    //            _Response.ContentType = "application/octet-stream";
    //            _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

    //            br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
    //            int maxCount = (int)Math.Floor((decimal)(fileLength - startBytes) / pack) + 1;

    //            for (int i = 0; i < maxCount; i++)
    //            {
    //                if (_Response.IsClientConnected)
    //                {
    //                    _Response.BinaryWrite(br.ReadBytes(pack));
    //                    Thread.Sleep(sleep);
    //                }
    //                else
    //                {
    //                    i = maxCount;
    //                }
    //            }
    //            _Response.Flush();
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //        finally
    //        {
    //            br.Close();
    //            myFile.Close();
    //        }
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //    return true;
    //}

    protected void dlsFileList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lblTid = (Label)e.Item.FindControl("lblTID");
        HyperLink hpl = (HyperLink)e.Item.FindControl("hplFile");

        _sql = "select FILE_NAME,FILE_PATH from T_FILE_ACCESSORIES where TID=" + lblTid.Text;
        DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
        if (dt == null && dt.Rows.Count != 1)
        {
            hpl.Visible = false;
        }
        else
        {
            string fileName = dt.Rows[0][0].ToString();
            string pathname = dt.Rows[0][1].ToString();
            hpl.Text = fileName;
            hpl.Target = "_blank";
            hpl.NavigateUrl = pathname + HttpUtility.UrlEncode(fileName);
        }
    }
}

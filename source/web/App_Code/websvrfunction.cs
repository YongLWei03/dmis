using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using PlatForm.DBUtility;
using PlatForm.Functions;
using System.Data;



/// <summary>
/// websvrfunction 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class websvrfunction : System.Web.Services.WebService
{

    public websvrfunction()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
    String sSql;
    public void openPage(System.Web.UI.Page pg,string sRetPage) {
        DataTable dtTmp;
        dtTmp = DBOpt.dbHelper.GetDataTable("select * from dmis_sys_doctype where f_packtypedef=1 and f_packtypeno=" + Session["PackTypeNo"]);
        if (dtTmp != null)
        {
            if (dtTmp.Rows.Count == 1)
            {
                string sFileName = "";
                sFileName = dtTmp.Rows[0]["F_FormFile"].ToString();
                Session["DocTypeNo"] = dtTmp.Rows[0]["F_NO"].ToString();
                Session["TableName"] = dtTmp.Rows[0]["f_tablename"].ToString();
                if (sRetPage != "")
                    sFileName += "?BackUrl=" + sRetPage;
                pg.Response.Redirect(sFileName);
            }
        }
    }
    public void openPage(System.Web.UI.Page pg, string sFileName, int iEdit)
    {
        DataTable oleRd;
        string sFile;
        if (sFileName != "")
        {
            Session["FormFile"] = sFileName;
        }
        else
        {
            if (!(Session["DocNo"] == null))
            {
                if (FieldToValue.FieldToInt(Session["DocNo"]) > -1)
                {
                    sSql = "SELECT F_DOCTYPENO,F_RECNO,F_TABLENAME FROM DMIS_SYS_DOC WHERE F_NO=" + Session["DocNo"];
                    oleRd = DBOpt.dbHelper.GetDataTable(sSql);
                    if (oleRd.Rows.Count > 0)
                    {
                        Session["DocTypeNo"] = oleRd.Rows[0][0].ToString();
                        Session["RecNo"] = oleRd.Rows[0][1].ToString();
                        Session["TableName"] = oleRd.Rows[0][2].ToString();
                    }
                }
            }
            if (FieldToValue.FieldToInt(Session["DocTypeNo"]) == -2)
            {
                sFile = DBOpt.dbHelper.ExecuteScalar("SELECT F_FILENAME FROM DMIS_SYS_FILE WHERE F_NO=" + Session["RecNo"]).ToString();
                if (sFile != "")
                {
                    sFile = sFile.Replace("\\", "/");
                    sFile = sFile.Substring(sFile.LastIndexOf("/") + 1);
                }
                Session["FormFile"] = Server.MapPath("..\\upload\\") + sFile;
            }
            else 
            {
                sSql = "SELECT F_FORMFILE FROM DMIS_SYS_DOCTYPE WHERE F_NO=" + Session["DocTypeNo"];
                Session["FormFile"] = FieldToValue.FieldToString(DBOpt.dbHelper.ExecuteScalar(sSql)) + "?TID=" + Session["RecNo"];
                Session["oper"] = -1;
                if (iEdit > 0)
                {
                    string sRight = WebWorkFlow.sDocTypeRight(Convert.ToInt32(Session["DocTypeNo"]), Session["RoleIDs"].ToString());
                    if (sRight.Length == 7)
                    {
                        if (sRight.Substring(2, 1) == "1" | sRight.Substring(3, 1) == "1")
                        {
                            Session["oper"] = 1;
                        }
                    }
                }
            }
        }
        if (Session["FormFile"].ToString() != "")
        {
            oleRd = DBOpt.dbHelper.GetDataTable("select F_STYLE,F_TARGET from DMIS_SYS_filestyle where F_FILENAME='" + Session["FormFile"] + "'");
            string sTyle = "";
            string starget = "";
            if (oleRd.Rows.Count > 0)
            {
                sTyle = FieldToValue.FieldToString(oleRd.Rows[0][0].ToString());
                starget = FieldToValue.FieldToString(oleRd.Rows[0][1].ToString());
            }
            if (starget == "")
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script language=javascript>\r\n" );
                if (sTyle == "")
                {
                    sb.Append("window.open('" + Session["FormFile"] + "');\r\n" );
                }
                else
                {
                    sb.Append("window.open('" + Session["FormFile"] + "','','" + sTyle + "');\r\n" );
                }
                sb.Append("</script>");
                pg.Response.Write(sb.ToString());
            }
            else
            {
                pg.Response.Redirect(Session["FormFile"].ToString());
            }
        }
    }

    public string GetAccess(int iNo, string iRoleNoS, string sCat)
    {
        string sRight = "0000000";
        DataTable dt = new DataTable();
        dt = DBOpt.dbHelper.GetDataTable("SELECT F_ACCESS FROM DMIS_SYS_RIGHTS WHERE F_FOREIGNKEY=" + iNo + " AND F_ROLENO IN(" + iRoleNoS + ") AND F_CATGORY='" + sCat + "'  order by f_no");
        if (dt.Rows.Count > 0)
        {
            sRight = dt.Rows[0][0].ToString();
        }
      return sRight;
    }
    public string OpenWin(string sHyper, string sTitle, string sStyle)
    {
        if (sStyle == "")
        {
            sStyle = "height=358,width=445,scrollbars=yes,resizable=no";
        }
        return "<script language=javascript>window.open('" + sHyper + "','" + sTitle + "','" + sStyle + "');</script>";
    }

    public string CloseWin(string id1)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script language=javascript>\r\n");
        if (id1 != "")
        {
            sb.Append("var obj=window.opener.document.getElementById('" + id1 + "');\r\n" );
            sb.Append("if(obj !=undefined){\r\n" );
            sb.Append("if(obj.value=='1')\r\n" );
            sb.Append("obj.value='0';\r\n" );
            sb.Append("else\r\n" );
            sb.Append("obj.value='1';\r\n}" );
        }
        sb.Append("self.close();\r\n" );
        sb.Append("</script>");
        return (sb.ToString());
    }

    public string RefreshParentWin(string id1)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script language=javascript>\r\n");
        if (id1 != "")
        {
            sb.Append("var obj=window.opener.document.getElementById('" + id1 + "');\r\n");
            sb.Append("if(obj !=undefined){\r\n");
            sb.Append("if(obj.value=='1')\r\n");
            sb.Append("obj.value='0';\r\n");
            sb.Append("else\r\n");
            sb.Append("obj.value='1';\r\n}");
        }
        sb.Append("</script>");
        return (sb.ToString());
    }

    public string CloseWinByValue(string id1, string sval)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script language=javascript>\r\n" );
        if (id1 != "")
        {
            sb.Append("var obj=window.opener.document.getElementById('" + id1 + "');\r\n" );
            sb.Append("if(obj !=undefined) obj.value='" + sval + "';\r\n" );
        }
        sb.Append("self.close();\r\n" );
        sb.Append("</script>");
        return (sb.ToString());
    }
    public string ChangeColor(object ii)
    {
        string str1;
        if (FieldToValue.FieldToInt(ii) <= 0)
        {
            str1 = "<font color=red>" + FieldToValue.FieldToInt(ii).ToString() + "<font>";
        }
        else
        {
            str1 = FieldToValue.FieldToInt(ii).ToString();
        }
        return (str1);
    }

    public string GetIcon(int iDocTypeNo,string sIcon)
    {
        if(sIcon!="") return("../img/" + sIcon);
        sIcon=FieldToValue.FieldToString(DBOpt.dbHelper.ExecuteScalar("select f_iconfile from dmis_sys_doctype where f_no=" +iDocTypeNo));

        if ((sIcon == ""))
        {
            return ("../img/wendang4.gif");
        }
        if ((sIcon.IndexOf("/") < 0))
        {
            return ("../img/" + sIcon);
        }
        return (sIcon);
    }

}


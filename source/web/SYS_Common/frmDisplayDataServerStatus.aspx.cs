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
using System.Data.OracleClient;

public partial class SYS_Common_frmDisplayDataServerStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (DBHelper.isDoubleDatabase)
                tdIsDouble.InnerText = "√";
            else
                tdIsDouble.InnerText = "X";

            Object obj;
            obj = DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_ROLE");

            if (obj!=null)
                tdMainServerStatus.InnerText = "√";
            else
                tdMainServerStatus.InnerText = "X";

            if (OracleHelper.isDoubleDatabase)
            {
                OracleConnection slaveConn = new OracleConnection(DBHelper.slaveConnectString);
                OracleCommand slaveCmd = new OracleCommand();
                try
                {
                    if (slaveConn.State != ConnectionState.Open)
                        slaveConn.Open();
                    slaveCmd.Connection = slaveConn;
                    slaveCmd.CommandText = "select count(*) from DMIS_SYS_ROLE";
                    slaveCmd.CommandType = CommandType.Text;
                    obj = slaveCmd.ExecuteScalar();

                    if (obj != null)
                        tdSlaveServerStatus.InnerText = "√";
                    else
                        tdSlaveServerStatus.InnerText = "X";
                }
                catch (Exception ex)
                {
                    tdSlaveServerStatus.InnerText = "×";
                    OracleHelper.LogError("备服务器", "测试", ex.Message);
                }
                finally
                {
                     if (slaveConn.State == ConnectionState.Open) slaveConn.Close();
                }

            }

        }
    }
}

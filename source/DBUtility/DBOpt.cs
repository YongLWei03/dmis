using System;
using System.Collections.Generic;
using System.Text;

namespace PlatForm.DBUtility
{
    public static class DBOpt
    {
        public static DBHelper dbHelper;
        static DBOpt()
        {
            string databaseType = System.Configuration.ConfigurationManager.AppSettings["DatabaseType"];
            if (databaseType == "Oracle")
            {
                dbHelper = new OracleHelper();
            }
            else if (databaseType == "SqlServer")
            {
                dbHelper = new SQLHelper();
            }
            else if (databaseType == "Sybase")
            {
                dbHelper = new OleDbHelper();
            }
            else if (databaseType == "Access")
            {
                dbHelper = new OleDbHelper();
            }
            else
            {
            }
        }
    }

}

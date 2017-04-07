using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PlatForm.DBUtility;

namespace PlatForm
{
    public partial class frmSelectTable : Form
    {
        string _sql;
        Int32 _typeId;
        public frmSelectTable(Int32 typeId)
        {
            InitializeComponent();
            _typeId = typeId;
        }

        private void frmSelectTable_Load(object sender, EventArgs e)
        {
            if (DBHelper.databaseType == "Oracle")
            {
                _sql = "select username from all_users order by user_id";
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                _sql = "select name from master.dbo.sysdatabases order by name";
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                _sql = "select name from master.dbo.sysdatabases order by name";
            }
            else
            {
            }
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbbDataBase.Items.Add(dt.Rows[i][0].ToString());
            }
        }
         

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbbTable.SelectedItem == null) return;
            uint maxId;

            maxId = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_TABLES","ID");
            int count = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_TABLES where TYPE_ID=" + _typeId.ToString()));
            _sql = "insert into DMIS_SYS_TABLES(TYPE_ID,ID,OWNER,NAME,DESCR,ORDER_ID) values(" + _typeId.ToString() + "," + maxId.ToString() + ",'" + cbbDataBase.SelectedItem.ToString() + "','"
                + cbbTable.SelectedItem.ToString() + "','" + cbbTable.SelectedItem.ToString() + "'," + Convert.ToString(count*10) + ")";

            if (DBOpt.dbHelper.ExecuteSql(_sql) < 0)
            {
                Log.InsertLog("insert", "error", "add table£º"+_sql);
                MessageBox.Show(Main.Properties.Resources.Error, Main.Properties.Resources.Note);
                return;
            }

            this.Close();
            this.Dispose();
        }

        private void cbbDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbTable.Items.Clear();
            if (DBHelper.databaseType == "Oracle")
            {
                _sql = "select table_name from all_all_tables where owner='" + cbbDataBase.SelectedItem.ToString() + "' order by table_name";
            }
            else if (DBHelper.databaseType == "SqlServer")
            {
                _sql = "select name from " + cbbDataBase.SelectedItem.ToString() + ".dbo.sysobjects where type in ('U','V')  order by name";
            }
            else if (DBHelper.databaseType == "Sybase")
            {
                _sql = "select name from " + cbbDataBase.SelectedItem.ToString() + ".dbo.sysobjects where type in ('U','V') order by name ";
            }
            else
            {
            }
            
            DataTable dt = DBOpt.dbHelper.GetDataTable(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbbTable.Items.Add(dt.Rows[i][0].ToString());
            }
        }

    }
}
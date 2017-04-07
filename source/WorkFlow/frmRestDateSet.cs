using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using PlatForm.DBUtility;
using System.Globalization;


namespace PlatForm.WorkFlow
{
    public partial class frmRestDateSet : Form
    {
        private string _sql;

        public frmRestDateSet()
        {
            InitializeComponent();
        }

        private void frmRestDateSet_Load(object sender, EventArgs e)
        {
            
            dtpStartDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM")+"-01");
            dtpEndDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            tlbSearch_Click(null, null);
        }

        private void tlbSearch_Click(object sender, EventArgs e)
        {
            DateTime dtStart, dtEnd;
            dtStart = dtpStartDate.Value;
            dtEnd = dtpEndDate.Value;
            if (dtEnd < dtStart)
            {
                //MessageBox.Show("开始日期不能大于结束日期！");
                return;
            }
            initRestDate(dtStart.ToString("yyyyMMdd"), dtEnd.ToString("yyyyMMdd"));
        }

        private void tlbSetRestDate_Click(object sender, EventArgs e)
        {
            DateTime dtStart, dtEnd,dtTemp;
            object obj;
            string week,temp;
            StringBuilder sql=new StringBuilder();
            dtStart = dtpStartDate.Value;
            dtEnd = dtpEndDate.Value;
            uint maxTid;
            if (dtEnd < dtStart)
            {
                //MessageBox.Show("开始日期不能大于结束日期！");
                return;
            }
            maxTid = DBOpt.dbHelper.GetMaxNum("DMIS_SYS_WK_RESTDAY","TID");
            dtTemp = dtStart;
            while (dtTemp <= dtEnd)
            {
                //先判断此日期是否已经存在表DMIS_SYS_WK_RESTDAY中
                obj = DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_WK_RESTDAY where to_char(RES_DATE,'YYYYMMDD')='" + dtTemp.ToString("yyyyMMdd") + "'");
                if(Convert.ToInt16(obj)>0) 
                {
                    dtTemp = dtTemp.AddDays(1);
                    continue;
                }
                
                switch (dtTemp.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        week = "lunes";//星期一
                        break;
                    case DayOfWeek.Tuesday:
                        week = "martes";//星期二
                        break;
                    case DayOfWeek.Wednesday:
                        week = "miércoles";//星期三
                        break;
                    case DayOfWeek.Thursday:
                        week = "jueves";//星期四
                        break;
                    case DayOfWeek.Friday:
                        week = "viernes";//星期五
                        break;
                    case DayOfWeek.Saturday:
                        week = "sábado";//星期六
                        break;
                    default:
                        week = "domingo";//星期日
                        break;
                }
                temp=dtTemp.ToString("yyyy-MM-dd");
                sql.Append( "insert into DMIS_SYS_WK_RESTDAY(TID,RES_DATE,RES_WEEKNAME,IS_HOLIDAY,NOTE) values(" +
                    maxTid + ",TO_DATE('" + temp  + "','YYYY-MM-DD'),'" + week+"',");
                if (dtTemp.DayOfWeek == DayOfWeek.Saturday || dtTemp.DayOfWeek == DayOfWeek.Sunday)  //周末是休息日
                    sql.Append("'1','fiesta')");   //休息日
                else
                {
                    //再判断是否是法定节假日  2010-3-8  赤几不判断节假日
                    //obj = DBOpt.dbHelper.ExecuteScalar("select count(*) from DMIS_SYS_WK_LEGAL_HOLIDAY where to_char(HOLIDAY_DATE,'YYYYMMDD')='" + dtTemp.ToString("yyyyMMdd") + "'");
                    //if (Convert.ToInt16(obj) == 1)
                    //    sql.Append("'1','fiesta')");
                    //else
                        sql.Append("'0','jornada')");  //工作日
                }

                if (DBOpt.dbHelper.ExecuteSql(sql.ToString()) > 0) maxTid++;
                sql.Remove(0, sql.Length);
                dtTemp = dtTemp.AddDays(1);
            }
            tlbSearch_Click(null, null);
        }

        private void initRestDate(string StartDate,string EndDate)
        {
            lsvRestDay.Items.Clear();
            ListViewItem lv;
            DataTable dt = DBOpt.dbHelper.GetDataTable("select RES_DATE,RES_WEEKNAME,IS_HOLIDAY,NOTE,TID" +
                " from DMIS_SYS_WK_RESTDAY where to_char(RES_DATE,'YYYYMMDD')>='" + StartDate + "' and to_char(RES_DATE,'YYYYMMDD')<='" + EndDate + "' order by RES_DATE");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lv = new ListViewItem(Convert.ToDateTime(dt.Rows[i][0]).ToString("dd-MM-yyyy"));
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (!(dt.Rows[i][j] is DBNull)) 
                        lv.SubItems.Add(dt.Rows[i][j].ToString());
                    else
                        lv.SubItems.Add("");
              
                }
                if (dt.Rows[i][2].ToString() == "1")  //是休息日则以背景色突出显示
                {
                    lv.BackColor=Color.LightGray;
                    foreach (ListViewItem.ListViewSubItem li in lv.SubItems)
                        li.BackColor = Color.LightGray;
                }
                lsvRestDay.Items.Add(lv);
            }
        }

        private void lsvRestDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvRestDay.SelectedItems.Count != 1) return;
            txtRES_DATE.Text = lsvRestDay.SelectedItems[0].Text;
            cbbIS_HOLIDAY.Text=lsvRestDay.SelectedItems[0].SubItems[2].Text;
            txtNOTE.Text=lsvRestDay.SelectedItems[0].SubItems[3].Text;
            txtTID.Text=lsvRestDay.SelectedItems[0].SubItems[4].Text;
        }

        //修改休息日状态
        private void lsvRestDay_DoubleClick(object sender, EventArgs e)
        {
            if (lsvRestDay.SelectedItems.Count != 1) return;
            DateTime dt = DateTime.Parse(lsvRestDay.SelectedItems[0].Text, new CultureInfo("es-ES"));
            if (lsvRestDay.SelectedItems[0].SubItems[2].Text == "1")
            {
                //if (MessageBox.Show("是否要把" + dt.ToString("yyyy年MM月dd日") + "改为工作日？", "注意!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
                _sql = "update DMIS_SYS_WK_RESTDAY set IS_HOLIDAY='0',note='jornada' where TID=" + lsvRestDay.SelectedItems[0].SubItems[4].Text;
                if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
                    tlbSearch_Click(null, null);
            }
            else
            {
                //if (MessageBox.Show("是否要把" + dt.ToString("yyyy年MM月dd日") + "改为休息日？", "注意!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
                _sql = "update DMIS_SYS_WK_RESTDAY set IS_HOLIDAY='1',note='fiesta' where TID=" + lsvRestDay.SelectedItems[0].SubItems[4].Text;
                if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
                    tlbSearch_Click(null, null);
            }
        }



        private void tlbDel_Click(object sender, EventArgs e)
        {
            if (lsvRestDay.SelectedItems.Count != 1) return;
            DateTime dt = Convert.ToDateTime(lsvRestDay.SelectedItems[0].Text);
            //if (MessageBox.Show("是否要删除" + dt.ToString("yyyy年MM月dd日") + "的记录？", "注意!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            _sql = "delete from DMIS_SYS_WK_RESTDAY where TID=" + lsvRestDay.SelectedItems[0].SubItems[4].Text;
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
                lsvRestDay.Items.Remove(lsvRestDay.SelectedItems[0]);
               //tlbSearch_Click(null, null);

        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            if (txtTID.Text == "") return;
             if (lsvRestDay.SelectedItems.Count != 1) return;
            _sql = "update DMIS_SYS_WK_RESTDAY set NOTE='" + txtNOTE.Text + "' where TID=" + txtTID.Text;
            if (DBOpt.dbHelper.ExecuteSql(_sql) > 0)
                lsvRestDay.SelectedItems[0].SubItems[3].Text = txtNOTE.Text;

        }

   

 
    }
}
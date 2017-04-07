using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PlatForm.Functions
{

    /// <summary>
    /// 弹出修改数据窗口时数据修改的状态
    /// </summary>
    public enum ModifyFlag
    {
        Add=0,
        Modify=1,
        Read=2
    }
    
    /// <summary>
    /// 把一些不好分类的公共功能放到此抽象类中
    /// </summary>
    public abstract class CMix
    {

        /// <summary>
        /// WindowForm里的ComboBox没找到根据VALUE值来确定SelectedIndex，故先根据VALUE找到TEXT，
        /// 再利用FindString函数来确定
        /// </summary>
        /// <param name="cbb">要查找的ComboBox控件</param>
        /// <param name="sValueFilter">条件表达式，如：VALUE列名＝1</param>
        /// <returns>找到的Index</returns>
        public static int GetComboBoxIndexByValue(ComboBox cbb, string sValueFilter)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = (System.Data.DataTable)cbb.DataSource;
            if (dt == null) return -1;

            System.Data.DataRow[] dr;
            dr = dt.Select(sValueFilter);
            if (dr.Length < 1)
            {
                return -1;
            }
            else
            {
                return cbb.FindString(dr[0][1].ToString());
            }
        }


        /// <summary> 
        /// 设置ListView控件交替背景颜色
        /// </summary> 
        /// <param name="lv">要设置颜色的ListView控件</param> 
        /// <param name="color1">偶数行的背景色</param> 
        /// <param name="color2">奇数行的背景色</param> 
        public static void SetListViewAlternatingBackColor(ListView lv, Color color1, Color color2)
        {
            foreach (ListViewItem lvItem in lv.Items)
            {
                if (lvItem.Index % 2 == 0)
                {
                    lvItem.BackColor = color1;
                }
                else
                {
                    lvItem.BackColor = color2;
                }

                foreach (ListViewItem.ListViewSubItem subItem in lvItem.SubItems)
                {
                    subItem.BackColor = lvItem.BackColor;
                }
            }
        }


        /// <summary>
        /// 根据窗口中的输入控件得到INSERT　SQL语句，然后交给OracleHelper处理
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">控件、值类型、值</param>
        /// <returns>得到的sql语句</returns>

    }

  

}

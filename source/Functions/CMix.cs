using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PlatForm.Functions
{

    /// <summary>
    /// �����޸����ݴ���ʱ�����޸ĵ�״̬
    /// </summary>
    public enum ModifyFlag
    {
        Add=0,
        Modify=1,
        Read=2
    }
    
    /// <summary>
    /// ��һЩ���÷���Ĺ������ܷŵ��˳�������
    /// </summary>
    public abstract class CMix
    {

        /// <summary>
        /// WindowForm���ComboBoxû�ҵ�����VALUEֵ��ȷ��SelectedIndex�����ȸ���VALUE�ҵ�TEXT��
        /// ������FindString������ȷ��
        /// </summary>
        /// <param name="cbb">Ҫ���ҵ�ComboBox�ؼ�</param>
        /// <param name="sValueFilter">�������ʽ���磺VALUE������1</param>
        /// <returns>�ҵ���Index</returns>
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
        /// ����ListView�ؼ����汳����ɫ
        /// </summary> 
        /// <param name="lv">Ҫ������ɫ��ListView�ؼ�</param> 
        /// <param name="color1">ż���еı���ɫ</param> 
        /// <param name="color2">�����еı���ɫ</param> 
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
        /// ���ݴ����е�����ؼ��õ�INSERT��SQL��䣬Ȼ�󽻸�OracleHelper����
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="fields">�ؼ���ֵ���͡�ֵ</param>
        /// <returns>�õ���sql���</returns>

    }

  

}

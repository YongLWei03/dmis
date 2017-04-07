using System;
using System.Collections.Generic;
using System.Collections;
using System.Web.Security;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Web.UI.HtmlControls;

using PlatForm.DBUtility;
using PlatForm.CustomControlLib;

namespace PlatForm.Functions
{
    /// <summary>
    /// ����ϸҳ�����ֵʱ��ͨ�����úõĲ�������䣬�ӿ����д�Ĺ���
    /// 
    /// </summary>
    public class CustomControlFill
    {
        /// <summary>
        /// ͨ��SQL���������DataRead,ע�⣺ֻ��ȡ��һ������,
        /// ���SQL����ܶ���������¼��ֻȡ��һ����
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sql"></param>
        public static void CustomControlFillByTableAndWhere(Page page,string TableName,string wheres)
        {
            string sql;
            int tableID;
            TextBox txt;
            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //����ǩ����,������Ӧ�˵Ŀ�����ܰ������ŵ��ؼ���
            CheckBoxList cbl;
            tableID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + TableName + "'"));
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);

            sql = "select * from " + TableName + " where " + wheres;
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   // if(dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull) continue;    //"û��ֵ���򲻴���"  ҲҪ����
                    if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] is System.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() == "") continue;   //ƽ̨�������ô��ж�Ӧ�Ŀؼ�����������
                    switch (dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString())
                    {
                        case "TextBox":
                            txt = (TextBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (txt != null)   //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    txt.Text = "";
                                else
                                    txt.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "WebDateLib":
                            wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (wdl != null)  //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    wdl.Text = "1900-1-1";
                                else
                                    wdl.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "WebComboBox":
                            wbx = (WebComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (wbx != null)   //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    wbx.Text = "";
                                else
                                    wbx.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "HtmlComboBox":
                            hbx = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (hbx != null)   //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    hbx.SelectedText = "";
                                else
                                    hbx.SelectedText = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "WebDropDown":
                            wdd = (WebDropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (wdd != null)   //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    wdd.Text = "";
                                else
                                    wdd.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "DropDownList":
                            ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (ddl != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    ddl.SelectedIndex = -1;
                                else
                                {
                                    if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Value")
                                        ddl.SelectedIndex = FieldToValue.FieldToDropDownListByValue(dr[dt.Rows[i]["NAME"].ToString()], ddl);
                                    else
                                        ddl.SelectedIndex = FieldToValue.FieldToDropDownListByText(dr[dt.Rows[i]["NAME"].ToString()], ddl);
                                }
                            }
                            break;
                        case "CheckBox":
                            ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (ckb != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    ckb.Checked = false;
                                else
                                    ckb.Checked = FieldToValue.FieldToCheckBox(dr[dt.Rows[i]["NAME"].ToString()]);
                            }
                            break;
                        case "CheckBoxList":
                            cbl = (CheckBoxList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (cbl != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                {
                                    for (int k = 0; k < cbl.Items.Count; k++)
                                        cbl.Items[k].Selected = false;
                                }
                                else
                                {
                                    string[] text = dr[dt.Rows[i]["NAME"].ToString()].ToString().Split(',');
                                    for (int z = 0; z < cbl.Items.Count; z++)
                                    {
                                        for (int j = 0; j < text.Length; j++)
                                        {
                                            if (cbl.Items[z].Text == text[j])
                                                cbl.Items[z].Selected = true;
                                        }
                                    }
                                }
                            }
                            break;
                        case "RadioButtonList":
                            rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (rbl != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    rbl.SelectedIndex = -1;
                                else
                                {
                                    if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Value")
                                        rbl.SelectedIndex = FieldToValue.FieldToRadioListButtonByValue(dr[dt.Rows[i]["NAME"].ToString()], rbl);
                                    else
                                        rbl.SelectedIndex = FieldToValue.FieldToRadioListButtonByText(dr[dt.Rows[i]["NAME"].ToString()], rbl);
                                }
                            }
                            break;
                        case "HtmlInputText":
                            hit = (HtmlInputText)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (hit != null)   //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    hit.Value="";
                                else
                                    hit.Value = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            dr.Close();
        }

        /// <summary>
        /// ͨ��SQL���������DataRead,ע�⣺ֻ��ȡ��һ������,
        /// ���SQL����ܶ���������¼��ֻȡ��һ����
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sql"></param>
        public static void CustomControlFillByTableIDAndWhere(Page page, int TableID, string wheres)
        {
            string sql;
            int tableID;
            TextBox txt;
            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //����ǩ����,������Ӧ�˵Ŀ�����ܰ������ŵ��ؼ���
            CheckBoxList cbl;

            string TableName = (string)DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + TableID.ToString());
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);

            sql = "select * from " + TableName + " where " + wheres;
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // if(dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull) continue;    //"û��ֵ���򲻴���"  ҲҪ����
                    if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] is System.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() == "") continue;   //ƽ̨�������ô��ж�Ӧ�Ŀؼ�����������
                    switch (dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString())
                    {
                        case "TextBox":
                            txt = (TextBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (txt != null)   //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    txt.Text = "";
                                else
                                    txt.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "WebDateLib":
                            wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (wdl != null)  //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    wdl.Text = "1900-1-1";
                                else
                                    wdl.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "WebComboBox":
                            wbx = (WebComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (wbx != null)   //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    wbx.Text = "";
                                else
                                    wbx.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "HtmlComboBox":
                            hbx = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (hbx != null)   //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    hbx.SelectedText = "";
                                else
                                    hbx.SelectedText = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "WebDropDown":
                            wdd = (WebDropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (wdd != null)   //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    wdd.Text = "";
                                else
                                    wdd.Text = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        case "DropDownList":
                            ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (ddl != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    ddl.SelectedIndex = -1;
                                else
                                {
                                    if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Value")
                                        ddl.SelectedIndex = FieldToValue.FieldToDropDownListByValue(dr[dt.Rows[i]["NAME"].ToString()], ddl);
                                    else
                                        ddl.SelectedIndex = FieldToValue.FieldToDropDownListByText(dr[dt.Rows[i]["NAME"].ToString()], ddl);
                                }
                            }
                            break;
                        case "CheckBox":
                            ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (ckb != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    ckb.Checked = false;
                                else
                                    ckb.Checked = FieldToValue.FieldToCheckBox(dr[dt.Rows[i]["NAME"].ToString()]);
                            }
                            break;
                        case "CheckBoxList":
                            cbl = (CheckBoxList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (cbl != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                {
                                    for (int k = 0; k < cbl.Items.Count; k++)
                                        cbl.Items[k].Selected = false;
                                }
                                else
                                {
                                    string[] text = dr[dt.Rows[i]["NAME"].ToString()].ToString().Split(',');
                                    for (int z = 0; z < cbl.Items.Count; z++)
                                    {
                                        for (int j = 0; j < text.Length; j++)
                                        {
                                            if (cbl.Items[z].Text == text[j])
                                                cbl.Items[z].Selected = true;
                                        }
                                    }
                                }
                            }
                            break;
                        case "RadioButtonList":
                            rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (rbl != null)
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    rbl.SelectedIndex = -1;
                                else
                                {
                                    if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Value")
                                        rbl.SelectedIndex = FieldToValue.FieldToRadioListButtonByValue(dr[dt.Rows[i]["NAME"].ToString()], rbl);
                                    else
                                        rbl.SelectedIndex = FieldToValue.FieldToRadioListButtonByText(dr[dt.Rows[i]["NAME"].ToString()], rbl);
                                }
                            }
                            break;
                        case "HtmlInputText":
                            hit = (HtmlInputText)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                            if (hit != null)   //���õĿؼ�����ʵ�ʲ�һ��
                            {
                                if (dr[dt.Rows[i]["NAME"].ToString()] is System.DBNull)
                                    hit.Value = "";
                                else
                                    hit.Value = dr[dt.Rows[i]["NAME"].ToString()].ToString();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            dr.Close();
        }
    }
}

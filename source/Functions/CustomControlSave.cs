using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using PlatForm.DBUtility;
using PlatForm.CustomControlLib;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace PlatForm.Functions
{

    /// <summary>
    /// ����ϸҳ�汣��ʱ��ͨ�����úõĲ��������ɣӣѣ���䣬�ӿ����д�Ĺ���
    /// </summary>
    public class CustomControlSave
    {
        /// <summary>
        /// ͨ����ɣĺ������ɣӣѣ����
        /// ��������ҳ��������Ϣ
        /// </summary>
        /// <param name="TableID"></param>
        /// <returns></returns>
        public static int CustomControlSaveByTableID(Page page, int TableID)
        {
            bool bAdd = true;   //Ϊ��ʱ����Ҫ���ӣ�Ϊ��ʱ����Ҫ�޸�
            string tableName,columnName,sql;
            string contolType;
            tableName = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID="+TableID.ToString()).ToString();
            
            //���ж���������Ҫ����
            sql="select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID="+TableID.ToString()+" order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where=new WherePara[1];

            //�������ж�Ӧ�Ŀؼ������û��ֵ����Ϊ���棬����Ϊ�޸�
            TextBox txt;
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " and ISPRIMARY=1";
            DbDataReader dr= DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                txt = (TextBox)page.FindControl(dr["CUSTOM_CONTROL_NAME"].ToString());
                if (txt == null)
                {
                    dr.Close();
                    return -1;
                }
                columnName = dr["NAME"].ToString();
            }
            else
            {
                dr.Close();
                return -1;
            }
            dr.Close();

            if (txt.Text.Trim() == "")
            {
                bAdd = true;
                txt.Text = DBOpt.dbHelper.GetMaxNum(tableName, columnName).ToString();
            }
            else
            {
                where[0] = new WherePara(columnName, FieldType.Int, txt.Text, "=", "and");
                bAdd = false;
            }
           
            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //����ǩ����,������Ӧ�˵Ŀ�����ܰ������ŵ��ؼ���
            CheckBoxList cbl;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //û�ж���ؼ������򲻴��������
                if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] == Convert.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString().Trim() == "") continue;

                FieldPara fp= new FieldPara();
                fp.fieldName = dt.Rows[i]["NAME"].ToString();
                if (dt.Rows[i]["TYPE"].ToString() == "String")
                    fp.fieldType = FieldType.String;
                else if (dt.Rows[i]["TYPE"].ToString() == "Datetime")
                    fp.fieldType = FieldType.Datetime;
                else
                    fp.fieldType = FieldType.Int;
                contolType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                switch (contolType)
                {
                    case "TextBox":
                        txt = (TextBox)page.FindControl(dr["CUSTOM_CONTROL_NAME"].ToString());
                        if (txt == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(txt.Text);
                        break;
                    case "DropDownList":
                        ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ddl == null) return -i;
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.DropDownListToFieldByText(ddl);
                        else
                            fp.fieldValue = ValueToField.DropDownListToFieldByValue(ddl);
                        break;
                    case "CheckBox":
                        ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ckb == null) return -i;
                        fp.fieldValue = ValueToField.CheckBoxToField(ckb.Checked);
                        break;
                    case "CheckBoxList":
                        cbl = (CheckBoxList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (cbl == null) return -i;
                        fp.fieldValue = ValueToField.CheckBoxListToField(cbl);
                        break;
                    case "RadioButtonList":
                        rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (rbl == null) return -i;
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByText(rbl);
                        else
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByValue(rbl);
                        break;
                    case "WebDateLib":
                         wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                         if (wdl == null) return -i;
                         fp.fieldValue = ValueToField.StringToField(wdl.Text);
                        break;
                    case "WebComboBox":
                        wbx = (WebComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wbx == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wbx.Text);
                        break;
                    case "HtmlComboBox":
                        hbx = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hbx == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(hbx.SelectedText);
                        break;
                    case "WebDropDown":
                        wdd = (WebDropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdd == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wdd.Text);
                        break;
                    case "HtmlInputText":
                        hit = (HtmlInputText)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hit == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(hit.Value); 
                        break;
                    default:
                        break;
                }
                fields[i] = fp;
            }

            if (bAdd)
                sql = DBOpt.dbHelper.GetInserSql(tableName, fields);
            else
                sql = DBOpt.dbHelper.GetUpdateSql(tableName, fields, where);

            return DBOpt.dbHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// ͨ����ɣĺ������ɣӣѣ����,ͬʱ���أӣѣ���䣬�Բ��뵽��־��
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableID"></param>
        /// <param name="returnSql">Ҫ���صģӣѣ����</param>
        /// <returns></returns>
        public static int CustomControlSaveByTableID(Page page, int TableID,out string returnSql)
        {
            returnSql = "";
            bool bAdd = true;   //Ϊ��ʱ����Ҫ���ӣ�Ϊ��ʱ����Ҫ�޸�
            string tableName, columnName, sql;
            tableName = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + TableID.ToString()).ToString();

            //���ж���������Ҫ����
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //�������ж�Ӧ�Ŀؼ������û��ֵ����Ϊ���棬����Ϊ�޸�
            TextBox txt;
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " and ISPRIMARY=1";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                txt = (TextBox)page.FindControl(dr["CUSTOM_CONTROL_NAME"].ToString());
                if (txt == null)
                {
                    dr.Close();
                    return -1;
                }
                columnName = dr["NAME"].ToString();
            }
            else
            {
                dr.Close();
                return -1;
            }
            dr.Close();

            if (txt.Text.Trim() == "")
            {
                bAdd = true;
                txt.Text = DBOpt.dbHelper.GetMaxNum(tableName, columnName).ToString();
            }
            else
            {
                where[0] = new WherePara(columnName, FieldType.Int, txt.Text, "=", "and");
                bAdd = false;
            }

            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //����ǩ����,������Ӧ�˵Ŀ�����ܰ������ŵ��ؼ���
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //û�ж���ؼ������򲻴��������
                if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] == Convert.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString().Trim() == "") continue;

                FieldPara fp = new FieldPara();
                fp.fieldName = dt.Rows[i]["NAME"].ToString();
                if (dt.Rows[i]["TYPE"].ToString() == "String")
                    fp.fieldType = FieldType.String;
                else if (dt.Rows[i]["TYPE"].ToString() == "Datetime")
                    fp.fieldType = FieldType.Datetime;
                else
                    fp.fieldType = FieldType.Int;

                controlType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                switch (controlType)
                {
                    case "TextBox":
                        txt = (TextBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (txt == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(txt.Text);
                        break;
                    case "DropDownList":
                        ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ddl == null) return -i;
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.DropDownListToFieldByText(ddl);
                        else
                            fp.fieldValue = ValueToField.DropDownListToFieldByValue(ddl);
                        break;
                    case "CheckBox":
                        ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ckb == null) return -i;
                        fp.fieldValue = ValueToField.CheckBoxToField(ckb.Checked);
                        break;
                    case "CheckBoxList":
                        cbl = (CheckBoxList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (cbl == null) return -i;
                        fp.fieldValue = ValueToField.CheckBoxListToField(cbl);
                        break;
                    case "RadioButtonList":
                        rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (rbl == null) return -i;
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByText(rbl);
                        else
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByValue(rbl);
                        break;
                    case "WebDateLib":
                        wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdl == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wdl.Text);
                        break;
                    case "WebComboBox":
                        wbx = (WebComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wbx == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wbx.Text);
                        break;
                    case "HtmlComboBox":
                        hbx = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hbx == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(hbx.SelectedText);
                        break;
                    case "WebDropDown":
                        wdd = (WebDropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdd == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wdd.Text);
                        break;
                    case "HtmlInputText":
                        hit = (HtmlInputText)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hit == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(hit.Value);
                        break;
                    default:
                        break;
                }
                fields[i] = fp;
            }
            if (bAdd)
            {
                sql = DBOpt.dbHelper.GetInserSql(tableName, fields);
            }
            else
            {
                sql = DBOpt.dbHelper.GetUpdateSql(tableName, fields, where);
            }
            returnSql = sql;
            return DBOpt.dbHelper.ExecuteSql(sql);
        }


        /// <summary>
        /// ͨ�����������ɣӣѣ����
        /// ��������ҳ��������Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableName"></param>
        /// <returns>�ɹ�����Ӱ��������������򷵻�����ŵĸ���</returns>
        public static int CustomControlSaveByTableName(Page page,string TableName)
        {
            bool bAdd = true;   //Ϊ��ʱ����Ҫ���ӣ�Ϊ��ʱ����Ҫ�޸�
            string sql,columnName;
            int tableID;
            tableID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + TableName + "'"));

            //���ж���������Ҫ����
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara [dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //�������ж�Ӧ�Ŀؼ������û��ֵ����Ϊ���棬����Ϊ�޸�
            TextBox txt;
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " and ISPRIMARY=1";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                txt = (TextBox)page.FindControl(dr["CUSTOM_CONTROL_NAME"].ToString());
                if (txt == null)
                {
                    dr.Close();
                    return -1;
                }
                columnName = dr["NAME"].ToString();
            }
            else
            {
                dr.Close();
                return -1;
            }
            dr.Close();
            if (txt.Text.Trim() == "")
            {
                bAdd = true;
                txt.Text = DBOpt.dbHelper.GetMaxNum(TableName, columnName).ToString();
            }
            else
            {
                where[0] = new WherePara(columnName, FieldType.Int, txt.Text, "=", "and");
                bAdd = false;
            }
            
            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //����ǩ����,������Ӧ�˵Ŀ�����ܰ������ŵ��ؼ���
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //û�ж���ؼ������򲻴��������
                if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] == Convert.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString().Trim() == "") continue;

                FieldPara fp = new FieldPara();
                fp.fieldName = dt.Rows[i]["NAME"].ToString();
                if (dt.Rows[i]["TYPE"].ToString() == "String")
                    fp.fieldType = FieldType.String;
                else if (dt.Rows[i]["TYPE"].ToString() == "Datetime")
                    fp.fieldType = FieldType.Datetime;
                else
                    fp.fieldType = FieldType.Int;

                controlType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                switch (controlType)
                {
                    case "TextBox":
                        txt = (TextBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (txt == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(txt.Text);
                        break;
                    case "DropDownList":
                        ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ddl == null) return -i;
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.DropDownListToFieldByText(ddl);
                        else
                            fp.fieldValue = ValueToField.DropDownListToFieldByValue(ddl);
                        break;
                    case "CheckBox":
                        ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ckb == null) return -i;
                        fp.fieldValue = ValueToField.CheckBoxToField(ckb.Checked);
                        break;
                    case "CheckBoxList":
                        cbl = (CheckBoxList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (cbl == null) return -i;
                        fp.fieldValue = ValueToField.CheckBoxListToField(cbl);
                        break;
                    case "RadioButtonList":
                        rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (rbl == null) return -i;
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByText(rbl);
                        else
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByValue(rbl);
                        break;
                    case "WebDateLib":
                        wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdl == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wdl.Text);
                        break;
                    case "WebComboBox":
                        wbx = (WebComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wbx == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wbx.Text);
                        break;
                    case "HtmlComboBox":
                        hbx = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hbx == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(hbx.SelectedText);
                        break;
                    case "WebDropDown":
                        wdd = (WebDropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdd == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wdd.Text);
                        break;
                    case "HtmlInputText":
                        hit = (HtmlInputText)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hit == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(hit.Value);
                        break;
                    default:
                        break;
                }
                fields[i] = fp;
            }
            if (bAdd)
            {
                sql = DBOpt.dbHelper.GetInserSql(TableName, fields);
            }
            else
            {
                sql = DBOpt.dbHelper.GetUpdateSql(TableName, fields, where);
            }
            return DBOpt.dbHelper.ExecuteSql(sql);
        }


        /// <summary>
        /// ͨ�����������ɣӣѣ����,ͬʱ���أӣѣ����
        /// ��������ҳ��������Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableName"></param>
        /// <param name="returnSql">Ҫ���صģӣѣ����</param>
        /// <returns></returns>
        public static int CustomControlSaveByTableName(Page page, string TableName,out string returnSql)
        {
            returnSql = "";
            bool bAdd = true;   //Ϊ��ʱ����Ҫ���ӣ�Ϊ��ʱ����Ҫ�޸�
            string sql, columnName;
            int tableID;
            tableID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + TableName + "'"));

            //���ж���������Ҫ����
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //�������ж�Ӧ�Ŀؼ������û��ֵ����Ϊ���棬����Ϊ�޸�
            TextBox txt;
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " and ISPRIMARY=1";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                txt = (TextBox)page.FindControl(dr["CUSTOM_CONTROL_NAME"].ToString());
                if (txt == null)
                {
                    dr.Close();
                    return -1;
                }
                columnName = dr["NAME"].ToString();
            }
            else
            {
                dr.Close();
                return -1;
            }
            dr.Close();
            if (txt.Text.Trim() == "")
            {
                bAdd = true;
                txt.Text = DBOpt.dbHelper.GetMaxNum(TableName, columnName).ToString();
            }
            else
            {
                where[0] = new WherePara(columnName, FieldType.Int, txt.Text, "=", "and");
                bAdd = false;
            }


            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //����ǩ����,������Ӧ�˵Ŀ�����ܰ������ŵ��ؼ���
            CheckBoxList cbl;

            string controlType;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //û�ж���ؼ������򲻴��������
                if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] == Convert.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString().Trim() == "") continue;

                FieldPara fp = new FieldPara();
                fp.fieldName = dt.Rows[i]["NAME"].ToString();
                if (dt.Rows[i]["TYPE"].ToString() == "String")
                    fp.fieldType = FieldType.String;
                else if (dt.Rows[i]["TYPE"].ToString() == "Datetime")
                    fp.fieldType = FieldType.Datetime;
                else
                    fp.fieldType = FieldType.Int;

                controlType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                switch (controlType)
                {
                    case "TextBox":
                        txt = (TextBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (txt == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(txt.Text);
                        break;
                    case "DropDownList":
                        ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ddl == null) return -i;
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.DropDownListToFieldByText(ddl);
                        else
                            fp.fieldValue = ValueToField.DropDownListToFieldByValue(ddl);
                        break;
                    case "CheckBox":
                        ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ckb == null) return -i;
                        fp.fieldValue = ValueToField.CheckBoxToField(ckb.Checked);
                        break;
                    case "CheckBoxList":
                        cbl = (CheckBoxList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (cbl == null) return -i;
                        fp.fieldValue = ValueToField.CheckBoxListToField(cbl);
                        break;
                    case "RadioButtonList":
                        rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (rbl == null) return -i;
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByText(rbl);
                        else
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByValue(rbl);
                        break;
                    case "WebDateLib":
                        wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdl == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wdl.Text);
                        break;
                    case "WebComboBox":
                        wbx = (WebComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wbx == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wbx.Text);
                        break;
                    case "HtmlComboBox":
                        hbx = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hbx == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(hbx.SelectedText);
                        break;
                    case "WebDropDown":
                        wdd = (WebDropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdd == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(wdd.Text);
                        break;
                    case "HtmlInputText":
                        hit = (HtmlInputText)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hit == null) return -i;
                        fp.fieldValue = ValueToField.StringToField(hit.Value);
                        break;
                    default:
                        break;
                }
                fields[i] = fp;
            }

            if (bAdd)
                sql = DBOpt.dbHelper.GetInserSql(TableName, fields);
            else
                sql = DBOpt.dbHelper.GetUpdateSql(TableName, fields, where);

            returnSql = sql;
            return DBOpt.dbHelper.ExecuteSql(sql);
        }


        /// <summary>
        /// ͨ����ɣĺ������ɣӣѣ����,ͬʱ���أӣѣ���䣬�Բ��뵽��־��
        /// ���س�����У����������Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableID"></param>
        /// <param name="returnSql">Ҫ���صģӣѣ����</param>
        /// <returns>�ɹ�ʱ����Ϊ�յ��ַ���������ʱ���س�����ַ�������������в�����������</returns>
        public static string CustomControlSaveByTableIDReturnS(Page page, int TableID, out string returnSql)
        {
            returnSql = "";
            bool bAdd = true;   //Ϊ��ʱ����Ҫ���ӣ�Ϊ��ʱ����Ҫ�޸�
            string tableName, columnName, sql;
            tableName = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + TableID.ToString()).ToString();

            //���ж���������Ҫ����
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //�������ж�Ӧ�Ŀؼ������û��ֵ����Ϊ���棬����Ϊ�޸�
            TextBox txt;
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " and ISPRIMARY=1";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                txt = (TextBox)page.FindControl(dr["CUSTOM_CONTROL_NAME"].ToString());
                if (txt == null)
                {
                    dr.Close();
                    return dr["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "PrimaryColumnInexistence").ToString();  //"�����в����ڣ�"
                }
                columnName = dr["NAME"].ToString();
            }
            else
            {
                dr.Close();
                return HttpContext.GetGlobalResourceObject("WebGlobalResource", "PrimaryColumnInexistence").ToString();
            }
            dr.Close();

            if (txt.Text.Trim() == "")
            {
                bAdd = true;
                txt.Text = DBOpt.dbHelper.GetMaxNum(tableName, columnName).ToString();
            }
            else
            {
                where[0] = new WherePara(columnName, FieldType.Int, txt.Text, "=", "and");
                bAdd = false;
            }

            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //����ǩ����,������Ӧ�˵Ŀ�����ܰ������ŵ��ؼ���
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //û�ж���ؼ������򲻴��������
                if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] == Convert.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString().Trim() == "") continue;

                FieldPara fp = new FieldPara();
                fp.fieldName = dt.Rows[i]["NAME"].ToString();
                if (dt.Rows[i]["TYPE"].ToString() == "String")
                    fp.fieldType = FieldType.String;
                else if (dt.Rows[i]["TYPE"].ToString() == "Datetime")
                    fp.fieldType = FieldType.Datetime;
                else
                    fp.fieldType = FieldType.Int;

                controlType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                switch (controlType)
                {
                    case "TextBox":
                        txt = (TextBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (txt == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString()+HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();  //�ؼ�������
                        fp.fieldValue = ValueToField.StringToField(txt.Text);
                        break;
                    case "DropDownList":
                        ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ddl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString()+HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.DropDownListToFieldByText(ddl);
                        else
                            fp.fieldValue = ValueToField.DropDownListToFieldByValue(ddl);
                        break;
                    case "CheckBox":
                        ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ckb == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.CheckBoxToField(ckb.Checked);
                        break;
                    case "CheckBoxList":
                        cbl = (CheckBoxList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (cbl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.CheckBoxListToField(cbl);
                        break;
                    case "RadioButtonList":
                        rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (rbl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByText(rbl);
                        else
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByValue(rbl);
                        break;
                    case "WebDateLib":
                        wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wdl.Text);
                        break;
                    case "WebComboBox":
                        wbx = (WebComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wbx == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wbx.Text);
                        break;
                    case "HtmlComboBox":
                        hbx = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hbx == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(hbx.SelectedText);
                        break;
                    case "WebDropDown":
                        wdd = (WebDropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdd == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wdd.Text);
                        break;
                    case "HtmlInputText":
                        hit = (HtmlInputText)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hit == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(hit.Value);
                        break;
                    default:
                        break;
                }
                fields[i] = fp;
            }
            if (bAdd)
            {
                sql = DBOpt.dbHelper.GetInserSql(tableName, fields);
            }
            else
            {
                sql = DBOpt.dbHelper.GetUpdateSql(tableName, fields, where);
            }
            returnSql = sql;
            if (DBOpt.dbHelper.ExecuteSql(sql) > 0)
                return "";
            else
                return HttpContext.GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage").ToString();
        }

        /// <summary>
        /// ͨ����ɣĺ������ɣӣѣ����,ͬʱ���أӣѣ���䣬�Բ��뵽��־��
        /// ���س�����У����������Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableID"></param>
        /// <returns>�ɹ�ʱ����Ϊ�յ��ַ���������ʱ���س�����ַ�������������в�����������</returns>
        public static string CustomControlSaveByTableIDReturnS(Page page, int TableID)
        {
            bool bAdd = true;   //Ϊ��ʱ����Ҫ���ӣ�Ϊ��ʱ����Ҫ�޸�
            string tableName, columnName, sql;
            tableName = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + TableID.ToString()).ToString();

            //���ж���������Ҫ����
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //�������ж�Ӧ�Ŀؼ������û��ֵ����Ϊ���棬����Ϊ�޸�
            TextBox txt;
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " and ISPRIMARY=1";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                txt = (TextBox)page.FindControl(dr["CUSTOM_CONTROL_NAME"].ToString());
                if (txt == null)
                {
                    dr.Close();
                    return dr["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "PrimaryColumnInexistence").ToString();
                }
                columnName = dr["NAME"].ToString();
            }
            else
            {
                dr.Close();
                return HttpContext.GetGlobalResourceObject("WebGlobalResource", "PrimaryColumnInexistence").ToString();
            }
            dr.Close();

            if (txt.Text.Trim() == "")
            {
                bAdd = true;
                txt.Text = DBOpt.dbHelper.GetMaxNum(tableName, columnName).ToString();
            }
            else
            {
                where[0] = new WherePara(columnName, FieldType.Int, txt.Text, "=", "and");
                bAdd = false;
            }

            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //����ǩ����,������Ӧ�˵Ŀ�����ܰ������ŵ��ؼ���
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //û�ж���ؼ������򲻴��������
                if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] == Convert.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString().Trim() == "") continue;

                FieldPara fp = new FieldPara();
                fp.fieldName = dt.Rows[i]["NAME"].ToString();
                if (dt.Rows[i]["TYPE"].ToString() == "String")
                    fp.fieldType = FieldType.String;
                else if (dt.Rows[i]["TYPE"].ToString() == "Datetime")
                    fp.fieldType = FieldType.Datetime;
                else
                    fp.fieldType = FieldType.Int;

                controlType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                switch (controlType)
                {
                    case "TextBox":
                        txt = (TextBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (txt == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(txt.Text);
                        break;
                    case "DropDownList":
                        ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ddl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.DropDownListToFieldByText(ddl);
                        else
                            fp.fieldValue = ValueToField.DropDownListToFieldByValue(ddl);
                        break;
                    case "CheckBox":
                        ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ckb == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.CheckBoxToField(ckb.Checked);
                        break;
                    case "CheckBoxList":
                        cbl = (CheckBoxList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (cbl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.CheckBoxListToField(cbl);
                        break;
                    case "RadioButtonList":
                        rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (rbl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByText(rbl);
                        else
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByValue(rbl);
                        break;
                    case "WebDateLib":
                        wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wdl.Text);
                        break;
                    case "WebComboBox":
                        wbx = (WebComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wbx == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wbx.Text);
                        break;
                    case "HtmlComboBox":
                        hbx = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hbx == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(hbx.SelectedText);
                        break;
                    case "WebDropDown":
                        wdd = (WebDropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdd == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wdd.Text);
                        break;
                    case "HtmlInputText":
                        hit = (HtmlInputText)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hit == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(hit.Value);
                        break;
                    default:
                        break;
                }
                fields[i] = fp;
            }
            if (bAdd)
            {
                sql = DBOpt.dbHelper.GetInserSql(tableName, fields);
            }
            else
            {
                sql = DBOpt.dbHelper.GetUpdateSql(tableName, fields, where);
            }
            if (DBOpt.dbHelper.ExecuteSql(sql) > 0)
                return "";
            else
                return HttpContext.GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage").ToString();
        }


         /// <summary>
        /// ͨ�����������ɣӣѣ����
        /// ��������ҳ��������Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableName"></param>
        /// <returns>�ɹ�ʱ����Ϊ�յ��ַ���������ʱ���س�����ַ�������������в�����������</returns>
        public static string CustomControlSaveByTableNameReturnS(Page page,string TableName)
        {
            bool bAdd = true;   //Ϊ��ʱ����Ҫ���ӣ�Ϊ��ʱ����Ҫ�޸�
            string sql, columnName;
            int tableID;
            tableID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + TableName + "'"));

            //���ж���������Ҫ����
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //�������ж�Ӧ�Ŀؼ������û��ֵ����Ϊ���棬����Ϊ�޸�
            TextBox txt;
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " and ISPRIMARY=1";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                txt = (TextBox)page.FindControl(dr["CUSTOM_CONTROL_NAME"].ToString());
                if (txt == null)
                {
                    dr.Close();
                    return dr["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "PrimaryColumnInexistence").ToString();
                }
                columnName = dr["NAME"].ToString();
            }
            else
            {
                dr.Close();
                return HttpContext.GetGlobalResourceObject("WebGlobalResource", "PrimaryColumnInexistence").ToString();
            }
            dr.Close();
            if (txt.Text.Trim() == "")
            {
                bAdd = true;
                txt.Text = DBOpt.dbHelper.GetMaxNum(TableName, columnName).ToString();
            }
            else
            {
                where[0] = new WherePara(columnName, FieldType.Int, txt.Text, "=", "and");
                bAdd = false;
            }

            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //����ǩ����,������Ӧ�˵Ŀ�����ܰ������ŵ��ؼ���
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //û�ж���ؼ������򲻴��������
                if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] == Convert.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString().Trim() == "") continue;

                FieldPara fp = new FieldPara();
                fp.fieldName = dt.Rows[i]["NAME"].ToString();
                if (dt.Rows[i]["TYPE"].ToString() == "String")
                    fp.fieldType = FieldType.String;
                else if (dt.Rows[i]["TYPE"].ToString() == "Datetime")
                    fp.fieldType = FieldType.Datetime;
                else
                    fp.fieldType = FieldType.Int;

                controlType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                switch (controlType)
                {
                    case "TextBox":
                        txt = (TextBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (txt == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(txt.Text);
                        break;
                    case "DropDownList":
                        ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ddl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.DropDownListToFieldByText(ddl);
                        else
                            fp.fieldValue = ValueToField.DropDownListToFieldByValue(ddl);
                        break;
                    case "CheckBox":
                        ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ckb == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.CheckBoxToField(ckb.Checked);
                        break;
                    case "CheckBoxList":
                        cbl = (CheckBoxList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (cbl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.CheckBoxListToField(cbl);
                        break;
                    case "RadioButtonList":
                        rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (rbl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByText(rbl);
                        else
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByValue(rbl);
                        break;
                    case "WebDateLib":
                        wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wdl.Text);
                        break;
                    case "WebComboBox":
                        wbx = (WebComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wbx == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wbx.Text);
                        break;
                    case "HtmlComboBox":
                        hbx = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hbx == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(hbx.SelectedText);
                        break;
                    case "WebDropDown":
                        wdd = (WebDropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdd == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wdd.Text);
                        break;
                    case "HtmlInputText":
                        hit = (HtmlInputText)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hit == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(hit.Value);
                        break;
                    default:
                        break;
                }
                fields[i] = fp;
            }
            if (bAdd)
            {
                sql = DBOpt.dbHelper.GetInserSql(TableName, fields);
            }
            else
            {
                sql = DBOpt.dbHelper.GetUpdateSql(TableName, fields, where);
            }
            if (DBOpt.dbHelper.ExecuteSql(sql) > 0)
                return "";
            else
                return HttpContext.GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage").ToString();  //"�������ݿ�ʧ��!"
        }


        /// <summary>
        /// ͨ�����������ɣӣѣ����
        /// ��������ҳ��������Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableName"></param>
        /// <param name="returnSql"></param>
        /// <returns>�ɹ�ʱ����Ϊ�յ��ַ���������ʱ���س�����ַ�������������в�����������</returns>
        public static string CustomControlSaveByTableNameReturnS(Page page, string TableName, out string returnSql)
        {
            returnSql = "";
            bool bAdd = true;   //Ϊ��ʱ����Ҫ���ӣ�Ϊ��ʱ����Ҫ�޸�
            string sql, columnName;
            int tableID;
            tableID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + TableName + "'"));

            //���ж���������Ҫ����
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //�������ж�Ӧ�Ŀؼ������û��ֵ����Ϊ���棬����Ϊ�޸�
            TextBox txt;
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " and ISPRIMARY=1";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                txt = (TextBox)page.FindControl(dr["CUSTOM_CONTROL_NAME"].ToString());
                if (txt == null)
                {
                    dr.Close();
                    return dr["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "PrimaryColumnInexistence").ToString();
                }
                columnName = dr["NAME"].ToString();
            }
            else
            {
                dr.Close();
                return HttpContext.GetGlobalResourceObject("WebGlobalResource", "PrimaryColumnInexistence").ToString();
            }
            dr.Close();
            if (txt.Text.Trim() == "")
            {
                bAdd = true;
                txt.Text = DBOpt.dbHelper.GetMaxNum(TableName, columnName).ToString();
            }
            else
            {
                where[0] = new WherePara(columnName, FieldType.Int, txt.Text, "=", "and");
                bAdd = false;
            }

            DropDownList ddl;
            CheckBox ckb;
            RadioButtonList rbl;
            WebDate wdl;
            WebComboBox wbx;
            HtmlComboBox hbx;
            WebDropDownList wdd;
            HtmlInputText hit;   //����ǩ����,������Ӧ�˵Ŀ�����ܰ������ŵ��ؼ���
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //û�ж���ؼ������򲻴��������
                if (dt.Rows[i]["CUSTOM_CONTROL_NAME"] == Convert.DBNull || dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString().Trim() == "") continue;

                FieldPara fp = new FieldPara();
                fp.fieldName = dt.Rows[i]["NAME"].ToString();
                if (dt.Rows[i]["TYPE"].ToString() == "String")
                    fp.fieldType = FieldType.String;
                else if (dt.Rows[i]["TYPE"].ToString() == "Datetime")
                    fp.fieldType = FieldType.Datetime;
                else
                    fp.fieldType = FieldType.Int;

                controlType = dt.Rows[i]["CUSTOM_CONTROL_TYPE"].ToString();
                switch (controlType)
                {
                    case "TextBox":
                        txt = (TextBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (txt == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(txt.Text);
                        break;
                    case "DropDownList":
                        ddl = (DropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ddl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.DropDownListToFieldByText(ddl);
                        else
                            fp.fieldValue = ValueToField.DropDownListToFieldByValue(ddl);
                        break;
                    case "CheckBox":
                        ckb = (CheckBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (ckb == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.CheckBoxToField(ckb.Checked);
                        break;
                    case "CheckBoxList":
                        cbl = (CheckBoxList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (cbl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.CheckBoxListToField(cbl);
                        break;
                    case "RadioButtonList":
                        rbl = (RadioButtonList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (rbl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        if (dt.Rows[i]["CUSTOM_CONTROL_SVAE_TYPE"].ToString() == "Text")
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByText(rbl);
                        else
                            fp.fieldValue = ValueToField.RadioButtonListToFieldByValue(rbl);
                        break;
                    case "WebDateLib":
                        wdl = (WebDate)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdl == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wdl.Text);
                        break;
                    case "WebComboBox":
                        wbx = (WebComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wbx == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wbx.Text);
                        break;
                    case "HtmlComboBox":
                        hbx = (HtmlComboBox)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hbx == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(hbx.SelectedText);
                        break;
                    case "WebDropDown":
                        wdd = (WebDropDownList)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (wdd == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(wdd.Text);
                        break;
                    case "HtmlInputText":
                        hit = (HtmlInputText)page.FindControl(dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString());
                        if (hit == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();
                        fp.fieldValue = ValueToField.StringToField(hit.Value);
                        break;
                    default:
                        break;
                }
                fields[i] = fp;
            }
            if (bAdd)
            {
                sql = DBOpt.dbHelper.GetInserSql(TableName, fields);
            }
            else
            {
                sql = DBOpt.dbHelper.GetUpdateSql(TableName, fields, where);
            }
            returnSql = sql;
            if (DBOpt.dbHelper.ExecuteSql(sql) > 0)
                return "";
            else
                return HttpContext.GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage").ToString();  //"�������ݿ�ʧ��!"
        }

    }


}

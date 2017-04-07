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
    /// 在详细页面保存时，通过配置好的参数来生成ＳＱＬ语句，加快代码写的过程
    /// </summary>
    public class CustomControlSave
    {
        /// <summary>
        /// 通过表ＩＤ号来生成ＳＱＬ语句
        /// 不方便查找出错的列信息
        /// </summary>
        /// <param name="TableID"></param>
        /// <returns></returns>
        public static int CustomControlSaveByTableID(Page page, int TableID)
        {
            bool bAdd = true;   //为真时代表要增加，为假时代表要修改
            string tableName,columnName,sql;
            string contolType;
            tableName = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID="+TableID.ToString()).ToString();
            
            //共有多少列数据要保存
            sql="select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID="+TableID.ToString()+" order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where=new WherePara[1];

            //找主键列对应的控件，如果没有值，则为保存，否则为修改
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
            HtmlInputText hit;   //用于签名列,输入相应人的口令才能把人名放到控件中
            CheckBoxList cbl;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //没有定义控件名，则不处理保存代码
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
        /// 通过表ＩＤ号来生成ＳＱＬ语句,同时返回ＳＱＬ语句，以插入到日志中
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableID"></param>
        /// <param name="returnSql">要返回的ＳＱＬ语句</param>
        /// <returns></returns>
        public static int CustomControlSaveByTableID(Page page, int TableID,out string returnSql)
        {
            returnSql = "";
            bool bAdd = true;   //为真时代表要增加，为假时代表要修改
            string tableName, columnName, sql;
            tableName = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + TableID.ToString()).ToString();

            //共有多少列数据要保存
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //找主键列对应的控件，如果没有值，则为保存，否则为修改
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
            HtmlInputText hit;   //用于签名列,输入相应人的口令才能把人名放到控件中
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //没有定义控件名，则不处理保存代码
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
        /// 通过表名来生成ＳＱＬ语句
        /// 不方便查找出错的列信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableName"></param>
        /// <returns>成功返回影响的行数，出错则返回列序号的负数</returns>
        public static int CustomControlSaveByTableName(Page page,string TableName)
        {
            bool bAdd = true;   //为真时代表要增加，为假时代表要修改
            string sql,columnName;
            int tableID;
            tableID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + TableName + "'"));

            //共有多少列数据要保存
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara [dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //找主键列对应的控件，如果没有值，则为保存，否则为修改
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
            HtmlInputText hit;   //用于签名列,输入相应人的口令才能把人名放到控件中
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //没有定义控件名，则不处理保存代码
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
        /// 通过表名来生成ＳＱＬ语句,同时返回ＳＱＬ语句
        /// 不方便查找出错的列信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableName"></param>
        /// <param name="returnSql">要返回的ＳＱＬ语句</param>
        /// <returns></returns>
        public static int CustomControlSaveByTableName(Page page, string TableName,out string returnSql)
        {
            returnSql = "";
            bool bAdd = true;   //为真时代表要增加，为假时代表要修改
            string sql, columnName;
            int tableID;
            tableID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + TableName + "'"));

            //共有多少列数据要保存
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //找主键列对应的控件，如果没有值，则为保存，否则为修改
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
            HtmlInputText hit;   //用于签名列,输入相应人的口令才能把人名放到控件中
            CheckBoxList cbl;

            string controlType;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //没有定义控件名，则不处理保存代码
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
        /// 通过表ＩＤ号来生成ＳＱＬ语句,同时返回ＳＱＬ语句，以插入到日志中
        /// 返回出错的列，方便查找信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableID"></param>
        /// <param name="returnSql">要返回的ＳＱＬ语句</param>
        /// <returns>成功时返回为空的字符串，错误时返回出错的字符串，方便查找列参数出错在哪</returns>
        public static string CustomControlSaveByTableIDReturnS(Page page, int TableID, out string returnSql)
        {
            returnSql = "";
            bool bAdd = true;   //为真时代表要增加，为假时代表要修改
            string tableName, columnName, sql;
            tableName = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + TableID.ToString()).ToString();

            //共有多少列数据要保存
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //找主键列对应的控件，如果没有值，则为保存，否则为修改
            TextBox txt;
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " and ISPRIMARY=1";
            DbDataReader dr = DBOpt.dbHelper.GetDataReader(sql);
            if (dr.Read())
            {
                txt = (TextBox)page.FindControl(dr["CUSTOM_CONTROL_NAME"].ToString());
                if (txt == null)
                {
                    dr.Close();
                    return dr["CUSTOM_CONTROL_NAME"].ToString() + HttpContext.GetGlobalResourceObject("WebGlobalResource", "PrimaryColumnInexistence").ToString();  //"主键列不存在！"
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
            HtmlInputText hit;   //用于签名列,输入相应人的口令才能把人名放到控件中
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //没有定义控件名，则不处理保存代码
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
                        if (txt == null) return dt.Rows[i]["CUSTOM_CONTROL_NAME"].ToString()+HttpContext.GetGlobalResourceObject("WebGlobalResource", "ControlInexistence").ToString();  //控件不存在
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
        /// 通过表ＩＤ号来生成ＳＱＬ语句,同时返回ＳＱＬ语句，以插入到日志中
        /// 返回出错的列，方便查找信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableID"></param>
        /// <returns>成功时返回为空的字符串，错误时返回出错的字符串，方便查找列参数出错在哪</returns>
        public static string CustomControlSaveByTableIDReturnS(Page page, int TableID)
        {
            bool bAdd = true;   //为真时代表要增加，为假时代表要修改
            string tableName, columnName, sql;
            tableName = DBOpt.dbHelper.ExecuteScalar("select NAME from DMIS_SYS_TABLES where ID=" + TableID.ToString()).ToString();

            //共有多少列数据要保存
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + TableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //找主键列对应的控件，如果没有值，则为保存，否则为修改
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
            HtmlInputText hit;   //用于签名列,输入相应人的口令才能把人名放到控件中
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //没有定义控件名，则不处理保存代码
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
        /// 通过表名来生成ＳＱＬ语句
        /// 不方便查找出错的列信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableName"></param>
        /// <returns>成功时返回为空的字符串，错误时返回出错的字符串，方便查找列参数出错在哪</returns>
        public static string CustomControlSaveByTableNameReturnS(Page page,string TableName)
        {
            bool bAdd = true;   //为真时代表要增加，为假时代表要修改
            string sql, columnName;
            int tableID;
            tableID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + TableName + "'"));

            //共有多少列数据要保存
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //找主键列对应的控件，如果没有值，则为保存，否则为修改
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
            HtmlInputText hit;   //用于签名列,输入相应人的口令才能把人名放到控件中
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //没有定义控件名，则不处理保存代码
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
                return HttpContext.GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage").ToString();  //"更新数据库失败!"
        }


        /// <summary>
        /// 通过表名来生成ＳＱＬ语句
        /// 不方便查找出错的列信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="TableName"></param>
        /// <param name="returnSql"></param>
        /// <returns>成功时返回为空的字符串，错误时返回出错的字符串，方便查找列参数出错在哪</returns>
        public static string CustomControlSaveByTableNameReturnS(Page page, string TableName, out string returnSql)
        {
            returnSql = "";
            bool bAdd = true;   //为真时代表要增加，为假时代表要修改
            string sql, columnName;
            int tableID;
            tableID = Convert.ToInt16(DBOpt.dbHelper.ExecuteScalar("select ID from DMIS_SYS_TABLES where NAME='" + TableName + "'"));

            //共有多少列数据要保存
            sql = "select NAME,DESCR,TYPE,CUSTOM_CONTROL_NAME,CUSTOM_CONTROL_TYPE,CUSTOM_CONTROL_SVAE_TYPE from DMIS_SYS_COLUMNS where TABLE_ID=" + tableID.ToString() + " order by ORDER_ID";
            DataTable dt = DBOpt.dbHelper.GetDataTable(sql);
            FieldPara[] fields = new FieldPara[dt.Rows.Count];
            WherePara[] where = new WherePara[1];

            //找主键列对应的控件，如果没有值，则为保存，否则为修改
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
            HtmlInputText hit;   //用于签名列,输入相应人的口令才能把人名放到控件中
            CheckBoxList cbl;

            string controlType;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //没有定义控件名，则不处理保存代码
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
                return HttpContext.GetGlobalResourceObject("WebGlobalResource", "SaveFailMessage").ToString();  //"更新数据库失败!"
        }

    }


}

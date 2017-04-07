

namespace PlatForm.DBUtility
{
    public struct FieldPara
    {
        public string fieldName;
        public FieldType fieldType;
        public string fieldValue;
        public FieldPara(string fieldname, FieldType fieldtype, string fieldvalue)
        {
            fieldName = fieldname;
            fieldType = fieldtype;
            fieldValue = fieldvalue;
        }
    }

    //条件参数
    public struct WherePara
    {
        public string fieldName;
        public FieldType fieldType;
        public string fieldValue;
        public string optType;
        public string relationType;  //取值为and和or
        public WherePara(string fieldname, FieldType fieldtype, string fieldvalue, string opttype, string relationtype)
        {
            fieldName = fieldname;
            fieldType = fieldtype;
            fieldValue = fieldvalue;
            optType = opttype;
            relationType = relationtype;
        }
    }

    //列类型枚举
    public enum FieldType
    {
        String = 0,
        Int = 1,
        Datetime = 2
    }

}
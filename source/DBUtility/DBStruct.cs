

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

    //��������
    public struct WherePara
    {
        public string fieldName;
        public FieldType fieldType;
        public string fieldValue;
        public string optType;
        public string relationType;  //ȡֵΪand��or
        public WherePara(string fieldname, FieldType fieldtype, string fieldvalue, string opttype, string relationtype)
        {
            fieldName = fieldname;
            fieldType = fieldtype;
            fieldValue = fieldvalue;
            optType = opttype;
            relationType = relationtype;
        }
    }

    //������ö��
    public enum FieldType
    {
        String = 0,
        Int = 1,
        Datetime = 2
    }

}
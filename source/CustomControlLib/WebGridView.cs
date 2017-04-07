using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PlatForm.CustomControlLib
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:WebGridView runat=server></{0}:WebGridView>")]
    [Themeable(true)]
    public class WebGridView : GridView
    {
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                // ��ÿһ��ָ���̶����еĵ�Ԫ�����css����
                if (!String.IsNullOrEmpty(FixColumnIndices))
                {
                    // ������
                    foreach (string s in FixColumnIndices.Split(','))
                    {
                        int i;
                        if (!Int32.TryParse(s, out i))
                            throw new ArgumentException("FixColumnIndices", "���з����ε��ַ�");
                        if (i > e.Row.Cells.Count)
                            throw new ArgumentOutOfRangeException("FixColumnIndices", "���");

                        e.Row.Cells[i].Attributes.Add("style", "position: relative; left: expression(this.offsetParent.scrollLeft);");
                    }
                }

                bool isFixRow = false; // ��ǰ���Ƿ�̶�
                if (IsFixHeader && e.Row.RowType == DataControlRowType.Header)
                {
                    isFixRow = true;
                }

                if (!String.IsNullOrEmpty(FixRowIndices) && e.Row.RowType == DataControlRowType.DataRow)
                {
                    // ������
                    foreach (string s in FixRowIndices.Split(','))
                    {
                        int i;
                        if (!Int32.TryParse(s, out i))
                            throw new ArgumentException("FixRowIndices", "���з����ε��ַ�");
                        if (i > e.Row.Cells.Count)
                            throw new ArgumentOutOfRangeException("FixRowIndices", "���");

                        if (i == e.Row.RowIndex)
                        {
                            isFixRow = true;
                            break;
                        }
                    }
                }

                // �̶�����
                if (isFixRow)
                {
                    // ���е�ÿһ����Ԫ��
                    for (int j = 0; j < e.Row.Cells.Count; j++)
                    {
                        // �õ�Ԫ�����ڹ̶���
                        if (String.IsNullOrEmpty(e.Row.Cells[j].Attributes["style"]) || e.Row.Cells[j].Attributes["style"].IndexOf("position: relative;") == -1)
                        {
                            e.Row.Cells[j].Attributes.Add("style", " position: relative; top: expression(this.offsetParent.scrollTop);");
                        }
                        // �õ�Ԫ�����ڹ̶���
                        else
                        {
                            e.Row.Cells[j].Attributes.Add("style", e.Row.Cells[j].Attributes["style"] + "top: expression(this.offsetParent.scrollTop); z-index: 666;");
                        }
                    }
                }
            }

            base.OnRowDataBound(e);
        }
        protected override void Render(HtmlTextWriter writer)
        {
            // ��GridViewһ������ <div>
            if (!TableWidth.IsEmpty || !TableHeight.IsEmpty)
            {
                if (TableWidth.IsEmpty) TableWidth = new Unit(100, UnitType.Percentage);
                if (TableHeight.IsEmpty) TableHeight = new Unit(100, UnitType.Percentage);

                writer.Write("<div id='yy_ScrollDiv' style=\"overflow: auto; width: "
                    + TableWidth.ToString() + "; height: "
                    + TableHeight.ToString() + "; position: relative;\" ");
                writer.Write(">");
            }

            base.Render(writer);

            // </div> ����
            if (!TableWidth.IsEmpty || !TableHeight.IsEmpty)
            {
                writer.Write("</div>");
            }
        }

        //------------------------------------------------------------------------------
        /**/
        /// <summary>
        /// �̶���ͷ��ָ���л�ָ���е�ʵ����
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        private bool _isFixHeader;
        /**/
        /// <summary>
        /// �̶���ͷ��
        /// </summary>
        [Description("�̶���ͷ��"), Category("��չ"), DefaultValue(false), NotifyParentProperty(true)]
        public virtual bool IsFixHeader
        {
            get { return _isFixHeader; }
            set { _isFixHeader = value; }
        }

        private string _fixRowIndices;
        /**/
        /// <summary>
        /// ��Ҫ�̶����е��������ö��š�,���ָ���
        /// </summary>
        [Description("��Ҫ�̶����е��������ö��š�,���ָ���"), Category("��չ"), NotifyParentProperty(true)]
        public virtual string FixRowIndices
        {
            get { return _fixRowIndices; }
            set { _fixRowIndices = value; }
        }

        private string _fixColumnIndices;
        /**/
        /// <summary>
        /// ��Ҫ�̶����е��������ö��š�,���ָ���
        /// </summary>
        [Description("��Ҫ�̶����е��������ö��š�,���ָ���"), Category("��չ"), NotifyParentProperty(true)]
        public virtual string FixColumnIndices
        {
            get { return _fixColumnIndices; }
            set { _fixColumnIndices = value; }
        }

        private System.Web.UI.WebControls.Unit _tableWidth;
        /**/
        /// <summary>
        /// ���Ŀ��
        /// </summary>
        [Description("���Ŀ��"), Category("��չ"), NotifyParentProperty(true)]
        public System.Web.UI.WebControls.Unit TableWidth
        {
            get { return _tableWidth; }
            set { _tableWidth = value; }
        }

        private System.Web.UI.WebControls.Unit _tableHeight;
        /**/
        /// <summary>
        /// ���ĸ߶�
        /// </summary>
        [Description("���ĸ߶�"), Category("��չ"), NotifyParentProperty(true)]
        public System.Web.UI.WebControls.Unit TableHeight
        {
            get { return _tableHeight; }
            set { _tableHeight = value; }
        }
    }
}


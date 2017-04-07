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
                // 给每一个指定固定的列的单元格加上css属性
                if (!String.IsNullOrEmpty(FixColumnIndices))
                {
                    // 列索引
                    foreach (string s in FixColumnIndices.Split(','))
                    {
                        int i;
                        if (!Int32.TryParse(s, out i))
                            throw new ArgumentException("FixColumnIndices", "含有非整形的字符");
                        if (i > e.Row.Cells.Count)
                            throw new ArgumentOutOfRangeException("FixColumnIndices", "溢出");

                        e.Row.Cells[i].Attributes.Add("style", "position: relative; left: expression(this.offsetParent.scrollLeft);");
                    }
                }

                bool isFixRow = false; // 当前行是否固定
                if (IsFixHeader && e.Row.RowType == DataControlRowType.Header)
                {
                    isFixRow = true;
                }

                if (!String.IsNullOrEmpty(FixRowIndices) && e.Row.RowType == DataControlRowType.DataRow)
                {
                    // 行索引
                    foreach (string s in FixRowIndices.Split(','))
                    {
                        int i;
                        if (!Int32.TryParse(s, out i))
                            throw new ArgumentException("FixRowIndices", "含有非整形的字符");
                        if (i > e.Row.Cells.Count)
                            throw new ArgumentOutOfRangeException("FixRowIndices", "溢出");

                        if (i == e.Row.RowIndex)
                        {
                            isFixRow = true;
                            break;
                        }
                    }
                }

                // 固定该行
                if (isFixRow)
                {
                    // 该行的每一个单元格
                    for (int j = 0; j < e.Row.Cells.Count; j++)
                    {
                        // 该单元格不属于固定列
                        if (String.IsNullOrEmpty(e.Row.Cells[j].Attributes["style"]) || e.Row.Cells[j].Attributes["style"].IndexOf("position: relative;") == -1)
                        {
                            e.Row.Cells[j].Attributes.Add("style", " position: relative; top: expression(this.offsetParent.scrollTop);");
                        }
                        // 该单元格属于固定列
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
            // 给GridView一个容器 <div>
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

            // </div> 结束
            if (!TableWidth.IsEmpty || !TableHeight.IsEmpty)
            {
                writer.Write("</div>");
            }
        }

        //------------------------------------------------------------------------------
        /**/
        /// <summary>
        /// 固定表头、指定行或指定列的实体类
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        private bool _isFixHeader;
        /**/
        /// <summary>
        /// 固定表头否？
        /// </summary>
        [Description("固定表头否？"), Category("扩展"), DefaultValue(false), NotifyParentProperty(true)]
        public virtual bool IsFixHeader
        {
            get { return _isFixHeader; }
            set { _isFixHeader = value; }
        }

        private string _fixRowIndices;
        /**/
        /// <summary>
        /// 需要固定的行的索引（用逗号“,”分隔）
        /// </summary>
        [Description("需要固定的行的索引（用逗号“,”分隔）"), Category("扩展"), NotifyParentProperty(true)]
        public virtual string FixRowIndices
        {
            get { return _fixRowIndices; }
            set { _fixRowIndices = value; }
        }

        private string _fixColumnIndices;
        /**/
        /// <summary>
        /// 需要固定的列的索引（用逗号“,”分隔）
        /// </summary>
        [Description("需要固定的列的索引（用逗号“,”分隔）"), Category("扩展"), NotifyParentProperty(true)]
        public virtual string FixColumnIndices
        {
            get { return _fixColumnIndices; }
            set { _fixColumnIndices = value; }
        }

        private System.Web.UI.WebControls.Unit _tableWidth;
        /**/
        /// <summary>
        /// 表格的宽度
        /// </summary>
        [Description("表格的宽度"), Category("扩展"), NotifyParentProperty(true)]
        public System.Web.UI.WebControls.Unit TableWidth
        {
            get { return _tableWidth; }
            set { _tableWidth = value; }
        }

        private System.Web.UI.WebControls.Unit _tableHeight;
        /**/
        /// <summary>
        /// 表格的高度
        /// </summary>
        [Description("表格的高度"), Category("扩展"), NotifyParentProperty(true)]
        public System.Web.UI.WebControls.Unit TableHeight
        {
            get { return _tableHeight; }
            set { _tableHeight = value; }
        }
    }
}


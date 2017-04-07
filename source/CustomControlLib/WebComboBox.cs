using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace PlatForm.CustomControlLib
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:WebComboBox runat=\"server\"></{0}:WebComboBox>")]
    public class WebComboBox : WebControl, INamingContainer
    {
        private TextBox myTextBox = new TextBox();
        public ListBox myListBox = new ListBox();
        private Button myButton = new Button();
        int maxLength = 0;

        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls(); //确认子控件集都已被创建 
                return base.Controls;
            }
        }
        // 实现属性ButtonText
        [Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("按钮上的文字内容.")]
        public string ButtonText
        {
            get
            {
                EnsureChildControls();
                return myButton.Text;
            }
            set
            {
                EnsureChildControls();
                myButton.Text = value;
            }
        }
        [Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("文字内容.")]
        public string Text
        {
            get
            {
                EnsureChildControls();
                return myTextBox.Text;
            }
            set
            {
                EnsureChildControls();
                myTextBox.Text = value;
            }
        }
        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            //myTextBox = new TextBox();
            myTextBox.ID = "txt";
            myTextBox.BackColor = this.BackColor;
            myTextBox.BorderColor = this.BorderColor;
            myTextBox.BorderStyle = this.BorderStyle;
            myTextBox.BorderWidth = this.BorderWidth;
            myTextBox.CssClass = this.CssClass;
            myTextBox.Enabled = this.Enabled;
            myTextBox.Font.Bold = this.Font.Bold;
            myTextBox.Font.Italic = this.Font.Italic;
            myTextBox.Font.Name = this.Font.Name;
            myTextBox.Font.Names = this.Font.Names;
            myTextBox.Font.Overline = this.Font.Overline;
            myTextBox.Font.Size = this.Font.Size;
            myTextBox.Font.Strikeout = this.Font.Strikeout;
            myTextBox.Font.Underline = this.Font.Underline;
            myTextBox.ForeColor = this.ForeColor;
            myTextBox.TabIndex = this.TabIndex;

            //myButton = new Button();
            myButton.ID = "but";
            myButton.Text = "..";
            myButton.Font.Bold = true;
            myButton.Click += new EventHandler(myButton_Click);

            //myListBox = new ListBox();
            myListBox.ID = "lsb";
            myListBox.Rows = 9;
            myListBox.SelectionMode = ListSelectionMode.Single;
            myListBox.BackColor = this.BackColor;
            myListBox.BorderColor = this.BorderColor;
            myListBox.BorderStyle = this.BorderStyle;
            myListBox.BorderWidth = this.BorderWidth;
            myListBox.CssClass = this.CssClass;
            myListBox.Enabled = this.Enabled;
            myListBox.Font.Bold = this.Font.Bold;
            myListBox.Font.Italic = this.Font.Italic;
            myListBox.Font.Name = this.Font.Name;
            myListBox.Font.Names = this.Font.Names;
            myListBox.Font.Overline = this.Font.Overline;
            myListBox.Font.Size = this.Font.Size;
            myListBox.Font.Strikeout = this.Font.Strikeout;
            myListBox.Font.Underline = this.Font.Underline;
            myListBox.ForeColor = this.ForeColor;
            myListBox.Visible = false;
            myListBox.SelectedIndexChanged += new EventHandler(myListBox_SelectedIndexChanged);
            myListBox.AutoPostBack = true;
            myListBox.Height = 150;
            if (myListBox.SelectedItem != null)
                myTextBox.Text = myListBox.SelectedItem.Text;
            myTextBox.MaxLength = this.maxLength;
            this.TabIndex = -1;
            //this.ID = String.Format("{0}_cmb", myListBox.ID);

            this.Controls.Add(myTextBox);
            this.Controls.Add(myListBox);
            this.Controls.Add(myButton);
        }

        void myListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myListBox.SelectedItem != null)
            {
                myTextBox.Text = myListBox.SelectedItem.ToString();
                myListBox.Visible = false;
            }
        }

        void myButton_Click(object sender, EventArgs e)
        {
            if (myListBox.Visible == false)
                myListBox.Visible = true;
            else
                myListBox.Visible = false;
        }

        [Category("自定义属性")]
        [DefaultValue("100")]
        [Description("日期的宽度")]
        [Themeable(false)]
        public string myComboxWidth
        {
            get
            {
                EnsureChildControls();
                return ViewState["myDateWidth"] == null ? "100" : (string)ViewState["myDateWidth"];
            }
            set
            {
                EnsureChildControls();
                ViewState["myDateWidth"] = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string myListBoxWidth
        {
            get
            {
                EnsureChildControls();
                return ViewState["myDateWidth"] == null ? "130" : (Convert.ToUInt16(ViewState["myDateWidth"]) + 30).ToString();
            }
            set
            {
                //EnsureChildControls();
                //ViewState["myDateWidth"] = value;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            if (this.Visible)
            {
                output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                output.AddStyleAttribute("LEFT", this.Style["LEFT"]);
                output.AddStyleAttribute("TOP", this.Style["TOP"]);
                output.RenderBeginTag(HtmlTextWriterTag.Table); //表 
                output.RenderBeginTag(HtmlTextWriterTag.Tr); //行 
                output.RenderBeginTag(HtmlTextWriterTag.Td); //列 
                if (!Enabled)
                {
                    output.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "true");
                }
                output.AddAttribute(HtmlTextWriterAttribute.Style, "Width:" + myComboxWidth + "px;");
                myTextBox.RenderControl(output);
                output.RenderEndTag();
                output.RenderBeginTag(HtmlTextWriterTag.Td); //列 
                if (!Enabled)
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
                }
                output.AddAttribute(HtmlTextWriterAttribute.Style, "Width:30px");
                myButton.RenderControl(output);
                output.RenderEndTag();
                //if (myListBox.Visible == true)
                //{
                output.RenderEndTag();
                output.RenderBeginTag(HtmlTextWriterTag.Tr); //行 
                output.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                output.RenderBeginTag(HtmlTextWriterTag.Td); //列 
                output.AddAttribute(HtmlTextWriterAttribute.Id, myDivID);
                output.AddAttribute(HtmlTextWriterAttribute.Style, myDivStyle);
                output.RenderBeginTag(HtmlTextWriterTag.Div);
                output.AddAttribute(HtmlTextWriterAttribute.Style, "Width:" + myListBoxWidth + "px;");
                myListBox.RenderControl(output);
                output.RenderEndTag();
                output.AddAttribute(HtmlTextWriterAttribute.Id, myIframeID);
                output.AddAttribute(HtmlTextWriterAttribute.Style, myIframeStyle);
                output.RenderBeginTag(HtmlTextWriterTag.Iframe);
                output.RenderEndTag();
                output.RenderEndTag();
                output.RenderEndTag();
                output.RenderEndTag();
                //}
                //else
                //{
                //    output.RenderEndTag();
                //    output.RenderEndTag();
                //}
            }

        }
        //重载服务器控件的Enabled属性，将选择日期按钮变灰（禁用）
        [Category("自定义属性")]
        [Description("是否可编辑")]
        public override bool Enabled
        {
            get
            {
                EnsureChildControls();
                return ViewState["Enabled"] == null ? true : (bool)ViewState["Enabled"];
            }
            set
            {
                EnsureChildControls();
                ViewState["Enabled"] = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string myDivID //复合控件中按钮的ID 
        {
            get
            {
                EnsureChildControls();
                return this.ClientID + "_myDivID";
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string myIframeID //复合控件中按钮的ID 
        {
            get
            {
                EnsureChildControls();
                return this.ClientID + "_myIframe";
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string myIframeStyle //复合控件中按钮的ID 
        {
            get
            {
                EnsureChildControls();
                string _myStyle = "";
                if (myListBox.Visible == true)
                {
                    _myStyle = "position:absolute; width:" + myListBoxWidth + "px;height:100px;z-index:98";
                }
                else
                {
                    _myStyle = "position:absolute; width:0px;height:0px;";
                }
                return _myStyle;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string myDivStyle //复合控件中按钮的ID 
        {
            get
            {
                EnsureChildControls();
                string _myStyle = "";
                if (myListBox.Visible == true)
                {
                    _myStyle = "position:absolute; width:" + myListBoxWidth + "px;height:100px;z-index:99;";
                }
                else
                {
                    _myStyle = "position:absolute; width:0px;height:0px;";
                }
                return _myStyle;
            }
        }

        [Description("Gets or sets the maximum number of characters allowed to be manually entered."),
        Browsable(true),
        Category("Behavior")]
        public int MaxLength
        {
            get
            {
                return this.maxLength;
            }
            set
            {
                this.maxLength = value;
            }
        }
    }
}


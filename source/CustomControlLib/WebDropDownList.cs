using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using PlatForm.DBUtility;

namespace PlatForm.CustomControlLib
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:WebDropDownList runat=server></{0}:WebDropDownList>")]
    [Themeable(false)]
    public class WebDropDownList : WebControl, INamingContainer, IPostBackEventHandler 
    {
        private const string _BUTTONDEFAULTTEXT = "..";
        private TextBox myTextBox;
        private TreeView myTreeView;
        private Button myButton;
        private static readonly object EventSelectedNodeKey = new object();
        //���ÿؼ�״̬ʵ������
        private static DataTable _dt;

        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls(); //ȷ���ӿؼ������ѱ����� 
                return base.Controls;
            }
        }
//------------------------------------ʵ���¼��Ѿ��ش����ݵȵȣ����ԣ���֪�����Բ�,����ͨ��---------------
        public event EventHandler SelectedNode
        {
            add 
            { 
                Events.AddHandler(EventSelectedNodeKey, value); 
            }
            remove 
            { 
                Events.RemoveHandler(EventSelectedNodeKey, value); 
            }
        }
        protected virtual void OnSelectedNode(EventArgs e) 
        {
            EventHandler SelectedNodeHandler = (EventHandler)Events[EventSelectedNodeKey];
            if (SelectedNodeHandler != null)
            { SelectedNodeHandler(this, e); }
        }
        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            OnSelectedNode(EventArgs.Empty);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue("")]
        [Localizable(true)]
        public string GetValue
        {
            get
            {
                EnsureChildControls();
                //String s = myTreeView.SelectedNode.Value;
                return ((myTreeView.SelectedNode == null) ? String.Empty : myTreeView.SelectedNode.Value);
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                EnsureChildControls();
                String s = myTextBox.Text;
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                EnsureChildControls();
                myTextBox.Text = value;
            }
        }
        //���ط������ؼ���Enabled���ԣ���ѡ�����ڰ�ť��ң����ã� 
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
//---------------------------------------------------------------------------------------
        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            myTextBox = new TextBox();
            //myTextBox.ID = "myTextbox";

            myButton = new Button();
            myButton.ID = "myButton";
            myButton.Width = Unit.Parse("25px");
            myButton.Click += new EventHandler(myButton_Click);

            myTreeView = new TreeView();
            myTreeView.ID = "myTreeview";
            myTreeView.ShowLines = true;
            myTreeView.Font.Bold = true;
            myTreeView.Font.Size = 10;
            //myTreeView.ExpandDepth = 3;
            //if (TableSql == "")
            //{
            //    //TreeNode tmp = new TreeNode();
            //    //tmp.Text = "1";
            //    //tmp.Value = "1";
            //    //myTreeView.Nodes.Add(tmp);
            //}
            //else
            //{
            //    _dt = DBOpt.dbHelper.GetDataTable(TableSql);
            //    myTreeView.Nodes.Clear();
            //    if (FlagNode == 0)
            //        BuildTree();
            //    else
            //        BuildTreeNoTop(null);
            //}
            myTreeView.SelectedNodeChanged += new EventHandler(myTreeView_SelectedNodeChanged);
            myTreeView.Visible = false;

            this.Controls.Add(myTextBox);
            this.Controls.Add(myButton);
            this.Controls.Add(myTreeView);
        }

        void myButton_Click(object sender, EventArgs e)
        {
            if (myTreeView.Visible == false)
            {
                myTreeView.Visible = true;
            }
            else
                myTreeView.Visible = false;
        }

        void myTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            myTextBox.Text = myTreeView.SelectedNode.Text;
            myTreeView.Visible = false;
            OnSelectedNode(EventArgs.Empty);
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            output.RenderBeginTag(HtmlTextWriterTag.Table);
            output.RenderBeginTag(HtmlTextWriterTag.Tr);

            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.AddAttribute(HtmlTextWriterAttribute.Id, txtTextBoxID);// ��������
            output.AddAttribute(HtmlTextWriterAttribute.Style, "TEXT-ALIGN: center");
            if (!Enabled)
                output.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "true");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "Width:" + TextBoxWidth);// ��������
            myTextBox.RenderControl(output);
            output.RenderEndTag();

            output.RenderBeginTag(HtmlTextWriterTag.Td); //�� 
            if (!Enabled)
            {
                output.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
            }
            output.AddAttribute(HtmlTextWriterAttribute.Value, ButtonText);
            myButton.RenderControl(output);
            output.RenderEndTag();

            output.RenderEndTag();
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
            output.RenderBeginTag(HtmlTextWriterTag.Td); //��

            output.AddAttribute(HtmlTextWriterAttribute.Id, myDivID);
            output.AddAttribute(HtmlTextWriterAttribute.Style, myDivStyle);
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            myTreeView.RenderControl(output);
            output.RenderEndTag();
            output.AddAttribute(HtmlTextWriterAttribute.Id, myIframeID);
            output.AddAttribute(HtmlTextWriterAttribute.Style, myIframeStyle);
            output.RenderBeginTag(HtmlTextWriterTag.Iframe);
            output.RenderEndTag();

            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
        }
//-----------------MyIframe�����Զ���----------------------------------------------------
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string myIframeID //���Ͽؼ��а�ť��ID 
        {
            get
            {
                EnsureChildControls();
                return this.ClientID + "_Iframe";
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string myIframeStyle //���Ͽؼ��а�ť��ID 
        {
            get
            {
                EnsureChildControls();
                string _myStyle = "";
                if (myTreeView.Visible == true)
                {
                    _myStyle = "position:absolute; width:150px;height:200px;z-index:98";
                }
                else
                {
                    _myStyle = "position:absolute; width:0px;height:0px;";
                }
                return _myStyle;
            }
        }
//-----------------MyDiv�����Զ���-------------------------------------------------------
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string myDivID //���Ͽؼ��а�ť��ID 
        {
            get
            {
                EnsureChildControls();
                return this.ClientID + "_Div";
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string myDivStyle //���Ͽؼ��а�ť��ID 
        {
            get
            {
                EnsureChildControls();
                string _myStyle = "";
                if (myTreeView.Visible == true)
                {
                    _myStyle = "position:absolute; width:150px;height:200px;z-index:99;background-color:LightGrey;overflow-y:scroll;";
                }
                else
                {
                    _myStyle = "position:absolute; width:0px;height:0px;";
                }
                return _myStyle;
            }
        }
//-----------------MyButton�����Զ���----------------------------------------------------
        [Category("�Զ�������")]
        [Description("��ť������")]
        public string ButtonText
        {
            get
            {
                EnsureChildControls();
                return ViewState["ButtonText"] == null ? _BUTTONDEFAULTTEXT : (string)ViewState["ButtonText"];
            }
            set
            {
                EnsureChildControls();
                ViewState["ButtonText"] = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string myButtonID //���Ͽؼ��а�ť��ID 
        {
            get
            {
                EnsureChildControls();
                return this.ClientID + "_myDutton";
            }
        }
//-----------------MyTextBox�����Զ���---------------------------------------------------
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string txtTextBoxID //���Ͽؼ��а�ť��ID 
        {
            get
            {
                EnsureChildControls();
                return this.ClientID;
            }
        }

        [Category("�Զ�������")]
        [DefaultValue("100px")]
        [Description("���ڵĿ��")]
        [Themeable(false)]
        public string TextBoxWidth
        {
            get
            {
                EnsureChildControls();
                return ViewState["TextBoxWidth"] == null ? "100px" : (string)ViewState["TextBoxWidth"];
            }
            set
            {
                EnsureChildControls();
                ViewState["TextBoxWidth"] = value;
            }
        }
//-----------------------------------------------------------------------------------------
        public void InitDataTabel(DataTable myDt)
        {
            _dt = myDt;
        }
        public void BuildTree(string TreeName, string TreeValue)
        {
            EnsureChildControls();
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                TreeNode tmp = new TreeNode();
                tmp.Text = _dt.Rows[i][TreeName].ToString();
                tmp.Value = _dt.Rows[i][TreeValue].ToString();
                myTreeView.Nodes.Add(tmp);
            }
        }

        public void BuildTreeNoTop(TreeNode tn, string TreeName, string TreeValue, string TreeNode, string TreeNodeValue)
        {
            int i; EnsureChildControls();
            // �սڵ�ʱ�������ڵ㣬��IDΪNULL�ĵ������ڵ�
            if (tn == null)
            {
                myTreeView.Nodes.Clear();
                for (i = 0; i < _dt.Rows.Count; i++)
                {
                    if (_dt.Rows[i][TreeNode].ToString() == TreeNodeValue)
                    {
                        TreeNode tmp = new TreeNode();
                        tmp.SelectAction = TreeNodeSelectAction.Select;
                        tmp.Text = _dt.Rows[i][TreeName].ToString();
                        tmp.Value = _dt.Rows[i][TreeValue].ToString();
                        myTreeView.Nodes.Add(tmp);
                    }
                }
                // ѭ���ݹ鴴����
                for (i = 0; i < myTreeView.Nodes.Count; i++)
                {
                    BuildTreeNoTop(myTreeView.Nodes[i],TreeName,TreeValue,TreeNode,TreeNodeValue);
                }
            }
            else // �ڵ�ǿ�Ϊ�ݹ����
            {

                for (i = 0; i < _dt.Rows.Count; i++)
                {
                    if (tn.Value == _dt.Rows[i][TreeNode].ToString())
                    {
                        TreeNode tmp = new TreeNode(_dt.Rows[i][TreeName].ToString());
                        tmp.SelectAction = TreeNodeSelectAction.Select;
                        tmp.Text = _dt.Rows[i][TreeName].ToString();
                        tmp.Value = _dt.Rows[i][TreeValue].ToString();
                        tn.ChildNodes.Add(tmp);
                    }
                }
                for (i = 0; i < tn.ChildNodes.Count; i++)
                {
                    BuildTreeNoTop(tn.ChildNodes[i], TreeName, TreeValue, TreeNode, TreeNodeValue);
                }
            }
        }
//-----------------------------------------------------------------------------------------
    }
}

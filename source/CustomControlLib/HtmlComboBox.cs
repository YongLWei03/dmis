using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


[assembly: TagPrefix("CustomControlLib.ComboBoxLib", "vortex")]
namespace PlatForm.CustomControlLib
{
	/// <summary>
	/// Represents a control that allows the user to select a single item from a drop-down list,
	/// or type in a new value that is not in the list.
	/// </summary>
	/// <remarks>
	/// Since the ComboBox control inherits from <see cref="System.Web.UI.WebControls.ListBox"/>,
	/// you can use it in exactly the same manner (including DataBinding).
	/// For older browsers that do not support the functionality required to render this control,
	/// a normal dropdown box will be emitted instead.
	/// </remarks>
	[ToolboxData("<{0}:HtmlComboBox runat=\"server\"></{0}:HtmlComboBox>")]
	[ValidationPropertyAttribute("SelectedValue")]
	public class HtmlComboBox : System.Web.UI.WebControls.ListBox
	{
		/// <summary>
		/// The <see cref="System.Web.UI.WebControls.TextBox"/> used internally to allow the user
		/// to type in new values.
		/// </summary>
		TextBox textBox = new TextBox();

		/// <summary>
		/// Indicates if the current browser supports rendering the ComboBox.
		/// </summary>
		bool isSupportedBrowser = false;

		/// <summary>
		/// The maximum number of characters that a user can manually type in
		/// </summary>
		int maxLength = 0;

		/// <summary>
		/// Indicates if the ComboBox control should use the AutoFill technique
		/// </summary>
		bool enableAutoFill = false;

		/// <summary>
		/// The path to an external .js file that contains all of the javascript functions
		/// needed by this control. If this is not set (the default behavior), then the 
		/// javascript is emitted inline.
		/// </summary>
		string externalResourcePath = null;

		/// <summary>
		/// The ID of the containing control
		/// </summary>
		string masterID = "combobox";

		/// <summary>
		/// The client ID of the containing control
		/// </summary>
		string masterClientID;


		/// <summary>
		/// Initializes a new instance of the ComboBox control
		/// </summary>
        public HtmlComboBox()
		{
			// determine if the current browser supports rendering the ComboBox
			// (this wont work in design-time, so lets skip it)
			CheckBrowserCapabilities();
		}


		/// <summary>
		/// Initializes the ComboBox control
		/// </summary>
		/// <param name="e">The <see cref="EventArgs"/> passed to this method</param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);

			this.masterID = this.ID;
			this.masterClientID = this.ClientID;

			// this makes the ListBox act like a DropDownList
			// (we cant just use DropDownList because it does not support adding child controls)
			this.SelectionMode = ListSelectionMode.Single;
			this.Rows = 1;

			// if the current browser supports rendering of the ComboBox, we need to set up
			// the internal TextBox control
			if(this.isSupportedBrowser)
			{
				// if we are rendering a ComboBox, we change the ID of the dropdown so 
				// that we can give the ID to the TextBox (this is required so that the control
				// works with validators)
				this.ID = String.Format("{0}", this.masterID);
				this.textBox.ID = String.Format("{0}_txt", this.masterID);

				this.Controls.Add(textBox);
			}
		}

		/// <summary>
		/// Gets or sets the index of the selected item in the DropDownList control.
		/// </summary>
		/// <value>
		/// The index of the selected item in the control. The default value is 0, which 
		/// selects the first item in the list.
		/// </value>
		/// <remarks>
		/// If the current browser supports rendering this control as a ComboBox,
		/// then this property will always return '-1'. Since the value can be typed
		/// in, the value may not exist in the Items collection, so SelectedIndex
		/// does not apply.
		/// If you set the SelectedIndex property, the value will be set even if the
		/// control is rendered as a ComboBox.
		/// </remarks>
		public override int SelectedIndex
		{
			get
			{
				if(this.isSupportedBrowser)
					return -1;
				else
					return base.SelectedIndex;
			}
			set
			{
				base.SelectedIndex = value;
			}
		}

		/// <summary>
		/// Gets the selected item with the lowest index in the list control.
		/// </summary>
		/// <remarks>
		/// If the current browser supports rendering this control as a ComboBox,
		/// then this property will always return <c>null</c>. Since the value can be typed
		/// in, the value may not exist in the Items collection, so SelectedItem
		/// does not apply.
		/// </remarks>
		public override ListItem SelectedItem
		{
			get
			{
				if(this.isSupportedBrowser)
				{
					if(base.Items.FindByText(this.textBox.Text) != null)
					{
						return base.Items.FindByText(this.textBox.Text);
					}
					else
					{
						return null;
					}
				}
				else
				{
					return base.SelectedItem;
				}
			}
		}

		/// <summary>
		/// Gets the value of the selected item in the list control, or selects the item 
		/// in the list control that contains the specified value.
		/// </summary>
		public override string SelectedValue
		{
			get
			{
				if(this.isSupportedBrowser)
				{
					if(this.SelectedItem != null)
					{
						return this.SelectedItem.Value;
					}
					else
					{
						return this.textBox.Text;
					}
				}
				else
				{
					return base.SelectedValue;
				}
			}
			set
			{
				if(this.isSupportedBrowser)
					this.textBox.Text = value;
				else
				{
					// we need a try...catch here in case the selected value was typed
					// in and thus does not exist in the Items collection
					try
					{
						base.SelectedValue = value;
					}
					catch
					{
						// swallow the exception
					}
				}
			}
		}
        public string SelectedText
        {
            get
            {
                return this.textBox.Text;
            }
            set
            {
                this.textBox.Text = value;
            }
        }
		/// <summary>
		/// Raises the PreRender event.
		/// </summary>
		/// <remarks>
		/// The common javascript functions must be dealt with here because 'RegisterClientScriptBlock'
		/// cannot be called from the Render method (it is too late in the lifesycle).
		/// </remarks>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnPreRender(EventArgs e)
		{
			if(this.Visible && this.isSupportedBrowser)
			{
				// if the javascript is in an external file, render a link to it.
				// otherwise, emit the javascript inline.
				if(this.externalResourcePath != null)
				{
					this.Page.RegisterClientScriptBlock("combobox_external_functions", String.Format("<script language='javascript' type='text/javascript' src='{0}'></script>", this.ResolveUrl(this.externalResourcePath)));
				}
				else
				{
					// however, if another control has already linked in the external file, we 
					// cant emit the inline javascript because it will conflict. if that is the case, 
					// we dont have to do anything because it is already all handled.
					if(!this.Page.IsClientScriptBlockRegistered("combobox_external_functions"))
					{
						// register the common javascript needed by all instances of the ComboBox control
						this.Page.RegisterClientScriptBlock("combobox_basic_functions", javascriptBasicFunctions);

						// if autofill is on, emit the necessary javascript
						// (we dont emit it if we dont need it to save on bandwidth)
						if(this.enableAutoFill)
						{
							this.Page.RegisterClientScriptBlock("combobox_autofill_functions", javascriptAutoFillFunctions);
						}
					}
				}
			}
			else
			{
				base.OnPreRender(e);
			}
		}


		/// <summary>
		/// Renders the control as HTML.
		/// </summary>
		/// <remarks>
		/// If the current browser does not support the functionality required to render the
		/// ComboBox, a normal <see cref="ListBox"/> will be rendered instead.
		/// </remarks>
		/// <param name="writer">The <see cref="HtmlTextWriter"/> to which to emit the resulting HTML.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			if(this.Visible && this.isSupportedBrowser)
			{
				// to make the control look correct, we have to pass all of the dropdown's 
				// visual properties to the textbox as well.
				this.textBox.BackColor = this.BackColor;
				this.textBox.BorderColor = this.BorderColor;
				this.textBox.BorderStyle = this.BorderStyle;
				this.textBox.BorderWidth = this.BorderWidth;
				this.textBox.CssClass = this.CssClass;
				this.textBox.Enabled = this.Enabled;
				this.textBox.Font.Bold = this.Font.Bold;
				this.textBox.Font.Italic = this.Font.Italic;
				this.textBox.Font.Name = this.Font.Name;
				this.textBox.Font.Names = this.Font.Names;
				this.textBox.Font.Overline = this.Font.Overline;
				this.textBox.Font.Size = this.Font.Size;
				this.textBox.Font.Strikeout = this.Font.Strikeout;
				this.textBox.Font.Underline = this.Font.Underline;
				this.textBox.ForeColor = this.ForeColor;
                this.textBox.Height = this.Height;
				this.textBox.TabIndex = this.TabIndex;
                this.textBox.Width = this.Width;
                
				// handle the default value
				if(this.SelectedItem != null)
					this.textBox.Text = this.SelectedItem.Text;

				// handle the maximum length
				this.textBox.MaxLength = this.maxLength;

				// dont allow the dropdown box to get the focus if this is a ComboBox
				this.TabIndex = -1;

				// register the javascript specific to this instance of the ComboBox
				this.Page.RegisterStartupScript(this.masterClientID, String.Format(initializationScriptFormat, this.masterClientID));

				// hook up the javascript 'onchange' event of the dropdown.
				string onChangeCommand = String.Format("combobox_dropdown_onchange('{0}');", this.masterClientID);
				this.Attributes.Add("onchange", onChangeCommand);

				// hook up the javascript 'onkeyup' event of the textbox, if we are using autofill
				if(this.enableAutoFill)
				{
					this.textBox.Attributes.Add("onkeyup", String.Format("return combobox_textbox_autofill_onkeyup('{0}', event);", this.masterClientID));
				}

				// hook up the javascript 'onkeydown' event of the text box. this makes sure the 
				// selectedIndex gets removed properly if a new value is typed in
				this.textBox.Attributes.Add("onkeydown", String.Format("return combobox_textbox_onkeydown('{0}', event);", this.masterClientID));

				// this is mainly used with 'GridLayout' where the designer has already set some
				// styles that position this element
				string existingStyle = this.Attributes["style"];

				// these 'style' attributes are responsible (with some of the javascript initialization)
				// for making sure the control renders properly
                this.Attributes.Add("style", String.Format("position:absolute;top:0px;left:0px;visibility:hidden;{0}", (!this.Width.IsEmpty ? String.Format("width:{0};", this.Width.ToString()) : "")));
                this.textBox.Attributes.Add("style", String.Format("position:relative;top:0px;left:0px;z-index:200;{0}", (!this.Width.IsEmpty ? String.Format("width:{0};", this.Width.ToString()) : "")));

				// render the control
                writer.Write(String.Format("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" id=\"{2}_container\" name=\"{2}_container\" style=\"margin:0px;padding:0px;display:inline;vertical-align:-4px;{0}{1}\"><tr><td><div style=\"position:relative;\">", (!this.Width.IsEmpty ? String.Format("width:{0};", this.Width.ToString()) : ""), existingStyle, this.masterClientID));
                base.Render(writer);
                this.textBox.RenderControl(writer);
                writer.Write("</div></td></tr></table>");
			}
			else
			{
				// if the ComboBox is not supported by the current browser, just render
				// a plain old ListBox. we also do this if the control is in design-mode
				base.Render(writer);
			}
		}

		/// <summary>
		/// Checks the current browser to determine if it supports the functionality required
		/// to render the ComboBox.
		/// </summary>
		/// <remarks>
		/// This is currently coded to emit the ComboBox in IE 5+, Netscape 6+, Mozilla 1+,
		/// and Firefox 1+. These were the only browsers that I could accurately verify that
		/// the ComboBox rendered correctly in.
		/// All other browsers will get a standard <see cref="ListBox"/> control.
		/// If you can verify that other browsers support the rendering requirements (like Opera,
		/// etc), then you can modify this method OR your browser capabilities section in the
		/// machine.config.
		/// </remarks>
		private void CheckBrowserCapabilities()
		{
			bool result = true;//GLT ÐÞ¸Ä¹ý£¡
			if(HttpContext.Current != null)
			{
				if(HttpContext.Current.Request.Browser.JavaScript)
				{
					// allow IE 5+
					if(HttpContext.Current.Request.Browser.W3CDomVersion.Major >= 1)
					{
						result = true;
					}
					else
					{
						// allow NS 5+, MZ 1+, and FireFox 1+
						if(HttpContext.Current.Request.Browser.Browser == "Netscape" && HttpContext.Current.Request.Browser.MajorVersion >= 5)
						{
							result = true;
						}
					}
				}
			}
			this.isSupportedBrowser = result;
		}

		/// <summary>
		/// Indicates if the current browser supports rendering the control as a ComboBox or not
		/// </summary>
		/// <value>
		/// <c>true</c> if the browser supports rendering the ComboBox control;
		/// <c>false</c> if the browser is incapable of rendering the ComboBox control.
		/// </value>
		/// <remarks>
		/// If the current browser does NOT support rendering the ComboBox control, a simple
		/// <see cref="ListBox"/> control will be rendered instead.
		/// </remarks>
		[Description("Indicates if the current browser supports rendering the control as a ComboBox or not." ),
		Browsable (true),
		Category("Behavior")]
		public bool IsSupportedBrowser
        {
            get
            {
                return this.isSupportedBrowser;
            }
            set
            {
                this.isSupportedBrowser = value;
            }
        }

		/// <summary>
		/// Indicates if the ComboBox should use the AutoFill technique
		/// </summary>
		/// <value>
		/// <c>true</c> if the control should AutoFill;
		/// <c>false</c> otherwise [default].
		/// </value>
		/// <remarks>
		/// If the current browser does NOT support rendering the ComboBox control, this
		/// property has no effect.
		/// </remarks>
		[Description("Indicates if the ComboBox should use the AutoFill technique." ),
		Browsable (true) ,
		Category("Behavior")]
		public bool EnableAutoFill
		{
			get
			{
				return this.enableAutoFill;
			}
			set
			{
				this.enableAutoFill = value;
			}
		}

		/// <summary>
		/// Gets or sets the maximum number of characters allowed to be manually entered.
		/// </summary>
		/// <value>
		/// The maxumum number of characters allowed to be manually entered. 
		/// The default is 0, which indicates that the property is not set.
		/// </value>
		/// <remarks>
		/// If the current browser does not support rendering of the ComboBox, this
		/// property has no effect.
		/// </remarks>
		[Description("Gets or sets the maximum number of characters allowed to be manually entered." ),
		Browsable (true) ,
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

		/// <summary>
		/// Sets the path to an external .js file that contains all of the javascript 
		/// required by this control. If not set, the javascript will be emitted inline.
		/// </summary>
		/// <value>
		/// The valid path to the .js file, or <c>null</c>.
		/// </value>
		/// <remarks>
		/// If the current browser does not support rendering of the ComboBox, this
		/// property has no effect.
		/// </remarks>
		[Description("Sets the path to an external .js file that contains all of the javascript required by this control. If not set, the javascript will be emitted inline." ),
		Browsable (true) ,
		Category("Behavior")]
		public string ExternalResourcePath
		{
			get
			{
				return this.externalResourcePath;
			}
			set
			{
				this.externalResourcePath = value;
			}
		}

		/// <summary>
		/// The format of the javascript that will be emitted for each ComboBox control that
		/// is placed on the page.
		/// </summary>
		private static string initializationScriptFormat = "<script type='text/javascript'>combobox_initialize('{0}');</script>";

		/// <summary>
		/// The basic javascript that needs to be emitted once per page that contains any ComboBox
		/// controls.
		/// (This would be nice if it were not in the compiled code so that it could be edited/
		/// updated with out a recompile. However, in the interest of making this control self-
		/// contained and simple for the demo, it is just included here. It could easily be moved
		/// to a seperate .js file that is referenced here.)
		/// </summary>
		private static string javascriptBasicFunctions = @"
			<script type='text/javascript'>
			<!--
			function combobox_initialize(comboboxID)
			{
				var DROPDOWN_DOWN_ARROW_WIDTH = 18;

				var combobox = document.getElementById(comboboxID + '_container');
				var combobox_textbox = document.getElementById(comboboxID + '_txt');
				var combobox_dropdown = document.getElementById(comboboxID + '');

				var controlWidth = combobox_dropdown.offsetWidth;
				var controlHeight = combobox_dropdown.offsetHeight-5;//GLT
				var visibleWidth = controlWidth - DROPDOWN_DOWN_ARROW_WIDTH;
				var visibleHeight = controlHeight;

				combobox_textbox.offsetParent.style.width = (controlWidth) + 'px';

				combobox.style.width = (controlWidth) + 'px';
				combobox_textbox.style.width = (visibleWidth + 1) + 'px';//GLT
				combobox_textbox.style.height = visibleHeight + 'px';
				combobox_textbox.style.visibility = 'visible';

				var szClippingRegion = 'rect (auto auto auto ' + (visibleWidth - 1) + 'px)';
				combobox_dropdown.style.overflow = 'hidden';
				combobox_dropdown.style.clip = szClippingRegion;
				combobox_dropdown.selectedIndex = -1;
				combobox_dropdown.style.visibility = 'visible';

				// browser specific hacks
				if(document.all)
				{
					combobox_dropdown.style.marginTop = '1px';
				}
				else
				{
					combobox.style.verticalAlign = '-6px;'
					combobox_textbox.style.paddingLeft = '3px';
				}
			}

			function combobox_dropdown_onchange(comboboxID)
			{
				var combobox_textbox = document.getElementById(comboboxID + '_txt');
				var combobox_dropdown = document.getElementById(comboboxID + '');
				combobox_textbox.value = combobox_dropdown[combobox_dropdown.selectedIndex].text;
			}

			function combobox_textbox_onkeydown(comboboxID, e)
			{
				var combobox_textbox = document.getElementById(comboboxID + '_txt');
				var combobox_dropdown = document.getElementById(comboboxID + '');
				combobox_dropdown.selectedIndex = -1;
				return true;
			}
			//-->
			</script>
			";

		/// <summary>
		/// The auto-fill javascript that needs to be emitted once per page that contains any ComboBox
		/// controls.
		/// (This would be nice if it were not in the compiled code so that it could be edited/
		/// updated with out a recompile. However, in the interest of making this control self-
		/// contained and simple for the demo, it is just included here. It could easily be moved
		/// to a seperate .js file that is referenced here.)
		/// </summary>
		private static string javascriptAutoFillFunctions = @"
			<script type='text/javascript'>
			<!--
			function combobox_textbox_autofill_onkeyup(comboboxID, e)
			{
				var charCode = (e.charCode ? e.charCode : e.keyCode);
				if(charCode < 32 || (charCode >= 33 && charCode <= 46) || (charCode >= 112 && charCode <= 123))
				{
					return true;
				}

				var combobox_textbox = document.getElementById(comboboxID + '');
				var combobox_dropdown = document.getElementById(comboboxID + '_dropdown');
				combobox_dropdown.selectedIndex = -1;

				var searchString = combobox_textbox.value.toUpperCase();
				var currentLength = searchString.length;

				for(i=0;i<combobox_dropdown.options.length;i++)
				{
					var option = combobox_dropdown.options[i];
					var optionText = option.text.toUpperCase();
					if(optionText.indexOf(searchString) == 0)
					{
						var optionLength = option.text.length;
						combobox_textbox.value = option.text;
						combobox_selectrange(combobox_textbox, currentLength, optionLength - currentLength);
						return false;
						break;
					}
				}
				return true;
			}

			function combobox_selectrange(element, start, length)
			{
				if(element.createTextRange)
				{
					var oRange = element.createTextRange();
					oRange.moveStart('character', start);
					oRange.moveEnd('character', length);
					oRange.select();
				}
				else if(element.setSelectionRange)
				{
					element.setSelectionRange(start, start + length);
				}

				element.focus();
			};
			//-->
			</script>
			";
	}
}

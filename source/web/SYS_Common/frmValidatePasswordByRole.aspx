<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmValidatePasswordByRole.aspx.cs" Inherits="SYS_Common_frmValidatePasswordByRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>验证口令</title>
</head>
<body>
    <form id="form1" runat="server">
        <div  align="center">
		    <table id="Table1" cellspacing="0" borderColorDark="#ffffff" cellPadding="2"
		        borderColorLight="#003b8a" border="1" style="width: 262px; height: 162px">
		        <tr style="COLOR: #006600; BACKGROUND-COLOR: #ddeecc" bgColor="#f3d388">
			        <td align="center" colSpan="2" style="height: 20px">
                        验证口令
			        </td>
		        </tr>
		        <tr>
			        <td height="60" align="center" valign="middle">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 184px; height: 89px">
                            <tr>
                                <td style="width: 63px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 63px">
                                    <span style="font-size: 10pt">姓名</span></td>
                                <td>
                                    <asp:DropDownList ID="ddlMemberName" runat="server" Width="104px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 63px">
                                    <span style="font-size: 10pt">口令</span></td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" Width="99px" TextMode="Password"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 17px; color: red;" colspan="2" id="tdMessage" runat="server">
                                </td>
                            </tr>
                        </table>
				        </td>
		        </tr>
		        <tr>
		        <td>
		        <p align="center">
		        <asp:button id="btnOk" Text="确定" runat="server" BorderStyle="Solid" BorderWidth="1px" BackColor="Gainsboro" ForeColor="Black" OnClick="btnOk_Click" />
		        &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
		        <input type="button" value="返回" onclick="javascript:window.close();"  id="Button1" name="Button1" style="background-color: gainsboro"/>
		        </p>
		        </td>
		        </tr>
	        </table>
        </div>
    </form>
</body>
</html>

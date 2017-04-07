<%@ page language="C#" autoeventwireup="true" inherits="frmModiyfPassword, App_Web_og9prjkz" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body style="text-align: center; ">
    <div style="padding-top: 5%;">
    <form id="form1" runat="server">
    
    <table  style="background-color: #000000; width: 63%;" border="0" cellpadding="0" cellspacing="1">
        <tr>
            <td align="center" style="height: 20px; background-color: skyblue; width: 609px;">
                <strong>
                    <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1" Text="修改口令"></asp:Label></strong></td>
        </tr>
        <tr>
            <td align="center" style="width: 609px; background-color: white; height: 248px;">
                <table style="width: 310px; height: 163px">
                    <tr>
                        <td style="width: 108px; height: 29px; text-align: right;">
                            <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" Text="原口令："></asp:Label>
                        </td>
                        <td style="width: 50%; height: 29px">
                            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" meta:resourcekey="txtOldPasswordResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 108px; height: 28px; text-align: right;">
                            <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" Text="新口令："></asp:Label></td>
                        <td style="width: 50%; height: 28px">
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" meta:resourcekey="txtNewPasswordResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 108px; height: 31px; text-align: right;">
                            <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" Text="确认新口令："></asp:Label></td>
                        <td style="width: 50%; height: 31px">
                            <asp:TextBox ID="txtAgain" runat="server" TextMode="Password" meta:resourcekey="txtAgainResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 29px">
                            <asp:Button ID="btnOk" runat="server" Text="确定" OnClick="btnOk_Click" meta:resourcekey="btnOkResource1" /></td>
                        <td style="height: 29px">
                            <asp:Button ID="btnReset" runat="server" meta:resourcekey="btnResetResource1" OnClick="btnReset_Click"
                                Text="重置" /></td>
                    </tr>
                    <tr>
                        <td id="tdMessage" runat="server" colspan="2" style="color: red; height: 24px; text-align: left">
                        </td>
                    </tr>
                </table>
  
             </td>
         </tr>
     </table>
    </form>
    </div>
</body>
</html>

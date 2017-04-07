<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDisplayDataServerStatus.aspx.cs" Inherits="SYS_Common_frmDisplayDataServerStatus" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据库服务器状态查询</title>
</head>
<body style="text-align: center; ">
    <div style="padding-top: 5%;">
    <form id="form1" runat="server">
    
    <table  style="background-color: #000000; width: 66%;" border="0" cellpadding="0" cellspacing="1">
        <tr>
            <td align="center" style="height: 20px; background-color: skyblue; width: 609px;">
                <strong>
                    <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" Text="数据库服务器状态"></asp:Label></strong></td>
        </tr>
        <tr>
            <td align="center" style="width: 609px; background-color: white; height: 248px;">
                <table border="0" cellpadding="0" cellspacing="1" style="width: 497px; height: 147px;
                    background-color: #330000">
                    <tr>
                        <td style="font-size: 9pt; width: 18%; height: 33px; background-color: gainsboro;
                            text-align: center">
                            &nbsp;<asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1" Text="是否设置成双数据库服务器"></asp:Label></td>
                        <td id="tdIsDouble" runat="server" style="width: 35%; height: 33px; background-color: #ffffff;
                            text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 9pt; width: 18%; height: 35px; background-color: gainsboro;
                            text-align: center">
                            <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" Text="主数据库服务器是否正常"></asp:Label></td>
                        <td id="tdMainServerStatus" runat="server" style="width: 35%; height: 35px; background-color: #ffffff;
                            text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 9pt; width: 18%; height: 35px; background-color: gainsboro;
                            text-align: center">
                            <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" Text="备数据库服务器是否正常"></asp:Label></td>
                        <td id="tdSlaveServerStatus" runat="server" style="width: 35%; height: 35px; background-color: #ffffff;
                            text-align: left">
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

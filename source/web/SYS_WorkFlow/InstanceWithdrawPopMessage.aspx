<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InstanceWithdrawPopMessage.aspx.cs" Inherits="SYS_WorkFlow_ayf_InstanceWithdrawPopMessage" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>退回理由</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 408px; height: 147px;
            background-color: #330000">
            <tr>
                <td style="font-size: 9pt; width: 15%; height: 26px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" Text="任务描述"></asp:Label></td>
                <td id="tdPackDesc" runat="server" colspan="3" style="font-size: 9pt; height: 26px;
                    background-color: #ffffff">
                </td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 15%; height: 26px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" Text="退回步骤"></asp:Label></td>
                <td id="TD1" style="width: 35%; height: 26px; background-color: #ffffff">
                    <asp:TextBox ID="txtF_FLOWNAME" runat="server" BorderStyle="None" Enabled="False" Width="137px" meta:resourcekey="txtF_FLOWNAMEResource1"></asp:TextBox></td>
                <td style="font-size: 9pt; width: 15%; height: 26px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1" Text="接受人"></asp:Label></td>
                <td id="TD3" style="width: 35%; height: 26px; background-color: #ffffff">
                    <asp:TextBox ID="txtMEMBER_NAME" runat="server" BorderStyle="None" Enabled="False"
                        Width="127px" meta:resourcekey="txtMEMBER_NAMEResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 15%; height: 65px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" Text="退回理由"></asp:Label></td>
                <td colspan="3" style="width: 75%; height: 65px; background-color: #ffffff">
                    <asp:TextBox ID="txtREASON" runat="server" Height="57px" MaxLength="255" TextMode="MultiLine"
                        Width="331px" meta:resourcekey="txtREASONResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 28px; background-color: #ffffff; text-align: center">
                    &nbsp;<asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" Text="确定" Width="46px" meta:resourcekey="btnOKResource1" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="取消" Width="44px" meta:resourcekey="btnCancelResource1" /></td>
            </tr>
            <tr>
                <td id="tdMessage" runat="server" align="left" colspan="4" style="color: red; height: 25px;
                    background-color: #ffffff; text-align: center">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

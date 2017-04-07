<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InstanceAbolishPopMessage.aspx.cs" Inherits="SYS_WorkFlow_ayf_InstanceAbolishPopMessage" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>业务作废原因</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="1" style="background-color: #330000; width: 408px; height: 147px;">
            <tr>
                <td style="width: 15%; height: 26px; background-color: gainsboro; text-align: center; font-size: 9pt;">
                    <asp:Label ID="lblPackDesc" runat="server" Text="任务描述" meta:resourcekey="lblPackDescResource1"></asp:Label></td>
                <td id="tdPackDesc" runat="server" colspan="3" style="height: 26px; background-color: #ffffff; font-size: 9pt;">
                </td>
            </tr>
            <tr>
                <td style="background-color: gainsboro; width: 15%; height: 26px; text-align: center; font-size: 9pt;">
                    <asp:Label ID="lblDATEM" runat="server" Text="时间" meta:resourcekey="lblDATEMResource1"></asp:Label></td>
                <td style="height: 26px; background-color: #ffffff; width: 35%;" id="TD1">
                    <asp:TextBox ID="txtDATEM" runat="server" BorderStyle="None" Enabled="False" Width="137px" meta:resourcekey="txtDATEMResource1"></asp:TextBox></td>
                <td style="height: 26px; background-color: gainsboro; width: 15%; text-align: center; font-size: 9pt;">
                    <asp:Label ID="lblMEMBER_NAME" runat="server" Text="操作人" meta:resourcekey="lblMEMBER_NAMEResource1"></asp:Label></td>
                <td style="height: 26px; background-color: #ffffff; width: 35%;" id="TD3">
                    <asp:TextBox ID="txtMEMBER_NAME" runat="server" BorderStyle="None" Enabled="False" Width="127px" meta:resourcekey="txtMEMBER_NAMEResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="background-color: gainsboro; width: 15%; height: 26px; text-align: center; font-size: 9pt;">
                    <asp:Label ID="lblOPT_TYPE" runat="server" Text="操作类别" meta:resourcekey="lblOPT_TYPEResource1"></asp:Label></td>
                <td style="background-color: #ffffff; width: 35%; height: 26px;" id="TD2">
                    <asp:TextBox ID="txtOPT_TYPE" runat="server" BorderStyle="None" Enabled="False" Width="143px" meta:resourcekey="txtOPT_TYPEResource1"></asp:TextBox></td>
                <td style="background-color: gainsboro; width: 15%; height: 26px;">
                </td>
                <td style="background-color: #ffffff; width: 35%; height: 26px;">
                    <asp:TextBox ID="txtTID" runat="server" Visible="False" Width="20px" meta:resourcekey="txtTIDResource1"></asp:TextBox>
                    <asp:TextBox ID="txtPACKNO" runat="server" Visible="False" Width="19px" meta:resourcekey="txtPACKNOResource1"></asp:TextBox>
                    <asp:TextBox ID="txtF_PACKTYPENAME" runat="server" Visible="False" Width="20px" meta:resourcekey="txtF_PACKTYPENAMEResource1"></asp:TextBox>
                    <asp:TextBox ID="txtF_PACKTYPENO" runat="server" Visible="False" Width="20px" meta:resourcekey="txtF_PACKTYPENOResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="background-color: gainsboro; width: 15%; text-align: center; height: 65px; font-size: 9pt;">
                    <asp:Label ID="lblREASON" runat="server" Text="理由" meta:resourcekey="lblREASONResource1"></asp:Label></td>
                <td style="background-color: #ffffff; width: 75%; height: 65px;" colspan="3">
                    <asp:TextBox ID="txtREASON" runat="server" Height="57px" MaxLength="255" TextMode="MultiLine"
                        Width="331px" meta:resourcekey="txtREASONResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="background-color: #ffffff; height: 28px; text-align: center;" colspan="4">
                    &nbsp;<asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" Text="确定" Width="46px" meta:resourcekey="btnOKResource1" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="取消" Width="44px" meta:resourcekey="btnCancelResource1" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

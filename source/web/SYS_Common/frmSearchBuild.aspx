<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSearchBuild.aspx.cs" Inherits="frmSearchBuild" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <LINK HREF="Calendar.css" TYPE="text/css" REL="stylesheet">
	<script language="javascript" src="Calendar.js"></script>
</head>
<body style="text-align: center; ">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 81%; height: 251px;">
            <tr>
                <td colspan="2" width="15%" style="height: 21px; text-align: center;" >
                    </td>
                <td style="height: 21px; font-weight: bold; font-size: 14pt; color: #660000;" id="tdTitle" runat="server" width="70%" >
                </td>
                <td style="height: 21px; text-align: center;" width="15%">
                    </td>
            </tr>
            <tr>
                <td style="height: 14px; text-align: center;" colspan="4" >
                    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; background-color: #000000">
                        <tr>
                            <td align="center" style="height: 20px; background-color: lightskyblue">
                                <span style="font-size: 10pt; color: #990000">
                                    <asp:Label ID="Label1" runat="server" Text="自定义条件查询" meta:resourcekey="Label1Resource1"></asp:Label></span></td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 67px; background-color: white">
                <asp:Table ID="tblQuery" runat="server" Width="579px" BorderColor="Maroon" BorderStyle="Solid" GridLines="Both" HorizontalAlign="Center" BorderWidth="1px" CellPadding="2" CellSpacing="0" Font-Size="9pt" meta:resourcekey="tblQueryResource1">
            <asp:TableRow ID="trHead" runat="server" HorizontalAlign="Center" meta:resourcekey="trHeadResource1">
                <asp:TableCell ID="TableCell1" runat="server" meta:resourcekey="TableCell1Resource1">列名</asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" meta:resourcekey="TableCell2Resource1">关系符</asp:TableCell>
                <asp:TableCell ID="TableCell3" runat="server" meta:resourcekey="TableCell3Resource1">值</asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server" meta:resourcekey="TableCell4Resource1">逻辑符</asp:TableCell>
                <asp:TableCell ID="TableCell5" runat="server" meta:resourcekey="TableCell5Resource1">删除</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 33px; background-color: white">
                    <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" meta:resourcekey="btnOKResource1" />&nbsp; &nbsp;<asp:Button ID="Button1" runat="server" Text="关闭" OnClientClick="javascript:window.close();" meta:resourcekey="Button1Resource1" /></td>
                        </tr>
                    </table>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td id="tdMessage" runat="server" colspan="4" style="height: 14px; text-align: center; font-size: 9pt; color: red;" align="left">
                </td>
            </tr>
        </table>
       </div>
        &nbsp; &nbsp;&nbsp;
    </form>

</body>
</html>

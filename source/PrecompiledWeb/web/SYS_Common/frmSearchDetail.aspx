<%@ page language="C#" autoeventwireup="true" inherits="frmSearchDetail, App_Web_og9prjkz" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>自定义查询-详细</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" Height="56px" Width="100%" HorizontalAlign="Center">
            <table style="width: 80%; height: 38px">
                <tr>
                    <td style="width: 78px">
                        <img src="../img/seg_rec.gif" /></td>
                    <td align="left" style="width: 356px">
                        <asp:Label ID="lbName" runat="server" Font-Names="楷体_GB2312" Font-Size="X-Large"
                            ForeColor="#400000"></asp:Label></td>
                    <td style="width: 100px">
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table style="width: 100%">
            <tr>
                <td style="width: 100%">
                    <asp:Table ID="tbMain" runat="server" BorderColor="SlateGray" CellPadding="1" CellSpacing="1" Font-Size="Small" Width="100%" BorderWidth="1px" GridLines="Both">
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 21px;">
                    <asp:Table ID="tblDetail1" runat="server" BorderColor="LightSlateGray" CellPadding="1" CellSpacing="1" Font-Size="Small" Width="100%" BorderWidth="1px" GridLines="Both">
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <asp:Table ID="tblDetail2" runat="server" BorderColor="LightSlateGray" CellPadding="1" CellSpacing="1" Font-Size="Small" Width="100%" BorderWidth="1px" GridLines="Both">
                    </asp:Table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

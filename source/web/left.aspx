<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>树形菜单</title>
</head>
<body bgcolor="WhiteSmoke">
    <form id="form1" runat="server">
    <div style="text-align: left" >
        <asp:TreeView ID="trvTreeMenu" runat="server" BackColor="Transparent" ExpandDepth="0"
            ExpandImageUrl="img/selectChildNode.gif" Font-Names="宋体" Font-Size="10pt" Height="100%"
            Width="195px" ShowLines="True" ForeColor="Black">
            <SelectedNodeStyle Font-Bold="True" Font-Italic="True" ForeColor="Maroon" ImageUrl="~/img/selectChildNode.gif" />
            <RootNodeStyle Font-Size="14px" />
            <NodeStyle NodeSpacing="1pt" HorizontalPadding="2pt" />
        </asp:TreeView>
    </div>
    </form>
</body>
</html>

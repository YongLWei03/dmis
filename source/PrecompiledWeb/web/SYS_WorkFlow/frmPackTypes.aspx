<%@ page language="C#" autoeventwireup="true" inherits="SYS_WorkFlow_frmPackTypes, App_Web_iakvuhia" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: left">
        <asp:TreeView ID="trvPackTypes" runat="server" ExpandDepth="0" Font-Names="宋体" Font-Size="10pt"
            ForeColor="Black" Height="100%" ShowLines="True" Width="243px">
            <SelectedNodeStyle Font-Bold="True" Font-Italic="True" Font-Overline="True" Font-Size="12pt"
                ForeColor="#400000" ImageUrl="~/img/stationopen.gif" />
            <NodeStyle ImageUrl="~/img/station.gif" />
        </asp:TreeView>
    
    </div>
    </form>
</body>
</html>

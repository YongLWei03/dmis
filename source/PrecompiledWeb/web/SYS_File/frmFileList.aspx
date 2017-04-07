<%@ page language="C#" autoeventwireup="true" inherits="frmFileList, App_Web_d7hnbwar" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body background="../img/bj.gif">
    <form id="form1" runat="server">
    <div>
        <asp:DataList ID="dlsFileList" runat="server" Height="75px" HorizontalAlign="Center"
            RepeatDirection="Horizontal" Width="80%" OnItemCommand="dlsFileList_ItemCommand" DataKeyField="TID" OnItemDataBound="dlsFileList_ItemDataBound" >
            <ItemTemplate >
                    <asp:ImageButton ID="imbFileIcon" runat="server" Height="31px" Width="29px" ImageUrl='<%# Eval("ICO") %>'  />&nbsp;
                <asp:Label ID="lblTID" runat="server" Visible="False" Width="1px" Text='<%# Eval("TID") %>'></asp:Label><br />
                &nbsp;<asp:HyperLink ID="hplFile" runat="server">[hplFile]</asp:HyperLink>
            </ItemTemplate>
            <SelectedItemTemplate>
                <asp:ImageButton ID="imbSelectedFileIcon" runat="server" ImageUrl='<%# Eval("ICO") %>' />&nbsp;
                <asp:Label ID="lblTID" runat="server" Visible="False" Width="1px" Text='<%# Eval("TID") %>'></asp:Label><br />
                &nbsp;<asp:HyperLink ID="hplFile" runat="server">[hplFile]</asp:HyperLink>
            </SelectedItemTemplate>
            <SelectedItemStyle Font-Bold="True" Font-Italic="True" Font-Underline="True" ForeColor="Red" />
        </asp:DataList></div>
    </form>
</body>
</html>

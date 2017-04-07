<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmBrowseDetail.aspx.cs" Inherits="SYS_Common_frmBrowseDetail" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body class="detail_body">
    <form id="form1" runat="server">
    <div id="detail_head">
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label></div>
    <div id="detail_data" runat="server">

    </div>
    <div id="detail_control">
        <br />
        <asp:TextBox ID="txtTID" runat="server" EnableTheming="False" Visible="False" Width="32px" meta:resourcekey="txtTIDResource2"></asp:TextBox>
        <asp:Button ID="btnSave" runat="server" Enabled="False" OnClick="btnSave_Click" Text="保存" meta:resourcekey="btnSaveResource1" />&nbsp;
        <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="返回" meta:resourcekey="btnReturnResource1" />&nbsp;
    </div>
    <div id="detail_info" runat="server">
    </div>
    </form>
</body>
</html>

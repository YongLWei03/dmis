<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpLoad.aspx.cs" Inherits="UpLoad" Theme="default" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target='_self'/>
    <title>上传文件</title>
</head>
<body leftmargin=0 topmargin="5" style="text-align: center;">
	<form id="Form2" encType="multipart/form-data" runat="server">
        &nbsp;
   <table  style="background-color: #000000; width: 540px;" border="0" cellpadding="0" cellspacing="1">
        <tr>
            <td align="center" style="height: 20px; background-color: lightskyblue;">
                <strong>
                    <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1" Text="文件上传"></asp:Label></strong></td>
        </tr>
        <tr>
            <td align="center" style="background-color: white; height: 248px;">
                <table bgcolor="#cccccc" border="0" cellpadding="0" cellspacing="1" style="width: 528px">
                    <tr>
                        <td align="center" bgcolor="#ffffff" colspan="2" style="height: 35px; width: 100px; background-color: gainsboro; font-size: 9pt;">
                            <span style="font-size: 9pt">&nbsp;<asp:Label ID="Label3" runat="server" Text="选择上载文件：" meta:resourcekey="Label3Resource1"></asp:Label></span></td>
                        <td bgcolor="#ffffff" colspan="2" style="height: 35px; text-align: left;">
                            <asp:FileUpload ID="MyFileInput" runat="server"
                                            Width="409px" OnDataBinding="MyFileInput_DataBinding" meta:resourcekey="MyFileInputResource1"  /></td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="#ffffff" colspan="2" style="height: 36px; width: 100px; background-color: gainsboro; font-size: 9pt;">
                            <span style="font-size: 9pt">
                                <asp:Label ID="Label2" runat="server" Text="文件标题：" meta:resourcekey="Label2Resource1"></asp:Label></span></td>
                        <td bgcolor="#ffffff" colspan="2" style="height: 36px; text-align: left;">
                            <asp:TextBox ID="txtTitle" runat="server" Width="406px" meta:resourcekey="txtTitleResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="#ffffff" colspan="2" style="height: 34px; width: 100px; background-color: gainsboro; font-size: 9pt;">
                            <span style="font-size: 9pt; background-color: gainsboro;">
                                <asp:Label ID="Label1" runat="server" Text="文件描述：" meta:resourcekey="Label1Resource1"></asp:Label></span></td>
                        <td bgcolor="#ffffff" colspan="2" style="height: 34px; text-align: left;">
                            <asp:TextBox ID="txtDesc" runat="server"
                                            Width="405px" meta:resourcekey="txtDescResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td bgcolor="#ffffff" style="height: 31px" colspan="4" align="center">
                        <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" Width="54px" meta:resourcekey="btnUploadResource1" />
                            &nbsp; &nbsp;&nbsp;
                        <asp:Button ID="btnClose" runat="server" Text="关闭" OnClick="btnClose_Click" Width="54px" meta:resourcekey="btnCloseResource1" /></td>
                    </tr>
                </table>
            </td>
            </tr>
            </table>							
		</form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewTask.aspx.cs" Inherits="SYS_WorkFlow_NewTask" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新建任务</title>
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="background-image: url(../img/main_bar.jpg); background-color: #006699; height: 20px; font-size: 9pt; text-align: left;" valign="middle">
                    <img src="../img/s_img.gif" />
                    <asp:Label ID="lblFuncName" runat="server" Text="新建任务" meta:resourcekey="lblFuncNameResource1"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px; height: 23px" valign="middle">
                </td>
            </tr>
            <tr>
                <td style="height: 299px" align="center" valign="top">
                    &nbsp;<table style="width: 224px; height: 311px">
                        <tr>
                            <td colspan="6" style="width: 215px; border-bottom: menu 1px dashed; height: 150px;
                                text-align: center" valign="top">
                                <asp:GridView ID="grvList" runat="server" AutoGenerateColumns="False" CaptionAlign="Top"
                                    DataKeyNames="f_no" Font-Size="10pt" OnSelectedIndexChanged="grvList_SelectedIndexChanged"
                                    Width="362px" OnRowDataBound="grvList_RowDataBound" meta:resourcekey="grvListResource1">
                                    <Columns>
                                        <asp:CommandField HeaderText="选择" SelectText=""
                                            ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/img/unselected.gif" meta:resourcekey="CommandFieldResource1" >
                                            <ItemStyle Width="40px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="f_name" HeaderText="业务类别名称" meta:resourcekey="BoundFieldResource1" />
                                    </Columns>
                                    <RowStyle Height="25px" />
                                    <HeaderStyle Height="25px" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 26px; text-align: center">
                                <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" Text="新建" Width="62px" meta:resourcekey="btnOKResource1" /></td>
                        </tr>
                    </table>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 23px;">
                </td>
            </tr>
            <tr>
                <td style="height: 23px;">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

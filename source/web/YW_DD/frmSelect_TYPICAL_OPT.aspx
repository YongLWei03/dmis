<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSelect_TYPICAL_OPT.aspx.cs" Inherits="YW_DD_frmSelect_TYPICAL_OPT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择典型票</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 550px">
            <tr>
                <td colspan="6" style="border-bottom: menu 1px dashed; height: 22px" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100px; height: 13px; text-align: center">
                                <asp:Label ID="Label1" runat="server" Text="厂站"></asp:Label></td>
                            <td style="width: 100px; height: 13px">
                                <asp:DropDownList ID="ddlStation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStation_SelectedIndexChanged"
                                    Width="127px">
                                </asp:DropDownList></td>
                            <td style="width: 100px; height: 13px; text-align: center">
                                <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="确定" Width="49px" /></td>
                            <td style="width: 100px; height: 13px; text-align: center">
                                &nbsp;<asp:Button ID="btnCancel" runat="server" EnableViewState="False" OnClientClick="window.close();"
                                    Text="取消" Width="49px" /></td>
                            <td style="width: 100px; height: 13px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border-bottom: menu 1px dashed; height: 150px" valign="top">
                    &nbsp;<asp:GridView ID="grvList" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="4" CssClass="font" DataKeyNames="TID"
                        EmptyDataText="没有满足条件的记录！" OnSelectedIndexChanged="grvRef_SelectedIndexChanged"
                        PageSize="20" Width="100%">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:ButtonField CommandName="Select" HeaderText="选择" Text="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="STATION" HeaderText="厂站">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TASK" HeaderText="操作目的" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="height: 26px">
                    <span>&nbsp; &nbsp;</span>&nbsp;<table width="100%">
                        <tr>
                            <td id="tdMessage" runat="server" style="font-size: 9pt; width: 645px; height: 23px">
                            </td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnFirst" runat="server" CommandName="first" OnClick="NavigateToPage"
                                    Text="首页" /></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnPrevious" runat="server" CommandName="prev" OnClick="NavigateToPage"
                                    Text="上一页" /></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnNext" runat="server" CommandName="next" OnClick="NavigateToPage"
                                    Text="下一页" /></td>
                            <td align="center" style="width: 71px; height: 23px">
                                <asp:Button ID="btnLast" runat="server" CommandName="last" OnClick="NavigateToPage"
                                    Text="末页" /></td>
                            <td align="right" style="width: 100px; height: 23px">
                                转向</td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:TextBox ID="txtPage" runat="server" Width="40px"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 100px; height: 23px">
                                <asp:Button ID="btnTurn" runat="server" CommandName="go" OnClick="NavigateToPage"
                                    Text="转向" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignTaskWindow.aspx.cs" Inherits="SYS_WorkFlow_AssignTaskWindow" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>任务派工</title>
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 668px; height: 147px;
            background-color: #330000">
            <tr>
                <td style="font-size: 9pt; width: 84px; height: 27px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1" Text="任务名称"></asp:Label>
                </td>
                <td id="tdPackName" runat="server" colspan="3" style="font-size: 9pt; height: 27px;
                    background-color: #ffffff" align="left">
                </td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 84px; height: 28px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" Text="当前环节"></asp:Label></td>
                <td id="tdCurrentTacheName" runat="server" colspan="3" style="font-size: 9pt; height: 28px;
                    background-color: #ffffff" align="left">
                </td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 84px; height: 23px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" Text="可以发送的下一环节"></asp:Label></td>
                <td id="tdPackDesc" colspan="3" style="font-size: 9pt; height: 23px; background-color: #ffffff; text-align: left;">
                    <asp:RadioButtonList ID="rblTache" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblTache_SelectedIndexChanged" meta:resourcekey="rblTacheResource1">
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 84px; height: 31px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1" Text="工作负责人"></asp:Label></td>
                <td id="TD1" style="width: 35%; height: 31px; background-color: #ffffff; text-align: left;">
                    <asp:DropDownList ID="ddlFzr" runat="server" Width="167px" meta:resourcekey="ddlFzrResource1">
                    </asp:DropDownList></td>
                <td style="background-color: white; width: 80px;">
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="增加" Width="46px" meta:resourcekey="btnAddResource1" /></td>
                <td id="TD3" style="width: 35%; height: 31px; background-color: #ffffff">
                </td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 84px; height: 57px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label5" runat="server" meta:resourcekey="Label5Resource1" Text="工作人员"></asp:Label></td>
                <td id="TD2" colspan="3" style="height: 57px; background-color: #ffffff; text-align: left;">
                    &nbsp;<asp:CheckBoxList ID="chlWorkingPeople" runat="server" RepeatDirection="Horizontal" RepeatColumns="6" meta:resourcekey="chlWorkingPeopleResource1">
                    </asp:CheckBoxList></td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 84px; height: 65px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1" Text="已经选择的环节及办理人员列表"></asp:Label></td>
                <td colspan="3" style="width: 75%; height: 65px; background-color: #ffffff; text-align: center;">
                    <asp:GridView ID="grvSelectTache" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        DataKeyNames="FlowID" EmptyDataText="还没有安排任务！"
                        Font-Size="9pt" ForeColor="#333333" OnRowCommand="grvSelectTache_RowCommand"
                        PageSize="15" Width="97%" meta:resourcekey="grvSelectTacheResource1">
                        <PagerSettings Visible="False" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="FlowID" HeaderText="环节ID" meta:resourcekey="BoundFieldResource1">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="环节名称" DataField="FlowName" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField HeaderText="工作负责人" DataField="FZR" meta:resourcekey="BoundFieldResource3" >
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="工作人员" DataField="CJRY" meta:resourcekey="BoundFieldResource4" >
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:ButtonField CommandName="Del" HeaderText="删除" Text="删除" meta:resourcekey="ButtonFieldResource1">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="DodgerBlue" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 84px; height: 66px; background-color: gainsboro;
                    text-align: center">
                    <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" Text="办理意见"></asp:Label></td>
                <td colspan="3" style="width: 75%; height: 66px; background-color: #ffffff">
                    <asp:TextBox ID="txtMessage" runat="server" Height="53px" TextMode="MultiLine" Width="555px" meta:resourcekey="txtMessageResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 31px; background-color: #ffffff; text-align: center">
                    &nbsp;<asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="发送" Width="46px" meta:resourcekey="btnSendResource1" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server"  Text="取消" Width="44px" OnClientClick="window.close();" meta:resourcekey="btnCancelResource1" /></td>
            </tr>
            <tr>
                <td colspan="4" style="color: red; height: 31px; background-color: #ffffff; text-align: left" id="tdErrorMessage" runat="server">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

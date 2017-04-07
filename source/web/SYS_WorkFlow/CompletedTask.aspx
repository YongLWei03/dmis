<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompletedTask.aspx.cs" Inherits="SYS_WorkFlow_CompletedTask" EnableEventValidation="false" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>已完成的任务</title>
</head>
<body style="margin-top: 0px;  margin-left: 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 1300px">
            <tr>
                <td style="background-image: url(../img/main_bar.jpg); background-color: #006699; height: 20px; font-size: 9pt; text-align: left;" valign="middle">
                    <img src="../img/s_img.gif" />
                    <asp:Label ID="lblFuncName" runat="server" Text="已完成的任务" meta:resourcekey="lblFuncNameResource1"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px; height: 36px; text-align: left;" valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 703px; height: 50px;">
                        <tr>
                            <td style="width: 256px; height: 9px; text-align: center; font-size: 9pt;">
                                <asp:Label ID="lblStart" runat="server" Text="开始日期" meta:resourcekey="lblStartResource1"></asp:Label></td>
                            <td style="width: 162px; height: 9px; text-align: left;">
                                <cc1:WebDate ID="wdlStart" runat="server" DateTimeStyle="Date" DateStyle="DateFormat3" ButtonText=".." meta:resourcekey="wdlStartResource1" myDateWidth="80px" />
                            </td>
                            <td style="width: 247px; height: 9px; text-align: center; font-size: 9pt;">
                                <asp:Label ID="lblEnd" runat="server" Text="结束日期" meta:resourcekey="lblEndResource1"></asp:Label></td>
                            <td style="width: 168px; height: 9px; text-align: left;">
                                <cc1:WebDate ID="wdlEnd" runat="server" DateTimeStyle="Date" DateStyle="DateFormat3" ButtonText=".." meta:resourcekey="wdlEndResource1" myDateWidth="80px" />
                            </td>
                            <td style="width: 147px; height: 9px; text-align: center; font-size: 9pt;">
                                </td>
                            <td style="width: 176px; height: 9px; text-align: center">
                                <asp:Button ID="btnSearch" runat="server" EnableTheming="False" Text="查询" Width="47px" UseSubmitBehavior="False" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1" /></td>
                        </tr>
                        <tr>
                            <td style="font-size: 9pt; width: 256px; height: 30px; text-align: center">
                                <asp:Label ID="lblSTATION" runat="server" Text="变电站" meta:resourcekey="lblSTATIONResource1"></asp:Label></td>
                            <td style="width: 162px; height: 30px; text-align: left">
                                <asp:DropDownList ID="ddlSTATION" runat="server" Width="125px" meta:resourcekey="ddlSTATIONResource1">
                                </asp:DropDownList></td>
                            <td style="width: 247px; height: 30px; text-align: center; font-size: 9pt;">
                                <asp:Label ID="lblTaskDesc" runat="server" Text="任务描述" meta:resourcekey="lblTaskDescResource1"></asp:Label></td>
                            <td colspan="2" style="height: 30px; text-align: left">
                                <asp:TextBox ID="txtTaskDesc" runat="server" Width="289px" meta:resourcekey="txtTaskDescResource1"></asp:TextBox></td>
                            <td colspan="1" style="height: 30px; text-align: center">
                                            <asp:Button ID="btnExcel" runat="server" EnableTheming="False" OnClick="btnSaveExcel_Click"
                                                Text="Excel" UseSubmitBehavior="False" Width="50px" meta:resourcekey="btnExcelResource1" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 299px" align="center" valign="top">
                    <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" Font-Size="9pt" ForeColor="#333333" PageSize="15" Width="100%" OnRowCommand="grvList_RowCommand" DataKeyNames="F_NO,F_FLOWNO,F_PACKTYPENO,F_CurWorkFlowNo" EmptyDataText="无已完成的任务！" HorizontalAlign="Left" AllowSorting="True" OnRowDataBound="grvList_RowDataBound" OnSorting="grvList_Sorting" meta:resourcekey="grvListResource1">
                        <Columns>
                            <asp:ButtonField ButtonType="Button" HeaderText="流程" Text="流程" CommandName="FlowTable" meta:resourcekey="ButtonFieldResource1">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Button" CommandName="Deal" HeaderText="查看" Text="查看" meta:resourcekey="ButtonFieldResource2">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:BoundField HeaderText="当前办理环节" meta:resourcekey="BoundFieldResource1">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="主办人" meta:resourcekey="BoundFieldResource2">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务类别" DataField="F_PACKNAME" SortExpression="F_PACKNAME" meta:resourcekey="BoundFieldResource3" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_MSG" HeaderText="变电站" SortExpression="F_MSG" meta:resourcekey="BoundFieldResource4">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_DESC" HeaderText="任务描述" meta:resourcekey="BoundFieldResource5">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="环节名称" DataField="F_FLOWNAME" SortExpression="F_FLOWNAME" meta:resourcekey="BoundFieldResource6" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_receiver" HeaderText="主办人" SortExpression="f_receiver" meta:resourcekey="BoundFieldResource7">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_SENDER" HeaderText="传来人" SortExpression="F_SENDER" meta:resourcekey="BoundFieldResource8">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_receivedate" HeaderText="接单时间" SortExpression="f_receivedate" meta:resourcekey="BoundFieldResource9">
                                <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_finishdate" HeaderText="完成时间" SortExpression="f_finishdate" meta:resourcekey="BoundFieldResource10">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_planday" HeaderText="计划分钟" SortExpression="f_planday" meta:resourcekey="BoundFieldResource11">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="55px" />
                                <HeaderStyle Font-Size="8pt" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="实耗分钟" DataField="f_workday" SortExpression="f_workday" meta:resourcekey="BoundFieldResource12">
                                <ItemStyle Width="55px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Font-Size="8pt" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_CREATEMAN" HeaderText="创建人" meta:resourcekey="BoundFieldResource13">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_NO" HeaderText="业务编号" meta:resourcekey="BoundFieldResource14">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                        </Columns>
                        <PagerSettings Visible="False" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
                <td style="height: 23px;">
                    <table width="100%">
                        <tr>
                            <td id="tdMessage" runat="server" style="height: 23px; font-size: 11px; width: 302px;">
                            </td>
                            <td align="center" style="width: 56px; height: 23px">
                                <asp:Button ID="btnFirst" runat="server" CommandName="first" OnClick="NavigateToPage"
                                    Text="首页" meta:resourcekey="btnFirstResource1" /></td>
                            <td align="center" style="width: 67px; height: 23px">
                                <asp:Button ID="btnPrevious" runat="server" CommandName="prev" OnClick="NavigateToPage"
                                    Text="上一页" meta:resourcekey="btnPreviousResource1" /></td>
                            <td align="center" style="width: 68px; height: 23px">
                                <asp:Button ID="btnNext" runat="server" CommandName="next" OnClick="NavigateToPage"
                                    Text="下一页" meta:resourcekey="btnNextResource1" /></td>
                            <td align="center" style="width: 61px; height: 23px">
                                <asp:Button ID="btnLast" runat="server" CommandName="last" OnClick="NavigateToPage"
                                    Text="末页" meta:resourcekey="btnLastResource1" /></td>
                            <td align="right" style="width: 41px; height: 23px; font-size: 11px;">
                                <asp:Label ID="lblTurn" runat="server" Font-Overline="False" Font-Size="9pt" meta:resourcekey="lblTurnResource1"
                                    Text="转向"></asp:Label></td>
                            <td align="center" style="width: 44px; height: 23px">
                                <asp:TextBox ID="txtPage" runat="server" Width="40px" meta:resourcekey="txtPageResource1"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 200px; height: 23px">
                                <asp:Button ID="btnTurn" runat="server" CommandName="go" OnClick="NavigateToPage"
                                    Text="确定" meta:resourcekey="btnTurnResource1" /></td>
                        </tr>
                    </table>
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

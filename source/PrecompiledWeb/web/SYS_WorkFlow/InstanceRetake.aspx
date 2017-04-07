﻿<%@ page language="C#" autoeventwireup="true" inherits="SYS_WorkFlow_InstanceRetake, App_Web_iakvuhia" enableeventvalidation="false" culture="auto" uiculture="auto" meta:resourcekey="PageResource2" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>业务抽回</title>
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 1100px">
            <tr>
                <td style="background-image: url(../img/main_bar.jpg); background-color: #006699; height: 20px; font-size: 9pt; text-align: left;" valign="middle">
                    <img src="../img/s_img.gif" />
                    <asp:Label ID="lblFuncName" runat="server" Text="业务抽回" meta:resourcekey="lblFuncNameResource1"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px; height: 39px; text-align: left;" valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 620px">
                        <tr>
                            <td style="font-size: 9pt; width: 68px; height: 33px; text-align: center">
                                <asp:Label ID="lblSTATION" runat="server" Text="变电站" meta:resourcekey="lblSTATIONResource1"></asp:Label></td>
                            <td style="width: 31px; height: 33px; text-align: center">
                                <asp:DropDownList ID="ddlSTATION" runat="server" Width="93px" meta:resourcekey="ddlSTATIONResource1">
                                </asp:DropDownList></td>
                            <td style="width: 72px; height: 33px; text-align: center">
                                <asp:Label ID="lblStart" runat="server" Text="开始日期" meta:resourcekey="lblStartResource1"></asp:Label></td>
                            <td style="width: 100px; height: 33px; text-align: left">
                                <cc1:WebDate ID="wdlStart" runat="server" DateTimeStyle="Date" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlStartResource1" />
                            </td>
                            <td style="width: 66px; height: 33px; text-align: center">
                                <asp:Label ID="lblEnd" runat="server" Text="结束日期" meta:resourcekey="lblEndResource1"></asp:Label></td>
                            <td style="width: 119px; height: 33px; text-align: left">
                                <cc1:WebDate ID="wdlEnd" runat="server" DateTimeStyle="Date" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlEndResource1" />
                            </td>
                            <td style="width: 66px; height: 33px; text-align: center">
                                <asp:Button ID="btnSearch" runat="server" EnableTheming="False" OnClick="btnSearch_Click"
                                    Text="查询" UseSubmitBehavior="False" Width="46px" meta:resourcekey="btnSearchResource1" /></td>
                            <td style="width: 66px; height: 33px; text-align: center">
                                            <asp:Button ID="btnExcel" runat="server" EnableTheming="False" OnClick="btnSaveExcel_Click"
                                                Text="Excel" UseSubmitBehavior="False" Width="50px" meta:resourcekey="btnExcelResource1" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 299px" align="center" valign="top">
                    <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" Font-Size="9pt" ForeColor="#333333" PageSize="20" Width="100%" OnRowCommand="grvList_RowCommand" DataKeyNames="F_NO,F_FLOWNO,F_PACKTYPENO,F_CurWorkFlowNo" EmptyDataText="无任务要处理！" OnRowDataBound="grvList_RowDataBound" meta:resourcekey="grvListResource1">
                        <Columns>
                            <asp:ButtonField HeaderText="流程" Text="流程" CommandName="FlowTable" meta:resourcekey="ButtonFieldResource1">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Deal" HeaderText="查看" Text="查看" meta:resourcekey="ButtonFieldResource2">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Retake" HeaderText="抽回" Text="抽回" meta:resourcekey="ButtonFieldResource3">
                                <ItemStyle Width="35px" />
                            </asp:ButtonField>
                            <asp:BoundField HeaderText="业务类别" DataField="F_PACKNAME" meta:resourcekey="BoundFieldResource1" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_MSG" HeaderText="变电站" meta:resourcekey="BoundFieldResource2">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_CREATEMAN" HeaderText="创建人" meta:resourcekey="BoundFieldResource3">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="当前环节" DataField="F_FLOWNAME" meta:resourcekey="BoundFieldResource4" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_receiver" HeaderText="主办人" meta:resourcekey="BoundFieldResource5">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_DESC" HeaderText="任务描述" meta:resourcekey="BoundFieldResource6">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_SENDER" HeaderText="传来人" meta:resourcekey="BoundFieldResource7">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_SENDDATE" HeaderText="传来时间" meta:resourcekey="BoundFieldResource8">
                                <ItemStyle Width="80px" />
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
                            <td id="tdMessage" runat="server" style="height: 23px; font-size: 11px;">
                            </td>
                            <td align="center" style="width: 80px; height: 23px">
                                <asp:Button ID="btnFirst" runat="server" CommandName="first" OnClick="NavigateToPage"
                                    Text="首页" meta:resourcekey="btnFirstResource1" /></td>
                            <td align="center" style="width: 80px; height: 23px">
                                <asp:Button ID="btnPrevious" runat="server" CommandName="prev" OnClick="NavigateToPage"
                                    Text="上一页" meta:resourcekey="btnPreviousResource1" /></td>
                            <td align="center" style="width: 80px; height: 23px">
                                <asp:Button ID="btnNext" runat="server" CommandName="next" OnClick="NavigateToPage"
                                    Text="下一页" meta:resourcekey="btnNextResource1" /></td>
                            <td align="center" style="width: 80px; height: 23px">
                                <asp:Button ID="btnLast" runat="server" CommandName="last" OnClick="NavigateToPage"
                                    Text="末页" meta:resourcekey="btnLastResource1" /></td>
                            <td align="right" style="width: 50px; height: 23px; font-size: 11px;">
                                <asp:Label ID="lblTurn" runat="server" Font-Overline="False" Font-Size="9pt" meta:resourcekey="lblTurnResource1"
                                    Text="转向"></asp:Label></td>
                            <td align="center" style="width: 70px; height: 23px">
                                <asp:TextBox ID="txtPage" runat="server" Width="40px" meta:resourcekey="txtPageResource1"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 80px; height: 23px">
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

<%@ page language="C#" autoeventwireup="true" inherits="SYS_WorkFlow_AllTasks, App_Web_iakvuhia" enableeventvalidation="false" culture="auto" uiculture="auto" meta:resourcekey="PageResource2" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>所有任务</title>
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 1260px">
            <tr>
                <td style="background-image: url(../img/main_bar.jpg); background-color: #006699; height: 20px; font-size: 9pt; text-align: left;" valign="middle">
                    <img src="../img/s_img.gif" /><asp:Label ID="lblFuncName" runat="server" Text="所有任务" meta:resourcekey="lblFuncNameResource1"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px; height: 39px; text-align: left;" valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 674px; height: 50px">
                        <tr>
                            <td style="font-size: 9pt; width: 164px; height: 15px; text-align: center">
                                <asp:Label ID="lblStart" runat="server" Text="开始日期" meta:resourcekey="lblStartResource1"></asp:Label></td>
                            <td style="width: 124px; height: 15px; text-align: left">
                                <cc1:WebDate ID="wdlStart" runat="server" DateTimeStyle="Date" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlStartResource1" />
                            </td>
                            <td style="font-size: 9pt; width: 185px; height: 15px; text-align: center">
                                <asp:Label ID="lblEnd" runat="server" Text="结束日期" meta:resourcekey="lblEndResource1"></asp:Label></td>
                            <td style="width: 103px; height: 15px; text-align: left">
                                <cc1:WebDate ID="wdlEnd" runat="server" DateTimeStyle="Date" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlEndResource1" />
                            </td>
                            <td style="font-size: 9pt; width: 79px; height: 15px; text-align: center" colspan="2">
                                <asp:Label ID="lblStatus" runat="server" Text="状态" meta:resourcekey="lblStatusResource1"></asp:Label></td>
                            <td colspan="2" style="height: 15px; text-align: left">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="69px" meta:resourcekey="ddlStatusResource1">
                                </asp:DropDownList></td>
                            <td colspan="1" style="height: 15px; text-align: left">
                                <asp:Button ID="btnSearch" runat="server" EnableTheming="False" OnClick="btnSearch_Click"
                                    Text="查询" UseSubmitBehavior="False" Width="70px" meta:resourcekey="btnSearchResource1" /></td>
                        </tr>
                        <tr>
                            <td style="font-size: 9pt; width: 164px; height: 27px; text-align: center">
                                <asp:Label ID="lblSTATION" runat="server" Text="变电站" Width="36px" meta:resourcekey="lblSTATIONResource1"></asp:Label></td>
                            <td style="width: 124px; height: 27px; text-align: left">
                                <asp:DropDownList ID="ddlSTATION" runat="server" Width="102px" meta:resourcekey="ddlSTATIONResource1">
                                </asp:DropDownList></td>
                            <td style="font-size: 9pt; width: 185px; height: 27px; text-align: center">
                                <asp:Label ID="lblTaskDesc" runat="server" Text="任务描述" meta:resourcekey="lblTaskDescResource1"></asp:Label></td>
                            <td colspan="5" style="height: 27px; text-align: left">
                                <asp:TextBox ID="txtTaskDesc" runat="server" Width="331px" meta:resourcekey="txtTaskDescResource1"></asp:TextBox></td>
                            <td colspan="1" style="height: 27px; text-align: left">
                                            <asp:Button ID="btnExcel" runat="server" EnableTheming="False" OnClick="btnSaveExcel_Click"
                                                Text="Excel" UseSubmitBehavior="False" Width="70px" meta:resourcekey="btnExcelResource1" /></td>
                        </tr>
                    </table>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 299px" align="center" valign="top">
                    <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyNames="F_NO,F_PACKTYPENO" EmptyDataText="无数据！" Font-Size="9pt"
                        ForeColor="#333333" OnRowCommand="grvList_RowCommand" OnRowDataBound="grvList_RowDataBound"
                        PageSize="15" Width="100%" AllowSorting="True" OnSorting="grvList_Sorting" meta:resourcekey="grvListResource1">
                        <PagerSettings Visible="False" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="FlowTable" HeaderText="流程" Text="流程" meta:resourcekey="ButtonFieldResource1">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Button" CommandName="Deal" HeaderText="查看" Text="查看" meta:resourcekey="ButtonFieldResource2">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="F_PACKNAME" HeaderText="业务类别" SortExpression="F_PACKNAME" meta:resourcekey="BoundFieldResource1">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_msg" HeaderText="变电站" SortExpression="f_msg" meta:resourcekey="BoundFieldResource2">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_CREATEMAN" HeaderText="创建人" SortExpression="F_CREATEMAN" meta:resourcekey="BoundFieldResource3">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_DESC" HeaderText="任务描述" meta:resourcekey="BoundFieldResource4">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_status" HeaderText="状态" meta:resourcekey="BoundFieldResource5">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="当前环节" meta:resourcekey="BoundFieldResource6">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="主办人" meta:resourcekey="BoundFieldResource7">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_createdate" HeaderText="创建时间" SortExpression="f_createdate" meta:resourcekey="BoundFieldResource8">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_archivedate" HeaderText="结案时间" SortExpression="f_archivedate" meta:resourcekey="BoundFieldResource9">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PLAN_STARTTIME" HeaderText="计划开始时间" SortExpression="PLAN_STARTTIME" meta:resourcekey="BoundFieldResource10">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PLAN_ENDTIME" HeaderText="计划结束时间" SortExpression="PLAN_ENDTIME" meta:resourcekey="BoundFieldResource11">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_no" HeaderText="业务编号" meta:resourcekey="BoundFieldResource12">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="55px" />
                            </asp:BoundField>
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
                <td style="height: 30px;">
                    <table width="100%">
                        <tr>
                            <td id="tdMessage" runat="server" style="height: 23px; font-size: 11px; text-align: left;">
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

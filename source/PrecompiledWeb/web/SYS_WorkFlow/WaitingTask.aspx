<%@ page language="C#" autoeventwireup="true" inherits="SYS_WorkFlow_WaitingTask, App_Web_iakvuhia" enableeventvalidation="false" culture="auto" uiculture="auto" meta:resourcekey="PageResource2" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>待办任务</title>
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 1300px">
            <tr>
                <td style="background-image: url(../img/main_bar.jpg); background-color: #006699; height: 20px; font-size: 9pt; text-align: left;" valign="middle">
                    <img src="../img/s_img.gif" />
                    <asp:Label ID="lblFuncName" runat="server" Text="待办任务" meta:resourcekey="lblFuncNameResource1"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px; height: 30px; text-align: left;" valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 726px">
                        <tr>
                            <td style="width: 116px; height: 38px; text-align: center">
                                <asp:Label ID="Label1" runat="server" Text="变电站" meta:resourcekey="Label1Resource1"></asp:Label></td>
                            <td style="width: 164px; height: 38px; text-align: left">
                                <asp:DropDownList ID="ddlSTATION" runat="server" Width="118px" meta:resourcekey="ddlSTATIONResource1">
                                </asp:DropDownList></td>
                            <td style="width: 233px; height: 38px; text-align: center">
                                <asp:Label ID="Label2" runat="server" Text="开始日期" meta:resourcekey="Label2Resource1"></asp:Label></td>
                            <td style="width: 175px; height: 38px; text-align: left">
                                <cc1:WebDate ID="wdlStart" runat="server" DateTimeStyle="Date" ButtonText=".." DateStyle="DateFormat3" meta:resourcekey="wdlStartResource1" myDateWidth="80px" />
                            </td>
                            <td style="width: 151px; height: 38px; text-align: center">
                                <asp:Label ID="Label3" runat="server" Text="结束日期" meta:resourcekey="Label3Resource1"></asp:Label></td>
                            <td style="width: 152px; height: 38px; text-align: left">
                                <cc1:WebDate ID="wdlEnd" runat="server" DateTimeStyle="Date" ButtonText=".." DateStyle="DateFormat3" meta:resourcekey="wdlEndResource1" myDateWidth="80px" />
                            </td>
                            <td style="width: 60px; height: 38px; text-align: center">
                                <asp:Button ID="Button1" runat="server" EnableTheming="False" OnClick="btnSearch_Click"
                                    Text="查询" UseSubmitBehavior="False" Width="47px" meta:resourcekey="Button1Resource1" /></td>
                            <td style="width: 82px; height: 38px; text-align: center">
                                            <asp:Button ID="btnExcel" runat="server" EnableTheming="False" OnClick="btnSaveExcel_Click"
                                                Text="Excel" UseSubmitBehavior="False" Width="47px" meta:resourcekey="btnExcelResource1" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 299px" align="center" valign="top">
                    <asp:GridView ID="grvList" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" Font-Size="9pt" ForeColor="#333333" PageSize="15" Width="1300px" OnRowCommand="grvList_RowCommand" DataKeyNames="F_NO,F_FLOWNO,F_PACKTYPENO,F_CurWorkFlowNo" EmptyDataText="无任务要处理！" OnRowDataBound="grvList_RowDataBound" HorizontalAlign="Left" AllowSorting="True" OnSorting="grvList_Sorting" meta:resourcekey="grvListResource1">
                        <Columns>
                            <asp:ButtonField HeaderText="流程" Text="流程" CommandName="FlowTable" meta:resourcekey="ButtonFieldResource1">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Deal" HeaderText="查看" Text="查看" meta:resourcekey="ButtonFieldResource2">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Accept" HeaderText="接单" Text="接单" meta:resourcekey="ButtonFieldResource3">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:BoundField HeaderText="业务类别" DataField="F_PACKNAME" SortExpression="F_PACKNAME" meta:resourcekey="BoundFieldResource1" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_MSG" HeaderText="厂站" SortExpression="F_MSG" meta:resourcekey="BoundFieldResource2">
                                <ItemStyle VerticalAlign="Middle" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_CREATEMAN" HeaderText="创建人" SortExpression="F_CREATEMAN" meta:resourcekey="BoundFieldResource3">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="当前环节" DataField="F_FLOWNAME" SortExpression="F_FLOWNAME" meta:resourcekey="BoundFieldResource4" >
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
                            <asp:BoundField DataField="F_SENDDATE" HeaderText="传来时间" SortExpression="F_SENDDATE" meta:resourcekey="BoundFieldResource8">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="最晚接单时间" DataField="F_LAST_INCEPT_TIME" SortExpression="F_LAST_INCEPT_TIME" meta:resourcekey="BoundFieldResource9">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                           <asp:BoundField HeaderText="还剩时间" meta:resourcekey="BoundFieldResource10">
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                           </asp:BoundField>
                           <asp:BoundField DataField="PLAN_STARTTIME" HeaderText="计划开始时间" SortExpression="PLAN_STARTTIME" meta:resourcekey="BoundFieldResource11">
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                           </asp:BoundField>
                           <asp:BoundField DataField="PLAN_ENDTIME" HeaderText="计划结束时间" SortExpression="PLAN_ENDTIME" meta:resourcekey="BoundFieldResource12">
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
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
                                <asp:TextBox ID="txtPage" runat="server" Width="40px" meta:resourcekey="txtPageResource1"></asp:TextBox></td>
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

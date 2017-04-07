<%@ page language="C#" autoeventwireup="true" inherits="SYS_WorkFlow_InstanceOvertimeAcceptQuery, App_Web_iakvuhia" enableeventvalidation="false" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>业务接单超时查询</title>
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px">
            <tr>
                <td style="background-image: url(../img/main_bar.jpg); background-color: #006699; height: 20px; font-size: 9pt;" valign="middle">
                    <img src="../img/s_img.gif" />
                    环节接单超时查询</td>
            </tr>
            <tr>
                <td style="width: 100px; height: 39px" valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 696px">
                        <tr>
                            <td style="font-size: 11px; width: 76px; height: 30px; text-align: center">
                                业务类别</td>
                            <td colspan="5" style="height: 30px">
                                <asp:DataList ID="dlsPackType" runat="server" Font-Size="9pt" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="95%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckbPackType" runat="server" Text='<%#Eval("F_NAME") %>' ToolTip='<%#Eval("F_NO") %>' />
                                    </ItemTemplate>
                                </asp:DataList></td>
                            <td style="width: 71px; height: 30px; text-align: center">
                                <asp:Button ID="btnSearch" runat="server" EnableTheming="False" Text="查 询" Width="59px" UseSubmitBehavior="False" OnClick="btnSearch_Click" /></td>
                        </tr>
                        <tr>
                            <td style="width: 76px; height: 21px; text-align: center; font-size: 11px;">
                                开始日期</td>
                            <td style="width: 100px; height: 21px;">
                                <cc1:WebDate ID="wdlStart" runat="server" DateTimeStyle="Date" />
                            </td>
                            <td style="width: 81px; height: 21px; text-align: center; font-size: 9pt;">
                                终止日期</td>
                            <td style="width: 100px; height: 21px; text-align: center;">
                                <cc1:WebDate ID="wdlEnd" runat="server" DateTimeStyle="Date" />
                            </td>
                            <td style="width: 94px; height: 21px; text-align: center; font-size: 9pt;">
                                任务状态</td>
                            <td style="width: 90px; height: 21px; text-align: center">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="78px">
                                    <asp:ListItem>全部</asp:ListItem>
                                    <asp:ListItem>在办</asp:ListItem>
                                    <asp:ListItem>完成</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="width: 71px; height: 21px; text-align: center">
                                <asp:Button ID="btnExcel" runat="server" EnableTheming="False" Text="Excel" Width="59px" UseSubmitBehavior="False" OnClick="btnSaveExcel_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 299px" align="center" valign="top">
                    <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" Font-Size="9pt" ForeColor="#333333" PageSize="15" Width="1000px" OnRowCommand="grvList_RowCommand" DataKeyNames="F_NO,F_FLOWNO,F_PACKTYPENO,F_CurWorkFlowNo" EmptyDataText="无办理超时任务！" HorizontalAlign="Left">
                        <Columns>
                            <asp:ButtonField HeaderText="流程" Text="流程" CommandName="FlowTable">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Deal" HeaderText="详细" Text="详细">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:BoundField HeaderText="业务类别" DataField="F_PACKNAME" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_DESC" HeaderText="任务描述">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_MSG" HeaderText="厂站">
                                <ItemStyle VerticalAlign="Middle" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="环节名称" DataField="F_FLOWNAME" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_receiver" HeaderText="主办人">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_receivedate" HeaderText="实际接单时间">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="标准接单时间" DataField="f_last_incept_time">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_incept_hours" HeaderText="标准小时数">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_STATUS" HeaderText="状态">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
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
                                    Text="首页" /></td>
                            <td align="center" style="width: 80px; height: 23px">
                                <asp:Button ID="btnPrevious" runat="server" CommandName="prev" OnClick="NavigateToPage"
                                    Text="上一页" /></td>
                            <td align="center" style="width: 80px; height: 23px">
                                <asp:Button ID="btnNext" runat="server" CommandName="next" OnClick="NavigateToPage"
                                    Text="下一页" /></td>
                            <td align="center" style="width: 80px; height: 23px">
                                <asp:Button ID="btnLast" runat="server" CommandName="last" OnClick="NavigateToPage"
                                    Text="末页" /></td>
                            <td align="right" style="width: 50px; height: 23px; font-size: 11px;">
                                转向</td>
                            <td align="center" style="width: 70px; height: 23px">
                                <asp:TextBox ID="txtPage" runat="server" Width="40px"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 80px; height: 23px">
                                <asp:Button ID="btnTurn" runat="server" CommandName="go" OnClick="NavigateToPage"
                                    Text="确定" /></td>
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

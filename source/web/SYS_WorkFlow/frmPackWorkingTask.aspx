<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPackWorkingTask.aspx.cs" Inherits="SYS_WorkFlow_frmPackWorkingTask" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 1400px">
            <tr>
                <td style="background-image: url(../img/main_bar.jpg); background-color: #006699; height: 20px; font-size: 9pt;" valign="middle">
                    <img src="../img/s_img.gif" />
                    在办任务</td>
            </tr>
            <tr>
                <td style="width: 100px; height: 39px" valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 687px">
                        <tr>
                            <td style="width: 44px; height: 33px; text-align: center; font-size: 9pt;">
                                厂站</td>
                            <td style="width: 31px; height: 33px; text-align: center">
                                <asp:DropDownList ID="ddlSTATION" runat="server" Width="93px">
                                </asp:DropDownList></td>
                            <td style="width: 35px; height: 33px; text-align: center; font-size: 9pt;">
                                开始日期</td>
                            <td style="width: 100px; height: 33px; text-align: center;">
                                <cc1:WebDate ID="wdlStart" runat="server" DateTimeStyle="Date" />
                            </td>
                            <td style="width: 32px; height: 33px; text-align: center; font-size: 9pt;">
                                结束日期</td>
                            <td style="font-size: 9pt; width: 66px; height: 33px; text-align: center">
                                <cc1:WebDate ID="wdlEnd" runat="server" DateTimeStyle="Date" />
                            </td>
                            <td style="font-size: 9pt; width: 47px; height: 33px; text-align: center">
                                主办人</td>
                            <td style="font-size: 9pt; width: 66px; height: 33px; text-align: center">
                                <cc1:HtmlComboBox ID="hcbMember" runat="server" Width="75px">
                                </cc1:HtmlComboBox></td>
                            <td style="font-size: 9pt; width: 66px; height: 33px; text-align: center">
                                <asp:Button ID="btnSearch" runat="server" EnableTheming="False" Text="查询" Width="46px" UseSubmitBehavior="False" OnClick="btnSearch_Click" /></td>
                            <td style="font-size: 9pt; width: 66px; height: 33px; text-align: center">
                                            <asp:Button ID="btnExcel" runat="server" EnableTheming="False" OnClick="btnSaveExcel_Click"
                                                Text="Excel" UseSubmitBehavior="False" Width="49px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 299px" align="center" valign="top">
                    <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" Font-Size="9pt" ForeColor="#333333" PageSize="15" Width="100%" OnRowCommand="grvList_RowCommand" DataKeyNames="F_NO,F_FLOWNO,F_PACKTYPENO,F_CurWorkFlowNo" EmptyDataText="无任务要处理！" OnRowDataBound="grvList_RowDataBound" AllowSorting="True" OnSorting="grvList_Sorting">
                        <Columns>
                            <asp:ButtonField ButtonType="Button" HeaderText="流程" Text="流程" CommandName="FlowTable">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Button" CommandName="Deal" HeaderText="查看" Text="查看">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:BoundField HeaderText="业务类别" DataField="F_PACKNAME" SortExpression="F_PACKNAME" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_MSG" HeaderText="厂站" SortExpression="F_MSG">
                                <ItemStyle VerticalAlign="Middle" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_CREATEMAN" HeaderText="创建人" SortExpression="F_CREATEMAN">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="当前环节" DataField="F_FLOWNAME" SortExpression="F_FLOWNAME" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_receiver" HeaderText="主办人" SortExpression="f_receiver">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_DESC" HeaderText="任务描述">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_SENDER" HeaderText="传来人" SortExpression="F_SENDER">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_SENDDATE" HeaderText="传来时间" SortExpression="F_SENDDATE">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="接单时间" DataField="f_receivedate" SortExpression="f_receivedate">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_LAST_FINISHED_TIME" HeaderText="最晚完成时间" SortExpression="F_LAST_FINISHED_TIME">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                            </asp:BoundField>
                           <asp:BoundField HeaderText="还剩时间">
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                           </asp:BoundField>
                           <asp:BoundField DataField="PLAN_STARTTIME" HeaderText="计划开始时间" SortExpression="PLAN_STARTTIME">
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                           </asp:BoundField>
                           <asp:BoundField DataField="PLAN_ENDTIME" HeaderText="计划结束时间" SortExpression="PLAN_ENDTIME">
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                           </asp:BoundField>                             
                            <asp:BoundField DataField="F_NO" HeaderText="业务号">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
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
                    <table width="80%">
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

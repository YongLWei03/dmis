<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskWorkingTimesQuery.aspx.cs" Inherits="SYS_WorkFlow_TaskWorkingTimesQuery" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>实际工作时间查询</title>
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 900px">
            <tr>
                <td style="background-image: url(../img/main_bar.jpg); background-color: #006699; height: 20px; font-size: 9pt;" valign="middle">
                    <img src="../img/s_img.gif" />
                    实际工作时间查询</td>
            </tr>
            <tr>
                <td style="width: 100px; height: 39px" valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 730px">
                        <tr>
                            <td style="width: 192px; height: 22px; text-align: center; font-size: 11px;">
                                办理人员</td>
                            <td style="width: 100px; height: 22px;">
                                <asp:DropDownList ID="ddlMember" runat="server" Width="87px">
                                </asp:DropDownList></td>
                            <td style="width: 168px; height: 22px; text-align: center; font-size: 11px;">
                                开始日期</td>
                            <td style="width: 100px; height: 22px;">
                                <cc1:WebDate ID="wdlStart" runat="server" DateTimeStyle="Date" />
                            </td>
                            <td align="center" style="width: 163px; height: 22px">
                                终止日期</td>
                            <td style="width: 100px; height: 22px">
                                <cc1:WebDate ID="wdlEnd" runat="server" DateTimeStyle="Date" />
                            </td>
                            <td style="width: 100px; height: 22px; text-align: center;">
                                <asp:Button ID="btnSearch" runat="server" EnableTheming="False" Text="查  询" Width="59px" UseSubmitBehavior="False" OnClick="btnSearch_Click" /></td>
                            <td style="width: 100px; height: 22px; text-align: center;">
                                <asp:Button ID="btnExcel" runat="server" EnableTheming="False" OnClick="btnSaveExcel_Click"
                                    Text="Excel" UseSubmitBehavior="False" Width="56px" /></td>
                            <td style="width: 100px; height: 22px; text-align: center">
                                <asp:Button ID="btnSort" runat="server" OnClick="btnSort_Click" Text="排  序" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 299px" align="center" valign="top">
                    <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" Font-Size="9pt" ForeColor="#333333" PageSize="15" Width="100%" OnRowCommand="grvList_RowCommand" DataKeyNames="TID,F_PACKTYPENO,F_PACKNO,F_FLOWNO" EmptyDataText="无数据！" OnRowDataBound="grvList_RowDataBound" AllowSorting="True" OnSorting="grvList_Sorting">
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="Deal" HeaderText="查看" Text="查看">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:BoundField HeaderText="业务名称" DataField="F_PACKTYPENAME" SortExpression="F_PACKTYPENAME" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="station" HeaderText="变电站" SortExpression="station">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="任务描述" DataField="F_PACK_DESC" >
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="办理人" DataField="F_MEMEBER_NAME" SortExpression="F_MEMEBER_NAME" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                            </asp:BoundField>
                           <asp:BoundField HeaderText="开始工作时间" DataField="STARTTIME" DataFormatString="{0:dd-MM-yyyy HH:mm}" HtmlEncode="False" SortExpression="STARTTIME" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>
                           <asp:BoundField HeaderText="结束工作时间" DataField="ENDTIME" DataFormatString="{0:dd-MM-yyyy HH:mm}" HtmlEncode="False" SortExpression="ENDTIME" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="HOURS" HeaderText="消耗小时" SortExpression="HOURS">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="is_to_station" HeaderText="是否下站">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
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
                                    Text="确定" />

                </td>
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

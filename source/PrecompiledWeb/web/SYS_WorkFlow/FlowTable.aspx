<%@ page language="C#" autoeventwireup="true" inherits="SYS_WorkFlow_FlowTable, App_Web_iakvuhia" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>流程表</title>
</head>
<body  style="margin-top: 0px; margin-left: 0px;text-align:center">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 95%; height: 101px">
            <tr valign="middle">
                <td style="height: 24px; background-image: url(../img/main_bar.jpg); background-color: #006699;  vertical-align: middle; text-align: left;">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 24px; text-align: center">
                                    <img src="../img/s_img.gif" /></td>
                            <td style="width: 100px; font-size: 9pt;">
                            流程表</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 43px; text-align: center;">
                    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; background-color: #000000">
                        <tr>
                            <td style="width: 20%; height: 24px; background-color: #99ff33; text-align: center; font-size: 9pt;">
                                &nbsp;<asp:Label ID="Label1" runat="server" Text="业务类别：" Width="84px" meta:resourcekey="Label1Resource1"></asp:Label></td>
                            <td style="width: 80%; height: 24px; background-color: #ffffff; text-align: left; font-size: 9pt;" id="tdPackTypeName" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 24px; background-color: #66ff33;  width: 20%; text-align: center; font-size: 9pt;">
                                &nbsp;<asp:Label ID="Label2" runat="server" Text="任务描述：" Width="86px" meta:resourcekey="Label2Resource1"></asp:Label></td>
                            <td style="width: 80%; height: 24px; background-color: #ffffff; text-align: left; font-size: 9pt;" id="tdPackDesc" runat="server">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 24px; text-align: center; ">
                    <table width="100%">
                        <tr>
                            <td style="width: 30%; text-align: left; font-size: 9pt;">
                                <asp:Label ID="Label3" runat="server" Text="办理列表：" Width="90px" meta:resourcekey="Label3Resource1"></asp:Label></td>
                            <td style="width: 70%; text-align: right;">
                                &nbsp;&nbsp;
                                <asp:Button ID="btnFlowChart" runat="server" Text="流程图" Width="62px" OnClick="btnFlowChart_Click" meta:resourcekey="btnFlowChartResource1" />
                                <asp:Button ID="btnReturn" runat="server" Text="返回" Width="62px" OnClick="btnReturn_Click" meta:resourcekey="btnReturnResource1" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 153px" align="center" valign="top">
                    <asp:GridView ID="grvList" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" Font-Size="9pt" ForeColor="#333333" PageSize="15" Width="100%" OnRowDataBound="grvList_RowDataBound" DataKeyNames="F_PACKNO,F_NO" meta:resourcekey="grvListResource1">
                        <PagerSettings Visible="False" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField HeaderText="序号" meta:resourcekey="BoundFieldResource1">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_SENDDATE" HeaderText="传来时间" meta:resourcekey="BoundFieldResource2">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_SENDER" HeaderText="传来人员" meta:resourcekey="BoundFieldResource3">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_FLOWNAME" HeaderText="环节名称" meta:resourcekey="BoundFieldResource4">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="办理意见" DataField="F_MSG" meta:resourcekey="BoundFieldResource5">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="办理人员" DataField="F_RECEIVER" meta:resourcekey="BoundFieldResource6">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="办理时间" DataField="F_RECEIVEDATE" meta:resourcekey="BoundFieldResource7">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_FINISHDATE" HeaderText="完成时间" meta:resourcekey="BoundFieldResource8">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_planday" HeaderText="计划分钟" meta:resourcekey="BoundFieldResource9">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_workday" HeaderText="实耗分钟" meta:resourcekey="BoundFieldResource10">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_STATUS" HeaderText="状态" meta:resourcekey="BoundFieldResource11">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_NO" HeaderText="任务号" meta:resourcekey="BoundFieldResource12">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
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
                <td style="height: 22px;">
                </td>
            </tr>
            <tr>
                <td style="height: 24px; ">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

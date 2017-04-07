<%@ page language="C#" autoeventwireup="true" inherits="SYS_WorkFlow_CurrentTask, App_Web_iakvuhia" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>当前任务（包含在办和待办的任务）</title>
    <script language="javascript" type="text/javascript" >
        
        function addPack()
        {
            var win=window.open("SelectTemplate.aspx","SelectTemplate","toolbar=no,menubar=no,directories=no,status=no,left=100,top=10,width=250,height=400");
        }
    </script>
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="background-image: url(../img/main_bar.jpg); background-color: #006699; height: 20px; font-size: 9pt; text-align: left;" valign="middle">
                    <img src="../img/s_img.gif" />
                    当前任务</td>
            </tr>
            <tr>
                <td style="width: 100px; height: 39px; text-align: left;" valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 672px">
                        <tr>
                            <td style="width: 246px; height: 22px; text-align: center; font-size: 11px;">
                                业务类别</td>
                            <td style="width: 100px; height: 22px;">
                                <asp:DropDownList ID="ddlPackType" runat="server" Width="110px">
                                </asp:DropDownList></td>
                            <td style="width: 221px; height: 22px; text-align: center; font-size: 11px;">
                                任务描述</td>
                            <td style="width: 100px; height: 22px;">
                                <asp:TextBox ID="txtTaskDesc" runat="server" Width="180px"></asp:TextBox></td>
                            <td style="width: 100px; height: 22px; text-align: center;">
                                <asp:Button ID="btnSearch" runat="server" EnableTheming="False" Text="查  询" Width="59px" UseSubmitBehavior="False" OnClick="btnSearch_Click" /></td>
                            <td style="width: 100px; height: 22px; text-align: center;"><asp:Button ID="btnRefresh" runat="server" EnableTheming="False" Text=" 刷 新 " Width="59px" UseSubmitBehavior="False" OnClick="btnRefresh_Click" /></td>
                            <td style="width: 116px; height: 22px; text-align: center">
                                <asp:Button ID="btnAdd" runat="server" EnableTheming="False" Text=" 新 建 " Width="59px" UseSubmitBehavior="False" ToolTip="新建业务" OnClick="btnAdd_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 299px" align="center" valign="top">
                    <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" Font-Size="9pt" ForeColor="#333333" PageSize="15" Width="100%" OnRowCommand="grvList_RowCommand" DataKeyNames="F_NO,F_FLOWNO,F_PACKTYPENO,F_CurWorkFlowNo" EmptyDataText="无任务要处理！" OnRowDataBound="grvList_RowDataBound">
                        <Columns>
                            <asp:ButtonField ButtonType="Button" HeaderText="流程" Text="流程" CommandName="FlowTable">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Button" CommandName="Deal" HeaderText="办理" Text="办理">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                            </asp:ButtonField>
                            <asp:BoundField HeaderText="业务类别" DataField="F_PACKNAME" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_MSG" HeaderText="厂站">
                                <ItemStyle VerticalAlign="Middle" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_CREATEMAN" HeaderText="创建人">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="当前环节" DataField="F_FLOWNAME" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="f_receiver" HeaderText="主办人">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_DESC" HeaderText="任务描述">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_SENDER" HeaderText="传来人">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_SENDDATE" HeaderText="传来时间">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="接单时间" DataField="f_receivedate">
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
                            <td id="tdMessage" runat="server" style="height: 23px; font-size: 11px; text-align: left;">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCycleTaskPara.aspx.cs" Inherits="SYS_WorkFlow_frmCycleTaskPara" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table width="100%" align="center" cellpadding="0" cellspacing="0">
        <tr height="30px">
        <td align="right" style="width: 100%; height: 30px;">
            <table width="100%">
                <tr>
                    <td style="width: 262px; height: 21px">
                    </td>
                    <td style="width: 134px; height: 21px; text-align: center;"></td>
                    <td style="width: 466px; height: 21px" align="center">
            
            </td>
                    <td style="width: 100px; height: 21px" align="center"></td>
                    <td style="width: 100px; height: 21px" align="center">
            <asp:Button id="btnAdd" Text="添加" runat="server" OnClick="btnAdd_Click" Enabled="False"  /></td>
                    <td style="width: 100px; height: 21px" align="center">
            <asp:Button id="btnDelete" Text="删除" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm('确定要删除吗？');" Enabled="False"/></td>
                    <td style="width: 100px; height: 21px" align="center">
            <asp:Button ID="btnModify" Text="修改" runat="server" OnClick="btnModify_Click" Enabled="False" /></td>
                    <td align="center" style="width: 100px; height: 21px">
            </td>
                    <td style="width: 100px; height: 21px" align="center">
                        </td>
                </tr>
            </table>
        </td>
        </tr>
        <tr>
        <td align="center" class="captiontd" id="tdFileType" runat="server" style="width: 100%; height: 25px;">
            周期性任务参数表</td>
        </tr>
        <tr>
        <td align="left" style="width: 100%; height: 400px;" valign="top">
        <asp:GridView CssClass="font" ID="grvList" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="TID"  PageSize="20" AllowPaging="True" CellPadding="4" EmptyDataText="没有满足条件的记录！" OnSelectedIndexChanged="grvRef_SelectedIndexChanged" OnRowDataBound="grvList_RowDataBound"  >
            <Columns>
               <asp:CommandField ShowSelectButton="True" HeaderText="选择" SelectText="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;" >
                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
               </asp:CommandField>
               <asp:BoundField DataField="任务名称" HeaderText="任务名称" >
                   <ItemStyle HorizontalAlign="Left" />
               </asp:BoundField>
               <asp:BoundField DataField="对应业务ID" HeaderText="业务名称" >
                   <ItemStyle Width="120px" />
               </asp:BoundField>
                <asp:BoundField DataField="周期类型" HeaderText="周期类型" >
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="发生时候" HeaderText="发生时候">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="任务启动人" HeaderText="任务启动人">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
            </Columns>
            <PagerSettings  Visible="False" />
        </asp:GridView>
        </td>
        </tr>
        <tr>
            <td class="bottomtd" style="height: 12px; width: 100%;" >
                    <table width="100%">
                        <tr>
                            <td id="tdMessage" runat="server" style="width: 645px; height: 23px"></td>
                            <td style="width: 100px; height: 23px" align="center">
                                <asp:Button id="btnFirst" Text="首页" runat="server" CommandName="first" OnClick="NavigateToPage" /></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button id="btnPrevious" Text="上一页" runat="server" CommandName="prev" OnClick="NavigateToPage"/></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnNext" Text="下一页" runat="server" CommandName="next" OnClick="NavigateToPage" /></td>
                            <td align="center" style="width: 71px; height: 23px">
                                <asp:Button ID="btnLast" Text="末页" runat="server" CommandName="last" OnClick="NavigateToPage" /></td>
                            <td style="width: 100px; height: 23px" align="right">
                                        转向</td>
                            <td style="width: 100px; height: 23px" align="center">
                                    <asp:TextBox ID="txtPage" runat="server" Width="40px" />
                                    </td>
                            <td style="width: 100px; height: 23px" align="left">
                                    <asp:Button ID="btnTurn" Text="确定" runat="server" CommandName="go" OnClick="NavigateToPage" /></td>
                             </tr>
                     </table>
            </td>
            </tr>
        </table>     
        <input id="refreshPage" runat="server" onpropertychange="javascript:form1.submit();"
            size="0" style="visibility: hidden; width: 68px" type="text" value="0" />
    </form>
</body>
</html>

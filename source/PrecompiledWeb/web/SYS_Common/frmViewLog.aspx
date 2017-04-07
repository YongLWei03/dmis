<%@ page language="C#" autoeventwireup="true" inherits="yw_ztgl_frmViewLog, App_Web_og9prjkz" stylesheettheme="default" %>

<%@ Register Src="../uSelectMonth.ascx" TagName="uSelectMonth" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>日志查询</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table width="776px" align="center" cellpadding="0" cellspacing="0">
        <tr height="30px">
        <td align="right" style="width: 726px; height: 30px;">
            <table width="100%">
                <tr>
                    <td style="width: 262px; height: 21px">
                        <uc1:uSelectMonth ID="uwcMonth" runat="server" />
                    </td>
                    <td style="width: 134px; height: 21px; text-align: center;"><asp:Button id="btnQuery" Text="检索" runat="server" OnClick="btnQuery_Click"  /></td>
                    <td style="width: 466px; height: 21px" align="center">
            
            </td>
                    <td style="width: 100px; height: 21px" align="center">
            <asp:Button ID="btnSearch" Text="查询" runat="server" OnClick="btnSearch_Click" Visible="False" /></td>
                    <td style="width: 100px; height: 21px" align="center">
                        <asp:Button ID="btnSort" Text="排序" runat="server" OnClick="btnSort_Click" Visible="False" /></td>
                    <td style="width: 100px; height: 21px" align="center">
            <asp:Button ID="btnPrint" Text="打印" runat="server" OnClick="btnPrint_Click" Visible="False" /></td>
                    <td style="width: 100px; height: 21px" align="center">
            </td>
                    <td align="center" style="width: 100px; height: 21px">
                        </td>
                    <td style="width: 100px; height: 21px" align="center">
            </td>
                </tr>
            </table>
        </td>
        </tr>
        <tr>
        <td align="center" class="captiontd" width="776px" height="25px">
            &nbsp;<asp:Label ID="Label1" runat="server" Font-Size="14pt" Text="日志查询"></asp:Label></td>
        </tr>
        <tr>
        <td align="left" style="width: 100%; height: 400px;" valign="top">
        <asp:GridView CssClass="font" ID="grvList" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="TID"  PageSize="20" AllowPaging="True" CellPadding="4" EmptyDataText="没有满足条件的记录！" AllowSorting="True" OnSorting="grvList_Sorting" >
            <Columns>
                <asp:BoundField DataField="OPT_TIME" HeaderText="发生时间" DataFormatString="{0:dd-MM-yyyy HH:mm:sss}" HtmlEncode="False" SortExpression="OPT_TIME" >
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="MEMBER_NAME" HeaderText="操作人" SortExpression="MEMBER_NAME" >
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
               <asp:BoundField DataField="MEMBER_ID" HeaderText="操作人ID" SortExpression="MEMBER_ID" />
               <asp:BoundField DataField="IP" HeaderText="IP地址" SortExpression="IP" >
                   <ItemStyle Wrap="False" />
               </asp:BoundField>
               <asp:BoundField DataField="LOG_TYPE" HeaderText="类别" SortExpression="LOG_TYPE" >
                   <ItemStyle Wrap="False" />
               </asp:BoundField>
               <asp:BoundField DataField="STATE" HeaderText="状态" SortExpression="OPT_TIME" >
                   <ItemStyle Wrap="False" />
               </asp:BoundField>
               <asp:BoundField DataField="CONTENT" HeaderText="日志内容" />
            </Columns>
            <PagerSettings  Visible="False" />
        </asp:GridView>
        </td>
        </tr>
        <tr>
            <td class="bottomtd" style="height: 12px" >
                    <table width="100%">
                        <tr>
                            <td id="tdMessage" runat="server" style="width: 645px; height: 23px; font-size: 9pt;"></td>
                            <td style="width: 100px; height: 23px" align="center">
                                <asp:Button id="btnFirst" Text="首页" runat="server" CommandName="first" OnClick="NavigateToPage" /></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button id="btnPrevious" Text="上一页" runat="server" CommandName="prev" OnClick="NavigateToPage"/></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnNext" Text="下一页" runat="server" CommandName="next" OnClick="NavigateToPage" /></td>
                            <td align="center" style="width: 71px; height: 23px">
                                <asp:Button ID="btnLast" Text="末页" runat="server" CommandName="last" OnClick="NavigateToPage" /></td>
                            <td style="width: 100px; height: 23px; font-size: 9pt;" align="right">
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
    </div>
    </form>
</body>
</html>

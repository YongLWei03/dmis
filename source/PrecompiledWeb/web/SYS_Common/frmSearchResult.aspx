<%@ page language="C#" enableeventvalidation="false" autoeventwireup="true" inherits="yw_zhuhai_frmSearchResult, App_Web_og9prjkz" theme="default" culture="auto" uiculture="auto" meta:resourcekey="PageResource2" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td runat="server" align="center" colspan="3" style="font-weight: bold; font-size: large;
                    color: maroon; font-family: 楷体_GB2312; height: 24px; width: 100%;" valign="middle">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 80px; height: 23px">
                                <span style="font-size: 10pt; color: #000000; font-family: 宋体"><asp:Label ID="Label2"
                                    runat="server" Text="功能名称：" meta:resourcekey="Label2Resource1"></asp:Label></span></td>
                            <td id="tdTitle" runat="server" style="height: 23px; text-align: left; font-weight: bold; font-size: 12pt; color: #660000; font-family: 楷体_GB2312;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px; height: 23px">
                                <span style="font-size: 10pt; color: black; font-family: 宋体;">
                                    <asp:Label ID="Label1" runat="server" Text="查询条件：" meta:resourcekey="Label1Resource1"></asp:Label></span></td>
                            <td id="tdCondition" runat="server" style="height: 23px; text-align: left; font-weight: bold; font-size: 12pt; color: #660000; font-family: 楷体_GB2312;">
                            </td>
                        </tr>
                    </table>
                    
                </td>
                <td style="text-align: center; width: 107px;"><asp:ImageButton ID="imbOutExcel" runat="server" ImageUrl="../img/fileType/excel.gif" OnClick="imbOutExcel_Click" ToolTip="把查询结果导出到EXCEL文件中！" meta:resourcekey="imbOutExcelResource1" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 21px">
                    <hr color="#339966" width="100%" />
                   </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 79px">
                    <asp:GridView ID="grvList" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="4" CssClass="font" EmptyDataText="没有满足条件的记录！"
                        OnSelectedIndexChanged="grvList_SelectedIndexChanged" PageSize="20" Width="100%" Font-Size="9pt" meta:resourcekey="grvListResource1">
                        <PagerSettings Visible="False" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td id="tdMessage" runat="server" style="width: 50%; height: 23px; font-size: 9pt;">
                            </td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnFirst" runat="server" CommandName="first" OnClick="NavigateToPage"
                                    Text="首页" meta:resourcekey="btnFirstResource1" /></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnPrevious" runat="server" CommandName="prev" OnClick="NavigateToPage"
                                    Text="上一页" meta:resourcekey="btnPreviousResource1" /></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnNext" runat="server" CommandName="next" OnClick="NavigateToPage"
                                    Text="下一页" meta:resourcekey="btnNextResource1" /></td>
                            <td align="center" style="width: 71px; height: 23px">
                                <asp:Button ID="btnLast" runat="server" CommandName="last" OnClick="NavigateToPage"
                                    Text="末页" meta:resourcekey="btnLastResource1" /></td>
                            <td align="right" style="width: 100px; height: 23px; font-size: 9pt;">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" Text="转向"></asp:Label></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:TextBox ID="txtPage" runat="server" Width="40px" meta:resourcekey="txtPageResource1"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 100px; height: 23px">
                                <asp:Button ID="btnTurn" runat="server" CommandName="go" OnClick="NavigateToPage"
                                    Text="确定" meta:resourcekey="btnTurnResource1" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

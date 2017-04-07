<%@ page language="C#" autoeventwireup="true" inherits="YW_ZDH_frmWorkFlowQueryByStation, App_Web_iakvuhia" maintainscrollpositiononpostback="true" enableeventvalidation="false" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>按厂站查询</title>
</head>
<body style="margin-top: 0px; margin-left: 2px;">
  <form id="form1" runat="server">
    <div>
        <table style="width: 1200px; ">
            <tr>
<td style="height: 17px; background-image: url(../img/pics.jpg);" colspan="2" >
    <table style="width: 643px">
        <tr>
            <td style="width: 96px; height: 26px; text-align: center;">
                开始日期</td>
            <td style="width: 80px; height: 26px; text-align: center">
                <cc1:WebDate ID="wdlStart" runat="server" DateTimeStyle="Date" />
            </td>
            <td style="width: 97px; height: 26px; text-align: center">
                结束日期</td>
            <td style="width: 80px; height: 26px; text-align: center">
                <cc1:WebDate ID="wdlEnd" runat="server" DateTimeStyle="Date" />
            </td>
            <td style="width: 80px; height: 26px; text-align: center;">
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="查询" />&nbsp;</td>
            <td style="width: 80px; height: 26px; text-align: center;">
                <asp:Button ID="btnSaveExcel" runat="server" OnClick="btnSaveExcel_Click"
                    Text="Excel" ToolTip="导出Excel文件" /></td>
            <td style="width: 76px; height: 26px; text-align: center;">
                &nbsp;</td>
            <td style="width: 128px; height: 26px; text-align: center">
                </td>
        </tr>
    </table>
</td>
            </tr>
            <tr>
               <td rowspan="2" width="170" align="left" valign="top" bgcolor="#ffffcc" bordercolordark="#cc3300" bordercolorlight="#99ffcc" style="height: 652px; background-image: url(../img/Nature Bkgrd.jpg);">
                    &nbsp;<div style="width: 170px; height: 100%;overflow:scroll">
                        <asp:TreeView ID="trvStation" runat="server"  
                            Width="168px" Font-Size="10pt" ExpandDepth="0" OnSelectedNodeChanged="trvStation_SelectedNodeChanged">
                            <SelectedNodeStyle Font-Bold="True" Font-Underline="True" ImageUrl="~/img/selectChildNode.gif" Font-Italic="True" Font-Size="12pt" ForeColor="#400040" />
                        </asp:TreeView>
                    </div>
                </td>
                <td style="width: 101%;" align="left" valign="top" rowspan="2">
                    <table style="width: 100%">
                        <tr>
                            <td id="Td1" runat="server" colspan="8" style="height: 200px" align="center" valign="top">
                    <asp:GridView ID="grvList" runat="server" AllowPaging="True" AllowSorting="True" CellPadding="4" CssClass="font"
                        EmptyDataText="没有满足条件的记录！"
                        OnSorting="grvList_Sorting" PageSize="20" Width="100%" DataKeyNames="f_no,f_packtypeno" OnRowCommand="grvList_RowCommand">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:BoundField DataField="f_packname" HeaderText="业务类别">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_CREATEMAN" HeaderText="创建人">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_CREATEDATE" HeaderText="创建时间">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_DESC" HeaderText="任务描述" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_ARCHIVEDATE" HeaderText="归档时间">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_STATUS" HeaderText="状态">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PLAN_STARTTIME" HeaderText="计划开始时间">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PLAN_ENDTIME" HeaderText="计划结束时间">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>
                            <asp:ButtonField CommandName="Query" HeaderText="详细" ButtonType="Image" ImageUrl="~/img/view.gif">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td id="tdMessage" runat="server" style="width: 614px; height: 23px">
                            </td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnFirst" runat="server" CommandName="first" OnClick="NavigateToPage"
                                    Text="首页" /></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnPrevious" runat="server" CommandName="prev" OnClick="NavigateToPage"
                                    Text="上一页" /></td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:Button ID="btnNext" runat="server" CommandName="next" OnClick="NavigateToPage"
                                    Text="下一页" /></td>
                            <td align="center" style="width: 71px; height: 23px">
                                <asp:Button ID="btnLast" runat="server" CommandName="last" OnClick="NavigateToPage"
                                    Text="末页" /></td>
                            <td align="right" style="width: 100px; height: 23px">
                                转向</td>
                            <td align="center" style="width: 100px; height: 23px">
                                <asp:TextBox ID="txtPage" runat="server" Width="40px"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 100px; height: 23px">
                                <asp:Button ID="btnTurn" runat="server" CommandName="go" OnClick="NavigateToPage"
                                    Text="确定" /></td>
                        </tr>
                    </table>
  
                    <br />
                    &nbsp;</td>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
    <input id="refreshPage" runat="server" onpropertychange="javascript:form1.submit();"
            size="0" style="visibility: hidden; width: 68px" type="text" value="0" />
    </form>
</body>
</html>

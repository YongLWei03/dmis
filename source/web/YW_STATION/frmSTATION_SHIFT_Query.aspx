<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSTATION_SHIFT_Query.aspx.cs" Inherits="YW_STATION_frmSTATION_SHIFT_Query" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>变电站值班记录查询</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="list_header" runat="server">
       <img src="../img/s_img.gif" alt="" />
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
    </div>
    <div id="list_control">
        <table class="invisible_table" style="width: 84%">
            <tr>
                <td class="invisible_cell">
                    <asp:Label ID="lblStation" runat="server" Text="厂站" Width="36px" meta:resourcekey="lblStationResource1"></asp:Label></td>
                <td class="invisible_cell">
                    <asp:DropDownList ID="ddlStation" runat="server" Width="118px" meta:resourcekey="ddlStationResource1">
                    </asp:DropDownList></td>
                <td class="invisible_cell">
                    <asp:Label ID="lblStart" runat="server"  Text="开始日期" Width="63px" meta:resourcekey="lblStartResource1"></asp:Label></td>
                <td class="invisible_cell">
                    <cc1:WebDate ID="wdlStart" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date"
                        myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlStartResource1" />
                </td>
                <td class="invisible_cell">
                    <asp:Label ID="lblEnd" runat="server"  Text="结束日期" Width="65px" meta:resourcekey="lblEndResource1"></asp:Label></td>
                <td class="invisible_cell">
                    <cc1:WebDate ID="wdlEnd" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date"
                        myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlEndResource1" />
                </td>
                <td class="invisible_cell">
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="检索" meta:resourcekey="btnQueryResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="查询" meta:resourcekey="btnSearchResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnSort" runat="server" OnClick="btnSort_Click" Text="排序" meta:resourcekey="btnSortResource1"  /></td>
                <td class="invisible_cell"><asp:Button ID="btnOutExcel" runat="server" OnClick="btnSaveExcel_Click" Text="Excel" meta:resourcekey="btnOutExcelResource1"  /></td>
            </tr>
        </table>
    </div>
    <div id="list_data">
        <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="4" CssClass="font" DataKeyNames="tid"
            OnSelectedIndexChanged="grvRef_SelectedIndexChanged" PageSize="31" Width="2500px" meta:resourcekey="grvListResource1" OnRowDataBound="grvList_RowDataBound">
            <PagerSettings FirstPageText="" LastPageText="" NextPageText="" PreviousPageText=""
                Visible="False" />
            <Columns>
                <asp:BoundField DataField="STATION_ID" HeaderText="厂站" meta:resourcekey="BoundFieldResource1" >
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="DATEM" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False" meta:resourcekey="BoundFieldResource2" >
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="SHIFT" HeaderText="班次" meta:resourcekey="BoundFieldResource3" >
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="WEATHER" HeaderText="天气" meta:resourcekey="BoundFieldResource4" >
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="CURRENT_SHIFT_MAN1" HeaderText="当班值长" meta:resourcekey="BoundFieldResource5">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="JOIN_TIME" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="接班时间"
                    HtmlEncode="False" meta:resourcekey="BoundFieldResource6" >
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="NEXT_SHIFT_MAN1" HeaderText="接班值长" meta:resourcekey="BoundFieldResource7">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="RUN_MODE" HeaderText="运行方式" meta:resourcekey="BoundFieldResource8" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="STATUS1" HeaderText="非正常运行方式" meta:resourcekey="BoundFieldResource9">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="STATUS2" HeaderText="事故（障碍）情况" meta:resourcekey="BoundFieldResource10">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="STATUS3" HeaderText="检修（试验）情况" meta:resourcekey="BoundFieldResource11">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="STATUS4" HeaderText="通信、自动化异常情况" meta:resourcekey="BoundFieldResource12">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="STATUS5" HeaderText="其他交接工作" meta:resourcekey="BoundFieldResource13">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="STATUS6" HeaderText="领导指示、重要文件" meta:resourcekey="BoundFieldResource14">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="list_pager">
        <table class="invisible_table">
            <tr>
                <td id="tdMessage" runat="server" class="pager_info">
                </td>
                <td class="invisible_cell">
                    <asp:Button ID="btnFirst" runat="server" CommandName="first" meta:resourcekey="btnFirstResource1"
                        OnClick="NavigateToPage" Text="首页" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnPrevious" runat="server" CommandName="prev" meta:resourcekey="btnPreviousResource1"
                        OnClick="NavigateToPage" Text="上一页" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnNext" runat="server" CommandName="next" meta:resourcekey="btnNextResource1"
                        OnClick="NavigateToPage" Text="下一页" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnLast" runat="server" CommandName="last" meta:resourcekey="btnLastResource1"
                        OnClick="NavigateToPage" Text="末页" /></td>
                <td  class="invisible_cell">
                    <asp:Label ID="lblTurn" runat="server" Font-Overline="False" Font-Size="9pt" meta:resourcekey="lblTurnResource1"
                        Text="转向"></asp:Label></td>
                <td  class="invisible_cell">
                    <asp:TextBox ID="txtPage" runat="server" meta:resourcekey="txtPageResource1" Width="40px"></asp:TextBox></td>
                <td  class="invisible_cell">
                    <asp:Button ID="btnTurn" runat="server" CommandName="go" meta:resourcekey="btnTurnResource1"
                        OnClick="NavigateToPage" Text="确定" /></td>
            </tr>
        </table>
    </div>
    <input id="refreshPage" runat="server" onpropertychange="javascript:form1.submit();"
            size="0" style="visibility: hidden; width: 68px" type="text" value="0" />
    </form>
</body>
</html>

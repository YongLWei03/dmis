<%@ page language="C#" autoeventwireup="true" inherits="YW_STATION_frmSTATION_RUNNING_LOG_Query, App_Web_jbl8kjcp" culture="auto" uiculture="auto" meta:resourcekey="PageResource2" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>变电站运行日志查询</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="list_header" >
       <img src="../img/s_img.gif" alt="" />
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1" ></asp:Label>
    </div>
    <div id="list_control">
        <table class="invisible_table" style="width: 84%">
            <tr>
                <td class="invisible_cell">
                    <asp:Label ID="lblStation" runat="server" Text="厂站"  Width="36px" meta:resourcekey="lblStationResource1"></asp:Label></td>
                <td class="invisible_cell">
                    <asp:DropDownList ID="ddlStation" runat="server" Width="118px" meta:resourcekey="ddlStationResource1" >
                    </asp:DropDownList></td>
                <td class="invisible_cell">
                    <asp:Label ID="lblStart" runat="server"  Text="开始日期"
                        Width="63px" meta:resourcekey="lblStartResource1"></asp:Label></td>
                <td class="invisible_cell">
                    <cc1:WebDate ID="wdlStart" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date"
                        myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlStartResource1"  />
                </td>
                <td class="invisible_cell">
                    <asp:Label ID="lblEnd" runat="server"  Text="结束日期"
                        Width="65px" meta:resourcekey="lblEndResource1"></asp:Label></td>
                <td class="invisible_cell">
                    <cc1:WebDate ID="wdlEnd" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date"
                        myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlEndResource1" />
                </td>
                <td class="invisible_cell">
                        <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="检索" meta:resourcekey="btnQueryResource1" />
                </td>
                <td class="invisible_cell">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="查询" meta:resourcekey="btnSearchResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnSort" runat="server" OnClick="btnSort_Click" Text="排序" meta:resourcekey="btnSortResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnOutExcel" runat="server" OnClick="btnSaveExcel_Click" Text="Excel" meta:resourcekey="btnOutExcelResource1" /></td>
            </tr>
        </table>
     </div>
    <div id="list_data">
        <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="2" DataKeyNames="TID"
            OnSelectedIndexChanged="grvRef_SelectedIndexChanged" PageSize="31" Width="100%"  OnRowDataBound="grvList_RowDataBound" CaptionAlign="Top" meta:resourcekey="grvListResource1">
            <PagerSettings FirstPageText="" LastPageText="" NextPageText="" PreviousPageText=""
                Visible="False" />
            <Columns>
                <asp:BoundField DataField="STATION_ID" HeaderText="厂站" meta:resourcekey="BoundFieldResource1"  >
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="SHIFT_DATE" HeaderText="值班日期" meta:resourcekey="BoundFieldResource2" >
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="SHIFT" HeaderText="班次" meta:resourcekey="BoundFieldResource3"  >
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="DATEM" HeaderText="发生时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False" meta:resourcekey="BoundFieldResource4"  >
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="DEPARTMENT" HeaderText="联系单位" meta:resourcekey="BoundFieldResource5" >
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="LINKMAN" HeaderText="联系人" meta:resourcekey="BoundFieldResource6" >
                    <ItemStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="CATEGORY" HeaderText="类别" meta:resourcekey="BoundFieldResource7" >
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="CONTENT" HeaderText="联系内容" meta:resourcekey="BoundFieldResource8">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DISPATCHER" HeaderText="记录人" meta:resourcekey="BoundFieldResource9">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
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
                    <asp:Button ID="btnFirst" runat="server" CommandName="first" 
                        OnClick="NavigateToPage" Text="首页" meta:resourcekey="btnFirstResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnPrevious" runat="server" CommandName="prev" 
                        OnClick="NavigateToPage" Text="上一页" meta:resourcekey="btnPreviousResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnNext" runat="server" CommandName="next" 
                        OnClick="NavigateToPage" Text="下一页" meta:resourcekey="btnNextResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnLast" runat="server" CommandName="last" 
                        OnClick="NavigateToPage" Text="末页" meta:resourcekey="btnLastResource1" /></td>
                <td  class="invisible_cell">
                    <asp:Label ID="lblTurn" runat="server" Font-Overline="False" Font-Size="9pt" Text="转向" meta:resourcekey="lblTurnResource1"></asp:Label></td>
                <td  class="invisible_cell">
                    <asp:TextBox ID="txtPage" runat="server"  Width="40px" meta:resourcekey="txtPageResource1"></asp:TextBox></td>
                <td  class="invisible_cell">
                    <asp:Button ID="btnTurn" runat="server" CommandName="go" 
                        OnClick="NavigateToPage" Text="确定" meta:resourcekey="btnTurnResource1" /></td>
            </tr>
        </table>
    </div>

    </form>
</body>
</html>

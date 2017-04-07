<%@ page language="C#" autoeventwireup="true" inherits="YW_TX_frmTX_RUNNING_LOG_Det, App_Web_uyk1uz7_" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>通信系统运行日志</title>
</head>
<body class="detail_body">
    <form id="form1" runat="server">
    <div id="detail_head" style="text-align: center">
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
    </div>
    <div id="detail_control">    
            <table class="invisible_table">
            <tr>
                <td style="width: 100px; height: 21px;">
                <asp:Label ID="lblDate" runat="server" Text="值班日期" meta:resourcekey="lblDateResource1"></asp:Label></td>
                <td style="width: 100px; height: 21px;">
                    <cc1:WebDate ID="wdlDate" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlDateResource1" />
                </td>
                <td style="width: 100px; height: 21px;">
                    <asp:Label ID="lblWeather" runat="server" Text="天气" meta:resourcekey="lblWeatherResource1"></asp:Label></td>
                <td style="width: 100px; height: 21px">
                    <cc1:HtmlComboBox ID="hcbWeather" runat="server" Width="73px" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbWeatherResource1" Rows="1" SelectedText="">
                    </cc1:HtmlComboBox></td>
                <td style="width: 100px; height: 21px">
                    <asp:Button ID="btnSave" runat="server" Enabled="False" OnClick="btnSave_Click" Text="查询" meta:resourcekey="btnSaveResource1" /></td>
                <td style="width: 100px; height: 21px;">
                    <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="返回" meta:resourcekey="btnReturnResource1" /></td>
                <td style="width: 100px; height: 21px;">
                </td>
                <td style="width: 100px; height: 21px;">
                </td>
                <td style="width: 100px; height: 21px;">
                </td>
            </tr>
        </table>
        </div>


    <div id="detail_data">
        &nbsp;<asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            AutoGenerateEditButton="True" CellPadding="4" CssClass="font" DataKeyNames="TID" OnRowCancelingEdit="grvList_RowCancelingEdit" 
            OnRowEditing="grvList_RowEditing" OnRowUpdating="grvList_RowUpdating" PageSize="20"
            Width="100%" meta:resourcekey="grvListResource1">
            <PagerSettings FirstPageText="" LastPageText="" NextPageText="" PreviousPageText=""
                Visible="False" />
            <Columns>
                <asp:BoundField DataField="STATION" HeaderText="厂站" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />
                <asp:BoundField DataField="PLAN_WORKING_HOURS" HeaderText="应工作小时数" ReadOnly="True" meta:resourcekey="BoundFieldResource2" />
                <asp:BoundField DataField="ACTUAL_WORKING_HOURS" HeaderText="实际工作小时数" ReadOnly="True" meta:resourcekey="BoundFieldResource3" />
                <asp:TemplateField HeaderText="停运小时数" meta:resourcekey="TemplateFieldResource1">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtINTERRUPT_HOURS" runat="server" Columns="10" Rows="3" Text='<%# Eval("INTERRUPT_HOURS") %>' meta:resourcekey="txtINTERRUPT_HOURSResource1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("INTERRUPT_HOURS") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RUNNING_RATE" HeaderText="运行率" ReadOnly="True" DataFormatString="{0:p}" HtmlEncode="False" meta:resourcekey="BoundFieldResource4" />
                <asp:TemplateField HeaderText="备注" meta:resourcekey="TemplateFieldResource2">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNOTE" runat="server" Columns="30" Rows="3" Text='<%# Eval("NOTE") %>' Height="88px" TextMode="MultiLine" Width="272px" meta:resourcekey="txtNOTEResource1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("NOTE") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="OPERATOR" HeaderText="值班人" ReadOnly="True" meta:resourcekey="BoundFieldResource5">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="WEATHER" HeaderText="天气" ReadOnly="True" meta:resourcekey="BoundFieldResource6">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="DATEM" DataFormatString="{0:dd-MM-yyyy}" HeaderText="值班日期"
                    HtmlEncode="False" ReadOnly="True" meta:resourcekey="BoundFieldResource7">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="detail_info" runat="server">
    </div>
    </form>
</body>
</html>

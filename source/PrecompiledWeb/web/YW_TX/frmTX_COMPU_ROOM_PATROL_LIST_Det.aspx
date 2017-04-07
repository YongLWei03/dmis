<%@ page language="C#" autoeventwireup="true" inherits="YW_TX_frmTX_COMPU_ROOM_PATROL_LIST_Det, App_Web_uyk1uz7_" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>通信机房设备巡视记录</title>
</head>
<body class="detail_body">
    <form id="form1" runat="server">
    <div id="detail_head" style="text-align: left">
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
    </div>
    <div >
        <table class="invisible_table">
            <tr>
                <td style="width: 100px; height: 21px;">
                <asp:Label ID="lblDate" runat="server" Text="巡视日期" meta:resourcekey="lblDateResource1"></asp:Label></td>
                <td style="width: 100px; height: 21px;">
                    <cc1:WebDate ID="wdlDate" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date"  myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlDateResource1" />
                </td>
                <td style="width: 100px; height: 21px;">
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
        <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            AutoGenerateEditButton="True" CellPadding="4" CssClass="font" DataKeyNames="TID"
            OnRowCancelingEdit="grvList_RowCancelingEdit"
            OnRowEditing="grvList_RowEditing" OnRowUpdating="grvList_RowUpdating" PageSize="20"
            Width="100%" meta:resourcekey="grvListResource1">
            <PagerSettings FirstPageText="" LastPageText="" NextPageText="" PreviousPageText=""
                Visible="False" />
            <Columns>
                <asp:BoundField DataField="DEVICE_NAME" HeaderText="设备名称" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />
                <asp:TemplateField HeaderText="8:30" meta:resourcekey="TemplateFieldResource1">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRUN_SITUATION1" runat="server" meta:resourcekey="ddlRUN_SITUATION1Resource1">
                            <asp:ListItem meta:resourcekey="ListItemResource1">√</asp:ListItem>
                            <asp:ListItem meta:resourcekey="ListItemResource2">&#215;</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("RUN_SITUATION1") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="11:30" meta:resourcekey="TemplateFieldResource2">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRUN_SITUATION2" runat="server" meta:resourcekey="ddlRUN_SITUATION2Resource1">
                            <asp:ListItem meta:resourcekey="ListItemResource3">√</asp:ListItem>
                            <asp:ListItem meta:resourcekey="ListItemResource4">&#215;</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("RUN_SITUATION2") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="18:00" meta:resourcekey="TemplateFieldResource3">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRUN_SITUATION3" runat="server" meta:resourcekey="ddlRUN_SITUATION3Resource1">
                            <asp:ListItem meta:resourcekey="ListItemResource5">√</asp:ListItem>
                            <asp:ListItem meta:resourcekey="ListItemResource6">&#215;</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("RUN_SITUATION3") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="设备故障分析" meta:resourcekey="TemplateFieldResource4">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFAULT_ANALYSIS" runat="server" Columns="30" Rows="3" Text='<%# Eval("FAULT_ANALYSIS") %>' Height="72px" TextMode="MultiLine" meta:resourcekey="txtFAULT_ANALYSISResource1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("FAULT_ANALYSIS") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="DATEM" DataFormatString="{0:dd-MM-yyyy}" HeaderText="巡视日期"
                    ReadOnly="True" HtmlEncode="False" meta:resourcekey="BoundFieldResource2">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="INSPECTOR" HeaderText="巡视人" ReadOnly="True" meta:resourcekey="BoundFieldResource3">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="detail_control">
        &nbsp;</div>
    <div id="detail_info" runat="server">
    </div>
    </form>
</body>
</html>

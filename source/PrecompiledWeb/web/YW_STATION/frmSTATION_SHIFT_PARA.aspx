<%@ page language="C#" autoeventwireup="true" inherits="YW_STATION_frmSTATION_SHIFT_PARA, App_Web_jbl8kjcp" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>变电站值班班次参数</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="list_header" runat="server">
       <img src="../img/s_img.gif" alt="" />
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
    </div>
    <div id="list_control">
        <table class="invisible_table">
            <tr>
                <td class="invisible_cell">
                    <asp:Label ID="lblStation" runat="server" Text="厂站" Width="51px" meta:resourcekey="lblStationResource1"></asp:Label></td>
                <td class="invisible_cell">
                    <asp:DropDownList ID="ddlStation" runat="server" Width="138px" meta:resourcekey="ddlStationResource1">
                    </asp:DropDownList></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="检索" meta:resourcekey="btnQueryResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" Enabled="False" meta:resourcekey="btnAddResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                        Text="删除" Enabled="False" meta:resourcekey="btnDeleteResource1" /></td>
                <td class="invisible_cell">
                    </td>
                <td class="invisible_cell">
                    </td>
                <td class="invisible_cell">
                    </td>
                <td class="invisible_cell">
                    </td>
            </tr>
        </table>
    </div>
    <div id="list_data">
        <asp:GridView ID="grvList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CellPadding="4" CssClass="font" DataKeyNames="tid"
            OnRowCancelingEdit="grvRef_RowCancelingEdit" OnRowDataBound="grv_RowDataBound"
            OnRowEditing="grv_RowEditing" OnRowUpdating="grv_RowUpdating" OnSelectedIndexChanged="grvRef_SelectedIndexChanged"
            OnSorting="grvList_Sorting" PageSize="20" Width="100%" meta:resourcekey="grvListResource1">
            <Columns>
                <asp:BoundField DataField="STATION_ID" HeaderText="变电站" ReadOnly="True" meta:resourcekey="BoundFieldResource1">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="班次名称" meta:resourcekey="TemplateFieldResource1">
                    <EditItemTemplate>
                    <asp:TextBox ID="txtSHIFT_NAME" runat="server" Width="122px" meta:resourcekey="txtSHIFT_NAMEResource1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("SHIFT_NAME") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="开始时间" meta:resourcekey="TemplateFieldResource2">
                    <EditItemTemplate>
                        <cc1:WebDate ID="wdlSHIFT_STARTTIME" runat="server" DateStyle="DateFormat3" myDateWidth="80px"
                            myHourWidth="15px" myMinuteWidth="15px" ShowLine="DoubleLine" ButtonText=".." meta:resourcekey="wdlSHIFT_STARTTIMEResource1" />
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SHIFT_STARTTIME", "{0:HH:mm}") %>' meta:resourcekey="Label1Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="结束时间" meta:resourcekey="TemplateFieldResource3">
                    <EditItemTemplate>
                        <cc1:WebDate ID="wdlSHIFT_ENDTIME" runat="server" DateStyle="DateFormat3" myDateWidth="80px"
                            myHourWidth="15px" myMinuteWidth="15px" ShowLine="DoubleLine" ButtonText=".." meta:resourcekey="wdlSHIFT_ENDTIMEResource1" />
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("SHIFT_ENDTIME", "{0:HH:mm}") %>' meta:resourcekey="Label8Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="跨越天数" meta:resourcekey="TemplateFieldResource4">
                    <EditItemTemplate>
                    <asp:TextBox ID="txtSHIFT_DAYS" runat="server" Width="80px" meta:resourcekey="txtSHIFT_DAYSResource1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("SHIFT_DAYS") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField CancelText="&lt;img border=0 align=absmiddle src=../img/quxiao.gif&gt;"
                    EditText="&lt;img border=0 align=absmiddle src=../img/modifyFlag.gif&gt;" HeaderText="编辑"
                    ShowEditButton="True" UpdateText="&lt;img border=0 align=absmiddle src=../img/save.gif&gt;" meta:resourcekey="CommandFieldResource1">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="list_pager">
    </div>
        <input id="refreshPage" runat="server" onpropertychange="javascript:form1.submit();"
            size="0" style="visibility: hidden; width: 68px" type="text" value="0" />
    </form>
</body>
</html>

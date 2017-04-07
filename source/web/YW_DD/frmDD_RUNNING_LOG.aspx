<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDD_RUNNING_LOG.aspx.cs" Inherits="YW_DD_frmDD_RUNNING_LOG" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>调度运行日志</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="list_header" >
       <img src="../img/s_img.gif" alt="" />
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
    </div>
    <div id="list_control">
        <table class="invisible_table" style="width: 84%">
            <tr>
                <td class="invisible_cell">
                    <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1" Text="值班日期" Width="76px"></asp:Label></td>
                <td class="invisible_cell">
                    <cc1:WebDate ID="wdlDate" runat="server" ButtonText=".."
                        DateTimeStyle="Date" meta:resourcekey="wdlDateResource1" myDateWidth="80px" />
                </td>
                <td class="invisible_cell">
                    <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1" Text="班次" Width="47px"></asp:Label></td>
                <td class="invisible_cell">
                    <asp:DropDownList ID="ddlShift" runat="server" Width="76px" meta:resourcekey="ddlShiftResource1">
                    </asp:DropDownList></td>
                <td class="invisible_cell">
                    <span style="font-size: 9pt">
                        <asp:Button ID="btnQuery" runat="server" meta:resourcekey="btnQueryResource1" OnClick="btnQuery_Click"
                            Text="检索" /></span></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnCurrentShift" runat="server" meta:resourcekey="btnCurrentShiftResource1"
                        OnClick="btnCurrentShift_Click" Text="本班" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnAdd" runat="server" Enabled="False" meta:resourcekey="btnAddResource1"
                        OnClick="btnAdd_Click" Text="添加" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnDelete" runat="server" Enabled="False" meta:resourcekey="btnDeleteResource1"
                        OnClick="btnDelete_Click" OnClientClick='return confirm("确定要删除此值班记事？");' Text="删除" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnSearch" runat="server" Enabled="False" meta:resourcekey="btnSearchResource1"
                        OnClick="btnSearch_Click" Text="查询" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnSort" runat="server" meta:resourcekey="btnSortResource1" OnClick="btnSort_Click"
                        Text="排序" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnPrint" runat="server" Enabled="False" meta:resourcekey="btnPrintResource1"
                        OnClick="btnPrint_Click" Text="打印" />
                </td>
            </tr>
        </table>
    </div>
    <div id="list_data">
        <asp:GridView ID="grvList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CellPadding="4" CssClass="font" DataKeyNames="tid" EmptyDataText="无数据" meta:resourcekey="grvListResource1"
            OnRowCancelingEdit="grvRef_RowCancelingEdit"
            OnRowEditing="grvRef_RowEditing" OnRowUpdating="grvRef_RowUpdating" OnSelectedIndexChanged="grvRef_SelectedIndexChanged"
            OnSorting="grvList_Sorting" PageSize="20" Width="100%">
            <Columns>
                <asp:CommandField HeaderText="选择" meta:resourceKey="CommandFieldResource1" SelectText="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;"
                    ShowSelectButton="True">
                    <ItemStyle Width="30px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="发生时间" meta:resourceKey="TemplateFieldResource1" SortExpression="DATEM">
                    <EditItemTemplate>
                        <cc1:WebDate ID="wdlDATEM" runat="server" ButtonText=".."
                            meta:resourcekey="wdlDATEMResource1" myDateWidth="80px" myHourWidth="15px" myMinuteWidth="15px"
                            ShowLine="DoubleLine" />
                    </EditItemTemplate>
                    <ItemStyle Width="80px" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" Text='<%# Bind("DATEM", "{0:dd-MM-yyyy HH:mm}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="联系单位" meta:resourceKey="TemplateFieldResource2" SortExpression="DEPARTMENT">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDEPARTMENT" runat="server" Width="103px" meta:resourcekey="txtDEPARTMENTResource1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="80px" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" Text='<%# Bind("DEPARTMENT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="联系人" meta:resourceKey="TemplateFieldResource3" SortExpression="LINKMAN">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLINKMAN" runat="server" Width="77px" meta:resourcekey="txtLINKMANResource1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label5Resource1" Text='<%# Bind("LINKMAN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类别" meta:resourceKey="TemplateFieldResource4" SortExpression="CATEGORY">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlCATEGORY" runat="server" meta:resourcekey="ddlCATEGORYResource1"
                            Width="106px">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemStyle Width="80px" />
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1" Text='<%# Bind("CATEGORY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="内容" meta:resourceKey="TemplateFieldResource5">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCONTENT" runat="server" Height="66px" meta:resourcekey="txtCONTENTResource1"
                            Text='<%# Bind("CONTENT") %>' TextMode="MultiLine" Width="268px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1" Text='<%# Bind("CONTENT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField CancelText="&lt;img border=0 align=absmiddle src=../img/quxiao.gif&gt;"
                    EditText="&lt;img border=0 align=absmiddle src=../img/modifyFlag.gif&gt;" HeaderText="编辑"
                    meta:resourceKey="CommandFieldResource2" ShowEditButton="True" UpdateText="&lt;img border=0 align=absmiddle src=../img/save.gif&gt;">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                </asp:CommandField>
                <asp:BoundField DataField="DISPATCHER" HeaderText="记录人" ReadOnly="True" SortExpression="DISPATCHER" meta:resourcekey="BoundFieldResource1">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
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

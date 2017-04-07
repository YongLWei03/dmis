<%@ page language="C#" autoeventwireup="true" inherits="YW_DD_frmDD_TYPICAL_OPT_Det, App_Web_docfbltz" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>典型票</title>
</head>
<body class="detail_body">
    <form id="form1" runat="server">
    
            <div id="detail_head">
                        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label></div> 
        <div id="detail_data">
            <br />
            <table class="slim_table">
                <tr>
                    <td class="slim_table_td_desc" >
                               <asp:Label ID="lblSTATION" runat="server" Text="变电站" meta:resourcekey="lblSTATIONResource1"></asp:Label></td>
                    <td class="slim_table_td_control" colspan="3">
                        <asp:DropDownList ID="ddlSTATION" runat="server" Width="150px" meta:resourcekey="ddlSTATIONResource1">
                        </asp:DropDownList></td>
                </tr>
                    <tr>
                        <td class="slim_table_td_desc">
                               <asp:Label ID="lblTASK" runat="server" Text="操作任务" meta:resourcekey="lblSTARTTIMEResource1"></asp:Label></td>
                        <td class="slim_table_td_control" colspan="3">
                            <asp:TextBox ID="txtTASK" runat="server" Height="37px" meta:resourcekey="txtACCIDENT_REASONResource1"
                                TextMode="MultiLine" Width="544px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="slim_table_td_desc" colspan="4">
                            <asp:Label ID="lblCONTENT" runat="server" Text="操作步骤" Width="76px" meta:resourcekey="lblACCIDENT_REASONResource1"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="slim_table_td_control" colspan="4">
                            <asp:GridView ID="grvList" runat="server" DataKeyNames="TID" Font-Names="宋体" Font-Size="9pt"
                                OnRowCancelingEdit="grvList_RowCancelingEdit" OnRowEditing="grvList_RowEditing"
                                OnRowUpdating="grvList_RowUpdating" OnSelectedIndexChanged="grvList_SelectedIndexChanged" meta:resourcekey="grvListResource1">
                                <RowStyle Height="25px" />
                                <Columns>
                                    <asp:CommandField HeaderText="选择" SelectText="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;"
                                        ShowSelectButton="True" meta:resourcekey="CommandFieldResource1">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="单位" meta:resourcekey="TemplateFieldResource1">
                                        <EditItemTemplate>
                                            <cc1:HtmlComboBox ID="hcbUNIT" runat="server" Width="136px" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbUNITResource1" SelectedText="">
                                            </cc1:HtmlComboBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("UNIT") %>' Width="71px" meta:resourcekey="Label1Resource1"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="步骤" meta:resourcekey="TemplateFieldResource2">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtXH" runat="server" Text='<%# Bind("XH") %>' Width="46px" meta:resourcekey="txtXHResource1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("XH") %>' meta:resourcekey="Label2Resource1"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="操作内容" meta:resourcekey="TemplateFieldResource3">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCONTENT" runat="server" Height="49px" Text='<%# Bind("CONTENT") %>'
                                                TextMode="MultiLine" Width="570px" meta:resourcekey="txtCONTENTResource1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("CONTENT") %>' meta:resourcekey="Label3Resource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField CancelText="&lt;img border=0 align=absmiddle src=../img/quxiao.gif&gt;"
                                        EditText="&lt;img border=0 align=absmiddle src=../img/modifyFlag.gif&gt;" HeaderText="编辑"
                                        ShowEditButton="True" UpdateText="&lt;img border=0 align=absmiddle src=../img/save.gif&gt;" meta:resourcekey="CommandFieldResource2">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                    </asp:CommandField>
                                </Columns>
                                <HeaderStyle Height="25px" />
                            </asp:GridView>
                        </td>
                    </tr>
            </table>
            <br />
        </div>

        <div id="detail_control">
            <asp:Button ID="btnAppendItem" runat="server" Enabled="False" OnClick="btnAppendItem_Click"
                Text="添加步骤" meta:resourcekey="btnAppendItemResource1" />&nbsp;<asp:Button ID="btnInsertItem" runat="server" Enabled="False"
                    OnClick="btnInsertItem_Click" Text="插入步骤" meta:resourcekey="btnInsertItemResource1" />&nbsp;<asp:Button ID="btnDeleteItem"
                        runat="server" Enabled="False" OnClick="btnDeleteItem_Click"
                        Text="删除步骤" meta:resourcekey="btnDeleteItemResource1" />
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" Enabled="False" meta:resourcekey="btnSaveResource1" />
                <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="返回" meta:resourcekey="btnReturnResource1" />
                <asp:TextBox ID="txtTID" runat="server" EnableTheming="False" Visible="False" Width="32px" meta:resourcekey="txtTIDResource1"></asp:TextBox>&nbsp;
         </div>
        <div id="detail_info" runat="server">
        </div>
    </form>
</body>
</html>

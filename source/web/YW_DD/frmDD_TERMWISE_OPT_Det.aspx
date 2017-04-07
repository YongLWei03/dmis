<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDD_TERMWISE_OPT_Det.aspx.cs" Inherits="YW_DD_frmDD_TERMWISE_OPT_Det" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>逐项操作票</title>
</head>
<body style="margin-top: 2px;  margin-left: 2px;">
    <form id="form1" runat="server">
            <div id="detail_head" style="width:90%; ">
                 <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label></div> 
            <div style="width:90%; text-align:left">
                &nbsp; &nbsp;
                <asp:Button id="btnSend" onclick="btnSend_Click" runat="server" meta:resourcekey="btnSendResource1" Text="提 交" Width="60px"></asp:Button>&nbsp;
                <asp:Button id="btnWithdraw" onclick="btnWithdraw_Click" runat="server" meta:resourcekey="btnWithdrawResource1" Text="退 回" Width="60px"></asp:Button>&nbsp;
                <asp:Button id="btnPrint" onclick="btnPrint_Click" runat="server" meta:resourcekey="btnPrintResource1" Text="打 印" Width="60px"></asp:Button>&nbsp;
                <asp:Button id="btnAccept" onclick="btnAccept_Click" runat="server" meta:resourcekey="btnAcceptResource1" Text="接 单" Width="60px"></asp:Button>&nbsp;
                <asp:Button id="btnSave" onclick="btnSave_Click" runat="server" meta:resourcekey="btnSaveResource1" Text="保 存" Width="60px"></asp:Button>&nbsp;
                <asp:Button id="btnSaveClose" onclick="btnSaveClose_Click" runat="server" meta:resourcekey="btnSaveCloseResource1" Text="保存返回" Width="61px"></asp:Button>
                &nbsp;
                <asp:Button id="btnClose" onclick="btnClose_Click" runat="server" meta:resourcekey="btnCloseResource1" Text="返 回" Width="60px"></asp:Button>
            </div>

        <div id="detail_data"  style="width:90%">
            <br />
            <table class="slim_table">
                <tr>
                    <td class="slim_table_td_desc" style="width: 16.6%">
                        <asp:Label ID="lblPH" runat="server" Text="编号" meta:resourcekey="lblPHResource1"></asp:Label></td>
                    <td class="slim_table_td_control" style="width: 16.6%">
                        <asp:TextBox ID="txtPH" runat="server" MaxLength="20" Width="99%" meta:resourcekey="txtPHResource1" ReadOnly="True"></asp:TextBox></td>
                    <td class="slim_table_td_desc" style="width: 16.6%">
                        <asp:Label ID="lblDATEM" runat="server" Text="填写日期" meta:resourcekey="lblDATEMResource1"></asp:Label></td>
                    <td class="slim_table_td_control" style="width: 16.6%">
                        <cc1:WebDate ID="wdlDATEM" runat="server" DateTimeStyle="Date" ButtonText=".." meta:resourcekey="wdlDATEMResource1" Enabled="False" />
                    </td>
                    <td class="slim_table_td_desc" style="width: 16.6%">
                        <asp:Label ID="lblYPR" runat="server" Text="拟票人" meta:resourcekey="lblYPRResource1"></asp:Label></td>
                    <td class="slim_table_td_control" style="width: 16.6%">
                        <asp:TextBox ID="txtYPR" runat="server" MaxLength="20" Width="99%" meta:resourcekey="txtYPRResource1" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc" style="width: 16.6%" >
                        <asp:Label ID="lblSTATION" runat="server" Text="变电站" meta:resourcekey="lblSTATIONResource1"></asp:Label></td>
                    <td class="slim_table_td_control" style="width: 16.6%" >
                        <cc1:HtmlComboBox ID="hcbSTATION" runat="server" Enabled="False" Width="99%" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbSTATIONResource1" Rows="1" SelectedText="">
                        </cc1:HtmlComboBox></td>
                    <td class="slim_table_td_desc" style="width: 16.6%" >
                        </td>
                    <td class="slim_table_td_control" style="width: 16.6%" >
                        &nbsp;</td>
                    <td class="slim_table_td_desc" style="width: 16.6%" >
                        </td>
                    <td class="slim_table_td_control" style="width: 16.6%" >
                        </td>                                        
                </tr>
                <tr>
                    <td class="slim_table_td_desc" style="width: 16.6%">
                        <asp:Label ID="lblTASK" runat="server" Text="操作任务" meta:resourcekey="lblTASKResource1"></asp:Label></td>
                    <td class="slim_table_td_control" colspan="5">
                        <asp:TextBox ID="txtTASK" runat="server" Height="49px" TextMode="MultiLine" Width="99%" meta:resourcekey="txtTASKResource1" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc" style="width: 16.6%">
                        <asp:Label ID="lblNOTE" runat="server" Text="备注" meta:resourcekey="lblNOTEResource1"></asp:Label></td>
                    <td class="slim_table_td_control" colspan="3">
                        <asp:TextBox ID="txtNOTE" runat="server" TextMode="MultiLine" Width="99%" meta:resourcekey="txtNOTEResource1" ReadOnly="True"></asp:TextBox></td>
                    <td class="slim_table_td_desc" style="width: 16.6%">
                        <asp:Label ID="lblZXRQ" runat="server" Text="执行日期" meta:resourcekey="lblZXRQResource1"></asp:Label></td>
                    <td class="slim_table_td_control" style="width: 16.6%">
                        <cc1:WebDate ID="wdlZXRQ" runat="server" DateTimeStyle="Date" ButtonText=".." meta:resourcekey="wdlZXRQResource1" Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc" style="width: 16.6%">
                        <asp:Label ID="lblJHR" runat="server" Text="监护人" meta:resourcekey="lblJHRResource1"></asp:Label></td>
                    <td class="slim_table_td_control" style="width: 16.6%">
                        <cc1:HtmlComboBox ID="hcbJHR" runat="server" Width="99%" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbJHRResource1" Rows="1" SelectedText="" Enabled="False">
                        </cc1:HtmlComboBox></td>
                    <td class="slim_table_td_desc" style="width: 16.6%">
                        <asp:Label ID="lblSHR" runat="server" Text="审核人" meta:resourcekey="lblSHRResource1"></asp:Label></td>
                    <td class="slim_table_td_control" style="width: 16.6%">
                        <asp:TextBox ID="txtSHR" runat="server" MaxLength="20" Width="99%" meta:resourcekey="txtSHRResource1" ReadOnly="True"></asp:TextBox></td>
                    <td class="slim_table_td_desc" style="width: 16.6%">
                        <asp:Label ID="lblFLAG" runat="server" Text="状态" meta:resourcekey="lblFLAGResource1"></asp:Label></td>
                    <td class="slim_table_td_control" style="width: 16.6%">
                        <asp:DropDownList ID="ddlFLAG" runat="server" Width="99%" meta:resourcekey="ddlFLAGResource1" Enabled="False">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc" colspan="6">
                        <asp:Label ID="Label6" runat="server" Text="操作步骤" meta:resourcekey="Label6Resource1"></asp:Label></td>
                </tr>
                <tr>
                    <td class="slim_table_td_control" colspan="6">
                        <asp:GridView ID="grvList" runat="server" DataKeyNames="TID" Font-Names="宋体" Font-Size="9pt"
                            meta:resourcekey="grvListResource1" OnRowCancelingEdit="grvList_RowCancelingEdit"
                            OnRowEditing="grvList_RowEditing" OnRowUpdating="grvList_RowUpdating" OnSelectedIndexChanged="grvList_SelectedIndexChanged">
                            <RowStyle Height="25px" />
                            <Columns>
                                <asp:CommandField HeaderText="选择" meta:resourceKey="CommandFieldResource1" SelectText="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;"
                                    ShowSelectButton="True">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="单位" meta:resourceKey="TemplateFieldResource1">
                                    <EditItemTemplate>
                                        <cc1:HtmlComboBox ID="hcbUNIT" runat="server" EnableAutoFill="False" IsSupportedBrowser="True"
                                            MaxLength="0" meta:resourcekey="hcbUNITResource1" SelectedText="" Width="105px">
                                        </cc1:HtmlComboBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" Text='<%# Bind("UNIT") %>'
                                            Width="71px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="步骤" meta:resourceKey="TemplateFieldResource2">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtXH" runat="server" meta:resourcekey="txtXHResource1" Text='<%# Bind("XH") %>'
                                            Width="46px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" Text='<%# Bind("XH") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作内容" meta:resourceKey="TemplateFieldResource3">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCONTENT" runat="server" Height="49px" meta:resourcekey="txtCONTENTResource1"
                                            Text='<%# Bind("CONTENT") %>' TextMode="MultiLine" Width="501px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" Text='<%# Bind("CONTENT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="发令时间" meta:resourcekey="TemplateFieldResource4">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFLSJ" runat="server" Text='<%# Bind("FLSJ") %>' Width="39px" meta:resourcekey="txtFLSJResource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FLSJ") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="发令人" meta:resourcekey="TemplateFieldResource5">
                                    <EditItemTemplate>
                                        <cc1:HtmlComboBox ID="hcbFLR" runat="server" Width="86px" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbFLRResource1" SelectedText="">
                                        </cc1:HtmlComboBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("FLR") %>' meta:resourcekey="Label5Resource1"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="受令人" meta:resourcekey="TemplateFieldResource6">
                                    <EditItemTemplate>
                                        <cc1:HtmlComboBox ID="hcbSLR" runat="server" Width="89px" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbSLRResource1" SelectedText="">
                                        </cc1:HtmlComboBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("SLR") %>' meta:resourcekey="Label6Resource2"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="汇报时间" meta:resourcekey="TemplateFieldResource7">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSLSJ" runat="server" Text='<%# Bind("SLSJ") %>' Width="53px" meta:resourcekey="txtSLSJResource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("SLSJ") %>' meta:resourcekey="Label7Resource1"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>
                                <asp:CommandField CancelText="&lt;img border=0 align=absmiddle src=../img/quxiao.gif&gt;"
                                    EditText="&lt;img border=0 align=absmiddle src=../img/modifyFlag.gif&gt;" HeaderText="编辑"
                                    meta:resourceKey="CommandFieldResource2" ShowEditButton="True" UpdateText="&lt;img border=0 align=absmiddle src=../img/save.gif&gt;">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle Height="25px" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>

        <div id="detail_control"  style="width:90%">
            &nbsp;<asp:Button ID="btnAppendItem" runat="server" Enabled="False" OnClick="btnAppendItem_Click"
                Text="添加步骤" meta:resourcekey="btnAppendItemResource1" />&nbsp;<asp:Button ID="btnInsertItem" runat="server" Enabled="False"
                    OnClick="btnInsertItem_Click" Text="插入步骤" meta:resourcekey="btnInsertItemResource1" />&nbsp;<asp:Button ID="btnDeleteItem"
                        runat="server" Enabled="False" OnClick="btnDeleteItem_Click"
                        Text="删除步骤" meta:resourcekey="btnDeleteItemResource1" />
            <asp:Button ID="btnTypicalOpt" runat="server" Enabled="False" OnClick="btnTypicalOpt_Click"
                Text="典型票" meta:resourcekey="btnTypicalOptResource1" />
                <asp:TextBox ID="txtPACK_NO" runat="server" EnableTheming="False" Visible="False" Width="32px" meta:resourcekey="txtTIDResource1"></asp:TextBox>
            <asp:TextBox ID="txtTID" runat="server" EnableTheming="False" meta:resourcekey="txtTIDResource1"
                Visible="False" Width="32px"></asp:TextBox>&nbsp;
         </div>
        <div id="detail_info" runat="server"  style="width:90%">
        </div>
        <input id="refreshPage" runat="server" onpropertychange="javascript:form1.submit();"
            size="0" style="visibility: hidden; width: 68px" type="text" value="0" /> 
    </form>
</body>
</html>

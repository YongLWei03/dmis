<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDD_DISPATCH_COMMAND_FORM_Det.aspx.cs" Inherits="YW_DD_frmDD_DISPATCH_COMMAND_FORM_Det" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>综合命令票</title>
</head>
<body class="detail_body">
    <form id="form1" runat="server">
        <div id="detail_head">
             <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
        </div>
        <div id="detail_control" style=" text-align:left">
                &nbsp; &nbsp;
                <asp:Button id="btnSend" onclick="btnSend_Click" runat="server" meta:resourcekey="btnSendResource1" Text="提 交" Width="60px"></asp:Button>&nbsp;
                <asp:Button id="btnWithdraw" onclick="btnWithdraw_Click" runat="server" meta:resourcekey="btnWithdrawResource1" Text="退 回" Width="60px"></asp:Button>&nbsp;
                <asp:Button id="btnPrint" onclick="btnPrint_Click" runat="server" meta:resourcekey="btnPrintResource1" Text="打 印" Width="60px"></asp:Button>&nbsp;
                <asp:Button id="btnAccept" onclick="btnAccept_Click" runat="server" meta:resourcekey="btnAcceptResource1" Text="接 单" Width="60px"></asp:Button>&nbsp;
                <asp:Button id="btnSave" onclick="btnSave_Click" runat="server" meta:resourcekey="btnSaveResource1" Text="保 存" Width="60px"></asp:Button>&nbsp;
                <asp:Button id="btnSaveClose" onclick="btnSaveClose_Click" runat="server" meta:resourcekey="btnSaveCloseResource1" Text="保存返回" Width="61px"></asp:Button>
                &nbsp;
                <asp:Button id="btnClose" onclick="btnClose_Click" runat="server" meta:resourcekey="btnCloseResource1" Text="返 回" Width="60px"></asp:Button>
            <asp:TextBox ID="txtTID" runat="server" EnableTheming="False" Visible="False" Width="32px" meta:resourcekey="txtTIDResource1"></asp:TextBox>
            <asp:TextBox ID="txtPACK_NO" runat="server" EnableTheming="False" meta:resourcekey="txtTIDResource1"
                Visible="False" Width="32px"></asp:TextBox></div>
        <div id="detail_data">
            <br />
            <table class="slim_table">
                <tr>
                    <td class="slim_table_td_desc" >
                               <asp:Label ID="lblPH" runat="server" Text="编号" meta:resourcekey="lblSTATIONResource1"></asp:Label></td>
                    <td class="slim_table_td_control">
                               <asp:TextBox ID="txtPH" runat="server" meta:resourcekey="txtNUMResource1" ReadOnly="True"></asp:TextBox></td>
                    <td class="slim_table_td_desc" >
                        <asp:Label ID="lblSTATION" runat="server" Text="变电站" meta:resourcekey="lblSTATIONResource2"></asp:Label></td>
                    <td class="slim_table_td_control">
                        <cc1:HtmlComboBox ID="hcbSTATION" runat="server" Width="155px">
                        </cc1:HtmlComboBox></td>

                </tr>
                <tr>
                    <td class="slim_table_td_desc" style="height: 30px">
                        <asp:Label ID="lblYPR" runat="server" meta:resourcekey="lblYPRResource1" Text="拟票人"></asp:Label></td>
                    <td class="slim_table_td_control" style="height: 30px">
                        <asp:TextBox ID="txtYPR" runat="server" meta:resourcekey="txtYPRResource1" ReadOnly="True"></asp:TextBox></td>
                    <td class="slim_table_td_desc" style="height: 30px">
                        <asp:Label ID="lblDATEM" runat="server" meta:resourcekey="lblDATEMResource1" Text="拟票时间"></asp:Label></td>
                    <td class="slim_table_td_control" style="height: 30px">
                        <cc1:WebDate ID="wdlDATEM" runat="server" ButtonText=".." Enabled="False" meta:resourcekey="wdlDATEMResource1" />
                    </td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc">
                            <asp:Label ID="lblCOMMAND_CONTENT" runat="server" Text="命令内容" Width="76px" meta:resourcekey="lblACCIDENT_REASONResource1"></asp:Label></td>
                    <td class="slim_table_td_control" colspan="3">
                            <asp:TextBox ID="txtCOMMAND_CONTENT" runat="server" TextMode="MultiLine" Width="544px" Height="129px" meta:resourcekey="txtACCIDENT_REASONResource1" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc">
                            <asp:Label ID="lblCHECKER" runat="server" Text="审核人" meta:resourcekey="lblDISPATCHERResource1"></asp:Label></td>
                    <td class="slim_table_td_control">
                            <asp:TextBox ID="txtCHECKER" runat="server" meta:resourcekey="txtDISPATCHERResource1" ReadOnly="True"></asp:TextBox></td>
                    <td class="slim_table_td_desc">
                    </td>
                    <td class="slim_table_td_control">
                    </td>
                </tr>
                    <tr>
                        <td class="slim_table_td_desc" style="height: 30px">
                            <asp:Label ID="lblSTARTER" runat="server" Text="发令人" meta:resourcekey="lblSWITCH_LINE_NUMBERResource1"></asp:Label></td>
                        <td class="slim_table_td_control" style="height: 30px">
                            <asp:TextBox ID="txtSTARTER" runat="server" meta:resourcekey="txtSWITCH_LINE_NUMBERResource1" ReadOnly="True"></asp:TextBox></td>
                        <td class="slim_table_td_desc" style="height: 30px">
                               <asp:Label ID="lblSTART_TIME" runat="server" Text="发令时间" meta:resourcekey="lblSTARTTIMEResource1"></asp:Label></td>
                        <td class="slim_table_td_control" style="height: 30px">
                            <cc1:WebDate ID="wdlFLSJ" runat="server" ButtonText=".." meta:resourcekey="wdlSTARTTIMEResource1" Enabled="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="slim_table_td_desc" style="height: 30px">
                            <asp:Label ID="lblACCEPT_UNIT" runat="server" Text="受令单位" meta:resourcekey="lblLOSSESResource1"></asp:Label></td>
                        <td class="slim_table_td_control" style="height: 30px">
                            <cc1:HtmlComboBox ID="hcbACCEPT_UNIT" runat="server" EnableAutoFill="False" IsSupportedBrowser="True"
                                MaxLength="0" meta:resourcekey="hcbACCEPT_UNITResource1" Rows="1" SelectedText="" Enabled="False" Width="159px">
                            </cc1:HtmlComboBox></td>
                        <td class="slim_table_td_desc" style="height: 30px">
                            <asp:Label ID="lblACCEPTER" runat="server" Text="受令人" meta:resourcekey="lblENDTIMEResource1"></asp:Label></td>
                        <td class="slim_table_td_control" style="height: 30px">
                               <asp:TextBox ID="txtACCEPTER" runat="server" meta:resourcekey="txtLOSSESResource1" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                <tr>
                    <td class="slim_table_td_desc">
                            <asp:Label ID="lblREPLY_TIME" runat="server" meta:resourcekey="lblREPLY_TIMEResource1"
                                Text="回令时间"></asp:Label></td>
                    <td class="slim_table_td_control">
                            <cc1:WebDate ID="wdlREPLY_TIME" runat="server" ButtonText=".." meta:resourcekey="wdlENDTIMEResource1" Enabled="False" />
                    </td>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblFLAG" runat="server" meta:resourcekey="lblFLAGResource1" Text="状态"></asp:Label></td>
                    <td class="slim_table_td_control">
                        <asp:DropDownList ID="ddlFLAG" runat="server" Enabled="False" meta:resourcekey="ddlFLAGResource1"
                            Width="154px">
                        </asp:DropDownList></td>
                </tr>
            </table>
            <br />
        </div>
        <div id="detail_control">
            &nbsp;&nbsp;
        </div>
        <div id="detail_info" runat="server">
        </div>
        <input id="refreshPage" runat="server" onpropertychange="javascript:form1.submit();"
            size="0" style="visibility: hidden" type="text" value="0" />
    </form>
</body>
</html>

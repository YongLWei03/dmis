<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDD_POWERGRID_LIMIT_ELECTRIC_Det.aspx.cs" Inherits="YW_DD_frmDD_POWERGRID_LIMIT_ELECTRIC_Det" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>电网限电记录</title>
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
                    <asp:Label ID="lblSTATION" runat="server" Text="站名" meta:resourcekey="lblNUMResource1"></asp:Label></td>
                <td class="slim_table_td_control" >
                    <cc1:HtmlComboBox ID="hcbSTATION" runat="server" EnableAutoFill="False" IsSupportedBrowser="True"
                        MaxLength="0" meta:resourcekey="hcbSTATIONResource1" Rows="1" SelectedText="">
                    </cc1:HtmlComboBox></td>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblLINE_NO" runat="server" Text="线路名称" meta:resourcekey="lblWRITE_TIMEResource1"></asp:Label></td>
                <td class="slim_table_td_control" >
                    <asp:TextBox ID="txtLINE_NO" runat="server" meta:resourcekey="txtOVERHAUL_SHEET_NOResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblREASON" runat="server" Text="限电原因" meta:resourcekey="lblTASKResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3" >
                    <asp:TextBox ID="txtREASON" runat="server" TextMode="MultiLine" Height="57px" Width="560px" meta:resourcekey="txtTASKResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblSTARTTIME" runat="server" Text="停电时间" meta:resourcekey="lblRUN_TIMEResource1"></asp:Label></td>
                <td class="slim_table_td_control">
                    <cc1:WebDate ID="wdlSTARTTIME" runat="server" ButtonText=".." meta:resourcekey="wdlRUN_TIMEResource1" DateStyle="DateFormat3" myDateWidth="80px" />
                </td>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblLOADS" runat="server" meta:resourcekey="lblOVERHAUL_SHEET_NOResource1"
                        Text="负荷"></asp:Label></td>
                <td class="slim_table_td_control">
                    <asp:TextBox ID="txtLOADS" runat="server" meta:resourcekey="txtOVERHAUL_SHEET_NOResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc" style="height: 25px">
                    <asp:Label ID="lblSTATER" runat="server" Text="发令人" meta:resourcekey="lblSTATERResource1"></asp:Label></td>
                <td class="slim_table_td_control" style="height: 25px">
                    <asp:TextBox ID="txtSTARTER" runat="server" meta:resourcekey="txtSTATERResource1"></asp:TextBox></td>
                <td class="slim_table_td_desc" style="height: 25px">
                    <asp:Label ID="lblACCEPTER" runat="server" Text="受令人" meta:resourcekey="lblACCEPTERResource1"></asp:Label></td>
                <td class="slim_table_td_control" style="height: 25px">
                    <asp:TextBox ID="txtACCEPTER" runat="server" meta:resourcekey="txtACCEPTERResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc" style="height: 17px" >
                    <asp:Label ID="lblENDTIME" runat="server" Text="送电时间" meta:resourcekey="lblNOTEResource1"></asp:Label></td>
                <td class="slim_table_td_control" style="height: 17px" ><cc1:WebDate ID="wdlENDTIME" runat="server" ButtonText=".." meta:resourcekey="wdlRUN_TIMEResource1" DateStyle="DateFormat3" myDateWidth="80px" /></td>
                <td class="slim_table_td_desc" style="height: 17px" >
                    <asp:Label ID="lblRESTORE_STARTER" runat="server" Text="送电发令人" meta:resourcekey="lblWRITERResource1"></asp:Label></td>
                <td class="slim_table_td_control" style="height: 17px" >
                    <asp:TextBox ID="txtRESTORE_STARTER" runat="server" meta:resourcekey="txtWRITERResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc" style="height: 30px">
                    <asp:Label ID="lblCHECKER" runat="server" Text="送电受令人" meta:resourcekey="lblCHECKERResource1"></asp:Label></td>
                <td class="slim_table_td_control" style="height: 30px">
                    <asp:TextBox ID="txtRESTOTR_ACCEPTER" runat="server" Width="149px" meta:resourcekey="txtCHECKERResource1"></asp:TextBox></td>
                <td class="slim_table_td_desc" style="height: 30px">
                </td>
                <td class="slim_table_td_control" style="height: 30px">
                </td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    &nbsp;<asp:Label ID="lblSTOP_HOURS" runat="server" Text="停电小时数" meta:resourcekey="lblEVALUATIONResource1"></asp:Label></td>
                <td class="slim_table_td_control">
                    <asp:TextBox ID="txtSTOP_HOURS" runat="server" Enabled="False" meta:resourcekey="txtACCEPTERResource1"></asp:TextBox></td>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblLOSSES" runat="server" Text="损失电量" meta:resourcekey="lblGUARDIANResource1"></asp:Label></td>
                <td class="slim_table_td_control">
                    <asp:TextBox ID="txtLOSSES" runat="server" Width="145px" meta:resourcekey="txtGUARDIANResource1"></asp:TextBox></td>
            </tr>
        </table>
    </div>
    <div id="detail_control">
        <br />
        <asp:TextBox id="txtTYPE" runat="server" meta:resourcekey="txtTIDResource1" Width="32px" Visible="False" EnableTheming="False"></asp:TextBox>
        <asp:TextBox ID="txtPACKNO" runat="server" EnableTheming="False" Visible="False" Width="32px" meta:resourcekey="txtPACKNOResource1"></asp:TextBox>
        <asp:TextBox ID="txtTID" runat="server" EnableTheming="False" Visible="False" Width="32px" meta:resourcekey="txtTIDResource2"></asp:TextBox>
        <asp:Button ID="btnSave" runat="server" Enabled="False" OnClick="btnSave_Click" Text="保存" meta:resourcekey="btnSaveResource1" />&nbsp;
        <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="返回" meta:resourcekey="btnReturnResource1" />&nbsp;
    </div>
    <div id="detail_info" runat="server">
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmGL_ANTI_ACCIDENT_EXERCISE_Det.aspx.cs" Inherits="YW_GL_frmGL_ANTI_ACCIDENT_EXERCISE_Det" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>反事故演习</title>
</head>
<body class="detail_body">
    <form id="form1" runat="server">
    <div id="detail_head">
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1" ></asp:Label></div>
    <div id="detail_data">
        <br />
        <table class="slim_table">
            <tr>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblUNIT" runat="server" Text="单位" meta:resourcekey="lblUNITResource1" ></asp:Label></td>
                <td class="slim_table_td_control" >
                    <asp:TextBox ID="txtUNIT" runat="server" meta:resourcekey="txtUNITResource1" ></asp:TextBox></td>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblDATEM" runat="server" Text="演习时间" meta:resourcekey="lblDATEMResource1" ></asp:Label></td>
                <td class="slim_table_td_control" >
                    <cc1:WebDate ID="wdlDATEM" runat="server" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlDATEMResource1" />
                </td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblLEADER" runat="server" Text="领导人" meta:resourcekey="lblLEADERResource1" ></asp:Label></td>
                <td class="slim_table_td_control">
                    <asp:TextBox ID="txtLEADER" runat="server" meta:resourcekey="txtLEADERResource1" ></asp:TextBox></td>
                <td class="slim_table_td_desc">
                </td>
                <td class="slim_table_td_control">
                </td>
            </tr>
            <tr>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblATTENDEE" runat="server" Text="参加人员" meta:resourcekey="lblATTENDEEResource1" ></asp:Label></td>
                <td class="slim_table_td_control" colspan="3" >
                    <asp:TextBox ID="txtATTENDEE" runat="server" TextMode="MultiLine" Height="57px" Width="98%" meta:resourcekey="txtATTENDEEResource1" ></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblCONTENT" runat="server"  Text="演习内容" meta:resourcekey="lblCONTENTResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtCONTENT" runat="server" Height="164px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtCONTENTResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblANALYSIS" runat="server"  Text="分析判断" meta:resourcekey="lblANALYSISResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtANALYSIS" runat="server" Height="111px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtANALYSISResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblPROCESSING_METHOD" runat="server" 
                        Text="处理步骤" meta:resourcekey="lblPROCESSING_METHODResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtPROCESSING_METHOD" runat="server" Height="130px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtPROCESSING_METHODResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    &nbsp;<asp:Label ID="lblEVALUATION" runat="server" Text="评价" meta:resourcekey="lblEVALUATIONResource1" ></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtEVALUATION" runat="server" Height="38px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtEVALUATIONResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblCHECKER" runat="server" Text="评价人" meta:resourcekey="lblCHECKERResource1" ></asp:Label></td>
                <td class="slim_table_td_control">
                    <asp:TextBox ID="txtCHECKER" runat="server" Width="149px" meta:resourcekey="txtCHECKERResource1" ></asp:TextBox></td>
                <td class="slim_table_td_desc">
                    </td>
                <td class="slim_table_td_control">
                    </td>
            </tr>
        </table>
    </div>
    <div id="detail_control">
        <br />
        <asp:TextBox id="txtDEPART_ID" runat="server" Width="32px" Visible="False" EnableTheming="False" meta:resourcekey="txtDEPART_IDResource1"></asp:TextBox>&nbsp;
        <asp:TextBox ID="txtTID" runat="server" EnableTheming="False" Visible="False" Width="32px" meta:resourcekey="txtTIDResource1" ></asp:TextBox>
        <asp:Button ID="btnSave" runat="server" Enabled="False" OnClick="btnSave_Click" Text="保存" meta:resourcekey="btnSaveResource1"  />&nbsp;
        <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="返回" meta:resourcekey="btnReturnResource1"  />&nbsp;
    </div>
    <div id="detail_info" runat="server">
    </div>
    </form>
</body>
</html>

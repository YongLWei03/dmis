<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmGL_SAFE_ACTIVITY_Det.aspx.cs" Inherits="YW_GL_frmGL_SAFE_ACTIVITY_Det" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>安全活动记录</title>
</head>
<body class="detail_body">
    <form id="form1" runat="server">
    <div id="detail_head">
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1" ></asp:Label></div>
    <div id="detail_data">
        <br />
        <table class="slim_table">
            <tr>
                <td class="slim_table_td_desc" style="height: 30px" >
                    <asp:Label ID="lblDATEM" runat="server" Text="时间" meta:resourcekey="lblDATEMResource1" ></asp:Label></td>
                <td class="slim_table_td_control" style="height: 30px" >
                    <cc1:WebDate ID="wdlDATEM" runat="server" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlDATEMResource1" />
                </td>
                <td class="slim_table_td_desc" style="height: 30px" >
                    <asp:Label ID="lblLOCATION" runat="server" Text="地点" meta:resourcekey="lblLOCATIONResource1"></asp:Label></td>
                <td class="slim_table_td_control" style="height: 30px" >
                    <asp:TextBox ID="txtLOCATION" runat="server" meta:resourcekey="txtLOCATIONResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc" style="height: 30px">
                    <asp:Label ID="lblEMCEE" runat="server" Text="主持人" meta:resourcekey="lblEMCEEResource1" ></asp:Label></td>
                <td class="slim_table_td_control" style="height: 30px">
                    <asp:TextBox ID="txtEMCEE" runat="server" meta:resourcekey="txtEMCEEResource1" ></asp:TextBox></td>
                <td class="slim_table_td_desc" style="height: 30px">
                    <asp:Label ID="lblRECORDER" runat="server" Text="记录人" meta:resourcekey="lblRECORDERResource1"></asp:Label></td>
                <td class="slim_table_td_control" style="height: 30px">
                    <asp:TextBox ID="txtRECORDER" runat="server" meta:resourcekey="txtRECORDERResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblATTENDEE" runat="server" Text="参加人员" meta:resourcekey="lblATTENDEEResource1" ></asp:Label></td>
                <td class="slim_table_td_control" colspan="3" >
                    <asp:TextBox ID="txtATTENDEE" runat="server" TextMode="MultiLine" Height="57px" Width="98%" meta:resourcekey="txtATTENDEEResource1" ></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblCONTENT" runat="server"  Text="活动内容" meta:resourcekey="lblCONTENTResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtCONTENT" runat="server" Height="164px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtCONTENTResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblQUESTION" runat="server"  Text="发现问题" meta:resourcekey="lblQUESTIONResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtQUESTION" runat="server" Height="111px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtQUESTIONResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblCORRECT_ACTIVITIES" runat="server" 
                        Text="整改措施" meta:resourcekey="lblCORRECT_ACTIVITIESResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtCORRECT_ACTIVITIES" runat="server" Height="85px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtCORRECT_ACTIVITIESResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    &nbsp;<asp:Label ID="lblPERSON_STATEMENT" runat="server" Text="个人发言" meta:resourcekey="lblPERSON_STATEMENTResource1" ></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtPERSON_STATEMENT" runat="server" Height="68px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtPERSON_STATEMENTResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblCHECKER" runat="server" Text="审核人" meta:resourcekey="lblCHECKERResource1" ></asp:Label></td>
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

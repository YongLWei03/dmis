<%@ page language="C#" autoeventwireup="true" inherits="YW_GL_frmGL_ACCIDENT_FORECAST_Det, App_Web_yi4rgbqt" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>事故预想记录</title>
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
                    <asp:Label ID="lblDATEM" runat="server" Text="预想日期" meta:resourcekey="lblDATEMResource1" ></asp:Label></td>
                <td class="slim_table_td_control" >
                    <cc1:WebDate ID="wdlDATEM" runat="server" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." DateTimeStyle="Date" meta:resourcekey="wdlDATEMResource1" />
                </td>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblFORECASTER" runat="server" Text="领导人" meta:resourcekey="lblFORECASTERResource1" ></asp:Label></td>
                <td class="slim_table_td_control" >
                    <asp:TextBox ID="txtFORECASTER" runat="server" meta:resourcekey="txtFORECASTERResource1" ></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblSUBJECT" runat="server" Text="事故简题" meta:resourcekey="lblSUBJECTResource1" ></asp:Label></td>
                <td class="slim_table_td_control" colspan="3" >
                    <asp:TextBox ID="txtSUBJECT" runat="server" TextMode="MultiLine" Height="57px" Width="98%" meta:resourcekey="txtSUBJECTResource1" ></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblRUN_MODE" runat="server"  Text="运行方式" meta:resourcekey="lblRUN_MODEResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtRUN_MODE" runat="server" Height="130px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtRUN_MODEResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblFAULT_APPEARANCE" runat="server"  Text="故障现象" meta:resourcekey="lblFAULT_APPEARANCEResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtFAULT_APPEARANCE" runat="server" Height="111px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtFAULT_APPEARANCEResource1"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblPROCESSING_METHOD" runat="server" 
                        Text="处理方法" meta:resourcekey="lblPROCESSING_METHODResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtPROCESSING_METHOD" runat="server" Height="130px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtPROCESSING_METHODResource1"></asp:TextBox></td>
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

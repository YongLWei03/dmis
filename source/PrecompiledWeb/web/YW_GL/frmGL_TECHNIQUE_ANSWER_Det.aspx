<%@ page language="C#" autoeventwireup="true" inherits="YW_GL_frmGL_TECHNIQUE_ANSWER_Det, App_Web_yi4rgbqt" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>技术问答</title>
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
                    <asp:Label ID="lblDATEM" runat="server" Text="日期" meta:resourcekey="lblDATEMResource1" ></asp:Label></td>
                <td class="slim_table_td_control" >
                    <cc1:WebDate ID="wdlDATEM" runat="server" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." DateTimeStyle="Date" meta:resourcekey="wdlDATEMResource1" />
                </td>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblEXAMINER" runat="server" Text="出题人" meta:resourcekey="lblEXAMINERResource1" ></asp:Label></td>
                <td class="slim_table_td_control" >
                    <asp:TextBox ID="txtEXAMINER" runat="server" meta:resourcekey="txtEXAMINERResource1" ></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblANSWERER" runat="server" Text="解答人" meta:resourcekey="lblANSWERERResource1" ></asp:Label></td>
                <td class="slim_table_td_control">
                    <asp:TextBox ID="txtANSWERER" runat="server" meta:resourcekey="txtANSWERERResource1" ></asp:TextBox></td>
                <td class="slim_table_td_desc">
                </td>
                <td class="slim_table_td_control">
                </td>
            </tr>
            <tr>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblTOPIC" runat="server" Text="题目" meta:resourcekey="lblTOPICResource1" ></asp:Label></td>
                <td class="slim_table_td_control" colspan="3" >
                    <asp:TextBox ID="txtTOPIC" runat="server" TextMode="MultiLine" Height="57px" Width="98%" meta:resourcekey="txtTOPICResource1" ></asp:TextBox></td>
            </tr>
            <tr>
                <td class="slim_table_td_desc">
                    <asp:Label ID="lblCONTENT" runat="server"  Text="解答内容" meta:resourcekey="lblCONTENTResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtCONTENT" runat="server" Height="164px" 
                        TextMode="MultiLine" Width="98%" meta:resourcekey="txtCONTENTResource1"></asp:TextBox></td>
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
                    <asp:Label ID="lblAPPRAISER" runat="server" Text="评价人" meta:resourcekey="lblAPPRAISERResource1" ></asp:Label></td>
                <td class="slim_table_td_control">
                    <asp:TextBox ID="txtAPPRAISER" runat="server" Width="149px" meta:resourcekey="txtAPPRAISERResource1" ></asp:TextBox></td>
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

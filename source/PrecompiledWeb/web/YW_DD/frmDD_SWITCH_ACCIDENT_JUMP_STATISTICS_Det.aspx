<%@ page language="C#" autoeventwireup="true" inherits="YW_DD_frmDD_SWITCH_ACCIDENT_JUMP_STATISTICS_Det, App_Web_docfbltz" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>开关事故跳闸统计</title>
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
                           <asp:Label ID="lblDATEM" runat="server" Text="日期" meta:resourcekey="lblSTARTTIMEResource1"></asp:Label></td>
                <td class="slim_table_td_control">
                        <cc1:WebDate ID="wdlDATEM" runat="server" ButtonText=".." meta:resourcekey="wdlSTARTTIMEResource1" DateStyle="DateFormat3" DateTimeStyle="Date" />
                </td>
                <td class="slim_table_td_desc" >
                </td>
                <td class="slim_table_td_control">
                </td>
            </tr>
                <tr>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblSTATION" runat="server" Text="变电站" meta:resourcekey="lblSWITCH_LINE_NUMBERResource1"></asp:Label></td>
                    <td class="slim_table_td_control">
                        <asp:TextBox ID="txtSTATION" runat="server" meta:resourcekey="txtSWITCH_LINE_NUMBERResource1"></asp:TextBox></td>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblSWITCH_NUM" runat="server" Text="开关编号" meta:resourcekey="lblLOSSESResource1"></asp:Label></td>
                    <td class="slim_table_td_control">
                           <asp:TextBox ID="txtSWITCH_NUM" runat="server" meta:resourcekey="txtSWITCH_NUMResource1"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblPROTECT_ACTION_DESC" runat="server" Text="保护动作描述" Width="76px" meta:resourcekey="lblPROTECT_ACTION_DESCResource1"></asp:Label></td>
                    <td class="slim_table_td_control" colspan="3">
                        <asp:TextBox ID="txtPROTECT_ACTION_DESC" runat="server" TextMode="MultiLine" Width="544px" Height="78px" meta:resourcekey="txtACCIDENT_REASONResource1"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc">
                           <asp:Label ID="lblRESAON" runat="server" meta:resourcekey="lblRESAONResource1"
                               Text="动作时间" Width="76px"></asp:Label></td>
                    <td class="slim_table_td_control">
                           <cc1:WebDate ID="wdlACTION_TIME" runat="server" ButtonText=".." meta:resourcekey="wdlSTARTTIMEResource1" DateStyle="DateFormat3" />
                    </td>
                    <td class="slim_table_td_desc">
                           <asp:Label ID="lblALL_ACTION_TIMES" runat="server" meta:resourcekey="lblACCIDENT_REASONResource1"
                               Text="累计动作次数" Width="76px"></asp:Label></td>
                    <td class="slim_table_td_control">
                           <asp:TextBox ID="txtALL_ACTION_TIMES" runat="server" meta:resourcekey="txtALL_ACTION_TIMESResource1"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblNOTE" runat="server" meta:resourcekey="lblDISPATCHERResource1"
                            Text="备注"></asp:Label></td>
                    <td class="slim_table_td_control" colspan="3">
                           <asp:TextBox ID="txtNOTE" runat="server" Height="103px" meta:resourcekey="txtACCIDENT_REASONResource1"
                               TextMode="MultiLine" Width="544px"></asp:TextBox></td>
                </tr>
            </table>
            <br />
        </div>

        <div id="detail_control">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" Enabled="False" meta:resourcekey="btnSaveResource1" />
            <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="返回" meta:resourcekey="btnReturnResource1" />
            <asp:TextBox ID="txtTID" runat="server" EnableTheming="False" Visible="False" Width="32px" meta:resourcekey="txtTIDResource1"></asp:TextBox>
         </div>
        <div id="detail_info" runat="server">
        </div>    
    </form>
</body>
</html>

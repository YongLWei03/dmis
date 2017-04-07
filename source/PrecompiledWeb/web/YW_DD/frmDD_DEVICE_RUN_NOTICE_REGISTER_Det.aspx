<%@ page language="C#" autoeventwireup="true" inherits="YW_DD_frmDD_DEVICE_RUN_NOTICE_REGISTER_Det, App_Web_docfbltz" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>投运通知单</title>
</head>
<body style="text-align: center; background-color: aliceblue; margin-top: 0px; padding-left: 3px; padding-top: 2px;">
    <form id="form1" runat="server">
    <div>
        <table id="TABLE1" runat="server" style="width: 660px; height: 526px">
            <tr>
                <td align="center" style="background-image: url(../img/main_bar.jpg); height: 18px;
                    text-align: left">
                    <span style="font-size: 10pt; color: #3300cc">
                        <img src="../img/s_img.gif" style="font-weight: bold" />
                        <span style="color: #000000">
                            <asp:Label ID="lblFuncName" runat="server" Text="投运通知单"></asp:Label></span></span></td>
            </tr>
            <tr style="color: #000000">
                <td align="center" style="height: 18px; text-align: left">
                    &nbsp;<asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="提 交"
                        Width="58px" />&nbsp;
                    <asp:Button ID="btnWithdraw" runat="server" OnClick="btnWithdraw_Click"
                        Text="退 回" Width="54px" />&nbsp;
                    <asp:Button ID="btnAccept" runat="server" OnClick="btnAccept_Click"
                        Text="接 单" Width="52px" />&nbsp;
                    <asp:Button ID="bntPrint" runat="server" OnClick="bntPrint_Click" Text="打印" Width="54px" />
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保 存" Width="56px" />
                    <asp:Button ID="btnSaveClose" runat="server" OnClick="btnSaveClose_Click" Text="保存返回"
                        Width="62px" />
                    <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="返 回" Width="52px" /></td>
            </tr>
            <tr style="color: #000000">
                <td align="center" style="height: 89px">
                    <table border="0" cellpadding="0" cellspacing="1" style="width: 630px; height: 207px;
                        background-color: #000000">
                        <tr>
                            <td align="center" colspan="1" style="height: 20px; background-color: skyblue" valign="middle">
                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="10pt" Text="方式"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 188px; background-color: white">
                                <table border="0" cellpadding="0" cellspacing="1" style="width: 600px; height: 182px">
                                    <tr>
                                        <td align="center" style="font-size: 9pt; width: 173px; height: 16px">
                                            <asp:Label ID="Label1" runat="server" Text="投运单编号"></asp:Label></td>
                                        <td align="left" colspan="2" style="width: 164px; height: 16px">
                                            <asp:TextBox ID="txtPH" runat="server" BackColor="LightGray" ReadOnly="True" Width="170px"></asp:TextBox></td>
                                        <td align="center" style="font-size: 12px; width: 117px; height: 16px">
                                            <asp:Label ID="Label2" runat="server" Text="变电站"></asp:Label></td>
                                        <td colspan="2" style="height: 16px; text-align: left;">
                                            <cc1:HtmlComboBox ID="hcbSTATION" runat="server" Enabled="False">
                                            </cc1:HtmlComboBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 9pt; width: 173px; height: 56px">
                                            <asp:Label ID="Label3" runat="server" Text="新建、改建设备名称"></asp:Label></td>
                                        <td colspan="5" style="height: 56px; text-align: left">
                                            <asp:TextBox ID="txtDEVICE_NAME" runat="server" BackColor="LightGray" Height="61px"
                                                ReadOnly="True" TextMode="MultiLine" Width="468px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 12px; width: 173px; height: 18px">
                                            <asp:Label ID="Label4" runat="server" Text="施工单位" Width="72px"></asp:Label></td>
                                        <td colspan="2" style="width: 164px; height: 18px; text-align: left"><cc1:HtmlComboBox ID="hcbBUILDER" runat="server" Enabled="False" Width="199px">
                                        </cc1:HtmlComboBox></td>
                                        <td align="center" style="font-size: 12px; width: 117px; height: 18px">
                                            <asp:Label ID="Label5" runat="server" Text="启动联系人及电话"></asp:Label></td>
                                        <td align="center" colspan="2" style="height: 18px">
                                            <asp:TextBox ID="txtLINKNAME" runat="server" BackColor="LightGray" ReadOnly="True"
                                                Width="171px"></asp:TextBox>&nbsp;<strong></strong></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 12px; width: 173px; height: 25px">
                                            <asp:Label ID="Label6" runat="server" Text="申请投运日期" Width="88px"></asp:Label></td>
                                        <td colspan="2" style="width: 164px; height: 25px; text-align: left">
                                            <cc1:WebDate ID="wdlSTARTDATE" runat="server" Enabled="False" DateTimeStyle="Date" Width="144px" DateStyle="DateFormat3" />
                                        </td>
                                        <td align="center" style="font-size: 12px; width: 117px; height: 25px">
                                            <asp:Label ID="Label7" runat="server" Text="批准投运日期" Width="98px"></asp:Label></td>
                                        <td align="center" colspan="2" style="height: 25px; text-align: left">
                                            <cc1:WebDate ID="wdlENDDATE" runat="server" Enabled="False" DateTimeStyle="Date" Width="144px" DateStyle="DateFormat3" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                                            <asp:TextBox ID="txtPACK_NO"
                                                    runat="server" BackColor="LightGray" EnableTheming="False" ReadOnly="True" Visible="False"
                                                    Width="14px"></asp:TextBox>
                    <asp:TextBox ID="txtTID" runat="server" BackColor="LightGray" EnableTheming="False"
                        ReadOnly="True" Visible="False" Width="14px"></asp:TextBox></td>
            </tr>
            <tr style="color: #000000">
                <td align="center" style="height: 75px">
                    <table border="0" cellpadding="0" cellspacing="1" style="width: 630px; height: 167px;
                        background-color: #000000">
                        <tr>
                            <td align="center" colspan="1" style="height: 19px; background-color: paleturquoise"
                                valign="middle">
                                <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="10pt" Text="调度"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 601px; height: 141px; background-color: white">
                                <table border="0" cellpadding="0" cellspacing="1" style="width: 600px; height: 167px;">
                                    <tr>
                                        <td align="center" style="font-size: 12px; width: 126px; height: 23px">
                                            <asp:Label ID="Label8" runat="server" Text="投运单接收登记人"></asp:Label></td>
                                        <td colspan="2" style="height: 23px; text-align: left">
                                            <cc1:HtmlComboBox ID="hcbACCEPTER" runat="server" Enabled="False" Width="143px">
                                            </cc1:HtmlComboBox></td>
                                        <td align="center" style="font-size: 12px; width: 100px; height: 23px">
                                            <asp:Label ID="Label9" runat="server" Text="投运调度员" Width="76px"></asp:Label></td>
                                        <td colspan="2" style="height: 23px; text-align: left;">
                                            <cc1:HtmlComboBox ID="hcbDISPATCHER" runat="server" Enabled="False" Width="141px">
                                            </cc1:HtmlComboBox>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 12px; width: 126px; height: 30px">
                                            <asp:Label ID="Label10" runat="server" Text="处理内容" Width="80px"></asp:Label></td>
                                        <td colspan="5" style="height: 30px; text-align: left">
                                            <asp:TextBox ID="txtDISPATCH_CONTENT" runat="server" BackColor="LightGray" Height="48px" ReadOnly="True"
                                                TextMode="MultiLine" Width="468px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 12px; width: 126px; height: 20px">
                                            <asp:Label ID="Label11" runat="server" Text="是否改图" Width="82px"></asp:Label></td>
                                        <td colspan="2" style="height: 20px; text-align: left">
                                            <asp:DropDownList ID="ddlIS_ALTER_DRAWING" runat="server" Enabled="False" Width="148px">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>是</asp:ListItem>
                                                <asp:ListItem>否</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td align="center" style="font-size: 12px; width: 100px; height: 20px">
                                            <asp:Label ID="Label12" runat="server" Text="实际投运日期" Width="96px"></asp:Label></td>
                                        <td colspan="2" style="height: 20px; text-align: left;">
                                            <cc1:WebDate ID="wdlRUN_DATE" runat="server" Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 12px; width: 126px; height: 32px">
                                            <asp:Label ID="Label13" runat="server" Text="现场验收人" Width="86px"></asp:Label></td>
                                        <td colspan="2" style="height: 32px; text-align: left">
                                            <asp:TextBox ID="txtSITE_EXAMINER" runat="server" BackColor="LightGray" ReadOnly="True" Width="144px"></asp:TextBox></td>
                                        <td align="center" style="font-size: 12px; width: 100px; height: 32px">
                                        </td>
                                        <td colspan="2" style="height: 32px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="color: #000000">
                <td align="center" style="height: 13px">
                </td>
            </tr>
            <tr style="color: #000000">
                <td id="tdMessage" runat="server" align="left" style="font-size: 12px; color: red;
                    height: 19px">
                </td>
            </tr>
        </table>
    </div>
    <input type="text" id="refreshPage" value="0" runat="server"  onpropertychange="javascript:form1.submit();" size="0" style="VISIBILITY: hidden; width: 54px;"/>
    <input type="hidden" id="fileid" runat="server" value="-1" style="width: 51px" />
    </form>
</body>
</html>

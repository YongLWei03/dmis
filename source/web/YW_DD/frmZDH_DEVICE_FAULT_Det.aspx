<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmZDH_DEVICE_FAULT_Det.aspx.cs" Inherits="YW_ZDH_frmZDH_DEVICE_FAULT_Det" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>自动化设备缺陷</title>
</head>
<body style="text-align: center; background-color: aliceblue; margin-top: 0px; padding-left: 3px; padding-top: 2px;">
    <form id="form1" runat="server">
    <div>
        <table style="width: 696px; height: 474px" id="TABLE1" runat="server">
            <tr>
                <td align="center" style="background-image: url(../img/main_bar.jpg); height: 18px;
                    text-align: left; width: 646px;">
                    <span style="font-size: 10pt; color: #3300cc">
                        <img src="../img/s_img.gif" style="font-weight: bold" />
                        <span style="color: #000000">
                            <asp:Label ID="lblFuncName" runat="server" Text="自动化设备缺陷" meta:resourcekey="lblFuncNameResource1"></asp:Label></span></span></td>
            </tr>
            <tr style="color: #000000">
                <td align="center" style="height: 14px; text-align: left; width: 646px;">
                    &nbsp; &nbsp;<asp:Button ID="btnSend" runat="server"
                        Text="提 交" Width="56px" OnClick="btnSend_Click" meta:resourcekey="btnSendResource1" />&nbsp;
                    <asp:Button ID="btnWithdraw" runat="server" OnClick="btnWithdraw_Click"
                        Text="退 回" Width="56px" meta:resourcekey="btnWithdrawResource1" />
                    <asp:Button ID="bntPrint" runat="server" Text="打印" Width="56px" OnClick="bntPrint_Click" meta:resourcekey="bntPrintResource1" />&nbsp;
                    &nbsp;<asp:Button ID="btnAccept" runat="server" Text="接 单" Width="52px" OnClick="btnAccept_Click" meta:resourcekey="btnAcceptResource1" />&nbsp;
                     <asp:Button ID="btnSave" runat="server" Text="保 存" Width="52px" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                     <asp:Button ID="btnSaveClose" runat="server" Text="保存返回" Width="60px" OnClick="btnSaveClose_Click" meta:resourcekey="btnSaveCloseResource1" />
                     <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click"
                        Text="返 回" Width="56px" meta:resourcekey="btnCloseResource1" />&nbsp;
                    <asp:TextBox ID="txtPACK_NO" runat="server" BackColor="LightGray" EnableTheming="False"
                        ReadOnly="True" Visible="False" Width="14px" meta:resourcekey="txtPACK_NOResource1"></asp:TextBox>
                    <asp:TextBox ID="txtTID" runat="server" BackColor="LightGray" EnableTheming="False"
                        ReadOnly="True" Visible="False" Width="14px" meta:resourcekey="txtTIDResource1"></asp:TextBox>
                                </td>
            </tr>
            <tr style="color: #000000">
                <td align="center" style="height: 70px; width: 646px;">
                    <table border="0" cellpadding="0" cellspacing="1" style="height: 157px; background-color: #000000; width: 700px;">
                        <tr>
                            <td align="center" colspan="1" style="font-size: 9pt; height: 21px; background-color: skyblue"
                                valign="middle">
                                <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Size="10pt" Text="填 写 内 容" meta:resourcekey="Label23Resource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="center" style="background-color: white; height: 153px;">
                    <table border="0" cellpadding="0" cellspacing="1" style="width: 648px; height: 140px;">
                        <tr>
                            <td style="width: 125px; height: 13px; font-size: 12pt;" align="center">
                                &nbsp;<asp:Label ID="lblDD_PH" runat="server" Font-Size="9pt" Text="编号" Width="92px" meta:resourcekey="lblDD_PHResource1"></asp:Label></td>
                            <td colspan="2" style="height: 13px; width: 164px;" align="left">
                                <asp:TextBox ID="txtDD_PH" runat="server" Width="154px" BackColor="LightGray" ReadOnly="True" meta:resourcekey="txtDD_PHResource1"></asp:TextBox></td>
                            <td align="center" style="font-size: 12px; width: 100px; height: 13px">
                                </td>
                            <td colspan="2" style="height: 13px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 12px; width: 125px; height: 19px">
                                &nbsp;<asp:Label ID="lblDD_STATION" runat="server" Font-Size="9pt" Text="变电站" Width="92px" meta:resourcekey="lblDD_STATIONResource1"></asp:Label></td>
                            <td colspan="2" style="height: 19px; text-align: left; width: 164px;">
                                <asp:DropDownList ID="ddlDD_STATION" runat="server" Width="163px" Enabled="False" BackColor="WhiteSmoke" ForeColor="Black" meta:resourcekey="ddlDD_STATIONResource1">
                                </asp:DropDownList></td>
                            <td align="center" style="font-size: 12px; width: 100px; height: 19px">
                                &nbsp;<asp:Label ID="lblDD_TYPE" runat="server" Font-Size="9pt" Text="缺陷级别" Width="92px" meta:resourcekey="lblDD_TYPEResource1"></asp:Label></td>
                            <td colspan="2" style="height: 19px; text-align: left;">
                                <asp:DropDownList ID="ddlDD_TYPE" runat="server" Width="99px" Enabled="False" BackColor="WhiteSmoke" meta:resourcekey="ddlDD_TYPEResource1">
                                    <asp:ListItem Value="Commonly" meta:resourcekey="ListItemResource1" Text="一般"></asp:ListItem>
                                    <asp:ListItem Value="Importance" meta:resourcekey="ListItemResource2" Text="重大"></asp:ListItem>
                                    <asp:ListItem Value="Urgency" meta:resourcekey="ListItemResource3" Text="紧急"></asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 12px; width: 125px; height: 49px;">
                                &nbsp;<asp:Label ID="lblDD_FAULT" runat="server" Font-Size="9pt" Text="缺陷内容" Width="92px" meta:resourcekey="lblDD_FAULTResource1"></asp:Label></td>
                            <td colspan="5" style="height: 49px; text-align: left;">
                                <asp:TextBox ID="txtDD_FAULT" runat="server" Height="63px" TextMode="MultiLine" Width="554px" ReadOnly="True" BackColor="LightGray" meta:resourcekey="txtDD_FAULTResource1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 12px; width: 125px; height: 15px">
                                &nbsp;<asp:Label ID="lblDD_FINDER" runat="server" Font-Size="9pt" Text="填单人" Width="92px" meta:resourcekey="lblDD_FINDERResource1"></asp:Label></td>
                            <td colspan="2" style="height: 15px; text-align: left; width: 164px;">
                                <asp:TextBox ID="txtDD_FINDER" runat="server" Width="145px" ReadOnly="True" BackColor="LightGray" meta:resourcekey="txtDD_FINDERResource1"></asp:TextBox></td>
                            <td align="center" style="font-size: 12px; width: 100px; height: 15px">
                                &nbsp;<asp:Label ID="lblDD_DATEM" runat="server" Font-Size="9pt" Text="填单时间" Width="92px" meta:resourcekey="lblDD_DATEMResource1"></asp:Label></td>
                            <td align="center" style="height: 15px; text-align: left;" colspan="2">
                                <cc1:WebDate ID="wdlDD_DATEM" runat="server" Enabled="False" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlDD_DATEMResource1" />
                            </td>
                        </tr>
                    </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="color: #000000">
                <td align="center" style="height: 96px; width: 646px;">
                   <table border="0" cellpadding="0" cellspacing="1" style="height: 138px; background-color: #000000; width: 700px;">
                       <tr>
                           <td align="center" colspan="1" style="font-size: 9pt; height: 22px; background-color: lightsteelblue"
                               valign="middle">
                               <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="10pt" Text="系 统 处 理" meta:resourcekey="Label24Resource1"></asp:Label></td>
                       </tr>
                        <tr>
                            <td align="center" style="background-color: white; height: 126px; width: 601px;">
                    <table border="0" cellpadding="0" cellspacing="1" style="width: 664px">
                        <tr>
                            <td style="font-size: 12px; width: 86px; height: 3px" align="center">
                                &nbsp;<asp:Label ID="lblXT_JDR" runat="server" Font-Size="9pt" Text="系统接单人" Width="92px" meta:resourcekey="lblXT_JDRResource1"></asp:Label></td>
                            <td colspan="2" style="height: 3px; width: 193px; text-align: left;" align="center">
                                <table style="width: 100%; height: 22px;">
                                    <tr>
                                        <td style="height: 24px">
                                            <asp:TextBox ID="txtXT_JDR" runat="server" BackColor="Gainsboro" ReadOnly="True"
                                                Width="51px" meta:resourcekey="txtXT_JDRResource1"></asp:TextBox></td>
                                        <td style="height: 24px">
                                            <asp:TextBox ID="txtXT_JDR2" runat="server" BackColor="Gainsboro" ReadOnly="True"
                                                Width="51px" meta:resourcekey="txtXT_JDR2Resource1"></asp:TextBox></td>
                                        <td style="height: 24px">
                                            <asp:TextBox ID="txtXT_JDR3" runat="server" BackColor="Gainsboro" ReadOnly="True"
                                                Width="51px" meta:resourcekey="txtXT_JDR3Resource1"></asp:TextBox></td>
                                        <td style="height: 24px">
                                            <asp:TextBox ID="txtXT_JDR4" runat="server" BackColor="Gainsboro" ReadOnly="True"
                                                Width="51px" meta:resourcekey="txtXT_JDR4Resource1"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center" style="font-size: 12px; width: 85px; height: 3px">
                                &nbsp;<asp:Label ID="lblXT_XDSJ" runat="server" Font-Size="9pt" Text="接单时间" Width="92px" meta:resourcekey="lblXT_XDSJResource1"></asp:Label></td>
                            <td colspan="2" style="height: 3px">
                                <cc1:WebDate ID="wdlXT_XDSJ" runat="server" Enabled="False" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlXT_XDSJResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 12px; width: 86px; height: 19px">
                                &nbsp;<asp:Label ID="lblXT_TYPE" runat="server" Font-Size="9pt" Text="缺陷类型" Width="92px" meta:resourcekey="lblXT_TYPEResource1"></asp:Label></td>
                            <td colspan="2" style="height: 19px; width: 193px; text-align: left;" align="center">
                                <asp:DropDownList ID="ddlXT_TYPE" runat="server" Width="171px" Enabled="False" BackColor="WhiteSmoke" meta:resourcekey="ddlXT_TYPEResource1">
                                </asp:DropDownList></td>
                            <td align="center" style="font-size: 12px; width: 85px; height: 19px">
                                &nbsp;</td>
                            <td colspan="2" style="height: 19px">
                                </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 12px; width: 86px; height: 37px;">
                                &nbsp;<asp:Label ID="lblXT_CLRY" runat="server" Font-Size="9pt" Text="处理内容" Width="92px" meta:resourcekey="lblXT_CLRYResource1"></asp:Label></td>
                            <td colspan="5" style="height: 37px; text-align: left;">
                                <asp:TextBox ID="txtXT_CLRY" runat="server" Height="59px" TextMode="MultiLine" Width="554px" ReadOnly="True" BackColor="Gainsboro" meta:resourcekey="txtXT_CLRYResource1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 12px; width: 86px; height: 17px">
                                &nbsp;<asp:Label ID="lblXT_CLJR" runat="server" Font-Size="9pt" Text="处理结论" Width="92px" meta:resourcekey="lblXT_CLJRResource1"></asp:Label></td>
                            <td colspan="2" style="height: 17px; width: 193px; text-align: left;">
                                <asp:DropDownList ID="ddlXT_CLJR" runat="server" Width="163px" Enabled="False" BackColor="WhiteSmoke" AutoPostBack="True" OnSelectedIndexChanged="ddlXT_CLJR_SelectedIndexChanged" meta:resourcekey="ddlXT_CLJRResource1">
                                    <asp:ListItem Selected="True" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource5" Value="0" Text="结束"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource6" Value="1" Text="转远动"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource7" Value="2" Text="待观察"></asp:ListItem>
                                </asp:DropDownList></td>
                            <td align="center" style="font-size: 12px; width: 85px; height: 17px">
                                &nbsp;<asp:Label ID="lblXT_ENDTIME" runat="server" Font-Size="9pt" Text="结束时间" Width="92px" meta:resourcekey="lblXT_ENDTIMEResource1"></asp:Label></td>
                            <td colspan="2" style="height: 17px">
                                <cc1:WebDate ID="wdlXT_ENDTIME" runat="server" Enabled="False" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlXT_ENDTIMEResource1" />
                            </td>
                        </tr>
                    </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="color: #000000">
                <td align="center" style="height: 60px; width: 646px;">
                    <table border="0" cellpadding="0" cellspacing="1" style="height: 149px; background-color: #000000; width: 700px;">
                        <tr>
                            <td align="center" colspan="1" style="font-size: 9pt; height: 28px; background-color: paleturquoise"
                                valign="middle">
                                <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Size="10pt" Text="远 动 处 理" meta:resourcekey="Label25Resource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="center" style="background-color: white; height: 125px; width: 601px;">
                    <table border="0" cellpadding="0" cellspacing="1" style="width: 662px">
                        <tr>
                            <td style="font-size: 12px; width: 126px; height: 10px" align="center">
                                &nbsp;<asp:Label ID="lblYD_JDR" runat="server" Font-Size="9pt" Text="远动接单人" Width="92px" meta:resourcekey="lblYD_JDRResource1"></asp:Label></td>
                            <td colspan="2" style="height: 10px; text-align: left;">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                <asp:TextBox ID="txtYD_JDR" runat="server" Width="55px" ReadOnly="True" BackColor="Gainsboro" meta:resourcekey="txtYD_JDRResource1"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtYD_JDR2" runat="server" BackColor="Gainsboro" ReadOnly="True"
                                                Width="51px" meta:resourcekey="txtYD_JDR2Resource1"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtYD_JDR3" runat="server" BackColor="Gainsboro" ReadOnly="True"
                                                Width="51px" meta:resourcekey="txtYD_JDR3Resource1"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtYD_JDR4" runat="server" BackColor="Gainsboro" ReadOnly="True"
                                                Width="51px" meta:resourcekey="txtYD_JDR4Resource1"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center" style="font-size: 12px; width: 100px; height: 10px">
                                &nbsp;<asp:Label ID="lblYD_JDSJ" runat="server" Font-Size="9pt" Text="接单时间" Width="92px" meta:resourcekey="lblYD_JDSJResource1"></asp:Label></td>
                            <td  style="height: 10px" colspan="2">
                                <cc1:WebDate ID="wdlYD_JDSJ" runat="server" Enabled="False" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlYD_JDSJResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 12px; width: 126px; height: 20px">
                                &nbsp;<asp:Label ID="lblYD_TYPE" runat="server" Font-Size="9pt" Text="缺陷类型" Width="92px" meta:resourcekey="lblYD_TYPEResource1"></asp:Label></td>
                            <td colspan="2" style="height: 20px; text-align: left;">
                                <asp:DropDownList ID="ddlYD_TYPE" runat="server" Width="167px" Enabled="False" BackColor="WhiteSmoke" meta:resourcekey="ddlYD_TYPEResource1">
                                </asp:DropDownList></td>
                            <td align="center" style="font-size: 12px; width: 100px; height: 20px"></td>
                            <td colspan="2" style="height: 20px">
                                </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 12px; width: 126px; height: 42px;">
                                &nbsp;<asp:Label ID="lblYD_CLRY" runat="server" Font-Size="9pt" Text="处理内容" Width="92px" meta:resourcekey="lblYD_CLRYResource1"></asp:Label></td>
                            <td colspan="5" style="height: 42px; text-align: left;">
                                <asp:TextBox ID="txtYD_CLRY" runat="server" Height="45px" TextMode="MultiLine" Width="554px" ReadOnly="True" BackColor="LightGray" meta:resourcekey="txtYD_CLRYResource1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 12px; width: 126px; height: 20px">
                                &nbsp;<asp:Label ID="lblYD_CLZT" runat="server" Font-Size="9pt" Text="处理结论" Width="92px" meta:resourcekey="lblYD_CLZTResource1"></asp:Label></td>
                            <td colspan="2" style="height: 20px; text-align: left;">
                                <asp:DropDownList ID="ddlYD_CLZT" runat="server" Width="167px" Enabled="False" BackColor="WhiteSmoke" AutoPostBack="True" OnSelectedIndexChanged="ddlYD_CLZT_SelectedIndexChanged" meta:resourcekey="ddlYD_CLZTResource1">
                                    <asp:ListItem Selected="True" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource9" Value="0">结束</asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource10" Value="1">待观察</asp:ListItem>
                                </asp:DropDownList></td>
                            <td align="center" style="font-size: 12px; width: 100px; height: 20px">
                                &nbsp;<asp:Label ID="lblYD_ENDTIME" runat="server" Font-Size="9pt" Text="结束时间" Width="92px" meta:resourcekey="lblYD_ENDTIMEResource1"></asp:Label></td>
                            <td colspan="2" style="height: 20px">
                                <cc1:WebDate ID="wdlYD_ENDTIME" runat="server" Enabled="False" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlYD_ENDTIMEResource1" />
                            </td>
                        </tr>
                    </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="color: #000000">
                <td align="center" style="height: 8px; width: 646px;">
                                    <table border="0" cellpadding="0" cellspacing="1" style=" background-color: #000000; width: 700px;">
                                        <tr>
                                            <td align="center" colspan="1" style="font-size: 9pt; height: 24px; background-color: lightblue"
                                                valign="middle">
                                                <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Size="10pt" Text="验 收" meta:resourcekey="Label26Resource1"></asp:Label></td>
                                        </tr>
                        <tr>
                            <td align="center" style="background-color: white; height: 58px; width: 601px; text-align: center;">
                    <table style="width: 668px; ">
                        <tr>
                            <td style="width: 80px; height: 17px; text-align: center;" align="center">
                                &nbsp;<asp:Label ID="lblYS_YSR" runat="server" Font-Size="9pt" Text="验收人" Width="76px" meta:resourcekey="lblYS_YSRResource1"></asp:Label></td>
                            <td style="width: 52px; height: 17px">
                                <asp:TextBox ID="txtYS_YSR" runat="server" BackColor="LightGray" ReadOnly="True" Width="81px" meta:resourcekey="txtYS_YSRResource1"></asp:TextBox></td>
                            <td style="width: 64px; height: 17px">
                                </td>
                            <td style="width: 62px; height: 17px" align="center">
                                &nbsp;<asp:Label ID="lblYS_YSRQ" runat="server" Font-Size="9pt" Text="验收日期" Width="86px" meta:resourcekey="lblYS_YSRQResource1"></asp:Label></td>
                            <td style="width: 4px; height: 17px">
                                <cc1:WebDate ID="wdlYS_YSRQ" runat="server" DateTimeStyle="Date" Enabled="False" DateStyle="DateFormat3" ButtonText=".." meta:resourcekey="wdlYS_YSRQResource1" myDateWidth="80px" />
                            </td>
                            <td style="width: 12px; height: 17px" align="center">
                                &nbsp;<asp:Label ID="lblYS_JR" runat="server" Font-Size="9pt" Text="验收结论" Width="72px" meta:resourcekey="lblYS_JRResource1"></asp:Label></td>
                            <td style="width: 12px; height: 17px">
                                <asp:DropDownList ID="ddlYS_JR" runat="server" BackColor="WhiteSmoke" Enabled="False" meta:resourcekey="ddlYS_JRResource1">
                                    <asp:ListItem Selected="True" meta:resourcekey="ListItemResource11"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource12" Value="0">合格</asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource13" Value="1">不合格</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 80px; height: 20px" align="center">
                                &nbsp;<asp:Label ID="lblYS_NOTE" runat="server" Font-Size="9pt" Text="备注" Width="72px" meta:resourcekey="lblYS_NOTEResource1"></asp:Label></td>
                            <td colspan="6" style="height: 20px; text-align: left">
                                <asp:TextBox ID="txtYS_NOTE" runat="server" BackColor="LightGray" ReadOnly="True"
                                    Width="554px" TextMode="MultiLine" meta:resourcekey="txtYS_NOTEResource1"></asp:TextBox></td>
                        </tr>
                    </table>
                            </td>
                            </tr>
                            </table>
                </td>
            </tr>
            <tr style="color: #000000">
                <td id="tdMessage" runat="server" align="left" style="height: 19px; font-size: 12px; color: red; width: 646px;">
                </td>
            </tr>
        </table>
    </div>
    <input type="text" id="refreshPage" value="0" runat="server"  onpropertychange="javascript:form1.submit();" size="0" style="VISIBILITY: hidden"/>
    </form>
</body>
</html>

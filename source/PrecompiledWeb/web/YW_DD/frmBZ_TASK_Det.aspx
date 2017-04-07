<%@ page language="C#" autoeventwireup="true" inherits="YW_ZDH_frmBZ_TASK_Det, App_Web_docfbltz" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>班组任务单</title>
</head>
<body style="margin-top: 2px;  margin-left: 2px; text-align: center;">
<form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px; height: 219px">
            <tr>

            </tr>
            <tr>
                <td colspan="3" id="TD1">
                        <table id="TopTable" runat="server" cellpadding="0" cellspacing="0" width="800" border="0">
                            <tr>
                                <td class="SepBorderNew" colspan="11" style="background-image: url(../img/main_bar.jpg);
                                    width: 988px; height: 25px; text-align: left">
                                    <img src="../img/s_img.gif" style="font-weight: bold" /><span style="font-size: 10pt"><span
                                        style="color: #3300cc"> <span style="color: #000000"></span></span><span>
                                            <asp:Label ID="lblFuncName" runat="server" Text="班组任务单" meta:resourcekey="lblFuncNameResource1"></asp:Label></span></span></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="SepBorderNew" colspan="11" style="height: 31px; text-align: left">
                                    &nbsp;<asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="提 交" Width="60px" meta:resourcekey="btnSendResource1" />&nbsp;<asp:Button
                                        ID="btnWithdraw" runat="server" OnClick="btnWithdraw_Click"
                                        Text="退 回" Width="60px" meta:resourcekey="btnWithdrawResource1" />&nbsp;<asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click"
                                            Text="打 印" Width="60px" meta:resourcekey="btnPrintResource1" />&nbsp;<asp:Button ID="btnAccept" runat="server" OnClick="btnAccept_Click" Text="接 单" Width="60px" meta:resourcekey="btnAcceptResource1" />&nbsp;<asp:Button
                                                    ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保 存" Width="60px" meta:resourcekey="btnSaveResource1" />&nbsp;<asp:Button
                                                        ID="btnSaveClose" runat="server" OnClick="btnSaveClose_Click" Text="保存返回" Width="61px" meta:resourcekey="btnSaveCloseResource1" />&nbsp;<asp:Button
                                                            ID="btnClose" runat="server" OnClick="btnClose_Click" Text="返 回" Width="60px" meta:resourcekey="btnCloseResource1" /></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="SelectedTopBorder" id="Cell1" align="center" style="width: 56px; height: 18px; text-align: center;">
                                    <asp:LinkButton ID="lbnView1" runat="server" CssClass="TopTitle" OnClick="lbnView_Click" Width="89px" meta:resourcekey="lbnView1Resource1">任务单工作内容</asp:LinkButton></td>
                                <td class="SepBorder" style="width: 2px; height: 18px;">
                                    &nbsp;</td>
                                <td class="TopBorder" id="Cell2" align="center" style="width: 65px; height: 18px; text-align: center;">
                                    <asp:LinkButton ID="lbnView2" runat="server" CssClass="TopTitle" OnClick="lbnView_Click" Width="112px" meta:resourcekey="lbnView2Resource1">正在进行的处理环节</asp:LinkButton></td>
                                <td class="SepBorder" style="width: 2px; height: 18px;">
                                    &nbsp;</td>
                                <td class="TopBorder" id="Cell3" align="center" style="width: 63px; height: 18px; text-align: right;">
                                    <asp:LinkButton ID="lbnView3" runat="server" CssClass="TopTitle" OnClick="lbnView_Click" Width="92px" meta:resourcekey="lbnView3Resource1">所有处理环节</asp:LinkButton></td>
                                <td class="SepBorder" style="width: 2px; height: 18px;">
                                    &nbsp;</td>
                                <td class="TopBorder" id="Cell4" align="center" style="width: 70px; height: 18px; text-align: center;">
                                    <asp:LinkButton ID="lbnView4" runat="server" CssClass="TopTitle" OnClick="lbnView_Click" Width="59px" meta:resourcekey="lbnView4Resource1">相关附件</asp:LinkButton></td>
                                <td class="SepBorderNew" colspan="4" style="width: 430px;height: 18px; text-align: right">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table  class="ContentBorder" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td  valign="top" style="width: 96%; ">
                                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="View1" runat="server">
                                             <table cellspacing="10" width="100%" style="height: 465px">
                                                 <tr>
                                                     <td style="text-align: center; width: 964px; height: 319px;" >   
                                                         <table border="0" cellpadding="0" cellspacing="1" style="width: 716px; height: 413px">
                                                             <tr>
                                                                 <td align="center" style="font-size: 12pt; width: 115px; height: 27px">
                                                                     &nbsp;<asp:Label ID="lblPH" runat="server" Font-Size="9pt" Text="任务单编号" meta:resourcekey="lblPHResource1"></asp:Label></td>
                                                                 <td align="left" colspan="2" style="width: 164px; height: 27px">
                                                                     <asp:TextBox ID="txtPH" runat="server" BackColor="LightGray" ReadOnly="True" Width="144px" meta:resourcekey="txtPHResource1"></asp:TextBox></td>
                                                                 <td align="center" style="font-size: 12px; width: 121px; height: 27px">
                                                                     &nbsp;<asp:Label ID="lblSTATION" runat="server" Font-Size="9pt" Text="变电站" meta:resourcekey="lblSTATIONResource1"></asp:Label></td>
                                                                 <td colspan="2" style="height: 27px; text-align: left">
                                                                     &nbsp;<cc1:HtmlComboBox ID="hcbSTATION" runat="server" Enabled="False" Width="157px" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbSTATIONResource1" SelectedText="">
                                                                     </cc1:HtmlComboBox></td>
                                                             </tr>
                                                             <tr>
                                                                 <td align="center" style="font-size: 12px; width: 115px; height: 15px">
                                                                     &nbsp;<asp:Label ID="lblSTARTER" runat="server" Font-Size="9pt" Text="任务启动人" meta:resourcekey="lblSTARTERResource1"></asp:Label></td>
                                                                 <td colspan="2" style="width: 164px; height: 15px; text-align: left">
                                                                     <asp:TextBox ID="txtSTARTER" runat="server" BackColor="LightGray" ReadOnly="True"
                                                                         Width="145px" meta:resourcekey="txtSTARTERResource1"></asp:TextBox></td>
                                                                 <td align="center" style="font-size: 12px; width: 121px; height: 15px">
                                                                 </td>
                                                                 <td colspan="2" style="height: 15px; text-align: left">
                                                                     &nbsp;</td>
                                                             </tr>
                                                             <tr>
                                                                 <td align="center" style="font-size: 12px; width: 115px; height: 30px">
                                                                     &nbsp;<asp:Label ID="lblSTARTTIME" runat="server" Font-Size="9pt" Text="计划开始时间" meta:resourcekey="lblSTARTTIMEResource1"></asp:Label></td>
                                                                 <td colspan="2" style="width: 164px; height: 30px; text-align: left">
                                                                     <cc1:WebDate ID="wdlSTARTTIME" runat="server" Enabled="False" DateStyle="DateFormat3" ButtonText=".." meta:resourcekey="wdlSTARTTIMEResource1" />
                                                                 </td>
                                                                 <td align="center" style="font-size: 12px; width: 121px; height: 30px">
                                                                     &nbsp;<asp:Label ID="lblENDTIME" runat="server" Font-Size="9pt" Text="计划结束时间" meta:resourcekey="lblENDTIMEResource1"></asp:Label></td>
                                                                 <td colspan="2" style="height: 30px; text-align: left">
                                                                     <cc1:WebDate ID="wdlENDTIME" runat="server" Enabled="False" DateStyle="DateFormat3" ButtonText=".." meta:resourcekey="wdlENDTIMEResource1" />
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td align="center" style="font-size: 12px; width: 115px; height: 49px; text-align: center">
                                                                     &nbsp;<asp:Label ID="lblCONTENT" runat="server" Font-Size="9pt" Text="工作内容" meta:resourcekey="lblCONTENTResource1"></asp:Label></td>
                                                                 <td colspan="5" style="height: 49px; text-align: left">
                                                                     <asp:TextBox ID="txtCONTENT" runat="server" BackColor="LightGray" Height="73px" ReadOnly="True"
                                                                         TextMode="MultiLine" Width="588px" meta:resourcekey="txtCONTENTResource1"></asp:TextBox></td>
                                                             </tr>
                                                             <tr>
                                                                 <td align="center" style="font-size: 12px; width: 115px; height: 49px">
                                                                     &nbsp;<asp:Label ID="lblNOTE" runat="server" Font-Size="9pt" Text="备注" meta:resourcekey="lblNOTEResource1"></asp:Label></td>
                                                                 <td colspan="5" style="height: 49px; text-align: left">
                                                                     <asp:TextBox ID="txtNOTE" runat="server" BackColor="LightGray" Height="59px" ReadOnly="True"
                                                                         TextMode="MultiLine" Width="588px" meta:resourcekey="txtNOTEResource1"></asp:TextBox></td>
                                                             </tr>
                                                             <tr>
                                                                 <td align="center" style="font-size: 12px; width: 115px; height: 15px">
                                                                     &nbsp;<asp:Label ID="lblCHECKER" runat="server" Font-Size="9pt" Text="验收人" meta:resourcekey="lblCHECKERResource1"></asp:Label></td>
                                                                 <td colspan="2" style="width: 164px; height: 15px; text-align: left">
                                                                     <asp:TextBox ID="txtCHECKER" runat="server" BackColor="LightGray" ReadOnly="True"
                                                                         Width="145px" meta:resourcekey="txtCHECKERResource1"></asp:TextBox></td>
                                                                 <td align="center" style="font-size: 12px; width: 121px; height: 15px">
                                                                     &nbsp;<asp:Label ID="lblCHECK_DATE" runat="server" Font-Size="9pt" Text="验收时间" meta:resourcekey="lblCHECK_DATEResource1"></asp:Label></td>
                                                                 <td align="center" colspan="2" style="height: 15px; text-align: left">
                                                                     <cc1:WebDate ID="wdlCHECK_DATE" runat="server" Enabled="False" DateStyle="DateFormat3" ButtonText=".." meta:resourcekey="wdlCHECK_DATEResource1" />
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td align="center" style="font-size: 12px; width: 115px; height: 29px">
                                                                     &nbsp;<asp:Label ID="lblCHECK_CONCLUSION" runat="server" Font-Size="9pt" Text="验收结论" meta:resourcekey="lblCHECK_CONCLUSIONResource1"></asp:Label></td>
                                                                 <td colspan="2" style="width: 164px; height: 29px; text-align: left">
                                                                     <asp:DropDownList ID="ddlCHECK_CONCLUSION" runat="server" Enabled="False" Width="148px" meta:resourcekey="ddlCHECK_CONCLUSIONResource1">
                                                                         <asp:ListItem meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                                         <asp:ListItem meta:resourcekey="ListItemResource2" Value="0">合格</asp:ListItem>
                                                                         <asp:ListItem meta:resourcekey="ListItemResource3" Value="1">不合格</asp:ListItem>
                                                                     </asp:DropDownList></td>
                                                                 <td align="center" style="font-size: 12px; width: 121px; height: 29px">
                                                                     &nbsp;<asp:TextBox ID="txtTID" runat="server" BackColor="LightGray" EnableTheming="False"
                                                                         ReadOnly="True" Visible="False" Width="18px" meta:resourcekey="txtTIDResource1"></asp:TextBox>&nbsp;
                                                                     <asp:TextBox ID="txtPACK_NO" runat="server" BackColor="LightGray" EnableTheming="False"
                                                                         ReadOnly="True" Visible="False" Width="26px" meta:resourcekey="txtPACK_NOResource1"></asp:TextBox>
                                                                     <asp:TextBox ID="txtCHECK_TID" runat="server" BackColor="LightGray" EnableTheming="False"
                                                                         ReadOnly="True" Visible="False" Width="26px" meta:resourcekey="txtCHECK_TIDResource1"></asp:TextBox></td>
                                                                 <td align="center" colspan="2" style="height: 29px">
                                                                     <asp:TextBox ID="txtCHECK_PACK_NO" runat="server" BackColor="LightGray" EnableTheming="False"
                                                                         ReadOnly="True" Visible="False" Width="26px" meta:resourcekey="txtCHECK_PACK_NOResource1"></asp:TextBox></td>
                                                             </tr>
                                                             <tr>
                                                                 <td align="center" style="font-size: 12px; width: 115px; height: 29px">
                                                                     &nbsp;<asp:Label ID="lblCHECK_NOTE" runat="server" Font-Size="9pt" Text="验收备注" meta:resourcekey="lblCHECK_NOTEResource1"></asp:Label></td>
                                                                 <td colspan="5" style="height: 29px; text-align: left">
                                                                     <asp:TextBox ID="txtCHECK_NOTE" runat="server" BackColor="LightGray" Height="45px"
                                                                         ReadOnly="True" TextMode="MultiLine" Width="588px" meta:resourcekey="txtCHECK_NOTEResource1"></asp:TextBox></td>
                                                             </tr>
                                                         </table>
                                                         &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                     </td>
                                                 </tr>
                                            </table>
                                         </asp:View>
                                        <asp:View ID="View2" runat="server">
                                            &nbsp;<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td id="tdCurDispoase" runat="server" style="height: 25px; text-align: left; font-weight: bold; color: #660000; background-image: url(../img/bj.gif); font-size: 9pt;">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        </td>
                                                </tr>
                                            </table>
                                            <table border="0" style="width: 100%">
                                                <tr>
                                                    <td align="center" valign="middle" style="width: 956px; height: 49px;">
                                                                <table border="0" cellpadding="0" cellspacing="1" style="width: 718px; height: 266px">
                                                                    <tr>
                                                                        <td align="center" style="font-size: 12pt; width: 98px; height: 12px">
                                                                            &nbsp;<asp:Label ID="lblFZR" runat="server" Font-Size="9pt" Text="工作人员" meta:resourcekey="lblFZRResource1"></asp:Label></td>
                                                                        <td align="left" colspan="2" style="width: 164px; height: 12px">
                                                                            <table style="width: 100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtFZR" runat="server" BackColor="LightGray" ReadOnly="True" Width="55px" meta:resourcekey="txtFZRResource1"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtJDR1" runat="server" BackColor="LightGray" ReadOnly="True" Width="51px" meta:resourcekey="txtJDR1Resource1"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtJDR2" runat="server" BackColor="LightGray" ReadOnly="True" Width="51px" meta:resourcekey="txtJDR2Resource1"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtJDR3" runat="server" BackColor="LightGray" ReadOnly="True" Width="51px" meta:resourcekey="txtJDR3Resource1"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td align="center" style="font-size: 12px; width: 91px; height: 12px; text-align: center">
                                                                            &nbsp;<asp:Label ID="lblDISPOSE_STARTTIME" runat="server" Font-Size="9pt" Text="开始工作时间" Width="110px" meta:resourcekey="lblDISPOSE_STARTTIMEResource1"></asp:Label></td>
                                                                        <td colspan="2" style="height: 12px; text-align: left">
                                                                            <cc1:WebDate ID="wdlDISPOSE_STARTTIME" runat="server" Enabled="False" DateStyle="DateFormat3" ButtonText=".." meta:resourcekey="wdlDISPOSE_STARTTIMEResource1" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="font-size: 12px; width: 98px; height: 58px; text-align: center">
                                                                            &nbsp;<asp:Label ID="lblDISPOSE" runat="server" Font-Size="9pt" Text="处理内容" meta:resourcekey="lblDISPOSEResource1"></asp:Label></td>
                                                                        <td colspan="5" style="height: 58px; text-align: left">
                                                                            <asp:TextBox ID="txtDISPOSE" runat="server" BackColor="LightGray" Height="100px" ReadOnly="True"
                                                                                TextMode="MultiLine" Width="588px" meta:resourcekey="txtDISPOSEResource1"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="font-size: 12px; width: 98px; height: 49px">
                                                                            &nbsp;<asp:Label ID="lblLEAVINGS" runat="server" Font-Size="9pt" Text="遗留问题" meta:resourcekey="lblLEAVINGSResource1"></asp:Label></td>
                                                                        <td colspan="5" style="height: 49px; text-align: left">
                                                                            <asp:TextBox ID="txtLEAVINGS" runat="server" BackColor="LightGray" Height="50px"
                                                                                ReadOnly="True" TextMode="MultiLine" Width="588px" meta:resourcekey="txtLEAVINGSResource1"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="font-size: 12px; width: 98px; height: 15px">
                                                                            &nbsp;<asp:Label ID="lblDISPOSE_ENDTIME" runat="server" Font-Size="9pt" Text="结束工作时间" Width="106px" meta:resourcekey="lblDISPOSE_ENDTIMEResource1"></asp:Label></td>
                                                                        <td colspan="2" style="width: 164px; height: 15px; text-align: left">
                                                                            <cc1:WebDate ID="wdlDISPOSE_ENDTIME" runat="server" Enabled="False" DateStyle="DateFormat3" ButtonText=".." meta:resourcekey="wdlDISPOSE_ENDTIMEResource1" />
                                                                        </td>
                                                                        <td align="center" style="font-size: 12px; width: 91px; height: 15px">
                                                                            <asp:TextBox ID="txtDISPOSE_TID" runat="server" BackColor="LightGray" EnableTheming="False"
                                                                                ReadOnly="True" Visible="False" Width="26px" meta:resourcekey="txtDISPOSE_TIDResource1"></asp:TextBox>
                                                                            <asp:TextBox ID="txtF_FLOWNAME" runat="server" BackColor="LightGray" EnableTheming="False"
                                                                                ReadOnly="True" Visible="False" Width="26px" meta:resourcekey="txtF_FLOWNAMEResource1"></asp:TextBox></td>
                                                                        <td align="center" colspan="2" style="height: 15px; text-align: left">
                                                                            <asp:TextBox ID="txtDISPOSE_PACKNO" runat="server" BackColor="LightGray" EnableTheming="False"
                                                                                ReadOnly="True" Visible="False" Width="26px" meta:resourcekey="txtDISPOSE_PACKNOResource1"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                    </td>
                                                </tr>
                                            </table>

                                        </asp:View>
                                        <asp:View ID="View3" runat="server">
                                        <br />
                                            <cc1:WebGridView ID="wgvCompleteTask" runat="server" AutoGenerateColumns="False"
                                                CssClass="font" DataKeyNames="TID" EmptyDataText="还没有已完成的环节！" Height="25px" HorizontalAlign="Right" TableHeight="400px" TableWidth="800px"
                                                Width="1100px" meta:resourcekey="wgvCompleteTaskResource1">
                                                <RowStyle Height="25px" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="环节名称" DataField="F_FLOWNAME" meta:resourcekey="BoundFieldResource1">
                                                        <itemstyle width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FZR" HeaderText="工作负责人" meta:resourcekey="BoundFieldResource2">
                                                        <itemstyle width="65px" horizontalalign="Center" verticalalign="Middle" />
                                                        <headerstyle font-size="8pt" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JDR1" HeaderText="工作人员一" meta:resourcekey="BoundFieldResource3">
                                                        <itemstyle horizontalalign="Center" verticalalign="Middle" width="65px" />
                                                        <headerstyle font-size="8pt" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JDR2" HeaderText="工作人员二" meta:resourcekey="BoundFieldResource4">
                                                        <itemstyle horizontalalign="Center" verticalalign="Middle" width="65px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JDR3" HeaderText="工作人员三" meta:resourcekey="BoundFieldResource5">
                                                        <itemstyle horizontalalign="Center" verticalalign="Middle" width="65px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="STARTTIME" DataFormatString="{0:dd-MM-yyyy HH:mm}"
                                                        HeaderText="开始工作时间" HtmlEncode="False" meta:resourcekey="BoundFieldResource6">
                                                        <itemstyle width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DISPOSE" HeaderText="处理情况" meta:resourcekey="BoundFieldResource7">
                                                        <itemstyle horizontalalign="Left" verticalalign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="遗留问题" DataField="LEAVINGS" meta:resourcekey="BoundFieldResource8">
                                                        <itemstyle horizontalalign="Left" width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="结束工作时间" DataField="ENDTIME" DataFormatString="{0:dd-MM-yyyy HH:mm}" HtmlEncode="False" meta:resourcekey="BoundFieldResource9">
                                                        <itemstyle width="80px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle Height="25px" />
                                                <HeaderStyle Height="25px" />
                                                <AlternatingRowStyle Height="25px" />
                                            </cc1:WebGridView>
                                        </asp:View>
                                        <asp:View ID="View4" runat="server">
                                        <table width="100%" height="650" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                        <td style="height: 20px; width: 35px;">
                                            <asp:Button ID="btnAddFile" runat="server" OnClick="btnAddFile_Click" Text="添加"
                                                Width="38px" meta:resourcekey="btnAddFileResource1" /></td>
                                         <td style="height: 20px; width: 29px;">
                                             <asp:Button ID="btnDelFile" runat="server" OnClick="btnDelFile_Click"
                                                 Text="删除" Width="38px" meta:resourcekey="btnDelFileResource1" /></td>       
                                         <td style="height: 20px; width: 35px;">
                                             <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click"
                                                 Text="刷新" Width="40px" meta:resourcekey="btnRefreshResource1" /></td>                                                 
                                            <td style="width: 35px; height: 20px">
                                                <asp:Button ID="btnDown" runat="server" OnClick="btnDown_Click" Text="下载" Width="36px" meta:resourcekey="btnDownResource1" /></td>
                                        <td id="tdTitle" style="width:70%;height:20px;background-color:transparent; text-align: left;" valign="top" >
                                            <asp:DataList ID="dltFiles" runat="server" CellPadding="2" CellSpacing="2" DataKeyField="F_NO"
                                                OnItemCommand="dltFiles_ItemCommand" RepeatDirection="Horizontal" Width="506px" meta:resourcekey="dltFilesResource1">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbSelect" runat="server" CommandName="Select" ImageUrl="~/img/fileType/bmp.gif" Width="19px" meta:resourcekey="imbSelectResource2" />
                                                    <br />
                                                    <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("F_CAPTION") %>' meta:resourcekey="lblDescResource2"></asp:Label>
                                                    <asp:HiddenField ID="hfdFileName" runat="server" Value='<%# Eval("F_FILENAME") %>' />
                                                </ItemTemplate>
                                                <SelectedItemTemplate>
                                                    <asp:ImageButton ID="imbSelect" runat="server" CommandName="Select" ImageUrl="~/img/selectChildNode.gif" meta:resourcekey="imbSelectResource1" /><br />
                                                    <br />
                                                    <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("F_CAPTION") %>' meta:resourcekey="lblDescResource1"></asp:Label>
                                                    <asp:HiddenField ID="hfdFileName" runat="server" Value='<%# Eval("F_FILENAME") %>' />
                                                </SelectedItemTemplate>
                                            </asp:DataList></td>                                        
                                        </tr>
                                        <tr>
                                        <td style="width: 970px; height: 432px;"  valign="top" colspan="5"> 
                                            <iframe runat="server" id="childWin" src="" style="width: 970px; height: 429px;BORDER-LEFT-COLOR: #ffffff; BORDER-BOTTOM-COLOR: #ffffff; PAGE-BREAK-AFTER: always; OVERFLOW: auto; WIDTH: 100%; BORDER-TOP-COLOR: #ffffff; HEIGHT: 550px; BACKGROUND-COLOR: #ffffff; BORDER-RIGHT-COLOR: #ffffff"></iframe>
                                        </td>
                                        </tr>
                                        </table>                                        
                                        </asp:View>
                                    </asp:MultiView>
                                  </td>
                            </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 23px; color: red; font-size: 9pt; text-align: left;" id="tdMessage" runat="server">
                    <span style="color: red"></span>
                </td>
            </tr>
        </table>
    
    </div>
    <input type="text" id="refreshPage" value="0" runat="server"  onpropertychange="javascript:form1.submit();" size="0" style="VISIBILITY: hidden"/>
    <input type="hidden" id="fileid" runat="server" value="-1" />
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmBH_FIXED_VALUE_SHEET_Det.aspx.cs" Inherits="YW_BH_frmBH_FIXED_VALUE_SHEET_Det" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
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
                                            <asp:Label ID="lblFuncName" runat="server" Text="保护定值单" meta:resourcekey="lblFuncNameResource1"></asp:Label></span></span></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="SepBorderNew" colspan="11" style="height: 31px; text-align: left">
                                    &nbsp;<asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="提 交" Width="60px" meta:resourcekey="btnSendResource1" />&nbsp;<asp:Button
                                        ID="btnWithdraw" runat="server" OnClick="btnWithdraw_Click"
                                        Text="退 回" Width="60px" meta:resourcekey="btnWithdrawResource1" />&nbsp;<asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click"
                                            Text="打 印" Width="60px" meta:resourcekey="btnPrintResource1" />&nbsp;<asp:Button ID="btnAccept" runat="server" OnClick="btnAccept_Click" Text="接 单" Width="60px" meta:resourcekey="btnAcceptResource1" />&nbsp;<asp:Button
                                                    ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保 存" Width="60px" meta:resourcekey="btnSaveResource1" />&nbsp;<asp:Button
                                                        ID="btnSaveClose" runat="server" OnClick="btnSaveClose_Click" Text="保存返回" Width="61px" meta:resourcekey="btnSaveCloseResource1" />&nbsp;<asp:Button
                                                            ID="btnClose" runat="server" OnClick="btnClose_Click" Text="返 回" Width="60px" meta:resourcekey="btnCloseResource1" />
                                    <asp:TextBox ID="txtTID" runat="server" BackColor="LightGray" EnableTheming="False"
                                                                         ReadOnly="True" Visible="False" Width="18px" meta:resourcekey="txtTIDResource1"></asp:TextBox>
                                                                     <asp:TextBox ID="txtPACK_NO" runat="server" BackColor="LightGray" EnableTheming="False"
                                                                         ReadOnly="True" Visible="False" Width="26px" meta:resourcekey="txtPACK_NOResource1"></asp:TextBox></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="SelectedTopBorder" id="Cell1" align="center" style="width: 56px; height: 18px; text-align: center;">
                                    <asp:LinkButton ID="lbnView1" runat="server" CssClass="TopTitle" OnClick="lbnView_Click" Width="89px" meta:resourcekey="lbnView1Resource1">定值单审核</asp:LinkButton></td>
                                <td class="SepBorder" style="width: 2px; height: 18px;">
                                    &nbsp;</td>

                                <td class="TopBorder" id="Cell2" align="center" style="width: 70px; height: 18px; text-align: center;">
                                    <asp:LinkButton ID="lbnView2" runat="server" CssClass="TopTitle" OnClick="lbnView_Click" Width="77px" meta:resourcekey="lbnView4Resource1"></asp:LinkButton></td>
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
                                            <table class="slim_table">
                                                <tr>
                                                    <td class="slim_table_td_desc" style="height: 30px" >
                                                        <asp:Label ID="lblPH" runat="server" Text="定值单编号" meta:resourcekey="lblPHResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <asp:TextBox ID="txtPH" runat="server" ReadOnly="True" Width="225px" meta:resourcekey="txtPHResource1"></asp:TextBox></td>
                                                    <td class="slim_table_td_desc" style="height: 30px" >
                                                        <asp:Label ID="lblSTATION" runat="server" Text="变电站" meta:resourcekey="lblSTATIONResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <cc1:HtmlComboBox ID="hcbSTATION" runat="server" Enabled="False" Width="203px" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbSTATIONResource1" SelectedText="">
                                                        </cc1:HtmlComboBox></td>
                                                    </tr>
                                                <tr>
                                                    <td class="slim_table_td_desc">
                                                        <asp:Label ID="lblDEV_NAME" runat="server" Text="设备名称" meta:resourcekey="lblDEV_NAMEResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control">
                                                        <asp:TextBox ID="txtDEV_NAME" runat="server" ReadOnly="True" Width="225px" meta:resourcekey="txtDEV_NAMEResource1"></asp:TextBox></td>
                                                    <td class="slim_table_td_desc">
                                                        <asp:Label ID="lblDEV_NUM" runat="server" Text="设备编号" meta:resourcekey="lblDEV_NUMResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control">
                                                        <asp:TextBox ID="txtDEV_NUM" runat="server" ReadOnly="True" Width="199px" meta:resourcekey="txtDEV_NUMResource1"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblNOTE" runat="server" Text="备注" meta:resourcekey="lblNOTEResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" colspan="3" style="height: 30px">
                                                        <asp:TextBox ID="txtNOTE" runat="server" Height="50px" TextMode="MultiLine"
                                                            Width="90%" meta:resourcekey="txtNOTEResource1" ReadOnly="True"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblWRITER" runat="server" Text="制定人" meta:resourcekey="lblWRITERResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <asp:TextBox ID="txtWRITER" runat="server" ReadOnly="True" Width="225px" meta:resourcekey="txtWRITERResource1"></asp:TextBox></td>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblWRITE_TIME" runat="server" Text="制定日期" meta:resourcekey="lblWRITE_TIMEResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <cc1:WebDate ID="wdlWRITE_TIME" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date" Enabled="False" ButtonText=".." meta:resourcekey="wdlWRITE_TIMEResource1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblCHECKER" runat="server" Text="审核人" meta:resourcekey="lblCHECKERResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <asp:TextBox ID="txtCHECKER" runat="server" ReadOnly="True" Width="225px" meta:resourcekey="txtCHECKERResource1"></asp:TextBox></td>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblCHECK_TIME" runat="server" Text="审核日期" meta:resourcekey="lblCHECK_TIMEResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <cc1:WebDate ID="wdlCHECK_TIME" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date"
                                                            Enabled="False" ButtonText=".." meta:resourcekey="wdlCHECK_TIMEResource1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblCHECK_OPINION" runat="server" Text="审核意见" meta:resourcekey="lblCHECK_OPINIONResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" colspan="3" style="height: 30px">
                                                        <asp:TextBox ID="txtCHECK_OPINION" runat="server" Height="50px" TextMode="MultiLine"
                                                            Width="90%" meta:resourcekey="txtCHECK_OPINIONResource1" ReadOnly="True"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblAPPROVER" runat="server" Text="批准人" meta:resourcekey="lblAPPROVERResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <asp:TextBox ID="txtAPPROVER" runat="server" ReadOnly="True" Width="225px" meta:resourcekey="txtAPPROVERResource1"></asp:TextBox></td>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblAPPROVE_TIME" runat="server" Text="批准日期" meta:resourcekey="lblAPPROVE_TIMEResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <cc1:WebDate ID="wdlAPPROVE_TIME" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date"
                                                            Enabled="False" ButtonText=".." meta:resourcekey="wdlAPPROVE_TIMEResource1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblAPPROVE_OPINION" runat="server" Text="批准意见" meta:resourcekey="lblAPPROVE_OPINIONResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" colspan="3" style="height: 30px">
                                                        <asp:TextBox ID="txtAPPROVE_OPINION" runat="server" Height="50px" TextMode="MultiLine"
                                                            Width="90%" meta:resourcekey="txtAPPROVE_OPINIONResource1" ReadOnly="True"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblOPERATOR" runat="server" Text="执行人" meta:resourcekey="lblOPERATORResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <asp:TextBox ID="txtOPERATOR" runat="server" ReadOnly="True" Width="225px" meta:resourcekey="txtOPERATORResource1"></asp:TextBox></td>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblOPERATE_TIME" runat="server" Text="执行日期" meta:resourcekey="lblOPERATE_TIMEResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <cc1:WebDate ID="wdlOPERATE_TIME" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date"
                                                            Enabled="False" ButtonText=".." meta:resourcekey="wdlOPERATE_TIMEResource1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblOPERATE_OPINION" runat="server" Text="执行意见" meta:resourcekey="Label19Resource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" colspan="3" style="height: 30px">
                                                        <asp:TextBox ID="txtOPERATE_OPINION" runat="server" Height="50px" TextMode="MultiLine"
                                                            Width="90%" meta:resourcekey="TextBox11Resource1" ReadOnly="True"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                        <asp:Label ID="lblFLAG" runat="server" Text="状态" meta:resourcekey="lblFLAGResource1"></asp:Label></td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                        <asp:DropDownList ID="ddlFLAG" runat="server" Enabled="False" Width="214px" meta:resourcekey="ddlFLAGResource1">
                                                        </asp:DropDownList></td>
                                                    <td class="slim_table_td_desc" style="height: 30px">
                                                    </td>
                                                    <td class="slim_table_td_control" style="height: 30px">
                                                    </td>
                                                </tr>
                                              </table>
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
                                                            </asp:DataList>
                                                           </td>                                        
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

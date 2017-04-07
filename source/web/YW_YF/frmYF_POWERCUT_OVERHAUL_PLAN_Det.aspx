<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmYF_POWERCUT_OVERHAUL_PLAN_Det.aspx.cs" Inherits="YW_YF_frmYF_POWERCUT_OVERHAUL_PLAN_Det" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>停电检修计划</title>
</head>
<body class="detail_body">
    <form id="form1" runat="server">
        <div id="detail_head" style="text-align: left">
            <img src="../img/s_img.gif" alt="" />
            <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
        </div> 
        <div id="detail_control">
            <asp:Button id="btnSend" onclick="btnSend_Click" runat="server" Text="提 交" Width="60px" meta:resourcekey="btnSendResource1"></asp:Button>
            <asp:Button id="btnWithdraw" onclick="btnWithdraw_Click" runat="server" Text="退 回" Width="60px" meta:resourcekey="btnWithdrawResource1"></asp:Button>
            <asp:Button id="btnPrint" onclick="btnPrint_Click" runat="server" Text="打 印" Width="60px" meta:resourcekey="btnPrintResource1"></asp:Button>
            <asp:Button id="btnAccept" onclick="btnAccept_Click" runat="server" Text="接 单" Width="60px" meta:resourcekey="btnAcceptResource1"></asp:Button>
            <asp:Button id="btnSave" onclick="btnSave_Click" runat="server" Text="保 存" Width="60px" meta:resourcekey="btnSaveResource1"></asp:Button>
            <asp:Button id="btnSaveClose" onclick="btnSaveClose_Click" runat="server" Text="保存返回" Width="61px" meta:resourcekey="btnSaveCloseResource1"></asp:Button>
            <asp:Button id="btnClose" onclick="btnClose_Click" runat="server" Text="返 回" Width="60px" meta:resourcekey="btnCloseResource1"></asp:Button>
        </div>        
        <div id="detail_info" runat="server">
        </div>
        <div id="detail_data">
            <br />
             <table id="Table1" runat="server" cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td class="SelectedTopBorder" id="Cell1" align="center" style="width: 70px; height: 19px;">
                            <asp:LinkButton ID="lbnView1" runat="server"  OnClick="lbnView_Click" meta:resourcekey="lbnView1Resource1" Width="120px">停电基本信息</asp:LinkButton></td>
                        <td class="SepBorder" style="width: 2px; height: 19px;">
                            &nbsp;</td>
                        <td class="TopBorder" id="Cell2" align="center"  style="width: 70px; height: 19px;">
                            <asp:LinkButton ID="lbnView2" runat="server" OnClick="lbnView_Click" meta:resourcekey="lbnView2Resource1">附件</asp:LinkButton></td>
                        <td class="SepBorder" style="width: 2px; height: 19px;">
                            &nbsp;</td>
                        <td class="SepBorderNew" id="tdMessage" style="height: 19px; text-align: right; width: 681px;" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                    <td colspan="5" style="border-left: gray 1px solid;border-right: gray 1px solid;border-bottom: Gray 1px solid">
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <br />
                                <table class="slim_table" style="width: 98%">
                                        <tr>
                                            <td class="slim_table_td_desc"  >
                                                <asp:Label ID="lblNUM" runat="server" Text="编号" meta:resourcekey="lblNUMResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:TextBox ID="txtNUM" runat="server" meta:resourcekey="txtNUMResource1"></asp:TextBox></td>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblAPPLY_UNIT" runat="server" Text="申请单位" meta:resourcekey="lblAPPLY_UNITResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:TextBox ID="txtAPPLY_UNIT" runat="server" meta:resourcekey="txtAPPLY_UNITResource1"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblAPPLY_MAN" runat="server" Text="申请人" meta:resourcekey="lblAPPLY_MANResource1"></asp:Label></td>
                                            <td class="slim_table_td_control" >
                                                <asp:TextBox ID="txtAPPLY_MAN" runat="server" meta:resourcekey="txtAPPLY_MANResource1"></asp:TextBox></td>
                                            <td class="slim_table_td_desc" >
                                            </td>
                                            <td class="slim_table_td_control" >
                                                    <asp:TextBox ID="txtTID" runat="server" Width="39px" Visible="False" meta:resourcekey="txtTIDResource1"></asp:TextBox>
                                                    <asp:TextBox ID="txtPACK_NO" runat="server" Width="32px" Visible="False" meta:resourcekey="txtFLAGResource1"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblLEADER" runat="server" Text="工作负责人" meta:resourcekey="lblLEADERResource1"></asp:Label></td>
                                            <td class="slim_table_td_control" >
                                                <asp:TextBox ID="txtLEADER" runat="server" meta:resourcekey="txtLEADERResource1"></asp:TextBox></td>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblLEADER_PHONE" runat="server" Text="工作负责人电话" meta:resourcekey="lblLEADER_PHONEResource1"></asp:Label></td>
                                            <td class="slim_table_td_control" >
                                                <asp:TextBox ID="txtLEADER_PHONE" runat="server" meta:resourcekey="txtLEADER_PHONEResource1"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblSTATION" runat="server" Text="变电站" meta:resourcekey="lblSTATIONResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <cc1:HtmlComboBox ID="hcbSTATION" runat="server" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbSTATIONResource1" SelectedText="">
                                                </cc1:HtmlComboBox></td>
                                            <td class="slim_table_td_desc">
                                                <asp:Label ID="lblDEVICE_NAME" runat="server" Text="停电设备名称" meta:resourcekey="lblDEVICE_NAMEResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:TextBox ID="txtDEVICE_NAME" runat="server" meta:resourcekey="txtDEVICE_NAMEResource1"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblRELATED_LINE" runat="server" Text="关联线路" meta:resourcekey="lblRELATED_LINEResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:TextBox ID="txtRELATED_LINE" runat="server" meta:resourcekey="txtRELATED_LINEResource1"></asp:TextBox></td>
                                            <td class="slim_table_td_desc">
                                                <asp:Label ID="lblRELATED_UNIT" runat="server" Text="配合工作单位" meta:resourcekey="lblRELATED_UNITResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:TextBox ID="txtRELATED_UNIT" runat="server" meta:resourcekey="txtRELATED_UNITResource1"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblAPPLY_STARTTIME" runat="server" Text="停电开始时间" meta:resourcekey="lblAPPLY_STARTTIMEResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <cc1:WebDate ID="wdlAPPLY_STARTTIME" runat="server" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlAPPLY_STARTTIMEResource1" />
                                            </td>
                                            <td class="slim_table_td_desc">
                                                <asp:Label ID="lblAPPLY_ENDTIME" runat="server" Text="停电结束时间" meta:resourcekey="lblAPPLY_ENDTIMEResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <cc1:WebDate ID="wdlAPPLY_ENDTIME" runat="server" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlAPPLY_ENDTIMEResource1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblCONTENT" runat="server" Text="工作内容" meta:resourcekey="lblCONTENTResource1"></asp:Label></td>
                                            <td class="slim_table_td_control" colspan="3">
                                                <asp:TextBox ID="txtCONTENT" runat="server" Height="115px" TextMode="MultiLine" Width="98%" meta:resourcekey="txtCONTENTResource1"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblNOTE" runat="server" Text="备注" meta:resourcekey="lblNOTEResource1"></asp:Label></td>
                                            <td class="slim_table_td_control" colspan="3">
                                                <asp:TextBox ID="txtNOTE" runat="server" Height="73px" TextMode="MultiLine" Width="98%" meta:resourcekey="txtNOTEResource1"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblAPPROVE_STARTTIME" runat="server" Text="批准停电开始时间" meta:resourcekey="lblAPPROVE_STARTTIMEResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <cc1:WebDate ID="wdlAPPROVE_STARTTIME" runat="server" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlAPPROVE_STARTTIMEResource1" />
                                            </td>
                                            <td class="slim_table_td_desc">
                                                <asp:Label ID="lblAPPROVE_ENDTIME" runat="server" Text="批准停电结束时间" meta:resourcekey="lblAPPROVE_ENDTIMEResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <cc1:WebDate ID="wdlAPPROVE_ENDTIME" runat="server" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlAPPROVE_ENDTIMEResource1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblAPPROVE_MATTERS" runat="server" Text="注意事项" meta:resourcekey="lblAPPROVE_MATTERSResource1"></asp:Label></td>
                                            <td class="slim_table_td_control" colspan="3">
                                                <asp:TextBox ID="txtAPPROVE_MATTERS" runat="server" Height="69px" TextMode="MultiLine" Width="98%" meta:resourcekey="txtAPPROVE_MATTERSResource1"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblAPPROVER" runat="server" Text="方式员" meta:resourcekey="lblAPPROVERResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:TextBox ID="txtAPPROVER" runat="server" meta:resourcekey="txtAPPROVERResource1"></asp:TextBox></td>
                                            <td class="slim_table_td_desc">
                                            </td>
                                            <td class="slim_table_td_control">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="Label18" runat="server" Text="主任批准" meta:resourcekey="Label18Resource1"></asp:Label></td>
                                            <td class="slim_table_td_control" >
                                                <asp:TextBox ID="txtDIRECTOR" runat="server" meta:resourcekey="txtDIRECTORResource1"></asp:TextBox></td>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="Label19" runat="server" Text="主任批准时间" meta:resourcekey="Label19Resource1"></asp:Label></td>
                                            <td class="slim_table_td_control" >
                                                <cc1:WebDate ID="wdlDIRECTOR_TIME" runat="server" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlDIRECTOR_TIMEResource1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" style="height: 30px" >
                                                <asp:Label ID="lblDISPATCHER" runat="server" Text="调度员" meta:resourcekey="lblDISPATCHERResource1"></asp:Label></td>
                                            <td class="slim_table_td_control" style="height: 30px">
                                                <asp:TextBox ID="txtDISPATCHER" runat="server" meta:resourcekey="txtDISPATCHERResource1"></asp:TextBox></td>
                                            <td class="slim_table_td_desc" style="height: 30px">
                                                <asp:Label ID="lblDISPATCH_FINISHEDTIME" runat="server" Text="完工时间" meta:resourcekey="lblDISPATCH_FINISHEDTIMEResource1"></asp:Label></td>
                                            <td class="slim_table_td_control" style="height: 30px">
                                                <cc1:WebDate ID="wdlDISPATCH_FINISHEDTIME" runat="server" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlDISPATCH_FINISHEDTIMEResource1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblDISPATCH_IMPLEMENTATION" runat="server" Text="执行情况" meta:resourcekey="lblDISPATCH_IMPLEMENTATIONResource1"></asp:Label></td>
                                            <td class="slim_table_td_control" colspan="3">
                                                <asp:TextBox ID="txtDISPATCH_IMPLEMENTATION" runat="server" Height="29px" TextMode="MultiLine" Width="98%" meta:resourcekey="txtDISPATCH_IMPLEMENTATIONResource1"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                  <br />
                               </asp:View>
                               <asp:View ID="View2" runat="server">
                                  <table class="slim_table">
                                        <tr>
                                        <td style="height: 20px; width: 35px;">
                                            <asp:Button ID="btnAddFile" runat="server" OnClick="btnAddFile_Click" Text="添加"
                                                Width="38px" meta:resourcekey="btnAddFileResource1" /></td>
                                         <td style="height: 20px; width: 29px;">
                                             <asp:Button ID="btnDelFile" runat="server" OnClick="btnDelFile_Click" OnClientClick='return confirm(&quot;确定要删除所选择的文件？&quot;);'
                                                 Text="删除" Width="38px" meta:resourcekey="btnDelFileResource1" /></td>       
                                         <td style="height: 20px; width: 35px;">
                                             <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click"
                                                 Text="刷新" Width="40px" meta:resourcekey="btnRefreshResource1" /></td>                                                 
                                            <td style="width: 35px; height: 20px">
                                                <asp:Button ID="btnDown" runat="server" OnClick="btnDown_Click" Text="下载" Width="36px" meta:resourcekey="btnDownResource1" /></td>
                                        <td id="tdTitle" style="width:70%;height:20px;background-color:transparent; text-align: left;" valign="top" >
                                            <asp:DataList ID="dltFiles" runat="server" CellPadding="2" CellSpacing="2" DataKeyField="F_NO"
                                                OnItemCommand="files_ItemCommand" RepeatDirection="Horizontal" Width="506px" meta:resourcekey="dltFilesResource1">
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
                                        <td valign="top" colspan="5"> 
                                            <iframe runat="server" id="childWin" src="" style="width: 970px; height: 429px;BORDER-LEFT-COLOR: #ffffff; BORDER-BOTTOM-COLOR: #ffffff; PAGE-BREAK-AFTER: always; OVERFLOW: auto; WIDTH: 100%; BORDER-TOP-COLOR: #ffffff; HEIGHT: 550px; BACKGROUND-COLOR: #ffffff; BORDER-RIGHT-COLOR: #ffffff"></iframe>
                                        </td>
                                        </tr>
                                    </table>  
                                </asp:View>
                        </asp:MultiView>
                    </td>
                 </tr>
            </table>
            <br />
        </div>

    </form>
</body>
</html>

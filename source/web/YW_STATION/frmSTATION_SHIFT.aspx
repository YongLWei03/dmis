<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSTATION_SHIFT.aspx.cs" Inherits="YW_STATION_frmSTATION_SHIFT" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>变电站值班记录</title>
   <link href="../App_Themes/default/DefaultStyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body style="padding-right: 5px; padding-left: 5px;">
    <form id="form1" runat="server">
       <div id="list_header">
           <img src="../img/s_img.gif" alt="" />
            <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
       </div>
       <div id="list_control">
            <table class="invisible_table" style="width: 84%">
                <tr>
                    <td class="invisible_cell">
                        <asp:Label ID="lblQueryDate" runat="server" Text="日期：" meta:resourcekey="lblQueryDateResource1"></asp:Label></td>
                    <td class="invisible_cell">
                        <cc1:WebDate ID="wdlQueryDate" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlQueryDateResource1" />
                    </td>
                    <td class="invisible_cell">
                        <span style="font-size: 9pt">
                            <asp:Label ID="lblQueryShift" runat="server" Text="班次：" Width="39px" meta:resourcekey="lblQueryShiftResource1"></asp:Label></span></td>
                    <td class="invisible_cell">
                        <asp:DropDownList ID="ddlQueryShift" runat="server" Width="91px" meta:resourcekey="ddlQueryShiftResource1">
                        </asp:DropDownList></td>
                    <td class="invisible_cell">
                        <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="检索" meta:resourcekey="btnQueryResource1" /></td>
                    <td class="invisible_cell">
                        <asp:Button ID="btnCurrentShift" runat="server" OnClick="btnCurrentShift_Click" Text="本班" meta:resourcekey="btnCurrentShiftResource1" /></td>
                    <td class="invisible_cell">
                        <asp:Button ID="btnSave" runat="server" Enabled="False" OnClick="btnSave_Click" Text="保存" meta:resourcekey="btnSaveResource1" /></td>
                    <td class="invisible_cell">
                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="打印" Enabled="False" meta:resourcekey="btnPrintResource1" /></td>
                    <td class="invisible_cell">
                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" Visible="False" meta:resourcekey="btnAddResource1" /></td>
                    <td class="invisible_cell">
                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除" Visible="False" meta:resourcekey="btnDeleteResource1" /></td>
                    <td class="invisible_cell">
                        </td>
                </tr>
            </table>
        </div>
       <div id="list_data">
            <table runat="server" cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td class="SelectedTopBorder" id="Cell1" align="center" style="width: 70px; height: 19px;">
                            <asp:LinkButton ID="lbnView1" runat="server"  OnClick="lbnView1_Click" meta:resourcekey="lbnView1Resource1" Text="值班信息"></asp:LinkButton></td>
                        <td class="SepBorder" style="width: 2px; height: 19px;">
                            &nbsp;</td>
                        <td class="TopBorder" id="Cell2" align="center"  style="width: 70px; height: 19px;">
                            <asp:LinkButton ID="lbnView2" runat="server" OnClick="lbnView2_Click" meta:resourcekey="lbnView2Resource1" Text="运行方式"></asp:LinkButton></td>
                        <td class="SepBorder" style="width: 2px; height: 19px;">
                            &nbsp;</td>
                        <td class="TopBorder" id="Cell3" align="center"  style="width: 70px; height: 19px;">
                            <asp:LinkButton ID="lbnView3" runat="server" OnClick="lbnView3_Click" meta:resourcekey="lbnView3Resource1" Text="异常情况"></asp:LinkButton></td>
                        <td class="SepBorder" style="width: 2px; height: 19px;">
                            &nbsp;</td>
                        <td class="TopBorder" id="Cell4" align="center"  style="width: 70px; height: 19px;">
                            <asp:LinkButton ID="lbnView4" runat="server" OnClick="lbnView4_Click" meta:resourcekey="lbnView4Resource1" Text="其他情况"></asp:LinkButton></td>
                        <td class="SepBorderNew" id="tdMessage" style="height: 19px; text-align: right; width: 681px;" >
                            <asp:Label ID="lblMessage" runat="server" ForeColor="#FF0033" Width="291px" Font-Size="9pt" EnableTheming="False" meta:resourcekey="lblMessageResource1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="8" style="border-left: gray 1px solid;border-right: gray 1px solid;border-bottom: Gray 1px solid">
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                            <br />
                            <br />
                            <asp:TextBox ID="txtTID" runat="server" Width="39px" Visible="False" meta:resourcekey="txtTIDResource1"></asp:TextBox>
                            <asp:TextBox ID="txtFLAG" runat="server" Width="32px" Visible="False" meta:resourcekey="txtFLAGResource1"></asp:TextBox>
                            <asp:TextBox ID="txtSTATION_ID" runat="server" Visible="False" Width="33px" meta:resourcekey="txtSTATION_IDResource1"></asp:TextBox>

                            <table class="slim_table" style="width:90%;">
                                        <tr >
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblDATEM" runat="server" Text="日期" meta:resourcekey="lblDATEMResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <cc1:WebDate ID="wdlDATEM" runat="server" DateTimeStyle="Date" DateStyle="DateFormat3" Enabled="False" ButtonText=".." meta:resourcekey="wdlDATEMResource1" myDateWidth="80px"  />
                                            </td>
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblSHIFT" runat="server" Text="班次" meta:resourcekey="lblSHIFTResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:DropDownList ID="ddlSHIFT" runat="server" Width="108px" Enabled="False" meta:resourcekey="ddlSHIFTResource1">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr >
                                            <td class="slim_table_td_desc">
                                                <asp:Label ID="lblWEATHER" runat="server" Text="天气" meta:resourcekey="lblWEATHERResource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:DropDownList ID="ddlWEATHER" runat="server" Width="115px" meta:resourcekey="ddlWEATHERResource1"></asp:DropDownList>
                                            </td>
                                            <td  class="slim_table_td_desc">
                                            </td>
                                            <td class="slim_table_td_control">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 44px" >
                                                <span style="color: background; font-size: 12pt;">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Text="当    班" Width="104px" meta:resourcekey="Label1Resource1" Font-Size="12pt"></asp:Label></strong></span></td>
                                        </tr>
                                        <tr>
                                            <td class="slim_table_td_desc">
                                                <asp:Label ID="lblCURRENT_SHIFT_MAN1" runat="server" Text="正值调度员" meta:resourcekey="lblCURRENT_SHIFT_MAN1Resource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:DropDownList ID="ddlCURRENT_SHIFT_MAN1" runat="server" Width="114px" meta:resourcekey="ddlCURRENT_SHIFT_MAN1Resource1"></asp:DropDownList></td>
                                            <td class="slim_table_td_desc">
                                                <asp:Label ID="lblCURRENT_SHIFT_MAN2" runat="server" Text="副值调度员1" meta:resourcekey="lblCURRENT_SHIFT_MAN2Resource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:DropDownList ID="ddlCURRENT_SHIFT_MAN2" runat="server" Width="112px" meta:resourcekey="ddlCURRENT_SHIFT_MAN2Resource1">
                                                </asp:DropDownList>
                                                </td>
                                        </tr>
                                        <tr >
                                            <td class="slim_table_td_desc">
                                                <asp:Label ID="lblCURRENT_SHIFT_MAN3" runat="server" Text="副值调度员2 " meta:resourcekey="lblCURRENT_SHIFT_MAN3Resource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:DropDownList ID="ddlCURRENT_SHIFT_MAN3" runat="server" Width="112px" meta:resourcekey="ddlCURRENT_SHIFT_MAN3Resource1"></asp:DropDownList></td>
                                            <td class="slim_table_td_desc">
                                                <asp:Label ID="lblCURRENT_SHIFT_MAN4" runat="server" Text="副值调度员3" meta:resourcekey="lblCURRENT_SHIFT_MAN4Resource1"></asp:Label>
                                            </td>
                                            <td class="slim_table_td_control">
                                                <asp:DropDownList ID="ddlCURRENT_SHIFT_MAN4" runat="server" Width="109px" meta:resourcekey="ddlCURRENT_SHIFT_MAN4Resource1"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="font-size: 12pt; height: 40px">
                                            <td colspan="4" style="height: 40px">
                                                <strong><span style="color: activecaption">
                                                    <asp:Label ID="Label2" runat="server" Text="接    班" Width="100px" meta:resourcekey="Label2Resource1" Font-Size="12pt"></asp:Label></span></strong>
                                            </td>
                                        </tr>
                                        <tr style="font-size: 12pt; height: 40px">
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblNEXT_SHIFT_MAN1" runat="server" Text="正值调度员" meta:resourcekey="lblNEXT_SHIFT_MAN1Resource1" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td class="slim_table_td_control">
                                                <asp:DropDownList ID="ddlNEXT_SHIFT_MAN1" runat="server" Width="109px" meta:resourcekey="ddlNEXT_SHIFT_MAN1Resource1"></asp:DropDownList></td>
                                            <td  class="slim_table_td_desc">
                                                <asp:Label ID="lblNEXT_SHIFT_MAN2" runat="server" Text="副值调度员1" meta:resourcekey="lblNEXT_SHIFT_MAN2Resource1" Font-Size="9pt"></asp:Label></td>
                                            <td class="slim_table_td_control"><asp:DropDownList ID="ddlNEXT_SHIFT_MAN2" runat="server" Width="109px" meta:resourcekey="ddlNEXT_SHIFT_MAN2Resource1">
                                            </asp:DropDownList></td>
                                        </tr>
                                        <tr >
                                            <td class="slim_table_td_desc">
                                                <asp:Label ID="lblNEXT_SHIFT_MAN3" runat="server" Text="副值调度员2" meta:resourcekey="lblNEXT_SHIFT_MAN3Resource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:DropDownList ID="ddlNEXT_SHIFT_MAN3" runat="server" Width="109px" meta:resourcekey="ddlNEXT_SHIFT_MAN3Resource1"></asp:DropDownList></td>
                                            <td  class="slim_table_td_desc">
                                                <asp:Label ID="lblNEXT_SHIFT_MAN4" runat="server" Text="副值调度员3" meta:resourcekey="lblNEXT_SHIFT_MAN4Resource1"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <asp:DropDownList ID="ddlNEXT_SHIFT_MAN4" runat="server" Width="109px" meta:resourcekey="ddlNEXT_SHIFT_MAN4Resource1"></asp:DropDownList></td>
                                        </tr>
                                        <tr style="font-size: 12pt; height: 40px">
                                            <td class="slim_table_td_desc" >
                                                <asp:Label ID="lblJOIN_TIME" runat="server" Text="交班时间" meta:resourcekey="lblJOIN_TIMEResource1" Font-Size="9pt"></asp:Label></td>
                                            <td class="slim_table_td_control">
                                                <cc1:WebDate ID="wdlJOIN_TIME" runat="server" ButtonText=".." meta:resourcekey="wdlJOIN_TIMEResource1"  /></td>
                                            <td align="left" class="slim_table_td_control" colspan="2">
                                                <asp:Button ID="btnJIAOBAN" runat="server" Font-Bold="True" ForeColor="Black" Height="26px"
                                                Text="交  班" OnClick="btnJIAOBAN_Click" OnClientClick='return confirm(&quot;确定要交班?&quot;);' meta:resourcekey="btnJIAOBANResource1"  />&nbsp;</td>
                                        </tr>
                                     </table>
                                  <br />
                                  <br />
                               </asp:View>
                            <asp:View ID="View2" runat="server">
                                    <div style="text-align:left;width:95%">
                                           <br />
                                           <asp:Label ID="lblRUN_MODE" runat="server" Text="当前运行方式" Font-Bold="True"  meta:resourcekey="lblRUN_MODEResource1"></asp:Label>
                                           <asp:TextBox ID="txtRUN_MODE" runat="server" Height="200px" Width="100%" TextMode="MultiLine" meta:resourcekey="txtRUN_MODEResource1"></asp:TextBox>
                                           <br />
                                           <asp:Label ID="lblSTATUS1" runat="server" Text="非正常运行方式" Font-Bold="True"  meta:resourcekey="lblSTATUS1Resource1"></asp:Label>
                                           <asp:TextBox ID="txtSTATUS1" runat="server"  Height="200px" TextMode="MultiLine" Width="100%" meta:resourcekey="txtSTATUS1Resource1"></asp:TextBox>
                                            <br />
                                    </div>

                                </asp:View>
                            <asp:View ID="View3" runat="server">
                                    <div style="text-align:left;width:95%">
                                            <br />
                                            <asp:Label ID="lblSTATUS2" runat="server" Text="事故（障碍）情况" Font-Bold="True" meta:resourcekey="lblSTATUS2Resource1"></asp:Label>
                                            <asp:TextBox ID="txtSTATUS2" runat="server"  Height="130px" TextMode="MultiLine" Width="100%" meta:resourcekey="txtSTATUS2Resource1"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="lblSTATUS3" runat="server" Text="检修（试验）情况" Font-Bold="True"  meta:resourcekey="lblSTATUS3Resource1"></asp:Label>
                                            <asp:TextBox ID="txtSTATUS3" runat="server"  Height="130px" TextMode="MultiLine" Width="100%" meta:resourcekey="txtSTATUS3Resource1"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="lblSTATUS4" runat="server" Text="通信、自动化异常情况" Font-Bold="True"  meta:resourcekey="lblSTATUS4Resource1"></asp:Label>
                                            <asp:TextBox ID="txtSTATUS4" runat="server" Height="130px" TextMode="MultiLine" Width="100%" meta:resourcekey="txtSTATUS4Resource1"></asp:TextBox>
                                            <br />                                    
                                    </div>
                                </asp:View>
                            <asp:View ID="View4" runat="server">
                                <div style="text-align:left;width:95%">
                                    <br />
                                    <asp:Label ID="lblSTATUS5" runat="server" Text="其他交接工作" Font-Bold="True"  meta:resourcekey="lblSTATUS5Resource1"></asp:Label>
                                    <asp:TextBox ID="txtSTATUS5" runat="server" Height="300px" Width="99.5%" CssClass="mulinput" TextMode="MultiLine" meta:resourcekey="txtSTATUS5Resource1"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblSTATUS6" runat="server" Font-Bold="True" Text="领导指示、重要文件"  meta:resourcekey="lblSTATUS6Resource1"></asp:Label>
                                    <asp:TextBox ID="txtSTATUS6" runat="server" Height="100px" Width="100%" CssClass="mulinput" TextMode="MultiLine" meta:resourcekey="txtSTATUS6Resource1"></asp:TextBox>
                                    <br />
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    
                    </td>
                    </tr>
                </table>
 
       </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectReassignMember.aspx.cs" Inherits="SYS_WorkFlow_SelectReassignMember" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>任务改派--选择人员</title>
</head>
<body style="text-align: center; ">
    <div >
    <form id="form1" runat="server">
    
    <table  style="background-color: #000000; width: 445px; height: 313px;" border="0" cellpadding="0" cellspacing="1">
        <tr>
            <td align="center" style="height: 20px; background-color: skyblue; width: 563px;">
                <strong>
                    <asp:Label ID="lblFuncName" runat="server" Text="任务改派--选择人员" meta:resourcekey="lblFuncNameResource1"></asp:Label></strong></td>
        </tr>
        <tr>
            <td align="center" style="width: 563px; background-color: white; height: 248px;">
                <br />
                <table border="0" cellpadding="0" cellspacing="1" style="width: 408px; height: 147px;
                    background-color: #330000">
                    <tr>
                        <td style="font-size: 9pt; width: 15%; height: 26px; background-color: gainsboro;
                            text-align: center">
                            <asp:Label ID="lblPackDesc" runat="server" Text="任务描述" meta:resourcekey="lblPackDescResource1"></asp:Label></td>
                        <td id="tdPackDesc" runat="server" colspan="3" style="font-size: 9pt; height: 26px;
                            background-color: #ffffff; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 9pt; width: 15%; height: 26px; background-color: gainsboro;
                            text-align: center">
                            <asp:Label ID="lblF_FLOWNAME" runat="server" Text="办理环节" meta:resourcekey="lblF_FLOWNAMEResource1"></asp:Label></td>
                        <td id="TD1" style="width: 35%; height: 26px; background-color: #ffffff">
                            <asp:TextBox ID="txtF_FLOWNAME" runat="server" BorderStyle="None" Enabled="False"
                                Width="137px" meta:resourcekey="txtF_FLOWNAMEResource1"></asp:TextBox></td>
                        <td style="font-size: 9pt; width: 15%; height: 26px; background-color: gainsboro;
                            text-align: center">
                            <asp:Label ID="lblMEMBER_NAME" runat="server" Text="办理人" meta:resourcekey="lblMEMBER_NAMEResource1"></asp:Label></td>
                        <td id="TD3" style="width: 35%; height: 26px; background-color: #ffffff">
                            <asp:TextBox ID="txtMEMBER_NAME" runat="server" BorderStyle="None" Enabled="False"
                                Width="127px" meta:resourcekey="txtMEMBER_NAMEResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="font-size: 9pt; width: 15%; height: 26px; background-color: gainsboro;
                            text-align: center">
                            <asp:Label ID="lblDepart" runat="server" Text="班组" meta:resourcekey="lblDepartResource1"></asp:Label></td>
                        <td style="width: 35%; height: 26px; background-color: #ffffff">
                            <asp:DropDownList ID="ddlDepart" runat="server" Width="121px" AutoPostBack="True" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" meta:resourcekey="ddlDepartResource1">
                            </asp:DropDownList></td>
                        <td style="font-size: 9pt; width: 15%; height: 26px; background-color: gainsboro;
                            text-align: center">
                        </td>
                        <td style="width: 35%; height: 26px; background-color: #ffffff">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 9pt; width: 15%; height: 65px; background-color: gainsboro;
                            text-align: center">
                            <asp:Label ID="lblMember" runat="server" Text="改派人员" meta:resourcekey="lblMemberResource1"></asp:Label></td>
                        <td colspan="3" style="width: 75%; height: 65px; background-color: #ffffff">
                            <asp:RadioButtonList ID="rblMember" runat="server" Height="97px" RepeatColumns="3"
                                RepeatDirection="Horizontal" Width="330px" meta:resourcekey="rblMemberResource1">
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 28px; background-color: #ffffff; text-align: center">
                            <asp:Button ID="btnOk" runat="server" Text="确定" OnClick="btnOk_Click" Width="55px" meta:resourcekey="btnOkResource1" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClientClick="window.close();" Width="49px" meta:resourcekey="btnCancelResource1" /></td>
                    </tr>
                    <tr>
                        <td id="tdMessage" runat="server" align="left" colspan="4" style="color: red; height: 25px;
                            background-color: #ffffff; text-align: left">
                        </td>
                    </tr>
                </table>
  
             </td>
         </tr>
     </table>
    </form>
    </div>
</body>
</html>

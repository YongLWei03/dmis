<%@ page language="C#" autoeventwireup="true" inherits="frmDetailView, App_Web_og9prjkz" theme="default" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>详细内容</title>
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
    <div>
        <table style="width: 518px">
            <tr>
                <td style="width: 25%; text-align: center;" class="captiontd">
                    <img src="img/edit.gif" /></td>
                <td colspan="2" id="tdTitle" runat="server" class="captiontd">
                </td>
                <td style="width: 25%" class="captiontd">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:Table ID="tabMainTable" runat="server" Width="100%" CssClass="detailtable">
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center"><asp:Table ID="tabSecondTable1" runat="server" Width="100%" CssClass="detailtable">
                </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

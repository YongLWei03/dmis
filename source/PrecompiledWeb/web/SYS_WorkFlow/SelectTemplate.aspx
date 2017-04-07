<%@ page language="C#" autoeventwireup="true" inherits="SYS_WorkFlow_SelectTemplate, App_Web_iakvuhia" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择业务类别</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                    <table style="width: 224px; height: 311px;">
                    <tr>
                        <td colspan="6" style="height: 150px; border-bottom: menu 1px dashed; width: 220px; text-align: center;" valign="top">
                            <asp:GridView ID="grvList" runat="server" AutoGenerateColumns="False" CaptionAlign="Top" OnSelectedIndexChanged="grvList_SelectedIndexChanged"
                                Width="208px" Font-Size="10pt" DataKeyNames="f_no">
                                <Columns>
                                    <asp:CommandField HeaderText="选择" SelectText="&lt;img border=0 align=absmiddle src=../../img/Unselected.gif&gt;"
                                        ShowSelectButton="True" />
                                    <asp:BoundField DataField="f_name" HeaderText="业务类别名称" />
                                </Columns>
                                <RowStyle Height="25px" />
                                <HeaderStyle Height="25px" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                     <td style="height: 26px; text-align: center; width: 220px;" colspan="6">
                         <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />&nbsp; &nbsp;&nbsp;&nbsp;
                         <asp:Button ID="btnCancel" runat="server" Text="取消" OnClientClick="window.close();" /></td>
                    </tr>
                </table>
    </div>
    </form>
</body>
</html>

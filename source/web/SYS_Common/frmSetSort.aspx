<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSetSort.aspx.cs" Inherits="frmSetSort"  Theme="default" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body  style="text-align: center; ">
    <div style="padding-top: 2%;">
    <form id="form1" runat="server">
    
    <table width="80%"  style="background-color: #000000" border="0" cellpadding="0" cellspacing="1">
        <tr>
            <td align="center" style="height: 20px; background-color: skyblue; width: 672px;">
                <strong>
                    <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" Text="选择排序条件"></asp:Label></strong></td>
        </tr>
        <tr>
            <td align="center" style="width: 672px; background-color: white">
                <div >
                   <table style="width: 559px">
                       <tr>
                           <td style="width: 405px; height: 25px; text-align: left">
                               <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" Text="可选列："></asp:Label></td>
                           <td style="width: 100px; height: 25px; text-align: center">
                           </td>
                           <td style="height: 25px; text-align: left" colspan="2">
                               <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" Text="已选列："></asp:Label></td>
                       </tr>
                       <tr>
                           <td rowspan="7" style="width: 405px; text-align: center" valign="top">
                               <asp:Panel ID="Panel1" runat="server" Height="345px" ScrollBars="Vertical"
                                   Width="90%" HorizontalAlign="Left" meta:resourcekey="Panel1Resource1">
                                   <asp:GridView ID="grvColumns" runat="server" AutoGenerateColumns="False" Height="20px" DataKeyNames="NAME" OnSelectedIndexChanged="grvColumns_SelectedIndexChanged" Width="100%" meta:resourcekey="grvColumnsResource1">
                                       <Columns>
                                           <asp:CommandField SelectText="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;"
                                               ShowSelectButton="True" meta:resourcekey="CommandFieldResource1" CancelText="" DeleteText="" EditText="" InsertText="" NewText="" UpdateText="" >
                                               <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                           </asp:CommandField>
                                           <asp:BoundField DataField="DESCR" HeaderText="列名" meta:resourcekey="BoundFieldResource1" HtmlEncode="False" />
                                       </Columns>
                                   </asp:GridView>
                               </asp:Panel>
                           </td>
                           <td style="width: 100px; height: 44px; text-align: center">
                           </td>
                           <td style="text-align: center" colspan="2" rowspan="6" valign="top"><asp:GridView ID="grvSelectedColumns" runat="server" AutoGenerateColumns="False" Height="20px" OnSelectedIndexChanged="grvSelectedColumns_SelectedIndexChanged" Width="100%" DataKeyNames="ORDER" OnRowCommand="grvSelectedColumns_RowCommand" meta:resourcekey="grvSelectedColumnsResource1">
                               <Columns>
                                   <asp:CommandField SelectText="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;"
                                               ShowSelectButton="True" meta:resourcekey="CommandFieldResource2" CancelText="" DeleteText="" EditText="" InsertText="" NewText="" UpdateText="" >
                                       <ItemStyle Width="30px" />
                                   </asp:CommandField>
                                   <asp:BoundField DataField="ORDER" HeaderText="序号" meta:resourcekey="BoundFieldResource2">
                                       <ItemStyle Width="0px" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="NAME" HeaderText="列代码" Visible="False" meta:resourcekey="BoundFieldResource3">
                                       <ItemStyle Width="0px" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="DESCR" HeaderText="列名" meta:resourcekey="BoundFieldResource4" />
                                   <asp:ButtonField ButtonType="Button" CommandName="Order" DataTextField="ORDER_TYPE"
                                       Text="升" meta:resourcekey="ButtonFieldResource1" />
                               </Columns>
                           </asp:GridView>
                           </td>
                       </tr>
                       <tr>
                           <td style="width: 100px; height: 44px; text-align: center">
                               <asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" meta:resourcekey="btnAddResource1" Width="63px" /></td>
                       </tr>
                       <tr>
                           <td style="width: 100px; height: 44px; text-align: center">
                               <asp:Button ID="btnDel" runat="server" Text="删除" OnClick="btnDel_Click" meta:resourcekey="btnDelResource1" Width="63px" /></td>
                       </tr>
                       <tr>
                           <td style="width: 100px; height: 44px; text-align: center">
                               <asp:Button ID="btnUp" runat="server" Text="上移" OnClick="btnUp_Click" meta:resourcekey="btnUpResource1" Width="63px" /></td>
                       </tr>
                       <tr>
                           <td style="width: 100px; height: 44px; text-align: center">
                               <asp:Button ID="btnDown" runat="server" Text="下移" OnClick="btnDown_Click" meta:resourcekey="btnDownResource1" Width="63px" /></td>
                       </tr>
                       <tr>
                           <td style="width: 100px; height: 44px; text-align: center">
                           </td>
                       </tr>
                        <tr>
                            <td style="width: 100px; text-align: center; height: 37px;">
                                </td>
                            <td style="width: 222px; height: 37px; text-align: center">
                                <asp:Button ID="btnOk" runat="server" Text="确定" OnClick="btnOk_Click" meta:resourcekey="btnOkResource1" /></td>
                            <td style="width: 223px; text-align: center; height: 37px;">
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="取消" meta:resourcekey="btnCancelResource1" /></td>
                        </tr>
                    </table>
                 </div>
             </td>
         </tr>
     </table>
    </form>
    </div>
</body>
</html>

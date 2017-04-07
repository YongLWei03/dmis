<%@ page language="C#" autoeventwireup="true" inherits="SYS_Common_frmSetParamsByGridView, App_Web_og9prjkz" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
    <div style="padding-top: 2%;">
       <table  style="background-color: #000000; width: 80%;" border="0" cellpadding="0" cellspacing="1">
        <tr>
            <td align="center" style="height: 20px; background-color: lightskyblue;  font-size: 12pt;">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="DarkRed"></asp:Label></td>
        </tr>
        <tr>
            <td align="center" style="background-color: white; height: 178px;">
    <asp:GridView id="grvTable" runat="server" EmptyDataText="没有满足条件的记录！" CellPadding="4" PageSize="20" DataKeyNames="TID" Width="100%" AutoGenerateColumns="False" CssClass="font" OnRowCancelingEdit="grvRef_RowCancelingEdit" OnRowCommand="grvTable_RowCommand" OnRowDeleting="grvTable_RowDeleting" OnRowEditing="grvRef_RowEditing" OnRowUpdating="grvRef_RowUpdating">
            <PagerSettings  Visible="False"  />
        <Columns>
            <asp:CommandField ButtonType="Image" CancelImageUrl="~/img/quxiao.gif" CancelText=""
                DeleteImageUrl="~/img/delete.gif" DeleteText="" EditImageUrl="~/img/modifyFlag.gif"
                EditText="" HeaderText="编辑" InsertText="" NewImageUrl="~/img/insert.gif" NewText=""
                ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" UpdateImageUrl="~/img/save.gif"
                UpdateText="">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:CommandField>
            <asp:BoundField />
        </Columns>
    </asp:GridView>
  
             </td>
         </tr>
           <tr>
               <td align="center" style="background-color: aliceblue; height: 22px; text-align: left;" id="tdPageMessage" runat="server">
               </td>
           </tr>
     </table>   
    </div>
    </form>
</body>
</html>

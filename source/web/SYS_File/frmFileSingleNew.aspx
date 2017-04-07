<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFileSingleNew.aspx.cs" Inherits="SYS_File_frmFileSingleNew" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="CustomControlLib" Namespace="PlatForm.CustomControlLib" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>单文档上传</title>
</head>
<body class="detail_body">
    <form id="form1" runat="server">
        <div id="detail_head">
             <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
        </div>
         <div id="detail_data">
            <br />
            <table class="slim_table">
                <tr>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblDESCR" runat="server" Text="文档描述" meta:resourcekey="lblDESCRResource1"></asp:Label></td>
                    <td class="slim_table_td_control">
                    <asp:TextBox ID="txtDESCR" runat="server" Width="182px" meta:resourcekey="txtDESCRResource1" /></td>
                    <td class="slim_table_td_desc">
                    </td>
                    <td class="slim_table_td_control">
                    <asp:TextBox ID="txtFILE_PATH" runat="server" EnableTheming="False" Visible="False" Width="29px" meta:resourcekey="txtFILE_PATHResource1" />
                    <asp:TextBox ID="txtTID" runat="server" EnableTheming="False" Visible="False" Width="29px" meta:resourcekey="txtTIDResource1"></asp:TextBox>
                    <asp:TextBox ID="txtMODULE_ID" runat="server" EnableTheming="False" Visible="False"
                                    Width="29px" meta:resourcekey="txtMODULE_IDResource1"></asp:TextBox>
                    <asp:TextBox ID="txtFILE_SUFFIX" runat="server" EnableTheming="False" Visible="False"
                                    Width="29px" meta:resourcekey="txtFILE_SUFFIXResource1"></asp:TextBox></td>                    
                </tr>
                <tr>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblDATEM" runat="server" Text="上传日期" meta:resourcekey="lblDATEMResource1"></asp:Label></td>
                    <td class="slim_table_td_control">
                    <cc1:WebDate ID="wdlDATEM" runat="server" DateTimeStyle="Date" DateStyle="DateFormat3" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlDATEMResource1" />
                    </td>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblSCR" runat="server" Text="上传人" meta:resourcekey="lblSCRResource1"></asp:Label></td>
                    <td class="slim_table_td_control">
                    <asp:TextBox ID="txtSCR" runat="server" meta:resourcekey="txtSCRResource1"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblNOTE" runat="server" Text="备注" meta:resourcekey="lblNOTEResource1"></asp:Label></td>
                    <td class="slim_table_td_control" colspan="3">
                    <asp:TextBox ID="txtNOTE" runat="server" Height="64px" TextMode="MultiLine" Width="519px" meta:resourcekey="txtNOTEResource1"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblFILE_NAME" runat="server" Text="文件名" meta:resourcekey="lblFILE_NAMEResource1"></asp:Label></td>
                    <td class="slim_table_td_control" colspan="3">
                        <asp:TextBox ID="txtFILE_NAME" runat="server" Width="522px" Enabled="False" meta:resourcekey="txtFILE_NAMEResource1"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="slim_table_td_desc">
                        <asp:Label ID="lblUpload" runat="server" Text="上传文件" meta:resourcekey="lblUploadResource1"></asp:Label></td>
                    <td class="slim_table_td_control" colspan="3">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100px; height: 22px;">
                    <asp:FileUpload ID="fulFile" runat="server" Width="436px" meta:resourcekey="fulFileResource1" /></td>
                            <td align="center" style="width: 66px; height: 22px;">
                    <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" Enabled="False" meta:resourcekey="btnUploadResource1" /></td>
                        </tr>
                    </table>
                    </td>
                </tr>
             </table>
             <br />
         </div>

      <div id="detail_control">
      <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" Enabled="False" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnCancel" runat="server" Text="返回" OnClick="btnReturn_Click" meta:resourcekey="btnCancelResource1" />
      </div>
      <div id="detail_info" runat="server">
      </div>


    </form>
</body>
</html>

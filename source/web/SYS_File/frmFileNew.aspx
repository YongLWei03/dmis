<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFileNew.aspx.cs" Inherits="frmFileNew" theme ="default" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="CustomControlLib" Namespace="PlatForm.CustomControlLib" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>创建文件</title>
</head>
<body class="detail_body">
    <form id="form1" runat="server">
     <div id="detail_head">
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
    </div>
    <div id="detail_control">
        <asp:TextBox ID="txtTID" runat="server" EnableTheming="False" Visible="False" Width="29px" meta:resourcekey="txtTIDResource1"></asp:TextBox>
        <asp:TextBox ID="txtMODULE_ID" runat="server" EnableTheming="False" Visible="False" Width="29px" meta:resourcekey="txtMODULE_IDResource1"></asp:TextBox>
        <asp:Button ID="btnSaveCancel" runat="server" Text="保存并返回" OnClick="btnSaveCancel_Click" Enabled="False" meta:resourcekey="btnSaveCancelResource1" />
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" Enabled="False" meta:resourcekey="btnSaveResource1" />
        <asp:Button ID="btnCancel" runat="server" Text="返回" OnClick="btnReturn_Click" meta:resourcekey="btnCancelResource1" />
    </div>
    <div id="detail_data">
        <table class="slim_table">
	        <tr>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblDESCR" runat="server" Text="文档描述*" meta:resourcekey="lblDESCRResource1"></asp:Label></td>
                <td class="slim_table_td_control" colspan="3" >
                                <asp:TextBox ID="txtDESCR" runat="server" Width="518px" meta:resourcekey="txtDESCRResource1"></asp:TextBox></td>
	        </tr>
	        <tr>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblNEW_DATE" runat="server" Text="创建日期" meta:resourcekey="lblNEW_DATEResource1"></asp:Label></td>
                    <td class="slim_table_td_control" >
                                <cc1:WebDate ID="wdlNEW_DATE" runat="server" DateStyle="DateFormat3" myDateWidth="80px" DateTimeStyle="Date" ButtonText=".." meta:resourcekey="wdlNEW_DATEResource1" />
	            </td>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblMEMBER" runat="server" Text="创建人" meta:resourcekey="lblMEMBERResource1"></asp:Label></td>
                <td class="slim_table_td_control" >
                    <asp:TextBox ID="txtMEMBER" runat="server" meta:resourcekey="txtMEMBERResource1"></asp:TextBox></td>
	        </tr>
	        <tr>
                <td class="slim_table_td_desc" >
                    <asp:Label ID="lblNOTE" runat="server" Text="备注" meta:resourcekey="lblNOTEResource1"></asp:Label></td>
                 <td class="slim_table_td_control" colspan="3" >
                    <asp:TextBox ID="txtNOTE" runat="server" Height="55px" TextMode="MultiLine" Width="519px" meta:resourcekey="txtNOTEResource1"></asp:TextBox></td>
	        </tr>
        </table>
    </div>
    <div id="detail_info" runat="server">
    </div>
    <div style="background-image: url(../img/pics.jpg);width: 80%; height: 20px;">
        <asp:Label ID="Label5" runat="server" Text="附件" Width="90px" meta:resourcekey="Label5Resource1"></asp:Label>
    </div>
    <div style="width: 80%;">     
        <asp:GridView ID="grvUpFileList" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="TID" OnRowCommand="grvUpFileList_RowCommand" OnRowDataBound="grvUpFileList_RowDataBound" CssClass="font" meta:resourcekey="grvUpFileListResource1">
            <Columns>
                <asp:BoundField DataField="TID" HeaderText="文件编号" meta:resourcekey="BoundFieldResource1" />
                <asp:BoundField DataField="FILE_ID" HeaderText="文件ID" meta:resourcekey="BoundFieldResource2" />
                <asp:BoundField DataField="FILE_PATH" HeaderText="路径" meta:resourcekey="BoundFieldResource3" />
                <asp:BoundField DataField="FILE_NAME" HeaderText="文件名" meta:resourcekey="BoundFieldResource4" />
                <asp:BoundField DataField="UP_DATE" HeaderText="上传日期" DataFormatString="{0:yyyy年MM月dd日}" HtmlEncode="False" meta:resourcekey="BoundFieldResource5" />
                <asp:ButtonField CommandName="del" HeaderText="删除" Text="&lt;img border=0  src=../img/delete.gif&gt;" meta:resourcekey="ButtonFieldResource1">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:ButtonField>
            </Columns>
           </asp:GridView>
    </div>
     <div style="width: 80%;">
         <table style="width: 100%">
            <tr>
                <td style="width: 73px; height: 26px;" class="detailtd">
                    <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" Text="上传文件："></asp:Label></td>
                <td style="width: 221px; height: 26px;">
                    <asp:FileUpload ID="fulFile" runat="server" Width="349px" meta:resourcekey="fulFileResource1" /></td>
                <td style="width: 100px; height: 26px; text-align: center;">
                        <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" Enabled="False" meta:resourcekey="btnUploadResource1" /></td>
                <td style="width: 100px; height: 26px;">
                </td>
            </tr>
        </table>
  
    
    </div>
    </form>
</body>
</html>

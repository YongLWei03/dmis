<%@ page language="C#" autoeventwireup="true" inherits="frmFile, App_Web_d7hnbwar" theme="default" culture="auto" uiculture="auto" meta:resourcekey="PageResource2" stylesheettheme="default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>多文档上传</title>
</head>
<body>
    <form id="form1" runat="server">
   <div id="list_header" runat="server">
       <img src="../img/s_img.gif" alt="" />
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
    </div>
    <div id="list_control">
        <table class="invisible_table">
            <tr>
                <td class="invisible_cell">
                   
                </td>
                <td class="invisible_cell">
                    </td>
                <td style="width: 150px; " >
                </td>
                <td class="invisible_cell">
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" Enabled="False" meta:resourcekey="btnAddResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                        Text="删除" Enabled="False" meta:resourcekey="btnDeleteResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnModify" runat="server" OnClick="btnModify_Click" Text="修改" meta:resourcekey="btnModifyResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="查询" Enabled="False" meta:resourcekey="btnSearchResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnSort" runat="server" OnClick="btnSort_Click" Text="排序" meta:resourcekey="btnSortResource1" /></td>
                <td class="invisible_cell">
                        <asp:Button ID="btnSaveExcel" runat="server" Enabled="False" OnClick="btnSaveExcel_Click"
                            Text="Excel" meta:resourcekey="btnSaveExcelResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="打印" Enabled="False" meta:resourcekey="btnPrintResource1" Visible="False" />
                </td>
	     </tr>
        </table>
    </div>
    <div id="list_data">
         <asp:GridView CssClass="font" ID="grvList" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="TID"  PageSize="20" AllowPaging="True" CellPadding="4" EmptyDataText="没有满足条件的记录！" OnRowCommand="grvList_RowCommand" OnSelectedIndexChanged="grvRef_SelectedIndexChanged" meta:resourcekey="grvListResource1"  >
            <Columns>
               <asp:CommandField ShowSelectButton="True" HeaderText="选择" SelectText="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;" meta:resourcekey="CommandFieldResource1" >
                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
               </asp:CommandField>
                <asp:BoundField DataField="TID" HeaderText="主键" Visible="False" meta:resourcekey="BoundFieldResource1" />
               <asp:BoundField DataField="MODULE_ID" HeaderText="功能ID" Visible="False" meta:resourcekey="BoundFieldResource2" />
               <asp:BoundField DataField="DESCR" HeaderText="文档描述" meta:resourcekey="BoundFieldResource3" >
                   <ItemStyle HorizontalAlign="Left" />
               </asp:BoundField>
               <asp:BoundField DataField="NEW_DATE" HeaderText="创建日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" meta:resourcekey="BoundFieldResource4">
                   <ItemStyle Width="100px" />
               </asp:BoundField>
               <asp:BoundField DataField="MEMBER" HeaderText="创建人" meta:resourcekey="BoundFieldResource5" >
                   <ItemStyle Width="80px" />
               </asp:BoundField>
                <asp:BoundField DataField="NOTE" HeaderText="备注" meta:resourcekey="BoundFieldResource6" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:ButtonField CommandName="view" HeaderText="查看" Text="&lt;img border=0 src=../img/view.gif&gt;" meta:resourcekey="ButtonFieldResource1">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                </asp:ButtonField>
            </Columns>
            <PagerSettings  Visible="False" />
        </asp:GridView>
    </div>
    <div id="list_pager">
        <table class="invisible_table">
            <tr>
                <td id="tdMessage" runat="server" class="pager_info">
                </td>
                <td class="invisible_cell">
                    <asp:Button ID="btnFirst" runat="server" CommandName="first" meta:resourcekey="btnFirstResource1"
                        OnClick="NavigateToPage" Text="首页" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnPrevious" runat="server" CommandName="prev" meta:resourcekey="btnPreviousResource1"
                        OnClick="NavigateToPage" Text="上一页" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnNext" runat="server" CommandName="next" meta:resourcekey="btnNextResource1"
                        OnClick="NavigateToPage" Text="下一页" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnLast" runat="server" CommandName="last" meta:resourcekey="btnLastResource1"
                        OnClick="NavigateToPage" Text="末页" /></td>
                <td  class="invisible_cell">
                    <asp:Label ID="lblTurn" runat="server" Font-Overline="False" Font-Size="9pt" meta:resourcekey="lblTurnResource1"
                        Text="转向"></asp:Label></td>
                <td  class="invisible_cell">
                    <asp:TextBox ID="txtPage" runat="server" meta:resourcekey="txtPageResource1" Width="40px"></asp:TextBox></td>
                <td  class="invisible_cell">
                    <asp:Button ID="btnTurn" runat="server" CommandName="go" meta:resourcekey="btnTurnResource1"
                        OnClick="NavigateToPage" Text="确定" /></td>
            </tr>
        </table>
    </div>
    <input id="refreshPage" runat="server" onpropertychange="javascript:form1.submit();"
            size="0" style="visibility: hidden; width: 68px" type="text" value="0" />
    </form>
</body>
</html>

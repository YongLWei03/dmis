<%@ page language="C#" autoeventwireup="true" inherits="YW_DD_frmDD_ShiftList, App_Web_docfbltz" culture="auto" uiculture="auto" meta:resourcekey="PageResource2" stylesheettheme="default" %>
<%@ Register Src="../uSelectMonth.ascx" TagName="uSelectMonth" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>调度值班历史记录</title>
</head>
<body>
    <form id="form1" runat="server">
    
   <div id="list_header" >
       <img src="../img/s_img.gif" alt="" />
        <asp:Label ID="lblFuncName" runat="server" meta:resourcekey="lblFuncNameResource1"></asp:Label>
    </div>
    <div id="list_control">
        <table  class="invisible_table" style="width: 84%">
            <tr>
                <td class="invisible_cell" style="height: 42px">
                    <uc1:uSelectMonth ID="uwcMonth" runat="server" />
                </td>
                <td class="invisible_cell" style="height: 42px">
                    <asp:Button id="btnQuery" Text="检索" runat="server" OnClick="btnQuery_Click" meta:resourcekey="btnQueryResource1"  /></td>
                <td class="invisible_cell" style="height: 42px">
                    <asp:Button ID="btnSearch" Text="查询" runat="server" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1" /></td>
                <td class="invisible_cell" style="height: 42px">
                    <asp:Button ID="btnSort" Text="排序" runat="server" OnClick="btnSort_Click" meta:resourcekey="btnSortResource1" /></td>
                <td style="width: 261px; height: 42px">
                </td>
                <td class="invisible_cell" style="height: 42px">
                </td>
                <td class="invisible_cell" style="height: 42px">
                </td>
            </tr>
        </table>
    </div>
    <div id="list_data">
        <asp:GridView CssClass="font" ID="grvList" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="TID"  PageSize="31" AllowPaging="True" CellPadding="4" AllowSorting="True" OnSorting="grvList_Sorting" OnRowCommand="grvList_RowCommand" OnRowDataBound="grvList_RowDataBound" meta:resourcekey="grvListResource1" >
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="TID" HeaderText="详细" NavigateUrl="../SYS_Common/frmDetail.aspx"
                    Target="_blank" Text="&lt;img border=0 align=absmiddle src=../img/view.gif /&gt;" DataNavigateUrlFormatString="../SYS_Common/frmDetail.aspx?TID={0}" meta:resourcekey="HyperLinkFieldResource1">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="DATEM" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" SortExpression="DATEM" meta:resourcekey="BoundFieldResource1" >
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="SHIFT" HeaderText="班次" SortExpression="SHIFT" meta:resourcekey="BoundFieldResource2" >
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
               <asp:BoundField DataField="CURRENT_SHIFT_MAN1" HeaderText="调度员1" SortExpression="CURRENT_SHIFT_MAN1" meta:resourcekey="BoundFieldResource3" >
                   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
               </asp:BoundField>
               <asp:BoundField DataField="CURRENT_SHIFT_MAN2" HeaderText="调度员2" SortExpression="CURRENT_SHIFT_MAN2" meta:resourcekey="BoundFieldResource4" >
                   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
               </asp:BoundField>
               <asp:BoundField DataField="CURRENT_SHIFT_MAN3" HeaderText="调度员3" SortExpression="CURRENT_SHIFT_MAN3" meta:resourcekey="BoundFieldResource5" />
                <asp:BoundField DataField="CURRENT_SHIFT_MAN4" HeaderText="调度员4" SortExpression="CURRENT_SHIFT_MAN4" meta:resourcekey="BoundFieldResource6" />
               <asp:BoundField DataField="WEATHER" HeaderText="天气" SortExpression="WEATHER" meta:resourcekey="BoundFieldResource7" >
                   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
               </asp:BoundField>
                <asp:BoundField DataField="FLAG" HeaderText="状态" meta:resourcekey="BoundFieldResource8">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:ButtonField CommandName="ModifyFlag" HeaderText="修改状态" Text="&lt;img border=0 src=../img/modifyFlag.gif&gt;" meta:resourcekey="ButtonFieldResource1">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                    <asp:Button ID="btnFirst" runat="server" CommandName="first" 
                        OnClick="NavigateToPage" Text="首页" meta:resourcekey="btnFirstResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnPrevious" runat="server" CommandName="prev" 
                        OnClick="NavigateToPage" Text="上一页" meta:resourcekey="btnPreviousResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnNext" runat="server" CommandName="next" 
                        OnClick="NavigateToPage" Text="下一页" meta:resourcekey="btnNextResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnLast" runat="server" CommandName="last" 
                        OnClick="NavigateToPage" Text="末页" meta:resourcekey="btnLastResource1" /></td>
                <td  class="invisible_cell">
                    <asp:Label ID="lblTurn" runat="server" Font-Overline="False" Font-Size="9pt" Text="转向" meta:resourcekey="lblTurnResource1"></asp:Label></td>
                <td  class="invisible_cell">
                    <asp:TextBox ID="txtPage" runat="server"  Width="40px" meta:resourcekey="txtPageResource1"></asp:TextBox></td>
                <td  class="invisible_cell">
                    <asp:Button ID="btnTurn" runat="server" CommandName="go" 
                        OnClick="NavigateToPage" Text="确定" meta:resourcekey="btnTurnResource1" /></td>
            </tr>
        </table>
   </div>
   <input id="refreshPage" runat="server" onpropertychange="javascript:form1.submit();"
            size="0" style="visibility: hidden; width: 68px" type="text" value="0" />
    </form>
</body>
</html>

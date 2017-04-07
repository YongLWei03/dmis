<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDD_TYPICAL_OPT.aspx.cs" Inherits="YW_DD_frmDD_TYPICAL_OPT" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>典型票</title>
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
                <td class="invisible_cell" style="height: 24px">
                    <asp:Label ID="lblStation" runat="server" Text="变电站" meta:resourcekey="lblStationResource1"></asp:Label></td>
                <td class="invisible_cell" style="width: 93px; height: 24px">
                    <asp:DropDownList ID="ddlStation" runat="server" Width="107px" meta:resourcekey="ddlStationResource1">
                    </asp:DropDownList></td>
                <td class="invisible_cell" style="height: 24px">
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="检索" meta:resourcekey="btnQueryResource1" /></td>
                <td style="height: 24px;" >
                </td>
                <td class="invisible_cell" style="height: 24px">
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" Enabled="False" meta:resourcekey="btnAddResource1" /></td>
                <td class="invisible_cell" style="height: 24px">
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                        Text="删除" Enabled="False" meta:resourcekey="btnDeleteResource1" /></td>
                <td class="invisible_cell" style="height: 24px">
                    <asp:Button ID="btnModify" runat="server" OnClick="btnModify_Click" Text="修改" meta:resourcekey="btnModifyResource1" /></td>
                <td class="invisible_cell" style="height: 24px">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="查询" Enabled="False" meta:resourcekey="btnSearchResource1" /></td>
                <td class="invisible_cell" style="height: 24px">
                    <asp:Button ID="btnSort" runat="server" OnClick="btnSort_Click" Text="排序" meta:resourcekey="btnSortResource1" /></td>
                <td class="invisible_cell" style="height: 24px">
                    <asp:Button ID="btnSaveExcel" runat="server" Enabled="False" OnClick="btnSaveExcel_Click"
                        Text="Excel" meta:resourcekey="btnSaveExcelResource1" /></td>
                <td class="invisible_cell" style="height: 24px">
                    <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="打印" Enabled="False" meta:resourcekey="btnPrintResource1" Visible="False" /></td>
            </tr>
        </table>
    </div>
    <div id="list_data">
        <asp:GridView ID="grvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" CssClass="font" DataKeyNames="TID" 
                OnSelectedIndexChanged="grvRef_SelectedIndexChanged" PageSize="20" Width="100%" meta:resourcekey="grvListResource1">
                <Columns>
                    <asp:CommandField HeaderText="选择" SelectText="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;"
                        ShowSelectButton="True" meta:resourcekey="CommandFieldResource1" CancelText="" DeleteText="" EditText="" InsertText="" NewText="" UpdateText="">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                    </asp:CommandField>
                    <asp:BoundField DataField="STATION" HeaderText="变电站" meta:resourcekey="BoundFieldResource1" >
                        <ItemStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TASK" HeaderText="操作任务" meta:resourcekey="BoundFieldResource2">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
             <PagerSettings Visible="False" />
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
            size="0" style="visibility: hidden; width: 68px" type="text" value="0" />    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmGL_ACCIDENT_FORECAST.aspx.cs" Inherits="YW_GL_frmGL_ACCIDENT_FORECAST" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>事故预想记录</title>
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
                    <asp:Label ID="lblYear" runat="server" Text="年份" meta:resourcekey="lblYearResource1"></asp:Label></td>
                <td class="invisible_cell">
                    <cc1:HtmlComboBox ID="hcbYear" runat="server" Width="66px" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbYearResource1" Rows="1" SelectedText="">
                        <asp:ListItem meta:resourcekey="ListItemResource1">2009</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource2">2010</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource3">2011</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource4">2012</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource5">2013</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource6">2014</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource7">2015</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource8">2016</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource9">2017</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource10">2018</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource11">2019</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource12">2020</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource13">2021</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource14">2022</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource15">2023</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource16">2024</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource17">2025</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource18">2026</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource19">2027</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource20">2028</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource21">2029</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource22">2030</asp:ListItem>
                    </cc1:HtmlComboBox></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="检索" meta:resourcekey="btnQueryResource1" /></td>
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
                    <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="打印" Enabled="False" meta:resourcekey="btnPrintResource1" /></td>
                <td class="invisible_cell">
                        <asp:Button ID="btnSaveExcel" runat="server" Enabled="False" OnClick="btnSaveExcel_Click"
                            Text="Excel" meta:resourcekey="btnSaveExcelResource1" /></td>
           
            </tr>
        </table>
    </div>
    <div id="list_data">
        <asp:GridView id="grvList" runat="server" meta:resourcekey="grvListResource1" Width="100%" PageSize="20" OnSelectedIndexChanged="grvRef_SelectedIndexChanged" DataKeyNames="TID" CssClass="font" CellPadding="4" AutoGenerateColumns="False" AllowPaging="True" EmptyDataText="">
            <Columns>
                <asp:CommandField HeaderText="选择" SelectText="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;"
                    ShowSelectButton="True" meta:resourcekey="CommandFieldResource1" CancelText="" DeleteText="" EditText="" InsertText="" NewText="" UpdateText="">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"  />
                </asp:CommandField>
                
                <asp:BoundField DataField="DATEM" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" meta:resourcekey="BoundFieldResource3" >
                    <ItemStyle Width="80px" HorizontalAlign="Center"  />
                </asp:BoundField>
                <asp:BoundField DataField="FORECASTER" HeaderText="预想人" meta:resourcekey="BoundFieldResource6">
                    <ItemStyle Width="60px"  />
                </asp:BoundField>
                <asp:BoundField DataField="SUBJECT" HeaderText="事故简题" meta:resourcekey="BoundFieldResource1" >
                    <ItemStyle HorizontalAlign="Left"  />
                </asp:BoundField>
                <asp:BoundField DataField="RUN_MODE" HeaderText="运行方式" meta:resourcekey="BoundFieldResource5" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FAULT_APPEARANCE" HeaderText="故障现象" meta:resourcekey="BoundFieldResource2" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PROCESSING_METHOD" HeaderText="处理方法" meta:resourcekey="BoundFieldResource4">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            <PagerSettings Visible="False"  />
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
                        OnClick="NavigateToPage" Text="下一页" Width="55px" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnLast" runat="server" CommandName="last" meta:resourcekey="btnLastResource1"
                        OnClick="NavigateToPage" Text="末页" Width="45px" /></td>
                <td  class="invisible_cell">
                    <asp:Label ID="lblTurn" runat="server" Font-Overline="False" Font-Size="9pt" meta:resourcekey="lblTurnResource1"
                        Text="转向"></asp:Label></td>
                <td  class="invisible_cell">
                    <asp:TextBox ID="txtPage" runat="server" meta:resourcekey="txtPageResource1" Width="40px"></asp:TextBox>
                </td>
                <td  class="invisible_cell">
                    <asp:Button ID="btnTurn" runat="server" CommandName="go" meta:resourcekey="btnTurnResource1"
                        OnClick="NavigateToPage" Text="确定" Width="46px" /></td>
            </tr>
        </table>
    </div>
    <input id="refreshPage" runat="server" onpropertychange="javascript:form1.submit();"
            size="0" style="visibility: hidden; width: 68px" type="text" value="0" />
    </form>
</body>
</html>

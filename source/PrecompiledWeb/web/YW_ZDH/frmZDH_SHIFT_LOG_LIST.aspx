<%@ page language="C#" autoeventwireup="true" inherits="YW_ZDH_frmZDH_SHIFT_LOG_LIST, App_Web_01guwefr" culture="auto" uiculture="auto" meta:resourcekey="PageResource2" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>自动化值班记事</title>
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
                    <asp:Label ID="lblDate" runat="server" Text="值班日期" Width="56px" meta:resourcekey="lblDateResource1"></asp:Label></td>
                <td class="invisible_cell">
                    <cc1:WebDate ID="wdlDate" runat="server" DateStyle="DateFormat3" DateTimeStyle="Date" myDateWidth="80px" ButtonText=".." meta:resourcekey="wdlDateResource1" />
                </td>
                <td class="invisible_cell">
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="检索" meta:resourcekey="btnQueryResource1" /></td>
                <td style="width: 150px; " >
                </td>
                <td class="invisible_cell">
                    <asp:Button ID="btnCurrentShift" runat="server" OnClick="btnCurrentShift_Click" Text="本值" meta:resourcekey="btnCurrentShiftResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" Enabled="False" meta:resourcekey="btnAddResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除" Enabled="False" meta:resourcekey="btnDeleteResource1" />
                </td>
                <td class="invisible_cell">
                    <asp:Button ID="btnModify" runat="server" OnClick="btnModify_Click" Text="修改" meta:resourcekey="btnModifyResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="查询" Enabled="False" meta:resourcekey="btnSearchResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnSort" runat="server" OnClick="btnSort_Click" Text="排序" meta:resourcekey="btnSortResource1" /></td>
                <td class="invisible_cell">
                    <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="打印" Enabled="False" meta:resourcekey="btnPrintResource1" /></td>
            </tr>
        </table>
    </div>
    <div id="list_data">
        <asp:GridView ID="grvList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CellPadding="4" CssClass="font" DataKeyNames="tid"
            OnRowCancelingEdit="grvRef_RowCancelingEdit" OnRowDataBound="grv_RowDataBound"
            OnRowEditing="grv_RowEditing" OnRowUpdating="grv_RowUpdating" OnSelectedIndexChanged="grvRef_SelectedIndexChanged"
            OnSorting="grvList_Sorting" PageSize="20" Width="100%" meta:resourcekey="grvListResource1">
            <Columns>
                <asp:CommandField HeaderText="选择" SelectText="&lt;img border=0 align=absmiddle src=../img/Unselected.gif&gt;"
                    ShowSelectButton="True" meta:resourcekey="CommandFieldResource1">
                    <ItemStyle Width="30px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="发生时间" SortExpression="DATEM" meta:resourcekey="TemplateFieldResource1">
                    <EditItemTemplate>
                        <cc1:WebDate ID="wdlDATEM" runat="server" DateStyle="DateFormat3" myDateWidth="80px"
                            myHourWidth="15px" myMinuteWidth="15px" ShowLine="DoubleLine" ButtonText=".." meta:resourcekey="wdlDATEMResource1" />
                        &nbsp;
                    </EditItemTemplate>
                    <ItemStyle Width="80px" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DATEM", "{0:yyyy-MM-dd HH:mm}") %>' meta:resourcekey="Label1Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="联系人" SortExpression="LINKMAN" meta:resourcekey="TemplateFieldResource2">
                    <EditItemTemplate>
                        <cc1:HtmlComboBox ID="hcbLINKMAN" runat="server" Width="83px" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbLINKMANResource1" SelectedText="">
                        </cc1:HtmlComboBox>&nbsp;
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("LINKMAN") %>' meta:resourcekey="Label5Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="厂站" SortExpression="STATION" meta:resourcekey="TemplateFieldResource3">
                    <EditItemTemplate>
                        <cc1:HtmlComboBox ID="hcbSTATION" runat="server" Width="97px" EnableAutoFill="False" IsSupportedBrowser="True" MaxLength="0" meta:resourcekey="hcbSTATIONResource1" SelectedText="">
                        </cc1:HtmlComboBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("STATION") %>' meta:resourcekey="Label3Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类别" SortExpression="TYPE" meta:resourcekey="TemplateFieldResource4">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlTYPE" runat="server" Width="122px" meta:resourcekey="ddlTYPEResource1">
                        </asp:DropDownList>&nbsp;
                    </EditItemTemplate>
                    <ItemStyle Width="80px" />
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("TYPE") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="内容" meta:resourcekey="TemplateFieldResource5">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCONTENT" runat="server" Height="66px" Text='<%# Bind("CONTENT") %>' TextMode="MultiLine"
                            Width="268px" meta:resourcekey="txtCONTENTResource1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("CONTENT") %>' meta:resourcekey="Label7Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="结束时间" meta:resourcekey="TemplateFieldResource6">
                    <EditItemTemplate>
                        <cc1:WebDate ID="wdlENDTIME" runat="server" DateStyle="DateFormat3" myDateWidth="80px"
                            myHourWidth="15px" myMinuteWidth="15px" ShowLine="DoubleLine" ButtonText=".." meta:resourcekey="wdlENDTIMEResource1" />
                    </EditItemTemplate>
                    <ItemStyle Width="80px" />
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("ENDTIME", "{0:yyyy-MM-dd HH:mm}") %>' meta:resourcekey="Label8Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField CancelText="&lt;img border=0 align=absmiddle src=../img/quxiao.gif&gt;"
                    EditText="&lt;img border=0 align=absmiddle src=../img/modifyFlag.gif&gt;" HeaderText="编辑"
                    ShowEditButton="True" UpdateText="&lt;img border=0 align=absmiddle src=../img/save.gif&gt;" meta:resourcekey="CommandFieldResource2">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="记录人" meta:resourcekey="TemplateFieldResource7">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRECORDER" runat="server" Width="68px" meta:resourcekey="ddlRECORDERResource1">
                        </asp:DropDownList>&nbsp;
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("RECORDER") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="HOURS" DataFormatString="{0:f2}" HeaderText="小时数" ReadOnly="True" meta:resourcekey="BoundFieldResource1">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
            </Columns>
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

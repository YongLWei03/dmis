<%@ page language="C#" autoeventwireup="true" inherits="SYS_WorkFlow_LoadStatisticByDepart, App_Web_iakvuhia" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>工作流业务统计-按部门班组</title>
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr height="30">
                <td style="background-image: url(../img/main_bar.jpg); background-color: #006699; height: 20px; font-size: 9pt;" valign="middle">
                    <img src="../img/s_img.gif" />
                    工作量统计-按部门班组</td>
            </tr>
 <tr>
        <td align="right" style="width: 726px">
           <table width="100%">
                <tr>
                    <td style="width: 68px; height: 21px">
                        开始日期</td>
                    <td style="width: 103px; height: 21px; text-align: center;">
                        <cc1:WebDate ID="wdlStart" runat="server" DateTimeStyle="Date" />
                    </td>
                    <td style="width: 62px; height: 21px" align="center">
                        结束日期</td>
                    <td align="center" style="width: 93px; height: 21px">
                        <cc1:WebDate ID="wdlEnd" runat="server" DateTimeStyle="Date" />
                    </td>
                    <td style="width: 35px; height: 21px" align="center">
                        部门</td>
                    <td style="width: 72px; height: 21px" align="center">
                        <asp:DropDownList ID="ddlDepart" runat="server" Width="89px"></asp:DropDownList></td>
                    <td style="width: 52px; height: 21px" align="center">
            <asp:Button ID="btnAdd" Text="统计" runat="server" OnClick="btnAdd_Click" Enabled="False" /></td>
                    <td style="width: 46px; height: 21px" align="center">
            <asp:Button ID="btnSaveExcel" Text="导出" runat="server" OnClick="btnSaveExcel_Click" Enabled="False" ToolTip="把统计结果导出Excel文件。" /></td>
                    <td align="center" style="width: 51px; height: 21px">
                        <asp:Button ID="btnQuery" Text="检索" runat="server" Visible="False" /></td>
                    <td align="center" style="height: 21px">
                    </td>
                </tr>
            </table>
            
        </td>
        </tr>
        <tr>
        <td align="center" style="height: 300px" valign="top" >
            <asp:GridView ID="grvList" runat="server" Font-Size="10pt" AllowPaging="True" AllowSorting="True" PageSize="20">
                <Columns>
                    <asp:BoundField DataField="depart" HeaderText="部门" />
                    <asp:BoundField DataField="member" HeaderText="姓名" />
                    <asp:BoundField DataField="F_PACKNAME" HeaderText="业务类型" >
                    </asp:BoundField>
                    <asp:BoundField DataField="COUNTS" HeaderText="处理任务次数" >
                    </asp:BoundField>
                    <asp:BoundField DataField="totalHours" HeaderText="消耗小时数" DataFormatString="{0:f2}" >
                    </asp:BoundField>
                </Columns>
                <PagerSettings Visible="False" />
                <FooterStyle BackColor="Silver" />
            </asp:GridView>
 
        </td>
        </tr>
        <tr >
            <td class="bottomtd" style="height: 25px" >
                <table width="100%">
                    <tr>
                        <td id="tdMessage" runat="server" style="width: 645px; height: 23px">
                                 </td>
                        <td style="width: 100px; height: 23px" align="center">
                            <asp:Button id="btnFirst" Text="首页" runat="server" CommandName="first" OnClick="NavigateToPage" /></td>
                        <td align="center" style="width: 100px; height: 23px">
                            <asp:Button id="btnPrevious" Text="上一页" runat="server" CommandName="prev" OnClick="NavigateToPage"/></td>
                        <td align="center" style="width: 100px; height: 23px">
                            <asp:Button ID="btnNext" Text="下一页" runat="server" CommandName="next" OnClick="NavigateToPage" /></td>
                        <td align="center" style="width: 71px; height: 23px">
                            <asp:Button ID="btnLast" Text="末页" runat="server" CommandName="last" OnClick="NavigateToPage" /></td>
                        <td style="width: 100px; height: 23px" align="right">
                                    转向</td>
                        <td style="width: 100px; height: 23px" align="center">
                                <asp:TextBox ID="txtPage" runat="server" Width="40px" />
                                </td>
                        <td style="width: 100px; height: 23px" align="left">
                        </td>
                         </tr>
                 </table>
            </td>
        </tr>
        </table> 
        <input id="refreshPage" runat="server" onpropertychange="javascript:form1.submit();"
                       size="0" style="visibility: hidden; width: 68px" type="text" value="0" />
    </div>
    </form>
</body>
</html>

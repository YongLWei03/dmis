<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCycleTaskPara_Det.aspx.cs" Inherits="SYS_WorkFlow_frmCycleTaskPara_Det" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
		<div>
			<table align="center" class="detailtable">
				<tr>
					<td class="captiontd" colspan="4">
                        周期任务参数表
					</td>
				</tr>
				<tr>
					<td class="detailtd">
                        任务名称*</td>

					<td class="controltd" style="text-align: left" colspan="3">
                        &nbsp;<asp:TextBox ID="txt任务名称" runat="server" Height="35px" TextMode="MultiLine"
                            Width="509px"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="detailtd" style="height: 32px">
                        业务类型</td>

					<td class="controltd" style="height: 32px">
                        &nbsp;<asp:DropDownList ID="ddl对应业务ID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl对应业务ID_SelectedIndexChanged"
                            Width="160px">
                        </asp:DropDownList></td>
					<td class="detailtd" style="height: 32px">
                        任务启动人*&nbsp;</td>

					<td class="controltd" style="height: 32px">
                        &nbsp;<asp:DropDownList ID="ddl任务启动人" runat="server" Width="161px">
                        </asp:DropDownList></td>
				</tr>
				<tr>
					<td class="detailtd" style="height: 31px">
                        周期类型</td>

					<td class="controltd" style="height: 31px">
                        &nbsp;<asp:DropDownList ID="ddl周期类型" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl周期类型_SelectedIndexChanged"
                            Width="160px">
                            <asp:ListItem>按年</asp:ListItem>
                            <asp:ListItem>按季</asp:ListItem>
                            <asp:ListItem>按月</asp:ListItem>
                            <asp:ListItem>按周</asp:ListItem>
                        </asp:DropDownList></td>
					<td class="detailtd" style="height: 31px">
                        发生时候</td>

					<td class="controltd" style="height: 31px">
                        &nbsp;<asp:TextBox ID="txt发生时候" runat="server" ReadOnly="True" ToolTip="只读，根据选择的结果自动生成！"
                            Width="152px"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="detailtd">
                        &nbsp;<asp:Label ID="lbl1" runat="server" Text="月份" Width="38px"></asp:Label></td>

					<td class="controltd">
                        &nbsp;<asp:DropDownList ID="ddl1" runat="server" Width="160px">
                        </asp:DropDownList></td>
					<td class="detailtd">
                        <asp:Label ID="lbl2" runat="server" Text="日期" Width="38px"></asp:Label>
					</td>

					<td class="controltd">
                        &nbsp;<asp:DropDownList ID="ddl2" runat="server" Width="160px">
                        </asp:DropDownList></td>
				</tr>
				<tr>
					<td class="detailtd">
					</td>

					<td class="controltd">
					</td>
					<td class="detailtd">
					</td>

					<td class="controltd">
					</td>

				</tr>
				<tr>
					<td align="right" class="bottomtd" colspan="4" style="height: 25px">
                        <asp:TextBox ID="txt月份数字" runat="server" EnableTheming="False"
                            Visible="False" Width="32px"></asp:TextBox>
                        <asp:TextBox ID="txt日期数字" runat="server" EnableTheming="False"
                            Visible="False" Width="32px"></asp:TextBox>
                        <asp:TextBox ID="txtTID" runat="server" EnableTheming="False"
                            Visible="False" Width="32px"></asp:TextBox>
									<asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" Enabled="False" Width="48px" />&nbsp;
                        &nbsp;<asp:Button ID="btnReturn" runat="server" Text="返回" OnClick="btnReturn_Click" Width="48px" />&nbsp;
					</td>
				</tr>
				<tr>
					<td id="tdMessage" runat="server" align="left" class="bottomtd" colspan="4" style="font-size: 12px;color: red; height: 21px"></td>
				</tr>
			</table>
		</div>
    </form>
</body>
</html>

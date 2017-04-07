<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uSelectMonth.ascx.cs" Inherits="uSelectMonth" %>
<div style="width:184px">
    <table>
        <tr>
            <td style="width: 63px; text-align: center; height: 16px;">
                <span style="font-size: 10pt">
                    <asp:Label ID="lblMonth" runat="server" Font-Size="9pt" meta:resourcekey="lblMonthResource1"
                        Text="月份:"></asp:Label></span></td>
            <td style="width: 79px; height: 26px;">
                <asp:TextBox ID="txtMonth" runat="server" Width="58px" EnableTheming="False" meta:resourcekey="txtMonthResource1"></asp:TextBox></td>
            <td style="width: 25px; text-align: center; height: 26px;">
                <asp:ImageButton ID="imbPerMonth" runat="server" ImageUrl="img/prev_date.gif" OnClick="imbPerMonth_Click" meta:resourcekey="imbPerMonthResource1" ToolTip="上一月份" /></td>
            <td style="width: 23px; text-align: center; height: 26px;">
                <asp:ImageButton ID="imbCurMonth" runat="server" ImageUrl="img/month.gif" OnClick="imbCurMonth_Click" meta:resourcekey="imbCurMonthResource1" ToolTip="当前月份" /></td>
            <td style="width: 24px; text-align: center; height: 26px;">
                <asp:ImageButton ID="imbNextMonth" runat="server" ImageUrl="img/next_date.gif" OnClick="imbNextMonth_Click" meta:resourcekey="imbNextMonthResource1" ToolTip="下一月份" /></td>
        </tr>
    </table>
</div>
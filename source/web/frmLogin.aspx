<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLogin.aspx.cs" Inherits="frmlogin" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DF8360调度生产管理系统登录：</title>
    	<script language="javascript">
		<!--
			if(top!=self)
			{
			top.location=location;
			}

			self.moveTo(0,0)
			self.resizeTo(screen.availWidth,screen.availHeight)

			function setFocus()
			{
				if(document.login.username.value=="")
 					document.login.username.focus();
 				else
 					document.login.password.focus();
 				return;
			}
 
			//-->
		</script>
</head>
<body  bgcolor="#0173a9">
    <form id="form1" runat="server">
			<p align="center"><font color="#0173a9">起空行</font>
			</p>
			<p><font color="#0173a9">起空行</font>
			</p>
			<div>
				<center>
					<table border="1" width="48%" height="168">
						<tr>
							<td width="100%" colspan="2" style="HEIGHT: 34px">
								<p align="center"><font color="#ffffff"><span style="font-size: 11pt">
                                    <asp:Label ID="Label3" runat="server" Text="DF8360调度生产管理系统" meta:resourcekey="Label3Resource1"></asp:Label></span></font></p>
							</td>
						</tr>
						<tr>
							<td width="50%" height="42">
								<p align="center"><font color="#ffffff">
                                    <asp:Label ID="Label1" runat="server" Text="用户名：" Font-Size="10pt" meta:resourcekey="Label1Resource1"></asp:Label></font>&nbsp;</p>
							</td>
							<td width="50%" height="42">
								<p align="center"><font color="#ffffff"><input type="text" name="username" size="15" id="txtCode" runat="server"></font></p>
							</td>
						</tr>
						<tr>
							<td width="50%" style="height: 36px">
								<p align="center"><font color="#ffffff">
                                    <asp:Label ID="Label2" runat="server" Text="口令：" Font-Size="10pt" meta:resourcekey="Label2Resource1"></asp:Label></font>&nbsp;</p>
							</td>
							<td width="50%" style="height: 36px">
								<p align="center"><font color="#ffffff"><input type="password" name="password" size="15" id="txtPwd" runat="server"></font></p>
							</td>
						</tr>
						<tr>
							<td width="50%" style="HEIGHT: 32px">
								<p align="center" style="text-align: center">
									<asp:Button ID="btnOk" runat="server" Text="登录" Width="48px" OnClick="btnOk_Click" BackColor="Silver" EnableTheming="True" ForeColor="Black" meta:resourcekey="btnOkResource1" />&nbsp;</p>
							</td>
							<td width="42%" style="HEIGHT: 32px; text-align: center;">
								<asp:Button ID="btnReset" runat="server" Text="重置" Width="48px" OnClick="btnReset_Click" BackColor="Silver" ForeColor="Black" meta:resourcekey="btnResetResource1" />
							</td>
						</tr>
					</table>
					<p></p>
					<asp:Label ID="lblMessage" runat="server" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label>
				</center>
			</div>
        
    </form>
</body>
</html>

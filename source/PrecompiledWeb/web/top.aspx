<%@ page language="C#" autoeventwireup="true" inherits="top, App_Web_nco4kbcn" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body >
    <form id="form1" runat="server" >
			<table id="table1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px; HEIGHT: 111px; background-image: url(img/index.jpg); width: 100%;"
				cellspacing="0" cellpadding="0" border="0">
				<tr>
                    <td style="width: 207px; height: 60px">
                    </td>
                    <td style="width: 64px; height: 60px">
                    </td>
                    <td style="width: 274px; height: 60px">
                    </td>
					<td style="HEIGHT: 60px; width: 280px;">
					</td>
                    <td style="width: 595px; height: 60px">
                    </td>
				</tr>
                <tr>
                    <td style="width: 207px; height: 21px;">
                        </td>
                    <td style="width: 64px; height: 21px;">
                    <img src="img/app_2.gif" alt="调整窗口大小" onclick="cwin();"/>&nbsp;</td>
                    <td style="width: 274px; font-size: 10pt; color: green; height: 21px;" id="tdMember" runat="server">
                    </td>
                    <td style="width: 280px; font-size: 10pt; color: green; height: 21px;" id="tdDate" runat="server">
                    </td>
                    <td runat="server" style="height: 21px; text-align: center">
                        <asp:ImageButton ID="imgZhuxiao" runat="server"  ImageUrl="~/img/btnlogout.jpg" OnClick="imgZhuxiao_Click" OnClientClick='return confirm("确定要注销并重新登录系统?");' />
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="imgExit" runat="server" ImageUrl="~/img/btnQuit.jpg" OnClick="imgExit_Click" OnClientClick='return confirm("确定要退出系统?");' /></td>
                </tr>
			</table>
			
    </form>
    
		<script type="text/javascript">
				function cwin()
				 {  if(parent.mainframset.cols!='196,*')
					{parent.mainframset.cols='196,*';}
					//document.all.pic.src ="img/hide_menu.gif";document.all.dir.innerHTML="隐藏快捷栏"}
					else{parent.mainframset.cols='0,*';}
					//document.all.pic.src ="img/show_menu.gif";document.all.dir.innerHTML="显示快捷栏"}}
					//function MM_goToURL() { //v3.0
					//var i, args=MM_goToURL.arguments; document.MM_returnValue = false;
					//for (i=0; i<(args.length-1); i+=2) eval(args[i]+".location='"+args[i+1]+"'");
				 }
		</script>		
</body>
</html>

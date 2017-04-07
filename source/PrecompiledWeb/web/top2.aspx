<%@ page language="C#" autoeventwireup="true" inherits="top2, App_Web_nco4kbcn" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" stylesheettheme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>标题面</title>
</head>
<body style="margin: 0px; position: absolute;">
    <form id="form1" runat="server">
    <div >
        <div style="background-image: url(img/bg_top.jpg); width: 100%; height: 75px; vertical-align: middle; ">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 68px;">
            <tr></tr>
                <tr>
                    <td style="width: 311px;" rowspan="3" >
                        </td>
                    <td style="height: 9px;" >
                    </td>
                    <td style="width: 10px; height: 9px" >
                    </td>
                    <td style="width: 22px; height: 9px" >
                    </td>
                    <td style="width: 31px; height: 9px" >
                        <br />
                        <asp:LinkButton ID="lbnEnglish" runat="server" Font-Bold="True" ForeColor="#400000"
                            Width="29px" OnClick="lbnEnglish_Click" meta:resourcekey="lbnEnglishResource1" Text="Spanish"></asp:LinkButton></td>
                    <td style="height: 9px; width: 24px;" >
                    </td>
                    <td style="width: 34px; height: 9px" >
                        <br />
                        <asp:LinkButton ID="lbnChina" runat="server" Font-Bold="True" ForeColor="#400000"
                            Width="29px" OnClick="lbnChina_Click" meta:resourcekey="lbnChinaResource1">中文</asp:LinkButton></td>
                    <td style="width: 32px; height: 9px">
                    </td>
                    <td style="height: 9px" >
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" >
                        <asp:Label ID="lblProductInfo" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="Black"
                            Text="调度生产管理系统" meta:resourcekey="lblProductInfoResource1" Font-Names="Arial"></asp:Label></td>
                    <td style="width: 10px" >
                    </td>
                    <td style="width: 22px" >
                    </td>
                    <td style="width: 31px" >
                    </td>
                    <td style="width: 24px" >
                    </td>
                    <td style="width: 34px" >
                    </td>
                    <td style="width: 32px">
                    </td>
                    <td >
                    </td>
                </tr>
                <tr>
                    <td style="height: 9px; text-align: right;" valign="middle" >
                        <img src="img/man.gif" onclick="cwin();" />&nbsp;
                        <asp:Label ID="lblMan" runat="server" Text="张三  28-08-2009 " meta:resourcekey="lblManResource1"></asp:Label></td>
                    <td style="width: 10px; height: 9px" >
                        &nbsp;</td>
                    <td style="width: 22px; height: 9px; text-align: center" >
                        <img src="img/login.gif" /></td>
                    <td style="width: 31px; height: 9px" >
                        <asp:LinkButton ID="lbnRelogin" runat="server" Font-Bold="True" ForeColor="#400000"
                            Width="29px" OnClick="lbnRelogin_Click" meta:resourcekey="lbnReloginResource1">注销</asp:LinkButton></td>
                    <td style="height: 9px; width: 24px; text-align: center;" >
                        <img src="img/exit.gif" /></td>
                    <td style="width: 34px; height: 9px" >
                        <asp:LinkButton ID="lbnExit" runat="server" Font-Bold="True" ForeColor="#400000"
                            Width="29px" OnClick="lbnExit_Click" meta:resourcekey="lbnExitResource1">退出</asp:LinkButton></td>
                    <td style="width: 32px; height: 9px">
                    </td>
                    <td style="height: 9px" >
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
	<script type="text/javascript">
		function cwin()
		 {  if(parent.mainframset.cols!='0,*')
			{parent.mainframset.cols='0,*';}
			//document.all.pic.src ="img/hide_menu.gif";document.all.dir.innerHTML="隐藏快捷栏"}
			else{parent.mainframset.cols='200,*';}
			//document.all.pic.src ="img/show_menu.gif";document.all.dir.innerHTML="显示快捷栏"}}
			//function MM_goToURL() { //v3.0
			//var i, args=MM_goToURL.arguments; document.MM_returnValue = false;
			//for (i=0; i<(args.length-1); i+=2) eval(args[i]+".location='"+args[i+1]+"'");
		 }
    </script>	

</body>
</html>

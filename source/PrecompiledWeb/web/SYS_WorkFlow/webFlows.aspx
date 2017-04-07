<%@ page language="C#" autoeventwireup="true" inherits="webFlows, App_Web_iakvuhia" stylesheettheme="default" %>
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="URN:SCHEMAS-MICROSOFT-COM:VML">
<head runat="server">
    <title>流程显示器</title>
    <style type="text/css"> v\:* { BEHAVIOR: url(#default#VML) }</style>
		<script language="javascript" type="text/javascript" src="Flow.js"></script>
		<script language="javascript"  type="text/javascript">
		function drawFlow()
	    {
		    var flow1=new FlowNode();
		    ParentObj=document.all["group1"];
		    var str=document.all["NodeData"].value;
		    var nodeArr=str.split("|");
		    if(nodeArr.length>0)
			    for(var i=0;i<nodeArr.length;i++){
				    var sArr1=nodeArr[i].split(":");
				    if(sArr1.length==7)
					    flow1.addNode(sArr1[0],sArr1[1],sArr1[2],sArr1[3],sArr1[4],sArr1[5],sArr1[6]);
			    }		
		    //refreshNode();
		    show();
	    }
		</script>
</head>
	<body onload="drawFlow();">
		<v:group id="group1" style="WIDTH:800pt;HEIGHT:600pt" coordsize="800,600" />
		<form id="frmFlow" runat="server">
			<textarea rows="0" cols="0" id="NodeData" runat="server" name="NodeData" style="VISIBILITY: hidden; WIDTH: 1px; HEIGHT: 1px"> </textarea>
		</form>
	</body>
</html>

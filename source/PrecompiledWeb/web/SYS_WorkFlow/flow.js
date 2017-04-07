var ParentObj;
var fontsize="9pt";
var fontcolor="red";
var fontwid=-1;
var fonthei=-1;
var fontalign="center";

var cFinished="rgb(255,192,192)";
var cNoFinish="rgb(192,192,255)";
var cFinishing="rgb(192,234,255)";
var cNoDeal="white";

function FlowNode(){
	this.Nodes=new Array();
	this.addNode=addNode;
}
function addNode(id,name,ileft,itop,title,nodetype,parentid){
	//nodetype:0(开始),1(完成),2(在办),3(未办),4(结束)
	if(FlowNode.Nodes==null)
		FlowNode.Nodes=new Array();
	FlowNode.Nodes[FlowNode.Nodes.length]=new theitem(id,name,ileft,itop,title,nodetype,parentid);
	return(FlowNode.Nodes.length-1);
}

function theitem(id,name,ileft,itop,title,nodetype,parentid)
{
	this.id=id;
	this.name=name;
	this.nodetype=nodetype;
	this.title=title;
	this.parentid=parentid;
	this.left=ileft;
	this.top=itop;
	this.width=60;
	this.height=30;
}

function refreshNode(){
	var id1=-1;
	var boxwidth,boxheight;
	boxwidth=1000;
	boxheight=0;
	if(FlowNode.Nodes==null) return;
	if(FlowNode.Nodes.length>0){
		for(var i=0;i<FlowNode.Nodes.length;i++){
			if(id1!=FlowNode.Nodes[i].parentid){
				boxheight=boxheight+100;
				boxwidth=100;
				id1=FlowNode.Nodes[i].parentid;
			}//end if
			else
				boxwidth+=100;
			FlowNode.Nodes[i].left=boxwidth;
			FlowNode.Nodes[i].top=boxheight;
		}//end for
	}//end if
}
function show()
{
	var i,j;
	var id1;
	var tmp,tmp11;
	var frmx,frmy,tox,toy;
	var m_NodeFont;
	if(FlowNode.Nodes==null) return;
	tmp11="fillcolor=rgb(255,255,255)";
	if(FlowNode.Nodes.length>0){
		for(i=0;i<FlowNode.Nodes.length;i++){
			tmp="<v:rect id='Node_" +FlowNode.Nodes[i].id+"' style='width:"+FlowNode.Nodes[i].width
					+";height:"+FlowNode.Nodes[i].height+";left:"+FlowNode.Nodes[i].left
					+";top:"+FlowNode.Nodes[i].top	+"' ";
			switch(FlowNode.Nodes[i].nodetype){
			case "完成": 
				tmp11="fillcolor=" +cFinished;
				break;
			case "待办":
				tmp11="fillcolor=" +cNoFinish;
				break;
			case "在办":
				tmp11="fillcolor=" +cFinishing;
				break;
			default:
				tmp11="fillcolor=" +cNoDeal;
				break;
			}//end switch

			tmp+="strokeweight=1 "+ tmp11+" title='"+FlowNode.Nodes[i].title+"'/>"
			var tmp2=document.createElement(tmp);
			var vCaption = document.createElement("v:textbox");
			vCaption.style.fontSize =fontsize; 
			vCaption.style.color = fontcolor;
			vCaption.style.height =FlowNode.Nodes[i].height;
			vCaption.style.width =FlowNode.Nodes[i].width;
			vCaption.innerText = FlowNode.Nodes[i].name;
			vCaption.style.textAlign = fontalign;
			tmp2.appendChild(vCaption);//.insertBefore(vCaption,null);
			ParentObj.appendChild(tmp2);//.insertBefore(tmp2,null);
		}//end for
	}
	var iitop1,iitop2;
	if(FlowNode.Nodes.length>1)
		for(i=0;i<FlowNode.Nodes.length;i++){
			var sId=FlowNode.Nodes[i].parentid.split("_");
			for(var j=0;j<sId.length;j++){
				var node2=findnode(parseInt(sId[j]));
				if(node2!=null){
					iitop1=parseInt(FlowNode.Nodes[i].top)+parseInt(FlowNode.Nodes[i].height);
					iitop2=Math.abs(parseInt(FlowNode.Nodes[i].top)-parseInt(node2.top));
					if(iitop1<parseInt(node2.top)){
							frmx=parseInt(FlowNode.Nodes[i].left)+parseInt(FlowNode.Nodes[i].width)/2;
							frmy=parseInt(FlowNode.Nodes[i].top)+parseInt(FlowNode.Nodes[i].height);
							tox=parseInt(node2.left)+parseInt(node2.width)/2;
							toy=parseInt(node2.top);
						}
					else{
						if(iitop2<parseInt(node2.height)){
							if(parseInt(FlowNode.Nodes[i].left)<parseInt(node2.left)){
								frmx=parseInt(FlowNode.Nodes[i].left)+parseInt(FlowNode.Nodes[i].width);
								frmy=parseInt(FlowNode.Nodes[i].top)+parseInt(FlowNode.Nodes[i].height)/2;
								tox=parseInt(node2.left);
								toy=parseInt(node2.top)+parseInt(node2.height)/2;
								}
							else{
								frmx=parseInt(FlowNode.Nodes[i].left);
								frmy=parseInt(FlowNode.Nodes[i].top)+parseInt(FlowNode.Nodes[i].height)/2;
								tox=parseInt(node2.left)+parseInt(FlowNode.Nodes[i].width);
								toy=parseInt(node2.top)+parseInt(node2.height)/2;								
								}						
						  }
						  else{
							frmx=parseInt(FlowNode.Nodes[i].left)+parseInt(FlowNode.Nodes[i].width)/2;
							frmy=parseInt(FlowNode.Nodes[i].top);
							tox=parseInt(node2.left)+parseInt(node2.width)/2;
							toy=parseInt(node2.top)+parseInt(node2.height);		
							}
					
						}
					tmp="<v:line from="+ frmx+","+frmy+" to="+tox+","+toy+" strokeweight=1pt/>"
					var tmp2=document.createElement(tmp);
					var tmp3=document.createElement("<v:stroke StartArrow='classic'/>");
					tmp2.appendChild(tmp3);//tmp2.insertBefore(tmp3,null);
					ParentObj.appendChild(tmp2);//ParentObj.insertBefore(tmp2,null);				
				}	
			}
	}//end if
}

function findnode(nodeid){
	var node1=null;
	for(i=0;i<FlowNode.Nodes.length-1;i++)
		if(FlowNode.Nodes[i].id==nodeid){
			node1=FlowNode.Nodes[i];
			break;
		}
	return(node1);	
}
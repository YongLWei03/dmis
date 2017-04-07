<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCellReportDisplay.aspx.cs" Inherits="frmCellReportDisplay" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>报表显示</title>
       <script language="javascript">
        var cllFile;
        function printSet_onclick()
        {
            //form1.m_cell.SetCurSheet(form1.m_cell.GetTotalSheets());
            form1.m_cell.PrintPageSetup();
        }
        
        function preview_onclick()
        {
            form1.m_cell.SaveEdit();
            form1.m_cell.PrintPreview(0,form1.m_cell.GetCurSheet());
        }
        
        function print_onclick()
        {
           form1.m_cell.SaveEdit();
           for(var i=0;i<form1.m_cell.GetTotalSheets();i++)
           {
                form1.m_cell.PrintSheet(0,i);
           }
        }
        
        function export_onclick()
        {
            form1.m_cell.ExportExcelDlg();
        }
                        
        function SetSingleTableData()
        {
            var arr2=new Array();
            var arrCllInfo=new Array();
            var str;
            str=form1.hdnValue.value;
            //alert(str);
            if(str.length>0)
            {
                arrCllInfo=str.split("◆");
                //alert(arrCllInfo.length);
                for(var i=0;i< arrCllInfo.length; i++)
                {
                    if(arrCllInfo[i].indexOf("^",0)>0)
                    {   //alert(arrCllInfo[i]);
                        arr2=arrCllInfo[i].split("^");
                        
                        if(arr2[4]=="Numeric")
                        {
                            if(arr2[3]!="")
                            {
                                if(arr2[5]=="Percentage")
                                    form1.m_cell.SetCellNumType(arr2[1],arr2[0],arr2[2],5);  //百分比显示
                                else
                                    form1.m_cell.SetCellNumType(arr2[1],arr2[0],arr2[2],1);  //普通数值
                                form1.m_cell.D(arr2[1],arr2[0],arr2[2],arr2[3]);
                            }
                        }
                        else  //字符串
                        {
                            form1.m_cell.SetCurSheet(arr2[2]);  //2008-3-27后加上的，有多页时，后面的页显示不出来，加上此语句就可以了。
                            form1.m_cell.SetCellNumType(arr2[1],arr2[0],arr2[2],7);
                            form1.m_cell.SetCellString(arr2[1],arr2[0],arr2[2],arr2[3]);
                            
                            
                            //只有字符串的列类型才设置自适应行高和列宽
                            var height,width
                            if(arr2[5]=="RowBestHeight")
                            {
                                height=form1.m_cell.GetRowBestHeight(arr2[0]);
                                form1.m_cell.SetRowHeight(1,height,arr2[0],arr2[2]);
                            }
                            else if(arr2[5]=="ColBestWidth")
                            {
                                width=form1.m_cell.GetColBestWidth(arr2[1]);
                                form1.m_cell.SetColWidth(1,width,arr2[1],arr2[2]);
                            }
                            else
                            {
                            }
                            
                        }
                    }
                }
                

            }
        }
        
        function OpenFile()
        {
            var str=form1.hdnReportPath.value+cllFile;
            //alert(str);
            var iopenfile=form1.m_cell.OpenFile(str,"");
            if(iopenfile>0)
            {
                form1.m_cell.AllowDragdrop = false;
                form1.m_cell.AllowExtend = false;
            }
            else
            {
                switch(iopenfile)
                {
                    case -1:
                        //alert("文件不存在！");
                        break;
                   case -2:
                        //alert("文件操作错误！");
                        break;
                    case -3:
                       //alert("文件格式错误！");
                       break;                        
                   case -4:
                        //alert("密码错误！");
                        break;
                   case -5:
                        //alert("不能打开高版本文件！");
                        break;
                   case -6:
                        //alert("不能打开特定版本文件！");
                        break;
                   default:
                        //alert("文件打开失败！");
                        break;                                                                                                
                }
                alert("Cell File Error!");
            }
        }
        /*****
        function AddPages()
        {
            form1.m_cell.setcursheet(0);
            var pageNo=form1.hdnPageNo.value;
            if(pageNo=="0") return;
            var perPageRows=form1.m_cell.GetRows(0)-1;  //总行数
            //alert("总行数"+perPageRows);
            var Cols=form1.m_cell.GetCols(0)-1;
            //alert("总列数"+Cols);
            while(pageNo>0)
            {
                //form1.m_cell.insertrow(form1.m_cell.GetRows(0),perPageRows,0);
                
                form1.m_cell.insertrow(form1.m_cell.GetRows(0),1,0);  //只插入一行
                alert(form1.m_cell.GetRows(0));
                form1.m_cell.CopyRange(1,1,Cols,perPageRows);  //把第一页的格式拷贝一下
                alert(form1.m_cell.GetRows(0));
                form1.m_cell.SelectRange(1,form1.m_cell.GetRows(0)-1,Cols,form1.m_cell.GetRows(0)-1);
                form1.m_cell.Paste(1,form1.m_cell.GetRows(0)-1,0,1,1);
                alert(form1.m_cell.GetRows(0));
                pageNo=pageNo-1;
            }
        }
        */
        
        //采用加sheet的方式来达到换页的效果,
        //form1.hdnPageNo.value的格式：源页号^增加的页数◆源页号^增加的页数.....如果增加的页数为0,则不增加
        function AddSheets()
        {
            var pageNo=form1.hdnPageNo.value;
            if(pageNo=="" || pageNo==null || pageNo==undefined) return;
 
            var arrPageAndPages=new Array();
            var arr3=new Array();
            arrPageAndPages=pageNo.split("◆");
            for(var i=0;i< arrPageAndPages.length; i++)
            {
          
                if(arrPageAndPages[i]=="" || arrPageAndPages[i]==null || arrPageAndPages[i]==undefined) return;
                arr3=arrPageAndPages[i].split("^");
               
                if(arr3[1]=="0") continue;
                var cols=form1.m_cell.getcols(arr3[0]);
                var rows=form1.m_cell.getrows(arr3[0]);
                for(var j=1;j<=arr3[1];j++)
                {
                     form1.m_cell.InsertSheet(form1.m_cell.GetTotalSheets(),1);
                     form1.m_cell.DeleteCol(cols+1,form1.m_cell.getcols(j)-cols-1,arr3[0]+j);    //被粘贴的区域比第一页大一些,这样不会弹出提示窗口.
                     form1.m_cell.DeleteRow(rows+1,form1.m_cell.getrows(j)-rows-1,arr3[0]+j);
                     form1.m_cell.setcursheet(arr3[0]);
                     form1.m_cell.CopyRange(1,1,cols,rows);  //把第一页的格式拷贝一下
                     form1.m_cell.setcursheet(j);
                     form1.m_cell.SelectRange(1,1,form1.m_cell.getcols(j),form1.m_cell.getrows(j));
                     form1.m_cell.Paste(1,1,0,1,1);
                     form1.m_cell.ClearSelection();
                }
            }
        }
        
        function PageLoad()
        {
            if(form1.m_cell.Login("宁夏电力公司","","1304020291","0060-1665-0123-5004")==0)
            {
                alert("Cell注册失败！");
                return;
            }
            cllFile=form1.hdnCellFileName.value;
            OpenFile();
            AddSheets();
            SetSingleTableData();
            form1.m_cell.CalculateAll();
        }
        

       </script>
</head>
<body  onload="PageLoad()" style="text-align: center; background-image: url(img/Nature Bkgrd.jpg);">
    <form id="form1" runat="server">
         <input id="hdnValue" type="hidden" runat="server" style="width: 22px"  /><input id="hdnCellFileName" type="hidden" runat="server" style="width: 20px"  /><input id="hdnPageNo" type="hidden" runat="server" style="width: 20px"  />
        <input id="hdnReportPath" type="hidden" runat="server" style="width: 20px"  />&nbsp;
        <input id="hfHostID" type="hidden" runat="server" style="width: 20px"  />&nbsp;
        <table border="0" cellpadding="0" cellspacing="0"  style=" width:83%; height: 100%">
            <tr style=" height:20%; "  >
                <td style="width: 30%; height: 19%; font-weight: bold; font-size: 15pt; color: purple; font-family: 楷体_GB2312;" id="tdReportName" runat="server" >
                </td>
                <td style="width: 60%; height: 19%;" id="tdControl" runat="server" >
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
                <td style="width: 30%; height: 19%;" align="center" valign="middle" >
                    <div style="text-align: center">
                        <table border="0" cellpadding="0" cellspacing="0" style="height: 78px" id="TABLE1" >
                            <tr>
                                <td colspan="2" style="height: 35px">
                                    <asp:Button ID="btnDisplay" runat="server" Text="显示" OnClick="btnDisplay_Click" Width="77px" meta:resourcekey="btnDisplayResource1" /></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 35px;">
                                    <asp:Button ID="btnPrint" runat="server" OnClientClick="print_onclick()" Text="打印" Width="77px" meta:resourcekey="btnPrintResource1" /></td>
                                <td style="width: 100px; height: 35px;">
                                    <asp:Button ID="btnExcel" runat="server" OnClientClick="export_onclick()" Text="Excel" Width="77px" meta:resourcekey="btnExcelResource1" />
	            </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 33px;">
                                    <asp:Button ID="btnPrintSet" runat="server" OnClientClick="printSet_onclick()" Text="打印设置" Width="77px" meta:resourcekey="btnPrintSetResource1" /></td>
                                <td style="width: 100px; height: 33px;">
                                    <asp:Button ID="btnPreview" runat="server" OnClientClick="preview_onclick()" Text="打印预览" Width="77px" meta:resourcekey="btnPreviewResource1" /></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style=" height:80%" >
                <td colspan="3" valign="top" >
                       <object id="m_cell" style="Z-INDEX: 11; LEFT: 7px; WIDTH: 798px; HEIGHT: 600px"
			            codebase="cellweb5.cab#version=5,2,5,205" classid="clsid:3F166327-8030-4881-8BD2-EA25350E574A"
			            name="m_cell" >
		            </object>

                </td>
            </tr>
        </table>
        &nbsp; &nbsp;
    </form>
</body>
</html>

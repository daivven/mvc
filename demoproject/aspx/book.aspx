<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="book.aspx.cs" Inherits="demoproject.aspx.book" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>  
    <link href="../css/main.css" rel="stylesheet" type="text/css" />
    <link href="../js/jquery-easyui-1.2.6/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../js/jquery-easyui-1.2.6/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-easyui-1.2.6/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery-easyui-1.2.6/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/jquery-easyui-1.2.6/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>   
    
    <script src="../scripts/jQuery.Ajax.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/book.js?v=16"></script>  
</head>
<body>  
    <div  class="easyui-panel" title="查询条件" style="width:850px;height:160px" collapsible="true">
    <table style ="width:100%">
    <tr>
    <td style="width:800px;">
    <div class="searchitem"  >
    <label>出版社名称：</label>
      <select id="textBookPress" name="textBookPress" style="width:200px"  >
     </select>
     <script type="text/javascript">

         $("#textBookPress").combobox({
             url: "../ashx/presshandle.ashx?action=list&n=" + Math.random(),
             valueField: "ID",
             textField: "PressName",
             panelHeight: "auto"
         });
     </script>
    </div>   

    <div class="searchitem">
    <label>价格区间：</label>
    <input type="text" id="priceLow" class="easyui-numberbox" style="width:89px"/>
    <label>-</label>
    <input type="text" id="priceHigh" class="easyui-numberbox" style="width:89px" />
    </div>
    </td>
    
    </tr>
      <tr>
      <td>
    <div class="searchitem">
    <label>&nbsp;&nbsp;SN编码：</label>
    <input type="text" id="sn" class="easyui-validatebox"  />
    </div>
     <div class="searchitem" >
    <label>书籍名称：</label>
    <input type="text" id="bookName" class="easyui-validatebox" />
    </div>
    </td>
    <td></td>
      </tr>
        <tr>
        <td>
    
    <div class="searchitem"  style="width:750px;text-align:right">
    <a href="#" class="easyui-linkbutton" onclick="tsearch()" >查询</a>
    </div>    
    </td>
        </tr>
    </table>

    <%--<div class="searchitem"  style="width:320px;">
    <label>出版社名称：</label>
      <select id="textBookPress" name="textBookPress"  style="width:140px;" >
     </select>
     <script type="text/javascript">

         $("#textBookPress").combobox({
             url: "../ashx/presshandle.ashx?action=list&n=" + Math.random(),
             valueField: "ID",
             textField: "PressName",
             panelHeight: "auto"
         });
     </script>
    </div>   

    <div class="searchitem">
    <label>价格区间：</label>
    <input type="text" id="priceLow" class="easyui-validatebox" style="width:59px"/>
    <label>-</label>
    <input type="text" id="priceHigh" class="easyui-validatebox" style="width:59px" />
    </div>
  
    <div class="searchitem" style="width:220px;">
    <label>SN编码：</label>
    <input type="text" id="sn" class="easyui-validatebox"  style="width:120px;"/>
    </div>
     <div class="searchitem" style="width:220px;" >
    <label>书籍名称：</label>
    <input type="text" id="bookName" class="easyui-validatebox" />
    </div>
    <div class="searchitem"  style="width:800px;text-align:right">
    <a href="#" class="easyui-linkbutton" onclick="tsearch()" >查询</a>
    </div>--%>

    </div>

    <div id="toolbar" style="padding:5px;height:auto">					
                    <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="addBookInfo()">添加书籍</a>
                    <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="editBookInfo()">编辑书籍</a>
                    <a href="#" class="easyui-linkbutton" iconCls="icon-remove" plain="true" onclick="deleteBookInfo()">删除书籍</a>
    </div>

    <div id="addBookInfo" class="easyui-dialog" closed="true" buttons="#addDepartmentInfo-buttons" style="padding:10px 20px">
        	</div>
			<div id="addDepartmentInfo-buttons">
				<a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="saveInfo()">保存</a>
				<a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#addBookInfo').dialog('close')">关闭</a>
			</div>

            <div id="editBookInfo" class="easyui-dialog" closed="true" buttons="#editDepartmentInfo-buttons" style="padding:10px 20px">
        	</div>
            <div id="editDepartmentInfo-buttons">
				<a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="saveInfo()">保存</a>
				<a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#editBookInfo').dialog('close')">关闭</a>
			</div>

     <table id="grid"></table>
    
   
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="press.aspx.cs" Inherits="demoproject.aspx.press" %>

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
    <script type="text/javascript" src="../js/press.js?v=16"></script>
   
</head>
<body>  
    <div  class="easyui-panel" title="查询条件" style="width:850px;height:90px" collapsible="true">

    <div class="searchitem" style="width:280px">
    <label>出版社名称：</label>
    <input type="text" id="press" class="easyui-validatebox"  style="width:180px"/>
    </div>   
   
     <div class="searchitem" style="width:250px">
    <label>电话：</label>
    <input type="text" id="phone" class="easyui-validatebox" style="width:180px"/>
    </div>
<div class="searchitem" style="width:100px" >
<a href="#" class="easyui-linkbutton" onclick="tsearch()" >查询</a>
</div>

    </div>

    <div id="toolbar" style="padding:5px;height:auto">					
                    <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="addPressInfo()">添加出版社</a>
                    <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="editPressInfo()">编辑出版社</a>
                    <a href="#" class="easyui-linkbutton" iconCls="icon-remove" plain="true" onclick="deletePressInfo()">删除出版社</a>
    </div>

    <div id="addPressInfo" class="easyui-dialog" closed="true" buttons="#addDepartmentInfo-buttons" style="padding:10px 20px">
        	</div>
			<div id="addDepartmentInfo-buttons">
				<a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="saveInfo()">保存</a>
				<a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#addPressInfo').dialog('close')">关闭</a>
			</div>

            <div id="editPressInfo" class="easyui-dialog" closed="true" buttons="#editDepartmentInfo-buttons" style="padding:10px 20px">
        	</div>
            <div id="editDepartmentInfo-buttons">
				<a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="saveInfo()">保存</a>
				<a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#editPressInfo').dialog('close')">关闭</a>
			</div>

     <table id="grid"></table>
    
   
</body>
</html>

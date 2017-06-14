<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addpress.aspx.cs" Inherits="demoproject.aspx.addpress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="add"  method="post">
    <div class="ftitle">添加出版社信息</div>
       <div class="fitem">
    <label>出版社名称：</label>
    <input type="text" name="textPressName" id="textPressName" class="easyui-validatebox" required="true"/><span class="error" id="error"></span>
    </div>

       <div class="fitem">
      <label>出版社地址：</label>
    <input type="text" name="textPressAddress" id="textPressAddress" class="easyui-validatebox" required="true" style="width:300px;"/>   
     
     </div>

       <div class="fitem">
      <label>出版社电话：</label>
    <input type="text" name="textPressPhone" id="textPressPhone" class="easyui-validatebox" required="true"/>   
     
     </div>  


    </form>
</body>
</html>

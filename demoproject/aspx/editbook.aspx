<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editbook.aspx.cs" Inherits="demoproject.aspx.editbook" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="edit"  method="post">
    <div class="ftitle">修改书籍信息</div>
       <div class="fitem">
    <label>书籍名称：</label>
    <input type="text" name="textBookName" id="textBookName" class="easyui-validatebox" required="true"/><span class="error" id="error"></span>
    </div>

       <div class="fitem">
      <label>书籍SN：</label>
    <input type="text" name="textBookSN" id="textBookSN" class="easyui-validatebox" required="true"/>   
     
     </div>

       <div class="fitem">
      <label>书籍价格：</label>
    <input type="text" name="textBookPrice" id="textBookPrice" class="easyui-validatebox" required="true"/>   
     
     </div>

  <div class="fitem">
     <label>书籍出版社：</label>
     <select id="etextBookPress" name="etextBookPress" required="true" style="width:150px;">
     </select>
     <script type="text/javascript">

         $("#etextBookPress").combobox({
             url: "../ashx/presshandle.ashx?action=list&n=" + Math.random(),
             valueField: "ID",
             textField: "PressName",
             panelHeight: "auto"
         });

     </script>
     </div>


    </form>
</body>
</html>

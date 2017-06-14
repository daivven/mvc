var grid;
var formUrl = "addbook.aspx?n=" + Math.random();
var actionURL = '../ashx/bookhandle.ashx';

var url; //提交数据的路径
var formId; //当天要提交的Form的编号
var dialogId; //对话框的编号

$(function () {
    autoResize({ dataGrid: '#grid', gridType: 'datagrid', callback: grid.databind, height: 5 }); 

    $("#editBookInfo").dialog({
            "title": "编辑书籍信息",
            width: 500,
            height: 450,
            href: 'editbook.aspx'
        });

        $('#editBookInfo').dialog('open').dialog('close');    
});

var grid = {
    databind: function (winsize) {
        grid = $('#grid').datagrid({
            toolbar: '#toolbar',
            width: 850,
            height: 500,
            url: actionURL,
            idField: 'id',
            singleSelect: true,
            striped: true,
            pagination: true,
            pageSize: 20,
            pageList: [20, 10, 30, 50],
            frozenColumns: [[               
            ]],
           columns: [[
                { title: 'ID', field: 'ID', width: 60 },
                { title: '书名', field: 'BookName', width: 140 },
                { title: 'SN编码', field: 'BookSNCode', width: 140 },
                { title: '价格', field: 'BookPrice', width: 80 },
                { title: '出版社名称', field: 'PressName', width: 80 },
                {title: '出版社地址', field: 'PressAddress', width: 300 }
            ]]
        });
    },
    reload: function () {       
        $('#grid').datagrid('clearSelections').datagrid('reload');
    },
    selected: function () {
        return $('#grid').datagrid('getSelected');
    }
};

function tsearch() {
//debugger
    var pressName = $("#textBookPress").combobox("getValue");
    var sn = $("#sn").val();
    var bookName = $("#bookName").val();
    var priceLow = $("#priceLow").val();
    var priceHigh = $("#priceHigh").val();
    if (priceLow == "") {
        priceLow = 0;
    }
    if (priceHigh == "") {
        priceHigh = 99999;
    }

    var filterObj;
    if (pressName == "") {

        filterObj = { "groupOp": "AND", "rules": [{ "field": "BookName", "op": "cn", "data": bookName }, { "field": "BookSNCode", "op": "cn", "data": sn }
    , { "field": "BookPrice", "op": "ge", "data": priceLow }, { "field": "BookPrice", "op": "le", "data": priceHigh}]
        };
    }
    else {
        filterObj = { "groupOp": "AND", "rules": [{ "field": "BookName", "op": "cn", "data": bookName }, { "field": "BookSNCode", "op": "cn", "data": sn }, { "field": "pressID", "op": "eq", "data": pressName }
    , { "field": "BookPrice", "op": "ge", "data": priceLow }, { "field": "BookPrice", "op": "le", "data": priceHigh}]
        };
    }
    $('#grid').datagrid('load', { filter: JSON.stringify(filterObj) });
    //$('#grid').datagrid("reload");
}

//添加书籍部分
function addBookInfo() {
    $("#addBookInfo").dialog({
        "title": "新建书籍信息",
        width: 500,
        height: 450,
        href: 'addbook.aspx'
    });
    $('#addBookInfo').dialog('open');
    $('#add').form('clear');

    url = '../ashx/bookhandle.ashx?action=add';
    formId = "#add";
    dialogId = "#addBookInfo";
}

//编辑书籍部分
function editBookInfo() {
    var row = $('#grid').datagrid('getSelected');
    if (row) {
//        $("#editBookInfo").dialog({
//            "title": "编辑书籍信息",
//            width: 500,
//            height: 450,
//            href: 'editbook.aspx'
//        });

        $('#editBookInfo').dialog('open');
        $("#textBookName").val(row.BookName);
        $("#textBookSN").val(row.BookSNCode);
        $("#textBookPrice").val(row.BookPrice);
        $("#etextBookPress").combobox('setValue', row.PressID);
        //   $('#edit').form('clear');
        url = '../ashx/bookhandle.ashx?action=edit&id=' + row.ID;
        formId = "#edit";
        dialogId = "#editBookInfo";

    }
    else {
        $.messager.alert("提示", "您没有选中任何行！");
    }
}

function saveInfo() {

    $(formId).form('submit', {
        url: url,
        onSubmit: function () {
            //alert(formId);
            return $(this).form('validate');
        },
        success: successCallback
    });
}


//  删除代码部分
function deleteBookInfo() {
    var row = $('#grid').datagrid('getSelected');
    if (row) {
        $.messager.confirm('删除提示', '确定要删除' + row.BookName + '吗', function (r) {
            if (r) {
                $.post('../ashx/bookhandle.ashx', { id: row.ID, action: 'delete' }, function (data, status) {

                    if (data == "ok") {
                        $('#grid').datagrid('reload');
                    } else {
                        $.messager.show({
                            title: 'Error',
                            msg: '删除该书籍失败!'
                        });
                    }
                });
            }
        });
    }
}

var successCallback = function (d) {

    $.messager.show({
        title: 'Success',
        msg: "操作成功"
    });
    $(dialogId).dialog('close');
    $('#grid').datagrid('reload');
}
var grid;
var formUrl = "addpress.aspx?n=" + Math.random();
var actionURL = '../ashx/presshandle.ashx';

var url; //提交数据的路径
var formId; //当天要提交的Form的编号
var dialogId; //对话框的编号

$(function () {
    autoResize({ dataGrid: '#grid', gridType: 'datagrid', callback: grid.databind, height: 5 });  

    $("#editPressInfo").dialog({
        "title": "编辑出版社信息",
            width: 500,
            height: 300,
            href: 'editpress.aspx'
        });

        $('#editPressInfo').dialog('open').dialog('close');    
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
                { title: '出版社名称', field: 'PressName', width: 140 },
                { title: '出版社地址', field: 'PressAddress', width: 300 },
                { title: '出版社电话', field: 'PressPhone', width: 150 },               
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
    var press = $("#press").val();
    var phone = $("#phone").val();   

    var filterObj = { "groupOp": "AND", "rules": [{ "field": "PressName", "op": "cn", "data": press }, { "field": "PressPhone", "op": "cn", "data": phone } ] };    
    $('#grid').datagrid('load', { filter: JSON.stringify(filterObj) });
    //$('#grid').datagrid("reload");
}

//添加出版社部分
function addPressInfo() {
    $("#addPressInfo").dialog({
        "title": "新建出版社信息",
        width: 500,
        height: 300,
        href: 'addpress.aspx'
    });
    $('#addPressInfo').dialog('open');
    $('#add').form('clear');

    url = '../ashx/presshandle.ashx?action=add';
    formId = "#add";
    dialogId = "#addPressInfo";
}

//编辑出版社部分
function editPressInfo() {
    var row = $('#grid').datagrid('getSelected');
    if (row) {

        $('#editPressInfo').dialog('open');
        $("#textPressName").val(row.PressName);
        $("#textPressAddress").val(row.PressAddress);
        $("#textPressPhone").val(row.PressPhone);      

        url = '../ashx/presshandle.ashx?action=edit&id=' + row.ID;
        formId = "#edit";
        dialogId = "#editPressInfo";

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
function deletePressInfo() {
    var row = $('#grid').datagrid('getSelected');
    if (row) {
        $.messager.confirm('删除提示', '确定要删除' + row.PressName + '吗', function (r) {
            if (r) {
                $.post('../ashx/presshandle.ashx', { id: row.ID, action: 'delete' }, function (data, status) {

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
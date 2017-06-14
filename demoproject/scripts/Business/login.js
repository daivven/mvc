
var themes = [{ "id": "default", "text": "默认皮肤", "selected": true }, { "id": "gray", "text": "流行灰" }]
var savecookdays = [{ "id": 7, "text": "保存7天", "selected": true }, { "id": 30, "text": "保存30天" }, { "id": 365, "text": "永久保存" }, { "id": 1, "text": "不保存" }]

$(function () {

    $.hDialog({
        title:'用户登录',boxPadding:'2px',
        width: 478,closable:false,
        height: 355, iconCls: 'icon-user',
        modal:false,draggable:false,
        href: 'common/html/loginForm.html',
        buttons:[{
            text: '登录',
            iconCls: 'icon-user',
            handler: login
        }, {
            text: '了解',
            iconCls: 'icon-comments',
            handler:aboutMe
        }],align:'center',
        onLoad: function() {
            $('#txt_save').combobox({
                data: savecookdays, width: 120, valueField: 'id', textField: 'text', editable: false, panelHeight: 'auto'
            });
//            $('#imgValidateCode').click(function () {
//                $(this).attr('src', "validateCode.hxl?t=4&n=" + Math.random());
//            });
        }
    });
    
    //响应键盘的回车事件
    $(this).keydown(function (event) {
        if (event.keyCode == 13) {
            event.returnValue = false;
            event.cancel = true;
            return login();
        }
    });

    
});



function login() {
    
    $('#loginForm').form('submit', {
        url: 'ashx/loginhandler.ashx',
        onSubmit: function () {
            var isValid = $('#loginForm').form('validate');
            if(isValid) {
                $.hLoading.show({ msg: '正在登录中...' });
            }
            return isValid;
        },
        success: function (data) {
            $.hLoading.hide();
            var d = eval('(' + data + ')');
            if (d.success)
                location.href = "/";
            else {
                //更新验证码
                //$('#imgValidateCode').click();
                alert(d.message);
            }
        }
    });
}

function aboutMe(){
    $.hDialog({
        title: '说明',
        width: 400,
        height: 300,
        showBtns: false,
        content:''
    });
}

function getsize() {
    var windowHeight = 0;
    var widowWidth = 0;
    if (typeof (window.innerHeight) == 'number') {
        windowHeight = window.innerHeight;
        widowWidth = window.innerWidth;
    }
    else {
        if (document.documentElement && document.documentElement.clientHeight) {
            windowHeight = document.documentElement.clientHeight;
            widowWidth = document.documentElement.clientWidth;
        }
        else {
            if (document.body && document.body.clientHeight) {
                windowHeight = document.body.clientHeight;
                widowWidth = document.body.clientWidth;
            }
        }
    }

    return { width: widowWidth, height: windowHeight };
}
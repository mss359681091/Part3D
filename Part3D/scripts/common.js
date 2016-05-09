
function MM_showHideLayers() { //v9.0
    var i, p, v, obj, args = MM_showHideLayers.arguments;
    for (i = 0; i < (args.length - 2) ; i += 3)
        with (document) if (getElementById && ((obj = getElementById(args[i])) != null)) {
            v = args[i + 2];
            if (obj.style) { obj = obj.style; v = (v == 'show') ? 'visible' : (v == 'hide') ? 'hidden' : v; }
            obj.visibility = v;
        }
}

function fnLogin() {
    var username = $("#txtusername").val();
    var password = $("#txtpassword").val();
    var isremember = $("#chkremember").prop("checked");
    if (username.trim() == "") {
        $("#tipusername").text("用户名不能为空！");
        $("#txtusername").focus();
        return;
    }
    if (password.trim() == "") {
        $("#tippassword").text("密码不能为空！");
        $("#txtpassword").focus();
        return;
    }
    if (password.length < 6 || password.length > 20) {
        $("#tippassword").text("密码长度为6-20！");
        $("#txtpassword").focus();
        return;
    }
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Login.aspx/DoLogin",
        data: "{paramUsername:'" + username + "',paramPassword:'" + password + "',paramRemember:'" + isremember + "'}",
        dataType: 'json',
        success: function (result) {
            $("#tipusername").text("");
            $("#tippassword").text("");
            switch (result.d) {
                case "-1":
                    $("#tipusername").text("用户名不能为空！");
                    $("#txtusername").focus();
                    return;
                    break;
                case "-2":
                    $("#tippassword").text("密码不能为空！");
                    $("#txtpassword").focus();
                    return;
                    break;
                case "-3":
                    $("#tippassword").text("用户名或密码错误！");
                    $("#txtpassword").focus();
                    return;
                    break;
                case "1":
                    window.location = "/Index.aspx";
                    break;
                default:
                    break;
            }
        }
    });
}

function fnChecked() {
    if ($("#chkreaded").prop("checked") == true) {
        var username = $("#txt_regUsername").val();
        var password = $("#txt_regPassword").val();
        var password1 = $("#txt_regPassword1").val();
        var email = $("#txt_regEmail").val();
        var nickname = $("#txt_regNickname").val();
        var mobile = $("#txt_regMobile").val();
        $("#main0 i").text("");
        if (username.trim() == "") {
            $("#txt_regUsername").next("i").text("用户名不能为空！");
            $("#txt_regUsername").focus();
            return;
        }
        if (nickname.trim() == "") {
            $("#txt_regNickname").next("i").text("昵称不能为空！");
            $("#txt_regNickname").focus();
            return;
        }
        if (password.trim() == "") {
            $("#txt_regPassword").next("i").text("密码不能为空！");
            $("#txt_regPassword").focus();
            return;
        }
        if (password1.trim() != password.trim()) {
            $("#txt_regPassword1").next("i").text("两次输入密码不一致！");
            $("#txt_regPassword1").focus();
            return;
        }

        fnRegister(username, password, nickname, email, mobile);

    }
    else {
        alert("请阅读并接受相关条款！");
    }
}

function fnCheckedUsername() {
    var username = $("#txt_regUsername").val();
    var myreg = /^(?!_)(?!.*?_$)[a-zA-Z0-9_\u4e00-\u9fa5]+$/;
    if ((!myreg.test(username)) || username.trim().length < 4) {
        $("#txt_regUsername").next("i").text("用不名格式不正确,请输入4-20字符，不能以下划线开头和结尾！");
        $("#txt_regUsername").focus();
        return;
    }
    else {
        $("#txt_regUsername").next("i").text("");
    }
    //do something

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Login.aspx/ChkUsername",
        data: "{paramUsername:'" + username + "'}",
        dataType: 'json',
        success: function (result) {
            if (result.d == "-1") {
                $("#txt_regUsername").next("i").text("该用户名已经存在！");
                $("#txt_regUsername").focus();
                return;
            }
            else {
                $("#txt_regUsername").next("i").text("");
            }
        }
    });
}

function fnCheckedEmail(obj) {
    var email = $(obj).val();
    var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    if (!myreg.test(email)) {
        $(obj).next("i").text("邮箱格式不正确！");
        //$("#txt_regEmail").focus();
        return false;
    }
    else {
        $(obj).next("i").text("");
    }
    //do something

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Login.aspx/ChkEmail",
        data: "{paramEmail:'" + email + "'}",
        dataType: 'json',
        success: function (result) {
            if (result.d == "-1") {
                $("#txt_regEmail").next("i").text("该邮箱已经关联一个账号！");
                //$("#txt_regEmail").focus();
                return;
            }
            else {
                $("#txt_regEmail").next("i").text("");
            }
        }
    });

}

function fnCheckedMobile() {
    var mobile = $("#txt_regMobile").val();
    var partten = /^1[3,5,8]\d{9}$/;
    if (!partten.test(mobile)) {
        $("#txt_regMobile").next("i").text("手机格式不正确！");
        return false;
    }
    else {
        $("#txt_regMobile").next("i").text("");
    }

}

function fnRegister(username, password, nickname, email, mobile) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Login.aspx/FnRegister",
        data: "{paramUsername:'" + username + "',paramPassword:'" + password + "',paramNickname:'" + nickname + "',paramEmail:'" + email + "',paramMobile:'" + mobile + "'}",
        dataType: 'json',
        success: function (result) {

            switch (result.d) {
                case "1":
                    alert("注册成功!");
                    return;
                    break;
                case "-1":
                    alert("注册信息不能为空!");
                    return;
                    break;
                case "-2":
                    $("#txt_regUsername").next("i").text("用户名包含非法字符！");
                    $("#txt_regUsername").focus();
                    return;
                    break;
                case "-3":
                    $("#txt_regPassword").next("i").text("密码包含非法字符！");
                    $("#txt_regPassword").focus();
                    return;
                    break;
                case "-4":
                    $("#txt_regUsername").next("i").text("该用户名已经存在！");
                    $("#txt_regUsername").focus();
                    return;
                    break;
                case "-5":
                    $("#txt_regEmail").next("i").text("该邮箱已经关联一个账号！");
                    $("#txt_regEmail").focus();
                    return;
                    break;
                case "-6":
                    $("#txt_regEmail").next("i").text("邮箱格式不正确！");
                    $("#txt_regEmail").focus();
                    return;
                    break;
                default:
                    break;
            }
        }
    });
}

function fnChooseme(ClassifyID, obj) {
    $(obj).parents("ul").data("ClassifyID", ClassifyID);
    $("#ulclassify li a").removeClass("hover");
    $(obj).addClass("hover");
    $(".txtClassifyID").val($(obj).text());
    document.getElementById("hidClassfiyID").value = ClassifyID;
}

$(document).ready(function () {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Index.aspx/GetClassify",
        data: "{paramtype:'0'}",
        dataType: 'json',
        success: function (result) {
            $("#top_ul").append(result.d);
        }
    });

});

function fnNext() {
    if ($("#lisend").next("i").text().length > 0) {
        return;
    }
    var lisend = $("#lisend").val();
    if (lisend.length == "") {
        $("#lisend").next("i").text("请输入邮箱！");
        return;
    }
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/Login.aspx/SendEmail",
        data: "{paramEmail:'" + lisend + "'}",
        dataType: 'json',
        success: function (result) {
            if (result.d == "1") {
                $("#femail").text(lisend);
                $("#lnkmaileto").attr("href", "mailto:" + lisend);
                $("#ulsend").hide();
                $("#ulsuccess").show();
                return;
            }

        }
    });
}
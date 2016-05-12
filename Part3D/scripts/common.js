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
    $(obj).parents("ul").data("classid", ClassifyID);
    $("#ulclassify li a").removeClass("hover");
    $(obj).addClass("hover");
    $(".txtClassifyID").val($(obj).text());
    document.getElementById("hidClassifyId").value = ClassifyID;
}

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

//图片上传预览    IE是用了滤镜。
function previewImage(file) {
    var MAXWIDTH = 260;
    var MAXHEIGHT = 180;
    var div = document.getElementById('preview');
    if (file.files && file.files[0]) {
        //div.innerHTML = '<img onclick="fnChooseImg();" id=imghead>';
        var img = document.getElementById('imghead');
        img.onload = function () {
            var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
            img.width = rect.width;
            img.height = rect.height;
            //                 img.style.marginLeft = rect.left+'px';
            img.style.marginTop = rect.top + 'px';
        }
        var reader = new FileReader();
        reader.onload = function (evt) { img.src = evt.target.result; }
        reader.readAsDataURL(file.files[0]);
    }
    else //兼容IE
    {
        var sFilter = 'filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src="';
        file.select();
        var src = document.selection.createRange().text;
        //div.innerHTML = '<img onclick="fnChooseImg();" id=imghead>';
        var img = document.getElementById('imghead');
        img.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = src;
        var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
        status = ('rect:' + rect.top + ',' + rect.left + ',' + rect.width + ',' + rect.height);
        div.innerHTML = "<div id=divhead style='width:" + rect.width + "px;height:" + rect.height + "px;margin-top:" + rect.top + "px;" + sFilter + src + "\"'></div>";
    }

    $("#addpic").hide();
    $("#preview").show();
}

function clacImgZoomParam(maxWidth, maxHeight, width, height) {
    var param = { top: 0, left: 0, width: width, height: height };
    if (width > maxWidth || height > maxHeight) {
        rateWidth = width / maxWidth;
        rateHeight = height / maxHeight;

        if (rateWidth > rateHeight) {
            param.width = maxWidth;
            param.height = Math.round(height / rateWidth);
        } else {
            param.width = Math.round(width / rateHeight);
            param.height = maxHeight;
        }
    }

    param.left = Math.round((maxWidth - param.width) / 2);
    param.top = Math.round((maxHeight - param.height) / 2);
    return param;
}

function fnChooseImg() {
    $("#btnfile").click();
}

function doUplaod() {
    if ($.trim($("#txtPartname").val()).length == 0) {
        $("#txtPartname").focus();
        alert("组件名称不能为空!");
        return;
    }
    var classid = document.getElementById("hidClassifyId").value;
    if ($.trim(classid).length == 0) {
        alert("请选择分类!");
        return;
    }
    $('#file_upload').uploadify('upload', '*');
}

function closeLoad() {
    $('#file_upload').uploadify('cancel', '*');
}

function fnSaveImg(filenames, successcount) {
    var options = {
        url: "/user/jqUploadify/WebUpload.aspx",
        success: function () {
            $("#fm1").resetForm();
            $("#preview").hide();
            $("#addpic").show();
            alert("上传成功！");
        }
    };
    $("#fm1").ajaxForm(options);
}

function fnsearch() {
    var searchkey = $("#txtkey").val();
    var classid = $("#Claa_S").data("classid");
    if (window.location.pathname == "/List.aspx") {
        $("#lnkclass li").removeClass();
        switch ($.trim(classid)) {
            case "":
                $("#lnkclass li:eq(0)").addClass("hover");
                break;
            case "1":
                $("#lnkclass li:eq(1)").addClass("hover");
                break;
            case "12":
                $("#lnkclass li:eq(2)").addClass("hover");
                break;
            case "13":
                $("#lnkclass li:eq(3)").addClass("hover");
                break;
            default:
                break;
        }
        fnGetList('', '', classid, searchkey, '1', '12');
    }
    else {
        window.open(encodeURI("/List.aspx?classid=" + classid + "&& sk=" + searchkey));
    }
}

function fnmore() {
    window.open("list.aspx");
}

function fnGetList(ParentID, UserID, ClassifyID, Name, CurrentIndex, PageSize) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/List.aspx/GetPartList",
        async: true,
        data: "{ParentID:'" + ParentID + "',UserID:'" + UserID + "',ClassifyID:'" + ClassifyID + "',Name:'" + Name + "',CurrentIndex:'" + CurrentIndex + "',PageSize:'" + PageSize + "'}",
        dataType: 'json',
        success: function (result) {
            //var status = result.d.result;//状态
            //var errmsg = result.d.errmsg;//错误信息
            $(".Index_List li").remove();
            var returnData = result.d.returnData;
            if (returnData != null) {
                var strli = "";
                $.each(returnData, function (i, item) {
                    strli += "<li><span>";
                    strli += "<button type='button' data-event='D_Step'>IGS</button>";
                    strli += "<button type='button' data-event='D_Step'>STEP</button>";
                    strli += "<button type='button' data-event='D_Step'>X_T</button></span>";
                    strli += "<p>";
                    strli += "<a target='_blank' href='/View.aspx?partid=" + item.ID + "' style='padding: 0'><img  src='" + item.PreviewSmall + "' alt='" + item.Name + "' /></a>";
                    strli += "</p>";
                    strli += "<a target='_blank' href='/View.aspx?partid=" + item.ID + "'>" + item.Name + "</a></li>";
                });
                $(".Index_List li").remove();
                $(".Index_List ul").append(strli);
                $('button[data-event=D_Step]').on('click', function () {
                    var d = dialog({
                        fixed: true,
                        title: 'IGS格式',
                        content: document.getElementById('D_Step')
                    })
                    d.width(960);
                    d.showModal();
                });
            }
        }
    });
}
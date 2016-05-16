<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="Part3D.PersonalInfo" %>

<%@ Register Src="WUCBottom.ascx" TagName="WUCBottom" TagPrefix="uc1" %>
<%@ Register Src="WUCTop.ascx" TagName="WUCTop" TagPrefix="uc2" %>
<%@ Register Src="WUCLink.ascx" TagName="WUCLink" TagPrefix="uc3" %>
<%@ Register Src="WUCPersonalBanner.ascx" TagName="WUCPersonalBanner" TagPrefix="uc4" %>
<!doctype html>
<html class="no-js">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="">
    <meta name="keywords" content="">
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon">
    <title>个人中心-个人信息</title>
    <link rel="stylesheet" type="text/css" href="/content/Style.css" />
    <link rel="stylesheet" href="/content/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />
    <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript">
        //修改密码
        function fnSetPassword() {
            var oldpassword = $("#txt_oldpassword").val();
            var newpassword = $("#txt_newpassword").val();
            var chkpassword = $("#txt_chkpassword").val();
            if (newpassword.trim().length < 6) {
                $("#txt_newpassword").focus();
                alert("密码过短，密码长度6-20！");
                return;
            }
            if (chkpassword != newpassword) {
                $("#txt_chkpassword").focus();
                alert("两次输入密码不一致！");
                return;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/PersonalInfo.aspx/SetPassword",
                data: "{oldpassword:'" + oldpassword + "',newpassword:'" + newpassword + "'}",
                dataType: 'json',
                success: function (result) {
                    if (result.d == "1") {
                        $("#txt_newpassword").val("");
                        $("#txt_chkpassword").val("");
                        alert("修改成功！");

                    }
                    else {
                        alert("原密码不正确！");
                        return;
                    }
                }
            });

        }
        //修改昵称
        function fnSetNickname() {
            var nickname = $("#txt_nickname").val();
            if (nickname.trim().length == 0) {
                $("#txt_nickname").focus();
                alert("昵称不能为空");
                return;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/PersonalInfo.aspx/SetNickname",
                data: "{nickname:'" + nickname + "'}",
                dataType: 'json',
                success: function (result) {
                    if (result.d == "1") {
                        $(".spNickname").text(nickname);
                        $(".sp_nickname").text("昵称：" + nickname);
                        $("#txt_nickname").val("");
                        $(".lnkLogined").html("<i class='iconfont'>&#xe606;</i>" + nickname);
                        alert("修改成功！");

                    }
                }
            });
        }
        //修改邮箱
        function fnSetEmail() {

            var email = $("#txt_email").val();
            var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
            if (!myreg.test(email)) {
                $("#txt_email").focus();
                alert("邮箱格式不正确");
                return false;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/PersonalInfo.aspx/SetEmail",
                data: "{email:'" + email + "'}",
                dataType: 'json',
                success: function (result) {
                    if (result.d == "1") {
                        $(".spEmail").text(email);
                        $(".sp_email").text("邮箱：" + email);
                        $("#txt_email").val("");
                        alert("修改成功！");
                    }
                }
            });
        }

        //修改手机号
        function fnSetMobile() {

            var mobile = $("#txt_mobile").val();
            var partten = /^1[3,5,8]\d{9}$/;
            if (!partten.test(mobile)) {
                $("#txt_mobile").focus();
                alert("手机格式不正确！");
                return false;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/PersonalInfo.aspx/SetMobile",
                data: "{mobile:'" + mobile + "'}",
                dataType: 'json',
                success: function (result) {
                    if (result.d == "1") {
                        $(".spMobile,.sp_mobile").text(mobile);
                        $(".sp_mobile").text("手机号：" + mobile);
                        $("#txt_mobile").val("");
                        alert("修改成功！");

                    }
                }
            });

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="navbarM">
                <uc2:WUCTop ID="WUCTop1" runat="server" />
            </div>

            <div class="Clear">
            </div>
            <uc4:WUCPersonalBanner ID="WUCPersonalBanner1" runat="server" />

            <div class="User_My Container">
                <h1>账户绑定</h1>
                <ul>
                    <li>登陆账户：<span id="spUsername" class="spUsername" runat="server"></span></li>
                    <li>绑定手机： <span id="spMobile" class="spMobile" runat="server"></span>
                        <button type="button" data-event="Phone">更换</button></li>
                    <li>保密邮箱： <span id="spEmail" class="spEmail" runat="server"></span>
                        <button type="button" data-event="Mail">绑定</button></li>
                    <li>昵称：<span id="spNickname" class="spNickname" runat="server"></span>
                        <button type="button" data-event="User">更换</button></li>
                </ul>
                <h1>密码安全</h1>
                <div>
                    <span>登陆密码</span><p>安全性高的密码可以使帐号更安全。建议你定期更换密码，设置一个包含字母，符号或数字中至少两项具长度超过6位的密码。</p>
                    <button type="button" data-event="Password">修改</button>
                </div>
            </div>

            <!--弹出下载窗口-->
            <script type="text/ecmascript">
                $('button[data-event=Password]').on('click', function () {
                    var d = dialog({
                        title: '修改密码',
                        content: document.getElementById('Password'),
                        okValue: '确定',
                        ok: function () {
                            //this.title('提交中…');
                            fnSetPassword();
                            return false;
                        },
                        cancelValue: '取消',
                        cancel: function () { }
                    });
                    d.showModal();
                });
            </script>
            <div id="Password" style="display: none;" class="User_Windows">
                <ul>
                    <li><span>旧密码验证：</span><input maxlength="20" id="txt_oldpassword" type="password" placeholder="请输入旧密码..."></li>
                    <li><span>新密码：</span><input maxlength="20" id="txt_newpassword" type="password" placeholder="请输入6-20位密码..."></li>
                    <li><span>再次确认密码：</span><input maxlength="20" id="txt_chkpassword" type="password" placeholder="请输入6-20位密码..."></li>
                </ul>
            </div>

            <!--弹出下载窗口-->
            <script type="text/ecmascript">
                $('button[data-event=Phone]').on('click', function () {
                    var d = dialog({
                        title: '绑定/变更手机',
                        content: document.getElementById('Phone'),
                        okValue: '确定',
                        ok: function () {
                            fnSetMobile();
                            return false;
                        },
                        cancelValue: '取消',
                        cancel: function () { }
                    });
                    d.showModal();
                });
            </script>
            <div id="Phone" style="display: none;" class="User_Windows">
                <ul>
                    <!--<li><span>手机号码：</span><input type="text"></li>-->
                    <li><span id="sp_mobile" class="sp_mobile" runat="server">手机号码：</span></li>
                    <%-- <li><span>验证码：</span><input type="text" class="Inp" placeholder="请输入验证码...">
                        <button type="button">获取验证码</button></li>--%>
                    <li><span>新的手机号码：</span><input id="txt_mobile" type="text" maxlength="11" placeholder="请输入新的手机号码..."></li>
                </ul>
            </div>

            <!--弹出下载窗口-->
            <script type="text/ecmascript">
                $('button[data-event=Mail]').on('click', function () {
                    var d = dialog({
                        title: '绑定/变更邮箱',
                        content: document.getElementById('Mail'),
                        okValue: '确定',
                        ok: function () {
                            fnSetEmail();
                            return false;
                        },
                        cancelValue: '取消',
                        cancel: function () { }
                    });
                    d.showModal();
                });
            </script>
            <div id="Mail" style="display: none;" class="User_Windows">
                <ul>
                    <li><span id="sp_email" class="sp_email" runat="server">邮箱：</span></li>
                    <li><span>新邮箱：</span><input maxlength="50" id="txt_email" type="text" placeholder="请输入新的邮箱地址..."></li>
                </ul>
            </div>

            <!--弹出下载窗口-->
            <script type="text/ecmascript">
                $('button[data-event=User]').on('click', function () {
                    var d = dialog({
                        title: '变更昵称',
                        content: document.getElementById('User'),
                        okValue: '确定',
                        ok: function () {
                            fnSetNickname();
                            return false;
                        },
                        cancelValue: '取消',
                        cancel: function () { }
                    });
                    d.showModal();
                });
            </script>
            <div id="User" style="display: none;" class="User_Windows">
                <ul>
                    <li><span id="sp_nickname" class="sp_nickname" runat="server">昵称：</span></li>
                    <li><span>新昵称：</span><input maxlength="50" id="txt_nickname" type="text" placeholder="请输入新的昵称..."></li>
                </ul>
            </div>

            <div class="Clear"></div>
            <div class="Index_Foot">
                <uc3:WUCLink ID="WUCLink1" runat="server" />
            </div>

            <div class="Index_FootB">
                <uc1:WUCBottom ID="WUCBottom1" runat="server" />
            </div>

        </div>
    </form>
</body>
</html>

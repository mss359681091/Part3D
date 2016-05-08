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
    <link rel="stylesheet" href="/contenticonfont/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />
    <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
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
                    <li>登陆账户： kenboy@liknet.cn</li>
                    <li>绑定手机： 15909449087
                        <button type="button" data-event="Phone">更换</button></li>
                    <li>保密邮箱： 未绑定
                        <button type="button" data-event="Mail">绑定</button></li>
                    <li>昵称：一泽瑞尔
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
                            this.title('提交中…');
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
                    <li><span>旧密码验证：</span><input type="text" placeholder="请输入旧密码..."></li>
                    <li><span>新密码：</span><input type="text" placeholder="请输入6-20位密码..."></li>
                    <li><span>再次确认密码：</span><input type="text" placeholder="请输入6-20位密码..."></li>
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
                            this.title('提交中…');
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
                    <li><span>手机号码：15909449087</span></li>
                    <li><span>验证码：</span><input type="text" class="Inp" placeholder="请输入验证码...">
                        <button type="button">获取验证码</button></li>
                    <li><span>新的手机号码：</span><input type="text" placeholder="请输入新的手机号码..."></li>
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
                            this.title('提交中…');
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
                    <li><span>邮箱：</span><input type="text" placeholder="请输入新的邮箱地址..."></li>
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
                            this.title('提交中…');
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
                    <li><span>昵称：一泽瑞尔</span></li>
                    <li><span>新昵称：</span><input type="text" placeholder="请输入新的昵称..."></li>
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

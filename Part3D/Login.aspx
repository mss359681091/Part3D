<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Part3D.Login" %>


<%@ Register Src="/user/WUCBottom.ascx" TagName="WUCBottom" TagPrefix="uc1" %>
<%@ Register Src="/user/WUCTop.ascx" TagName="WUCTop" TagPrefix="uc2" %>
<%@ Register Src="/user/WUCLink.ascx" TagName="WUCLink" TagPrefix="uc3" %>
<%@ Register Src="/user/WUCBanner.ascx" TagName="WUCBanner" TagPrefix="uc4" %>
<!doctype html>
<html class="no-js">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="">
    <meta name="keywords" content="">
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon">
    <title>3D组件</title>
    <link rel="stylesheet" type="text/css" href="/content/Style.css" />
    <link rel="stylesheet" href="/content/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />

    <!-- Bootstrap -->
    <link rel="stylesheet" media="screen" href="/content/bootstrap.min.css">

    <!-- jQuery ui -->
    <link href="/content/jquery-ui-1.10.4.min.css" rel="stylesheet" media="screen">

    <!-- SliderLock -->
    <link href="/content/sliderlock.css" rel="stylesheet" media="screen">

    <script type="text/javascript">
        function fnLogin() {
            var username = $("#txtusername").val();
            var password = $("#txtpassword").val();
            $.post("Login.aspx", { username: '' + username + '' }, function (data) {
                alert(data);
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

            <div class="Clear"></div>
            <div class="Top_divM">
                <uc4:WUCBanner ID="WUCBanner1" runat="server" />
            </div>

            <div class="Login_L Container">
                <script type="text/javascript">
                    function setTab_T(m, n) {
                        var tli = document.getElementById("menu" + m).getElementsByTagName("li");
                        var mli = document.getElementById("main" + m).getElementsByTagName("dd");
                        for (i = 0; i < tli.length; i++) {
                            tli[i].className = i == n ? "hover" : "";
                            mli[i].style.display = i == n ? "block" : "none";
                        }
                    }
                </script>
                <ul id="menu0" class="Mun">
                    <li class="hover" onclick="setTab_T(0,0)">登录</li>
                    <li onclick="setTab_T(0,1)">注册</li>
                    <li onclick="setTab_T(0,2)">忘记密码</li>
                </ul>
                <div id="main0" class="Mun_V">
                    <dd class="block">
                        <h1>Hi，登录网站与设计师，一起分享快乐吧！</h1>
                        <ul>
                            <li><span>帐号：</span><input id="txtusername" type="text" placeholder="请输入登录帐号或绑定邮箱..." class="inp" value=""><i>提示</i></li>
                            <li><span>密码：</span><input id="txtpassword" type="password" placeholder="请输入6-20个字符..." class="inp"></li>
                            <li>
                                <label>
                                    <input type="checkbox" />
                                    下次自动登录</label><a href="#" class="Oth">忘记密码？</a></li>
                            <li>
                                <button type="button" onclick="fnLogin()">立即登录</button><a href="#" style="margin-left: 20px;">注册<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                                </a></li>
                        </ul>
                    </dd>
                    <dd>
                        <ul>
                            <li><span>帐号：</span><input type="text" placeholder="请输入登录帐号..." class="inp" value=""><i></i></li>
                            <li><span>邮箱：</span><input type="text" placeholder="请输入邮箱..." class="inp" value=""><i></i></li>
                            <li><span>昵称：</span><input type="text" placeholder="请输入用户名..." class="inp"></li>
                            <li><span>密码：</span><input type="password" placeholder="请输入6-20个字符..." class="inp"></li>
                            <li><span>确认密码：</span><input type="password" placeholder="请再输入一次密码！" class="inp"></li>
                            <li>
                                <span>验证：</span>
                                <div style="width: 400px; height: 40px; line-height: 40px; margin-top: 8px; float: left;">
                                    <div id="slider" class="sliderLock" style="height: 40px; line-height: 40px;">
                                        <p>用鼠标点击箭头向右滑动解锁</p>
                                    </div>
                                </div>
                                <i class="slider_tip">通过验证才能注册</i>
                            </li>
                            <li>
                                <label>
                                    <input type="checkbox" />
                                    我已阅读并接受 <a href="#">版权声明</a> 和 <a href="#">隐私保护</a> 条款</label></li>
                            <li>
                                <button class="chkhk" type="button" onclick="fn();">马上注册</button><a href="#" style="margin-left: 20px;">己有帐号</a></li>

                        </ul>
                    </dd>
                    <dd>
                        <ul>
                            <li><span>邮箱：</span><input type="text" placeholder="请输入绑定邮箱..." class="inp"><i></i></li>
                            <li>
                                <button class="chkhk" type="button" onclick="fn();">下一步，安全验证</button></li>
                        </ul>

                        <!--以下内容，可以新一个页面里-->
                        <ul class="Pw_Main">
                            <h2>已发送重设密码邮件</h2>
                            <img src="/images/mail.png" alt="" />
                            <li>重设密码邮件已发送到 <font>wangguifu@chengrui.com.cn</font>
                                <br>
                                点击邮箱中的链接即可完成重设密码
                            </li>
                            <li>
                                <button type="button">登陆邮箱完成验证</button></li>
                            <p>
                                1. 如果您没有收到重设密码邮件，请检查垃圾邮箱或广告邮箱目录;<br>
                                2. 邮件到达可能需要几分钟，如果仍没有收到，点击这里 重新发送重设密码邮件;<br>
                                3. 如果一直没有收到邮件，请您使用注册邮件联系我们：support@geetest.com
                            </p>
                        </ul>
                    </dd>
                </div>
            </div>

            <div class="Index_Foot">
                <uc3:WUCLink ID="WUCLink1" runat="server" />
            </div>

            <div class="Index_FootB">
                <uc1:WUCBottom ID="WUCBottom1" runat="server" />
            </div>

        </div>
        <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
        <script type="text/javascript" src="/scripts/dialog.js"></script>
        <script src="/scripts/sliderlock.js"></script>
        <script src="/scripts/bootstrap.min.js"></script>
        <script src="/scripts/jquery-ui-1.10.4.min.js"></script>

    </form>
</body>
</html>

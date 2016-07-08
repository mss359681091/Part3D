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
    <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <script type="text/javascript" src="/scripts/scriptC.js"></script>
    <script src="scripts/laypage/laypage.js"></script>
    <!-- Bootstrap -->
    <link rel="stylesheet" media="screen" href="/content/bootstrap.min.css">
    <script src="/scripts/bootstrap.min.js"></script>

    <!-- jQuery ui -->
    <link href="/content/jquery-ui-1.10.4.min.css" rel="stylesheet" media="screen">
    <script src="/scripts/jquery-ui-1.10.4.min.js"></script>
    <!-- SliderLock -->
    <link href="/content/sliderlock.css" rel="stylesheet" media="screen">
    <%--    <script type="text/javascript" src="/scripts/sliderlock.js"></script>--%>
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
                            <li><span>用户名：</span><input id="txtusername" maxlength="20" type="text" placeholder="请输入登录帐号/绑定邮箱/手机号..." class="inp" value=""><i id="tipusername"></i></li>
                            <li><span>密码：</span><input id="txtpassword" maxlength="20" type="password" placeholder="请输入6-20个字符..." class="inp"><i id="tippassword"></i></li>
                            <li>
                                <label>
                                    <input id="chkremember" type="checkbox" />
                                    下次自动登录</label><a href="javascript:void(0);" class="Oth" onclick="setTab_T(0,2)">忘记密码？</a></li>
                            <li>
                                <button type="button" onclick="fnLogin()">立即登录</button><a href="javascript:void(0);" onclick="setTab_T(0,1)" style="margin-left: 20px;">注册
                                </a></li>
                        </ul>
                    </dd>
                    <dd>
                        <ul>
                            <li><span>用户名：</span><input id="txt_regUsername" maxlength="20" type="text" placeholder="请填写4-20个字符..." class="inp" value="" onblur="fnCheckedUsername()"><i></i></li>
                            <li><span>邮箱：</span><input id="txt_regEmail" maxlength="50" type="text" onblur="fnCheckedEmail(this)" placeholder="请填写正确邮箱..." class="inp" value=""><i></i></li>
                            <li><span>昵称：</span><input id="txt_regNickname" maxlength="20" type="text" placeholder="请填写昵称..." class="inp"><i></i></li>
                            <li><span>手机号：</span><input id="txt_regMobile" maxlength="50" type="text" onblur="fnCheckedMobile()" placeholder="请填写手机号..." class="inp"><i></i></li>
                            <li><span>密码：</span><input id="txt_regPassword" maxlength="20" type="password" placeholder="请填写6-20个字符..." class="inp"><i></i></li>
                            <li><span>确认密码：</span><input id="txt_regPassword1" maxlength="50" type="password" placeholder="请再输入一次密码..." class="inp"><i></i></li>
                            <li>
                                <span>验证码：</span>
                                <div id="idchk" style="height: 40px; line-height: 40px; float: left;">
                                    <%--   <div id="slider" class="sliderLock" style="height: 40px; line-height: 40px;">
                                        <p>用鼠标点击箭头向右滑动解锁</p>
                                    </div>--%>
                                    <input type="text" id="txtCheckCode" maxlength="4"
                                        class="inp" style="width: 200px;" />
                                    <img id="checkCode" src="CreateCheckCode.aspx" title="点击刷新验证码..." width="81"
                                        height="37" onclick="javascript:this.src=this.src+'?'+new Date().getTime().toString(36);"
                                        style="cursor: pointer; margin: 10px 0 0 20px;">
                                </div>
                                <i></i>
                            </li>
                            <li>
                                <label>
                                    <input id="chkreaded" type="checkbox" />
                                    我已阅读并接受 <a href="#">版权声明</a> 和 <a href="#">隐私保护</a> 条款</label></li>
                            <li>
                                <button class="chkhk" type="button" onclick="fnChecked();">马上注册</button><a href="javascript:void(0);" onclick="setTab_T(0,0)" style="margin-left: 20px;">己有帐号</a></li>

                        </ul>
                    </dd>
                    <dd>
                        <ul id="ulsend">
                            <li><span>邮箱：</span><input id="lisend" type="text" placeholder="请输入绑定邮箱..." onblur="fnCheckedEmail(this)" class="inp"><i></i></li>
                            <li>
                                <button type="button" onclick="fnNext()">下一步，安全验证</button></li>
                        </ul>

                        <!--以下内容，可以新一个页面里-->
                        <ul id="ulsuccess" style="display: none" class="Pw_Main">
                            <h2>已发送找回密码邮件</h2>
                            <img src="/images/mail.png" alt="" />
                            <li>找回密码邮件已发送到 <font id="femail"></font>
                                <br>
                                点击邮箱中的链接即可完成重设密码
                            </li>
                            <li>
                                <button type="button"><a style="color: #ffffff" id="lnkmaileto" target="_blank" href="#">登陆邮箱完成验证</a></button></li>
                            <p>
                                1. 如果您没有收到找回密码邮件，请检查垃圾邮箱或广告邮箱目录;<br>
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



    </form>
</body>
</html>

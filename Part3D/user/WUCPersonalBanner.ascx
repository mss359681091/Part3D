<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCPersonalBanner.ascx.cs" Inherits="Part3D.WUCPersonalBanner" %>
<script type="text/javascript">
    $(document).ready(function () {

        var pathname = window.location.pathname;
        $("#pcm a").removeClass("hover");
        switch (pathname) {
            case "/user/PersonalResouces.aspx":
                $("#pcm a").eq(0).addClass("hover");
                break;
            case "/user/PersonalDownloadRecord.aspx":
                $("#pcm a").eq(1).addClass("hover");
                break;
            default:
                $("#pcm a").eq(2).addClass("hover");
                break;

        }


        $("#Claa_S li").bind("click", function () {
            var $this = $(this).text();
            $("#Claa_S").prev().prev().text($this);
            $("#Claa_S").data("classid", $(this).data("classid"))
            MM_showHideLayers('Claa_S', '', 'hide');
        });
    });

</script>
<div class="User_TM">
    <div class="Container">
        <div class="User_img">
            <img id="imgphoto" runat="server" src="/images/img.png" alt="" />
            <span id="spnickname" runat="server">昵称</span>
        </div>
        <div class="SearchM">
            <div class="Class" onmouseover="MM_showHideLayers('Claa_S','','show')" onmouseout="MM_showHideLayers('Claa_S','','hide')">
                <b>全部</b><i class="iconfont">&#xe603;</i>
                <div id="Claa_S" style="margin: 39px 0 0 -8px; *margin: 48px 0 0 -40px" data-classid="">
                    <ul>
                        <li data-classid="">全部</li>
                        <li data-classid="1">国标</li>
                        <li data-classid="12">3D素材</li>
                        <li data-classid="13">3D模型</li>
                    </ul>
                </div>
            </div>
            <input id="txtkey" type="text" maxlength="30" placeholder="请输入关键字..." />
            <button type="button" onclick="fnsearch()"><i class="iconfont">&#xe600;</i>搜索组件</button>
        </div>
    </div>
</div>

<div class="User_Maun">
    <div id="pcm" class="Container Maunx">
        <a class="hover" href="/user/PersonalResouces.aspx">我的资源</a>|<a href="/user/PersonalDownloadRecord.aspx">下载记录</a>|<a href="/user/PersonalInfo.aspx">个人信息</a>
    </div>
</div>

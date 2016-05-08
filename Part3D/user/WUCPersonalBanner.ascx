<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCPersonalBanner.ascx.cs" Inherits="Part3D.WUCPersonalBanner" %>
<div class="User_TM">
    <div class="Container">
        <div class="User_img">
            <img src="/images/img.png" alt="" />
            Afu_UI
        </div>
        <div class="SearchM">
            <div class="Class" onmouseover="MM_showHideLayers('Claa_Su','','show')" onmouseout="MM_showHideLayers('Claa_Su','','hide')">
                <b>3D素材</b><i class="iconfont">&#xe603;</i>
                <div id="Claa_Su">
                    <ul>
                        <li>组件</li>
                        <li>3D模型</li>
                    </ul>
                </div>
            </div>
            <input type="text" placeholder="4332个精品组件" />
            <button type="button"><i class="iconfont">&#xe600;</i>搜索组件</button>
        </div>
    </div>
</div>

<div class="User_Maun">
    <div class="Container Maunx">
        <a href="#">我的资源</a>|<a href="#">下载记录</a>|<a href="#" class="hover">个人信息</a>
    </div>
</div>

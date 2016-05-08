<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCBanner.ascx.cs" Inherits="Part3D.WUCBanner" %>
<div class="Container">
    <div class="Left">
        <img src="/images/Login.png" alt="" />
    </div>
    <div class="SearchM">
        <div class="Class" onmouseover="MM_showHideLayers('Claa_S','','show')" onmouseout="MM_showHideLayers('Claa_S','','hide')">
            <b>组件</b><i class="iconfont">&#xe603;</i>
            <div id="Claa_S">
                <ul>
                    <li>3D素材</li>
                    <li>3D模型</li>
                </ul>
            </div>
        </div>
        <input type="text" placeholder="4332个精品组件" />
        <button type="button"><i class="iconfont">&#xe600;</i>搜索组件</button>
    </div>
</div>

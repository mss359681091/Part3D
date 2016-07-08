<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCTop.ascx.cs" Inherits="Part3D.WUCTop" %>
<div class="Container">
    <div class="Left">
        <a href="/Index.aspx">首页</a><span>|</span>
        <b onmouseover="MM_showHideLayers('T_Class','','show')" onmouseout="MM_showHideLayers('T_Class','','hide')">分类 <i class="iconfont">&#xe603;</i>
            <div id="T_Class">
                <strong>
                    <img src="/images/TOp_T.png" alt="" style="display: none" /></strong>
                <ul id="top_ul">
                    <%--  <li>
                        <p><a target="_blank" href="/List.aspx">国标</a></p>
                        <a href="/user/jqUploadify/WebUpload.aspx">组合件</a><span>/</span><a href="#">连接副</a><span>/</span><a href="#">焊钉</a><span>/</span><a href="#">螺栓</a><span>/</span><a href="#">螺母销</a><span>/</span><a href="#">柳钉</a><span>/</span><a href="#">挡圈</a><span>/</span><a href="#">螺钉</a></li>
                    <li>
                        <p><a target="_blank" href="/List.aspx">3D素材</a></p>
                    </li>
                    <li style="border: none;">
                        <p><a target="_blank" href="/List.aspx">3D模型</a></p>
                    </li>--%>
                </ul>
            </div>
        </b>
        <span>|</span><a target="_blank" href="http://tieba.baidu.com/f?kw=3d%E7%BB%84%E4%BB%B6%E5%BA%93">论坛</a>
    </div>
    <div class="Right">

        <a id="lnkLogin" runat="server" href="/Login.aspx"><i class="iconfont">&#xe606;</i>登录</a>
        <a id="lnkLogined" class="lnkLogined" visible="false" runat="server" href="/user/PersonalResouces.aspx"></a><a id="lnkLoginout" runat="server" visible="false" href="/user/Loginout.aspx"><i class="iconfont">&#xe60b;</i>注销</a><a href="/user/jqUploadify/WebUpload.aspx"><i class="iconfont">&#xe605;</i>上传</a>
    </div>
</div>

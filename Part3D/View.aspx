<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Part3D.View" %>

<%@ Register Src="/user/WUCBottom.ascx" TagName="WUCBottom" TagPrefix="uc1" %>
<%@ Register Src="/user/WUCTop.ascx" TagName="WUCTop" TagPrefix="uc2" %>
<%@ Register Src="/user/WUCLink.ascx" TagName="WUCLink" TagPrefix="uc3" %>
<%@ Register Src="/user/WUCBanner.ascx" TagName="WUCBanner" TagPrefix="uc4" %>

<!doctype html>
<html class="no-js">

<head runat="server">
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
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            var classid = document.getElementById("hidclassid").value;
            var id = document.getElementById("hidid").value;
            $("#btn button").eq(0).data("partid", id);
            $("#btn button").eq(1).data("partid", id);
            $("#btn button").eq(2).data("partid", id);
            //加载最新推荐
            fnRecommend('', classid, id);

        });
        //下载js
        function fndw(strid) {
            $("#hidfileid").val(strid);
            document.getElementById('<%=LinkButton1.ClientID %>').click();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="hidfileid" type="hidden" value="" runat="server" />
            <asp:LinkButton ID="LinkButton1" class="lnkdown" Style="display: none" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
            <div class="navbarM">
                <uc2:WUCTop ID="WUCTop1" runat="server" />
            </div>

            <div class="Clear"></div>
            <div class="Top_divM">
                <uc4:WUCBanner ID="WUCBanner1" runat="server" />
            </div>

            <div class="View Container">
                <div class="View_L">
                    <div class="Tatle">
                        <div class="Left">
                            <b id="btitle" runat="server">组件名称</b>
                            <%--     <span>标签：<a href="#">波形</a><a href="#">弹簧</a></span>--%>
                            <span>上传人：<a id="ausername" runat="server" href="javascript:void(0);">李赛赛</a>上传日期：<a id="acreate" runat="server" href="javascript:void(0);">2016-05-14</a></span>
                        </div>
                        <div class="Right">
                            <%-- <img id="imgPreview" runat="server" src="/images/img.png" alt="" />--%>
                        </div>
                    </div>
                    <div class="V_Img">
                        <img id="imgPreview" runat="server" src="/images/Img.jpg" alt="" style="max-width: 850px;" />
                    </div>
                </div>
                <div class="View_R">
                    <%--    <img src="Img/img.png" alt="" />一泽瑞尔--%>
                    <h5 id="btn">
                        <button type="button" data-format=".igs" data-event="D_Step">下载IGS</button>
                        <button type="button" data-format=".step" data-event="D_Step">下载STEP</button>
                        <button type="button" data-format=".x_t" data-event="D_Step">下载X_T</button></h5>
                </div>
            </div>

            <div id="D_Step" style="display: none;">
                <dl class="Class">
                    <dt>快速定位型号：</dt>

                </dl>
                <ul>
                </ul>
            </div>

            <div class="Index_Hot Container">
                <img src="/images/Tatle3.png" alt="" />
            </div>
            <asp:HiddenField ID="hidclassid" runat="server" />
            <asp:HiddenField ID="hidid" runat="server" />
            <div class="Index_List Container">
                <ul>
                    <li>
                        <p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a>
                    </li>

                </ul>
            </div>

            <div class="Clear"></div>

            <div class="V_Ad Container">
                <a id="lnkad" runat="server" href="#" target="_blank" visible="false">
                    <img id="imgad" width="1000" runat="server" alt="" />
                </a>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Part3D.Index" %>

<%@ Register Src="/user/WUCBottom.ascx" TagName="WUCBottom" TagPrefix="uc1" %>
<%@ Register Src="/user/WUCTop.ascx" TagName="WUCTop" TagPrefix="uc2" %>
<%@ Register Src="/user/WUCLink.ascx" TagName="WUCLink" TagPrefix="uc3" %>

<!doctype html>
<html class="no-js">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="">
    <meta name="keywords" content="3D组件库">
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon">
    <title>3D组件库</title>
    <link rel="stylesheet" type="text/css" href="/content/Style.css" />
    <link rel="stylesheet" href="/content/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />
    <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/scripts/scriptC.js"></script>
    <script type="text/javascript" src="/scripts/jquery.kinMaxShow-1.1.min.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/Index.aspx/GetClassify",
                data: "{paramtype:'2',parentid:'1'}",
                dataType: 'json',
                success: function (result) {
                    $(".Index_Top ul").append(result.d);
                }
            });

            //加载最新推荐
            fnGetList('', '', '', '', '1', '12');
            //getStandard('', '');
        });


        function fndw(strid) {
            $("#hidfileid").val(strid);
            document.getElementById('<%=LinkButton1.ClientID %>').click();
        }
    </script>
</head>
<body>
    <form id="fm1" runat="server" method="post">
        <div>
            <header>
                <div class="navbar fixed-top ZindexT">
                    <uc2:WUCTop ID="WUCTop1" runat="server" />
                </div>
            </header>

            <div class="Login absolute_T ZindexZ">
                <img src="/images/Login.png" alt="" />
            </div>
            <div class="Search absolute_T">
                <div class="Class" onmouseover="MM_showHideLayers('Claa_S','','show')" onmouseout="MM_showHideLayers('Claa_S','','hide')">
                    <b>全部</b><i class="iconfont">&#xe603;</i>
                    <div id="Claa_S" data-classid="">
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
            <script type="text/javascript">
                $(function () {
                    $("#kinMaxShow").kinMaxShow({
                        height: 500,
                        button: {
                            showIndex: false,
                            normal: { marginRight: '8px', border: '0', right: '44%', bottom: '20px', background: '#CCC' },
                            focus: { border: '0', background: '#F60' }
                        }
                    });
                });
            </script>

            <div id="kinMaxShow">
                <div>
                    <img src="/images/Top_Ad1.jpg" />
                </div>
                <div>
                    <img src="/images/Top_Ad2.jpg" />
                </div>
                <div>
                    <img src="/images/Top_Ad3.jpg" />
                </div>
                <div>
                    <img src="/images/Top_Ad4.jpg" />
                </div>
                <div>
                    <img src="/images/Top_Ad5.jpg" />
                </div>
            </div>

            <div class="Index_Top">
                <div class="Container">
                    <img src="/images/Tatle1.png" alt="" />
                    <ul>
                        <%--  <li><a href="#"><i class="Ico1"></i>组合件</a></li>
                        <li><a href="#"><i class="Ico2"></i>连接副</a></li>
                        <li><a href="#"><i class="Ico3"></i>焊钉</a></li>
                        <li><a href="#"><i class="Ico4"></i>螺栓</a></li>
                        <li><a href="#"><i class="Ico5"></i>垫圈</a></li>
                        <li><a href="#"><i class="Ico6"></i>柳钉</a></li>
                        <li><a href="#"><i class="Ico7"></i>螺母</a></li>
                        <li><a href="#"><i class="Ico8"></i>螺钉</a></li>
                        <li><a href="#"><i class="Ico9"></i>销</a></li>
                        <li><a href="#"><i class="Ico10"></i>挡圈</a></li>
                        <li><a href="#"><i class="Ico11"></i>更多标准</a></li>--%>
                    </ul>
                </div>
            </div>

            <div class="Index_Hot Container">
                <img src="/images/Tatle2.png" alt="" />
            </div>

            <div class="Index_List Container">
                <ul>
                    <%--  <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span>
                        <p>
                            <a target="_blank" href="View.aspx" style="padding: 0">
                                <img src="/images/img.png" alt="" /></a>
                        </p>
                        <a target="_blank" href="/Web3DPart/View.aspx">GBT7246 波形弹簧垫圈</a></li>--%>
                </ul>
            </div>

            <!--弹出下载窗口-->
            <%--            <script type="text/ecmascript">
               
            </script>--%>
            <input id="hidfileid" type="hidden" value="" runat="server" />
            <asp:LinkButton ID="LinkButton1" class="lnkdown" Style="display: none" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
            <div id="D_Step" style="display: none;">
                <dl class="Class">
                    <dt>快速定位型号：</dt>
                    <%--                <dd class="hover" title="点击选中，再点取消。">M010</dd>
                    <dd title="点击选中，再点取消。">M020</dd>
                    <dd title="点击选中，再点取消。">M030</dd>
                    <dd title="点击选中，再点取消。">M040</dd>
                    <dd title="点击选中，再点取消。">M050</dd>--%>
                </dl>
                <ul>
                    <%--<li class="hover">M010-00101</li>
                    <li class="hover">M010-00102</li>
                    <li class="hover">M010-00102</li>
                    <li class="hover">M010-00104</li>
                    <li class="hover">M010-00105</li>
                    <li class="hover">M010-00106</li>
                    <li class="hover">M010-00107</li>
                    <li>M020-00101</li>
                    <li>M020-00102</li>
                    <li>M020-00102</li>
                    <li>M020-00104</li>
                    <li>M020-00105</li>--%>
                </ul>
            </div>

            <div class="Clear"></div>
            <div class="Index_Button">
                <button type="button" onclick="fnmore()">发现更多</button>
            </div>

            <div class="Index_Foot">
                <uc3:WUCLink ID="WUCLink1" runat="server" />
            </div>

            <div class="Index_FootB">
                <uc1:WUCBottom ID="WUCBottom1" runat="server" />
            </div>

        </div>

        <script type="text/javascript">

            $(document).ready(function () {
                $("#Claa_S li").bind("click", function () {
                    var $this = $(this).text();
                    $("#Claa_S").prev().prev().text($this);
                    $("#Claa_S").data("classid", $(this).data("classid"))
                    MM_showHideLayers('Claa_S', '', 'hide');
                });
            });
        </script>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Part3D.List" %>

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
            $(".Top_Class li").bind("click", function () {
                $(this).addClass("hover").siblings().removeClass("hover");
            });
        });
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

            <div class="Top_Class">
                <ul>
                    <li class="hover"><a href="#">全部</a><span>(2384)</span></li>
                    <li><a href="#">国标</a><span>(2243)</span></li>
                    <li><a href="#">3D素材</a><span>(77)</span></li>
                    <li><a href="#">3D模型</a><span>(64)</span></li>
                </ul>
            </div>

            <div class="Index_List Container">
                <ul>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span>
                        <p>
                            <a target="_blank" href="View.aspx" style="padding: 0">
                                <img src="/images/img.png" alt="" /></a>
                        </p>
                        <a target="_blank" href="View.aspx">GBT7246 波形弹簧垫圈</a></li>

                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span>
                        <p>
                            <a target="_blank" href="View.aspx" style="padding: 0">
                                <img src="/images/img.png" alt="" /></a>
                        </p>
                        <a target="_blank" href="View.aspx">GBT7246 波形弹簧垫圈</a></li>

                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                    <li><span>
                        <button type="button" data-event="D_Step">IGS</button>
                        <button type="button" data-event="D_Step">STEP</button>
                        <button type="button" data-event="D_Step">X_T</button></span><p>
                            <img src="/images/img.png" alt="" />
                        </p>
                        <a href="#">GBT7246 波形弹簧垫圈</a></li>
                </ul>
            </div>

            <!--弹出下载窗口-->
            <script type="text/ecmascript">
                $('button[data-event=D_Step]').on('click', function () {
                    var d = dialog({
                        fixed: true,
                        title: 'IGS格式',
                        content: document.getElementById('D_Step')
                    })
                    d.width(960);
                    d.showModal();
                });
            </script>
            <div id="D_Step" style="display: none;">
                <dl class="Class">
                    <dt>快速定位标签：</dt>
                    <dd class="hover" title="点击选中，再点取消。">M010</dd>
                    <dd title="点击选中，再点取消。">M020</dd>
                    <dd title="点击选中，再点取消。">M030</dd>
                    <dd title="点击选中，再点取消。">M040</dd>
                    <dd title="点击选中，再点取消。">M050</dd>
                </dl>
                <ul>
                    <li class="hover">M010-00101</li>
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
                    <li>M020-00105</li>
                    <li>M020-00106</li>
                    <li>M020-00107</li>
                    <li>M030-00101</li>
                    <li>M030-00102</li>
                    <li>M030-00102</li>
                    <li>M030-00104</li>
                    <li>M030-00105</li>
                    <li>M030-00106</li>
                    <li>M030-00107</li>
                    <li>M040-00101</li>
                    <li>M040-00102</li>
                    <li>M040-00102</li>
                    <li>M040-00104</li>
                    <li>M040-00105</li>
                    <li>M040-00106</li>
                    <li>M040-00107</li>
                    <li>M050-00101</li>
                    <li>M050-00102</li>
                    <li>M050-00102</li>
                    <li>M050-00104</li>
                    <li>M050-00105</li>
                    <li>M050-00106</li>
                    <li>M050-00107</li>
                </ul>
            </div>

            <div class="Clear"></div>
            <div class="Page">
                <ul>
                    <li><a href="#">上一页</a></li>
                    <li class="hover"><a href="#">1</a></li>
                    <li><a href="#">2</a></li>
                    <li><a href="#">3</a></li>
                    <li><a href="#">4</a></li>
                    <li>......</li>
                    <li><a href="#">15</a></li>
                    <li><a href="#">下一页</a></li>
                </ul>
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
        <script type="text/javascript">
            $(document).ready(function () {
                $(".Top_Class li").bind("click", function () {
                    $(this).addClass("hover").siblings().removeClass("hover");
                });
            });
        </script>
    </form>
</body>
</html>

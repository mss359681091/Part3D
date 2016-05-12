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
    <script src="/scripts/laypage/laypage.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#lnkclass li ").bind("click", function () {
                $(this).addClass("hover").siblings().removeClass("hover");
                var classid = $(this).children().data("classid");
                $("#Claa_S").data("classid", classid)
                //fnGetList('', '', classid, '', '1', '12');
                binddate($(this).data("count"));
            });
        });

        //获取各个类别组件总数
        function getcount() {

            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/List.aspx/GetCount",
                async: false,
                dataType: 'json',
                success: function (result) {
                    if (result.d.length > 0) {
                        var strs = new Array(); //定义一数组 
                        strs = result.d.split(","); //字符分割 
                        for (i = 0; i < strs.length ; i++) {
                            $("#lnkclass li span").eq(i).text("( " + strs[i] + " )");//赋值
                            $("#lnkclass li ").eq(i).data("count", strs[i]);//赋值
                        }
                        binddate($("#lnkclass li").eq(0).data("count"));
                    }

                }
            });
        }
        function binddate(allcount) {
            //开始执行分页
            var nums = 2; //每页出现的数量
            allcount = (allcount < nums) ? nums : allcount;
            var all = allcount;
            var pages = Math.ceil(all / nums); //得到总页数

            laypage({
                cont: $('#page'), //容器。值支持id名、原生dom对象，jquery对象,
                pages: pages, //总页数
                skip: true, //是否开启跳页
                skin: '#F60',
                groups: 5, //连续显示分页数
                jump: function (obj) {
                    fnGetList('', '', $("#Claa_S").data("classid"), $("#txtkey").val(), obj.curr, nums);
                }
            });
        }
    </script>



    <script type="text/javascript">
        $(document).ready(function () {

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
                <ul id="lnkclass">
                    <li data-count="0" class="hover"><a data-classid="" href="javascript:void(0);">全部</a><span>(0)</span></li>
                    <li data-count="0"><a data-classid="1" href="javascript:void(0);">国标</a><span>(0)</span></li>
                    <li data-count="0"><a data-classid="12" href="javascript:void(0);">3D素材</a><span>(0)</span></li>
                    <li data-count="0"><a data-classid="13" href="javascript:void(0);">3D模型</a><span>(0)</span></li>
                </ul>
            </div>

            <div class="Index_List Container">
                <ul>
                </ul>
            </div>

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
            <div id="page" class="Page">

                <%--   <ul>
                    <li><a href="#">上一页</a></li>
                    <li class="hover"><a href="#">1</a></li>
                    <li><a href="#">2</a></li>
                    <li><a href="#">3</a></li>
                    <li><a href="#">4</a></li>
                    <li>......</li>
                    <li><a href="#">15</a></li>
                    <li><a href="#">下一页</a></li>
                </ul>--%>
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

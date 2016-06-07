<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchCount.aspx.cs" Inherits="Part3D.SearchCount" %>


<%@ Register Src="WUCBottom.ascx" TagName="WUCBottom" TagPrefix="uc1" %>
<%@ Register Src="WUCTop.ascx" TagName="WUCTop" TagPrefix="uc2" %>
<%@ Register Src="WUCLink.ascx" TagName="WUCLink" TagPrefix="uc3" %>
<%@ Register Src="WUCPersonalBanner.ascx" TagName="WUCPersonalBanner" TagPrefix="uc4" %>
<!doctype html>
<html class="no-js">

<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="">
    <meta name="keywords" content="">
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon">
    <title>个人中心-搜索统计</title>
    <link rel="stylesheet" type="text/css" href="/content/Style.css" />
    <link rel="stylesheet" href="/content/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />
    <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/laypage/laypage.js"></script>
    <script src="/controls/laydate-v1.1/laydate/laydate.js"></script>
    <script type="text/javascript">
       

    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="navbarM">
                <uc2:WUCTop ID="WUCTop1" runat="server" />
            </div>
            <div class="Clear"></div>
            <uc4:WUCPersonalBanner ID="WUCPersonalBanner1" runat="server" />

            <div class="User_List Container" style="border: 1px solid #d3cfcf; padding: 15px;">

                <table>
                    <tr>
                        <td>开始日：</td>
                        <td>
                            <li class="laydate-icon" id="start" style="width: 220px; margin-right: 10px; height: 30px; line-height: 30px; padding: 0 10px;"></li>
                        </td>
                        <td>结束日：</td>
                        <td>
                            <li class="laydate-icon" id="end" style="width: 220px; height: 30px; line-height: 30px; padding: 0 10px;"></li>
                        </td>
                    </tr>

                </table>


                <script>
                    var start = {
                        elem: '#start',
                        format: 'YYYY-MM-DD hh:mm:ss',
                        min: laydate.now(), //设定最小日期为当前日期
                        max: '2099-06-16 23:59:59', //最大日期
                        istime: true,
                        istoday: false,
                        choose: function (datas) {
                            end.min = datas; //开始日选好后，重置结束日的最小日期
                            end.start = datas //将结束日的初始值设定为开始日
                        }
                    };
                    var end = {
                        elem: '#end',
                        format: 'YYYY-MM-DD hh:mm:ss',
                        min: laydate.now(),
                        max: '2099-06-16 23:59:59',
                        istime: true,
                        istoday: false,
                        choose: function (datas) {
                            start.max = datas; //结束日选好后，重置开始日的最大日期
                        }
                    };
                    laydate(start);
                    laydate(end);
                </script>
            </div>
            <div class="User_List Container" style="border: 1px solid #d3cfcf; padding: 15px;">
            </div>

            <div class="User_List Container">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr id="trtop" class="bold">
                        <td class="Top center" style="width: 100px;">缩略图</td>
                        <td class="Top center">类别</td>
                        <td class="Top center">名称</td>
                        <td class="Top center">期间浏览量</td>
                        <td class="Top center">最后浏览时间</td>
                    </tr>

                    <tr class="center">
                        <td>
                            <img src="/images/img.png" alt="" /></td>
                        <td>GBT7246</td>
                        <td>波形弹簧垫圈</td>
                        <td>41</td>
                        <td>2015.12.31 12:23</td>

                    </tr>

                    <tr>
                        <td class="Top" colspan="2">
                            <button type="button" class="_button" onclick=""><i class="iconfont">&#xe60a;</i>导出Excel</button>

                        </td>
                        <td class="Top" colspan="5" align="right">
                            <div id="page" class="Page">
                            </div>
                        </td>
                    </tr>
                </table>
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

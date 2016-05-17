<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalResouces.aspx.cs" Inherits="Part3D.PersonalResouces" %>


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
    <title>个人中心-我的资源</title>
    <link rel="stylesheet" type="text/css" href="/content/Style.css" />
    <link rel="stylesheet" href="/content/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />
    <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/PersonalResouces.aspx/GetAllCount",
                dataType: 'json',
                success: function (result) {
                    $(".Index_List li").remove();
                    if (result.d.length > 0) {
                        //开始执行分页
                        var nums = 9; //每页出现的数量
                        var all = result.d;
                        var pages = Math.ceil(all / nums); //得到总页数

                        laypage({
                            cont: $('#page'), //容器。值支持id名、原生dom对象，jquery对象,
                            pages: pages, //总页数
                            skip: true, //是否开启跳页
                            skin: '#F60',
                            groups: 5, //连续显示分页数
                            jump: function (obj) {
                                fnbinddata(obj.curr, nums); //绑定数据
                            }
                        });
                    }
                }
            });


        });
        function fnbinddata(cindex, pagesize) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/PersonalResouces.aspx/GetMyResouces",
                data: "{CurrentIndex:'" + cindex + "',PageSize:'" + pagesize + "'}",
                dataType: 'json',
                success: function (result) {
                    $(".trcenter").remove();
                    if (result.d != null) {
                        var strtr = "";
                        $.each(returnData, function (i, item) {
                            var names = subString(item.Name, 28);
                            strtr += "<tr class='trcenter'>";
                            strtr += "<td align='center'><input type='checkbox'></td>";
                            strtr += "<td><img src='/images/img.png' alt='' /></td>";
                            strtr += "<td>GBT7246 波形弹簧垫圈</td>";
                            strtr += "<td>127</td>";
                            strtr += "<td>41</td>";
                            strtr += "<td>2015.12.31 12:23</td>";
                            strtr += "<td align='center' style='color: #CCC;'><a href='#'>修改</a>|<a href='#' class='del'>删除</a></td>";
                            strtr += "</tr>";
                        });
                        $("#trtop").append(strtr);
                    }
                }
            });
        }

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
            <div class="User_List Container">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr id="trtop">
                        <td class="Top" width="50">全选</td>
                        <td class="Top">缩略图</td>
                        <td class="Top">名称</td>
                        <td class="Top">浏览量</td>
                        <td class="Top">下载量</td>
                        <td class="Top" width="160">创建时间</td>
                        <td class="Top" width="120" align="center">操作</td>
                    </tr>

                    <tr class="trcenter">
                        <td align="center">
                            <input type="checkbox"></td>
                        <td>
                            <img src="/images/img.png" alt="" /></td>
                        <td>GBT7246 波形弹簧垫圈</td>
                        <td>127</td>
                        <td>41</td>
                        <td>2015.12.31 12:23</td>
                        <td align="center" style="color: #CCC;"><a href="#">修改</a>|<a href="#" class="del">删除</a></td>
                    </tr>


                    <tr>
                        <td class="Top" colspan="2">全选
                            <button type="button"><i class="iconfont">&#xe60a;</i>删除</button></td>
                        <td class="Top" colspan="5" align="right">
                            <div class="Page_U"><a href="#"><</a><a href="#" class="hover">1</a><a href="#">2</a><a href="#">3</a><a href="#">4</a><a href="#">5</a><a href="#">></a></div>
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

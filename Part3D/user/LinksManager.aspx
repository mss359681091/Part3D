<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinksManager.aspx.cs" Inherits="Part3D.LinksManager" %>


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
    <title>个人中心-友情链接</title>
    <link rel="stylesheet" type="text/css" href="/content/Style.css" />
    <link rel="stylesheet" href="/content/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />
    <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/laypage/laypage.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            fnload();
        });

        function fnload() {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/LinksManager.aspx/GetAllCount",
                dataType: 'json',
                success: function (result) {

                    $(".trcenter").remove();
                    if (result.d.length > 0) {
                        //开始执行分页
                        var nums = 12; //每页出现的数量
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
        }

        function fnbinddata(cindex, pagesize) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/LinksManager.aspx/GetLinks",
                data: "{CurrentIndex:'" + cindex + "',PageSize:'" + pagesize + "'}",
                dataType: 'json',
                success: function (result) {

                    if (result.d != null) {
                        var strtr = "";
                        $.each(result.d.returnData, function (i, item) {
                            var linkname = item.LinkName;
                            linkname = linkname == "" ? "请选择" : linkname;
                            var linkurl = item.LinkUrl;
                            linkurl = linkurl == "" ? "请选择" : linkurl;
                            strtr += "<tr class='trcenter'>";
                            strtr += "<td align='center'><input data-id='" + item.ID + "' type='checkbox'></td>";
                            strtr += "<td><button data-id='" + item.ID + "' type='button' class='_button' data-event='SetLinkName'>" + linkname + "</button></td>";
                            strtr += "<td><button data-id='" + item.ID + "' type='button' class='_button' data-event='SetLinkUrl'>" + linkurl + "</button></td>";
                            strtr += "<td>" + item.CreateDate + "</td>";
                            strtr += "<td align='center' style='color: #CCC;'> <button type='button' class='_button'  onclick='fnDel(" + item.ID + ")'>删除</button></td>";
                            strtr += "</tr>";
                        });

                        $("#trtop").after(strtr);
                        //修改链接名称
                        $('button[data-event=SetLinkName]').on('click', function () {
                            var $this = $(this);
                            var linkid = $(this).data("id");
                            var flag = $(this).text();
                            flag = flag == "请选择" ? "" : flag;
                            $("#txt_newname").val(flag);
                            var d = dialog({
                                title: '修改链接名称',
                                content: document.getElementById('SetLinkName'),
                                okValue: '确定',
                                ok: function () {
                                    var newname = $("#txt_newname").val();
                                    if (newname.trim() == "") {
                                        $("#txt_newname").focus();
                                        alert("链接名称不能为空！");
                                        return false;
                                    }
                                    fnSetLinksName(linkid, newname);
                                    $this.text(newname);
                                    return false;
                                },
                                cancelValue: '取消',
                                cancel: function () { }
                            });
                            d.showModal();
                        });
                        //修改链接地址
                        $('button[data-event=SetLinkUrl]').on('click', function () {
                            var $this = $(this);
                            var linkid = $(this).data("id");
                            var flag = $(this).text();
                            flag = flag == "请选择" ? "" : flag;
                            $("#txt_newlnk").val(flag);
                            var d = dialog({
                                fixed: true,
                                title: '修改链接名称',
                                content: document.getElementById('SetLinkUrl'),
                                okValue: '确定',
                                ok: function () {
                                    var newlink = $("#txt_newlnk").val();

                                    newlink = newlink == "" ? "请选择" : newlink;

                                    fnSetLinksUrl(linkid, newlink);
                                    $this.text(newlink);
                                    return false;
                                },
                                cancelValue: '取消',
                                cancel: function () { }
                            })
                            d.width(460);
                            d.showModal();
                        });

                    }
                }
            });
        }

        //修改名称
        function fnSetLinksName(Linksid, newname) {
            if (newname.trim().length == 0) {
                $("#txt_newname").focus();
                alert("链接名称不能为空！");
                return;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/LinksManager.aspx/SetLinksName",
                data: "{linksid:'" + Linksid + "',newname:'" + newname + "'}",
                async: false,
                dataType: 'json',
                success: function (result) {
                    if (result.d == "1") {
                        alert("修改成功！");
                    }
                }
            });
        }
        //修改链接
        function fnSetLinksUrl(Linksid, newlink) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/LinksManager.aspx/SetLinksUrl",
                data: "{linksid:'" + Linksid + "',newlink:'" + newlink + "'}",
                async: false,
                dataType: 'json',
                success: function (result) {
                    if (result.d == "1") {
                        alert("修改成功！");
                    }
                }
            });
        }


        function fnchk() {

            if ($("#chkall").prop("checked")) {

                $(".trcenter :checkbox").prop("checked", true);
            }
            else {
                $(".trcenter :checkbox").prop("checked", false);
            }

        }

        function fnchkall() {
            $(".User_List :checkbox").prop("checked", true);
        }

        function fnDel(id) {
            if (confirm("确定要删除选中项吗？")) {
                var linksids = "";
                if (id == "") {
                    $(".trcenter :checked").each(function () {
                        linksids += $(this).data("id") + ",";
                    });
                    if (linksids == "") {
                        alert("请选择删除项！");
                        return;
                    }
                }
                else {
                    linksids = id;
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "/user/LinksManager.aspx/DelLinks",
                    data: "{linksids:'" + linksids + "'}",
                    dataType: 'json',
                    success: function (result) {
                        if (result.d != "-1") {
                            alert("删除成功");
                            fnload();
                        }
                    }
                });
            }
        }

        function fnAddLinks(lnkname, lnkurl) {
            if (lnkname.trim().length == 0) {
                $("#txt_lnkname").focus();
                alert("链接名称不能为空！");
                return;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/LinksManager.aspx/AddLinks",
                data: "{lnkname:'" + lnkname + "',lnkurl:'" + lnkurl + "'}",
                async: false,
                dataType: 'json',
                success: function (result) {
                    if (result.d == "1") {
                        alert("添加成功！");
                        fnload();
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
                        <td class="Top center" width="70">
                            <input id="chkall" onclick="fnchk()" type="checkbox"></td>
                        <td class="Top center">链接名称</td>
                        <td class="Top center">链接地址</td>
                        <td class="Top center" width="160">创建时间</td>
                        <td class="Top center" width="120" align="center">操作</td>
                    </tr>

                    <tr>
                        <td class="Top" colspan="2">
                            <a onclick="fnchkall()" href="javascript:void(0);">全选</a>
                            <button type="button" class="_button" onclick="fnDel('')"><i class="iconfont">&#xe60a;</i>删除</button>
                            <button type="button" class="_button" data-event='AddLink'><i class="iconfont">&#xe60f;</i>添加</button>
                            <script>

                                //添加友情链接
                                $('button[data-event=AddLink]').on('click', function () {

                                    var d = dialog({
                                        title: '添加友情链接',
                                        content: document.getElementById('AddLinkName'),
                                        okValue: '确定',
                                        ok: function () {
                                            var lnkname = $("#txt_lnkname").val();
                                            var lnkurl = $("#txt_lnkurl").val();
                                            fnAddLinks(lnkname, lnkurl);
                                            return false;
                                        },
                                        cancelValue: '取消',
                                        cancel: function () { }
                                    });
                                    d.showModal();
                                });
                            </script>
                        </td>
                        <td class="Top" colspan="5" align="right">

                            <%-- <div class="Page_U"><a href="#"><</a><a href="#" class="hover">1</a><a href="#">2</a><a href="#">3</a><a href="#">4</a><a href="#">5</a><a href="#">></a></div>--%>
                            <div id="page" class="Page">
                            </div>
                        </td>
                    </tr>
                </table>

                <div id="AddLinkName" style="display: none;" class="User_Windows">
                    <ul>
                        <li><span>链接名称：</span><input maxlength="50" id="txt_lnkname" type="text" placeholder="请输入新名称..."></li>
                        <li><span>链接地址：</span><input maxlength="200" id="txt_lnkurl" type="text" placeholder="请输入新链接..."></li>
                    </ul>
                </div>

                <div id="SetLinkName" style="display: none;" class="User_Windows">
                    <ul>
                        <li><span>新名称：</span><input maxlength="50" id="txt_newname" type="text" placeholder="请输入新名称..."></li>
                    </ul>
                </div>

                <div id="SetLinkUrl" style="display: none;" class="User_Windows">
                    <ul>
                        <li><span>新链接：</span><input maxlength="200" id="txt_newlnk" type="text" placeholder="请输入新链接..."></li>
                    </ul>
                </div>

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

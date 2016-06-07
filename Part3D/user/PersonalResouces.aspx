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
    <script src="/scripts/laypage/laypage.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/Index.aspx/GetClassify",
                data: "{paramtype:'1'}",
                dataType: 'json',
                success: function (result) {
                    $("#ulclassify").append(result.d);
                }
            });
            fnload();
        });

        function fnload() {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/PersonalResouces.aspx/GetAllCount",
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
            $(".trcenter").remove();
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/PersonalResouces.aspx/GetMyResouces",
                data: "{CurrentIndex:'" + cindex + "',PageSize:'" + pagesize + "'}",
                dataType: 'json',
                success: function (result) {
                    //$(".center").remove();
                    if (result.d != null) {
                        var strtr = "";
                        $.each(result.d.returnData, function (i, item) {
                            var names = subString(item.Name, 28);
                            strtr += "<tr class='center trcenter'>";
                            strtr += "<td align='center'><input data-id='" + item.ID + "' type='checkbox'></td>";
                            strtr += "<td><a href='/View.aspx?partid=" + item.ID + "' target='_blank' ><img src='" + item.PreviewSmall + "' alt='' /></a></td>";
                            strtr += "<td><button data-classid='" + item.ClassifyID + "' data-id='" + item.ID + "' type='button' class='_button' data-event='Class_L'>" + item.classname + "</button></td>";
                            strtr += "<td><button data-id='" + item.ID + "' type='button' class='_button' data-event='setpartname'>" + names + "</button></li></td>";
                            strtr += "<td>" + item.Accesslog + "</td>";
                            strtr += "<td>" + item.mycount + "</td>";
                            strtr += "<td>" + item.CreateDate1 + "</td>";
                            strtr += "<td align='center' style='color: #CCC;'> <button type='button' class='_button'  onclick='fnDel(" + item.ID + ")'>删除</button></td>";
                            strtr += "</tr>";
                        });
                        $("#trtop").after(strtr);

                        //修改名称
                        $('button[data-event=setpartname]').on('click', function () {
                            var $this = $(this);
                            var partid = $(this).data("id");
                            $("#txt_newname").val($(this).text());
                            var d = dialog({
                                title: '修改名称',
                                content: document.getElementById('setnewname'),
                                okValue: '确定',
                                ok: function () {
                                    var newname = $("#txt_newname").val();
                                    if (newname.trim() == "") {
                                        $("#txt_newname").focus();
                                        alert("名称不能为空！");
                                        return false;
                                    }
                                    fnSetPartname(partid, newname);
                                    $this.text(newname);
                                    return false;
                                },
                                cancelValue: '取消',
                                cancel: function () { }
                            });
                            d.showModal();
                        });
                        //修改分类
                        $('button[data-event=Class_L]').on('click', function () {
                            var $this = $(this);
                            var partid = $(this).data("id");
                            document.getElementById("hidClassifyId").value = $this.data("classid");
                            var d = dialog({
                                fixed: true,
                                title: '选择分类',
                                content: document.getElementById('Class_L'),
                                okValue: '确定',
                                ok: function () {
                                    var classname = fnSetClass(partid);
                                    $this.text(classname);
                                }
                            })
                            d.width(460);
                            d.showModal();
                        });
                    }
                }
            });
        }

        //修改名称
        function fnSetPartname(partid, newname) {

            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/PersonalResouces.aspx/SetPartname",
                data: "{partid:'" + partid + "',newname:'" + newname + "'}",
                async: false,
                dataType: 'json',
                success: function (result) {
                    if (result.d == "1") {
                        alert("修改成功！");
                    }
                }
            });
        }
        //修改分类
        function fnSetClass(partid) {
            var classid = document.getElementById("hidClassifyId").value;
            var classname = "";
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/PersonalResouces.aspx/SetClass",
                data: "{partid:'" + partid + "',classid:'" + classid + "'}",
                async: false,
                dataType: 'json',
                success: function (result) {
                    if (result.d.length > 0) {
                        classname = result.d;//重新赋值
                        alert("修改成功！");
                    }
                }
            });
            return classname;
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
                var partids = "";
                if (id == "") {
                    $(".trcenter :checked").each(function () {
                        partids += $(this).data("id") + ",";
                    });
                    if (partids == "") {
                        alert("请选择删除项！");
                        return;
                    }
                }
                else {
                    partids = id;
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: "/user/PersonalResouces.aspx/DelMyRs",
                    data: "{partids:'" + partids + "'}",
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
                    <tr id="trtop" class="bold">
                        <td class="Top center" width="70">
                            <input id="chkall" onclick="fnchk()" type="checkbox"></td>
                        <td class="Top center">缩略图</td>
                        <td class="Top center">类别</td>
                        <td class="Top center">名称</td>
                        <td class="Top center">浏览量</td>
                        <td class="Top center">下载量</td>
                        <td class="Top center" width="160">创建时间</td>
                        <td class="Top center" width="120">操作</td>
                    </tr>

                    <%--  <tr class="center">
                        <td align="center">
                            <input type="checkbox"></td>
                        <td>
                            <img src="/images/img.png" alt="" /></td>
                        <td>GBT7246 波形弹簧垫圈</td>
                        <td>127</td>
                        <td>41</td>
                        <td>2015.12.31 12:23</td>
                        <td align="center" style="color: #CCC;"><a href="#">修改</a>|<a href="#" class="del">删除</a></td>
                    </tr>--%>


                    <tr>
                        <td class="Top" colspan="2"><a onclick="fnchkall()" href="javascript:void(0);">全选</a>
                            <button type="button" class="_button" onclick="fnDel('')"><i class="iconfont">&#xe60a;</i>删除</button></td>
                        <td class="Top" colspan="5" align="right">

                            <%-- <div class="Page_U"><a href="#"><</a><a href="#" class="hover">1</a><a href="#">2</a><a href="#">3</a><a href="#">4</a><a href="#">5</a><a href="#">></a></div>--%>
                            <div id="page" class="Page">
                            </div>
                        </td>
                    </tr>
                </table>

                <asp:HiddenField ID="hidClassifyId" runat="server" />
                <div id="Class_L" style="display: none;">
                    <ul id="ulclassify">
                    </ul>
                </div>
                <div id="setnewname" style="display: none;" class="User_Windows">
                    <ul>
                        <li><span>新名称：</span><input maxlength="50" id="txt_newname" type="text" placeholder="请输入新名称..."></li>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdManager.aspx.cs" Inherits="Part3D.AdManager" %>


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
    <title>个人中心-广告管理</title>
    <link rel="stylesheet" type="text/css" href="/content/Style.css" />
    <link rel="stylesheet" href="/content/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />
    <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/laypage/laypage.js"></script>
    <script src="/controls/laydate-v1.1/laydate/laydate.js"></script>
    <script src="/scripts/jquery.form.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
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
            });
            fnload();
        });

        function fnload() {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/AdManager.aspx/GetAllCount",
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


        function fnchoose(obj) {
            var $parent = $(obj).parent();
            document.getElementById("hidid").value = $(obj).data("id");
            $(obj).remove();
            $parent.append("<input id='btnfile'  onchange='fnsubmit()' type='file' name='btnfile' />");

        }

        function fnsubmit() {
            $("#Submit1").click();
        }

        //保存图片
        function fnSaveImg() {

            var options = {
                url: "/user/AdManager.aspx",
                success: function () {
                    //$("#td" + ("#hidid").val()).children().remove();
                    //$("#td" + ("#hidid").val()).append($("#hidvalue").val());
                    //$("#fm1").resetForm();
                    //$("#preview").hide();
                    //$("#addpic").show();
                    //alert("上传成功！");
                    //window.location.href = "/user/AdManager.aspx";
                    fnload();
                }
            };
            $("#fm1").ajaxForm(options);
        }



        function fnbinddata(cindex, pagesize) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/AdManager.aspx/GetAdList",
                data: "{CurrentIndex:'" + cindex + "',PageSize:'" + pagesize + "'}",
                dataType: 'json',
                success: function (result) {
                    if (result.d != null) {
                        var strtr = "";
                        $.each(result.d.returnData, function (i, item) {

                            var val_Manufacturer = subString(item.Manufacturer.trim(), 10);
                            val_Manufacturer = (val_Manufacturer.trim() == "" || val_Manufacturer.trim() == "&nbsp;") ? "请选择" : val_Manufacturer.trim();

                            var val_PicturePath = item.PicturePath;
                            if (val_PicturePath.trim() == "") {
                                val_PicturePath = "/images/nopic.png";
                            }


                            var val_ADLink = subString(item.ADLink.trim(), 20);
                            val_ADLink = (val_ADLink.trim() == "" || val_ADLink.trim() == "&nbsp;") ? "请选择" : val_ADLink.trim();
                            var val_ClassName = item.Name;
                            val_ClassName = (val_ClassName.trim() == "" || val_ClassName.trim() == "&nbsp;") ? "请选择" : val_ClassName.trim();

                            var val_ADPosition = subString(item.ADPosition.trim(), 10);
                            val_ADPosition = (val_ADPosition.trim() == "" || val_ADPosition.trim() == "&nbsp;") ? "请选择" : val_ADPosition.trim();
                            var val_ADStartDate = item.ADStartDate;
                            var var_ADEndDate = item.ADEndDate;

                            strtr += "<tr class='trcenter'>";
                            strtr += "<td align='center'><input data-id='" + item.ID + "' type='checkbox'></td>";
                            strtr += "<td><button title='" + item.Manufacturer + "' data-id='" + item.ID + "' type='button' class='_button' data-event='setManufacturer'>" + val_Manufacturer + "</button></td>";
                            strtr += "<td id='td" + item.ID + "' ><a data-id='" + item.ID + "' href='javascript:void(0)' onclick='fnchoose(this)' ><img src='" + val_PicturePath + "' /></a></td>";
                            strtr += "<td><button title='" + item.ADLink + "' data-id='" + item.ID + "' type='button' class='_button' data-event='setADLink'>" + val_ADLink + "</button></td>";
                            strtr += "<td><button data-classid='" + item.ClassifyID + "' data-id='" + item.ID + "' type='button' class='_button' data-event='Class_L'>" + val_ClassName + "</button></td>";
                            strtr += "<td><button title='" + item.ADPosition + "' data-id='" + item.ID + "' type='button' class='_button' data-event='setADPosition'>" + val_ADPosition + "</button></td>";
                            strtr += "<td><button title='" + item.ADStartDate + "' data-id='" + item.ID + "' type='button' class='_button' data-event='setADStartDate'>" + val_ADStartDate + "</button></td>";
                            strtr += "<td><button title='" + item.ADEndDate + "' data-id='" + item.ID + "' type='button' class='_button' data-event='setADEndDate'>" + var_ADEndDate + "</button></td>";

                            strtr += "<td align='center' style='color: #CCC;'> <button type='button' class='_button'  onclick='fnDel(" + item.ID + ")'>删除</button></td>";
                            strtr += "</tr>";
                        });
                        $("#trtop").after(strtr);

                        //修改厂商
                        $('button[data-event=setManufacturer]').on('click', function () {
                            var $this = $(this);
                            var partid = $(this).data("id");
                            var curname = $(this).text();
                            curname = curname == "请选择" ? "" : curname;
                            $("#txt_Manufacturer").val(curname);
                            var d = dialog({
                                title: '修改厂商',
                                content: document.getElementById('setManufacturer'),
                                okValue: '确定',
                                ok: function () {
                                    var newname = $("#txt_Manufacturer").val().trim();
                                    if (newname.length == 0) {
                                        $("#txt_Manufacturer").focus();
                                        alert("厂商不能为空！");
                                        return false;
                                    }
                                    fnSetPartname(partid, newname, "Manufacturer");
                                    $this.text(newname);
                                    return false;
                                },
                                cancelValue: '取消',
                                cancel: function () { }
                            });
                            d.showModal();
                        });

                        //修改链接
                        $('button[data-event=setADLink]').on('click', function () {
                            var $this = $(this);
                            var partid = $(this).data("id");
                            var curname = $(this).text();
                            curname = curname == "请选择" ? "" : curname;
                            $("#txt_ADLink").val(curname);
                            var d = dialog({
                                title: '修改链接',
                                content: document.getElementById('setADLink'),
                                okValue: '确定',
                                ok: function () {
                                    var newname = $("#txt_ADLink").val().trim();
                                    newname = newname == "" ? "" : newname;
                                    fnSetPartname(partid, newname, "ADLink");
                                    $this.text(newname);
                                    return false;
                                },
                                cancelValue: '取消',
                                cancel: function () { }
                            });
                            d.showModal();
                        });

                        //投放位置
                        $('button[data-event=setADPosition]').on('click', function () {
                            var $this = $(this);
                            var partid = $(this).data("id");
                            //$("#sltADPosition").val();
                            var d = dialog({
                                title: '选择投放位置',
                                content: document.getElementById('setADPosition'),
                                okValue: '确定',
                                ok: function () {
                                    var newname = $("#sltADPosition").val().trim();
                                    newname = newname == "请选择" ? "" : newname;
                                    fnSetPartname(partid, newname, "ADPosition");
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
                            var adid = $(this).data("id");
                            document.getElementById("hidClassifyId").value = $this.data("classid");
                            var d = dialog({
                                fixed: true,
                                title: '选择分类',
                                content: document.getElementById('Class_L'),
                                okValue: '确定',
                                ok: function () {
                                    if (adid != "") {
                                        var classname = fnSetClass(adid);
                                        $this.text(classname);
                                    }
                                    else {
                                        var classname = fnGetClassname();
                                        $this.text(classname);
                                    }
                                }
                            })
                            d.width(460);
                            d.showModal();
                        });


                        //开始日期
                        $('button[data-event=setADStartDate]').on('click', function () {
                            var $this = $(this);
                            var partid = $(this).data("id");
                            $("#txt_ADStartDate").val($(this).text());
                            var d = dialog({
                                title: '选择开始日期',
                                content: document.getElementById('setADStartDate'),
                                okValue: '确定',
                                ok: function () {
                                    var newname = $("#txt_ADStartDate").val();
                                    fnSetPartname(partid, newname, "ADStartDate");
                                    $this.text(newname);
                                    return false;
                                },
                                cancelValue: '取消',
                                cancel: function () { }
                            });
                            d.showModal();
                        });
                        //截止日期
                        $('button[data-event=setADEndDate]').on('click', function () {
                            var $this = $(this);
                            var partid = $(this).data("id");
                            $("#txt_ADEndDate").val($(this).text());
                            var d = dialog({
                                title: '选择截止日期',
                                content: document.getElementById('setADEndDate'),
                                okValue: '确定',
                                ok: function () {
                                    var newname = $("#txt_ADEndDate").val();
                                    fnSetPartname(partid, newname, "ADEndDate");
                                    $this.text(newname);
                                    return false;
                                },
                                cancelValue: '取消',
                                cancel: function () { }
                            });
                            d.showModal();
                        });


                    }
                }
            });
        }



        //修改值
        function fnSetPartname(partid, newname, columns) {

            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/AdManager.aspx/SetPartname",
                data: "{partid:'" + partid + "',newname:'" + newname + "',columns:'" + columns + "'}",
                async: false,
                dataType: 'json',
                success: function (result) {
                    if (result.d == "1") {
                        alert("修改成功！");
                    }
                }
            });
        }

        //获取分类值
        function fnGetClassname() {
            var classid = document.getElementById("hidClassifyId").value;
            var classname = "";
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/AdManager.aspx/GetClass",
                data: "{classid:'" + classid + "'}",
                async: false,
                dataType: 'json',
                success: function (result) {
                    if (result.d.length > 0) {
                        classname = result.d;//重新赋值
                        //alert("修改成功！");
                    }
                }
            });
            return classname;
        }

        //修改分类
        function fnSetClass(adid) {
            var classid = document.getElementById("hidClassifyId").value;
            var classname = "";
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/AdManager.aspx/SetClass",
                data: "{adid:'" + adid + "',classid:'" + classid + "'}",
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
                    url: "/user/AdManager.aspx/DelAd",
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

        function fnAddADs(txt_newADLink, txt_newManufacturer, newsltADPosition, txt_newADStartDate, txt_newADEndDate) {
            if (txt_newManufacturer.trim().length == 0) {
                $("#txt_newManufacturer").focus();
                alert("厂家名称不能为空！");
                return;
            }
            var newClassifyID = document.getElementById("hidClassifyId").value;
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/user/AdManager.aspx/AddADs",
                data: "{newADLink:'" + txt_newADLink + "',newManufacturer:'" + txt_newManufacturer + "',newsltADPosition:'" + newsltADPosition + "',newADStartDate:'" + txt_newADStartDate + "',newADEndDate:'" + txt_newADEndDate + "',newClassifyID:'" + newClassifyID + "'}",
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
    <form id="fm1" runat="server" method="post">
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
                        <td class="Top center">厂商</td>
                        <td class="Top center">广告图片</td>
                        <td class="Top center">广告链接</td>
                        <td class="Top center">广告类别</td>
                        <td class="Top center">投放位置</td>
                        <td class="Top center">开始时间</td>
                        <td class="Top center">截止时间</td>
                        <td class="Top center" width="80" align="center">操作</td>
                    </tr>
                    <tr>
                        <td class="Top" colspan="8"><a onclick="fnchkall()" href="javascript:void(0);">全选</a>
                            <button type="button" class="_button" onclick="fnDel('')"><i class="iconfont">&#xe60a;</i>删除</button>
                            <button type="button" class="_button" data-event='AddAd'><i class="iconfont">&#xe60f;</i>添加</button>
                            <script>

                                //添加广告
                                $('button[data-event=AddAd]').on('click', function () {

                                    var d = dialog({
                                        title: '添加广告',
                                        content: document.getElementById('AddAd'),
                                        okValue: '确定',
                                        ok: function () {

                                            var txt_newADLink = $("#txt_newADLink").val();
                                            var txt_newManufacturer = $("#txt_newManufacturer").val();
                                            var newsltADPosition = $("#newsltADPosition").val();
                                            var txt_newADStartDate = $("#txt_newADStartDate").val();
                                            var txt_newADEndDate = $("#txt_newADEndDate").val();
                                            fnAddADs(txt_newADLink, txt_newManufacturer, newsltADPosition, txt_newADStartDate, txt_newADEndDate);

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

                <div id="setADLink" style="display: none;" class="User_Windows">
                    <ul>
                        <li><span>链接名称：</span><input maxlength="50" id="txt_ADLink" type="text" placeholder="请输入链接名称..."></li>
                    </ul>
                </div>

                <div id="setManufacturer" style="display: none;" class="User_Windows">
                    <ul>
                        <li><span>厂商名称：</span><input maxlength="50" id="txt_Manufacturer" type="text" placeholder="请输入厂商名称..."></li>
                    </ul>
                </div>

                <div id="setADPosition" style="display: none;" class="User_Windows">
                    <ul>
                        <li>
                            <span>投放位置：</span>
                            <select id="sltADPosition">
                                <option value="首页">首页</option>
                                <option value="列表页">列表页</option>
                                <option value="下载页">下载页</option>
                            </select>

                        </li>
                    </ul>
                </div>

                <div id="setADStartDate" style="display: none;" class="User_Windows">
                    <ul>
                        <li><span>开始日期：</span><input id="txt_ADStartDate" placeholder="请输入日期" class="laydate-icon" onclick="laydate()"></li>
                    </ul>
                </div>

                <div id="setADEndDate" style="display: none;" class="User_Windows">
                    <ul>
                        <li><span>结束日期：</span><input id="txt_ADEndDate" placeholder="请输入日期" class="laydate-icon" onclick="laydate()"></li>
                    </ul>
                </div>

                <div id="AddAd" style="display: none;" class="User_Windows">
                    <ul>

                        <li><span>厂商名称：</span><input maxlength="200" id="txt_newManufacturer" type="text" placeholder="请输入厂商名称..."></li>
                        <li><span>链接名称：</span><input maxlength="50" id="txt_newADLink" type="text" placeholder="请输入链接名称..."></li>
                        <li>
                            <span>投放位置：</span>
                            <select id="newsltADPosition">
                                <option value="首页">首页</option>
                                <option value="列表页">列表页</option>
                                <option value="下载页">下载页</option>
                            </select>

                        </li>
                        <li><span>开始日期：</span><input id="txt_newADStartDate" placeholder="请输入日期" class="laydate-icon" onclick="laydate()"></li>
                        <li><span>结束日期：</span><input id="txt_newADEndDate" placeholder="请输入日期" class="laydate-icon" onclick="laydate()"></li>
                        <li>
                            <span>选择分类：</span>
                            <button id="btnclass" style="margin-left: 0; margin-top: 5px;" data-classid='' data-id='' type='button' class='_button' data-event='Class_L'>选择分类</button>
                        </li>
                    </ul>
                </div>

                <asp:HiddenField ID="hidid" runat="server" />
                <input id="Submit1" data-id="" onclick="fnSaveImg()" type="submit" value="" style="display: none" />
                <div id="setImg" style="display: none;" class="User_Windows">
                    <ul>
                        <li><span>选择图片：</span>
                            <input id="btnfile" type="file" name="btnfile" />
                        </li>
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

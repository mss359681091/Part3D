﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebUpload.aspx.cs" Inherits="Part3D.WebUpload" %>

<%@ Register Src="/user/WUCBottom.ascx" TagName="WUCBottom" TagPrefix="uc1" %>
<%@ Register Src="/user/WUCTop.ascx" TagName="WUCTop" TagPrefix="uc2" %>
<%@ Register Src="/user/WUCLink.ascx" TagName="WUCLink" TagPrefix="uc3" %>
<%@ Register Src="/user/WUCBanner.ascx" TagName="WUCBanner" TagPrefix="uc4" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>批量上传文件</title>
    <meta charset="utf-8">
    <meta name="description" content="">
    <meta name="keywords" content="">
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon">
    <link rel="stylesheet" type="text/css" href="/content/Style.css" />
    <link rel="stylesheet" href="/content/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />
    <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <%--    <script src="/scripts/jquery.form.js"></script>--%>
    <script src="/scripts/ajaxfileupload.js" type="text/javascript"></script>
    <script src="/scripts/laypage/laypage.js"></script>
    <!--上传控件-->
    <link href="scripts/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="scripts/default.css" rel="stylesheet" type="text/css" />
    <%--  图片预览--%>
    <style type="text/css">
        #imghead {
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image);
        }
    </style>
    <script type="text/javascript">
        function fndw(strid) {
            $("#hidfileid").val(strid);
            document.getElementById('<%=LinkButton1.ClientID %>').click();
        }

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
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#file_upload").uploadify({
                //开启调试
                'debug': false,
                //是否自动上传
                'auto': false,
                'buttonText': '选择文件',
                //flash
                'swf': "scripts/uploadify.swf",
                //文件选择后的容器ID
                'queueID': 'uploadfileQueue',
                'uploader': 'scripts/upload.ashx',
                'width': '75',
                'height': '24',
                'multi': true,
                'fileTypeDesc': '支持的格式：',
                'fileTypeExts': '*.jpg;*.jpge;*.gif;*.png;*.IGS;*.STEP;*.x_t',
                'fileSizeLimit': '10MB',
                'removeTimeout': 1,

                //返回一个错误，选择文件的时候触发
                'onSelectError': function (file, errorCode, errorMsg) {
                    switch (errorCode) {
                        case -100:
                            alert("上传的文件数量已经超出系统限制的" + $('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                            break;
                        case -110:
                            alert("文件 [" + file.name + "] 大小超出系统限制的" + $('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
                            break;
                        case -120:
                            alert("文件 [" + file.name + "] 大小异常！");
                            break;
                        case -130:
                            alert("文件 [" + file.name + "] 类型不正确！");
                            break;
                    }
                },
                //检测FLASH失败调用
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                },
                //上传到服务器，服务器返回相应信息到data里(单个文件触发)
                'onUploadSuccess': function (file, data, response) {
                    //do something
                },
                'onQueueComplete': function (queueData) {
                    fnSaveImg("0");
                }
            });
            $(".swfupload").css("left", "0");
        });
    </script>
    <script type="text/javascript">

        //type=0：普通组件，type=1：组合件
        function fnSaveImg(type) {

            var txtpartname = $("#txtPartname").val();
            var txtclassifyId = $('#<%=this.hidClassifyId.ClientID %>').val();
            $.ajaxFileUpload
            (
                {
                    type: "POST",
                    url: 'WebUpload.aspx', //用于文件上传的服务器端请求地址
                    secureuri: false, //一般设置为false
                    fileElementId: 'btnfile', //文件上传空间的id属性  <input type="file" id="file" name="file" />
                    dataType: 'json', //返回值类型 一般设置为json

                    data: { "partname": txtpartname, "classifyId": txtclassifyId, "paramtype": type }, //附加参数，json格式
                    success: function (data, status)  //服务器成功响应处理函数
                    {
                        if (typeof (data.error) != 'undefined') {
                            if (data.error != '') {
                                alert(data.error);
                            } else {
                                $("#preview").hide();
                                $("#addpic").show();
                                window.location.href = "/user/PersonalResouces.aspx";
                            }
                        }
                    },
                    error: function (data, status, e)//服务器响应失败处理函数
                    {
                        alert(e);
                    }
                }
            )
            return false;
        }
    </script>
</head>
<body>
    <form id="fm1" runat="server" method="post">
        <div>
            <input id="hidfileid" type="hidden" value="" runat="server" />
            <asp:LinkButton ID="LinkButton1" class="lnkdown" Style="display: none" runat="server"
                OnClick="LinkButton1_Click"></asp:LinkButton>
            <div class="navbarM">
                <uc2:WUCTop ID="WUCTop1" runat="server" />
            </div>
            <div class="Clear">
            </div>
            <div class="Top_divM">
                <uc4:WUCBanner ID="WUCBanner1" runat="server" />
            </div>
            <div class="upload_List Container">
                <ul class="Left">
                    <li style="margin-bottom: 2px;"><span class="_span"><b>预览图：</b><i>格式:JPG,PNG,GIF，推荐大小：小于2M</i></span></li>
                    <!--<div><img src="Img/img.png" alt="" /></div>-->
                    <!--己上传完成。显示图片。-->
                    <div id="addpic" class="Up _div">
                        <a style="line-height: 36px;" href="javascript:void(0);" onclick="fnChooseImg();"><i
                            class="iconfont">&#xe608;</i>上传图片</a>
                    </div>
                    <div id="preview" class="Up _div" runat="server" style="display: none">
                        <img id="imghead" class="imghead" width="230" height="160" border="0" src='' onclick="fnChooseImg();" />
                    </div>
                    <input id="btnfile" type="file" name="btnfile" onchange="previewImage(this)" style="display: none" />
                    <!--显示上传按钮-->
                </ul>
                <ul class="Right">
                    <li><span class="_span"><b>名称：</b><i></i></span><input type="text" id="txtPartname"
                        name="txtPartname" maxlength="30" placeholder="请在这里输入名称..." /></li>
                    <li><span class="_span"><b>分类：</b></span><input type="text" placeholder="请选择分类..."
                        data-event="Class_L" class="inp txtClassifyID"><strong class="iconfont" data-event="Class_L">&#xe607;</strong>
                    </li>
                </ul>
                <ul class="Clear">
                </ul>
                <%--         <input id="Submit1" type="submit" value="submit" onclick="fnSaveImg('', '')" style="display: none" />--%>
                <ul class="Clear">
                </ul>
                <div id="divUpload">
                    <ul class="All">
                        <li style="margin-bottom: 3px;"><span class="_span"><b>3D上传文件：</b><i>推荐大小：单个文件10M之内</i></span></li>
                        <div class="_div" style="max-height: 300px; overflow-y: scroll">
                            <button type="button">
                                <div id="file_upload">
                                </div>
                            </button>
                            <%-- <strong>己导入 <font>10</font>个文件</strong>--%>
                            <ul class="Clear">
                            </ul>
                            <dl>
                                <div id="uploadfileQueue" style="padding: 3px;">
                                </div>
                            </dl>
                        </div>
                    </ul>
                    <ul class="Clear">
                    </ul>
                    <button type="button" class="But" onclick="closeLoad()">
                        取消上传</button>
                    <button type="button" style="margin-right: 15px" class="But" onclick="doUplaod()">
                        上传文件</button>
                </div>

                <div id="divOk" style="display: none">
                    <button type="button" style="margin-right: 15px" class="But" onclick="fnSaveImg('1')">
                        确定</button>

                </div>
            </div>
            <!--弹出下载窗口-->
            <script type="text/ecmascript">
                $('[data-event=Class_L]').on('click', function () {
                    var d = dialog({
                        fixed: true,
                        title: '选择分类',
                        content: document.getElementById('Class_L'),
                        okValue: '确定',
                        ok: function () {

                            var txtclassifyId = $('#<%=this.hidClassifyId.ClientID %>').val();
                            //if (txtclassifyId == 2) {
                            //    $("#divUpload").hide();
                            //    $("#divOk").show();
                            //}
                            //else {
                            //    $("#divUpload").show();
                            //    $("#divOk").hide();
                            //}

                        }
                    })
                    d.width(460);
                    d.showModal();
                });
            </script>
            <asp:HiddenField ID="hidClassifyId" runat="server" />
            <div id="Class_L" style="display: none;">
                <ul id="ulclassify">
                </ul>
            </div>
            <div class="Index_Foot">
                <uc3:WUCLink ID="WUCLink1" runat="server" />
            </div>
            <div class="Index_FootB">
                <uc1:WUCBottom ID="WUCBottom1" runat="server" />
            </div>
        </div>
        <!--上传控件-->
        <script src="scripts/swfobject.js" type="text/javascript"></script>
        <script src="scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

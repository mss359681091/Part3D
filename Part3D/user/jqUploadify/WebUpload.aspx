<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebUpload.aspx.cs" Inherits="Part3D.WebUpload" %>

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
    <link rel="stylesheet" href="/content/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />
    <link href="/content/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery.min.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
    <!--上传控件-->
    <link href="scripts/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="scripts/default.css" rel="stylesheet" type="text/css" />

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

            <div class="upload_List Container">
                <ul class="Left">
                    <li style="margin-bottom: 2px;"><span class="_span"><b>预览图：</b><i>格式:JPG,PNG,GIF，大小：小于10M</i></span></li>
                    <!--<div><img src="Img/img.png" alt="" /></div>-->
                    <!--己上传完成。显示图片。-->
                    <div class="Up _div"><a style="line-height: 36px;" href="#"><i class="iconfont">&#xe608;</i>上传图片</a></div>
                    <!--显示上传按钮-->
                </ul>
                <ul class="Right">
                    <li><span class="_span"><b>标题：</b><i>3-30 字</i></span><input type="text" placeholder="请在这里输入信息..." /></li>
                    <li><span class="_span"><b>分类：</b></span><input type="text" placeholder="请选择分类..." class="inp"><strong class="iconfont" data-event="Class_L">&#xe607;</strong></li>
                </ul>
                <ul class="Clear"></ul>
                <ul class="All">
                    <li style="margin-bottom: 3px;"><span class="_span"><b>3D上传文件：</b><i>推荐大小：单个文件10M之内</i></span></li>
                    <div class="_div">
                        <button type="button">
                            <div id="file_upload"></div>
                        </button>
                        <%-- <strong>己导入 <font>10</font>个文件</strong>--%>

                        <ul class="Clear"></ul>

                        <dl>
                            <div id="uploadfileQueue" style="padding: 3px;"></div>


                        </dl>
                    </div>
                </ul>
                <ul class="Clear"></ul>

                <button type="button" class="But" onclick="closeLoad()">取消上传</button>
                <button type="button" style="margin-right: 15px" class="But" onclick="doUplaod()">上传文件</button>
            </div>

            <!--弹出下载窗口-->
            <script type="text/ecmascript">
                $('strong[data-event=Class_L]').on('click', function () {
                    var d = dialog({
                        fixed: true,
                        title: '选择分类',
                        content: document.getElementById('Class_L'),
                        okValue: '确定',
                        ok: function () { }
                    })
                    d.width(460);
                    d.showModal();
                });
            </script>
            <div id="Class_L" style="display: none;">
                <ul>
                    <li>
                        <p><a href="#">国标</a></p>
                        <a href="#">组合件</a><span>/</span><a href="#">连接副</a><span>/</span><a href="#">焊钉</a><span>/</span><a href="#">螺栓</a><span>/</span><a href="#">螺母销</a><span>/</span><a href="#">柳钉</a><span>/</span><a href="#">挡圈</a><span>/</span><a href="#">螺钉</a></li>
                    <li>
                        <p><a href="#">3D素材</a></p>
                    </li>
                    <li style="border: none;">
                        <p><a href="#">3D模型</a></p>
                    </li>
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
        <script src="scripts/jquery-1.7.2.min.js"></script>
        <script src="scripts/swfobject.js" type="text/javascript"></script>
        <script src="scripts/jquery.uploadify.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                var filenames = "";

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
                    'fileTypeExts': '*.jpg;*.jpge;*.gif;*.png',
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
                        filenames += file.name + ",";
                    },
                    'onQueueComplete': function (queueData) {
                        alert('成功上传的文件数: ' + queueData.uploadsSuccessful);
                    }


                });
                $(".swfupload").css("left", "0");
            });

            function doUplaod() {
                $('#file_upload').uploadify('upload', '*');
            }

            function closeLoad() {
                $('#file_upload').uploadify('cancel', '*');
            }
        </script>
    </form>
</body>
</html>

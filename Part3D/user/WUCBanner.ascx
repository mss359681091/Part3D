<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCBanner.ascx.cs" Inherits="Part3D.WUCBanner" %>

<script type="text/javascript">
    $(document).ready(function () {

        var request = {
            QueryString: function (val) {
                var uri = window.location.search;
                var re = new RegExp("" + val + "\=([^\&\?]*)", "ig");
                return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
            }
        }
        var classid = request.QueryString("classid"); //类别id
        var searchkey = request.QueryString("sk"); //类别id

        searchkey = searchkey == null ? "" : searchkey;
        classid = classid == null ? "" : classid;
        searchkey = decodeURI(searchkey);
        var classname = "";
        if ((searchkey.length > 0 && searchkey != null) || classid.length > 0) {

            $("#lnkclass li").removeClass();
            $("#Claa_S").data("classid", classid)
            switch (classid) {
                case "1":
                    classname = "国标";
                    $("#lnkclass li:eq(1)").addClass("hover");

                    break;
                case "12":
                    classname = "3D素材";
                    $("#lnkclass li:eq(2)").addClass("hover");

                    break;
                case "13":
                    classname = "3D模型";
                    $("#lnkclass li:eq(3)").addClass("hover");

                    break;
                default:
                    classname = "全部";
                    $("#lnkclass li:eq(0)").addClass("hover");
                    break;
            }
            $("#Claa_S").prev().prev().text(classname);
            $("#txtkey").val(searchkey);

        }
        getcount('0');//获取组件列表
        $("#Claa_S li").bind("click", function () {
            var $this = $(this).text();
            $("#Claa_S").prev().prev().text($this);
            $("#Claa_S").data("classid", $(this).data("classid"))
            MM_showHideLayers('Claa_S', '', 'hide');
        });
    });
</script>

<script type="text/javascript">
    document.onkeydown = function (event) {

        if (document.activeElement.id == "txtkey") {
            var e = event || window.event || arguments.callee.caller.arguments[0];
            if (e && e.keyCode == 13) {
                fnsearch();
            }
        }

    }
</script>
<div class="Container">
    <div class="Left">
        <img src="/images/Login.png" alt="" />
    </div>
    <div class="SearchM">
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
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCLink.ascx.cs" Inherits="Part3D.WUCLink" %>
<script type="text/javascript">
    $(document).ready(function () {
        var cindex = 1;
        var pagesize = 16;
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/user/LinksManager.aspx/GetLinks",
            data: "{CurrentIndex:'" + cindex + "',PageSize:'" + pagesize + "'}",
            dataType: 'json',
            success: function (result) {
                $("#ullink li").remove();
                if (result.d != null) {
                    var strli = "<span>友情链接</span>";
                    $.each(result.d.returnData, function (i, item) {
                        strli += "<li><a target='_blank' href='" + item.LinkUrl + "'>" + item.LinkName + "</a></li>";
                    });
                    $("#ullink").prepend(strli);
                }
            }
        });

    });

</script>

<div class="Container">
    <ul id="ullink">
    </ul>
    <%--    <img src="" alt="" />--%>
</div>

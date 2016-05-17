<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalDownloadRecord.aspx.cs" Inherits="Part3D.PersonalDownloadRecord" %>

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
            getcount("1");//获取下载记录
        });

        //下载js
        function fndw(strid) {
            $("#hidfileid").val(strid);
            document.getElementById('<%=LinkButton1.ClientID %>').click();
}

    </script>
</head>
<body class="body_Bg">
    <form id="form1" runat="server">
        <div>
            <div class="navbarM">
                <uc2:WUCTop ID="WUCTop1" runat="server" />
            </div>
            <div class="Clear"></div>
            <uc4:WUCPersonalBanner ID="WUCPersonalBanner1" runat="server" />

            <div class="Index_List Container Margin_T30">
                <ul>
                </ul>
            </div>

            <div id="D_Step" style="display: none;">
                <dl class="Class">
                    <dt>快速定位型号：</dt>

                </dl>
                <ul>
                </ul>
            </div>

            <div class="Clear"></div>
            <div id="page" class="Page">
            </div>

            <input id="hidfileid" type="hidden" value="" runat="server" />
            <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none" OnClick="LinkButton1_Click"></asp:LinkButton>

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

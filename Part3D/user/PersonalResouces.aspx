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
    <link rel="stylesheet" href="/contenticonfont/iconfont.css" />
    <link rel="stylesheet" href="/content/ui-dialog.css" />
    <script type="text/javascript" src="/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/scripts/dialog.js"></script>
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
                    <tr>
                        <td class="Top" width="50">全选</td>
                        <td class="Top">缩略图</td>
                        <td class="Top">名称</td>
                        <td class="Top">浏览量</td>
                        <td class="Top">下载量</td>
                        <td class="Top" width="160">创建时间</td>
                        <td class="Top" width="120" align="center">操作</td>
                    </tr>
                    <tr>
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

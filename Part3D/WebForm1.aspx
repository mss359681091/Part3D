<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Part3D.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="scripts/jquery-1.10.2.min.js"></script>
    <script src="scripts/jquery.form.js"></script>
    <script type="text/javascript">
        //保存图片
        function fnSaveImg() {
            var options = {
                url: "/WebForm1.aspx",
                success: function () {

                    //$("#fm1").resetForm();
                    //$("#preview").hide();
                    //$("#addpic").show();
                    //alert("上传成功！");
                    //window.location.href = "/user/PersonalResouces.aspx";

                }
            };
            $("#fm1").ajaxForm(options);
        }

    </script>
</head>
<body>
    <form id="fm1" runat="server" method="post">
        <div>
            <input id="btnfile" name="btnfile" type="file" />
            <a href="javascript:void(0);" onclick="fnSaveImg()">ceshi</a>
            <input id="Submit1" type="submit" value="submit" />
        </div>
    </form>
</body>
</html>

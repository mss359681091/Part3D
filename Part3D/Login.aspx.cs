using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Schema;
using Part3D.models;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.Services;

namespace Part3D
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }

        [WebMethod(Description = "登录", EnableSession = true)]
        public static string DoLogin(string paramUsername, string paramPassword, string paramRemember)
        {
            string returnValue = string.Empty;

            if (paramUsername.Trim() == string.Empty)
            {
                return "-1";//状态-1：用不名空值
            }
            if (paramPassword.Trim() == string.Empty)
            {
                return "-2";//状态-2：密码空值
            }
            returnValue = CommonManager.LoginValidate(paramUsername, paramPassword, paramRemember);//登录验证
            return returnValue;
        }

        [WebMethod(Description = "验证用户名是否存在", EnableSession = true)]
        public static string ChkUsername(string paramUsername)
        {
            string returnValue = string.Empty;

            string strUserID = new sysUserManager().GetParams(new sysUserQuery() { Username = paramUsername }, sysUser.ID);
            if (strUserID.Length > 0)
            {
                returnValue = "-1";//用户名已存在
            }
            return returnValue;
        }

        [WebMethod(Description = "验证邮箱是否存在", EnableSession = true)]
        public static string ChkEmail(string paramEmail)
        {
            string returnValue = string.Empty;

            string strUserID = new sysUserManager().GetParams(new sysUserQuery() { Email = paramEmail }, sysUser.ID);
            if (strUserID.Length > 0)
            {
                returnValue = "-1";//用户名已存在
            }
            return returnValue;
        }

        [WebMethod(Description = "注册", EnableSession = true)]
        public static string FnRegister(string paramUsername, string paramPassword, string paramNickname, string paramEmail, string paramMobile)
        {
            string returnValue = string.Empty;
            if (paramUsername.Trim() == "" || paramPassword.Trim() == "" || paramNickname.Trim() == "")
            {
                return "-1";//信息不能为空
            }
            if (paramUsername != CommonManager.FilterHtmlTarget(paramUsername))
            {
                return "-2";//用户名包含非法字符
            }
            if (paramPassword != CommonManager.FilterHtmlTarget(paramPassword))
            {
                return "-3";//密码包含非法字符
            }
            //判断用户名是否已存在
            if (ChkUsername(paramUsername) == "-1")
            {
                return "-4";//用户名已经存在
            }
            //判断邮箱是否已存在
            if (ChkEmail(paramEmail) == "-1")
            {
                return "-5";//邮箱已经关联
            }

            //正则表达式字符串

            string emailStr = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";
            //邮箱正则表达式对象
            Regex emailReg = new Regex(emailStr);
            if (!emailReg.IsMatch(paramEmail))
            {
                return "-6";//邮箱格式不正确
            }

            paramNickname = CommonManager.FilterHtmlTarget(paramNickname);
            paramEmail = CommonManager.FilterHtmlTarget(paramEmail);
            paramMobile = CommonManager.FilterHtmlTarget(paramMobile);

            Hashtable myHashtable = new Hashtable();
            myHashtable.Add(sysUser.Username, paramUsername);
            myHashtable.Add(sysUser.Password, paramPassword);
            myHashtable.Add(sysUser.Nickname, paramNickname);
            myHashtable.Add(sysUser.Email, paramEmail);
            myHashtable.Add(sysUser.Mobile, paramMobile);

            SQLHelper.ExcuteProc("sp_Register", myHashtable);//执行存储过程注册
            returnValue = "1";//注册成功

            return returnValue;
        }

        [WebMethod(Description = "发送邮件", EnableSession = true)]
        public static string SendEmail(string paramEmail)
        {
            string returnValue = string.Empty;

            returnValue = "1";
            return returnValue;
        }

    }
}

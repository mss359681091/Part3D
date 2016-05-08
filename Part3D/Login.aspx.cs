using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Schema;
using Part3D.models;
using System;
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


    }
}

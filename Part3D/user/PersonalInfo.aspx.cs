using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Schema;
using Part3D.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Part3D
{
    public partial class PersonalInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[sysUser.Username] != null)
                {
                    this.spUsername.InnerText = Session[sysUser.Username].ToString();
                    this.spNickname.InnerText = Session[sysUser.Nickname].ToString();
                    this.spMobile.InnerText = Session[sysUser.Mobile].ToString();
                    this.spEmail.InnerText = Session[sysUser.Email].ToString();
                    this.sp_nickname.InnerText = "昵称：" + Session[sysUser.Nickname].ToString();
                    this.sp_email.InnerText = "邮箱：" + Session[sysUser.Email].ToString();
                    this.sp_mobile.InnerText = "手机号：" + Session[sysUser.Mobile].ToString();
                }
            }
        }

        [WebMethod(Description = "修改手机号", EnableSession = true)]
        public static string SetMobile(string mobile)
        {
            string returnValue = string.Empty;
            sysUserManager mysysUserManager = new sysUserManager();
            returnValue = mysysUserManager.UpdateParams(sysUser.Mobile, mobile, HttpContext.Current.Session[sysUser.ID].ToString());
            if (returnValue == "1")
            {
                HttpContext.Current.Session[sysUser.Mobile] = mobile;
            }
            return returnValue;
        }

        [WebMethod(Description = "修改邮箱", EnableSession = true)]
        public static string SetEmail(string email)
        {
            string returnValue = string.Empty;
            sysUserManager mysysUserManager = new sysUserManager();
            returnValue = mysysUserManager.UpdateParams(sysUser.Email, email, HttpContext.Current.Session[sysUser.ID].ToString());
            if (returnValue == "1")
            {
                HttpContext.Current.Session[sysUser.Email] = email;
            }
            return returnValue;
        }

        [WebMethod(Description = "修改昵称", EnableSession = true)]
        public static string SetNickname(string nickname)
        {
            string returnValue = string.Empty;
            sysUserManager mysysUserManager = new sysUserManager();
            returnValue = mysysUserManager.UpdateParams(sysUser.Nickname, nickname, HttpContext.Current.Session[sysUser.ID].ToString());
            if (returnValue == "1")
            {
                HttpContext.Current.Session[sysUser.Nickname] = nickname;
            }
            return returnValue;
        }

        [WebMethod(Description = "修改密码", EnableSession = true)]
        public static string SetPassword(string oldpassword, string newpassword)
        {
            string returnValue = string.Empty;
            string password = DESEncrypt.Encrypt(oldpassword);//加密
            string strPassword = new sysUserManager().GetParams(new sysUserQuery() { Username = HttpContext.Current.Session[sysUser.Username].ToString() }, sysUser.Password);

            if (password != strPassword)
            {
                return "-1";
            }
            sysUserManager mysysUserManager = new sysUserManager();
            returnValue = mysysUserManager.UpdateParams(sysUser.Password, password, HttpContext.Current.Session[sysUser.ID].ToString());
            if (returnValue == "1")
            {
                HttpContext.Current.Session[sysUser.Password] = password;
            }
            return returnValue;
        }
    }
}
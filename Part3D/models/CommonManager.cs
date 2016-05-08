using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Schema;
using System;
using System.Data;
using System.Web;
using System.Web.Security;

namespace Part3D.models
{
    public class CommonManager
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="paramUsername">用户名</param>
        /// <param name="paramPassword">密码</param>
        /// <param name="paramRemember">是否记住密码</param>
        /// <returns></returns>
        public static string LoginValidate(string paramUsername, string paramPassword, string paramRemember)
        {
            string returnValue = string.Empty;
            DataSet myDataSet = new DataSet();
            sysUserManager mysysUserManager = new sysUserManager();
            sysUserQuery mysysUserQuery = new sysUserQuery();
            mysysUserQuery.Username = paramUsername;
            mysysUserQuery.Password = paramPassword;
            myDataSet = mysysUserManager.DSLogin(mysysUserQuery);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                FnAuthentication(paramUsername, myDataSet.Tables[0].Rows[0]["Rolename"].ToString());//登录授权
                //记录登录状态
                HttpContext.Current.Session[sysUser.ID] = myDataSet.Tables[0].Rows[0][sysUser.ID].ToString();//用户ID
                HttpContext.Current.Session[sysUser.Username] = myDataSet.Tables[0].Rows[0][sysUser.Username].ToString();//用户名
                HttpContext.Current.Session["Rolename"] = myDataSet.Tables[0].Rows[0]["Rolename"].ToString();//角色名
                HttpContext.Current.Session[sysUser.Email] = myDataSet.Tables[0].Rows[0][sysUser.Email].ToString();//邮箱
                HttpContext.Current.Session[sysUser.Mobile] = myDataSet.Tables[0].Rows[0][sysUser.Mobile].ToString();//手机号
                HttpContext.Current.Session[sysUser.Photo] = myDataSet.Tables[0].Rows[0][sysUser.Photo].ToString();//头像
                HttpContext.Current.Session[sysUser.Password] = myDataSet.Tables[0].Rows[0][sysUser.Password].ToString();//密码
                //记住密码
                if (paramRemember == "true")
                {
                    if (HttpContext.Current.Request.Cookies["username"] != null && HttpContext.Current.Request.Cookies["password"] != null)
                    {
                        HttpContext.Current.Response.Cookies["username"].Expires = System.DateTime.Now.AddSeconds(-1);//Expires过期时间
                        HttpContext.Current.Response.Cookies["password"].Expires = System.DateTime.Now.AddSeconds(-1);
                    }
                    HttpCookie hcUserName1 = new HttpCookie("username");
                    hcUserName1.Expires = System.DateTime.Now.AddDays(7);
                    hcUserName1.Value = myDataSet.Tables[0].Rows[0][sysUser.Username].ToString();//用户名
                    HttpCookie hcPassword1 = new HttpCookie("password");
                    hcPassword1.Expires = System.DateTime.Now.AddDays(7);
                    hcPassword1.Value = myDataSet.Tables[0].Rows[0][sysUser.Password].ToString();//密码
                    HttpContext.Current.Response.Cookies.Add(hcUserName1);
                    HttpContext.Current.Response.Cookies.Add(hcPassword1);
                }

                returnValue = "1";//登录成功
            }
            else
            {
                returnValue = "-3";//状态-3：用户名密码错误
            }

            return returnValue;
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="rolename">角色名</param>
        public static void FnAuthentication(string username, string rolename)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), true, rolename, FormsAuthentication.FormsCookiePath);
            string strticket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strticket);
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
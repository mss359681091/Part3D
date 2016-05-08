using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Part3D
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Form["username"] != null)
                {
                    if (Request.Form["username"].ToString() == "admin")
                    {
                        string username = Request.Form["username"].ToString();//用户名
                        string roles = "Administrator";  //从其他地方取得用户角色数据

                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), true, roles, FormsAuthentication.FormsCookiePath);

                        string strticket = FormsAuthentication.Encrypt(ticket);

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strticket);

                        if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                        Response.Cookies.Add(cookie);
                        Response.Write("登录成功");

                    }

                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();//注销授权
            Session.Clear();  //从会话状态集合中移除所有的键和值
            Session.Abandon(); //取消当前会话

        }
    }
}

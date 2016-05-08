using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace _3DPart
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id =(FormsIdentity)HttpContext.Current.User.Identity;

                        FormsAuthenticationTicket ticket = id.Ticket;

                        // 取存储在票据中的用户数据，在这里其实就是用户的角色

                        string userData = ticket.UserData;

                        string[] roles = userData.Split(',');

                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                    }
                }
            }
        }
    }
}
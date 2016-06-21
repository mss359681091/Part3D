using System;
using System.Collections.Generic;
using System.IO;
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
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("Log4Net.config")));

            //定义连接字符串  
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            System.Data.SqlClient.SqlDependency.Start(conStr);//启动监听服务，ps：只需启动一次  
            System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(conStr);//设置通知的数据库连接，ps：只需设置一次  
            string[] str = { "dp_Advertisement", "dp_Classify", "dp_DownRecord", "dp_Links", "dp_ModelFile", "dp_Part", "dp_Standard", "dp_StandardMapping", "dp_Verification", "sysUser", "tLog" };
            System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(conStr, str);//设置通知的数据库连接和表，ps：只需设置一次  

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;

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
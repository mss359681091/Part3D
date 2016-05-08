using _3DPart.DAL.BULayer.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Part3D.user
{
    public partial class Loginout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //删除Cookie
            if (HttpContext.Current.Request.Cookies["username"] != null && HttpContext.Current.Request.Cookies["password"] != null)
            {
                if (Session[sysUser.Username] != null && Session[sysUser.Password] != null)
                {
                    HttpCookie hcUserName1 = new HttpCookie("username");
                    hcUserName1.Expires = System.DateTime.Now.AddDays(-7);
                    hcUserName1.Value = Session[sysUser.Username].ToString();
                    HttpCookie hcPassword1 = new HttpCookie("password");
                    hcPassword1.Expires = System.DateTime.Now.AddDays(-7);
                    hcPassword1.Value = Session[sysUser.Password].ToString();
                    Response.Cookies.Add(hcUserName1);
                    Response.Cookies.Add(hcPassword1);
                }
            }

            FormsAuthentication.SignOut();//注销授权
            Session.Clear();  //从会话状态集合中移除所有的键和值
            Session.Abandon(); //取消当前会话
            Response.Redirect("/Index.aspx");
        }
    }
}
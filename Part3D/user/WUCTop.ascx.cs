using _3DPart.DAL.BULayer.Schema;
using Part3D.models;
using System;

namespace Part3D
{
    public partial class WUCTop : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[sysUser.Username] == null)
                {
                    if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
                    {
                        //用户曾登录 

                        string username = Request.Cookies["username"].Value.ToString();//读取Cookie 
                        string password = Request.Cookies["password"].Value.ToString();//判断Cookie读取出来的用户名和密码是否能正确登录
                        CommonManager.LoginValidate(username, password, "");//登录验证
                    }
                }

                if (Session[sysUser.Username] == null)
                {
                    this.lnkLogin.Visible = true;
                    this.lnkLogined.Visible = false;
                    this.lnkLoginout.Visible = false;
                }
                else
                {
                    this.lnkLogin.Visible = false;
                    this.lnkLogined.Visible = true;
                    this.lnkLoginout.Visible = true;
                    this.lnkLogined.InnerHtml = "<i class='iconfont'>&#xe606;</i>" + Session[sysUser.Nickname].ToString();
                }
            }
        }
    }
}
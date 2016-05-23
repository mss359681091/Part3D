using _3DPart.DAL.BULayer.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Part3D
{
    public partial class WUCPersonalBanner : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[sysUser.Username] != null)
                {
                    if (Session[sysUser.Username].ToString().Trim() == "admin")
                    {
                        this.lnk_ad.Visible = true;//显示广告模块
                        this.lnk_yqlj.Visible = true;//显示友情链接模块
                        this.sp_ad.Visible = true;
                        this.sp_yqlj.Visible = true;
                    }
                }
                if (Session[sysUser.Nickname] != null)
                {
                    this.spnickname.InnerText = Session[sysUser.Nickname].ToString() + "的个人中心";
                    this.imgphoto.Src = Session[sysUser.Photo].ToString();
                }
            }
        }
    }
}
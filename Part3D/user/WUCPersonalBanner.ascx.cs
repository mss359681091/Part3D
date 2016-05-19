﻿using _3DPart.DAL.BULayer.Schema;
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
                if (Session[sysUser.Nickname] != null)
                {
                    this.spnickname.InnerText = Session[sysUser.Nickname].ToString() + "的个人中心";
                    this.imgphoto.Src = Session[sysUser.Photo].ToString();
                }
            }
        }
    }
}
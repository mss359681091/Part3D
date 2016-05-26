using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Part3D
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Files["btnfile"] != null)
            {
                //获取上传的文件的对象  
                HttpPostedFile img = Request.Files["btnfile"];
                string str = img.FileName;
                //HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;
                //if (file.Count > 0)
                //{
                //    string str = file[0].FileName;
                //}
            }
        }
    }
}
using System;

namespace Part3D
{
    public partial class WUCBottom : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.spyear.InnerText = DateTime.Now.Year.ToString();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _3DPart.DAL.BULayer;
using System.Data;
using _3DPart.DAL.BULayer.Schema;

namespace Part3D
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["partid"] != null)
                {
                    dpPartManager mydpPartManager = new dpPartManager();
                    dpPartQuery mydpPartQuery = new dpPartQuery();
                    mydpPartQuery.ID = Request.QueryString["partid"];
                    DataSet myDataSet = mydpPartManager.SearchSingle(mydpPartQuery);
                    if (myDataSet.Tables[0].Rows.Count > 0)
                    {
                        this.hidid.Value = Request.QueryString["partid"];
                        this.hidclassid.Value = myDataSet.Tables[0].Rows[0][dpPart.ClassifyID].ToString();
                        this.btitle.InnerText = myDataSet.Tables[0].Rows[0][dpPart.Name].ToString();
                        this.ausername.InnerText = myDataSet.Tables[0].Rows[0][sysUser.Nickname].ToString();
                        this.acreate.InnerText = myDataSet.Tables[0].Rows[0]["CreateDate"].ToString();
                        this.imgPreview.Src = myDataSet.Tables[0].Rows[0][dpPart.Preview].ToString();
                    }

                }
                else
                {
                    Response.Redirect("/Index.aspx");
                }
            }
        }
    }
}
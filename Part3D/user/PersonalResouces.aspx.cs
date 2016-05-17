using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Schema;
using Part3D.models;
using System.Web.Services;
using System.Data;

namespace Part3D
{
    public partial class PersonalResouces : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(Description = "获取我的资源", EnableSession = true)]
        public dynamic GetMyResouces(string CurrentIndex, string PageSize)
        {
            string status = string.Empty;//状态
            string errmsg = string.Empty;//错误信息
            IList<dpPartData> returnData = null;//返回实体列表

            if (HttpContext.Current.Session[dpPart.UserID] != null)
            {
                dpPartManager my = new dpPartManager();
                dpPartQuery myquery = new dpPartQuery();
                myquery.UserID = HttpContext.Current.Session[dpPart.UserID].ToString();
                myquery.CurrentIndex = Convert.ToInt32(CurrentIndex == "" ? "1" : CurrentIndex);
                myquery.PageSize = Convert.ToInt32(PageSize == "" ? "12" : PageSize);
                DataSet myds = my.SearchMyPart(myquery);
                if (myds.Tables[0].Rows.Count > 0)
                {
                    returnData = CommonManager.GetList<dpPartData>(myds.Tables[0]);//转换实体类list
                }
            }
            return new { status = status, errmsg = errmsg, returnData = returnData };
        }

    }
}
using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Part3D
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 获取组件类别
        /// </summary>
        /// <param name="paramtype">0:导航，1:选中id</param>
        /// <returns></returns>
        [WebMethod(Description = "获取组件类别", EnableSession = true)]
        public static string GetClassify(string paramtype)
        {
            string returnValue = string.Empty;
            paramtype = paramtype == "" ? "0" : "1";
            dpClassifyManager mydpClassifyManager = new dpClassifyManager();
            dpClassifyQuery mydpClassifyQuery = new dpClassifyQuery();
            mydpClassifyQuery.ParentID = "0";
            DataSet myDataSet = mydpClassifyManager.Search(mydpClassifyQuery);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    returnValue += "<li>";
                    if (paramtype == "0")
                    {
                        returnValue += "<p><a target='_blank' href ='/List.aspx?classifyid=" + myDataSet.Tables[0].Rows[i][dpClassify.ID] + "'>" + myDataSet.Tables[0].Rows[i][dpClassify.Name] + "</a></p>";
                    }
                    else
                    {
                        returnValue += "<p><a href ='javascript:void(0);' onclick='fnChooseme(" + myDataSet.Tables[0].Rows[i][dpClassify.ID] + ",this);'>" + myDataSet.Tables[0].Rows[i][dpClassify.Name] + "</a></p>";
                    }

                    string flag = string.Empty;
                    mydpClassifyQuery = new dpClassifyQuery();
                    mydpClassifyQuery.ParentID = myDataSet.Tables[0].Rows[i][dpClassify.ID].ToString();
                    DataSet myDs = mydpClassifyManager.Search(mydpClassifyQuery);
                    if (myDs.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < myDs.Tables[0].Rows.Count; j++)
                        {
                            if (paramtype == "0")
                            {
                                flag += "<a target='_blank' href ='/List.aspx?classifyid=" + myDs.Tables[0].Rows[j][dpClassify.ID] + "'>" + myDs.Tables[0].Rows[j][dpClassify.Name] + "</a><span>/</span>";
                            }
                            else
                            {
                                flag += "<a href ='javascript:void(0);' onclick='fnChooseme(" + myDs.Tables[0].Rows[j][dpClassify.ID] + ",this);'>" + myDs.Tables[0].Rows[j][dpClassify.Name] + "</a><span>/</span>";
                            }
                        }
                    }
                    returnValue += flag;
                    returnValue += "</li>";

                }
            }
            return returnValue;
        }
    }
}
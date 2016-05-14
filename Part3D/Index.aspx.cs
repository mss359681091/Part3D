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
            string returnType = string.Empty;
            paramtype = paramtype == "" ? "0" : paramtype;
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
                        returnValue += "<p><a target='_blank' href ='/List.aspx?classid=" + myDataSet.Tables[0].Rows[i][dpClassify.ID] + "'>" + myDataSet.Tables[0].Rows[i][dpClassify.Name] + "</a></p>";
                    }
                    else if (paramtype == "1")
                    {
                        returnValue += "<p><a href ='javascript:void(0);' onclick='fnChooseme(" + myDataSet.Tables[0].Rows[i][dpClassify.ID] + ",this);'>" + myDataSet.Tables[0].Rows[i][dpClassify.Name] + "</a></p>";
                    }
                    else
                    {

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
                                flag += "<a target='_blank' href ='/List.aspx?classid=" + myDs.Tables[0].Rows[j][dpClassify.ID] + "'>" + myDs.Tables[0].Rows[j][dpClassify.Name] + "</a><span>/</span>";
                            }
                            else if (paramtype == "1")
                            {
                                flag += "<a href ='javascript:void(0);' onclick='fnChooseme(" + myDs.Tables[0].Rows[j][dpClassify.ID] + ",this);'>" + myDs.Tables[0].Rows[j][dpClassify.Name] + "</a><span>/</span>";
                            }
                            else
                            {
                                returnType += "<li><a target='_blank' href='/List.aspx?classid=" + myDs.Tables[0].Rows[j][dpClassify.ID] + "'><i class='Ico" + (Convert.ToInt32(j) + 1) + "'></i>" + myDs.Tables[0].Rows[j][dpClassify.Name] + "</a></li>";
                            }
                        }
                    }
                    returnValue += flag;
                    returnValue += "</li>";
                }
                if (paramtype == "2")
                {
                    returnType += "<li><a target='_blank' href='/List.aspx'><i class='Ico11'></i>更多标准</a></li>";
                    returnValue = returnType;
                }
            }
            return returnValue;
        }


        /// <summary>
        /// 获取标准列表
        /// </summary>
        /// <param name="partid"></param>
        /// <returns></returns>
        [WebMethod(Description = "获取标准列表", EnableSession = true)]
        public static string GetStandard(string partid, string format)
        {
            string returnValue = string.Empty;
            dpStandardMappingManager mydpStandardMappingManager = new dpStandardMappingManager();
            dpStandardMappingQuery mydpStandardMappingQuery = new dpStandardMappingQuery();
            mydpStandardMappingQuery.PartID = partid;
            DataSet myDataSet = mydpStandardMappingManager.SearchBind(mydpStandardMappingQuery);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    returnValue += "<dd data-id='" + myDataSet.Tables[0].Rows[i][dpStandard.ID] + "' id='dd" + myDataSet.Tables[0].Rows[i][dpStandard.ID] + "' title='标准'>" + myDataSet.Tables[0].Rows[i][dpStandard.Name] + "</dd>";
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 获取模型文件列表
        /// </summary>
        /// <param name="partid"></param>
        /// <returns></returns>
        [WebMethod(Description = "获取模型文件列表", EnableSession = true)]
        public static string GetModelfile(string partid, string standardname, string format)
        {
            string returnValue = string.Empty;
            dpModelFileManager mydpModelFileManager = new dpModelFileManager();
            dpModelFileQuery mydpModelFileQuery = new dpModelFileQuery();
            mydpModelFileQuery.PartID = partid;
            mydpModelFileQuery.Name = standardname;
            mydpModelFileQuery.Format = format;
            DataSet myDataSet = mydpModelFileManager.Search(mydpModelFileQuery);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    returnValue += "<li>" + myDataSet.Tables[0].Rows[i][dpModelFile.Name] + "</li>";
                }
            }
            return returnValue;
        }
    }
}
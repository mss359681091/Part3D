using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Schema;
using log4net;
using Part3D.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;

namespace Part3D
{
    public partial class DownloadCount : System.Web.UI.Page
    {
        private static readonly ILog m_log = LogHelper.GetInstance(); //LogManager.GetLogger(typeof(TEST));

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 获取我的组件
        /// </summary>
        /// <param name="CurrentIndex">页数</param>
        /// <param name="PageSize">每页显示条数</param>
        /// <returns></returns>
        [WebMethod(Description = "获取我的组件", EnableSession = true)]
        public static dynamic GetMyResouces(string CurrentIndex, string PageSize)
        {
            string status = string.Empty;//状态
            string errmsg = string.Empty;//错误信息
            IList<dpPartData> returnData = null;//返回实体列表
            try
            {
                if (HttpContext.Current.Session[sysUser.ID] != null)
                {
                    dpPartManager my = new dpPartManager();
                    dpPartQuery myquery = new dpPartQuery();
                    myquery.UserID = HttpContext.Current.Session[sysUser.ID].ToString();
                    myquery.CurrentIndex = Convert.ToInt32(CurrentIndex == "" ? "1" : CurrentIndex);
                    myquery.PageSize = Convert.ToInt32(PageSize == "" ? "12" : PageSize);
                    DataSet myds = my.SearchMyPart(myquery);
                    if (myds.Tables[0].Rows.Count > 0)
                    {
                        returnData = CommonManager.GetList<dpPartData>(myds.Tables[0]);//转换实体类list
                    }
                }
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return new { status = status, errmsg = errmsg, returnData = returnData };
        }

        /// <summary>
        /// 获取我的组件总数
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "获取我的组件总数", EnableSession = true)]
        public static string GetAllCount()
        {
            string returnValue = string.Empty;
            try
            {
                if (HttpContext.Current.Session[sysUser.ID] != null)
                {
                    dpPartManager my = new dpPartManager();
                    dpPartQuery myquery = new dpPartQuery();
                    myquery.UserID = HttpContext.Current.Session[sysUser.ID].ToString();
                    DataSet myds = my.SearchAllCount(myquery);
                    if (myds.Tables[0].Rows.Count > 0)
                    {
                        returnValue = myds.Tables[0].Rows[0]["countall"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }

        /// <summary>
        /// 删除我的组件
        /// </summary>
        /// <param name="ids">组件id列表，逗号分隔</param>
        /// <returns></returns>
        [WebMethod(Description = "删除我的组件", EnableSession = true)]
        public static string DelMyRs(string partids)
        {
            string returnValue = string.Empty;

            try
            {

                if (partids.Length > 0)
                {
                    if (partids.Contains(","))
                    {
                        partids = partids.Substring(0, partids.Length - 1);
                    }
                    string[] str = partids.Split(',');

                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i].ToString().Trim().Length > 0)
                        {
                            //删除下载记录
                            dpDownRecordManager mydpDownRecordManager = new dpDownRecordManager();
                            dpDownRecordQuery mydpDownRecordQuery = new dpDownRecordQuery();
                            mydpDownRecordQuery.PartID = str[i].ToString().Trim();
                            mydpDownRecordManager.DeleteParams(mydpDownRecordQuery);

                            //删除组件标准匹配
                            dpStandardMappingManager mydpStandardMappingManager = new dpStandardMappingManager();
                            dpStandardMappingQuery mydpStandardMappingQuery = new dpStandardMappingQuery();
                            mydpStandardMappingQuery.PartID = str[i].ToString().Trim();
                            mydpStandardMappingManager.DeleteParams(mydpStandardMappingQuery);

                            DelModelFile(str[i].ToString().Trim());//删除组件文件

                            //删除组件
                            DelPreview(str[i].ToString().Trim());//删除组件缩略图
                            dpPartManager mydpPartManager = new dpPartManager();
                            dpPartQuery mydpPartQuery = new dpPartQuery();
                            mydpPartQuery.UserID = HttpContext.Current.Session[sysUser.ID].ToString();
                            mydpPartQuery.ID = str[i].ToString().Trim();
                            returnValue = mydpPartManager.DeleteParams(mydpPartQuery);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }

        /// <summary>
        /// 删除组件预览图和缩略图
        /// </summary>
        /// <param name="partID">组件ID</param>
        public static void DelPreview(string partID)
        {
            if (partID.Trim().Length > 0)
            {
                dpPartManager mydpPartManager = new dpPartManager();
                dpPartQuery mydpPartQuery = new dpPartQuery();
                mydpPartQuery.ID = partID;
                DataSet myDataSet = mydpPartManager.Search(mydpPartQuery);
                if (myDataSet.Tables[0].Rows.Count > 0)
                {
                    string strPreview = myDataSet.Tables[0].Rows[0][dpPart.Preview].ToString();
                    string strPreviewSmall = myDataSet.Tables[0].Rows[0][dpPart.PreviewSmall].ToString();
                    if (File.Exists(HttpContext.Current.Server.MapPath(strPreview)))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath(strPreview));//删除文件
                    }
                    if (File.Exists(HttpContext.Current.Server.MapPath(strPreviewSmall)))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath(strPreviewSmall));//删除文件
                    }
                }
            }
        }

        /// <summary>
        ///删除组件文件
        /// </summary>
        /// <param name="partID">组件ID</param>
        public static void DelModelFile(string partID)
        {
            if (partID.Trim().Length > 0)
            {
                dpModelFileManager mydpModelFileManager = new dpModelFileManager();
                dpModelFileQuery mydpModelFileQuery = new dpModelFileQuery();
                mydpModelFileQuery.PartID = partID;
                DataSet myDataSet = mydpModelFileManager.Search(mydpModelFileQuery);
                if (myDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                    {
                        string strLocation = myDataSet.Tables[0].Rows[i][dpModelFile.Location].ToString();
                        if (File.Exists(HttpContext.Current.Server.MapPath(strLocation)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(strLocation));//删除文件
                        }
                    }
                }
                //删除组件文件
                mydpModelFileManager.DeleteParams(mydpModelFileQuery);
            }
        }

        /// <summary>
        /// 修改组件名称
        /// </summary>
        /// <param name="partid">组件id</param>
        /// <param name="newname">新名称</param>
        /// <returns></returns>
        [WebMethod(Description = "修改组件名称", EnableSession = true)]
        public static string SetPartname(string partid, string newname)
        {
            string returnValue = string.Empty;
            try
            {
                dpPartManager mydpPartManager = new dpPartManager();
                mydpPartManager.UpdateParams(dpPart.Name, newname, partid);
                returnValue = "1";
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }

        /// <summary>
        /// 修改组件类别
        /// </summary>
        /// <param name="partid">组件id</param>
        /// <param name="classid">组件类别</param>
        /// <returns></returns>
        [WebMethod(Description = "修改组件类别", EnableSession = true)]
        public static string SetClass(string partid, string classid)
        {
            string returnValue = string.Empty;
            try
            {
                dpPartManager mydpPartManager = new dpPartManager();
                mydpPartManager.UpdateParams(dpPart.ClassifyID, classid, partid);
                returnValue = new dpClassifyManager().GetParams(new dpClassifyQuery() { ID = classid }, dpClassify.Name);
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }
    }
}
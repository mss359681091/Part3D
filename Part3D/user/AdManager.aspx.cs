using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Data;
using _3DPart.DAL.BULayer.Schema;
using log4net;
using Part3D.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;

namespace Part3D
{
    public partial class AdManager : System.Web.UI.Page
    {
        private static readonly ILog m_log = LogHelper.GetInstance(); //LogManager.GetLogger(typeof(TEST));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Files["btnfile"] != null)
            {
                var adid = this.hidid.Value;
                UpdateAdImg(adid);
            }
        }


        /// <summary>
        /// 修改广告图片
        /// </summary>
        /// <param name="adid">广告ID</param>
        private void UpdateAdImg(string adid)
        {
            if (adid.Trim().Length > 0)
            {
                try
                {
                    HttpPostedFile img = Request.Files["btnfile"];
                    string uploadPath = HttpContext.Current.Server.MapPath(@"/user/jqUploadify/uploads/");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    string filePath = img.FileName;//文件路径
                    string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);//文件名称
                    string fileExtname = System.IO.Path.GetExtension(filePath).ToLower();
                    string ran = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(100, 999);
                    string temp = uploadPath + ran;
                    string newfilename = temp + fileExtname; //新文件名称

                    img.SaveAs(newfilename);//上传文件

                    dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                    dpAdvertisementQuery mydpAdvertisementQuery = new dpAdvertisementQuery();
                    mydpAdvertisementQuery.ID = adid;

                    //删除广告图片
                    string strimg = mydpAdvertisementManager.GetParams(mydpAdvertisementQuery, dpAdvertisement.PicturePath);
                    if (File.Exists(HttpContext.Current.Server.MapPath(strimg)))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath(strimg));//删除文件
                    }
                    //更新广告信息
                    mydpAdvertisementManager.UpdateParams(dpAdvertisement.PicturePath, @"/user/jqUploadify/uploads/" + ran + fileExtname, adid);

                }
                catch (Exception ex)
                {
                    m_log.Error(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="CurrentIndex">页数</param>
        /// <param name="PageSize">每页显示条数</param>
        /// <returns></returns>
        [WebMethod(Description = "获取广告列表", EnableSession = true)]
        public static dynamic GetAdList(string CurrentIndex, string PageSize)
        {
            string status = string.Empty;//状态
            string errmsg = string.Empty;//错误信息
            IList<dpAdvertisementData> returnData = null;//返回实体列表
            try
            {
                if (HttpContext.Current.Session[sysUser.ID] != null)
                {
                    dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                    dpAdvertisementQuery mydpAdvertisementQuery = new dpAdvertisementQuery();
                    mydpAdvertisementQuery.CurrentIndex = Convert.ToInt32(CurrentIndex == "" ? "1" : CurrentIndex);
                    mydpAdvertisementQuery.PageSize = Convert.ToInt32(PageSize == "" ? "12" : PageSize);
                    mydpAdvertisementQuery.ClassifyID = "";
                    DataSet myds = mydpAdvertisementManager.SearchPaging(mydpAdvertisementQuery);
                    if (myds.Tables[0].Rows.Count > 0)
                    {
                        returnData = CommonManager.GetList<dpAdvertisementData>(myds.Tables[0]);//转换实体类list
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
        /// 获取广告总数
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "获取广告总数", EnableSession = true)]
        public static string GetAllCount()
        {
            string returnValue = string.Empty;
            try
            {
                dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                dpAdvertisementQuery mydpAdvertisementQuery = new dpAdvertisementQuery();
                DataSet myds = mydpAdvertisementManager.SearchAllCount(mydpAdvertisementQuery);
                if (myds.Tables[0].Rows.Count > 0)
                {
                    returnValue = myds.Tables[0].Rows[0]["countall"].ToString();
                }
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="ids">广告id列表，逗号分隔</param>
        /// <returns></returns>
        [WebMethod(Description = "删除广告", EnableSession = true)]
        public static string DelAd(string partids)
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
                            //删除广告
                            dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                            dpAdvertisementQuery mydpAdvertisementQuery = new dpAdvertisementQuery();
                            mydpAdvertisementQuery.ID = str[i].ToString().Trim();
                            //删除广告图片
                            string strimg = mydpAdvertisementManager.GetParams(mydpAdvertisementQuery, dpAdvertisement.PicturePath);
                            if (File.Exists(HttpContext.Current.Server.MapPath(strimg)))
                            {
                                File.Delete(HttpContext.Current.Server.MapPath(strimg));//删除文件
                            }
                            //删除广告信息
                            mydpAdvertisementManager.DeleteParams(mydpAdvertisementQuery);
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
        /// 修改广告
        /// </summary>
        /// <param name="partid">广告id</param>
        /// <param name="newname">新值</param>
        /// <returns></returns>
        [WebMethod(Description = "修改广告厂商", EnableSession = true)]
        public static string SetPartname(string partid, string newname, string columns)
        {
            string returnValue = string.Empty;
            try
            {
                dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                mydpAdvertisementManager.UpdateParams(columns, newname, partid);
                returnValue = "1";
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }

        /// <summary>
        /// 修改广告类别
        /// </summary>
        /// <param name="partid">广告id</param>
        /// <param name="classid">广告类别</param>
        /// <returns></returns>
        [WebMethod(Description = "修改广告类别", EnableSession = true)]
        public static string SetClass(string adid, string classid)
        {
            string returnValue = string.Empty;
            try
            {
                dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                mydpAdvertisementManager.UpdateParams(dpAdvertisement.ClassifyID, classid, adid);
                returnValue = new dpClassifyManager().GetParams(new dpClassifyQuery() { ID = classid }, dpClassify.Name);
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }

        /// <summary>
        /// 获取广告类别
        /// </summary>
        /// <param name="classid">分类id</param>
        /// <returns></returns>
        [WebMethod(Description = "获取广告类别", EnableSession = true)]
        public static string GetClass(string classid)
        {
            string returnValue = string.Empty;
            try
            {
                returnValue = new dpClassifyManager().GetParams(new dpClassifyQuery() { ID = classid }, dpClassify.Name);
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }

        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="newADLink">广告链接</param>
        /// <param name="newManufacturer">厂商名称</param>
        /// <param name="newsltADPosition">投放位置</param>
        /// <param name="newADStartDate">开始日期</param>
        /// <param name="newADEndDate">结束日期</param>
        /// <returns></returns>
        [WebMethod(Description = "添加广告", EnableSession = true)]
        public static string AddADs(string newADLink, string newManufacturer, string newsltADPosition, string newADStartDate, string newADEndDate, string newClassifyID)
        {
            string returnValue = string.Empty;
            newADStartDate = newADStartDate == "" ? DateTime.Now.ToString() : newADStartDate;
            newADEndDate = newADEndDate == "" ? DateTime.Now.AddDays(10).ToString() : newADEndDate;

            Hashtable myHashtable = new Hashtable();
            myHashtable.Add(dpAdvertisement.ADLink, newADLink);
            myHashtable.Add(dpAdvertisement.Manufacturer, newManufacturer);
            myHashtable.Add(dpAdvertisement.ADPosition, newsltADPosition);
            myHashtable.Add(dpAdvertisement.ADStartDate, newADStartDate);
            myHashtable.Add(dpAdvertisement.ADEndDate, newADEndDate);
            myHashtable.Add(dpAdvertisement.ClassifyID, newClassifyID);
            myHashtable.Add(dpAdvertisement.PicturePath, "");
            myHashtable.Add(dpAdvertisement.UserID, HttpContext.Current.Session[sysUser.ID].ToString());
            SQLHelper.ExcuteProc("sp_dpAdvertisement", myHashtable);//执行存储过程注册
            returnValue = "1";//添加成功
            return returnValue;
        }

    }


}
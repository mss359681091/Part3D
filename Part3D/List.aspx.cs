using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Schema;
using log4net;
using Part3D.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Services;

namespace Part3D
{
    public partial class List : System.Web.UI.Page
    {
        private static readonly ILog m_log = LogHelper.GetInstance(); //LogManager.GetLogger(typeof(TEST));

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadAd();
            }
        }

        private void LoadAd()
        {

            //缓存
            DataSet myDataSet = new DataSet();

            string CacheKey = "SearchAd_列表页";
            object objModel = CookiesHelper.GetCache(CacheKey);//从缓存中获取
            if (objModel == null)//缓存里没有
            {
                dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                dpAdvertisementQuery mydpAdvertisementQuery = new dpAdvertisementQuery();
                mydpAdvertisementQuery.ADPosition = "列表页";
                myDataSet = mydpAdvertisementManager.Search(mydpAdvertisementQuery);
                objModel = myDataSet;//把数据存入缓存
                if (objModel != null)
                {
                    //依赖数据库codematic中的P_Product表变化 来更新缓存
                    System.Web.Caching.SqlCacheDependency dep = new System.Web.Caching.SqlCacheDependency(ConfigurationManager.AppSettings["DataBase"].ToString(), dpAdvertisement.TABLENAME);
                    CookiesHelper.SetCache(CacheKey, objModel, dep);//写入缓存
                }
            }
            else
            {
                myDataSet = (DataSet)objModel;
            }

            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                this.lnkad.HRef = myDataSet.Tables[0].Rows[0][dpAdvertisement.ADLink].ToString();
                this.imgad.Src = myDataSet.Tables[0].Rows[0][dpAdvertisement.PicturePath].ToString();
                this.lnkad.Visible = true;
            }
            else
            {
                this.lnkad.Visible = false;
            }
        }

        [WebMethod(Description = "获取组件列表", EnableSession = true)]
        public static dynamic GetPartList(string ParentID, string UserID, string ClassifyID, string Name, string CurrentIndex, string PageSize, string type)
        {
            string status = string.Empty;//状态
            string errmsg = string.Empty;//错误信息
            IList<dpPartData> returnData = null;//返回实体列表
            try
            {
                if (type == "0")
                {

                    DataSet myDataSet = new DataSet();
                    //缓存
                    string CacheKey = "GetPartList_" + ParentID + "_" + UserID + "_" + ClassifyID + "_" + Name + "_" + CurrentIndex + "_" + PageSize + "_" + type;
                    object objModel = CookiesHelper.GetCache(CacheKey);//从缓存中获取
                    if (objModel == null)//缓存里没有
                    {
                        myDataSet = GetDataSetPart(ParentID, UserID, ClassifyID, Name, CurrentIndex, PageSize);
                        objModel = myDataSet;//把数据存入缓存
                        if (objModel != null)
                        {
                            //依赖数据库codematic中的P_Product表变化 来更新缓存
                            System.Web.Caching.SqlCacheDependency dep = new System.Web.Caching.SqlCacheDependency(ConfigurationManager.AppSettings["DataBase"].ToString(), "dp_Part");
                            CookiesHelper.SetCache(CacheKey, objModel, dep);//写入缓存
                        }
                    }
                    else
                    {
                        myDataSet = (DataSet)objModel;
                    }

                    if (myDataSet.Tables[0].Rows.Count > 0)
                    {
                        returnData = CommonManager.GetList<dpPartData>(myDataSet.Tables[0]);//转换实体类list
                    }
                }
                else
                {
                    //缓存
                    DataSet myDataSet = new DataSet();

                    string CacheKey = "GetDataSetDownRecord_" + CurrentIndex + "_" + PageSize;
                    object objModel = CookiesHelper.GetCache(CacheKey);//从缓存中获取
                    if (objModel == null)//缓存里没有
                    {
                        myDataSet = GetDataSetDownRecord(CurrentIndex, PageSize);
                        objModel = myDataSet;//把数据存入缓存
                        if (objModel != null)
                        {
                            //依赖数据库codematic中的P_Product表变化 来更新缓存
                            System.Web.Caching.SqlCacheDependency dep = new System.Web.Caching.SqlCacheDependency(ConfigurationManager.AppSettings["DataBase"].ToString(), "dp_DownRecord");
                            CookiesHelper.SetCache(CacheKey, objModel, dep);//写入缓存
                        }
                    }
                    else
                    {
                        myDataSet = (DataSet)objModel;
                    }
                    if (myDataSet.Tables[0].Rows.Count > 0)
                    {
                        returnData = CommonManager.GetList<dpPartData>(myDataSet.Tables[0]);//转换实体类list
                    }
                }

            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return new { status = status, errmsg = errmsg, returnData = returnData };
        }

        private static DataSet GetDataSetDownRecord(string CurrentIndex, string PageSize)
        {
            dpDownRecordManager my = new dpDownRecordManager();
            dpDownRecordQuery myquery = new dpDownRecordQuery();
            myquery.UserID = HttpContext.Current.Session[sysUser.ID].ToString();
            myquery.CurrentIndex = Convert.ToInt32(CurrentIndex == "" ? "1" : CurrentIndex);
            myquery.PageSize = Convert.ToInt32(PageSize == "" ? "12" : PageSize);
            myquery.RecordType = "1";
            DataSet myDataSet = my.SearchPaging(myquery);
            return myDataSet;
        }

        private static DataSet GetDataSetPart(string ParentID, string UserID, string ClassifyID, string Name, string CurrentIndex, string PageSize)
        {
            dpPartManager mydp_Part = new dpPartManager();
            dpPartQuery mydpPartQuery = new dpPartQuery();
            mydpPartQuery.ParentID = ParentID;
            mydpPartQuery.UserID = UserID;
            mydpPartQuery.ClassifyID = ClassifyID;
            mydpPartQuery.Name = Name;
            mydpPartQuery.CurrentIndex = Convert.ToInt32(CurrentIndex == "" ? "1" : CurrentIndex);
            mydpPartQuery.PageSize = Convert.ToInt32(PageSize == "" ? "12" : PageSize);
            DataSet myDataSet = mydp_Part.SearchPaging(mydpPartQuery);
            return myDataSet;
        }

        [WebMethod(Description = "获取各个分类总数", EnableSession = true)]
        public static string GetCount()
        {
            string reutrnValue = string.Empty;
            try
            {
                dpPartManager mydpPartManager = new dpPartManager();
                dpPartQuery mydpPartQuery = new dpPartQuery();
                DataSet myDataSet = mydpPartManager.SearchCount(mydpPartQuery);
                if (myDataSet.Tables[0].Rows.Count > 0)
                {
                    reutrnValue = myDataSet.Tables[0].Rows[0]["countall"].ToString() + ",";
                    reutrnValue += myDataSet.Tables[0].Rows[0]["count1"].ToString() + ",";
                    reutrnValue += myDataSet.Tables[0].Rows[0]["count2"].ToString() + ",";
                    reutrnValue += myDataSet.Tables[0].Rows[0]["count3"].ToString();
                }
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }

            return reutrnValue;
        }

        [WebMethod(Description = "获取总数", EnableSession = true)]
        public static string GetAllCount(string classid, string partname, string type)
        {
            string reutrnValue = string.Empty;

            try
            {
                if (type == "0")
                {
                    //缓存
                    DataSet myDataSet = new DataSet();

                    string CacheKey = "SearchAllCount_" + classid + "_" + partname + "_" + type;
                    object objModel = CookiesHelper.GetCache(CacheKey);//从缓存中获取
                    if (objModel == null)//缓存里没有
                    {
                        dpPartManager mydpPartManager = new dpPartManager();
                        dpPartQuery mydpPartQuery = new dpPartQuery();
                        mydpPartQuery.ClassifyID = classid;
                        mydpPartQuery.Name = partname;
                        myDataSet = mydpPartManager.SearchAllCount(mydpPartQuery);

                        objModel = myDataSet;//把数据存入缓存
                        if (objModel != null)
                        {
                            //依赖数据库codematic中的P_Product表变化 来更新缓存
                            System.Web.Caching.SqlCacheDependency dep = new System.Web.Caching.SqlCacheDependency(ConfigurationManager.AppSettings["DataBase"].ToString(), dpPart.TABLENAME);
                            CookiesHelper.SetCache(CacheKey, objModel, dep);//写入缓存
                        }
                    }
                    else
                    {
                        myDataSet = (DataSet)objModel;
                    }
                    reutrnValue = myDataSet.Tables[0].Rows[0]["countall"].ToString();

                    //dpPartManager mydpPartManager = new dpPartManager();
                    //dpPartQuery mydpPartQuery = new dpPartQuery();
                    //mydpPartQuery.ClassifyID = classid;
                    //mydpPartQuery.Name = partname;
                    //DataSet myDataSet = mydpPartManager.SearchAllCount(mydpPartQuery);
                    //if (myDataSet.Tables[0].Rows.Count > 0)
                    //{
                    //    reutrnValue = myDataSet.Tables[0].Rows[0]["countall"].ToString();
                    //}
                }
                else
                {
                    //缓存
                    DataSet myDataSet = new DataSet();

                    string CacheKey = "GetDataSetDownRecord_1_999999999";
                    object objModel = CookiesHelper.GetCache(CacheKey);//从缓存中获取
                    if (objModel == null)//缓存里没有
                    {
                        myDataSet = GetDataSetDownRecord("1", "999999999");
                        objModel = myDataSet;//把数据存入缓存
                        if (objModel != null)
                        {
                            //依赖数据库codematic中的P_Product表变化 来更新缓存
                            System.Web.Caching.SqlCacheDependency dep = new System.Web.Caching.SqlCacheDependency(ConfigurationManager.AppSettings["DataBase"].ToString(), dpDownRecord.TABLENAME);
                            CookiesHelper.SetCache(CacheKey, objModel, dep);//写入缓存
                        }
                    }
                    else
                    {
                        myDataSet = (DataSet)objModel;
                    }
                    reutrnValue = myDataSet.Tables[0].Rows.Count.ToString();
                    //dpDownRecordManager my = new dpDownRecordManager();
                    //dpDownRecordQuery myquery = new dpDownRecordQuery();
                    //myquery.UserID = HttpContext.Current.Session[sysUser.ID].ToString();
                    //DataSet myDataSet = my.SearchAllCount(myquery);
                    //if (myDataSet.Tables[0].Rows.Count > 0)
                    //{
                    //    reutrnValue = myDataSet.Tables[0].Rows[0]["countall"].ToString();
                    //}

                }
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }

            return reutrnValue;
        }

        [WebMethod(Description = "获取推荐列表", EnableSession = true)]
        public static dynamic GetRecommend(string UserID, string ClassifyID, string ID)
        {
            string status = string.Empty;//状态
            string errmsg = string.Empty;//错误信息
            IList<dpPartData> returnData = null;//返回实体列表

            try
            {
                //缓存
                DataSet myDataSet = new DataSet();

                string CacheKey = "SearchRecommend_" + UserID + "_" + ClassifyID + "_" + ID;
                object objModel = CookiesHelper.GetCache(CacheKey);//从缓存中获取
                if (objModel == null)//缓存里没有
                {
                    dpPartManager mydp_Part = new dpPartManager();
                    dpPartQuery mydpPartQuery = new dpPartQuery();
                    mydpPartQuery.UserID = UserID;
                    mydpPartQuery.ClassifyID = ClassifyID;
                    mydpPartQuery.ID = ID;

                    myDataSet = mydp_Part.SearchRecommend(mydpPartQuery);

                    objModel = myDataSet;//把数据存入缓存
                    if (objModel != null)
                    {
                        //依赖数据库codematic中的P_Product表变化 来更新缓存
                        System.Web.Caching.SqlCacheDependency dep = new System.Web.Caching.SqlCacheDependency(ConfigurationManager.AppSettings["DataBase"].ToString(), dpPart.TABLENAME);
                        CommonManager.SetCache(CacheKey, objModel, dep);//写入缓存
                    }
                }
                else
                {
                    myDataSet = (DataSet)objModel;
                }
                if (myDataSet.Tables[0].Rows.Count > 0)
                {
                    returnData = CommonManager.GetList<dpPartData>(myDataSet.Tables[0]);//转换实体类list
                }
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }

            return new { status = status, errmsg = errmsg, returnData = returnData };
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string id = this.hidfileid.Value;
            if (Index.ChkIP())
            {
                Index.DowmLoad(id);
            }
            else
            {
                Response.Write("<script>alert('非注册用户每天只能下载10次！')</script>");
            }
        }


    }
}
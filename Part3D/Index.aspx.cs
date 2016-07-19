using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Data;
using _3DPart.DAL.BULayer.Schema;
using log4net;
using Part3D.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace Part3D
{
    public partial class Index : System.Web.UI.Page
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

            string CacheKey = "SearchAd_首页";
            object objModel = CookiesHelper.GetCache(CacheKey);//从缓存中获取
            if (objModel == null)//缓存里没有
            {
                dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                dpAdvertisementQuery mydpAdvertisementQuery = new dpAdvertisementQuery();
                mydpAdvertisementQuery.ADPosition = "首页";
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

        /// <summary>
        /// 获取组件类别
        /// </summary>
        /// <param name="paramtype">0:导航，1:选中id</param>
        /// <returns></returns>
        [WebMethod(Description = "获取组件类别", EnableSession = true)]
        public static string GetClassify(string paramtype)
        {
            string returnValue = string.Empty;
            try
            {
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

                            if (HttpContext.Current.Session[sysUser.Username].ToString().Trim() == "admin")
                            {
                                returnValue += "<p><a href ='javascript:void(0);' onclick='fnChooseme(" + myDataSet.Tables[0].Rows[i][dpClassify.ID] + ",this);'>" + myDataSet.Tables[0].Rows[i][dpClassify.Name] + "</a></p>";
                            }
                            else
                            {
                                if (myDataSet.Tables[0].Rows[i][dpClassify.Name].ToString().Trim() == "国标")
                                {
                                    returnValue += "<p><a href ='javascript:void(0);'>" + myDataSet.Tables[0].Rows[i][dpClassify.Name] + "</a></p>";
                                }
                                else
                                {
                                    returnValue += "<p><a href ='javascript:void(0);' onclick='fnChooseme(" + myDataSet.Tables[0].Rows[i][dpClassify.ID] + ",this);'>" + myDataSet.Tables[0].Rows[i][dpClassify.Name] + "</a></p>";
                                }

                            }
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
                        //returnType += "<li><a target='_blank' href='/List.aspx'><i class='Ico11'></i>更多标准</a></li>";
                        returnValue = returnType;
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
        /// 获取标准列表
        /// </summary>
        /// <param name="partid"></param>
        /// <returns></returns>
        [WebMethod(Description = "获取标准列表", EnableSession = true)]
        public static string GetStandard(string partid)
        {
            string returnValue = string.Empty;
            try
            {
                dpStandardMappingManager mydpStandardMappingManager = new dpStandardMappingManager();
                dpStandardMappingQuery mydpStandardMappingQuery = new dpStandardMappingQuery();
                mydpStandardMappingQuery.PartID = partid;
                DataSet myDataSet = mydpStandardMappingManager.SearchBind(mydpStandardMappingQuery);
                if (myDataSet.Tables[0].Rows.Count > 0)
                {
                    returnValue += myDataSet.Tables[0].Rows[0][dpStandard.Name].ToString();
                }
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }


            return returnValue;
        }

        [WebMethod(Description = "获取型号列表", EnableSession = true)]
        public static string GetModels(string partid, string format)
        {
            string returnValue = string.Empty;
            try
            {
                dpModelFileManager mydpModelFileManager = new dpModelFileManager();
                dpModelFileQuery mydpModelFileQuery = new dpModelFileQuery();
                mydpModelFileQuery.PartID = partid;
                mydpModelFileQuery.Format = format.ToUpper();
                DataSet myDataSet = mydpModelFileManager.SearchModels(mydpModelFileQuery);
                if (myDataSet.Tables[0].Rows.Count > 0)
                {
                    returnValue += "<dd id='ddall' title='型号'>全部</dd>";
                    for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                    {
                        if (myDataSet.Tables[0].Rows[i][dpModelFile.Models].ToString().Trim().Length > 0)
                        {
                            returnValue += "<dd id='dd" + myDataSet.Tables[0].Rows[i][dpModelFile.Models] + "' title='型号'>" + myDataSet.Tables[0].Rows[i][dpModelFile.Models] + "</dd>";
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
        /// 获取模型文件列表
        /// </summary>
        /// <param name="partid"></param>
        /// <returns></returns>
        [WebMethod(Description = "获取模型文件列表", EnableSession = true)]
        public static string GetModelfile(string partid, string modelsname, string format, string models)
        {
            string returnValue = string.Empty;
            try
            {
                dpModelFileManager mydpModelFileManager = new dpModelFileManager();
                dpModelFileQuery mydpModelFileQuery = new dpModelFileQuery();
                mydpModelFileQuery.PartID = partid;
                mydpModelFileQuery.Name = modelsname;
                mydpModelFileQuery.Format = format.ToUpper();
                mydpModelFileQuery.Models = models;
                DataSet myDataSet = mydpModelFileManager.Search(mydpModelFileQuery);
                if (myDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                    {
                        string showname = myDataSet.Tables[0].Rows[i][dpModelFile.Name].ToString();
                        showname = showname.Split(' ')[1].ToString();
                        returnValue += "<li onclick='fndw(" + myDataSet.Tables[0].Rows[i][dpModelFile.ID].ToString() + ")' >" + showname + "</li>";
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
        /// 文件下载
        /// </summary>
        /// <param name="strid">文件id</param>
        public static void DowmLoad(string strid)
        {
            try
            {
                dpModelFileManager mydpModelFileManager = new dpModelFileManager();
                dpModelFileQuery mydpModelFileQuery = new dpModelFileQuery();
                mydpModelFileQuery.ID = strid;
                DataSet myDataSet = mydpModelFileManager.Search(mydpModelFileQuery);
                if (myDataSet.Tables[0].Rows.Count > 0)
                {

                    string fullPathUrl = HttpContext.Current.Server.MapPath(myDataSet.Tables[0].Rows[0][dpModelFile.Location].ToString());//获取下载文件的路劲
                    System.IO.FileInfo file = new System.IO.FileInfo(fullPathUrl);

                    if (file.Exists)//判断文件是否存在
                    {
                        //添加下载记录
                        Hashtable myHashtable = new Hashtable();
                        if (HttpContext.Current.Session[sysUser.ID] != null)
                        {
                            myHashtable.Add(dpDownRecord.UserID, HttpContext.Current.Session[sysUser.ID].ToString());
                        }
                        else
                        {
                            myHashtable.Add(dpDownRecord.UserID, "0");
                        }
                        myHashtable.Add(dpDownRecord.PartID, myDataSet.Tables[0].Rows[0][dpModelFile.PartID].ToString());
                        myHashtable.Add(dpDownRecord.IPAddress, CommonManager.GetClientIPv4Address());
                        myHashtable.Add(dpDownRecord.RecordType, "1");
                        SQLHelper.ExcuteProc("sp_DownRecord", myHashtable);//执行存储过程注册
                        //执行下载
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ClearHeaders();
                        HttpContext.Current.Response.Buffer = false;
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + file.Name);
                        HttpContext.Current.Response.AddHeader("cintent_length", "attachment;filename=" + HttpUtility.UrlDecode(file.Name));
                        HttpContext.Current.Response.AddHeader("cintent_length", file.Length.ToString());
                        HttpContext.Current.Response.ContentType = "application/octet-stream";
                        HttpContext.Current.Response.WriteFile(file.FullName);//通过response对象，执行下载操作
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();

                    }
                }


            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string id = this.hidfileid.Value;
            if (ChkIP())
            {
                DowmLoad(id);
            }
            else
            {
                Response.Write("<script>alert('非注册用户每天只能下载10次！')</script>");
            }
        }

        public static bool ChkIP()
        {
            bool returnValue = false;

            if (HttpContext.Current.Session[sysUser.ID] != null)
            {
                returnValue = true;
            }
            else
            {
                string currentip = CommonManager.GetClientIPv4Address();
                dpDownRecordManager mydpDownRecordManager = new dpDownRecordManager();
                dpDownRecordQuery mydpDownRecordQuery = new dpDownRecordQuery();
                mydpDownRecordQuery.CreateDate = DateTime.Now.ToString("yyyy-MM-dd");
                mydpDownRecordQuery.IP = currentip.Trim();
                mydpDownRecordQuery.RecordType = "1";
                string count = mydpDownRecordManager.SearchIP(mydpDownRecordQuery);
                if (count.Length > 0)
                {
                    int defaultcount = Convert.ToInt32(ConfigurationManager.AppSettings["UploadCount"].ToString());//默认非用户可下载数量
                    if (int.Parse(count) < defaultcount)
                    {
                        returnValue = true;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="CurrentIndex">页数</param>
        /// <param name="PageSize">每页显示条数</param>
        /// <returns></returns>
        [WebMethod(Description = "获取友情链接", EnableSession = true)]
        public static dynamic GetLinks(string CurrentIndex, string PageSize)
        {
            string status = string.Empty;//状态
            string errmsg = string.Empty;//错误信息
            IList<dpLinksData> returnData = null;//返回实体列表
            try
            {
                //缓存
                DataSet myDataSet = new DataSet();

                string CacheKey = "SearchBindLinks_" + CurrentIndex + "_" + PageSize;
                object objModel = CookiesHelper.GetCache(CacheKey);//从缓存中获取
                if (objModel == null)//缓存里没有
                {
                    dpLinksManager mydpLinksManager = new dpLinksManager();
                    dpLinksQuery mydpLinksQuery = new dpLinksQuery();
                    mydpLinksQuery.CurrentIndex = Convert.ToInt32(CurrentIndex == "" ? "1" : CurrentIndex);
                    mydpLinksQuery.PageSize = Convert.ToInt32(PageSize == "" ? "12" : PageSize);
                    myDataSet = mydpLinksManager.SearchBind(mydpLinksQuery);

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
                    returnData = CommonManager.GetList<dpLinksData>(myDataSet.Tables[0]);//转换实体类list
                }

            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return new { status = status, errmsg = errmsg, returnData = returnData };
        }


    }
}
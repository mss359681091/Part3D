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
                dpAdvertisementManager mydpAdvertisementManager = new dpAdvertisementManager();
                dpAdvertisementQuery mydpAdvertisementQuery = new dpAdvertisementQuery();
                //mydpAdvertisementQuery.ClassifyID = myDataSet.Tables[0].Rows[0][dpPart.ClassifyID].ToString();
                mydpAdvertisementQuery.ADPosition = "列表页";
                DataSet ds = mydpAdvertisementManager.Search(mydpAdvertisementQuery);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.lnkad.HRef = ds.Tables[0].Rows[0][dpAdvertisement.ADLink].ToString();
                    this.imgad.Src = ds.Tables[0].Rows[0][dpAdvertisement.PicturePath].ToString();
                    this.lnkad.Visible = true;
                }
                else
                {
                    this.lnkad.Visible = false;
                }
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
                    dpPartManager mydp_Part = new dpPartManager();
                    dpPartQuery mydpPartQuery = new dpPartQuery();
                    mydpPartQuery.ParentID = ParentID;
                    mydpPartQuery.UserID = UserID;
                    mydpPartQuery.ClassifyID = ClassifyID;
                    mydpPartQuery.Name = Name;
                    mydpPartQuery.CurrentIndex = Convert.ToInt32(CurrentIndex == "" ? "1" : CurrentIndex);
                    mydpPartQuery.PageSize = Convert.ToInt32(PageSize == "" ? "12" : PageSize);

                    DataSet myDataSet = mydp_Part.SearchPaging(mydpPartQuery);

                    if (myDataSet.Tables[0].Rows.Count > 0)
                    {
                        returnData = CommonManager.GetList<dpPartData>(myDataSet.Tables[0]);//转换实体类list
                    }
                }
                else
                {
                    dpDownRecordManager my = new dpDownRecordManager();
                    dpDownRecordQuery myquery = new dpDownRecordQuery();
                    myquery.UserID = HttpContext.Current.Session[sysUser.ID].ToString();
                    myquery.CurrentIndex = Convert.ToInt32(CurrentIndex == "" ? "1" : CurrentIndex);
                    myquery.PageSize = Convert.ToInt32(PageSize == "" ? "12" : PageSize);
                    DataSet myDataSet = my.SearchPaging(myquery);

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
                    dpPartManager mydpPartManager = new dpPartManager();
                    dpPartQuery mydpPartQuery = new dpPartQuery();
                    mydpPartQuery.ClassifyID = classid;
                    mydpPartQuery.Name = partname;
                    DataSet myDataSet = mydpPartManager.SearchAllCount(mydpPartQuery);
                    if (myDataSet.Tables[0].Rows.Count > 0)
                    {
                        reutrnValue = myDataSet.Tables[0].Rows[0]["countall"].ToString();
                    }
                }
                else
                {
                    dpDownRecordManager my = new dpDownRecordManager();
                    dpDownRecordQuery myquery = new dpDownRecordQuery();
                    myquery.UserID = HttpContext.Current.Session[sysUser.ID].ToString();
                    DataSet myDataSet = my.SearchAllCount(myquery);
                    if (myDataSet.Tables[0].Rows.Count > 0)
                    {
                        reutrnValue = myDataSet.Tables[0].Rows[0]["countall"].ToString();
                    }

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
                dpPartManager mydp_Part = new dpPartManager();
                dpPartQuery mydpPartQuery = new dpPartQuery();
                mydpPartQuery.UserID = UserID;
                mydpPartQuery.ClassifyID = ClassifyID;
                mydpPartQuery.ID = ID;

                DataSet myDataSet = mydp_Part.SearchRecommend(mydpPartQuery);

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

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="strid">文件id</param>
        private void DowmLoad(string strid)
        {
            try
            {
                dpModelFileManager mydpModelFileManager = new dpModelFileManager();
                dpModelFileQuery mydpModelFileQuery = new dpModelFileQuery();
                mydpModelFileQuery.ID = strid;
                DataSet myDataSet = mydpModelFileManager.Search(mydpModelFileQuery);
                if (myDataSet.Tables[0].Rows.Count > 0)
                {
                    string fullPathUrl = Server.MapPath(myDataSet.Tables[0].Rows[0][dpModelFile.Location].ToString());//获取下载文件的路劲
                    System.IO.FileInfo file = new System.IO.FileInfo(fullPathUrl);

                    if (file.Exists)//判断文件是否存在
                    {
                        //添加下载记录
                        Hashtable myHashtable = new Hashtable();
                        if (Session[sysUser.ID] != null)
                        {
                            myHashtable.Add(dpDownRecord.UserID, Session[sysUser.ID].ToString());
                        }
                        else
                        {
                            myHashtable.Add(dpDownRecord.UserID, "0");
                        }
                        myHashtable.Add(dpDownRecord.PartID, myDataSet.Tables[0].Rows[0][dpModelFile.PartID].ToString());
                        myHashtable.Add(dpDownRecord.IPAddress, CommonManager.GetClientIPv4Address());
                        SQLHelper.ExcuteProc("sp_DownRecord", myHashtable);//执行存储过程注册
                        //执行下载
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.Buffer = false;
                        Response.AddHeader("content-disposition", "attachment;filename=" + file.Name);
                        Response.AddHeader("cintent_length", "attachment;filename=" + HttpUtility.UrlDecode(file.Name));
                        Response.AddHeader("cintent_length", file.Length.ToString());
                        Response.ContentType = "application/octet-stream";
                        Response.WriteFile(file.FullName);//通过response对象，执行下载操作
                        Response.Flush();
                        Response.End();

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
                ////如果有UpdatePanel就用如下代码调用前台js
                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "Ceshi();", true);
                //如果没有就如下代码
                //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>fntip();</script>", true);
                Response.Write("<script>alert('非注册用户每天只能下载10次！')</script>");
            }
        }
        private bool ChkIP()
        {
            bool returnValue = false;
            string currentip = CommonManager.GetClientIPv4Address();
            dpDownRecordManager mydpDownRecordManager = new dpDownRecordManager();
            dpDownRecordQuery mydpDownRecordQuery = new dpDownRecordQuery();
            mydpDownRecordQuery.CreateDate = DateTime.Now.ToString("yyyy-MM-dd");
            mydpDownRecordQuery.IP = currentip.Trim();
            string count = mydpDownRecordManager.SearchIP(mydpDownRecordQuery);
            if (count.Length > 0)
            {
                int defaultcount = Convert.ToInt32(ConfigurationManager.AppSettings["UploadCount"].ToString());//默认非用户可下载数量
                if (int.Parse(count) < defaultcount)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }


    }
}
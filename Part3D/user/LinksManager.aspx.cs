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
    public partial class LinksManager : System.Web.UI.Page
    {
        private static readonly ILog m_log = LogHelper.GetInstance(); //LogManager.GetLogger(typeof(TEST));

        protected void Page_Load(object sender, EventArgs e)
        {

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
                dpLinksManager mydpLinksManager = new dpLinksManager();
                dpLinksQuery mydpLinksQuery = new dpLinksQuery();
                mydpLinksQuery.CurrentIndex = Convert.ToInt32(CurrentIndex == "" ? "1" : CurrentIndex);
                mydpLinksQuery.PageSize = Convert.ToInt32(PageSize == "" ? "12" : PageSize);
                DataSet myds = mydpLinksManager.SearchBind(mydpLinksQuery);
                if (myds.Tables[0].Rows.Count > 0)
                {
                    returnData = CommonManager.GetList<dpLinksData>(myds.Tables[0]);//转换实体类list
                }

            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return new { status = status, errmsg = errmsg, returnData = returnData };
        }

        /// <summary>
        /// 获取友情链接总数
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "获取友情链接总数", EnableSession = true)]
        public static string GetAllCount()
        {
            string returnValue = string.Empty;
            try
            {
                //if (HttpContext.Current.Session[sysUser.ID] != null)
                //{

                dpLinksManager mydpLinksManager = new dpLinksManager();
                dpLinksQuery mydpLinksQuery = new dpLinksQuery();

                DataSet myds = mydpLinksManager.SearchAllCount(mydpLinksQuery);
                if (myds.Tables[0].Rows.Count > 0)
                {
                    returnValue = myds.Tables[0].Rows[0]["countall"].ToString();
                }
                //}

            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="ids">组件id列表，逗号分隔</param>
        /// <returns></returns>
        [WebMethod(Description = "删除友情链接", EnableSession = true)]
        public static string DelLinks(string linksids)
        {
            string returnValue = string.Empty;

            try
            {

                if (linksids.Length > 0)
                {
                    if (linksids.Contains(","))
                    {
                        linksids = linksids.Substring(0, linksids.Length - 1);
                    }
                    string[] str = linksids.Split(',');

                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i].ToString().Trim().Length > 0)
                        {
                            //删除友情链接
                            dpLinksManager mydpLinksManager = new dpLinksManager();
                            dpLinksQuery mydpLinksQuery = new dpLinksQuery();
                            mydpLinksQuery.ID = str[i].ToString().Trim();
                            mydpLinksManager.DeleteParams(mydpLinksQuery);


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
        /// 修改友情链接名称
        /// </summary>
        /// <param name="partid">友情链接id</param>
        /// <param name="newname">新名称</param>
        /// <returns></returns>
        [WebMethod(Description = "修改友情链接名称", EnableSession = true)]
        public static string SetLinksName(string linksid, string newname)
        {
            string returnValue = string.Empty;
            try
            {
                dpLinksManager mydpLinksManager = new dpLinksManager();
                mydpLinksManager.UpdateParams(dpLinks.LinkName, newname, linksid);
                returnValue = "1";
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }

        /// <summary>
        /// 修改友情链接地址
        /// </summary>
        /// <param name="partid">友情链接id</param>
        /// <param name="newlink">新链接</param>
        /// <returns></returns>
        [WebMethod(Description = "修改友情链接名称", EnableSession = true)]
        public static string SetLinksUrl(string linksid, string newlink)
        {
            string returnValue = string.Empty;
            try
            {
                dpLinksManager mydpLinksManager = new dpLinksManager();
                mydpLinksManager.UpdateParams(dpLinks.LinkUrl, newlink, linksid);
                returnValue = "1";
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }

        [WebMethod(Description = "添加友情链接", EnableSession = true)]
        public static string AddLinks(string lnkname, string lnkurl)
        {
            string returnValue = string.Empty;
            try
            {
                Hashtable myHashtable = new Hashtable();

                myHashtable.Add(dpLinks.LinkName, lnkname);
                myHashtable.Add(dpLinks.LinkUrl, lnkurl);
                myHashtable.Add(dpLinks.ImgUrl, "");
                myHashtable.Add(dpLinks.UserID, HttpContext.Current.Session[sysUser.ID].ToString());
                SQLHelper.ExcuteProc("sp_dpLinks", myHashtable);//执行存储过程注册
                returnValue = "1";//添加成功
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }


            return returnValue;
        }


    }
}
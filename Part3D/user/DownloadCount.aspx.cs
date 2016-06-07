using _3DPart.DAL.BULayer;
using _3DPart.DAL.BULayer.Data;
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

        [WebMethod(Description = "获取分页总数", EnableSession = true)]
        public static string GetAllCount(string start, string end)
        {
            string returnValue = string.Empty;
            try
            {
                dpDownRecordManager mydpDownRecordManager = new dpDownRecordManager();
                dpDownRecordQuery mydpDownRecordQuery = new dpDownRecordQuery();
                mydpDownRecordQuery.start = start;
                mydpDownRecordQuery.end = end;
                string strAllCount = mydpDownRecordManager.SearchDownCount(mydpDownRecordQuery);
                strAllCount = strAllCount == "" ? "0" : strAllCount;
                returnValue = strAllCount;
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            return returnValue;
        }


        [WebMethod(Description = "获取组件下载情况", EnableSession = true)]
        public static dynamic GetDC(string CurrentIndex, string PageSize, string start, string end)
        {
            string status = string.Empty;//状态
            string errmsg = string.Empty;//错误信息
            IList<dpDownRecordData> returnData = null;//返回实体列表
            try
            {
                dpDownRecordManager mydpDownRecordManager = new dpDownRecordManager();
                dpDownRecordQuery mydpDownRecordQuery = new dpDownRecordQuery();
                mydpDownRecordQuery.CurrentIndex = Convert.ToInt32(CurrentIndex);
                mydpDownRecordQuery.PageSize = Convert.ToInt32(PageSize);
                mydpDownRecordQuery.start = start;
                mydpDownRecordQuery.end = end;
                DataSet myDs = mydpDownRecordManager.SearchDownPaging(mydpDownRecordQuery);
                if (myDs.Tables[0].Rows.Count > 0)
                {
                    returnData = CommonManager.GetList<dpDownRecordData>(myDs.Tables[0]);//转换实体类list
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
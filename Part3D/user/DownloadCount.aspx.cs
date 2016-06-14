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
                mydpDownRecordQuery.RecordType = "1";
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
                if (end.Length > 0)
                {
                    DateTime dtend = DateTime.Parse(end);
                    if (dtend.Hour.ToString() == "0" && dtend.Minute.ToString() == "0" && dtend.Second.ToString() == "0")
                    {
                        dtend = dtend.AddDays(1);
                    }
                    end = dtend.ToString();
                }
                dpDownRecordManager mydpDownRecordManager = new dpDownRecordManager();
                dpDownRecordQuery mydpDownRecordQuery = new dpDownRecordQuery();
                mydpDownRecordQuery.CurrentIndex = Convert.ToInt32(CurrentIndex);
                mydpDownRecordQuery.PageSize = Convert.ToInt32(PageSize);
                mydpDownRecordQuery.start = start;
                mydpDownRecordQuery.end = end;
                mydpDownRecordQuery.RecordType = "1";
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

        /// <summary>
        /// 填充图表
        /// </summary>
        /// <param name="start">起始日期</param>
        /// <param name="end">结束日</param>
        /// <param name="RecordType">记录类别，0：浏览，1：下载</param>
        /// <returns></returns>
        [WebMethod(Description = "填充图表", EnableSession = true)]
        public static dynamic GetChartData(string start, string end, string RecordType)
        {
            if (end.Length > 0)
            {
                DateTime dtend = DateTime.Parse(end);
                if (dtend.Hour.ToString() == "0" && dtend.Minute.ToString() == "0" && dtend.Second.ToString() == "0")
                {
                    dtend = dtend.AddDays(1);
                }
                end = dtend.ToString();
            }
            string categories = string.Empty;
            string series = string.Empty;
            dpDownRecordManager mydpDownRecordManager = new dpDownRecordManager();
            dpDownRecordQuery mydpDownRecordQuery = new dpDownRecordQuery();
            mydpDownRecordQuery.start = start;
            mydpDownRecordQuery.end = end;
            mydpDownRecordQuery.RecordType = RecordType;
            DataSet myDataSet = mydpDownRecordManager.SearchChart(mydpDownRecordQuery);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                categories += "[";
                series += "[";
                string strall = string.Empty;
                string strgb = string.Empty;
                string strsc = string.Empty;
                string strmx = string.Empty;
                for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    categories += "'" + myDataSet.Tables[0].Rows[i]["date_day"].ToString().Trim() + "',";
                    strall += myDataSet.Tables[0].Rows[i]["all_count"].ToString().Trim() + ",";
                    strgb += myDataSet.Tables[0].Rows[i]["gb_count"].ToString().Trim() + ",";
                    strsc += myDataSet.Tables[0].Rows[i]["sc_count"].ToString().Trim() + ",";
                    strmx += myDataSet.Tables[0].Rows[i]["mx_count"].ToString().Trim() + ",";
                }
                if (strall.Contains(","))
                {
                    strall = strall.Substring(0, strall.Length - 1);
                }
                if (strgb.Contains(","))
                {
                    strgb = strgb.Substring(0, strgb.Length - 1);
                }
                if (strsc.Contains(","))
                {
                    strsc = strsc.Substring(0, strsc.Length - 1);
                }
                if (strmx.Contains(","))
                {
                    strmx = strmx.Substring(0, strmx.Length - 1);
                }

                series += "{ name: '总下载量', data: [" + strall + "]},";
                series += "{ name: '国标', data: [" + strgb + "]},";
                series += "{ name: '3D素材', data: [" + strsc + "]},";
                series += "{ name: '3D模型', data: [" + strmx + "]}";


                if (categories.Contains(","))
                {
                    categories = categories.Substring(0, categories.Length - 1);
                }
                series += "]";
                categories += "]";
            }
            return new { categories = categories, series = series };

        }

    }
}
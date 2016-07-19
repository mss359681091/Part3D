/******************************************************************
** Copyright (c) 2016-2016 csdn-李赛赛专栏
** 创建人:
** 创建日期:
** 修改人:
** 修改日期:
** 描 述: 
** 版 本:1.0
**----------------------------------------------------------------------------
******************************************************************/

using System;
using System.Collections;
using System.Data;

namespace _3DPart.DAL.BULayer
{
    using _3DPart.DAL.BULayer.Schema;

    [Serializable()]
    public class dpAdvertisementManager
    {
        public DataSet Search(dpAdvertisementQuery QueryData)
        {
            //string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
            //+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
            string strQuery = @"SELECT "
            + dpAdvertisement.ID_FULL + ","
            + dpAdvertisement.UserID_FULL + ","
            + dpAdvertisement.Manufacturer_FULL + ","
            + dpAdvertisement.PicturePath_FULL + ","
            + dpAdvertisement.ADStartDate_FULL + ","
            + dpAdvertisement.ADLink_FULL + ","
            + dpAdvertisement.ADEndDate_FULL + ","
            + dpAdvertisement.ADKeyword_FULL + ","
            + dpAdvertisement.ADPosition_FULL + ","
            + dpAdvertisement.Remark_FULL + ","
            + dpAdvertisement.Enabled_FULL + ","
            + dpAdvertisement.CreateStaff_FULL + ","
            + dpAdvertisement.CreateDate_FULL + ","
            + dpAdvertisement.ModifyStaff_FULL + ","
            + dpAdvertisement.ModifyDate_FULL
            + " FROM " + dpAdvertisement.TABLENAME + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpAdvertisement.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpAdvertisement.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.Enabled.Length > 0)
            {
                strQuery += " AND " + dpAdvertisement.Enabled_FULL + " = @Enabled ";
                myParam.Add("@Enabled", QueryData.Enabled);
            }
            if(QueryData.ADPosition.Length>0)
            {
                strQuery += " AND " + dpAdvertisement.ADPosition_FULL + " = @ADPosition ";
                myParam.Add("@ADPosition", QueryData.ADPosition);

            }

            DataSet myDs = new DataSet();
            try
            {
                myDs = SQLHelper.GetDataSet(strQuery, myParam);
            }
            catch (Exception myEx)
            {

                throw new Exception(myEx.Message + "\r\n SQL:" + strQuery);
            }
            finally
            {

            }
            return myDs;
        }


        public DataSet SearchPaging(dpAdvertisementQuery QueryData)
        {
            string strQuery = @" SELECT TOP " + QueryData.PageSize + " * FROM ( "
            + " SELECT ROW_NUMBER() OVER ( ORDER BY CONVERT( int , " + dpAdvertisement.ID_FULL + ") DESC ) AS RowNumber , "
            + dpAdvertisement.ID_FULL + ","
            + dpAdvertisement.ClassifyID_FULL + ","
            + dpAdvertisement.UserID_FULL + ","
            + dpAdvertisement.Manufacturer_FULL + ","
            + dpAdvertisement.PicturePath_FULL + ","
            + dpAdvertisement.ADLink_FULL + ","
            + "CONVERT(varchar(100)," + dpAdvertisement.ADStartDate_FULL + ", 23) as ADStartDate ,"
            + "CONVERT(varchar(100)," + dpAdvertisement.ADEndDate_FULL + ", 23) as ADEndDate ,"
            + dpAdvertisement.ADKeyword_FULL + ","
            + dpAdvertisement.ADPosition_FULL + ","
            + dpClassify.Name_FULL
            + " FROM " + dpAdvertisement.TABLENAME
            + " LEFT JOIN " + dpClassify.TABLENAME + " ON " + dpAdvertisement.ClassifyID_FULL + " = " + dpClassify.ID_FULL
            + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpAdvertisement.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpAdvertisement.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.Enabled.Length > 0)
            {
                strQuery += " AND " + dpAdvertisement.Enabled_FULL + " = @Enabled ";
                myParam.Add("@Enabled", QueryData.Enabled);
            }
            if (QueryData.ClassifyID.Length > 0)
            {
                strQuery += " AND " + dpAdvertisement.ClassifyID_FULL + " = @ClassifyID ";
                myParam.Add("@ClassifyID", QueryData.ClassifyID);
            }
            strQuery += " ) A ";
            strQuery += " WHERE RowNumber > " + QueryData.PageSize * (QueryData.CurrentIndex - 1);


            DataSet myDs = new DataSet();
            try
            {
                myDs = SQLHelper.GetDataSet(strQuery, myParam);
            }
            catch (Exception myEx)
            {

                throw new Exception(myEx.Message + "\r\n SQL:" + strQuery);
            }
            finally
            {

            }
            return myDs;
        }

        /// <summary>
        /// 加载所有广告数量
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet SearchAllCount(dpAdvertisementQuery QueryData)
        {

            string strQuery = @"SELECT ";
            strQuery += " SUM( 1 ) as countall ";
            strQuery += " FROM " + dpAdvertisement.TABLENAME;
            strQuery += " LEFT JOIN " + dpClassify.TABLENAME + " ON " + dpAdvertisement.ClassifyID_FULL + " = " + dpClassify.ID_FULL;
            strQuery += " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.ClassifyID.Length > 0)
            {
                strQuery += " AND (" + dpClassify.ID_FULL + " = @ClassifyID OR " + dpClassify.ParentID_FULL + " = " + QueryData.ClassifyID + " ) ";
                myParam.Add("@ClassifyID", QueryData.ClassifyID);
            }

            DataSet myDs = new DataSet();
            try
            {
                myDs = SQLHelper.GetDataSet(strQuery, myParam);
            }
            catch (Exception myEx)
            {

                throw new Exception(myEx.Message + "\r\n SQL:" + strQuery);
            }
            finally
            {

            }
            return myDs;
        }

        /// <summary>
        /// 删除广告信息
        /// </summary>
        /// <param name="strParam">修改字段</param>
        /// <param name="strValue">字段值</param>
        /// <param name="strPartID">组件ID</param>
        /// <returns></returns>
        public string DeleteParams(dpAdvertisementQuery QueryData)
        {
            string returnValue = string.Empty;
            string strQuery = @"DELETE FROM " + dpAdvertisement.TABLENAME + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpAdvertisement.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            try
            {
                returnValue = SQLHelper.ExcuteSQL(strQuery, myParam).ToString();
            }
            catch (Exception myEx)
            {

                throw new Exception(myEx.Message + "\r\n SQL:" + strQuery);
            }
            finally
            {

            }
            return returnValue;
        }

        /// <summary>
        /// 修改广告信息
        /// </summary>
        /// <param name="strParam">修改字段</param>
        /// <param name="strValue">字段值</param>
        /// <param name="strPartID">组件ID</param>
        /// <returns></returns>
        public string UpdateParams(string strParam, string strValue, string strAdID)
        {
            string returnValue = string.Empty;
            string strQuery = @"Update " + dpAdvertisement.TABLENAME + " Set " + strParam + " = '" + strValue + "' WHERE 1 = 1 ";
            strQuery += " AND " + dpAdvertisement.Enabled_FULL + " =1 ";
            strQuery += " AND " + dpAdvertisement.ID_FULL + " = " + strAdID;
            Hashtable myParam = new Hashtable();
            try
            {
                returnValue = SQLHelper.ExcuteSQL(strQuery).ToString();
            }
            catch (Exception myEx)
            {

                throw new Exception(myEx.Message + "\r\n SQL:" + strQuery);
            }
            finally
            {

            }
            return returnValue;
        }

        /// <summary>
        /// 根据id检索
        /// </summary>
        /// <param name="QueryData"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public string GetParams(dpAdvertisementQuery QueryData, string strParam)
        {
            string returnValue = string.Empty;
            string strQuery = @"SELECT " + strParam + " FROM " + dpAdvertisement.TABLENAME + " WHERE 1 = 1 ";
            strQuery += " AND " + dpAdvertisement.Enabled_FULL + " =1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpAdvertisement.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }
            try
            {
                if (SQLHelper.GetObject(strQuery, myParam) != null)
                {
                    returnValue = SQLHelper.GetObject(strQuery, myParam).ToString();
                }
            }
            catch (Exception myEx)
            {

                throw new Exception(myEx.Message + "\r\n SQL:" + strQuery);
            }
            finally
            {

            }
            return returnValue;
        }

    }

    [Serializable()]
    public class dpAdvertisementQuery
    {
        public string ID = string.Empty;
        public string UserID = string.Empty;
        public string Enabled = string.Empty;
        public string ClassifyID = string.Empty;
        public int CurrentIndex = 1;
        public int PageSize = 12;
        public string ADPosition = string.Empty;

        public string SortField = " ID ";
        public string SortDir = " DESC ";
        public dpAdvertisementQuery(string paramID, string paramUserID, string paramEnabled)
        {
            this.ID = paramID;
            this.UserID = paramUserID;
            this.Enabled = paramEnabled;
        }
        public dpAdvertisementQuery()
        {
        }
    }
}

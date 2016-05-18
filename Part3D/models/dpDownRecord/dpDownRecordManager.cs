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
    public class dpDownRecordManager
    {


        public DataSet Search(dpDownRecordQuery QueryData)
        {
            //string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
            //+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
            string strQuery = @"SELECT "
            + dpDownRecord.ID_FULL + ","
            + dpDownRecord.PartID_FULL + ","
            + dpDownRecord.UserID_FULL + ","
            + dpDownRecord.IPAddress_FULL + ","
            + dpDownRecord.Remark_FULL + ","
            + dpDownRecord.Enabled_FULL + ","
            + dpDownRecord.CreateStaff_FULL + ","
            + dpDownRecord.CreateDate_FULL + ","
            + dpDownRecord.ModifyStaff_FULL + ","
            + dpDownRecord.ModifyDate_FULL
             + " FROM " + dpDownRecord.TABLENAME + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.PartID_FULL + " = @PartID ";
                myParam.Add("@PartID", QueryData.PartID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.Enabled.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.Enabled_FULL + " = @Enabled ";
                myParam.Add("@Enabled", QueryData.Enabled);
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

        public DataSet SearchAllCount(dpDownRecordQuery QueryData)
        {

            string strQuery = @"SELECT ";
            strQuery += " SUM( 1 ) as countall ";
            strQuery += " FROM " + dpDownRecord.TABLENAME;
            strQuery += " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.PartID_FULL + " = @PartID ";
                myParam.Add("@PartID", QueryData.PartID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.Enabled.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.Enabled_FULL + " = @Enabled ";
                myParam.Add("@Enabled", QueryData.Enabled);
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

        public DataSet SearchPaging(dpDownRecordQuery QueryData)
        {

            string strQuery = @" SELECT TOP " + QueryData.PageSize + "  * FROM ( SELECT  ROW_NUMBER() OVER ( ORDER BY " + dpDownRecord.PartID + " DESC ) AS RowNumber ,* FROM ( "
            + " SELECT distinct(" + dpDownRecord.PartID_FULL + "),"
            + dpPart.ID_FULL + ","
            + dpPart.ParentID_FULL + ","
            + dpPart.UserID_FULL + ","
            + dpPart.ClassifyID_FULL + ","
            + dpPart.Name_FULL + ","
            + dpPart.Preview_FULL + ","
            + dpPart.PreviewSmall_FULL + ","
            + dpPart.Description_FULL + ","
            + dpPart.Limits_FULL + ","
            + dpPart.Keyword_FULL + ","
            + dpPart.Accesslog_FULL + ","
            + dpPart.Remark_FULL + ","
            + dpPart.Enabled_FULL + ","
            + dpPart.CreateStaff_FULL + ","
            + dpPart.CreateDate_FULL + ","
            + dpPart.ModifyStaff_FULL + ","
            + dpPart.ModifyDate_FULL
            + " FROM " + dpDownRecord.TABLENAME
            + " LEFT JOIN  " + dpPart.TABLENAME + " ON " + dpDownRecord.PartID_FULL + " = " + dpPart.ID_FULL
            + " LEFT JOIN " + dpClassify.TABLENAME + " ON " + dpPart.ClassifyID_FULL + " = " + dpClassify.ID_FULL
            + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.PartID_FULL + " = @PartID ";
                myParam.Add("@PartID", QueryData.PartID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.Enabled.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.Enabled_FULL + " = @Enabled ";
                myParam.Add("@Enabled", QueryData.Enabled);
            }

            strQuery += " ) A ) B";
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
        /// 删除下载记录
        /// </summary>
        /// <param name="strParam">修改字段</param>
        /// <param name="strValue">字段值</param>
        /// <param name="strPartID">组件ID</param>
        /// <returns></returns>
        public string DeleteParams(dpDownRecordQuery QueryData)
        {
            string returnValue = string.Empty;
            string strQuery = @"DELETE FROM " + dpDownRecord.TABLENAME + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();
            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpDownRecord.PartID_FULL + " =@PartID";
                myParam.Add("@PartID", QueryData.PartID);
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

    }

    [Serializable()]
    public class dpDownRecordQuery
    {
        public string ID = string.Empty;
        public string PartID = string.Empty;
        public string UserID = string.Empty;
        public string Enabled = string.Empty;
        public int CurrentIndex = 1;
        public int PageSize = 12;
        public string SortField = " ID ";
        public string SortDir = " DESC ";
        public dpDownRecordQuery(string paramID, string paramPartID, string paramUserID, string paramEnabled)
        {
            this.ID = paramID;
            this.PartID = paramPartID;
            this.UserID = paramUserID;
            this.Enabled = paramEnabled;
        }
        public dpDownRecordQuery()
        {
        }
    }
}

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
    public class dpPartManager
    {
        /// <summary>
        /// 默认查询，无关联表
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet Search(dpPartQuery QueryData)
        {
            string strQuery = @"SELECT "
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
            + " FROM " + dpPart.TABLENAME + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpPart.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.ParentID.Length > 0)
            {
                strQuery += " AND " + dpPart.ParentID_FULL + " = @ParentID ";
                myParam.Add("@ParentID", QueryData.ParentID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpPart.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.ClassifyID.Length > 0)
            {
                strQuery += " AND " + dpPart.ClassifyID_FULL + " = @ClassifyID ";
                myParam.Add("@ClassifyID", QueryData.ClassifyID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpPart.Name_FULL + " LIKE @Name ";
                myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
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
        /// 查询单条组件信息
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet SearchSingle(dpPartQuery QueryData)
        {
            string strQuery = @"SELECT "
            + dpPart.ID_FULL + ","
            + dpPart.ParentID_FULL + ","
            + sysUser.Nickname_FULL + ","
            + dpPart.ClassifyID_FULL + ","
            + dpPart.Name_FULL + ","
            + dpPart.Preview_FULL + ","
            + dpPart.PreviewSmall_FULL + ","
            + dpPart.Description_FULL + ","
            + dpPart.Limits_FULL + ","
            + dpPart.Keyword_FULL + ","
            + dpPart.Accesslog_FULL + ","
            + dpPart.Remark_FULL + ","
            + " CONVERT(varchar(100), " + dpPart.CreateDate_FULL + ", 23) as CreateDate "
            + " FROM " + dpPart.TABLENAME
            + " LEFT JOIN " + sysUser.TABLENAME + " ON " + dpPart.UserID_FULL + " = " + sysUser.ID_FULL
            + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpPart.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.ParentID.Length > 0)
            {
                strQuery += " AND " + dpPart.ParentID_FULL + " = @ParentID ";
                myParam.Add("@ParentID", QueryData.ParentID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpPart.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.ClassifyID.Length > 0)
            {
                strQuery += " AND " + dpPart.ClassifyID_FULL + " = @ClassifyID ";
                myParam.Add("@ClassifyID", QueryData.ClassifyID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpPart.Name_FULL + " LIKE @Name ";
                myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
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
        /// 带分页的组件列表
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet SearchPaging(dpPartQuery QueryData)
        {

            string strQuery = @" SELECT TOP " + QueryData.PageSize + " * FROM ( "
            + " SELECT ROW_NUMBER() OVER ( ORDER BY CONVERT( int , " + dpPart.ID_FULL + ") DESC ) AS RowNumber , "
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
            + " FROM " + dpPart.TABLENAME
            + " LEFT JOIN " + dpClassify.TABLENAME + " ON " + dpPart.ClassifyID_FULL + " = " + dpClassify.ID_FULL
            + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpPart.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.ParentID.Length > 0)
            {
                strQuery += " AND " + dpPart.ParentID_FULL + " = @ParentID ";
                myParam.Add("@ParentID", QueryData.ParentID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpPart.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.ClassifyID.Length > 0)
            {
                strQuery += " AND (" + dpPart.ClassifyID_FULL + " = @ClassifyID OR " + dpClassify.ParentID_FULL + " = @ClassifyID )";
                myParam.Add("@ClassifyID", QueryData.ClassifyID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpPart.Name_FULL + " LIKE @Name ";
                myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
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
        /// 根据分类显示数量
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet SearchCount(dpPartQuery QueryData)
        {

            string strQuery = @"SELECT ";
            strQuery += " SUM( 1 ) as countall,";
            strQuery += " sum(case when " + dpClassify.ParentID_FULL + "=1  or dp_Part.ClassifyID=1 then 1 else 0 end) as count1, ";
            strQuery += " sum(case when " + dpClassify.ParentID_FULL + "=12 or dp_Part.ClassifyID=12 then 1 else 0 end) as count2, ";
            strQuery += " sum(case when " + dpClassify.ParentID_FULL + "=13 or dp_Part.ClassifyID=13 then 1 else 0 end) as count3 ";
            strQuery += " FROM " + dpPart.TABLENAME;
            strQuery += " LEFT JOIN " + dpClassify.TABLENAME + " ON " + dpPart.ClassifyID_FULL + " = " + dpClassify.ID_FULL;
            strQuery += " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ParentID.Length > 0)
            {
                strQuery += " AND " + dpPart.ParentID_FULL + " = @ParentID ";
                myParam.Add("@ParentID", QueryData.ParentID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpPart.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpPart.Name_FULL + " LIKE @Name ";
                myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
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
        /// 加载所有组件数量
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet SearchAllCount(dpPartQuery QueryData)
        {

            string strQuery = @"SELECT ";
            strQuery += " SUM( 1 ) as countall ";
            strQuery += " FROM " + dpPart.TABLENAME;
            strQuery += " LEFT JOIN " + dpClassify.TABLENAME + " ON " + dpPart.ClassifyID_FULL + " = " + dpClassify.ID_FULL;
            strQuery += " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ClassifyID.Length > 0)
            {
                strQuery += " AND (" + dpClassify.ID_FULL + " = @ClassifyID OR " + dpClassify.ParentID_FULL + " = " + QueryData.ClassifyID + " ) ";
                myParam.Add("@ClassifyID", QueryData.ClassifyID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpPart.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpPart.Name_FULL + " LIKE @Name ";
                myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
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
        /// 加载推荐列表
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet SearchRecommend(dpPartQuery QueryData)
        {
            string strQuery = @"SELECT "
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
            + " FROM " + dpPart.TABLENAME
            + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();
            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpPart.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.ClassifyID.Length > 0)
            {
                strQuery += " AND " + dpPart.ClassifyID_FULL + " = @ClassifyID ";
                myParam.Add("@ClassifyID", QueryData.ClassifyID);
            }
            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpPart.ID_FULL + " != @ID ";
                myParam.Add("@ID", QueryData.ID);
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
        /// 修改组件信息
        /// </summary>
        /// <param name="strParam">修改字段</param>
        /// <param name="strValue">字段值</param>
        /// <param name="strPartID">组件ID</param>
        /// <returns></returns>
        public string UpdateParams(string strParam, string strValue, string strPartID)
        {
            string returnValue = string.Empty;
            string strQuery = @"Update " + dpPart.TABLENAME + " Set " + strParam + " = '" + strValue + "' WHERE 1 = 1 ";
            strQuery += " AND " + dpPart.Enabled_FULL + " =1 ";
            strQuery += " AND " + dpPart.ID_FULL + " = " + strPartID;
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
        /// 删除组件信息
        /// </summary>
        /// <param name="strParam">修改字段</param>
        /// <param name="strValue">字段值</param>
        /// <param name="strPartID">组件ID</param>
        /// <returns></returns>
        public string DeleteParams(dpPartQuery QueryData)
        {
            string returnValue = string.Empty;
            string strQuery = @"DELETE FROM " + dpPart.TABLENAME + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();
            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpPart.UserID_FULL + " =@UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }
            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpPart.ID_FULL + " = @ID ";
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
        /// 检索我的资源，带分页
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet SearchMyPart(dpPartQuery QueryData)
        {
            string strQuery = @" SELECT TOP " + QueryData.PageSize + " * FROM ( "
            + " SELECT ROW_NUMBER() OVER ( ORDER BY CONVERT( int , " + dpPart.ID_FULL + ") DESC ) AS RowNumber , "
            + dpPart.ID_FULL + ","
            + dpPart.PreviewSmall_FULL + ","
            + dpPart.Name_FULL + ","
            + dpPart.Accesslog_FULL + ","
            + dpPart.ClassifyID_FULL + ","
            + dpClassify.Name_FULL + " as classname ,"
            + " (select count(*) from  " + dpDownRecord.TABLENAME
            + " left join " + dpPart.TABLENAME + " as dp1 on " + dpDownRecord.PartID_FULL + " = dp1.id "
            + " where 1=1 "
            + " and dp1.userid = " + dpPart.UserID_FULL
            + " and dp1.id= " + dpPart.ID_FULL
            + " and  " + dpDownRecord.RecordType_FULL + " =1 "
            + " ) as mycount , "
            + sysUser.Username_FULL + " as Remark ,"
            + "CONVERT(varchar(100)," + dpPart.CreateDate_FULL + ", 23) as CreateDate1 "
            + " FROM " + dpPart.TABLENAME
            + " LEFT JOIN " + dpClassify.TABLENAME + " ON " + dpPart.ClassifyID_FULL + " = " + dpClassify.ID_FULL
            + " LEFT JOIN " + sysUser.TABLENAME + " ON " + dpPart.UserID_FULL + " = " + sysUser.ID_FULL
            + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpPart.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpPart.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.ClassifyID.Length > 0)
            {
                strQuery += " AND (" + dpPart.ClassifyID_FULL + " = @ClassifyID OR " + dpClassify.ParentID_FULL + " = @ClassifyID )";
                myParam.Add("@ClassifyID", QueryData.ClassifyID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpPart.Name_FULL + " LIKE @Name ";
                myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
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
    }

    [Serializable()]
    public class dpPartQuery
    {
        public string ID = string.Empty;
        public string ParentID = string.Empty;
        public string UserID = string.Empty;
        public string ClassifyID = string.Empty;
        public string Name = string.Empty;
        public int CurrentIndex = 1;
        public int PageSize = 12;

        public string SortField = " ID ";
        public string SortDir = " DESC ";
        public dpPartQuery(string paramID, string paramParentID, string paramUserID, string paramClassifyID, string paramName)
        {
            this.ID = paramID;
            this.ParentID = paramParentID;
            this.UserID = paramUserID;
            this.ClassifyID = paramClassifyID;
            this.Name = paramName;
        }
        public dpPartQuery()
        {
        }
    }
}

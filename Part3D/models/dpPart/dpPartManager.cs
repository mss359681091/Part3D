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
        public DataSet Search(dpPartQuery QueryData)
        {
            //string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
            //+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
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

        public DataSet SearchPaging(dpPartQuery QueryData)
        {

            string strQuery = @" SELECT TOP " + QueryData.PageSize + " * FROM ( "
            + " SELECT ROW_NUMBER() OVER ( ORDER BY " + dpPart.ID_FULL + ") AS RowNumber , "
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

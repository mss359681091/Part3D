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
    public class dpClassifyManager
    {
        public DataSet Search(dpClassifyQuery QueryData)
        {
            //string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
            //+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
            string strQuery = @"SELECT "
            + dpClassify.ID_FULL + ","
            + dpClassify.ParentID_FULL + ","
            + dpClassify.Name_FULL + ","
            + dpClassify.Remark_FULL + ","
            + dpClassify.Enabled_FULL + ","
            + dpClassify.CreateStaff_FULL + ","
            + dpClassify.CreateDate_FULL + ","
            + dpClassify.ModifyStaff_FULL + ","
            + dpClassify.ModifyDate_FULL
             + " FROM " + dpClassify.TABLENAME + " WHERE 1 = 1 ";

            
            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpClassify.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.ParentID.Length > 0)
            {
                strQuery += " AND " + dpClassify.ParentID_FULL + " = @ParentID ";
                myParam.Add("@ParentID", QueryData.ParentID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpClassify.Name_FULL + " LIKE @Name ";
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
    }

    [Serializable()]
    public class dpClassifyQuery
    {
        public string ID = string.Empty;
        public string ParentID = string.Empty;
        public string Name = string.Empty;

        public string SortField = " ID ";
        public string SortDir = " DESC ";
        public dpClassifyQuery(string paramID, string paramParentID, string paramName)
        {
            this.ID = paramID;
            this.ParentID = paramParentID;
            this.Name = paramName;
        }
        public dpClassifyQuery()
        {
        }
    }
}

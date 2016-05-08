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
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace _3DPart.DAL.BULayer
{
    using _3DPart.DAL.BULayer.Schema;


    [Serializable()]
    public class dp_ClassifyManager
    {
        public DataSet Search(dp_ClassifyQuery QueryData)
        {
            //string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
            //+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
            string strQuery = @"SELECT "
            + dp_Classify.ID_FULL + ","
            + dp_Classify.ParentID_FULL + ","
            + dp_Classify.Name_FULL + ","
            + dp_Classify.Remark_FULL + ","
            + dp_Classify.Enabled_FULL + ","
            + dp_Classify.CreateStaff_FULL + ","
            + dp_Classify.CreateDate_FULL + ","
            + dp_Classify.ModifyStaff_FULL + ","
            + dp_Classify.ModifyDate_FULL
             + " FROM " + dp_Classify.TABLENAME + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dp_Classify.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.ParentID.Length > 0)
            {
                strQuery += " AND " + dp_Classify.ParentID_FULL + " = @ParentID ";
                myParam.Add("@ParentID", QueryData.ParentID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dp_Classify.Name_FULL + " LIKE @Name ";
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
    public class dp_ClassifyQuery
    {
        public string ID = string.Empty;
        public string ParentID = string.Empty;
        public string Name = string.Empty;

        public string SortField = " ID ";
        public string SortDir = " DESC ";
        public dp_ClassifyQuery(string paramID, string paramParentID, string paramName)
        {
            this.ID = paramID;
            this.ParentID = paramParentID;
            this.Name = paramName;
        }
        public dp_ClassifyQuery()
        {
        }
    }
}

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
    public class dpStandardMappingManager
    {
        public DataSet Search(dpStandardMappingQuery QueryData)
        {
            string strQuery = @"SELECT "
            + dpStandardMapping.ID_FULL + ","
            + dpStandardMapping.StandardID_FULL + ","
            + dpStandardMapping.PartID_FULL + ","
            + dpStandardMapping.Name_FULL + ","
            + dpStandardMapping.Remark_FULL + ","
            + dpStandardMapping.Enabled_FULL + ","
            + dpStandardMapping.CreateStaff_FULL + ","
            + dpStandardMapping.CreateDate_FULL + ","
            + dpStandardMapping.ModifyStaff_FULL + ","
            + dpStandardMapping.ModifyDate_FULL
             + " FROM " + dpStandardMapping.TABLENAME + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpStandardMapping.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.StandardID.Length > 0)
            {
                strQuery += " AND " + dpStandardMapping.StandardID_FULL + " = @StandardID ";
                myParam.Add("@StandardID", QueryData.StandardID);
            }

            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpStandardMapping.PartID_FULL + " = @PartID ";
                myParam.Add("@PartID", QueryData.PartID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpStandardMapping.Name_FULL + " LIKE @Name ";
                myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
            }
            // strQuery += ") AS " + dpStandardMapping.TABLENAME;

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

        public DataSet SearchBind(dpStandardMappingQuery QueryData)
        {
            string strQuery = @"SELECT "
            + dpStandardMapping.ID_FULL + ","
            + dpStandardMapping.StandardID_FULL + ","
            + dpStandardMapping.PartID_FULL + ","
            + dpStandard.Name_FULL
            + " FROM " + dpStandardMapping.TABLENAME
            + " LEFT JOIN " + dpStandard.TABLENAME + " ON " + dpStandardMapping.StandardID_FULL + " = " + dpStandard.ID_FULL
            + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpStandardMapping.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.StandardID.Length > 0)
            {
                strQuery += " AND " + dpStandardMapping.StandardID_FULL + " = @StandardID ";
                myParam.Add("@StandardID", QueryData.StandardID);
            }

            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpStandardMapping.PartID_FULL + " = @PartID ";
                myParam.Add("@PartID", QueryData.PartID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpStandardMapping.Name_FULL + " LIKE @Name ";
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

        public string CheckISNull(dpStandardMappingQuery QueryData)
        {
            string returnValue = "";
            string strQuery = @"SELECT "
            + dpStandardMapping.ID_FULL
            + " FROM " + dpStandardMapping.TABLENAME
            + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.StandardID.Length > 0)
            {
                strQuery += " AND " + dpStandardMapping.StandardID_FULL + "= @StandardID ";
                myParam.Add("@StandardID", QueryData.StandardID);
            }
            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpStandardMapping.PartID_FULL + "= @PartID ";
                myParam.Add("@PartID", QueryData.PartID);
            }
            try
            {
                returnValue = SQLHelper.ExcuteScalarSQL(strQuery, myParam);
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
    public class dpStandardMappingQuery
    {
        public string ID = string.Empty;
        public string StandardID = string.Empty;
        public string PartID = string.Empty;
        public string Name = string.Empty;

        public string SortField = " ID ";
        public string SortDir = " DESC ";
        public dpStandardMappingQuery(string paramID, string paramStandardID, string paramPartID, string paramName)
        {
            this.ID = paramID;
            this.StandardID = paramStandardID;
            this.PartID = paramPartID;
            this.Name = paramName;
        }
        public dpStandardMappingQuery()
        {
        }
    }
}

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
    public class dpAdvertisementQuery
    {
        public string ID = string.Empty;
        public string UserID = string.Empty;
        public string Enabled = string.Empty;

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

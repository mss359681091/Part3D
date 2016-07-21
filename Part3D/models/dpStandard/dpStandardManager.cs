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
    public class dpStandardManager
    {
        public DataSet Search(dpStandardQuery QueryData)
        {
            //string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
            //+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
            string strQuery = @"SELECT "
            + dpStandard.ID_FULL + ","
            + dpStandard.Name_FULL + ","
            + dpStandard.Content_FULL + ","
            + dpStandard.Remark_FULL + ","
            + dpStandard.Enabled_FULL + ","
            + dpStandard.CreateStaff_FULL + ","
            + dpStandard.CreateDate_FULL + ","
            + dpStandard.ModifyStaff_FULL + ","
            + dpStandard.ModifyDate_FULL
             + " FROM " + dpStandard.TABLENAME + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpStandard.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpStandard.Name_FULL + " LIKE @Name ";
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

        public string CheckISNull(dpStandardQuery QueryData)
        {
            string returnValue = "";
            string strQuery = @"SELECT "
            + dpStandard.ID_FULL
            + " FROM " + dpStandard.TABLENAME
            + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpStandard.Name_FULL + "= @Name ";
                myParam.Add("@Name", QueryData.Name);
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
    public class dpStandardQuery
    {
        public string ID = string.Empty;
        public string Name = string.Empty;

        public string SortField = " ID ";
        public string SortDir = " DESC ";
        public dpStandardQuery(string paramID, string paramName)
        {
            this.ID = paramID;
            this.Name = paramName;
        }
        public dpStandardQuery()
        {
        }
    }
}

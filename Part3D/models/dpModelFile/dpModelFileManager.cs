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
    public class dpModelFileManager
    {
        public DataSet Search(dpModelFileQuery QueryData)
        {
            //string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
            //+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
            string strQuery = @"SELECT "
            + dpModelFile.ID_FULL + ","
            + dpModelFile.PartID_FULL + ","
            + dpModelFile.Name_FULL + ","
            + dpModelFile.Format_FULL + ","
            + dpModelFile.Location_FULL + ","
            + dpModelFile.Size_FULL + ","
            + dpModelFile.Models_FULL + ","
            + dpModelFile.Remark_FULL + ","
            + dpModelFile.Enabled_FULL + ","
            + dpModelFile.CreateStaff_FULL + ","
            + dpModelFile.CreateDate_FULL + ","
            + dpModelFile.ModifyStaff_FULL + ","
            + dpModelFile.ModifyDate_FULL
            + " FROM " + dpModelFile.TABLENAME
            + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpModelFile.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpModelFile.PartID_FULL + " = @PartID ";
                myParam.Add("@PartID", QueryData.PartID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpModelFile.Name_FULL + " LIKE @Name ";
                myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
            }
            if (QueryData.Format.Length > 0)
            {
                strQuery += " AND " + dpModelFile.Format_FULL + " = @Format ";
                myParam.Add("@Format", QueryData.Format);
            }
            if (QueryData.Models.Length > 0)
            {
                strQuery += " AND " + dpModelFile.Models_FULL + " = @Models ";
                myParam.Add("@Models", QueryData.Models);
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
        /// 查询文件型号列表
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet SearchModels(dpModelFileQuery QueryData)
        {

            string strQuery = @"SELECT distinct "
            + dpModelFile.Models_FULL
            + " FROM " + dpModelFile.TABLENAME
            + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpModelFile.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpModelFile.PartID_FULL + " = @PartID ";
                myParam.Add("@PartID", QueryData.PartID);
            }

            if (QueryData.Name.Length > 0)
            {
                strQuery += " AND " + dpModelFile.Name_FULL + " LIKE @Name ";
                myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
            }
            if (QueryData.Format.Length > 0)
            {
                strQuery += " AND " + dpModelFile.Format_FULL + " = @Format ";
                myParam.Add("@Format", QueryData.Format);
            }
            if (QueryData.Models.Length > 0)
            {
                strQuery += " AND " + dpModelFile.Models_FULL + " = @Models ";
                myParam.Add("@Models", QueryData.Models);
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
        /// 删除文件
        /// </summary>
        /// <param name="strParam">修改字段</param>
        /// <param name="strValue">字段值</param>
        /// <param name="strPartID">组件ID</param>
        /// <returns></returns>
        public string DeleteParams(dpModelFileQuery QueryData)
        {
            string returnValue = string.Empty;
            string strQuery = @"DELETE FROM " + dpModelFile.TABLENAME + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpModelFile.PartID_FULL + " = @PartID";
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

        /// <summary>
        /// 根据id检索
        /// </summary>
        /// <param name="QueryData"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public string GetParams(dpModelFileQuery QueryData, string strParam)
        {
            string returnValue = string.Empty;
            string strQuery = @"SELECT " + strParam + " FROM " + dpModelFile.TABLENAME + " WHERE 1 = 1 ";
            strQuery += " AND " + dpModelFile.Enabled_FULL + " =1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpModelFile.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }
            if (QueryData.PartID.Length > 0)
            {
                strQuery += " AND " + dpModelFile.PartID_FULL + " = @PartID ";
                myParam.Add("@PartID", QueryData.PartID);
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
    public class dpModelFileQuery
    {
        public string ID = string.Empty;
        public string PartID = string.Empty;
        public string Name = string.Empty;
        public string Format = string.Empty;
        public string Models = string.Empty;

        public string SortField = " ID ";
        public string SortDir = " DESC ";
        public dpModelFileQuery(string paramID, string paramPartID, string paramName)
        {
            this.ID = paramID;
            this.PartID = paramPartID;
            this.Name = paramName;
        }
        public dpModelFileQuery()
        {
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace _3DPart.DAL.BULayer
{
    using _3DPart.DAL.BULayer.Data;
    using _3DPart.DAL.BULayer.Schema;


    [Serializable()]
    public class dpLinksManager : dpLinksData
    {
        public DataSet Search(dpLinksQuery QueryData)
        {
            string strQuery = @"SELECT "
            + dpLinks.ID_FULL + ","
            + dpLinks.UserID_FULL + ","
            + dpLinks.LinkName_FULL + ","
            + dpLinks.LinkUrl_FULL + ","
            + dpLinks.ImgUrl_FULL + ","
            + dpLinks.Remark_FULL + ","
            + dpLinks.Enabled_FULL + ","
            + dpLinks.CreateStaff_FULL + ","
            + dpLinks.CreateDate_FULL + ","
            + dpLinks.ModifyStaff_FULL + ","
            + dpLinks.ModifyDate_FULL
             + " FROM " + dpLinks.TABLENAME + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpLinks.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.LinkName.Length > 0)
            {
                strQuery += " AND " + dpLinks.LinkName_FULL + " LIKE @LinkName ";
                myParam.Add("@LinkName", "%" + QueryData.LinkName.Replace(" ", "%") + "%");
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
        /// 加载所有友情链接数量
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet SearchAllCount(dpLinksQuery QueryData)
        {

            string strQuery = @"SELECT ";
            strQuery += " SUM( 1 ) as countall ";
            strQuery += " FROM " + dpLinks.TABLENAME;
            strQuery += " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpLinks.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
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
        /// 检索友情链接，带分页
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet SearchBind(dpLinksQuery QueryData)
        {
            string strQuery = @" SELECT TOP " + QueryData.PageSize + " * FROM ( "
            + " SELECT ROW_NUMBER() OVER ( ORDER BY CONVERT( int , " + dpLinks.ID_FULL + ") DESC ) AS RowNumber , "
            + dpLinks.ID_FULL + ","
            + dpLinks.UserID_FULL + ","
            + dpLinks.LinkName_FULL + ","
            + dpLinks.LinkUrl_FULL + ","
            + dpLinks.ImgUrl_FULL + ","
            + "CONVERT(varchar(100)," + dpLinks.CreateDate_FULL + ", 23) as CreateDate "
            + " FROM " + dpLinks.TABLENAME
            + " LEFT JOIN " + sysUser.TABLENAME + " ON " + dpLinks.UserID_FULL + " = " + sysUser.ID_FULL
            + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpLinks.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
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
        /// 修改友情链接信息
        /// </summary>
        /// <param name="strParam">修改字段</param>
        /// <param name="strValue">字段值</param>
        /// <param name="strPartID">组件ID</param>
        /// <returns></returns>
        public string UpdateParams(string strParam, string strValue, string strLinksID)
        {
            string returnValue = string.Empty;
            string strQuery = @"Update " + dpLinks.TABLENAME + " Set " + strParam + " = '" + strValue + "' WHERE 1 = 1 ";
            strQuery += " AND " + dpLinks.Enabled_FULL + " =1 ";
            strQuery += " AND " + dpLinks.ID_FULL + " = " + strLinksID;
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
        /// 删除友情链接信息
        /// </summary>
        /// <param name="strParam">修改字段</param>
        /// <param name="strValue">字段值</param>
        /// <param name="strPartID">组件ID</param>
        /// <returns></returns>
        public string DeleteParams(dpLinksQuery QueryData)
        {
            string returnValue = string.Empty;
            string strQuery = @"DELETE FROM " + dpLinks.TABLENAME + " WHERE 1 = 1 ";

            Hashtable myParam = new Hashtable();
            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + dpLinks.UserID_FULL + " =@UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }
            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + dpLinks.ID_FULL + " = @ID ";
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


    }

    [Serializable()]
    public class dpLinksQuery
    {
        public string ID = string.Empty;
        public string UserID = string.Empty;
        public string LinkName = string.Empty;
        public int CurrentIndex = 1;
        public int PageSize = 12;

        public string SortField = " ID ";
        public string SortDir = " DESC ";


        public dpLinksQuery(string paramUserID, string paramLinkName)
        {
            this.UserID = paramUserID;
            this.LinkName = paramLinkName;
        }
        public dpLinksQuery()
        {
        }
    }
}

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
    public class sysUserManager
    {
        public DataSet Search(sysUserQuery QueryData)
        {
            //string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
            //+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
            string strQuery = @"SELECT "
            + sysUser.ID_FULL + ","
            + sysUser.Username_FULL + ","
            + sysUser.Email_FULL + ","
            + sysUser.Mobile_FULL + ","
            + sysUser.Password_FULL + ","
            + sysUser.Photo_FULL + ","
            + sysUser.Remark_FULL + ","
            + sysUser.Enabled_FULL + ","
            + sysUser.CreateStaff_FULL + ","
            + sysUser.CreateDate_FULL + ","
            + sysUser.ModifyStaff_FULL + ","
            + sysUser.ModifyDate_FULL
             + " FROM " + sysUser.TABLENAME + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + sysUser.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.Username.Length > 0)
            {
                strQuery += " AND " + sysUser.Username_FULL + " LIKE @Username ";
                myParam.Add("@Username", "%" + QueryData.Username.Replace(" ", "%") + "%");
            }

            if (QueryData.Mobile.Length > 0)
            {
                strQuery += " AND " + sysUser.Mobile_FULL + " = @Mobile ";
                myParam.Add("@Mobile", QueryData.Mobile);
            }
            // strQuery += ") AS " + sysUser.TABLENAME;

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
        /// 登录
        /// </summary>
        /// <param name="QueryData"></param>
        /// <returns></returns>
        public DataSet DSLogin(sysUserQuery QueryData)
        {
            string strQuery = @"SELECT "
            + sysUser.ID_FULL + ","
            + sysUser.Username_FULL + ","
            + sysUser.Email_FULL + ","
            + sysUser.Mobile_FULL + ","
            + sysUser.Password_FULL + ","
            + sysUser.Nickname_FULL + ","
            + sysRoles.Name_FULL + " as Rolename ,"
            + sysUser.Photo_FULL
            + " FROM " + sysUser.TABLENAME
            + " LEFT JOIN " + sysRoleUser.TABLENAME + " ON " + sysRoleUser.UserID_FULL + " = " + sysUser.ID_FULL
            + " LEFT JOIN " + sysRoles.TABLENAME + " ON " + sysRoleUser.RoleID_FULL + " = " + sysRoles.ID_FULL
            + " WHERE 1 = 1 "
            + " AND " + sysUser.Enabled_FULL + " = 1";

            Hashtable myParam = new Hashtable();

            if (QueryData.Username.Length > 0)
            {
                strQuery += " AND (" + sysUser.Username_FULL + " = @Username or " + sysUser.Mobile_FULL + "= @Username or " + sysUser.Email_FULL + "= @Username )";

                myParam.Add("@Username", QueryData.Username);
            }
            if (QueryData.Password.Length > 0)
            {
                strQuery += " AND " + sysUser.Password_FULL + " = @Password ";
                myParam.Add("@Password", QueryData.Password);
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
        /// 验证是否存在
        /// </summary>
        /// <param name="QueryData"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public string GetParams(sysUserQuery QueryData, string strParam)
        {
            string returnValue = string.Empty;
            string strQuery = @"SELECT " + strParam + " FROM " + sysUser.TABLENAME + " WHERE 1 = 1 ";
            strQuery += " AND " + sysUser.Enabled_FULL + " =1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + sysUser.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.Username.Length > 0)
            {
                strQuery += " AND " + sysUser.Username_FULL + "= @Username ";
                myParam.Add("@Username", QueryData.Username);
            }

            if (QueryData.Mobile.Length > 0)
            {
                strQuery += " AND " + sysUser.Mobile_FULL + " = @Mobile ";
                myParam.Add("@Mobile", QueryData.Mobile);
            }

            if (QueryData.Email.Length > 0)
            {
                strQuery += " AND " + sysUser.Email_FULL + " = @Email ";
                myParam.Add("@Email", QueryData.Email);
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


        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="strParam">修改字段</param>
        /// <param name="strValue">字段值</param>
        /// <param name="strUserID">用户ID</param>
        /// <returns></returns>
        public string UpdateParams(string strParam, string strValue, string strUserID)
        {
            string returnValue = string.Empty;
            string strQuery = @"Update " + sysUser.TABLENAME + " Set " + strParam + " = '" + strValue + "' WHERE 1 = 1 ";
            strQuery += " AND " + sysUser.Enabled_FULL + " =1 ";
            strQuery += " AND " + sysUser.ID_FULL + " = " + strUserID;
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

    }

    [Serializable()]
    public class sysUserQuery
    {
        public string ID = string.Empty;
        public string Username = string.Empty;
        public string Mobile = string.Empty;
        public string Password = string.Empty;
        public string Email = string.Empty;

        public string SortField = " ID ";
        public string SortDir = " DESC ";
        public sysUserQuery(string paramID, string paramUsername, string paramMobile, string paramPassword, string paramEmail)
        {
            this.ID = paramID;
            this.Username = paramUsername;
            this.Mobile = paramMobile;
            this.Password = paramPassword;
            this.Email = paramEmail;
        }
        public sysUserQuery()
        {
        }
    }
}

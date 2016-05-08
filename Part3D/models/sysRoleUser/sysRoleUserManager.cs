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
    public class sysRoleUserManager
    {
        public DataSet Search(sysRoleUserQuery QueryData)
        {
            //string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
            //+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
            string strQuery = @"SELECT "
            + sysRoleUser.ID_FULL + ","
            + sysRoleUser.UserID_FULL + ","
            + sysRoleUser.RoleID_FULL + ","
            + sysRoleUser.Remark_FULL + ","
            + sysRoleUser.Enabled_FULL + ","
            + sysRoleUser.CreateStaff_FULL + ","
            + sysRoleUser.CreateDate_FULL + ","
            + sysRoleUser.ModifyStaff_FULL + ","
            + sysRoleUser.ModifyDate_FULL
             + " FROM " + sysRoleUser.TABLENAME + " WHERE 1 = 1 ";


            Hashtable myParam = new Hashtable();

            if (QueryData.ID.Length > 0)
            {
                strQuery += " AND " + sysRoleUser.ID_FULL + " = @ID ";
                myParam.Add("@ID", QueryData.ID);
            }

            if (QueryData.UserID.Length > 0)
            {
                strQuery += " AND " + sysRoleUser.UserID_FULL + " = @UserID ";
                myParam.Add("@UserID", QueryData.UserID);
            }

            if (QueryData.RoleID.Length > 0)
            {
                strQuery += " AND " + sysRoleUser.RoleID_FULL + " = @RoleID ";
                myParam.Add("@RoleID", QueryData.RoleID);
            }
            // strQuery += ") AS " + sysRoleUser.TABLENAME;

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
    public class sysRoleUserQuery
    {
        public string ID = string.Empty;
        public string UserID = string.Empty;
        public string RoleID = string.Empty;

        public string SortField = " ID ";
        public string SortDir = " DESC ";
        public sysRoleUserQuery(string paramID, string paramUserID, string paramRoleID)
        {
            this.ID = paramID;
            this.UserID = paramUserID;
            this.RoleID = paramRoleID;
        }
        public sysRoleUserQuery()
        {
        }
    }
}

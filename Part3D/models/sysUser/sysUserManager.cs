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
		public DataSet Search( sysUserQuery QueryData)
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
				strQuery += " AND " +sysUser.ID_FULL  + " = @ID ";
				myParam.Add("@ID",  QueryData.ID);
			}
			
			if (QueryData.Username.Length > 0)
			{
				strQuery += " AND " +sysUser.Username_FULL  + " LIKE @Username ";
				myParam.Add("@Username", "%" + QueryData.Username.Replace(" ", "%") + "%");
			}
			
			if (QueryData.Mobile.Length > 0)
			{
				strQuery += " AND " +sysUser.Mobile_FULL  + " = @Mobile ";
				myParam.Add("@Mobile",  QueryData.Mobile);
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
	}
	
	[Serializable()]
	public class sysUserQuery
	{
		public string ID = string.Empty;
		public string Username = string.Empty;
		public string Mobile = string.Empty;
		
		public string SortField = " ID ";
		public string SortDir = " DESC ";
		public sysUserQuery(string paramID,string paramUsername,string paramMobile)
		{
			this.ID= paramID;
			this.Username= paramUsername;
			this.Mobile= paramMobile;
		}
		public sysUserQuery()
		{
		}
	}
}

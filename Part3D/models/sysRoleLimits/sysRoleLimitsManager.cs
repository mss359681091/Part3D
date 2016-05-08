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
	public class sysRoleLimitsManager
	{
		public DataSet Search(sysRoleLimitsQuery QueryData)
		{
			//string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
			//+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
			string strQuery = @"SELECT "
			+ sysRoleLimits.ID_FULL + ","
			+ sysRoleLimits.RoleID_FULL + ","
			+ sysRoleLimits.ModuleID_FULL + ","
			+ sysRoleLimits.IsLimit_FULL + ","
			+ sysRoleLimits.Quota_FULL + ","
			+ sysRoleLimits.Remark_FULL + ","
			+ sysRoleLimits.Enabled_FULL + ","
			+ sysRoleLimits.CreateStaff_FULL + ","
			+ sysRoleLimits.CreateDate_FULL + ","
			+ sysRoleLimits.ModifyStaff_FULL + ","
			+ sysRoleLimits.ModifyDate_FULL 
			 + " FROM " + sysRoleLimits.TABLENAME + " WHERE 1 = 1 ";
			
			
			Hashtable myParam = new Hashtable();
			
			if (QueryData.ID.Length > 0)
			{
				strQuery += " AND " +sysRoleLimits.ID_FULL  + " = @ID ";
				myParam.Add("@ID",  QueryData.ID);
			}
			
			if (QueryData.RoleID.Length > 0)
			{
				strQuery += " AND " +sysRoleLimits.RoleID_FULL  + " = @RoleID ";
				myParam.Add("@RoleID",  QueryData.RoleID);
			}
			
			if (QueryData.ModuleID.Length > 0)
			{
				strQuery += " AND " +sysRoleLimits.ModuleID_FULL  + " = @ModuleID ";
				myParam.Add("@ModuleID",  QueryData.ModuleID);
			}
			
			if (QueryData.IsLimit.Length > 0)
			{
				strQuery += " AND " +sysRoleLimits.IsLimit_FULL  + " = @IsLimit ";
				myParam.Add("@IsLimit",  QueryData.IsLimit);
			}
			
			if (QueryData.Quota.Length > 0)
			{
				strQuery += " AND " +sysRoleLimits.Quota_FULL  + " = @Quota ";
				myParam.Add("@Quota",  QueryData.Quota);
			}
			
			if (QueryData.Enabled.Length > 0)
			{
				strQuery += " AND " +sysRoleLimits.Enabled_FULL  + " = @Enabled ";
				myParam.Add("@Enabled",  QueryData.Enabled);
			}
			// strQuery += ") AS " + sysRoleLimits.TABLENAME;
			
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
	public class sysRoleLimitsQuery
	{
		public string ID = string.Empty;
		public string RoleID = string.Empty;
		public string ModuleID = string.Empty;
		public string IsLimit = string.Empty;
		public string Quota = string.Empty;
		public string Enabled = string.Empty;
		
		public string SortField = " ID ";
		public string SortDir = " DESC ";
		public sysRoleLimitsQuery(string paramID,string paramRoleID,string paramModuleID,string paramIsLimit,string paramQuota,string paramEnabled)
		{
			this.ID= paramID;
			this.RoleID= paramRoleID;
			this.ModuleID= paramModuleID;
			this.IsLimit= paramIsLimit;
			this.Quota= paramQuota;
			this.Enabled= paramEnabled;
		}
		public sysRoleLimitsQuery()
		{
		}
	}
}

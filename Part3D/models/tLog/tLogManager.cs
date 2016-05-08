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
	public class tLogManager
	{
		public DataSet Search(tLogQuery QueryData)
		{
			//string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
			//+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
			string strQuery = @"SELECT "
			+ tLog.ID_FULL + ","
			+ tLog.UserID_FULL + ","
			+ tLog.ModuleID_FULL + ","
			+ tLog.IPAddress_FULL + ","
			+ tLog.Description_FULL + ","
			+ tLog.Remark_FULL + ","
			+ tLog.Enabled_FULL + ","
			+ tLog.ModifyStaff_FULL + ","
			+ tLog.CreateStaff_FULL + ","
			+ tLog.ModifyDate_FULL + ","
			+ tLog.CreateDate_FULL 
			 + " FROM " + tLog.TABLENAME + " WHERE 1 = 1 ";
			
			
			Hashtable myParam = new Hashtable();
			
			if (QueryData.ID.Length > 0)
			{
				strQuery += " AND " +tLog.ID_FULL  + " = @ID ";
				myParam.Add("@ID",  QueryData.ID);
			}
			
			if (QueryData.UserID.Length > 0)
			{
				strQuery += " AND " +tLog.UserID_FULL  + " = @UserID ";
				myParam.Add("@UserID",  QueryData.UserID);
			}
			
			if (QueryData.ModuleID.Length > 0)
			{
				strQuery += " AND " +tLog.ModuleID_FULL  + " = @ModuleID ";
				myParam.Add("@ModuleID",  QueryData.ModuleID);
			}
			// strQuery += ") AS " + tLog.TABLENAME;
			
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
	public class tLogQuery
	{
		public string ID = string.Empty;
		public string UserID = string.Empty;
		public string ModuleID = string.Empty;
		
		public string SortField = " ID ";
		public string SortDir = " DESC ";
		public tLogQuery(string paramID,string paramUserID,string paramModuleID)
		{
			this.ID= paramID;
			this.UserID= paramUserID;
			this.ModuleID= paramModuleID;
		}
		public tLogQuery()
		{
		}
	}
}

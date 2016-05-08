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
	public class dpVerificationManager
	{
		public DataSet Search(dpVerificationQuery QueryData)
		{
			//string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
			//+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
			string strQuery = @"SELECT "
			+ dpVerification.ID_FULL + ","
			+ dpVerification.UserID_FULL + ","
			+ dpVerification.Code_FULL + ","
			+ dpVerification.Succeed_FULL + ","
			+ dpVerification.Verified_FULL + ","
			+ dpVerification.Remark_FULL + ","
			+ dpVerification.Enabled_FULL + ","
			+ dpVerification.CreateStaff_FULL + ","
			+ dpVerification.CreateDate_FULL + ","
			+ dpVerification.ModifyStaff_FULL + ","
			+ dpVerification.ModifyDate_FULL 
			 + " FROM " + dpVerification.TABLENAME + " WHERE 1 = 1 ";
			
			
			Hashtable myParam = new Hashtable();
			
			if (QueryData.ID.Length > 0)
			{
				strQuery += " AND " +dpVerification.ID_FULL  + " = @ID ";
				myParam.Add("@ID",  QueryData.ID);
			}
			
			if (QueryData.UserID.Length > 0)
			{
				strQuery += " AND " +dpVerification.UserID_FULL  + " = @UserID ";
				myParam.Add("@UserID",  QueryData.UserID);
			}
			
			if (QueryData.Code.Length > 0)
			{
				strQuery += " AND " +dpVerification.Code_FULL  + " LIKE @Code ";
				myParam.Add("@Code", "%" + QueryData.Code.Replace(" ", "%") + "%");
			}
			
			if (QueryData.Succeed.Length > 0)
			{
				strQuery += " AND " +dpVerification.Succeed_FULL  + " = @Succeed ";
				myParam.Add("@Succeed",  QueryData.Succeed);
			}
			
			if (QueryData.Verified.Length > 0)
			{
				strQuery += " AND " +dpVerification.Verified_FULL  + " = @Verified ";
				myParam.Add("@Verified",  QueryData.Verified);
			}
			// strQuery += ") AS " + dpVerification.TABLENAME;
			
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
	public class dpVerificationQuery
	{
		public string ID = string.Empty;
		public string UserID = string.Empty;
		public string Code = string.Empty;
		public string Succeed = string.Empty;
		public string Verified = string.Empty;
		
		public string SortField = " ID ";
		public string SortDir = " DESC ";
		public dpVerificationQuery(string paramID,string paramUserID,string paramCode,string paramSucceed,string paramVerified)
		{
			this.ID= paramID;
			this.UserID= paramUserID;
			this.Code= paramCode;
			this.Succeed= paramSucceed;
			this.Verified= paramVerified;
		}
		public dpVerificationQuery()
		{
		}
	}
}

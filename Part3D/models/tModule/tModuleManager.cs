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
	public class tModuleManager
	{
		public DataSet Search( tModuleQuery QueryData)
		{
			//string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
			//+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
			string strQuery = @"SELECT "
			+ tModule.ID_FULL + ","
			+ tModule.ParentID_FULL + ","
			+ tModule.ModuleNmae_FULL + ","
			+ tModule.NavigateUrl_FULL + ","
			+ tModule.IsPublic_FULL + ","
			+ tModule.PageCode_FULL + ","
			+ tModule.SortCode_FULL + ","
			+ tModule.Remark_FULL + ","
			+ tModule.Enabled_FULL + ","
			+ tModule.ModifyDate_FULL + ","
			+ tModule.CreateDate_FULL + ","
			+ tModule.ModifyStaff_FULL + ","
			+ tModule.CreateStaff_FULL 
			 + " FROM " + tModule.TABLENAME + " WHERE 1 = 1 ";
			
			
			Hashtable myParam = new Hashtable();
			
			if (QueryData.ID.Length > 0)
			{
				strQuery += " AND " +tModule.ID_FULL  + " = @ID ";
				myParam.Add("@ID",  QueryData.ID);
			}
			
			if (QueryData.ParentID.Length > 0)
			{
				strQuery += " AND " +tModule.ParentID_FULL  + " = @ParentID ";
				myParam.Add("@ParentID",  QueryData.ParentID);
			}
			
			if (QueryData.ModuleNmae.Length > 0)
			{
				strQuery += " AND " +tModule.ModuleNmae_FULL  + " LIKE @ModuleNmae ";
				myParam.Add("@ModuleNmae", "%" + QueryData.ModuleNmae.Replace(" ", "%") + "%");
			}
			
			if (QueryData.IsPublic.Length > 0)
			{
				strQuery += " AND " +tModule.IsPublic_FULL  + " = @IsPublic ";
				myParam.Add("@IsPublic",  QueryData.IsPublic);
			}
			
			if (QueryData.PageCode.Length > 0)
			{
				strQuery += " AND " +tModule.PageCode_FULL  + " = @PageCode ";
				myParam.Add("@PageCode",  QueryData.PageCode);
			}
			// strQuery += ") AS " + tModule.TABLENAME;
			
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
	public class tModuleQuery
	{
		public string ID = string.Empty;
		public string ParentID = string.Empty;
		public string ModuleNmae = string.Empty;
		public string IsPublic = string.Empty;
		public string PageCode = string.Empty;
		
		public string SortField = " ID ";
		public string SortDir = " DESC ";
		public tModuleQuery(string paramID,string paramParentID,string paramModuleNmae,string paramIsPublic,string paramPageCode)
		{
			this.ID= paramID;
			this.ParentID= paramParentID;
			this.ModuleNmae= paramModuleNmae;
			this.IsPublic= paramIsPublic;
			this.PageCode= paramPageCode;
		}
		public tModuleQuery()
		{
		}
	}
}

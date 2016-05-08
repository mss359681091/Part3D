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
	public class sysRolesManager 
	{
		public DataSet Search(sysRolesQuery QueryData)
		{
			//string strQuery = @"SELECT *, ROW_NUMBER() Over(order by "
			//+ QuerysData.SortField + " "  + QuerysData.SortDir + ") as ROWNUM FROM ( 
			string strQuery = @"SELECT "
			+ sysRoles.ID_FULL + ","
			+ sysRoles.Name_FULL + ","
			+ sysRoles.Remark_FULL + ","
			+ sysRoles.Enabled_FULL + ","
			+ sysRoles.CreateStaff_FULL + ","
			+ sysRoles.CreateDate_FULL + ","
			+ sysRoles.ModifyStaff_FULL + ","
			+ sysRoles.ModifyDate_FULL 
			 + " FROM " + sysRoles.TABLENAME + " WHERE 1 = 1 ";
			
			
			Hashtable myParam = new Hashtable();
			
			if (QueryData.ID.Length > 0)
			{
				strQuery += " AND " +sysRoles.ID_FULL  + " = @ID ";
				myParam.Add("@ID",  QueryData.ID);
			}
			
			if (QueryData.Name.Length > 0)
			{
				strQuery += " AND " +sysRoles.Name_FULL  + " LIKE @Name ";
				myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
			}
			// strQuery += ") AS " + sysRoles.TABLENAME;
			
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
	public class sysRolesQuery
	{
		public string ID = string.Empty;
		public string Name = string.Empty;
		
		public string SortField = " ID ";
		public string SortDir = " DESC ";
		public sysRolesQuery(string paramID,string paramName)
		{
			this.ID= paramID;
			this.Name= paramName;
		}
		public sysRolesQuery()
		{
		}
	}
}

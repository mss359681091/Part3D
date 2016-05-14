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
		public DataSet Search( dpModelFileQuery QueryData)
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
			+ dpModelFile.Remark_FULL + ","
			+ dpModelFile.Enabled_FULL + ","
			+ dpModelFile.CreateStaff_FULL + ","
			+ dpModelFile.CreateDate_FULL + ","
			+ dpModelFile.ModifyStaff_FULL + ","
			+ dpModelFile.ModifyDate_FULL 
			 + " FROM " + dpModelFile.TABLENAME + " WHERE 1 = 1 ";
			
			
			Hashtable myParam = new Hashtable();
			
			if (QueryData.ID.Length > 0)
			{
				strQuery += " AND " +dpModelFile.ID_FULL  + " = @ID ";
				myParam.Add("@ID",  QueryData.ID);
			}
			
			if (QueryData.PartID.Length > 0)
			{
				strQuery += " AND " +dpModelFile.PartID_FULL  + " = @PartID ";
				myParam.Add("@PartID",  QueryData.PartID);
			}
			
			if (QueryData.Name.Length > 0)
			{
				strQuery += " AND " +dpModelFile.Name_FULL  + " LIKE @Name ";
				myParam.Add("@Name", "%" + QueryData.Name.Replace(" ", "%") + "%");
			}
            if (QueryData.Format.Length > 0)
            {
                strQuery += " AND " + dpModelFile.Format_FULL + " = @Format ";
                myParam.Add("@Format", QueryData.Format);
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
	}
	
	[Serializable()]
	public class dpModelFileQuery
	{
		public string ID = string.Empty;
		public string PartID = string.Empty;
		public string Name = string.Empty;
        public string Format = string.Empty;
		
		public string SortField = " ID ";
		public string SortDir = " DESC ";
		public dpModelFileQuery(string paramID,string paramPartID,string paramName)
		{
			this.ID= paramID;
			this.PartID= paramPartID;
			this.Name= paramName;
		}
		public dpModelFileQuery()
		{
		}
	}
}

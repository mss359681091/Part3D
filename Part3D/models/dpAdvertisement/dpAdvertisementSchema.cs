/******************************************************************
** Copyright (c) 2016-2016 csdn-李赛赛专栏
** 创建人:
** 创建日期:2016-05-07
** 修改人:
** 修改日期:
** 描 述: 
** 版 本:1.0
**----------------------------------------------------------------------------
******************************************************************/
 using System;

namespace _3DPart.DAL.BULayer.Schema {
    
    
    public class dpAdvertisement {
        
        /// <summary>
        /// 表名
        /// </summary>
        public static string TABLENAME = "dp_Advertisement";

        /// <summary>
        /// 广告分类
        /// </summary>
        public static string ClassifyID = "ClassifyID";

        /// <summary>
        /// 广告分类
        /// </summary>
        public static string ClassifyID_FULL = "dp_Advertisement.ClassifyID";

        /// <summary>
        /// 广告分类
        /// </summary>
        public static string ClassifyID_TABLE_FULL = "dp_AdvertisementClassifyID";

        /// <summary>
        /// 
        /// </summary>
        public static string ID = "ID";
        
        /// <summary>
        /// 
        /// </summary>
        public static string ID_FULL = "dp_Advertisement.ID";
        
        /// <summary>
        /// 
        /// </summary>
        public static string ID_TABLE_FULL = "dp_AdvertisementID";
        
        /// <summary>
        /// 用户编号
        /// </summary>
        public static string UserID = "UserID";
        
        /// <summary>
        /// 用户编号
        /// </summary>
        public static string UserID_FULL = "dp_Advertisement.UserID";
        
        /// <summary>
        /// 用户编号
        /// </summary>
        public static string UserID_TABLE_FULL = "dp_AdvertisementUserID";
        
        /// <summary>
        /// 厂商
        /// </summary>
        public static string Manufacturer = "Manufacturer";
        
        /// <summary>
        /// 厂商
        /// </summary>
        public static string Manufacturer_FULL = "dp_Advertisement.Manufacturer";
        
        /// <summary>
        /// 厂商
        /// </summary>
        public static string Manufacturer_TABLE_FULL = "dp_AdvertisementManufacturer";
        
        /// <summary>
        /// 广告图
        /// </summary>
        public static string PicturePath = "PicturePath";
        
        /// <summary>
        /// 广告图
        /// </summary>
        public static string PicturePath_FULL = "dp_Advertisement.PicturePath";
        
        /// <summary>
        /// 广告图
        /// </summary>
        public static string PicturePath_TABLE_FULL = "dp_AdvertisementPicturePath";
        
        /// <summary>
        /// 开始日期
        /// </summary>
        public static string ADStartDate = "ADStartDate";
        
        /// <summary>
        /// 开始日期
        /// </summary>
        public static string ADStartDate_FULL = "dp_Advertisement.ADStartDate";
        
        /// <summary>
        /// 开始日期
        /// </summary>
        public static string ADStartDate_TABLE_FULL = "dp_AdvertisementADStartDate";
        
        /// <summary>
        /// 广告链接
        /// </summary>
        public static string ADLink = "ADLink";
        
        /// <summary>
        /// 广告链接
        /// </summary>
        public static string ADLink_FULL = "dp_Advertisement.ADLink";
        
        /// <summary>
        /// 广告链接
        /// </summary>
        public static string ADLink_TABLE_FULL = "dp_AdvertisementADLink";
        
        /// <summary>
        /// 结束日期
        /// </summary>
        public static string ADEndDate = "ADEndDate";
        
        /// <summary>
        /// 结束日期
        /// </summary>
        public static string ADEndDate_FULL = "dp_Advertisement.ADEndDate";
        
        /// <summary>
        /// 结束日期
        /// </summary>
        public static string ADEndDate_TABLE_FULL = "dp_AdvertisementADEndDate";
        
        /// <summary>
        /// 关键字
        /// </summary>
        public static string ADKeyword = "ADKeyword";
        
        /// <summary>
        /// 关键字
        /// </summary>
        public static string ADKeyword_FULL = "dp_Advertisement.ADKeyword";
        
        /// <summary>
        /// 关键字
        /// </summary>
        public static string ADKeyword_TABLE_FULL = "dp_AdvertisementADKeyword";
        
        /// <summary>
        /// 投放位置
        /// </summary>
        public static string ADPosition = "ADPosition";
        
        /// <summary>
        /// 投放位置
        /// </summary>
        public static string ADPosition_FULL = "dp_Advertisement.ADPosition";
        
        /// <summary>
        /// 投放位置
        /// </summary>
        public static string ADPosition_TABLE_FULL = "dp_AdvertisementADPosition";

     



        /// <summary>
        /// 备注
        /// </summary>
        public static string Remark = "Remark";
        
        /// <summary>
        /// 备注
        /// </summary>
        public static string Remark_FULL = "dp_Advertisement.Remark";
        
        /// <summary>
        /// 备注
        /// </summary>
        public static string Remark_TABLE_FULL = "dp_AdvertisementRemark";
        
        /// <summary>
        /// 
        /// </summary>
        public static string Enabled = "Enabled";
        
        /// <summary>
        /// 
        /// </summary>
        public static string Enabled_FULL = "dp_Advertisement.Enabled";
        
        /// <summary>
        /// 
        /// </summary>
        public static string Enabled_TABLE_FULL = "dp_AdvertisementEnabled";
        
        /// <summary>
        /// 
        /// </summary>
        public static string CreateStaff = "CreateStaff";
        
        /// <summary>
        /// 
        /// </summary>
        public static string CreateStaff_FULL = "dp_Advertisement.CreateStaff";
        
        /// <summary>
        /// 
        /// </summary>
        public static string CreateStaff_TABLE_FULL = "dp_AdvertisementCreateStaff";
        
        /// <summary>
        /// 
        /// </summary>
        public static string CreateDate = "CreateDate";
        
        /// <summary>
        /// 
        /// </summary>
        public static string CreateDate_FULL = "dp_Advertisement.CreateDate";
        
        /// <summary>
        /// 
        /// </summary>
        public static string CreateDate_TABLE_FULL = "dp_AdvertisementCreateDate";
        
        /// <summary>
        /// 
        /// </summary>
        public static string ModifyStaff = "ModifyStaff";
        
        /// <summary>
        /// 
        /// </summary>
        public static string ModifyStaff_FULL = "dp_Advertisement.ModifyStaff";
        
        /// <summary>
        /// 
        /// </summary>
        public static string ModifyStaff_TABLE_FULL = "dp_AdvertisementModifyStaff";
        
        /// <summary>
        /// 
        /// </summary>
        public static string ModifyDate = "ModifyDate";
        
        /// <summary>
        /// 
        /// </summary>
        public static string ModifyDate_FULL = "dp_Advertisement.ModifyDate";
        
        /// <summary>
        /// 
        /// </summary>
        public static string ModifyDate_TABLE_FULL = "dp_AdvertisementModifyDate";
    }
}

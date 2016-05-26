

using System;

namespace _3DPart.DAL.BULayer.Data
{



    [Serializable()]
    public class dpAdvertisementData
    {
        public dpAdvertisementData()
        {

        }

        private string _Name = string.Empty;
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private int _ID = 0;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _UserID = 0;
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _Manufacturer = string.Empty;
        /// <summary>
        /// 厂商
        /// </summary>
        public string Manufacturer
        {
            get { return _Manufacturer; }
            set { _Manufacturer = value; }
        }

        private string _PicturePath = string.Empty;
        /// <summary>
        /// 广告图
        /// </summary>
        public string PicturePath
        {
            get { return _PicturePath; }
            set { _PicturePath = value; }
        }

        private string _ADStartDate = string.Empty;
        /// <summary>
        /// 开始日期
        /// </summary>
        public string ADStartDate
        {
            get { return _ADStartDate; }
            set { _ADStartDate = value; }
        }

        private string _ADLink = string.Empty;
        /// <summary>
        /// 广告链接
        /// </summary>
        public string ADLink
        {
            get { return _ADLink; }
            set { _ADLink = value; }
        }

        private string _ADEndDate = string.Empty;
        /// <summary>
        /// 结束日期
        /// </summary>
        public string ADEndDate
        {
            get { return _ADEndDate; }
            set { _ADEndDate = value; }
        }

        private string _ADKeyword = string.Empty;
        /// <summary>
        /// 关键字
        /// </summary>
        public string ADKeyword
        {
            get { return _ADKeyword; }
            set { _ADKeyword = value; }
        }

        private string _ADPosition = string.Empty;
        /// <summary>
        /// 投放位置
        /// </summary>
        public string ADPosition
        {
            get { return _ADPosition; }
            set { _ADPosition = value; }
        }

        private int _ClassifyID = 0;
        /// <summary>
        /// 广告分类
        /// </summary>
        public int ClassifyID
        {
            get { return _ClassifyID; }
            set { _ClassifyID = value; }
        }

        private string _Remark = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        private int _Enabled = 0;
        /// <summary>
        /// 
        /// </summary>
        public int Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        private int _CreateStaff = 0;
        /// <summary>
        /// 
        /// </summary>
        public int CreateStaff
        {
            get { return _CreateStaff; }
            set { _CreateStaff = value; }
        }

        private string _CreateDate = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        private int _ModifyStaff = 0;
        /// <summary>
        /// 
        /// </summary>
        public int ModifyStaff
        {
            get { return _ModifyStaff; }
            set { _ModifyStaff = value; }
        }

        private string _ModifyDate = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string ModifyDate
        {
            get { return _ModifyDate; }
            set { _ModifyDate = value; }
        }

    }
}

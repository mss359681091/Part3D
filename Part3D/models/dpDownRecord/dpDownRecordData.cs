
using System;

namespace _3DPart.DAL.BULayer.Data
{
    [Serializable()]
    public class dpDownRecordData
    {
        public dpDownRecordData()
        {

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

        private int _PartID = 0;
        /// <summary>
        /// 组件ID
        /// </summary>
        public int PartID
        {
            get { return _PartID; }
            set { _PartID = value; }
        }

        private int _UserID = 0;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
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

        //private int _partid1 = 0;
        ///// <summary>
        ///// 组件ID
        ///// </summary>
        //public int partid1
        //{
        //    get { return _partid1; }
        //    set { _partid1 = value; }
        //}

        private string _PreviewSmall = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string PreviewSmall
        {
            get { return _PreviewSmall; }
            set { _PreviewSmall = value; }
        }

        private string _classname = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string classname
        {
            get { return _classname; }
            set { _classname = value; }
        }

        private string _name = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _partcount = 0;
        /// <summary>
        /// 
        /// </summary>
        public int partcount
        {
            get { return _partcount; }
            set { _partcount = value; }
        }


        private string _lastdownload = DateTime.Now.ToString();
        /// <summary>
        /// 
        /// </summary>
        public string lastdownload
        {
            get { return _lastdownload; }
            set { _lastdownload = value; }
        }
    }
}



using System;

namespace _3DPart.DAL.BULayer.Data
{



    [Serializable()]
    public class dpLinksData
    {
        public dpLinksData()
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

        private int _UserID = 0;
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _LinkName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string LinkName
        {
            get { return _LinkName; }
            set { _LinkName = value; }
        }

        private string _LinkUrl = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string LinkUrl
        {
            get { return _LinkUrl; }
            set { _LinkUrl = value; }
        }

        private string _ImgUrl = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string ImgUrl
        {
            get { return _ImgUrl; }
            set { _ImgUrl = value; }
        }

        private string _Username = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        private string _Remark = string.Empty;
        /// <summary>
        /// 
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

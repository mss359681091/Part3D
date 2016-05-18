/******************************************************************
** Copyright (c) 2005 -2007 灵动软件研发部
** 创建人:
** 创建日期:
** 修改人:
** 修改日期:
** 描 述: 
** 版 本:1.0
**----------------------------------------------------------------------------
******************************************************************/

using System;

namespace _3DPart.DAL.BULayer
{

    public class dpPartData
    {
        public dpPartData()
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

        private int _ParentID = 0;
        /// <summary>
        /// 父组件编号
        /// </summary>
        public int ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
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

        private int _ClassifyID = 0;
        /// <summary>
        /// 分类ID
        /// </summary>
        public int ClassifyID
        {
            get { return _ClassifyID; }
            set { _ClassifyID = value; }
        }

        private string _Name = string.Empty;
        /// <summary>
        /// 组件名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Preview = string.Empty;
        /// <summary>
        /// 预览图
        /// </summary>
        public string Preview
        {
            get { return _Preview; }
            set { _Preview = value; }
        }

        private string _PreviewSmall = string.Empty;
        /// <summary>
        /// 小预览图
        /// </summary>
        public string PreviewSmall
        {
            get { return _PreviewSmall; }
            set { _PreviewSmall = value; }
        }

        private string _Description = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private int _Limits = 0;
        /// <summary>
        /// 权限
        /// </summary>
        public int Limits
        {
            get { return _Limits; }
            set { _Limits = value; }
        }

        private string _Keyword = string.Empty;
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword
        {
            get { return _Keyword; }
            set { _Keyword = value; }
        }

        private int _Accesslog = 0;
        /// <summary>
        /// 关键字
        /// </summary>
        public int Accesslog
        {
            get { return _Accesslog; }
            set { _Accesslog = value; }
        }

        private string _classname = string.Empty;
        /// <summary>
        /// 类别
        /// </summary>
        public string classname
        {
            get { return _classname; }
            set { _classname = value; }
        }

        private int _mycount = 0;
        /// <summary>
        /// 我的资源总数
        /// </summary>
        public int mycount
        {
            get { return _mycount; }
            set { _mycount = value; }
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

        private DateTime _CreateDate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>

        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        private string _CreateDate1 = DateTime.Now.ToString();
        /// <summary>
        /// 
        /// </summary>

        public string CreateDate1
        {
            get { return _CreateDate1; }
            set { _CreateDate1 = value; }
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

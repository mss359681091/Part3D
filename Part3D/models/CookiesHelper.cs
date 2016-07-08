using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Part3D.models
{
    public class CookiesHelper
    {
        /**//// <summary>
            /// 获取数据缓存
            /// </summary>
            /// <param name="CacheKey">键</param>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        ///**//// <summary>
        //    /// 设置数据缓存
        //    /// </summary>
        //public static void SetCache(string CacheKey, object objObject)
        //{
        //    System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        //    objCache.Insert(CacheKey, objObject);
        //}

        ///**//// <summary>
        //    /// 设置数据缓存
        //    /// </summary>
        //public static void SetCache(string CacheKey, object objObject, TimeSpan Timeout)
        //{
        //    System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        //    objCache.Insert(CacheKey, objObject, null, DateTime.MaxValue, Timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        //}

        ///**//// <summary>
        //    /// 设置数据缓存
        //    /// </summary>
        //public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        //{
        //    System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        //    objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        //}

        /// <summary>
        /// 设置以缓存依赖的方式缓存数据
        /// </summary>
        /// <param name="CacheKey">索引键值</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="cacheDepen">依赖对象</param>
        public static void SetCache(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(
                CacheKey,
                objObject,
                dep,
                System.Web.Caching.Cache.NoAbsoluteExpiration,//从不过期
                System.Web.Caching.Cache.NoSlidingExpiration,//禁用可调过期
                System.Web.Caching.CacheItemPriority.Default,
                null);
        }

        /**//// <summary>
            /// 移除指定数据缓存
            /// </summary>
        public static void RemoveAllCache(string CacheKey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            _cache.Remove(CacheKey);
        }

        /**//// <summary>
            /// 移除全部缓存
            /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }

        
        public static DataSet ReturnDataSet(string CacheKey, string TableName, DataSet ds)
        {
            //缓存
            DataSet myDataSet = new DataSet();
            object objModel = CookiesHelper.GetCache(CacheKey);//从缓存中获取
            if (objModel == null)//缓存里没有
            {
                myDataSet = ds;
                //objModel = ds;//把数据存入缓存
                if (myDataSet != null)
                {
                    //依赖数据库codematic中的P_Product表变化 来更新缓存
                    System.Web.Caching.SqlCacheDependency dep = new System.Web.Caching.SqlCacheDependency(ConfigurationManager.AppSettings["DataBase"].ToString(), TableName);
                    CookiesHelper.SetCache(CacheKey, myDataSet, dep);//写入缓存
                }
            }
            else
            {
                myDataSet = (DataSet)objModel;
            }
            return myDataSet;
        }

        public static bool IsCache(string CacheKey)
        {
            bool returnValue = false;
            object objModel = CookiesHelper.GetCache(CacheKey);//从缓存中获取
            if (objModel != null)//缓存里没有
            {
                returnValue = true;
            }
            return returnValue;
        }

    }
}
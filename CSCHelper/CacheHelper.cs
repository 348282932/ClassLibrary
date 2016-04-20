using System;
using System.Collections;
using System.Web;

namespace MyClassLibrary
{
    public class CacheHelper
    {
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        public static Object GetCache(String CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;

            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        /// <param name="objObject">插入缓存的对象</param>
        public static void SetCache(String CacheKey, Object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;

            objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        /// <param name="objObject">插入缓存的对象</param>
        /// <param name="Timeout">最后一次访问时间与过期时间的间隔</param>
        public static void SetCache(String CacheKey, Object objObject, TimeSpan Timeout)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;

            objCache.Insert(CacheKey, objObject, null, DateTime.MaxValue, Timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        /// <param name="objObject">插入缓存的对象</param>
        /// <param name="absoluteExpiration">对象到期并从缓存中移除的时间</param>
        /// <param name="Timeout">最后一次访问时间与过期时间的间隔</param>
        public static void SetCache(String CacheKey, Object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;

            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        public static void RemoveAllCache(String CacheKey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;

            _cache.Remove(CacheKey);
        }

        /// <summary>
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
    }
}
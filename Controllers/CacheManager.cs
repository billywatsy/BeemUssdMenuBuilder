using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeNodeSample.Controllers
{

    public class CacheManager
    {
        /// <summary>
        /// Used to seperator cacheKey Name Only on instance where merchant group code is not null
        /// </summary>
        public static void AddCache(string cacheKeyName, object itemToCache, DateTime absoluteExpiration)
        {
          
            if (IsCacheKeyExist(cacheKeyName))
            {
                HttpContext.Current.Cache.Remove(cacheKeyName);
            }
            HttpContext.Current.Cache.Add(cacheKeyName, itemToCache, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
        }

        public static T GetCache<T>(string cacheKeyName) where T : class
        {
            return HttpContext.Current.Cache[cacheKeyName] as T;
        }
         

        public static List<T> GetCacheList<T>(string cacheKeyName) where T : class
        {
            return HttpContext.Current.Cache[cacheKeyName] as List<T>;
        }

        public static void RemoveCache(string cacheKeyName)
        {
            HttpContext.Current.Cache.Remove(cacheKeyName);
        }
         
        private static bool IsCacheKeyExist(string cacheKeyName)
        {
            return HttpContext.Current.Cache[cacheKeyName] != null;
        }
        public static void Update(string cacheKeyName , object newValue)
        {
            HttpContext.Current.Cache[cacheKeyName] = newValue;
        }

    }
}
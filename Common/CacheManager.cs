using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


public class CacheManager
{
    public static void SetCache(string key, object data, int min = 10)
    {
        HttpContext.Current.Cache.Insert(key, data, null, DateTime.Now.AddMinutes(min), TimeSpan.Zero);
    }

    public static object GetCache(string key)
    {
        return HttpContext.Current.Cache[key];
    }

    public static void ClearCache(string key)
    {
        HttpContext.Current.Cache.Remove(key);
    }
}


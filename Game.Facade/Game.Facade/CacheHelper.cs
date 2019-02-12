using System;
using System.Web;
using System.Web.Caching;

namespace Game.Facade
{
	public class CacheHelper
	{
		public static void AddCache(string key, object value)
		{
			Cache cache = HttpRuntime.Cache;
			cache.Insert(key, value, null, DateTime.Now.AddMinutes(1.0), TimeSpan.Zero);
		}
	}
}

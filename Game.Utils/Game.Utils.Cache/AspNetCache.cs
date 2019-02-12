using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace Game.Utils.Cache
{
	public class AspNetCache : ICache
	{
		public string ExpireTimeStr
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public object this[string key]
		{
			get
			{
				return GetValue(key);
			}
		}

		public void Add(Dictionary<string, object> dic)
		{
			foreach (KeyValuePair<string, object> item in dic)
			{
				HttpContext.Current.Cache.Insert(item.Key, item.Value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
			}
		}

		public void Add(Dictionary<string, object> dic, int? timeout)
		{
			if (!timeout.HasValue || timeout.Value == 0)
			{
				timeout = 20;
			}
			foreach (KeyValuePair<string, object> item in dic)
			{
				HttpContext.Current.Cache.Insert(item.Key, item.Value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, timeout.Value, 0));
			}
		}

		public void Delete()
		{
			throw new NotImplementedException();
		}

		public void Delete(string key)
		{
			HttpContext.Current.Cache.Remove(key);
		}

		public void Update(Dictionary<string, object> dic)
		{
			foreach (KeyValuePair<string, object> item in dic)
			{
				HttpContext.Current.Cache.Insert(item.Key, item.Value);
			}
		}

		public object GetValue(string key)
		{
			return HttpContext.Current.Cache.Get(key);
		}

		public void Add(string key, object value)
		{
			HttpContext.Current.Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
		}

		public void Add(string key, object value, int? timeout)
		{
			if (!timeout.HasValue || timeout.Value == 0)
			{
				timeout = 20;
			}
			HttpContext.Current.Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, timeout.Value, 0));
		}

		public void Update(string key, object value)
		{
			HttpContext.Current.Cache.Insert(key, value);
		}
	}
}

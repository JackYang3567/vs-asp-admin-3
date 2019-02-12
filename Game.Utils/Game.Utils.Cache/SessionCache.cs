using System.Collections.Generic;
using System.Web;

namespace Game.Utils.Cache
{
	public class SessionCache : ICache
	{
		public string ExpireTimeStr
		{
			get
			{
				return "SessionExpireDate";
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
				HttpContext.Current.Session[item.Key] = item.Value;
			}
		}

		public void Add(Dictionary<string, object> dic, int? timeout)
		{
			if (timeout.HasValue && timeout > 0)
			{
				HttpContext.Current.Session.Timeout = timeout.Value;
				foreach (KeyValuePair<string, object> item in dic)
				{
					HttpContext.Current.Session[item.Key] = item.Value;
				}
			}
		}

		public void Delete()
		{
			HttpContext.Current.Session.RemoveAll();
		}

		public void Delete(string key)
		{
			HttpContext.Current.Session.Remove(key);
		}

		public void Update(Dictionary<string, object> dic)
		{
			foreach (KeyValuePair<string, object> item in dic)
			{
				HttpContext.Current.Session[item.Key] = item.Value;
			}
		}

		public object GetValue(string key)
		{
			return HttpContext.Current.Session[key];
		}

		public void Add(string key, object value)
		{
			HttpContext.Current.Session[key] = value;
		}

		public void Add(string key, object value, int? timeout)
		{
			if (timeout.HasValue && timeout > 0)
			{
				HttpContext.Current.Session.Timeout = timeout.Value;
				HttpContext.Current.Session[key] = value;
			}
		}

		public void Update(string key, object value)
		{
			HttpContext.Current.Session[key] = value;
		}
	}
}

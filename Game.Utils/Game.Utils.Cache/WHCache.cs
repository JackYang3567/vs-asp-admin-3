using System.Collections.Generic;

namespace Game.Utils.Cache
{
	public class WHCache
	{
		private static volatile WHCache _instance;

		private static object lockObj = new object();

		public static WHCache Default
		{
			get
			{
				if (_instance == null)
				{
					lock (lockObj)
					{
						if (_instance == null)
						{
							_instance = new WHCache();
						}
					}
				}
				return _instance;
			}
		}

		public void Save<CacheType>(string key, object value) where CacheType : ICache, new()
		{
			ICache cache = (ICache)(object)new CacheType();
			cache.Add(key, value);
		}

		public void Save<CacheType>(string key, object value, int? timeout) where CacheType : ICache, new()
		{
			ICache cache = (ICache)(object)new CacheType();
			cache.Add(key, value, timeout);
		}

		public void Save<CacheType>(Dictionary<string, object> dic) where CacheType : ICache, new()
		{
			ICache cache = (ICache)(object)new CacheType();
			cache.Add(dic);
		}

		public void Save<CacheType>(Dictionary<string, object> dic, int? timeout) where CacheType : ICache, new()
		{
			ICache cache = (ICache)(object)new CacheType();
			cache.Add(dic, timeout);
		}

		public object Get<CacheType>(string key) where CacheType : ICache, new()
		{
			ICache cache = (ICache)(object)new CacheType();
			return cache.GetValue(key);
		}

		public void Clear<CacheType>() where CacheType : ICache, new()
		{
			ICache cache = (ICache)(object)new CacheType();
			cache.Delete();
		}

		public void Delete<CacheType>(string key) where CacheType : ICache, new()
		{
			ICache cache = (ICache)(object)new CacheType();
			cache.Delete(key);
		}
	}
}

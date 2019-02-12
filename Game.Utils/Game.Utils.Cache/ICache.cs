using System.Collections.Generic;

namespace Game.Utils.Cache
{
	public interface ICache
	{
		string ExpireTimeStr
		{
			get;
		}

		object this[string key]
		{
			get;
		}

		void Add(string key, object value);

		void Add(string key, object value, int? timeout);

		void Add(Dictionary<string, object> dic);

		void Add(Dictionary<string, object> dic, int? timeout);

		void Delete();

		void Delete(string key);

		void Update(string key, object value);

		void Update(Dictionary<string, object> dic);

		object GetValue(string key);
	}
}

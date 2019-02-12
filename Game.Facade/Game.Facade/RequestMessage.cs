using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Game.Facade
{
	public class RequestMessage
	{
		private Dictionary<string, object> _data = new Dictionary<string, object>();

		public int msgid
		{
			get;
			set;
		}

		public Dictionary<string, object> content
		{
			get
			{
				return _data;
			}
			set
			{
				_data = value;
			}
		}

		public RequestMessage(int id)
		{
			msgid = id;
		}

		public void AddDataItem(string key, object value)
		{
			_data.Add(key, value);
		}

		public void SetDataItem(string key, object value)
		{
			_data[key] = value;
		}

		public string SerializeToJson()
		{
			return new JavaScriptSerializer().Serialize(this);
		}

		public string Post()
		{
			string param = SerializeToJson();
			return HttpHelper.HttpRequest(AppConfig.ServerUrl, param, "post", "GB2312");
		}
	}
}

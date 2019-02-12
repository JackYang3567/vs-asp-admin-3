using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Game.Facade
{
	public class AjaxJson
	{
		private int _code;

		private string _msg;

		private Dictionary<string, object> _data = new Dictionary<string, object>();

		public int code
		{
			get
			{
				return _code;
			}
			set
			{
				_code = value;
			}
		}

		public string msg
		{
			get
			{
				return _msg;
			}
			set
			{
				_msg = value;
			}
		}

		public Dictionary<string, object> data
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

		public AjaxJson()
		{
			_code = 0;
		}

		public void AddDataItem(string key, object value)
		{
			_data.Add(key, value);
		}

		public void SetDataItem(string key, object value)
		{
			_data[key] = value;
		}

		public object GetDataItemValue(string key, object value)
		{
			return _data[key];
		}

		public string SerializeToJson()
		{
			return new JavaScriptSerializer().Serialize(this);
		}
	}
}

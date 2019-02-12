using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiguang.JPush.Model
{
	public class Trigger
	{
		[JsonProperty("start")]
		public string StartDate
		{
			get;
			set;
		}

		[JsonProperty("end")]
		public string EndDate
		{
			get;
			set;
		}

		[JsonProperty("time")]
		public string TriggerTime
		{
			get;
			set;
		}

		[JsonProperty("time_unit")]
		public string TimeUnit
		{
			get;
			set;
		}

		[JsonProperty("frequency")]
		public int Frequency
		{
			get;
			set;
		}

		[JsonProperty("point")]
		public List<string> TimeList
		{
			get;
			set;
		}

		private string GetJson()
		{
			return JsonConvert.SerializeObject(this, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore
			});
		}

		public override string ToString()
		{
			return GetJson();
		}
	}
}

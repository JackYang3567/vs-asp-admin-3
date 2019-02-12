using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
	public class SmsMessage
	{
		[JsonProperty("content")]
		public string Content
		{
			get;
			set;
		}

		[JsonProperty("delay_time", DefaultValueHandling = DefaultValueHandling.Include)]
		public int DelayTime
		{
			get;
			set;
		}
	}
}

using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
	public class Notification
	{
		[JsonProperty("alert")]
		public string Alert
		{
			get;
			set;
		}

		[JsonProperty("android", NullValueHandling = NullValueHandling.Ignore)]
		public Android Android
		{
			get;
			set;
		}

		[JsonProperty("ios", NullValueHandling = NullValueHandling.Ignore)]
		public IOS IOS
		{
			get;
			set;
		}
	}
}

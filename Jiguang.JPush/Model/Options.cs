using Newtonsoft.Json;

namespace Jiguang.JPush.Model
{
	public class Options
	{
		[JsonProperty("sendno", NullValueHandling = NullValueHandling.Ignore)]
		public int? SendNo
		{
			get;
			set;
		}

		[JsonProperty("time_to_live", NullValueHandling = NullValueHandling.Ignore)]
		public int? TimeToLive
		{
			get;
			set;
		}

		[JsonProperty("override_msg_id", NullValueHandling = NullValueHandling.Ignore)]
		public long? OverrideMessageId
		{
			get;
			set;
		}

		[JsonProperty("apns_production", DefaultValueHandling = DefaultValueHandling.Include)]
		public bool IsApnsProduction
		{
			get;
			set;
		}

		[JsonProperty("apns_collapse_id", NullValueHandling = NullValueHandling.Ignore)]
		public string ApnsCollapseId
		{
			get;
			set;
		}

		[JsonProperty("big_push_duration", NullValueHandling = NullValueHandling.Ignore)]
		public int? BigPushDuration
		{
			get;
			set;
		}
	}
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiguang.JPush.Model
{
	public class Audience
	{
		[JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Tag
		{
			get;
			set;
		}

		[JsonProperty("tag_and", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> TagAnd
		{
			get;
			set;
		}

		[JsonProperty("tag_not", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> TagNot
		{
			get;
			set;
		}

		[JsonProperty("alias", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Alias
		{
			get;
			set;
		}

		[JsonProperty("registration_id", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> RegistrationId
		{
			get;
			set;
		}

		[JsonProperty("segment", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Segment
		{
			get;
			set;
		}

		[JsonProperty("abtest", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Abtest
		{
			get;
			set;
		}
	}
}

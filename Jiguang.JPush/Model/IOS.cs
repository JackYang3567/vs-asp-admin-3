using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiguang.JPush.Model
{
	public class IOS
	{
		[JsonProperty("alert")]
		public object Alert
		{
			get;
			set;
		}

		[JsonProperty("sound")]
		public string Sound
		{
			get;
			set;
		}

		[JsonProperty("badge")]
		public string Badge
		{
			get;
			set;
		}

		[JsonProperty("content-available")]
		public bool ContentAvailable
		{
			get;
			set;
		}

		[JsonProperty("mutable-content")]
		public bool MutableContent
		{
			get;
			set;
		}

		[JsonProperty("category")]
		public string Category
		{
			get;
			set;
		}

		[JsonProperty("extras")]
		public Dictionary<string, object> Extras
		{
			get;
			set;
		}
	}
}

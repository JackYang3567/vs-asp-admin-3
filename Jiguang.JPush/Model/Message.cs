using Newtonsoft.Json;
using System.Collections;

namespace Jiguang.JPush.Model
{
	public class Message
	{
		[JsonProperty("msg_content")]
		public string Content
		{
			get;
			set;
		}

		[JsonProperty("title")]
		public string Title
		{
			get;
			set;
		}

		[JsonProperty("content_type")]
		public string ContentType
		{
			get;
			set;
		}

		[JsonProperty("extras")]
		public IDictionary Extras
		{
			get;
			set;
		}
	}
}

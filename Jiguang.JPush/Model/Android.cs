using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiguang.JPush.Model
{
	public class Android
	{
		[JsonProperty("alert")]
		public string Alert
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

		[JsonProperty("builder_id")]
		public int BuilderId
		{
			get;
			set;
		}

		[JsonProperty("priority")]
		public int Priority
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

		[JsonProperty("style")]
		public int Style
		{
			get;
			set;
		}

		[JsonProperty("alert_type")]
		public int AlertType
		{
			get;
			set;
		}

		[JsonProperty("big_text")]
		public string BigText
		{
			get;
			set;
		}

		[JsonProperty("inbox")]
		public Dictionary<string, object> Inbox
		{
			get;
			set;
		}

		[JsonProperty("big_pic_path")]
		public string BigPicturePath
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

namespace RestSharp.Serializers
{
	public class JsonSerializer : ISerializer
	{
		public string DateFormat
		{
			get;
			set;
		}

		public string RootElement
		{
			get;
			set;
		}

		public string Namespace
		{
			get;
			set;
		}

		public string ContentType
		{
			get;
			set;
		}

		public JsonSerializer()
		{
			ContentType = "application/json";
		}

		public string Serialize(object obj)
		{
			return SimpleJson.SerializeObject(obj);
		}
	}
}

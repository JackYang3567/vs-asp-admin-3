namespace RestSharp.Deserializers
{
	public interface IDeserializer
	{
		string RootElement
		{
			get;
			set;
		}

		string Namespace
		{
			get;
			set;
		}

		string DateFormat
		{
			get;
			set;
		}

		T Deserialize<T>(IRestResponse response);
	}
}

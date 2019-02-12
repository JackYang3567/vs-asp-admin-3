namespace RestSharp.Serializers
{
	public interface ISerializer
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

		string ContentType
		{
			get;
			set;
		}

		string Serialize(object obj);
	}
}

namespace RestSharp
{
	public class Parameter
	{
		public string Name
		{
			get;
			set;
		}

		public object Value
		{
			get;
			set;
		}

		public ParameterType Type
		{
			get;
			set;
		}

		public override string ToString()
		{
			return string.Format("{0}={1}", Name, Value);
		}
	}
}

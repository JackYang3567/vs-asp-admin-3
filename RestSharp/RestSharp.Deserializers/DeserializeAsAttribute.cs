using System;

namespace RestSharp.Deserializers
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class DeserializeAsAttribute : Attribute
	{
		public string Name
		{
			get;
			set;
		}

		public bool Attribute
		{
			get;
			set;
		}
	}
}

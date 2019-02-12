using RestSharp.Extensions;
using System;
using System.Globalization;

namespace RestSharp.Serializers
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class SerializeAsAttribute : Attribute
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

		public CultureInfo Culture
		{
			get;
			set;
		}

		public NameStyle NameStyle
		{
			get;
			set;
		}

		public int Index
		{
			get;
			set;
		}

		public SerializeAsAttribute()
		{
			NameStyle = NameStyle.AsIs;
			Index = 2147483647;
			Culture = CultureInfo.InvariantCulture;
		}

		public string TransformName(string input)
		{
			string text = Name ?? input;
			switch (NameStyle)
			{
			case NameStyle.CamelCase:
				return text.ToCamelCase(Culture);
			case NameStyle.PascalCase:
				return text.ToPascalCase(Culture);
			case NameStyle.LowerCase:
				return text.ToLower();
			default:
				return input;
			}
		}
	}
}

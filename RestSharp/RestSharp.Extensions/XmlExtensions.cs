using System.Xml.Linq;

namespace RestSharp.Extensions
{
	public static class XmlExtensions
	{
		public static XName AsNamespaced(this string name, string @namespace)
		{
			XName result = name;
			if (@namespace.HasValue())
			{
				result = XName.Get(name, @namespace);
			}
			return result;
		}
	}
}

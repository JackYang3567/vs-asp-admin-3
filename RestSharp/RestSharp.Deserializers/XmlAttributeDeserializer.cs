using RestSharp.Extensions;
using System.Reflection;
using System.Xml.Linq;

namespace RestSharp.Deserializers
{
	public class XmlAttributeDeserializer : XmlDeserializer
	{
		protected override object GetValueFromXml(XElement root, XName name, PropertyInfo prop)
		{
			bool flag = false;
			DeserializeAsAttribute attribute = prop.GetAttribute<DeserializeAsAttribute>();
			if (attribute != null)
			{
				string name2 = attribute.Name;
				name = ((name2 != null) ? ((XName)name2) : name);
				flag = attribute.Attribute;
			}
			if (flag)
			{
				XAttribute attributeByName = GetAttributeByName(root, name);
				if (attributeByName != null)
				{
					return attributeByName.Value;
				}
			}
			return base.GetValueFromXml(root, name, prop);
		}
	}
}

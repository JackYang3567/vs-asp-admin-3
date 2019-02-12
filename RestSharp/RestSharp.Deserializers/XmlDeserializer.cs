using RestSharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace RestSharp.Deserializers
{
	public class XmlDeserializer : IDeserializer
	{
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

		public string DateFormat
		{
			get;
			set;
		}

		public CultureInfo Culture
		{
			get;
			set;
		}

		public XmlDeserializer()
		{
			Culture = CultureInfo.InvariantCulture;
		}

		public virtual T Deserialize<T>(IRestResponse response)
		{
			if (!string.IsNullOrEmpty(response.Content))
			{
				XDocument xDocument = XDocument.Parse(response.Content);
				XElement root = xDocument.Root;
				if (RootElement.HasValue() && xDocument.Root != null)
				{
					root = xDocument.Root.Element(RootElement.AsNamespaced(Namespace));
				}
				if (!Namespace.HasValue())
				{
					RemoveNamespace(xDocument);
				}
				T val = Activator.CreateInstance<T>();
				Type type = val.GetType();
				val = ((!type.IsSubclassOfRawGeneric(typeof(List<>))) ? ((T)Map(val, root)) : ((T)HandleListDerivative(val, root, type.Name, type)));
				return val;
			}
			return default(T);
		}

		private void RemoveNamespace(XDocument xdoc)
		{
			foreach (XElement item in xdoc.Root.DescendantsAndSelf())
			{
				if (item.Name.Namespace != XNamespace.None)
				{
					item.Name = XNamespace.None.GetName(item.Name.LocalName);
				}
				if (item.Attributes().Any((XAttribute a) => a.IsNamespaceDeclaration || a.Name.Namespace != XNamespace.None))
				{
					item.ReplaceAttributes(from a in item.Attributes()
					select a.IsNamespaceDeclaration ? null : ((a.Name.Namespace != XNamespace.None) ? new XAttribute(XNamespace.None.GetName(a.Name.LocalName), a.Value) : a));
				}
			}
		}

		protected virtual object Map(object x, XElement root)
		{
			Type type = x.GetType();
			PropertyInfo[] properties = type.GetProperties();
			PropertyInfo[] array = properties;
			foreach (PropertyInfo propertyInfo in array)
			{
				Type type2 = propertyInfo.PropertyType;
				if ((type2.IsPublic || type2.IsNestedPublic) && propertyInfo.CanWrite)
				{
					object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DeserializeAsAttribute), false);
					XName name;
					if (customAttributes.Length > 0)
					{
						DeserializeAsAttribute deserializeAsAttribute = (DeserializeAsAttribute)customAttributes[0];
						name = deserializeAsAttribute.Name.AsNamespaced(Namespace);
					}
					else
					{
						name = propertyInfo.Name.AsNamespaced(Namespace);
					}
					object valueFromXml = GetValueFromXml(root, name, propertyInfo);
					if (valueFromXml == null)
					{
						if (type2.IsGenericType)
						{
							Type type3 = type2.GetGenericArguments()[0];
							XElement elementByName = GetElementByName(root, type3.Name);
							IList list = (IList)Activator.CreateInstance(type2);
							if (elementByName != null)
							{
								IEnumerable<XElement> elements = root.Elements(elementByName.Name);
								PopulateListFromElements(type3, elements, list);
							}
							propertyInfo.SetValue(x, list, null);
						}
					}
					else
					{
						if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(Nullable<>))
						{
							if (valueFromXml == null || string.IsNullOrEmpty(valueFromXml.ToString()))
							{
								propertyInfo.SetValue(x, null, null);
								continue;
							}
							type2 = type2.GetGenericArguments()[0];
						}
						object result;
						if (type2 == typeof(bool))
						{
							string s = valueFromXml.ToString().ToLower();
							propertyInfo.SetValue(x, XmlConvert.ToBoolean(s), null);
						}
						else if (type2.IsPrimitive)
						{
							propertyInfo.SetValue(x, valueFromXml.ChangeType(type2, Culture), null);
						}
						else if (type2.IsEnum)
						{
							object value = type2.FindEnumValue(valueFromXml.ToString(), Culture);
							propertyInfo.SetValue(x, value, null);
						}
						else if (type2 == typeof(Uri))
						{
							Uri value2 = new Uri(valueFromXml.ToString(), UriKind.RelativeOrAbsolute);
							propertyInfo.SetValue(x, value2, null);
						}
						else if (type2 == typeof(string))
						{
							propertyInfo.SetValue(x, valueFromXml, null);
						}
						else if (type2 == typeof(DateTime))
						{
							valueFromXml = ((!DateFormat.HasValue()) ? ((object)DateTime.Parse(valueFromXml.ToString(), Culture)) : ((object)DateTime.ParseExact(valueFromXml.ToString(), DateFormat, Culture)));
							propertyInfo.SetValue(x, valueFromXml, null);
						}
						else if (type2 == typeof(DateTimeOffset))
						{
							string s = valueFromXml.ToString();
							if (!string.IsNullOrEmpty(s))
							{
								try
								{
									DateTimeOffset dateTimeOffset = XmlConvert.ToDateTimeOffset(s);
									propertyInfo.SetValue(x, dateTimeOffset, null);
								}
								catch (Exception)
								{
									if (TryGetFromString(s, out result, type2))
									{
										propertyInfo.SetValue(x, result, null);
									}
									else
									{
										DateTimeOffset dateTimeOffset = DateTimeOffset.Parse(s);
										propertyInfo.SetValue(x, dateTimeOffset, null);
									}
								}
							}
						}
						else if (type2 == typeof(decimal))
						{
							valueFromXml = decimal.Parse(valueFromXml.ToString(), Culture);
							propertyInfo.SetValue(x, valueFromXml, null);
						}
						else if (type2 == typeof(Guid))
						{
							string value3 = valueFromXml.ToString();
							valueFromXml = (string.IsNullOrEmpty(value3) ? Guid.Empty : new Guid(valueFromXml.ToString()));
							propertyInfo.SetValue(x, valueFromXml, null);
						}
						else if (type2 == typeof(TimeSpan))
						{
							TimeSpan timeSpan = XmlConvert.ToTimeSpan(valueFromXml.ToString());
							propertyInfo.SetValue(x, timeSpan, null);
						}
						else if (type2.IsGenericType)
						{
							Type t = type2.GetGenericArguments()[0];
							IList list = (IList)Activator.CreateInstance(type2);
							XElement elementByName2 = GetElementByName(root, propertyInfo.Name.AsNamespaced(Namespace));
							if (elementByName2.HasElements)
							{
								XElement elementByName = elementByName2.Elements().FirstOrDefault();
								IEnumerable<XElement> elements = elementByName2.Elements(elementByName.Name);
								PopulateListFromElements(t, elements, list);
							}
							propertyInfo.SetValue(x, list, null);
						}
						else if (type2.IsSubclassOfRawGeneric(typeof(List<>)))
						{
							object value4 = HandleListDerivative(x, root, propertyInfo.Name, type2);
							propertyInfo.SetValue(x, value4, null);
						}
						else if (TryGetFromString(valueFromXml.ToString(), out result, type2))
						{
							propertyInfo.SetValue(x, result, null);
						}
						else if (root != null)
						{
							XElement elementByName3 = GetElementByName(root, name);
							if (elementByName3 != null)
							{
								object value5 = CreateAndMap(type2, elementByName3);
								propertyInfo.SetValue(x, value5, null);
							}
						}
					}
				}
			}
			return x;
		}

		private static bool TryGetFromString(string inputString, out object result, Type type)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (!converter.CanConvertFrom(typeof(string)))
			{
				result = null;
				return false;
			}
			result = converter.ConvertFromInvariantString(inputString);
			return true;
		}

		private void PopulateListFromElements(Type t, IEnumerable<XElement> elements, IList list)
		{
			foreach (XElement element in elements)
			{
				object value = CreateAndMap(t, element);
				list.Add(value);
			}
		}

		private object HandleListDerivative(object x, XElement root, string propName, Type type)
		{
			Type type2 = (!type.IsGenericType) ? type.BaseType.GetGenericArguments()[0] : type.GetGenericArguments()[0];
			IList list = (IList)Activator.CreateInstance(type);
			IEnumerable<XElement> enumerable = root.Descendants(type2.Name.AsNamespaced(Namespace));
			string name = type2.Name;
			if (!enumerable.Any())
			{
				XName name2 = name.ToLower().AsNamespaced(Namespace);
				enumerable = root.Descendants(name2);
			}
			if (!enumerable.Any())
			{
				XName name3 = name.ToCamelCase(Culture).AsNamespaced(Namespace);
				enumerable = root.Descendants(name3);
			}
			if (!enumerable.Any())
			{
				enumerable = from e in root.Descendants()
				where e.Name.LocalName.RemoveUnderscoresAndDashes() == name
				select e;
			}
			if (!enumerable.Any())
			{
				XName lowerName = name.ToLower().AsNamespaced(Namespace);
				enumerable = from e in root.Descendants()
				where (XName)e.Name.LocalName.RemoveUnderscoresAndDashes() == lowerName
				select e;
			}
			PopulateListFromElements(type2, enumerable, list);
			if (!type.IsGenericType)
			{
				Map(list, root.Element(propName.AsNamespaced(Namespace)) ?? root);
			}
			return list;
		}

		protected virtual object CreateAndMap(Type t, XElement element)
		{
			object obj;
			if (t == typeof(string))
			{
				obj = element.Value;
			}
			else if (t.IsPrimitive)
			{
				obj = element.Value.ChangeType(t, Culture);
			}
			else
			{
				obj = Activator.CreateInstance(t);
				Map(obj, element);
			}
			return obj;
		}

		protected virtual object GetValueFromXml(XElement root, XName name, PropertyInfo prop)
		{
			object result = null;
			if (root != null)
			{
				XElement elementByName = GetElementByName(root, name);
				if (elementByName == null)
				{
					XAttribute attributeByName = GetAttributeByName(root, name);
					if (attributeByName != null)
					{
						result = attributeByName.Value;
					}
				}
				else if (!elementByName.IsEmpty || elementByName.HasElements || elementByName.HasAttributes)
				{
					result = elementByName.Value;
				}
			}
			return result;
		}

		protected virtual XElement GetElementByName(XElement root, XName name)
		{
			XName name2 = name.LocalName.ToLower().AsNamespaced(name.NamespaceName);
			XName name3 = name.LocalName.ToCamelCase(Culture).AsNamespaced(name.NamespaceName);
			if (root.Element(name) == null)
			{
				if (root.Element(name2) == null)
				{
					if (root.Element(name3) == null)
					{
						if (!(name == "Value".AsNamespaced(name.NamespaceName)))
						{
							XElement xElement = (from d in root.Descendants()
							orderby d.Ancestors().Count()
							select d).FirstOrDefault((XElement d) => d.Name.LocalName.RemoveUnderscoresAndDashes() == name.LocalName) ?? (from d in root.Descendants()
							orderby d.Ancestors().Count()
							select d).FirstOrDefault((XElement d) => d.Name.LocalName.RemoveUnderscoresAndDashes() == name.LocalName.ToLower());
							if (xElement == null)
							{
								return null;
							}
							return xElement;
						}
						return root;
					}
					return root.Element(name3);
				}
				return root.Element(name2);
			}
			return root.Element(name);
		}

		protected virtual XAttribute GetAttributeByName(XElement root, XName name)
		{
			XName name2 = name.LocalName.ToLower().AsNamespaced(name.NamespaceName);
			XName name3 = name.LocalName.ToCamelCase(Culture).AsNamespaced(name.NamespaceName);
			if (root.Attribute(name) == null)
			{
				if (root.Attribute(name2) == null)
				{
					if (root.Attribute(name3) == null)
					{
						XAttribute xAttribute = root.Attributes().FirstOrDefault((XAttribute d) => d.Name.LocalName.RemoveUnderscoresAndDashes() == name.LocalName);
						if (xAttribute == null)
						{
							return null;
						}
						return xAttribute;
					}
					return root.Attribute(name3);
				}
				return root.Attribute(name2);
			}
			return root.Attribute(name);
		}
	}
}

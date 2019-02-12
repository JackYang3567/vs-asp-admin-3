using RestSharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace RestSharp.Serializers
{
	public class XmlSerializer : ISerializer
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

		public string ContentType
		{
			get;
			set;
		}

		public XmlSerializer()
		{
			ContentType = "text/xml";
		}

		public XmlSerializer(string @namespace)
		{
			Namespace = @namespace;
			ContentType = "text/xml";
		}

		public string Serialize(object obj)
		{
			XDocument xDocument = new XDocument();
			Type type = obj.GetType();
			string text = type.Name;
			SerializeAsAttribute attribute = type.GetAttribute<SerializeAsAttribute>();
			if (attribute != null)
			{
				text = attribute.TransformName(attribute.Name ?? text);
			}
			XElement xElement = new XElement(text.AsNamespaced(Namespace));
			if (obj is IList)
			{
				string text2 = "";
				foreach (object item in (IList)obj)
				{
					Type type2 = item.GetType();
					SerializeAsAttribute attribute2 = type2.GetAttribute<SerializeAsAttribute>();
					if (attribute2 != null)
					{
						text2 = attribute2.TransformName(attribute2.Name ?? text);
					}
					if (text2 == "")
					{
						text2 = type2.Name;
					}
					XElement xElement2 = new XElement(text2.AsNamespaced(Namespace));
					Map(xElement2, item);
					xElement.Add(xElement2);
				}
			}
			else
			{
				Map(xElement, obj);
			}
			if (RootElement.HasValue())
			{
				XElement content = new XElement(RootElement.AsNamespaced(Namespace), xElement);
				xDocument.Add(content);
			}
			else
			{
				xDocument.Add(xElement);
			}
			return xDocument.ToString();
		}

		private void Map(XElement root, object obj)
		{
			Type type = obj.GetType();
			IEnumerable<PropertyInfo> enumerable = from p in type.GetProperties()
			let indexAttribute = p.GetAttribute<SerializeAsAttribute>()
			where p.CanRead && p.CanWrite
			orderby (indexAttribute == null) ? 2147483647 : indexAttribute.Index
			select p;
			SerializeAsAttribute attribute = type.GetAttribute<SerializeAsAttribute>();
			foreach (PropertyInfo item in enumerable)
			{
				string text = item.Name;
				object value = item.GetValue(obj, null);
				if (value != null)
				{
					string serializedValue = GetSerializedValue(value);
					Type propertyType = item.PropertyType;
					bool flag = false;
					SerializeAsAttribute attribute2 = item.GetAttribute<SerializeAsAttribute>();
					if (attribute2 != null)
					{
						text = (attribute2.Name.HasValue() ? attribute2.Name : text);
						flag = attribute2.Attribute;
					}
					SerializeAsAttribute attribute3 = item.GetAttribute<SerializeAsAttribute>();
					if (attribute3 != null)
					{
						text = attribute3.TransformName(text);
					}
					else if (attribute != null)
					{
						text = attribute.TransformName(text);
					}
					XName name = text.AsNamespaced(Namespace);
					XElement xElement = new XElement(name);
					if (propertyType.IsPrimitive || propertyType.IsValueType || propertyType == typeof(string))
					{
						if (flag)
						{
							root.Add(new XAttribute(text, serializedValue));
							continue;
						}
						xElement.Value = serializedValue;
					}
					else if (value is IList)
					{
						string text2 = "";
						foreach (object item2 in (IList)value)
						{
							if (text2 == "")
							{
								Type type2 = item2.GetType();
								SerializeAsAttribute attribute4 = type2.GetAttribute<SerializeAsAttribute>();
								text2 = ((attribute4 != null && attribute4.Name.HasValue()) ? attribute4.Name : type2.Name);
							}
							XElement xElement2 = new XElement(text2.AsNamespaced(Namespace));
							Map(xElement2, item2);
							xElement.Add(xElement2);
						}
					}
					else
					{
						Map(xElement, value);
					}
					root.Add(xElement);
				}
			}
		}

		private string GetSerializedValue(object obj)
		{
			object obj2 = obj;
			if (obj is DateTime && DateFormat.HasValue())
			{
				obj2 = ((DateTime)obj).ToString(DateFormat, CultureInfo.InvariantCulture);
			}
			if (obj is bool)
			{
				obj2 = ((bool)obj).ToString(CultureInfo.InvariantCulture).ToLower();
			}
			if (!IsNumeric(obj))
			{
				return obj2.ToString();
			}
			return SerializeNumber(obj);
		}

		private static string SerializeNumber(object number)
		{
			if (!(number is long))
			{
				if (!(number is ulong))
				{
					if (!(number is int))
					{
						if (!(number is uint))
						{
							if (!(number is decimal))
							{
								if (!(number is float))
								{
									return Convert.ToDouble(number, CultureInfo.InvariantCulture).ToString("r", CultureInfo.InvariantCulture);
								}
								return ((float)number).ToString(CultureInfo.InvariantCulture);
							}
							return ((decimal)number).ToString(CultureInfo.InvariantCulture);
						}
						return ((uint)number).ToString(CultureInfo.InvariantCulture);
					}
					return ((int)number).ToString(CultureInfo.InvariantCulture);
				}
				return ((ulong)number).ToString(CultureInfo.InvariantCulture);
			}
			return ((long)number).ToString(CultureInfo.InvariantCulture);
		}

		private static bool IsNumeric(object value)
		{
			if (!(value is sbyte))
			{
				if (!(value is byte))
				{
					if (!(value is short))
					{
						if (!(value is ushort))
						{
							if (!(value is int))
							{
								if (!(value is uint))
								{
									if (!(value is long))
									{
										if (!(value is ulong))
										{
											if (!(value is float))
											{
												if (!(value is double))
												{
													if (!(value is decimal))
													{
														return false;
													}
													return true;
												}
												return true;
											}
											return true;
										}
										return true;
									}
									return true;
								}
								return true;
							}
							return true;
						}
						return true;
					}
					return true;
				}
				return true;
			}
			return true;
		}
	}
}

using RestSharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace RestSharp.Deserializers
{
	public class JsonDeserializer : IDeserializer
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

		public JsonDeserializer()
		{
			Culture = CultureInfo.InvariantCulture;
		}

		public T Deserialize<T>(IRestResponse response)
		{
			T val = Activator.CreateInstance<T>();
			if (((object)val) is IList)
			{
				Type type = val.GetType();
				if (RootElement.HasValue())
				{
					object parent = FindRoot(response.Content);
					val = (T)BuildList(type, parent);
				}
				else
				{
					object parent2 = SimpleJson.DeserializeObject(response.Content);
					val = (T)BuildList(type, parent2);
				}
			}
			else if (((object)val) is IDictionary)
			{
				object parent = FindRoot(response.Content);
				val = (T)BuildDictionary(val.GetType(), parent);
			}
			else
			{
				object parent = FindRoot(response.Content);
				val = (T)Map(val, (IDictionary<string, object>)parent);
			}
			return val;
		}

		private object FindRoot(string content)
		{
			IDictionary<string, object> dictionary = (IDictionary<string, object>)SimpleJson.DeserializeObject(content);
			if (!RootElement.HasValue() || !dictionary.ContainsKey(RootElement))
			{
				return dictionary;
			}
			return dictionary[RootElement];
		}

		private object Map(object target, IDictionary<string, object> data)
		{
			Type type = target.GetType();
			List<PropertyInfo> list = (from p in type.GetProperties()
			where p.CanWrite
			select p).ToList();
			foreach (PropertyInfo item in list)
			{
				Type propertyType = item.PropertyType;
				object[] customAttributes = item.GetCustomAttributes(typeof(DeserializeAsAttribute), false);
				string name;
				if (customAttributes.Length > 0)
				{
					DeserializeAsAttribute deserializeAsAttribute = (DeserializeAsAttribute)customAttributes[0];
					name = deserializeAsAttribute.Name;
				}
				else
				{
					name = item.Name;
				}
				string[] array = name.Split('.');
				IDictionary<string, object> dictionary = data;
				object obj = null;
				for (int i = 0; i < array.Length; i++)
				{
					IEnumerable<string> nameVariants = array[i].GetNameVariants(Culture);
					IDictionary<string, object> dictionary2 = dictionary;
					string text = nameVariants.FirstOrDefault(dictionary2.ContainsKey);
					if (text == null)
					{
						break;
					}
					if (i == array.Length - 1)
					{
						obj = dictionary[text];
					}
					else
					{
						dictionary = (IDictionary<string, object>)dictionary[text];
					}
				}
				if (obj != null)
				{
					item.SetValue(target, ConvertValue(propertyType, obj), null);
				}
			}
			return target;
		}

		private IDictionary BuildDictionary(Type type, object parent)
		{
			IDictionary dictionary = (IDictionary)Activator.CreateInstance(type);
			Type type2 = type.GetGenericArguments()[1];
			foreach (KeyValuePair<string, object> item in (IDictionary<string, object>)parent)
			{
				string key = item.Key;
				object value = (!type2.IsGenericType || !(type2.GetGenericTypeDefinition() == typeof(List<>))) ? ConvertValue(type2, item.Value) : BuildList(type2, item.Value);
				dictionary.Add(key, value);
			}
			return dictionary;
		}

		private IList BuildList(Type type, object parent)
		{
			IList list = (IList)Activator.CreateInstance(type);
			Type type2 = type.GetInterfaces().First((Type x) => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IList<>));
			Type type3 = type2.GetGenericArguments()[0];
			if (parent is IList)
			{
				foreach (object item in (IList)parent)
				{
					if (type3.IsPrimitive)
					{
						object value = ConvertValue(type3, item);
						list.Add(value);
					}
					else if (type3 == typeof(string))
					{
						if (item == null)
						{
							list.Add(null);
						}
						else
						{
							list.Add(item.ToString());
						}
					}
					else if (item == null)
					{
						list.Add(null);
					}
					else
					{
						object value = ConvertValue(type3, item);
						list.Add(value);
					}
				}
			}
			else
			{
				list.Add(ConvertValue(type3, parent));
			}
			return list;
		}

		private object ConvertValue(Type type, object value)
		{
			string text = Convert.ToString(value, Culture);
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				type = type.GetGenericArguments()[0];
			}
			if (type == typeof(object) && value != null)
			{
				type = value.GetType();
			}
			if (!type.IsPrimitive)
			{
				if (!type.IsEnum)
				{
					if (!(type == typeof(Uri)))
					{
						if (!(type == typeof(string)))
						{
							if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
							{
								DateTime dateTime = (!DateFormat.HasValue()) ? text.ParseJsonDate(Culture) : DateTime.ParseExact(text, DateFormat, Culture);
								if (type == typeof(DateTime))
								{
									return dateTime;
								}
								if (type == typeof(DateTimeOffset))
								{
									return (DateTimeOffset)dateTime;
								}
							}
							else
							{
								if (type == typeof(decimal))
								{
									if (!(value is double))
									{
										if (!text.Contains("e"))
										{
											return decimal.Parse(text, Culture);
										}
										return decimal.Parse(text, NumberStyles.Float, Culture);
									}
									return (decimal)(double)value;
								}
								if (type == typeof(Guid))
								{
									return string.IsNullOrEmpty(text) ? Guid.Empty : new Guid(text);
								}
								if (type == typeof(TimeSpan))
								{
									return TimeSpan.Parse(text);
								}
								if (!type.IsGenericType)
								{
									if (!type.IsSubclassOfRawGeneric(typeof(List<>)))
									{
										if (!(type == typeof(JsonObject)))
										{
											return CreateAndMap(type, value);
										}
										return BuildDictionary(typeof(Dictionary<string, object>), value);
									}
									return BuildList(type, value);
								}
								Type genericTypeDefinition = type.GetGenericTypeDefinition();
								if (genericTypeDefinition == typeof(List<>))
								{
									return BuildList(type, value);
								}
								if (!(genericTypeDefinition == typeof(Dictionary<, >)))
								{
									return CreateAndMap(type, value);
								}
								Type left = type.GetGenericArguments()[0];
								if (left == typeof(string))
								{
									return BuildDictionary(type, value);
								}
							}
							return null;
						}
						return text;
					}
					return new Uri(text, UriKind.RelativeOrAbsolute);
				}
				return type.FindEnumValue(text, Culture);
			}
			return value.ChangeType(type, Culture);
		}

		private object CreateAndMap(Type type, object element)
		{
			object obj = Activator.CreateInstance(type);
			Map(obj, (IDictionary<string, object>)element);
			return obj;
		}
	}
}

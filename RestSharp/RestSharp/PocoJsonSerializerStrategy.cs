using RestSharp.Reflection;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace RestSharp
{
	[GeneratedCode("simple-json", "1.0.0")]
	public class PocoJsonSerializerStrategy : IJsonSerializerStrategy
	{
		internal IDictionary<Type, ReflectionUtils.ConstructorDelegate> ConstructorCache;

		internal IDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>> GetCache;

		internal IDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>> SetCache;

		internal static readonly Type[] EmptyTypes = new Type[0];

		internal static readonly Type[] ArrayConstructorParameterTypes = new Type[1]
		{
			typeof(int)
		};

		private static readonly string[] Iso8601Format = new string[3]
		{
			"yyyy-MM-dd\\THH:mm:ss.FFFFFFF\\Z",
			"yyyy-MM-dd\\THH:mm:ss\\Z",
			"yyyy-MM-dd\\THH:mm:ssK"
		};

		public PocoJsonSerializerStrategy()
		{
			ConstructorCache = new ReflectionUtils.ThreadSafeDictionary<Type, ReflectionUtils.ConstructorDelegate>(ContructorDelegateFactory);
			GetCache = new ReflectionUtils.ThreadSafeDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>>(GetterValueFactory);
			SetCache = new ReflectionUtils.ThreadSafeDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>(SetterValueFactory);
		}

		protected virtual string MapClrMemberNameToJsonFieldName(string clrPropertyName)
		{
			return clrPropertyName;
		}

		internal virtual ReflectionUtils.ConstructorDelegate ContructorDelegateFactory(Type key)
		{
			return ReflectionUtils.GetContructor(key, key.IsArray ? ArrayConstructorParameterTypes : EmptyTypes);
		}

		internal virtual IDictionary<string, ReflectionUtils.GetDelegate> GetterValueFactory(Type type)
		{
			IDictionary<string, ReflectionUtils.GetDelegate> dictionary = new Dictionary<string, ReflectionUtils.GetDelegate>();
			foreach (PropertyInfo property in ReflectionUtils.GetProperties(type))
			{
				if (property.CanRead)
				{
					MethodInfo getterMethodInfo = ReflectionUtils.GetGetterMethodInfo(property);
					if (!getterMethodInfo.IsStatic && getterMethodInfo.IsPublic)
					{
						dictionary[MapClrMemberNameToJsonFieldName(property.Name)] = ReflectionUtils.GetGetMethod(property);
					}
				}
			}
			foreach (FieldInfo field in ReflectionUtils.GetFields(type))
			{
				if (!field.IsStatic && field.IsPublic)
				{
					dictionary[MapClrMemberNameToJsonFieldName(field.Name)] = ReflectionUtils.GetGetMethod(field);
				}
			}
			return dictionary;
		}

		internal virtual IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> SetterValueFactory(Type type)
		{
			IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> dictionary = new Dictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>();
			foreach (PropertyInfo property in ReflectionUtils.GetProperties(type))
			{
				if (property.CanWrite)
				{
					MethodInfo setterMethodInfo = ReflectionUtils.GetSetterMethodInfo(property);
					if (!setterMethodInfo.IsStatic && setterMethodInfo.IsPublic)
					{
						dictionary[MapClrMemberNameToJsonFieldName(property.Name)] = new KeyValuePair<Type, ReflectionUtils.SetDelegate>(property.PropertyType, ReflectionUtils.GetSetMethod(property));
					}
				}
			}
			foreach (FieldInfo field in ReflectionUtils.GetFields(type))
			{
				if (!field.IsInitOnly && !field.IsStatic && field.IsPublic)
				{
					dictionary[MapClrMemberNameToJsonFieldName(field.Name)] = new KeyValuePair<Type, ReflectionUtils.SetDelegate>(field.FieldType, ReflectionUtils.GetSetMethod(field));
				}
			}
			return dictionary;
		}

		public virtual bool TrySerializeNonPrimitiveObject(object input, out object output)
		{
			return TrySerializeKnownTypes(input, out output) || TrySerializeUnknownTypes(input, out output);
		}

		public virtual object DeserializeObject(object value, Type type)
		{
			if (type == (Type)null)
			{
				throw new ArgumentNullException("type");
			}
			string text = value as string;
			if (!(type == typeof(Guid)) || !string.IsNullOrEmpty(text))
			{
				if (value != null)
				{
					object obj = null;
					if (text != null)
					{
						if (text.Length != 0)
						{
							if (!(type == typeof(DateTime)) && (!ReflectionUtils.IsNullableType(type) || !(Nullable.GetUnderlyingType(type) == typeof(DateTime))))
							{
								if (!(type == typeof(DateTimeOffset)) && (!ReflectionUtils.IsNullableType(type) || !(Nullable.GetUnderlyingType(type) == typeof(DateTimeOffset))))
								{
									if (!(type == typeof(Guid)) && (!ReflectionUtils.IsNullableType(type) || !(Nullable.GetUnderlyingType(type) == typeof(Guid))))
									{
										if (!(type == typeof(Uri)))
										{
											if (!(type == typeof(string)))
											{
												return Convert.ChangeType(text, type, CultureInfo.InvariantCulture);
											}
											return text;
										}
										Uri result = default(Uri);
										if (!Uri.IsWellFormedUriString(text, UriKind.RelativeOrAbsolute) || !Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out result))
										{
											return null;
										}
										return result;
									}
									return new Guid(text);
								}
								return DateTimeOffset.ParseExact(text, Iso8601Format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
							}
							return DateTime.ParseExact(text, Iso8601Format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
						}
						obj = ((type == typeof(Guid)) ? ((object)default(Guid)) : ((!ReflectionUtils.IsNullableType(type) || !(Nullable.GetUnderlyingType(type) == typeof(Guid))) ? text : null));
						if (!ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof(Guid))
						{
							return text;
						}
					}
					else if (value is bool)
					{
						return value;
					}
					bool flag = value is long;
					bool flag2 = value is double;
					if ((!flag || !(type == typeof(long))) && (!flag2 || !(type == typeof(double))))
					{
						if ((!flag2 || !(type != typeof(double))) && (!flag || !(type != typeof(long))))
						{
							IDictionary<string, object> dictionary = value as IDictionary<string, object>;
							if (dictionary != null)
							{
								IDictionary<string, object> dictionary2 = dictionary;
								if (ReflectionUtils.IsTypeDictionary(type))
								{
									Type[] genericTypeArguments = ReflectionUtils.GetGenericTypeArguments(type);
									Type type2 = genericTypeArguments[0];
									Type type3 = genericTypeArguments[1];
									Type key = typeof(Dictionary<, >).MakeGenericType(type2, type3);
									IDictionary dictionary3 = (IDictionary)ConstructorCache[key]();
									foreach (KeyValuePair<string, object> item in dictionary2)
									{
										dictionary3.Add(item.Key, DeserializeObject(item.Value, type3));
									}
									obj = dictionary3;
								}
								else if (type == typeof(object))
								{
									obj = value;
								}
								else
								{
									obj = ConstructorCache[type]();
									foreach (KeyValuePair<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> item2 in SetCache[type])
									{
										object value2;
										if (dictionary2.TryGetValue(item2.Key, out value2))
										{
											object value3 = value2;
											KeyValuePair<Type, ReflectionUtils.SetDelegate> value4 = item2.Value;
											value2 = DeserializeObject(value3, value4.Key);
											value4 = item2.Value;
											value4.Value(obj, value2);
										}
									}
								}
							}
							else
							{
								IList<object> list = value as IList<object>;
								if (list != null)
								{
									IList<object> list2 = list;
									IList list3 = null;
									if (type.IsArray)
									{
										list3 = (IList)ConstructorCache[type](list2.Count);
										int num = 0;
										foreach (object item3 in list2)
										{
											list3[num++] = DeserializeObject(item3, type.GetElementType());
										}
									}
									else if (ReflectionUtils.IsTypeGenericeCollectionInterface(type) || ReflectionUtils.IsAssignableFrom(typeof(IList), type))
									{
										Type genericListElementType = ReflectionUtils.GetGenericListElementType(type);
										list3 = (IList)(ConstructorCache[type] ?? ConstructorCache[typeof(List<>).MakeGenericType(genericListElementType)])(list2.Count);
										foreach (object item4 in list2)
										{
											list3.Add(DeserializeObject(item4, genericListElementType));
										}
									}
									obj = list3;
								}
							}
							return obj;
						}
						obj = ((type == typeof(int) || type == typeof(long) || type == typeof(double) || type == typeof(float) || type == typeof(bool) || type == typeof(decimal) || type == typeof(byte) || type == typeof(short)) ? Convert.ChangeType(value, type, CultureInfo.InvariantCulture) : value);
						if (!ReflectionUtils.IsNullableType(type))
						{
							return obj;
						}
						return ReflectionUtils.ToNullableType(obj, type);
					}
					return value;
				}
				return null;
			}
			return default(Guid);
		}

		protected virtual object SerializeEnum(Enum p)
		{
			return Convert.ToDouble(p, CultureInfo.InvariantCulture);
		}

		protected virtual bool TrySerializeKnownTypes(object input, out object output)
		{
			bool result = true;
			if (input is DateTime)
			{
				DateTime dateTime = (DateTime)input;
				dateTime = dateTime.ToUniversalTime();
				output = dateTime.ToString(Iso8601Format[0], CultureInfo.InvariantCulture);
			}
			else if (input is DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)input;
				dateTimeOffset = dateTimeOffset.ToUniversalTime();
				output = dateTimeOffset.ToString(Iso8601Format[0], CultureInfo.InvariantCulture);
			}
			else if (input is Guid)
			{
				output = ((Guid)input).ToString("D");
			}
			else if (input is Uri)
			{
				output = input.ToString();
			}
			else
			{
				Enum @enum = input as Enum;
				if (@enum != null)
				{
					output = SerializeEnum(@enum);
				}
				else
				{
					result = false;
					output = null;
				}
			}
			return result;
		}

		protected virtual bool TrySerializeUnknownTypes(object input, out object output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			output = null;
			Type type = input.GetType();
			if (type.FullName != null)
			{
				IDictionary<string, object> dictionary = new JsonObject();
				IDictionary<string, ReflectionUtils.GetDelegate> dictionary2 = GetCache[type];
				foreach (KeyValuePair<string, ReflectionUtils.GetDelegate> item in dictionary2)
				{
					if (item.Value != null)
					{
						dictionary.Add(MapClrMemberNameToJsonFieldName(item.Key), item.Value(input));
					}
				}
				output = dictionary;
				return true;
			}
			return false;
		}
	}
}

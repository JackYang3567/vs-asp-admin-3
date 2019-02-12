using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace RestSharp.Extensions
{
	public static class ReflectionExtensions
	{
		public static T GetAttribute<T>(this MemberInfo prop) where T : Attribute
		{
			return Attribute.GetCustomAttribute(prop, typeof(T)) as T;
		}

		public static T GetAttribute<T>(this Type type) where T : Attribute
		{
			return Attribute.GetCustomAttribute(type, typeof(T)) as T;
		}

		public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
		{
			while (toCheck != typeof(object))
			{
				Type right = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
				if (generic == right)
				{
					return true;
				}
				toCheck = toCheck.BaseType;
			}
			return false;
		}

		public static object ChangeType(this object source, Type newType)
		{
			return Convert.ChangeType(source, newType);
		}

		public static object ChangeType(this object source, Type newType, CultureInfo culture)
		{
			return Convert.ChangeType(source, newType, culture);
		}

		public static object FindEnumValue(this Type type, string value, CultureInfo culture)
		{
			Enum @enum = Enum.GetValues(type).Cast<Enum>().FirstOrDefault((Enum v) => v.ToString().GetNameVariants(culture).Contains(value, StringComparer.Create(culture, true)));
			if (@enum == null)
			{
				object obj = Convert.ChangeType(value, Enum.GetUnderlyingType(type), culture);
				if (obj != null && Enum.IsDefined(type, obj))
				{
					@enum = (Enum)Enum.ToObject(type, obj);
				}
			}
			return @enum;
		}
	}
}

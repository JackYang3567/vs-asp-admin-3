using System;

namespace Game.Utils
{
	public class TypeHelper
	{
		public static object ChangeType(Type targetType, object val)
		{
			if (val == null || (targetType.IsGenericType && val.ToString() == ""))
			{
				return null;
			}
			if (targetType == val.GetType() || targetType.IsGenericType)
			{
				return val;
			}
			if (targetType == typeof(bool))
			{
				if (val.ToString() == "0")
				{
					return false;
				}
				if (val.ToString() == "1")
				{
					return true;
				}
			}
			if (targetType.IsEnum)
			{
				int result = 0;
				if (!int.TryParse(val.ToString(), out result))
				{
					return Enum.Parse(targetType, val.ToString());
				}
				return val;
			}
			if (targetType == typeof(Type))
			{
				return ReflectionHelper.GetType(val.ToString());
			}
			return Convert.ChangeType(val, targetType);
		}

		public static string GetClassSimpleName(Type t)
		{
			string[] array = t.ToString().Split('.');
			return array[array.Length - 1].ToString();
		}

		public static string GetDefaultValue(Type destType)
		{
			if (IsNumbericType(destType))
			{
				return "0";
			}
			if (destType == typeof(string))
			{
				return "\"\"";
			}
			if (destType == typeof(bool))
			{
				return "false";
			}
			if (destType == typeof(DateTime))
			{
				return "DateTime.Now";
			}
			if (destType == typeof(Guid))
			{
				return "System.Guid.NewGuid()";
			}
			if (destType == typeof(TimeSpan))
			{
				return "System.TimeSpan.Zero";
			}
			return "null";
		}

		public static Type GetTypeByRegularName(string regularName)
		{
			return ReflectionHelper.GetType(regularName);
		}

		public static string GetTypeRegularName(Type destType)
		{
			string arg = destType.Assembly.FullName.Split(',')[0];
			return string.Format("{0},{1}", destType.ToString(), arg);
		}

		public static string GetTypeRegularNameOf(object obj)
		{
			return GetTypeRegularName(obj.GetType());
		}

		public static bool IsFixLength(Type destDataType)
		{
			if (!IsNumbericType(destDataType))
			{
				if (destDataType != typeof(byte[]))
				{
					if (destDataType != typeof(DateTime))
					{
						return destDataType == typeof(bool);
					}
					return true;
				}
				return true;
			}
			return true;
		}

		public static bool IsNumbericType(Type destDataType)
		{
			if (destDataType != typeof(int) && destDataType != typeof(uint) && destDataType != typeof(double) && destDataType != typeof(short) && destDataType != typeof(ushort) && destDataType != typeof(decimal) && destDataType != typeof(long) && destDataType != typeof(ulong) && destDataType != typeof(float) && destDataType != typeof(byte))
			{
				return destDataType == typeof(sbyte);
			}
			return true;
		}

		public static bool IsSimpleType(Type t)
		{
			if (!IsNumbericType(t))
			{
				if (t != typeof(char))
				{
					if (t != typeof(string))
					{
						if (t != typeof(bool))
						{
							if (t != typeof(DateTime))
							{
								if (t != typeof(Type))
								{
									return t.IsEnum;
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

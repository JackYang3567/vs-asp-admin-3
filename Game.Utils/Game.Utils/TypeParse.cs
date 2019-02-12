using System;
using System.Text.RegularExpressions;

namespace Game.Utils
{
	public sealed class TypeParse
	{
		private TypeParse()
		{
		}

		public static bool IsNumericArray(string[] strNumber)
		{
			if (strNumber == null)
			{
				return false;
			}
			if (strNumber.Length < 1)
			{
				return false;
			}
			foreach (string expression in strNumber)
			{
				if (!Validate.IsNumeric(expression))
				{
					return false;
				}
			}
			return true;
		}

		public static int SafeLongToInt32(long expression)
		{
			if (expression > 2147483647)
			{
				return 2147483647;
			}
			if (expression < -2147483648)
			{
				return -2147483648;
			}
			return (int)expression;
		}

		public static bool StrToBool(object expression, bool defValue)
		{
			if (expression != null)
			{
				if (string.Compare(expression.ToString(), "true", true) == 0)
				{
					return true;
				}
				if (string.Compare(expression.ToString(), "false", true) == 0)
				{
					return false;
				}
			}
			return defValue;
		}

		public static float StrToFloat(object expression, float defValue)
		{
			if (expression == null || expression.ToString().Length > 10)
			{
				return defValue;
			}
			float result = defValue;
			if (expression != null && Regex.IsMatch(expression.ToString(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$"))
			{
				result = Convert.ToSingle(expression);
			}
			return result;
		}

		public static int StrToInt(object expression, int defValue)
		{
			if (expression == null)
			{
				return defValue;
			}
			string text = expression.ToString();
			if (text.Length <= 0 || text.Length > 11 || !Regex.IsMatch(text, "^[-]?[0-9]*$"))
			{
				return defValue;
			}
			if (text.Length >= 10 && (text.Length != 10 || text[0] != '1') && (text.Length != 11 || text[0] != '-' || text[1] != '1'))
			{
				return defValue;
			}
			return Convert.ToInt32(text);
		}

		public static DateTime ConvertIntDateTime(double d)
		{
			DateTime minValue = DateTime.MinValue;
			return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(d);
		}

		public static double ConvertDateTimeInt(DateTime time)
		{
			double num = 0.0;
			DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return (time - d).TotalSeconds;
		}
	}
}

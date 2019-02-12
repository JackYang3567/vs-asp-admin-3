using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RestSharp.Authenticators.OAuth.Extensions
{
	internal static class StringExtensions
	{
		private const RegexOptions Options = RegexOptions.IgnoreCase | RegexOptions.Compiled;

		public static bool IsNullOrBlank(this string value)
		{
			return string.IsNullOrEmpty(value) || (!string.IsNullOrEmpty(value) && value.Trim() == string.Empty);
		}

		public static bool EqualsIgnoreCase(this string left, string right)
		{
			return string.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
		}

		public static bool EqualsAny(this string input, params string[] args)
		{
			return args.Aggregate(false, (bool current, string arg) => current | input.Equals(arg));
		}

		public static string FormatWith(this string format, params object[] args)
		{
			return string.Format(format, args);
		}

		public static string FormatWithInvariantCulture(this string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		public static string Then(this string input, string value)
		{
			return input + value;
		}

		public static string UrlEncode(this string value)
		{
			return Uri.EscapeDataString(value);
		}

		public static string UrlDecode(this string value)
		{
			return Uri.UnescapeDataString(value);
		}

		public static Uri AsUri(this string value)
		{
			return new Uri(value);
		}

		public static string ToBase64String(this byte[] input)
		{
			return Convert.ToBase64String(input);
		}

		public static byte[] GetBytes(this string input)
		{
			return Encoding.UTF8.GetBytes(input);
		}

		public static string PercentEncode(this string s)
		{
			byte[] bytes = s.GetBytes();
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = bytes;
			foreach (byte b in array)
			{
				stringBuilder.Append(string.Format("%{0:X2}", b));
			}
			return stringBuilder.ToString();
		}

		public static IDictionary<string, string> ParseQueryString(this string query)
		{
			if (query.StartsWith("?"))
			{
				query = query.Substring(1);
			}
			if (!query.Equals(string.Empty))
			{
				string[] source = query.Split('&');
				return (from part in source
				select part.Split('=')).ToDictionary((string[] pair) => pair[0], (string[] pair) => pair[1]);
			}
			return new Dictionary<string, string>();
		}
	}
}

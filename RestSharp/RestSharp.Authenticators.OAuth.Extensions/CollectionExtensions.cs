using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace RestSharp.Authenticators.OAuth.Extensions
{
	internal static class CollectionExtensions
	{
		public static IEnumerable<T> AsEnumerable<T>(this T item)
		{
			return new T[1]
			{
				item
			};
		}

		public static IEnumerable<T> And<T>(this T item, T other)
		{
			return new T[2]
			{
				item,
				other
			};
		}

		public static IEnumerable<T> And<T>(this IEnumerable<T> items, T item)
		{
			foreach (T item2 in items)
			{
				yield return item2;
			}
			yield return item;
		}

		public static K TryWithKey<T, K>(this IDictionary<T, K> dictionary, T key)
		{
			return dictionary.ContainsKey(key) ? dictionary[key] : default(K);
		}

		public static IEnumerable<T> ToEnumerable<T>(this object[] items) where T : class
		{
			try
			{
				foreach (object item in items)
				{
					T record = item as T;
					yield return record;
				}
			}
			finally
			{
			}
		}

		public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (T item in items)
			{
				action(item);
			}
		}

		public static void AddRange(this IDictionary<string, string> collection, NameValueCollection range)
		{
			string[] allKeys = range.AllKeys;
			foreach (string text in allKeys)
			{
				collection.Add(text, range[text]);
			}
		}

		public static string ToQueryString(this NameValueCollection collection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (collection.Count > 0)
			{
				stringBuilder.Append("?");
			}
			int num = 0;
			string[] allKeys = collection.AllKeys;
			foreach (string text in allKeys)
			{
				stringBuilder.AppendFormat("{0}={1}", text, collection[text].UrlEncode());
				num++;
				if (num < collection.Count)
				{
					stringBuilder.Append("&");
				}
			}
			return stringBuilder.ToString();
		}

		public static string Concatenate(this WebParameterCollection collection, string separator, string spacer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = collection.Count;
			int num = 0;
			foreach (WebPair item in collection)
			{
				stringBuilder.Append(item.Name);
				stringBuilder.Append(separator);
				stringBuilder.Append(item.Value);
				num++;
				if (num < count)
				{
					stringBuilder.Append(spacer);
				}
			}
			return stringBuilder.ToString();
		}
	}
}

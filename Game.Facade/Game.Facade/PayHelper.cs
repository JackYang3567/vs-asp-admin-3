using Game.Utils;
using System.Collections.Generic;

namespace Game.Facade
{
	public class PayHelper
	{
		public static string GetOrderID()
		{
			StringBuffer buffer = new StringBuffer();
			buffer += TextUtility.GetDateTimeLongString();
			buffer += TextUtility.CreateRandom(8, 1, 0, 0, 0, "");
			return buffer.ToString();
		}

		public static string GetOrderIDByPrefix(string prefix)
		{
			int num = 22;
			int num2 = 6;
			StringBuffer buffer = new StringBuffer();
			buffer += prefix;
			buffer += TextUtility.GetDateTimeLongString();
			if (buffer.Length + num2 > num)
			{
				num2 = num - buffer.Length;
			}
			buffer += TextUtility.CreateRandom(num2, 1, 0, 0, 0, "");
			return buffer.ToString();
		}

		public static string PrepareSign(Dictionary<string, string> dic)
		{
			string text = "";
			foreach (KeyValuePair<string, string> item in dic)
			{
				string text2 = text;
				text = text2 + item.Key + "=" + item.Value + "&";
			}
			return text.Remove(text.Length - 1);
		}

		public static string PrepareSign(SortedDictionary<string, string> dic)
		{
			string text = "";
			foreach (KeyValuePair<string, string> item in dic)
			{
				if (!string.IsNullOrEmpty(item.Value))
				{
					string text2 = text;
					text = text2 + item.Key + "=" + item.Value + "&";
				}
			}
			return text.Remove(text.Length - 1);
		}
	}
}

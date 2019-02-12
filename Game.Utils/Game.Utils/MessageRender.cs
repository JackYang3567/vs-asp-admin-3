using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Game.Utils
{
	public class MessageRender
	{
		private const string variableBegin = "\\{";

		private const string variableEnd = "\\}";

		private Dictionary<string, string> replaceVariables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		public string this[string key]
		{
			get
			{
				return replaceVariables[key];
			}
			set
			{
				RegisterVariable(key, value);
			}
		}

		public MessageRender()
		{
			RegisterVariable("datetime", TextUtility.FormatDateTime(DateTime.Now.ToString(), 4));
		}

		public void RegisterVariable(string varName, string value)
		{
			if (varName != null)
			{
				if (replaceVariables.ContainsKey(varName))
				{
					replaceVariables[varName] = value;
				}
				else
				{
					replaceVariables.Add(varName, value);
				}
			}
		}

		public string Render(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			foreach (KeyValuePair<string, string> replaceVariable in replaceVariables)
			{
				if (!string.IsNullOrEmpty(replaceVariable.Key) && text.IndexOf(replaceVariable.Key, StringComparison.OrdinalIgnoreCase) != -1)
				{
					text = Regex.Replace(text, string.Format("{0}{1}{2}", "\\{", replaceVariable.Key, "\\}"), (replaceVariable.Value == null) ? "" : replaceVariable.Value, RegexOptions.IgnoreCase);
				}
			}
			return text;
		}
	}
}

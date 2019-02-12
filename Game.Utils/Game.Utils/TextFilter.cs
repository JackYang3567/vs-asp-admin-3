using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Game.Utils
{
	public class TextFilter
	{
		private static Regex REGEX_BR = new Regex("(\\r\\n)", RegexOptions.IgnoreCase);

		private TextFilter()
		{
		}

		public static string FilterAHrefScript(string content)
		{
			string input = FilterScript(content);
			string pattern = " href[ ^=]*= *[\\s\\S]*script *:";
			return Regex.Replace(input, pattern, string.Empty, RegexOptions.IgnoreCase);
		}

		public static string FilterAll(string content)
		{
			content = FilterHtml(content);
			content = FilterScript(content);
			content = FilterAHrefScript(content);
			content = FilterObject(content);
			content = FilterIframe(content);
			content = FilterFrameset(content);
			content = FilterSrc(content);
			content = FilterBadWords(content);
			return content;
		}

		private static string FilterBadWords(string chkStr)
		{
			string text = "";
			if (string.IsNullOrEmpty(chkStr))
			{
				return string.Empty;
			}
			string[] array = text.Split('#');
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				string pattern = array[i].ToString().Trim();
				Match match = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline).Match(chkStr);
				if (match.Success)
				{
					int length = match.Value.Length;
					stringBuilder.Insert(0, "*", length);
					string replacement = stringBuilder.ToString();
					chkStr = Regex.Replace(chkStr, pattern, replacement, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);
				}
				stringBuilder.Remove(0, stringBuilder.Length);
			}
			return chkStr;
		}

		public static string FilterBR(string filterStr)
		{
			Match match = null;
			match = REGEX_BR.Match(filterStr);
			while (match.Success)
			{
				filterStr = filterStr.Replace(match.Groups[0].ToString(), "");
				match = match.NextMatch();
			}
			return filterStr;
		}

		public static string FilterFrameset(string content)
		{
			string pattern = "(?i)<Frameset([^>])*>(\\w|\\W)*</Frameset([^>])*>";
			return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
		}

		public static string FilterHtml(string content)
		{
			string input = FilterScript(content);
			string pattern = "<[^>]*>";
			return Regex.Replace(input, pattern, string.Empty, RegexOptions.IgnoreCase);
		}

		public static string FilterIframe(string content)
		{
			string pattern = "(?i)<Iframe([^>])*>(\\w|\\W)*</Iframe([^>])*>";
			return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
		}

		public static string FilterObject(string content)
		{
			string pattern = "(?i)<Object([^>])*>(\\w|\\W)*</Object([^>])*>";
			return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
		}

		public static string FilterScript(string content)
		{
			string pattern = "<script[^>]*?>.*?</script>";
			return StripScriptAttributesFromTags(Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase));
		}

		public static string FilterSrc(string content)
		{
			string input = FilterScript(content);
			string pattern = " src *= *['\"]?[^\\.]+\\.(js|vbs|asp|aspx|php|jsp)['\"]";
			return Regex.Replace(input, pattern, "", RegexOptions.IgnoreCase);
		}

		public static string FilterTextFromHTML(string contentHtml)
		{
			Regex regex = new Regex("</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase);
			return regex.Replace(contentHtml, "");
		}

		public static string FilterTrim(string content)
		{
			for (int num = content.Length; num >= 0; num--)
			{
				char c = content[num];
				if (!c.Equals(" "))
				{
					c = content[num];
				}
				if (!c.Equals("\r"))
				{
					char c2 = c = content[num];
					if (!c2.Equals("\n"))
					{
						continue;
					}
				}
				content.Remove(num, 1);
			}
			return content;
		}

		public static string FilterUnsafeHtml(string content)
		{
			content = Regex.Replace(content, "(\\<|\\s+)o([a-z]+\\s?=)", "$1$2", RegexOptions.IgnoreCase);
			content = Regex.Replace(content, "(script|frame|form|meta|behavior|style)([\\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
			return content;
		}

		public static string FilterUserInput(string inputStr)
		{
			return Regex.Replace(inputStr.Trim(), "[^\\w\\.@-]", "");
		}

		public static string FilterXHtml(string content)
		{
			return FilterXHtml(content, true);
		}

		public static string FilterXHtml(string content, bool encode)
		{
			if (!string.IsNullOrEmpty(content))
			{
				content = Regex.Replace(content, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "-->", "", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "<!--.*", "", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
				content = Regex.Replace(content, "&#(\\d+);", "", RegexOptions.IgnoreCase);
				content.Replace("<", "");
				content.Replace(">", "");
				content.Replace("\r\n", "");
				if (encode)
				{
					content = HttpUtility.HtmlEncode(content).Trim();
				}
			}
			return content;
		}

		public static string FilterSql(string content)
		{
			if (string.IsNullOrEmpty(content))
			{
				return "";
			}
			string pattern = "select|insert|delete|from|count\\(|drop table|update|truncate|asc\\(|mid\\(|char\\(|xp_cmdshell|exec master|net localgroup administrators|:|net user|\"|\\'|or|exec|exectue|sp_executesql";
			return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
		}

		public static string Process(FilterType filterType, string filterContent)
		{
			switch (filterType)
			{
			case FilterType.Script:
				filterContent = FilterScript(filterContent);
				return filterContent;
			case FilterType.Html:
				filterContent = FilterHtml(filterContent);
				return filterContent;
			case FilterType.Object:
				filterContent = FilterObject(filterContent);
				return filterContent;
			case FilterType.AHrefScript:
				filterContent = FilterAHrefScript(filterContent);
				return filterContent;
			case FilterType.Iframe:
				filterContent = FilterIframe(filterContent);
				return filterContent;
			case FilterType.Frameset:
				filterContent = FilterFrameset(filterContent);
				return filterContent;
			case FilterType.Src:
				filterContent = FilterSrc(filterContent);
				return filterContent;
			case FilterType.BadWords:
				filterContent = FilterBadWords(filterContent);
				return filterContent;
			case FilterType.Sql:
			case FilterType.Html | FilterType.BadWords:
			case FilterType.Script | FilterType.Html | FilterType.BadWords:
			case FilterType.AHrefScript | FilterType.BadWords:
			case FilterType.Script | FilterType.AHrefScript | FilterType.BadWords:
			case FilterType.Html | FilterType.AHrefScript | FilterType.BadWords:
			case FilterType.Script | FilterType.Html | FilterType.AHrefScript | FilterType.BadWords:
				return filterContent;
			case FilterType.All:
				filterContent = FilterAll(filterContent);
				return filterContent;
			default:
				return filterContent;
			}
		}

		private static string StripAttributesHandler(Match m)
		{
			if (m.Groups["attribute"].Success)
			{
				return m.Value.Replace(m.Groups["attribute"].Value, "");
			}
			return m.Value;
		}

		private static string StripScriptAttributesFromTags(string content)
		{
			string text = "(on(blur|c(hange|lick)|dblclick|focus|keypress|(key|mouse)(down|up)|(un)?load\r\n|mouse(move|o(ut|ver))|reset|s(elect|ubmit)))|javascript";
			Regex regex = new Regex(string.Format("(?inx)\r\n\t\t\t\t\\<(\\w+)\\s+\r\n\t\t\t\t\t(\r\n\t\t\t\t\t\t(?'attribute'\r\n\t\t\t\t\t\t(?'attributeName'{0})\\s*=\\s*\r\n\t\t\t\t\t\t(?'delim'['\"]?)\r\n\t\t\t\t\t\t(?'attributeValue'[^'\">]+)\r\n\t\t\t\t\t\t(\\3)\r\n\t\t\t\t\t)\r\n\t\t\t\t\t|\r\n\t\t\t\t\t(?'attribute'\r\n\t\t\t\t\t\t(?'attributeName'href)\\s*=\\s*\r\n\t\t\t\t\t\t(?'delim'['\"]?)\r\n\t\t\t\t\t\t(?'attributeValue'javascript[^'\">]+)\r\n\t\t\t\t\t\t(\\3)\r\n\t\t\t\t\t)\r\n\t\t\t\t\t|\r\n\t\t\t\t\t[^>]\r\n\t\t\t\t)*\r\n\t\t\t\\>", text));
			content = regex.Replace(content, StripAttributesHandler);
			return Regex.Replace(content, text, "", RegexOptions.IgnoreCase);
		}
	}
}

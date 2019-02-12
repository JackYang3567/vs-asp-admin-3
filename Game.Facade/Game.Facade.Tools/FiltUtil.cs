using System.Text.RegularExpressions;
using System.Web;

namespace Game.Facade.Tools
{
	public class FiltUtil
	{
		public static string GetSafeSQL(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			value = Regex.Replace(value, ";", string.Empty);
			value = Regex.Replace(value, "'", string.Empty);
			value = Regex.Replace(value, "&", string.Empty);
			value = Regex.Replace(value, "%20", string.Empty);
			value = Regex.Replace(value, "--", string.Empty);
			value = Regex.Replace(value, "==", string.Empty);
			value = Regex.Replace(value, "<", string.Empty);
			value = Regex.Replace(value, ">", string.Empty);
			value = Regex.Replace(value, "%", string.Empty);
			value = Regex.Replace(value, "select", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "insert", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "delete from", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "count''", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "drop table", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "truncate", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "asc", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "mid", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "xp_cmdshell", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "exec master", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "net localgroup administrators", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "net user", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "net", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "delete", "", RegexOptions.IgnoreCase);
			value = Regex.Replace(value, "drop", "", RegexOptions.IgnoreCase);
			return value;
		}

		public static string GetNoHtmlStr(string Htmlstring)
		{
			if (Htmlstring == null)
			{
				return "";
			}
			Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "select", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "insert", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "delete from", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "count''", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "drop table", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "asc", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "mid", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "exec master", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "and", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "net user", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "or", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "net", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "-", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "delete", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "drop", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "script", "", RegexOptions.IgnoreCase);
			Htmlstring = Htmlstring.Replace("<", "");
			Htmlstring = Htmlstring.Replace(">", "");
			Htmlstring = Htmlstring.Replace("*", "");
			Htmlstring = Htmlstring.Replace("-", "");
			Htmlstring = Htmlstring.Replace("?", "");
			Htmlstring = Htmlstring.Replace("'", "''");
			Htmlstring = Htmlstring.Replace(",", "");
			Htmlstring = Htmlstring.Replace("/", "");
			Htmlstring = Htmlstring.Replace(";", "");
			Htmlstring = Htmlstring.Replace("*/", "");
			Htmlstring = Htmlstring.Replace("\r\n", "");
			Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
			return Htmlstring;
		}
	}
}

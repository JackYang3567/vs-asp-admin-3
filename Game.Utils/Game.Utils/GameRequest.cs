using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Game.Utils
{
	public class GameRequest
	{
		private static readonly string[] _WebSearchList = new string[27]
		{
			"google",
			"isaac",
			"surveybot",
			"baiduspider",
			"yahoo",
			"yisou",
			"3721",
			"qihoo",
			"daqi",
			"ia_archiver",
			"p.arthur",
			"fast-webcrawler",
			"java",
			"microsoft-atl-native",
			"turnitinbot",
			"webgather",
			"sleipnir",
			"msn",
			"sogou",
			"lycos",
			"tom",
			"iask",
			"soso",
			"sina",
			"baidu",
			"gougou",
			"zhongsou"
		};

		public static HttpRequest Request
		{
			get
			{
				HttpContext current = HttpContext.Current;
				if (current == null || current.Request == null)
				{
					return null;
				}
				return current.Request;
			}
		}

		public static bool IsPostFromAnotherDomain
		{
			get
			{
				if (HttpContext.Current.Request.HttpMethod == "GET")
				{
					return false;
				}
				return GetUrlReferrer().IndexOf(GetServerDomain()) == -1;
			}
		}

		private GameRequest()
		{
		}

		public static string GetCurrentFullHost()
		{
			HttpRequest request = HttpContext.Current.Request;
			if (!request.Url.IsDefaultPort)
			{
				return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
			}
			return request.Url.Host;
		}

		public static string GetHost()
		{
			if (HttpContext.Current == null)
			{
				return string.Empty;
			}
			return HttpContext.Current.Request.Url.Host;
		}

		public static float GetFloat(string strName, float defValue)
		{
			if (GetQueryFloat(strName, defValue) == defValue)
			{
				return GetFormFloat(strName, defValue);
			}
			return GetQueryFloat(strName, defValue);
		}

		public static float GetFormFloat(string strName, float defValue)
		{
			return TypeParse.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
		}

		public static int GetFormInt(string strName, int defValue)
		{
			return GetFormInt(Request, strName, defValue);
		}

		public static int GetFormInt(HttpRequest request, string strName, int defValue)
		{
			return TypeParse.StrToInt(request.Form[strName], defValue);
		}

		public static string GetFormString(string strName)
		{
			return GetFormString(Request, strName);
		}

		public static string GetFormString(HttpRequest request, string strName)
		{
			if (request == null || request.Form[strName] == null)
			{
				return string.Empty;
			}
			return request.Form[strName];
		}

		public static int GetInt(string strName, int defValue)
		{
			if (GetQueryInt(strName, defValue) == defValue)
			{
				return GetFormInt(strName, defValue);
			}
			return GetQueryInt(strName, defValue);
		}

		public static string GetPageName()
		{
			string[] array = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
			return array[array.Length - 1].ToLower();
		}

		public static int GetParamCount()
		{
			return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
		}

		public static float GetQueryFloat(string strName, float defValue)
		{
			return TypeParse.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
		}

		public static int GetQueryInt(string strName, int defValue)
		{
			return GetQueryInt(Request, strName, defValue);
		}

		public static int GetQueryInt(HttpRequest request, string strName, int defValue)
		{
			return TypeParse.StrToInt(request.QueryString[strName], defValue);
		}

		public static string GetQueryString(string strName)
		{
			return GetQueryString(Request, strName);
		}

		public static string GetQueryString(HttpRequest request, string strName)
		{
			if (request == null || request.QueryString[strName] == null)
			{
				return string.Empty;
			}
			return request.QueryString[strName];
		}

		public static string GetRawUrl()
		{
			return HttpContext.Current.Request.RawUrl;
		}

		public static string GetServerDomain()
		{
			string text = HttpContext.Current.Request.Url.Host.ToLower();
			if (text.Split('.').Length < 3 || Validate.IsIP(text))
			{
				return text;
			}
			string text2 = text.Remove(0, text.IndexOf(".") + 1);
			if (text2.StartsWith("com.") || text2.StartsWith("net.") || text2.StartsWith("org.") || text2.StartsWith("gov."))
			{
				return text;
			}
			return text2;
		}

		public static string GetServerString(string strName)
		{
			if (HttpContext.Current.Request.ServerVariables[strName] == null)
			{
				return "";
			}
			return HttpContext.Current.Request.ServerVariables[strName].ToString();
		}

		public static string GetString(string strName)
		{
			if ("".Equals(GetQueryString(strName)))
			{
				return GetFormString(strName);
			}
			return GetQueryString(strName);
		}

		public static string GetString(HttpRequest request, string strName)
		{
			if ("".Equals(GetQueryString(request, strName)))
			{
				return GetFormString(request, strName);
			}
			return GetQueryString(request, strName);
		}

		public static string GetUrl()
		{
			return HttpContext.Current.Request.Url.ToString();
		}

		public static string GetUrlReferrer()
		{
			Uri urlReferrer = HttpContext.Current.Request.UrlReferrer;
			if (urlReferrer == (Uri)null)
			{
				return string.Empty;
			}
			return Convert.ToString(urlReferrer);
		}

		public static string GetUserBrowser()
		{
			string result = "Unknown";
			if (Request == null)
			{
				return result;
			}
			string userAgent = Request.UserAgent;
			string a;
			if ((a = userAgent) == null || a == "")
			{
				return result;
			}
			userAgent = userAgent.ToLower();
			HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
			if (userAgent.IndexOf("firefox") >= 0 || userAgent.IndexOf("firebird") >= 0 || userAgent.IndexOf("myie") >= 0 || userAgent.IndexOf("opera") >= 0 || userAgent.IndexOf("netscape") >= 0 || userAgent.IndexOf("msie") >= 0)
			{
				return browser.Browser + browser.Version;
			}
			return "Unknown";
		}

		public static string GetUserIP()
		{
			if (HttpContext.Current == null)
			{
				return string.Empty;
			}
			string empty = string.Empty;
			empty = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			string a;
			if ((a = empty) == null || a == "")
			{
				empty = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			}
			if (empty == null || empty == string.Empty)
			{
				empty = HttpContext.Current.Request.UserHostAddress;
			}
			if (empty == null || !(empty != string.Empty) || !Validate.IsIP(empty))
			{
				return "0.0.0.0";
			}
			return empty;
		}

		public static string GetUserOsname()
		{
			string result = "Unknown";
			if (Request != null)
			{
				string userAgent = Request.UserAgent;
				string a;
				if ((a = userAgent) == null || a == "")
				{
					return result;
				}
				if (userAgent.Contains("NT 6.1"))
				{
					result = "Windows 7";
				}
				else
				{
					if (userAgent.Contains("NT 6.0"))
					{
						return "Windows Vista/Server 2008";
					}
					if (userAgent.Contains("NT 5.2"))
					{
						return "Windows Server 2003";
					}
					if (userAgent.Contains("NT 5.1"))
					{
						return "Windows XP";
					}
					if (userAgent.Contains("NT 5"))
					{
						return "Windows 2000";
					}
					if (userAgent.Contains("NT 4"))
					{
						return "Windows NT4";
					}
					if (userAgent.Contains("Me"))
					{
						return "Windows Me";
					}
					if (userAgent.Contains("98"))
					{
						return "Windows 98";
					}
					if (userAgent.Contains("95"))
					{
						return "Windows 95";
					}
					if (userAgent.Contains("Mac"))
					{
						return "Mac";
					}
					if (userAgent.Contains("Unix"))
					{
						return "UNIX";
					}
					if (userAgent.Contains("Linux"))
					{
						result = "Linux";
					}
					else if (userAgent.Contains("SunOS"))
					{
						result = "SunOS";
					}
				}
			}
			return result;
		}

		public static bool IsBrowserGet()
		{
			string[] array = new string[6]
			{
				"ie",
				"opera",
				"netscape",
				"mozilla",
				"konqueror",
				"firefox"
			};
			string text = HttpContext.Current.Request.Browser.Type.ToLower();
			for (int i = 0; i < array.Length; i++)
			{
				if (text.IndexOf(array[i]) >= 0)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsCrossSitePost()
		{
			if (HttpContext.Current.Request.HttpMethod.Equals("POST"))
			{
				return IsCrossSitePost(GetUrlReferrer(), HttpContext.Current.Request.Url.Host);
			}
			return true;
		}

		public static bool IsCrossSitePost(string urlReferrer, string host)
		{
			if (urlReferrer.Length < 7)
			{
				return true;
			}
			string text = urlReferrer.Remove(0, 7);
			text = ((text.IndexOf(":") <= -1) ? text.Substring(0, text.IndexOf('/')) : text.Substring(0, text.IndexOf(":")));
			return text != host;
		}

		public static bool IsGet()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("GET");
		}

		public static bool IsPost()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("POST");
		}

		public static bool IsRobots()
		{
			return IsSearchEnginesGet();
		}

		public static bool IsSearchEnginesGet()
		{
			string userAgent = HttpContext.Current.Request.UserAgent;
			if (userAgent == null || string.Empty == userAgent)
			{
				return true;
			}
			userAgent = userAgent.ToLower();
			for (int i = 0; i < _WebSearchList.Length; i++)
			{
				if (-1 != userAgent.IndexOf(_WebSearchList[i]))
				{
					return true;
				}
			}
			return GetUserBrowser().Equals("Unknown");
		}

		public static void SaveRequestFile(string path)
		{
			if (HttpContext.Current.Request.Files.Count > 0)
			{
				HttpContext.Current.Request.Files[0].SaveAs(path);
			}
		}

		public static string GetSubDomain()
		{
			string result = null;
			Regex regex = new Regex("http://((\\.|\\w)+)(/?)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
			string input = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
			Match match = regex.Match(input);
			if (match.Success)
			{
				result = match.Groups[1].Value.Split('.')[0];
			}
			return result;
		}
	}
}

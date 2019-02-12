using Game.Entity.Accounts;
using Game.Entity.PlatformManager;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Game.Facade
{
	public class AdminCookie
	{
		private static string ValidateKey = "{2EF1D4CB-16BA-471D-9DFC-12C1E4D15253}";

		private static string ValidateName = "VS";

		private static string ExpireTimeStr = "_ETS";

		protected static string CookiesName
		{
			get
			{
				string appSetting = Utility.GetAppSetting("CookiesName");
				if (!string.IsNullOrEmpty(appSetting))
				{
					return appSetting;
				}
				if (!string.IsNullOrEmpty(Fetch.GetCookieName))
				{
					return Fetch.GetCookieName;
				}
				return "Default";
			}
		}

		protected static DateTime CookiesExpire
		{
			get
			{
				int result;
				if (!int.TryParse(Utility.GetAppSetting("CookiesExpireMinutes"), out result))
				{
					result = 30;
				}
				return DateTime.Now.AddMinutes((double)result);
			}
		}

		protected static string CookiesPath
		{
			get
			{
				string appSetting = Utility.GetAppSetting("CookiesPath");
				if (!string.IsNullOrEmpty(appSetting))
				{
					return appSetting;
				}
				return "/";
			}
		}

		protected static string CookiesDomain
		{
			get
			{
				string appSetting = Utility.GetAppSetting("CookiesDomain");
				if (!string.IsNullOrEmpty(appSetting))
				{
					return appSetting;
				}
				return "";
			}
		}

		public static void SetUserCookie(Base_Users user)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("UserID", user.UserID);
			dictionary.Add("Username", user.Username);
			dictionary.Add("RoleID", user.RoleID);
			dictionary.Add("IsBand", user.IsBand);
			Add(dictionary, 30);
		}

		public static Base_Users GetUserFromCookie()
		{
			HttpContext current = HttpContext.Current;
			if (current == null)
			{
				return null;
			}
			Base_Users base_Users = new Base_Users();
			object value = GetValue("UserID");
			object value2 = GetValue("Username");
			object value3 = GetValue("RoleID");
			object value4 = GetValue("IsBand");
			if (value == null || value2 == null || value3 == null || value4 == null)
			{
				return null;
			}
			base_Users.UserID = int.Parse(value.ToString());
			base_Users.Username = value2.ToString();
			base_Users.RoleID = int.Parse(value3.ToString());
			base_Users.IsBand = int.Parse(value4.ToString());
			SetUserCookie(base_Users);
			return base_Users;
		}

		public static AgentInfo GetAgentInfoFromCookie()
		{
			HttpContext current = HttpContext.Current;
			if (current == null)
			{
				return null;
			}
			if (current.Session["agentinfo"] == null)
			{
				return null;
			}
			return current.Session["agentinfo"] as AgentInfo;
		}

		public static void SetAgentCookieNew(AgentInfo user)
		{
			HttpContext.Current.Session["agentinfo"] = user;
		}

		public static void ClearUserCookie()
		{
			if (HttpContext.Current != null)
			{
				HttpCookie httpCookie = HttpContext.Current.Request.Cookies[Fetch.GetCookieName];
				httpCookie.Expires = DateTime.Now.AddYears(-1);
				HttpContext.Current.Response.Cookies.Add(httpCookie);
			}
		}

		public static bool CheckedUserLogon()
		{
			Base_Users userFromCookie = GetUserFromCookie();
			if (userFromCookie == null || userFromCookie.UserID <= 0 || userFromCookie.RoleID <= 0)
			{
				return false;
			}
			SetUserCookie(userFromCookie);
			return true;
		}

		private static string GetCookie(string strKey)
		{
			return Utility.UrlDecode(Utility.GetCookie(Fetch.GetCookieName, strKey));
		}

		public static void Add(string key, object value, int? timeout)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookiesName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(CookiesName);
			}
			httpCookie.Expires = DateTime.Now.AddYears(50);
			httpCookie.Domain = CookiesDomain;
			httpCookie.Values[key] = HttpUtility.UrlEncode(value.ToString());
			httpCookie.Values[key + ExpireTimeStr] = ((!timeout.HasValue) ? HttpUtility.UrlEncode(CookiesExpire.ToString("yyyy-MM-dd HH:mm:ss")) : HttpUtility.UrlEncode(DateTime.Now.AddMinutes((double)timeout.Value).ToString("yyyy-MM-dd HH:mm:ss")));
			httpCookie.Values[ValidateName] = GetValidateStr(httpCookie);
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public static void Add(Dictionary<string, object> dic, int? timeout)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookiesName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(CookiesName);
			}
			httpCookie.Expires = DateTime.Now.AddYears(50);
			httpCookie.Domain = CookiesDomain;
			foreach (KeyValuePair<string, object> item in dic)
			{
				httpCookie.Values[item.Key] = HttpUtility.UrlEncode(item.Value.ToString());
				httpCookie.Values[item.Key + ExpireTimeStr] = ((!timeout.HasValue) ? HttpUtility.UrlEncode(CookiesExpire.ToString("yyyy-MM-dd HH:mm:ss")) : HttpUtility.UrlEncode(DateTime.Now.AddMinutes((double)timeout.Value).ToString("yyyy-MM-dd HH:mm:ss")));
			}
			httpCookie.Values[ValidateName] = GetValidateStr(httpCookie);
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public static object GetValue(string key)
		{
			if (key != null && key != "")
			{
				HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookiesName];
				if (httpCookie == null)
				{
					return null;
				}
				httpCookie.Expires = DateTime.Now.AddYears(50);
				httpCookie.Domain = CookiesDomain;
				if (!ValidateCookies(httpCookie))
				{
					httpCookie.Expires = DateTime.Now.AddYears(-1);
					HttpContext.Current.Response.Cookies.Add(httpCookie);
					return null;
				}
				string text = httpCookie.Values[key + ExpireTimeStr];
				DateTime result;
				if (string.IsNullOrEmpty(text) || !DateTime.TryParse(HttpUtility.UrlDecode(text), out result))
				{
					httpCookie.Values[key] = "";
					httpCookie.Values[key + ExpireTimeStr] = "";
					httpCookie.Values[ValidateName] = GetValidateStr(httpCookie);
					HttpContext.Current.Response.Cookies.Add(httpCookie);
					return null;
				}
				DateTime now = DateTime.Now;
				if (result > now)
				{
					return HttpUtility.UrlDecode(httpCookie.Values[key]);
				}
				httpCookie.Values[key] = "";
				httpCookie.Values[key + ExpireTimeStr] = "";
				httpCookie.Values[ValidateName] = GetValidateStr(httpCookie);
				HttpContext.Current.Response.Cookies.Add(httpCookie);
			}
			return null;
		}

		private static string GetValidateStr(HttpCookie ck)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] allKeys = ck.Values.AllKeys;
			foreach (string text in allKeys)
			{
				if (text != ValidateName)
				{
					stringBuilder.Append(ck.Values[text]);
				}
			}
			stringBuilder.Append(ValidateKey);
			stringBuilder.Append(HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"]);
			stringBuilder.Append(HttpContext.Current.Request.ServerVariables["INSTANCE_ID"]);
			stringBuilder.Append(HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"]);
			return FormsAuthentication.HashPasswordForStoringInConfigFile(stringBuilder.ToString(), "md5").ToLower().Substring(8, 16);
		}

		private static bool ValidateCookies(HttpCookie ck)
		{
			string text = ck.Values[ValidateName];
			StringBuilder stringBuilder = new StringBuilder();
			string[] allKeys = ck.Values.AllKeys;
			foreach (string text2 in allKeys)
			{
				if (text2 != ValidateName)
				{
					stringBuilder.Append(ck.Values[text2]);
				}
			}
			stringBuilder.Append(ValidateKey);
			stringBuilder.Append(HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"]);
			stringBuilder.Append(HttpContext.Current.Request.ServerVariables["INSTANCE_ID"]);
			stringBuilder.Append(HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"]);
			string value = FormsAuthentication.HashPasswordForStoringInConfigFile(stringBuilder.ToString(), "md5").ToLower().Substring(8, 16);
			return text.Equals(value);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Game.Utils.Cache
{
	public class CookiesCache : ICache
	{
		private string _cookiespath = "";

		protected string ValidateKey = "{D20BA3E5-47C9-471f-94E3-5E810B518EB3}";

		protected string ValidateName = "VS";

		public string CookiesPath
		{
			get
			{
				if (!string.IsNullOrEmpty(_cookiespath))
				{
					return _cookiespath;
				}
				return "/";
			}
			set
			{
				_cookiespath = value;
			}
		}

		public DateTime CookiesExpire
		{
			get
			{
				int result;
				if (!int.TryParse(ConfigurationManager.AppSettings["CookiesExpireHours"], out result))
				{
					result = 30;
				}
				return DateTime.Now.AddMinutes((double)result);
			}
		}

		protected string CookiesName
		{
			get
			{
				string text = ConfigurationManager.AppSettings["CookiesName"];
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				return "Default";
			}
		}

		public string ExpireTimeStr
		{
			get
			{
				return "_ETS";
			}
		}

		public object this[string key]
		{
			get
			{
				return GetValue(key);
			}
		}

		public void Add(Dictionary<string, object> dic)
		{
			Add(dic, null);
		}

		public void Add(Dictionary<string, object> dic, int? timeout)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookiesName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(CookiesName);
			}
			httpCookie.Expires = DateTime.Now.AddYears(50);
			httpCookie.Domain = _cookiespath;
			foreach (KeyValuePair<string, object> item in dic)
			{
				httpCookie.Values[item.Key] = HttpUtility.UrlEncode(item.Value.ToString());
				httpCookie.Values[item.Key + ExpireTimeStr] = ((!timeout.HasValue) ? HttpUtility.UrlEncode(CookiesExpire.ToString("yyyy-MM-dd HH:mm:ss")) : HttpUtility.UrlEncode(DateTime.Now.AddMinutes((double)timeout.Value).ToString("yyyy-MM-dd HH:mm:ss")));
			}
			httpCookie.Values[ValidateName] = GetValidateStr(httpCookie);
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public void Add(string key, object value)
		{
			Add(key, value, null);
		}

		public void Add(string key, object value, int? timeout)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookiesName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(CookiesName);
			}
			httpCookie.Expires = DateTime.Now.AddYears(50);
			httpCookie.Domain = _cookiespath;
			httpCookie.Values[key] = HttpUtility.UrlEncode(value.ToString());
			httpCookie.Values[key + ExpireTimeStr] = ((!timeout.HasValue) ? HttpUtility.UrlEncode(CookiesExpire.ToString("yyyy-MM-dd HH:mm:ss")) : HttpUtility.UrlEncode(DateTime.Now.AddMinutes((double)timeout.Value).ToString("yyyy-MM-dd HH:mm:ss")));
			httpCookie.Values[ValidateName] = GetValidateStr(httpCookie);
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public void Delete()
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookiesName];
			httpCookie.Expires = DateTime.Now.AddYears(-1);
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public void Delete(string key)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookiesName];
			httpCookie.Expires = DateTime.Now.AddYears(50);
			httpCookie.Domain = _cookiespath;
			httpCookie.Values[key] = "";
			httpCookie.Values[key + ExpireTimeStr] = "";
			httpCookie.Values[ValidateName] = GetValidateStr(httpCookie);
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public void Update(Dictionary<string, object> dic)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookiesName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(CookiesName);
			}
			httpCookie.Expires = DateTime.Now.AddYears(50);
			httpCookie.Domain = _cookiespath;
			foreach (KeyValuePair<string, object> item in dic)
			{
				httpCookie.Values[item.Key] = HttpUtility.UrlEncode(item.Value.ToString());
			}
			httpCookie.Values[ValidateName] = GetValidateStr(httpCookie);
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public void Update(string key, object value)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookiesName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(CookiesName);
			}
			httpCookie.Expires = DateTime.Now.AddYears(50);
			httpCookie.Domain = _cookiespath;
			httpCookie.Values[key] = HttpUtility.UrlEncode(value.ToString());
			httpCookie.Values[ValidateName] = GetValidateStr(httpCookie);
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public object GetValue(string key)
		{
			if (key == null || key == "")
			{
				return null;
			}
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookiesName];
			if (httpCookie == null)
			{
				return null;
			}
			httpCookie.Expires = DateTime.Now.AddYears(50);
			httpCookie.Domain = _cookiespath;
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
			return null;
		}

		private string GetValidateStr(HttpCookie ck)
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

		private bool ValidateCookies(HttpCookie ck)
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
			if (text.Equals(value))
			{
				return true;
			}
			return false;
		}
	}
}

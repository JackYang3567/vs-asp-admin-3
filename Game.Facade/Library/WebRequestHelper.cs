using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Library
{
	public class WebRequestHelper
	{
		public static string PostHttp(string url, string data, string contentType)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = contentType;
			httpWebRequest.Method = "POST";
			httpWebRequest.Timeout = 20000;
			byte[] bytes = Encoding.UTF8.GetBytes(data);
			httpWebRequest.ContentLength = bytes.Length;
			httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
			string result = streamReader.ReadToEnd();
			httpWebResponse.Close();
			streamReader.Close();
			httpWebRequest.Abort();
			httpWebResponse.Close();
			return result;
		}

		public static string Request_WebClient(string uri, string paramStr, Encoding encoding, string username, string password)
		{
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			string empty = string.Empty;
			WebClient webClient = new WebClient();
			webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
			byte[] bytes = encoding.GetBytes(paramStr);
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
			{
				webClient.Credentials = GetCredentialCache(uri, username, password);
				webClient.Headers.Add("Authorization", GetAuthorization(username, password));
			}
			byte[] bytes2 = webClient.UploadData(uri, "POST", bytes);
			return encoding.GetString(bytes2);
		}

		public static string WebApiPost(string uri, string postData)
		{
			HttpContent httpContent = new StringContent(postData);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			HttpClient httpClient = new HttpClient();
			return httpClient.PostAsync(uri, httpContent).Result.Content.ReadAsStringAsync().Result;
		}

		public static string GetHttp(string baseUrl, Dictionary<string, string> dictParam, bool isurlencode = true)
		{
			string empty = string.Empty;
			string lastUrl = GetLastUrl(baseUrl, dictParam, isurlencode);
			WebRequest webRequest = WebRequest.Create(new Uri(lastUrl));
			webRequest.Timeout = 20000;
			WebResponse response = webRequest.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
			empty = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			return empty;
		}

		private static CredentialCache GetCredentialCache(string uri, string username, string password)
		{
			string.Format("{0}:{1}", username, password);
			CredentialCache credentialCache = new CredentialCache();
			credentialCache.Add(new Uri(uri), "Basic", new NetworkCredential(username, password));
			return credentialCache;
		}

		private static string GetAuthorization(string username, string password)
		{
			string s = string.Format("{0}:{1}", username, password);
			return "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(s));
		}

		private static string GetLastUrl(string baseUrl, Dictionary<string, string> dictParam, bool isurlencode = true)
		{
			StringBuilder stringBuilder = new StringBuilder(baseUrl);
			if (dictParam != null && dictParam.Count > 0)
			{
				stringBuilder.Append("?");
				int num = 0;
				foreach (KeyValuePair<string, string> item in dictParam)
				{
					stringBuilder.Append(string.Format("{0}={1}", item.Key, isurlencode ? HttpUtility.UrlEncode(item.Value, Encoding.UTF8) : item.Value));
					if (num < dictParam.Count - 1)
					{
						stringBuilder.Append("&");
					}
					num++;
				}
			}
			return stringBuilder.ToString();
		}

		public static string GetPostData(Dictionary<string, string> dictParam, bool isurlencode = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (dictParam != null && dictParam.Count > 0)
			{
				int num = 0;
				foreach (KeyValuePair<string, string> item in dictParam)
				{
					stringBuilder.Append(string.Format("{0}={1}", item.Key, isurlencode ? HttpUtility.UrlEncode(item.Value, Encoding.UTF8) : item.Value));
					if (num < dictParam.Count - 1)
					{
						stringBuilder.Append("&");
					}
					num++;
				}
			}
			return stringBuilder.ToString();
		}

		public static string CreatFormHtml(string actionUrl, SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<form id='paysubmit' name='paysubmit' action='" + actionUrl + "' method='" + strMethod.ToLower().Trim() + "'>");
			foreach (KeyValuePair<string, string> item in sParaTemp)
			{
				stringBuilder.Append("<input type='hidden' name='" + item.Key + "' value='" + item.Value + "'/>");
			}
			stringBuilder.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
			stringBuilder.Append("<script>document.forms['paysubmit'].submit();</script>");
			return stringBuilder.ToString();
		}
	}
}

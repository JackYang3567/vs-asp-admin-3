using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Game.Facade
{
	public class HttpHelper
	{
		public static string HttpRequest(string url, string param)
		{
			return HttpRequest(url, param, "post");
		}

		public static string HttpRequest(string url, string param, string method)
		{
			return HttpRequest(url, param, method, "UTF-8");
		}

		public static string HttpRequest(string url, string param, string method, string charset)
		{
			if (method.ToLower() == "get")
			{
				url = url + "?" + param;
			}
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				if (method.ToLower() == "post")
				{
					byte[] bytes = Encoding.GetEncoding(charset).GetBytes(param);
					httpWebRequest.Method = "POST";
					httpWebRequest.ContentType = "application/x-www-form-urlencoded";
					httpWebRequest.ContentLength = bytes.Length;
					using (Stream stream = httpWebRequest.GetRequestStream())
					{
						stream.Write(bytes, 0, bytes.Length);
					}
				}
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				return new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(charset)).ReadToEnd();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		public static string GetParam(Dictionary<string, string> dic)
		{
			string text = "";
			foreach (KeyValuePair<string, string> item in dic)
			{
				string text2 = text;
				text = text2 + item.Key + "=" + item.Value + "&";
			}
			return text.Remove(text.Length - 1);
		}

		public static string CreatFormHtml(string actionUrl, SortedDictionary<string, string> sParaTemp, string strMethod)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<form id='paysubmit' name='paysubmit' action='" + actionUrl + "' method='" + strMethod.ToLower().Trim() + "'>");
			foreach (KeyValuePair<string, string> item in sParaTemp)
			{
				stringBuilder.Append("<input type='hidden' name='" + item.Key + "' value='" + item.Value + "'/>");
			}
			stringBuilder.Append("</form>");
			stringBuilder.Append("<script>document.forms['paysubmit'].submit();</script>");
			return stringBuilder.ToString();
		}
	}
}

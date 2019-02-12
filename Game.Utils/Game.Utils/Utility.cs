using Microsoft.JScript;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Utils
{
	public class Utility
	{
		public const string ASSEMBLY_VERSION = "4.0.0";

		public static string CurrentPath
		{
			get
			{
				if (HttpContext.Current == null)
				{
					return string.Empty;
				}
				string path = HttpContext.Current.Request.Path;
				path = path.Substring(0, path.LastIndexOf("/"));
				if (path == "/")
				{
					return string.Empty;
				}
				return path;
			}
		}

		public static string CurrentUrl
		{
			get
			{
				return GameRequest.GetUrl();
			}
		}

		public static string GetAppLogDirectory
		{
			get
			{
				string text = ConfigurationManager.AppSettings["AppLogDirectory"];
				if (string.IsNullOrEmpty(text))
				{
					text = "AppLog";
				}
				text = TextUtility.GetFullPath(text);
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				return text;
			}
		}

		public static string GetIPDbFilePath
		{
			get
			{
				string text = ApplicationSettings.Get("IPDBFilePath");
				if (string.IsNullOrEmpty(text))
				{
					return "/Config/IPData.dat";
				}
				return text;
			}
		}

		public static bool GetWriteAppLog
		{
			get
			{
				bool result = false;
				string value = ConfigurationManager.AppSettings["WriteAppLog"];
				if (!string.IsNullOrEmpty(value))
				{
					result = System.Convert.ToBoolean(value);
				}
				return result;
			}
		}

		public static string RawUrl
		{
			get
			{
				return GameRequest.GetRawUrl();
			}
		}

		public static string Referrer
		{
			get
			{
				return GameRequest.GetUrlReferrer();
			}
		}

		public static string ServerDomain
		{
			get
			{
				return GameRequest.GetServerDomain();
			}
		}

		public static string UserBrowser
		{
			get
			{
				return GameRequest.GetUserBrowser();
			}
		}

		public static string UserIP
		{
			get
			{
				return GameRequest.GetUserIP();
			}
		}

		public static void Aspx2XHtml(string path, string outPath)
		{
			Page page = new Page();
			StringWriter stringWriter = new StringWriter();
			page.Server.Execute(path, stringWriter);
			FileStream fileStream;
			if (File.Exists(page.Server.MapPath(outPath)))
			{
				File.Delete(page.Server.MapPath(outPath));
				fileStream = File.Create(page.Server.MapPath(outPath));
			}
			else
			{
				fileStream = File.Create(page.Server.MapPath(outPath));
			}
			byte[] bytes = Encoding.UTF8.GetBytes(stringWriter.ToString());
			fileStream.Write(bytes, 0, bytes.Length);
			fileStream.Close();
		}

		public static void ClearPageClientCache()
		{
			if (HttpContext.Current != null)
			{
				HttpContext.Current.Response.Buffer = false;
				HttpContext.Current.Response.Expires = 0;
				HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
				HttpContext.Current.Response.AddHeader("pragma", "no-cache");
				HttpContext.Current.Response.AddHeader("cache-control", "private");
				HttpContext.Current.Response.CacheControl = "no-cache";
				HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
				HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(true);
				HttpContext.Current.Response.Cookies.Clear();
			}
		}

		public static void SetPageNoCache()
		{
			if (HttpContext.Current != null)
			{
				HttpContext.Current.Response.Buffer = true;
				HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);
				HttpContext.Current.Response.Expires = 0;
				HttpContext.Current.Response.CacheControl = "no-cache";
				HttpContext.Current.Response.AddHeader("Pragma", "No-Cache");
			}
		}

		public static int ConvertVersionStr2Int(string strVersion)
		{
			if (!Validate.IsIP(strVersion))
			{
				return 0;
			}
			string[] array = strVersion.Split('.');
			return (System.Convert.ToInt32(array[0]) << 24) | (System.Convert.ToInt32(array[1]) << 16) | (System.Convert.ToInt32(array[2]) << 8) | System.Convert.ToInt32(array[3]);
		}

		public static StringBuilder DataTableToJSON(DataTable dt)
		{
			return DataTableToJson(dt, true);
		}

		public static StringBuilder DataTableToJson(DataTable dt, bool dtDispose)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[\r\n");
			string[] array = new string[dt.Columns.Count];
			int num = 0;
			string text = "{{";
			string text2 = "";
			foreach (DataColumn column in dt.Columns)
			{
				array[num] = column.Caption.ToLower().Trim();
				text = text + "'" + column.Caption.ToLower().Trim() + "':";
				text2 = column.DataType.ToString().Trim().ToLower();
				if (text2.IndexOf("int") > 0 || text2.IndexOf("deci") > 0 || text2.IndexOf("floa") > 0 || text2.IndexOf("doub") > 0 || text2.IndexOf("bool") > 0)
				{
					object obj = text;
					text = obj + "{" + num + "}";
				}
				else
				{
					object obj = text;
					text = obj + "'{" + num + "}'";
				}
				text += ",";
				num++;
			}
			if (text.EndsWith(","))
			{
				text = text.Substring(0, text.Length - 1);
			}
			text += "}},";
			num = 0;
			object[] array2 = new object[array.Length];
			foreach (DataRow row in dt.Rows)
			{
				string[] array3 = array;
				for (int i = 0; i < array3.Length; i++)
				{
					string text3 = array3[i];
					array2[num] = row[array[num]].ToString().Trim().Replace("\\", "\\\\")
						.Replace("'", "\\'");
					switch (array2[num].ToString())
					{
					case "True":
						array2[num] = "true";
						break;
					case "False":
						array2[num] = "false";
						break;
					}
					num++;
				}
				num = 0;
				stringBuilder.Append(string.Format(text, array2));
			}
			if (stringBuilder.ToString().EndsWith(","))
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			if (dtDispose)
			{
				dt.Dispose();
			}
			return stringBuilder.Append("\r\n];");
		}

		public static string escape(string str)
		{
			return GlobalObject.escape(str);
		}

		public static string MD5(string s)
		{
			return TextEncrypt.MD5EncryptPassword(s);
		}

		public static string GetAppSetting(string key)
		{
			return ApplicationSettings.Get(key);
		}

		public static string GetHostName()
		{
			return Dns.GetHostName();
		}

		public static string GetOSVersion()
		{
			string s = Environment.OSVersion.Version.ToString();
			return BitConverter.ToString(Encoding.GetEncoding("GB2312").GetBytes(s)).Replace("-", "");
		}

		public static string GetTextFromHTML(string HTML)
		{
			Regex regex = new Regex("</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase);
			return regex.Replace(HTML, "");
		}

		public static string Int2IP(long ipNumber)
		{
			IPAddress iPAddress = new IPAddress(ipNumber);
			return iPAddress.ToString();
		}

		public static long IP2Int(string ip)
		{
			if (!Validate.IsIP(ip))
			{
				return -1L;
			}
			string[] array = ip.Split('.');
			long num = long.Parse(array[3]) * 16777216;
			num += int.Parse(array[2]) * 65536;
			num += int.Parse(array[1]) * 256;
			return num + int.Parse(array[0]);
		}

		public static bool IsNumericArray(string[] strNumber)
		{
			return TypeParse.IsNumericArray(strNumber);
		}

		public static void Redirect(string url)
		{
			if (HttpContext.Current != null && !string.IsNullOrEmpty(url))
			{
				HttpContext.Current.Response.Redirect(url);
				HttpContext.Current.Response.StatusCode = 301;
				HttpContext.Current.Response.End();
			}
		}

		public static void ResponseFile(string filepath, string filename, string filetype)
		{
			if (HttpContext.Current != null)
			{
				Stream stream = null;
				byte[] buffer = new byte[10000];
				try
				{
					stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					long num = stream.Length;
					HttpContext.Current.Response.ContentType = filetype;
					HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + UrlEncode(filename.Trim()).Replace("+", " "));
					while (num > 0)
					{
						if (HttpContext.Current.Response.IsClientConnected)
						{
							int num2 = stream.Read(buffer, 0, 10000);
							HttpContext.Current.Response.OutputStream.Write(buffer, 0, num2);
							HttpContext.Current.Response.Flush();
							buffer = new byte[10000];
							num -= num2;
						}
						else
						{
							num = -1L;
						}
					}
				}
				catch (Exception ex)
				{
					HttpContext.Current.Response.Write("Error : " + ex.Message);
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
				HttpContext.Current.Response.End();
			}
		}

		public static string[] SearchUTF8File(string directory)
		{
			StringBuilder stringBuilder = new StringBuilder();
			FileInfo[] files = new DirectoryInfo(directory).GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				if (files[i].Extension.ToLower().Equals(".htm"))
				{
					FileStream fileStream = new FileStream(files[i].FullName, FileMode.Open, FileAccess.Read);
					bool flag = IsUTF8(fileStream);
					fileStream.Close();
					if (!flag)
					{
						stringBuilder.Append(files[i].FullName);
						stringBuilder.Append("\r\n");
					}
				}
			}
			return TextUtility.SplitStrArray(stringBuilder.ToString(), "\r\n");
		}

		private static bool IsUTF8(FileStream sbInputStream)
		{
			bool flag = true;
			long length = sbInputStream.Length;
			byte b = 0;
			for (int i = 0; i < length; i++)
			{
				byte b2 = (byte)sbInputStream.ReadByte();
				if ((b2 & 0x80) != 0)
				{
					flag = false;
				}
				if (b == 0)
				{
					if (b2 >= 128)
					{
						do
						{
							b2 = (byte)(b2 << 1);
							b = (byte)(b + 1);
						}
						while ((b2 & 0x80) != 0);
						b = (byte)(b - 1);
						if (b == 0)
						{
							return false;
						}
					}
				}
				else
				{
					if ((b2 & 0xC0) != 128)
					{
						return false;
					}
					b = (byte)(b - 1);
				}
			}
			if (b > 0)
			{
				return false;
			}
			if (flag)
			{
				return false;
			}
			return true;
		}

		public static bool StrToBool(object expression, bool defValue)
		{
			return TypeParse.StrToBool(expression, defValue);
		}

		public static bool StrToBool(string expression, bool defValue)
		{
			return TypeParse.StrToBool(expression, defValue);
		}

		public static float StrToFloat(object strValue, float defValue)
		{
			return TypeParse.StrToFloat(strValue, defValue);
		}

		public static float StrToFloat(string strValue, float defValue)
		{
			return TypeParse.StrToFloat(strValue, defValue);
		}

		public static int StrToInt(object expression, int defValue)
		{
			return TypeParse.StrToInt(expression, defValue);
		}

		public static int StrToInt(string expression, int defValue)
		{
			return TypeParse.StrToInt(expression, defValue);
		}

		public static Color ToColor(string color)
		{
			int num = 0;
			color = color.TrimStart('#');
			color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
			switch (color.Length)
			{
			case 3:
			{
				char[] array = color.ToCharArray();
				int red = System.Convert.ToInt32(array[0].ToString() + array[0].ToString(), 16);
				int green = System.Convert.ToInt32(array[1].ToString() + array[1].ToString(), 16);
				num = System.Convert.ToInt32(array[2].ToString() + array[2].ToString(), 16);
				return Color.FromArgb(red, green, num);
			}
			case 6:
			{
				char[] array = color.ToCharArray();
				int red = System.Convert.ToInt32(array[0].ToString() + array[1].ToString(), 16);
				int green = System.Convert.ToInt32(array[2].ToString() + array[3].ToString(), 16);
				num = System.Convert.ToInt32(array[4].ToString() + array[5].ToString(), 16);
				return Color.FromArgb(red, green, num);
			}
			default:
				return Color.FromName(color);
			}
		}

		public static void Trace(object obj)
		{
			string format = "<div style='border:1px solid #96C2F1;background-color: #F7F7FF;font-size:14px;font-family:宋体;text-align:right;margin: 0px auto;margin-bottom:5px;margin-right:5px;float:left; text-align:left; line-height:25px; width:800px;'><h5 style='margin: 1px;background-color:#E2EAF8;height: 24px;'>跟踪信息：</h5>{0}</div>";
			HttpContext.Current.Response.Write(string.Format(format, obj.ToString()));
		}

		public static void TraceWhite(object obj)
		{
			HttpContext.Current.Response.Write(obj.ToString());
		}

		public static string unescape(string str)
		{
			return GlobalObject.unescape(str);
		}

		public static string Url2HyperLink(string text)
		{
			string pattern = "(http|ftp|https):\\/\\/[\\w]+(.[\\w]+)([\\w\\-\\.,@?^=%&amp;:/~\\+#]*[\\w\\-\\@?^=%&amp;/~\\+#])";
			MatchCollection matchCollection = Regex.Matches(text, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			foreach (Match item in matchCollection)
			{
				text = text.Replace(item.ToString(), "<a target=\"_blank\" href=\"" + item.ToString() + "\">" + item.ToString() + "</a>");
			}
			return text;
		}

		public static string HtmlDecode(string str)
		{
			return HttpUtility.HtmlDecode(str);
		}

		public static string HtmlEncode(string str)
		{
			return HttpUtility.HtmlEncode(str);
		}

		public static string UrlDecode(string str)
		{
			return HttpUtility.UrlDecode(str);
		}

		public static string UrlEncode(string str)
		{
			return HttpUtility.UrlEncode(str);
		}

		public static void WriteCookie(string strName, string strValue)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(strName);
			}
			httpCookie.Value = strValue;
			HttpContext.Current.Response.AppendCookie(httpCookie);
		}

		public static void WriteCookie(string strName, string strValue, int expires)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(strName);
			}
			httpCookie.Value = strValue;
			httpCookie.Expires = DateTime.Now.AddMinutes((double)expires);
			HttpContext.Current.Response.AppendCookie(httpCookie);
		}

		public static void WriteCookie(string strName, string key, string strValue)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(strName);
			}
			httpCookie[key] = strValue;
			HttpContext.Current.Response.AppendCookie(httpCookie);
		}

		public static string GetCookie(string strName)
		{
			if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
			{
				return HttpContext.Current.Request.Cookies[strName].Value.ToString();
			}
			return "";
		}

		public static string GetCookie(string strName, string key)
		{
			if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
			{
				return HttpContext.Current.Request.Cookies[strName][key].ToString();
			}
			return "";
		}

		public static IList EnumToList(Type enumType)
		{
			ArrayList arrayList = new ArrayList();
			foreach (int value2 in Enum.GetValues(enumType))
			{
				ListItem value = new ListItem(Enum.GetName(enumType, value2), value2.ToString());
				arrayList.Add(value);
			}
			return arrayList;
		}
	}
}

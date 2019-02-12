using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Game.Utils
{
	public class TextUtility
	{
		private static readonly string PROLONG_SYMBOL = "...";

		private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

		private TextUtility()
		{
		}

		public static string CreateAuthStr(int len)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random();
			for (int i = 0; i < len; i++)
			{
				int num = random.Next();
				if (num % 2 == 0)
				{
					stringBuilder.Append((char)(48 + (ushort)(num % 10)));
				}
				else
				{
					stringBuilder.Append((char)(65 + (ushort)(num % 26)));
				}
			}
			return stringBuilder.ToString();
		}

		public static string CreateAuthStr(int len, bool onlyNum)
		{
			if (!onlyNum)
			{
				return CreateAuthStr(len);
			}
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random();
			for (int i = 0; i < len; i++)
			{
				int num = random.Next();
				stringBuilder.Append((char)(48 + (ushort)(num % 10)));
			}
			return stringBuilder.ToString();
		}

		public static string CreateRandom(int length, int useNum, int useLow, int useUpp, int useSpe, string custom)
		{
			byte[] array = new byte[4];
			new RNGCryptoServiceProvider().GetBytes(array);
			Random random = new Random(BitConverter.ToInt32(array, 0));
			string text = null;
			string text2 = custom;
			if (useNum == 1)
			{
				text2 += "0123456789";
			}
			if (useLow == 1)
			{
				text2 += "abcdefghijklmnopqrstuvwxyz";
			}
			if (useUpp == 1)
			{
				text2 += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			}
			if (useSpe == 1)
			{
				text2 += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
			}
			for (int i = 0; i < length; i++)
			{
				text += text2.Substring(random.Next(0, text2.Length - 1), 1);
			}
			return text;
		}

		public static string CreateRandomLowercase(int len)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random();
			for (int i = 0; i < len; i++)
			{
				int num = random.Next();
				stringBuilder.Append((char)(97 + (ushort)(num % 26)));
			}
			return stringBuilder.ToString();
		}

		public static string CreateRandomNum(int len)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random((int)DateTime.Now.Ticks);
			for (int i = 0; i < len; i++)
			{
				int num = random.Next();
				stringBuilder.Append((char)(48 + (ushort)(num % 10)));
			}
			return stringBuilder.ToString();
		}

		public static string CreateRandomNum2(int len)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random(GetNewSeed());
			for (int i = 0; i < len; i++)
			{
				int num = random.Next();
				stringBuilder.Append((char)(48 + (ushort)(num % 10)));
			}
			return stringBuilder.ToString();
		}

		public static int GetNewSeed()
		{
			byte[] array = new byte[4];
			rng.GetBytes(array);
			return BitConverter.ToInt32(array, 0);
		}

		public static string CreateTemporaryPassword(int length)
		{
			string text = Guid.NewGuid().ToString("N");
			for (int i = 0; i < length / 32; i++)
			{
				text += Guid.NewGuid().ToString("N");
			}
			return text.Substring(0, length);
		}

		public static string CutLeft(string originalVal, int cutLength)
		{
			if (string.IsNullOrEmpty(originalVal))
			{
				return string.Empty;
			}
			if (cutLength < 1)
			{
				return originalVal;
			}
			byte[] bytes = Encoding.Default.GetBytes(originalVal);
			if (bytes.Length <= cutLength)
			{
				return originalVal;
			}
			int num = cutLength;
			int[] array = new int[cutLength];
			byte[] array2 = null;
			int num2 = 0;
			for (int i = 0; i < cutLength; i++)
			{
				if (bytes[i] > 127)
				{
					num2++;
					if (num2 == 3)
					{
						num2 = 1;
					}
				}
				else
				{
					num2 = 0;
				}
				array[i] = num2;
			}
			if (bytes[cutLength - 1] > 127 && array[cutLength - 1] == 1)
			{
				num = cutLength + 1;
			}
			array2 = new byte[num];
			Array.Copy(bytes, array2, num);
			return Encoding.Default.GetString(array2);
		}

		public static string CutRight(string originalVal, int cutLength)
		{
			if (cutLength < 0)
			{
				cutLength = Math.Abs(cutLength);
			}
			if (originalVal.Length <= cutLength)
			{
				return originalVal;
			}
			return originalVal.Substring(originalVal.Length - cutLength);
		}

		public static string CutString(string originalVal, int startIndex)
		{
			return CutString(originalVal, startIndex, originalVal.Length);
		}

		public static string CutString(string originalVal, int startIndex, int cutLength)
		{
			if (startIndex >= 0)
			{
				if (cutLength < 0)
				{
					cutLength *= -1;
					if (startIndex - cutLength < 0)
					{
						cutLength = startIndex;
						startIndex = 0;
					}
					else
					{
						startIndex -= cutLength;
					}
				}
				if (startIndex > originalVal.Length)
				{
					return "";
				}
			}
			else
			{
				if (cutLength < 0 || cutLength + startIndex <= 0)
				{
					return "";
				}
				cutLength += startIndex;
				startIndex = 0;
			}
			if (originalVal.Length - startIndex < cutLength)
			{
				cutLength = originalVal.Length - startIndex;
			}
			try
			{
				return originalVal.Substring(startIndex, cutLength);
			}
			catch
			{
				return originalVal;
			}
		}

		public static string CutStringProlongSymbol(string originalVal, int cutLength)
		{
			if (originalVal.Length <= cutLength)
			{
				return originalVal;
			}
			return CutLeft(originalVal, cutLength) + PROLONG_SYMBOL;
		}

		public static string CutStringProlongSymbol(string originalVal, int cutLength, string prolongSymbol)
		{
			if (string.IsNullOrEmpty(prolongSymbol))
			{
				prolongSymbol = PROLONG_SYMBOL;
			}
			return CutLeft(originalVal, cutLength) + prolongSymbol;
		}

		public static string CutStringTitle(object content, int cutLength)
		{
			string text = Regex.Replace(content.ToString(), "<[^>]+>", "");
			if (text.Length > cutLength && text.Length > 2)
			{
				text = text.Substring(0, cutLength - 2) + "...";
			}
			if (text.IndexOf("<") > -1)
			{
				text = text.Remove(text.LastIndexOf("<"), text.Length - text.LastIndexOf("<"));
			}
			return text;
		}

		public static bool EmptyTrimOrNull(string text)
		{
			if (text != null)
			{
				return text.Trim().Length == 0;
			}
			return true;
		}

		public static string FormatIP(string ip, int fields)
		{
			if (string.IsNullOrEmpty(ip))
			{
				return "(未记录)";
			}
			if (fields > 3)
			{
				return ip;
			}
			if (ip.Contains(":"))
			{
				return "(不支持ipv6)";
			}
			string[] array = ip.Split('.');
			if (array.Length == 4)
			{
				switch (fields)
				{
				case 3:
					return array[0] + "." + array[1] + "." + array[2] + ".*";
				case 2:
					return array[0] + "." + array[1] + ".*.*";
				case 1:
					return array[0] + ".*.*.*";
				default:
					return "*.*.*.*";
				}
			}
			return "(未记录)";
		}

		public static string EmailEncode(string originalStr)
		{
			string text = TextEncode(originalStr).Replace("@", "&#64;").Replace(".", "&#46;");
			return JoinString("<a href='mailto:", text, "'>", text, "</a>");
		}

		public static string GetEmailHostName(string strEmail)
		{
			if (string.IsNullOrEmpty(strEmail) || strEmail.IndexOf("@") < 0)
			{
				return string.Empty;
			}
			return strEmail.Substring(strEmail.LastIndexOf("@") + 1).ToLower();
		}

		public static string FormatMoney(decimal money)
		{
			return money.ToString("0.00");
		}

		public static string[] DiffDateAndTime(object todate, object fodate)
		{
			string[] array = new string[2];
			double value = (DateTime.Parse(todate.ToString()) - DateTime.Parse(fodate.ToString())).TotalSeconds / 86400.0;
			value.ToString();
			int length = value.ToString().Length;
			int num = value.ToString().LastIndexOf(".");
			int num2 = (int)Math.Round(value, 10);
			int num3 = (int)(double.Parse("0" + value.ToString().Substring(num, length - num)) * 24.0);
			array[0] = num2.ToString();
			array[1] = num3.ToString();
			return array;
		}

		public static string DiffDateAndTime(object todate, object fodate, string v1, string v2, string v3, string v4, string v5, string v6)
		{
			TimeSpan timeSpan = DateTime.Parse(todate.ToString()) - DateTime.Parse(fodate.ToString());
			int num = (int)timeSpan.TotalDays / 365;
			int num2 = (int)((timeSpan.TotalDays / 365.0 - (double)(int)(timeSpan.TotalDays / 365.0)) * 12.0);
			int num3 = timeSpan.Days - num * 365 - num2 * 30;
			int hours = timeSpan.Hours;
			int minutes = timeSpan.Minutes;
			string text = "";
			if (num != 0)
			{
				text = text + num.ToString() + v1;
			}
			if (num2 != 0)
			{
				text = text + num2.ToString() + v2;
			}
			if (num3 != 0)
			{
				text = text + num3.ToString() + v3;
			}
			if (hours != 0)
			{
				text = text + hours.ToString() + v4;
			}
			if (minutes != 0)
			{
				text = text + minutes.ToString() + v5;
			}
			if (num == 0 && num2 == 0 && num3 == 0 && hours == 0 && minutes == 0)
			{
				return v6;
			}
			return text;
		}

		public static int DiffDateDays(DateTime oneDateTime)
		{
			TimeSpan timeSpan = DateTime.Now - oneDateTime;
			if (timeSpan.TotalDays > 2147483647.0)
			{
				return 2147483647;
			}
			if (timeSpan.TotalSeconds < -2147483648.0)
			{
				return -2147483648;
			}
			return (int)timeSpan.TotalDays;
		}

		public static int DiffDateDays(string oneDateTime)
		{
			if (string.IsNullOrEmpty(oneDateTime))
			{
				return 0;
			}
			return DiffDateDays(DateTime.Parse(oneDateTime));
		}

		public static string FormatDateSpan(object dateSpan)
		{
			DateTime d = (DateTime)dateSpan;
			TimeSpan timeSpan = DateTime.Now - d;
			if (timeSpan.TotalDays >= 365.0)
			{
				return string.Format("{0} 年前", (int)(timeSpan.TotalDays / 365.0));
			}
			if (timeSpan.TotalDays >= 30.0)
			{
				return string.Format("{0} 月前", (int)(timeSpan.TotalDays / 30.0));
			}
			if (timeSpan.TotalDays >= 7.0)
			{
				return string.Format("{0} 周前", (int)(timeSpan.TotalDays / 7.0));
			}
			if (timeSpan.TotalDays >= 1.0)
			{
				return string.Format("{0} 天前", (int)timeSpan.TotalDays);
			}
			if (timeSpan.TotalHours >= 1.0)
			{
				return string.Format("{0} 小时前", (int)timeSpan.TotalHours);
			}
			if (timeSpan.TotalMinutes >= 1.0)
			{
				return string.Format("{0} 分钟前", (int)timeSpan.TotalMinutes);
			}
			return "1 分钟前";
		}

		public static string FormatDateTime(DateTime oneDateVal, int formatType)
		{
			double value = 0.0;
			DateTime dateTime = oneDateVal.AddHours(value);
			switch (formatType)
			{
			case 2:
				return dateTime.ToShortDateString();
			case 3:
				return dateTime.ToString("yyyy年MM月dd日 HH点mm分ss秒");
			case 4:
				return dateTime.ToString("yyyy年MM月dd日");
			case 5:
				return dateTime.ToString("yyyy年MM月dd日 HH点mm分");
			case 6:
				return dateTime.ToString("yyyy-MM-dd HH:mm");
			case 7:
				return dateTime.ToString("yy年MM月dd日 HH点mm分");
			default:
				return dateTime.ToString();
			}
		}

		public static string FormatDateTime(string oneDateVal, int formatType)
		{
			return FormatDateTime(DateTime.Parse(oneDateVal), formatType);
		}

		public static string FormatSecondSpan(long second)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)second);
			string text = (timeSpan.Days <= 0) ? string.Empty : (timeSpan.Days.ToString() + "天");
			if (timeSpan.Hours > 0)
			{
				text = text + timeSpan.Hours.ToString() + "时";
			}
			if (timeSpan.Minutes > 0)
			{
				text = text + timeSpan.Minutes.ToString() + "分";
			}
			if (timeSpan.Seconds > 0)
			{
				text = text + timeSpan.Seconds.ToString() + "秒";
			}
			return text;
		}

		public static string GetDateTimeLongString()
		{
			DateTime now = DateTime.Now;
			return now.ToString("yyyyMMddHHmmss") + now.Millisecond.ToString("000");
		}

		public static string GetDateTimeLongString(string prefix)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				prefix = string.Empty;
			}
			return prefix + GetDateTimeLongString();
		}

		public static string AddLast(string originalVal, string lastStr)
		{
			if (originalVal.EndsWith(lastStr))
			{
				return originalVal;
			}
			return originalVal + lastStr;
		}

		public static string GetFullPath(string strPath)
		{
			string text = AddLast(AppDomain.CurrentDomain.BaseDirectory, "\\");
			if (strPath.IndexOf(":") < 0)
			{
				string text2 = strPath.Replace("..\\", "");
				if (text2 != strPath)
				{
					int num = (strPath.Length - text2.Length) / "..\\".Length + 1;
					for (int i = 0; i < num; i++)
					{
						text = text.Substring(0, text.LastIndexOf("\\"));
					}
					text2 = "\\" + text2;
				}
				strPath = text + text2;
			}
			return strPath;
		}

		public static string GetMapPath(string folderPath)
		{
			if (folderPath.IndexOf(":\\") > 0)
			{
				return AddLast(folderPath, "\\");
			}
			if (folderPath.StartsWith("~/"))
			{
				return AddLast(HttpContext.Current.Server.MapPath(folderPath), "\\");
			}
			string str = HttpContext.Current.Request.ApplicationPath + "/";
			return AddLast(HttpContext.Current.Server.MapPath(str + folderPath), "\\");
		}

		public static string GetRealPath(string strPath)
		{
			if (string.IsNullOrEmpty(strPath))
			{
				throw new Exception("strPath 不能为空！");
			}
			HttpContext httpContext = null;
			try
			{
				httpContext = HttpContext.Current;
			}
			catch
			{
				httpContext = null;
			}
			if (httpContext != null)
			{
				return httpContext.Server.MapPath(strPath);
			}
			string text = Path.Combine(strPath, "");
			text = (text.StartsWith("\\\\") ? text.Remove(0, 2) : text);
			return AppDomain.CurrentDomain.BaseDirectory + Path.Combine(strPath, "");
		}

		public static bool InArray(string matchStr, string[] strArray)
		{
			if (!string.IsNullOrEmpty(matchStr))
			{
				for (int i = 0; i < strArray.Length; i++)
				{
					if (matchStr == strArray[i])
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool InArray(string matchStr, string originalStr, string separator)
		{
			string[] array = SplitStrArray(originalStr, separator);
			for (int i = 0; i < array.Length; i++)
			{
				if (matchStr == array[i])
				{
					return true;
				}
			}
			return false;
		}

		public static bool InArray(string matchStr, string[] strArray, bool ignoreCase)
		{
			return InArrayIndexOf(matchStr, strArray, ignoreCase) >= 0;
		}

		public static bool InArray(string matchStr, string strArray, string separator, bool ignoreCase)
		{
			return InArray(matchStr, SplitStrArray(strArray, separator), ignoreCase);
		}

		public static int InArrayIndexOf(string originalStr, string[] strArray)
		{
			return InArrayIndexOf(originalStr, strArray, true);
		}

		public static int InArrayIndexOf(string originalStr, string[] strArray, bool ignoreCase)
		{
			for (int i = 0; i < strArray.Length; i++)
			{
				if (ignoreCase)
				{
					if (originalStr.ToLower() == strArray[i].ToLower())
					{
						return i;
					}
				}
				else if (originalStr == strArray[i])
				{
					return i;
				}
			}
			return -1;
		}

		public static bool InIPArray(string ip, string[] ipArray)
		{
			if (!string.IsNullOrEmpty(ip) && Validate.IsIP(ip))
			{
				string[] array = SplitStrArray(ip, ".");
				for (int i = 0; i < ipArray.Length; i++)
				{
					string[] array2 = SplitStrArray(ipArray[i], ".");
					int num = 0;
					for (int j = 0; j < array2.Length; j++)
					{
						if (array2[j] == "*")
						{
							return true;
						}
						if (array.Length <= j || array2[j] != array[j])
						{
							break;
						}
						num++;
					}
					if (num == 4)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static string JavaScriptEncode(object obj)
		{
			if (obj == null)
			{
				return string.Empty;
			}
			return JavaScriptEncode(obj.ToString());
		}

		public static string JavaScriptEncode(string originalStr)
		{
			if (string.IsNullOrEmpty(originalStr))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(originalStr);
			return stringBuilder.Replace("\\", "\\\\").Replace("/", "\\/").Replace("'", "\\'")
				.Replace("\"", "\\\"")
				.Replace("\r\n", "\r")
				.Replace("\r", "\\r")
				.ToString();
		}

		public static string Join(string separator, params string[] value)
		{
			return JoinString(separator, value);
		}

		public static string JoinString(params string[] value)
		{
			return JoinString(string.Empty, value);
		}

		private static string JoinString(string separator, params string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				return string.Empty;
			}
			return string.Join(separator, value);
		}

		public static int Length(string originalVal)
		{
			return Encoding.Default.GetBytes(originalVal).Length;
		}

		public static string RegexReplaceTags(string originalStr, string specialChares, params object[] entityClasses)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			foreach (object obj in entityClasses)
			{
				PropertyInfo[] properties = obj.GetType().GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					text = propertyInfo.Name;
					text2 = specialChares + text + specialChares;
					text3 = propertyInfo.GetValue(obj, null).ToString();
					originalStr = Regex.Replace(originalStr, text2, text3, RegexOptions.IgnoreCase);
				}
			}
			return originalStr;
		}

		public static string RepeatStr(string repeatStr, int repeatCount)
		{
			StringBuilder stringBuilder = new StringBuilder(repeatCount);
			for (int i = 0; i < repeatCount; i++)
			{
				stringBuilder.Append(repeatStr);
			}
			return stringBuilder.ToString();
		}

		public static string ReplaceCnChar(string originalVal)
		{
			if (string.IsNullOrEmpty(originalVal))
			{
				return string.Empty;
			}
			return Regex.Replace(originalVal, "[^\\u4E00-\\u9FA5]", "");
		}

		public static string ReplaceLuceneSpecialChar(string originalVal)
		{
			if (string.IsNullOrEmpty(originalVal))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(originalVal);
			stringBuilder.Replace("+", string.Empty);
			stringBuilder.Replace("-", string.Empty);
			stringBuilder.Replace("&&", string.Empty);
			stringBuilder.Replace("||", string.Empty);
			stringBuilder.Replace("!", string.Empty);
			stringBuilder.Replace("(", string.Empty);
			stringBuilder.Replace(")", string.Empty);
			stringBuilder.Replace("{", string.Empty);
			stringBuilder.Replace("}", string.Empty);
			stringBuilder.Replace("[", string.Empty);
			stringBuilder.Replace("]", string.Empty);
			stringBuilder.Replace("^", string.Empty);
			stringBuilder.Replace("\"", string.Empty);
			stringBuilder.Replace("~", string.Empty);
			stringBuilder.Replace("*", string.Empty);
			stringBuilder.Replace("?", string.Empty);
			stringBuilder.Replace(":", string.Empty);
			stringBuilder.Replace("\\", string.Empty);
			return stringBuilder.ToString();
		}

		public static string ReplaceStrUseSC(string originalStr, StringCollection sc)
		{
			if (!string.IsNullOrEmpty(originalStr))
			{
				StringEnumerator enumerator = sc.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						originalStr = Regex.Replace(originalStr, current, "*".PadLeft(current.Length, '*'), RegexOptions.IgnoreCase);
					}
					return originalStr;
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			return string.Empty;
		}

		public static string ReplaceStrUseSC(string originalStr, string[] sc)
		{
			if (string.IsNullOrEmpty(originalStr))
			{
				return string.Empty;
			}
			foreach (string text in sc)
			{
				originalStr = Regex.Replace(originalStr, text, "*".PadLeft(text.Length, '*'), RegexOptions.IgnoreCase);
			}
			return originalStr;
		}

		public static string ReplaceStrUseStr(string originalStr, string replacedStr, string replaceStr)
		{
			if (string.IsNullOrEmpty(originalStr))
			{
				return string.Empty;
			}
			return Regex.Replace(originalStr, replacedStr, replaceStr, RegexOptions.IgnoreCase);
		}

		public static string[] SplitStrArray(string originalStr, string separator)
		{
			if (originalStr.IndexOf(separator) < 0)
			{
				return new string[1]
				{
					originalStr
				};
			}
			return Regex.Split(originalStr, separator.Replace(".", "\\."), RegexOptions.IgnoreCase);
		}

		public static string SplitStrUseLines(string originalContent, int splitLines)
		{
			if (string.IsNullOrEmpty(originalContent))
			{
				return string.Empty;
			}
			string empty = string.Empty;
			int num = 1;
			int num2 = 0;
			int num3 = originalContent.Length - 5;
			for (num = 1; num < num3; num++)
			{
				if (originalContent.Substring(num, 6).ToLower().Equals("<br />"))
				{
					num2++;
				}
				if (originalContent.Substring(num, 5).ToLower().Equals("<br/>"))
				{
					num2++;
				}
				if (originalContent.Substring(num, 4).ToLower().Equals("<br>"))
				{
					num2++;
				}
				if (originalContent.Substring(num, 3).ToLower().Equals("<p>"))
				{
					num2++;
				}
				if (num2 >= splitLines)
				{
					break;
				}
			}
			if (num2 >= splitLines)
			{
				if (num != num3)
				{
					return originalContent.Substring(0, num);
				}
				return originalContent.Substring(0, num - 1);
			}
			return originalContent;
		}

		public static string SplitStrUseStr(string originalStr, string separator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(separator);
			for (int i = 0; i < originalStr.Length; i++)
			{
				stringBuilder.Append(originalStr[i]);
				stringBuilder.Append(separator);
			}
			return stringBuilder.ToString();
		}

		public static string SqlEncode(string strSQL)
		{
			if (string.IsNullOrEmpty(strSQL))
			{
				return string.Empty;
			}
			return strSQL.Trim().Replace("'", "''");
		}

		public static string TextDecode(string originalStr)
		{
			StringBuilder stringBuilder = new StringBuilder(originalStr);
			stringBuilder.Replace("<br/><br/>", "\r\n");
			stringBuilder.Replace("<br/>", "\r");
			stringBuilder.Replace("<p></p>", "\r\n\r\n");
			return stringBuilder.ToString();
		}

		public static string TextEncode(string originalStr)
		{
			if (string.IsNullOrEmpty(originalStr))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(originalStr);
			stringBuilder.Replace("\r\n", "<br />");
			stringBuilder.Replace("\n", "<br />");
			return stringBuilder.ToString();
		}

		public static string TransformFirstToLower(string originalVal)
		{
			if (string.IsNullOrEmpty(originalVal))
			{
				return originalVal;
			}
			if (originalVal.Length >= 2)
			{
				return originalVal.Substring(0, 1).ToLower() + originalVal.Substring(1);
			}
			return originalVal.ToUpper();
		}

		public static string TransformFirstToUpper(string originalVal)
		{
			if (string.IsNullOrEmpty(originalVal))
			{
				return originalVal;
			}
			if (originalVal.Length >= 2)
			{
				return originalVal.Substring(0, 1).ToUpper() + originalVal.Substring(1);
			}
			return originalVal.ToUpper();
		}

		public static int GetDaysDate(DateTime date)
		{
			DateTime d = Convert.ToDateTime("1900-01-01");
			return (date - d).Days;
		}

		public static DateTime GetDateTimeByDays(int days)
		{
			return Convert.ToDateTime("1900-01-01").AddDays((double)days);
		}

		public static string CutUrlReturnPath(string url)
		{
			Regex regex = new Regex("^(http:\\/\\/||https:\\/\\/)[A-Za-z0-9_:.]*\\/");
			return regex.Replace(url, "/");
		}

		public static string SetQueryValueReturnUrl(string queryName, string newValues)
		{
			NameValueCollection queryString = HttpContext.Current.Request.QueryString;
			string[] allKeys = queryString.AllKeys;
			string leftPart = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
			leftPart += "?";
			if (allKeys.Length > 0)
			{
				for (int i = 0; i < allKeys.Length; i++)
				{
					string str = queryString.GetValues(i)[0].ToString();
					if (allKeys[i] == queryName)
					{
						str = newValues;
					}
					if (i != 0)
					{
						leftPart += "&";
					}
					leftPart = leftPart + allKeys[i] + "=" + str;
				}
			}
			if (Array.IndexOf(allKeys, queryName) == -1)
			{
				if (allKeys.Length > 0)
				{
					string text = leftPart;
					leftPart = text + "&" + queryName + "=" + newValues;
				}
				else
				{
					leftPart = leftPart + queryName + "=" + newValues;
				}
			}
			return leftPart;
		}
	}
}

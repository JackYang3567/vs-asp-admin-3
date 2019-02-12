using Game.Utils.Cache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Web;

namespace Game.Facade
{
	public class Fetch
	{
		public static string GetVerifyCodeVer2
		{
			get
			{
				object obj = WHCache.Default.Get<SessionCache>("VerifyCodeKey");
				if (obj != null)
				{
					return obj.ToString();
				}
				return "";
			}
		}

		public static string GetCookieName
		{
			get
			{
				return "6603Admin";
			}
		}

		public static void Redirect(string url)
		{
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.StatusCode = 301;
			HttpContext.Current.Response.AppendHeader("location", url);
			HttpContext.Current.Response.End();
		}

		public static bool ValidVerifyCodeVer2(string verifyCode)
		{
			object obj = WHCache.Default.Get<SessionCache>("VerifyCodeKey");
			if (obj != null && obj.ToString() == verifyCode)
			{
				return true;
			}
			return false;
		}

		public static bool ValidVerifyCodeVer3(string verifyCode)
		{
			HttpContext current = HttpContext.Current;
			if (current == null || current.Session.Count == 0)
			{
				return false;
			}
			if (verifyCode.Equals(current.Session["CheckCode"].ToString(), StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}
			return false;
		}

		public static string GetStartTime(DateTime bgDate)
		{
			DateTime value = new DateTime(bgDate.Year, bgDate.Month, bgDate.Day, 0, 0, 0);
			return Convert.ToString(value);
		}

		public static string GetEndTime(DateTime enDate)
		{
			DateTime value = new DateTime(enDate.Year, enDate.Month, enDate.Day, 23, 59, 59);
			return Convert.ToString(value);
		}

		public static string GetTimeByDate(DateTime bgDate, DateTime enDate)
		{
			DateTime value = new DateTime(bgDate.Year, bgDate.Month, bgDate.Day, 0, 0, 0);
			DateTime value2 = new DateTime(enDate.Year, enDate.Month, enDate.Day, 23, 59, 59);
			return Convert.ToString(value) + "$" + Convert.ToString(value2);
		}

		public static string GetTodayTime()
		{
			DateTime now = DateTime.Now;
			return GetTimeByDate(now, now);
		}

		public static string GetYesterdayTime()
		{
			DateTime dateTime = DateTime.Now.AddDays(-1.0);
			return GetTimeByDate(dateTime, dateTime);
		}

		public static string GetWeekTime()
		{
			DateTime now = DateTime.Now;
			DateTime bgDate = now.AddDays((double)(-Convert.ToInt32(now.DayOfWeek.ToString("d"))));
			DateTime enDate = bgDate.AddDays(6.0);
			return GetTimeByDate(bgDate, enDate);
		}

		public static string GetLastWeekTime()
		{
			DateTime now = DateTime.Now;
			DateTime bgDate = now.AddDays((double)(-7 - Convert.ToInt32(now.DayOfWeek.ToString("d"))));
			DateTime enDate = bgDate.AddDays(6.0);
			return GetTimeByDate(bgDate, enDate);
		}

		public static string GetLastMonthTime()
		{
			DateTime now = DateTime.Now;
			DateTime bgDate = Convert.ToDateTime(now.AddMonths(-1).ToString("yyyy-MM-01"));
			DateTime enDate = Convert.ToDateTime(now.ToString("yyyy-MM-01"));
			return GetTimeByDate(bgDate, enDate);
		}

		public static string GetMonthTime()
		{
			DateTime now = DateTime.Now;
			DateTime bgDate = now.AddDays((double)(1 - now.Day));
			DateTime enDate = bgDate.AddMonths(1).AddDays(-1.0);
			return GetTimeByDate(bgDate, enDate);
		}

		public static string GetYearTime()
		{
			DateTime now = DateTime.Now;
			DateTime bgDate = now.AddDays((double)(1 - now.DayOfYear));
			DateTime enDate = bgDate.AddYears(1).AddDays(-1.0);
			return GetTimeByDate(bgDate, enDate);
		}

		public static string GetDateID(DateTime DateTime)
		{
			TimeSpan timeSpan = new TimeSpan(DateTime.Ticks);
			TimeSpan ts = new TimeSpan(Convert.ToDateTime("1900-01-01").Ticks);
			return timeSpan.Subtract(ts).Duration().Days.ToString();
		}

		public static string ShowDate(int dateID)
		{
			return Convert.ToDateTime("1900-01-01").AddDays((double)dateID).ToString("yyyy-MM-dd");
		}

		public static string GetTimeSpan(DateTime dtStartDate, DateTime dtEndDate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			TimeSpan timeSpan = dtEndDate.Subtract(dtStartDate);
			if (timeSpan.Days != 0)
			{
				stringBuilder.AppendFormat("{0}天", timeSpan.Days);
			}
			if (timeSpan.Hours != 0)
			{
				stringBuilder.AppendFormat("{0}小时", timeSpan.Hours);
			}
			if (timeSpan.Minutes != 0)
			{
				stringBuilder.AppendFormat("{0}分钟", timeSpan.Minutes);
			}
			if (timeSpan.Seconds != 0)
			{
				stringBuilder.AppendFormat("{0}秒", timeSpan.Seconds);
			}
			if (string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				return "0秒";
			}
			return stringBuilder.ToString();
		}

		public static string ConverTimeToDHMS(long seconds)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (seconds <= 0)
			{
				return "0秒";
			}
			long num = seconds / 86400;
			long num2 = seconds % 86400 / 3600;
			long num3 = seconds % 3600 / 60;
			long num4 = seconds % 60;
			if (num > 0)
			{
				stringBuilder.AppendFormat("{0}天", num);
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat("{0}小时", num2);
			}
			if (num3 > 0)
			{
				stringBuilder.AppendFormat("{0}分钟", num3);
			}
			if (num4 > 0)
			{
				stringBuilder.AppendFormat("{0}秒", num4);
			}
			if (string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				return "0秒";
			}
			return stringBuilder.ToString();
		}

		public static string GetRandomNumeric(int length)
		{
			if (length <= 0)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int num = length; num > 0; num--)
			{
				stringBuilder.Append(GetRandomSingleDigit().ToString());
			}
			return stringBuilder.ToString();
		}

		public static string GetRandomNumeric(int length, Random rand)
		{
			if (length <= 0)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int num = length; num > 0; num--)
			{
				stringBuilder.Append(rand.Next(0, 10).ToString());
			}
			return stringBuilder.ToString();
		}

		public static string GetRandomNumericAndEn(int length, Random random)
		{
			if (length <= 0)
			{
				return "";
			}
			string text = "nMe7lcIPKpQ1oAtuGCzL2qf8NO9X4mdFSaHbsOj3DvJrwV6ghiUYZWx5kETRyB";
			int num = length;
			StringBuilder stringBuilder = new StringBuilder();
			List<char> list = new List<char>();
			while (num > 0)
			{
				char item = text[random.Next(text.Length)];
				if (!list.Contains(item))
				{
					list.Add(item);
					num--;
				}
			}
			foreach (char item2 in list)
			{
				stringBuilder.Append(item2.ToString());
			}
			return stringBuilder.ToString();
		}

		public static int GetRandomSingleDigit()
		{
			Random random = new Random();
			Thread.Sleep(10);
			return random.Next(10);
		}

		public static string GetRandomStr(int length, bool notRepeat, Random random)
		{
			if (length <= 0)
			{
				return "";
			}
			string text = "cohxMLdeabzEFGNmQZPAstpvwTHOkKnlWqrSYyijXufgRIJUVBCD";
			int num = length;
			StringBuilder stringBuilder = new StringBuilder();
			List<char> list = new List<char>();
			while (num > 0)
			{
				char item = text[random.Next(text.Length)];
				if (notRepeat)
				{
					if (length >= 26)
					{
						throw new Exception("指定了不允许字母重复，并且要生成的字符串长度大于等于26，将造成系统陷入死循环。");
					}
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				else
				{
					list.Add(item);
				}
				num--;
			}
			foreach (char item2 in list)
			{
				stringBuilder.Append(item2.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}

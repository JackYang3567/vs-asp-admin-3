using System;

namespace RestSharp.Authenticators.OAuth.Extensions
{
	internal static class TimeExtensions
	{
		public static DateTime FromNow(this TimeSpan value)
		{
			return new DateTime((DateTime.Now + value).Ticks);
		}

		public static DateTime FromUnixTime(this long seconds)
		{
			DateTime dateTime = new DateTime(1970, 1, 1);
			dateTime = dateTime.AddSeconds((double)seconds);
			return dateTime.ToLocalTime();
		}

		public static long ToUnixTime(this DateTime dateTime)
		{
			return (long)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
		}
	}
}

using System;

namespace RestSharp.Validation
{
	public class Validate
	{
		public static void IsBetween(int value, int min, int max)
		{
			if (value < min || value > max)
			{
				throw new ArgumentException(string.Format("Value ({0}) is not between {1} and {2}.", value, min, max));
			}
		}

		public static void IsValidLength(string value, int maxSize)
		{
			if (value != null && value.Length > maxSize)
			{
				throw new ArgumentException(string.Format("String is longer than max allowed size ({0}).", maxSize));
			}
		}
	}
}

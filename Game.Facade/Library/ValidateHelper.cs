using Game.Facade.Tools;
using System;
using System.Text.RegularExpressions;

namespace Library
{
	public class ValidateHelper
	{
		private static Regex _emailregex = new Regex("^[a-z0-9]([a-z0-9]*[-_]?[a-z0-9]+)*@([a-z0-9]*[-_]?[a-z0-9]+)+[\\.][a-z]{2,3}([\\.][a-z]{2})?$", RegexOptions.IgnoreCase);

		private static Regex _mobileregex = new Regex("^((1[3,5,8][0-9])|(14[5,7])|(17[0,1,6,7,8]))[0-9]{8}$");

		private static Regex _phoneregex = new Regex("^(\\d{3,4}-?)?\\d{7,8}$");

		private static Regex _ipregex = new Regex("^(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])$");

		private static Regex _dateregex = new Regex("(\\d{4})-(\\d{1,2})-(\\d{1,2})");

		private static Regex _numericregex = new Regex("^[-]?[0-9]+(\\.[0-9]+)?$");

		private static Regex _zipcoderegex = new Regex("^\\d{6}$");

		private static Regex _cnnameegex = new Regex("^[\\u4E00-\\u9FA5]{2,5}(?:·[\\u4E00-\\u9FA5]{2,5})*$");

		public static bool IsEmail(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return true;
			}
			return _emailregex.IsMatch(s);
		}

		public static bool IsMobile(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return true;
			}
			return _mobileregex.IsMatch(s);
		}

		public static bool IsPhone(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return true;
			}
			return _phoneregex.IsMatch(s);
		}

		public static bool IsIP(string s)
		{
			return _ipregex.IsMatch(s);
		}

		public static bool IsIdCard(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return true;
			}
			if (id.Length == 18)
			{
				return CheckIDCard18(id);
			}
			if (id.Length == 15)
			{
				return CheckIDCard15(id);
			}
			return false;
		}

		private static bool CheckIDCard18(string Id)
		{
			long result = 0L;
			if (!long.TryParse(Id.Remove(17), out result) || (double)result < Math.Pow(10.0, 16.0) || !long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out result))
			{
				return false;
			}
			string text = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
			if (text.IndexOf(Id.Remove(2)) == -1)
			{
				return false;
			}
			string s = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
			DateTime result2 = default(DateTime);
			if (!DateTime.TryParse(s, out result2))
			{
				return false;
			}
			string[] array = "1,0,x,9,8,7,6,5,4,3,2".Split(',');
			string[] array2 = "7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2".Split(',');
			char[] array3 = Id.Remove(17).ToCharArray();
			int num = 0;
			for (int i = 0; i < 17; i++)
			{
				num += int.Parse(array2[i]) * int.Parse(array3[i].ToString());
			}
			int result3 = -1;
			Math.DivRem(num, 11, out result3);
			if (array[result3] != Id.Substring(17, 1).ToLower())
			{
				return false;
			}
			return true;
		}

		private static bool CheckIDCard15(string Id)
		{
			long result = 0L;
			if (!long.TryParse(Id, out result) || (double)result < Math.Pow(10.0, 14.0))
			{
				return false;
			}
			string text = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
			if (text.IndexOf(Id.Remove(2)) == -1)
			{
				return false;
			}
			string s = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
			DateTime result2 = default(DateTime);
			if (!DateTime.TryParse(s, out result2))
			{
				return false;
			}
			return true;
		}

		public static bool IsDate(string s)
		{
			return _dateregex.IsMatch(s);
		}

		public static bool IsNumeric(string numericStr)
		{
			return _numericregex.IsMatch(numericStr);
		}

		public static bool IsInt(string value)
		{
			return Regex.IsMatch(value, "^[+-]?\\d*$");
		}

		public static bool IsUnsign(string value)
		{
			return Regex.IsMatch(value, "^\\d*[.]?\\d*$");
		}

		public static bool IsZipCode(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return true;
			}
			return _zipcoderegex.IsMatch(s);
		}

		public static bool IsImgFileName(string fileName)
		{
			if (fileName.IndexOf(".") != -1)
			{
				string text = fileName.Trim().ToLower();
				string text2 = text.Substring(text.LastIndexOf("."));
				switch (text2)
				{
				default:
					return text2 == ".swf";
				case ".png":
				case ".bmp":
				case ".jpg":
				case ".jpeg":
				case ".gif":
					return true;
				}
			}
			return false;
		}

		public static bool InIP(string sourceIP, string targetIP)
		{
			if (string.IsNullOrEmpty(sourceIP) || string.IsNullOrEmpty(targetIP))
			{
				return false;
			}
			string[] array = StringHelper.SplitString(sourceIP, ".");
			string[] array2 = StringHelper.SplitString(targetIP, ".");
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (array2[i] == "*")
				{
					return true;
				}
				if (array[i] != array2[i])
				{
					return false;
				}
				if (i == 3)
				{
					return true;
				}
			}
			return false;
		}

		public static bool InIPList(string sourceIP, string[] targetIPList)
		{
			if (targetIPList != null && targetIPList.Length > 0)
			{
				foreach (string targetIP in targetIPList)
				{
					if (InIP(sourceIP, targetIP))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool InIPList(string sourceIP, string targetIPStr)
		{
			string[] targetIPList = StringHelper.SplitString(targetIPStr, "\n");
			return InIPList(sourceIP, targetIPList);
		}

		public static bool BetweenPeriod(string[] periodList, out string liePeriod)
		{
			if (periodList != null && periodList.Length > 0)
			{
				DateTime now = DateTime.Now;
				DateTime date = now.Date;
				foreach (string text in periodList)
				{
					int num = text.IndexOf("-");
					DateTime dateTime = TypeUtil.StringToDateTime(text.Substring(0, num));
					DateTime t = TypeUtil.StringToDateTime(text.Substring(num + 1));
					if (dateTime < t)
					{
						if (now > dateTime && now < t)
						{
							liePeriod = text;
							return true;
						}
					}
					else if ((now > dateTime && now < date.AddDays(1.0)) || now < t)
					{
						liePeriod = text;
						return true;
					}
				}
			}
			liePeriod = string.Empty;
			return false;
		}

		public static bool BetweenPeriod(string periodStr, out string liePeriod)
		{
			string[] periodList = StringHelper.SplitString(periodStr, "\n");
			return BetweenPeriod(periodList, out liePeriod);
		}

		public static bool BetweenPeriod(string periodList)
		{
			string liePeriod = string.Empty;
			return BetweenPeriod(periodList, out liePeriod);
		}

		public static bool IsNumericArray(string[] numericStrList)
		{
			if (numericStrList != null && numericStrList.Length > 0)
			{
				foreach (string numericStr in numericStrList)
				{
					if (!IsNumeric(numericStr))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public static bool IsNumericRule(string numericRuleStr, string splitChar)
		{
			return IsNumericArray(StringHelper.SplitString(numericRuleStr, splitChar));
		}

		public static bool IsNumericRule(string numericRuleStr)
		{
			return IsNumericRule(numericRuleStr, ",");
		}

		public static bool IsCN(string strInput)
		{
			Regex regex = new Regex("^[一-龥]+$");
			return regex.IsMatch(strInput);
		}

		public static bool IsCNName(string strInput)
		{
			return _cnnameegex.IsMatch(strInput);
		}
	}
}

using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Entity.GameMatch;
using Game.Entity.Platform;
using Game.Entity.PlatformManager;
using Game.Entity.Treasure;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Game.Facade.Tools
{
	public class TypeUtil
	{
		public static int BoolToInt(bool b)
		{
			if (!b)
			{
				return 0;
			}
			return 1;
		}

		public static int StringToInt(string s, int defaultValue)
		{
			int result;
			if (!string.IsNullOrWhiteSpace(s) && int.TryParse(s, out result))
			{
				return result;
			}
			return defaultValue;
		}

		public static int StringToInt(string s)
		{
			return StringToInt(s, 0);
		}

		public static int ObjectToInt(object o, int defaultValue)
		{
			if (o != null)
			{
				return StringToInt(o.ToString(), defaultValue);
			}
			return defaultValue;
		}

		public static int ObjectToInt(object o)
		{
			return ObjectToInt(o, 0);
		}

		public static string ObjectToString(object o)
		{
			if (o != null)
			{
				return string.Concat(o);
			}
			return "";
		}

		public static bool StringToBool(string s, bool defaultValue)
		{
			if (s.Equals("0") || s.Equals("false", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (s.Equals("1") || s.Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			return defaultValue;
		}

		public static bool ToBool(string s)
		{
			return StringToBool(s, false);
		}

		public static bool ObjectToBool(object o, bool defaultValue)
		{
			if (o != null)
			{
				return StringToBool(o.ToString(), defaultValue);
			}
			return defaultValue;
		}

		public static bool ObjectToBool(object o)
		{
			return ObjectToBool(o, false);
		}

		public static DateTime StringToDateTime(string s, DateTime defaultValue)
		{
			DateTime result;
			if (!string.IsNullOrWhiteSpace(s) && DateTime.TryParse(s, out result))
			{
				return result;
			}
			return defaultValue;
		}

		public static DateTime StringToDateTime(string s)
		{
			return StringToDateTime(s, DateTime.Now);
		}

		public static DateTime ObjectToDateTime(object o, DateTime defaultValue)
		{
			if (o != null)
			{
				return StringToDateTime(o.ToString(), defaultValue);
			}
			return defaultValue;
		}

		public static DateTime ObjectToDateTime(object o)
		{
			return ObjectToDateTime(o, DateTime.Now);
		}

		public static DateTime? ObjectToDateTimeByNull(object o)
		{
			DateTime result;
			if (o != null && !string.IsNullOrWhiteSpace(o.ToString()) && DateTime.TryParse(o.ToString(), out result))
			{
				return result;
			}
			return null;
		}

		public static decimal StringToDecimal(string s, decimal defaultValue)
		{
			decimal result;
			if (!string.IsNullOrWhiteSpace(s) && decimal.TryParse(s, out result))
			{
				return result;
			}
			return defaultValue;
		}

		public static decimal StringToDecimal(string s)
		{
			return StringToDecimal(s, 0m);
		}

		public static decimal ObjectToDecimal(object o, decimal defaultValue)
		{
			if (o != null)
			{
				return StringToDecimal(o.ToString(), defaultValue);
			}
			return defaultValue;
		}

		public static decimal ObjectToDecimal(object o)
		{
			return ObjectToDecimal(o, 0m);
		}

		public static Guid ObjectToGuid(object o)
		{
			try
			{
				Guid result = Guid.Empty;
				if (o != null)
				{
					Guid.TryParse(o.ToString(), out result);
				}
				return result;
			}
			catch
			{
				return Guid.Empty;
			}
		}

		public static long ObjectToLong(object strValue)
		{
			return ObjectToLong(strValue, 0L);
		}

		public static long ObjectToLong(object strValue, long defValue)
		{
			if (strValue == null || Convert.IsDBNull(strValue) || strValue.ToString() == string.Empty)
			{
				return defValue;
			}
			string text = strValue.ToString();
			if (text.IndexOf(".") >= 0)
			{
				text = text.Split('.')[0];
			}
			long result = 0L;
			if (long.TryParse(text, out result))
			{
				return result;
			}
			return defValue;
		}

		public static string GetMemberList(string list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = list.Split(new char[1]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.AppendFormat("{0},", GetMemberName(byte.Parse(array[i])));
			}
			return stringBuilder.ToString().TrimEnd(',');
		}

		public static bool IsAndroid(int userID)
		{
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
			if (accountInfoByUserID == null)
			{
				return false;
			}
			if (accountInfoByUserID.IsAndroid == 1)
			{
				return true;
			}
			return false;
		}

		public static string GetUserRange(int rangeid)
		{
			string text = "";
			switch (rangeid)
			{
			case 0:
				return "全部用户";
			case 1:
				return "新注册用户";
			case 2:
				return "第一次充值用户";
			default:
				return "";
			}
		}

		public static string GetMasterName(int masterID)
		{
			return FacadeManage.aidePlatformManagerFacade.GetAccountsByUserID(masterID);
		}

		public static string GetExchangePlace(int IsGamePlaza)
		{
			if (IsGamePlaza == 0)
			{
				return "<span>大厅</span>";
			}
			return "<span class='hong'>网页</span>";
		}

		public static int GetUserIDByAccount(string accounts)
		{
			AccountsInfo accountInfoByAccount = FacadeManage.aideAccountsFacade.GetAccountInfoByAccount(accounts);
			if (accountInfoByAccount != null)
			{
				return accountInfoByAccount.UserID;
			}
			return 0;
		}

		public static string GetMemberName(byte order)
		{
			string text = "";
			string name = Enum.GetName(typeof(MemberOrderStatus), order);
			switch (order)
			{
			case 0:
				text = "{0}";
				break;
			case 1:
				text = "<span style='color:#15599f;font-weight:bold;'>{0}</span>";
				break;
			case 2:
				text = "<span style='color:#d16213;font-weight:bold;'>{0}</span>";
				break;
			case 3:
				text = "<span style='color:#777777; font-weight:bold;'>{0}</span>";
				break;
			case 4:
				text = "<span style='color:#b70000; font-weight:bold;'>{0}</span>";
				break;
			case 5:
				text = "<span style='color:#FF6600; font-weight:bold;'>{0}</span>";
				break;
			default:
				text = "{0}";
				break;
			}
			return string.Format(text, name);
		}

		public static string GetOperatingTypeName(string op)
		{
			LogTypeEnum logTypeEnum = (LogTypeEnum)Enum.Parse(typeof(LogTypeEnum), op);
			string result = "";
			switch (logTypeEnum)
			{
			case LogTypeEnum.Login:
				result = "登录";
				break;
			case LogTypeEnum.IncomeProc:
				result = "入款处理";
				break;
			case LogTypeEnum.PlayerInfo:
				result = "玩家信息";
				break;
			case LogTypeEnum.LimtCop:
				result = "限制操作";
				break;
			case LogTypeEnum.AccountLock:
				result = "锁定账号";
				break;
			case LogTypeEnum.AccountUnlock:
				result = "解锁账号";
				break;
			case LogTypeEnum.AlmsModify:
				result = "修改救济金";
				break;
			case LogTypeEnum.GTimeAwardModify:
				result = "修改游戏时长奖励";
				break;
			case LogTypeEnum.AgentInfoModify:
				result = "修改代理资料";
				break;
			case LogTypeEnum.RechangeAgent:
				result = "后台充值代理";
				break;
			case LogTypeEnum.PromoterProModify:
				result = "推广员属性修改";
				break;
			case LogTypeEnum.PromoterLockOrNo:
				result = "冻结/解冻推广员";
				break;
			}
			return result;
		}

		public static string GetNullityStatus(byte nullity)
		{
			if (nullity == 0)
			{
				return "<span>启用</span>";
			}
			return "<span style=\"color:red;\">禁用</span>";
		}

		public static string GetRegisterOrigin(byte origin)
		{
			string text = "";
			switch (origin)
			{
			case 0:
				return "PC";
			case 16:
				return "Android";
			case 32:
				return "iTouch";
			case 48:
				return "iPhone";
			case 64:
				return "iPad";
			case 80:
				return "WEB";
			default:
				return "WEB";
			}
		}

		public static string GetPassPortType(byte type)
		{
			string text = "";
			switch (type)
			{
			case 0:
				return "没有设置";
			case 1:
				return "身份证";
			case 2:
				return "护照";
			case 3:
				return "军官证";
			case 4:
				return "驾驶执照";
			default:
				return "其他";
			}
		}

		public static int GetUserIDByGameID(int gameID)
		{
			AccountsInfo accountInfoByGameID = FacadeManage.aideAccountsFacade.GetAccountInfoByGameID(gameID);
			if (accountInfoByGameID != null)
			{
				return accountInfoByGameID.UserID;
			}
			return 0;
		}

		public static string GetUserMedalByUserID(int userID)
		{
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
			if (accountInfoByUserID == null)
			{
				return "0";
			}
			return accountInfoByUserID.UserMedal.ToString();
		}

		public static string GetPresentByUserID(int userID)
		{
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
			if (accountInfoByUserID == null)
			{
				return "0";
			}
			return accountInfoByUserID.Present.ToString();
		}

		public static string GetLoveLinessByUserID(int userID)
		{
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
			if (accountInfoByUserID == null)
			{
				return "0";
			}
			return accountInfoByUserID.LoveLiness.ToString();
		}

		public static string GetExperienceByUserID(int userID)
		{
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
			if (accountInfoByUserID == null)
			{
				return "0";
			}
			return accountInfoByUserID.Experience.ToString();
		}

		public static string GetGameRoomName(int serverID)
		{
			GameRoomInfo gameRoomInfoInfo = FacadeManage.aidePlatformFacade.GetGameRoomInfoInfo(serverID);
			if (gameRoomInfoInfo == null)
			{
				return "";
			}
			return gameRoomInfoInfo.ServerName;
		}

		public static string GetGameKindName(int kindID)
		{
			GameKindItem gameKindItemInfo = FacadeManage.aidePlatformFacade.GetGameKindItemInfo(kindID);
			if (gameKindItemInfo == null)
			{
				return "";
			}
			return gameKindItemInfo.KindName;
		}

		public static string GetGameTypeName(int typeID)
		{
			GameTypeItem gameTypeItemInfo = FacadeManage.aidePlatformFacade.GetGameTypeItemInfo(typeID);
			if (gameTypeItemInfo == null)
			{
				return "";
			}
			return gameTypeItemInfo.TypeName;
		}

		public static string GetGameFlagName(int gameFlag)
		{
			string text = "";
			if ((gameFlag & 1) > 0)
			{
				text += "新,";
			}
			if ((gameFlag & 2) > 0)
			{
				text += "荐,";
			}
			if ((gameFlag & 4) > 0)
			{
				text += "热,";
			}
			if ((gameFlag & 8) > 0)
			{
				text += "赛,";
			}
			if (text != "")
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		public static string GetMarkName(int gameFlag)
		{
			string text = "";
			if ((gameFlag & 1) > 0)
			{
				text += "ios,";
			}
			if ((gameFlag & 2) > 0)
			{
				text += "android,";
			}
			if (text != "")
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		public static string GetTypeName(int gameFlag)
		{
			string text = "";
			if ((gameFlag & 1) > 0)
			{
				text += "房卡,";
			}
			if ((gameFlag & 2) > 0)
			{
				text += "金币,";
			}
			if (text != "")
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		public static string GetRoomNameByUserID(int UserID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT ServerID FROM {0} WHERE UserID={1}", "GameScoreLocker", UserID);
			string scalarBySql = FacadeManage.aideTreasureFacade.GetScalarBySql(stringBuilder.ToString());
			if (!string.IsNullOrEmpty(scalarBySql))
			{
				return GetGameRoomName(Convert.ToInt32(scalarBySql));
			}
			return " ";
		}

		public static string GetGameNameByUserID(int UserID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT KindID FROM {0} WHERE UserID={1}", "GameScoreLocker", UserID);
			string scalarBySql = FacadeManage.aideTreasureFacade.GetScalarBySql(stringBuilder.ToString());
			if (!string.IsNullOrEmpty(scalarBySql))
			{
				return GetGameKindName(Convert.ToInt32(scalarBySql));
			}
			return " ";
		}

		public static string GetAddressWithIP(string IP)
		{
			string text = "";
			try
			{
				return IPQuery.GetAddressWithIP(IP);
			}
			catch (Exception)
			{
				return "";
			}
		}

		public static string GetGameID(int userID)
		{
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
			if (accountInfoByUserID == null)
			{
				return "0";
			}
			return accountInfoByUserID.GameID.ToString();
		}

		public static string GetAccounts(int userID)
		{
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
			if (accountInfoByUserID == null)
			{
				return "";
			}
			return accountInfoByUserID.Accounts;
		}

		public static string GetNickNameByUserID(int userID)
		{
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
			if (accountInfoByUserID != null)
			{
				return accountInfoByUserID.NickName;
			}
			return "";
		}

		public static string GetProcess(string pro)
		{
			if (pro == "" || pro == null)
			{
				return "";
			}
			string arg = (pro == "1") ? "发送邮件成功" : ((pro == "2") ? "发送邮件失败" : ((pro == "3") ? "更新密码成功" : ""));
			if (!(pro == "0"))
			{
				return string.Format("<label title=\"{0}\" class=\"lanse\">已处理</label>", arg);
			}
			return "<font class=\"hong\">未处理</font>";
		}

		public static string GetSalesperson(int buildID)
		{
			if (buildID <= 0)
			{
				return "";
			}
			return FacadeManage.aideTreasureFacade.GetSalesperson(buildID);
		}

		public static string GetCardTypeName(int typeID)
		{
			GlobalLivcard globalLivcardInfo = FacadeManage.aideTreasureFacade.GetGlobalLivcardInfo(typeID);
			if (globalLivcardInfo == null)
			{
				return "";
			}
			return globalLivcardInfo.CardName;
		}

		public static string GetSerialID(int length, int cardType, Random rand, string tempID = "")
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int num = length; num > 0; num--)
			{
				stringBuilder.Append(rand.Next(0, 9).ToString());
			}
			tempID = tempID + DateTime.Now.ToString("yyMMdd") + DateTime.Now.Minute.ToString() + cardType.ToString() + stringBuilder.ToString();
			return tempID.Substring(0, length);
		}

		public static string GetPassword(int length, Random rand, bool isDigit, bool isLower, bool isUpper)
		{
			string text = "";
			if (isDigit && isLower && isUpper)
			{
				text = Fetch.GetRandomNumericAndEn(32, rand);
			}
			else if (isDigit && !isLower && !isUpper)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int num = length; num > 0; num--)
				{
					stringBuilder.Append(rand.Next(0, 9).ToString());
				}
				text = stringBuilder.ToString();
			}
			else if (!isDigit && isLower && !isUpper)
			{
				text = Fetch.GetRandomStr(32, false, rand).ToLower();
			}
			else if (!isDigit && !isLower && isUpper)
			{
				text = Fetch.GetRandomStr(32, false, rand).ToUpper();
			}
			else if (isDigit && isLower && !isUpper)
			{
				text = Fetch.GetRandomNumericAndEn(32, rand).ToLower();
			}
			else if (isDigit && !isLower && isUpper)
			{
				text = Fetch.GetRandomNumericAndEn(32, rand).ToUpper();
			}
			else if (!isDigit && isLower && isUpper)
			{
				text = Fetch.GetRandomStr(32, false, rand);
			}
			else
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int num2 = length; num2 > 0; num2--)
				{
					stringBuilder2.Append(rand.Next(0, 9).ToString());
				}
				text = stringBuilder2.ToString();
			}
			return text.Substring(0, length);
		}

		public static string FormatMoney(string money)
		{
			int num = money.IndexOf("-");
			string text = string.Empty;
			if (num != -1)
			{
				text = "-";
				money = money.Substring(1);
			}
			int num2 = money.IndexOf(".");
			string text2 = string.Empty;
			if (num2 != -1)
			{
				text2 = money.Substring(num2);
				money = money.Substring(0, num2);
			}
			if (money.Length > 3)
			{
				string text3 = money.Substring(0, money.Length - 3);
				string text4 = money.Substring(money.Length - 3, 3);
				if (text3.Length > 3)
				{
					return text + FormatMoney(text3) + "," + text4 + text2;
				}
				return text + text3 + "," + text4 + text2;
			}
			return text + money + text2;
		}

		public static string GetShareName(int shareID)
		{
			GlobalShareInfo globalShareByShareID = FacadeManage.aideTreasureFacade.GetGlobalShareByShareID(shareID);
			if (globalShareByShareID == null)
			{
				return "";
			}
			return globalShareByShareID.ShareName.Trim();
		}

		public static string GetOnLineOrderStatus(byte status)
		{
			string result = "";
			switch (status)
			{
			case 0:
				result = "<font color=red>未付款</font>";
				break;
			case 1:
				result = "<font>待处理</font>";
				break;
			case 2:
				result = "<font color=green>完成</font>";
				break;
			}
			return result;
		}

		public static string GetBankName(string bankID)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string value = "";
			dictionary.Add("1000000-NET", "易宝会员支付");
			dictionary.Add("ICBC-NET", "工商银行");
			dictionary.Add("ICBC-WAP", "工商银行WAP");
			dictionary.Add("CMBCHINA-NET", "招商银行");
			dictionary.Add("CMBCHINA-WAP", "招商银行WAP");
			dictionary.Add("ABC-NET", "中国农业银行");
			dictionary.Add("CCB-NET", "建设银行");
			dictionary.Add("CCB-PHONE", "建设银行WAP");
			dictionary.Add("BCCB-NET", "北京银行");
			dictionary.Add("BOCO-NET", "交通银行");
			dictionary.Add("CIB-NET", "兴业银行");
			dictionary.Add("NJCB-NET", "南京银行");
			dictionary.Add("CMBC-NET", "中国民生银行");
			dictionary.Add("CEB-NET", "光大银行");
			dictionary.Add("BOC-NET", "中国银行");
			dictionary.Add("PAB-NET", "平安银行");
			dictionary.Add("CBHB-NET", "渤海银行");
			dictionary.Add("HKBEA-NET", "东亚银行");
			dictionary.Add("NBCB-NET", "宁波银行");
			dictionary.Add("ECITIC-NET", "中信银行");
			dictionary.Add("SDB-NET", "深圳发展银行");
			dictionary.Add("GDB-NET", "广东发展银行");
			dictionary.Add("SPDB-NET", "上海浦东发展银行");
			dictionary.Add("POST-NET", "中国邮政");
			dictionary.Add("BJRCB-NET", "北京农村商业银行");
			if (dictionary.ContainsKey(bankID))
			{
				dictionary.TryGetValue(bankID, out value);
			}
			else
			{
				value = "未知";
			}
			return value;
		}

		public static string GetMessageTypeName(int messageType)
		{
			string result = "";
			switch (messageType)
			{
			case 1:
				result = "游戏";
				break;
			case 2:
				result = "房间";
				break;
			case 3:
				result = "全部";
				break;
			}
			return result;
		}

		public static string GetIssueArea(int intIssueArea)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IList<EnumDescription> issueAreaList = IssueAreaHelper.GetIssueAreaList(typeof(IssueArea));
			foreach (EnumDescription item in issueAreaList)
			{
				if (item.EnumValue == (intIssueArea & item.EnumValue))
				{
					stringBuilder.AppendFormat("{0},", IssueAreaHelper.GetIssueAreaDes((IssueArea)item.EnumValue));
				}
			}
			return stringBuilder.ToString().TrimEnd(',');
		}

		public static string GetServiceArea(int intServiceArea)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IList<EnumDescription> serviceAreaList = ServiceAreaHelper.GetServiceAreaList(typeof(ServiceArea));
			foreach (EnumDescription item in serviceAreaList)
			{
				if (item.EnumValue == (intServiceArea & item.EnumValue))
				{
					stringBuilder.AppendFormat("{0},", ServiceAreaHelper.GetServiceAreaDes((ServiceArea)item.EnumValue));
				}
			}
			return stringBuilder.ToString().TrimEnd(',');
		}

		public static string CalVersion(int version)
		{
			string str = "";
			str = str + (version >> 24).ToString() + ".";
			str = str + (version >> 16 << 24 >> 24).ToString() + ".";
			str = str + (version >> 8 << 24 >> 24).ToString() + ".";
			return str + (version << 24 >> 24).ToString();
		}

		public static int CalVersion2(string version)
		{
			int num = 0;
			string[] array = version.Split('.');
			return (int.Parse(array[0]) << 24) | (int.Parse(array[1]) << 16) | (int.Parse(array[2]) << 8) | int.Parse(array[3]);
		}

		public static string GetGamePropertyName(int id)
		{
			Game.Entity.Platform.GameProperty gamePropertyInfo = FacadeManage.aidePlatformFacade.GetGamePropertyInfo(id);
			if (gamePropertyInfo != null)
			{
				return gamePropertyInfo.Name;
			}
			return "";
		}

		public static string GetTaskName(int taskID)
		{
			TaskInfo taskInfoByID = FacadeManage.aidePlatformFacade.GetTaskInfoByID(taskID);
			if (taskInfoByID != null)
			{
				return taskInfoByID.TaskName;
			}
			return "";
		}

		public static Dictionary<string, string> EnumToDictionary(Type enumType)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!enumType.IsEnum)
			{
				return dictionary;
			}
			string[] names = Enum.GetNames(enumType);
			string[] array = names;
			foreach (string text in array)
			{
				string empty = string.Empty;
				FieldInfo field = enumType.GetField(text);
				object[] customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
				empty = ((customAttributes == null || customAttributes.Length <= 0) ? text : ((DescriptionAttribute)customAttributes[0]).Description);
				dictionary.Add(empty, Convert.ToInt32(Enum.Parse(enumType, empty)).ToString());
			}
			return dictionary;
		}

		public static string GetMatchTypeName(int matchID)
		{
			MatchPublic matchPublicInfo = FacadeManage.aideGameMatchFacade.GetMatchPublicInfo(matchID);
			if (matchPublicInfo != null)
			{
				if (matchPublicInfo.MatchType == 0)
				{
					return "定时赛";
				}
				if (matchPublicInfo.MatchType == 1)
				{
					return "即时赛";
				}
				return "未知";
			}
			return "未知";
		}

		public static string GetMatchStatusName(int matchID)
		{
			MatchPublic matchPublicInfo = FacadeManage.aideGameMatchFacade.GetMatchPublicInfo(matchID);
			string text = "";
			if (matchPublicInfo != null)
			{
				switch (matchPublicInfo.MatchStatus)
				{
				case 0:
					return "<span style='color:red;' >未开始</span>";
				case 2:
					return "<span style='color:blue;'>进行中</span>";
				case 8:
					return "<span style='color:green;'>已结束</span>";
				default:
					return "<span>未知</span>";
				}
			}
			return "<span>未知</span>";
		}

		public static byte[] GetByte(MemoryStream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			stream.Close();
			return array;
		}

		public static byte[] GetByte(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			stream.Close();
			return array;
		}

		public static string GetMapPath(string path)
		{
			if (HttpContext.Current != null)
			{
				return HttpContext.Current.Server.MapPath(path);
			}
			return HostingEnvironment.MapPath(path);
		}

		public static DataTable FormatTable(DataTable sourceDt, string year, string month, string day)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("InsertDateTime");
			dataTable.Columns.Add("MaxCount");
			dataTable.Columns.Add("MinCount");
			dataTable.Columns.Add("AvgCount");
			if (month == "-1")
			{
				string text = "";
				for (int i = 1; i < 13; i++)
				{
					text = year + "-" + i.ToString().PadLeft(2, '0');
					DataRow dataRow = dataTable.NewRow();
					dataRow[0] = text;
					DataRow[] array = sourceDt.Select("InsertDateTime='" + text + "'");
					if (array.Length == 0)
					{
						dataRow[1] = "--";
						dataRow[2] = "--";
						dataRow[3] = "--";
					}
					else
					{
						dataRow[1] = array[0].ItemArray[1].ToString();
						dataRow[2] = array[0].ItemArray[2].ToString();
						dataRow[3] = array[0].ItemArray[3].ToString();
					}
					dataTable.Rows.Add(dataRow);
				}
			}
			else if (day == "-1")
			{
				DateTime d = Convert.ToDateTime(year + "-" + month + "-01");
				DateTime d2 = d.AddMonths(1);
				while (d != d2)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2[0] = d.ToString("yyyy-MM-dd");
					DataRow[] array2 = sourceDt.Select("InsertDateTime='" + d.ToString("yyyy-MM-dd") + "'");
					if (array2.Length == 0)
					{
						dataRow2[1] = "--";
						dataRow2[2] = "--";
						dataRow2[3] = "--";
					}
					else
					{
						dataRow2[1] = array2[0].ItemArray[1].ToString();
						dataRow2[2] = array2[0].ItemArray[2].ToString();
						dataRow2[3] = array2[0].ItemArray[3].ToString();
					}
					dataTable.Rows.Add(dataRow2);
					d = d.AddDays(1.0);
				}
			}
			else
			{
				for (int j = 0; j < 24; j++)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3[0] = j.ToString();
					DataRow[] array3 = sourceDt.Select("InsertDateTime=" + j);
					if (array3.Length == 0)
					{
						dataRow3[1] = "--";
						dataRow3[2] = "--";
						dataRow3[3] = "--";
					}
					else
					{
						dataRow3[1] = array3[0].ItemArray[1].ToString();
						dataRow3[2] = array3[0].ItemArray[2].ToString();
						dataRow3[3] = array3[0].ItemArray[3].ToString();
					}
					dataTable.Rows.Add(dataRow3);
				}
			}
			return dataTable;
		}

		public static string GameGameNameByServerID(int serverID)
		{
			GameRoomInfo gameRoomInfoInfo = FacadeManage.aidePlatformFacade.GetGameRoomInfoInfo(serverID);
			if (gameRoomInfoInfo != null)
			{
				GameKindItem gameKindItemInfo = FacadeManage.aidePlatformFacade.GetGameKindItemInfo(gameRoomInfoInfo.KindID);
				if (gameKindItemInfo != null)
				{
					return gameKindItemInfo.KindName;
				}
			}
			return "";
		}

		public static string GetRoleName(int roleID)
		{
			Base_Roles roleInfo = FacadeManage.aidePlatformManagerFacade.GetRoleInfo(roleID);
			if (roleInfo == null)
			{
				return "";
			}
			return roleInfo.RoleName;
		}

		public static bool IsExit(int o, int j)
		{
			if (o < j)
			{
				return false;
			}
			return (o & j) == j;
		}

		public static bool IsPower(int MoudleID, int RoleID, string PowerName)
		{
			if (RoleID <= 1)
			{
				return true;
			}
			DataSet permissionByModuleID = FacadeManage.aidePlatformManagerFacade.GetPermissionByModuleID(MoudleID, RoleID);
			if (permissionByModuleID == null || permissionByModuleID.Tables.Count <= 0 || permissionByModuleID.Tables[0].Rows.Count < 1)
			{
				return false;
			}
			if (string.IsNullOrEmpty(PowerName))
			{
				return false;
			}
			DataRow[] array = permissionByModuleID.Tables[0].Select("PermissionTitle='" + PowerName + "'");
			if (array != null && array.Count() > 0)
			{
				return true;
			}
			return false;
		}

		public static bool IsInteger(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}
			int result = 0;
			return int.TryParse(str, out result);
		}

		public static string LongToTimeStr(long t)
		{
			StringBuilder stringBuilder = new StringBuilder();
			long num = 0L;
			long num2 = 0L;
			long num3 = 0L;
			if (t > 0)
			{
				num = t / 3600;
				num2 = (t - num * 3600) / 60;
				num3 = t - num * 3600 - num2 * 60;
			}
			stringBuilder.AppendFormat("{0}时{1}分{2}秒", num, num2, num3);
			return stringBuilder.ToString();
		}

		public static string LongToTimeStr1(long t)
		{
			StringBuilder stringBuilder = new StringBuilder();
			long num = 0L;
			long num2 = 0L;
			long num3 = 0L;
			long num4 = 0L;
			if (t > 0)
			{
				if (t > 86400)
				{
					num4 = t / 86400;
				}
				num = (t - num4 * 24 * 3600) / 3600;
				num2 = (t - num * 3600) / 60;
				num3 = t - num * 3600 - num2 * 60;
			}
			stringBuilder.AppendFormat("{0}天{1}时{2}分{3}秒", num4, num, num2, num3);
			return stringBuilder.ToString();
		}

		public static bool AgentRight(int agentRight, int aguRight)
		{
			if (agentRight < aguRight)
			{
				return false;
			}
			return (agentRight & aguRight) == aguRight;
		}

		public static bool IsHasRight(string urlprefix, int agentRight, int aguRight, int isclient)
		{
			string agentRightKeys = GetAgentRightKeys(agentRight, aguRight, isclient);
			if (string.IsNullOrEmpty(agentRightKeys) || string.IsNullOrEmpty(urlprefix))
			{
				return false;
			}
			string[] source = agentRightKeys.Split(',');
			if (string.IsNullOrEmpty(source.FirstOrDefault((string q) => q.Equals(urlprefix))))
			{
				return false;
			}
			return true;
		}

		public static string GetAgentRightKeys(int agentRight, int aguRight, int isclient)
		{
			string ostr = "";
			if (isclient == 2)
			{
				ostr = addkeys(ostr, "recharges");
			}
			else
			{
				ostr = addkeys(ostr, "new");
				if (isclient == 1)
				{
					ostr = addkeys(ostr, "pay");
					ostr = addkeys(ostr, "payOrder");
				}
				if (AgentRight(agentRight, aguRight))
				{
					switch (aguRight)
					{
					case 1:
						ostr = addkeys(ostr, "recharges");
						ostr = addkeys(ostr, "payunline");
						break;
					case 2:
						ostr = addkeys(ostr, "gameRecord");
						break;
					case 4:
						ostr = addkeys(ostr, "balance");
						break;
					case 8:
						ostr = addkeys(ostr, "accountScoreList");
						break;
					case 16:
						ostr = addkeys(ostr, "notice");
						break;
					case 32:
						ostr = addkeys(ostr, "revise");
						break;
					case 64:
						ostr = addkeys(ostr, "game");
						break;
					case 128:
						ostr = addkeys(ostr, "agent");
						break;
					case 256:
						ostr = addkeys(ostr, "baobiao");
						break;
					case 512:
						ostr = addkeys(ostr, "recordcs");
						break;
					case 1024:
						ostr = addkeys(ostr, "change");
						break;
					case 2048:
						ostr = addkeys(ostr, "agent_win");
						break;
					case 4096:
						ostr = addkeys(ostr, "game_win");
						break;
					case 8192:
						ostr = addkeys(ostr, "money");
						break;
					case 16384:
						ostr = addkeys(ostr, "DuiHuanRecord");
						break;
					}
				}
			}
			return ostr;
		}

		public static string addkeys(string ostr, string astr)
		{
			ostr = ((!(ostr == "")) ? (ostr + "," + astr) : astr);
			return ostr;
		}

		public static DateTime GetMonthFirstDay()
		{
			DateTime now = DateTime.Now;
			return new DateTime(now.Year, now.Month, 1);
		}

		public static DateTime GetMonthLastDay()
		{
			return GetMonthFirstDay().AddMonths(1).AddDays(-1.0);
		}

		public static DateTime GetLastMonthFirstDay()
		{
			DateTime dateTime = DateTime.Now.AddMonths(-1);
			return new DateTime(dateTime.Year, dateTime.Month, 1);
		}

		public static DateTime GetLastMonthLastDay()
		{
			return GetLastMonthFirstDay().AddMonths(1).AddDays(-1.0);
		}
	}
}

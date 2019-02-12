using Game.Utils;
using System;

namespace Game.Facade
{
	public class AppConfig
	{
		public const string VerifyCodeKey = "VerifyCodeKey";

		public static string ServerUrl
		{
			get
			{
				try
				{
					string appSetting = Utility.GetAppSetting("ServerUrl");
					if (string.IsNullOrEmpty(appSetting))
					{
						return "";
					}
					return appSetting;
				}
				catch
				{
					return "";
				}
			}
		}

		public static string IsApnsProduction
		{
			get
			{
				try
				{
					string appSetting = Utility.GetAppSetting("IsApnsProduction");
					if (string.IsNullOrEmpty(appSetting))
					{
						return "true";
					}
					return appSetting;
				}
				catch
				{
					return "true";
				}
			}
		}

		public static string AppKey
		{
			get
			{
				string appSetting = Utility.GetAppSetting("AppKey");
				if (!string.IsNullOrEmpty(appSetting))
				{
					return appSetting;
				}
				return "24a30f5836cb644f5fedfd83";
			}
		}

		public static string MasterSecret
		{
			get
			{
				string appSetting = Utility.GetAppSetting("MasterSecret");
				if (!string.IsNullOrEmpty(appSetting))
				{
					return appSetting;
				}
				return "61f10c7866c4b47d1df80026";
			}
		}

		public static string UserCacheKey
		{
			get
			{
				string appSetting = Utility.GetAppSetting("AppPrefix");
				if (!string.IsNullOrEmpty(appSetting))
				{
					return appSetting;
				}
				return "6603sAdministratorKey";
			}
		}

		public static int UserCacheTimeOut
		{
			get
			{
				string appSetting = Utility.GetAppSetting("UserCacheTimeOut");
				if (!string.IsNullOrEmpty(appSetting))
				{
					return Convert.ToInt32(appSetting);
				}
				return 30;
			}
		}

		public static string ReportForgetPasswordKey
		{
			get
			{
				string text = ApplicationSettings.Get("ReportForgetPasswordKey");
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				return "ReportForgetPasswordKeyValue";
			}
		}
	}
}

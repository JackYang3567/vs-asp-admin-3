using System.Configuration;

namespace Game.Entity
{
	public class SysConfig
	{
		public static string key
		{
			get
			{
				return ConfigurationManager.AppSettings["key_41"].ToString();
			}
		}

		public static string code
		{
			get
			{
				return ConfigurationManager.AppSettings["partner_41"].ToString();
			}
		}
	}
}

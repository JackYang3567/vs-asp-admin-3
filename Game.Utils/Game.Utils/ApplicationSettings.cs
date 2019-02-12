using System.Configuration;

namespace Game.Utils
{
	public class ApplicationSettings
	{
		public static string Get(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return string.Empty;
			}
			string text = ConfigurationManager.AppSettings[key];
			if (text == null)
			{
				throw new FrameworkExcption("WebConfigHasNotAddKey", key);
			}
			return text;
		}
	}
}

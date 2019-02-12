using System.Net;
using System.Text;
using System.Web;

namespace Game.Facade
{
	public class Ip138
	{
		public static string GetIPData(string token, string ip = null, string datatype = "txt")
		{
			if (string.IsNullOrEmpty(ip))
			{
				ip = HttpContext.Current.Request.UserHostAddress;
			}
			string address = string.Format("http://api.ip138.com/query/?ip={0}&datatype={1}&token={2}", ip, datatype, token);
			using (WebClient webClient = new WebClient())
			{
				webClient.Encoding = Encoding.UTF8;
				return webClient.DownloadString(address);
			}
		}
	}
}

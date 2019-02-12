using Game.Utils.Properties;
using System.Web;

namespace Game.Utils
{
	public class Terminator
	{
		public virtual string template
		{
			get
			{
				return AppExceptions.Terminator_ExceptionTemplate;
			}
		}

		public virtual void Alert(string s)
		{
			Echo("<script language='javascript'>alert('" + s.Replace("'", "\\'") + "');history.back();</script>");
			End();
		}

		public virtual void Alert(string s, string backurl)
		{
			Echo("<script language='javascript'>alert('" + s.Replace("'", "\\'") + "');location.href='" + backurl + "';</script>");
			End();
		}

		private void Echo(string s)
		{
			HttpContext.Current.Response.Write(s);
		}

		private void End()
		{
			HttpContext.Current.Response.End();
		}

		public virtual void Throw(string message)
		{
			HttpContext.Current.Response.ContentType = "text/html";
			HttpContext.Current.Response.AddHeader("Content-Type", "text/html");
			Throw(message, null, null, null, true);
		}

		public virtual void Throw(string message, string title, string links, string autojump, bool showback)
		{
			Echo("系统发生错误，错误信息：”" + message + "“");
			End();
		}
	}
}

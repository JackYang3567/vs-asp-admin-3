using System;
using System.IO;
using System.Web;

namespace Game.Facade.Mail
{
	public class MailConfigInfo
	{
		private string m_accounts;

		private string m_password;

		private string m_smtpServer;

		private int m_port;

		private string m_smtpSenderEmail;

		private string m_lossreportUrl;

		public string Accounts
		{
			get
			{
				return m_accounts;
			}
			set
			{
				m_accounts = value;
			}
		}

		public string Password
		{
			get
			{
				return m_password;
			}
			set
			{
				m_password = value;
			}
		}

		public string SmtpServer
		{
			get
			{
				return m_smtpServer;
			}
			set
			{
				m_smtpServer = value;
			}
		}

		public int Port
		{
			get
			{
				return m_port;
			}
			set
			{
				m_port = value;
			}
		}

		public string SmtpSenderEmail
		{
			get
			{
				return m_smtpSenderEmail;
			}
			set
			{
				m_smtpSenderEmail = value;
			}
		}

		public string LossreportUrl
		{
			get
			{
				return m_lossreportUrl;
			}
			set
			{
				m_lossreportUrl = value;
			}
		}

		public string GetMapPath(string strPath)
		{
			if (string.IsNullOrEmpty(strPath))
			{
				throw new Exception("strPath 不能为空！");
			}
			string empty = string.Empty;
			HttpContext httpContext = null;
			try
			{
				httpContext = HttpContext.Current;
			}
			catch
			{
				httpContext = null;
			}
			if (httpContext == null)
			{
				string text = Path.Combine(strPath, "");
				text = (text.StartsWith("\\\\") ? text.Remove(0, 2) : text);
				return AppDomain.CurrentDomain.BaseDirectory + Path.Combine(strPath, "");
			}
			return httpContext.Server.MapPath(strPath);
		}
	}
}

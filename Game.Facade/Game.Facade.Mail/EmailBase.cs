using Game.Utils;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace Game.Facade.Mail
{
	public class EmailBase
	{
		public delegate void MassMailingCallback(string mailAddress, string errorMessage);

		private string m_accounts;

		private string m_password;

		private string m_content;

		private string m_smtpServer;

		private int m_port;

		private string m_smtpSenderEmail;

		private MessageRender _messageRender;

		private string contentTempdate;

		private string subjectTemplate;

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

		public virtual string Subject
		{
			get
			{
				return _messageRender.Render(subjectTemplate);
			}
		}

		public string Content
		{
			get
			{
				if (string.IsNullOrEmpty(m_content))
				{
					m_content = _messageRender.Render(contentTempdate);
				}
				return m_content;
			}
			set
			{
				m_content = value;
			}
		}

		public MessageRender Render
		{
			get
			{
				return _messageRender;
			}
		}

		public EmailBase()
		{
			_messageRender = new MessageRender();
		}

		public EmailBase(MailConfigInfo mailConfigInfo)
		{
			_messageRender = new MessageRender();
			m_smtpServer = mailConfigInfo.SmtpServer;
			m_smtpSenderEmail = mailConfigInfo.SmtpSenderEmail;
			m_accounts = mailConfigInfo.Accounts;
			m_password = mailConfigInfo.Password;
			m_port = mailConfigInfo.Port;
		}

		public EmailBase(string subjectTemplate, string contentTemplate, MailConfigInfo mailConfigInfo)
			: this(mailConfigInfo)
		{
			_messageRender = new MessageRender();
			this.subjectTemplate = subjectTemplate;
			contentTempdate = contentTemplate;
		}

		public EmailBase(string subjectTemplate, string contentTemplate)
			: this()
		{
			_messageRender = new MessageRender();
			this.subjectTemplate = subjectTemplate;
			contentTempdate = contentTemplate;
		}

		public EmailBase(MailTMLConfigInfo mailTml, MailConfigInfo mailConfigInfo)
			: this(mailConfigInfo)
		{
			_messageRender = new MessageRender();
			subjectTemplate = mailTml.MailTitle;
			contentTempdate = mailTml.MailContent.Text;
		}

		public void TestEmail(string address)
		{
			SmtpClient smtpClient = new SmtpClient(SmtpServer);
			smtpClient.UseDefaultCredentials = false;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.Credentials = new NetworkCredential(Accounts, Password);
			smtpClient.Port = Port;
			SmtpClient smtpClient2 = smtpClient;
			MailMessage mailMessage = new MailMessage(SmtpSenderEmail, address, Subject, Content);
			mailMessage.IsBodyHtml = true;
			mailMessage.BodyEncoding = Encoding.GetEncoding("gb2312");
			MailMessage mailMessage2 = mailMessage;
			if (Port != 25)
			{
				smtpClient2.EnableSsl = true;
			}
			smtpClient2.Send(mailMessage2);
			mailMessage2.Dispose();
		}

		public void Send(string[] mailAddress)
		{
			if (mailAddress != null)
			{
				WaitCallback callBack = Send;
				foreach (string text in mailAddress)
				{
					if (!string.IsNullOrEmpty(text))
					{
						ThreadPool.QueueUserWorkItem(callBack, text.Trim());
					}
				}
			}
		}

		private void Send(object email)
		{
			try
			{
				SmtpClient smtpClient = new SmtpClient(SmtpServer);
				smtpClient.UseDefaultCredentials = false;
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtpClient.Credentials = new NetworkCredential(Accounts, Password);
				SmtpClient smtpClient2 = smtpClient;
				MailMessage mailMessage = new MailMessage(SmtpSenderEmail, email.ToString(), Subject, Content);
				mailMessage.IsBodyHtml = true;
				mailMessage.BodyEncoding = Encoding.GetEncoding("gb2312");
				MailMessage mailMessage2 = mailMessage;
				if (Port != 25)
				{
					smtpClient2.EnableSsl = true;
				}
				smtpClient2.Send(mailMessage2);
				mailMessage2.Dispose();
			}
			catch (Exception ex)
			{
				TextLogger.Write(ex.ToString());
			}
		}

		public void Send(string address)
		{
			ThreadPool.QueueUserWorkItem(Send, address);
		}
	}
}

using Game.Kernel;
using Game.Utils;
using System.Xml.Serialization;

namespace Game.Facade.Mail
{
	public class MailTMLConfigInfo : IConfigInfo
	{
		private CDATA m_contentTempdate = new CDATA("");

		private string m_subjectTemplate = "";

		[XmlElement("MailContent", Type = typeof(CDATA))]
		public CDATA MailContent
		{
			get
			{
				return m_contentTempdate;
			}
			set
			{
				m_contentTempdate = value;
			}
		}

		public string MailTitle
		{
			get
			{
				return m_subjectTemplate;
			}
			set
			{
				m_subjectTemplate = value;
			}
		}

		public MailTMLConfigInfo()
		{
		}

		public MailTMLConfigInfo(string contentTml, string titleTml)
		{
			m_contentTempdate = new CDATA(contentTml);
			m_subjectTemplate = titleTml;
		}
	}
}

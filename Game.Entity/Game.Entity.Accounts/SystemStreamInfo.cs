using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class SystemStreamInfo
	{
		public const string Tablename = "SystemStreamInfo";

		public const string _DateID = "DateID";

		public const string _WebLogonSuccess = "WebLogonSuccess";

		public const string _WebRegisterSuccess = "WebRegisterSuccess";

		public const string _GameLogonSuccess = "GameLogonSuccess";

		public const string _GameRegisterSuccess = "GameRegisterSuccess";

		public const string _CollectDate = "CollectDate";

		private int m_dateID;

		private int m_webLogonSuccess;

		private int m_webRegisterSuccess;

		private int m_gameLogonSuccess;

		private int m_gameRegisterSuccess;

		private DateTime m_collectDate;

		public int DateID
		{
			get
			{
				return m_dateID;
			}
			set
			{
				m_dateID = value;
			}
		}

		public int WebLogonSuccess
		{
			get
			{
				return m_webLogonSuccess;
			}
			set
			{
				m_webLogonSuccess = value;
			}
		}

		public int WebRegisterSuccess
		{
			get
			{
				return m_webRegisterSuccess;
			}
			set
			{
				m_webRegisterSuccess = value;
			}
		}

		public int GameLogonSuccess
		{
			get
			{
				return m_gameLogonSuccess;
			}
			set
			{
				m_gameLogonSuccess = value;
			}
		}

		public int GameRegisterSuccess
		{
			get
			{
				return m_gameRegisterSuccess;
			}
			set
			{
				m_gameRegisterSuccess = value;
			}
		}

		public DateTime CollectDate
		{
			get
			{
				return m_collectDate;
			}
			set
			{
				m_collectDate = value;
			}
		}

		public SystemStreamInfo()
		{
			m_dateID = 0;
			m_webLogonSuccess = 0;
			m_webRegisterSuccess = 0;
			m_gameLogonSuccess = 0;
			m_gameRegisterSuccess = 0;
			m_collectDate = DateTime.Now;
		}
	}
}

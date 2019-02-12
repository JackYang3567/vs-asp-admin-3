using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class SystemStreamInfo
	{
		public const string Tablename = "SystemStreamInfo";

		public const string _DateID = "DateID";

		public const string _KindID = "KindID";

		public const string _ServerID = "ServerID";

		public const string _LogonCount = "LogonCount";

		public const string _RegisterCount = "RegisterCount";

		public const string _CollectDate = "CollectDate";

		private int m_dateID;

		private int m_kindID;

		private int m_serverID;

		private int m_logonCount;

		private int m_registerCount;

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

		public int KindID
		{
			get
			{
				return m_kindID;
			}
			set
			{
				m_kindID = value;
			}
		}

		public int ServerID
		{
			get
			{
				return m_serverID;
			}
			set
			{
				m_serverID = value;
			}
		}

		public int LogonCount
		{
			get
			{
				return m_logonCount;
			}
			set
			{
				m_logonCount = value;
			}
		}

		public int RegisterCount
		{
			get
			{
				return m_registerCount;
			}
			set
			{
				m_registerCount = value;
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
			m_kindID = 0;
			m_serverID = 0;
			m_logonCount = 0;
			m_registerCount = 0;
			m_collectDate = DateTime.Now;
		}
	}
}

using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class GameScoreLocker
	{
		public const string Tablename = "GameScoreLocker";

		public const string _UserID = "UserID";

		public const string _KindID = "KindID";

		public const string _ServerID = "ServerID";

		public const string _EnterID = "EnterID";

		public const string _EnterIP = "EnterIP";

		public const string _EnterMachine = "EnterMachine";

		public const string _CollectDate = "CollectDate";

		private int m_userID;

		private int m_kindID;

		private int m_serverID;

		private int m_enterID;

		private string m_enterIP;

		private string m_enterMachine;

		private DateTime m_collectDate;

		public int UserID
		{
			get
			{
				return m_userID;
			}
			set
			{
				m_userID = value;
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

		public int EnterID
		{
			get
			{
				return m_enterID;
			}
			set
			{
				m_enterID = value;
			}
		}

		public string EnterIP
		{
			get
			{
				return m_enterIP;
			}
			set
			{
				m_enterIP = value;
			}
		}

		public string EnterMachine
		{
			get
			{
				return m_enterMachine;
			}
			set
			{
				m_enterMachine = value;
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

		public GameScoreLocker()
		{
			m_userID = 0;
			m_kindID = 0;
			m_serverID = 0;
			m_enterID = 0;
			m_enterIP = "";
			m_enterMachine = "";
			m_collectDate = DateTime.Now;
		}
	}
}

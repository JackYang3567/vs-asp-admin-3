using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class AccountsAgentGame
	{
		public const string Tablename = "AccountsAgentGame";

		public const string _ID = "ID";

		public const string _AgentID = "AgentID";

		public const string _KindID = "KindID";

		public const string _DeviceID = "DeviceID";

		public const string _SortID = "SortID";

		public const string _CollectDate = "CollectDate";

		private int m_id;

		private int m_agentID;

		private int m_kindID;

		private int m_deviceID;

		private int m_sortID;

		private DateTime m_collectDate;

		public int ID
		{
			get
			{
				return m_id;
			}
			set
			{
				m_id = value;
			}
		}

		public int AgentID
		{
			get
			{
				return m_agentID;
			}
			set
			{
				m_agentID = value;
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

		public int DeviceID
		{
			get
			{
				return m_deviceID;
			}
			set
			{
				m_deviceID = value;
			}
		}

		public int SortID
		{
			get
			{
				return m_sortID;
			}
			set
			{
				m_sortID = value;
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

		public AccountsAgentGame()
		{
			m_id = 0;
			m_agentID = 0;
			m_kindID = 0;
			m_deviceID = 0;
			m_sortID = 0;
			m_collectDate = DateTime.Now;
		}
	}
}

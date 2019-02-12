using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GlobalPlayPresent
	{
		public const string Tablename = "GlobalPlayPresent";

		public const string _ServerID = "ServerID";

		public const string _PresentMember = "PresentMember";

		public const string _MaxDatePresent = "MaxDatePresent";

		public const string _MaxPresent = "MaxPresent";

		public const string _CellPlayPresnet = "CellPlayPresnet";

		public const string _CellPlayTime = "CellPlayTime";

		public const string _StartPlayTime = "StartPlayTime";

		public const string _CellOnlinePresent = "CellOnlinePresent";

		public const string _CellOnlineTime = "CellOnlineTime";

		public const string _StartOnlineTime = "StartOnlineTime";

		public const string _IsPlayPresent = "IsPlayPresent";

		public const string _IsOnlinePresent = "IsOnlinePresent";

		public const string _CollectDate = "CollectDate";

		private int m_serverID;

		private string m_presentMember;

		private int m_maxDatePresent;

		private int m_maxPresent;

		private int m_cellPlayPresnet;

		private int m_cellPlayTime;

		private int m_startPlayTime;

		private int m_cellOnlinePresent;

		private int m_cellOnlineTime;

		private int m_startOnlineTime;

		private byte m_isPlayPresent;

		private byte m_isOnlinePresent;

		private DateTime m_collectDate;

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

		public string PresentMember
		{
			get
			{
				return m_presentMember;
			}
			set
			{
				m_presentMember = value;
			}
		}

		public int MaxDatePresent
		{
			get
			{
				return m_maxDatePresent;
			}
			set
			{
				m_maxDatePresent = value;
			}
		}

		public int MaxPresent
		{
			get
			{
				return m_maxPresent;
			}
			set
			{
				m_maxPresent = value;
			}
		}

		public int CellPlayPresnet
		{
			get
			{
				return m_cellPlayPresnet;
			}
			set
			{
				m_cellPlayPresnet = value;
			}
		}

		public int CellPlayTime
		{
			get
			{
				return m_cellPlayTime;
			}
			set
			{
				m_cellPlayTime = value;
			}
		}

		public int StartPlayTime
		{
			get
			{
				return m_startPlayTime;
			}
			set
			{
				m_startPlayTime = value;
			}
		}

		public int CellOnlinePresent
		{
			get
			{
				return m_cellOnlinePresent;
			}
			set
			{
				m_cellOnlinePresent = value;
			}
		}

		public int CellOnlineTime
		{
			get
			{
				return m_cellOnlineTime;
			}
			set
			{
				m_cellOnlineTime = value;
			}
		}

		public int StartOnlineTime
		{
			get
			{
				return m_startOnlineTime;
			}
			set
			{
				m_startOnlineTime = value;
			}
		}

		public byte IsPlayPresent
		{
			get
			{
				return m_isPlayPresent;
			}
			set
			{
				m_isPlayPresent = value;
			}
		}

		public byte IsOnlinePresent
		{
			get
			{
				return m_isOnlinePresent;
			}
			set
			{
				m_isOnlinePresent = value;
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

		public GlobalPlayPresent()
		{
			m_serverID = 0;
			m_presentMember = "";
			m_maxDatePresent = 0;
			m_maxPresent = 0;
			m_cellPlayPresnet = 0;
			m_cellPlayTime = 0;
			m_startPlayTime = 0;
			m_cellOnlinePresent = 0;
			m_cellOnlineTime = 0;
			m_startOnlineTime = 0;
			m_isPlayPresent = 0;
			m_isOnlinePresent = 0;
			m_collectDate = DateTime.Now;
		}
	}
}

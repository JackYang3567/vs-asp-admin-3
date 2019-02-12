using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class GameHistoryScore
	{
		public const string Tablename = "GameHistoryScore";

		public const string _MatchID = "MatchID";

		public const string _MatchNo = "MatchNo";

		public const string _UserID = "UserID";

		public const string _HScore = "HScore";

		public const string _HWinCount = "HWinCount";

		public const string _HLostCount = "HLostCount";

		public const string _HDrawCount = "HDrawCount";

		public const string _HFleeCount = "HFleeCount";

		public const string _HBackupTime = "HBackupTime";

		public const string _NScore = "NScore";

		public const string _NWinCount = "NWinCount";

		public const string _NLostCount = "NLostCount";

		public const string _NDrawCount = "NDrawCount";

		public const string _NFleeCount = "NFleeCount";

		public const string _NBackupTime = "NBackupTime";

		private int m_matchID;

		private int m_matchNo;

		private int m_userID;

		private long m_hScore;

		private int m_hWinCount;

		private int m_hLostCount;

		private int m_hDrawCount;

		private int m_hFleeCount;

		private DateTime m_hBackupTime;

		private long m_nScore;

		private int m_nWinCount;

		private int m_nLostCount;

		private int m_nDrawCount;

		private int m_nFleeCount;

		private DateTime m_nBackupTime;

		public int MatchID
		{
			get
			{
				return m_matchID;
			}
			set
			{
				m_matchID = value;
			}
		}

		public int MatchNo
		{
			get
			{
				return m_matchNo;
			}
			set
			{
				m_matchNo = value;
			}
		}

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

		public long HScore
		{
			get
			{
				return m_hScore;
			}
			set
			{
				m_hScore = value;
			}
		}

		public int HWinCount
		{
			get
			{
				return m_hWinCount;
			}
			set
			{
				m_hWinCount = value;
			}
		}

		public int HLostCount
		{
			get
			{
				return m_hLostCount;
			}
			set
			{
				m_hLostCount = value;
			}
		}

		public int HDrawCount
		{
			get
			{
				return m_hDrawCount;
			}
			set
			{
				m_hDrawCount = value;
			}
		}

		public int HFleeCount
		{
			get
			{
				return m_hFleeCount;
			}
			set
			{
				m_hFleeCount = value;
			}
		}

		public DateTime HBackupTime
		{
			get
			{
				return m_hBackupTime;
			}
			set
			{
				m_hBackupTime = value;
			}
		}

		public long NScore
		{
			get
			{
				return m_nScore;
			}
			set
			{
				m_nScore = value;
			}
		}

		public int NWinCount
		{
			get
			{
				return m_nWinCount;
			}
			set
			{
				m_nWinCount = value;
			}
		}

		public int NLostCount
		{
			get
			{
				return m_nLostCount;
			}
			set
			{
				m_nLostCount = value;
			}
		}

		public int NDrawCount
		{
			get
			{
				return m_nDrawCount;
			}
			set
			{
				m_nDrawCount = value;
			}
		}

		public int NFleeCount
		{
			get
			{
				return m_nFleeCount;
			}
			set
			{
				m_nFleeCount = value;
			}
		}

		public DateTime NBackupTime
		{
			get
			{
				return m_nBackupTime;
			}
			set
			{
				m_nBackupTime = value;
			}
		}

		public GameHistoryScore()
		{
			m_matchID = 0;
			m_matchNo = 0;
			m_userID = 0;
			m_hScore = 0L;
			m_hWinCount = 0;
			m_hLostCount = 0;
			m_hDrawCount = 0;
			m_hFleeCount = 0;
			m_hBackupTime = DateTime.Now;
			m_nScore = 0L;
			m_nWinCount = 0;
			m_nLostCount = 0;
			m_nDrawCount = 0;
			m_nFleeCount = 0;
			m_nBackupTime = DateTime.Now;
		}
	}
}

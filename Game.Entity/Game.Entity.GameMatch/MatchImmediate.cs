using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class MatchImmediate
	{
		public const string Tablename = "MatchImmediate";

		public const string _MatchID = "MatchID";

		public const string _MatchNo = "MatchNo";

		public const string _StartUserCount = "StartUserCount";

		public const string _AndroidUserCount = "AndroidUserCount";

		public const string _InitialBase = "InitialBase";

		public const string _InitialScore = "InitialScore";

		public const string _MinEnterGold = "MinEnterGold";

		public const string _PlayCount = "PlayCount";

		public const string _SwitchTableCount = "SwitchTableCount";

		public const string _PrecedeTimer = "PrecedeTimer";

		private int m_matchID;

		private short m_matchNo;

		private int m_startUserCount;

		private int m_androidUserCount;

		private int m_initialBase;

		private int m_initialScore;

		private int m_minEnterGold;

		private byte m_playCount;

		private byte m_switchTableCount;

		private int m_precedeTimer;

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

		public short MatchNo
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

		public int StartUserCount
		{
			get
			{
				return m_startUserCount;
			}
			set
			{
				m_startUserCount = value;
			}
		}

		public int AndroidUserCount
		{
			get
			{
				return m_androidUserCount;
			}
			set
			{
				m_androidUserCount = value;
			}
		}

		public int InitialBase
		{
			get
			{
				return m_initialBase;
			}
			set
			{
				m_initialBase = value;
			}
		}

		public int InitialScore
		{
			get
			{
				return m_initialScore;
			}
			set
			{
				m_initialScore = value;
			}
		}

		public int MinEnterGold
		{
			get
			{
				return m_minEnterGold;
			}
			set
			{
				m_minEnterGold = value;
			}
		}

		public byte PlayCount
		{
			get
			{
				return m_playCount;
			}
			set
			{
				m_playCount = value;
			}
		}

		public byte SwitchTableCount
		{
			get
			{
				return m_switchTableCount;
			}
			set
			{
				m_switchTableCount = value;
			}
		}

		public int PrecedeTimer
		{
			get
			{
				return m_precedeTimer;
			}
			set
			{
				m_precedeTimer = value;
			}
		}

		public MatchImmediate()
		{
			m_matchID = 0;
			m_matchNo = 0;
			m_startUserCount = 0;
			m_androidUserCount = 0;
			m_initialBase = 0;
			m_initialScore = 0;
			m_minEnterGold = 0;
			m_playCount = 0;
			m_switchTableCount = 0;
			m_precedeTimer = 0;
		}
	}
}

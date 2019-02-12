using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class MatchLockTime
	{
		public const string Tablename = "MatchLockTime";

		public const string _MatchID = "MatchID";

		public const string _MatchNo = "MatchNo";

		public const string _StartTime = "StartTime";

		public const string _EndTime = "EndTime";

		public const string _InitScore = "InitScore";

		public const string _CullScore = "CullScore";

		public const string _MinPlayCount = "MinPlayCount";

		private int m_matchID;

		private short m_matchNo;

		private DateTime m_startTime;

		private DateTime m_endTime;

		private long m_initScore;

		private long m_cullScore;

		private int m_minPlayCount;

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

		public DateTime StartTime
		{
			get
			{
				return m_startTime;
			}
			set
			{
				m_startTime = value;
			}
		}

		public DateTime EndTime
		{
			get
			{
				return m_endTime;
			}
			set
			{
				m_endTime = value;
			}
		}

		public long InitScore
		{
			get
			{
				return m_initScore;
			}
			set
			{
				m_initScore = value;
			}
		}

		public long CullScore
		{
			get
			{
				return m_cullScore;
			}
			set
			{
				m_cullScore = value;
			}
		}

		public int MinPlayCount
		{
			get
			{
				return m_minPlayCount;
			}
			set
			{
				m_minPlayCount = value;
			}
		}

		public MatchLockTime()
		{
			m_matchID = 0;
			m_matchNo = 0;
			m_startTime = DateTime.Now;
			m_endTime = DateTime.Now;
			m_initScore = 0L;
			m_cullScore = 0L;
			m_minPlayCount = 0;
		}
	}
}

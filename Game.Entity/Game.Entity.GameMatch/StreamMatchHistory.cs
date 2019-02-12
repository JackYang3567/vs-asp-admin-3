using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class StreamMatchHistory
	{
		public const string Tablename = "StreamMatchHistory";

		public const string _ID = "ID";

		public const string _UserID = "UserID";

		public const string _MatchID = "MatchID";

		public const string _MatchNo = "MatchNo";

		public const string _MatchType = "MatchType";

		public const string _ServerID = "ServerID";

		public const string _RankID = "RankID";

		public const string _MatchScore = "MatchScore";

		public const string _UserRight = "UserRight";

		public const string _RewardGold = "RewardGold";

		public const string _RewardIngot = "RewardIngot";

		public const string _RewardExperience = "RewardExperience";

		public const string _WinCount = "WinCount";

		public const string _LostCount = "LostCount";

		public const string _DrawCount = "DrawCount";

		public const string _FleeCount = "FleeCount";

		public const string _MatchStartTime = "MatchStartTime";

		public const string _MatchEndTime = "MatchEndTime";

		public const string _PlayTimeCount = "PlayTimeCount";

		public const string _OnlineTime = "OnlineTime";

		public const string _Machine = "Machine";

		public const string _ClientIP = "ClientIP";

		public const string _RecordDate = "RecordDate";

		private int m_iD;

		private int m_userID;

		private int m_matchID;

		private long m_matchNo;

		private byte m_matchType;

		private int m_serverID;

		private short m_rankID;

		private int m_matchScore;

		private int m_userRight;

		private long m_rewardGold;

		private long m_rewardIngot;

		private long m_rewardExperience;

		private int m_winCount;

		private int m_lostCount;

		private int m_drawCount;

		private int m_fleeCount;

		private DateTime m_matchStartTime;

		private DateTime m_matchEndTime;

		private int m_playTimeCount;

		private int m_onlineTime;

		private string m_machine;

		private string m_clientIP;

		private DateTime m_recordDate;

		public int ID
		{
			get
			{
				return m_iD;
			}
			set
			{
				m_iD = value;
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

		public long MatchNo
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

		public byte MatchType
		{
			get
			{
				return m_matchType;
			}
			set
			{
				m_matchType = value;
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

		public short RankID
		{
			get
			{
				return m_rankID;
			}
			set
			{
				m_rankID = value;
			}
		}

		public int MatchScore
		{
			get
			{
				return m_matchScore;
			}
			set
			{
				m_matchScore = value;
			}
		}

		public int UserRight
		{
			get
			{
				return m_userRight;
			}
			set
			{
				m_userRight = value;
			}
		}

		public long RewardGold
		{
			get
			{
				return m_rewardGold;
			}
			set
			{
				m_rewardGold = value;
			}
		}

		public long RewardIngot
		{
			get
			{
				return m_rewardIngot;
			}
			set
			{
				m_rewardIngot = value;
			}
		}

		public long RewardExperience
		{
			get
			{
				return m_rewardExperience;
			}
			set
			{
				m_rewardExperience = value;
			}
		}

		public int WinCount
		{
			get
			{
				return m_winCount;
			}
			set
			{
				m_winCount = value;
			}
		}

		public int LostCount
		{
			get
			{
				return m_lostCount;
			}
			set
			{
				m_lostCount = value;
			}
		}

		public int DrawCount
		{
			get
			{
				return m_drawCount;
			}
			set
			{
				m_drawCount = value;
			}
		}

		public int FleeCount
		{
			get
			{
				return m_fleeCount;
			}
			set
			{
				m_fleeCount = value;
			}
		}

		public DateTime MatchStartTime
		{
			get
			{
				return m_matchStartTime;
			}
			set
			{
				m_matchStartTime = value;
			}
		}

		public DateTime MatchEndTime
		{
			get
			{
				return m_matchEndTime;
			}
			set
			{
				m_matchEndTime = value;
			}
		}

		public int PlayTimeCount
		{
			get
			{
				return m_playTimeCount;
			}
			set
			{
				m_playTimeCount = value;
			}
		}

		public int OnlineTime
		{
			get
			{
				return m_onlineTime;
			}
			set
			{
				m_onlineTime = value;
			}
		}

		public string Machine
		{
			get
			{
				return m_machine;
			}
			set
			{
				m_machine = value;
			}
		}

		public string ClientIP
		{
			get
			{
				return m_clientIP;
			}
			set
			{
				m_clientIP = value;
			}
		}

		public DateTime RecordDate
		{
			get
			{
				return m_recordDate;
			}
			set
			{
				m_recordDate = value;
			}
		}

		public StreamMatchHistory()
		{
			m_iD = 0;
			m_userID = 0;
			m_matchID = 0;
			m_matchNo = 0L;
			m_matchType = 0;
			m_serverID = 0;
			m_rankID = 0;
			m_matchScore = 0;
			m_userRight = 0;
			m_rewardGold = 0L;
			m_rewardIngot = 0L;
			m_rewardExperience = 0L;
			m_winCount = 0;
			m_lostCount = 0;
			m_drawCount = 0;
			m_fleeCount = 0;
			m_matchStartTime = DateTime.Now;
			m_matchEndTime = DateTime.Now;
			m_playTimeCount = 0;
			m_onlineTime = 0;
			m_machine = "";
			m_clientIP = "";
			m_recordDate = DateTime.Now;
		}
	}
}

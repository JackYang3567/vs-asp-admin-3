using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class RecordAgentInfo
	{
		public const string Tablename = "RecordAgentInfo";

		public const string _RecordID = "RecordID";

		public const string _DateID = "DateID";

		public const string _UserID = "UserID";

		public const string _AgentScale = "AgentScale";

		public const string _PayBackScale = "PayBackScale";

		public const string _TypeID = "TypeID";

		public const string _PayScore = "PayScore";

		public const string _Score = "Score";

		public const string _ChildrenID = "ChildrenID";

		public const string _InsureScore = "InsureScore";

		public const string _CollectDate = "CollectDate";

		public const string _CollectIP = "CollectIP";

		public const string _CollectNote = "CollectNote";

		private int m_recordID;

		private int m_userID;

		private int m_dateID;

		private decimal m_agentScale;

		private decimal m_payBackScale;

		private int m_typeID;

		private long m_payScore;

		private long m_score;

		private int m_childrenID;

		private string m_insureScore;

		private DateTime m_collectDate;

		private string m_collectIP;

		private string m_collectNote;

		public int RecordID
		{
			get
			{
				return m_recordID;
			}
			set
			{
				m_recordID = value;
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

		public decimal AgentScale
		{
			get
			{
				return m_agentScale;
			}
			set
			{
				m_agentScale = value;
			}
		}

		public decimal PayBackScale
		{
			get
			{
				return m_payBackScale;
			}
			set
			{
				m_payBackScale = value;
			}
		}

		public int TypeID
		{
			get
			{
				return m_typeID;
			}
			set
			{
				m_typeID = value;
			}
		}

		public long PayScore
		{
			get
			{
				return m_payScore;
			}
			set
			{
				m_payScore = value;
			}
		}

		public long Score
		{
			get
			{
				return m_score;
			}
			set
			{
				m_score = value;
			}
		}

		public int ChildrenID
		{
			get
			{
				return m_childrenID;
			}
			set
			{
				m_childrenID = value;
			}
		}

		public string InsureScore
		{
			get
			{
				return m_insureScore;
			}
			set
			{
				m_insureScore = value;
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

		public string CollectIP
		{
			get
			{
				return m_collectIP;
			}
			set
			{
				m_collectIP = value;
			}
		}

		public string CollectNote
		{
			get
			{
				return m_collectNote;
			}
			set
			{
				m_collectNote = value;
			}
		}

		public RecordAgentInfo()
		{
			m_recordID = 0;
			m_userID = 0;
			m_dateID = 0;
			m_agentScale = 0m;
			m_payBackScale = 0m;
			m_typeID = 0;
			m_payScore = 0L;
			m_score = 0L;
			m_childrenID = 0;
			m_insureScore = "";
			m_collectDate = DateTime.Now;
			m_collectIP = "";
			m_collectNote = "";
		}
	}
}

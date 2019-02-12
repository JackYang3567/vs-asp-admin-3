using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class RecordInsure
	{
		public const string Tablename = "RecordInsure";

		public const string _RecordID = "RecordID";

		public const string _KindID = "KindID";

		public const string _ServerID = "ServerID";

		public const string _SourceUserID = "SourceUserID";

		public const string _SourceGold = "SourceGold";

		public const string _SourceBank = "SourceBank";

		public const string _TargetUserID = "TargetUserID";

		public const string _TargetGold = "TargetGold";

		public const string _TargetBank = "TargetBank";

		public const string _SwapScore = "SwapScore";

		public const string _Revenue = "Revenue";

		public const string _IsGamePlaza = "IsGamePlaza";

		public const string _TradeType = "TradeType";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		public const string _CollectNote = "CollectNote";

		private int m_recordID;

		private int m_kindID;

		private int m_serverID;

		private int m_sourceUserID;

		private long m_sourceGold;

		private long m_sourceBank;

		private int m_targetUserID;

		private long m_targetGold;

		private long m_targetBank;

		private long m_swapScore;

		private long m_revenue;

		private byte m_isGamePlaza;

		private byte m_tradeType;

		private string m_clientIP;

		private DateTime m_collectDate;

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

		public int SourceUserID
		{
			get
			{
				return m_sourceUserID;
			}
			set
			{
				m_sourceUserID = value;
			}
		}

		public long SourceGold
		{
			get
			{
				return m_sourceGold;
			}
			set
			{
				m_sourceGold = value;
			}
		}

		public long SourceBank
		{
			get
			{
				return m_sourceBank;
			}
			set
			{
				m_sourceBank = value;
			}
		}

		public int TargetUserID
		{
			get
			{
				return m_targetUserID;
			}
			set
			{
				m_targetUserID = value;
			}
		}

		public long TargetGold
		{
			get
			{
				return m_targetGold;
			}
			set
			{
				m_targetGold = value;
			}
		}

		public long TargetBank
		{
			get
			{
				return m_targetBank;
			}
			set
			{
				m_targetBank = value;
			}
		}

		public long SwapScore
		{
			get
			{
				return m_swapScore;
			}
			set
			{
				m_swapScore = value;
			}
		}

		public long Revenue
		{
			get
			{
				return m_revenue;
			}
			set
			{
				m_revenue = value;
			}
		}

		public byte IsGamePlaza
		{
			get
			{
				return m_isGamePlaza;
			}
			set
			{
				m_isGamePlaza = value;
			}
		}

		public byte TradeType
		{
			get
			{
				return m_tradeType;
			}
			set
			{
				m_tradeType = value;
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

		public RecordInsure()
		{
			m_recordID = 0;
			m_kindID = 0;
			m_serverID = 0;
			m_sourceUserID = 0;
			m_sourceGold = 0L;
			m_sourceBank = 0L;
			m_targetUserID = 0;
			m_targetGold = 0L;
			m_targetBank = 0L;
			m_swapScore = 0L;
			m_revenue = 0L;
			m_isGamePlaza = 0;
			m_tradeType = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
			m_collectNote = "";
		}
	}
}

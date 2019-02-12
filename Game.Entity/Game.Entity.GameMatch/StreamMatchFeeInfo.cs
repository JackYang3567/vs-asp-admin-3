using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class StreamMatchFeeInfo
	{
		public const string Tablename = "StreamMatchFeeInfo";

		public const string _RecordID = "RecordID";

		public const string _UserID = "UserID";

		public const string _ServerID = "ServerID";

		public const string _MatchID = "MatchID";

		public const string _MatchNo = "MatchNo";

		public const string _Fee = "Fee";

		public const string _CollectDate = "CollectDate";

		private int m_recordID;

		private int m_userID;

		private int m_serverID;

		private int m_matchID;

		private int m_matchNo;

		private int m_fee;

		private DateTime m_collectDate;

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

		public int Fee
		{
			get
			{
				return m_fee;
			}
			set
			{
				m_fee = value;
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

		public StreamMatchFeeInfo()
		{
			m_recordID = 0;
			m_userID = 0;
			m_serverID = 0;
			m_matchID = 0;
			m_matchNo = 0;
			m_fee = 0;
			m_collectDate = DateTime.Now;
		}
	}
}

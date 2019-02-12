using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordConvertPresent
	{
		public const string Tablename = "RecordConvertPresent";

		public const string _RecordID = "RecordID";

		public const string _UserID = "UserID";

		public const string _KindID = "KindID";

		public const string _ServerID = "ServerID";

		public const string _CurInsureScore = "CurInsureScore";

		public const string _CurPresent = "CurPresent";

		public const string _ConvertPresent = "ConvertPresent";

		public const string _ConvertRate = "ConvertRate";

		public const string _IsGamePlaza = "IsGamePlaza";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		private int m_recordID;

		private int m_userID;

		private int m_kindID;

		private int m_serverID;

		private long m_curInsureScore;

		private int m_curPresent;

		private int m_convertPresent;

		private int m_convertRate;

		private byte m_isGamePlaza;

		private string m_clientIP;

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

		public long CurInsureScore
		{
			get
			{
				return m_curInsureScore;
			}
			set
			{
				m_curInsureScore = value;
			}
		}

		public int CurPresent
		{
			get
			{
				return m_curPresent;
			}
			set
			{
				m_curPresent = value;
			}
		}

		public int ConvertPresent
		{
			get
			{
				return m_convertPresent;
			}
			set
			{
				m_convertPresent = value;
			}
		}

		public int ConvertRate
		{
			get
			{
				return m_convertRate;
			}
			set
			{
				m_convertRate = value;
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

		public RecordConvertPresent()
		{
			m_recordID = 0;
			m_userID = 0;
			m_kindID = 0;
			m_serverID = 0;
			m_curInsureScore = 0L;
			m_curPresent = 0;
			m_convertPresent = 0;
			m_convertRate = 0;
			m_isGamePlaza = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
		}
	}
}

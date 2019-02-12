using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordEncashPresent
	{
		public const string Tablename = "RecordEncashPresent";

		public const string _UserID = "UserID";

		public const string _CurGold = "CurGold";

		public const string _CurPresent = "CurPresent";

		public const string _EncashGold = "EncashGold";

		public const string _EncashPresent = "EncashPresent";

		public const string _KindID = "KindID";

		public const string _ServerID = "ServerID";

		public const string _ClientIP = "ClientIP";

		public const string _EncashTime = "EncashTime";

		private int m_userID;

		private long m_curGold;

		private int m_curPresent;

		private int m_encashGold;

		private int m_encashPresent;

		private int m_kindID;

		private int m_serverID;

		private string m_clientIP;

		private DateTime m_encashTime;

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

		public long CurGold
		{
			get
			{
				return m_curGold;
			}
			set
			{
				m_curGold = value;
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

		public int EncashGold
		{
			get
			{
				return m_encashGold;
			}
			set
			{
				m_encashGold = value;
			}
		}

		public int EncashPresent
		{
			get
			{
				return m_encashPresent;
			}
			set
			{
				m_encashPresent = value;
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

		public DateTime EncashTime
		{
			get
			{
				return m_encashTime;
			}
			set
			{
				m_encashTime = value;
			}
		}

		public RecordEncashPresent()
		{
			m_userID = 0;
			m_curGold = 0L;
			m_curPresent = 0;
			m_encashGold = 0;
			m_encashPresent = 0;
			m_kindID = 0;
			m_serverID = 0;
			m_clientIP = "";
			m_encashTime = DateTime.Now;
		}
	}
}

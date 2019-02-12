using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordGrantTreasure
	{
		public const string Tablename = "RecordGrantTreasure";

		public const string _RecordID = "RecordID";

		public const string _MasterID = "MasterID";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		public const string _UserID = "UserID";

		public const string _CurGold = "CurGold";

		public const string _AddGold = "AddGold";

		public const string _Reason = "Reason";

		private int m_recordID;

		private int m_masterID;

		private string m_clientIP;

		private DateTime m_collectDate;

		private int m_userID;

		private long m_curGold;

		private long m_addGold;

		private string m_reason;

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

		public int MasterID
		{
			get
			{
				return m_masterID;
			}
			set
			{
				m_masterID = value;
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

		public long AddGold
		{
			get
			{
				return m_addGold;
			}
			set
			{
				m_addGold = value;
			}
		}

		public string Reason
		{
			get
			{
				return m_reason;
			}
			set
			{
				m_reason = value;
			}
		}

		public RecordGrantTreasure()
		{
			m_recordID = 0;
			m_masterID = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
			m_userID = 0;
			m_curGold = 0L;
			m_addGold = 0L;
			m_reason = "";
		}
	}
}

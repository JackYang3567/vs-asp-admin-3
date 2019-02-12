using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordGrantGameScore
	{
		public const string Tablename = "RecordGrantGameScore";

		public const string _RecordID = "RecordID";

		public const string _MasterID = "MasterID";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		public const string _UserID = "UserID";

		public const string _KindID = "KindID";

		public const string _CurScore = "CurScore";

		public const string _AddScore = "AddScore";

		public const string _Reason = "Reason";

		private int m_recordID;

		private int m_masterID;

		private string m_clientIP;

		private DateTime m_collectDate;

		private int m_userID;

		private int m_kindID;

		private long m_curScore;

		private int m_addScore;

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

		public long CurScore
		{
			get
			{
				return m_curScore;
			}
			set
			{
				m_curScore = value;
			}
		}

		public int AddScore
		{
			get
			{
				return m_addScore;
			}
			set
			{
				m_addScore = value;
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

		public RecordGrantGameScore()
		{
			m_recordID = 0;
			m_masterID = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
			m_userID = 0;
			m_kindID = 0;
			m_curScore = 0L;
			m_addScore = 0;
			m_reason = "";
		}
	}
}

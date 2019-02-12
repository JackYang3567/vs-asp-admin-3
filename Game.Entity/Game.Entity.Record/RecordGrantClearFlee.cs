using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordGrantClearFlee
	{
		public const string Tablename = "RecordGrantClearFlee";

		public const string _RecordID = "RecordID";

		public const string _MasterID = "MasterID";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		public const string _UserID = "UserID";

		public const string _KindID = "KindID";

		public const string _CurFlee = "CurFlee";

		public const string _Reason = "Reason";

		private int m_recordID;

		private int m_masterID;

		private string m_clientIP;

		private DateTime m_collectDate;

		private int m_userID;

		private int m_kindID;

		private int m_curFlee;

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

		public int CurFlee
		{
			get
			{
				return m_curFlee;
			}
			set
			{
				m_curFlee = value;
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

		public RecordGrantClearFlee()
		{
			m_recordID = 0;
			m_masterID = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
			m_userID = 0;
			m_kindID = 0;
			m_curFlee = 0;
			m_reason = "";
		}
	}
}

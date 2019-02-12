using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordGrantGameID
	{
		public const string Tablename = "RecordGrantGameID";

		public const string _RecordID = "RecordID";

		public const string _MasterID = "MasterID";

		public const string _UserID = "UserID";

		public const string _CurGameID = "CurGameID";

		public const string _ReGameID = "ReGameID";

		public const string _IDLevel = "IDLevel";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		public const string _Reason = "Reason";

		private int m_recordID;

		private int m_masterID;

		private int m_userID;

		private int m_curGameID;

		private int m_reGameID;

		private int m_iDLevel;

		private string m_clientIP;

		private DateTime m_collectDate;

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

		public int CurGameID
		{
			get
			{
				return m_curGameID;
			}
			set
			{
				m_curGameID = value;
			}
		}

		public int ReGameID
		{
			get
			{
				return m_reGameID;
			}
			set
			{
				m_reGameID = value;
			}
		}

		public int IDLevel
		{
			get
			{
				return m_iDLevel;
			}
			set
			{
				m_iDLevel = value;
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

		public RecordGrantGameID()
		{
			m_recordID = 0;
			m_masterID = 0;
			m_userID = 0;
			m_curGameID = 0;
			m_reGameID = 0;
			m_iDLevel = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
			m_reason = "";
		}
	}
}

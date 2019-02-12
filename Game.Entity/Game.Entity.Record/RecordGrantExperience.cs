using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordGrantExperience
	{
		public const string Tablename = "RecordGrantExperience";

		public const string _RecordID = "RecordID";

		public const string _MasterID = "MasterID";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		public const string _UserID = "UserID";

		public const string _CurExperience = "CurExperience";

		public const string _AddExperience = "AddExperience";

		public const string _Reason = "Reason";

		private int m_recordID;

		private int m_masterID;

		private string m_clientIP;

		private DateTime m_collectDate;

		private int m_userID;

		private int m_curExperience;

		private int m_addExperience;

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

		public int CurExperience
		{
			get
			{
				return m_curExperience;
			}
			set
			{
				m_curExperience = value;
			}
		}

		public int AddExperience
		{
			get
			{
				return m_addExperience;
			}
			set
			{
				m_addExperience = value;
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

		public RecordGrantExperience()
		{
			m_recordID = 0;
			m_masterID = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
			m_userID = 0;
			m_curExperience = 0;
			m_addExperience = 0;
			m_reason = "";
		}
	}
}

using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordGrantMember
	{
		public const string Tablename = "RecordGrantMember";

		public const string _RecordID = "RecordID";

		public const string _MasterID = "MasterID";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		public const string _UserID = "UserID";

		public const string _GrantCardType = "GrantCardType";

		public const string _Reason = "Reason";

		public const string _MemberDays = "MemberDays";

		private int m_recordID;

		private int m_masterID;

		private string m_clientIP;

		private DateTime m_collectDate;

		private int m_userID;

		private int m_grantCardType;

		private string m_reason;

		private int m_memberDays;

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

		public int GrantCardType
		{
			get
			{
				return m_grantCardType;
			}
			set
			{
				m_grantCardType = value;
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

		public int MemberDays
		{
			get
			{
				return m_memberDays;
			}
			set
			{
				m_memberDays = value;
			}
		}

		public RecordGrantMember()
		{
			m_recordID = 0;
			m_masterID = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
			m_userID = 0;
			m_grantCardType = 0;
			m_reason = "";
			m_memberDays = 0;
		}
	}
}

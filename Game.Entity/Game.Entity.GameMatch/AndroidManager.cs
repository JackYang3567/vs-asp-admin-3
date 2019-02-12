using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class AndroidManager
	{
		public const string Tablename = "AndroidManager";

		public const string _UserID = "UserID";

		public const string _ServerID = "ServerID";

		public const string _MinPlayDraw = "MinPlayDraw";

		public const string _MaxPlayDraw = "MaxPlayDraw";

		public const string _MinTakeScore = "MinTakeScore";

		public const string _MaxTakeScore = "MaxTakeScore";

		public const string _MinReposeTime = "MinReposeTime";

		public const string _MaxReposeTime = "MaxReposeTime";

		public const string _ServiceTime = "ServiceTime";

		public const string _ServiceGender = "ServiceGender";

		public const string _Nullity = "Nullity";

		public const string _CreateDate = "CreateDate";

		public const string _AndroidNote = "AndroidNote";

		private int m_userID;

		private int m_serverID;

		private int m_minPlayDraw;

		private int m_maxPlayDraw;

		private long m_minTakeScore;

		private long m_maxTakeScore;

		private int m_minReposeTime;

		private int m_maxReposeTime;

		private int m_serviceTime;

		private int m_serviceGender;

		private byte m_nullity;

		private DateTime m_createDate;

		private string m_androidNote;

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

		public int MinPlayDraw
		{
			get
			{
				return m_minPlayDraw;
			}
			set
			{
				m_minPlayDraw = value;
			}
		}

		public int MaxPlayDraw
		{
			get
			{
				return m_maxPlayDraw;
			}
			set
			{
				m_maxPlayDraw = value;
			}
		}

		public long MinTakeScore
		{
			get
			{
				return m_minTakeScore;
			}
			set
			{
				m_minTakeScore = value;
			}
		}

		public long MaxTakeScore
		{
			get
			{
				return m_maxTakeScore;
			}
			set
			{
				m_maxTakeScore = value;
			}
		}

		public int MinReposeTime
		{
			get
			{
				return m_minReposeTime;
			}
			set
			{
				m_minReposeTime = value;
			}
		}

		public int MaxReposeTime
		{
			get
			{
				return m_maxReposeTime;
			}
			set
			{
				m_maxReposeTime = value;
			}
		}

		public int ServiceTime
		{
			get
			{
				return m_serviceTime;
			}
			set
			{
				m_serviceTime = value;
			}
		}

		public int ServiceGender
		{
			get
			{
				return m_serviceGender;
			}
			set
			{
				m_serviceGender = value;
			}
		}

		public byte Nullity
		{
			get
			{
				return m_nullity;
			}
			set
			{
				m_nullity = value;
			}
		}

		public DateTime CreateDate
		{
			get
			{
				return m_createDate;
			}
			set
			{
				m_createDate = value;
			}
		}

		public string AndroidNote
		{
			get
			{
				return m_androidNote;
			}
			set
			{
				m_androidNote = value;
			}
		}

		public AndroidManager()
		{
			m_userID = 0;
			m_serverID = 0;
			m_minPlayDraw = 0;
			m_maxPlayDraw = 0;
			m_minTakeScore = 0L;
			m_maxTakeScore = 0L;
			m_minReposeTime = 0;
			m_maxReposeTime = 0;
			m_serviceTime = 0;
			m_serviceGender = 0;
			m_nullity = 0;
			m_createDate = DateTime.Now;
			m_androidNote = "";
		}
	}
}

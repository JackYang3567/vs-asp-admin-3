using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class SystemMessage
	{
		public const string Tablename = "SystemMessage";

		public const string _ID = "ID";

		public const string _MessageType = "MessageType";

		public const string _ServerRange = "ServerRange";

		public const string _MessageString = "MessageString";

		public const string _StartTime = "StartTime";

		public const string _ConcludeTime = "ConcludeTime";

		public const string _TimeRate = "TimeRate";

		public const string _Nullity = "Nullity";

		public const string _CreateDate = "CreateDate";

		public const string _CreateMasterID = "CreateMasterID";

		public const string _UpdateDate = "UpdateDate";

		public const string _UpdateMasterID = "UpdateMasterID";

		public const string _UpdateCount = "UpdateCount";

		public const string _CollectNote = "CollectNote";

		private int m_iD;

		private int m_messageType;

		private string m_serverRange;

		private string m_messageString;

		private DateTime m_startTime;

		private DateTime m_concludeTime;

		private int m_timeRate;

		private byte m_nullity;

		private DateTime m_createDate;

		private int m_createMasterID;

		private DateTime m_updateDate;

		private int m_updateMasterID;

		private int m_updateCount;

		private string m_collectNote;

		public int ID
		{
			get
			{
				return m_iD;
			}
			set
			{
				m_iD = value;
			}
		}

		public int MessageType
		{
			get
			{
				return m_messageType;
			}
			set
			{
				m_messageType = value;
			}
		}

		public string ServerRange
		{
			get
			{
				return m_serverRange;
			}
			set
			{
				m_serverRange = value;
			}
		}

		public string MessageString
		{
			get
			{
				return m_messageString;
			}
			set
			{
				m_messageString = value;
			}
		}

		public DateTime StartTime
		{
			get
			{
				return m_startTime;
			}
			set
			{
				m_startTime = value;
			}
		}

		public DateTime ConcludeTime
		{
			get
			{
				return m_concludeTime;
			}
			set
			{
				m_concludeTime = value;
			}
		}

		public int TimeRate
		{
			get
			{
				return m_timeRate;
			}
			set
			{
				m_timeRate = value;
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

		public int CreateMasterID
		{
			get
			{
				return m_createMasterID;
			}
			set
			{
				m_createMasterID = value;
			}
		}

		public DateTime UpdateDate
		{
			get
			{
				return m_updateDate;
			}
			set
			{
				m_updateDate = value;
			}
		}

		public int UpdateMasterID
		{
			get
			{
				return m_updateMasterID;
			}
			set
			{
				m_updateMasterID = value;
			}
		}

		public int UpdateCount
		{
			get
			{
				return m_updateCount;
			}
			set
			{
				m_updateCount = value;
			}
		}

		public string CollectNote
		{
			get
			{
				return m_collectNote;
			}
			set
			{
				m_collectNote = value;
			}
		}

		public SystemMessage()
		{
			m_iD = 0;
			m_messageType = 0;
			m_serverRange = "";
			m_messageString = "";
			m_startTime = DateTime.Now;
			m_concludeTime = DateTime.Now;
			m_timeRate = 0;
			m_nullity = 0;
			m_createDate = DateTime.Now;
			m_createMasterID = 0;
			m_updateDate = DateTime.Now;
			m_updateMasterID = 0;
			m_updateCount = 0;
			m_collectNote = "";
		}
	}
}

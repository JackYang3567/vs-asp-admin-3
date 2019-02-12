using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class OnLineStatusInfo
	{
		public const string Tablename = "OnLineStatusInfo";

		public const string _KindID = "KindID";

		public const string _ServerID = "ServerID";

		public const string _OnLineCount = "OnLineCount";

		public const string _InsertDateTime = "InsertDateTime";

		public const string _ModifyDateTime = "ModifyDateTime";

		private int m_kindID;

		private int m_serverID;

		private int m_onLineCount;

		private DateTime m_insertDateTime;

		private DateTime m_modifyDateTime;

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

		public int OnLineCount
		{
			get
			{
				return m_onLineCount;
			}
			set
			{
				m_onLineCount = value;
			}
		}

		public DateTime InsertDateTime
		{
			get
			{
				return m_insertDateTime;
			}
			set
			{
				m_insertDateTime = value;
			}
		}

		public DateTime ModifyDateTime
		{
			get
			{
				return m_modifyDateTime;
			}
			set
			{
				m_modifyDateTime = value;
			}
		}

		public OnLineStatusInfo()
		{
			m_kindID = 0;
			m_serverID = 0;
			m_onLineCount = 0;
			m_insertDateTime = DateTime.Now;
			m_modifyDateTime = DateTime.Now;
		}
	}
}

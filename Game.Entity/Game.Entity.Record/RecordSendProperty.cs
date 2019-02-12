using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordSendProperty
	{
		public const string Tablename = "RecordSendProperty";

		public const string _PropID = "PropID";

		public const string _SourceUserID = "SourceUserID";

		public const string _TargetUserID = "TargetUserID";

		public const string _PropPrice = "PropPrice";

		public const string _PropCount = "PropCount";

		public const string _KindID = "KindID";

		public const string _ServerID = "ServerID";

		public const string _SendTime = "SendTime";

		public const string _ClientIP = "ClientIP";

		private byte m_propID;

		private int m_sourceUserID;

		private int m_targetUserID;

		private int m_propPrice;

		private int m_propCount;

		private int m_kindID;

		private int m_serverID;

		private DateTime m_sendTime;

		private string m_clientIP;

		public byte PropID
		{
			get
			{
				return m_propID;
			}
			set
			{
				m_propID = value;
			}
		}

		public int SourceUserID
		{
			get
			{
				return m_sourceUserID;
			}
			set
			{
				m_sourceUserID = value;
			}
		}

		public int TargetUserID
		{
			get
			{
				return m_targetUserID;
			}
			set
			{
				m_targetUserID = value;
			}
		}

		public int PropPrice
		{
			get
			{
				return m_propPrice;
			}
			set
			{
				m_propPrice = value;
			}
		}

		public int PropCount
		{
			get
			{
				return m_propCount;
			}
			set
			{
				m_propCount = value;
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

		public DateTime SendTime
		{
			get
			{
				return m_sendTime;
			}
			set
			{
				m_sendTime = value;
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

		public RecordSendProperty()
		{
			m_propID = 0;
			m_sourceUserID = 0;
			m_targetUserID = 0;
			m_propPrice = 0;
			m_propCount = 0;
			m_kindID = 0;
			m_serverID = 0;
			m_sendTime = DateTime.Now;
			m_clientIP = "";
		}
	}
}

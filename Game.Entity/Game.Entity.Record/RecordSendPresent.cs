using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordSendPresent
	{
		public const string Tablename = "RecordSendPresent";

		public const string _PresentID = "PresentID";

		public const string _RcvUserID = "RcvUserID";

		public const string _SendUserID = "SendUserID";

		public const string _LovelinessRcv = "LovelinessRcv";

		public const string _LovelinessSend = "LovelinessSend";

		public const string _PresentPrice = "PresentPrice";

		public const string _PresentCount = "PresentCount";

		public const string _KindID = "KindID";

		public const string _ServerID = "ServerID";

		public const string _SendTime = "SendTime";

		public const string _ClientIP = "ClientIP";

		private byte m_presentID;

		private int m_rcvUserID;

		private int m_sendUserID;

		private int m_lovelinessRcv;

		private int m_lovelinessSend;

		private int m_presentPrice;

		private int m_presentCount;

		private int m_kindID;

		private int m_serverID;

		private DateTime m_sendTime;

		private string m_clientIP;

		public byte PresentID
		{
			get
			{
				return m_presentID;
			}
			set
			{
				m_presentID = value;
			}
		}

		public int RcvUserID
		{
			get
			{
				return m_rcvUserID;
			}
			set
			{
				m_rcvUserID = value;
			}
		}

		public int SendUserID
		{
			get
			{
				return m_sendUserID;
			}
			set
			{
				m_sendUserID = value;
			}
		}

		public int LovelinessRcv
		{
			get
			{
				return m_lovelinessRcv;
			}
			set
			{
				m_lovelinessRcv = value;
			}
		}

		public int LovelinessSend
		{
			get
			{
				return m_lovelinessSend;
			}
			set
			{
				m_lovelinessSend = value;
			}
		}

		public int PresentPrice
		{
			get
			{
				return m_presentPrice;
			}
			set
			{
				m_presentPrice = value;
			}
		}

		public int PresentCount
		{
			get
			{
				return m_presentCount;
			}
			set
			{
				m_presentCount = value;
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

		public RecordSendPresent()
		{
			m_presentID = 0;
			m_rcvUserID = 0;
			m_sendUserID = 0;
			m_lovelinessRcv = 0;
			m_lovelinessSend = 0;
			m_presentPrice = 0;
			m_presentCount = 0;
			m_kindID = 0;
			m_serverID = 0;
			m_sendTime = DateTime.Now;
			m_clientIP = "";
		}
	}
}

using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordPasswdExpend
	{
		public const string Tablename = "RecordPasswdExpend";

		public const string _RecordID = "RecordID";

		public const string _OperMasterID = "OperMasterID";

		public const string _UserID = "UserID";

		public const string _ReLogonPasswd = "ReLogonPasswd";

		public const string _ReInsurePasswd = "ReInsurePasswd";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		private int m_recordID;

		private int m_operMasterID;

		private int m_userID;

		private string m_reLogonPasswd;

		private string m_reInsurePasswd;

		private string m_clientIP;

		private DateTime m_collectDate;

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

		public int OperMasterID
		{
			get
			{
				return m_operMasterID;
			}
			set
			{
				m_operMasterID = value;
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

		public string ReLogonPasswd
		{
			get
			{
				return m_reLogonPasswd;
			}
			set
			{
				m_reLogonPasswd = value;
			}
		}

		public string ReInsurePasswd
		{
			get
			{
				return m_reInsurePasswd;
			}
			set
			{
				m_reInsurePasswd = value;
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

		public RecordPasswdExpend()
		{
			m_recordID = 0;
			m_operMasterID = 0;
			m_userID = 0;
			m_reLogonPasswd = "";
			m_reInsurePasswd = "";
			m_clientIP = "";
			m_collectDate = DateTime.Now;
		}
	}
}

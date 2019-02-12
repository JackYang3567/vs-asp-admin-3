using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordAccountsExpend
	{
		public const string Tablename = "RecordAccountsExpend";

		public const string _RecordID = "RecordID";

		public const string _OperMasterID = "OperMasterID";

		public const string _UserID = "UserID";

		public const string _ReAccounts = "ReAccounts";

		public const string _Type = "Type";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		private int m_recordID;

		private int m_operMasterID;

		private int m_userID;

		private string m_reAccounts;

		private byte m_type;

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

		public string ReAccounts
		{
			get
			{
				return m_reAccounts;
			}
			set
			{
				m_reAccounts = value;
			}
		}

		public byte Type
		{
			get
			{
				return m_type;
			}
			set
			{
				m_type = value;
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

		public RecordAccountsExpend()
		{
			m_recordID = 0;
			m_operMasterID = 0;
			m_userID = 0;
			m_reAccounts = "";
			m_type = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
		}
	}
}

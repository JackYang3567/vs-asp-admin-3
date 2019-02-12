using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class SystemSecurity
	{
		public const string Tablename = "SystemSecurity";

		public const string _RecordID = "RecordID";

		public const string _OperatingTime = "OperatingTime";

		public const string _OperatingName = "OperatingName";

		public const string _OperatingIP = "OperatingIP";

		public const string _OperatingAccounts = "OperatingAccounts";

		private int m_recordID;

		private DateTime m_operatingTime;

		private string m_operatingName;

		private string m_operatingIP;

		private string m_operatingAccounts;

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

		public DateTime OperatingTime
		{
			get
			{
				return m_operatingTime;
			}
			set
			{
				m_operatingTime = value;
			}
		}

		public string OperatingName
		{
			get
			{
				return m_operatingName;
			}
			set
			{
				m_operatingName = value;
			}
		}

		public string OperatingIP
		{
			get
			{
				return m_operatingIP;
			}
			set
			{
				m_operatingIP = value;
			}
		}

		public string OperatingAccounts
		{
			get
			{
				return m_operatingAccounts;
			}
			set
			{
				m_operatingAccounts = value;
			}
		}

		public string Remark
		{
			get;
			set;
		}

		public SystemSecurity()
		{
			m_recordID = 0;
			m_operatingTime = DateTime.Now;
			m_operatingName = "";
			m_operatingIP = "";
			m_operatingAccounts = "";
		}
	}
}

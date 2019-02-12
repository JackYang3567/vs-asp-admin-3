using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class DataBaseInfo
	{
		public const string Tablename = "DataBaseInfo";

		public const string _DBInfoID = "DBInfoID";

		public const string _DBAddr = "DBAddr";

		public const string _DBPort = "DBPort";

		public const string _DBUser = "DBUser";

		public const string _DBPassword = "DBPassword";

		public const string _MachineID = "MachineID";

		public const string _Information = "Information";

		private int m_dBInfoID;

		private string m_dBAddr;

		private int m_dBPort;

		private string m_dBUser;

		private string m_dBPassword;

		private string m_machineID;

		private string m_information;

		public int DBInfoID
		{
			get
			{
				return m_dBInfoID;
			}
			set
			{
				m_dBInfoID = value;
			}
		}

		public string DBAddr
		{
			get
			{
				return m_dBAddr;
			}
			set
			{
				m_dBAddr = value;
			}
		}

		public int DBPort
		{
			get
			{
				return m_dBPort;
			}
			set
			{
				m_dBPort = value;
			}
		}

		public string DBUser
		{
			get
			{
				return m_dBUser;
			}
			set
			{
				m_dBUser = value;
			}
		}

		public string DBPassword
		{
			get
			{
				return m_dBPassword;
			}
			set
			{
				m_dBPassword = value;
			}
		}

		public string MachineID
		{
			get
			{
				return m_machineID;
			}
			set
			{
				m_machineID = value;
			}
		}

		public string Information
		{
			get
			{
				return m_information;
			}
			set
			{
				m_information = value;
			}
		}

		public DataBaseInfo()
		{
			m_dBInfoID = 0;
			m_dBAddr = "";
			m_dBPort = 1433;
			m_dBUser = "";
			m_dBPassword = "";
			m_machineID = "";
			m_information = "";
		}
	}
}

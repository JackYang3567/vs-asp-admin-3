using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class SystemGrantCount
	{
		public const string Tablename = "SystemGrantCount";

		public const string _DateID = "DateID";

		public const string _RegisterIP = "RegisterIP";

		public const string _RegisterMachine = "RegisterMachine";

		public const string _GrantScore = "GrantScore";

		public const string _GrantCount = "GrantCount";

		public const string _CollectDate = "CollectDate";

		private int m_dateID;

		private string m_registerIP;

		private string m_registerMachine;

		private long m_grantScore;

		private long m_grantCount;

		private DateTime m_collectDate;

		public int DateID
		{
			get
			{
				return m_dateID;
			}
			set
			{
				m_dateID = value;
			}
		}

		public string RegisterIP
		{
			get
			{
				return m_registerIP;
			}
			set
			{
				m_registerIP = value;
			}
		}

		public string RegisterMachine
		{
			get
			{
				return m_registerMachine;
			}
			set
			{
				m_registerMachine = value;
			}
		}

		public long GrantScore
		{
			get
			{
				return m_grantScore;
			}
			set
			{
				m_grantScore = value;
			}
		}

		public long GrantCount
		{
			get
			{
				return m_grantCount;
			}
			set
			{
				m_grantCount = value;
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

		public SystemGrantCount()
		{
			m_dateID = 0;
			m_registerIP = "";
			m_registerMachine = "";
			m_grantScore = 0L;
			m_grantCount = 0L;
			m_collectDate = DateTime.Now;
		}
	}
}

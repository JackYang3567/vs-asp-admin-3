using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class AccountsAgent
	{
		public const string Tablename = "AccountsAgent";

		public const string _AgentID = "AgentID";

		public const string _UserID = "UserID";

		public const string _Compellation = "Compellation";

		public const string _Domain = "Domain";

		public const string _AgentType = "AgentType";

		public const string _AgentScale = "AgentScale";

		public const string _PayBackScore = "PayBackScore";

		public const string _PayBackScale = "PayBackScale";

		public const string _MobilePhone = "MobilePhone";

		public const string _EMail = "EMail";

		public const string _DwellingPlace = "DwellingPlace";

		public const string _Nullity = "Nullity";

		public const string _AgentNote = "AgentNote";

		public const string _CollectDate = "CollectDate";

		private int m_agentID;

		private int m_userID;

		private string m_compellation;

		private string m_domain;

		private int m_agentType;

		private decimal m_agentScale;

		private long m_payBackScore;

		private decimal m_payBackScale;

		private string m_mobilePhone;

		private string m_eMail;

		private string m_dwellingPlace;

		private byte m_nullity;

		private string m_agentNote;

		private DateTime m_collectDate;

		public int AgentID
		{
			get
			{
				return m_agentID;
			}
			set
			{
				m_agentID = value;
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

		public string Compellation
		{
			get
			{
				return m_compellation;
			}
			set
			{
				m_compellation = value;
			}
		}

		public string Domain
		{
			get
			{
				return m_domain;
			}
			set
			{
				m_domain = value;
			}
		}

		public int AgentType
		{
			get
			{
				return m_agentType;
			}
			set
			{
				m_agentType = value;
			}
		}

		public decimal AgentScale
		{
			get
			{
				return m_agentScale;
			}
			set
			{
				m_agentScale = value;
			}
		}

		public long PayBackScore
		{
			get
			{
				return m_payBackScore;
			}
			set
			{
				m_payBackScore = value;
			}
		}

		public decimal PayBackScale
		{
			get
			{
				return m_payBackScale;
			}
			set
			{
				m_payBackScale = value;
			}
		}

		public string MobilePhone
		{
			get
			{
				return m_mobilePhone;
			}
			set
			{
				m_mobilePhone = value;
			}
		}

		public string EMail
		{
			get
			{
				return m_eMail;
			}
			set
			{
				m_eMail = value;
			}
		}

		public string DwellingPlace
		{
			get
			{
				return m_dwellingPlace;
			}
			set
			{
				m_dwellingPlace = value;
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

		public string AgentNote
		{
			get
			{
				return m_agentNote;
			}
			set
			{
				m_agentNote = value;
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

		public AccountsAgent()
		{
			m_agentID = 0;
			m_userID = 0;
			m_compellation = "";
			m_domain = "";
			m_agentType = 0;
			m_agentScale = 0m;
			m_payBackScore = 0L;
			m_payBackScale = 0m;
			m_mobilePhone = "";
			m_eMail = "";
			m_dwellingPlace = "";
			m_nullity = 0;
			m_agentNote = "";
			m_collectDate = DateTime.Now;
		}
	}
}

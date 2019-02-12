using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class RecordUserRevenue
	{
		public const string Tablename = "RecordUserRevenue";

		public const string _RecordID = "RecordID";

		public const string _DateID = "DateID";

		public const string _UserID = "UserID";

		public const string _Revenue = "Revenue";

		public const string _AgentUserID = "AgentUserID";

		public const string _AgentScale = "AgentScale";

		public const string _AgentRevenue = "AgentRevenue";

		public const string _CompanyScale = "CompanyScale";

		public const string _CompanyRevenue = "CompanyRevenue";

		public const string _CollectDate = "CollectDate";

		private int m_recordID;

		private int m_dateID;

		private int m_userID;

		private long m_revenue;

		private int m_agentUserID;

		private decimal m_agentScale;

		private long m_agentRevenue;

		private decimal m_companyScale;

		private long m_companyRevenue;

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

		public long Revenue
		{
			get
			{
				return m_revenue;
			}
			set
			{
				m_revenue = value;
			}
		}

		public int AgentUserID
		{
			get
			{
				return m_agentUserID;
			}
			set
			{
				m_agentUserID = value;
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

		public long AgentRevenue
		{
			get
			{
				return m_agentRevenue;
			}
			set
			{
				m_agentRevenue = value;
			}
		}

		public decimal CompanyScale
		{
			get
			{
				return m_companyScale;
			}
			set
			{
				m_companyScale = value;
			}
		}

		public long CompanyRevenue
		{
			get
			{
				return m_companyRevenue;
			}
			set
			{
				m_companyRevenue = value;
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

		public RecordUserRevenue()
		{
			m_recordID = 0;
			m_dateID = 0;
			m_userID = 0;
			m_revenue = 0L;
			m_agentUserID = 0;
			m_agentScale = 0m;
			m_agentRevenue = 0L;
			m_companyScale = 0m;
			m_companyRevenue = 0L;
			m_collectDate = DateTime.Now;
		}
	}
}

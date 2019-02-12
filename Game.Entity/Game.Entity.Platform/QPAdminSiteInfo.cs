using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class QPAdminSiteInfo
	{
		public const string Tablename = "QPAdminSiteInfo";

		public const string _SiteID = "SiteID";

		public const string _Revenue = "Revenue";

		public const string _GameScore = "GameScore";

		private int m_siteID;

		private decimal m_revenue;

		private long m_gameScore;

		public int SiteID
		{
			get
			{
				return m_siteID;
			}
			set
			{
				m_siteID = value;
			}
		}

		public decimal Revenue
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

		public long GameScore
		{
			get
			{
				return m_gameScore;
			}
			set
			{
				m_gameScore = value;
			}
		}

		public QPAdminSiteInfo()
		{
			m_siteID = 0;
			m_revenue = 0m;
			m_gameScore = 0L;
		}
	}
}

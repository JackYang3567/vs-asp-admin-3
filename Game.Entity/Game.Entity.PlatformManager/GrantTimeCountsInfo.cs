using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class GrantTimeCountsInfo
	{
		public const string Tablename = "GrantTimeCountsInfo";

		public const string _GrantID = "GrantID";

		public const string _GrantCouts = "GrantCouts";

		public const string _GrantScore = "GrantScore";

		public const string _GrantGameScore = "GrantGameScore";

		public const string _GrantLoveliness = "GrantLoveliness";

		public const string _GrantType = "GrantType";

		public const string _GrantExp = "GrantExp";

		public const string _SiteID = "SiteID";

		private int m_grantID;

		private int m_grantCouts;

		private int m_grantScore;

		private int m_grantGameScore;

		private int m_grantLoveliness;

		private int m_grantType;

		private int m_grantExp;

		private string m_siteID;

		public int GrantID
		{
			get
			{
				return m_grantID;
			}
			set
			{
				m_grantID = value;
			}
		}

		public int GrantCouts
		{
			get
			{
				return m_grantCouts;
			}
			set
			{
				m_grantCouts = value;
			}
		}

		public int GrantScore
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

		public int GrantGameScore
		{
			get
			{
				return m_grantGameScore;
			}
			set
			{
				m_grantGameScore = value;
			}
		}

		public int GrantLoveliness
		{
			get
			{
				return m_grantLoveliness;
			}
			set
			{
				m_grantLoveliness = value;
			}
		}

		public int GrantType
		{
			get
			{
				return m_grantType;
			}
			set
			{
				m_grantType = value;
			}
		}

		public int GrantExp
		{
			get
			{
				return m_grantExp;
			}
			set
			{
				m_grantExp = value;
			}
		}

		public string SiteID
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

		public GrantTimeCountsInfo()
		{
			m_grantID = 0;
			m_grantCouts = 0;
			m_grantScore = 0;
			m_grantGameScore = 0;
			m_grantLoveliness = 0;
			m_grantType = 0;
			m_grantExp = 0;
			m_siteID = "";
		}
	}
}

using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class QPAdminSiteInfo
	{
		public const string Tablename = "QPAdminSiteInfo";

		public const string _SiteID = "SiteID";

		public const string _SiteName = "SiteName";

		public const string _PageSize = "PageSize";

		public const string _CopyRight = "CopyRight";

		private int m_siteID;

		private string m_siteName;

		private int m_pageSize;

		private string m_copyRight;

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

		public string SiteName
		{
			get
			{
				return m_siteName;
			}
			set
			{
				m_siteName = value;
			}
		}

		public int PageSize
		{
			get
			{
				return m_pageSize;
			}
			set
			{
				m_pageSize = value;
			}
		}

		public string CopyRight
		{
			get
			{
				return m_copyRight;
			}
			set
			{
				m_copyRight = value;
			}
		}

		public QPAdminSiteInfo()
		{
			m_siteID = 0;
			m_siteName = "";
			m_pageSize = 0;
			m_copyRight = "";
		}
	}
}

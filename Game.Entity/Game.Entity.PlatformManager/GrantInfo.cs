using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class GrantInfo
	{
		public const string Tablename = "GrantInfo";

		public const string _SiteID = "SiteID";

		public const string _GrantRoom = "GrantRoom";

		public const string _GrantStartDate = "GrantStartDate";

		public const string _GrantEndDate = "GrantEndDate";

		public const string _GrantObjet = "GrantObjet";

		public const string _MaxGrant = "MaxGrant";

		public const string _DayMaxGrant = "DayMaxGrant";

		private int m_siteID;

		private int m_grantRoom;

		private DateTime m_grantStartDate;

		private DateTime m_grantEndDate;

		private string m_grantObjet;

		private int m_maxGrant;

		private int m_dayMaxGrant;

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

		public int GrantRoom
		{
			get
			{
				return m_grantRoom;
			}
			set
			{
				m_grantRoom = value;
			}
		}

		public DateTime GrantStartDate
		{
			get
			{
				return m_grantStartDate;
			}
			set
			{
				m_grantStartDate = value;
			}
		}

		public DateTime GrantEndDate
		{
			get
			{
				return m_grantEndDate;
			}
			set
			{
				m_grantEndDate = value;
			}
		}

		public string GrantObjet
		{
			get
			{
				return m_grantObjet;
			}
			set
			{
				m_grantObjet = value;
			}
		}

		public int MaxGrant
		{
			get
			{
				return m_maxGrant;
			}
			set
			{
				m_maxGrant = value;
			}
		}

		public int DayMaxGrant
		{
			get
			{
				return m_dayMaxGrant;
			}
			set
			{
				m_dayMaxGrant = value;
			}
		}

		public GrantInfo()
		{
			m_siteID = 0;
			m_grantRoom = 0;
			m_grantStartDate = DateTime.Now;
			m_grantEndDate = DateTime.Now;
			m_grantObjet = "";
			m_maxGrant = 0;
			m_dayMaxGrant = 0;
		}
	}
}

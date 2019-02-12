using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class MemberProperty
	{
		public const string Tablename = "MemberProperty";

		public const string _MemberOrder = "MemberOrder";

		public const string _MemberName = "MemberName";

		public const string _UserRight = "UserRight";

		public const string _TaskRate = "TaskRate";

		public const string _ShopRate = "ShopRate";

		public const string _InsureRate = "InsureRate";

		public const string _DayPresent = "DayPresent";

		public const string _DayGiftID = "DayGiftID";

		public const string _CollectDate = "CollectDate";

		public const string _CollectNote = "CollectNote";

		private int m_memberOrder;

		private string m_memberName;

		private int m_userRight;

		private int m_taskRate;

		private int m_shopRate;

		private int m_insureRate;

		private int m_dayPresent;

		private int m_dayGiftID;

		private DateTime m_collectDate;

		private string m_collectNote;

		public int MemberOrder
		{
			get
			{
				return m_memberOrder;
			}
			set
			{
				m_memberOrder = value;
			}
		}

		public string MemberName
		{
			get
			{
				return m_memberName;
			}
			set
			{
				m_memberName = value;
			}
		}

		public int UserRight
		{
			get
			{
				return m_userRight;
			}
			set
			{
				m_userRight = value;
			}
		}

		public int TaskRate
		{
			get
			{
				return m_taskRate;
			}
			set
			{
				m_taskRate = value;
			}
		}

		public int ShopRate
		{
			get
			{
				return m_shopRate;
			}
			set
			{
				m_shopRate = value;
			}
		}

		public int InsureRate
		{
			get
			{
				return m_insureRate;
			}
			set
			{
				m_insureRate = value;
			}
		}

		public int DayPresent
		{
			get
			{
				return m_dayPresent;
			}
			set
			{
				m_dayPresent = value;
			}
		}

		public int DayGiftID
		{
			get
			{
				return m_dayGiftID;
			}
			set
			{
				m_dayGiftID = value;
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

		public string CollectNote
		{
			get
			{
				return m_collectNote;
			}
			set
			{
				m_collectNote = value;
			}
		}

		public MemberProperty()
		{
			m_memberOrder = 0;
			m_memberName = "";
			m_userRight = 0;
			m_taskRate = 0;
			m_shopRate = 0;
			m_insureRate = 0;
			m_dayPresent = 0;
			m_dayGiftID = 0;
			m_collectDate = DateTime.Now;
			m_collectNote = "";
		}
	}
}

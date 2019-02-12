using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class AccountsMember
	{
		public const string Tablename = "AccountsMember";

		public const string _UserID = "UserID";

		public const string _MemberOrder = "MemberOrder";

		public const string _UserRight = "UserRight";

		public const string _MemberOverDate = "MemberOverDate";

		private int m_userID;

		private byte m_memberOrder;

		private int m_userRight;

		private DateTime m_memberOverDate;

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

		public byte MemberOrder
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

		public DateTime MemberOverDate
		{
			get
			{
				return m_memberOverDate;
			}
			set
			{
				m_memberOverDate = value;
			}
		}

		public AccountsMember()
		{
			m_userID = 0;
			m_memberOrder = 0;
			m_userRight = 0;
			m_memberOverDate = DateTime.Now;
		}
	}
}

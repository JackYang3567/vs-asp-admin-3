using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class MemberType
	{
		public const string Tablename = "MemberType";

		public const string _MemberOrder = "MemberOrder";

		public const string _MemberName = "MemberName";

		public const string _MemberPrice = "MemberPrice";

		public const string _PresentScore = "PresentScore";

		public const string _UserRight = "UserRight";

		private byte m_memberOrder;

		private string m_memberName;

		private decimal m_memberPrice;

		private int m_presentScore;

		private int m_userRight;

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

		public decimal MemberPrice
		{
			get
			{
				return m_memberPrice;
			}
			set
			{
				m_memberPrice = value;
			}
		}

		public int PresentScore
		{
			get
			{
				return m_presentScore;
			}
			set
			{
				m_presentScore = value;
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

		public MemberType()
		{
			m_memberOrder = 0;
			m_memberName = "";
			m_memberPrice = 0m;
			m_presentScore = 0;
			m_userRight = 0;
		}
	}
}

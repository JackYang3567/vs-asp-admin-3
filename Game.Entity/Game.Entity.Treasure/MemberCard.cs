using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class MemberCard
	{
		public const string Tablename = "MemberCard";

		public const string _CardID = "CardID";

		public const string _CardName = "CardName";

		public const string _CardPrice = "CardPrice";

		public const string _PresentScore = "PresentScore";

		public const string _MemberOrder = "MemberOrder";

		public const string _UserRight = "UserRight";

		public const string _Describe = "Describe";

		private int m_cardID;

		private string m_cardName;

		private int m_cardPrice;

		private int m_presentScore;

		private byte m_memberOrder;

		private int m_userRight;

		private string m_describe;

		public int CardID
		{
			get
			{
				return m_cardID;
			}
			set
			{
				m_cardID = value;
			}
		}

		public string CardName
		{
			get
			{
				return m_cardName;
			}
			set
			{
				m_cardName = value;
			}
		}

		public int CardPrice
		{
			get
			{
				return m_cardPrice;
			}
			set
			{
				m_cardPrice = value;
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

		public string Describe
		{
			get
			{
				return m_describe;
			}
			set
			{
				m_describe = value;
			}
		}

		public MemberCard()
		{
			m_cardID = 0;
			m_cardName = "";
			m_cardPrice = 0;
			m_presentScore = 0;
			m_memberOrder = 0;
			m_userRight = 0;
			m_describe = "";
		}
	}
}

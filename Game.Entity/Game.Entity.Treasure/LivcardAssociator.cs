using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class LivcardAssociator
	{
		public const string Tablename = "LivcardAssociator";

		public const string _CardID = "CardID";

		public const string _SerialID = "SerialID";

		public const string _Password = "Password";

		public const string _BuildID = "BuildID";

		public const string _CardTypeID = "CardTypeID";

		public const string _CardPrice = "CardPrice";

		public const string _Currency = "Currency";

		public const string _ValidDate = "ValidDate";

		public const string _BuildDate = "BuildDate";

		public const string _ApplyDate = "ApplyDate";

		public const string _UseRange = "UseRange";

		public const string _SalesPerson = "SalesPerson";

		public const string _Nullity = "Nullity";

		public const string _Gold = "Gold";

		private int m_cardID;

		private string m_serialID;

		private string m_password;

		private int m_buildID;

		private int m_cardTypeID;

		private decimal m_cardPrice;

		private decimal m_currency;

		private DateTime m_validDate;

		private DateTime m_buildDate;

		private DateTime m_applyDate;

		private int m_useRange;

		private string m_salesPerson;

		private byte m_nullity;

		private int m_gold;

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

		public int Gold
		{
			get
			{
				return m_gold;
			}
			set
			{
				m_gold = value;
			}
		}

		public string SerialID
		{
			get
			{
				return m_serialID;
			}
			set
			{
				m_serialID = value;
			}
		}

		public string Password
		{
			get
			{
				return m_password;
			}
			set
			{
				m_password = value;
			}
		}

		public int BuildID
		{
			get
			{
				return m_buildID;
			}
			set
			{
				m_buildID = value;
			}
		}

		public int CardTypeID
		{
			get
			{
				return m_cardTypeID;
			}
			set
			{
				m_cardTypeID = value;
			}
		}

		public decimal CardPrice
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

		public decimal Currency
		{
			get
			{
				return m_currency;
			}
			set
			{
				m_currency = value;
			}
		}

		public DateTime ValidDate
		{
			get
			{
				return m_validDate;
			}
			set
			{
				m_validDate = value;
			}
		}

		public DateTime BuildDate
		{
			get
			{
				return m_buildDate;
			}
			set
			{
				m_buildDate = value;
			}
		}

		public DateTime ApplyDate
		{
			get
			{
				return m_applyDate;
			}
			set
			{
				m_applyDate = value;
			}
		}

		public int UseRange
		{
			get
			{
				return m_useRange;
			}
			set
			{
				m_useRange = value;
			}
		}

		public string SalesPerson
		{
			get
			{
				return m_salesPerson;
			}
			set
			{
				m_salesPerson = value;
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

		public LivcardAssociator()
		{
			m_cardID = 0;
			m_serialID = "";
			m_password = "";
			m_buildID = 0;
			m_cardTypeID = 0;
			m_cardPrice = 0m;
			m_currency = 0m;
			m_validDate = DateTime.Now;
			m_buildDate = DateTime.Now;
			m_applyDate = DateTime.Now;
			m_useRange = 0;
			m_salesPerson = "";
			m_nullity = 0;
			m_gold = 0;
		}
	}
}

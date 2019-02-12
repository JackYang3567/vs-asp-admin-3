using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class GlobalLivcard
	{
		public const string Tablename = "GlobalLivcard";

		public const string _CardTypeID = "CardTypeID";

		public const string _CardName = "CardName";

		public const string _CardPrice = "CardPrice";

		public const string _Currency = "Currency";

		public const string _InputDate = "InputDate";

		public const string _Gold = "Gold";

		private int m_cardTypeID;

		private string m_cardName;

		private decimal m_cardPrice;

		private decimal m_currency;

		private DateTime m_inputDate;

		private int m_gold;

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

		public DateTime InputDate
		{
			get
			{
				return m_inputDate;
			}
			set
			{
				m_inputDate = value;
			}
		}

		public GlobalLivcard()
		{
			m_cardTypeID = 0;
			m_cardName = "";
			m_cardPrice = 0m;
			m_currency = 0m;
			m_inputDate = DateTime.Now;
		}
	}
}

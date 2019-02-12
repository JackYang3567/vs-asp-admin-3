using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordEveryDayData
	{
		public const string Tablename = "RecordEveryDayData";

		public const string _DateID = "DateID";

		public const string _UserTotal = "UserTotal";

		public const string _PayUserTotal = "PayUserTotal";

		public const string _ActiveUserTotal = "ActiveUserTotal";

		public const string _LossUserTotal = "LossUserTotal";

		public const string _LossPayUserTotal = "LossPayUserTotal";

		public const string _PayTotalAmount = "PayTotalAmount";

		public const string _PayAmountForCurrency = "PayAmountForCurrency";

		public const string _GoldTotal = "GoldTotal";

		public const string _CurrencyTotal = "CurrencyTotal";

		public const string _GameTax = "GameTax";

		public const string _GameTaxTotal = "GameTaxTotal";

		public const string _BankTax = "BankTax";

		public const string _Waste = "Waste";

		public const string _UserAVGOnlineTime = "UserAVGOnlineTime";

		public const string _CollectDate = "CollectDate";

		private int m_dateID;

		private int m_userTotal;

		private int m_payUserTotal;

		private int m_activeUserTotal;

		private int m_lossUserTotal;

		private int m_lossPayUserTotal;

		private int m_payTotalAmount;

		private int m_payAmountForCurrency;

		private long m_goldTotal;

		private long m_currencyTotal;

		private long m_gameTax;

		private long m_gameTaxTotal;

		private long m_bankTax;

		private long m_waste;

		private int m_userAVGOnlineTime;

		private DateTime m_collectDate;

		public int DateID
		{
			get
			{
				return m_dateID;
			}
			set
			{
				m_dateID = value;
			}
		}

		public int UserTotal
		{
			get
			{
				return m_userTotal;
			}
			set
			{
				m_userTotal = value;
			}
		}

		public int PayUserTotal
		{
			get
			{
				return m_payUserTotal;
			}
			set
			{
				m_payUserTotal = value;
			}
		}

		public int ActiveUserTotal
		{
			get
			{
				return m_activeUserTotal;
			}
			set
			{
				m_activeUserTotal = value;
			}
		}

		public int LossUserTotal
		{
			get
			{
				return m_lossUserTotal;
			}
			set
			{
				m_lossUserTotal = value;
			}
		}

		public int LossPayUserTotal
		{
			get
			{
				return m_lossPayUserTotal;
			}
			set
			{
				m_lossPayUserTotal = value;
			}
		}

		public int PayTotalAmount
		{
			get
			{
				return m_payTotalAmount;
			}
			set
			{
				m_payTotalAmount = value;
			}
		}

		public int PayAmountForCurrency
		{
			get
			{
				return m_payAmountForCurrency;
			}
			set
			{
				m_payAmountForCurrency = value;
			}
		}

		public long GoldTotal
		{
			get
			{
				return m_goldTotal;
			}
			set
			{
				m_goldTotal = value;
			}
		}

		public long CurrencyTotal
		{
			get
			{
				return m_currencyTotal;
			}
			set
			{
				m_currencyTotal = value;
			}
		}

		public long GameTax
		{
			get
			{
				return m_gameTax;
			}
			set
			{
				m_gameTax = value;
			}
		}

		public long GameTaxTotal
		{
			get
			{
				return m_gameTaxTotal;
			}
			set
			{
				m_gameTaxTotal = value;
			}
		}

		public long BankTax
		{
			get
			{
				return m_bankTax;
			}
			set
			{
				m_bankTax = value;
			}
		}

		public long Waste
		{
			get
			{
				return m_waste;
			}
			set
			{
				m_waste = value;
			}
		}

		public int UserAVGOnlineTime
		{
			get
			{
				return m_userAVGOnlineTime;
			}
			set
			{
				m_userAVGOnlineTime = value;
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

		public RecordEveryDayData()
		{
			m_dateID = 0;
			m_userTotal = 0;
			m_payUserTotal = 0;
			m_activeUserTotal = 0;
			m_lossUserTotal = 0;
			m_lossPayUserTotal = 0;
			m_payTotalAmount = 0;
			m_payAmountForCurrency = 0;
			m_goldTotal = 0L;
			m_currencyTotal = 0L;
			m_gameTax = 0L;
			m_gameTaxTotal = 0L;
			m_bankTax = 0L;
			m_waste = 0L;
			m_userAVGOnlineTime = 0;
			m_collectDate = DateTime.Now;
		}
	}
}

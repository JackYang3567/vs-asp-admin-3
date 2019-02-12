using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class ShareDetailInfo
	{
		public const string Tablename = "ShareDetailInfo";

		public const string _DetailID = "DetailID";

		public const string _OperUserID = "OperUserID";

		public const string _ShareID = "ShareID";

		public const string _UserID = "UserID";

		public const string _GameID = "GameID";

		public const string _Accounts = "Accounts";

		public const string _CardTypeID = "CardTypeID";

		public const string _SerialID = "SerialID";

		public const string _OrderID = "OrderID";

		public const string _OrderAmount = "OrderAmount";

		public const string _DiscountScale = "DiscountScale";

		public const string _PayAmount = "PayAmount";

		public const string _Currency = "Currency";

		public const string _BeforeCurrency = "BeforeCurrency";

		public const string _IPAddress = "IPAddress";

		public const string _ApplyDate = "ApplyDate";

		private int m_detailID;

		private int m_operUserID;

		private int m_shareID;

		private int m_userID;

		private int m_gameID;

		private string m_accounts;

		private int m_cardTypeID;

		private string m_serialID;

		private string m_orderID;

		private decimal m_orderAmount;

		private decimal m_discountScale;

		private decimal m_payAmount;

		private decimal m_currency;

		private decimal m_beforeCurrency;

		private string m_iPAddress;

		private DateTime m_applyDate;

		public int DetailID
		{
			get
			{
				return m_detailID;
			}
			set
			{
				m_detailID = value;
			}
		}

		public int OperUserID
		{
			get
			{
				return m_operUserID;
			}
			set
			{
				m_operUserID = value;
			}
		}

		public int ShareID
		{
			get
			{
				return m_shareID;
			}
			set
			{
				m_shareID = value;
			}
		}

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

		public int GameID
		{
			get
			{
				return m_gameID;
			}
			set
			{
				m_gameID = value;
			}
		}

		public string Accounts
		{
			get
			{
				return m_accounts;
			}
			set
			{
				m_accounts = value;
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

		public string OrderID
		{
			get
			{
				return m_orderID;
			}
			set
			{
				m_orderID = value;
			}
		}

		public decimal OrderAmount
		{
			get
			{
				return m_orderAmount;
			}
			set
			{
				m_orderAmount = value;
			}
		}

		public decimal DiscountScale
		{
			get
			{
				return m_discountScale;
			}
			set
			{
				m_discountScale = value;
			}
		}

		public decimal PayAmount
		{
			get
			{
				return m_payAmount;
			}
			set
			{
				m_payAmount = value;
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

		public decimal BeforeCurrency
		{
			get
			{
				return m_beforeCurrency;
			}
			set
			{
				m_beforeCurrency = value;
			}
		}

		public string IPAddress
		{
			get
			{
				return m_iPAddress;
			}
			set
			{
				m_iPAddress = value;
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

		public ShareDetailInfo()
		{
			m_detailID = 0;
			m_operUserID = 0;
			m_shareID = 0;
			m_userID = 0;
			m_gameID = 0;
			m_accounts = "";
			m_cardTypeID = 0;
			m_serialID = "";
			m_orderID = "";
			m_orderAmount = 0m;
			m_discountScale = 0m;
			m_payAmount = 0m;
			m_currency = 0m;
			m_beforeCurrency = 0m;
			m_iPAddress = "";
			m_applyDate = DateTime.Now;
		}
	}
}

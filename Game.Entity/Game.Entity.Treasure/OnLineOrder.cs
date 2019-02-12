using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class OnLineOrder
	{
		public const string Tablename = "OnLineOrder";

		public const string _OnLineID = "OnLineID";

		public const string _OperUserID = "OperUserID";

		public const string _ShareID = "ShareID";

		public const string _UserID = "UserID";

		public const string _GameID = "GameID";

		public const string _Accounts = "Accounts";

		public const string _OrderID = "OrderID";

		public const string _CardTypeID = "CardTypeID";

		public const string _CardPrice = "CardPrice";

		public const string _CardGold = "CardGold";

		public const string _CardTotal = "CardTotal";

		public const string _OrderAmount = "OrderAmount";

		public const string _DiscountScale = "DiscountScale";

		public const string _PayAmount = "PayAmount";

		public const string _OrderStatus = "OrderStatus";

		public const string _IPAddress = "IPAddress";

		public const string _ApplyDate = "ApplyDate";

		private int m_onLineID;

		private int m_operUserID;

		private int m_shareID;

		private int m_userID;

		private int m_gameID;

		private string m_accounts;

		private string m_orderID;

		private int m_cardTypeID;

		private decimal m_cardPrice;

		private long m_cardGold;

		private int m_cardTotal;

		private decimal m_orderAmount;

		private decimal m_discountScale;

		private decimal m_payAmount;

		private byte m_orderStatus;

		private string m_iPAddress;

		private DateTime m_applyDate;

		public int OnLineID
		{
			get
			{
				return m_onLineID;
			}
			set
			{
				m_onLineID = value;
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

		public long CardGold
		{
			get
			{
				return m_cardGold;
			}
			set
			{
				m_cardGold = value;
			}
		}

		public int CardTotal
		{
			get
			{
				return m_cardTotal;
			}
			set
			{
				m_cardTotal = value;
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

		public byte OrderStatus
		{
			get
			{
				return m_orderStatus;
			}
			set
			{
				m_orderStatus = value;
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

		public OnLineOrder()
		{
			m_onLineID = 0;
			m_operUserID = 0;
			m_shareID = 0;
			m_userID = 0;
			m_gameID = 0;
			m_accounts = "";
			m_orderID = "";
			m_cardTypeID = 0;
			m_cardPrice = 0m;
			m_cardGold = 0L;
			m_cardTotal = 0;
			m_orderAmount = 0m;
			m_discountScale = 0m;
			m_payAmount = 0m;
			m_orderStatus = 0;
			m_iPAddress = "";
			m_applyDate = DateTime.Now;
		}
	}
}

using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class ReturnKQDetailInfo
	{
		public const string Tablename = "ReturnKQDetailInfo";

		public const string _DetailID = "DetailID";

		public const string _MerchantAcctID = "MerchantAcctID";

		public const string _Version = "Version";

		public const string _Language = "Language";

		public const string _SignType = "SignType";

		public const string _PayType = "PayType";

		public const string _BankID = "BankID";

		public const string _OrderID = "OrderID";

		public const string _OrderTime = "OrderTime";

		public const string _OrderAmount = "OrderAmount";

		public const string _DealID = "DealID";

		public const string _BankDealID = "BankDealID";

		public const string _DealTime = "DealTime";

		public const string _PayAmount = "PayAmount";

		public const string _Fee = "Fee";

		public const string _PayResult = "PayResult";

		public const string _ErrCode = "ErrCode";

		public const string _SignMsg = "SignMsg";

		public const string _Ext1 = "Ext1";

		public const string _Ext2 = "Ext2";

		public const string _CardNumber = "CardNumber";

		public const string _CardPwd = "CardPwd";

		public const string _BossType = "BossType";

		public const string _ReceiveBossType = "ReceiveBossType";

		public const string _ReceiverAcctId = "ReceiverAcctId";

		public const string _PayDate = "PayDate";

		private int m_detailID;

		private string m_merchantAcctID;

		private string m_version;

		private int m_language;

		private int m_signType;

		private string m_payType;

		private string m_bankID;

		private string m_orderID;

		private DateTime m_orderTime;

		private decimal m_orderAmount;

		private string m_dealID;

		private string m_bankDealID;

		private DateTime m_dealTime;

		private decimal m_payAmount;

		private decimal m_fee;

		private string m_payResult;

		private string m_errCode;

		private string m_signMsg;

		private string m_ext1;

		private string m_ext2;

		private string m_cardNumber;

		private string m_cardPwd;

		private string m_bossType;

		private string m_receiveBossType;

		private string m_receiverAcctId;

		private DateTime m_payDate;

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

		public string MerchantAcctID
		{
			get
			{
				return m_merchantAcctID;
			}
			set
			{
				m_merchantAcctID = value;
			}
		}

		public string Version
		{
			get
			{
				return m_version;
			}
			set
			{
				m_version = value;
			}
		}

		public int Language
		{
			get
			{
				return m_language;
			}
			set
			{
				m_language = value;
			}
		}

		public int SignType
		{
			get
			{
				return m_signType;
			}
			set
			{
				m_signType = value;
			}
		}

		public string PayType
		{
			get
			{
				return m_payType;
			}
			set
			{
				m_payType = value;
			}
		}

		public string BankID
		{
			get
			{
				return m_bankID;
			}
			set
			{
				m_bankID = value;
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

		public DateTime OrderTime
		{
			get
			{
				return m_orderTime;
			}
			set
			{
				m_orderTime = value;
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

		public string DealID
		{
			get
			{
				return m_dealID;
			}
			set
			{
				m_dealID = value;
			}
		}

		public string BankDealID
		{
			get
			{
				return m_bankDealID;
			}
			set
			{
				m_bankDealID = value;
			}
		}

		public DateTime DealTime
		{
			get
			{
				return m_dealTime;
			}
			set
			{
				m_dealTime = value;
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

		public decimal Fee
		{
			get
			{
				return m_fee;
			}
			set
			{
				m_fee = value;
			}
		}

		public string PayResult
		{
			get
			{
				return m_payResult;
			}
			set
			{
				m_payResult = value;
			}
		}

		public string ErrCode
		{
			get
			{
				return m_errCode;
			}
			set
			{
				m_errCode = value;
			}
		}

		public string SignMsg
		{
			get
			{
				return m_signMsg;
			}
			set
			{
				m_signMsg = value;
			}
		}

		public string Ext1
		{
			get
			{
				return m_ext1;
			}
			set
			{
				m_ext1 = value;
			}
		}

		public string Ext2
		{
			get
			{
				return m_ext2;
			}
			set
			{
				m_ext2 = value;
			}
		}

		public string CardNumber
		{
			get
			{
				return m_cardNumber;
			}
			set
			{
				m_cardNumber = value;
			}
		}

		public string CardPwd
		{
			get
			{
				return m_cardPwd;
			}
			set
			{
				m_cardPwd = value;
			}
		}

		public string BossType
		{
			get
			{
				return m_bossType;
			}
			set
			{
				m_bossType = value;
			}
		}

		public string ReceiveBossType
		{
			get
			{
				return m_receiveBossType;
			}
			set
			{
				m_receiveBossType = value;
			}
		}

		public string ReceiverAcctId
		{
			get
			{
				return m_receiverAcctId;
			}
			set
			{
				m_receiverAcctId = value;
			}
		}

		public DateTime PayDate
		{
			get
			{
				return m_payDate;
			}
			set
			{
				m_payDate = value;
			}
		}

		public ReturnKQDetailInfo()
		{
			m_detailID = 0;
			m_merchantAcctID = "";
			m_version = "";
			m_language = 0;
			m_signType = 0;
			m_payType = "";
			m_bankID = "";
			m_orderID = "";
			m_orderTime = DateTime.Now;
			m_orderAmount = 0m;
			m_dealID = "";
			m_bankDealID = "";
			m_dealTime = DateTime.Now;
			m_payAmount = 0m;
			m_fee = 0m;
			m_payResult = "";
			m_errCode = "";
			m_signMsg = "";
			m_ext1 = "";
			m_ext2 = "";
			m_cardNumber = "";
			m_cardPwd = "";
			m_bossType = "";
			m_receiveBossType = "";
			m_receiverAcctId = "";
			m_payDate = DateTime.Now;
		}
	}
}

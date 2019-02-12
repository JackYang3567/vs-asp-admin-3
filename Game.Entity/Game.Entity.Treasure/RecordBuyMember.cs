using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class RecordBuyMember
	{
		public const string Tablename = "RecordBuyMember";

		public const string _RecordID = "RecordID";

		public const string _UserID = "UserID";

		public const string _MemberOrder = "MemberOrder";

		public const string _MemberMonths = "MemberMonths";

		public const string _MemberPrice = "MemberPrice";

		public const string _Currency = "Currency";

		public const string _PresentScore = "PresentScore";

		public const string _BeforeCurrency = "BeforeCurrency";

		public const string _BeforeScore = "BeforeScore";

		public const string _ClinetIP = "ClinetIP";

		public const string _InputDate = "InputDate";

		private int m_recordID;

		private int m_userID;

		private int m_memberOrder;

		private int m_memberMonths;

		private decimal m_memberPrice;

		private decimal m_currency;

		private long m_presentScore;

		private decimal m_beforeCurrency;

		private long m_beforeScore;

		private string m_clinetIP;

		private DateTime m_inputDate;

		public int RecordID
		{
			get
			{
				return m_recordID;
			}
			set
			{
				m_recordID = value;
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

		public int MemberMonths
		{
			get
			{
				return m_memberMonths;
			}
			set
			{
				m_memberMonths = value;
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

		public long PresentScore
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

		public long BeforeScore
		{
			get
			{
				return m_beforeScore;
			}
			set
			{
				m_beforeScore = value;
			}
		}

		public string ClinetIP
		{
			get
			{
				return m_clinetIP;
			}
			set
			{
				m_clinetIP = value;
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

		public RecordBuyMember()
		{
			m_recordID = 0;
			m_userID = 0;
			m_memberOrder = 0;
			m_memberMonths = 0;
			m_memberPrice = 0m;
			m_currency = 0m;
			m_presentScore = 0L;
			m_beforeCurrency = 0m;
			m_beforeScore = 0L;
			m_clinetIP = "";
			m_inputDate = DateTime.Now;
		}
	}
}

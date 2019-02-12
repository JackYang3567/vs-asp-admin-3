using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class RecordExchCurrency
	{
		public const string Tablename = "RecordExchCurrency";

		public const string _RecordID = "RecordID";

		public const string _CardID = "CardID";

		public const string _CardName = "CardName";

		public const string _CardPrice = "CardPrice";

		public const string _ExchNum = "ExchNum";

		public const string _UserID = "UserID";

		public const string _ExchCurrency = "ExchCurrency";

		public const string _PresentScore = "PresentScore";

		public const string _BeforeCurrency = "BeforeCurrency";

		public const string _BeforeScore = "BeforeScore";

		public const string _ClinetIP = "ClinetIP";

		public const string _InputDate = "InputDate";

		private int m_recordID;

		private int m_cardID;

		private string m_cardName;

		private decimal m_cardPrice;

		private int m_exchNum;

		private int m_userID;

		private decimal m_exchCurrency;

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

		public int ExchNum
		{
			get
			{
				return m_exchNum;
			}
			set
			{
				m_exchNum = value;
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

		public decimal ExchCurrency
		{
			get
			{
				return m_exchCurrency;
			}
			set
			{
				m_exchCurrency = value;
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

		public RecordExchCurrency()
		{
			m_recordID = 0;
			m_cardID = 0;
			m_cardName = "";
			m_cardPrice = 0m;
			m_exchNum = 0;
			m_userID = 0;
			m_exchCurrency = 0m;
			m_presentScore = 0L;
			m_beforeCurrency = 0m;
			m_beforeScore = 0L;
			m_clinetIP = "";
			m_inputDate = DateTime.Now;
		}
	}
}

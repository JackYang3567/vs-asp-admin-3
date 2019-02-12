using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class RecordCurrencyChange
	{
		public const string Tablename = "RecordCurrencyChange";

		public const string _RecordID = "RecordID";

		public const string _UserID = "UserID";

		public const string _ChangeCurrency = "ChangeCurrency";

		public const string _ChangeType = "ChangeType";

		public const string _BeforeCurrency = "BeforeCurrency";

		public const string _AfterCurrency = "AfterCurrency";

		public const string _ClinetIP = "ClinetIP";

		public const string _InputDate = "InputDate";

		public const string _Remark = "Remark";

		private int m_recordID;

		private int m_userID;

		private decimal m_changeCurrency;

		private byte m_changeType;

		private decimal m_beforeCurrency;

		private decimal m_afterCurrency;

		private string m_clinetIP;

		private DateTime m_inputDate;

		private string m_remark;

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

		public decimal ChangeCurrency
		{
			get
			{
				return m_changeCurrency;
			}
			set
			{
				m_changeCurrency = value;
			}
		}

		public byte ChangeType
		{
			get
			{
				return m_changeType;
			}
			set
			{
				m_changeType = value;
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

		public decimal AfterCurrency
		{
			get
			{
				return m_afterCurrency;
			}
			set
			{
				m_afterCurrency = value;
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

		public string Remark
		{
			get
			{
				return m_remark;
			}
			set
			{
				m_remark = value;
			}
		}

		public RecordCurrencyChange()
		{
			m_recordID = 0;
			m_userID = 0;
			m_changeCurrency = 0m;
			m_changeType = 0;
			m_beforeCurrency = 0m;
			m_afterCurrency = 0m;
			m_clinetIP = "";
			m_inputDate = DateTime.Now;
			m_remark = "";
		}
	}
}

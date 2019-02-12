using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class AccountsControlRecord
	{
		public const string Tablename = "AccountsControlRecord";

		public const string _ID = "ID";

		public const string _UserID = "UserID";

		public const string _Accounts = "Accounts";

		public const string _ControlStatus = "ControlStatus";

		public const string _ControlType = "ControlType";

		public const string _ChangeScore = "ChangeScore";

		public const string _SustainedTimeCount = "SustainedTimeCount";

		public const string _ConsumeTimeCount = "ConsumeTimeCount";

		public const string _ConcludeType = "ConcludeType";

		public const string _StartDateTime = "StartDateTime";

		public const string _ConcludeDateTime = "ConcludeDateTime";

		public const string _WinRate = "WinRate";

		public const string _WinScore = "WinScore";

		public const string _LoseScore = "LoseScore";

		private int m_iD;

		private int m_userID;

		private string m_accounts;

		private short m_controlStatus;

		private short m_controlType;

		private decimal m_changeScore;

		private int m_sustainedTimeCount;

		private int m_consumeTimeCount;

		private short m_concludeType;

		private DateTime m_startDateTime;

		private DateTime m_concludeDateTime;

		private short m_winRate;

		private decimal m_winScore;

		private decimal m_loseScore;

		public int ID
		{
			get
			{
				return m_iD;
			}
			set
			{
				m_iD = value;
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

		public short ControlStatus
		{
			get
			{
				return m_controlStatus;
			}
			set
			{
				m_controlStatus = value;
			}
		}

		public short ControlType
		{
			get
			{
				return m_controlType;
			}
			set
			{
				m_controlType = value;
			}
		}

		public decimal ChangeScore
		{
			get
			{
				return m_changeScore;
			}
			set
			{
				m_changeScore = value;
			}
		}

		public int SustainedTimeCount
		{
			get
			{
				return m_sustainedTimeCount;
			}
			set
			{
				m_sustainedTimeCount = value;
			}
		}

		public int ConsumeTimeCount
		{
			get
			{
				return m_consumeTimeCount;
			}
			set
			{
				m_consumeTimeCount = value;
			}
		}

		public short ConcludeType
		{
			get
			{
				return m_concludeType;
			}
			set
			{
				m_concludeType = value;
			}
		}

		public DateTime StartDateTime
		{
			get
			{
				return m_startDateTime;
			}
			set
			{
				m_startDateTime = value;
			}
		}

		public DateTime ConcludeDateTime
		{
			get
			{
				return m_concludeDateTime;
			}
			set
			{
				m_concludeDateTime = value;
			}
		}

		public short WinRate
		{
			get
			{
				return m_winRate;
			}
			set
			{
				m_winRate = value;
			}
		}

		public decimal WinScore
		{
			get
			{
				return m_winScore;
			}
			set
			{
				m_winScore = value;
			}
		}

		public decimal LoseScore
		{
			get
			{
				return m_loseScore;
			}
			set
			{
				m_loseScore = value;
			}
		}

		public AccountsControlRecord()
		{
			m_iD = 0;
			m_userID = 0;
			m_accounts = "";
			m_controlStatus = 0;
			m_controlType = 0;
			m_changeScore = 0m;
			m_sustainedTimeCount = 0;
			m_consumeTimeCount = 0;
			m_concludeType = 0;
			m_startDateTime = DateTime.Now;
			m_concludeDateTime = DateTime.Now;
			m_winRate = 0;
			m_winScore = 0m;
			m_loseScore = 0m;
		}
	}
}

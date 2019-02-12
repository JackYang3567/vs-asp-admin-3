using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class AccountsControl
	{
		public const string Tablename = "AccountsControl";

		public const string _UserID = "UserID";

		public const string _Accounts = "Accounts";

		public const string _ActiveDateTime = "ActiveDateTime";

		public const string _ControlStatus = "ControlStatus";

		public const string _ControlType = "ControlType";

		public const string _ChangeScore = "ChangeScore";

		public const string _SustainedTimeCount = "SustainedTimeCount";

		public const string _ConsumeTimeCount = "ConsumeTimeCount";

		public const string _WinRate = "WinRate";

		public const string _WinScore = "WinScore";

		public const string _LoseScore = "LoseScore";

		private int m_userID;

		private string m_accounts;

		private DateTime m_activeDateTime;

		private short m_controlStatus;

		private short m_controlType;

		private long m_changeScore;

		private int m_sustainedTimeCount;

		private int m_consumeTimeCount;

		private short m_winRate;

		private long m_winScore;

		private long m_loseScore;

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

		public DateTime ActiveDateTime
		{
			get
			{
				return m_activeDateTime;
			}
			set
			{
				m_activeDateTime = value;
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

		public long ChangeScore
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

		public long WinScore
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

		public long LoseScore
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

		public AccountsControl()
		{
			m_userID = 0;
			m_accounts = "";
			m_activeDateTime = DateTime.Now;
			m_controlStatus = 0;
			m_controlType = 0;
			m_changeScore = 0L;
			m_sustainedTimeCount = 0;
			m_consumeTimeCount = 0;
			m_winRate = 0;
			m_winScore = 0L;
			m_loseScore = 0L;
		}
	}
}

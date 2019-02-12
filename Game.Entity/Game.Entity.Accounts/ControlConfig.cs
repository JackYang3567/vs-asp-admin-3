using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class ControlConfig
	{
		public const string Tablename = "ControlConfig";

		public const string _AutoControlEnable = "AutoControlEnable";

		public const string _BSustainedTimeCount = "BSustainedTimeCount";

		public const string _WSustainedTimeCount = "WSustainedTimeCount";

		public const string _JoinBlackWinScore = "JoinBlackWinScore";

		public const string _JoinWhiteLoseScore = "JoinWhiteLoseScore";

		public const string _BlackControlType = "BlackControlType";

		public const string _WhiteControlType = "WhiteControlType";

		public const string _QuitBlackLoseScore = "QuitBlackLoseScore";

		public const string _QuitWhiteWinScore = "QuitWhiteWinScore";

		public const string _BlackWinRate = "BlackWinRate";

		public const string _WhiteWinRate = "WhiteWinRate";

		private byte m_autoControlEnable;

		private int m_bSustainedTimeCount;

		private int m_wSustainedTimeCount;

		private long m_joinBlackWinScore;

		private long m_joinWhiteLoseScore;

		private short m_blackControlType;

		private short m_whiteControlType;

		private long m_quitBlackLoseScore;

		private long m_quitWhiteWinScore;

		private short m_blackWinRate;

		private short m_whiteWinRate;

		public byte AutoControlEnable
		{
			get
			{
				return m_autoControlEnable;
			}
			set
			{
				m_autoControlEnable = value;
			}
		}

		public int BSustainedTimeCount
		{
			get
			{
				return m_bSustainedTimeCount;
			}
			set
			{
				m_bSustainedTimeCount = value;
			}
		}

		public int WSustainedTimeCount
		{
			get
			{
				return m_wSustainedTimeCount;
			}
			set
			{
				m_wSustainedTimeCount = value;
			}
		}

		public long JoinBlackWinScore
		{
			get
			{
				return m_joinBlackWinScore;
			}
			set
			{
				m_joinBlackWinScore = value;
			}
		}

		public long JoinWhiteLoseScore
		{
			get
			{
				return m_joinWhiteLoseScore;
			}
			set
			{
				m_joinWhiteLoseScore = value;
			}
		}

		public short BlackControlType
		{
			get
			{
				return m_blackControlType;
			}
			set
			{
				m_blackControlType = value;
			}
		}

		public short WhiteControlType
		{
			get
			{
				return m_whiteControlType;
			}
			set
			{
				m_whiteControlType = value;
			}
		}

		public long QuitBlackLoseScore
		{
			get
			{
				return m_quitBlackLoseScore;
			}
			set
			{
				m_quitBlackLoseScore = value;
			}
		}

		public long QuitWhiteWinScore
		{
			get
			{
				return m_quitWhiteWinScore;
			}
			set
			{
				m_quitWhiteWinScore = value;
			}
		}

		public short BlackWinRate
		{
			get
			{
				return m_blackWinRate;
			}
			set
			{
				m_blackWinRate = value;
			}
		}

		public short WhiteWinRate
		{
			get
			{
				return m_whiteWinRate;
			}
			set
			{
				m_whiteWinRate = value;
			}
		}

		public ControlConfig()
		{
			m_autoControlEnable = 0;
			m_bSustainedTimeCount = 0;
			m_wSustainedTimeCount = 0;
			m_joinBlackWinScore = 0L;
			m_joinWhiteLoseScore = 0L;
			m_blackControlType = 0;
			m_whiteControlType = 0;
			m_quitBlackLoseScore = 0L;
			m_quitWhiteWinScore = 0L;
			m_blackWinRate = 0;
			m_whiteWinRate = 0;
		}
	}
}

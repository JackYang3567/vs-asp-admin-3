using System;

namespace Game.Entity.GameScore
{
	[Serializable]
	public class GameScoreInfo
	{
		public const string Tablename = "GameScoreInfo";

		public const string _UserID = "UserID";

		public const string _Score = "Score";

		public const string _WinCount = "WinCount";

		public const string _LostCount = "LostCount";

		public const string _DrawCount = "DrawCount";

		public const string _FleeCount = "FleeCount";

		public const string _UserRight = "UserRight";

		public const string _MasterRight = "MasterRight";

		public const string _MasterOrder = "MasterOrder";

		public const string _AllLogonTimes = "AllLogonTimes";

		public const string _PlayTimeCount = "PlayTimeCount";

		public const string _OnLineTimeCount = "OnLineTimeCount";

		public const string _LastLogonIP = "LastLogonIP";

		public const string _LastLogonDate = "LastLogonDate";

		public const string _LastLogonMachine = "LastLogonMachine";

		public const string _RegisterIP = "RegisterIP";

		public const string _RegisterDate = "RegisterDate";

		public const string _RegisterMachine = "RegisterMachine";

		private int m_userID;

		private long m_score;

		private int m_winCount;

		private int m_lostCount;

		private int m_drawCount;

		private int m_fleeCount;

		private int m_userRight;

		private int m_masterRight;

		private byte m_masterOrder;

		private int m_allLogonTimes;

		private int m_playTimeCount;

		private int m_onLineTimeCount;

		private string m_lastLogonIP;

		private DateTime m_lastLogonDate;

		private string m_lastLogonMachine;

		private string m_registerIP;

		private DateTime m_registerDate;

		private string m_registerMachine;

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

		public long Score
		{
			get
			{
				return m_score;
			}
			set
			{
				m_score = value;
			}
		}

		public int WinCount
		{
			get
			{
				return m_winCount;
			}
			set
			{
				m_winCount = value;
			}
		}

		public int LostCount
		{
			get
			{
				return m_lostCount;
			}
			set
			{
				m_lostCount = value;
			}
		}

		public int DrawCount
		{
			get
			{
				return m_drawCount;
			}
			set
			{
				m_drawCount = value;
			}
		}

		public int FleeCount
		{
			get
			{
				return m_fleeCount;
			}
			set
			{
				m_fleeCount = value;
			}
		}

		public int UserRight
		{
			get
			{
				return m_userRight;
			}
			set
			{
				m_userRight = value;
			}
		}

		public int MasterRight
		{
			get
			{
				return m_masterRight;
			}
			set
			{
				m_masterRight = value;
			}
		}

		public byte MasterOrder
		{
			get
			{
				return m_masterOrder;
			}
			set
			{
				m_masterOrder = value;
			}
		}

		public int AllLogonTimes
		{
			get
			{
				return m_allLogonTimes;
			}
			set
			{
				m_allLogonTimes = value;
			}
		}

		public int PlayTimeCount
		{
			get
			{
				return m_playTimeCount;
			}
			set
			{
				m_playTimeCount = value;
			}
		}

		public int OnLineTimeCount
		{
			get
			{
				return m_onLineTimeCount;
			}
			set
			{
				m_onLineTimeCount = value;
			}
		}

		public string LastLogonIP
		{
			get
			{
				return m_lastLogonIP;
			}
			set
			{
				m_lastLogonIP = value;
			}
		}

		public DateTime LastLogonDate
		{
			get
			{
				return m_lastLogonDate;
			}
			set
			{
				m_lastLogonDate = value;
			}
		}

		public string LastLogonMachine
		{
			get
			{
				return m_lastLogonMachine;
			}
			set
			{
				m_lastLogonMachine = value;
			}
		}

		public string RegisterIP
		{
			get
			{
				return m_registerIP;
			}
			set
			{
				m_registerIP = value;
			}
		}

		public DateTime RegisterDate
		{
			get
			{
				return m_registerDate;
			}
			set
			{
				m_registerDate = value;
			}
		}

		public string RegisterMachine
		{
			get
			{
				return m_registerMachine;
			}
			set
			{
				m_registerMachine = value;
			}
		}

		public GameScoreInfo()
		{
			m_userID = 0;
			m_score = 0L;
			m_winCount = 0;
			m_lostCount = 0;
			m_drawCount = 0;
			m_fleeCount = 0;
			m_userRight = 0;
			m_masterRight = 0;
			m_masterOrder = 0;
			m_allLogonTimes = 0;
			m_playTimeCount = 0;
			m_onLineTimeCount = 0;
			m_lastLogonIP = "";
			m_lastLogonDate = DateTime.Now;
			m_lastLogonMachine = "";
			m_registerIP = "";
			m_registerDate = DateTime.Now;
			m_registerMachine = "";
		}
	}
}

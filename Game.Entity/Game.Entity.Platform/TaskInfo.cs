using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class TaskInfo
	{
		public const string Tablename = "TaskInfo";

		public const string _TaskID = "TaskID";

		public const string _TaskName = "TaskName";

		public const string _TaskDescription = "TaskDescription";

		public const string _TaskType = "TaskType";

		public const string _UserType = "UserType";

		public const string _KindID = "KindID";

		public const string _MatchID = "MatchID";

		public const string _Innings = "Innings";

		public const string _StandardAwardGold = "StandardAwardGold";

		public const string _StandardAwardMedal = "StandardAwardMedal";

		public const string _MemberAwardGold = "MemberAwardGold";

		public const string _MemberAwardMedal = "MemberAwardMedal";

		public const string _TimeLimit = "TimeLimit";

		public const string _InputDate = "InputDate";

		private int m_taskID;

		private string m_taskName;

		private string m_taskDescription;

		private int m_taskType;

		private byte m_userType;

		private int m_kindID;

		private int m_matchID;

		private int m_innings;

		private decimal m_standardAwardGold;

		private int m_standardAwardMedal;

		private int m_memberAwardGold;

		private int m_memberAwardMedal;

		private int m_timeLimit;

		private DateTime m_inputDate;

		public int TaskID
		{
			get
			{
				return m_taskID;
			}
			set
			{
				m_taskID = value;
			}
		}

		public string TaskName
		{
			get
			{
				return m_taskName;
			}
			set
			{
				m_taskName = value;
			}
		}

		public string TaskDescription
		{
			get
			{
				return m_taskDescription;
			}
			set
			{
				m_taskDescription = value;
			}
		}

		public int TaskType
		{
			get
			{
				return m_taskType;
			}
			set
			{
				m_taskType = value;
			}
		}

		public byte UserType
		{
			get
			{
				return m_userType;
			}
			set
			{
				m_userType = value;
			}
		}

		public int KindID
		{
			get
			{
				return m_kindID;
			}
			set
			{
				m_kindID = value;
			}
		}

		public int MatchID
		{
			get
			{
				return m_matchID;
			}
			set
			{
				m_matchID = value;
			}
		}

		public int Innings
		{
			get
			{
				return m_innings;
			}
			set
			{
				m_innings = value;
			}
		}

		public decimal StandardAwardGold
		{
			get
			{
				return m_standardAwardGold;
			}
			set
			{
				m_standardAwardGold = value;
			}
		}

		public int StandardAwardMedal
		{
			get
			{
				return m_standardAwardMedal;
			}
			set
			{
				m_standardAwardMedal = value;
			}
		}

		public int MemberAwardGold
		{
			get
			{
				return m_memberAwardGold;
			}
			set
			{
				m_memberAwardGold = value;
			}
		}

		public int MemberAwardMedal
		{
			get
			{
				return m_memberAwardMedal;
			}
			set
			{
				m_memberAwardMedal = value;
			}
		}

		public int TimeLimit
		{
			get
			{
				return m_timeLimit;
			}
			set
			{
				m_timeLimit = value;
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

		public TaskInfo()
		{
			m_taskID = 0;
			m_taskName = "";
			m_taskDescription = "";
			m_taskType = 0;
			m_userType = 0;
			m_kindID = 0;
			m_matchID = 0;
			m_innings = 0;
			m_standardAwardGold = 0m;
			m_standardAwardMedal = 0;
			m_memberAwardGold = 0;
			m_memberAwardMedal = 0;
			m_timeLimit = 0;
			m_inputDate = DateTime.Now;
		}
	}
}

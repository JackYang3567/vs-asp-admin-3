using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class RecordUserInout
	{
		public const string Tablename = "RecordUserInout";

		public const string _ID = "ID";

		public const string _UserID = "UserID";

		public const string _KindID = "KindID";

		public const string _ServerID = "ServerID";

		public const string _EnterTime = "EnterTime";

		public const string _EnterScore = "EnterScore";

		public const string _EnterGrade = "EnterGrade";

		public const string _EnterInsure = "EnterInsure";

		public const string _EnterUserMedal = "EnterUserMedal";

		public const string _EnterLoveliness = "EnterLoveliness";

		public const string _EnterMachine = "EnterMachine";

		public const string _EnterClientIP = "EnterClientIP";

		public const string _LeaveTime = "LeaveTime";

		public const string _LeaveReason = "LeaveReason";

		public const string _LeaveMachine = "LeaveMachine";

		public const string _LeaveClientIP = "LeaveClientIP";

		public const string _Score = "Score";

		public const string _Grade = "Grade";

		public const string _Insure = "Insure";

		public const string _Revenue = "Revenue";

		public const string _WinCount = "WinCount";

		public const string _LostCount = "LostCount";

		public const string _DrawCount = "DrawCount";

		public const string _FleeCount = "FleeCount";

		public const string _UserMedal = "UserMedal";

		public const string _LoveLiness = "LoveLiness";

		public const string _Experience = "Experience";

		public const string _PlayTimeCount = "PlayTimeCount";

		public const string _OnLineTimeCount = "OnLineTimeCount";

		private int m_iD;

		private int m_userID;

		private int m_kindID;

		private int m_serverID;

		private DateTime m_enterTime;

		private long m_enterScore;

		private long m_enterGrade;

		private long m_enterInsure;

		private int m_enterUserMedal;

		private int m_enterLoveliness;

		private string m_enterMachine;

		private string m_enterClientIP;

		private DateTime m_leaveTime;

		private int m_leaveReason;

		private string m_leaveMachine;

		private string m_leaveClientIP;

		private long m_score;

		private long m_grade;

		private long m_insure;

		private long m_revenue;

		private int m_winCount;

		private int m_lostCount;

		private int m_drawCount;

		private int m_fleeCount;

		private int m_userMedal;

		private int m_loveLiness;

		private int m_experience;

		private int m_playTimeCount;

		private int m_onLineTimeCount;

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

		public int ServerID
		{
			get
			{
				return m_serverID;
			}
			set
			{
				m_serverID = value;
			}
		}

		public DateTime EnterTime
		{
			get
			{
				return m_enterTime;
			}
			set
			{
				m_enterTime = value;
			}
		}

		public long EnterScore
		{
			get
			{
				return m_enterScore;
			}
			set
			{
				m_enterScore = value;
			}
		}

		public long EnterGrade
		{
			get
			{
				return m_enterGrade;
			}
			set
			{
				m_enterGrade = value;
			}
		}

		public long EnterInsure
		{
			get
			{
				return m_enterInsure;
			}
			set
			{
				m_enterInsure = value;
			}
		}

		public int EnterUserMedal
		{
			get
			{
				return m_enterUserMedal;
			}
			set
			{
				m_enterUserMedal = value;
			}
		}

		public int EnterLoveliness
		{
			get
			{
				return m_enterLoveliness;
			}
			set
			{
				m_enterLoveliness = value;
			}
		}

		public string EnterMachine
		{
			get
			{
				return m_enterMachine;
			}
			set
			{
				m_enterMachine = value;
			}
		}

		public string EnterClientIP
		{
			get
			{
				return m_enterClientIP;
			}
			set
			{
				m_enterClientIP = value;
			}
		}

		public DateTime LeaveTime
		{
			get
			{
				return m_leaveTime;
			}
			set
			{
				m_leaveTime = value;
			}
		}

		public int LeaveReason
		{
			get
			{
				return m_leaveReason;
			}
			set
			{
				m_leaveReason = value;
			}
		}

		public string LeaveMachine
		{
			get
			{
				return m_leaveMachine;
			}
			set
			{
				m_leaveMachine = value;
			}
		}

		public string LeaveClientIP
		{
			get
			{
				return m_leaveClientIP;
			}
			set
			{
				m_leaveClientIP = value;
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

		public long Grade
		{
			get
			{
				return m_grade;
			}
			set
			{
				m_grade = value;
			}
		}

		public long Insure
		{
			get
			{
				return m_insure;
			}
			set
			{
				m_insure = value;
			}
		}

		public long Revenue
		{
			get
			{
				return m_revenue;
			}
			set
			{
				m_revenue = value;
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

		public int UserMedal
		{
			get
			{
				return m_userMedal;
			}
			set
			{
				m_userMedal = value;
			}
		}

		public int LoveLiness
		{
			get
			{
				return m_loveLiness;
			}
			set
			{
				m_loveLiness = value;
			}
		}

		public int Experience
		{
			get
			{
				return m_experience;
			}
			set
			{
				m_experience = value;
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

		public RecordUserInout()
		{
			m_iD = 0;
			m_userID = 0;
			m_kindID = 0;
			m_serverID = 0;
			m_enterTime = DateTime.Now;
			m_enterScore = 0L;
			m_enterGrade = 0L;
			m_enterInsure = 0L;
			m_enterUserMedal = 0;
			m_enterLoveliness = 0;
			m_enterMachine = "";
			m_enterClientIP = "";
			m_leaveTime = DateTime.Now;
			m_leaveReason = 0;
			m_leaveMachine = "";
			m_leaveClientIP = "";
			m_score = 0L;
			m_grade = 0L;
			m_insure = 0L;
			m_revenue = 0L;
			m_winCount = 0;
			m_lostCount = 0;
			m_drawCount = 0;
			m_fleeCount = 0;
			m_userMedal = 0;
			m_loveLiness = 0;
			m_experience = 0;
			m_playTimeCount = 0;
			m_onLineTimeCount = 0;
		}
	}
}

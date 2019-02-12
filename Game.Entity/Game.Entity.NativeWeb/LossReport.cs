using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class LossReport
	{
		public const string Tablename = "LossReport";

		public const string _ReportID = "ReportID";

		public const string _ReportNo = "ReportNo";

		public const string _UserID = "UserID";

		public const string _GameID = "GameID";

		public const string _Accounts = "Accounts";

		public const string _ReportEmail = "ReportEmail";

		public const string _Compellation = "Compellation";

		public const string _PassportID = "PassportID";

		public const string _MobilePhone = "MobilePhone";

		public const string _FixedPhone = "FixedPhone";

		public const string _RegisterDate = "RegisterDate";

		public const string _OldNickName1 = "OldNickName1";

		public const string _OldNickName2 = "OldNickName2";

		public const string _OldNickName3 = "OldNickName3";

		public const string _OldLogonPass1 = "OldLogonPass1";

		public const string _OldLogonPass2 = "OldLogonPass2";

		public const string _OldLogonPass3 = "OldLogonPass3";

		public const string _OldQuestion1 = "OldQuestion1";

		public const string _OldResponse1 = "OldResponse1";

		public const string _OldQuestion2 = "OldQuestion2";

		public const string _OldResponse2 = "OldResponse2";

		public const string _OldQuestion3 = "OldQuestion3";

		public const string _OldResponse3 = "OldResponse3";

		public const string _SuppInfo = "SuppInfo";

		public const string _ProcessStatus = "ProcessStatus";

		public const string _SendCount = "SendCount";

		public const string _Random = "Random";

		public const string _SolveDate = "SolveDate";

		public const string _OverDate = "OverDate";

		public const string _ReportIP = "ReportIP";

		public const string _ReportDate = "ReportDate";

		private int m_reportID;

		private string m_reportNo;

		private int m_userID;

		private int m_gameID;

		private string m_accounts;

		private string m_reportEmail;

		private string m_compellation;

		private string m_passportID;

		private string m_mobilePhone;

		private string m_fixedPhone;

		private string m_registerDate;

		private string m_oldNickName1;

		private string m_oldNickName2;

		private string m_oldNickName3;

		private string m_oldLogonPass1;

		private string m_oldLogonPass2;

		private string m_oldLogonPass3;

		private string m_oldQuestion1;

		private string m_oldResponse1;

		private string m_oldQuestion2;

		private string m_oldResponse2;

		private string m_oldQuestion3;

		private string m_oldResponse3;

		private string m_suppInfo;

		private byte m_processStatus;

		private int m_sendCount;

		private string m_random;

		private DateTime m_solveDate;

		private DateTime m_overDate;

		private string m_reportIP;

		private DateTime m_reportDate;

		public int ReportID
		{
			get
			{
				return m_reportID;
			}
			set
			{
				m_reportID = value;
			}
		}

		public string ReportNo
		{
			get
			{
				return m_reportNo;
			}
			set
			{
				m_reportNo = value;
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

		public int GameID
		{
			get
			{
				return m_gameID;
			}
			set
			{
				m_gameID = value;
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

		public string ReportEmail
		{
			get
			{
				return m_reportEmail;
			}
			set
			{
				m_reportEmail = value;
			}
		}

		public string Compellation
		{
			get
			{
				return m_compellation;
			}
			set
			{
				m_compellation = value;
			}
		}

		public string PassportID
		{
			get
			{
				return m_passportID;
			}
			set
			{
				m_passportID = value;
			}
		}

		public string MobilePhone
		{
			get
			{
				return m_mobilePhone;
			}
			set
			{
				m_mobilePhone = value;
			}
		}

		public string FixedPhone
		{
			get
			{
				return m_fixedPhone;
			}
			set
			{
				m_fixedPhone = value;
			}
		}

		public string RegisterDate
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

		public string OldNickName1
		{
			get
			{
				return m_oldNickName1;
			}
			set
			{
				m_oldNickName1 = value;
			}
		}

		public string OldNickName2
		{
			get
			{
				return m_oldNickName2;
			}
			set
			{
				m_oldNickName2 = value;
			}
		}

		public string OldNickName3
		{
			get
			{
				return m_oldNickName3;
			}
			set
			{
				m_oldNickName3 = value;
			}
		}

		public string OldLogonPass1
		{
			get
			{
				return m_oldLogonPass1;
			}
			set
			{
				m_oldLogonPass1 = value;
			}
		}

		public string OldLogonPass2
		{
			get
			{
				return m_oldLogonPass2;
			}
			set
			{
				m_oldLogonPass2 = value;
			}
		}

		public string OldLogonPass3
		{
			get
			{
				return m_oldLogonPass3;
			}
			set
			{
				m_oldLogonPass3 = value;
			}
		}

		public string OldQuestion1
		{
			get
			{
				return m_oldQuestion1;
			}
			set
			{
				m_oldQuestion1 = value;
			}
		}

		public string OldResponse1
		{
			get
			{
				return m_oldResponse1;
			}
			set
			{
				m_oldResponse1 = value;
			}
		}

		public string OldQuestion2
		{
			get
			{
				return m_oldQuestion2;
			}
			set
			{
				m_oldQuestion2 = value;
			}
		}

		public string OldResponse2
		{
			get
			{
				return m_oldResponse2;
			}
			set
			{
				m_oldResponse2 = value;
			}
		}

		public string OldQuestion3
		{
			get
			{
				return m_oldQuestion3;
			}
			set
			{
				m_oldQuestion3 = value;
			}
		}

		public string OldResponse3
		{
			get
			{
				return m_oldResponse3;
			}
			set
			{
				m_oldResponse3 = value;
			}
		}

		public string SuppInfo
		{
			get
			{
				return m_suppInfo;
			}
			set
			{
				m_suppInfo = value;
			}
		}

		public byte ProcessStatus
		{
			get
			{
				return m_processStatus;
			}
			set
			{
				m_processStatus = value;
			}
		}

		public int SendCount
		{
			get
			{
				return m_sendCount;
			}
			set
			{
				m_sendCount = value;
			}
		}

		public string Random
		{
			get
			{
				return m_random;
			}
			set
			{
				m_random = value;
			}
		}

		public DateTime SolveDate
		{
			get
			{
				return m_solveDate;
			}
			set
			{
				m_solveDate = value;
			}
		}

		public DateTime OverDate
		{
			get
			{
				return m_overDate;
			}
			set
			{
				m_overDate = value;
			}
		}

		public string ReportIP
		{
			get
			{
				return m_reportIP;
			}
			set
			{
				m_reportIP = value;
			}
		}

		public DateTime ReportDate
		{
			get
			{
				return m_reportDate;
			}
			set
			{
				m_reportDate = value;
			}
		}

		public LossReport()
		{
			m_reportID = 0;
			m_reportNo = "";
			m_userID = 0;
			m_gameID = 0;
			m_accounts = "";
			m_reportEmail = "";
			m_compellation = "";
			m_passportID = "";
			m_mobilePhone = "";
			m_fixedPhone = "";
			m_registerDate = "";
			m_oldNickName1 = "";
			m_oldNickName2 = "";
			m_oldNickName3 = "";
			m_oldLogonPass1 = "";
			m_oldLogonPass2 = "";
			m_oldLogonPass3 = "";
			m_oldQuestion1 = "";
			m_oldResponse1 = "";
			m_oldQuestion2 = "";
			m_oldResponse2 = "";
			m_oldQuestion3 = "";
			m_oldResponse3 = "";
			m_suppInfo = "";
			m_processStatus = 0;
			m_sendCount = 0;
			m_random = "";
			m_solveDate = DateTime.Now;
			m_overDate = DateTime.Now;
			m_reportIP = "";
			m_reportDate = DateTime.Now;
		}
	}
}

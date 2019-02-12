using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class AccountsInfo
	{
		public const string Tablename = "AccountsInfo";

		public const string _UserID = "UserID";

		public const string _GameID = "GameID";

		public const string _ProtectID = "ProtectID";

		public const string _PasswordID = "PasswordID";

		public const string _SpreaderID = "SpreaderID";

		public const string _Accounts = "Accounts";

		public const string _NickName = "NickName";

		public const string _RegAccounts = "RegAccounts";

		public const string _UnderWrite = "UnderWrite";

		public const string _PassPortID = "PassPortID";

		public const string _Compellation = "Compellation";

		public const string _LogonPass = "LogonPass";

		public const string _InsurePass = "InsurePass";

		public const string _DynamicPass = "DynamicPass";

		public const string _DynamicPassTime = "DynamicPassTime";

		public const string _FaceID = "FaceID";

		public const string _CustomID = "CustomID";

		public const string _Present = "Present";

		public const string _UserMedal = "UserMedal";

		public const string _Experience = "Experience";

		public const string _GrowLevelID = "GrowLevelID";

		public const string _LoveLiness = "LoveLiness";

		public const string _UserRight = "UserRight";

		public const string _MasterRight = "MasterRight";

		public const string _ServiceRight = "ServiceRight";

		public const string _MasterOrder = "MasterOrder";

		public const string _MemberOrder = "MemberOrder";

		public const string _MemberOverDate = "MemberOverDate";

		public const string _MemberSwitchDate = "MemberSwitchDate";

		public const string _CustomFaceVer = "CustomFaceVer";

		public const string _Gender = "Gender";

		public const string _Nullity = "Nullity";

		public const string _NullityOverDate = "NullityOverDate";

		public const string _StunDown = "StunDown";

		public const string _MoorMachine = "MoorMachine";

		public const string _IsAndroid = "IsAndroid";

		public const string _WebLogonTimes = "WebLogonTimes";

		public const string _GameLogonTimes = "GameLogonTimes";

		public const string _PlayTimeCount = "PlayTimeCount";

		public const string _OnLineTimeCount = "OnLineTimeCount";

		public const string _LastLogonIP = "LastLogonIP";

		public const string _LastLogonDate = "LastLogonDate";

		public const string _LastLogonMobile = "LastLogonMobile";

		public const string _LastLogonMachine = "LastLogonMachine";

		public const string _RegisterIP = "RegisterIP";

		public const string _RegisterDate = "RegisterDate";

		public const string _RegisterMobile = "RegisterMobile";

		public const string _RegisterMachine = "RegisterMachine";

		public const string _UserUin = "UserUin";

		public const string _RankID = "RankID";

		public const string _AgentID = "AgentID";

		private int m_userID;

		private int m_gameID;

		private int m_protectID;

		private int m_passwordID;

		private int m_spreaderID;

		private string m_accounts;

		private string m_nickName;

		private string m_regAccounts;

		private string m_underWrite;

		private string m_passPortID;

		private string m_compellation;

		private string m_logonPass;

		private string m_insurePass;

		private string m_dynamicPass;

		private DateTime m_dynamicPassTime;

		private short m_faceID;

		private int m_customID;

		private int m_present;

		private int m_userMedal;

		private int m_experience;

		private int m_growLevelID;

		private int m_loveLiness;

		private int m_userRight;

		private int m_masterRight;

		private int m_serviceRight;

		private byte m_masterOrder;

		private byte m_memberOrder;

		private DateTime m_memberOverDate;

		private DateTime m_memberSwitchDate;

		private byte m_customFaceVer;

		private byte m_gender;

		private byte m_nullity;

		private DateTime m_nullityOverDate;

		private byte m_stunDown;

		private byte m_moorMachine;

		private byte m_isAndroid;

		private int m_webLogonTimes;

		private int m_gameLogonTimes;

		private int m_playTimeCount;

		private int m_onLineTimeCount;

		private string m_lastLogonIP;

		private DateTime m_lastLogonDate;

		private string m_lastLogonMobile;

		private string m_lastLogonMachine;

		private string m_registerIP;

		private DateTime m_registerDate;

		private string m_registerMobile;

		private string m_registerMachine;

		private byte m_registerOrigin;

		private long m_userUin;

		private int m_rankID;

		private int m_agentID;

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

		public int ProtectID
		{
			get
			{
				return m_protectID;
			}
			set
			{
				m_protectID = value;
			}
		}

		public int PasswordID
		{
			get
			{
				return m_passwordID;
			}
			set
			{
				m_passwordID = value;
			}
		}

		public int SpreaderID
		{
			get
			{
				return m_spreaderID;
			}
			set
			{
				m_spreaderID = value;
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

		public string NickName
		{
			get
			{
				return m_nickName;
			}
			set
			{
				m_nickName = value;
			}
		}

		public string RegAccounts
		{
			get
			{
				return m_regAccounts;
			}
			set
			{
				m_regAccounts = value;
			}
		}

		public string UnderWrite
		{
			get
			{
				return m_underWrite;
			}
			set
			{
				m_underWrite = value;
			}
		}

		public string PassPortID
		{
			get
			{
				return m_passPortID;
			}
			set
			{
				m_passPortID = value;
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

		public string LogonPass
		{
			get
			{
				return m_logonPass;
			}
			set
			{
				m_logonPass = value;
			}
		}

		public string InsurePass
		{
			get
			{
				return m_insurePass;
			}
			set
			{
				m_insurePass = value;
			}
		}

		public string DynamicPass
		{
			get
			{
				return m_dynamicPass;
			}
			set
			{
				m_dynamicPass = value;
			}
		}

		public DateTime DynamicPassTime
		{
			get
			{
				return m_dynamicPassTime;
			}
			set
			{
				m_dynamicPassTime = value;
			}
		}

		public short FaceID
		{
			get
			{
				return m_faceID;
			}
			set
			{
				m_faceID = value;
			}
		}

		public int CustomID
		{
			get
			{
				return m_customID;
			}
			set
			{
				m_customID = value;
			}
		}

		public int Present
		{
			get
			{
				return m_present;
			}
			set
			{
				m_present = value;
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

		public int GrowLevelID
		{
			get
			{
				return m_growLevelID;
			}
			set
			{
				m_growLevelID = value;
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

		public int ServiceRight
		{
			get
			{
				return m_serviceRight;
			}
			set
			{
				m_serviceRight = value;
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

		public byte MemberOrder
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

		public DateTime MemberOverDate
		{
			get
			{
				return m_memberOverDate;
			}
			set
			{
				m_memberOverDate = value;
			}
		}

		public DateTime MemberSwitchDate
		{
			get
			{
				return m_memberSwitchDate;
			}
			set
			{
				m_memberSwitchDate = value;
			}
		}

		public byte CustomFaceVer
		{
			get
			{
				return m_customFaceVer;
			}
			set
			{
				m_customFaceVer = value;
			}
		}

		public byte Gender
		{
			get
			{
				return m_gender;
			}
			set
			{
				m_gender = value;
			}
		}

		public byte Nullity
		{
			get
			{
				return m_nullity;
			}
			set
			{
				m_nullity = value;
			}
		}

		public DateTime NullityOverDate
		{
			get
			{
				return m_nullityOverDate;
			}
			set
			{
				m_nullityOverDate = value;
			}
		}

		public byte StunDown
		{
			get
			{
				return m_stunDown;
			}
			set
			{
				m_stunDown = value;
			}
		}

		public byte MoorMachine
		{
			get
			{
				return m_moorMachine;
			}
			set
			{
				m_moorMachine = value;
			}
		}

		public byte IsAndroid
		{
			get
			{
				return m_isAndroid;
			}
			set
			{
				m_isAndroid = value;
			}
		}

		public int WebLogonTimes
		{
			get
			{
				return m_webLogonTimes;
			}
			set
			{
				m_webLogonTimes = value;
			}
		}

		public int GameLogonTimes
		{
			get
			{
				return m_gameLogonTimes;
			}
			set
			{
				m_gameLogonTimes = value;
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

		public string LastLogonMobile
		{
			get
			{
				return m_lastLogonMobile;
			}
			set
			{
				m_lastLogonMobile = value;
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

		public string RegisterMobile
		{
			get
			{
				return m_registerMobile;
			}
			set
			{
				m_registerMobile = value;
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

		public byte RegisterOrigin
		{
			get
			{
				return m_registerOrigin;
			}
			set
			{
				m_registerOrigin = value;
			}
		}

		public long UserUin
		{
			get
			{
				return m_userUin;
			}
			set
			{
				m_userUin = value;
			}
		}

		public int RankID
		{
			get
			{
				return m_rankID;
			}
			set
			{
				m_rankID = value;
			}
		}

		public int AgentID
		{
			get
			{
				return m_agentID;
			}
			set
			{
				m_agentID = value;
			}
		}

		public int UserType
		{
			get;
			set;
		}

		public string LastLogonIPAddress
		{
			get;
			set;
		}

		public AccountsInfo()
		{
			m_userID = 0;
			m_gameID = 0;
			m_protectID = 0;
			m_passwordID = 0;
			m_spreaderID = 0;
			m_accounts = "";
			m_nickName = "";
			m_regAccounts = "";
			m_underWrite = "";
			m_passPortID = "";
			m_compellation = "";
			m_logonPass = "";
			m_insurePass = "";
			m_dynamicPass = "";
			m_dynamicPassTime = DateTime.Now;
			m_faceID = 0;
			m_customID = 0;
			m_present = 0;
			m_userMedal = 0;
			m_experience = 0;
			m_growLevelID = 0;
			m_loveLiness = 0;
			m_userRight = 0;
			m_masterRight = 0;
			m_serviceRight = 0;
			m_masterOrder = 0;
			m_memberOrder = 0;
			m_memberOverDate = DateTime.Now;
			m_memberSwitchDate = DateTime.Now;
			m_customFaceVer = 0;
			m_gender = 0;
			m_nullity = 0;
			m_nullityOverDate = DateTime.Now;
			m_stunDown = 0;
			m_moorMachine = 0;
			m_isAndroid = 0;
			m_webLogonTimes = 0;
			m_gameLogonTimes = 0;
			m_playTimeCount = 0;
			m_onLineTimeCount = 0;
			m_lastLogonIP = "";
			m_lastLogonDate = DateTime.Now;
			m_lastLogonMobile = "";
			m_lastLogonMachine = "";
			m_registerIP = "";
			m_registerDate = DateTime.Now;
			m_registerMobile = "";
			m_registerMachine = "";
			m_registerOrigin = 0;
			m_userUin = 0L;
			m_rankID = 0;
			m_agentID = 0;
		}
	}
}

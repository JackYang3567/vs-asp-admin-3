using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GameRoomInfo
	{
		public const string Tablename = "GameRoomInfo";

		public const string _ServerID = "ServerID";

		public const string _ServerName = "ServerName";

		public const string _KindID = "KindID";

		public const string _NodeID = "NodeID";

		public const string _SortID = "SortID";

		public const string _GameID = "GameID";

		public const string _TableCount = "TableCount";

		public const string _ServerKind = "ServerKind";

		public const string _ServerType = "ServerType";

		public const string _ServerPort = "ServerPort";

		public const string _ServerPasswd = "ServerPasswd";

		public const string _DataBaseName = "DataBaseName";

		public const string _DataBaseAddr = "DataBaseAddr";

		public const string _CellScore = "CellScore";

		public const string _RevenueRatio = "RevenueRatio";

		public const string _ServiceScore = "ServiceScore";

		public const string _RestrictScore = "RestrictScore";

		public const string _MinTableScore = "MinTableScore";

		public const string _MinEnterScore = "MinEnterScore";

		public const string _MaxEnterScore = "MaxEnterScore";

		public const string _MinEnterMember = "MinEnterMember";

		public const string _MaxEnterMember = "MaxEnterMember";

		public const string _MaxPlayer = "MaxPlayer";

		public const string _ServerRule = "ServerRule";

		public const string _DistributeRule = "DistributeRule";

		public const string _MinDistributeUser = "MinDistributeUser";

		public const string _DistributeTimeSpace = "DistributeTimeSpace";

		public const string _DistributeDrawCount = "DistributeDrawCount";

		public const string _MinPartakeGameUser = "MinPartakeGameUser";

		public const string _MaxPartakeGameUser = "MaxPartakeGameUser";

		public const string _AttachUserRight = "AttachUserRight";

		public const string _ServiceMachine = "ServiceMachine";

		public const string _CustomRule = "CustomRule";

		public const string _Nullity = "Nullity";

		public const string _ServerNote = "ServerNote";

		public const string _CreateDateTime = "CreateDateTime";

		public const string _ModifyDateTime = "ModifyDateTime";

		public const string _EnterPassword = "EnterPassword";

		private int m_serverID;

		private string m_serverName;

		private int m_kindID;

		private int m_nodeID;

		private int m_sortID;

		private int m_gameID;

		private int m_tableCount;

		private int m_serverKind;

		private int m_serverType;

		private int m_serverPort;

		private string m_serverPasswd;

		private string m_dataBaseName;

		private string m_dataBaseAddr;

		private long m_cellScore;

		private byte m_revenueRatio;

		private long m_serviceScore;

		private long m_restrictScore;

		private long m_minTableScore;

		private long m_minEnterScore;

		private long m_maxEnterScore;

		private int m_minEnterMember;

		private int m_maxEnterMember;

		private int m_maxPlayer;

		private int m_serverRule;

		private int m_distributeRule;

		private int m_minDistributeUser;

		private int m_distributeTimeSpace;

		private int m_distributeDrawCount;

		private int m_minPartakeGameUser;

		private int m_maxPartakeGameUser;

		private int m_attachUserRight;

		private string m_serviceMachine;

		private string m_customRule;

		private byte m_nullity;

		private string m_serverNote;

		private DateTime m_createDateTime;

		private DateTime m_modifyDateTime;

		private string m_enterPassword;

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

		public string ServerName
		{
			get
			{
				return m_serverName;
			}
			set
			{
				m_serverName = value;
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

		public int NodeID
		{
			get
			{
				return m_nodeID;
			}
			set
			{
				m_nodeID = value;
			}
		}

		public int SortID
		{
			get
			{
				return m_sortID;
			}
			set
			{
				m_sortID = value;
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

		public int TableCount
		{
			get
			{
				return m_tableCount;
			}
			set
			{
				m_tableCount = value;
			}
		}

		public int ServerKind
		{
			get
			{
				return m_serverKind;
			}
			set
			{
				m_serverKind = value;
			}
		}

		public int ServerType
		{
			get
			{
				return m_serverType;
			}
			set
			{
				m_serverType = value;
			}
		}

		public int ServerPort
		{
			get
			{
				return m_serverPort;
			}
			set
			{
				m_serverPort = value;
			}
		}

		public string ServerPasswd
		{
			get
			{
				return m_serverPasswd;
			}
			set
			{
				m_serverPasswd = value;
			}
		}

		public string DataBaseName
		{
			get
			{
				return m_dataBaseName;
			}
			set
			{
				m_dataBaseName = value;
			}
		}

		public string DataBaseAddr
		{
			get
			{
				return m_dataBaseAddr;
			}
			set
			{
				m_dataBaseAddr = value;
			}
		}

		public long CellScore
		{
			get
			{
				return m_cellScore;
			}
			set
			{
				m_cellScore = value;
			}
		}

		public byte RevenueRatio
		{
			get
			{
				return m_revenueRatio;
			}
			set
			{
				m_revenueRatio = value;
			}
		}

		public long ServiceScore
		{
			get
			{
				return m_serviceScore;
			}
			set
			{
				m_serviceScore = value;
			}
		}

		public long RestrictScore
		{
			get
			{
				return m_restrictScore;
			}
			set
			{
				m_restrictScore = value;
			}
		}

		public long MinTableScore
		{
			get
			{
				return m_minTableScore;
			}
			set
			{
				m_minTableScore = value;
			}
		}

		public long MinEnterScore
		{
			get
			{
				return m_minEnterScore;
			}
			set
			{
				m_minEnterScore = value;
			}
		}

		public long MaxEnterScore
		{
			get
			{
				return m_maxEnterScore;
			}
			set
			{
				m_maxEnterScore = value;
			}
		}

		public int MinEnterMember
		{
			get
			{
				return m_minEnterMember;
			}
			set
			{
				m_minEnterMember = value;
			}
		}

		public int MaxEnterMember
		{
			get
			{
				return m_maxEnterMember;
			}
			set
			{
				m_maxEnterMember = value;
			}
		}

		public int MaxPlayer
		{
			get
			{
				return m_maxPlayer;
			}
			set
			{
				m_maxPlayer = value;
			}
		}

		public int ServerRule
		{
			get
			{
				return m_serverRule;
			}
			set
			{
				m_serverRule = value;
			}
		}

		public int DistributeRule
		{
			get
			{
				return m_distributeRule;
			}
			set
			{
				m_distributeRule = value;
			}
		}

		public int MinDistributeUser
		{
			get
			{
				return m_minDistributeUser;
			}
			set
			{
				m_minDistributeUser = value;
			}
		}

		public int DistributeTimeSpace
		{
			get
			{
				return m_distributeTimeSpace;
			}
			set
			{
				m_distributeTimeSpace = value;
			}
		}

		public int DistributeDrawCount
		{
			get
			{
				return m_distributeDrawCount;
			}
			set
			{
				m_distributeDrawCount = value;
			}
		}

		public int MinPartakeGameUser
		{
			get
			{
				return m_minPartakeGameUser;
			}
			set
			{
				m_minPartakeGameUser = value;
			}
		}

		public int MaxPartakeGameUser
		{
			get
			{
				return m_maxPartakeGameUser;
			}
			set
			{
				m_maxPartakeGameUser = value;
			}
		}

		public int AttachUserRight
		{
			get
			{
				return m_attachUserRight;
			}
			set
			{
				m_attachUserRight = value;
			}
		}

		public string ServiceMachine
		{
			get
			{
				return m_serviceMachine;
			}
			set
			{
				m_serviceMachine = value;
			}
		}

		public string CustomRule
		{
			get
			{
				return m_customRule;
			}
			set
			{
				m_customRule = value;
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

		public string ServerNote
		{
			get
			{
				return m_serverNote;
			}
			set
			{
				m_serverNote = value;
			}
		}

		public DateTime CreateDateTime
		{
			get
			{
				return m_createDateTime;
			}
			set
			{
				m_createDateTime = value;
			}
		}

		public DateTime ModifyDateTime
		{
			get
			{
				return m_modifyDateTime;
			}
			set
			{
				m_modifyDateTime = value;
			}
		}

		public string EnterPassword
		{
			get
			{
				return m_enterPassword;
			}
			set
			{
				m_enterPassword = value;
			}
		}

		public GameRoomInfo()
		{
			m_serverID = 0;
			m_serverName = "";
			m_kindID = 0;
			m_nodeID = 0;
			m_sortID = 0;
			m_gameID = 0;
			m_tableCount = 0;
			m_serverKind = 0;
			m_serverType = 0;
			m_serverPort = 0;
			m_serverPasswd = "";
			m_dataBaseName = "";
			m_dataBaseAddr = "";
			m_cellScore = 0L;
			m_revenueRatio = 0;
			m_serviceScore = 0L;
			m_restrictScore = 0L;
			m_minTableScore = 0L;
			m_minEnterScore = 0L;
			m_maxEnterScore = 0L;
			m_minEnterMember = 0;
			m_maxEnterMember = 0;
			m_maxPlayer = 0;
			m_serverRule = 0;
			m_distributeRule = 0;
			m_minDistributeUser = 0;
			m_distributeTimeSpace = 0;
			m_distributeDrawCount = 0;
			m_minPartakeGameUser = 0;
			m_maxPartakeGameUser = 0;
			m_attachUserRight = 0;
			m_serviceMachine = "";
			m_customRule = "";
			m_nullity = 0;
			m_serverNote = "";
			m_createDateTime = DateTime.Now;
			m_modifyDateTime = DateTime.Now;
			m_enterPassword = "";
		}
	}
}

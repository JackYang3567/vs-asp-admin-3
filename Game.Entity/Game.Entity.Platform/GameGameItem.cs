using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GameGameItem
	{
		public const string Tablename = "GameGameItem";

		public const string _GameID = "GameID";

		public const string _GameName = "GameName";

		public const string _SuportType = "SuportType";

		public const string _DataBaseAddr = "DataBaseAddr";

		public const string _DataBaseName = "DataBaseName";

		public const string _ServerVersion = "ServerVersion";

		public const string _ClientVersion = "ClientVersion";

		public const string _ServerDLLName = "ServerDLLName";

		public const string _ClientExeName = "ClientExeName";

		private int m_gameID;

		private string m_gameName;

		private int m_suportType;

		private string m_dataBaseAddr;

		private string m_dataBaseName;

		private int m_serverVersion;

		private int m_clientVersion;

		private string m_serverDLLName;

		private string m_clientExeName;

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

		public string GameName
		{
			get
			{
				return m_gameName;
			}
			set
			{
				m_gameName = value;
			}
		}

		public int SuportType
		{
			get
			{
				return m_suportType;
			}
			set
			{
				m_suportType = value;
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

		public int ServerVersion
		{
			get
			{
				return m_serverVersion;
			}
			set
			{
				m_serverVersion = value;
			}
		}

		public int ClientVersion
		{
			get
			{
				return m_clientVersion;
			}
			set
			{
				m_clientVersion = value;
			}
		}

		public string ServerDLLName
		{
			get
			{
				return m_serverDLLName;
			}
			set
			{
				m_serverDLLName = value;
			}
		}

		public string ClientExeName
		{
			get
			{
				return m_clientExeName;
			}
			set
			{
				m_clientExeName = value;
			}
		}

		public GameGameItem()
		{
			m_gameID = 0;
			m_gameName = "";
			m_suportType = 0;
			m_dataBaseAddr = "";
			m_dataBaseName = "";
			m_serverVersion = 0;
			m_clientVersion = 0;
			m_serverDLLName = "";
			m_clientExeName = "";
		}
	}
}

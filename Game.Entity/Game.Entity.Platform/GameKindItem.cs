using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GameKindItem
	{
		public const string Tablename = "GameKindItem";

		public const string _KindID = "KindID";

		public const string _GameID = "GameID";

		public const string _TypeID = "TypeID";

		public const string _JoinID = "JoinID";

		public const string _SortID = "SortID";

		public const string _KindName = "KindName";

		public const string _ProcessName = "ProcessName";

		public const string _GameRuleUrl = "GameRuleUrl";

		public const string _DownLoadUrl = "DownLoadUrl";

		public const string _Recommend = "Recommend";

		public const string _GameFlag = "GameFlag";

		public const string _Nullity = "Nullity";

		private int m_kindID;

		private int m_gameID;

		private int m_typeID;

		private int m_joinID;

		private int m_sortID;

		private string m_kindName;

		private string m_processName;

		private string m_gameRuleUrl;

		private string m_downLoadUrl;

		private int m_recommend;

		private int m_gameFlag;

		private byte m_nullity;

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

		public int TypeID
		{
			get
			{
				return m_typeID;
			}
			set
			{
				m_typeID = value;
			}
		}

		public int JoinID
		{
			get
			{
				return m_joinID;
			}
			set
			{
				m_joinID = value;
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

		public string KindName
		{
			get
			{
				return m_kindName;
			}
			set
			{
				m_kindName = value;
			}
		}

		public string ProcessName
		{
			get
			{
				return m_processName;
			}
			set
			{
				m_processName = value;
			}
		}

		public string GameRuleUrl
		{
			get
			{
				return m_gameRuleUrl;
			}
			set
			{
				m_gameRuleUrl = value;
			}
		}

		public string DownLoadUrl
		{
			get
			{
				return m_downLoadUrl;
			}
			set
			{
				m_downLoadUrl = value;
			}
		}

		public int Recommend
		{
			get
			{
				return m_recommend;
			}
			set
			{
				m_recommend = value;
			}
		}

		public int GameFlag
		{
			get
			{
				return m_gameFlag;
			}
			set
			{
				m_gameFlag = value;
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

		public GameKindItem()
		{
			m_kindID = 0;
			m_gameID = 0;
			m_typeID = 0;
			m_joinID = 0;
			m_sortID = 0;
			m_kindName = "";
			m_processName = "";
			m_gameRuleUrl = "";
			m_downLoadUrl = "";
			m_recommend = 0;
			m_gameFlag = 0;
			m_nullity = 0;
		}
	}
}

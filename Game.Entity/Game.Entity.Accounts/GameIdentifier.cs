using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class GameIdentifier
	{
		public const string Tablename = "GameIdentifier";

		public const string _UserID = "UserID";

		public const string _GameID = "GameID";

		public const string _IDLevel = "IDLevel";

		private int m_userID;

		private int m_gameID;

		private int m_iDLevel;

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

		public int IDLevel
		{
			get
			{
				return m_iDLevel;
			}
			set
			{
				m_iDLevel = value;
			}
		}

		public GameIdentifier()
		{
			m_userID = 0;
			m_gameID = 0;
			m_iDLevel = 0;
		}
	}
}

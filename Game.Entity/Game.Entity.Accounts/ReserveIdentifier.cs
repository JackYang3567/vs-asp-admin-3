using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class ReserveIdentifier
	{
		public const string Tablename = "ReserveIdentifier";

		public const string _GameID = "GameID";

		public const string _IDLevel = "IDLevel";

		public const string _Distribute = "Distribute";

		private int m_gameID;

		private int m_iDLevel;

		private bool m_distribute;

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

		public bool Distribute
		{
			get
			{
				return m_distribute;
			}
			set
			{
				m_distribute = value;
			}
		}

		public ReserveIdentifier()
		{
			m_gameID = 0;
			m_iDLevel = 0;
			m_distribute = false;
		}
	}
}

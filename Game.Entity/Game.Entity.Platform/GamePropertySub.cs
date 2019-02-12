using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GamePropertySub
	{
		public const string Tablename = "GamePropertySub";

		public const string _ID = "ID";

		public const string _OwnerID = "OwnerID";

		public const string _Count = "Count";

		public const string _SortID = "SortID";

		private int m_iD;

		private int m_ownerID;

		private int m_count;

		private int m_sortID;

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

		public int OwnerID
		{
			get
			{
				return m_ownerID;
			}
			set
			{
				m_ownerID = value;
			}
		}

		public int Count
		{
			get
			{
				return m_count;
			}
			set
			{
				m_count = value;
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

		public GamePropertySub()
		{
			m_iD = 0;
			m_ownerID = 0;
			m_count = 0;
			m_sortID = 0;
		}
	}
}

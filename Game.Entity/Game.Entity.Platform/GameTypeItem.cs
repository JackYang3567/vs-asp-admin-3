using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GameTypeItem
	{
		public const string Tablename = "GameTypeItem";

		public const string _TypeID = "TypeID";

		public const string _JoinID = "JoinID";

		public const string _SortID = "SortID";

		public const string _TypeName = "TypeName";

		public const string _Nullity = "Nullity";

		private int m_typeID;

		private int m_joinID;

		private int m_sortID;

		private string m_typeName;

		private byte m_nullity;

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

		public string TypeName
		{
			get
			{
				return m_typeName;
			}
			set
			{
				m_typeName = value;
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

		public GameTypeItem()
		{
			m_typeID = 0;
			m_joinID = 0;
			m_sortID = 0;
			m_typeName = "";
			m_nullity = 0;
		}
	}
}

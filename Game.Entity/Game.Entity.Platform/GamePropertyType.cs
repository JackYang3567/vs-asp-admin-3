using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GamePropertyType
	{
		public const string Tablename = "GamePropertyType";

		public const string _TypeID = "TypeID";

		public const string _SortID = "SortID";

		public const string _TypeName = "TypeName";

		public const string _TagID = "TagID";

		public const string _Nullity = "Nullity";

		private int m_typeID;

		private int m_sortID;

		private string m_typeName;

		private int m_tagID;

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

		public int TagID
		{
			get
			{
				return m_tagID;
			}
			set
			{
				m_tagID = value;
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

		public GamePropertyType()
		{
			m_typeID = 0;
			m_sortID = 0;
			m_typeName = "";
			m_tagID = 0;
			m_nullity = 0;
		}
	}
}

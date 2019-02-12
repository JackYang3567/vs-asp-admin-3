using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class AwardType
	{
		public const string Tablename = "AwardType";

		public const string _TypeID = "TypeID";

		public const string _ParentID = "ParentID";

		public const string _TypeName = "TypeName";

		public const string _SortID = "SortID";

		public const string _Nullity = "Nullity";

		public const string _CollectDate = "CollectDate";

		private int m_typeID;

		private int m_parentID;

		private string m_typeName;

		private int m_sortID;

		private byte m_nullity;

		private DateTime m_collectDate;

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

		public int ParentID
		{
			get
			{
				return m_parentID;
			}
			set
			{
				m_parentID = value;
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

		public DateTime CollectDate
		{
			get
			{
				return m_collectDate;
			}
			set
			{
				m_collectDate = value;
			}
		}

		public AwardType()
		{
			m_typeID = 0;
			m_parentID = 0;
			m_typeName = "";
			m_sortID = 0;
			m_nullity = 0;
			m_collectDate = DateTime.Now;
		}
	}
}

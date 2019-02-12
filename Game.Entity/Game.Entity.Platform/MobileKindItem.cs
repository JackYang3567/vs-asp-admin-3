using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class MobileKindItem
	{
		public const string Tablename = "MobileKindItem";

		public const string _KindID = "KindID";

		public const string _KindName = "KindName";

		public const string _TypeID = "TypeID";

		public const string _ModuleName = "ModuleName";

		public const string _ClientVersion = "ClientVersion";

		public const string _ResVersion = "ResVersion";

		public const string _SortID = "SortID";

		public const string _KindMark = "KindMark";

		public const string _KindType = "KindType";

		public const string _Nullity = "Nullity";

		private int m_kindID;

		private string m_kindName;

		private int m_typeID;

		private string m_moduleName;

		private int m_clientVersion;

		private int m_resVersion;

		private int m_sortID;

		private int m_kindMark;

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

		public string ModuleName
		{
			get
			{
				return m_moduleName;
			}
			set
			{
				m_moduleName = value;
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

		public int ResVersion
		{
			get
			{
				return m_resVersion;
			}
			set
			{
				m_resVersion = value;
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

		public int KindMark
		{
			get
			{
				return m_kindMark;
			}
			set
			{
				m_kindMark = value;
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

		public int KindType
		{
			get;
			set;
		}

		public MobileKindItem()
		{
			m_kindID = 0;
			m_kindName = "";
			m_typeID = 0;
			m_moduleName = "";
			m_clientVersion = 0;
			m_resVersion = 0;
			m_sortID = 0;
			m_kindMark = 0;
			m_nullity = 0;
		}
	}
}

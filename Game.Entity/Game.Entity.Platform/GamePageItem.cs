using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GamePageItem
	{
		public const string Tablename = "GamePageItem";

		public const string _PageID = "PageID";

		public const string _KindID = "KindID";

		public const string _NodeID = "NodeID";

		public const string _SortID = "SortID";

		public const string _OperateType = "OperateType";

		public const string _DisplayName = "DisplayName";

		public const string _ResponseUrl = "ResponseUrl";

		public const string _Nullity = "Nullity";

		private int m_pageID;

		private int m_kindID;

		private int m_nodeID;

		private int m_sortID;

		private int m_operateType;

		private string m_displayName;

		private string m_responseUrl;

		private byte m_nullity;

		public int PageID
		{
			get
			{
				return m_pageID;
			}
			set
			{
				m_pageID = value;
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

		public int OperateType
		{
			get
			{
				return m_operateType;
			}
			set
			{
				m_operateType = value;
			}
		}

		public string DisplayName
		{
			get
			{
				return m_displayName;
			}
			set
			{
				m_displayName = value;
			}
		}

		public string ResponseUrl
		{
			get
			{
				return m_responseUrl;
			}
			set
			{
				m_responseUrl = value;
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

		public GamePageItem()
		{
			m_pageID = 0;
			m_kindID = 0;
			m_nodeID = 0;
			m_sortID = 0;
			m_operateType = 0;
			m_displayName = "";
			m_responseUrl = "";
			m_nullity = 0;
		}
	}
}

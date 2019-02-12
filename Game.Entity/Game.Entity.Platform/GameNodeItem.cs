using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GameNodeItem
	{
		public const string Tablename = "GameNodeItem";

		public const string _NodeID = "NodeID";

		public const string _KindID = "KindID";

		public const string _JoinID = "JoinID";

		public const string _SortID = "SortID";

		public const string _NodeName = "NodeName";

		public const string _Nullity = "Nullity";

		private int m_nodeID;

		private int m_kindID;

		private int m_joinID;

		private int m_sortID;

		private string m_nodeName;

		private byte m_nullity;

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

		public string NodeName
		{
			get
			{
				return m_nodeName;
			}
			set
			{
				m_nodeName = value;
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

		public GameNodeItem()
		{
			m_nodeID = 0;
			m_kindID = 0;
			m_joinID = 0;
			m_sortID = 0;
			m_nodeName = "";
			m_nullity = 0;
		}
	}
}

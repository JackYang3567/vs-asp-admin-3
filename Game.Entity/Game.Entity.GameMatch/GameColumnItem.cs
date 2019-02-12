using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class GameColumnItem
	{
		public const string Tablename = "GameColumnItem";

		public const string _SortID = "SortID";

		public const string _ColumnName = "ColumnName";

		public const string _ColumnWidth = "ColumnWidth";

		public const string _DataDescribe = "DataDescribe";

		private int m_sortID;

		private string m_columnName;

		private byte m_columnWidth;

		private byte m_dataDescribe;

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

		public string ColumnName
		{
			get
			{
				return m_columnName;
			}
			set
			{
				m_columnName = value;
			}
		}

		public byte ColumnWidth
		{
			get
			{
				return m_columnWidth;
			}
			set
			{
				m_columnWidth = value;
			}
		}

		public byte DataDescribe
		{
			get
			{
				return m_dataDescribe;
			}
			set
			{
				m_dataDescribe = value;
			}
		}

		public GameColumnItem()
		{
			m_sortID = 0;
			m_columnName = "";
			m_columnWidth = 0;
			m_dataDescribe = 0;
		}
	}
}

namespace Game.Kernel
{
	public class PagerParameters
	{
		private bool m_ascending;

		private int m_cacherSize;

		private string[] m_fieldAlias;

		private string[] m_fields;

		private int m_pageIndex;

		private int m_pageSize;

		private string m_pkey;

		private string m_table;

		private string m_whereStr;

		public bool Ascending
		{
			get
			{
				return m_ascending;
			}
			set
			{
				m_ascending = value;
			}
		}

		public int CacherSize
		{
			get
			{
				return m_cacherSize;
			}
			set
			{
				m_cacherSize = value;
			}
		}

		public string[] FieldAlias
		{
			get
			{
				return m_fieldAlias;
			}
			set
			{
				m_fieldAlias = value;
			}
		}

		public string[] Fields
		{
			get
			{
				return m_fields;
			}
			set
			{
				m_fields = value;
			}
		}

		public int PageIndex
		{
			get
			{
				return m_pageIndex;
			}
			set
			{
				m_pageIndex = value;
			}
		}

		public int PageSize
		{
			get
			{
				return m_pageSize;
			}
			set
			{
				m_pageSize = value;
			}
		}

		public string PKey
		{
			get
			{
				return m_pkey;
			}
			set
			{
				m_pkey = value;
			}
		}

		public string Table
		{
			get
			{
				return m_table;
			}
			set
			{
				m_table = value;
			}
		}

		public string WhereStr
		{
			get
			{
				return m_whereStr;
			}
			set
			{
				m_whereStr = value;
			}
		}

		public PagerParameters()
		{
			m_ascending = true;
			m_pageIndex = 1;
			m_pageSize = 100;
			m_cacherSize = 0;
			m_pkey = "";
			m_whereStr = "";
			m_table = "";
		}

		public PagerParameters(string table, string pkey, int pageIndex)
		{
			m_ascending = true;
			m_pageSize = 20;
			m_cacherSize = 0;
			m_table = table;
			m_pkey = pkey;
			m_pageIndex = pageIndex;
		}

		public PagerParameters(string table, string pkey, int pageIndex, int pageSize)
			: this(table, pkey, pageIndex)
		{
			m_pageSize = pageSize;
		}

		public PagerParameters(string table, string pkey, int pageIndex, string whereStr)
			: this(table, pkey, pageIndex)
		{
			m_whereStr = whereStr;
		}

		public PagerParameters(string table, string pkey, string whereStr, int pageIndex, int pageSize)
			: this(table, pkey, pageIndex, whereStr)
		{
			m_pageSize = pageSize;
		}

		public PagerParameters(string table, string pkey, string whereStr, int pageIndex, int pageSize, string[] fields)
			: this(table, pkey, whereStr, pageIndex, pageSize)
		{
			Fields = fields;
		}

		public PagerParameters(string table, string pkey, string whereStr, int pageIndex, int pageSize, string[] fields, string[] fieldAlias)
			: this(table, pkey, whereStr, pageIndex, pageSize, fields)
		{
			FieldAlias = fieldAlias;
		}
	}
}

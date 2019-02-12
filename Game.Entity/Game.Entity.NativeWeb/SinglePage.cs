using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class SinglePage
	{
		public const string Tablename = "SinglePage";

		public const string _PageID = "PageID";

		public const string _KeyValue = "KeyValue";

		public const string _PageName = "PageName";

		public const string _KeyWords = "KeyWords";

		public const string _Description = "Description";

		public const string _Contents = "Contents";

		private int m_pageID;

		private string m_keyValue;

		private string m_pageName;

		private string m_keyWords;

		private string m_description;

		private string m_contents;

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

		public string KeyValue
		{
			get
			{
				return m_keyValue;
			}
			set
			{
				m_keyValue = value;
			}
		}

		public string PageName
		{
			get
			{
				return m_pageName;
			}
			set
			{
				m_pageName = value;
			}
		}

		public string KeyWords
		{
			get
			{
				return m_keyWords;
			}
			set
			{
				m_keyWords = value;
			}
		}

		public string Description
		{
			get
			{
				return m_description;
			}
			set
			{
				m_description = value;
			}
		}

		public string Contents
		{
			get
			{
				return m_contents;
			}
			set
			{
				m_contents = value;
			}
		}

		public SinglePage()
		{
			m_pageID = 0;
			m_keyValue = "";
			m_pageName = "";
			m_keyWords = "";
			m_description = "";
			m_contents = "";
		}
	}
}

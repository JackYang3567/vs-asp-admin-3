using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class ConfineContent
	{
		public const string Tablename = "ConfineContent";

		public const string _ContentID = "ContentID";

		public const string _String = "String";

		public const string _EnjoinOverDate = "EnjoinOverDate";

		public const string _CollectDate = "CollectDate";

		private int m_contentID;

		private string m_string;

		private DateTime? m_enjoinOverDate;

		private DateTime m_collectDate;

		public int ContentID
		{
			get
			{
				return m_contentID;
			}
			set
			{
				m_contentID = value;
			}
		}

		public string String
		{
			get
			{
				return m_string;
			}
			set
			{
				m_string = value;
			}
		}

		public DateTime? EnjoinOverDate
		{
			get
			{
				return m_enjoinOverDate;
			}
			set
			{
				m_enjoinOverDate = value;
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

		public ConfineContent()
		{
			m_contentID = 0;
			m_string = "";
			m_enjoinOverDate = null;
			m_collectDate = DateTime.Now;
		}
	}
}

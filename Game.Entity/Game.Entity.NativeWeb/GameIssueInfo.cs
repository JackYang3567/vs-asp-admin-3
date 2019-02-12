using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class GameIssueInfo
	{
		public const string Tablename = "GameIssueInfo";

		public const string _IssueID = "IssueID";

		public const string _IssueTitle = "IssueTitle";

		public const string _IssueContent = "IssueContent";

		public const string _TypeID = "TypeID";

		public const string _Nullity = "Nullity";

		public const string _CollectDate = "CollectDate";

		public const string _ModifyDate = "ModifyDate";

		private int m_issueID;

		private string m_issueTitle;

		private string m_issueContent;

		private int m_typeID;

		private byte m_nullity;

		private DateTime m_collectDate;

		private DateTime m_modifyDate;

		public int IssueID
		{
			get
			{
				return m_issueID;
			}
			set
			{
				m_issueID = value;
			}
		}

		public string IssueTitle
		{
			get
			{
				return m_issueTitle;
			}
			set
			{
				m_issueTitle = value;
			}
		}

		public string IssueContent
		{
			get
			{
				return m_issueContent;
			}
			set
			{
				m_issueContent = value;
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

		public DateTime ModifyDate
		{
			get
			{
				return m_modifyDate;
			}
			set
			{
				m_modifyDate = value;
			}
		}

		public int Hot
		{
			get;
			set;
		}

		public GameIssueInfo()
		{
			m_issueID = 0;
			m_issueTitle = "";
			m_issueContent = "";
			m_typeID = 0;
			m_nullity = 0;
			m_collectDate = DateTime.Now;
			m_modifyDate = DateTime.Now;
		}
	}
}

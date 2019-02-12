using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class News
	{
		public const string Tablename = "News";

		public const string _NewsID = "NewsID";

		public const string _PopID = "PopID";

		public const string _Subject = "Subject";

		public const string _Subject1 = "Subject1";

		public const string _OnTop = "OnTop";

		public const string _OnTopAll = "OnTopAll";

		public const string _IsElite = "IsElite";

		public const string _IsHot = "IsHot";

		public const string _IsLock = "IsLock";

		public const string _IsDelete = "IsDelete";

		public const string _IsLinks = "IsLinks";

		public const string _LinkUrl = "LinkUrl";

		public const string _Body = "Body";

		public const string _FormattedBody = "FormattedBody";

		public const string _HighLight = "HighLight";

		public const string _ClassID = "ClassID";

		public const string _GameRange = "GameRange";

		public const string _ImageUrl = "ImageUrl";

		public const string _UserID = "UserID";

		public const string _IssueIP = "IssueIP";

		public const string _LastModifyIP = "LastModifyIP";

		public const string _IssueDate = "IssueDate";

		public const string _LastModifyDate = "LastModifyDate";

		private int m_newsID;

		private int m_popID;

		private string m_subject;

		private string m_subject1;

		private byte m_onTop;

		private byte m_onTopAll;

		private byte m_isElite;

		private byte m_isHot;

		private byte m_isLock;

		private byte m_isDelete;

		private byte m_isLinks;

		private string m_linkUrl;

		private string m_body;

		private string m_formattedBody;

		private string m_highLight;

		private byte m_classID;

		private string m_gameRange;

		private string m_imageUrl;

		private int m_userID;

		private string m_issueIP;

		private string m_lastModifyIP;

		private DateTime m_issueDate;

		private DateTime m_lastModifyDate;

		public int NewsID
		{
			get
			{
				return m_newsID;
			}
			set
			{
				m_newsID = value;
			}
		}

		public int PopID
		{
			get
			{
				return m_popID;
			}
			set
			{
				m_popID = value;
			}
		}

		public string Subject
		{
			get
			{
				return m_subject;
			}
			set
			{
				m_subject = value;
			}
		}

		public string Subject1
		{
			get
			{
				return m_subject1;
			}
			set
			{
				m_subject1 = value;
			}
		}

		public byte OnTop
		{
			get
			{
				return m_onTop;
			}
			set
			{
				m_onTop = value;
			}
		}

		public byte OnTopAll
		{
			get
			{
				return m_onTopAll;
			}
			set
			{
				m_onTopAll = value;
			}
		}

		public byte IsElite
		{
			get
			{
				return m_isElite;
			}
			set
			{
				m_isElite = value;
			}
		}

		public byte IsHot
		{
			get
			{
				return m_isHot;
			}
			set
			{
				m_isHot = value;
			}
		}

		public byte IsLock
		{
			get
			{
				return m_isLock;
			}
			set
			{
				m_isLock = value;
			}
		}

		public byte IsDelete
		{
			get
			{
				return m_isDelete;
			}
			set
			{
				m_isDelete = value;
			}
		}

		public byte IsLinks
		{
			get
			{
				return m_isLinks;
			}
			set
			{
				m_isLinks = value;
			}
		}

		public string LinkUrl
		{
			get
			{
				return m_linkUrl;
			}
			set
			{
				m_linkUrl = value;
			}
		}

		public string Body
		{
			get
			{
				return m_body;
			}
			set
			{
				m_body = value;
			}
		}

		public string FormattedBody
		{
			get
			{
				return m_formattedBody;
			}
			set
			{
				m_formattedBody = value;
			}
		}

		public string HighLight
		{
			get
			{
				return m_highLight;
			}
			set
			{
				m_highLight = value;
			}
		}

		public byte ClassID
		{
			get
			{
				return m_classID;
			}
			set
			{
				m_classID = value;
			}
		}

		public string GameRange
		{
			get
			{
				return m_gameRange;
			}
			set
			{
				m_gameRange = value;
			}
		}

		public string ImageUrl
		{
			get
			{
				return m_imageUrl;
			}
			set
			{
				m_imageUrl = value;
			}
		}

		public int UserID
		{
			get
			{
				return m_userID;
			}
			set
			{
				m_userID = value;
			}
		}

		public string IssueIP
		{
			get
			{
				return m_issueIP;
			}
			set
			{
				m_issueIP = value;
			}
		}

		public string LastModifyIP
		{
			get
			{
				return m_lastModifyIP;
			}
			set
			{
				m_lastModifyIP = value;
			}
		}

		public DateTime IssueDate
		{
			get
			{
				return m_issueDate;
			}
			set
			{
				m_issueDate = value;
			}
		}

		public DateTime LastModifyDate
		{
			get
			{
				return m_lastModifyDate;
			}
			set
			{
				m_lastModifyDate = value;
			}
		}

		public News()
		{
			m_newsID = 0;
			m_popID = 0;
			m_subject = "";
			m_subject1 = "";
			m_onTop = 0;
			m_onTopAll = 0;
			m_isElite = 0;
			m_isHot = 0;
			m_isLock = 0;
			m_isDelete = 0;
			m_isLinks = 0;
			m_linkUrl = "";
			m_body = "";
			m_formattedBody = "";
			m_highLight = "";
			m_classID = 0;
			m_gameRange = "";
			m_imageUrl = "";
			m_userID = 0;
			m_issueIP = "";
			m_lastModifyIP = "";
			m_issueDate = DateTime.Now;
			m_lastModifyDate = DateTime.Now;
		}
	}
}

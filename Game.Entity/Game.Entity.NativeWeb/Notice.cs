using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class Notice
	{
		public const string Tablename = "Notice";

		public const string _NoticeID = "NoticeID";

		public const string _Subject = "Subject";

		public const string _IsLink = "IsLink";

		public const string _LinkUrl = "LinkUrl";

		public const string _Body = "Body";

		public const string _Location = "Location";

		public const string _Width = "Width";

		public const string _Height = "Height";

		public const string _StartDate = "StartDate";

		public const string _OverDate = "OverDate";

		public const string _Nullity = "Nullity";

		private int m_noticeID;

		private string m_subject;

		private byte m_isLink;

		private string m_linkUrl;

		private string m_body;

		private string m_location;

		private int m_width;

		private int m_height;

		private DateTime m_startDate;

		private DateTime m_overDate;

		private byte m_nullity;

		public int NoticeID
		{
			get
			{
				return m_noticeID;
			}
			set
			{
				m_noticeID = value;
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

		public byte IsLink
		{
			get
			{
				return m_isLink;
			}
			set
			{
				m_isLink = value;
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

		public string Location
		{
			get
			{
				return m_location;
			}
			set
			{
				m_location = value;
			}
		}

		public int Width
		{
			get
			{
				return m_width;
			}
			set
			{
				m_width = value;
			}
		}

		public int Height
		{
			get
			{
				return m_height;
			}
			set
			{
				m_height = value;
			}
		}

		public DateTime StartDate
		{
			get
			{
				return m_startDate;
			}
			set
			{
				m_startDate = value;
			}
		}

		public DateTime OverDate
		{
			get
			{
				return m_overDate;
			}
			set
			{
				m_overDate = value;
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

		public Notice()
		{
			m_noticeID = 0;
			m_subject = "";
			m_isLink = 0;
			m_linkUrl = "";
			m_body = "";
			m_location = "";
			m_width = 0;
			m_height = 0;
			m_startDate = DateTime.Now;
			m_overDate = DateTime.Now;
			m_nullity = 0;
		}
	}
}

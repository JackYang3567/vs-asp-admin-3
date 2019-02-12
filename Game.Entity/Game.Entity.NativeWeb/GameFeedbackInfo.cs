using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class GameFeedbackInfo
	{
		public const string Tablename = "GameFeedbackInfo";

		public const string _FeedbackID = "FeedbackID";

		public const string _FeedbackTitle = "FeedbackTitle";

		public const string _FeedbackContent = "FeedbackContent";

		public const string _FeedbackDate = "FeedbackDate";

		public const string _UserID = "UserID";

		public const string _ClientIP = "ClientIP";

		public const string _ViewCount = "ViewCount";

		public const string _RevertUserID = "RevertUserID";

		public const string _RevertContent = "RevertContent";

		public const string _RevertDate = "RevertDate";

		public const string _Nullity = "Nullity";

		public const string _IsProcessed = "IsProcessed";

		private int m_feedbackID;

		private string m_feedbackTitle;

		private string m_feedbackContent;

		private DateTime m_feedbackDate;

		private int m_userID;

		private string m_clientIP;

		private int m_viewCount;

		private int m_revertUserID;

		private string m_revertContent;

		private DateTime m_revertDate;

		private byte m_nullity;

		private byte m_isProcessed;

		public int FeedbackID
		{
			get
			{
				return m_feedbackID;
			}
			set
			{
				m_feedbackID = value;
			}
		}

		public string FeedbackTitle
		{
			get
			{
				return m_feedbackTitle;
			}
			set
			{
				m_feedbackTitle = value;
			}
		}

		public string FeedbackContent
		{
			get
			{
				return m_feedbackContent;
			}
			set
			{
				m_feedbackContent = value;
			}
		}

		public DateTime FeedbackDate
		{
			get
			{
				return m_feedbackDate;
			}
			set
			{
				m_feedbackDate = value;
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

		public string ClientIP
		{
			get
			{
				return m_clientIP;
			}
			set
			{
				m_clientIP = value;
			}
		}

		public int ViewCount
		{
			get
			{
				return m_viewCount;
			}
			set
			{
				m_viewCount = value;
			}
		}

		public int RevertUserID
		{
			get
			{
				return m_revertUserID;
			}
			set
			{
				m_revertUserID = value;
			}
		}

		public string RevertContent
		{
			get
			{
				return m_revertContent;
			}
			set
			{
				m_revertContent = value;
			}
		}

		public DateTime RevertDate
		{
			get
			{
				return m_revertDate;
			}
			set
			{
				m_revertDate = value;
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

		public byte IsProcessed
		{
			get
			{
				return m_isProcessed;
			}
			set
			{
				m_isProcessed = value;
			}
		}

		public GameFeedbackInfo()
		{
			m_feedbackID = 0;
			m_feedbackTitle = "";
			m_feedbackContent = "";
			m_feedbackDate = DateTime.Now;
			m_userID = 0;
			m_clientIP = "";
			m_viewCount = 0;
			m_revertUserID = 0;
			m_revertContent = "";
			m_revertDate = DateTime.Now;
			m_nullity = 0;
			m_isProcessed = 0;
		}
	}
}

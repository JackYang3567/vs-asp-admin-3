using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class StreamPlayPresent
	{
		public const string Tablename = "StreamPlayPresent";

		public const string _DateID = "DateID";

		public const string _UserID = "UserID";

		public const string _PresentCount = "PresentCount";

		public const string _PresentScore = "PresentScore";

		public const string _PlayTimeCount = "PlayTimeCount";

		public const string _OnLineTimeCount = "OnLineTimeCount";

		public const string _FirstDate = "FirstDate";

		public const string _LastDate = "LastDate";

		private int m_dateID;

		private int m_userID;

		private int m_presentCount;

		private int m_presentScore;

		private int m_playTimeCount;

		private int m_onLineTimeCount;

		private DateTime m_firstDate;

		private DateTime m_lastDate;

		public int DateID
		{
			get
			{
				return m_dateID;
			}
			set
			{
				m_dateID = value;
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

		public int PresentCount
		{
			get
			{
				return m_presentCount;
			}
			set
			{
				m_presentCount = value;
			}
		}

		public int PresentScore
		{
			get
			{
				return m_presentScore;
			}
			set
			{
				m_presentScore = value;
			}
		}

		public int PlayTimeCount
		{
			get
			{
				return m_playTimeCount;
			}
			set
			{
				m_playTimeCount = value;
			}
		}

		public int OnLineTimeCount
		{
			get
			{
				return m_onLineTimeCount;
			}
			set
			{
				m_onLineTimeCount = value;
			}
		}

		public DateTime FirstDate
		{
			get
			{
				return m_firstDate;
			}
			set
			{
				m_firstDate = value;
			}
		}

		public DateTime LastDate
		{
			get
			{
				return m_lastDate;
			}
			set
			{
				m_lastDate = value;
			}
		}

		public StreamPlayPresent()
		{
			m_dateID = 0;
			m_userID = 0;
			m_presentCount = 0;
			m_presentScore = 0;
			m_playTimeCount = 0;
			m_onLineTimeCount = 0;
			m_firstDate = DateTime.Now;
			m_lastDate = DateTime.Now;
		}
	}
}

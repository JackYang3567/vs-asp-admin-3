using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GameConfig
	{
		public const string Tablename = "GameConfig";

		public const string _KindID = "KindID";

		public const string _NoticeChangeGolds = "NoticeChangeGolds";

		public const string _WinExperience = "WinExperience";

		private int m_kindID;

		private long m_noticeChangeGolds;

		private int m_winExperience;

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

		public long NoticeChangeGolds
		{
			get
			{
				return m_noticeChangeGolds;
			}
			set
			{
				m_noticeChangeGolds = value;
			}
		}

		public int WinExperience
		{
			get
			{
				return m_winExperience;
			}
			set
			{
				m_winExperience = value;
			}
		}

		public GameConfig()
		{
			m_kindID = 0;
			m_noticeChangeGolds = 0L;
			m_winExperience = 0;
		}
	}
}

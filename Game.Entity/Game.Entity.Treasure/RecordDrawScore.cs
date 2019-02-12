using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class RecordDrawScore
	{
		public const string Tablename = "RecordDrawScore";

		public const string _DrawID = "DrawID";

		public const string _UserID = "UserID";

		public const string _ChairID = "ChairID";

		public const string _Score = "Score";

		public const string _Grade = "Grade";

		public const string _Revenue = "Revenue";

		public const string _UserMedal = "UserMedal";

		public const string _PlayTimeCount = "PlayTimeCount";

		public const string _InsertTime = "InsertTime";

		private int m_drawID;

		private int m_userID;

		private int m_chairID;

		private long m_score;

		private long m_grade;

		private long m_revenue;

		private int m_userMedal;

		private int m_playTimeCount;

		private DateTime m_insertTime;

		public int DrawID
		{
			get
			{
				return m_drawID;
			}
			set
			{
				m_drawID = value;
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

		public int ChairID
		{
			get
			{
				return m_chairID;
			}
			set
			{
				m_chairID = value;
			}
		}

		public long Score
		{
			get
			{
				return m_score;
			}
			set
			{
				m_score = value;
			}
		}

		public long Grade
		{
			get
			{
				return m_grade;
			}
			set
			{
				m_grade = value;
			}
		}

		public long Revenue
		{
			get
			{
				return m_revenue;
			}
			set
			{
				m_revenue = value;
			}
		}

		public int UserMedal
		{
			get
			{
				return m_userMedal;
			}
			set
			{
				m_userMedal = value;
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

		public DateTime InsertTime
		{
			get
			{
				return m_insertTime;
			}
			set
			{
				m_insertTime = value;
			}
		}

		public RecordDrawScore()
		{
			m_drawID = 0;
			m_userID = 0;
			m_chairID = 0;
			m_score = 0L;
			m_grade = 0L;
			m_revenue = 0L;
			m_userMedal = 0;
			m_playTimeCount = 0;
			m_insertTime = DateTime.Now;
		}
	}
}

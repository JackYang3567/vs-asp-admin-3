using System;

namespace Game.Entity.GameScore
{
	[Serializable]
	public class RecordDrawInfo
	{
		public const string Tablename = "RecordDrawInfo";

		public const string _DrawID = "DrawID";

		public const string _Waste = "Waste";

		public const string _Revenue = "Revenue";

		public const string _Present = "Present";

		public const string _UserMedal = "UserMedal";

		public const string _UserCount = "UserCount";

		public const string _StartTime = "StartTime";

		public const string _ConcludeTime = "ConcludeTime";

		public const string _InsertTime = "InsertTime";

		public const string _DrawCourse = "DrawCourse";

		private int m_drawID;

		private long m_waste;

		private long m_revenue;

		private long m_present;

		private int m_userMedal;

		private int m_userCount;

		private DateTime m_startTime;

		private DateTime m_concludeTime;

		private DateTime m_insertTime;

		private byte[] m_drawCourse;

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

		public long Waste
		{
			get
			{
				return m_waste;
			}
			set
			{
				m_waste = value;
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

		public long Present
		{
			get
			{
				return m_present;
			}
			set
			{
				m_present = value;
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

		public int UserCount
		{
			get
			{
				return m_userCount;
			}
			set
			{
				m_userCount = value;
			}
		}

		public DateTime StartTime
		{
			get
			{
				return m_startTime;
			}
			set
			{
				m_startTime = value;
			}
		}

		public DateTime ConcludeTime
		{
			get
			{
				return m_concludeTime;
			}
			set
			{
				m_concludeTime = value;
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

		public byte[] DrawCourse
		{
			get
			{
				return m_drawCourse;
			}
			set
			{
				m_drawCourse = value;
			}
		}

		public RecordDrawInfo()
		{
			m_drawID = 0;
			m_waste = 0L;
			m_revenue = 0L;
			m_present = 0L;
			m_userMedal = 0;
			m_userCount = 0;
			m_startTime = DateTime.Now;
			m_concludeTime = DateTime.Now;
			m_insertTime = DateTime.Now;
			m_drawCourse = null;
		}
	}
}

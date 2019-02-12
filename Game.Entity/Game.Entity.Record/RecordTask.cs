using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordTask
	{
		public const string Tablename = "RecordTask";

		public const string _RecordID = "RecordID";

		public const string _DateID = "DateID";

		public const string _UserID = "UserID";

		public const string _TaskID = "TaskID";

		public const string _AwardGold = "AwardGold";

		public const string _AwardMedal = "AwardMedal";

		public const string _InputDate = "InputDate";

		private int m_recordID;

		private int m_dateID;

		private int m_userID;

		private int m_taskID;

		private int m_awardGold;

		private int m_awardMedal;

		private DateTime m_inputDate;

		public int RecordID
		{
			get
			{
				return m_recordID;
			}
			set
			{
				m_recordID = value;
			}
		}

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

		public int TaskID
		{
			get
			{
				return m_taskID;
			}
			set
			{
				m_taskID = value;
			}
		}

		public int AwardGold
		{
			get
			{
				return m_awardGold;
			}
			set
			{
				m_awardGold = value;
			}
		}

		public int AwardMedal
		{
			get
			{
				return m_awardMedal;
			}
			set
			{
				m_awardMedal = value;
			}
		}

		public DateTime InputDate
		{
			get
			{
				return m_inputDate;
			}
			set
			{
				m_inputDate = value;
			}
		}

		public RecordTask()
		{
			m_recordID = 0;
			m_dateID = 0;
			m_userID = 0;
			m_taskID = 0;
			m_awardGold = 0;
			m_awardMedal = 0;
			m_inputDate = DateTime.Now;
		}
	}
}

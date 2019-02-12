using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class RecordSpreadInfo
	{
		public const string Tablename = "RecordSpreadInfo";

		public const string _RecordID = "RecordID";

		public const string _UserID = "UserID";

		public const string _Score = "Score";

		public const string _TypeID = "TypeID";

		public const string _ChildrenID = "ChildrenID";

		public const string _InsureScore = "InsureScore";

		public const string _CollectDate = "CollectDate";

		public const string _CollectNote = "CollectNote";

		private int m_recordID;

		private int m_userID;

		private long m_score;

		private int m_typeID;

		private int m_childrenID;

		private long m_insureScore;

		private DateTime m_collectDate;

		private string m_collectNote;

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

		public int ChildrenID
		{
			get
			{
				return m_childrenID;
			}
			set
			{
				m_childrenID = value;
			}
		}

		public long InsureScore
		{
			get
			{
				return m_insureScore;
			}
			set
			{
				m_insureScore = value;
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

		public string CollectNote
		{
			get
			{
				return m_collectNote;
			}
			set
			{
				m_collectNote = value;
			}
		}

		public RecordSpreadInfo()
		{
			m_recordID = 0;
			m_userID = 0;
			m_score = 0L;
			m_typeID = 0;
			m_childrenID = 0;
			m_insureScore = 0L;
			m_collectDate = DateTime.Now;
			m_collectNote = "";
		}
	}
}

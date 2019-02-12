using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordEveryDayRoomData
	{
		public const string Tablename = "RecordEveryDayRoomData";

		public const string _DateID = "DateID";

		public const string _KindID = "KindID";

		public const string _ServerID = "ServerID";

		public const string _Waste = "Waste";

		public const string _Revenue = "Revenue";

		public const string _Medal = "Medal";

		public const string _CollectDate = "CollectDate";

		private int m_dateID;

		private int m_kindID;

		private int m_serverID;

		private long m_waste;

		private long m_revenue;

		private int m_medal;

		private DateTime m_collectDate;

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

		public int ServerID
		{
			get
			{
				return m_serverID;
			}
			set
			{
				m_serverID = value;
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

		public int Medal
		{
			get
			{
				return m_medal;
			}
			set
			{
				m_medal = value;
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

		public RecordEveryDayRoomData()
		{
			m_dateID = 0;
			m_kindID = 0;
			m_serverID = 0;
			m_waste = 0L;
			m_revenue = 0L;
			m_medal = 0;
			m_collectDate = DateTime.Now;
		}
	}
}

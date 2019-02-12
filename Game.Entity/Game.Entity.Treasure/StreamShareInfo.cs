using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class StreamShareInfo
	{
		public const string Tablename = "StreamShareInfo";

		public const string _DateID = "DateID";

		public const string _ShareID = "ShareID";

		public const string _ShareTotals = "ShareTotals";

		public const string _CollectDate = "CollectDate";

		private int m_dateID;

		private int m_shareID;

		private int m_shareTotals;

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

		public int ShareID
		{
			get
			{
				return m_shareID;
			}
			set
			{
				m_shareID = value;
			}
		}

		public int ShareTotals
		{
			get
			{
				return m_shareTotals;
			}
			set
			{
				m_shareTotals = value;
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

		public StreamShareInfo()
		{
			m_dateID = 0;
			m_shareID = 0;
			m_shareTotals = 0;
			m_collectDate = DateTime.Now;
		}
	}
}

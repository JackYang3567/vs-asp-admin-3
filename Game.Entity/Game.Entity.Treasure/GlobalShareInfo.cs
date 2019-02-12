using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class GlobalShareInfo
	{
		public const string Tablename = "GlobalShareInfo";

		public const string _ShareID = "ShareID";

		public const string _ShareName = "ShareName";

		public const string _ShareAlias = "ShareAlias";

		public const string _ShareNote = "ShareNote";

		public const string _CollectDate = "CollectDate";

		private int m_shareID;

		private string m_shareName;

		private string m_shareAlias;

		private string m_shareNote;

		private DateTime m_collectDate;

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

		public string ShareName
		{
			get
			{
				return m_shareName;
			}
			set
			{
				m_shareName = value;
			}
		}

		public string ShareAlias
		{
			get
			{
				return m_shareAlias;
			}
			set
			{
				m_shareAlias = value;
			}
		}

		public string ShareNote
		{
			get
			{
				return m_shareNote;
			}
			set
			{
				m_shareNote = value;
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

		public GlobalShareInfo()
		{
			m_shareID = 0;
			m_shareName = "";
			m_shareAlias = "";
			m_shareNote = "";
			m_collectDate = DateTime.Now;
		}
	}
}
